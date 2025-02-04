using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDtCtctKPQLModel : DetailModelBase
    {
        public Guid Id { get; set; }
        public Guid? IIDChungTuChiTiet { get; set; }
        public string SXauNoiMa { get; set; }
        public Guid? IIDChungTu { get; set; }
        public string SLNS { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STMM { get; set; }
        public string SNG { get; set; }
        public string IIDMaDonVi { get; set; }
        public int INamLamViec { get; set; }
        public string SNoiDung { get; set; }
        private double? _fSoTien;
        public double? FSoTien
        {
            get => _fSoTien;
            set => SetProperty(ref _fSoTien, value);
        }
        public string SNguoiTao { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgayTao { get; set; }
        public DateTime? DNgaySua { get; set; }
        public Guid? IID_MLNS { get; set; }
        public Guid? IID_MLNS_Cha { get; set; }
        public bool BHangCha { get; set; }
        public bool IsHangCha { get; set; }
        public bool IsRemainRow { get; set; }
        public int STT { get; set; }
        public string STenDonVi { get; set; }
    }
}
