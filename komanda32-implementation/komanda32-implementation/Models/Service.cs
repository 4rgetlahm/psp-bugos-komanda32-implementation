namespace komanda32_implementation.Models;

public class Service
{
    public int Id { get; set; }
    public bool IsProduct { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int FranciseId { get; set; }
    public decimal PriceBeforeTaxes { get; set; }
    public int TaxeId { get; set; }
    public int Size { get; set; }
    public int Category { get; set; }
    public int WorkerId { get; set; }
    public DateTime Time { get; set; }
    public DateTime ReservationTime { get; set; }
    public decimal Discount { get; set; }
    public DiscountType DiscountType { get; set; }
}