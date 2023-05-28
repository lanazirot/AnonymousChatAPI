namespace Utils;
public static class NumberUtils {
    public static double ToRadians(this double degrees) {
        return degrees * Math.PI / 180;
    }
    public static double ToRadians(this float degrees) {
        return degrees * Math.PI / 180;
    }
}

