using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class NsQtCongKhaiThuChi
    {
        public class Data
        {
            public Data(double soTien)
            {
                SoTien = soTien;
            }

            public double SoTien { get; set; }
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string MaMucLuc { get; set; }
        public string MaMucLucCha { get; set; }
        public string STT { get; set; }
        public string NoiDung { get; set; }
        public string MaDonVi { get; set; }
        public double DuToanDuocGiao { get; set; }
        public double SoBaoCaoQuyetToan { get; set; }
        public double SoQuyetToanDuocDuyet { get; set; }
        public double SoTien { get; set; }
        public bool IsHangCha { get; set; }
        public bool HasDataChiTiet => DuToanDuocGiao != 0 || SoBaoCaoQuyetToan != 0 || SoTien != 0 || ListGiaTri.Any(x => x.SoTien != 0);
        public bool HasDataTongHop => DuToanDuocGiao != 0 || SoBaoCaoQuyetToan != 0 || SoQuyetToanDuocDuyet != 0;
        public List<Data> ListGiaTri { get; set; } = new List<Data>();
    }


}
