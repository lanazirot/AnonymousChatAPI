using Application.Interfaces;
using Application.Interfaces.Services;

namespace Infrastructure.Services.User {
    /// <summary>
    /// User service to connect and create users in the StreamIO
    /// </summary>
    public class UserService : IUserService {

        private readonly IStreamIOService _streamIOService;

        public UserService(IStreamIOService streamIOService) {
            _streamIOService = streamIOService;
        }

        public Task<string> ConnectUser(string Email) {
            return _streamIOService.CreateToken(Email);
        }

        public Task CreateUser(string Email) {
            return _streamIOService.CreateUser(Email);
        }
    }
}
