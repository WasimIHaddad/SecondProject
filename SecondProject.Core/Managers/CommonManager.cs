using AutoMapper;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace SecondProject.Core.Managers
{
    public class CommonManager : ICommonManager
    {
        private secdbContext _secdbContext;
        private IMapper _mapper;

        public CommonManager(secdbContext secdbContext, IMapper mapper)
        {
            _secdbContext = secdbContext;
            _mapper = mapper;
        }
        public UserModel GetUserRole(UserModel user)
        {
            var dbUser = _secdbContext.Users
                                      .FirstOrDefault(a => a.Id == user.Id)
                                      ?? throw new ServiceValidationException("Invalid User id received");
            return _mapper.Map<UserModel>(dbUser);
        }
    }

}
