using Domain.DTOs.RandomUser;

namespace Application.Interfaces {
    public interface IRandomUserService {
       Task<RandomUserDTO> AssignRandomUser(string UserIdentifier);
    }
}
