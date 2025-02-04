using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using System.Windows;
using System.ComponentModel;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.Control;
using System.IO;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Expertise
{
    public class SendDataExpertiseViewModel : GridViewModelBase<ExpertiseModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly INsDonViService _donViService;
        private IDanhMucService _danhMucService;
        private IExportService _exportService;
        private readonly ISktMucLucService _SktMucLucService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        private ICryptographyService _cryptographyService;
        private readonly ISktNganhThamDinhService _sktThamDinhService;
        private readonly ISktNganhThamDinhChiTietService _sktThamDinhChiTietService;

        public RelayCommand ExportCommand { get; }
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        
        public List<UserAPIModel> _listChildAgency;
        public List<UserAPIModel> ListChildAgency
        {
            get => _listChildAgency;
            set => SetProperty(ref _listChildAgency, value);
        }

        private bool _selectAllAgency;
        public bool SelectAllAgency
        {
            get => (ListChildAgency == null || !ListChildAgency.Any()) ? false : ListChildAgency.All(item => item.IsChecked);
            set
            {
                SetProperty(ref _selectAllAgency, value);
                if (ListChildAgency != null)
                {
                    ListChildAgency.Select(c => { c.IsChecked = _selectAllAgency; return c; }).ToList();
                }
            }
        }

        public SendDataExpertiseViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              IExportService exportService,
                                              IDanhMucService danhMucService,
                                              ISktMucLucService SktMucLucService,                                             
                                              IHTTPUploadFileService hTTPUploadFileService,
                                              ICryptographyService cryptographyService,
                                              ISktNganhThamDinhChiTietService sktThamDinhChiTietService,
                                              ISktNganhThamDinhService sktThamDinhService,
                                              INsDonViService donViService
                                              )
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _sktThamDinhService = sktThamDinhService;
            _sktThamDinhChiTietService = sktThamDinhChiTietService;
            _exportService = exportService;
            _SktMucLucService = SktMucLucService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _hTTPUploadFileService = hTTPUploadFileService;
            _cryptographyService = cryptographyService;
            ExportCommand = new RelayCommand(obj => OnUpload());
        }

        public override void Init()
        {
            base.Init();
            _sessionInfo = _sessionService.Current;
            LoadTenBaoCao();
            LoadData();
        }

        public void LoadData()
        {
            foreach(var item in ListChildAgency)
            {
                item.PropertyChanged += DetailModel_PropertyChanged;
            }
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            try
            {
                UserAPIModel item = (UserAPIModel)sender;
                switch (args.PropertyName)
                {
                    case nameof(UserAPIModel.IsChecked):
                        if (ListChildAgency.Count(n => n.IsChecked) == ListChildAgency.Count)
                        {
                            SelectAllAgency = true;
                        }
                        else if (ListChildAgency.Count(n => !n.IsChecked) == ListChildAgency.Count)
                        {
                            SelectAllAgency = false;
                        }
                        break;
                }
                OnPropertyChanged(nameof(SelectAllAgency));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void LoadTenBaoCao()
        {
            var itemImport = Items.FirstOrDefault(x => x.Selected);
            _tenBaoCao = StringUtils.ConvertVN(string.Format("NganhThamDinh_{0}", itemImport.SoChungTu));
        }

        private async void OnUpload()
        {
            try
            {
                if (!ListChildAgency.Any(n => n.IsChecked) || ListChildAgency.Where(n => n.IsChecked).Count() == 0)
                {
                    StringBuilder messageBuilder = new StringBuilder();
                    messageBuilder.AppendFormat("Vui lòng chọn ít nhất 1 đơn vị để phân bổ!");
                    MessageBox.Show(messageBuilder.ToString());
                    return;
                }

                IsLoading = true;
                var token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);

                string templateFileName;
                

                string fileNamePrefix;
                string fileNameWithoutExtension;

                int yearOfWork = _sessionService.Current.YearOfWork;
                var predicate = PredicateBuilder.True<NsSktMucLuc>();
                predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                var sktMucLucsOrder = from sktMucLuc in sktMucLucs orderby sktMucLuc.SKyHieu select sktMucLuc;
                var listAgencyUpload = new List<FileUploadStreamModel>();
                List<Guid> listIdMaDonViSend = new List<Guid>();

                foreach (ExpertiseModel item in Items.Where(n => n.Selected))
                {
                    if (!ListChildAgency.Where(x => x.IsChecked).Select(x => x.Code).Contains(item.IdDonVi))
                    {
                        break;
                    }

                    if (item.ILoai == LoaiNganhThamDinh.CTNTD)
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NTD);
                        if (item.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)))
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD_NTD);
                        }
                    }
                    else
                    {
                        templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP);
                        if (item.ILoaiChungTu.Equals(int.Parse(VoucherType.NSBD_Key)))
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_NTD, ExportFileName.RPT_NS_NGANHTHAMDINH_CHUNGTU_TONGHOP_NSBD);
                        }
                    }

                    List<ExpertiseModelDetailModel> listDetail = new List<ExpertiseModelDetailModel>();
                    if (item.ILoaiChungTu.Equals(int.Parse(VoucherType.NSSD_Key)))
                    {
                        listDetail = GetDataDetail(item);
                    }
                    else
                    {
                        listDetail = GetDataDetailNSBD(item);
                    }
                    if (item.ILoai.HasValue && item.ILoai.Value == LoaiNganhThamDinh.CTCTCDN)
                    {
                        if (listDetail != null && listDetail.Count > 0)
                        {
                            List<string> listNganh = listDetail.Where(n => n.TuChi != 0 && !string.IsNullOrEmpty(n.Nganh)).Select(n => n.Nganh).Distinct().ToList();
                            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
                            List<string> listDonVi = listDanhMuc.Where(n => listNganh.Contains(n.IIDMaDanhMuc)).Select(n => n.SGiaTri).Distinct().ToList();
                            foreach (string donViChiTiet in listDonVi)
                            {
                                List<string> listNganhChiTiet = listDanhMuc.Where(n => n.SGiaTri == donViChiTiet).Select(n => n.IIDMaDanhMuc).Distinct().ToList();
                                List<ExpertiseModelDetailModel> listDonViData = listDetail.Where(n => listNganhChiTiet.Contains(n.Nganh) || n.IsHangCha).ToList();
                                FormatDataExport(ref listDonViData);
                                Dictionary<string, object> data = new Dictionary<string, object>();
                                data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                                data.Add("Cap2", GetHeader2Report());
                                data.Add("TenDonVi", item.TenDonVi);
                                data.Add("ListData", listDonViData);
                                data.Add("SKTML", sktMucLucsOrder);

                                fileNamePrefix = item.SoChungTu;
                                fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix) + "_" + donViChiTiet;
                                var xlsFile = _exportService.Export<ExpertiseModelDetailModel, NsSktMucLuc>(templateFileName, data);

                                FileUploadStreamModel childData = new FileUploadStreamModel();
                                var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                                var fileStream = new MemoryStream();
                                var outputFileStream = new MemoryStream();
                                _exportService.Open(Result, ref fileStream);

                                await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);

                                childData.File = outputFileStream;
                                childData.Name = fileNameWithoutExtension + FileExtensionFormats.Security;
                                listAgencyUpload.Add(childData);
                                listIdMaDonViSend.Add(ListChildAgency.Where(x => x.Code == item.IdDonVi).FirstOrDefault().Id);
                            }
                        }
                    }
                    else if (item.ILoai.HasValue && item.ILoai.Value == LoaiNganhThamDinh.CTNTD)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("Cap1", _danhMucService.FindDonViQuanLy(_sessionService.Current.YearOfWork).ToUpper());
                        data.Add("Cap2", GetHeader2Report());
                        data.Add("TenDonVi", item.TenDonVi);
                        data.Add("ListData", listDetail);
                        data.Add("SKTML", sktMucLucsOrder);

                        fileNamePrefix = item.SoChungTu;
                        fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix) + "_" + item.IdDonVi;
                        var xlsFile = _exportService.Export<ExpertiseModelDetailModel, NsSktMucLuc>(templateFileName, data);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));

                        FileUploadStreamModel childData = new FileUploadStreamModel();
                        var Result = new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile);
                        var fileStream = new MemoryStream();
                        var outputFileStream = new MemoryStream();
                        _exportService.Open(Result, ref fileStream);

                        await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);

                        childData.File = outputFileStream;
                        childData.Name = fileNameWithoutExtension + FileExtensionFormats.Security;
                        listAgencyUpload.Add(childData);
                        listIdMaDonViSend.Add(ListChildAgency.Where(x => x.Code == item.IdDonVi).FirstOrDefault().Id);
                    }
                }

                bool status = await _hTTPUploadFileService.UploadFileAsync(IsSendHTTP, new FileUploadStreamModel()
                {
                    Description = "Ngành thẩm định",
                    Module = NSFunctionCode.BUDGET,
                    ModuleName = "Ngành thẩm định",
                    SubModule = NSFunctionCode.BUDGET_EXPERTISE,
                    SubModuleName = "Ngành thẩm định",
                    YearOfWork = _sessionInfo.YearOfWork,
                    YearOfBudget = _sessionInfo.YearOfBudget,
                    SourceOfBudget = _sessionInfo.Budget,
                    Department = "",
                    Quarter = "",
                    IdChild = string.Join(",", listIdMaDonViSend),
                    listAgencyUpload = listAgencyUpload
                });
                if (!status)
                {
                    IsLoading = false;
                    MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                    return;
                }

                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thành công").ToString());
                return;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                MessageBox.Show(new StringBuilder("Gửi dữ liệu thất bại").ToString());
                _logger.Error(ex.Message, ex);
                return;
            }
        }

        public string GetHeader2Report()
        {
            DonVi donViParent = _donViService.FindByLoai(LoaiDonVi.ROOT, _sessionService.Current.YearOfWork);
            return donViParent != null ? donViParent.TenDonVi.ToUpper() : string.Empty;
        }

        private List<ExpertiseModelDetailModel> GetDataDetail(ExpertiseModel chungTu)
        {
            List<ExpertiseModelDetailModel> resultDetail = new List<ExpertiseModelDetailModel>();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            if (chungTu == null || chungTu.Id == Guid.Empty || listDanhMuc == null || listDanhMuc.Count == 0)
                return resultDetail;
            List<ThDChungTuChiTietQuery> data = _sktThamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();

            List<string> listKyHieu = new List<string>();
            foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
            {
                listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
            }
            data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            resultDetail = _mapper.Map<List<Model.ExpertiseModelDetailModel>>(data);

            if (chungTu.ILoai == LoaiNganhThamDinh.CTNTD)
            {
                List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev(chungTu);
                if (dataDeNghi != null && dataDeNghi.Count > 0)
                {
                    foreach (ExpertiseModelDetailModel item in resultDetail)
                    {
                        ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                        if (valueItem != null)
                        {
                            item.TuChiPrev = valueItem.TuChi;
                            item.SuDungTonKhoPrev = valueItem.SuDungTonKho;
                            item.ChiDacThuNganhPhanCapPrev = valueItem.ChiDacThuNganhPhanCap;
                        }
                    }
                }
            }

            FormatDataExport(ref resultDetail);
            return resultDetail;
        }

        public List<ThDChungTuChiTietQuery> GetValueTuChiPrev(ExpertiseModel chungTu)
        {
            var predicate = PredicateBuilder.True<NsSktNganhThamDinh>();
            predicate = predicate.And(x => x.INamLamViec == _sessionService.Current.YearOfWork);
            predicate = predicate.And(x => x.ILoai == VTS.QLNS.CTC.Utility.LoaiNganhThamDinh.CTCTCDN);

            if (chungTu.ILoaiChungTu.HasValue && chungTu.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key);
            }
            else
            {
                predicate = predicate.And(x => x.ILoaiChungTu.Value.ToString() == VoucherType.NSSD_Key);
            }

            NsSktNganhThamDinh chungTuDeNghi = _sktThamDinhService.FindByCondition(predicate).FirstOrDefault();
            if (chungTuDeNghi == null)
            {
                return new List<ThDChungTuChiTietQuery>();
            }
            else
            {
                List<ThDChungTuChiTietQuery> data = new List<ThDChungTuChiTietQuery>();
                if (chungTu.ILoaiChungTu.HasValue && chungTu.ILoaiChungTu.Value.ToString() == VoucherType.NSBD_Key)
                {
                    data = _sktThamDinhChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                        _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                else
                {
                    data = _sktThamDinhChiTietService.FindByCondition(_sessionService.Current.YearOfWork, chungTuDeNghi.Id.ToString(),
                        _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();
                }
                return data.Where(n => n.TuChi != 0 || n.ChiDacThuNganhPhanCap != 0 || n.SuDungTonKho != 0).ToList();
            }
        }

        private List<ExpertiseModelDetailModel> GetDataDetailNSBD(ExpertiseModel chungTu)
        {
            List<ExpertiseModelDetailModel> resultDetail = new List<ExpertiseModelDetailModel>();
            List<DanhMuc> listDanhMuc = _danhMucService.FindByType("NS_Nganh", _sessionService.Current.YearOfWork).Where(n => !string.IsNullOrEmpty(n.SGiaTri)).ToList();
            if (chungTu == null || chungTu.Id == Guid.Empty || listDanhMuc == null || listDanhMuc.Count == 0)
                return resultDetail;
            List<ThDChungTuChiTietQuery> data = _sktThamDinhChiTietService.FindByConditionNSBD(_sessionService.Current.YearOfWork, chungTu.Id.ToString(),
                _sessionService.Current.YearOfBudget, _sessionService.Current.Budget).ToList();

            List<string> listKyHieu = new List<string>();
            foreach (ThDChungTuChiTietQuery item in data.Where(n => !n.IsHangCha))
            {
                listKyHieu.AddRange(StringUtils.SplitKyHieuParent(item.KyHieu));
            }
            data = data.Where(n => listKyHieu.Contains(n.KyHieu)).ToList();
            resultDetail = _mapper.Map<List<Model.ExpertiseModelDetailModel>>(data);

            if (chungTu.ILoai == LoaiNganhThamDinh.CTNTD)
            {
                List<ThDChungTuChiTietQuery> dataDeNghi = GetValueTuChiPrev(chungTu);
                if (dataDeNghi != null && dataDeNghi.Count > 0)
                {
                    foreach (ExpertiseModelDetailModel item in resultDetail)
                    {
                        ThDChungTuChiTietQuery valueItem = dataDeNghi.Where(n => n.IdMucLuc == item.IdMucLuc && n.IdDonVi == item.IdDonVi).FirstOrDefault();
                        if (valueItem != null)
                        {
                            item.TuChiPrev = valueItem.TuChi;
                            item.SuDungTonKhoPrev = valueItem.SuDungTonKho;
                            item.ChiDacThuNganhPhanCapPrev = valueItem.ChiDacThuNganhPhanCap;
                        }
                    }
                }
            }

            FormatDataExport(ref resultDetail);
            return resultDetail;
        }

        private List<ExpertiseModelDetailModel> FormatDataExport(ref List<ExpertiseModelDetailModel> data)
        {
            CalculateData(ref data);
            data = data.Where(n => n.TuChiCTC != 0 || n.TuChiNganh != 0
                                                   || n.TuChi != 0 || n.SuDungTonKho != 0 || n.ChiDacThuNganhPhanCap != 0).ToList();
            return data;
        }

        private void CalculateData(ref List<ExpertiseModelDetailModel> listData)
        {
            listData.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.TuChi = 0;
                    x.SuDungTonKho = 0;
                    x.ChiDacThuNganhPhanCap = 0;
                    x.TuChiCTC = 0;
                    x.TuChiNganh = 0;
                    x.HuyDongCTC = 0;
                    x.HuyDongNganh = 0;
                    x.TuChiPrev = 0;
                    x.SuDungTonKhoPrev = 0;
                    x.ChiDacThuNganhPhanCapPrev = 0;
                    x.Tang = 0;
                    x.Giam = 0;
                    return x;
                }).ToList();
            foreach (var item in listData.Where(x => x.IsFilter && !x.IsHangCha && !x.IsDeleted && (x.TuChi != 0 || x.TuChiCTC != 0 || x.TuChiNganh != 0 || x.HuyDongCTC != 0 || x.HuyDongNganh != 0)))
            {
                CalculateParent(ref listData, item, item);
            }
        }

        private void CalculateParent(ref List<ExpertiseModelDetailModel> listData, ExpertiseModelDetailModel currentItem, ExpertiseModelDetailModel selfItem)
        {
            var parentItem = listData.Where(x => x.IdMucLuc == currentItem.IdParent).FirstOrDefault();
            if (parentItem == null) return;
            parentItem.TuChi += selfItem.TuChi;
            parentItem.SuDungTonKho += selfItem.SuDungTonKho;
            parentItem.ChiDacThuNganhPhanCap += selfItem.ChiDacThuNganhPhanCap;
            parentItem.TuChiCTC += selfItem.TuChiCTC;
            parentItem.TuChiNganh += selfItem.TuChiNganh;
            parentItem.HuyDongCTC += selfItem.HuyDongCTC;
            parentItem.HuyDongNganh += selfItem.HuyDongNganh;
            parentItem.TuChiPrev += selfItem.TuChiPrev;
            parentItem.SuDungTonKhoPrev += selfItem.SuDungTonKhoPrev;
            parentItem.ChiDacThuNganhPhanCapPrev += selfItem.ChiDacThuNganhPhanCapPrev;
            parentItem.Tang += selfItem.Tang;
            parentItem.Giam += selfItem.Giam;
            CalculateParent(ref listData, parentItem, selfItem);
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
