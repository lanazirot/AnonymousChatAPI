using Domain.DTOs.Channel;

namespace Application.Interfaces.Services {
    public interface IStreamIOService {
        Task<string> CreateToken(string Email);
        Task<bool> CreateUser(string Email);
        Task<CreateChannelDTO> CreateChannel(CreateChannelDTO createChannelDTO);
        Task<bool> DeleteChannel(string ChannelId);
        Task<AddMemberToChannelDTO> AddMemberToChannel(AddMemberToChannelDTO addMemberToChannelDTO);
    }
}
