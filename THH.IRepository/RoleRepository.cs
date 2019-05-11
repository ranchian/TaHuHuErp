using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THH.DAL;
using THH.Model;
using THH.Repository.IRepository;

namespace THH.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
    }
}
