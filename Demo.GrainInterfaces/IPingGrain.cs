using Orleans;

namespace Demo.GrainInterfaces;

public interface IPingGrain : IGrainWithStringKey
{
    Task SayHelloWorld();

    Task<string> ReplyWithSomeThings(int numberOfThings);
}