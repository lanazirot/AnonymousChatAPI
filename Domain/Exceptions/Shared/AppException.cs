namespace Domain.Exceptions.Shared {
    public class AppException : Exception{
        public AppException(string message) : base(message) { }
    }
}
