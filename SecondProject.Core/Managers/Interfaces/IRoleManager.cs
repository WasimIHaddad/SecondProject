using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.Core.Managers.Interfaces
{
    public interface IRoleManager : IManager
    {
        bool CheckAccess(UserModel userModel);
    }
}
