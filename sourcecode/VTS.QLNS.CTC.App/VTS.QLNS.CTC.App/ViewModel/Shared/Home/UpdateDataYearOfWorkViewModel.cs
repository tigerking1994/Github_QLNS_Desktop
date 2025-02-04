using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared.Home;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel
{
    public class UpdateDataYearOfWorkViewModel : ViewModelBase
    {
        private ISessionService _sessionService;
        private IDanhMucService _dmService;
        private IMapper _mapper;
        private IMucLucNganSachService _mucLucNganSachService;
        private ICauHinhCanCuService _cauHinhCanCuService;
        private INsDonViService _nsDonViService;
        private INsPhongBanService _nsPhongBanService;
        private INsQsMucLucService _qsMucLucService;
        private ISktMucLucService _sktMucLucService;
        private ICpDanhMucService _cpDanhMucService;
        private ICloneDataYearOfWork _cloneDataYearOfWork;
        private ITlPhuCapMlnsService _tlPhuCapMlnsService;
        private ITlDmCapBacKeHoachService _tlDmCapBacKeHoachService;
        private DanhMucCauHinhHeThongService _danhMucCauHinhHeThongService;
        private IDmCongKhaiTaiChinhService _dmCongKhaiTaiChinhService;
        private IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private IBhDanhMucLoaiChiService _bhDanhMucLoaiChiService;
        private IBhDmCoSoYTeService _bhDmCoSoYTeService;
        private IBhDmThamDinhQuyetToanService _bhDmThamDinhQuyetToanService;
        private IBhDmCauHinhThamSoService _bhDmCauHinhThamSoService;
        private ITlDmNgayNghiService _tlDmNgayNghiService;
        private IDmMucLucQuyetToanService _nsDmMucLucQuyetToanService;
        private readonly ILog _logger;

        public override string Name => "TRANG CHỦ";
        public override Type ContentType => typeof(UpdateDataYearOfWorkView);
        public override PackIconKind IconKind => PackIconKind.Home;

        public ObservableCollection<ComboboxItem> Years { get; set; }

        private int _sourceYear;
        public int SourceYear
        {
            get => _sourceYear;
            set
            {
                SetProperty(ref _sourceYear, value);
                LoadDataYearOfWork();
            }
        }

        private int? _destinationYear;
        public int? DestinationYear
        {
            get => _destinationYear;
            set => SetProperty(ref _destinationYear, value);
        }

        public bool? IsAllItemsSelected
        {
            get
            {
                var selected = DataYearOfWorks.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, DataYearOfWorks);
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<DataYearOfWork> DataYearOfWorks { get; set; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand ChangeCopiedYearYearCommand { get; }
        public RelayCommand CloseCommand { get; }

        public UpdateDataYearOfWorkViewModel(
            ISessionService sessionService,
            IMucLucNganSachService mucLucNganSachService,
            INsPhongBanService nsPhongBanService,
            INsDonViService nsDonViService,
            IDanhMucService danhMucService,
            INsQsMucLucService qsMucLucService,
            ICpDanhMucService cpDanhMucService,
            ISktMucLucService sktMucLucService,
            ICloneDataYearOfWork cloneDataYearOfWork,
            ITlPhuCapMlnsService tlPhuCapMlnsService,
            ICauHinhCanCuService cauHinhCanCuService,
            ITlDmCapBacKeHoachService tlDmCapBacKeHoachService,
            DanhMucCauHinhHeThongService danhMucCauHinhHeThongService,
            IDmCongKhaiTaiChinhService dmCongKhaiTaiChinhService,
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            IBhDanhMucLoaiChiService bhDanhMucLoaiChiService,
            IBhDmCoSoYTeService bhDmCoSoYTeService,
            IBhDmThamDinhQuyetToanService bhDmThamDinhQuyetToanService,
            IBhDmCauHinhThamSoService bhDmCauHinhThamSoService,
            ITlDmNgayNghiService tlDmNgayNghiService,
            IDmMucLucQuyetToanService nsDmMucLucQuyetToanService,
            ILog logger,
            IMapper mapper)
        {
            _sessionService = sessionService;
            _mucLucNganSachService = mucLucNganSachService;
            _nsPhongBanService = nsPhongBanService;
            _nsDonViService = nsDonViService;
            _dmService = danhMucService;
            _qsMucLucService = qsMucLucService;
            _cauHinhCanCuService = cauHinhCanCuService;
            _cpDanhMucService = cpDanhMucService;
            _sktMucLucService = sktMucLucService;
            _cloneDataYearOfWork = cloneDataYearOfWork;
            _tlPhuCapMlnsService = tlPhuCapMlnsService;
            _tlDmCapBacKeHoachService = tlDmCapBacKeHoachService;
            _dmCongKhaiTaiChinhService = dmCongKhaiTaiChinhService;
            _danhMucCauHinhHeThongService = danhMucCauHinhHeThongService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _bhDanhMucLoaiChiService = bhDanhMucLoaiChiService;
            _bhDmCoSoYTeService = bhDmCoSoYTeService;
            _bhDmThamDinhQuyetToanService = bhDmThamDinhQuyetToanService;
            _bhDmCauHinhThamSoService = bhDmCauHinhThamSoService;
            _tlDmNgayNghiService = tlDmNgayNghiService;
            _nsDmMucLucQuyetToanService = nsDmMucLucQuyetToanService;
            _logger = logger;
            _mapper = mapper;

            SaveCommand = new RelayCommand(obj => OnSave(), o => !IsLoading);
            CloseCommand = new RelayCommand(obj => DialogHost.CloseDialogCommand.Execute(null, null));
            ChangeCopiedYearYearCommand = new RelayCommand(obj => LoadDataYearOfWork());
        }

        private void LoadDataYearOfWork()
        {
            DataYearOfWorks = new ObservableCollection<DataYearOfWork>
            {
                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Mục lục ngân sách",
                    CopiedRecords = _mucLucNganSachService.countMLNS(SourceYear),
                    Type = "MucLucNganSach"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục đơn vị",
                    CopiedRecords = _nsDonViService.countNsDonViByNamLamViec(SourceYear),
                    Type = "DanhMucDonVi"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục B quản lý",
                    CopiedRecords = _nsPhongBanService.countPhongBanByNamLamViec(SourceYear),
                    Type = "DanhMucBQuanLy"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục chuyên ngành",
                    CopiedRecords = _dmService.countDanhMucByTypeAndNLV("NS_Nganh", SourceYear),
                    Type = "DanhMucChuyenNganh"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục ngành",
                    CopiedRecords = _dmService.countDanhMucByTypeAndNLV("NS_Nganh_Nganh", SourceYear),
                    Type = "DanhMucNganh"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Mục lục quân số",
                    CopiedRecords = _qsMucLucService.countMLQSByNamLamViec(SourceYear),
                    Type = "DanhMucQuanSo"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Mục lục số kiểm tra",
                    CopiedRecords = _sktMucLucService.CountSktMucLuc(SourceYear),
                    Type = "MucLucSoKiemTra"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục cấp phát",
                    CopiedRecords = _cpDanhMucService.CountDanhMucCP(SourceYear),
                    Type = "DanhMucCapPhat"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Cấu hình chỉ tiêu lương - MLNS",
                    CopiedRecords = _tlPhuCapMlnsService.CountByYear(SourceYear),
                    Type = "CauHinhChiTieuLuong"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục cấp bậc - kế hoạch",
                    //TotalRecords = _tlDmCapBacKeHoachService.CountByYear(DestinationYear.Value),
                    CopiedRecords = _tlDmCapBacKeHoachService.CountByYear(SourceYear),
                    Type = "DanhMucCapBac"
                },


                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Mục lục số kiểm tra - mục lục ngân sách",
                    CopiedRecords = _sktMucLucService.CountNsMlsktMlns(SourceYear),
                    Type = "MucLucSoKiemTraNganSach"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục cấu hình hệ thống",
                    CopiedRecords = _danhMucCauHinhHeThongService.CountByYear(SourceYear),
                    Type = "DanhMucCauHinhHeThong"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Dữ liệu ngành - chuyên ngành",
                    CopiedRecords = _dmService.countDanhMucNganhChuyenNganh(SourceYear),
                    Type = "MucLucNganhChuyenNganh"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục đơn vị tính",
                    CopiedRecords = _dmService.countDanhMucByTypeAndNLV("DM_DonViTinh", SourceYear),
                    Type = "DanhMucDonViTinh"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục căn cứ",
                    CopiedRecords = _cauHinhCanCuService.FindByCondition(x => x.INamLamViec == SourceYear).Count(),
                    Type = "DanhMucCanCu"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục công khai tài chính",
                    CopiedRecords = _dmCongKhaiTaiChinhService.FindByCondition(x => x.iNamLamViec == SourceYear).Count(),
                    Type = "DanhMucCongKhaiTaiChinh"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục MLNS BHXH",
                    CopiedRecords = _bhDmMucLucNganSachService.FindByCondition(x => x.INamLamViec == SourceYear).Count(),
                    Type = "DanhMucMLNSBHXH"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục các loại chi",
                    CopiedRecords = _bhDanhMucLoaiChiService.FindByNamLamViec(SourceYear).Count(),
                    Type = "DanhMucCacLoaiChi"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục cơ sở y tế",
                    CopiedRecords = _bhDmCoSoYTeService.FindByCondition(x => x.INamLamViec == SourceYear).Count(),
                    Type = "DanhMucCoSoYTe"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục thẩm định quyết toán",
                    CopiedRecords = _bhDmThamDinhQuyetToanService.FindAll(x => x.INamLamViec == SourceYear).Count(),
                    Type = "DanhMucThamDinhQuyetToan"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục cấu hình tham số",
                    CopiedRecords = _bhDmCauHinhThamSoService.FindByCondition(x => x.INamLamViec == SourceYear).Count(),
                    Type = "DanhMucCauHinhThamSoBHXH"
                },

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Danh mục ngày nghỉ",
                    CopiedRecords = _tlDmNgayNghiService.FindByYear(SourceYear).Count(),
                    Type = "DanhMucCauHinhNgayNghi"
                }
                ,

                new DataYearOfWork
                {
                    IsSelected = true,
                    TableName = "Mục lục quyết toán năm",
                    CopiedRecords = _nsDmMucLucQuyetToanService.FindByCondition(x => x.NamLamViec == SourceYear).Count(),
                    Type = "MucLucQuyetToanNam"
                }
            };

            foreach (DataYearOfWork dataYearOfWork in DataYearOfWorks)
            {
                dataYearOfWork.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(DataYearOfWork.IsSelected))
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                };
            }

            OnPropertyChanged(nameof(DataYearOfWorks));
        }

        public override void Init()
        {
            base.Init();
            LoadDataYearOfWork();
        }

        public override void OnSave()
        {
            if (!DestinationYear.HasValue)
            {
                MessageBoxHelper.Error(Resources.YearInvalid);
                return;
            }

            AuthenticationInfo authenticationInfo = _mapper.Map<AuthenticationInfo>(_sessionService.Current);
            int isUpdatedMLNS = DataYearOfWorks[0].IsSelected ? 1 : 0;
            int isUpdatedNSDV = DataYearOfWorks[1].IsSelected ? 1 : 0;
            int isUpdatedBQuanly = DataYearOfWorks[2].IsSelected ? 1 : 0;
            int isUpdateDanhMucChuyenNganh = DataYearOfWorks[3].IsSelected ? 1 : 0;
            int isUpdateDanhMucNganh = DataYearOfWorks[4].IsSelected ? 1 : 0;
            int isUpdateMLQS = DataYearOfWorks[5].IsSelected ? 1 : 0;
            int isUpdateMuclucSkt = DataYearOfWorks[6].IsSelected ? 1 : 0;
            int isUpdateDanhMucCapPhat = DataYearOfWorks[7].IsSelected ? 1 : 0;
            int isUpdatePhuCapMLNS = DataYearOfWorks[8].IsSelected ? 1 : 0;
            int isUpdateDanhMucCapBacKH = DataYearOfWorks[9].IsSelected ? 1 : 0;
            int isUpdateMucLucSktMuclucNs = DataYearOfWorks[10].IsSelected ? 1 : 0;
            int isUpdateCauHinhHeThong = DataYearOfWorks[11].IsSelected ? 1 : 0;
            int isUpdateDanhMucDonViTinh = DataYearOfWorks[13].IsSelected ? 1 : 0;
            int isUpdateDanhMucCanCu = DataYearOfWorks[14].IsSelected ? 1 : 0;
            int isUpdateDanhMucCKTC = DataYearOfWorks[15].IsSelected ? 1 : 0;
            int isUpdateDanhMucBHXH = DataYearOfWorks[16].IsSelected ? 1 : 0;
            int isUpdateDanhMucCacLoaiChi = DataYearOfWorks[17].IsSelected ? 1 : 0;
            int isUpdateDanhMucCoSoYTe = DataYearOfWorks[18].IsSelected ? 1 : 0;
            int isUpdateDanhMucThamDinhQuyetToan = DataYearOfWorks[19].IsSelected ? 1 : 0;
            int isUpdateDanhMucCauHinhThamSoBHXH = DataYearOfWorks[20].IsSelected ? 1 : 0;
            int isUpdateDanhMucNgayNghi = DataYearOfWorks[21].IsSelected ? 1 : 0;
            int isUpdateMucLucQuyetToannam = DataYearOfWorks[22].IsSelected ? 1 : 0;

            DanhMuc namLamViec = new DanhMuc
            {
                INamLamViec = DestinationYear.Value,
                SType = "NS_NamLamViec",
                STen = "Năm làm việc " + DestinationYear.Value,
                SGiaTri = DestinationYear.Value.ToString(),
                ITrangThai = 1,
                IIDMaDanhMuc = DestinationYear.Value.ToString(),
            };

            var selected = DataYearOfWorks.Where(c => c.IsSelected).Select(c => c.Type).ToList();
            bool isCopyUnit = true;
            if (selected.Contains("DanhMucDonVi"))
            {
                var isDuplicate = _cloneDataYearOfWork.IsDuplicateUnit(SourceYear, DestinationYear.Value);
                if (isDuplicate)
                {
                    var confirm = MessageBoxHelper.Confirm("Đã tồn tại đơn vị sử dụng phần mềm, tiếp tục sao chép đơn vị?");
                    if (confirm == MessageBoxResult.No)
                    {
                        selected.Remove("DanhMucDonVi");
                    }
                }
            }

            BackgroundWorkerHelper.Run((s, e) =>
            {
                IsLoading = true;

                // thêm mới năm làm việc nếu chưa tồn tại
                if (!Years.Select(t => t.ValueItem).Contains(DestinationYear.ToString()))
                {
                    if (DestinationYear == 2024 || DestinationYear == 2025)
                    {
                        isUpdateMuclucSkt = 0;
                        isUpdateMucLucSktMuclucNs = 0;
                    }

                    _dmService.Add(namLamViec);
                    _cloneDataYearOfWork.CloneData(SourceYear, DestinationYear.Value, authenticationInfo,
                        isUpdatedMLNS: isUpdatedMLNS,
                        isUpdatedNSDV: isUpdatedNSDV,
                        isUpdatedBQuanly: isUpdatedBQuanly,
                        isUpdateMLQS: isUpdateMLQS,
                        isUpdateDanhMucChuyenNganh: isUpdateDanhMucChuyenNganh,
                        isUpdateDanhMucNganh: isUpdateDanhMucNganh,
                        isUpdateMuclucSkt: isUpdateMuclucSkt,
                        isUpdateDanhMucCapPhat: isUpdateDanhMucCapPhat,
                        isUpdatePhuCapMLNS: isUpdatePhuCapMLNS,
                        isUpdateDanhMucCapBacKH: isUpdateDanhMucCapBacKH,
                        isUpdateMucLucSktMuclucNs: isUpdateMucLucSktMuclucNs,
                        isUpdateCauHinhHeThong: isUpdateCauHinhHeThong,
                        isUpdateDanhMucDonViTinh: isUpdateDanhMucDonViTinh,
                        isUpdateDanhMucCanCu: isUpdateDanhMucCanCu,
                        isUpdateDanhMucCKTC: isUpdateDanhMucCKTC,
                        isUpdateDanhMucBHXH: isUpdateDanhMucBHXH,
                        isUpdateDanhMucCacLoaiChi: isUpdateDanhMucCacLoaiChi,
                        isUpdateDanhMucCoSoYTe: isUpdateDanhMucCoSoYTe,
                        isUpdateDanhMucThamDinhQuyetToan: isUpdateDanhMucThamDinhQuyetToan,
                        isUpdateDanhMucCauHinhThamSoBHXH: isUpdateDanhMucCauHinhThamSoBHXH,
                        isUpdateDanhMucNgayNghi: isUpdateDanhMucNgayNghi,
                        isUpdateMucLucQuyetToannam: isUpdateMucLucQuyetToannam
                        );
                }
                else
                {
                    Stopwatch stopWatch = new Stopwatch();
                    foreach (var item in selected)
                    {
                        stopWatch.Restart();
                        _cloneDataYearOfWork.CloneDataExistData(SourceYear, DestinationYear.Value, item, authenticationInfo, isCopyUnit);
                        stopWatch.Stop();
                        Console.WriteLine(item + ": " + stopWatch.ElapsedMilliseconds);
                    }
                }

            }, (s, e) =>
            {
                IsLoading = false;
                if (e.Error == null)
                {
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    System.Windows.Forms.MessageBox.Show("Lấy dữ liệu thành công", Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _logger.Error(e.Error.Message, e.Error);
                    MessageBoxHelper.Info("Năm làm việc đã tồn tại");
                }
            });
        }

        private static void SelectAll(bool select, IEnumerable<DataYearOfWork> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }
    }

    public class DataYearOfWork : BindableBase
    {
        private string _tableName;
        public string TableName
        {
            get => _tableName;
            set => SetProperty(ref _tableName, value);
        }

        private int _totalRecords;
        public int TotalRecords
        {
            get => _totalRecords;
            set => SetProperty(ref _totalRecords, value);
        }

        private int _copiedRecords;
        public int CopiedRecords
        {
            get => _copiedRecords;
            set => SetProperty(ref _copiedRecords, value);
        }

        public string Note { get; set; }
        public string Type { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }

    public class DataYearOfWorkContants
    {

    }
}
