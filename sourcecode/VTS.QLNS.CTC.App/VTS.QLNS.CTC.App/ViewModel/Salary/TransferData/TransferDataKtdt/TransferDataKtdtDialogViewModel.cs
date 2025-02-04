using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.TransferData.TransferDataKtdt
{
    public class TransferDataKtdtDialogViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ISessionService _sessionService;
        //private TlDmDonViService _tlDmDonViService;
        public override Type ContentType => typeof(View.Salary.TransferData.TranferDataKtdt.TransferDataKtdtDialog);
        public override PackIconKind IconKind => PackIconKind.FolderSwapOutline;
        public override string Title => "Cấu hình import";
        public override string Description => "Cấu hình import";

        public override string FuncCode => NSFunctionCode.SALARY_CHUYEN_DOI_DU_LIEU_KTDT_DIALOG;

        private List<ComboboxItem> _months;
        public List<ComboboxItem> Months
        {
            get => _months;
            set => SetProperty(ref _months, value);
        }

        private ComboboxItem _monthSelected;
        public ComboboxItem MonthSelected
        {
            get => _monthSelected;
            set => SetProperty(ref _monthSelected, value);
        }

        private ComboboxItem _monthImportSelected;
        public ComboboxItem MonthImportSelected
        {
            get => _monthImportSelected;
            set => SetProperty(ref _monthImportSelected, value);
        }

        private List<ComboboxItem> _years;
        public List<ComboboxItem> Years
        {
            get => _years;
            set => SetProperty(ref _years, value);
        }

        private ComboboxItem _yearSelected;
        public ComboboxItem YearSelected
        {
            get => _yearSelected;
            set => SetProperty(ref _yearSelected, value);
        }

        private ComboboxItem _yearImportSelected;
        public ComboboxItem YearImportSelected
        {
            get => _yearImportSelected;
            set => SetProperty(ref _yearImportSelected, value);
        }

        private string _port;
        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public DataTable DtDonVi { get; set; }
        public DataTable DtDoiTuong { get; set; }
        public DataTable DtPhuCapCanBo { get; set; }

        public RelayCommand ChooseCommand { get; }

        public Action<object> ChooseAction;

        public TransferDataKtdtDialogViewModel(
            IMapper mapper,
            ISessionService sessionService,
            //TlDmDonViService tlDmDonViService,
            ILog logger)
        {
            _mapper = mapper;
            _sessionService = sessionService;
            //_tlDmDonViService = tlDmDonViService;
            _logger = logger;

            ChooseCommand = new RelayCommand(o => OnChoose());
        }

        public override void Init()
        {
            base.Init();
            LoadMonths();
            LoadYear();
        }

        private void LoadMonths()
        {
            _months = new List<ComboboxItem>();
            for (int i = 1; i <= 12; i++)
            {
                ComboboxItem month = new ComboboxItem("Tháng " + i, i.ToString());
                _months.Add(month);
            }
            var thang = _sessionService.Current.Month;
            OnPropertyChanged(nameof(Months));
            MonthSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
            MonthImportSelected = _months.FirstOrDefault(x => x.ValueItem == thang.ToString());
        }

        private void LoadYear()
        {
            _years = new List<ComboboxItem>();
            for (int i = DateTime.Now.Year - 29; i <= DateTime.Now.Year + 29; i++)
            {
                ComboboxItem year = new ComboboxItem("Năm " + i, i.ToString());
                _years.Add(year);
            }
            var nam = _sessionService.Current.YearOfWork;
            OnPropertyChanged(nameof(Years));
            YearSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
            YearImportSelected = _years.FirstOrDefault(x => x.ValueItem == nam.ToString());
        }

        private void OnChoose()
        {
            try
            {
                if (string.IsNullOrEmpty(Port))
                {
                    MessageBox.Show("Port không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DtDoiTuong = new DataTable();
                DtDonVi = new DataTable();
                DtPhuCapCanBo = new DataTable();

                string conStr = string.Format("Host=localhost;Database=IFS2018;Port={0};Username=postgres;Password=ifspro 123", Port);
                NpgsqlConnection pgCon = new NpgsqlConnection(conStr);
                pgCon.Open();
                var query1 = "SELECT * FROM \"public\""
                    + "."
                    + "\"DM_CBO\" WHERE \"THANG\" = "
                    + MonthSelected.ValueItem
                    + " AND \"NAM\" = "
                    + YearSelected.ValueItem + " AND \"PARENT\" IS NOT NULL";
                /*
                var query2 = "SELECT * FROM \"public\""
                    + "."
                    + "\"DM_CBO\" WHERE \"THANG\" = "
                    + MonthSelected.ValueItem
                    + " AND \"NAM\" = "
                    + YearSelected.ValueItem 
                    + " AND \"PARENT\" IS NULL";
                var query3 = "SELECT * FROM \"public\"" + "." + "\"DM_PHUCAP_CBO\"";
                */

                var query2 = "SELECT * FROM \"DM_CBO\" \"dv\" "
                    + " inner join(select DISTINCT \"PARENT\" from  \"DM_CBO\"  where \"THANG\" = "
                    + MonthSelected.ValueItem
                    + " and \"NAM\" = "
                    + YearSelected.ValueItem
                    + " and \"PARENT\" is NOT null)  \"CB\" on \"dv\".\"MA_CBO\" = \"CB\".\"PARENT\" "
                    + " where 1 = 1"
                    + " and  \"dv\".\"PARENT\" is NULL"
                    + " order by \"dv\".\"MA_CBO\" ";

                //var query3 = "SELECT * FROM \"public\"" + "." + "\"DM_PHUCAP_CBO\""
                //    + " WHERE 1 = 1 AND \"GIA_TRI\" <> 0 AND  \"GIA_TRI\" IS NOT NULL";

                var thang = MonthSelected.ValueItem;

                if (!(thang == "10" || thang == "11" || thang == "12")) {

                    thang = "0" + thang;
                }

                var query3 = "SELECT * FROM \"public\"" + "." + "\"DM_PHUCAP_CBO\""
                    + " WHERE 1 = 1 and \"MA_CBO\"" + " like '" + YearSelected.ValueItem + thang + "%' AND \"GIA_TRI\" > 0 AND  \"GIA_TRI\" IS NOT NULL";


                using var cmd1 = new NpgsqlCommand(query1, pgCon);
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(cmd1);
                da1.Fill(DtDoiTuong);

                using var cmd2 = new NpgsqlCommand(query2, pgCon);
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(cmd2);
                da2.Fill(DtDonVi);

                using var cmd3 = new NpgsqlCommand(query3, pgCon);
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(cmd3);
                da3.Fill(DtPhuCapCanBo);

                //var predicate = PredicateBuilder.True<TlDmDonVi>();
                //var listDonvi = _tlDmDonViService.FindByCondition(predicate).ToList();
                //if (listDonvi != null && listDonvi.Count() > 0)
                //{
                //    foreach (var item in listDonvi)
                //    {
                //        item.ITrangThai = true;
                //    }
                //    _tlDmDonViService.UpdateRange(listDonvi);
                //}

                pgCon.Close();
                ChooseAction.Invoke(ChooseCommand);
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                MessageBox.Show(string.Format("Kết nối database tại cổng {0} thất bại.", Port), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
