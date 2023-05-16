namespace Infrastructure.Constants {
    public static class RequestURL {
        public static string RANDOM_USER = "https://random-data-api.com/api/v2/";

        public static Uri GetRandomUserURI() {
            return new Uri(RANDOM_USER);
        }

    }
}
