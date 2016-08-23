using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PYP.Domain.Entities;
using PYP.Domain.Entities.Core;
using PYP.Domain.Entities.Extensions;

namespace PYP.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityRepository<User> _userRepository;
        private readonly IEntityRepository<Role> _roleRepository;
        private readonly IEntityRepository<UserInRole> _userInRoleRepository;
        private readonly ICryptoService _cryptoService;


        public MembershipService(IEntityRepository<User> userRepository, IEntityRepository<Role> roleRepository, IEntityRepository<UserInRole> userInRoleRepository, ICryptoService cryptoService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userInRoleRepository = userInRoleRepository;
            _cryptoService = cryptoService;
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(_cryptoService.EncryptPassword(password, user.Salt), user.HashedPassword);
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
            return CreateUser(userName, email, password, role: null);
        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string[] roles)
        {
            var existingUser = _userRepository.GetAll().Any(x => x.Name == userName);
            if (existingUser)
            {
                return new OperationResult<UserWithRoles>(false);
            }

            var passwordSalt = _cryptoService.GenerateSalt();

            var user = new User() { Name = userName, Salt = passwordSalt, Email = email, IsLocked = false, HashedPassword = _cryptoService.EncryptPassword(password, passwordSalt), CreatedOn = DateTime.Now };
            _userRepository.Add(user);
            _userRepository.Save();


            if (roles != null && roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    addUserToRole(user, role);
                }
            }


            return new OperationResult<UserWithRoles>(true){Entity = GetUser(user.Name)};
        
    }

        private void addUserToRole(User user, string roleName)
        {
            var role = _roleRepository.GetSingleByRoleName(roleName);
            if(role==null)
            {
                var tempRole = new Role() { Name = roleName };
                _roleRepository.Add(tempRole);
                _roleRepository.Save();
                role = tempRole;
            }

            var userInRole = new UserInRole() { RoleKey = role.Key, UserKey = user.Key };
            _userInRoleRepository.Add(userInRole);
            _userInRoleRepository.Save();

        }

        public OperationResult<UserWithRoles> CreateUser(string userName, string email, string password, string role)
        {
            return CreateUser(userName, email, password, roles: new[] { role });
        }

        public Role GetRole(string name)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(Guid roleKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles(Guid userKey)
        {
            var userInRoles = _userInRoleRepository.FindBy(x => x.UserKey == userKey).ToList();
            if(userInRoles!=null && userInRoles.Count>0)
            {
                var rolesKey = userInRoles.Select(x => x.RoleKey).ToArray();

                var userRoles = _userRepository.FindBy(x => rolesKey.Contains(x.Key));
                return userRoles as IEnumerable<Role>;
            }

            return Enumerable.Empty<Role>();

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
            user.Name = userName;
            user.Email = email;
            user.LastUpdatedOn = DateTime.Now;

            _userRepository.Edit(user);
            _userRepository.Save();

            return GetUser(userName);
        }

        public ValidUserContext ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
