namespace Demo.GrainInterfaces;

public interface IEgdeGrain : IGrainWithStringKey
{
    Task<int> HowManyWithName(string name);
    Task RegisterName(string name);
}