

using DataAccess;
using DataAccess.Model;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace UI.Components.Pages
{
    public partial class Home
    {
        private string _currentConfig = @"{
    ""a"": 1,
    ""b"": ""foo"",
    ""c"": [
        false,
        ""false"",
        null,
        ""null"",
        {
            ""d"": {
                ""e"": 130000,
                ""f"": ""1.3e5""
            }
        }
    ]
}";
    }
}