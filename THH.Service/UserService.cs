using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using THH.Core;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;
using THH.Model.Dto;

namespace THH.Service
{
    public class UserService
    {
        private IRepository<User> userRepository = null;
        private IRepository<Role> roleRepository = null;
        public bool isSave = true;

        public UserService()
        {
            userRepository = new RepositoryBase<User>();
            roleRepository = new RepositoryBase<Role>();
        }
        public List<UserDto> GetAllUsers()
        {
            var user = userRepository.Entities.ToList();
            return Mapper.Map<List<User>, List<UserDto>>(user);
        }
        public User GetUser(int id)
        {
            return userRepository.Find(id);
        }
        public UserDto Find(int id)
        {
            User user = userRepository.Find(id);
            return Mapper.Map<User, UserDto>(user);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="userName"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<UserDto> GetUserGrid(int limit, int offset, string userName, string loginName)
        {
            var user = userRepository.Entities.ToList();
            return Mapper.Map<List<User>, List<UserDto>>(user);
        }
        /// <summary>
        /// 验证登录用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string loginName, string password)
        {
            try
            {
                var user = userRepository.Entities;//
                return user.Where(p => p.LogingName == loginName && p.Password == password && !p.IsDelete).FirstOrDefault();

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool Add(UserDto userDto)
        {
            var user = Mapper.Map<UserDto, User>(userDto);
            return userRepository.Insert(user) > 0;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public bool Update(UserDto userDto)
        {
            var user = Mapper.Map<UserDto, User>(userDto);
            return userRepository.Update(user) > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Deletes(string ids)
        {
            int[] ida = ids.StrToIntArray();
            return userRepository.Delete(ida) > 0;
        }
    }
}
