using Autofac;
using AutoMapper;
using BUSINESS.AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Interface;
using DATAACCESS.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using WEB.AutoMapper;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly).AsClosedTypesOf(typeof(IBaseRepository<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly).AsClosedTypesOf(typeof(IBaseManager<,>)).InstancePerLifetimeScope();

            builder.RegisterType<RoleService>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<RequestManager>().As<IRequestManager>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentManager>().As<IDepartmentManager>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseManager>().As<IWarehouseManager>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentManager>().As<IPaymentManager>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().InstancePerLifetimeScope();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RequestMapping());
                mc.AddProfile(new RoleMapping());
                mc.AddProfile(new UserMapping());


                mc.AddProfile(new RequestBusinessMapping());
                mc.AddProfile(new RoleBusinessMapping());
                mc.AddProfile(new UserBusinessMapping());
            });


            IMapper mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance(mapper);
        }
    }
}
