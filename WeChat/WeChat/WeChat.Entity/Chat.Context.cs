﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeChat.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WeChatEntities : DbContext
    {
        public WeChatEntities()
            : base("name=WeChatEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<SYS_Log> SYS_Log { get; set; }
        public DbSet<SYS_Menus> SYS_Menus { get; set; }
        public DbSet<SYS_pic> SYS_pic { get; set; }
        public DbSet<SYS_Role> SYS_Role { get; set; }
        public DbSet<SYS_UserInfo> SYS_UserInfo { get; set; }
        public DbSet<WeChat_Menus> WeChat_Menus { get; set; }
        public DbSet<WeChat_Token> WeChat_Token { get; set; }
    }
}