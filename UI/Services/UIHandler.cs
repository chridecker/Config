using DionySys.UI.Shared.Contracts.Interfaces;

namespace UI.Services
{
    public class UIHandler : IUIHandler
    {
        public bool RenderIconsInClassicFontAwesomeSyntax => false;

        public bool IsTestEnv => false;
    }
}
