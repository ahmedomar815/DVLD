using Org.BouncyCastle.Tls;

namespace DVLD.Contracts.ApplicationRole;

public record RoleRequest(string Name, IEnumerable<string> Permissions);
