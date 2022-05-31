using BLL.Businesses.Base;
using COMN.Attributes;
using COMN.Extensions;
using DAL.Entities.Login;
using DAL.Models.Common;
using DAL.Repositories.Base;

namespace BLL.Businesses.Login
{
    public class UserBusiness : BaseBusiness<User>
    {
        public UserBusiness(IRepository<User> repository) : base(repository)
        {
        }

        public override async Task<List<User>> GetAll()
        {
            var users = await this._repository.GetAll().ConfigureAwait(false);
            // Nulling the password ...
            users.ForEach(u =>
            {
                var props = u.PropertiesFindByAttribute(typeof(ExcludeAttribute));
                if (props != null)
                {
                    foreach (var prop in props)
                    {
                        prop.SetValue(u, null);
                    }
                }
            });
            return users;
        }

        public override async Task<User> Get(long id)
        {
            var user = await this._repository.Get(id).ConfigureAwait(false);
            // Nulling the password ...
            var props = user.PropertiesFindByAttribute(typeof(ExcludeAttribute));
            if (props != null)
            {
                foreach (var prop in props)
                {
                    prop.SetValue(user, null);
                }
            }
            return user;
        }

        public async Task<User> Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;

            var entities = (await this._repository.GetBy((u) => u.Username == model.Username).ConfigureAwait(false))?.ToList();
            if (entities?.Count > 0)
            {
                return null;
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);

            return await this._repository.Add(new User 
            { 
                Username = model.Username, 
                PasswordHash = passwordHash, 
                PasswordSalt = passwordSalt, 
                RoleId = model.RoleId,
            }).ConfigureAwait(false);
        }

        public async Task<User> Authenticate(AuthenticateModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                return null;

            var entities = (await this._repository.GetBy((u) => u.Username == model.Username).ConfigureAwait(false))?.ToList();
            if (entities?.Count > 0)
            {
                if (VerifyPasswordHash(model.Password, entities[0].PasswordHash, entities[0].PasswordSalt))
                    return entities[0];
            }

            return null;
        }

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

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}