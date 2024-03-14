namespace UI.Dto
{
    public struct DionySysVersion
    {
        public int Major { get; set; }
        public DateOnly Date { get; set; }
        public int Minor { get; set; }

        public DionySysVersion(int major, int minor)
        {
            this.Major = major;
            this.Date = DateOnly.FromDateTime(DateTime.Now);
            this.Minor = minor;
        }

        public DionySysVersion(int major, DateOnly date, int minor)
        {
            this.Major = major;
            this.Date = date;
            this.Minor = minor;
        }

        public DionySysVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version)) { throw new ArgumentNullException("Version darf nicht leer sein", nameof(version)); }

            var split = version.Split('.');

            if (split.Length != 4) { throw new ArgumentException("Version hat falsches Format", nameof(version)); }

            if (!int.TryParse(split[0], out var major)) { throw new Exception($"Konnte [{split[0]}] nicht zu einer Zahl parsen"); }
            this.Major = major;

            if (!int.TryParse(split[3], out var minor)) { throw new Exception($"Konnte [{split[3]}] nicht zu einer Zahl parsen"); }
            this.Minor = minor;

            if (split[2].Length != 5) { throw new ArgumentException($"Datums Identifier hat falsches Format"); }

            if (!int.TryParse(split[2][..2], out var year)) { throw new Exception($"Konnte [{split[2][..2]}] nicht zu einer Zahl parsen"); }
            if (!int.TryParse(split[2][2..], out var dayOfYear)) { throw new Exception($"Konnte [{split[2][2..]}] nicht zu einer Zahl parsen"); }

            this.Date = new DateOnly(year+2000, 1, 1).AddDays(dayOfYear - 1);
        }

        public override string ToString() => $"{this.Major}.0.{this.Date:yy}{this.Date.DayOfYear:000}.{this.Minor}";

        public static bool operator <(DionySysVersion version1, DionySysVersion version2)
        {
            if (version1.Major < version2.Major) { return true; }
            if (version1.Date < version2.Date) { return true; }
            if (version1.Minor < version2.Minor) { return true; }

            return false;
        }

        public static bool operator >(DionySysVersion version1, DionySysVersion version2)
        {
            if (version1.Major > version2.Major) { return true; }
            if (version1.Date > version2.Date) { return true; }
            if (version1.Minor > version2.Minor) { return true; }

            return false;
        }

        public static bool operator ==(DionySysVersion version1, DionySysVersion version2) => version1.Major == version2.Major && version1.Date == version2.Date && version1.Minor == version2.Minor;

        public static bool operator !=(DionySysVersion version1, DionySysVersion version2) => !(version1 == version2);
    }
}
