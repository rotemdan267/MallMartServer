
using Microsoft.EntityFrameworkCore;

namespace MallMartDB.Models
{
    public partial class MallMartDBContext : DbContext
    {
        public MallMartDBContext()
        {
        }

        public MallMartDBContext(DbContextOptions<MallMartDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Entrance).IsRequired(false);
                entity.Property(e => e.ApartmentNo).IsRequired(false);
                entity.Property(e => e.Postal).IsRequired(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.ArrivalTime).IsRequired(false);
                entity.Property(e => e.EmployeeId).IsRequired(false);
                entity.Property(e => e.TotalPrice).IsRequired(false);
                entity.Property(e => e.PricePaid).IsRequired(false);
                entity.Property(e => e.Comment).IsRequired(false);
                entity.Property(o => o.DateOrdered).IsRequired(false);
                entity.Property(o => o.DueTimeFirst).IsRequired(false);
                entity.Property(o => o.DueTimeLast).IsRequired(false);
            });

            CreateData createData = new CreateData();

            List<Category> categories = createData.GetCategories();
            modelBuilder.Entity<Category>().HasData(categories);

            List<Product> products = createData.GetProducts(categories);
            modelBuilder.Entity<Product>().HasData(products);

            List<Region> regions = createData.GetRegions();
            modelBuilder.Entity<Region>().HasData(regions);

            List<Address> addresses = createData.GetAddresses(regions);
            modelBuilder.Entity<Address>().HasData(addresses);

            List<UserImage> images = createData.GetUserImages();
            modelBuilder.Entity<UserImage>().HasData(images);

            List<User> users = createData.GetUsers(images);
            modelBuilder.Entity<User>().HasData(users);

            List<Customer> customers = createData.GetCustomers(addresses, users);
            modelBuilder.Entity<Customer>().HasData(customers);

            List<Employee> employees = createData.GetEmployees(users);
            modelBuilder.Entity<Employee>().HasData(employees);

            List<EmployeeRegion> employeeRegions = createData.GetEmployeeRegions(employees, regions);
            modelBuilder.Entity<EmployeeRegion>().HasData(employeeRegions);

            List<OrderLine> lines = new List<OrderLine>();
            List<Order> orders = createData.GetOrders(lines, employees, customers, products);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<OrderLine>().HasData(lines);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        //public DbSet<AcquisitionOrder> AcquisitionOrders { get; set; }
        //public DbSet<AcquisitionOrderLine> AcquisitionOrderLines { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Region> Regions { get; set; }
        //public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<EmployeeRegion> EmployeeRegions { get; set; }
    }
}
