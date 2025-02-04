using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.AdjustedEstimate;
using System.IO;
using System.Windows;
using System.IO.Pipes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MaterialDesignThemes.Wpf;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.DemandCheck.Distribution
{
    public class SendDataDistributionViewModel : GridViewModelBase<NsSktChungTuModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private readonly ISktChungTuChiTietService _sktChungTuChiTietService;
        private readonly ISktChungTuService _sktChungTuService;
        private readonly INsDonViService _nsDonViService;
        private IExportService _exportService;
        private readonly ISktMucLucService _SktMucLucService;
        private readonly IDanhMucService _danhMucService;
        private IHTTPUploadFileService _hTTPUploadFileService;
        private ICryptographyService _cryptographyService;
        public RelayCommand ExportCommand { get; }
        public bool IsSendHTTP;
        public string TitleButton => IsSendHTTP ? "Gửi dữ liệu HTTP" : "Gửi dữ liệu FTP";

        private string _tenBaoCao;
        public string TenBaoCao
        {
            get => _tenBaoCao;
            set => SetProperty(ref _tenBaoCao, value);
        }

        public NsSktChungTuModel _selectedNsSktChungTuModel;
        public NsSktChungTuModel SelectedNsSktChungTuModel
        {
            get => _selectedNsSktChungTuModel;
            set => SetProperty(ref _selectedNsSktChungTuModel, value);
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

        public SendDataDistributionViewModel(ILog logger,
                                              IMapper mapper,
                                              ISessionService sessionService,
                                              INsDonViService nsDonViService,
                                              IExportService exportService,
                                              ISktMucLucService SktMucLucService,
                                              IDanhMucService danhMucService,
                                              IHTTPUploadFileService hTTPUploadFileService,
                                              ICryptographyService cryptographyService,
                                              ISktChungTuChiTietService sktChungTuChiTietService
                                              )
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _sktChungTuChiTietService = sktChungTuChiTietService;
            _nsDonViService = nsDonViService;
            _exportService = exportService;
            _SktMucLucService = SktMucLucService;
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
            var itemImport = Items.Where(x => x.Selected).FirstOrDefault();
            _tenBaoCao = StringUtils.ConvertVN(string.Format("SKT_{0}", itemImport.SSoChungTu));
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
                List<ExportResult> results = new List<ExportResult>();
                string templateFileName;
                string fileNamePrefix;
                string fileNameWithoutExtension;

                List<NsSktChungTuModel> sktChungTuModelsSummary = Items.Where(x => x.Selected).ToList();
                var token = await _hTTPUploadFileService.GetToken(IsSendHTTP);
                string salt = _cryptographyService.GetSalt();
                string tokenKey = Scramble(token.Item2 + salt);
                var listAgencyUpload = new List<FileUploadStreamModel>();
                List<Guid> listIdMaDonViSend = new List<Guid>();
                foreach (var item in sktChungTuModelsSummary)
                {
                    if (!ListChildAgency.Where(x => x.IsChecked).Select(x => x.Code).Contains(item.IIdMaDonVi))
                    {
                        break;
                    }
                    else
                    {
                        var yearOfWork = _sessionInfo.YearOfWork;
                        var currentIdDonVi = _sessionInfo.IdDonVi;
                        var currentDonVi = GetNsDonViOfCurrentUser();
                        List<NsSktChungTuChiTietModel> sktChungTuChiTietModels = GetDistributionVoucherDetail(item);
                        var predicate = PredicateBuilder.True<NsSktMucLuc>();
                        predicate = predicate.And(x => x.INamLamViec == yearOfWork);
                        predicate = predicate.And(x => x.ITrangThai == StatusType.ACTIVE);
                        List<NsSktMucLuc> sktMucLucs = _SktMucLucService.FindByCondition(predicate).ToList();
                        var sktMucLucsOrder = from sktMucLuc in sktMucLucs
                                              orderby sktMucLuc.SKyHieu
                                              select sktMucLuc;
                        foreach (var ct in sktChungTuChiTietModels)
                        {
                            var ml = sktMucLucsOrder.FirstOrDefault(x => x.IIDMLSKT.Equals(ct.IIdMlskt));
                            if (ml != null)
                            {
                                ct.Nganh = ml.SNg;
                                ct.NganhParent = ml.SNGCha;
                                ct.Stt = ml.SSTT;
                            }
                        }
                        //NSSD
                        double SumTotalHuyDong = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FHuyDongTonKho);
                        double SumTotalTuChi = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FTuChi);
                        double SumTotalTongCongNSSD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongHuyDongTuChi);
                        //NSBD
                        double SumTotalMHHV = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FMuaHangCapHienVat);
                        double SumTotalDT = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.FPhanCap);
                        double SumTotalTongCongNSBD = sktChungTuChiTietModels.Where(item => item.IdParent == Guid.Empty).Sum(x => x.TongMuaHangHienVatDacThu);

                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("TenDonVi", item.STenDonVi);
                        data.Add("IdDonVi", item.IIdMaDonVi);
                        data.Add("Cap1", currentDonVi.TenDonVi);
                        data.Add("TieuDe1", "THÔNG BÁO SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM " + _sessionService.Current.YearOfWork);
                        data.Add("DonViPB", item.STenDonVi);
                        data.Add("h2", "Lữ đoàn X");
                        data.Add("h1", "Lữ đoàn X");
                        data.Add("LoaiChungTu", item.ILoaiChungTu == 1 ? VoucherType.NSSD_Value : VoucherType.NSBD_Value);
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NgayChungTu", item.DNgayChungTu.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy"));
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", item.DNgayTao.GetValueOrDefault(new DateTime()).ToString("dd/MM/yyyy"));
                        data.Add("SumTotalHuyDong", SumTotalHuyDong);
                        data.Add("SumTotalTuChi", SumTotalTuChi);
                        data.Add("SumTotalMHHV", SumTotalMHHV);
                        data.Add("SumTotalDT", SumTotalDT);
                        data.Add("SumTotalTongCongNSSD", SumTotalTongCongNSSD);
                        data.Add("SumTotalTongCongNSBD", SumTotalTongCongNSBD);
                        data.Add("ListData", sktChungTuChiTietModels);
                        data.Add("SKTML", sktMucLucsOrder);

                        if (item.ILoaiChungTu == 1)
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSSD);
                        }
                        else
                        {
                            templateFileName = Path.Combine(ExportPrefix.PATH_TL_SKT, ExportFileName.RPT_NS_SOKIEMTRA_CHUNGTU_PHANBO_NSBD);
                        }
                        fileNamePrefix = item.STenDonVi;
                        fileNameWithoutExtension = String.IsNullOrEmpty(TenBaoCao) ? StringUtils.ConvertVN(StringUtils.CreateExportFileName(fileNamePrefix)) : TenBaoCao;

                        FileUploadStreamModel childData = new FileUploadStreamModel();
                        string fileNameChildWithoutExtension = fileNameWithoutExtension + "_" + item.IIdMaDonVi;
                        var xlsFile = _exportService.Export<NsSktChungTuChiTietModel, NsSktMucLuc>(templateFileName, data);
                        var Result = new ExportResult(fileNameChildWithoutExtension, fileNameChildWithoutExtension, null, xlsFile);
                        var fileStream = new MemoryStream();
                        var outputFileStream = new MemoryStream();
                        _exportService.Open(Result, ref fileStream);

                        await _cryptographyService.EncryptFile(fileStream, ref outputFileStream, tokenKey);

                        childData.File = outputFileStream;
                        childData.Name = fileNameChildWithoutExtension + FileExtensionFormats.Security;
                        listAgencyUpload.Add(childData);
                        listIdMaDonViSend.Add(ListChildAgency.Where(x => x.Code == item.IIdMaDonVi).FirstOrDefault().Id);
                    }
                }                
                bool status = await _hTTPUploadFileService.UploadFileAsync(IsSendHTTP, new FileUploadStreamModel()
                {
                    //File = outputFileStream,
                    //Name = fileNameWithoutExtension + FileExtensionFormats.Security,
                    Description = "Phân bổ số kiểm tra",
                    Module = NSFunctionCode.BUDGET,
                    ModuleName = "Số nhu cầu - Kiểm tra",
                    SubModule = NSFunctionCode.BUDGET_DEMANDCHECK_DISTRIBUTION,
                    SubModuleName = "Phân bổ số kiểm tra",
                    TokenKey = tokenKey,
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

                //NsSktChungTu entity = _sktChungTuService.FindById(item.Id);
                //entity.DNgaySua = DateTime.Now;
                //entity.IsSent = true;
                //_sktChungTuService.Update(entity);

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

        private List<NsSktChungTuChiTietModel> GetDistributionVoucherDetail(NsSktChungTuModel nsSktChungTuModel)
        {
            SktChungTuChiTietCriteria searchCondition = new SktChungTuChiTietCriteria();
            searchCondition.NamLamViec = _sessionInfo.YearOfWork;
            searchCondition.NamNganSach = _sessionInfo.YearOfBudget;
            searchCondition.NguonNganSach = _sessionInfo.Budget;
            searchCondition.ITrangThai = StatusType.ACTIVE;
            searchCondition.SktChungTuId = nsSktChungTuModel.Id;
            //searchCondition.ILoai = nsSktChungTuModel.IIdMaDonVi == _sessionInfo.IdDonVi ? DemandCheckType.CHECK : DemandCheckType.DISTRIBUTION;
            searchCondition.ILoai = nsSktChungTuModel.ILoai;
            searchCondition.IdDonVi = nsSktChungTuModel.IIdMaDonVi;
            searchCondition.CurrentIdDonVi = _sessionInfo.IdDonVi;
            searchCondition.UserName = _sessionInfo.Principal;
            searchCondition.LoaiChungTu = nsSktChungTuModel.ILoaiChungTu.GetValueOrDefault();
            var temp = _sktChungTuChiTietService.FindByConditionForChildUnit(searchCondition);
            var lstChungTuChiTietModels = _mapper.Map<List<NsSktChungTuChiTietModel>>(temp);
            CalculateData(lstChungTuChiTietModels);
            lstChungTuChiTietModels = lstChungTuChiTietModels.Where(item => item.FTuChi > 0 || item.FHuyDongTonKho > 0 || item.FMuaHangCapHienVat > 0 || item.FPhanCap > 0).ToList();
            return lstChungTuChiTietModels;
        }

        private void CalculateData(List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            lstSktChungTuChiTiet.Where(x => x.IsHangCha)
                .Select(x =>
                {
                    x.FTuChi = 0;
                    x.FHuyDongTonKho = 0;
                    x.FMuaHangCapHienVat = 0;
                    x.FPhanCap = 0;
                    return x;
                }).ToList();
            var temp = lstSktChungTuChiTiet.Where(x => !x.IsHangCha && !x.IsDeleted);
            foreach (var item in temp)
            {
                CalculateParent(item.IdParent, item, lstSktChungTuChiTiet);
            }

        }

        private void CalculateParent(Guid idParent, NsSktChungTuChiTietModel item, List<NsSktChungTuChiTietModel> lstSktChungTuChiTiet)
        {
            var model = lstSktChungTuChiTiet.FirstOrDefault(x => x.IIdMlskt == idParent);
            if (model == null) return;
            model.FTuChi += item.FTuChi;
            model.FHuyDongTonKho += item.FHuyDongTonKho;
            model.FMuaHangCapHienVat += item.FMuaHangCapHienVat;
            model.FPhanCap += item.FPhanCap;
            CalculateParent(model.IdParent, item, lstSktChungTuChiTiet);
        }

        private DonVi GetNsDonViOfCurrentUser()
        {
            var yearOfWork = _sessionInfo.YearOfWork;
            var currentIdDonVi = _sessionInfo.IdDonVi;
            var predicate = PredicateBuilder.True<DonVi>();
            predicate = predicate.And(x => x.NamLamViec == yearOfWork && x.Loai == "0");
            var nsDonViOfCurrentUser = _nsDonViService.FindByCondition(predicate).FirstOrDefault();
            return nsDonViOfCurrentUser;
        }

        private string Scramble(string s)
        {
            return new string(s.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
        }
    }
}
