using Autofac;
using AutoMapper;

// Mapping profillerin olduğu namespace'ler
using BUSINESS.AutoMapper;
using WEB.AutoMapper;

// Service/Manager arayüz ve sınıfların
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
            // ---------- Generic repo/service kayıtları ----------
            builder.RegisterAssemblyTypes(typeof(BaseService<>).Assembly)
                   .AsClosedTypesOf(typeof(IBaseRepository<>))
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(BaseManager<,>).Assembly)
                   .AsClosedTypesOf(typeof(IBaseManager<,>))
                   .InstancePerLifetimeScope();

            // ---------- Concrete servis/manager kayıtları ----------
            builder.RegisterType<RoleService>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleManager>().As<IRoleManager>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerLifetimeScope(); // (tek kayıt - duplikeyi kaldırdık)

            builder.RegisterType<RequestManager>().As<IRequestManager>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentManager>().As<IDepartmentManager>().InstancePerLifetimeScope();
            builder.RegisterType<WarehouseManager>().As<IWarehouseManager>().InstancePerLifetimeScope();
            builder.RegisterType<PaymentManager>().As<IPaymentManager>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeManager>().As<IEmployeeManager>().InstancePerLifetimeScope();

            // ---------- AutoMapper ----------
            // Burada mapping profillerini içeren ASSEMBLY’leri AÇIKÇA belirtiyoruz.
            // (BUSINESS içindeki *BusinessMapping* profilleri + WEB içindeki UI profilleri)
            var mappingAssemblies = new[]
            {
                // BUSINESS mapping assembly (ör: RequestBusinessMapping, RoleBusinessMapping, UserBusinessMapping, SubCategoryBusinessMapping, CategoryBusinessMapping vs.)
                typeof(SubCategoryBusinessMapping).Assembly,

                // WEB mapping assembly (ör: RequestMapping, RoleMapping, UserMapping, CategoryMapping vs.)
                typeof(RequestMapping).Assembly
            };

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                // Belirttiğimiz assembly’lerdeki TÜM Profile’ları tara ve ekle
                cfg.AddMaps(mappingAssemblies);

                // İstersen burada global ayarlar yapılabilir:
                // cfg.ShouldMapProperty = p => p.GetMethod != null && p.GetMethod.IsPublic;
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(ctx =>
                ctx.Resolve<MapperConfiguration>().CreateMapper(ctx.Resolve)) // value resolver vb. için ctor DI
            .As<IMapper>()
            .InstancePerLifetimeScope();
        }
    }
}
