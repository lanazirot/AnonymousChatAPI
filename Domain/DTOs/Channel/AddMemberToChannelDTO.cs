namespace Domain.DTOs.Channel {
    /// <summary>
    /// DTO for adding a member to a channel.
    /// </summary>
    /// <param name="ChannelID">Channel ID to be added</param>
    /// <param name="Email">User email</param>
    public record AddMemberToChannelDTO(string ChannelID, string Email);
}
