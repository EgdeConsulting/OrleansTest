using Demo.GrainInterfaces;
using Orleans.Runtime;

namespace Demo.Grains;

public class EgdeGrain : Grain, IEgdeGrain
{
    private readonly IPersistentState<EgdeState> _egde;

    public EgdeGrain([PersistentState("egde")] IPersistentState<EgdeState> egde
    )
    {
        _egde = egde;
    }

    public Task<int> HowManyWithName(string name)
    {
        if (_egde.State.Names != null)
            return Task.FromResult(_egde.State.Names.Count(x => x == name));
        return Task.FromResult(0);
    }

    public async Task RegisterName(string name)
    {
        // if(_egde.State.Names == null)
        //     _egde.State.Names= new List<string>();
        _egde.State.Names.Add(name);
        await _egde.WriteStateAsync();
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        if (_egde.State.Names == null) _egde.State.Names = new List<string>();
        return base.OnActivateAsync(cancellationToken);
    }
}