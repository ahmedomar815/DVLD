namespace DVLD.Entities;

public class TestType
{
    public int Id { get; set; }
    public string TestTypeTitle { get; set;  } = string.Empty;
    public string TestTypeDescription { get; set; } = string.Empty;
    public decimal TestTypeFees { get; set; }
    public ICollection<TestAppointment> TestAppointments { get; set; } = [];
}
