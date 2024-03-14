using UI.Dto;

namespace UI.Services
{
    public static class VersionHelper
    {
        public static string CreateNewVersion(string? latestVersion)
        {
            var version = new DionySysVersion(1, 1);

            if (latestVersion is not null)
            {
                var latestDSVersion = new DionySysVersion(latestVersion);

                if (version.Major != latestDSVersion.Major)
                {
                    version.Major = latestDSVersion.Major;
                }

                if (version.Date == latestDSVersion.Date)
                {
                    version.Minor = latestDSVersion.Minor + 1;
                }
            }

            return version.ToString();
        }
    }
}
