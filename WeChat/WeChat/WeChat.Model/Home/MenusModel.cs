using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Model
{
    public class MenusModel
    {
        public int Id { get; set; }
        public string menuName { get; set; }
        public string menuPth { get; set; }
        public Nullable<int> FId { get; set; }
        public string rightId { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public string remark1 { get; set; }
        public Nullable<int> flat1 { get; set; }
        public Nullable<bool> isdeleted { get; set; }
    }
}
