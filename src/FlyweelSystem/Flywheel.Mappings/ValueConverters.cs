using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Flywheel.Mappings
{
    public static class ValueConverters
    {
        public static ValueConverter<string, string> UpperConverter =>
            new ValueConverter<string, string>(
                v => v.ToUpperInvariant(),
                v => v.ToUpperInvariant());

        public static ValueConverter<string, string> LowerConverter =>
            new ValueConverter<string, string>(
                v => v.ToLowerInvariant(),
                v => v.ToLowerInvariant());
    }
}
