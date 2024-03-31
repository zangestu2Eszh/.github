using IraqWebsite.AuthManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IraqWebsite.Models;

namespace IraqWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EmailSettings> EmailSettings { get; set; }
        public DbSet<UserActivityLog> UserActivityLog { get; set; }
        public DbSet<PasswordComplexityApp> PasswordComplexityApp { get; set; }
        public DbSet<UserManagement> UserManagement { get; set; }
        public DbSet<UserLockout> UserLockout { get; set; }
        public DbSet<Appearance> Appearance { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<CustemerReview> CustemerReview { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Subscribers> Subscribers { get; set; }
        public DbSet<MetaKeyWord> MetaKeyWord { get; set; }
        public DbSet<GoogleRecaptcha> GoogleRecaptcha { get; set; }
        public DbSet<AboutUsPage> AboutUsPages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogSection> BlogSection { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<Clinet> Clients { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProjectSection> ProjectSection { get; set; }
        public DbSet<ProjectCategory> ProjectCategory { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSection> EventSection { get; set; }
        public DbSet<EventCategory> EventCategory { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceSection> ServiceSection { get; set; }
        public DbSet<Statisc> Statiscs { get; set; }
        public DbSet<AcadmeicTraining> AcadmeicTraining { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Structure> Structrue { get; set; } = null!;

    }
}