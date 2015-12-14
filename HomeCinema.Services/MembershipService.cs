using HomeCinema.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeCinema.Entities;
using HomeCinema.Data;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Extensions;

namespace HomeCinema.Services
{
    public class MembershipService : IMembershipService
    {
        #region
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository;
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        public MembershipService(IEntityBaseRepository<User> userRepository, 
            IEntityBaseRepository<Role> roleRepository,
            IEntityBaseRepository<UserRole> userRoleRepository,
            IEncryptionService encryptionService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        } 

        private void addUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role does not exist");
            }
            var userRole = new UserRole()
            {
                RoleId = role.ID,
                UserId = user.ID
            };
            _userRoleRepository.Add(userRole);

        }

        private bool isPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsLocked;
            }
            return false;
        }   

        public User CreateUser(string username, string email, string password, int[] roles)
        {
            var existingUser = _userRepository.GetSingleByUsername(username);
        

        }

        public User GetUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetUserRoles(string username)
        {
            throw new NotImplementedException();
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
