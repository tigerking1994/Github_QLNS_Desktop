using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using System.Windows;
using VTS.QLNS.CTC.Utility.Enum;
using System.Collections.ObjectModel;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceSettlement.QuyetToanThuBHXH.Explanation
{
    public class QuyetToanThuGiaiThichBangLoiViewModel : ViewModelBase
    {
        private IQttBHXHChiTietGiaiThichService _chungTuChiTietGiaiThichService;
        private IMapper _mapper;
        private ISessionService _sessionService;
        private SessionInfo _sessionInfo;
        private bool _isCreate;

        public override string Name => "Quyết toán - Chứng từ chi tiết - Giải thích bằng lời";
        public override Type ContentType => typeof(View.Budget.Settlement.Explanation.VerbalExplanation);

        public BhQttBHXHModel SettlementVoucher;
        public Guid ExplainId;
        public string AgencyId;
        public int QuarterYear;
        public int QuarterYearType;
        public string QuarterYearDescription;

        private BhQttBHXHChiTietGiaiThichModel _settlementVoucherDetailExplain;
        public BhQttBHXHChiTietGiaiThichModel SettlementVoucherDetailExplain
        {
            get => _settlementVoucherDetailExplain;
            set => SetProperty(ref _settlementVoucherDetailExplain, value);
        }

        private ObservableCollection<ComboboxItem> _collectTypeDisplays;
        public ObservableCollection<ComboboxItem> CollectTypeDisplays
        {
            get => _collectTypeDisplays;
            set => SetProperty(ref _collectTypeDisplays, value);
        }

        private string _CollectTypeDisplaysSelected;
        public string CollectTypeDisplaysSelected
        {
            get => _CollectTypeDisplaysSelected;
            set 
            {
                SetProperty(ref _CollectTypeDisplaysSelected, value);
                LoadData();
            }
        }

        public new RelayCommand CloseCommand { get; }

        public QuyetToanThuGiaiThichBangLoiViewModel(IQttBHXHChiTietGiaiThichService chungTuChiTietGiaiThichService,
            IMapper mapper,
            ISessionService sessionService)
        {
            _chungTuChiTietGiaiThichService = chungTuChiTietGiaiThichService;
            _mapper = mapper;
            _sessionService = sessionService;

            SaveCommand = new RelayCommand(obj => OnSaveData());
            CloseCommand = new RelayCommand(obj => OnCloseWindow(obj));
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
            LoadTypeDisplay();
        }

        private void LoadData()
        {
            BhQttBHXHChiTietGiaiThichCriteria condition = new BhQttBHXHChiTietGiaiThichCriteria
            {
                VoucherId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id,
                ExplainId = SettlementVoucher == null ? ExplainId : SettlementVoucher.Id,
                AgencyId = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi,
                YearOfWork = _sessionInfo.YearOfWork,
                ExplainType = (int)ExplainType.GIAITHICH_BANGLOI,
                CollectionType = CollectTypeDisplaysSelected
            };
            var tempData = _chungTuChiTietGiaiThichService.GetGiaiThichBangLoi(condition);
            BhQttBHXHChiTietGiaiThich chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindByCondition(condition);
            if (chungTuChiTietGiaiThich != null)
            {
                SettlementVoucherDetailExplain = _mapper.Map<BhQttBHXHChiTietGiaiThichModel>(chungTuChiTietGiaiThich);
                _isCreate = false;
            }
            else
            {
                SettlementVoucherDetailExplain = new BhQttBHXHChiTietGiaiThichModel();
                _isCreate = true;
            }

            var loaiThu = CollectTypeDisplaysSelected == null ? "ALL" : CollectTypeDisplaysSelected;
            SettlementVoucherDetailExplain.FPhaiNopTrongQuyNam = SettlementVoucherDetailExplain.FPhaiNopTrongQuyNam.GetValueOrDefault() == 0 ? tempData.FirstOrDefault(x => x.SLoaiThu.Equals(loaiThu)).FPhaiNopTrongQuyNam : SettlementVoucherDetailExplain.FPhaiNopTrongQuyNam;
            SettlementVoucherDetailExplain.FTruyThuQuyNamTruoc = SettlementVoucherDetailExplain.FTruyThuQuyNamTruoc.GetValueOrDefault() == 0 ? tempData.FirstOrDefault(x => x.SLoaiThu.Equals(loaiThu)).FTruyThuQuyNamTruoc : SettlementVoucherDetailExplain.FTruyThuQuyNamTruoc;
            SettlementVoucherDetailExplain.FConPhaiNopTiep = SettlementVoucherDetailExplain.FPhaiNopBHXH.GetValueOrDefault()
                        - SettlementVoucherDetailExplain.FDaNopTrongQuyNam.GetValueOrDefault();

            SettlementVoucherDetailExplain.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FPhaiNopTrongQuyNam)
                    || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FTruyThuQuyNamTruoc)
                    || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FPhaiNopQuyNamTruoc)
                    || args.PropertyName == nameof(BhQttBHXHChiTietGiaiThichModel.FDaNopTrongQuyNam))
                {
                    SettlementVoucherDetailExplain.FConPhaiNopTiep = SettlementVoucherDetailExplain.FPhaiNopBHXH.GetValueOrDefault()
                        - SettlementVoucherDetailExplain.FDaNopTrongQuyNam.GetValueOrDefault();
                }
            };
        }

        private void LoadTypeDisplay()
        {
            CollectTypeDisplays = new ObservableCollection<ComboboxItem>();
            CollectTypeDisplays.Add(new ComboboxItem { ValueItem = null, DisplayItem = "Thu BHXH, BHYT, BHTN" });
            CollectTypeDisplays.Add(new ComboboxItem { ValueItem = CollectTypeDisplay.BHXH, DisplayItem = "Thu BHXH" });
            CollectTypeDisplays.Add(new ComboboxItem { ValueItem = CollectTypeDisplay.BHYT, DisplayItem = "Thu BHYT" });
            CollectTypeDisplays.Add(new ComboboxItem { ValueItem = CollectTypeDisplay.BHTN, DisplayItem = "Thu BHTN" });

            if (!_isCreate)
                CollectTypeDisplaysSelected = CollectTypeDisplays.FirstOrDefault(x => x.ValueItem == SettlementVoucherDetailExplain.SLoaiThu).ValueItem;
            else
                CollectTypeDisplaysSelected = CollectTypeDisplay.TAT_CA;
        }

        private void OnSaveData()
        {
            BhQttBHXHChiTietGiaiThich chungTuChiTietGiaiThich = new BhQttBHXHChiTietGiaiThich();

            //trường hợp tạo mới
            if (_isCreate)
            {
                SettlementVoucherDetailExplain.Id = Guid.NewGuid();
                SettlementVoucherDetailExplain.QttBHXHId = SettlementVoucher == null ? Guid.Empty : SettlementVoucher.Id;
                SettlementVoucherDetailExplain.IIDMaDonVi = SettlementVoucher == null ? AgencyId : SettlementVoucher.IIDMaDonVi;
                SettlementVoucherDetailExplain.INamLamViec = _sessionInfo.YearOfWork;
                SettlementVoucherDetailExplain.IQuyNam = SettlementVoucher == null ? QuarterYear : Convert.ToInt32(SettlementVoucher.IQuyNam);
                SettlementVoucherDetailExplain.IQuyNamLoai = SettlementVoucher == null ? QuarterYearType : SettlementVoucher.IQuyNamLoai;
                SettlementVoucherDetailExplain.SQuyNamMoTa = SettlementVoucher == null ? QuarterYearDescription : SettlementVoucher.SQuyNamMoTa;
                SettlementVoucherDetailExplain.SNguoiTao = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgayTao = DateTime.Now;
                SettlementVoucherDetailExplain.ILoaiGiaiThich = (int)ExplainType.GIAITHICH_BANGLOI;
                SettlementVoucherDetailExplain.SLoaiThu = CollectTypeDisplaysSelected;
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                if (ValidateData()) return;
                _chungTuChiTietGiaiThichService.Add(chungTuChiTietGiaiThich);
                _isCreate = false;
            }
            else
            {
                SettlementVoucherDetailExplain.SNguoiSua = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgaySua = DateTime.Now;
                SettlementVoucherDetailExplain.ILoaiGiaiThich = (int)ExplainType.GIAITHICH_BANGLOI;
                SettlementVoucherDetailExplain.SLoaiThu = CollectTypeDisplaysSelected;
                SettlementVoucherDetailExplain.SNguoiTao = _sessionInfo.Principal;
                SettlementVoucherDetailExplain.DNgayTao = DateTime.Now;
                chungTuChiTietGiaiThich = _chungTuChiTietGiaiThichService.FindById(SettlementVoucherDetailExplain.Id);
                _mapper.Map(SettlementVoucherDetailExplain, chungTuChiTietGiaiThich);
                if (ValidateData()) return;
                _chungTuChiTietGiaiThichService.Update(chungTuChiTietGiaiThich);
            }

            System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnCloseWindow(object obj)
        {
            Window window = obj as Window;
            window.Close();
        }

        private bool ValidateData()
        {
            StringBuilder messageBuilder = new StringBuilder();
            var soTienConPhaiNopTiep = SettlementVoucherDetailExplain.FConPhaiNopTiep;
            var soTienPhaiNopGoc = SettlementVoucherDetailExplain.FPhaiNopBHXH.GetValueOrDefault() - SettlementVoucherDetailExplain.FDaNopTrongQuyNam.GetValueOrDefault();
        
            if (soTienConPhaiNopTiep > soTienPhaiNopGoc)
            {
                var sSoTienPhaiNopGoc = soTienPhaiNopGoc.ToString("#,##0");
                messageBuilder.AppendFormat(Resources.MsgValidRemainingFee, sSoTienPhaiNopGoc);
            }
            if (messageBuilder.Length != 0)
            {
                System.Windows.MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return true;
            }
            return false;
        }
    }
}
