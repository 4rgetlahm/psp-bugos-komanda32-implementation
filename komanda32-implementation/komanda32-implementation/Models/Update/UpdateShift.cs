﻿namespace komanda32_implementation.Models.Update;
public class UpdateShift
{
    public int? WorkerId { get; set; }
    public int? GroupId { get; set; }
    public int? LocationId { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
