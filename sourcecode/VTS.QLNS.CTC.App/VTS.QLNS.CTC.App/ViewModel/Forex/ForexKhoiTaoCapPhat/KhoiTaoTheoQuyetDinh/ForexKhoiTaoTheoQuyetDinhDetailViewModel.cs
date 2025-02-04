using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Forex.ForexKhoiTaoCapPhat.ForexDanhSachKhoiTao;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows.Controls;
using VTS.QLNS.CTC.App.View.Shared;
using System.Windows;
using VTS.QLNS.CTC.App.View.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexKhoiTaoCapPhat.KhoiTaoTheoQuyetDinh
{
    public class ForexKhoiTaoTheoQuyetDinhDetailViewModel : DetailViewModelBase<NhKtKhoiTaoCapPhatModel, NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>
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

        public override string Name => "Khởi tạo theo quyết định";
        public override string Title => "Chi tiết khởi tạo theo quyết định";
        public override string Description => "Chi tiết khởi tạo theo quyết định";
        public override Type ContentType => typeof(ForexKhoiTaoTheoQuyetDinhDetail);
        public bool IsDetail { get; set; }
        public bool IsEditable => Model == null || Model.Id.IsNullOrEmpty();
        public RelayCommand OpenReferencePopupCommand { get; }

        public ForexKhoiTaoTheoQuyetDinhDetailViewModel(
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
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            Items = new ObservableCollection<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>();
            var data = _service.FindDetailById(Model.Id);
            Items = _mapper.Map<ObservableCollection<NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel>>(data);

            foreach (var item in Items)
            {
                if (item.ILoaiNoiDung == NHConstants.ILOAI_CHUONG_TRINH)
                {
                    item.BHangCha = true;
                }
                else if (item.ILoaiNoiDung != NHConstants.ZERO)
                {
                    item.BHangCha = Items.Any(y => y.IIdParentID == item.Id);
                }
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
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
                        System.Windows.Forms.DialogResult dialog = System.Windows.Forms.MessageBox.Show(Resources.MsgSaveDone);
                        if (dialog == System.Windows.Forms.DialogResult.OK)
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
            if (SelectedItem != null)
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
            NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel item = (NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel)sender;
            if (Model.IIdTiGiaID != null)
            {
                Guid idTyGia = (Guid)Model.IIdTiGiaID;
                var dmTyGia = _iNhDmTiGiaService.FindById(idTyGia);
                var listTyGiaCT = _iNhDmTiGiaChiTietService.FindByTiGiaId(idTyGia);
                var rootCurrency = dmTyGia.SMaTienTeGoc;
                double value = 0;
                //switch (e.PropertyName)
                //{
                //    case nameof(NhKtKhoiTaoTheoQuyetDinhKhacChiTietImportModel.FDeNghiQTNamNayUSD):
                //        value = Convert.ToDouble(item.FDeNghiQTNamNayUSD.GetValueOrDefault());
                //        item.FDeNghiQTNamNayVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                //        break;
                //    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FLuyKeKinhPhiDuocCapUSD):
                //        value = Convert.ToDouble(item.FLuyKeKinhPhiDuocCapUSD.GetValueOrDefault());
                //        item.FLuyKeKinhPhiDuocCapVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                //        break;
                //    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FQTKinhPhiDuyetCacNamTruocUSD):
                //        value = Convert.ToDouble(item.FQTKinhPhiDuyetCacNamTruocUSD.GetValueOrDefault());
                //        item.FQTKinhPhiDuyetCacNamTruocVND = _iNhDmTiGiaService.CurrencyExchange("USD", "VND", rootCurrency, listTyGiaCT, value);
                //        break;
                //    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FDeNghiQTNamNayVND):
                //        value = Convert.ToDouble(item.FDeNghiQTNamNayVND.GetValueOrDefault());
                //        item.FDeNghiQTNamNayUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                //        break;
                //    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FLuyKeKinhPhiDuocCapVND):
                //        value = Convert.ToDouble(item.FLuyKeKinhPhiDuocCapVND.GetValueOrDefault());
                //        item.FLuyKeKinhPhiDuocCapUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                //        break;
                //    case nameof(NhKtKhoiTaoCapPhatChiTietModel.FQTKinhPhiDuyetCacNamTruocVND):
                //        value = Convert.ToDouble(item.FQTKinhPhiDuyetCacNamTruocVND.GetValueOrDefault());
                //        item.FQTKinhPhiDuyetCacNamTruocUSD = _iNhDmTiGiaService.CurrencyExchange("VND", "USD", rootCurrency, listTyGiaCT, value);
                //        break;
                //    default:
                //        break;
                //}
            }
        }
    }
}
