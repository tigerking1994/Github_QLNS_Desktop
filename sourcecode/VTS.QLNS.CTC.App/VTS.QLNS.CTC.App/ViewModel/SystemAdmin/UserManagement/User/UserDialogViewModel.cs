using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Converters;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.User;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.User
{
    public class UserDialogViewModel : DialogViewModelBase<HTNguoiDungModel>
    {
        private IGroupService _groupService;
        private IAuthorityService _authorityService;
        private IUserService _userService;
        private IAuthorityTypeService _authorityTypeService;
        private IMapper _mapper;
        private HtNguoiDung _userEntity;
        private ICollectionView _dataGroupsView;
        private ICollectionView _donViGroupsView;
        private ICollectionView _phanHoGroupView;
        private ICollectionView _lnsView;
        private ISessionService _sessionService;
        private INsDonViService _nSDonViService;
        private ICauHinhMLNSService _cauHinhMLNSService;
        private ITlDmDonViService _tTlDmDonViService;

        public override Type ContentType => typeof(UserDialog);
        public bool IsDisabledUserLoginField { get; set; }
        public Visibility IsVisiblePasswordField { get; set; }
        public bool IsEnablePasswordField { get; set; }
        public bool IsTag { get; set; } = true;

        public string SelectedGroupsCount
        {
            get
            {
                int totalCount = Groups != null ? Groups.Count : 0;
                int totalSelected = Groups != null ? Groups.Count(x => x.IsSelected) : 0;
                return string.Format(NSLabel.SELECTED_COUNT_GROUPS_STR, totalSelected, totalCount);
            }
        }

        public string SelectedLNSCount
        {
            get
            {
                int totalCount = LNS != null ? LNS.Count : 0;
                int totalSelected = LNS != null ? LNS.Count(x => x.IsSelected) : 0;
                return string.Format(NSLabel.SELECTED_COUNT_GROUPS_STR, totalSelected, totalCount);
            }
        }

        public string SelectedCountDonVi
        {
            get
            {
                int totalCount = NsDonVis != null ? NsDonVis.Count : 0;
                int totalSelected = NsDonVis != null ? NsDonVis.Count(x => x.Selected) : 0;
                return string.Format(NSLabel.SELECTED_COUNT_DONVI_STR, totalSelected, totalCount);
            }
        }

        public string SelectedCountPhanHo
        {
            get
            {
                int totalCount = TlDmPhanHos != null ? TlDmPhanHos.Count : 0;
                int totalSelected = TlDmPhanHos != null ? TlDmPhanHos.Count(x => x.IsSelected) : 0;
                return string.Format(NSLabel.SELECTED_COUNT_TL_DONVI_STR, totalSelected, totalCount);
            }
        }

        private string _searchGroupText;
        public string SearchGroupText
        {
            get => _searchGroupText;
            set
            {
                SetProperty(ref _searchGroupText, value);
                OnPropertyChanged("SearchGroupText");
                if(_dataGroupsView != null)
                    _dataGroupsView.Refresh();
            }
        }

        private string _searchDonViText;
        public string SearchDonViText
        {
            get => _searchDonViText;
            set
            {
                SetProperty(ref _searchDonViText, value);
                OnPropertyChanged("SearchDonViText");
                if (_donViGroupsView != null)
                    _donViGroupsView.Refresh();
            }
        }

        private string _searchPhanHoText;
        public string SearchPhanHoText
        {
            get => _searchPhanHoText;
            set
            {
                SetProperty(ref _searchPhanHoText, value);
                OnPropertyChanged(nameof(SearchPhanHoText));
                if (_phanHoGroupView != null)
                    _phanHoGroupView.Refresh();
            }
        }

        private string _searchLnsText;
        public string SearchLNSText
        {
            get => _searchLnsText;
            set
            {
                SetProperty(ref _searchLnsText, value);
                if (_lnsView != null)
                    _lnsView.Refresh();
            }
        }

        private bool _selectAllLNS;
        public bool SelectAllLNS
        {
            get => LNS?.All(item => item.IsSelected) ?? false;
            set
            {
                SetProperty(ref _selectAllLNS, value);
                IsTag = false;
                LNS?.ForAll(x => x.IsSelected = _selectAllLNS);
                IsTag = true;
                OnPropertyChanged(nameof(SelectAllLNS));
            }
        }

        private bool _selectAllPhanHo;
        public bool SelectAllPhanHo
        {
            get => TlDmPhanHos?.All(item => item.IsSelected) ?? false;
            set
            {
                SetProperty(ref _selectAllPhanHo, value);
                IsTag = false;
                TlDmPhanHos?.ForAll(x => x.IsSelected = _selectAllPhanHo);
                IsTag = true;
                OnPropertyChanged(nameof(SelectAllPhanHo));
            }
        }

        private bool _selectAllDonVi;
        public bool SelectAllDonVi
        {
            get => NsDonVis?.All(item => item.Selected) ?? false;
            set
            {
                SetProperty(ref _selectAllDonVi, value);
                IsTag = false;
                NsDonVis?.ForAll(x => x.Selected = _selectAllDonVi);
                IsTag = true;
                OnPropertyChanged(nameof(SelectAllDonVi));
            }
        }

        private ObservableCollection<HTNhomModel> _groups;
        public ObservableCollection<HTNhomModel> Groups
        {
            get => _groups;
            set => SetProperty(ref _groups, value);
        }

        private ObservableCollection<DonViModel> _nsDonVis;
        public ObservableCollection<DonViModel> NsDonVis
        {
            get => _nsDonVis;
            set => SetProperty(ref _nsDonVis, value);
        }

        private ObservableCollection<TlDmDonViModel> _tlDmPhanHos;
        public ObservableCollection<TlDmDonViModel> TlDmPhanHos
        {
            get => _tlDmPhanHos;
            set => SetProperty(ref _tlDmPhanHos, value);
        }

        private ObservableCollection<CauHinhUserMLNSModel> _lns;
        public ObservableCollection<CauHinhUserMLNSModel> LNS
        {
            get => _lns;
            set => SetProperty(ref _lns, value);
        }

        public UserDialogViewModel(
            IGroupService groupService, 
            IAuthorityService authorityService, 
            IUserService userService, 
            IMapper mapper, 
            IAuthorityTypeService authorityTypeService,
            ISessionService sessionService,
            INsDonViService nsDonViService,
            ITlDmDonViService tTlDmDonViService,
            ICauHinhMLNSService cauHinhMLNSService)
        {
            _mapper = mapper;
            _groupService = groupService;
            _authorityService = authorityService;
            _userService = userService;
            _authorityTypeService = authorityTypeService;
            _cauHinhMLNSService = cauHinhMLNSService;
            _sessionService = sessionService;
            _nSDonViService = nsDonViService;
            _tTlDmDonViService = tTlDmDonViService;
        }

        public override void Init()
        {
            base.Init();
            BackgroundWorkerHelper.Run((e, s) =>
            {
                IsLoading = true;
                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    Name = "CẬP NHẬT TÀI KHOẢN";
                    Description = "Cập nhật tài khoản";
                    IconKind = PackIconKind.UserEdit;
                    _userEntity = _userService.FindUserWithGroupAndLns(Model.Id, _sessionService.Current.YearOfWork);
                }
                else
                {
                    Name = "TẠO MỚI TÀI KHOẢN";
                    Description = "Tạo mới tài khoản";
                    IconKind = PackIconKind.UserAdd;
                    _userEntity = new HtNguoiDung();
                }
                LoadAuthoritiesAndGroups();
                LoadDonVi();
                LoadPhanHo();
                LoadLns();
            }, (e, s) =>
            {
                IsLoading = false;
            });
            
        }

        private void LoadLns()
        {
            AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            ObservableCollection<NsMucLucNganSach> nsMucLucNganSaches = new ObservableCollection<NsMucLucNganSach>(_cauHinhMLNSService.FindAll(authenticationInfo));
            LNS = _mapper.Map<ObservableCollection<NsMucLucNganSach>, ObservableCollection<CauHinhUserMLNSModel>>(nsMucLucNganSaches);
            ObservableCollection<string> lnsOfUser = new ObservableCollection<string>(_userEntity.NguoiDungLns.Select(m => m.SLns));
            foreach (CauHinhUserMLNSModel model in LNS)
            {
                if (lnsOfUser.Contains(model.Lns))
                {
                    model.IsSelected = true;
                }
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(CauHinhUserMLNSModel.IsSelected) && IsTag)
                    {
                        OnPropertyChanged(nameof(SelectedLNSCount));
                        OnPropertyChanged(nameof(SelectAllLNS));
                    }
                };
            }
            _lnsView = CollectionViewSource.GetDefaultView(LNS);
            _lnsView.Filter = LNSFilter;
        }

        private bool LNSFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchLnsText))
            {
                return true;
            }
            return obj is CauHinhUserMLNSModel item && item.Lns.ToLower().Contains(_searchLnsText!.ToLower());
        }

        private void LoadAuthoritiesAndGroups()
        {
            ObservableCollection<HtLoaiQuyen> sysAuthorityTypes = new ObservableCollection<HtLoaiQuyen>(_authorityTypeService.FindAll(t => true));
            ObservableCollection<HtNhom> groupEntities = new ObservableCollection<HtNhom>(_groupService.FindAll(g => g.BKichHoat));
            Groups = _mapper.Map<ObservableCollection<HtNhom>, ObservableCollection<HTNhomModel>>(groupEntities);
            ObservableCollection<Guid> groupsOfUser = new ObservableCollection<Guid>(Model.SysGroupModels.Select(u => u.Id).ToList());
            foreach (HTNhomModel sysGroup in Groups)
            {
                if (groupsOfUser.Contains(sysGroup.Id))
                {
                    sysGroup.IsSelected = true;
                }
                sysGroup.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(HTNhomModel.IsSelected) && IsTag)
                    {
                        OnPropertyChanged(nameof(SelectedGroupsCount));
                    }
                };
            }
            _dataGroupsView = CollectionViewSource.GetDefaultView(Groups);
            _dataGroupsView.Filter = GroupsFilter;
        }

        private bool GroupsFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchGroupText))
            {
                return true;
            }
            return obj is HTNhomModel item && item.STenNhom.ToLower().Contains(_searchGroupText!.ToLower());
        }

        public void LoadDonVi()
        {
            List<string> selectedDonVi = _userEntity.NsNguoiDungDonVis.Select(t => t.IIdMaDonVi).ToList();
            
            IEnumerable<DonVi> listDonVi = _nSDonViService
                .FindByCondition(t => t.NamLamViec == _sessionService.Current.YearOfWork && t.ITrangThai == NSEntityStatus.ACTIVED 
                    && (t.Loai.Equals("0") || t.Loai.Equals("1")));
            NsDonVis = _mapper.Map<ObservableCollection<DonViModel>>(listDonVi);
            // Filter
            foreach (var model in NsDonVis)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DonViModel.Selected) && IsTag)
                    {
                        OnPropertyChanged(nameof(SelectedCountDonVi));
                        OnPropertyChanged(nameof(SelectAllDonVi));
                    }
                };
                model.Selected = selectedDonVi.Contains(model.IIDMaDonVi);
            }
            _donViGroupsView = CollectionViewSource.GetDefaultView(NsDonVis);
            _donViGroupsView.Filter = DonViFilter;
        }

        public void LoadPhanHo()
        {
            List<string> selectedPhanHo = _userEntity.TlNguoiDungPhanHos.Select(t => t.IIdMaDonVi).ToList();

            List<TlDmDonVi> listPhanHo = _tTlDmDonViService.FindAllDonVi().ToList();
            TlDmPhanHos = _mapper.Map<ObservableCollection<TlDmDonViModel>>(listPhanHo);
            // Filter
            foreach (var model in TlDmPhanHos)
            {
                model.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(TlDmDonViModel.IsSelected) && IsTag)
                    {
                        OnPropertyChanged(nameof(SelectedCountPhanHo));
                        OnPropertyChanged(nameof(SelectAllPhanHo));
                    }
                };
                model.IsSelected = selectedPhanHo.Contains(model.MaDonVi);
            }
            _phanHoGroupView = CollectionViewSource.GetDefaultView(TlDmPhanHos);
            _phanHoGroupView.Filter = PhanHoFilter;
        }

        private bool DonViFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchDonViText))
            {
                return true;
            }
            return obj is DonViModel item && item.TenDonVi.ToLower().Contains(_searchDonViText!.ToLower());
        }

        private bool PhanHoFilter(object obj)
        {
            if (string.IsNullOrWhiteSpace(_searchPhanHoText))
            {
                return true;
            }
            return obj is TlDmDonViModel item && item.TenDonVi.ToLower().Contains(_searchPhanHoText!.ToLower());
        }

        public override void OnSave(object parameter)
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                string password = ((PasswordBox)parameter).Password;
                HtNguoiDung existUser = _userService.FindByLogin(Model.STaiKhoan);
                if (existUser != null && Model.STaiKhoan.Equals(existUser.STaiKhoan))
                {
                    MessageBox.Show(Resources.ErrorUserNameExist);
                    return;
                }
                _userEntity.SMatKhau = _userService.CalculateHash(password, Model.STaiKhoan);
            }
            IEnumerable<string> selectedDvs = NsDonVis.Where(i => i.Selected).Select(i => i.IIDMaDonVi);
            Model.IdDonVi = string.Join(",", selectedDvs);
            
            _mapper.Map(Model, _userEntity);
            _userEntity.SysGroupUsers.Clear();

            // map nguoi dung don vi
            _userEntity.NsNguoiDungDonVis = NsDonVis.Where(i => i.Selected).Select(t => new NguoiDungDonVi
            {
                IIdMaDonVi = t.IIDMaDonVi,
                STenDonVi = t.TenDonVi
            }).ToList();

            _userEntity.TlNguoiDungPhanHos = TlDmPhanHos.Where(i => i.IsSelected).Select(t => new NguoiDungPhanHo
            {
                IIdMaDonVi = t.MaDonVi,
                STenDonVi = t.TenDonVi
            }).ToList();

            // map nguoi dung lns
            _userEntity.NguoiDungLns = LNS.Where(i => i.IsSelected).Select(t => new NsNguoiDungLns
            {
                SLns = t.Lns,
                SMaNguoiDung = _userEntity.STaiKhoan,
                INamLamViec = _sessionService.Current.YearOfWork
            }).ToList();

            // map nhom
            foreach (HTNhomModel group in Groups)
            {
                if (group.IsSelected)
                {
                    _userEntity.SysGroupUsers.Add(new HtNhomNguoiDung { IIDMaNguoiDung = Model.Id, IIDNhom = group.Id });
                }
            }

            if (Model.Id != null && Model.Id != Guid.Empty)
            {
                _userService.Update(_userEntity, _sessionService.Current.YearOfWork);
            }
            else
            {
                _userService.Add(_userEntity, _sessionService.Current.YearOfWork);
            }
            this.ParentPage.Init();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public override void OnClose(object obj)
        {
            base.OnClose(obj);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
