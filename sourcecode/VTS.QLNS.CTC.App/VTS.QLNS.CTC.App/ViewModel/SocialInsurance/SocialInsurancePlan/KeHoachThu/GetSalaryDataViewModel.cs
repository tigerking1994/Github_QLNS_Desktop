using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsurancePlan.KeHoachThu
{
    public class GetSalaryDataViewModel : GridViewModelBase<TlDsBangLuongKeHoachModel>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ILog _logger;
        private SessionInfo _sessionInfo;
        private readonly INsDonViService _nsDonViService;
        private readonly ITlDsBangLuongKeHoachService _tlDsBangLuongKeHoachService;
        private readonly IKhtBHXHChiTietService _khtBHXHChiTietService;

        //public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_BANG_LUONG_NAM_KH_INDEX;
        //public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Lấy dữ liệu lương kế hoạch";
        public override Type ContentType => typeof(View.SocialInsurance.SocialInsurancePlan.KeHoachThu.GetSalaryData);
        public override PackIconKind IconKind => PackIconKind.ClipboardListOutline;
        public override string Title => "Lấy dữ liệu lương kế hoạch";
        public override string Description => "Lấy dữ liệu lương kế hoạch";

        private bool _selectedAllItem;
        public bool SelectedAllItem
        {
            get => Items.All(x => x.IsChecked);
            set
            {
                SetProperty(ref _selectedAllItem, value);
                foreach (var item in Items) item.IsChecked = _selectedAllItem;
            }
        }

        public List<TlDsBangLuongKeHoachModel> ListItems;
        public string SLuongKeHoach { get; set; }

        public GetSalaryDataViewModel(
             IMapper mapper,
             ISessionService sessionService,
             ILog logger,
             ITlDmDonViService tlDmDonViService,
             INsDonViService nsDonViService,
             ITlDsBangLuongKeHoachService tlDsBangLuongKeHoachService,
             IKhtBHXHChiTietService khtBHXHChiTietService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            _logger = logger;
            _nsDonViService = nsDonViService;
            _tlDsBangLuongKeHoachService = tlDsBangLuongKeHoachService;
            _khtBHXHChiTietService = khtBHXHChiTietService;

        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<TlDsBangLuongKeHoach> data = new List<TlDsBangLuongKeHoach>();

                var _listDonVi = _nsDonViService.FindByCondition(n => n.NamLamViec == _sessionService.Current.YearOfWork && n.ITrangThai == 1).ToList();
                if (_listDonVi.Any(n => _sessionService.Current.IdsDonViQuanLy.Contains(n.IIDMaDonVi) && n.Loai == "0") || _sessionService.Current.Principal.Equals("admin"))
                {
                    data = _tlDsBangLuongKeHoachService.FindAll().Where(x => x.Nam == _sessionInfo.YearOfWork).ToList();
                }
                else
                {
                    data = _tlDsBangLuongKeHoachService.FindAll().Where(n => _sessionService.Current.IdsPhanHoQuanLy.Contains(n.MaDonVi) && n.Nam == _sessionInfo.YearOfWork).ToList();
                }


                Items = _mapper.Map<ObservableCollection<TlDsBangLuongKeHoachModel>>(data);
                if (!Items.IsEmpty() && !string.IsNullOrEmpty(SLuongKeHoach))
                {
                    List<string> lstBangLuong;
                    if (SLuongKeHoach.Contains(StringUtils.COMMA))
                    {
                        lstBangLuong = SLuongKeHoach.Split(StringUtils.COMMA).ToList();
                    }
                    else
                    {
                        lstBangLuong = new List<string>() { SLuongKeHoach };
                    }

                    Items.Where(x => lstBangLuong.Contains(x.Id.ToString())).Select(s =>
                    {
                        s.IsChecked = true;
                        return s;
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave(object obj)
        {
            BhGetSalaryDataModel SalaryData = new BhGetSalaryDataModel();

            if (!Items.IsEmpty() && Items.Any(x => x.IsChecked))
            {
                var sIdChecked = string.Join(StringUtils.COMMA, Items.Where(w => w.IsChecked).Select(x => x.Id));
                var planSalary = _khtBHXHChiTietService.GetPlanSalary(_sessionService.Current.YearOfWork, BhxhMLNS.LUONG_CHINH, BhxhMLNS.PHU_CAP_CHUC_VU, BhxhMLNS.PHU_CAP_TNN, BhxhMLNS.PHU_CAP_TNVK, sIdChecked);
                var quanSoBinhQuan = _khtBHXHChiTietService.GetQuanSoBinhQuan(_sessionService.Current.YearOfWork, sIdChecked);
                SalaryData.ItemsPlanSalary = planSalary;
                SalaryData.ItemArmy = quanSoBinhQuan;
                SalaryData.Items = Items.Where(x => x.IsChecked).ToList();
            }
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(SalaryData);
        }
    }
}
