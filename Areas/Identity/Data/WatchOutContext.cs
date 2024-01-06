using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchOut.Areas.Identity.Data;

namespace WatchOut.Data;

public class WatchOutContext : IdentityDbContext<WatchOutUser>
{
    public WatchOutContext(DbContextOptions<WatchOutContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<WatchOutUser>
{
    public void Configure(EntityTypeBuilder<WatchOutUser> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Surname).IsRequired();
        builder.Property(x => x.IsAdmin).IsRequired().HasConversion<bool>();
    //public int Id { get; set; }
    //public string Email { get; set; }
    //public string Password { get; set; }
    //public string Name { get; set; }
    //public string Surname { get; set; }
    //public bool IsAdmin { get; set; }
    }
}