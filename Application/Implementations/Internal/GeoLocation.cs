using Application.Interfaces.Internal;
using Domain.DTOs.Channel;
using Utils;
namespace Application.Implementations.Internal {
    public class GeoLocation : IGeoLocation {
        public double CalculateDistance(LatLongDTO originalCoordenates, LatLongDTO givenCoordenates) {
            double lat1Rad = originalCoordenates.Latitude.ToRadians();
            double lon1Rad = originalCoordenates.Longitude.ToRadians();
            double lat2Rad = givenCoordenates.Latitude.ToRadians();
            double lon2Rad = givenCoordenates.Longitude.ToRadians();
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;
            double a = Math.Pow(Math.Sin(deltaLat / 2), 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Pow(Math.Sin(deltaLon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = IGeoLocation.EarthRadiusInMeters * c;

            return distance;
        }
    }
}
