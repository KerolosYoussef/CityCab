namespace CityCab.Common.Results
{
    public class Result
    {
        // 1. Constructor is protected to prevent invalid state creation
        protected Result(bool isSuccess, Error? error)
        {
            // Optional: Add a Guard here to ensure state consistency
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("Success result cannot have an error.");

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("Failure result must have an error.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess; // Handy helper
        public Error? Error { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        // Private constructor for internal factory use
        private Result(bool isSuccess, T? value, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        // 3. Safe access: Throw if accessed on failure
        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        public static Result<T> Success(T value) => new(true, value, Error.None);

        // 2. 'new' keyword fixes the shadowing warning
        public new static Result<T> Failure(Error error) => new(false, default, error);
    }
}