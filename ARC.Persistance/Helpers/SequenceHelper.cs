using ARC.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARC.Persistance
{
    public static class SequenceHelper
    {
        private static int GetSequence<T>(this ApplicationDbContext context)
        {
            var sequence = $"{typeof(T).Name}Id";
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            context.Database.ExecuteSqlRaw($"SELECT @result = (NEXT VALUE FOR {sequence})", result);

            return (int)result.Value;
        }

        public static void AddSequencesForEntities(this ModelBuilder builder)
        {
            var domainAssembly = typeof(Entity<>).Assembly.GetName();
            var entities = builder.Model.GetEntityTypes();

            foreach (var entity in entities)
            {
                var type = Type.GetType($"{entity.Name}, {domainAssembly}");
                if(type.GetProperty("Id", typeof(int)) != null)
                {
                    builder.HasSequence<int>($"{type.Name}Id").StartsAt(1000);
                }
            }
        }

        public static IEnumerable<T> SetId<T>(this ApplicationDbContext context, IEnumerable<T> list) where T : Entity<int>
        {
            foreach (var item in list)
            {
                item.Id = context.GetSequence<T>();
            }

            return list;
        }

        public static T SetId<T>(this ApplicationDbContext context, T item) where T : Entity<int>
        {
            item.Id = context.GetSequence<T>();
            return item;
        }
    }
}
