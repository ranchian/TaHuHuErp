using System;
using System.Collections.Generic;
using System.Data.Entity;
using THH.Model;
using THH.Model.BaseModule;

namespace THH.DAL
{
    /// <summary>
    /// DropCreateDatabaseIfModelChanges
    /// 如果模型更改就重建数据表
    /// CreateDatabaseIfNotExists
    /// 如果不存在就创建数据库
    /// DropCreateDatabaseAlways
    /// </summary>
    public class ManagerInitializer : DropCreateDatabaseAlways<BaseDbContext>
    {
        protected override void Seed(BaseDbContext context)
        {
            try
            {
                //初始化用户
                InitializerUser(context);
                //初始化菜单
                InitializerSysMenu(context);
                //初始化角色
                InitializerRole(context);
                //初始化按钮
                InitializerSysButton(context);
                //初始化用户角色
                InitializerUserRole(context);
                //初始化功能
                InitializerSysFunction(context);
                //初始化用户权限
                InitializerUserRights(context);
                //初始化商品
                InitializerGoods(context);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 初始用户
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUser(BaseDbContext context)
        {
            List<User> userList = new List<User>() {
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "ljy",
                    Password = "123",
                    UserName = "李军毅",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "Admin",
                    Password = "123456",
                    UserName = "管理员",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
            };
            userList.ForEach(user =>
            {
                context.User.Add(user);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysMenu(BaseDbContext context)
        {
            List<SysMenu> sysMenuList = new List<SysMenu>() {
                 new SysMenu()
                 {
                    MenuName = "基本设置",
                    MenuCode = "BaseSet",
                    Url = "Home",
                    ParentId = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统管理",
                    MenuCode = "Home",
                    Url = "SystemManager",
                    ParentId = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    Icon="icon wb-settings",
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "菜单管理",
                    MenuCode = "Menu",
                    Url = "MenuManager",
                    ParentId = 2,
                    MenuLevel = 1,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "用户管理",
                    MenuCode = "User",
                    Url = "UserManager",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志信息",
                    MenuCode = "Log",
                    Url = "Log",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 3,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统设置",
                    MenuCode = "SystemSet",
                    Url = "SystemSetup",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 4,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "显示设置",
                    MenuCode = "DisplaySettings",
                    Url = "Display",
                    ParentId = 6,
                    MenuLevel = 3,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志设置",
                    MenuCode = "LogSettings",
                    Url = "Log",
                    ParentId = 6,
                    MenuLevel = 3,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
            };
            sysMenuList.ForEach(p =>
            {
                context.SysMenu.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerRole(BaseDbContext context)
        {
            List<Role> roleList = new List<Role>() {
                 new Role()
                 {
                    RoleName ="普通员工",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new Role()
                 {
                    RoleName ="超级管理员",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            roleList.ForEach(p =>
            {
                context.Role.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysButton(BaseDbContext context)
        {
            List<SysButton> SysButtonList = new List<SysButton>() {
                 new SysButton()
                 {
                    ButtonCode ="Menu",
                    ButtonName="菜单",
                    InputType="",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                new SysButton()
                 {
                    ButtonCode ="List",
                    ButtonName="列表",
                    InputType="",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Add",
                    ButtonName="新增",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Edit",
                    ButtonName="编辑",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Browse",
                    ButtonName="浏览",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Delete",
                    ButtonName="删除",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Submit ",
                    ButtonName="提交",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Confirm ",
                    ButtonName="确认",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Approval ",
                    ButtonName="审核",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Reject ",
                    ButtonName="拒绝",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            SysButtonList.ForEach(p =>
            {
                context.SysButton.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRole(BaseDbContext context)
        {
            List<UserRole> userRoleList = new List<UserRole>() {
                 new UserRole()
                 {
                    RoleId =1,
                    UserId=1,
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            userRoleList.ForEach(p =>
            {
                context.UserRole.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 功能
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysFunction(BaseDbContext context)
        {
            //List<SysFunction> sysFunctionList = new List<SysFunction>() {
            //     new SysFunction()
            //     {
            //        SysMenuId =1,
            //        //SysButtonId =1,
            //        CreateUser="李军毅",
            //        CredateTime = DateTime.Now
            //    }
            //};
            //sysFunctionList.ForEach(p =>
            //{
            //    context.SysFunction.Add(p);
            //});
            //context.SaveChanges();
        }
        /// <summary>
        /// 用户权限
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRights(BaseDbContext context)
        {
            //List<UserRights> userRightsList = new List<UserRights>() {
            //     new UserRights()
            //     {
            //         UserId=1,
            //         SysFunctionId=1,
            //         CreateUser="李军毅",
            //         CredateTime = DateTime.Now
            //    }
            //};
            //userRightsList.ForEach(p =>
            //{
            //    context.UserRights.Add(p);
            //});
            //context.SaveChanges();
        }

        /// <summary>
        /// 初始商品
        /// <param name="context"></param>
        public void InitializerGoods(BaseDbContext context)
        {
            List<Goods> goodsList = new List<Goods>() {
                new Goods()
                {
                    GoodsName = "商品1",
                    Picture = "暂无",
                    Price = 100,
                    VipPrice = 88,
                    Introduction = "无" ,
                    IsDelete = false,
                    CreateUser = "李维韩",
                    CredateTime = DateTime.Now
                },
                new Goods()
                {
                    GoodsName = "商品2",
                    Picture = "暂无",
                    Price = 200,
                    VipPrice = 168,
                    Introduction = "无",
                    IsDelete = false,
                    CreateUser = "李维韩",
                    CredateTime = DateTime.Now
                },
            };
            goodsList.ForEach(goods =>
            {
                context.Goods.Add(goods);
            });
            context.SaveChanges();
        }
    }

    public class Initializer
    {
        BaseDbContext context = null;
        public Initializer()
        {
            context = new BaseDbContext();
        }
        public void Seed()
        {
            try
            {
                //初始化用户
                InitializerUser(context);
                //初始化菜单
                InitializerSysMenu(context);
                //初始化角色
                InitializerRole(context);
                //初始化按钮
                InitializerSysButton(context);
                //初始化用户角色
                InitializerUserRole(context);
                //初始化功能
                InitializerSysFunction(context);
                //初始化用户权限
                InitializerUserRights(context);
                //初始化商品
                InitializerGoods(context);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// 初始用户
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUser(BaseDbContext context)
        {
            List<User> userList = new List<User>() {
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "ljy",
                    Password = "123",
                    UserName = "李军毅",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
                new User()
                {
                    Email = "979671716@qq.com",
                    LogingName = "Admin",
                    Password = "123456",
                    UserName = "管理员",
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now
                },
            };
            userList.ForEach(user =>
            {
                context.User.Add(user);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysMenu(BaseDbContext context)
        {
            List<SysMenu> sysMenuList = new List<SysMenu>() {
                 new SysMenu()
                 {
                    MenuName = "基本设置",
                    MenuCode = "BaseSet",
                    Url = "Home",
                    ParentId = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统管理",
                    MenuCode = "Home",
                    Url = "SystemManager",
                    ParentId = 0,
                    MenuLevel = 0,
                    SortNumber = 1,
                    Status = 1,
                    Icon="icon wb-settings",
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "菜单管理",
                    MenuCode = "Menu",
                    Url = "MenuManager",
                    ParentId = 2,
                    MenuLevel = 1,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "用户管理",
                    MenuCode = "User",
                    Url = "UserManager",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志信息",
                    MenuCode = "Log",
                    Url = "Log",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 3,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "系统设置",
                    MenuCode = "SystemSet",
                    Url = "SystemSetup",
                    ParentId = 2,
                    MenuLevel = 2,
                    SortNumber = 4,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "显示设置",
                    MenuCode = "DisplaySettings",
                    Url = "Display",
                    ParentId = 6,
                    MenuLevel = 3,
                    SortNumber = 1,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
                 new SysMenu()
                 {
                    MenuName = "日志设置",
                    MenuCode = "LogSettings",
                    Url = "Log",
                    ParentId = 6,
                    MenuLevel = 3,
                    SortNumber = 2,
                    Status = 1,
                    IsDelete = false,
                    CreateUser = "李军毅",
                    CredateTime = DateTime.Now,
                },
            };
            sysMenuList.ForEach(p =>
            {
                context.SysMenu.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerRole(BaseDbContext context)
        {
            List<Role> roleList = new List<Role>() {
                 new Role()
                 {
                    RoleName ="普通员工",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new Role()
                 {
                    RoleName ="超级管理员",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            roleList.ForEach(p =>
            {
                context.Role.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 初始化按钮
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysButton(BaseDbContext context)
        {
            List<SysButton> SysButtonList = new List<SysButton>() {
                 new SysButton()
                 {
                    ButtonCode ="Menu",
                    ButtonName="菜单",
                    InputType="",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                new SysButton()
                 {
                    ButtonCode ="List",
                    ButtonName="列表",
                    InputType="",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Add",
                    ButtonName="新增",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Edit",
                    ButtonName="编辑",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Browse",
                    ButtonName="浏览",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Delete",
                    ButtonName="删除",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Submit ",
                    ButtonName="提交",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Confirm ",
                    ButtonName="确认",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Approval ",
                    ButtonName="审核",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                },
                 new SysButton()
                 {
                    ButtonCode ="Reject ",
                    ButtonName="拒绝",
                    InputType="Input",
                    ButtonStyle="",
                    Status="N",
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            SysButtonList.ForEach(p =>
            {
                context.SysButton.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRole(BaseDbContext context)
        {
            List<UserRole> userRoleList = new List<UserRole>() {
                 new UserRole()
                 {
                    RoleId =1,
                    UserId=1,
                    CreateUser="李军毅",
                    CredateTime = DateTime.Now
                }
            };
            userRoleList.ForEach(p =>
            {
                context.UserRole.Add(p);
            });
            context.SaveChanges();
        }
        /// <summary>
        /// 功能
        /// </summary>
        /// <param name="context"></param>
        public void InitializerSysFunction(BaseDbContext context)
        {
            //List<SysFunction> sysFunctionList = new List<SysFunction>() {
            //     new SysFunction()
            //     {
            //        SysMenuId =1,
            //        //SysButtonId =1,
            //        CreateUser="李军毅",
            //        CredateTime = DateTime.Now
            //    }
            //};
            //sysFunctionList.ForEach(p =>
            //{
            //    context.SysFunction.Add(p);
            //});
            //context.SaveChanges();
        }
        /// <summary>
        /// 用户权限
        /// </summary>
        /// <param name="context"></param>
        public void InitializerUserRights(BaseDbContext context)
        {
            //List<UserRights> userRightsList = new List<UserRights>() {
            //     new UserRights()
            //     {
            //         UserId=1,
            //         SysFunctionId=1,
            //         CreateUser="李军毅",
            //         CredateTime = DateTime.Now
            //    }
            //};
            //userRightsList.ForEach(p =>
            //{
            //    context.UserRights.Add(p);
            //});
            //context.SaveChanges();
        }

        /// <summary>
        /// 初始商品
        /// <param name="context"></param>
        public void InitializerGoods(BaseDbContext context)
        {
            List<Goods> goodsList = new List<Goods>() {
                new Goods()
                {
                    GoodsName = "商品1",
                    Picture = "暂无",
                    Price = 100,
                    VipPrice = 88,
                    Introduction = "无" ,
                    IsDelete = false,
                    CreateUser = "李维韩",
                    CredateTime = DateTime.Now
                },
                new Goods()
                {
                    GoodsName = "商品2",
                    Picture = "暂无",
                    Price = 200,
                    VipPrice = 168,
                    Introduction = "无",
                    IsDelete = false,
                    CreateUser = "李维韩",
                    CredateTime = DateTime.Now
                },
            };
            goodsList.ForEach(goods =>
            {
                context.Goods.Add(goods);
            });
            context.SaveChanges();
        }
    }

}
