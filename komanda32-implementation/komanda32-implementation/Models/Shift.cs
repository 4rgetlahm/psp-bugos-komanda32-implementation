namespace komanda32_implementation.Models;

public class Shift
{
    public int WorkerId { get; set; }
    public int ShiftId { get; set; }
    public int? GroupId { get; set; }
    public int? LocationId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}