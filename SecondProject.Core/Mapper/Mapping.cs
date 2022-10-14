using AutoMapper;
using SecondProject.Common.Extinsions;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProject.Core.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<SecView, SecModelView>().ReverseMap();

            CreateMap<LoginUserResponse,User>().ReverseMap();
            CreateMap<UserModel,User>().ReverseMap();
            CreateMap<ItemModelView,Item>().ReverseMap();
            CreateMap<PagedResult<ItemModelView>, PagedResult<Item>>().ReverseMap();
        }
    }
}
