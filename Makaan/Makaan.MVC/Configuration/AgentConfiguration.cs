using Makaan.MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Makaan.MVC.Configuration;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder
            .Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(64);
        builder
            .Property(x=>x.ImageUrl)
            .IsRequired()
            .HasMaxLength(256);
        builder
            .HasOne(x => x.Department)
            .WithMany(x => x.Agents)
            .HasForeignKey(x => x.DepartmentId); 
    }
}
