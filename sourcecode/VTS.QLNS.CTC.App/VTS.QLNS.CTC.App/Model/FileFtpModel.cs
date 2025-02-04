using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class FileFtpModel : ModelBase
    {
        private bool _bIsCheck;
        public bool BIsCheck 
        { 
            get => _bIsCheck; 
            set => SetProperty(ref _bIsCheck, value); 
        }

        public int IStt { get; set; }
        public string SUrl { get; set; }
        public string SNameFile { get; set; }
        public string SAdversion { get; set; }
    }
}
