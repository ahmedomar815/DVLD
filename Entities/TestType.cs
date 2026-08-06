namespace DVLD.Entities;

public class TestType
{
    public int Id { get; set; }
    public string Title { get; set;  } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Fees { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<TestAppointment> TestAppointments { get; set; } = [];
}
