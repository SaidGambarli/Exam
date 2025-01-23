using Makaan.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Makaan.MVC.Configuration;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(16);
        builder
            .Property(x=>x.Surname)
            .IsRequired()
            .HasMaxLength(16);
    }
}
