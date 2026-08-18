using DVLD.Contracts.License;
using DVLD.Contracts.LicenseType;
using DVLD.Contracts.User;
using Mapster;

public class MyRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationTypeRequest, ApplicationType>()
            .Map(dest => dest.Name, src => src.Name.Trim());

        config.NewConfig<LicenseTypeRequest, LicenseType>()
            .Map(dest => dest.Name, src => src.Name.Trim())
            .Map(dest => dest.Description, src => src.Description.Trim());

        config.NewConfig<License, LicneseResponse>()
            .Map(dest => dest.Status, src => src.IsActive ? "IsActive" : "Disabled");
        config.NewConfig<UserRequest, ApplicationUser>().
            Map(dest => dest.UserName, src => src.Email);
            

    }
}
