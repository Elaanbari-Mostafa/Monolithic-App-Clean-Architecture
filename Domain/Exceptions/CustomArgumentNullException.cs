public static class CustomArgumentNullException
    {
        public static T ThrowIfNull<T>(T value) => value ?? throw new ArgumentNullException(nameof(value));
    }
