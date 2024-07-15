using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }

    public DbSet<Orders> Orders { get; set; }

    // https://frankofoedu.medium.com/how-to-seed-identity-role-with-associated-user-in-asp-net-core-ef-core-e40ecd643d0f
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string ADMIN_ID = "07086e6c-0592-4679-9ed6-e41260738a80";
        string ROLE_ID = "3dccc66f-a940-4065-b20a-cf27ad9340e8";

        //seed admin role
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN",
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        });

        //create user
        var appUser = new ApplicationUser
        {
            Id = ADMIN_ID,
            Email = "test@mail.com",
            EmailConfirmed = true,
            UserName = "test@mail.com",
            NormalizedEmail = "TEST@MAIL.COM",
            NormalizedUserName = "TEST@MAIL.COM"
        };

        //set user password
        PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "Mypassword_0?");

        //seed user
        builder.Entity<ApplicationUser>().HasData(appUser);

        //set user role to admin
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });
    }
}
