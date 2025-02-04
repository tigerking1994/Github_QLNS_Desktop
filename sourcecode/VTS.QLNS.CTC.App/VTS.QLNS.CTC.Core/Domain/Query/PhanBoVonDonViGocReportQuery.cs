﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class PhanBoVonDonViGocReportQuery
    {
        public string STT { get; set; }
        public string STenDuAn { get; set; }
        public Guid? IIdLoaiCongTrinh { get; set; }
        public Guid? IIdLoaiCongTrinhParent { get; set; }
        public int? Loai { get; set; }
        public int? LoaiParent { get; set; }
        public bool IsHangCha { get; set; }
        public string ThoiGianThucHien { get; set; }
        public string DiaDiemThucHien { get; set; }
        public string ChuDauTu { get; set; }
        public string SoPheDuyet { get; set; }
        public DateTime? NgayPheDuyet { get; set; }
        public double? TongMucDauTu { get; set; }
        [NotMapped]
        public double? TongMucDauTuNSQP { get; set; }
        public double? VonBoTriDenHetNamTruoc { get; set; }
        public double? KeHoachVonDauTuNam { get; set; }
        public double? VonGiaiNganNam { get; set; }
        public double? DieuChinhVonNam { get; set; }
        public string SGhiChu { get; set; }
        public Guid? IdDuAn { get; set; }
        public int? IdNguonVon { get; set; }
        public int? LoaiCongTrinh { get; set; }
        public double HanMucDauTu { get; set; }
        public double VonBoTri5Nam { get; set; }
        public double VonBoTriSau5Nam { get; set; }

        [NotMapped]
        public string dNgayPheduyet { get; set; }
    }
}
