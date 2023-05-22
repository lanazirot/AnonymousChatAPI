namespace Domain.DTOs.Channel {
    public record CreateChannelResponseDTO {
        public string? CreatedBy { get; set; }
        public string? RandomName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
