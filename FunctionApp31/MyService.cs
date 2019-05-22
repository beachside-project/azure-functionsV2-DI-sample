using System;

namespace FunctionApp31
{
    public interface IMyService
    {
        Guid MyId { get; }
        string Hello();
    }

    public class MyService : IMyService
    {
        public Guid MyId { get; } = Guid.NewGuid();

        public string Hello() => "Hello";
    }
}