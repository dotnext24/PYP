using PYP.Domain.Entities;
using PYP.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Services
{
    public interface IMembershipService
    {
        ValidUserContext ValidateUser(string username, string password);
        OperationResult<UserWithRoles> CreateUser(string userName,string email,string password);
        OperationResult<UserWithRoles> CreateUser(string userName,string email,string password,string role);
        OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles);
        UserWithRoles UpdateUser(User user, string userName, string email);

        bool ChangePassword(string userNmae, string oldPassword, string newPassword);

        bool AddToRole(Guid userKey, string role);
        bool AddToRole(string userName, string role);

        bool RemoveFromRole(string userName, string role);

        IEnumerable<Role> GetRoles(Guid userKey);

        Role GetRole(Guid roleKey);

        Role GetRole(string name);

        PaginatedList<UserWithRoles> GetUsers(int pageIndex,int pageSize);

        UserWithRoles GetUser(Guid userKey);

        UserWithRoles GetUser(string name);
              
            
    }
}
