namespace Utils {
    public static class StringUtils {
        public static bool IsEmpty(this string str) {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
    }
}