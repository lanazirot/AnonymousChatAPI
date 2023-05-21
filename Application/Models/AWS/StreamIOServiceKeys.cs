namespace Application.Models.AWS {
    /// <summary>
    /// Use these keys to connect to the StreamIO service.
    /// </summary>
    public class StreamIOServiceKeys {
        /// <summary>
        /// The secret key for the StreamIO service.
        /// </summary>
        public string? StreamIOSecret { get; set; }
        /// <summary>
        /// The key for the StreamIO service.
        /// </summary>
        public string? StreamIOKey { get; set; }
    }
}
