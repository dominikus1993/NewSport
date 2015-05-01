using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using NewSport.Domain.Api;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Concrete
{
    public class DefaultUserRepository:IUserRepository
    {
        private readonly DefaultDbContext _dbContext;
        private readonly IEncryptProvider _encryptProvider;

        public DefaultUserRepository(IEncryptProvider provider)
        {
            _encryptProvider = provider;
            this._dbContext = new DefaultDbContext();
        }

        public IQueryable<User> Users
        {
            get { return _dbContext.Users; }
        }

        public List<string> Roles
        {
            get
            {
                return Users.Select(usr => usr.Roles).ToList();
            }
        }

        public void Save(User user)
        {
           
            if (user.Id == 0)
            {
                user.Password = _encryptProvider.Encrypt(user.Password);
                _dbContext.Users.Add(user);
            }
            else
            {
                EditUser(user);
            }
            _dbContext.SaveChanges();
        }

        private void EditUser(User user)
        {
            User editingUser = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);
            if (editingUser != null)
            {
                editingUser.Username = user.Username;
                editingUser.Email = user.Email;
            }
        }

        public bool LogIn(string username, string password)
        {
            string hashPassword = _encryptProvider.Encrypt(password);
            bool loginResult = Users.Any(x => x.Username == username && x.Password == hashPassword);
            if (loginResult)
            {
                FormsAuthentication.SetAuthCookie(username, false);
                HttpContext.Current.Session["user"] = username;
            }
            return loginResult;
        }

        public void LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session["user"] = null;
        }

        public User FindById(int? id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User FindByUsername(string username)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public void Delete(User user)
        {         
                _dbContext.Users.Remove(user);        
        }
    }
}
