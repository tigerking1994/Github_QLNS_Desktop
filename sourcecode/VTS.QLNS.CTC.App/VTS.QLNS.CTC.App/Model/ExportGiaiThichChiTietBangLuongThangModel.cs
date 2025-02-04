using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class ExportGiaiThichChiTietBangLuongThangModel : BindableBase
    {
        public string MaCb { get; set; }
        public string Parent { get; set; }
        public int soNguoi { get; set; }
        public int Dem_PCCV { get; set; }
        public int Dem_PCTN { get; set; }
        public int Dem_PCKV { get; set; }
        public int Dem_PCCOV { get; set; }
        public int Dem_PCtrac { get; set; }
        public int Dem_PCKhac { get; set; }
        public decimal Lhths {get; set;}
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
        public string Tencb { get; set; }
    }
}
