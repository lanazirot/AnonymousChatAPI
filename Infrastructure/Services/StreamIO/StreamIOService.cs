using Application.Interfaces.Internal;
using Application.Interfaces.Services;
using Application.Models.AWS;
using Domain.DTOs.Channel;
using Domain.DTOs.User;
using Domain.Enums.Channel;
using Domain.Enums.User;
using Domain.Exceptions.Channel;
using Domain.Exceptions.RandomChannel;
using Microsoft.Extensions.Options;
using StreamChat.Clients;
using StreamChat.Models;
using Utils;

namespace Infrastructure.Services.StreamIO {
    /// <summary>
    /// Use this service to interact with StreamIO
    /// </summary>
    public class StreamIOService : IStreamIOService {

        private readonly StreamClientFactory? _clientFactory;
        private readonly StreamIOServiceKeys _streamIOCredentials;
        private readonly IRandomChannelService _randomChannelService;
        private readonly IGeoLocation _geoLocation;

        /// <summary>
        /// Create a new instance of the StreamIOService, using IAM credentials
        /// </summary>
        /// <param name="options">StreamIOServiceKeys injected by AWS</param>
        public StreamIOService(IOptions<StreamIOServiceKeys> options, IRandomChannelService randomChannelService, IGeoLocation geoLocation) {
            _streamIOCredentials = options.Value;
            _clientFactory = new StreamClientFactory(_streamIOCredentials.StreamIOSecret, _streamIOCredentials.StreamIOKey);
            _randomChannelService = randomChannelService;
            _geoLocation = geoLocation;
        }


        public Task<string> CreateToken(string Email) {
            var userClient = _clientFactory!.GetUserClient();
            return Task.FromResult(userClient.CreateToken(Email, DateTimeOffset.UtcNow.AddHours(10)));
        }


        public async Task<bool> CreateUser(string Email) {
            var newUser = new UserRequest {
                Id = Email,
                Role = UserStreamRole.Anon.GetString(),
                Name = Email,
            };
            var userClient = _clientFactory!.GetUserClient();
            var response = await userClient.UpsertAsync(newUser);
            return response.Users.Any();
        }

        public async Task<CreateChannelResponseDTO> CreateChannel(CreateChannelDTO createChannelDTO) {
            var newChannel = new ChannelRequest { CreatedBy = new UserRequest { Id = createChannelDTO.Email } };

            if (createChannelDTO.Coords.Latitude.ToString().IsEmpty() || createChannelDTO.Coords.Longitude.ToString().IsEmpty())
                throw new ChannelCoordenatesNotFound("Channel coordenates not found");

            newChannel.SetData("latitude", createChannelDTO.Coords.Latitude);
            newChannel.SetData("longitude", createChannelDTO.Coords.Longitude);

            var channelClient = _clientFactory!.GetChannelClient();
            var randomChannelName = await _randomChannelService.GetRandomChannelName();
            await channelClient.GetOrCreateAsync(ChannelType.MESSAGING.GetString(), randomChannelName.Name, new ChannelGetRequest { Data = newChannel });
            await channelClient.AddMembersAsync(ChannelType.MESSAGING.GetString(), randomChannelName.Name, new[] { createChannelDTO.Email });

            return new CreateChannelResponseDTO {
                RandomName = randomChannelName.Name,
                CreatedBy = createChannelDTO.Email
            };
        }

        public async Task<bool> DeleteChannel(string ChannelId) {
            var channelClient = _clientFactory!.GetChannelClient();
            var response = await channelClient.DeleteAsync(ChannelType.MESSAGING.GetString(), ChannelId);
            return response != null;
        }

        public async Task<AddMemberToChannelDTO> AddMemberToChannel(AddMemberToChannelDTO Member) {
            var channelClient = _clientFactory!.GetChannelClient();
            await channelClient.AddMembersAsync(ChannelType.MESSAGING.GetString(), Member.ChannelID, new[] { Member.Email });
            return Member;
        }

        private Task<bool> LocalDeleteMember(string ChannelId, string MemberId) {
            var channelClient = _clientFactory!.GetChannelClient();
            return Task.FromResult(channelClient.RemoveMembersAsync(ChannelType.MESSAGING.GetString(), ChannelId, new[] { MemberId }).IsCompletedSuccessfully);
        }

        public Task<bool> DeleteMember(string ChannelId, string MemberId) {
            var channel = ChannelId.Split(":")[1];
            return LocalDeleteMember(channel, MemberId);
        }

        public Task<bool> CheckIfUserStillInTheRoomByItsCurrentLocation(string ChannelId, UserCoordinatesDTO userCoordinatesDTO) {
            var channelClient = _clientFactory!.GetChannelClient();

            var channelResponse = channelClient.QueryChannelsAsync(QueryChannelsOptions.Default.WithFilter(new Dictionary<string, object> { { "cid", ChannelId } }));
            var channel = channelResponse.Result.Channels.FirstOrDefault()!.Channel ?? throw new ChannelNotFoundException("Channel not found");
            var latLongChannel = new LatLongDTO(channel.GetData<double>("latitude"), channel.GetData<double>("longitude"));
            var distance = _geoLocation.CalculateDistance(latLongChannel, userCoordinatesDTO.CurrentCoords);

            if (distance > IGeoLocation.MaxRadiusFromOrigin) {
                DeleteMember(ChannelId, userCoordinatesDTO.Email);
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public async Task RevealNewChatForCurrentUser(ChannelMemberDTO channelMemberDTO) {

            var channelClient = _clientFactory!.GetChannelClient();
            var userCurrentLocation = new LatLongDTO(channelMemberDTO.CurrentCoords.Latitude, channelMemberDTO.CurrentCoords.Longitude);

            var channelsResponse = await channelClient.QueryChannelsAsync(QueryChannelsOptions.Default.WithFilter(new Dictionary<string, object> { { "type", ChannelType.MESSAGING.GetString()! } }));
            var channels = channelsResponse.Channels;

            channels.ForEach(async channel => {
                var latitude = channel.Channel.GetData<double>("latitude");
                var longitude = channel.Channel.GetData<double>("longitude");
                var channelLocation = new LatLongDTO(latitude, longitude);
                var distance = _geoLocation.CalculateDistance(userCurrentLocation, channelLocation);
                var isInRange = distance <= IGeoLocation.MaxRadiusFromOrigin;
                if (!isInRange) await LocalDeleteMember(channel.Channel.Id, channelMemberDTO.Email);
                else await AddMemberToChannel(new AddMemberToChannelDTO(channel.Channel.Id, channelMemberDTO.Email));
            });
        }
    }
}
