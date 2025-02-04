using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhCpBsChungTuChiTietModel : DetailModelBase
    {
        public Guid IIDCTCapPhatBS { get; set; }

        private Guid _iIdMlns;
        public Guid IIdMlns
        {
            get => _iIdMlns;
            set => SetProperty(ref _iIdMlns, value);
        }

        private Guid? _iIdMlnsCha;
        public Guid? IIdMlnsCha
        {
            get => _iIdMlnsCha;
            set => SetProperty(ref _iIdMlnsCha, value);
        }

        private string _sXauNoiMa;
        public string SXauNoiMa
        {
            get => _sXauNoiMa;
            set => SetProperty(ref _sXauNoiMa, value);
        }

        private string _sLns;
        public string SLns
        {
            get => _sLns;
            set => SetProperty(ref _sLns, value);
        }

        private int? _namLamViec;
        public int? NamLamViec
        {
            get => _namLamViec;
            set => SetProperty(ref _namLamViec, value);
        }

        private int? _iTrangThai;
        public int? ITrangThai
        {
            get => _iTrangThai;
            set => SetProperty(ref _iTrangThai, value);
        }

        private string _idDonVi;
        public string IdDonVi
        {
            get => _idDonVi;
            set => SetProperty(ref _idDonVi, value);
        }

        private string _tenDonVi;
        public string TenDonVi
        {
            get => _tenDonVi;
            set => SetProperty(ref _tenDonVi, value);
        }

        private string _ghiChu;
        public string SGhiChu
        {
            get => _ghiChu;
            set => SetProperty(ref _ghiChu, value);
        }

        private DateTime? _dateCreated;
        public DateTime? DateCreated
        {
            get => _dateCreated;
            set => SetProperty(ref _dateCreated, value);
        }

        private string _userCreator;
        public string UserCreator
        {
            get => _userCreator;
            set => SetProperty(ref _userCreator, value);
        }

        private DateTime? _dateModified;
        public DateTime? DateModified
        {
            get => _dateModified;
            set => SetProperty(ref _dateModified, value);
        }

        private string _userModifier;
        public string UserModifier
        {
            get => _userModifier;
            set => SetProperty(ref _userModifier, value);
        }

        private string _tag;
        public string Tag
        {
            get => _tag;
            set => SetProperty(ref _tag, value);
        }

        private string _log;
        public string Log
        {
            get => _log;
            set => SetProperty(ref _log, value);
        }

        private string _sCoSoYTe;
        public string SCoSoYTe
        {
            get => _sCoSoYTe;
            set
            {
                SetProperty(ref _sCoSoYTe, value);
            }
        }

        private bool _isChildDeleted;
        public bool IsChildDeleted
        {
            get => _isChildDeleted;
            set
            {
                SetProperty(ref _isChildDeleted, value);
            }
        }

        private double? _fDaQuyetToan;
        public double? FDaQuyetToan
        {
            get => _fDaQuyetToan;
            set
            {
                SetProperty(ref _fDaQuyetToan, value);
                OnPropertyChanged(nameof(FThua));
                OnPropertyChanged(nameof(FThieu));
            }
        }

        private double? _fDaCapUng;
        public double? FDaCapUng
        {
            get => _fDaCapUng;
            set
            {
                SetProperty(ref _fDaCapUng, value);
                OnPropertyChanged(nameof(FThua));
                OnPropertyChanged(nameof(FThieu));
            }
        }

        private double? _fThua;
        public double? FThua
        {
            get => _fThua = FDaCapUng.GetValueOrDefault() > FDaQuyetToan.GetValueOrDefault() ? FDaCapUng.GetValueOrDefault() - FDaQuyetToan.GetValueOrDefault() : 0;
            set
            {
                SetProperty(ref _fThua, value);
            }
        }
        private double? _fThieu;
        public double? FThieu
        {
            get => _fThieu = FDaCapUng.GetValueOrDefault() < FDaQuyetToan.GetValueOrDefault() ? FDaQuyetToan.GetValueOrDefault() - FDaCapUng.GetValueOrDefault() : 0;
            set
            {
                SetProperty(ref _fThieu, value);
            }
        }
        public double? FThuaThieuTruocBoSung { get; set; }

        private double? _fSoCapBoSung;
        public double? FSoCapBoSung
        {
            get => _fSoCapBoSung;
            set
            {
                SetProperty(ref _fSoCapBoSung, value);
            }
        }
        public bool IsCreate { get; set; }

        private string _soChungTu;
        public string SoChungTu
        {
            get => _soChungTu;
            set => SetProperty(ref _soChungTu, value);
        }
        public bool? BHangCha { get; set; }
        public Guid IdParent { get; set; }
        public bool HasData => !IsHangCha && (FDaQuyetToan != 0 || FDaCapUng != 0 || FSoCapBoSung != 0 || FThieu != 0 || FThua != 0 || !string.IsNullOrEmpty(SGhiChu));
        private bool _isAdd;
        public bool IsAdd
        {
            get => _isAdd;
            set => SetProperty(ref _isAdd, value);
        }
        public string STenMLNS { get; set; }
        public string SMoTa { get; set; }
        public string STenCSYT { get; set; }
        public Guid IIDCoSoYTe { get; set; }
        public string IIDMaCoSoYTe { get; set; }
        public int? INamLamViec { get; set; }
        public string IIdMaDonVi { get; set; }
        public Guid IIdFilter { get; set; } = Guid.NewGuid();
        public string SL { get; set; }
        public string SK { get; set; }
        public string SM { get; set; }
        public string STM { get; set; }
        public string STTM { get; set; }
        public string SNG { get; set; }
        public string STNG { get; set; }
    }
}
