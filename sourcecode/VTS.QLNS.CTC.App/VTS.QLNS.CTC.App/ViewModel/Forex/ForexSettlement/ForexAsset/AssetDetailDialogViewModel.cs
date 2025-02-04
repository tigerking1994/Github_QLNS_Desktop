using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexSettlement.ForexAsset;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexSettlement.ForexAsset
{
    public class AssetDetailDialogViewModel : DialogCurrencyAttachmentViewModelBase<NhQtTaiSanModel>
    {
        private readonly INhDmLoaiTaiSanService _service;
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly INhQtTaiSanService _nhQtTaiSanService;
        private ObservableCollection<NhDmLoaiTaiSanModel> _itemsDanhMucLoaiTaiSan;
        public ObservableCollection<NhDmLoaiTaiSanModel> ItemsDanhMucLoaiTaiSan
        {
            get => _itemsDanhMucLoaiTaiSan;
            set => SetProperty(ref _itemsDanhMucLoaiTaiSan, value);
        }
        private NhDmLoaiTaiSanModel _selectedDanhMucLoaiTaiSan;
        public NhDmLoaiTaiSanModel SelectedDanhMucLoaiTaiSan
        {
            get => _selectedDanhMucLoaiTaiSan;
            set => SetProperty(ref _selectedDanhMucLoaiTaiSan, value);
        }
        public override Type ContentType => typeof(AssetDetailDialog);
        public override string Title => "Tài sản hình thành theo hợp đồng";
        public override string Name => "Tài sản hình thành theo hợp đồng";
        public AssetDialogViewModel AssetDialogViewModel { get; set; }
        public RelayCommand AddTaiSanCommand { get; }
        public RelayCommand DeleteTaiSanCommand { get; }
        public RelayCommand UpdateTaiSanCommand { get; }
        public AssetDetailDialogViewModel(
            IMapper mapper,
            ILog logger,
            ISessionService sessionService,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService,
             INhDmLoaiTaiSanService service,
             INhQtTaiSanService nhQtTaiSanService
            ) : base(mapper, nhDmTiGiaService, nhDmTiGiaChiTietService, storageServiceFactory, attachService)
        {
            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _service = service;
            _nhQtTaiSanService = nhQtTaiSanService;
            AddTaiSanCommand = new RelayCommand(obj => OnAddTaiSan());
            DeleteTaiSanCommand = new RelayCommand(obj => OnDeleteTaiSan());
            UpdateTaiSanCommand = new RelayCommand(obj => OnUpdateTaiSan());
        }
        public override void OnSave()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;
                // Main process
                //NhQtTaiSan entity;   
                NhDmLoaiTaiSan entity;
                var a = ItemsDanhMucLoaiTaiSan.Where(x => x.IsChecked == true);

                if (SelectedDanhMucLoaiTaiSan.Id.IsNullOrEmpty())
                {
                    // Thêm mới
                    //entity = _mapper.Map<NhQtTaiSan>(Model);
                    entity = _mapper.Map<NhDmLoaiTaiSan>(SelectedDanhMucLoaiTaiSan);
                    entity.Id = Guid.NewGuid();
                    _service.Add(entity);

                  
                    e.Result = entity;
                }
                else
                {
                    var lstDelete = ItemsDanhMucLoaiTaiSan.Where(x => x.IsDeleted && x.Id != Guid.Empty).ToList();
                    if (lstDelete != null && lstDelete.Count > 0)
                    {
                        foreach (var item in lstDelete)
                        {
                            _service.Delete(item.Id);
                        }
                        e.Result = lstDelete;
                    }
                    else
                    {
                        // Cập nhật
                        if(!SelectedDanhMucLoaiTaiSan.Id.IsNullOrEmpty())
                        {
                            entity = _service.FindById(SelectedDanhMucLoaiTaiSan.Id);
                        }    
                        entity = _mapper.Map<NhDmLoaiTaiSan>(SelectedDanhMucLoaiTaiSan);
                        _service.Update(entity);

                        //var entity2 = _mapper.Map<NhQtTaiSan>(Model);
                        //entity2.IIdLoaiTaiSan = entity.Id;
                        //entity2.STenTaiSan = entity.STenLoaiTaiSan;
                        //entity2.SMaTaiSan = entity.SMaLoaiTaiSan;
                        //_nhQtTaiSanService.Add(entity2);
                        e.Result = entity;
                    }    
                }

                if(a.Count()==1)
                {
                    if (a.Any())
                    {
                        //var entity2 = _mapper.Map<NhQtTaiSan>(Model);
                        Model.IIdLoaiTaiSan = a.FirstOrDefault().Id;
                        Model.STenTaiSan = a.FirstOrDefault().STenLoaiTaiSan;
                        Model.SMaTaiSan = a.FirstOrDefault().SMaLoaiTaiSan;
                        _nhQtTaiSanService.Update(_mapper.Map<NhQtTaiSan>(Model));
                        OnPropertyChanged(nameof(Model));
                    }
                }    
                
                if(a.Count()>1)
                    {
                        MessageBoxHelper.Info("Bạn chỉ được chọn 1 danh mục");
                        OnPropertyChanged(nameof(Model));
                }    
               

                //e.Result = entity;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    // Reload data
                    /*Model = _mapper.Map<NhQtQuyetToanNienDoModel>(e.Result);*/

                    // Invoke message
                    MessageBoxHelper.Info(Resources.MsgSaveDone);

                    SavedAction?.Invoke(SelectedDanhMucLoaiTaiSan);
                    DialogHost.CloseDialogCommand.Execute(null, null);

                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                LoadData();
                IsLoading = false;
            });
        }
        private void OnUpdateTaiSan()
        {
            if (SelectedDanhMucLoaiTaiSan != null)
            {
                SelectedDanhMucLoaiTaiSan.IsModified = !SelectedDanhMucLoaiTaiSan.IsModified;
            }
            OnPropertyChanged(nameof(ItemsDanhMucLoaiTaiSan));
        }
        private void OnDeleteTaiSan()
        {
            if (SelectedDanhMucLoaiTaiSan != null)
            {
                SelectedDanhMucLoaiTaiSan.IsDeleted = !SelectedDanhMucLoaiTaiSan.IsDeleted;
            }
            OnPropertyChanged(nameof(ItemsDanhMucLoaiTaiSan));
        }
        private void OnAddTaiSan()
        {
            NhDmLoaiTaiSanModel newItem = new NhDmLoaiTaiSanModel();
            ItemsDanhMucLoaiTaiSan.Insert(ItemsDanhMucLoaiTaiSan.Count, newItem);
            OnPropertyChanged(nameof(ItemsDanhMucLoaiTaiSan));
        }
        public override void Init()
        {
            LoadDanhMucLoaiTaiSan();
            LoadData();
            OnPropertyChanged(nameof(SelectedDanhMucLoaiTaiSan));
        }
        public override void LoadData(params object[] args)
        {
            Description = "Mã tài sản";
            var listDanhMuc = _service.FindAll().Where(x => !x.IsDeleted);
            var listDanhMucModel = _mapper.Map<IEnumerable<NhDmLoaiTaiSanModel>>(listDanhMuc);
            foreach (var item in listDanhMucModel)
            {
                if(item.Id==Model.IIdLoaiTaiSan)
                {
                    item.IsChecked = true;
                }    
                else
                {
                    item.IsChecked=false;
                }    
            }   
            ItemsDanhMucLoaiTaiSan = _mapper.Map<ObservableCollection< NhDmLoaiTaiSanModel>>(listDanhMucModel);

            foreach(var item in ItemsDanhMucLoaiTaiSan)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }    
            //OnPropertyChanged(nameof(ItemsDanhMucLoaiTaiSan));
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //var item = (NhDmLoaiTaiSanModel)sender;
            var item = sender as NhDmLoaiTaiSanModel;
            //if(!item.IsModified && !e.PropertyName.Equals(nameof(ModelBase.IsChecked)))
            if(!item.IsModified && !e.PropertyName.Equals(nameof(ModelBase.IsChecked)))
            {
                item.IsChecked = !item.IsChecked;
                item.IsModified = true;
            }    
        }

        private void LoadDanhMucLoaiTaiSan()
        {
            List<NhDmLoaiTaiSan> listData = _service.FindAll().ToList();
            ItemsDanhMucLoaiTaiSan = _mapper.Map<ObservableCollection<NhDmLoaiTaiSanModel>>(listData);
            OnPropertyChanged(nameof(ItemsDanhMucLoaiTaiSan));
        }
    }
}
