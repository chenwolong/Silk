using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Model
{
    public class UserInfoModel
    {
        public int Id { get; set; }
        public string Uname { get; set; }
        public string Upwd { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string EmployeeName { get; set; }
        public string pinyin { get; set; }
        public string idCard { get; set; }
        public string PhotoNum { get; set; }
        public string EmployeeSex { get; set; }
        public string EmployeePhone { get; set; }
        public Nullable<int> Age { get; set; }
        public string Worker { get; set; }
        public string HomeAddress { get; set; }
        public string IP { get; set; }
        public string AddTime { get; set; }
        public string remark1 { get; set; }
        public Nullable<bool> isdeleted { get; set; }
        //
        public SYS_RoleModel Role { get; set; }
    }

    public class SYS_RoleModel
    {
        public int Id { get; set; }
        public string RightName { get; set; }
        public Nullable<int> RightVle { get; set; }
        public Nullable<bool> isAdd { get; set; }
        public Nullable<bool> isUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
        public Nullable<System.DateTime> Addtime { get; set; }
    }

}
