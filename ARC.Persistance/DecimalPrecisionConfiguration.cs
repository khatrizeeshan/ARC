using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ARC.Persistance
{
    public static class DecimalPrecisionConfiguration
    {
        public static ModelBuilder SetDefaultPrecision(this ModelBuilder modelBuilder, int precision, int scale)
        {
            var properties = modelBuilder.Model.GetEntityTypes()
                                               .SelectMany(t => t.GetProperties())
                                               .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));

            foreach (var property in properties)
            {
                property.SetPrecision(precision);
                property.SetScale(scale);
            }

            return modelBuilder;
        }
    }
}