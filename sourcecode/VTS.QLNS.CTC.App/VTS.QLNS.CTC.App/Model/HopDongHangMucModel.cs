using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class HopDongHangMucModel : ModelBase
    {
        public Guid? IdGoiThauNhaThau { get; set; }
        public Guid? IIDHopDongID { get; set; }
        public Guid? IIDGoiThauID { get; set; }
        public Guid? IIDChiPhiID { get; set; }
        public Guid? IIDHangMucID { get; set; }
        public Guid? IIDNhaThauID { get; set; }
        public string STenChiPhi { get; set; }
        public double? FGiaTriDuocDuyet  { get; set; }

        private double? _fTienGoiThau;
        public double? FTienGoiThau
        {
            get => _fTienGoiThau;
            set => SetProperty(ref _fTienGoiThau, value);
        }

        private double? _fGiaTriConLai;
        public double? FGiaTriConLai
        {
            get => _fGiaTriConLai;
            set => SetProperty(ref _fGiaTriConLai, value);
        }

        public int? IThuTu { get; set; }
        public string MaOrDer { get; set; }
        public Guid? IdChiPhiDuAnParent { get; set; }
        public Guid? HangMucParentId { get; set; }
        //public bool? IsHangCha { get; set; }
        public string STenHangMuc { get; set; }

        private bool? _isEdit;
        public bool? IsEdit
        {
            get => _isEdit;
            set => SetProperty(ref _isEdit, value);
        }

        private bool? _isEditHangMuc;
        public bool? IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private bool? _isChecked;
        public bool? IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        private double? _fGiaTriTrungThau;
        public double? FGiaTriTrungThau
        {
            get => _fGiaTriTrungThau;
            set => SetProperty(ref _fGiaTriTrungThau, value);
        }

        private double? _fTienGoiThauTruocDC;
        public double? FTienGoiThauTruocDC 
        {
            get => _fTienGoiThauTruocDC;
            set => SetProperty(ref _fTienGoiThauTruocDC, value);
        }

        public double TienDaSuDung { get; set; }

        public bool IsTinhGiaTriConLai { get; set; }
    }
}
