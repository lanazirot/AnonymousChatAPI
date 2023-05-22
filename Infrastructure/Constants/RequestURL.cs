namespace Infrastructure.Constants {
    public static class RequestURL {
        public static string RANDOM_DATA_API = "https://random-data-api.com/api/v2/";
        public static Uri GetRandomUserURI() {
            return new Uri(RANDOM_DATA_API);
        }
        public static Uri GetRandomChannelURI() {
            return new Uri(RANDOM_DATA_API);
        }

    }
}
