namespace Application.Interfaces {
    public interface IUserService {
        Task<string> ConnectUser(string Email);
        Task CreateUser(string Email);
    }
}
