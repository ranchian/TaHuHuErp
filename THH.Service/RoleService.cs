using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using THH.DAL;
using THH.DAL.Repository;
using THH.Model;
using THH.Model.Dto;

namespace THH.Service
{
    public class RoleService
    {
        private IRepository<Role> roleRepository = null;
        public RoleService()
        {
            roleRepository = new RepositoryBase<Role>();
        }
        public List<RoleDto> GetRoleGrid(int limit, int offset)
        {
            var user = roleRepository.Entities.ToList();
            return Mapper.Map<List<Role>, List<RoleDto>>(user);
        }
    }
}
