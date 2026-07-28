namespace DVLD.Entities;

public class ApplicationType
{
   public int Id { get; init; } = default;
    public string Name { get; set; } = default!;
    public decimal Fees { get; set; } = default;
    public bool IsActive { get; set; } = true;
    public ICollection<Application> Applications { get; set; } = [];


} 
