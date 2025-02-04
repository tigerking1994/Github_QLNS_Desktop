using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation
{
    public class CapPhatTamUngKCBBHYTSummaryViewModel : ViewModelBase
    {
        private readonly INsDonViService _donViService;
        private readonly ICptuBHYTService _cptuBHYTService;
        private readonly ICptuBHYTChiTietService _cptuBHYTChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IDanhMucService _danhMucService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;

        public bool IsEditProcess = false;
        public override string Name => "TỔNG HỢP CẤP PHÁT TẠM ỨNG BHYT";
        public override string Title => "TỔNG HỢP CHỨNG TỪ";
        public override string Description => "Tổng hợp chứng từ cấp phát tạm ứng BHYT";
        public override Type ContentType => typeof(CapPhatTamUngKCBBHYTSummary);
        public override PackIconKind IconKind => PackIconKind.Dollar;
        public bool IsEnableView { get; set; }
        private bool _isCapPhatToanDonVi;


        private BhCptuBHYTModel _bhcptuBHYT;
        public BhCptuBHYTModel BhcptuBHYT
        {
            get => _bhcptuBHYT;
            set => SetProperty(ref _bhcptuBHYT, value);
        }

        private ObservableCollection<BhCptuBHYTModel> _dataBhcptuBHYT;
        public ObservableCollection<BhCptuBHYTModel> DataBhcptuBHYT
        {
            get => _dataBhcptuBHYT;
            set => SetProperty(ref _dataBhcptuBHYT, value);
        }

        private BhCptuBHYTModel _selectedBhcptuBHYT;
        public BhCptuBHYTModel SelectedBhcptuBHYT
        {
            get => _selectedBhcptuBHYT;
            set
            {
                SetProperty(ref _selectedBhcptuBHYT, value);
            }
        }

        public RelayCommand SaveCommand { get; }

        public CapPhatTamUngKCBBHYTSummaryViewModel(
            INsDonViService donViService,
            ISessionService sessionService,
            ICptuBHYTService cptuBHYTService,
            ICptuBHYTChiTietService cptuBHYTChiTietService,
            ILog logger,
            IDanhMucService danhMucService,
            IMapper mapper)
        {
            _donViService = donViService;
            _cptuBHYTService = cptuBHYTService;
            _cptuBHYTChiTietService = cptuBHYTChiTietService;
            _sessionService = sessionService;
            _danhMucService = danhMucService;
            _mapper = mapper;
            _logger = logger;
            SaveCommand = new RelayCommand(o => OnSave());
        }

        public override void Init()
        {
            try
            {
                IsEnableView = true;
                if (BhcptuBHYT == null) BhcptuBHYT = new Model.BhCptuBHYTModel();
                if (BhcptuBHYT.Id == Guid.Empty)
                {
                    BhcptuBHYT = new Model.BhCptuBHYTModel();
                    int soChungTuIndex = _cptuBHYTService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                    BhcptuBHYT.SSoChungTu = "CP-" + soChungTuIndex.ToString("D3");
                    BhcptuBHYT.DNgayChungTu = DateTime.Now;
                    BhcptuBHYT.DNgayQuyetDinh = DateTime.Now;
                    BhcptuBHYT.SMoTa = "Chi tiết chứng từ";
                }

                OnPropertyChanged(nameof(IsEnableView));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void CreateDetailSummary(BhCptuBHYT chungTu)
        {
            _cptuBHYTChiTietService.CreateVoudcherSummary(string.Join(",", DataBhcptuBHYT.Select(n => n.Id.ToString()).ToList())
                , _sessionService.Current.Principal, _sessionService.Current.YearOfWork, chungTu.Id.ToString());
        }

        private List<string> CheckSummary()
        {
            var predicate = PredicateBuilder.True<BhCptuBHYT>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => !string.IsNullOrEmpty(x.SDSSoChungTuTongHop));

            List<BhCptuBHYT> chungTu = _cptuBHYTService.FindByCondition(predicate).ToList();
            List<string> listSoChungTuSummary = new List<string>();
            foreach (var item in chungTu)
            {
                listSoChungTuSummary.AddRange(item.SDSSoChungTuTongHop.Split(",").ToList());
            }
            List<string> listResult = listSoChungTuSummary.Where(x => DataBhcptuBHYT.Select(n => n.SSoChungTu).ToList().Contains(x)).ToList();
            return listResult;
        }

        public override void OnSave()
        {
            try
            {
                if (BhcptuBHYT.Id != Guid.Empty)
                {


                    BhCptuBHYT entity = _cptuBHYTService.FindById(BhcptuBHYT.Id);
                    entity.SMoTa = BhcptuBHYT.SMoTa;
                    entity.SSoQuyetDinh = BhcptuBHYT.SSoQuyetDinh;
                    _cptuBHYTService.Update(entity);

                    DialogHost.Close(SystemConstants.ROOT_DIALOG);
                    SavedAction?.Invoke(_mapper.Map<BhCptuBHYTModel>(entity));
                }
                else
                {
                    DonVi donVi0 = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
                    if (donVi0 != null)
                    {
                        if (DataBhcptuBHYT == null || DataBhcptuBHYT.Count() == 0)
                        {
                            return;
                        }

                        List<string> listSummaryHistory = CheckSummary();
                        if (listSummaryHistory != null && listSummaryHistory.Count > 0)
                        {
                            MessageBoxHelper.Warning(string.Format(Resources.MsgSumaryWarning, string.Join(",", listSummaryHistory)));
                            return;
                        }

                        List<string> dsLns = new List<string>();
                        foreach (BhCptuBHYTModel item in DataBhcptuBHYT.Where(n => n.BIsKhoa && n.Selected))
                        {
                            if (!string.IsNullOrEmpty(item.SDSLNS))
                            {
                                dsLns.AddRange(item.SDSLNS.Split(","));
                            }
                        }

                        List<string> dsCsYTe = new List<string>();
                        foreach (BhCptuBHYTModel item in DataBhcptuBHYT.Where(n => n.BIsKhoa && n.Selected))
                        {
                            if (!string.IsNullOrEmpty(item.SDSID_CoSoYTe))
                            {
                                dsCsYTe.AddRange(item.SDSID_CoSoYTe.Split(","));
                            }
                        }

                        int soChungTuIndex = _cptuBHYTService.GetSoChungTuIndexByCondition(_sessionService.Current.YearOfWork);
                        BhCptuBHYT entity = new BhCptuBHYT();
                        entity.SSoChungTu = BhcptuBHYT.SSoChungTu;
                        entity.SSoQuyetDinh = BhcptuBHYT.SSoQuyetDinh;
                        entity.SMoTa = BhcptuBHYT.SMoTa;
                        entity.DNgayChungTu = BhcptuBHYT.DNgayChungTu;
                        entity.DNgayQuyetDinh = BhcptuBHYT.DNgayQuyetDinh;
                        entity.IQuy = DataBhcptuBHYT.First().IQuy;
                        entity.SDSLNS = string.Join(",", dsLns.Distinct().ToList());
                        entity.SDSID_CoSoYTe = string.Join(",", dsCsYTe.Distinct().ToList());
                        entity.INamLamViec = _sessionService.Current.YearOfWork;
                        entity.SDSSoChungTuTongHop = string.Join(",", DataBhcptuBHYT.Select(n => n.SSoChungTu).ToList());
                        entity.DNgayTao = DateTime.Now;
                        entity.DNgaySua = null;
                        entity.SNguoiTao = _sessionService.Current.Principal;
                        _cptuBHYTService.Add(entity);

                        CreateDetailSummary(entity);

                        _cptuBHYTService.UpdateTotalCPChungTu(entity.Id.ToString(), _sessionService.Current.Principal);

                        DialogHost.CloseDialogCommand.Execute(null, null);
                        MessageBoxHelper.Info(Resources.MsgSumaryDone);
                        SavedAction?.Invoke(_mapper.Map<BhCptuBHYTModel>(entity));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
