using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PYP.Domain.Entities;
using PYP.Domain.Entities.Core;

namespace PYP.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<UserInRole> _userInRoleRepository;
        private readonly ICryptoService _cryptoService;


        public MembershipService(IEntityRepository<User> userRepository,IEntityRepository<Role> roleRepository, IEntityRepository<UserInRole> userInRoleRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _cryptoService = cryptoService;
        }

        private bool IsUserValid(User user,string password)
        {

        }
        


        public bool AddToRole(string userName, string role)
        {
            throw new NotImplementedException();
        }

        public bool AddToRole(Guid userKey, string role)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userNmae, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles)
        {
            throw new NotImplementedException();
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string name)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(Guid roleKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(string name)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles GetUser(Guid userKey)
        {
            throw new NotImplementedException();
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromRole(string userName, string role)
        {
            throw new NotImplementedException();
        }

        public UserWithRoles UpdateUser(User user, string userName, string email)
        {
            throw new NotImplementedException();
        }

        public ValidUserContext ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
