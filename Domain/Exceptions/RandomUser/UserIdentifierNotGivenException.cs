using Domain.Exceptions.Shared;

namespace Domain.Exceptions.RandomUser {
    /// <summary>
    /// Use this exception when the user identifier was not given
    /// </summary>
    public class UserIdentifierNotGivenException : AppException{
        public UserIdentifierNotGivenException() : base("UserIdentifier was not given") {}
    }
}
