namespace DVLD.Entities;

public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public string ApplicationUserId { get; set; } = default!;
    public DateTime ExpiresOn { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedOn { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

    public bool IsActive => !IsExpired && RevokedOn is null;
}