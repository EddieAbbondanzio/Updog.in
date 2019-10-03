using System;
using Updog.Application;

namespace Updog.Api {
    public sealed class ConsoleOutputPort : IOutputPort {
        public void BadInput() => Console.WriteLine("Bad input.");

        public void BadInput<TResult>(TResult? result = null) where TResult : class => Console.WriteLine("Bad input: ", result);

        public void InvalidOperation() => Console.WriteLine("Invalid operation");

        public void InvalidOperation<TResult>(TResult? result = null) where TResult : class => Console.WriteLine("Invalid operation: ", result);

        public void NotFound() => Console.WriteLine("Not found.");

        public void NotFound<TResult>(TResult? result = null) where TResult : class => Console.WriteLine("Not found: ", result);

        public void Success() => Console.WriteLine("Success");

        public void Success<TResult>(TResult? result = null) where TResult : class => Console.WriteLine("Success: ", result);

        public void Unauthorized() => Console.WriteLine("Unauthorized");

        public void Unauthorized<TResult>(TResult? result = null) where TResult : class => Console.WriteLine("Unauthorized: ", result);
    }
}