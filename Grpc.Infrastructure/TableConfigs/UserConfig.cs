using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entities;
using System.Data;

namespace Infrastructure.TableConfigs
{
    internal class UserConfig : BaseEntityTypeConfiguration<User, Guid>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            RequireTraceable = false;
            UseForTraceable = true;
            GeneratedValueForKey = true;
            base.Configure(builder);

            builder.ToTable(nameof(User));

            builder.Property(x => x.FirstName).IsRequired().HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200);
            builder.Property(x => x.LastName).IsRequired().HasColumnType(SqlDbType.NVarChar.ToString()).HasMaxLength(200);
            builder.Property(x => x.NationalCode).IsRequired(false).HasColumnType(SqlDbType.VarChar.ToString()).HasMaxLength(20);
        }
    }
}
