using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class BhDmMucLucBHXHMapModel : ModelBase
    {
        private Guid _id;
        public override Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private string _sXauNoima;
        [DisplayDetailInfo("Xâu nối mã")]
        public string SXauNoiMa
        {
            get => _sXauNoima;
            set => SetProperty(ref _sXauNoima, value);
        }
        private string _sMoTa;
        [DisplayName("Mô tả")]
        [DisplayDetailInfo("Mô tả mục lục ngân sách BHXH")]
        public string SMoTa
        {
            get => _sMoTa;
            set
            {
                SetProperty(ref _sMoTa, value);
            }
        }
        private double? _fTyLeBHXHNSD;
        [DisplayName("BHXH (Hệ số NSD lao động đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHXHNSD
        {
            get => _fTyLeBHXHNSD;
            set
            {
                SetProperty(ref _fTyLeBHXHNSD, value);
            }
        }
        private double? _fTyLeBHXHNLD;
        [DisplayName("BHXH (Hệ số NLĐ đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHXHNLD
        {
            get => _fTyLeBHXHNLD;
            set
            {
                SetProperty(ref _fTyLeBHXHNLD, value);
            }
        }
        private double? _fTyLeBHYTNSD;
        [DisplayName("BHYT (Hệ số NSD lao động đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHYTNSD
        {
            get => _fTyLeBHYTNSD;
            set
            {
                SetProperty(ref _fTyLeBHYTNSD, value);
            }
        }
        private double? _fTyLeBHYTNLD;
        [DisplayName("BHYT (Hệ số NLĐ đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHYTNLD
        {
            get => _fTyLeBHYTNLD;
            set
            {
                SetProperty(ref _fTyLeBHYTNLD, value);
            }
        }
        private double? _fTyLeBHTNNSD;
        [DisplayName("BHTN (Hệ số NSD lao động đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHTNNSD
        {
            get => _fTyLeBHTNNSD;
            set
            {
                SetProperty(ref _fTyLeBHTNNSD, value);
            }
        }
        private double? _fTyLeBHTNNLD;
        [DisplayName("BHTN (Hệ số NLĐ đóng)")]
        [FormatAttribute("{0:N4}")]
        public double? FTyLeBHTNNLD
        {
            get => _fTyLeBHTNNLD;
            set
            {
                SetProperty(ref _fTyLeBHTNNLD, value);
            }
        }
        private double? _fHeSoLayQuyLuong;
        [DisplayName("Hệ số lấy quỹ lương")]
        [FormatAttribute("{0:N4}")]
        public double? FHeSoLayQuyLuong
        {
            get => _fHeSoLayQuyLuong;
            set
            {
                SetProperty(ref _fHeSoLayQuyLuong, value);
            }
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
        public override bool IsHangCha
        {
            get => BHangCha;
        }
        private string _sNS_LuongChinh;        
        public string SNS_LuongChinh
        {
            get => _sNS_LuongChinh;
            set => SetProperty(ref _sNS_LuongChinh, value);
        }
        private string _tenSNSLuongChinh;
        [DisplayName("MLNS - Lương chính (F6)")]
        [DisplayDetailInfo("MLNS - Lương chính")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        //[InitSelectedItemsMethodAttribute("SetSelectedMLNS")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSNSLuongChinh 
        {
            get => _tenSNSLuongChinh;
            set => SetProperty(ref _tenSNSLuongChinh, value);
        }
        private string _sNS_PCCV;        
        public string SNS_PCCV
        {
            get => _sNS_PCCV;
            set => SetProperty(ref _sNS_PCCV, value);
        }
        private string _tenSNSPCCV;
        [DisplayName("MLNS - Phụ cấp chức vụ (F6)")]
        [DisplayDetailInfo("MLNS - Phụ cấp chức vụ")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        //[InitSelectedItemsMethodAttribute("SetSelectedMLNS")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSNSPCCV
        {
            get => _tenSNSPCCV;
            set => SetProperty(ref _tenSNSPCCV, value);
        }
        private string _sNS_PCTN;        
        public string SNS_PCTN
        {
            get => _sNS_PCTN;
            set => SetProperty(ref _sNS_PCTN, value);
        }
        private string _tenSNSPCTN;
        [DisplayName("MLNS - Phụ cấp thâm niên (F6)")]
        [DisplayDetailInfo("MLNS - Phụ cấp thâm niên")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        //[InitSelectedItemsMethodAttribute("SetSelectedMLNS")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSNSPCTN {
            get => _tenSNSPCTN;
            set => SetProperty(ref _tenSNSPCTN, value);
        }
        private string _sNS_PCTNVK;        
        public string SNS_PCTNVK
        {
            get => _sNS_PCTNVK;
            set => SetProperty(ref _sNS_PCTNVK, value);
        }
        private string _tenSNSPCTNVK;
        [DisplayName("MLNS - Phụ cấp thâm niên vượt khung (F6)")]
        [DisplayDetailInfo("MLNS - Phụ cấp thâm niên vượt khung")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        //[InitSelectedItemsMethodAttribute("SetSelectedMLNS")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSNSPCTNVK {
            get => _tenSNSPCTNVK;
            set => SetProperty(ref _tenSNSPCTNVK, value);
        }
        private string _sNS_HSBL;
        public string SNS_HSBL
        {
            get => _sNS_HSBL;
            set => SetProperty(ref _sNS_HSBL, value);
        }
        private string _tenSNSHSBL;
        [DisplayName("MLNS - Hệ số bảo lưu (F6)")]
        [DisplayDetailInfo("MLNS - Hệ số bảo lưu")]
        [TypeOfDialogAttribute(typeof(NsMuclucNgansachModel), typeof(NsMucLucNganSach), typeof(MucLucNganSachService), typeof(IMucLucNganSachService))]
        //[InitSelectedItemsMethodAttribute("SetSelectedMLNS")]
        [ColumnTypeAttribute(ColumnType.ReferencePopup)]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenNSHSBL
        {
            get => _tenSNSHSBL;
            set => SetProperty(ref _tenSNSHSBL, value);
        }
        private string _sLuongChinh;        
        public string SLuongChinh
        {
            get => _sLuongChinh;
            set => SetProperty(ref _sLuongChinh, value);
        }
        private string _tensLuongChinh;
        //[DisplayName("Phụ cấp lương - Lương chính (F6)")]
        //[DisplayDetailInfo("Phụ cấp lương Lương chính")]
        //[InitSelectedItemsMethodAttribute("SetSelectedPhuCap")]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSLuongChinh {
            get => _tensLuongChinh;
            set => SetProperty(ref _tensLuongChinh, value);
        }
        private string _sPCCV;        
        public string SPCCV
        {
            get => _sPCCV;
            set => SetProperty(ref _sPCCV, value);
        }
        private string _tenSPCCV;
        //[DisplayName("Phụ cấp lương - Phụ cấp chức vụ (F6)")]
        //[DisplayDetailInfo("Phụ cấp lương Phụ cấp chức vụ")]
        //[InitSelectedItemsMethodAttribute("SetSelectedPhuCap")]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSPCCV {
            get => _tenSPCCV;
            set => SetProperty(ref _tenSPCCV, value);
        }
        private string _sPCTN;        
        public string SPCTN
        {
            get => _sPCTN;
            set => SetProperty(ref _sPCTN, value);
        }
        private string _tenSPCTN;
        //[DisplayName("Phụ cấp lương - Phụ cấp thâm niên (F6)")]
        //[DisplayDetailInfo("Phụ cấp lương - Phụ cấp thâm niên")]
        //[InitSelectedItemsMethodAttribute("SetSelectedPhuCap")]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSPCTN {
            get => _tenSPCTN;
            set => SetProperty(ref _tenSPCTN, value);
        }
        private string _sPCTNVK;        
        public string SPCTNVK
        {
            get => _sPCTNVK;
            set => SetProperty(ref _sPCTNVK, value);
        }
        private string _tenSPCTNVK;
        //[DisplayName("Phụ cấp lương - Phụ cấp thâm niên vượt khung (F6)")]
        //[DisplayDetailInfo("Phụ cấp lương - Phụ cấp thâm niên vượt khung")]
        //[InitSelectedItemsMethodAttribute("SetSelectedPhuCap")]
        [IsAllowMultipleSelectAttribute(true)]
        public string TenSPCTNVK
        {
            get => _tenSPCTNVK;
            set => SetProperty(ref _tenSPCTNVK, value);
        }
        public int? INamLamViec { get; set; }
        public string SNguoiSua { get; set; }
        public DateTime? DNgaySua { get; set; }
    }
}
