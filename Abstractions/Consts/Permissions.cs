namespace DVLD.Abstractions.Consts;

public static class Permissions
{
    public static string Type { get; } = nameof(Permissions);

    public const string GetApplications = "applications:read";
    public const string CreateApplications = "applications:create";
    public const string UpdateApplications = "applications:update";

    public const string GetApplicationTypes = "application-types:read";
    public const string CreateApplicationTypes = "application-types:create";
    public const string UpdateApplicationTypes = "application-types:update";
    public const string DeleteApplicationTypes = "application-types:delete";

    public const string GetDrivers = "drivers:read";
    public const string CreateDrivers = "drivers:create";

    public const string GetDrivingLicenseApplications = "driving-license-applications:read";
    public const string CreateDrivingLicenseApplications = "driving-license-applications:create";

    public const string GetLicenses = "licenses:read";
    public const string CreateLicenses = "licenses:create";
    public const string UpdateLicenses = "licenses:update";

    public const string GetLicenseTypes = "license-types:read";
    public const string CreateLicenseTypes = "license-types:create";
    public const string UpdateLicenseTypes = "license-types:update";

    public const string GetTestAppointments = "test-appointments:read";
    public const string CreateTestAppointments = "test-appointments:create";
    public const string UpdateTestAppointments = "test-appointments:update";

    public const string CreateTests = "tests:create";

    public const string GetRoles = "roles:read";
    public const string CreateRoles = "roles:create";
    public const string UpdateRoles = "roles:update";

    public const string GetTestTypes = "test-types:read";
    public const string CreateTestTypes = "test-types:create";
    public const string UpdateTestTypes = "test-types:update";
    public const string DeleteTestTypes = "test-types:delete";

    public static IReadOnlyList<string> GetAll()
    {
        return typeof(Permissions)
            .GetFields()
            .Select(field => (string)field.GetRawConstantValue()!)
            .ToList();
    }

}
