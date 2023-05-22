using Domain.DTOs.RandomChannel;

namespace Application.Interfaces.Services {
    public interface IRandomChannelService {
        Task<RandomChannelDTO> GetRandomChannelName();
    }
}
