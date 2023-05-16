namespace Domain.Exceptions.User {
    /// <summary>
    /// Throw an exception when the email is not valid as Anonymous Chat required.
    /// </summary>
    public class BadParsedEmailException : Exception {
        public BadParsedEmailException() : base("The email is not valid as Anonymous Chat required.") { }
    }
}
