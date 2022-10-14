using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SecondProject.Common.Helper;
using SecondProject.Core.Managers.Interfaces;
using SecondProject.DbModel.Models;
using SecondProject.ModelViews.ModelView;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tazeez.Common.Extensions;

namespace SecondProject.Core.Managers
{
    public class UserManager : IUserManager
    {
        private secdbContext _secdbContext;
        private IMapper _mapper;

        public UserManager(secdbContext secdbContext, IMapper mapper)
        {
            _secdbContext = secdbContext;
            _mapper = mapper;
        }

        #region public
        public LoginUserResponse Login(LoginModelView userReg)
        {
            var user = _secdbContext.Users
                                    .FirstOrDefault(a => a.Email
                                    .Equals(userReg.Email, StringComparison.InvariantCultureIgnoreCase));

            if (user == null || !VerifyHashPassword(userReg.Password, user.Password))
            {
                throw new ServiceValidationException("Invalid user name or password received");
            }
            var res = _mapper.Map<LoginUserResponse>(user);

            res.Token = $"Bearer {GenerateJWTToken(user)}";


            return res;
        }

        public LoginUserResponse SignUp(UserRegistrationViewModel userReg)
        {
            if (_secdbContext.Users.Any(a => a.Email.Equals(userReg.Email,
                                                               StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ServiceValidationException("User Already Exist");
            }
            var hashedPassword = HashPassword(userReg.Password);
            var user = _secdbContext.Users.Add(new User
            {
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                Email = userReg.Email.ToLower(),
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                Image = String.Empty


            }).Entity;
            _secdbContext.SaveChanges();
            var res = _mapper.Map<LoginUserResponse>(user);

            res.Token = $"Bearer {GenerateJWTToken(user)}";
            return res;
        }

        public UserModel UpdateProfile(UserModel currentUser, UserModel request)
        {
            var user = _secdbContext.Users
                 .FirstOrDefault(a => a.Id == currentUser.Id)
                 ?? throw new ServiceValidationException("User Not Found");

            var url = "";
            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "profileimages");
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseURL = "https://localhost:44371/";
                user.Image = @$"{baseURL}/api/v1/user/fileretrive/profilepic?filename={url}";
            }
            _secdbContext.SaveChanges();
            return _mapper.Map<UserModel>(user);
        }
        public void DeleteUser(UserModel currentUser, int id)
        {
            if (currentUser.Id == id)
            {
                throw new ServiceValidationException("you have no access to delete your self");

            }
            var user = _secdbContext.Users
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("User Not Found");
            user.Archived = true;
            _secdbContext.SaveChanges();
        }

        #endregion public

        #region private
        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }
        private static bool VerifyHashPassword(string password, string HashedPasword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPasword);
        }
        private string GenerateJWTToken(User user)
        {
            var jwtKey = "#test.key*&^vanthisismycustomSecretkeyforAuthentication";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("DateOfJoining", user.CreatedDate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var issuer = "test.com";

            var token = new JwtSecurityToken(
                        issuer,
                        issuer,
                        claims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        #endregion private
    }
}
