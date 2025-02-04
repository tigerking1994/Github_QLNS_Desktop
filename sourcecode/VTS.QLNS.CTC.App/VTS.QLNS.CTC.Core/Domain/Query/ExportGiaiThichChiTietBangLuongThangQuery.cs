using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VTS.QLNS.CTC.Core.Domain.Query
{
    public class ExportGiaiThichChiTietBangLuongThangQuery
    {
        public string Tencb { get; set; }
        public string Parent { get; set; }
        public int Dem { get; set; }
        public decimal HsblHs { get; set; }
        public decimal LuongThang { get; set; }
        public decimal PCCV { get; set; }
        public decimal PCTN { get; set; }
        public decimal PCKV { get; set; }
        public decimal PCCOV { get; set; }
        public decimal PcTraSum { get; set; }
        public decimal PcKhacSum { get; set; }
        public decimal LuongThangSum { get; set; }
        public decimal BhxhTt { get; set; }
        public decimal BhytTt { get; set; }
        public decimal BhtnTt { get; set; }
        public decimal ThueTncnTt { get; set; }
        public decimal GtKhacTt { get; set; }
        public decimal TaTt { get; set; }
        public decimal PhaiTruSum { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal TrichLuongTt { get; set; }
    }
}
