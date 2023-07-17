using MallMartDB.Models;
using MallMartLogic;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace MallMartDB
{
    public class CreateData
    {
        public List<Category> GetCategories()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\MallMartDB\Data\Categories.json";
            string categoriesJson = File.ReadAllText(path);
            var list = JsonSerializer.Deserialize<List<string>>(categoriesJson);
            List<Category> categories = new List<Category>();
            int i;
            for (i = 0; i < list.Count; i++)
            {
                categories.Add(new Category()
                {
                    Id = i + 1,
                    Name = list[i]
                });

            }

            return categories;
        }
        public List<Product> GetProducts(List<Category> categories)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\MallMartDB\Data\Products.json";
            string productsJson = File.ReadAllText(path);

            List<FakeStoreProduct> fakeProducts = JsonSerializer.Deserialize<List<FakeStoreProduct>>(productsJson);
            List<Product> products = new List<Product>();
            Category category;
            int i = 1;
            foreach (var item in fakeProducts)
            {
                category = categories.Where(c => c.Name == item.category).FirstOrDefault();
                products.Add(new Product()
                {
                    Id = i,
                    Name = item.title,
                    CategoryId = category.Id,
                    UnitPrice = item.price,
                    UnitsInStock = 100,
                    UnitsOnOrder = 0,
                    Description = item.description,
                    ImageLink = item.image,
                    Rating = item.rating.rate,
                    NumOfRaters = item.rating.count

                });
                i++;
            }

            #region Snacks

            category = categories.Where(c => c.Name == "Snacks").FirstOrDefault();

            products.Add(new Product()
            {
                Id = i++,
                Name = "Bamba",
                CategoryId = category.Id,
                UnitPrice = 3.5f,
                UnitsInStock = 50,
                UnitsOnOrder = 0,
                Description = "Peanuts snack",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 4.3f,
                NumOfRaters = 126
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Bissli Gril",
                CategoryId = category.Id,
                UnitPrice = 3.9f,
                UnitsInStock = 50,
                UnitsOnOrder = 0,
                Description = "Wheat snack. Gril flavoured",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 3.9f,
                NumOfRaters = 78
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Tapuchips",
                CategoryId = category.Id,
                UnitPrice = 4f,
                UnitsInStock = 50,
                UnitsOnOrder = 0,
                Description = "Potato chips snack",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 3.5f,
                NumOfRaters = 59
            });

            #endregion

            #region Cars

            category = categories.Where(c => c.Name == "Cars").FirstOrDefault();

            products.Add(new Product()
            {
                Id = i++,
                Name = "Volkswagen Passat⁠",
                CategoryId = category.Id,
                UnitPrice = 88982,
                UnitsInStock = 5,
                UnitsOnOrder = 0,
                Description = "Spacious and sporty, the capable 2022 Passat features" +
                    " an array of convenience features and driver assistance technology." +
                    " The Passat Limited Edition further boasts a Hands-Free Easy Open" +
                    " trunk, heated rear outboard seats, Fender® Premium Audio System" +
                    " and more.",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 4.3f,
                NumOfRaters = 17
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Audi TT Coupe",
                CategoryId = category.Id,
                UnitPrice = 62960,
                UnitsInStock = 5,
                UnitsOnOrder = 0,
                Description = "The TT Coupe is a quintessential Audi design icon, " +
                    "boasting a driver-focused interior with exceptional integration of " +
                    "technology and infotainment—while still delivering true sports-car " +
                    "performance.",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 4.8f,
                NumOfRaters = 8
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Audi A8",
                CategoryId = category.Id,
                UnitPrice = 79128,
                UnitsInStock = 5,
                UnitsOnOrder = 0,
                Description = "Beyond the elegant lines of the new 2022 Audi A8, lies " +
                    "a car so advanced it doesn’t just change the game—it can transform " +
                    "the way you drive.",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 3.2f,
                NumOfRaters = 9
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Audi R8 Coupe",
                CategoryId = category.Id,
                UnitPrice = 52000,
                UnitsInStock = 5,
                UnitsOnOrder = 0,
                Description = "The Audi R8 performance Coupe brings the racing-inspired" +
                    " performance you seek with uncompromised styling. This is your " +
                    "opportunity to enjoy the exhilarating performance that lies within.",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 4.75f,
                NumOfRaters = 16
            });

            products.Add(new Product()
            {
                Id = i++,
                Name = "Aston Martin DB11",
                CategoryId = category.Id,
                UnitPrice = 67570,
                UnitsInStock = 5,
                UnitsOnOrder = 0,
                Description = "Standard-bearer for an all-new generation of cars, " +
                    "DB11 is the most powerful and efficient ‘DB’ production model in " +
                    "Aston Martin’s history. ",
                ImageLink = "https://freesvg.org/img/Placeholder.png",
                Rating = 4.85f,
                NumOfRaters = 13
            });

            #endregion

            return products;

        }
        public List<Region> GetRegions()
        {
            List<Region> regions = new List<Region>();
            int i = 0;

            regions.Add(new Region() { Id = ++i, Name = "North" });
            regions.Add(new Region() { Id = ++i, Name = "South" });
            regions.Add(new Region() { Id = ++i, Name = "Jerusalem" });
            regions.Add(new Region() { Id = ++i, Name = "Tel Aviv" });
            regions.Add(new Region() { Id = ++i, Name = "Sharon" });

            return regions;

        }
        public List<Address> GetAddresses(List<Region> regions)
        {
            List<Address> addresses = new List<Address>();
            int i = 0;

            Region region;

            region = regions.Where(r => r.Name == "South").FirstOrDefault();
            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Dimona",
                Street = "Dogit",
                StreetNo = 8,
                Entrance = null,
                ApartmentNo = 7,
                Postal = 68249,
            });

            region = regions.Where(r => r.Name == "North").FirstOrDefault();
            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Haifa",
                Street = "Hapashosh",
                StreetNo = 3,
                Entrance = 'A',
                ApartmentNo = 2,
                Postal = 32990,
            });

            region = regions.Where(r => r.Name == "Jerusalem").FirstOrDefault();
            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Jerusalem",
                Street = "Pinsker",
                StreetNo = 11,
                Entrance = 'D',
                ApartmentNo = 5,
                Postal = 31158,
            });

            region = regions.Where(r => r.Name == "Sharon").FirstOrDefault();
            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Netanya",
                Street = "Nili",
                StreetNo = 21,
                Entrance = null,
                ApartmentNo = null,
                Postal = null,
            });

            region = regions.Where(r => r.Name == "Tel Aviv").FirstOrDefault();
            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Holon",
                Street = "Eilat",
                StreetNo = 38,
                Entrance = 'E',
                ApartmentNo = 8,
                Postal = 74628,
            });

            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Jaffa",
                Street = "Shaked",
                StreetNo = 2,
                Entrance = null,
                ApartmentNo = 2,
                Postal = 86122,
            });

            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Azur",
                Street = "Bilu",
                StreetNo = 5,
                Entrance = null,
                ApartmentNo = null,
                Postal = 22513,
            });

            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "Sheker-Colsheu",
                Street = "Sokolov",
                StreetNo = 0,
                Entrance = null,
                ApartmentNo = null,
                Postal = 10101,
            });

            addresses.Add(new Address()
            {
                AddressId = ++i,
                RegionId = region.Id,
                City = "",
                Street = "",
                StreetNo = 0,
                Entrance = null,
                ApartmentNo = null,
                Postal = 0,
            });

            return addresses;
        }
        public List<UserImage> GetUserImages()
        {
            List<UserImage> images = new List<UserImage>();
            FileStream stream;
            BinaryReader br;
            byte[] image = null;

            //string pathStart = @"C:\Users\User\Desktop\האקר-יו\MallMart\MallMartServer\MallMartDB\assests\";
            string pathStart = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\MallMartDB\assests\";
            string pathEnd = @"-image.jpg";
            string path;

            for (int i = 1; i < 13; i++)
            {
                path = pathStart + i.ToString() + pathEnd;
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(stream);
                image = br.ReadBytes((int)stream.Length);

                images.Add(new UserImage()
                {
                    Id = i,
                    Image = image
                });
            }

            path = pathStart + "abstract-user-flat-1.svg";
            stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(stream);
            image = br.ReadBytes((int)stream.Length);

            images.Add(new UserImage()
            {
                Id = 13,
                Image = image
            });

            return images;
        }
        public List<User> GetUsers(List<UserImage> images)
        {
            var users = new List<User>();
            User user;
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            int i = 0;

            user = new User()
            {
                Id = ++i,
                FirstName = "Rotem",
                LastName = "Dan",
                Email = "rotemdan@gmail.com",
                Phone = "0522785961",
                Username = "rotemdan",
                Authorization = "Manager",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "123456");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Idan",
                LastName = "Malka",
                Email = "idanMalka@gmail.com",
                Phone = "0548859162",
                Username = "idanMalka",
                Authorization = "AcquisitonManager",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "654321");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Nofar",
                LastName = "Gindi",
                Email = "nofargindi@gmail.com",
                Phone = "0534568271",
                Username = "nofari",
                Authorization = "DeliveryManager",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "963852");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Roi",
                LastName = "Ashkenazi",
                Email = "ashkenaziroi@gmail.com",
                Phone = "0579485237",
                Username = "Ashkenazi",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "852963");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Noa",
                LastName = "Ashkenazi",
                Email = "ashkenaziNoa@gmail.com",
                Phone = "0579485238",
                Username = "Noaly",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "852741");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Gal",
                LastName = "Malka",
                Email = "galmalka@gmail.com",
                Phone = "0547891543",
                Username = "GalMalka",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "761843");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Liran",
                LastName = "Lisha",
                Email = "lisha@gmail.com",
                Phone = "0576421849",
                Username = "Liran",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "761943");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Avi",
                LastName = "Nasi",
                Email = "avinasi@gmail.com",
                Phone = "0778214976",
                Username = "AviNasi2",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "849562");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Avi",
                LastName = "Levi",
                Email = "avilevi@gmail.com",
                Phone = "0521035084",
                Username = "AbrahamLevi",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "030201");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Roni",
                LastName = "Cohen",
                Email = "roniCohen@gmail.com",
                Phone = "0504443197",
                Username = "RoniC",
                Authorization = "DeliveryBoy",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "105021");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Dani",
                LastName = "Shovevani",
                Email = "danidani@gmail.com",
                Phone = "0508789434",
                Username = "Shovav",
                Authorization = "Customer",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "805060");
            users.Add(user);

            user = new User()
            {
                Id = ++i,
                FirstName = "Mor",
                LastName = "Biton",
                Email = "morBiton@gmail.com",
                Phone = "0553021684",
                Username = "Mor123",
                Authorization = "Customer",
                ImageId = images[i - 1].Id
            };
            user.HashedPassword = hasher.HashPassword(user, "465984");
            users.Add(user);

            return users;

        }
        public List<Customer> GetCustomers(List<Address> addresses, List<User> users)
        {
            List<Customer> customers = new List<Customer>();
            Address address;
            User user;
            int i = 0;

            address = addresses.Where(c => c.Street == "Shaked").FirstOrDefault();
            user = users.Where(c => c.Username == "Mor123").FirstOrDefault();
            customers.Add(new Customer()
            {
                CustomerId = ++i,
                AddressId = address.AddressId,
                UserId = user.Id,
                PaymentMethod = PaymentMethod.Paypal,
                PaymentDetails = "Paypal Account: morBiton@gmail.com. Password: 465984"
            });

            address = addresses.Where(c => c.Street == "Dogit").FirstOrDefault();
            user = users.Where(c => c.Username == "Shovav").FirstOrDefault();
            customers.Add(new Customer()
            {
                CustomerId = ++i,
                AddressId = address.AddressId,
                UserId = user.Id,
                PaymentMethod = PaymentMethod.Bitcoin,
                PaymentDetails = "I will pay you. For real. It's Bitcoin. It works. Wait for it..."
            });

            return customers;
        }
        public List<Employee> GetEmployees(List<User> users)
        {
            List<Employee> employees = new List<Employee>();
            User user;
            int i = 0;

            user = users.Where(u => u.Username == "rotemdan").FirstOrDefault();
            Employee rotem = new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = null,
                Manager = null,
                JobTitle = Job.Manager,
                DeliveryRegions = null,
                Employees = null
            };
            employees.Add(rotem);

            user = users.Where(u => u.Username == "idanMalka").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = rotem.Id,
                JobTitle = Job.AcquisitonManager,
                DeliveryRegions = null,
                Employees = null
            });

            user = users.Where(u => u.Username == "nofari").FirstOrDefault();
            Employee nofar = new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = rotem.Id,
                JobTitle = Job.DeliveryManager,
                DeliveryRegions = null,
                Employees = null
            };
            employees.Add(nofar);

            user = users.Where(u => u.Username == "Ashkenazi").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "Noaly").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "GalMalka").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "Liran").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "AviNasi2").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "AbrahamLevi").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            user = users.Where(u => u.Username == "RoniC").FirstOrDefault();
            employees.Add(new Employee()
            {
                Id = ++i,
                UserId = user.Id,
                ManagerId = nofar.Id,
                JobTitle = Job.DeliveryBoy,
                Employees = null
            });

            return employees;
        }
        public List<EmployeeRegion> GetEmployeeRegions(List<Employee> employees, List<Region> regions)
        {
            List<EmployeeRegion> employeesRegions = new List<EmployeeRegion>();
            Region north = regions.Where(r => r.Name == "North").FirstOrDefault();
            Region south = regions.Where(r => r.Name == "South").FirstOrDefault();
            Region jerusalem = regions.Where(r => r.Name == "Jerusalem").FirstOrDefault();
            Region telaviv = regions.Where(r => r.Name == "Tel Aviv").FirstOrDefault();
            Region sharon = regions.Where(r => r.Name == "Sharon").FirstOrDefault();
            Employee employee;
            int i = 0;

            employee = employees.Where(e => e.Id == 4).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = telaviv.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = jerusalem.Id
            });

            employee = employees.Where(e => e.Id == 5).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = telaviv.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = south.Id
            });

            employee = employees.Where(e => e.Id == 6).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = north.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });

            employee = employees.Where(e => e.Id == 7).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = north.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = jerusalem.Id
            });

            employee = employees.Where(e => e.Id == 8).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = north.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = jerusalem.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = south.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = telaviv.Id
            });

            employee = employees.Where(e => e.Id == 9).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = north.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = jerusalem.Id
            });

            employee = employees.Where(e => e.Id == 10).FirstOrDefault();
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = sharon.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = south.Id
            });
            employeesRegions.Add(new EmployeeRegion()
            {
                Id = ++i,
                EmployeeId = employee.Id,
                RegionId = telaviv.Id
            });

            return employeesRegions;
        }
        public List<Order> GetOrders(List<OrderLine> lines, List<Employee> employees, List<Customer> customers, List<Product> products)
        {
            List<Order> orders = new List<Order>();
            Employee employee = new Employee();
            Customer customer = new Customer();
            Product product = new Product();
            Order order = new Order();
            OrderLine line = new OrderLine();
            int i = 0;
            int j = 0;

            customer = customers.FirstOrDefault();
            employee = employees.Where(e => e.Id == 4).FirstOrDefault();
            order = new Order()
            {
                OrderId = ++i,
                CustomerId = customer.CustomerId,
                DateOrdered = new DateTime(2021, 11, 17, 11, 43, 13),
                DueTimeFirst = new DateTime(2021, 11, 19, 8, 0, 0),
                DueTimeLast = new DateTime(2021, 11, 19, 12, 0, 0),
                ArrivalTime = new DateTime(2021, 11, 19, 9, 51, 42),
                EmployeeId = employee.Id,
                IsOrderDone = true,
                TotalPrice = 76.59f,
                PricePaid = 76.59f
            };

            product = products.Where(p => p.Id == 23).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 4
            };
            lines.Add(line);
            product = products.Where(p => p.Id == 22).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 6
            };
            lines.Add(line);
            product = products.Where(p => p.Id == 17).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);

            orders.Add(order);


            employee = employees.Where(e => e.Id == 10).FirstOrDefault();
            order = new Order()
            {
                OrderId = ++i,
                CustomerId = customer.CustomerId,
                DateOrdered = new DateTime(2022, 1, 3, 16, 2, 53),
                DueTimeFirst = new DateTime(2022, 1, 10, 12, 0, 0),
                DueTimeLast = new DateTime(2022, 1, 10, 16, 0, 0),
                ArrivalTime = new DateTime(2022, 1, 10, 14, 12, 20),
                EmployeeId = employee.Id,
                IsOrderDone = true,
                TotalPrice = 999.99f,
                PricePaid = 999.99f
            };

            product = products.Where(p => p.Id == 14).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);

            orders.Add(order);


            customer = customers.Where(c => c.CustomerId == 2).FirstOrDefault();
            order = new Order()
            {
                OrderId = ++i,
                CustomerId = customer.CustomerId,
                DateOrdered = new DateTime(2022, 1, 4, 18, 21, 53),
                DueTimeFirst = new DateTime(2022, 1, 18, 16, 0, 0),
                DueTimeLast = new DateTime(2022, 1, 18, 20, 0, 0),
                ArrivalTime = new DateTime(2022, 1, 18, 18, 12, 20),
                EmployeeId = employee.Id,
                IsOrderDone = true,
                TotalPrice = 672570f,
                PricePaid = 672570f
            };

            product = products.Where(p => p.Id == 28).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);

            orders.Add(order);


            employee = employees.Where(e => e.Id == 5).FirstOrDefault();
            order = new Order()
            {
                OrderId = ++i,
                CustomerId = customer.CustomerId,
                DateOrdered = new DateTime(2022, 1, 5, 19, 43, 43),
                DueTimeFirst = new DateTime(2022, 1, 7, 16, 0, 0),
                DueTimeLast = new DateTime(2022, 1, 7, 20, 0, 0),
                ArrivalTime = new DateTime(2022, 1, 7, 18, 23, 0),
                EmployeeId = employee.Id,
                IsOrderDone = true,
                Comment = "",
                TotalPrice = 188.24f,
                PricePaid = 188.24f
            };

            product = products.Where(p => p.Id == 1).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);
            product = products.Where(p => p.Id == 2).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);
            product = products.Where(p => p.Id == 3).FirstOrDefault();
            line = new OrderLine()
            {
                Id = ++j,
                OrderId = order.OrderId,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                Quantity = 1
            };
            lines.Add(line);

            orders.Add(order);

            return orders;
        }
    }
}
