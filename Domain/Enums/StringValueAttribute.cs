namespace Domain.Enums {
    /// <summary>
    /// Use this attribute to give an enum a string value
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class StringValueAttribute : Attribute {
        public string Value { get; }

        public StringValueAttribute(string value) {
            Value = value;
        }
    }
}
