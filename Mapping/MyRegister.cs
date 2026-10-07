using DVLD.Application.Features.Applications.Queries.GetApplication;

public class MyRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateApplicationDto, CreateApplicationCommand>();
        config.NewConfig<CreateApplicationTypeDto, CreateApplicationTypeCommand>();
        config.NewConfig<UpdateApplicationTypeDto, UpdateApplicationTypeCommand>()
            .Ignore(dest => dest.ApplicationTypeId);
        config.NewConfig<CreateDriverDto, CreateDriverCommand>();
        config.NewConfig<CreateLicenseDto, CreateLicenseCommand>();
        config.NewConfig<UpdateLicenseDto, UpdateLicenseCommand>()
            .Ignore(dest => dest.LicenseNumber);
        config.NewConfig<DVLD.Application.Features.ApplicationTypes.Commands.CreateApplicationType.ApplicationTypeResponse, ApplicationTypeDto>();
        config.NewConfig<CountryResponse, CountryDto>();
        config.NewConfig<LicneseResponse, LicenseDto>();
        config.NewConfig<ApplicationResponse, ApplicationDto>();
        config.NewConfig<UserResponse, ApplicationUserDto>();
    }
}
