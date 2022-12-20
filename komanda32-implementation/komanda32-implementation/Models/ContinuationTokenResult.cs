namespace komanda32_implementation.Models;

public class ContinuationTokenResult<T>
{
    public T? Response { get; set; }

    public string ContinuationToken { get; set; } = "";
}