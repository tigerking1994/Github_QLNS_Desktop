using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class NhMstnKeHoachDatHangDanhMucModel : CurrencyDetailModelBase
    {
        public Guid? IID_KeHoachDatHang { get; set; }

        private string _stenDanhMuc;
        public string STenDanhMuc
        {
            get => _stenDanhMuc;
            set => SetProperty(ref _stenDanhMuc, value);
        }
        private string _sDonViTinh;
        public string SDonViTinh
        {
            get => _sDonViTinh;
            set => SetProperty(ref _sDonViTinh, value);
        }
        private int? _iSoLuong;
        public int? ISoLuong
        {
            get => _iSoLuong;
            set => SetProperty(ref _iSoLuong, value);
        }

        private double? _fDonGia_VND;
        public double? FDonGia_VND
        {
            get => _fDonGia_VND;
            set => SetProperty(ref _fDonGia_VND, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private Guid? _iIdParentId;
        public Guid? IID_ParentID
        {
            get => _iIdParentId;
            set
            {
                SetProperty(ref _iIdParentId, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        private string _sMaOrder;
        public string SMaOrder
        {
            get => _sMaOrder;
            set => SetProperty(ref _sMaOrder, value);
        }

        private string _sMaDanhMuc;
        public string SMaDanhMuc
        {
            get => _sMaDanhMuc;
            set => SetProperty(ref _sMaDanhMuc, value);
        }

        private Guid? _iIdNhaThauId;
        public Guid? IID_NhaThauID
        {
            get => _iIdNhaThauId;
            set => SetProperty(ref _iIdNhaThauId, value);
        }
    }
}
