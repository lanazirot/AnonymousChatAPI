﻿using Domain.DTOs.Channel;
namespace Application.Interfaces.Internal;
public interface IGeoLocation {
    /// <summary>
    /// Calculate the distance between two coordenates
    /// </summary>
    /// <param name="originalCoordenates">Original coordenates</param>
    /// <param name="givenCoordenates">Given coordenates</param>
    /// <returns>Distance between both coordenates using Haversine formule</returns>
    public double CalculateDistance(LatLongDTO originalCoordenates, LatLongDTO givenCoordenates);
    /// <summary>
    /// Earth radius in meters
    /// </summary>
    public const int EarthRadiusInMeters = 6371000;
    /// <summary>
    /// Use this constant to compare when a user is in the same channel
    /// </summary>
    public const float MaxRadiusFromOrigin = 100.0f;
}
