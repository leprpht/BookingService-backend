namespace BookingService.Search.GraphQL.Types.Unit;

public class UnitListType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public double Price { get; set; }
    public int Size { get; set; }
    public bool IsAvailable { get; set; }
}