using Newtonsoft.Json;
namespace Domain.DTOs.RandomUser {
    /// <summary>
    /// Using https://random-data-api.com/api/v2/ to generate random user data.
    /// </summary>
    public partial class RandomUserRequestDTO {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("first_name")]
        public string? FirstName { get; set; }

        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("avatar")]
        public Uri? Avatar { get; set; }

        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        [JsonProperty("social_insurance_number")]
        public string? SocialInsuranceNumber { get; set; }

        [JsonProperty("date_of_birth")]
        public DateTimeOffset? DateOfBirth { get; set; }

        [JsonProperty("employment")]
        public Employment? Employment { get; set; }

        [JsonProperty("address")]
        public Address? Address { get; set; }

        [JsonProperty("credit_card")]
        public CreditCard? CreditCard { get; set; }

        [JsonProperty("subscription")]
        public Subscription? Subscription { get; set; }
    }

    public partial class Address {
        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("street_name")]
        public string? StreetName { get; set; }

        [JsonProperty("street_address")]
        public string? StreetAddress { get; set; }

        [JsonProperty("zip_code")]
        public string? ZipCode { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates? Coordinates { get; set; }
    }

    public partial class Coordinates {
        [JsonProperty("lat")]
        public double? Lat { get; set; }

        [JsonProperty("lng")]
        public double? Lng { get; set; }
    }

    public partial class CreditCard {
        [JsonProperty("cc_number")]
        public string? CcNumber { get; set; }
    }

    public partial class Employment {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("key_skill")]
        public string? KeySkill { get; set; }
    }

    public partial class Subscription {
        [JsonProperty("plan")]
        public string? Plan { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("payment_method")]
        public string? PaymentMethod { get; set; }

        [JsonProperty("term")]
        public string? Term { get; set; }
    }
}
