using System;
using System.Collections.Generic;
using THH.Core;
using THH.Core.Json;
using THH.DAL;
using THH.Model;
using THH.Service;
using THH.Service.ReportService;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Initializer initializer = new Initializer();
           initializer.Seed();

            UserService userService = new UserService();
            SysFunctionService sysFunctionService = new SysFunctionService();
            RoleService roleService = new RoleService();
            UserRightsService userRightsService = new UserRightsService();
            SysButtonService sysButtonService = new SysButtonService();
            SysMenuService sysMenuService = new SysMenuService();
            UserRoleService userRoleService = new UserRoleService();
            SystemService systemService = new SystemService();
            StoreReportService storeReportService = new StoreReportService();
            storeReportService.test();
            var user = userService.Find(1);
            List<UserRole> list = userRoleService.GetUserRoles();
            foreach (var item in list)
            {
                Console.WriteLine("角色名称：" + item.Role.RoleName);
            }
            try
            {
                string jsonString = list.ToJsonString();
                Console.WriteLine("Json数据：" + jsonString);
                var userRoles = userRoleService.GetUserRole();

                string jsonString2 = userRoles.ToJsonString();
                Console.WriteLine("Json2数据：" + jsonString2);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
            }

            List<UserRights> userRights = userRightsService.GetUserRights(user.Id);

//<<<<<<< .mine
//            //List<SysMenu> SysMenuList = new List<SysMenu>();
//            //try
//            //{
//            //    SysMenuList = userRights.Where(p => p.SysFunction.SysButtonId == 3).Select(p => p.SysFunction.SysMenu).ToList();
//            //}
//            //catch (Exception ex)
//            //{
//            //    string msg = ex.Message;
//            //}
//            //List<SysButton> SysButtonList = userRights.Select(p => p.SysFunction.SysButton).ToList();
//            //foreach (var item in SysMenuList)
//            //{
//            //    Console.WriteLine("菜单名称：" + item.MenuName);
//            //}
//            //foreach (var item in SysButtonList)
//            //{
//            //    Console.WriteLine("按钮名称：" + item.ButtonName);
//            //}
//||||||| .r59
//            List<SysMenu> SysMenuList = new List<SysMenu>();
//            try
//            {
//                SysMenuList = userRights.Where(p => p.SysFunction.SysButtonId == 3).Select(p => p.SysFunction.SysMenu).ToList();
//            }
//            catch (Exception ex)
//            {
//                string msg = ex.Message;
//            }
//            List<SysButton> SysButtonList = userRights.Select(p => p.SysFunction.SysButton).ToList();
//            foreach (var item in SysMenuList)
//            {
//                Console.WriteLine("菜单名称：" + item.MenuName);
//            }
//            foreach (var item in SysButtonList)
//            {
//                Console.WriteLine("按钮名称：" + item.ButtonName);
//            }
//=======
//            List<SysMenu> SysMenuList = new List<SysMenu>();
//            //try
//            //{
//            //    SysMenuList = userRights.Where(p => p.SysFunction.SysButtonId == 3).Select(p => p.SysFunction.SysMenu).ToList();
//            //}
//            //catch (Exception ex)
//            //{
//            //    string msg = ex.Message;
//            //}
//            //List<SysButton> SysButtonList = userRights.Select(p => p.SysFunction.SysButton).ToList();
//            //foreach (var item in SysMenuList)
//            //{
//            //    Console.WriteLine("菜单名称：" + item.MenuName);
//            //}
//            //foreach (var item in SysButtonList)
//            //{
//            //    Console.WriteLine("按钮名称：" + item.ButtonName);
//            //}
//>>>>>>> .r71
            Console.WriteLine("按钮名称：");
            Retry.Execute(Test, new TimeSpan(2000));
        }
        public static void Test()
        {
            Console.WriteLine("按钮名称：");
        }
    }
}
