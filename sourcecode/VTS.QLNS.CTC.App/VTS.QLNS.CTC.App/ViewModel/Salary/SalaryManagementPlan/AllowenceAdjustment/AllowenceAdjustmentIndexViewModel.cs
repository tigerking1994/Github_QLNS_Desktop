using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Properties;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagementPlan.AllowenceAdjustment
{
    public class AllowenceAdjustmentIndexViewModel : GridViewModelBase<TlPhuCapDieuChinhModel>
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IServiceProvider _provider;
        private readonly ITlPhuCapDieuChinhService _tlPhuCapDieuChinhService;
        private readonly ITlDmPhuCapService _tlDmPhuCapService;

        public override string FuncCode => NSFunctionCode.SALARY_QUAN_LY_LUONG_KE_HOACH_DIEU_CHINH_PHU_CAP_THU_NHAP_INDEX;
        public override string GroupName => MenuItemContants.GROUP_FUNCTION;
        public override string Name => "Điều chỉnh phụ cấp thu nhập ";
        public override string Title => "Điều chỉnh phụ cấp lương năm kế hoạch";
        public override string Description => "Điều chỉnh phụ cấp lương năm kế hoạch";
        public override PackIconKind IconKind => PackIconKind.Tune;
        public override Type ContentType => typeof(View.Salary.SalaryManagementPlan.AllowenceAdjustment.AllowenceAdjustmentIndex);

        public RelayCommand OpenReferencePopupCommand { get; }

        public bool IsEnabled => SelectedItem != null;
        private int currentRow = -1;

        public AllowenceAdjustmentIndexViewModel(
            ISessionService sessionService,
            ILog logger,
            IMapper mapper,
            IServiceProvider serviceProvider,
            ITlPhuCapDieuChinhService tlPhuCapDieuChinhService,
            ITlDmPhuCapService tlDmPhuCapService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _logger = logger;

            _tlPhuCapDieuChinhService = tlPhuCapDieuChinhService;
            _provider = serviceProvider;
            _tlDmPhuCapService = tlDmPhuCapService;

            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
        }

        public override void Init()
        {
            base.Init();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            var data = _tlPhuCapDieuChinhService.FindAllPhuCapDieuChinh();
            Items = _mapper.Map<ObservableCollection<TlPhuCapDieuChinhModel>>(data);
            foreach (var item in Items)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            TlPhuCapDieuChinhModel tlPhuCapDieuChinhModel = (TlPhuCapDieuChinhModel)sender;
            tlPhuCapDieuChinhModel.IsModified = true;
            OnPropertyChanged(nameof(Items));
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                DataGrid dataGrid = obj as DataGrid;
                if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("MaPhuCap"))
                {
                    GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService> viewModelBase = new GenericControlCustomViewModel<TlDmPhuCapModel, TlDmPhuCap, TlDmPhuCapService>((TlDmPhuCapService)_tlDmPhuCapService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh mục phụ cấp",
                        Title = "Danh mục phụ cấp",
                        Description = "Danh sách danh mục phụ cấp",
                        IsDialog = true
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            TlDmPhuCapModel tlDmPhuCapModel = (TlDmPhuCapModel)obj;
                            if (tlDmPhuCapModel != null)
                            {
                                foreach (var item in Items)
                                {
                                    if (item.DateCreated.Equals(SelectedItem.DateCreated))
                                    {
                                        item.IdPhuCap = tlDmPhuCapModel.Id;
                                        item.MaPhuCap = tlDmPhuCapModel.MaPhuCap;
                                        item.TenPhuCap = tlDmPhuCapModel.TenPhuCap;
                                        item.GiaTriCu = tlDmPhuCapModel.GiaTri;
                                        item.GiaTriMoi = 0;
                                    }
                                }
                            }
                            OnPropertyChanged(nameof(Items));
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                        GenericControlCustomWindow.Close();
                    };
                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        protected override void OnAdd()
        {
            base.OnAdd();
            if (SelectedItem == null || Items.Count == 0)
            {
                TlPhuCapDieuChinhModel tlPhuCapDieuChinhModel = new TlPhuCapDieuChinhModel();
                tlPhuCapDieuChinhModel.UserCreator = _sessionService.Current.Principal;
                tlPhuCapDieuChinhModel.DateCreated = DateTime.Now;
                tlPhuCapDieuChinhModel.PropertyChanged += DetailModel_PropertyChanged;
                Items.Add(tlPhuCapDieuChinhModel);
            }
            else
            {
                TlPhuCapDieuChinhModel sourceItem = SelectedItem;
                TlPhuCapDieuChinhModel targetItem = ObjectCopier.Clone(sourceItem);

                currentRow = Items.IndexOf(SelectedItem);

                targetItem.Id = Guid.Empty;
                targetItem.UserCreator = _sessionService.Current.Principal;
                targetItem.DateCreated = DateTime.Now;
                targetItem.IsModified = true;

                targetItem.PropertyChanged += DetailModel_PropertyChanged;
                Items.Insert(currentRow + 1, targetItem);
            }
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnDelete()
        {
            base.OnDelete();
            if (Items != null && Items.Count > 0 && SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        public override void OnSave()
        {
            List<TlPhuCapDieuChinhModel> listAdd = Items.Where(x => x.IsModified && !x.IsDeleted && (x.Id == Guid.Empty || x.Id == null)).ToList();
            List<TlPhuCapDieuChinhModel> listEdit = Items.Where(x => x.IsModified && !x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();
            List<TlPhuCapDieuChinhModel> listDelete = Items.Where(x => x.IsDeleted && x.Id != Guid.Empty && x.Id != null).ToList();


            try
            {
                if (listAdd != null && listAdd.Count > 0)
                {
                    var listData = _mapper.Map<List<TlPhuCapDieuChinh>>(listAdd);
                    _tlPhuCapDieuChinhService.AddRange(listData);
                }

                if (listEdit != null && listEdit.Count > 0)
                {
                    foreach (var item in listEdit)
                    {
                        TlPhuCapDieuChinh tlPhuCapDieuChinh = _mapper.Map<TlPhuCapDieuChinh>(item);
                        tlPhuCapDieuChinh.DateModified = DateTime.Now;
                        tlPhuCapDieuChinh.UserModifier = _sessionService.Current.Principal;
                        _tlPhuCapDieuChinhService.Update(tlPhuCapDieuChinh);

                    }
                }

                if (listDelete != null && listDelete.Count > 0)
                {
                    foreach (var item in listDelete)
                    {
                        _tlPhuCapDieuChinhService.Delete(item.Id);
                    }
                }

                MessageBoxHelper.Info(Resources.MsgDieuChinhPhuCapLuongKeHoachSuccess);
            } catch
            {
                MessageBoxHelper.Info(Resources.MsgDieuChinhPhuCapLuongKeHoachError);
            }
            LoadData();
        }

        protected override void OnSelectedItemChanged()
        {
            base.OnSelectedItemChanged();
            OnPropertyChanged(nameof(IsEnabled));
        }
    }
}
