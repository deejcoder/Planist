using Planist.Interfaces;
using System.Reflection;

namespace Planist
{
    public class PlanistNavigator
    {
        private static Dictionary<Type, string> _registeredRoutes = [];

        /// <summary>
        /// Registers routes for pages that implement the IPlanistRoutable interface
        /// </summary>
        internal static void RegisterRoutables()
        {
            Type targetType = typeof(IPlanistRoutable);

            Assembly assembly = Assembly.GetExecutingAssembly();

            List<Type> pagesToRegister = assembly.GetTypes()
                .Where(t => targetType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (Type page in pagesToRegister)
            {
                if (string.IsNullOrWhiteSpace(page.FullName)) continue;

                Routing.RegisterRoute(page.FullName, page);

                _registeredRoutes.Add(page, page.FullName);
            }
        }

        /// <summary>
        /// Navigates to a specific routable page
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <returns></returns>
        public static async Task NavigateToAsync<TPage>() 
            where TPage : ContentPage
        {
            if (_registeredRoutes.TryGetValue(typeof(TPage), out string? route))
            {
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
