﻿namespace Domain.Enums {
    [AttributeUsage(AttributeTargets.All)]
    public class StringValueAttribute : Attribute {
        public string Value { get; }

        public StringValueAttribute(string value) {
            Value = value;
        }
    }
}
