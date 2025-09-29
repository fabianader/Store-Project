using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreProject.Entities;
using System.Reflection.Emit;

namespace StoreProject.Infrastructure.Data
{
    public class StoreContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers {  get; set; }
        //public DbSet<IdentityRole> ApplicationRole {  get; set; }
        //public DbSet<IdentityUserClaim<string>> ApplicationUserClaim {  get; set; }
        //public DbSet<IdentityUserRole> ApplicationUserRole {  get; set; }
        //public DbSet<IdentityUserLogin> ApplicationUserLogin {  get; set; }
        //public DbSet<IdentityRoleClaim> ApplicationRoleClaim {  get; set; }
        //public DbSet<IdentityUserToken> ApplicationUserToken {  get; set; }

        public DbSet<Cart> Carts {  get; set; }
        public DbSet<CartItem> CartItems {  get; set; }
        public DbSet<Category> Categories {  get; set; }
        public DbSet<ContactMessage> ContactMessages {  get; set; }
        public DbSet<Order> Orders {  get; set; }
        public DbSet<OrderChangeLog> OrderChangeLogs {  get; set; }
        public DbSet<OrderItem> OrderItems {  get; set; }
        public DbSet<Post> Posts {  get; set; }
        public DbSet<Product> Products {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Category>()
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
        //    base.OnModelCreating(builder);

        //    builder.Entity<ApplicationUser>(b =>
        //    {
        //        b.Property(user => user.Email).HasMaxLength(260);

        //        // Each User can have many UserClaims
        //        b.HasMany(e => e.Claims)
        //            .WithOne(e => e.User)
        //            .HasForeignKey(uc => uc.UserId)
        //            .IsRequired();

        //        // Each User can have many UserLogins
        //        b.HasMany(e => e.Logins)
        //            .WithOne(e => e.User)
        //            .HasForeignKey(ul => ul.UserId)
        //            .IsRequired();

        //        // Each User can have many UserTokens
        //        b.HasMany(e => e.Tokens)
        //            .WithOne(e => e.User)
        //            .HasForeignKey(ut => ut.UserId)
        //            .IsRequired();

        //        // Each User can have many entries in the UserRole join table
        //        b.HasMany(e => e.UserRoles)
        //            .WithOne(e => e.User)
        //            .HasForeignKey(ur => ur.UserId)
        //            .IsRequired();
        //    });

        //    builder.Entity<ApplicationRole>(b =>
        //    {
        //        // Each Role can have many entries in the UserRole join table
        //        b.HasMany(e => e.UserRoles)
        //            .WithOne(e => e.Role)
        //            .HasForeignKey(ur => ur.RoleId)
        //            .IsRequired();

        //        // Each Role can have many associated RoleClaims
        //        b.HasMany(e => e.RoleClaims)
        //            .WithOne(e => e.Role)
        //            .HasForeignKey(rc => rc.RoleId)
        //            .IsRequired(); 
        //    });

        //    builder.Entity<ApplicationUserClaim>()
        //        .HasOne(uc => uc.User)
        //        .WithMany(u => u.Claims)
        //        .HasForeignKey(uc => uc.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<ApplicationUserLogin>()
        //        .HasOne(uc => uc.User)
        //        .WithMany(u => u.Logins)
        //        .HasForeignKey(uc => uc.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<ApplicationUserToken>()
        //        .HasOne(uc => uc.User)
        //        .WithMany(u => u.Tokens)
        //        .HasForeignKey(uc => uc.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<ApplicationUserRole>()
        //        .HasOne(uc => uc.User)
        //        .WithMany(u => u.UserRoles)
        //        .HasForeignKey(uc => uc.UserId)
        //        .OnDelete(DeleteBehavior.Restrict);

    }
}
