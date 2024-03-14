using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.RegularExpressions;
using UI.Constants;

namespace UI.Components.Json
{
    public partial class JsonViewer
    {
        [Parameter] public string Value { get; set; }

        private string _formated;
        private bool _colored = true;

        protected override void OnParametersSet()
        {
            this._formated = this.Format();

            base.OnParametersSet();
        }

        private void ColoredChange(ChangeEventArgs args)
        {
            this._colored = !this._colored;

            this._formated = this.Format();
        }

        private string Format()
        {
            if (string.IsNullOrWhiteSpace(this.Value)) { return string.Empty; }

            JsonElement jsonElement;
            try
            {
                jsonElement = JsonSerializer.Deserialize<JsonElement>(this.Value, options: new()
                {
                    AllowTrailingCommas = true
                });
            }
            catch (JsonException ex)
            {
                return ex.Message;
            }

            var config = JsonSerializer.Serialize(jsonElement, options: new()
            {
                WriteIndented = true,
            });

            config = Regex.Unescape(config);

            if (this._colored)
            {
                config = this.ReplaceForHTMLColors(config);
            }

            return config;
        }

        private string ReplaceForHTMLColors(string value)
        {
            var config = RegexConstants.JsonTypes().Replace(value, delegate (Match m)
            {
                var cls = "number";
                if (RegexConstants.JsonTypeText().Match(m.Value).Success)
                {
                    if (RegexConstants.JsonTypeKey().Match(m.Value).Success)
                    {
                        cls = "key";

                        var split = RegexConstants.JsonTextValue().Split(m.Value);
                        if (split.Length == 2)
                        {
                            return $"{split[0]}<span class=key>{RegexConstants.JsonTextValue().Match(m.Value).Value}</span>{split[1]}";
                        }
                    }
                    else
                    {
                        cls = "string";
                    }
                }
                else if (RegexConstants.JsonTypeString().Match(m.Value).Success)
                {
                    cls = "boolean";
                }
                else if (RegexConstants.JsonTypeNull().Match(m.Value).Success)
                {
                    cls = "null";
                }

                return $"<span class={cls}>{m}</span>";
            });

            config = RegexConstants.JsonTypeSquareBraket().Replace(config, delegate (Match m)
            {
                return $"<span class=squarebraket>{m}</span>";
            });

            config = RegexConstants.JsonTypeCurvedBraket().Replace(config, delegate (Match m)
            {
                return $"<span class=curvedbraket>{m}</span>";
            });

            return config;
        }
    }
}