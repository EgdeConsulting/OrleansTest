namespace Demo.Grains;

[Serializable]
[GenerateSerializer]
public class EgdeState
{
    [Id(0)]
    public List<string> Names { get; set; }
    [Id(1)]
    public DateTime LastSaved { get; set; }
    
}