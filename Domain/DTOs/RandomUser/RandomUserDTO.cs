using System.Diagnostics.CodeAnalysis;

namespace Domain.DTOs.RandomUser {
    /// <summary>
    /// Random user DTO created using https://random-data-api.com/api/v2/
    /// </summary>
    public class RandomUserDTO {
        [NotNull]
        public string? Username { get; set; }
        [NotNull]
        public string? PhotoUrl { get; set; }
    }
}
