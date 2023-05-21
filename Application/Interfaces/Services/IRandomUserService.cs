using Domain.DTOs.RandomUser;

namespace Application.Interfaces {
    public interface IRandomUserService {
        /// <summary>
        /// Assign a random user using the RandomUser API
        /// </summary>
        /// <param name="UserIdentifier">AnonymousChat User ID (email)</param>
        /// <returns></returns>
       Task<RandomUserDTO> AssignRandomUser(string UserIdentifier);
    }
}
