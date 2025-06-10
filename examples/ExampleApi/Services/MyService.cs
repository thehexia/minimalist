using System;

namespace ExampleApi.Services;

public interface IMyService
{
    void DoThings();
}

public class MyService : IMyService
{
    public void DoThings()
    {
        Console.WriteLine("Hi");
    }
}
