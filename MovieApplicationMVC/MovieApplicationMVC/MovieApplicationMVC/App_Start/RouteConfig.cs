using System.Web.Mvc;
using System.Web.Routing;

namespace MovieApplicationMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Enable attribute-based routing
            routes.MapMvcAttributeRoutes();

            // Default route for conventional routing
            routes.MapRoute(
                name: "MovieDetails",
                url: "Home/MovieDetails/{id}",
                defaults: new { controller = "Home", action = "MovieDetails", id = UrlParameter.Optional }
            );

            // Default fallback route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
