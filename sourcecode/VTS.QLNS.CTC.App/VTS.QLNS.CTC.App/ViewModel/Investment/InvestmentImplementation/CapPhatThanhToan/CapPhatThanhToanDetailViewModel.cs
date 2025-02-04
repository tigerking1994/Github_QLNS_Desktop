using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.InvestmentImplementation.CapPhatThanhToan
{
    public class CapPhatThanhToanDetailViewModel : DetailViewModelBase<VdtTtDeNghiThanhToanModel, VdtTtDeNghiThanhToanChiTietModel>
    {
        private readonly IVdtTtDeNghiThanhToanService _deNghiThanhToanService;
        private readonly IVdtTtDeNghiThanhToanChiTietService _deNghiThanhToanChiTietService;
        private readonly ISessionService _sessionService;
        private readonly IVdtDaTtHopDongService _ttHopDongService;
        private readonly ITongHopNguonNSDauTuService _tonghopService;
        private bool _isUpdate;
        private IMapper _mapper;

        public override string Name => "Đề nghị cấp phát thanh toán chi tiết";

        private string _Description;
        public override string Description
        {
            get => _Description;
            set
            {
                SetProperty(ref _Description, value);
            }
        }

        #region data combobox
        private ObservableCollection<ComboboxItem> _cbxHopDong;
        public ObservableCollection<ComboboxItem> CbxHopDong
        {
            get => _cbxHopDong;
            set => SetProperty(ref _cbxHopDong, value);
        }
        #endregion

        public RelayCommand SaveDataCommand { get; }

        public CapPhatThanhToanDetailViewModel(
            IVdtTtDeNghiThanhToanService deNghiThanhToanService,
            IVdtTtDeNghiThanhToanChiTietService deNghiThanhToanChiTietService,
            IVdtDaTtHopDongService ttHopDongService,
            ITongHopNguonNSDauTuService tonghopService,
            ISessionService sessionService,
            IMapper mapper)
        {
            _deNghiThanhToanService = deNghiThanhToanService;
            _deNghiThanhToanChiTietService = deNghiThanhToanChiTietService;
            _ttHopDongService = ttHopDongService;
            _tonghopService = tonghopService;
            _sessionService = sessionService;
            _mapper = mapper;
            SaveDataCommand = new RelayCommand(obj => OnSaveData());
        }

        public override void Init()
        {
            MarginRequirement = new System.Windows.Thickness(10);
            LoadData();
        }

        #region RelayCommand
        public override void LoadData(params object[] args)
        {
            //Description = "Cấp phát cấp thanh toán trong chỉ tiêu chi tiết";
            //var data = _deNghiThanhToanService.GetDetailDuAnByDeNghiThanhToan(
            //    Model.iID_MaDonViQuanLy, Model.iID_NguonVonID, Model.iID_LoaiNguonVonID, Model.dNgayDeNghi.Value, Model.iNamKeHoach);
            
            //List<VdtTtDeNghiThanhToanChiTietQuery> lstDataUpdate = _deNghiThanhToanChiTietService.GetDuAnByIdThanhToan(Model.Id).ToList();
            //if (lstDataUpdate != null && lstDataUpdate.Count != 0)
            //{
            //    var lstDataExist = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiTietModel>>(lstDataUpdate);
               
            //    _isUpdate = true;
            //    Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiTietModel>>(lstDataExist);
            //}
            //else
            //{
            //    _isUpdate = false;
            //    Items = _mapper.Map<ObservableCollection<VdtTtDeNghiThanhToanChiTietModel>>(data);
            //}
            //List<VdtDaTtHopDong> lstHopDong = _ttHopDongService.FindByListDuAnId(Items.Select(n => n.iID_DuAnID).Distinct().ToList());
            //foreach (var item in Items)
            //{
            //    item.PropertyChanged += DetailModel_PropertyChanged;
            //    List<ComboboxItem> lstCbxDataHopDong = lstHopDong.Where(n => n.IIdDuAnId == item.iID_DuAnID).Select(n => new ComboboxItem { ValueItem = n.Id.ToString(), DisplayItem = n.SSoHopDong }).ToList();
            //    lstCbxDataHopDong.Insert(0, new ComboboxItem());
            //    item.CbxHopDong = new ObservableCollection<ComboboxItem>(lstCbxDataHopDong);
            //    if (item.iID_HopDongID.HasValue)
            //    {
            //        var objHopDongData = _deNghiThanhToanChiTietService.GetHopDongInfo(item.iID_HopDongID.Value, Model.dNgayDeNghi.Value, Model.iID_NguonVonID);
            //        StringBuilder sHopDong = new StringBuilder();
            //        sHopDong.AppendFormat("- Ngày hợp đồng : {0}\n", objHopDongData.dNgayHopDong.HasValue ? objHopDongData.dNgayHopDong.Value.ToString("dd/MM/yyyy") : string.Empty);
            //        sHopDong.AppendFormat("- Giá trị hợp đồng : {0}\n", objHopDongData.fGiaTriHopDong.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
            //        StringBuilder sGoiThau = new StringBuilder();
            //        sGoiThau.AppendFormat("- Tên gói thầu : {0}\n", objHopDongData.sTenGoiThau);
            //        sGoiThau.AppendFormat("- Tên nhà thầu : {0}\n", objHopDongData.sTenNhaThau);
            //        sGoiThau.AppendFormat("- Dự toán, giá gói thầu được duyệt : {0}\n", (objHopDongData.fGiaGoiThau ?? 0).ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
            //        sGoiThau.AppendFormat("- Số tài khoản : {0}\n", objHopDongData.sSoTaiKhoan);
            //        sGoiThau.AppendFormat("- Ngân hàng : {0}\n", objHopDongData.sNganHang);
            //        item.sThongTinHopDong = sHopDong.ToString();
            //        item.sThongTinNhaThau = sGoiThau.ToString();
            //        item.iId_GoiThau = objHopDongData.iId_GoiThau;
            //    }
            //}
            //OnPropertyChanged(nameof(Items));
        }

        protected override void OnRefresh()
        {
            base.OnRefresh();
            LoadData();
        }

        protected override void OnAdd()
        {
            if (SelectedItem != null)
            {
                int currentRow = Items.IndexOf(SelectedItem);
                VdtTtDeNghiThanhToanChiTietModel newItem = ObjectCopier.Clone(SelectedItem);
                newItem.iID_HopDongID = null;
                newItem.sThongTinHopDong = string.Empty;
                newItem.sThongTinNhaThau = string.Empty;
                newItem.fGiaTriThanhToanTN = 0;
                newItem.fGiaTriThanhToanNN = 0;
                newItem.fGiaTriThuHoiNamTruocTN = 0;
                newItem.fGiaTriThuHoiNamTruocNN = 0;
                newItem.fGiaTriThuHoiNamNayTN = 0;
                newItem.fGiaTriThuHoiNamNayNN = 0;
                newItem.sGhiChu = string.Empty;
                Items.Insert(currentRow + 1, newItem);
                SelectedItem = newItem;
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        protected override void OnDelete()
        {
            if (SelectedItem != null)
            {
                SelectedItem.IsDeleted = !SelectedItem.IsDeleted;
            }
            OnPropertyChanged(nameof(Items));
        }

        public void OnSaveData()
        {
            List<string> messageBuilder = new List<string>();
            List<VdtTtDeNghiThanhToanChiTietModel> lstDataNew = Items.Where(n => (n.fGiaTriThanhToanTN != 0 || n.fGiaTriThanhToanNN != 0) && !n.IsDeleted).ToList();
            if (lstDataNew == null || lstDataNew.Count == 0)
            {
                messageBuilder.Add(Resources.MsgErrorDataEmpty);
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            var lstDuplicate = lstDataNew.GroupBy(n => new { n.iID_DuAnID, n.sTenDuAn, n.sXauNoiMa, n.iID_HopDongID }).Where(n => n.Count() > 1).Select(n => n.Key);
            foreach (var item in lstDuplicate)
            {
                messageBuilder.Add(string.Format(Resources.MsgErrorThanhToanDuplicate, item.sTenDuAn + ", "
                    + item.sXauNoiMa + (item.iID_HopDongID.HasValue ? " và hợp đồng " : string.Empty)));
            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()));
                return;
            }
            List<VdtTtDeNghiThanhToanChiTiet> lstData = new List<VdtTtDeNghiThanhToanChiTiet>();
            foreach (var item in lstDataNew)
            {
                lstData.Add(ConvertDataInsert(item));
            }
            if (messageBuilder.Count != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }
            bool isSucess = _deNghiThanhToanChiTietService.Insert(Model.Id, lstData);
            if (!isSucess)
            {
                messageBuilder.Add(Resources.AlertDataError);
                MessageBox.Show(String.Join("\n", messageBuilder));
                return;
            }
            messageBuilder.Add(Resources.MsgSaveDone);
            MessageBox.Show(String.Join("\n", messageBuilder));
            LoadData();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            VdtTtDeNghiThanhToanChiTietModel item = (VdtTtDeNghiThanhToanChiTietModel)sender;
            switch (args.PropertyName)
            {
                case nameof(VdtTtDeNghiThanhToanChiTietModel.iID_HopDongID):
                    if (!item.iID_HopDongID.HasValue)
                    {
                        item.sThongTinHopDong = string.Empty;
                        item.sThongTinNhaThau = string.Empty;
                        item.iId_GoiThau = null;
                        return;
                    }
                    var objHopDongData = _deNghiThanhToanChiTietService.GetHopDongInfo(item.iID_HopDongID.Value, Model.dNgayDeNghi.Value, Model.iID_NguonVonID);
                    StringBuilder sHopDong = new StringBuilder();
                    sHopDong.AppendFormat("- Ngày hợp đồng : {0}\n", objHopDongData.dNgayHopDong.HasValue ? objHopDongData.dNgayHopDong.Value.ToString("dd/MM/yyyy") : string.Empty);
                    sHopDong.AppendFormat("- Giá trị hợp đồng : {0}\n", objHopDongData.fGiaTriHopDong.ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    StringBuilder sGoiThau = new StringBuilder();
                    sGoiThau.AppendFormat("- Tên gói thầu : {0}\n", objHopDongData.sTenGoiThau);
                    sGoiThau.AppendFormat("- Tên nhà thầu : {0}\n", objHopDongData.sTenNhaThau);
                    sGoiThau.AppendFormat("- Dự toán, giá gói thầu được duyệt : {0}\n", (objHopDongData.fGiaGoiThau ?? 0).ToString("##,#", CultureInfo.GetCultureInfo("vi-VN")));
                    sGoiThau.AppendFormat("- Số tài khoản : {0}\n", objHopDongData.sSoTaiKhoan);
                    sGoiThau.AppendFormat("- Ngân hàng : {0}\n", objHopDongData.sNganHang);
                    item.sThongTinHopDong = sHopDong.ToString();
                    item.sThongTinNhaThau = sGoiThau.ToString();
                    item.iId_GoiThau = objHopDongData.iId_GoiThau;
                    break;
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThanhToanTN):
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThanhToanNN):
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThuHoiNamTruocTN):
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThuHoiNamTruocNN):
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThuHoiNamNayTN):
                case nameof(VdtTtDeNghiThanhToanChiTietModel.fGiaTriThuHoiNamNayNN):
                    item.fSoThucThanhToanDotNay = (item.fGiaTriThanhToanTN + item.fGiaTriThanhToanNN)
                        - (item.fGiaTriThuHoiNamTruocTN + item.fGiaTriThuHoiNamTruocNN
                        + item.fGiaTriThuHoiNamNayTN + item.fGiaTriThuHoiNamNayNN);
                    break;
            }
        }
        #endregion

        #region Helper
        private VdtTtDeNghiThanhToanChiTiet ConvertDataInsert(VdtTtDeNghiThanhToanChiTietModel data)
        {
            VdtTtDeNghiThanhToanChiTiet dataInsert = new VdtTtDeNghiThanhToanChiTiet();
            dataInsert.Id = Guid.NewGuid();
            dataInsert.IIdDeNghiThanhToanId = Model.Id;
            dataInsert.IIdMucId = data.iID_MucID;
            dataInsert.IIdTieuMucId = data.iID_TieuMucID;
            dataInsert.IIdTietMucId = data.iID_TietMucID;
            dataInsert.IIdNganhId = data.iID_NganhID;
            dataInsert.IIdDuAnId = data.iID_DuAnID;
            dataInsert.IIdHopDongId = data.iID_HopDongID;
            dataInsert.IIdNhaThauId = data.iID_NhaThauID;
            dataInsert.FGiaTriThanhToanTN = data.fGiaTriThanhToanTN;
            dataInsert.FGiaTriThanhToanNN = data.fGiaTriThanhToanNN;
            dataInsert.FGiaTriThuHoiNamTruocTN = data.fGiaTriThuHoiNamTruocTN;
            dataInsert.FGiaTriThuHoiNamTruocNN = data.fGiaTriThuHoiNamTruocNN;
            dataInsert.FGiaTriThuHoiNamNayTN = data.fGiaTriThuHoiNamNayTN;
            dataInsert.FGiaTriThuHoiNamNayNN = data.fGiaTriThuHoiNamNayNN;
            dataInsert.SGhiChu = data.sGhiChu;
            dataInsert.M = data.M;
            dataInsert.Tm = data.TM;
            dataInsert.Ttm = data.TTM;
            dataInsert.Ng = data.NG;
            return dataInsert;
        }
        #endregion
    }
}
