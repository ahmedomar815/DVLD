
namespace DVLD.Entities;

public class License:AuditableEnitty
{
    public string LicenseNumber { get; set; } = Guid.NewGuid().ToString("N")[..5];
    public string ApplicaitonId { get; set; } = null!;
    public int LicenseTypeId { get; set; } 
    public DateOnly IssueDate { get; set; } 
    public DateOnly ExpiryDate { get; set; } 
    public string Notes { get; set; } = null!;
    public string DriverId { get; set; } = null!;
    public decimal PaidFees { get; set; }
    public bool IsActive { get; set; } = true;
    public IssueReason IssueReason { get; set; }
   
    public Application Application { get; set; } = null!;
    public LicenseType LicenseType { get; set; } = null!;
    public Driver Driver { get; set; } = null!;
    



}
