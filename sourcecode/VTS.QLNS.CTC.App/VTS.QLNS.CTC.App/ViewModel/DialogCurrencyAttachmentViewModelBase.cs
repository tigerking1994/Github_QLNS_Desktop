using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class DialogCurrencyAttachmentViewModelBase<T> : DialogAttachmentViewModelBase<T> where T : ModelBase
    {
        protected readonly INhDmTiGiaService _nhDmTiGiaService;
        protected readonly INhDmTiGiaChiTietService _nhDmTiGiaChiTietService;

        private ObservableCollection<NhDmTiGiaModel> _itemsTiGia;
        public ObservableCollection<NhDmTiGiaModel> ItemsTiGia
        {
            get => _itemsTiGia;
            set => SetProperty(ref _itemsTiGia, value);
        }

        private NhDmTiGiaModel _selectedTiGia;
        public NhDmTiGiaModel SelectedTiGia
        {
            get => _selectedTiGia;
            set
            {
                if (SetProperty(ref _selectedTiGia, value))
                {
                    LoadTiGiaChiTiet();
                }
            }
        }

        private ObservableCollection<NhDmTiGiaChiTietModel> _itemsTiGiaChiTiet;
        public ObservableCollection<NhDmTiGiaChiTietModel> ItemsTiGiaChiTiet
        {
            get => _itemsTiGiaChiTiet;
            set => SetProperty(ref _itemsTiGiaChiTiet, value);
        }

        private NhDmTiGiaChiTietModel _selectedTiGiaChiTiet;
        public NhDmTiGiaChiTietModel SelectedTiGiaChiTiet
        {
            get => _selectedTiGiaChiTiet;
            set => SetProperty(ref _selectedTiGiaChiTiet, value);
        }

        public DialogCurrencyAttachmentViewModelBase(
            IMapper mapper,
            INhDmTiGiaService nhDmTiGiaService,
            INhDmTiGiaChiTietService nhDmTiGiaChiTietService,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService) : base(mapper, storageServiceFactory, attachService)
        {
            _nhDmTiGiaService = nhDmTiGiaService;
            _nhDmTiGiaChiTietService = nhDmTiGiaChiTietService;
        }

        public virtual void LoadTiGia()
        {
            var data = _nhDmTiGiaService.FindAll();
            ItemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            ItemsTiGia = _mapper.Map<ObservableCollection<NhDmTiGiaModel>>(data);
        }

        public void LoadTiGiaChiTiet()
        {
            _itemsTiGiaChiTiet = new ObservableCollection<NhDmTiGiaChiTietModel>();
            if (SelectedTiGia != null)
            {
                var data = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id).Where(x => x.SMaTienTeQuyDoi.ToUpper() != "VND" && x.SMaTienTeQuyDoi.ToUpper() != "EUR");
                _itemsTiGiaChiTiet = _mapper.Map<ObservableCollection<NhDmTiGiaChiTietModel>>(data);
            }
            OnPropertyChanged(nameof(ItemsTiGiaChiTiet));
        }

        public override void OnCellEditEnding(object obj)
        {
            if (obj is DataGridCellEditEndingEventArgs e)
            {
                // Tính toán chuyển đổi tiền tệ
                if (e.EditAction == DataGridEditAction.Commit && e.Row.Item is CurrencyDetailModelBase item)
                {
                    string propertyName = e.Column.SortMemberPath;
                    if (propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriUsd))
                        || propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriEur))
                        || propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriVnd))
                        || propertyName.Equals(nameof(CurrencyDetailModelBase.FGiaTriNgoaiTeKhac)))
                    {
                        if (SelectedTiGia != null)
                        {
                            var listTiGiaChiTiet = _nhDmTiGiaChiTietService.FindByTiGiaId(SelectedTiGia.Id);
                            string rootCurrency = SelectedTiGia.SMaTienTeGoc;
                            string sourceCurrency;
                            string otherCurrency = SelectedTiGiaChiTiet != null ? SelectedTiGiaChiTiet.SMaTienTeQuyDoi : "";
                            double value = 0;
                            switch (propertyName)
                            {
                                case nameof(CurrencyDetailModelBase.FGiaTriEur):
                                    sourceCurrency = LoaiTienTeEnum.TypeCode.EUR;
                                    value = item.FGiaTriEur.HasValue ? item.FGiaTriEur.Value : 0;
                                    break;
                                case nameof(CurrencyDetailModelBase.FGiaTriVnd):
                                    sourceCurrency = LoaiTienTeEnum.TypeCode.VND;
                                    value = item.FGiaTriVnd.HasValue ? item.FGiaTriVnd.Value : 0;
                                    break;
                                case nameof(CurrencyDetailModelBase.FGiaTriNgoaiTeKhac):
                                    sourceCurrency = otherCurrency;
                                    value = item.FGiaTriNgoaiTeKhac.HasValue ? item.FGiaTriNgoaiTeKhac.Value : 0;
                                    break;
                                default:
                                    sourceCurrency = LoaiTienTeEnum.TypeCode.USD;
                                    value = item.FGiaTriUsd.HasValue ? item.FGiaTriUsd.Value : 0;
                                    break;
                            }
                            item.FGiaTriVnd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.VND, rootCurrency, listTiGiaChiTiet, value);
                            item.FGiaTriEur = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.EUR, rootCurrency, listTiGiaChiTiet, value);
                            item.FGiaTriUsd = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, LoaiTienTeEnum.TypeCode.USD, rootCurrency, listTiGiaChiTiet, value);
                            item.FGiaTriNgoaiTeKhac = _nhDmTiGiaService.CurrencyExchange(sourceCurrency, otherCurrency, rootCurrency, listTiGiaChiTiet, value);
                        }
                    }
                }
            }
        }
    }
}
