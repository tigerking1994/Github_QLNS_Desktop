using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.UserManagement.Group;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.UserManagement.Group
{
    public class GroupDialogViewModel : DialogViewModelBase<HTNhomModel>
    {
        private IGroupService _groupService;
        private IAuthorityService _authorityService;
        private IUserService _userService;
        private IAuthorityTypeService _authorityTypeService;
        private IMapper _mapper;
        private HtNhom _groupEntity;

        public override Type ContentType => typeof(GroupDialog);
        public override string Name => "NHÓM NGƯỜI DÙNG";
        public GroupIndexViewModel GroupIndexViewModel { get; set; }

        public string SelectedAuthoritiesCount
        {
            get
            {
                IEnumerable<HTQuyenModel> authorityModels = AuthorityTypes.Select(i => i.HTQuyenModels).SelectMany(i => i);
                int totalCount = authorityModels != null ? authorityModels.ToList().Count() : 0;
                int totalSelected = authorityModels != null ? authorityModels.Count(x => x.IsSelected) : 0;
                return string.Format(NSLabel.SELECTED_COUNT_AUTHORITIES_STR, totalSelected, totalCount);
            }
        }

        private ObservableCollection<HTLoaiQuyenModel> _authorityTypes;
        public ObservableCollection<HTLoaiQuyenModel> AuthorityTypes
        {
            get => _authorityTypes;
            set => SetProperty(ref _authorityTypes, value);
        }

        public GroupDialogViewModel(IGroupService groupService, IAuthorityService authorityService, IUserService userService, IMapper mapper, IAuthorityTypeService authorityTypeService)
        {
            _mapper = mapper;
            _groupService = groupService;
            _authorityService = authorityService;
            _userService = userService;
            _authorityTypeService = authorityTypeService;
        }

        public override void Init()
        {
            base.Init();
            if (Model.Id != null && Model.Id != Guid.Empty)
            {
                Description = "Cập nhật nhóm người dùng";
                _groupEntity = _groupService.FindById(Model.Id);
                Model = _mapper.Map<HtNhom, HTNhomModel>(_groupEntity);
            }
            else
            {
                _groupEntity = new HtNhom();
                Description = "Tạo mới nhóm người dùng";
            }
            LoadAuthorities();
        }

        private void LoadAuthorities()
        {
            ObservableCollection<HtLoaiQuyen> sysAuthorityTypes = new ObservableCollection<HtLoaiQuyen>(_authorityTypeService.FindAll());
            AuthorityTypes = _mapper.Map<ObservableCollection<HtLoaiQuyen>, ObservableCollection<HTLoaiQuyenModel>>(sysAuthorityTypes);
            ObservableCollection<string> authoritiesOfGroup = new ObservableCollection<string>(Model.HTQuyenModels.Select(u => u.IIDMaQuyen).ToList());
            foreach (HTLoaiQuyenModel sysAuthorityTypeModel in AuthorityTypes)
            {
                foreach (HTQuyenModel sysAuthorityModel in sysAuthorityTypeModel.HTQuyenModels)
                {
                    sysAuthorityModel.PropertyChanged += (sender, args) =>
                    {
                        sysAuthorityTypeModel.OnSelectChange();
                        OnPropertyChanged(nameof(SelectedAuthoritiesCount));
                    };
                    if (authoritiesOfGroup.Contains(sysAuthorityModel.IIDMaQuyen))
                    {
                        sysAuthorityModel.IsSelected = true;
                    }
                }
            }
            OnPropertyChanged(nameof(AuthorityTypes));
        }

        private bool Validate(object parameter)
        {
            if (String.IsNullOrEmpty(Model.STenNhom))
            {
                MessageBox.Show(Resources.ErrorGroupNameEmpty);
                return false;
            }
            return true;
        }

        private void SaveData(object parameter)
        {
            _mapper.Map<HTNhomModel, HtNhom>(Model, _groupEntity);
            _groupEntity.SysGroupAuthorities.Clear();
            foreach (HTLoaiQuyenModel sysAuthorityTypeModel in AuthorityTypes)
            {
                foreach (HTQuyenModel sysAuthorityModel in sysAuthorityTypeModel.HTQuyenModels)
                {
                    if (sysAuthorityModel.IsSelected)
                    {
                        _groupEntity.SysGroupAuthorities.Add(new HtNhomQuyen { IIDNhom = Model.Id, IIDMaQuyen = sysAuthorityModel.IIDMaQuyen });
                    }
                }
            }
            if (Model.Id != null && Model.Id != Guid.Empty)
            {
                _groupService.Update(_groupEntity);
            }
            else
            {
                _groupService.Add(_groupEntity);
            }
            GroupIndexViewModel.Init();
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public override void OnSave(object obj)
        {
            if (Validate(obj))
                SaveData(obj);
        }

        public override void OnClose(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
