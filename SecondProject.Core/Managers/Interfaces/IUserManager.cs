using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.Core.Managers.Interfaces
{
    public interface IUserManager
    {
        public UserModel UpdateProfile(UserModel currentUser, UserModel request);
        public LoginUserResponse Login(LoginModelView userReg);

        public LoginUserResponse SignUp(UserRegistrationViewModel userReg);
        void DeleteUser(UserModel currentUser, int id);
    }
}
