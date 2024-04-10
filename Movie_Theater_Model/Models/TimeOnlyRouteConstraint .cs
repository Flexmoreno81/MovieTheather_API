using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Theater_Model.Models
{

    public class TimeOnlyRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Implement your custom logic to validate the time format here
            // Return true if the value is valid, otherwise return false
            // Example: Check if the value is a valid time format
            return TimeSpan.TryParse(values[routeKey]?.ToString(), out _);
        }
    }




}