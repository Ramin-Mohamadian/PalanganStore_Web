using Microsoft.AspNetCore.Mvc;
using Palangan.Core.Dtos.AdminUser;
using Palangan.Core.Dtos.User;
using Palangan.Core.Generator;
using Palangan.Core.Security;
using Palangan.Core.Services.Interfaces;
using Palangan.DataLayer.Context;
using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Services
{
    public class AccountService : IAccountService
    {
        private MyContext _context;

        public AccountService(MyContext myContext)
        {
            _context = myContext;

        }


        #region Account


        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public int RegisterUser(SignUpViewModel user)
        {
            User register = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password=PasswordHelper.EncodePasswordMd5(user.Password),
                NumberPhone=user.NumberPhone,
                ActiveCode=NameGenerator.GenerateUniqeName(),
                Address=user.Address,
                IsActive=false,
                IsDelete=false,
                RegisterDate=DateTime.Now,
                UserAvatar="DefaultProfile.png"
            };

            _context.Users.Add(register);
            _context.SaveChanges();
            return register.UserId;

        }


        public User GetUserById(int UserId)
        {
            return _context.Users.Find(UserId);
        }

        public User GetUserByActiveCode(string ActiveCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == ActiveCode);
        }

        public bool ActiveAccount(string ActiveCode)
        {
            var user = GetUserByActiveCode(ActiveCode);
            if (user == null||user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode=NameGenerator.GenerateUniqeName();
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public User GetUserByEmail(string Email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == Email);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public User LoginUser(LoginViewModel login)
        {
            var hashpass = PasswordHelper.EncodePasswordMd5(login.Password);

            return _context.Users.SingleOrDefault(u => u.Email == login.Email&&u.Password==hashpass);
        }


        #endregion

        #region AdminAccount

        public List<AdminUserListViewModel> GetAllUserForAdminList(string name = "")
        {

            if (name!=null)
            {
                return _context.Users.Where(u => u.UserName.Contains(name)).Select(u => new AdminUserListViewModel()
                {
                    UserId=u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActiveUser = u.IsActive,
                    NumberPhone = u.NumberPhone,
                    ImageName=u.UserAvatar
                }).ToList();
            }
            else
            {
                return _context.Users.Select(u => new AdminUserListViewModel()
                {
                    UserId=u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActiveUser = u.IsActive,
                    NumberPhone = u.NumberPhone,
                    ImageName=u.UserAvatar
                }).ToList();
            }
        }

        public int AddminAddUser(AdminAddUserViewModel user)
        {
            User adduser = new User();


            adduser.UserName = user.UserName;
            adduser.Email = user.Email;
            adduser.Password =PasswordHelper.EncodePasswordMd5(user.Password);
            adduser.NumberPhone = user.NumberPhone;
            adduser.Address = user.Address;
            adduser.ActiveCode =NameGenerator.GenerateUniqeName();
            adduser.IsActive =true;
            adduser.IsDelete =false;
            adduser.RegisterDate =DateTime.Now;


            #region saveAvatar
            if (user.UserAvatar!=null)
            {
                string imagePath = "";
                adduser.UserAvatar = NameGenerator.GenerateUniqeName() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/Users", adduser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }
            }
            else
            {
                adduser.UserAvatar = "DefaultProfile.png";
            }

            #endregion

            _context.Users.Add(adduser);
            _context.SaveChanges();
            return adduser.UserId;
        }

        public AdminEditUserViewModel GetUserForEdit(int UserId)
        {
            return _context.Users.Where(u => u.UserId==UserId).Select(u => new AdminEditUserViewModel
            {

                userId=u.UserId,
                UserName=u.UserName,
                Email=u.Email,
                NumberPhone=u.NumberPhone,
                Address=u.Address,
                Password=u.Password,
                AvatarName = u.UserAvatar.ToString(),
                UserRoles= u.UserRoles.Select(r => r.RoleId).ToList()
            }).First();
        }

        public void EditeUser(AdminEditUserViewModel editUserViewModel)
        {
            User user = GetUserById(editUserViewModel.userId);

            user.Email = editUserViewModel.Email;
            user.UserName = editUserViewModel.UserName;
            user.NumberPhone = editUserViewModel.NumberPhone;
            user.Address = editUserViewModel.Address;
            if (editUserViewModel.Password!=null)
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUserViewModel.Password);
            }

            if (editUserViewModel.UserAvatar!=null)
            {

                string imagepath = "";
                if (editUserViewModel.AvatarName!="DefaultProfile.png")
                {
                    imagepath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/Users", editUserViewModel.AvatarName);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath);
                    }
                }

                user.UserAvatar=NameGenerator.GenerateUniqeName()+Path.GetExtension(editUserViewModel.UserAvatar.FileName);

                imagepath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/Users", user.UserAvatar);
                using (var stream = new FileStream(imagepath, FileMode.Create))
                {
                    editUserViewModel.UserAvatar.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();

        }

        public void DeleteUser(int UserId)
        {
            var user = GetUserById(UserId);
            if (user != null)
            {
                user.IsDelete = true;
            }
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public int UserCont()
        {
            return _context.Users.Count();
        }

        #endregion
    }
}
