using Microsoft.AspNetCore.Mvc.Rendering;

namespace GrTechRK.WebApp.Helpers
{
    public static class HtmlHelper
    {
        public static string IsActive(this IHtmlHelper htmlHelper, string action, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "active" : "";
        }
    }
}
