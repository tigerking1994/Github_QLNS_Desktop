using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongChiPhiHangMucQueryModel : ModelBase
    {
        public Guid IdHopDongGoiThauNhaThau { get; set; }
        public Guid IdGoiThauHangMuc { get; set; }
        public Guid? GoiThauId { get; set; }
        public Guid? HangMucId { get; set; }
        public Guid? ChiPhiId { get; set; }
        public Guid? IID_ParentID { get; set; }
        public double FTienGoiThau { get; set; }

        private double _fGiatriSuDung;
        public double FGiatriSuDung
        {
            get => _fGiatriSuDung;
            set
            {
                SetProperty(ref _fGiatriSuDung, value);
                OnPropertyChanged(nameof(FGiaTriConLai));
                OnPropertyChanged(nameof(IsDisabledSelection));
            }
        }
        public double FGiaTriConLai => FTienCoTheSD - FGiatriSuDung - FGiatriSuDungTrongGoiThauKhac;

        private double _fGiaTriSuDungTrongGoiTHauKhac;
        public bool IsDisabledSelection => FGiatriSuDungTrongGoiThauKhac >= FTienCoTheSD;

        public double FGiatriSuDungTrongGoiThauKhac
        {
            get => _fGiaTriSuDungTrongGoiTHauKhac;
            set
            {
                SetProperty(ref _fGiaTriSuDungTrongGoiTHauKhac, value);
                OnPropertyChanged(nameof(FGiaTriConLai));
                OnPropertyChanged(nameof(IsDisabledSelection));
            }
        }

        public double FTienCoTheSD { get; set; }
        public string MaOrDer { get; set; }
        public string STenHangMuc { get; set; }
        public double? FGiaTriTruocDC { get; set; }

        public HopDongChiPhiHangMucQueryModel Clone()
        {
            HopDongChiPhiHangMucQueryModel rs = new HopDongChiPhiHangMucQueryModel();
            rs.IdGoiThauHangMuc = IdGoiThauHangMuc;
            rs.GoiThauId = GoiThauId;
            rs.HangMucId = HangMucId;
            rs.ChiPhiId = ChiPhiId;
            rs.IID_ParentID = IID_ParentID;
            rs.FTienGoiThau = FTienGoiThau;
            rs.FGiatriSuDung = 0;
            rs.FTienCoTheSD = FTienCoTheSD;
            rs.MaOrDer = MaOrDer;
            rs.STenHangMuc = STenHangMuc;
            return rs;
        }

        public VdtDaHopDongDmHangMuc ToVdtDaHopDongDmHangMuc()
        {
            VdtDaHopDongDmHangMuc rs = new VdtDaHopDongDmHangMuc();
            rs.Id = HangMucId.Value;
            rs.IIdChiPhiId = ChiPhiId.Value;
            rs.STenHangMuc = STenHangMuc;
            rs.IIDHopDongGoiThauNhaThauID = IdHopDongGoiThauNhaThau;
            rs.IIdParentId = IID_ParentID;
            rs.MaOrder = MaOrDer;
            return rs;
        }
    }
}
