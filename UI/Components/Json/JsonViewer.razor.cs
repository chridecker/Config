using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text.RegularExpressions;

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
                jsonElement = JsonSerializer.Deserialize<JsonElement>(this.Value);
            }
            catch (JsonException ex)
            {
                return ex.Message;
            }

            var config = JsonSerializer.Serialize(jsonElement, options: new()
            {
                WriteIndented = true
            });

            if (this._colored)
            {
                config = this.ReplaceForHTMLColors(config);
            }

            return config;
        }

        private string ReplaceForHTMLColors(string value)
        {
            var config = Regex.Replace(value, "&", "&amp;");
            config = Regex.Replace(config, "<", "&lt;");
            config = Regex.Replace(config, ">", "&gt;");

            config = Regex.Replace(config, "(\"(\\\\u[a-zA-Z0-9]{4}|\\\\[^u]|[^\\\\\"])*\"(\\s*:)?|\\b(true|false|null)\\b|-?\\d+(?:\\.\\d*)?(?:[eE][+\\-]?\\d+)?)", delegate (Match m)
            {
                var cls = "number";
                if (Regex.Match(m.Value, "^\"").Success)
                {
                    if (Regex.Match(m.Value, ":$").Success)
                    {
                        cls = "key";

                        var split = Regex.Split(m.Value, "(?<=\").*(?=\")");
                        if (split.Length == 2)
                        {
                            return $"{split[0]}<span class=key>{Regex.Match(m.Value, "(?<=\").*(?=\")").Value}</span>{split[1]}";
                        }
                    }
                    else
                    {
                        cls = "string";
                    }
                }
                else if (Regex.Match(m.Value, "true|false").Success)
                {
                    cls = "boolean";
                }
                else if (Regex.Match(m.Value, "null").Success)
                {
                    cls = "null";
                }

                return $"<span class={cls}>{m}</span>";
            });

            config = Regex.Replace(config, "\\[|\\]", delegate (Match m)
            {
                return $"<span class=squarebraket>{m}</span>";
            });

            config = Regex.Replace(config, "{|}", delegate (Match m)
            {
                return $"<span class=curvedbraket>{m}</span>";
            });

            return config;
        }
    }
}