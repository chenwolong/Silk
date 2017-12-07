using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WeChat.Model
{
    public class UserHelper
    {
        public static UserPrincipal CurrentPrincipal
        {
            get
            {
                return HttpContext.Current.User as UserPrincipal;
            }
        }
    }

    public class UserPrincipal : ClientUserData, IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public string[] Roles { get; set; }

        public UserPrincipal(ClientUserData clientUserData)
        {
            this.Identity = new GenericIdentity(string.Format("{0}", clientUserData.UserId));
            this.UserId = clientUserData.UserId;
            this.Uname = clientUserData.Uname;
            this.RoleId = clientUserData.RoleId;
            this.pinyin = clientUserData.pinyin;
            this.idCard = clientUserData.idCard;
            this.PhotoNum = clientUserData.PhotoNum;
            this.EmployeeSex = clientUserData.EmployeeSex;
            this.EmployeePhone = clientUserData.EmployeePhone;
            this.Age = clientUserData.Age;
            this.Worker = clientUserData.Worker;
            this.HomeAddress = clientUserData.HomeAddress;
            this.RightVle = clientUserData.RightVle;
            this.RightName = clientUserData.RightName;
            this.isAdd = clientUserData.isAdd;
            this.isUpdate = clientUserData.isUpdate;
            this.isDelete = clientUserData.isDelete;
        }

        public bool IsInRole(string role)
        {
            if (Roles.Any(r => r == role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ClientUserData
    {
        public int UserId { get; set; }
        public string Uname { get; set; }
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
        public string RightName { get; set; }
        public Nullable<int> RightVle { get; set; }
        public Nullable<bool> isAdd { get; set; }
        public Nullable<bool> isUpdate { get; set; }
        public Nullable<bool> isDelete { get; set; }
    }
}
