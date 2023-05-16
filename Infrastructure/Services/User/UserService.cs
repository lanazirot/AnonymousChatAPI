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

        /// <summary>
        /// Connect the user to the StreamIO
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns></returns>
        public Task<string> ConnectUser(string Email) {
            return _streamIOService.CreateToken(Email);
        }

        /// <summary>
        /// Create a new user in the StreamIO
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns></returns>
        public Task CreateUser(string Email) {
            return _streamIOService.CreateUser(Email);
        }
    }
}
