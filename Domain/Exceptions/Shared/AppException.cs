namespace Domain.Exceptions.Shared {
    /// <summary>
    /// General app exception
    /// </summary>
    public class AppException : Exception{
        public AppException(string message) : base(message) { }
    }
}
