using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PYP.Domain.Entities;
using PYP.Domain.Entities.Core;
using PYP.Domain.Entities.Extensions;
using System.Security.Principal;

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
            var user = _userRepository.GetSingleByUserName(userName);
            if (user != null)
            {
                addUserToRole(user, role);
                return true;
            }
            else
                return false;

        }

        public bool AddToRole(Guid userKey, string role)
        {
            var user = _userRepository.GetSingle(userKey);
            if(user!=null)
            {
                addUserToRole(user,role);
                return true;
            }

            return false;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            if(user!=null && IsPasswordValid(user,oldPassword))
            {
                user.HashedPassword = _cryptoService.EncryptPassword(newPassword, user.Salt);

                _userRepository.Edit(user);
                _userRepository.Save();

                return true;
            }

            return false;
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
            return _roleRepository.GetSingleByRoleName(name);
        }

        public Role GetRole(Guid roleKey)
        {
            return _roleRepository.GetSingle(roleKey);
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
           var user= _userRepository.GetSingleByUserName(name);
           return GetUserWithRoles(user);
        }

        private UserWithRoles GetUserWithRoles(User user)
        {
            if(user!=null)
            {
                var userRoles = GetRoles(user.Key);
                return new UserWithRoles()
                {
                    User = user,
                    Roles = userRoles
                };
            }

            return null;
        }

        public UserWithRoles GetUser(Guid userKey)
        {
            var user = _userRepository.GetSingle(userKey);
            if (user != null)
                return GetUserWithRoles(user);
            else
                return null;
        }

        public PaginatedList<UserWithRoles> GetUsers(int pageIndex, int pageSize)
        {
            var users = _userRepository.Paginate<Guid>(pageIndex, pageSize, x => x.Key,null,null);

            return new PaginatedList<UserWithRoles>(
                users.PageIndex,
                users.PageSize,
                users.TotalCount,
                users.Select(u=>new UserWithRoles() {Roles=GetRoles(u.Key),User=u }).AsQueryable()
                );
        }

        public bool RemoveFromRole(string userName, string role)
        {
            var user = _userRepository.GetSingleByUserName(userName);
            var roleEntity = _roleRepository.GetSingleByRoleName(role);
            if(user!=null && roleEntity!=null)
            {
                var userInRole = _userInRoleRepository.GetAll().FirstOrDefault(x => x.UserKey == user.Key && x.RoleKey == roleEntity.Key);
                if(userInRole!=null)
                {
                    _userInRoleRepository.Delete(userInRole);
                    _userInRoleRepository.Save();
                    return true;
                }
            }

            return false;
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
            var userCtx = new ValidUserContext();
            var user = _userRepository.GetSingleByUserName(username);
            if(user!=null && IsUserValid(user,password))
            {
                var userRoles = GetRoles(user.Key);
                userCtx.User = new UserWithRoles()
                {
                    User=user,
                    Roles=userRoles
                };

                var identity= new GenericIdentity(user.Name);
                userCtx.Principal = new GenericPrincipal(identity, userRoles.Select(x => x.Name).ToArray());
              
            }

            return userCtx;
        }
    }
}
