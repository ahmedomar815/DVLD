using Mapster;

public class MyRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<TSource, TDestination>();
        config.NewConfig<ApplicationTypeRequest, ApplicationType>()
            .Map(dest => dest.Name, src => src.Name.Trim());
    }
}