using Domain.DTOs.Channel;
namespace Domain.DTOs.User;
public record UserCoordinatesDTO(string Email, LatLongDTO CurrentCoords, string ChannelID);
