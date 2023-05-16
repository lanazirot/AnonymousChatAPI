using Application.Interfaces;
using Domain.DTOs.RandomUser;
using Domain.Exceptions.RandomUser;
using Infrastructure.Constants;
using RestSharp;
using Utils;

namespace Infrastructure.Services.RandomUsername {
    public class RandomUserService : IRandomUserService {
        public async Task<RandomUserDTO> AssignRandomUser(string UserIdentifier) {
            if (UserIdentifier.IsEmpty()) throw new UserIdentifierNotGivenException();
            var randomUserDTO = new RandomUserDTO();

            var options = new RestClientOptions(RequestURL.GetRandomUserURI());
            var client = new RestClient(options);
            var request = new RestRequest("users", Method.Get);
            var response = await client.GetAsync<RandomUserRequestDTO>(request);
            
            randomUserDTO.Username = response!.Username;
            randomUserDTO.PhotoUrl = response!.Avatar!.AbsoluteUri;

            return randomUserDTO!;
        }
    }
}
