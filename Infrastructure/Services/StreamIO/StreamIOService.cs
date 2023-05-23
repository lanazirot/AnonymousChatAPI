using Application.Interfaces.Internal;
using Application.Interfaces.Services;
using Application.Models.AWS;
using Domain.DTOs.Channel;
using Domain.Enums.Channel;
using Domain.Enums.User;
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
        public StreamIOService(IOptions<StreamIOServiceKeys> options, IRandomChannelService randomChannelService, IGeoLocation geoLocation ) {
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
            var newChannel = new ChannelRequest { CreatedBy = new UserRequest { Id = createChannelDTO.Email }  };

            if (createChannelDTO.Coords.Lat.ToString().IsEmpty() || createChannelDTO.Coords.Long.ToString().IsEmpty())
                throw new ChannelCoordenatesNotFound("Channel coordenates not found");

            newChannel.SetData("latitude", createChannelDTO.Coords.Lat);
            newChannel.SetData("longitude", createChannelDTO.Coords.Long);

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

        public Task<bool> DeleteMember(string ChannelId, string MemberId) {
            var channelClient = _clientFactory!.GetChannelClient();
            return Task.FromResult(channelClient.RemoveMembersAsync(ChannelType.MESSAGING.GetString(), ChannelId, new[] { MemberId }).IsCompletedSuccessfully);
        }

        public Task<ChannelDTO>? RevealNewChatForCurrentUser(ChannelMemberDTO channelMemberDTO) {




            return null;
        }
    }
}
