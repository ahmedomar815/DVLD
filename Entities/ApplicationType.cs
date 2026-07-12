namespace DVLD.Entities;

public class ApplicationType
{
   public int Id { get; set; } = default;
    public string Name { get; set; } = default!;
    public decimal Fees { get; set; } = default;
    public ICollection<Application> Applications{ get; set; } = new List<Application>();


} 
