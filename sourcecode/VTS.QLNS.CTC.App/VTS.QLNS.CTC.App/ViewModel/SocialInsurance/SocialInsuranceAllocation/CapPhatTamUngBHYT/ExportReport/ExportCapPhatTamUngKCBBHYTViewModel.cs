using AutoMapper;
using log4net;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Markup;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.View.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ExportReport;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.SocialInsurance.SocialInsuranceAllocation.CapPhatTamUngBHYT.ExportReport
{
    public class ExportCapPhatTamUngKCBBHYTViewModel : DialogViewModelBase<BhCptuBHYTModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ISessionService _sessionService;
        private INsDonViService _donViService;
        private IExportService _exportService;
        private IDanhMucService _danhMucService;
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly ICptuBHYTRepository _cptuBHYTRepository;
        private readonly ICptuBHYTChiTietRepository _cptuChiTietRepository;
        private readonly IBhDmCoSoYTeService _bhDmCoSoYTeService;

        public ObservableCollection<BhCptuBHYTModel> Items;
        public override Type ContentType => typeof(ExportCapPhatTamUngKCBBHYT);
        private ObservableCollection<ComboboxItem> _bTieuChiItems = new ObservableCollection<ComboboxItem>();
        public ObservableCollection<ComboboxItem> BTieuChiItems
        {
            get => _bTieuChiItems;
            set => SetProperty(ref _bTieuChiItems, value);
        }

        private ComboboxItem _bTieuChiSelected;
        public ComboboxItem BTieuChiSelected
        {
            get => _bTieuChiSelected;
            set
            {
                SetProperty(ref _bTieuChiSelected, value);
            }
        }

        public RelayCommand ExportCommand { get; }

        private void LoadTieuChis()
        {

            var danhMucCauHinh = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).ToList();
            if (danhMucCauHinh.Count > 0)
            {
                var danhMucMLNS = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                string chiTietToi = danhMucMLNS == null ? string.Empty : danhMucMLNS.SGiaTri;
                BTieuChiItems = new ObservableCollection<ComboboxItem>(DynamicMLNS.CreateMLNSReportSetting(chiTietToi, false));
                _bTieuChiSelected = BTieuChiItems != null ? BTieuChiItems[0] : null;
            }

        }

        public ExportCapPhatTamUngKCBBHYTViewModel(ILog logger,
                                               IMapper mapper,
                                               ISessionService sessionService,
                                               IDanhMucService danhMucService,
                                               INsDonViService donViService,
                                               IExportService exportService,
                                               IBhDmMucLucNganSachService bhDmMucLucNganSachService,
                                               ICptuBHYTRepository cptuBHYTRepository,
                                               ICptuBHYTChiTietRepository cptuChiTietRepository,
                                               IBhDmCoSoYTeService bhDmCoSoYTeService)
        {

            _logger = logger;
            _mapper = mapper;
            _sessionService = sessionService;
            _donViService = donViService;
            _danhMucService = danhMucService;
            _exportService = exportService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _cptuBHYTRepository = cptuBHYTRepository;
            _cptuChiTietRepository = cptuChiTietRepository;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;

            ExportCommand = new RelayCommand(obj => OnExportGridData());
        }

        public override void Init()
        {
            base.Init();
            LoadTieuChis();
            _sessionInfo = _sessionService.Current;

        }

        private void OnExportGridData()
        {
            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                string chiTietToi = "NG";
                DanhMuc danhMucChiTietToi = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.MLNS_CHITIET_TOI).FirstOrDefault();
                if (danhMucChiTietToi != null)
                    chiTietToi = danhMucChiTietToi.SGiaTri;
                var itemDanhMuc = _danhMucService.FindByType(TypeDanhMuc.DM_CAUHINH, _sessionService.Current.YearOfWork).Where(n => n.IIDMaDanhMuc == MaDanhMuc.DV_QUANLY).FirstOrDefault();
                List<ExportResult> results = new List<ExportResult>();

                string templateFileName = ExportFileName.RP_BH_EXPORT_CAPPHATTAMUNGKCBHUYT;

                var namLamViec = _sessionService.Current.YearOfWork;

                var predicate_danhmuc = PredicateBuilder.True<BhDmMucLucNganSach>();
                predicate_danhmuc = predicate_danhmuc.And(x => x.INamLamViec == namLamViec);
                var listMucLucNganSach = _bhDmMucLucNganSachService.FindByCondition(predicate_danhmuc).Where(x => x.SLNS.StartsWith("904")).OrderBy(x=> x.SXauNoiMa).ToList();


                List<BhDmCoSoYTe> listCsYTe = new List<BhDmCoSoYTe>();
                var predicate_csyte = PredicateBuilder.True<BhDmCoSoYTe>();
                predicate_csyte = predicate_csyte.And(x => x.INamLamViec == namLamViec);
                listCsYTe = _bhDmCoSoYTeService.FindByCondition(predicate_csyte).ToList();
                
                var itemsExport = Items.Where(x => x.Selected);
            
                foreach (var item in itemsExport)
                {
                    var dataExportDetail = LoadDataExportDetail(item);
                   
                    List<string> lstCsYTe = item.SDSID_CoSoYTe.Split(",").ToList();

                    foreach (var dv in lstCsYTe)
                    {           
                        var csYTe = listCsYTe.Where(x => x.IIDMaCoSoYTe == dv).FirstOrDefault();
                        var lstDataPrent = dataExportDetail.Where(x => x.BHangCha).ToList();
                        var lstDataChildbyCsYTe = dataExportDetail.Where(x => x.IID_MaCoSoYTe == dv && !x.BHangCha).ToList();

                        lstDataPrent.AddRange(lstDataChildbyCsYTe);
                        List<BhCptuBHYTChiTietModel> lstData = new List<BhCptuBHYTChiTietModel>();
                        lstData = _mapper.Map(lstDataPrent, lstData);
                        CalculateData(lstData);
                        lstData = lstData.Where(x => (x.FQTQuyTruoc ?? 0) != 0 || (x.FLuyKeCapDenCuoiQuy ?? 0) != 0 || (x.FTamUngQuyNay ?? 0) != 0).OrderBy(x=> x.SXauNoiMa).ToList();

                        var data = new Dictionary<string, object>();
                        data.Add("Cap1", itemDanhMuc != null ? itemDanhMuc.SGiaTri.ToUpper() : "");
                        data.Add("Cap2", _sessionService.Current.TenDonVi.ToUpper());
                        data.Add("TitleFirst", $"CẤP PHÁT TẠM ỨNG KINH PHÍ BHYT NĂM {_sessionService.Current.YearOfWork}");
                        data.Add("TitleSecond", $"(Kèm theo Quyết định số: {item.SSoQuyetDinh}, ngày: {DateUtils.Format(item.DNgayQuyetDinh)})");

                        data.Add("HeaderTenDonVi", $"Cơ sở y tế: {csYTe?.IIDMaCoSoYTe.PadLeft(3, '0')}{StringUtils.DIVISION}{csYTe?.STenCoSoYTe}");
                        data.Add("TenDonVi", $"{csYTe?.IIDMaCoSoYTe.PadLeft(3, '0')}{StringUtils.DIVISION}{csYTe?.STenCoSoYTe}");
                        data.Add("SoChungTu", item.SSoChungTu);
                        data.Add("NgayChungTu", DateUtils.Format(item.DNgayChungTu));
                        data.Add("SoQuyetDinh", item.SSoQuyetDinh);
                        data.Add("NgayQuyetDinh", DateUtils.Format(item.DNgayQuyetDinh));
                        data.Add("MoTa", item.SMoTa);
                        data.Add("NguoiTao", item.SNguoiTao);
                        data.Add("NgayTao", DateUtils.Format(item.DNgayTao));


                        data.Add("Items", lstData);
                        data.Add("MLNS", listMucLucNganSach);

                        double? TotalQTQuyTruoc = lstData?.Where(x => !x.BHangCha).Sum(x => x.FQTQuyTruoc);
                        double? TotalLuyKeCapDenCuoiQuy = lstData?.Where(x => !x.BHangCha).Sum(x => x.FLuyKeCapDenCuoiQuy);
                        double? TotalTamUngQuyNay = lstData?.Where(x => !x.BHangCha).Sum(x => x.FTamUngQuyNay);


                        data.Add("TotalQTQuyTruoc", string.Format(StringUtils.FORMAT_ZERO, TotalQTQuyTruoc));
                        data.Add("TotalLuyKeCapDenCuoiQuy", string.Format(StringUtils.FORMAT_ZERO, TotalLuyKeCapDenCuoiQuy));
                        data.Add("TotalTamUngQuyNay", string.Format(StringUtils.FORMAT_ZERO, TotalTamUngQuyNay));

                        List<int> hideColumns = new List<int>();
                        hideColumns.AddRange(ExportExcelHelper<BhCptuBHYTChiTietModel>.HideColumn(chiTietToi));
                     

                        var xlsFile = _exportService.Export<BhCptuBHYTChiTietModel, BhDmMucLucNganSach>(templateFileName, data, hideColumns);
                        var nameRange = xlsFile.GetNamedRange(1);
                        nameRange.Comment = "Workbook";
                        xlsFile.SetNamedRange(nameRange);
                        xlsFile.SetNamedRange(new FlexCel.Core.TXlsNamedRange("__Area_Items__", 1, 0, "=1:2,A:B"));
                        xlsFile.SetCellValue(50, 50, "CheckSum");
                        xlsFile.SetRowHidden(50, true);
                        string fileNamePrefix = string.Format("{0}_{1}_{2}", item.SSoChungTu, item.SSoQuyetDinh, StringUtils.ConvertVN(csYTe?.STenCoSoYTe));
                        string fileNameWithoutExtension = StringUtils.CreateExportFileName(fileNamePrefix);
                        results.Add(new ExportResult(fileNameWithoutExtension, fileNameWithoutExtension, null, xlsFile));
                    }
                }

                e.Result = results;
            }, (s, e) =>
            {
                if (e.Error == null)
                {
                    var result = (List<ExportResult>)e.Result;
                    _exportService.Open(result, ExportType.EXCEL);
                }
                else
                {
                    _logger.Error(e.Error.Message);
                }
                IsLoading = false;
            });
        }

        private List<BhCptuBHYTChiTietModel> LoadDataExportDetail(BhCptuBHYTModel item)
        {
            int iQuyKyTruoc = 0;
            int iNamKyTruoc = 0;

            if (item.IQuy == 1)
            {
                iQuyKyTruoc = 4;
                iNamKyTruoc = (_sessionInfo.YearOfWork - 1);
            }
            else
            {
                iQuyKyTruoc = item.IQuy - 1;
                iNamKyTruoc = _sessionInfo.YearOfWork;
            }

            List<BhCptuBHYTChiTietModel> lstResult = new List<BhCptuBHYTChiTietModel>();
            var lstChungTuChiTiet = _cptuChiTietRepository.FinChungTuChiTiet(item.Id, item.SDSLNS, item.SDSID_CoSoYTe, _sessionInfo.YearOfWork, iQuyKyTruoc, iNamKyTruoc, _sessionInfo.Principal).ToList();
            lstResult = _mapper.Map(lstChungTuChiTiet, lstResult).ToList();
            return lstResult;
        }


        private void CalculateData(List<BhCptuBHYTChiTietModel> listData)
        {
            listData.Where(x => x.BHangCha)
                .Select(x =>
                {
                    x.FQTQuyTruoc = 0;
                    x.FTamUngQuyNay = 0;
                    x.FLuyKeCapDenCuoiQuy = 0;
                    return x;
                }).ToList();

            foreach (var item in listData.Where(x => !x.BHangCha && (x.FQTQuyTruoc != 0 || x.FTamUngQuyNay != 0 || x.FLuyKeCapDenCuoiQuy !=0)))
            {
                CalculateParent(listData, item, item);
            }
        }

        private void CalculateParent(List<BhCptuBHYTChiTietModel> listData, BhCptuBHYTChiTietModel currentItem, BhCptuBHYTChiTietModel seftItem)
        {
            var parrentItem = listData.FirstOrDefault(x => x.IID_MLNS == currentItem.IID_MLNS_Cha);
            if (parrentItem == null) return;
            parrentItem.FQTQuyTruoc = (parrentItem.FQTQuyTruoc ?? 0) + (seftItem.FQTQuyTruoc ?? 0);
            parrentItem.FTamUngQuyNay = (parrentItem.FTamUngQuyNay ?? 0) +  (seftItem.FTamUngQuyNay ?? 0);
            parrentItem.FLuyKeCapDenCuoiQuy = (parrentItem.FLuyKeCapDenCuoiQuy ?? 0) + (seftItem.FLuyKeCapDenCuoiQuy ?? 0);

            CalculateParent(listData, parrentItem, seftItem);
        }
    }
}
