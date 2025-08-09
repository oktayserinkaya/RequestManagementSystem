using Autofac;
using AutoMapper;

// Mapping profilleri
using BUSINESS.AutoMapper;
using WEB.AutoMapper;

// Servis/manager arayüz ve sınıfları
using CORE.Interface;
using BUSINESS.Manager.Interface;
using BUSINESS.Manager.Concrete;
using DATAACCESS.Services;

namespace WEB.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // ---------- Generic repo/service ----------
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly)
                   .AsClosedTypesOf(typeof(IBaseRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly)
                   .AsClosedTypesOf(typeof(IBaseManager<,>))
                   .InstancePerLifetimeScope();

            // ---------- Concrete Manager/Service ----------
            builder.RegisterType<RoleService>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>().As<IRequestManager>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentManager>().As<IDepartmentManager>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseManager>().As<IWarehouseManager>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentManager>().As<IPaymentManager>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryManager>().As<ICategoryManager>().InstancePerLifetimeScope();
            builder.RegisterType<SubCategoryManager>().As<ISubCategoryManager>().InstancePerLifetimeScope();
            builder.RegisterType<ProductManager>().As<IProductManager>().InstancePerLifetimeScope();

            // ---------- AutoMapper ----------
            var mappingAssemblies = new[]
            {
                typeof(RequestBusinessMapping).Assembly, // BUSINESS profillerinin bulunduğu assembly
                typeof(RequestMapping).Assembly          // WEB profillerinin bulunduğu assembly (VM<->DTO)
            };

            // MapperConfiguration tek yerde oluşturuluyor
            builder.Register(ctx =>
                new MapperConfiguration(cfg =>
                {
                    // Assembly'lerdeki TÜM Profile sınıflarını otomatik yükle
                    cfg.AddMaps(mappingAssemblies);
                    // Burada try/catch veya AssertConfigurationIsValid KULLANMIYORUZ
                }))
            .AsSelf()
            .SingleInstance();

            // IMapper
            builder.Register(ctx =>
                    ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve))
                   .As<IMapper>()
                   .InstancePerLifetimeScope();
        }
    }
}
