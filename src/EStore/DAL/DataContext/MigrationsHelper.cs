using DAL.Entities.Store;
using DAL.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DAL.DataContext
{
    public class MigrationsHelper
    {
        private static readonly AppConfigration _appConfigration = new AppConfigration();

        public static void SeedingUp(MigrationBuilder migrationBuilder)
        {
            var timer = Stopwatch.StartNew();
            Login(migrationBuilder);
            Store(migrationBuilder);
            timer.Stop();
            Console.WriteLine($"MigrationsHelper.SeedingUp() Completed in {timer.Elapsed.ToString("c")}");
        }

        public static void SeedingDown(MigrationBuilder migrationBuilder)
        {
            var timer = Stopwatch.StartNew();
            TruncateAllTables(migrationBuilder);
            timer.Stop();
            Console.WriteLine($"MigrationsHelper.SeedingDown() Completed in {timer.Elapsed.ToString("c")}");
        }

        #region Helpers
        private static void TruncateAllTables(MigrationBuilder migrationBuilder)
        {
            var timer = Stopwatch.StartNew();
            migrationBuilder.Sql("DELETE FROM OrderProducts", true);
            migrationBuilder.Sql("DELETE FROM Orders", true);
            migrationBuilder.Sql("DELETE FROM Comments", true);

            migrationBuilder.Sql("DELETE FROM ProductFeatures", true);
            migrationBuilder.Sql("DELETE FROM Products", true);
            migrationBuilder.Sql("DELETE FROM Markas", true);

            migrationBuilder.Sql("DELETE FROM Colors", true);
            migrationBuilder.Sql("DELETE FROM Sizes", true);

            migrationBuilder.Sql("DELETE FROM Users", true);
            migrationBuilder.Sql("DELETE FROM Roles", true);
            timer.Stop();
            Console.WriteLine($"MigrationsHelper.TruncateTables() Completed in {timer.Elapsed.ToString("c")}");
        }

        private static void Login(MigrationBuilder migrationBuilder)
        {
            var random = new Random();

            // Roles
            AddRole(migrationBuilder, 1, "SysAdmin");
            AddRole(migrationBuilder, 2, "Admin");
            AddRole(migrationBuilder, 3, "Customer");

            // Users
            AddUser(migrationBuilder, 1, "sysadmin", "sysadmin123", 1, "SysAdmin");
            AddUser(migrationBuilder, 2, "admin", "admin123", 2, "Admin");
            AddUser(migrationBuilder, 3, "customer", "customer123", 3, "Customer");
            for (int i = 4; i <= 20; i++)
            {
                AddUser(migrationBuilder, i, $"customer {random.Next(10, 99)}", "customer123", 3, "Customer");
            }
        }

        private static void Store(MigrationBuilder migrationBuilder)
        {
            var random = new Random();

            #region Features

            // Colors
            AddColor(migrationBuilder, 1, "Black", "#000000");
            AddColor(migrationBuilder, 2, "Red", "#DD0000");
            AddColor(migrationBuilder, 3, "Orange", "#FE6230");
            AddColor(migrationBuilder, 4, "Yellow", "#FEF600");
            AddColor(migrationBuilder, 5, "Green", "#00BB00");
            AddColor(migrationBuilder, 6, "Blue", "#009BFE");
            AddColor(migrationBuilder, 7, "Indigo", "#000083");
            AddColor(migrationBuilder, 8, "Violet", "#30009B");
            AddColor(migrationBuilder, 9, "White", "#FFFFFF");

            // Sizes
            AddSize(migrationBuilder, 1, "Large", "Large");
            AddSize(migrationBuilder, 2, "Medium", "Medium");
            AddSize(migrationBuilder, 3, "Small", "Small");

            #endregion Features

            #region Markas
            var markasMaxCount = 10;
            for (int i = 1; i <= markasMaxCount; i++)
            {
                var paragraph = Faker.Lorem.Paragraph();
                if (paragraph.Length > 230)
                {
                    paragraph = paragraph[..225];
                }
                AddMarka(migrationBuilder,
                    i,
                    Faker.Company.Name(),
                    Faker.Company.Suffix() + Environment.NewLine + paragraph
                );
            }
            #endregion

            #region Products
            var prodcutsMaxCount = 50;
            var products = new List<Product>();
            for (int i = 1; i <= prodcutsMaxCount; i++)
            {
                var paragraph = Faker.Lorem.Paragraph();
                if (paragraph.Length > 230)
                {
                    paragraph = paragraph[..225];
                }
                var product = new Product
                {
                    Id = i,
                    Name = $"{Faker.Enum.Random<Products>()} - {random.Next(100, 999)}",
                    Description = paragraph,
                    Price = (random.NextDouble() + 1d) * random.Next(1000, 9999),
                    MarkaId = random.Next(1, markasMaxCount),
                };

                products.Add(product);

                AddProduct(migrationBuilder,
                    product.Id,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.MarkaId
                );
            }
            #endregion

            #region ProductFeatures
            for (int i = 1; i <= prodcutsMaxCount; i++)
            {
                // Size
                for (int k = 1; k <= 3; k++)
                {
                    // Color
                    for (int j = 1; j <= 9; j++)
                    {
                        AddProductFeature(migrationBuilder,
                            products[i - 1].Price * (random.Next(15, 50) / 100),
                            products[i - 1].Id,
                            j,
                            k
                        );
                    }
                }
            }
            #endregion

            #region Comments
            for (int i = 1; i <= prodcutsMaxCount; i++)
            {
                var cnt = random.Next(1, 5);
                for (int k = 1; k <= cnt; k++)
                {
                    AddComment(migrationBuilder,
                        string.Join(" ", Faker.Lorem.Words(random.Next(5, 15))),
                        products[i - 1].Id,
                        random.Next(2, 3)
                    );
                }
            }
            #endregion

            #region Orders
            var ordersMaxCount = 25;
            var orders = new List<Order>();
            for (int i = 1; i <= ordersMaxCount; i++)
            {
                var order = new Order { Id = i, UserId = random.Next(3, 20) };
                orders.Add(order);
                AddOrder(migrationBuilder, order.Id, order.UserId);
            }
            #endregion

            #region OrderProducts
            for (int i = 0; i < 250; i++)
            {
                AddOrderProduct(migrationBuilder,
                    orders[random.Next(0, ordersMaxCount - 1)].Id,
                    products[random.Next(0, prodcutsMaxCount - 1)].Id
                );
            } 
            #endregion
        }

        #region Login

        private static void AddRole(MigrationBuilder migrationBuilder, long id, string name)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] {
                    "Id",
                    "Name",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    name,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddUser(MigrationBuilder migrationBuilder, long id, string username, string pasword, long roleId, string role)
        {
            CreatePasswordHash(pasword, out byte[] passHash1, out byte[] passSalt1);
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] {
                    "Id",
                    "Username",
                    "Token",
                    "PasswordHash",
                    "PasswordSalt",
                    "RoleId",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    username,
                    TokenGenerate(id, role, out DateTime expires1),
                    passHash1,
                    passSalt1,
                    roleId,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        #endregion Login

        #region Store

        #region Features

        private static void AddColor(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddSize(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        #endregion Features

        private static void AddMarka(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Markas",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddProduct(MigrationBuilder migrationBuilder, long id, string name, string description, double price, long markaId)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                    "Price",
                    "MarkaId",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                    price,
                    markaId,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddProductFeature(MigrationBuilder migrationBuilder, double discount, long productId, long colorId, long sizeId)
        {
            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] {
                    "Discount",
                    "ProductId",
                    "ColorId",
                    "SizeId",
                    "CreatedAt",
                },
                values: new object[] {
                    discount,
                    productId,
                    colorId,
                    sizeId,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddComment(MigrationBuilder migrationBuilder, string content, long productId, long userId)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] {
                    "Content",
                    "ProductId",
                    "UserId",
                    "CreatedAt",
                },
                values: new object[] {
                    content,
                    productId,
                    userId,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddOrder(MigrationBuilder migrationBuilder, long id, long userId)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] {
                    "Id",
                    "UserId",
                    "CreatedAt",
                },
                values: new object[] {
                    id,
                    userId,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                }
            );
        }

        private static void AddOrderProduct(MigrationBuilder migrationBuilder, long orderId, long productId)
        {
            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] {
                    "OrderId",
                    "ProductId",
                },
                values: new object[] {
                    orderId,
                    productId,
                }
            );
        }

        #endregion Store

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static string TokenGenerate(long id, string role, out DateTime expires)
        {
            expires = DateTime.UtcNow.AddDays(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appConfigration.SecretJwt);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim("id", id.ToString()), new Claim("role", role.ToString()) }),
                // new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, id.ToString()) }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion Helpers
    }

    internal enum Products
    {
        AutomotiveProduct,
        ElectronicProduct,
        FoodProduct,
        GameProduct,
        HealthCareProduct,
        IndustrialProduct,
        KitchenProduct,
        ScientificProduct,
        SoftwareProduct,
    }
}