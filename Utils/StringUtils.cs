namespace Utils {
    public static class StringUtils {
        /// <summary>
        /// Use this method to check if a string is null, empty or whitespace.
        /// </summary>
        /// <param name="str">A string</param>
        /// <returns>True if string was either null or empty or contains whitespaces</returns>
        public static bool IsEmpty(this string str) {
            return string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        }
    }
}