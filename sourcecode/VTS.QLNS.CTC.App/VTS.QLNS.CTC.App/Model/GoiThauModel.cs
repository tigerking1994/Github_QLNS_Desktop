using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;

namespace VTS.QLNS.CTC.App.Model
{
    public class GoiThauModel : CheckBoxItem
    {
        public Guid? IdGoiThauNhaThau { get; set; }
        public Guid IIDGoiThauID { get; set; }
        public Guid IIDDuAnID { get; set; }
        public string SMaGoiThau { get; set; }
        public string STenGoiThau { get; set; }
        public Guid? IIDGoiThauGocID { get; set; }

        private Guid? _iIdNhaThauId;
        public Guid? IIdNhaThauId 
        { 
            get => _iIdNhaThauId; 
            set => SetProperty(ref _iIdNhaThauId, value); 
        }

        public double? FTienTrungThau { get; set; }

        private double? _fGiaTriGoiThau;
        public double? FGiaTriGoiThau 
        { 
            get => _fGiaTriGoiThau; 
            set => SetProperty(ref _fGiaTriGoiThau, value); 
        }

        private double? _fGiaTriConLai;
        public double? FGiaTriConLai
        {
            get => _fGiaTriConLai;
            set => SetProperty(ref _fGiaTriConLai, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private double? _fGiaTriTrungThau;
        public double? FGiaTriTrungThau
        {
            get => _fGiaTriTrungThau;
            set => SetProperty(ref _fGiaTriTrungThau, value);
        }

        private double? _fGiaTriTrungThauTruocDC;
        public double? FGiaTriTrungThauTruocDC
        {
            get => _fGiaTriTrungThauTruocDC;
            set => SetProperty(ref _fGiaTriTrungThauTruocDC, value);
        }

        public double TienDaSuDung { get; set; }
    }
}
