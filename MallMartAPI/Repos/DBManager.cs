using MallMartDB.Models;
using Microsoft.EntityFrameworkCore;
namespace MallMartAPI.Repos
{
    // קלאס עבור שאר ה-
    // Queries
    // ופעולות ב-DB
    // שלא כלולים ב-GenericRepository
    public class DBManager : IDBManager
    {
        private MallMartDBContext db = null;
        public DBManager(MallMartDBContext context)
        {
            this.db = context;
        }

        #region Select full Table

        public async Task<List<Product>> GetProductsOrderByCategory()
        {
            List<Product> products = await db.Products.Include(p => p.Category).OrderBy(p => p.CategoryId).ToListAsync();
            return products;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = await db.Employees.Include(p => p.User).ToListAsync();

            return employees;
        }

        //public async Task<List<Region>> GetRegionsWithEmployees()
        //{
        //    List<Region> regions = await db.Regions.Include(r => r.Employees).ToListAsync();

        //    return regions;
        //}

        public async Task<List<Customer>> GetCustomers()
        {

            List<Customer> customers = await db.Customers
                .Include(p => p.User)
                .ThenInclude(u => u.Image)
                .Include(c => c.Address)
                .ThenInclude(a => a.Region)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderLines)
                .Include(c => c.Orders)
                .ThenInclude(o => o.Employee)
                .ToListAsync();

            return customers;
        }

        #endregion

        #region Select Queries

