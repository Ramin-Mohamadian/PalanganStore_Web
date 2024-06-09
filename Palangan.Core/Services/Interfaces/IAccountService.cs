using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Palangan.Core.Dtos.AdminUser;
using Palangan.Core.Dtos.User;
using Palangan.DataLayer.Entities.Users;

namespace Palangan.Core.Services.Interfaces
{
    public interface IAccountService
    {
        #region Account


        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int RegisterUser(SignUpViewModel user);
        
        User GetUserById(int UserId);

        User GetUserByActiveCode(string ActiveCode);
        
       bool ActiveAccount(string ActiveCode);

        User GetUserByEmail(string Email);

        void UpdateUser(User user);

        User LoginUser(LoginViewModel login);
        #endregion

        #region AdminAccount
        List<AdminUserListViewModel> GetAllUserForAdminList(string name="");
        int AddminAddUser(AdminAddUserViewModel user);

        AdminEditUserViewModel GetUserForEdit(int UserId);

        void EditeUser(AdminEditUserViewModel editUserViewModel);

        void DeleteUser(int UserId);

        int UserCont();
        #endregion
    }
}
