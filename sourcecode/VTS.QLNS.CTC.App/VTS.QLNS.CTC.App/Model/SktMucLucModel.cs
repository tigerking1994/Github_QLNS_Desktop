using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class SktMucLucModel : DetailModelBase
    {
        public Guid IIDMLSKT { get; set; }

        private string _sl;
        [DisplayName("Loại")]
        [DisplayDetailInfo("Loại")]
        public string SL
        {
            get => _sl;
            set => SetProperty(ref _sl, value);
        }

        private string _sk;
        [DisplayName("Khoản")]
        [DisplayDetailInfo("Khoản")]
        public string SK
        {
            get => _sk;
            set => SetProperty(ref _sk, value);
        }
      
        private string _sm;
        [DisplayName("Mục - Tiểu mục")]
        [DisplayDetailInfo("Mục - Tiểu mục")]
        public string SM 
        {
            get => _sm;
            set => SetProperty(ref _sm, value);
        }

        private string _sNGCha;
        [DisplayName("Ngành cha (F6)")]
        [TypeOfDialogAttribute(typeof(DanhMucNhomNganhModel), typeof(DanhMuc), typeof(DanhMucNhomNganhService), typeof(DanhMucNhomNganhService))]
        [DisplayDetailInfo("Ngành cha")]
        [MapperMethodAttribute("ConvertDMNhonNganhToSktMucLuc")]
        [InitSelectedItemsMethodAttribute("SetSelectedNhomNganh")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(false)]
        public string SNGCha 
        {
            get => _sNGCha;
            set => SetProperty(ref _sNGCha, value);
        }

        private bool _bCoDinhMuc;
        [ColumnTypeAttribute(ColumnType.Checkbox)]
        [DisplayName("Có định mức")]
        [DisplayDetailInfo("Có định mức")]
        public bool BCoDinhMuc
        {
            get => _bCoDinhMuc;
            set => SetProperty(ref _bCoDinhMuc, value);
        }

        private string _sNg;
        [DisplayName("Ngành chi tiết")]
        [DisplayDetailInfo("Ngành chi tiết")]
        public string SNg 
        {
            get => _sNg;
            set => SetProperty(ref _sNg, value);
        }

        private string _sSTT;
        [DisplayName("STT")]
        [DisplayDetailInfo("STT")]
        public string SSTT 
        {
            get => _sSTT;
            set => SetProperty(ref _sSTT, value);
        }

        private string _sSttBC;
        [DisplayName("STT in Báo cáo")]
        [DisplayDetailInfo("STT in Báo cáo")]
        public string SSttBC 
        {
            get => _sSttBC;
            set => SetProperty(ref _sSttBC, value);
        }

        private string _sKyHieu;
        [DisplayName("Mã")]
        [DisplayDetailInfo("Mã")]
        public string SKyHieu 
        {
            get => _sKyHieu;
            set => SetProperty(ref _sKyHieu, value);
        }

        private string _sKyHieuCu;
        [DisplayName("Mã cũ")]
        [DisplayDetailInfo("Mã cũ")]
        public string SKyHieuCu
        {
            get => _sKyHieuCu;
            set => SetProperty(ref _sKyHieuCu, value);
        }

        private string _sLoaiNhap;
        [DisplayName("Loại nhập")]
        [DisplayDetailInfo("Loại nhập")]
        public string SLoaiNhap
        {
            get => _sLoaiNhap;
            set => SetProperty(ref _sLoaiNhap, value);
        }

        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả")]
        public string SMoTa 
        {
            get => _sMoTa;
            set => SetProperty(ref _sMoTa, value);
        }

        private bool _bHangCha;
        public bool BHangCha
        {
            get => _bHangCha;
            set
            {
                SetProperty(ref _bHangCha, value);
                OnPropertyChanged(nameof(IsHangCha));
            }
        }

        public bool IsHangCha => BHangCha;

        public int ITrangThai { get; set; }
        [DisplayDetailInfo("Năm làm việc")]
        public int INamLamViec { get; set; }
        [DisplayDetailInfo("Ngày tạo")]
        public DateTime? DNgayTao { get; set; }
        [DisplayDetailInfo("Người tạo")]
        public string DNguoiTao { get; set; }
        [DisplayDetailInfo("Ngày cập nhật")]
        public DateTime? DNgaySua { get; set; }
        [DisplayDetailInfo("Người cập nhật")]
        public string DNguoiSua { get; set; }
        [DisplayDetailInfo("Tag")]
        public string Tag { get; set; }
        [DisplayDetailInfo("Log")]
        public string Log { get; set; }
        [DisplayDetailInfo("Mục")]
        public string Muc { get; set; }
        [DisplayDetailInfo("Lns")]
        public string Lns { get; set; }
        [DisplayDetailInfo("Tự chi")]
        public double TuChi { get; set; }

        private string _kyHieuCha;
        //[DisplayName("Mã cha (F6)")]
        //[DisplayDetailInfo("Mã cha")]
        public string KyHieuCha 
        {
            get => _kyHieuCha;
            set => SetProperty(ref _kyHieuCha, value);
        }

        public virtual ICollection<NsSktChungTuChiTietModel> SktChungTuChiTiets { get; set; }
        
        private NsSktChungTuChiTietModel _nsSktChungTuChiTietModel;
        public NsSktChungTuChiTietModel NsSktChungTuChiTietModel
        {
            get => SktChungTuChiTiets.FirstOrDefault();
            set => SetProperty(ref _nsSktChungTuChiTietModel, value );
        }

        [DisplayDetailInfo("Trạng thái")]
        public string TrangThaiDisplay
        {
            get => ITrangThai switch
            {
                0 => "không sử dụng",
                1 => "Đang sử dụng",
                2 => "ngành nghiệp vụ",
                _ => ""
            };
        }

        public override string DetailInfoModalTitle => "Chi tiết MLSKT " + SM;

        public override bool IsEditable => !IsDeleted;


        //private bool _isModified;
        //public bool IsModified
        //{
        //    get => _isModified;
        //    set => SetProperty(ref _isModified, value);
        //}

        public SktMucLucModel()
        {
            SktChungTuChiTiets = new HashSet<NsSktChungTuChiTietModel>();
        }

        public Guid? IIDMLSKTCha { get; set; }

        private double _tongSo;
        public double TongSo
        {
            get => _tongSo;
            set
            {
                SetProperty(ref _tongSo, value);
                OnPropertyChanged(nameof(ConLai));
            }
        }

        private double _conLai;
        public double ConLai
        {
            get
            {
                return TuChi - TongSo;
            }
            set => SetProperty(ref _conLai, value);
        }



        private double _tongSoHang;
        public double TongSoHang
        {
            get => _tongSoHang;
            set
            {
                SetProperty(ref _tongSoHang, value);
                OnPropertyChanged(nameof(ConLaiHang));
            }
        }

        private double _conLaiHang;
        public double ConLaiHang
        {
            get
            {
                return MuaHangHienVat - TongSoHang;
            }
            set => SetProperty(ref _conLaiHang, value);
        }

        private double _tongDacThu;
        public double TongDacThu
        {
            get => _tongDacThu;
            set
            {
                SetProperty(ref _tongDacThu, value);
                OnPropertyChanged(nameof(ConLaiDacThu));
            }
        }

        private double _conLaiDacThu;
        public double ConLaiDacThu
        {
            get
            {
                return DacThu - TongDacThu;
            }
            set => SetProperty(ref _conLaiDacThu, value);
        }

        public double HangMua { get; set; }
        public double HangNhap { get; set; }
        public double PhanCap { get; set; }

        private double _muaHangHienVat;
        public double MuaHangHienVat {
            get => _muaHangHienVat;
            set => SetProperty(ref _muaHangHienVat, value);
        }
        public double DacThu { get; set; }

        private string _mlns;
        [DisplayName("MLNS (F6)")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        [DisplayDetailInfo("MLNS")]
        [MapperMethodAttribute("MapMLNSToMLNSOfSktMucLuc")]
        [InitSelectedItemsMethodAttribute("SetSelectedMLNSOfSktMucLuc")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string MLNS
        {
            get => _mlns;
            set => SetProperty(ref _mlns, value);
        }

        [JsonIgnore]
        public ICollection<NsMlsktMlns> SktMucLucMaps { get; set; }
    }
}