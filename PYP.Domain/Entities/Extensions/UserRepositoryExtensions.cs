using PYP.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP.Domain.Entities.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static User GetSingleByUserName(this IEntityRepository<User> userRepository,string userName)
        {
           return userRepository.GetAll().FirstOrDefault(x => x.Name == userName);
        }
    }
}
