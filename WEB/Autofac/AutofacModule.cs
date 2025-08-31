using Autofac;
using AutoMapper;

using BUSINESS.AutoMapper;
using WEB.AutoMapper;

using CORE.Interface;
using BUSINESS.Manager.Interface;
using BUSINESS.Manager.Concrete;
using DATAACCESS.Services; 
using CORE.IdentityEntities;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // =========================
            // Repositories
            // =========================

            
            builder.RegisterGeneric(typeof(BaseService<>))
                   .As(typeof(IBaseRepository<>))
                   .InstancePerLifetimeScope();

            
            builder.RegisterType<UserService>()
                   .As<IUserRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<RequestService>()
                   .As<IRequestRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeService>()
                   .As<IEmployeeRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>()
                   .As<ICategoryReposiitory>() 
                   .InstancePerLifetimeScope();

            builder.RegisterType<SubCategoryService>()
                   .As<ISubCategoryRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ProductService>()
                   .As<IProductRepository>()
                   .InstancePerLifetimeScope();

           
            builder.RegisterType<RoleService>()
                   .As<IRoleRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<DepartmentService>()
                   .As<IDepartmentRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<WarehouseService>()
                   .As<IWarehouseRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PaymentService>()
                   .As<IPaymentRepository>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PurchaseService>()
                   .As<IPurchaseRepository>()
                   .InstancePerLifetimeScope();

            // =========================
            // Managers (Business katmanı)
            // =========================
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<RequestManager>().As<IRequestManager>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentManager>().As<IDepartmentManager>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseManager>().As<IWarehouseManager>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentManager>().As<IPaymentManager>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryManager>().As<ICategoryManager>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryManager>().As<ISubCategoryManager>().InstancePerLifetimeScope();
            builder.RegisterType<ProductManager>().As<IProductManager>().InstancePerLifetimeScope();
            builder.RegisterType<PurchaseManager>().As<IPurchaseManager>().InstancePerLifetimeScope();

            // =========================
            // AutoMapper
            // =========================
            var mappingAssemblies = new[]
            {
                typeof(RequestBusinessMapping).Assembly, 
                typeof(RequestMapping).Assembly,
                typeof(RoleBusinessMapping).Assembly,
                typeof(WEB.AutoMapper.RoleMapping).Assembly
            };

            builder.Register(ctx => new MapperConfiguration(cfg => cfg.AddMaps(mappingAssemblies)))
                   .AsSelf()
                   .SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve))
                   .As<IMapper>()
                   .InstancePerLifetimeScope();
        }
    }
}
