using BLL.Businesses.Base;
using BLL.Businesses.Login;
using BLL.Businesses.Store;
using DAL.Entities.Login;
using DAL.Entities.Store;
using DAL.Entities.Store.Features;
using DAL.Repositories.Base;
using DAL.Repositories.Login;
using DAL.Repositories.Store;
using DAL.Repositories.Store.Features;

namespace API.Helpers.Extensions
{
    public static class DIExtensions
    {
        public static void ConfigureDI(this IServiceCollection services)
        {
            Business(services);
            Repository(services);
        }

        private static void Business(IServiceCollection services)
        {
            #region Business

            #region Login

            services.AddScoped<IBusiness<User>, UserBusiness>();
            services.AddScoped<IBusiness<Role>, RoleBusiness>();
            services.AddScoped<IBusiness<Session>, SessionBusiness>();

            #endregion Login

            #region Store

            #region Features

            services.AddScoped<IBusiness<Color>, ColorBusiness>();
            services.AddScoped<IBusiness<Size>, SizeBusiness>();

            #endregion Features

            services.AddScoped<IBusiness<Marka>, MarkaBusiness>();
            services.AddScoped<IBusiness<Product>, ProductBusiness>();
            services.AddScoped<IBusiness<ProductFeature>, ProductFeatureBusiness>();

            services.AddScoped<IBusiness<Comment>, CommentBusiness>();
            services.AddScoped<IBusiness<Order>, OrderBusiness>();
            services.AddScoped<IBusiness<OrderProduct>, OrderProductBusiness>();

            #endregion Store

            #endregion Business
        }

        private static void Repository(IServiceCollection services)
        {
            #region Repository

            #region Login

            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Role>, RoleRepository>();
            services.AddScoped<IRepository<Session>, SessionRepository>();

            #endregion Login

            #region Store

            #region Features

            services.AddScoped<IRepository<Color>, ColorRepository>();
            services.AddScoped<IRepository<Size>, SizeRepository>();

            #endregion Features

            services.AddScoped<IRepository<Marka>, MarkaRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<ProductFeature>, ProductFeatureRepository>();

            services.AddScoped<IRepository<Comment>, CommentRepository>();
            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderProduct>, OrderProductRepository>();

            #endregion Store

            #endregion Repository
        }
    }
}