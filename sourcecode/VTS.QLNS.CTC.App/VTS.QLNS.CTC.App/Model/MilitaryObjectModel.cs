using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class MilitaryObjectModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public MilitaryObjectModel(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
