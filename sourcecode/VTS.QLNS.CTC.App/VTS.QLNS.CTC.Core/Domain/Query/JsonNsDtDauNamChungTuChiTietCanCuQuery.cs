using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class JsonNsDtDauNamChungTuChiTietCanCuQuery : NsDtdauNamChungTuChiTietCanCu
    {
        public string IIDMaChucNang { get; set; }
        public int? INamCanCu { get; set; }
        public string SModule { get; set; }
        public string STenChucNang { get; set; }
        [NotMapped]
        public bool ImportStatus { get; set; }
    }
}