        public async Task<Product> GetProductById(int id)
        {
            Product product = await db.Products.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefaultAsync();

            return product;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            User user = await db.Users.Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            return user;
        }
        public async Task<Customer> GetCustomerByUser(User user)
        {
            Customer customer = await db.Customers.Where(c => c.User.Id == user.Id)
                .Include(c => c.User)
                .Include(c => c.Address)
                .ThenInclude(a => a.Region)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderLines)
                .ThenInclude(l => l.Product)
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer customer = await db.Customers.Where(c => c.CustomerId == id)
                .Include(c => c.User)
                .Include(c => c.Address)
                .ThenInclude(a => a.Region)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderLines)
                .ThenInclude(l => l.Product)
                .Include(c => c.Orders)
                .ThenInclude(o => o.Employee)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync();

            return customer;
        }

        public async Task<Employee> GetEmployeeByUser(User user)
        {
            Employee employee = await db.Employees.Where(e => e.User.Id == user.Id)
                .Include(e => e.User)
                .Include(e => e.Manager)
                .Include(e => e.Employees)
                .Include(e => e.DeliveryRegions)
                .ThenInclude(d => d.Region)
                .FirstOrDefaultAsync();

            return employee;
        }
        /// <summary>
        /// הייתי צריך לקבל את ה"עובד" יחד עם הישויות שמתחתיו, לכן בניתי פונקציה נוספת מלבד הג'נריק ריפו
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee employee = await db.Employees.Where(e => e.Id == id)
                .Include(e => e.User)
                .Include(e => e.Manager)
                .Include(e => e.Employees)
                .Include(e => e.DeliveryRegions)
                .ThenInclude(d => d.Region)
                .FirstOrDefaultAsync();

            return employee;
        }

        public async Task<Address> GetAddressById(int id)
        {
            Address address = await db.Addresses.Where(a => a.AddressId == id)
                .Include(a => a.Region)
                .FirstOrDefaultAsync();

            return address;
        }
        public async Task<Order> GetOrderById(int id)
        {
            Order order = await db.Orders.Where(o => o.OrderId == id)
                 .Include(o => o.OrderLines)
                 .ThenInclude(l => l.Product)
                 .ThenInclude(l => l.Category)
                 .Include(o => o.Employee)
                 .ThenInclude(o => o.User)
                 .Include(e => e.Customer)
                 .FirstOrDefaultAsync();

            return order;
        }

        public async Task<List<Employee>> GetManagers()
        {
            List<Employee> managers = await db.Employees
                .Where(e => e.User.Authorization == "Manager" || e.User.Authorization == "DeliveryManager")
                .Include(e => e.User).ToListAsync();

            return managers;
        }

        public async Task<MallMartDB.Models.Region> GetRegionByName(string name)
        {
            var region = await db.Regions.Where(r => r.Name == name).FirstOrDefaultAsync();

            return region;
        }

        public async Task<List<EmployeeRegion>> GetEmployeeRegionByEmployee(Employee employee)
        {
            var employeeRegions = await db.EmployeeRegions.Where(r => r.Employee == employee).ToListAsync();

            return employeeRegions;
        }

        public async Task<List<Order>> GetOrders(bool areOrdersDeliverd)
        {
            var orders = await db.Orders.Where(o => o.IsOrderDone == areOrdersDeliverd)
                 .Include(o => o.Customer)
                 .ThenInclude(c => c.User)
                 .Include(o => o.Customer)
                 .ThenInclude(c => c.Orders)
                 .Include(o => o.Customer)
                 .ThenInclude(c => c.Address)
                 .ThenInclude(a => a.Region)
                 .Include(o => o.Employee)
                 .ThenInclude(e => e.User)
                 .Include(o => o.OrderLines)
                 .ThenInclude(l => l.Product)
                 .ToListAsync();

            return orders;
        }

        public async Task<List<Order>> GetOrdersByEmployee(Employee employee)
        {
            var orders = new List<Order>();

            try
            {
                orders = await db.Orders
                    .Where(o => o.EmployeeId == employee.Id && o.ArrivalTime == null)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Orders)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Address)
                    .ThenInclude(a => a.Region)
                    .Include(o => o.Employee)
                    .ThenInclude(e => e.User)
                    .Include(o => o.OrderLines)
                    .ThenInclude(l => l.Product)
                    .ToListAsync();
            }
            catch
            {
                orders = null;
            }

            return orders;
        }

        public async Task<List<Order>> GetOrdersByCustomerId(int id)
        {
            var orders = new List<Order>();
            try
            {
                orders = await db.Orders
                    .Where(o => o.CustomerId == id)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.User)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Orders)
                    .Include(o => o.Customer)
                    .ThenInclude(c => c.Address)
                    .ThenInclude(a => a.Region)
                    .Include(o => o.OrderLines)
                    .ThenInclude(l => l.Product)
                    .ToListAsync();
            }
            catch
            {
                orders = null;
            }
            return orders;
        }

        public async Task<List<Employee>> GetEmployeesByRegion(int regionId)
        {
            var employees = new List<Employee>();
            var employeeRegions = new List<EmployeeRegion>();

            employeeRegions = await db.EmployeeRegions.Where((e) => e.RegionId == regionId)
                .Include(e => e.Employee)
                .ThenInclude(c => c.User)
                .ToListAsync();

            foreach (var item in employeeRegions)
            {
                employees.Add(item.Employee);
            }
            return employees;
        }

        #endregion

        #region Update DB Methods

        public async Task UpdatePrudctQuantity(int id, int onOrder)
        {

            Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();
            product.UnitsOnOrder += onOrder;
            product.UnitsInStock -= onOrder;
            await db.SaveChangesAsync();

        }

        public async Task UpdatePrudct(Product product)
        {
            db.Update(product);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAddress(Address address)
        {
            db.Addresses.Update(address);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// This function's job is to update customer details that does not include "orders" ,"address", or "user" details 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCustomerDetails(Customer customer)
        {
            Customer exists = await db.Customers.Where(p => p.CustomerId == customer.CustomerId)
                .Include(c => c.Orders).FirstOrDefaultAsync();

            if (exists is null)
            {
                return false;
            }
            else
            {
                exists.PaymentMethod = customer.PaymentMethod;
                exists.PaymentDetails = customer.PaymentDetails;

                db.Customers.Update(exists);
                await db.SaveChangesAsync();

                return true;
            }
        }

        #endregion

        #region Add DB Methods

        public async Task<Order> SaveOrder(Order order)
        {
            foreach (var line in order.OrderLines)
            {
                db.Entry(line).State = EntityState.Added;
            }

            db.Entry(order).State = EntityState.Added;
            db.SaveChanges();

            return order;
        }

        #endregion

        #region Delete DB Methods

        public async Task DeleteEmployeeRegion(EmployeeRegion employeeRegion)
        { // בריפוזיטורי הסטטי מתודת "מחיקה" מקבלת 
          // id
          // אחד, ולטבלה הזו יש שניים...
          // לכן בניתי לה בנפרד

            db.Entry(employeeRegion).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        public async Task DeleteOrderLine(OrderLine orderLine)
        { // בריפוזיטורי הסטטי מתודת "מחיקה" מקבלת 
          // id
          // אחד, ולטבלה הזו יש שניים...
          // לכן בניתי לה בנפרד

            db.Entry(orderLine).State = EntityState.Deleted;
            await db.SaveChangesAsync();
        }

        #endregion
    }
}
