using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using NewSport.Domain.Concrete;
using NewSport.Domain.Entities;

namespace NewSport.Domain.Filters
{
    public sealed class DefaultRoleProvider:RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            return GetUsersInRole(username).Contains(roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var dbContext = new DefaultDbContext())
            {
                User user = dbContext.Users.FirstOrDefault(x => x.Username == username || x.Email == username);
                var roles = from role in dbContext.Roles
                    where role.Id == user.RoleId
                    select role.Name;
                return roles.ToArray();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            using (var dbContext = new DefaultDbContext())
            {
                return dbContext.Roles.Select(x => x.Name).ToArray();
            }
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}
