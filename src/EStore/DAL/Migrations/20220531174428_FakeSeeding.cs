using DAL.Entities.Store;
using DAL.Entities.Store.Features;
using DAL.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

#nullable disable

namespace DAL.Migrations
{
    public partial class FakeSeeding : Migration
    {
        private static readonly AppConfigration _appConfigration = new AppConfigration();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var timer = Stopwatch.StartNew();
            Login(migrationBuilder);
            Store(migrationBuilder);
            timer.Stop();
            Console.WriteLine($"FakeSeeding (Up) Completed in {timer.Elapsed.ToString("HH:mm:ss.ffffff")}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var timer = Stopwatch.StartNew();
            migrationBuilder.Sql("DELETE FROM ProductFeatures", true);
            migrationBuilder.Sql("DELETE FROM Products", true);
            migrationBuilder.Sql("DELETE FROM Markas", true);

            migrationBuilder.Sql("DELETE FROM Colors", true);
            migrationBuilder.Sql("DELETE FROM Sizes", true);

            migrationBuilder.Sql("DELETE FROM Users", true);
            migrationBuilder.Sql("DELETE FROM Roles", true);
            timer.Stop();
            Console.WriteLine($"FakeSeeding (Down) Completed in {timer.Elapsed.ToString("HH:mm:ss.ffffff")}");
        }

        #region Helpers
        private void Login(MigrationBuilder migrationBuilder)
        {
            // Roles
            AddRole(migrationBuilder, 1, "SysAdmin");
            AddRole(migrationBuilder, 2, "Admin");
            AddRole(migrationBuilder, 3, "Customer");

            // Users
            AddUser(migrationBuilder, 1, "sysadmin", "sysadmin123", 1, "SysAdmin");
            AddUser(migrationBuilder, 2, "admin", "admin123", 2, "Admin");
            AddUser(migrationBuilder, 3, "customer", "customer123", 3, "Customer");
        }

        private void Store(MigrationBuilder migrationBuilder)
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
            #endregion

            // Markas
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

            // Products
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

            // ProductFeatures
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
        }

        #region Login

        private void AddRole(MigrationBuilder migrationBuilder, long id, string name)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] {
                    "Id",
                    "Name",
                },
                values: new object[] {
                    id,
                    name,
                }
            );
        }

        private void AddUser(MigrationBuilder migrationBuilder, long id, string username, string pasword, long roleId, string role)
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
                },
                values: new object[] {
                    id,
                    username,
                    TokenGenerate(id, role, out DateTime expires1),
                    passHash1,
                    passSalt1,
                    roleId,
                }
            );
        }

        #endregion

        #region Store

        #region Features
        private void AddColor(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                }
            );
        }

        private void AddSize(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                }
            );
        }

        #endregion

        private void AddMarka(MigrationBuilder migrationBuilder, long id, string name, string description)
        {
            migrationBuilder.InsertData(
                table: "Markas",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                },
                values: new object[] {
                    id,
                    name,
                    description,
                }
            );
        }

        private void AddProduct(MigrationBuilder migrationBuilder, long id, string name, string description, double price, long markaId)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] {
                    "Id",
                    "Name",
                    "Description",
                    "Price",
                    "MarkaId"
                },
                values: new object[] {
                    id,
                    name,
                    description,
                    price,
                    markaId
                }
            );
        }

        private void AddProductFeature(MigrationBuilder migrationBuilder, long id, double discount, long productId, long colorId, long sizeId)
        {
            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] {
                    "Id",
                    "Discount",
                    "ProductId",
                    "ColorId",
                    "SizeId",
                },
                values: new object[] {
                    id,
                    discount,
                    productId,
                    colorId,
                    sizeId,
                }
            );
        }

        private void AddProductFeature(MigrationBuilder migrationBuilder, double discount, long productId, long colorId, long sizeId)
        {
            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] {
                    "Discount",
                    "ProductId",
                    "ColorId",
                    "SizeId",
                },
                values: new object[] {
                    discount,
                    productId,
                    colorId,
                    sizeId,
                }
            );
        }

        #endregion

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

        private string TokenGenerate(long id, string role, out DateTime expires)
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

        #endregion
    }

    enum Products
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
