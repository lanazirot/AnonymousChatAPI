namespace Domain.DTOs.Channel {
    public record CreateChannelResponseDTO {
        public string? CreatedBy { get; set; }
        public string? RandomName { get; set; }
        public string? CreatedAt { get; set; } = DateTime.Now.ToString();
        public string? Cid { get; set; }
    }
}
