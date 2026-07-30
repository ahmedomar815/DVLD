using DVLD.Contracts.LicenseType;
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
    }
}
