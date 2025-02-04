using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Category
{
    public class CategoryForexDialogViewModel : DialogViewModelBase<NhDmTiGiaModel>
    {
        public override string Name => "Danh mục tỉ giá";
        public override string Description => "Chỉ chọn USD - VND, USD - EUR, USD - Ngoại tệ khác";

        private readonly INhDmTiGiaService _service;
        private readonly ILog _logger;
        private IMapper _mapper;

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private List<NhDmTiGiaModel> _lstChoose;
        public List<NhDmTiGiaModel> LstChoose
        {
            get => _lstChoose;
            set => SetProperty(ref _lstChoose, value);
        }

        public CategoryForexDialogViewModel(
            INhDmTiGiaService service,
            IMapper mapper,
            ILog logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                Get();
                if (LstChoose == null) return;
                foreach(var item in ItemsTiGia)
                {
                    if (LstChoose.Any(n => n.Id == item.Id))
                        item.IsChecked = true;
                    else
                        item.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private void Get()
        {
            try
            {
                var data = _service.FindAll();
                if (data == null)
                    ItemsTiGia = new ObservableCollection<NhDmTiGiaModel>();
                else
                    ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            OnPropertyChanged(nameof(ItemsTiGia));
        }

        public override void OnSave()
        {
            LstChoose = ItemsTiGia.Where(n => n.IsChecked).ToList();
            DialogHost.CloseDialogCommand.Execute(null, null);
            SavedAction?.Invoke(Model);
        }
    }
}
