using System.Diagnostics.CodeAnalysis;

namespace Domain.DTOs.RandomUser {
    public class RandomUserDTO {
        [NotNull]
        public string? Username { get; set; }
        [NotNull]
        public string? PhotoUrl { get; set; }
    }
}
