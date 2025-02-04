using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtKhlcNhaThauGoiThauDetailModel : BindableBase
    {
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
        private bool _isCheckedChild = false;
        public bool IsCheckedchild
        {
            get => _isCheckedChild ;
            set => SetProperty(ref _isCheckedChild, value);
        }

        private Guid? _iIdGoiThauId;
        public Guid? IIdGoiThauId
        {
            get => _iIdGoiThauId;
            set => SetProperty(ref _iIdGoiThauId, value);
        }

        private int? _iIdNguonVonId;
        public int? IIdNguonVonId
        {
            get => _iIdNguonVonId;
            set => SetProperty(ref _iIdNguonVonId, value);
        }

        private Guid? _iIdChiPhiGocId;
        public Guid? IIdChiPhiGocId
        {
            get => _iIdChiPhiGocId;
            set => SetProperty(ref _iIdChiPhiGocId, value);
        }

        private Guid? _iIdChiPhiId;
        public Guid? IIdChiPhiId
        {
            get => _iIdChiPhiId;
            set => SetProperty(ref _iIdChiPhiId, value);
        }

        private Guid? _iIdHangMucId;
        public Guid? IIdHangMucId
        {
            get => _iIdHangMucId;
            set => SetProperty(ref _iIdHangMucId, value);
        }

        private Guid? _iIdParentId;
        public Guid? IIdParentId
        {
            get => _iIdParentId;
            set => SetProperty(ref _iIdParentId, value);
        }

        private bool _isHangCha;
        public bool IsHangCha
        {
            get => _isHangCha;
            set => SetProperty(ref _isHangCha, value);
        }

        private string _sNoiDung;
        public string SNoiDung
        {
            get => _sNoiDung;
            set => SetProperty(ref _sNoiDung, value);
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private double _fGiaTriTruocDieuChinh;
        public double FGiaTriTruocDieuChinh
        {
            get => _fGiaTriTruocDieuChinh;
            set => SetProperty(ref _fGiaTriTruocDieuChinh, value);
        }

        private double _fGiaTriDuocDuyet;
        public double FGiaTriDuocDuyet
        {
            get => _fGiaTriDuocDuyet;
            set => SetProperty(ref _fGiaTriDuocDuyet, value);
        }

        private double _fGiaTriGoiThau;
        public double FGiaTriGoiThau
        {
            get => _fGiaTriGoiThau;
            set => SetProperty(ref _fGiaTriGoiThau, value);
        }

        private bool _bHaveHangMuc;
        public bool BHaveHangMuc
        {
            get => _bHaveHangMuc;
            set => SetProperty(ref _bHaveHangMuc, value);
        }

        private double _fGiaTriConLai;
        public double FGiaTriConLai
        {
            get => _fGiaTriConLai;
            set => SetProperty(ref _fGiaTriConLai, value);
        }

        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private double _fGiaTriConLaiShow;
        public double FGiaTriConLaiShow
        {
            get => _fGiaTriConLaiShow;
            set => SetProperty(ref _fGiaTriConLaiShow, value);
        }

        public bool IsEdit => !IsHangCha && !BHaveHangMuc && !IsDeleted;

        public int STT { get; set; }
    }
}
