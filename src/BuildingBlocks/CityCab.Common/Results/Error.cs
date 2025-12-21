namespace CityCab.Common.Results
{
    public sealed record Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

        public static Error NotFound(string entity, object id) =>
            new("Error.NotFound", $"{entity} with id {id} was not found");

        public static Error Validation(string message) =>
            new("Error.Validation", message);
    }
}
