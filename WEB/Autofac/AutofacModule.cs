using Autofac;
using AutoMapper;
using CORE.Interface;
using DATAACCESS.Services;
using WEB.AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RequestMapping());
            });


            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance(mapper);
        }
    }
}
