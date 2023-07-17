using MallMartDB.Models;

namespace MallMartAPI.Repos
{
    public interface IDBManager
    {
        Task DeleteEmployeeRegion(EmployeeRegion employeeRegion);
        Task DeleteOrderLine(OrderLine orderLine);
        Task<Customer> GetCustomerById(int id);
        Task<User> GetUserByUsername(string username);
        Task<Customer> GetCustomerByUser(User user);
        Task<List<Customer>> GetCustomers();
        //Task<List<Region>> GetRegionsWithEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Address> GetAddressById(int id);
        Task<Employee> GetEmployeeByUser(User user);
        Task<List<EmployeeRegion>> GetEmployeeRegionByEmployee(Employee employee);
        Task<List<Employee>> GetEmployees();
        Task<List<Employee>> GetEmployeesByRegion(int regionId);
        Task<List<Employee>> GetManagers();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrders(bool areOrdersDeliverd);
        Task<List<Order>> GetOrdersByEmployee(Employee employee);
        Task<List<Order>> GetOrdersByCustomerId(int id);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProductsOrderByCategory();
        Task<Region> GetRegionByName(string name);
        Task<Order> SaveOrder(Order order);
        Task UpdateAddress(Address address);
        Task UpdatePrudct(Product product);
        Task UpdatePrudctQuantity(int id, int onOrder);
        /// <summary>
        ///This function's job is to update customer details that does not include "orders" ,"address", or "user" details 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> UpdateCustomerDetails(Customer customer);
    }
}