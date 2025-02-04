using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Core.Domain.Query;

namespace VTS.QLNS.CTC.App.Model
{
    public class VdtTtPheDuyetThanhToanChiTietModel : BindableBase
    {
        private string _lns;
        public string LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        private string _l;
        public string L
        {
            get => _l;
            set => SetProperty(ref _l, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => SetProperty(ref _k, value);
        }

        public string L_K
        {
            get
            {
                return L + "-" + K;
            }
        }

        private string _m;
        public string M
        {
            get => _m;
            set => SetProperty(ref _m, value);
        }

        private string _tm;
        public string TM
        {
            get => _tm;
            set => SetProperty(ref _tm, value);
        }

        private string _ttm;
        public string TTM
        {
            get => _ttm;
            set => SetProperty(ref _ttm, value);
        }

        private string _ng;
        public string NG
        {
            get => _ng;
            set => SetProperty(ref _ng, value);
        }

        public Guid? iID_MucID { get; set; }
        public Guid? iID_TieuMucID { get; set; }
        public Guid? iID_TietMucID { get; set; }
        public Guid? iID_NganhID { get; set; }

        private string _sTenMucLuc;
        public string STenMucLuc
        {
            get => _sTenMucLuc;
            set => SetProperty(ref _sTenMucLuc, value);
        }

        private string _sGhiChu;
        public string SGhiChu
        {
            get => _sGhiChu;
            set => SetProperty(ref _sGhiChu, value);
        }

        private int _iLoai;
        public int ILoai
        {
            get => _iLoai;
            set => SetProperty(ref _iLoai, value);
        }

        private int _iLoaiNamKeHoach;
        public int ILoaiNamKeHoach
        {
            get => _iLoaiNamKeHoach;
            set => SetProperty(ref _iLoaiNamKeHoach, value);
        }
        public double? fGiaTriThanhToanTN { get; set; }
        public double? fGiaTriThanhToanNN { get; set; }
        public double? fGiaTriNamTruocTrongNuoc { get; set; }
        public double? fGiaTriNamTruocNgoaiNuoc { get; set; }
        public double? fGiaTriNamNayTrongNuoc { get; set; }
        public double? fGiaTriNamNayNgoaiNuoc { get; set; }

        private double _fGiaTriTrongNuoc;
        public double FGiaTriTrongNuoc
        {
            get => _fGiaTriTrongNuoc;
            set => SetProperty(ref _fGiaTriTrongNuoc, value);
        }

        private double _fGiaTriNgoaiNuoc;
        public double FGiaTriNgoaiNuoc
        {
            get => _fGiaTriNgoaiNuoc;
            set => SetProperty(ref _fGiaTriNgoaiNuoc, value);
        }

        private double _fTongSo;
        public double FTongSo
        {
            get => _fTongSo;
            set => SetProperty(ref _fTongSo, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private Guid? _IID_KeHoachVonID;
        public Guid? IID_KeHoachVonID
        {
            get => _IID_KeHoachVonID;
            set => SetProperty(ref _IID_KeHoachVonID, value);
        }

        //private Guid? _iId_DeNghiTamUng;
        //public Guid? IIdDeNghiTamUng
        //{
        //    get => _iId_DeNghiTamUng;
        //    set => SetProperty(ref _iId_DeNghiTamUng, value);
        //}

        private int? _iLoaiKeHoachVon;
        public int? ILoaiKeHoachVon
        {
            get => _iLoaiKeHoachVon;
            set => SetProperty(ref _iLoaiKeHoachVon, value);
        }


        private KeHoachVonQuery _cbxKeHoachVonSelected;
        public KeHoachVonQuery CbxKeHoachVonSelected
        {
            get => _cbxKeHoachVonSelected;
            set => SetProperty(ref _cbxKeHoachVonSelected, value);
        }

        private ObservableCollection<KeHoachVonQuery> _cbxKeHoachVon;
        public ObservableCollection<KeHoachVonQuery> CbxKeHoachVon
        {
            get => _cbxKeHoachVon;
            set => SetProperty(ref _cbxKeHoachVon, value);
        }

        //private ChungTuThanhToanQuery _cbxDeNghiTamUngSelected;
        //public ChungTuThanhToanQuery CbxDeNghiTamUngSelected
        //{
        //    get => _cbxDeNghiTamUngSelected;
        //    set => SetProperty(ref _cbxDeNghiTamUngSelected, value);
        //}

        //private ObservableCollection<ChungTuThanhToanQuery> _cbxDeNghiTamUng;
        //public ObservableCollection<ChungTuThanhToanQuery> CbxDeNghiTamUng
        //{
        //    get => _cbxDeNghiTamUng;
        //    set => SetProperty(ref _cbxDeNghiTamUng, value);
        //}
    }
}
