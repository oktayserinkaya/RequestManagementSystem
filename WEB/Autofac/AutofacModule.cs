using Autofac;
using AutoMapper;
using CORE.Interface;
using DATAACCESS.Services;
using WEB.AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using BUSINESS.AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly).AsClosedTypesOf(typeof(IBaseManager<,>)).InstancePerLifetimeScope();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RequestMapping());


                mc.AddProfile(new RequestBusinessMapping());
            });


            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance(mapper);
        }
    }
}
