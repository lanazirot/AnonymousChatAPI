using Application.Interfaces.Services;
using Application.Models.AWS;
using Domain.DTOs.Channel;
using Domain.Enums.Channel;
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

        /// <summary>
        /// Create a new instance of the StreamIOService, using IAM credentials
        /// </summary>
        /// <param name="options">StreamIOServiceKeys injected by AWS</param>
        public StreamIOService(IOptions<StreamIOServiceKeys> options) {
            _streamIOCredentials = options.Value;
            _clientFactory = new StreamClientFactory(_streamIOCredentials.StreamIOSecret,_streamIOCredentials.StreamIOKey);
        }


        public Task<string> CreateToken(string Email) {
            var userClient = _clientFactory!.GetUserClient();
            return Task.FromResult(userClient.CreateToken(Email, DateTimeOffset.UtcNow.AddHours(10)));
        }


        public async Task<bool> CreateUser(string Email) {
            var newUser = new UserRequest {
                Id = Email,
                Role = Role.Anonymous,
                Name = Email,
            };
            var userClient = _clientFactory!.GetUserClient();
            var response = await userClient.UpsertAsync(newUser);
            return response.Users.Any();
        }

        public async Task<CreateChannelDTO> CreateChannel(CreateChannelDTO createChannelDTO) {
            var newChannel = new ChannelRequest { CreatedBy = new UserRequest { Id = createChannelDTO.Email } };

            var channelClient = _clientFactory!.GetChannelClient();
            await channelClient.GetOrCreateAsync(ChannelType.MESSAGING.GetString(), "general", new ChannelGetRequest {
                Data = newChannel
            });

            return createChannelDTO;
        }

        public async Task<bool> DeleteChannel(string ChannelId) {
            //First check if the channel exists
            var channelClient = _clientFactory!.GetChannelClient();
            var response = await channelClient.DeleteAsync(ChannelType.MESSAGING.GetString(), ChannelId);
            return response != null;
        }

        public async Task<AddMemberToChannelDTO> AddMemberToChannel(AddMemberToChannelDTO Member) {
            var channelClient = _clientFactory!.GetChannelClient();
            await channelClient.AddMembersAsync(ChannelType.MESSAGING.GetString(), Member.ChannelID, new[] { Member.Email });
            return Member;
        }
    }
}
