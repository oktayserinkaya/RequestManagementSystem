using Autofac;
using CORE.Interface;
using DATAACCESS.Services;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
        }
    }
}
