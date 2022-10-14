using AutoMapper;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.Core.Managers
{
    public class RoleManager : IRoleManager
    {

        private secdbContext _secdbContext;
        private IMapper _mapper;

        public RoleManager(secdbContext secdbContext, IMapper mapper)
        {
            _secdbContext = secdbContext;
            _mapper = mapper;
        }
        public bool CheckAccess(UserModel userModel)
        {
            var isAdmin = _secdbContext.Users
                                       .Any(a => a.Id == userModel.Id && a.IsAdmin);
            return isAdmin;
        }
    }
}
