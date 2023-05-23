namespace Domain.DTOs.Channel {
    /// <summary>
    /// Create a new channel
    /// </summary>
    /// <param name="Email">Created by user email</param>
    public record CreateChannelDTO(string Email, LatLong Coords);
}
