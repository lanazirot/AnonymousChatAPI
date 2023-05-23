using Application.Interfaces.Internal;
using Domain.DTOs.Channel;
using Utils;
namespace Application.Implementations.Internal {
    public class GeoLocation : IGeoLocation {
        public double CalculateDistance(LatLong originalCoordenates, LatLong givenCoordenates) {
            double lat1Rad = originalCoordenates.Lat.ToRadians();
            double lon1Rad = originalCoordenates.Long.ToRadians();
            double lat2Rad = givenCoordenates.Lat.ToRadians();
            double lon2Rad = givenCoordenates.Long.ToRadians();
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
