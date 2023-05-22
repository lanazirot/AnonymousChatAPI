using Application.Interfaces.Services;
using Domain.DTOs.RandomChannel;
using Infrastructure.Constants;
using RestSharp;

namespace Infrastructure.Services.RandomChannel {
    public class RandomChannelService : IRandomChannelService {
        public async Task<RandomChannelDTO> GetRandomChannelName() {
            var randomChannelName = new RandomChannelDTO();
            var options = new RestClientOptions(RequestURL.GetRandomUserURI());
            var client = new RestClient(options);
            var request = new RestRequest("appliances", Method.Get);
            var response = await client.GetAsync<RandomChannelRequestDTO>(request);
            randomChannelName.Name = $"{response!.Brand}-{response!.Equipment}-{response!.Id}".ToLower().Replace(" ", "-");
            return randomChannelName!;
        }
    }
}
