namespace DVLD.Contracts.ApplicationRole;

public record RoleDetailsResponse(string Id, string Name, bool IsDeleted, IEnumerable<string> permission);
