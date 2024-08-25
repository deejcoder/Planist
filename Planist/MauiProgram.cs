using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Planist.Features.Storage;
using Planist.Features.Todo;
using Planist.Interfaces;
using System.Reflection;

namespace Planist
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // register services
            RegisterImplementations<ITransientService>(builder.Services, ServiceLifetime.Transient);

            // add the database
            builder.Services.AddSingleton(typeof(IPlanistDb), typeof(PlanistDb));            

            // register routes
            RegisterRoutables();

            return builder.Build();
        }

        /// <summary>
        /// Registers all implementations of a particular type (interface)
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="services"></param>
        private static void RegisterImplementations<TInterface>(IServiceCollection services, ServiceLifetime lifeTime)
        {
            Type targetType = typeof(TInterface);

            Assembly assembly = Assembly.GetExecutingAssembly();

            // get all classes that are not abstract, that implement the interface
            List<Type> typesToRegister = assembly.GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();

            // add the services to the DI container based on the service life time
            foreach (Type type in typesToRegister)
            {
                switch (lifeTime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(type);
                        break;

                    case ServiceLifetime.Singleton:
                        services.AddSingleton(type);
                        break;
                }                
            }
        }

        /// <summary>
        /// Registers routes for all routable pages
        /// </summary>
        private static void RegisterRoutables()
        {
            PlanistNavigator.RegisterRoutables();
        }
    }
}
