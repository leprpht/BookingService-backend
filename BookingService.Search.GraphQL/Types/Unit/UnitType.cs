namespace BookingService.Search.GraphQL.Types.Unit;

public class UnitType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public double Price { get; set; }
    public int Size { get; set; }
    public List<string> Pictures { get; set; } = new();
    public List<UnitCustomizationType> Customizations { get; set; } = new();
}