namespace Application.Interfaces {
    public interface IUserService {
        /// <summary>
        /// Connect a user to the application
        /// </summary>
        /// <param name="Email">User email</param>
        /// <returns>User token</returns>
        Task<string> ConnectUser(string Email);
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="Email">User email</param>
        Task CreateUser(string Email);
    }
}
