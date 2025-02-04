using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.View.Shared;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service.UserFunction;
using VTS.QLNS.CTC.App.View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;
using System.Windows.Markup;
using MaterialDesignThemes.Wpf;
using System.Windows.Forms;
using DataGrid = System.Windows.Controls.DataGrid;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao
{
    public class ForexDanhSachKhoiTaoDetailViewModel : DetailViewModelBase<NhKtKhoiTaoCapPhatModel, NhKtKhoiTaoCapPhatChiTietModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhDaDuAnService _nhDaDuAnService;
        private readonly INhDaHopDongService _nhDaHopDongService;
        private readonly IServiceProvider _provider;
        private readonly INhKtKhoiTaoCapPhatChiTietService _service;
        private readonly INhKtKhoiTaoCapPhatService _nhKtKhoiTaoCapPhatService;
        private readonly INhThTongHopService _nhThTongHopService;
        private readonly INhDmTiGiaService _iNhDmTiGiaService;
        private readonly INhDmTiGiaChiTietService _iNhDmTiGiaChiTietService;
        private static Guid IIdCheck = Guid.Empty;

        public override string Name => "Khởi tạo cấp phát";
        public override string Title => "Chi tiết khởi tạo cấp phát";
        public override string Description => "Chi tiết khởi tạo cấp phát";
        public override Type ContentType => typeof(ForexDanhSachKhoiTaoDetail);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public RelayCommand OpenReferencePopupCommand { get; }

        public ForexDanhSachKhoiTaoDetailViewModel(
            ILog logger,
            IMapper mapper,
            INhDaDuAnService nhDaDuAnService,
            INhDaHopDongService nhDaHopDongService,
            ISessionService sessionService,
            IServiceProvider serviceProvider,
            INhDmTiGiaChiTietService iNhDmTiGiaChiTietService,
            INhDmTiGiaService iNhDmTiGiaService,
            INhThTongHopService nhThTongHopService,
            INhKtKhoiTaoCapPhatService nhKtKhoiTaoCapPhatService,
            INhKtKhoiTaoCapPhatChiTietService service)
        {
            _logger = logger;
            _provider = serviceProvider;
            _mapper = mapper;
            _nhDaDuAnService = nhDaDuAnService;
            _nhDaHopDongService = nhDaHopDongService;
            _sessionService = sessionService;
            _service = service;
            _iNhDmTiGiaService = iNhDmTiGiaService;
            _nhThTongHopService = nhThTongHopService;
            _iNhDmTiGiaChiTietService = iNhDmTiGiaChiTietService;
            _nhKtKhoiTaoCapPhatService = nhKtKhoiTaoCapPhatService;
            OpenReferencePopupCommand = new RelayCommand(obj => OnOpenReferencePopup(obj));
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>();
            var data = _service.FindById(Model.Id);
            Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>>(data);
            foreach (var item in Items)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void OnOpenReferencePopup(object obj)
        {
            try
            {
                var iIDDon_Vi = Model.IIdDonViID;
                DataGrid dataGrid = obj as DataGrid;
                if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenDuAn"))
                {
                    GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService> viewModelBase = new GenericControlCustomViewModel<NhDaDuAnModel, Core.Domain.NhDaDuAn, NhDaDuAnService>((NhDaDuAnService)_nhDaDuAnService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh sách dự án",
                        Title = "Danh sách dự án",
                        Description = "Danh sách dự án",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Building,
                        IsDialog = true,
                        iID_DonViID = iIDDon_Vi
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh sách dự án",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            NhDaDuAnModel item2 = (NhDaDuAnModel)obj;

                            if (item2 != null)
                            {
                                var temp = Items;
                                foreach (var item in temp)
                                {
                                    if (item.Id == IIdCheck)
                                    {
                                        item.STenDuAn = item2.STenDuAn;
                                        item.IIdDuAnID = item2.Id;
                                    }
                                }
                                Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>>(temp);
                            }
                            GenericControlCustomWindow.Close();
                            OnPropertyChanged(nameof(Items));
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
                    };
                    viewModelBase.GenericControlCustomWindow = GenericControlCustomWindow;
                    GenericControlCustomWindow.Show();
                }
                else if (dataGrid.CurrentCell.Column.SortMemberPath.Equals("STenHopDong"))
                {
                    GenericControlCustomViewModel<NhDaHopDongModel, Core.Domain.NhDaHopDong, NhDaHopDongService> viewModelBase = new GenericControlCustomViewModel<NhDaHopDongModel, Core.Domain.NhDaHopDong, NhDaHopDongService>((NhDaHopDongService)_nhDaHopDongService, _mapper, _sessionService, _provider)
                    {
                        Name = "Danh sách hợp đồng",
                        Title = "Danh sách hợp đồng",
                        Description = "Danh sách hợp đồng",
                        IconKind = MaterialDesignThemes.Wpf.PackIconKind.Building,
                        IsDialog = true,
                        iID_DonViID = iIDDon_Vi
                    };
                    GenericControlCustomWindowViewModel genericControlCustomWindowViewModel = new GenericControlCustomWindowViewModel(viewModelBase);
                    GenericControlCustomWindow GenericControlCustomWindow = new GenericControlCustomWindow
                    {
                        DataContext = genericControlCustomWindowViewModel,
                        Title = "Danh sách hợp đồng",
                    };

                    GenericControlCustomWindow.SavedAction = obj =>
                    {
                        try
                        {
                            NhDaHopDongModel item2 = (NhDaHopDongModel)obj;

                            if (item2 != null)
                            {
                                var temp = Items;
                                foreach (var item in temp)
                                {
                                    if (item.Id == IIdCheck)
                                    {
                                        item.STenHopDong = item2.STenHopDong;
                                        item.IIdHopDongID = item2.Id;
                                    }
                                }
                                Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoCapPhatChiTietModel>>(temp);
                            }
                            GenericControlCustomWindow.Close();
                            OnPropertyChanged(nameof(Items));
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex.Message, ex);
                        }
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
            int currentRow = -1;
            if (!Items.IsEmpty())
            {
                if (SelectedItem != null)
                {
                    currentRow = Items.IndexOf(SelectedItem);
                }
            }

            NhKtKhoiTaoCapPhatChiTietModel targetItem = new NhKtKhoiTaoCapPhatChiTietModel();
            targetItem.Id = Guid.NewGuid();
            targetItem.IIdKhoiTaoCapPhatID = Model.Id;
            targetItem.IsAdded = true;
            targetItem.IsModified = true;
            targetItem.PropertyChanged += DetailModel_PropertyChanged;

            Items.Insert(currentRow + 1, targetItem);
            OnPropertyChanged(nameof(Items));
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
        }

        public override void OnSave(object obj)
        {
            var entities = _mapper.Map<List<NhKtKhoiTaoCapPhatChiTiet>>(Items);
            bool isNull = false, isValidate = false, isAddTongHop = false, isUpdateTongHop = false;

            //Check validate
            foreach (var item in entities.Where(x => !x.IsDeleted))
            {
                if (item.IIdDuAnID == null && item.IIdHopDongID == null)
                {
                    isNull = true;
                    break;
                }
                NhKtKhoiTaoCapPhatChiTietModel model = new NhKtKhoiTaoCapPhatChiTietModel();
                model.FDeNghiQTNamNayUSD = item.FDeNghiQTNamNayUSD;
                model.FDeNghiQTNamNayVND = item.FDeNghiQTNamNayVND;
                model.FLuyKeKinhPhiDuocCapUSD = item.FQTKinhPhiDuyetCacNamTruocUSD;
                model.FLuyKeKinhPhiDuocCapVND = item.FLuyKeKinhPhiDuocCapVND;
                model.FQTKinhPhiDuyetCacNamTruocUSD = item.FQTKinhPhiDuyetCacNamTruocUSD;
                model.FQTKinhPhiDuyetCacNamTruocVND = item.FQTKinhPhiDuyetCacNamTruocVND;
                if (!ValidateViewModelHelper.Validate(model))
                {
                    isValidate = true;
                    break;
                }
                if (item.IsModified)
                {
                    isUpdateTongHop = true;
                }
                if (item.IsAdded)
                {
                    isAddTongHop = true;
                }
                else
                {
                    isAddTongHop = false;
                }
            }

            if (isNull || isValidate)
            {
                if (isNull)
                {
                    List<string> lstError = new List<string>();
                    lstError.Add(Resources.MsgCheckProjectOrContract);
                    MessageBoxHelper.Warning(string.Join("\n", lstError));
                    return;
                }
                else
                {
                    return;
                }
            }
            else 
            {
                BackgroundWorkerHelper.Run((s, e) =>
                {
                    IsLoading = true;

                    var entity = _mapper.Map<NhKtKhoiTaoCapPhat>(Model);
                    if (Model.Id.IsNullOrEmpty())
                    {
                        // Thêm mới
                        _nhKtKhoiTaoCapPhatService.Add(entity);
                        foreach (var item in entities)
                        {
                            item.IIdKhoiTaoCapPhatID = entity.Id;
                        }
                    }
                    else
                    {
                        // Cập nhật
                        _nhKtKhoiTaoCapPhatService.Update(entity);
                    }

                    _service.AddOrUpdate(entities);

                    if (!Model.IsModified) //Insert phần tổng hợp
                    {
                        if (isAddTongHop)
                        {
                            //_nhThTongHopService.InsertNHTongHop_Tang("KHOI_TAO", 1, entity.Id);
                            //_nhThTongHopService.InsertNHTongHop_Giam("KHOI_TAO", 1, entity.Id);
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.KHOI_TAO, (int)TypeExecute.Insert, entity.Id);
                        }
                        else
                        {
                            if (isUpdateTongHop) //Update phần tổng hợp
                            {
                                //_nhThTongHopService.InsertNHTongHop_Tang("KHOI_TAO", 2, entity.Id);
                                //_nhThTongHopService.InsertNHTongHop_Giam("KHOI_TAO", 2, entity.Id);
                                _nhThTongHopService.InsertNHTongHop_New(NHConstants.KHOI_TAO, (int)TypeExecute.Update, entity.Id);

                            }
                        }
                    }
                    else //Update phần tổng hợp
                    {
                        if (isUpdateTongHop)
                        {
                            //_nhThTongHopService.InsertNHTongHop_Tang("KHOI_TAO", 2, entity.Id);
                            //_nhThTongHopService.InsertNHTongHop_Giam("KHOI_TAO", 2, entity.Id);
                            _nhThTongHopService.InsertNHTongHop_New(NHConstants.KHOI_TAO, (int)TypeExecute.Update, entity.Id);
                        }
                    }
                }, (s, e) =>
                {
                    IsLoading = false;

                    if (e.Error == null)
                    {
                        DialogResult dialog = System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
                        if (dialog == DialogResult.OK)
                        {
                            Model = _mapper.Map<NhKtKhoiTaoCapPhatModel>(e.Result);
                            SavedAction?.Invoke(Model);

                            //Đóng popup
                            if (obj is Window window)
                            {
                                window.Close();
                            }
                        }
                    }
                    else
                    {
                        _logger.Error(e.Error.Message);
                    }
                });
            }
        }

        protected override void OnSelectedItemChanged()
        {
            if(SelectedItem != null)
            {
                IIdCheck = SelectedItem.Id;
                SelectedItem.IsModified = true;
            }
        }

        public override void OnClosing()
        {
            // Clear items
            if (!Items.IsEmpty()) Items.Clear();
        }

        public override void OnClose(object obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NhKtKhoiTaoCapPhatChiTietModel item = (NhKtKhoiTaoCapPhatChiTietModel)sender;
            if(Model.IIdTiGiaID != null)
            {
                Guid idTyGia = (Guid)Model.IIdTiGiaID;
                var dmTyGia = _iNhDmTiGiaService.FindById(idTyGia);
                var listTyGiaCT = _iNhDmTiGiaChiTietService.FindByTiGiaId(idTyGia);
                var rootCurrency = dmTyGia.SMaTienTeGoc;
                double value = 0;
                switch (e.PropertyName)
                {
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FDeNghiQTNamNayUSD):
                        value = Convert.ToDouble(item.FDeNghiQTNamNayUSD.GetValueOrDefault());
                        item.FDeNghiQTNamNayVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                        break;
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FLuyKeKinhPhiDuocCapUSD):
                        value = Convert.ToDouble(item.FLuyKeKinhPhiDuocCapUSD.GetValueOrDefault());
                        item.FLuyKeKinhPhiDuocCapVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                        break;
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FQTKinhPhiDuyetCacNamTruocUSD):
                        value = Convert.ToDouble(item.FQTKinhPhiDuyetCacNamTruocUSD.GetValueOrDefault());
                        item.FQTKinhPhiDuyetCacNamTruocVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                        break;
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FDeNghiQTNamNayVND):
                        value = Convert.ToDouble(item.FDeNghiQTNamNayVND.GetValueOrDefault());
                        item.FDeNghiQTNamNayUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                        break;
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FLuyKeKinhPhiDuocCapVND):
                        value = Convert.ToDouble(item.FLuyKeKinhPhiDuocCapVND.GetValueOrDefault());
                        item.FLuyKeKinhPhiDuocCapUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                        break;
                    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FQTKinhPhiDuyetCacNamTruocVND):
                        value = Convert.ToDouble(item.FQTKinhPhiDuyetCacNamTruocVND.GetValueOrDefault());
                        item.FQTKinhPhiDuyetCacNamTruocUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
