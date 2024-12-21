using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;

namespace Infrastructure
{
    internal abstract class BaseEntityTypeConfiguration<T, KeyTypeId> : IEntityTypeConfiguration<T> where T : BaseEntity<KeyTypeId> where KeyTypeId : struct
    {
        protected bool GeneratedValueForKey { get; set; } = true;
        protected bool UseForTraceable { get; set; } = false;
        protected bool RequireTraceable { get; set; } = false;

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            if (GeneratedValueForKey)
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

            if (UseForTraceable == false)
            {
                builder.Ignore(x => x.CreatedDateTime);
                builder.Ignore(x => x.UpdatedDataTime);
                builder.Ignore(x => x.CreatedUserId);
                builder.Ignore(x => x.CreatedUser);
                builder.Ignore(x => x.UpdatedUserId);
                builder.Ignore(x => x.UpdatedUser);
            }
            else
            {
                builder.Property(x => x.CreatedDateTime).IsRequired(RequireTraceable);
                builder.Property(x => x.UpdatedDataTime).IsRequired(RequireTraceable);
                builder.HasOne(x => x.CreatedUser).WithMany().HasForeignKey(x => x.CreatedUserId).IsRequired(RequireTraceable).OnDelete(DeleteBehavior.NoAction);
                builder.HasOne(x => x.UpdatedUser).WithMany().HasForeignKey(x => x.UpdatedUserId).IsRequired(RequireTraceable).OnDelete(DeleteBehavior.NoAction);
            }
        }
    }
}
