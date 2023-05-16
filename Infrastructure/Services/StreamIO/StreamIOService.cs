using Application.Interfaces.Services;
using Domain.DTOs.Channel;
using Domain.Enums.Channel;
using StreamChat.Clients;
using StreamChat.Models;
using Utils;

using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Infrastructure.Services.StreamIO {
    public class StreamIOService : IStreamIOService {

        private readonly StreamClientFactory? _clientFactory;

        public StreamIOService() {
            _clientFactory = new StreamClientFactory("szv7syqafk64", "cjuxzedy69cry4wswqruxnv3z4f2whxm6s72vw2t4usm3afnyh3xtr5f6neew58x");
        }




        private static async Task<string> GetSecret() {
            string secretName = "StreamIOApiKeys"; string region = "us-east-2";
            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));
            GetSecretValueRequest request = new() {
                SecretId = secretName,
                VersionStage = "AWSCURRENT",
            };
            GetSecretValueResponse response;
            try {
                response = await client.GetSecretValueAsync(request);
            } catch {
                throw;
            }
            return response.SecretString;
        }

        /// <summary>
        /// Create a new token based in the user email
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns>Token string</returns>
        public Task<string> CreateToken(string Email) {
            var userClient = _clientFactory!.GetUserClient();
            return Task.FromResult(userClient.CreateToken(Email, DateTimeOffset.UtcNow.AddHours(10)));
        }

        /// <summary>
        /// Create a new user in the StreamIO
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns>True if user was created correctly</returns>
        public async Task<bool> CreateUser(string Email) {
            var newUser = new UserRequest {
                Id = Email,
                Role = Role.Admin,
                Name = Email,
            };
            var userClient = _clientFactory!.GetUserClient();
            var response = await userClient.UpsertAsync(newUser);
            return response.Users.Any();
        }

        /// <summary>
        /// Create a new anonymous chat channel
        /// </summary>
        /// <param name="EmailCreatedBy"></param>
        /// <returns></returns>
        public async Task<CreateChannelDTO> CreateChannel(CreateChannelDTO createChannelDTO) {
            var newChannel = new ChannelRequest { CreatedBy = new UserRequest { Id = createChannelDTO.Email } };

            var channelClient = _clientFactory!.GetChannelClient();
            await channelClient.GetOrCreateAsync(ChannelType.MESSAGING.GetString(), "general", new ChannelGetRequest {
                Data = newChannel
            });

            return createChannelDTO;
        }

        /// <summary>
        /// Delete an existing channel
        /// </summary>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteChannel(string ChannelId) {
            //First check if the channel exists
            var channelClient = _clientFactory!.GetChannelClient();
            var response = await channelClient.DeleteAsync(ChannelType.MESSAGING.GetString(), ChannelId);
            return response != null;
        }

        /// <summary>
        /// Add member to an existing channel
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="ChannelId"></param>
        /// <returns></returns>
        public async Task<AddMemberToChannelDTO> AddMemberToChannel(AddMemberToChannelDTO Member) {
            var channelClient = _clientFactory!.GetChannelClient();
            await channelClient.AddMembersAsync(ChannelType.MESSAGING.GetString(), Member.ChannelID, new[] { Member.Email });
            return Member;
        }
    }
}
