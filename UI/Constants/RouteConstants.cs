﻿namespace UI.Constants
{
    public static class RouteConstants
    {
        public const string Home = "/";
        public const string Services = $"/services";
        public const string Service = $"/service/{RouteParameterConstants.Id}";
        public const string ServiceVersion = $"/serviceVersion/{RouteParameterConstants.Id}";
        public const string Setting = $"/setting/{RouteParameterConstants.Id}";
    }
}
