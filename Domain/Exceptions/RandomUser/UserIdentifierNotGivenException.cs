using Domain.Exceptions.Shared;

namespace Domain.Exceptions.RandomUser {
    public class UserIdentifierNotGivenException : AppException{
        public UserIdentifierNotGivenException() : base("UserIdentifier was not given") {}
    }
}
