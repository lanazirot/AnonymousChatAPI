using Domain.DTOs.Channel;

namespace Application.Interfaces.Services {
    public interface IStreamIOService {
        /// <summary>
        /// Create a new token based in the user email
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns>User token</returns>
        Task<string> CreateToken(string Email);
        /// <summary>
        /// Create a new user in StreamIO using the user email
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns>True, if user was created successfully</returns>
        Task<bool> CreateUser(string Email);
        /// <summary>
        /// Create a new channel in StreamIO 
        /// </summary>
        /// <param name="createChannelDTO">Channel properties</param>
        /// <returns>Same created channel</returns>
        Task<CreateChannelResponseDTO> CreateChannel(CreateChannelDTO createChannelDTO);
        /// <summary>
        /// Delete a channel in StreamIO
        /// </summary>
        /// <param name="ChannelId">Channel ID</param>
        /// <returns>True, if channel was deleted sucessfully</returns>
        Task<bool> DeleteChannel(string ChannelId);
        /// <summary>
        /// Add a member to a channel in StreamIO
        /// </summary>
        /// <param name="addMemberToChannelDTO">Member properties</param>
        /// <returns>Same created member</returns>
        Task<AddMemberToChannelDTO> AddMemberToChannel(AddMemberToChannelDTO addMemberToChannelDTO);
    }
}
