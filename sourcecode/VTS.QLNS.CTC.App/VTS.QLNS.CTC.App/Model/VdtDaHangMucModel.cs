using System;
using System.Collections.Generic;
using System.Text;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtDaHangMucModel : ModelBase
    {
        public Guid IdDuAnHangMuc { get; set; }
        public Guid? IIdDuAnId { get; set; }
        public Guid? IdChuTruongHangMuc { get; set; }

        private string _sMaHangMuc;
        public string SMaHangMuc
        {
            get => _sMaHangMuc;
            set => SetProperty(ref _sMaHangMuc, value);
        }

        private string _sTenHangMuc;
        public string STenHangMuc
        {
            get => _sTenHangMuc;
            set => SetProperty(ref _sTenHangMuc, value);
        }
       
        public Guid? IIdParentId { get; set; }
        public string MaOrDer { get; set; }
        public Guid? IdChuTruong { get; set; }

        private Guid? _loaiCongTrinhId;
        public Guid? LoaiCongTrinhId
        {
            get => _loaiCongTrinhId;
            set => SetProperty(ref _loaiCongTrinhId, value);
        }

        private double _hanMucDT;
        public double HanMucDT
        {
            get => _hanMucDT;
            set => SetProperty(ref _hanMucDT, value);
        }

        private int _indexMaHangMuc;
        public int indexMaHangMuc
        {
            get => _indexMaHangMuc;
            set => SetProperty(ref _indexMaHangMuc, value);
        }
        public string TenLoaiCongTrinh { get; set; }

        private bool? _isEditHangMuc;
        public bool? IsEditHangMuc
        {
            get => _isEditHangMuc;
            set => SetProperty(ref _isEditHangMuc, value);
        }

        private bool _bIsAdd;
        public bool BIsAdd
        {
            get => _bIsAdd;
            set => SetProperty(ref _bIsAdd, value);
        }
        public int Stt { get; set; }
    }
}
