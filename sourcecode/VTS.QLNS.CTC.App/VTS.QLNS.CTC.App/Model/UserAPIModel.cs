using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class UserAPIModel : CheckBoxItem
    {      
        public Guid? ParentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}