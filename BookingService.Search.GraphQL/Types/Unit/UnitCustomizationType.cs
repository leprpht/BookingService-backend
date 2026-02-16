namespace BookingService.Search.GraphQL.Types.Unit;

public class UnitCustomizationType
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public List<string> Text { get; set; } = new();
}