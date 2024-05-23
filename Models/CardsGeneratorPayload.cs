namespace CardsGeneratorBackend.Models;

public class CardsGeneratorPayload
{
    private string? _prefix;
    private int? _count;
    private int PurgeValue(int? i, int d) => (int)(!i.HasValue || i == 0 ? d : i);
    private string PurgeValue(string? s, string d) => string.IsNullOrEmpty(s) || s == "string" ? d : s;

    public CardsGeneratorPayload() { }
    public void Deconstruct() => throw new NotImplementedException();
    public void Deconstruct(out int count, out string prefix)
    {
        count = Count;
        prefix = Prefix;
    }

    public int Count
    {
        get => PurgeValue(_count, 15); // => _count ?? 15;
        set => _count = value;
    }
    public string Prefix
    {
        get => PurgeValue(_prefix, "914"); // => _prefix ?? "914";
        set => _prefix = value;
    }

    /*
    1,000,000 -> 14 MB -> (5) 70 MB -> zip 28.9 MB
    2024-05-22_21:31:35 - 2024-05-22_21:31:35

    5,000,000 -> 70 MB -> (5) 350 MB -> zip 144.7 MB
    2024-05-22_21:32:54 - 2024-05-22_21:32:56

    8,000,000 -> 112 MB -> (5) 560 MB -> zip 231.5 MB
    2024-05-22_21:33:30 - 2024-05-22_21:33:33

    10,000,000 -> 140 MB -> (5) 700 MB -> zip 289.3 MB
    2024-05-22_21:34:04 - 2024-05-22_21:34:09
    */
}
