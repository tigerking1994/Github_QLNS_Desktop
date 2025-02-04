using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service.Impl;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using System.Web.UI.WebControls;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.App.View.Shared;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class PhuCapDialogViewModel : DialogViewModelBase<BhDmMucLucNganSachModel>
    {
        private readonly IBhDmMucLucNganSachService _bhDmMucLucNganSachService;
        private readonly TlDmPhuCapService _tlDmPhuCapService;
        private readonly ISessionService _sessionService;
        private readonly ISysAuditLogService _log;
        private readonly IMapper _mapper;
        private SessionInfo _sessionInfo;
        private readonly ILog _logger;
        private ICollectionView _dataPhuCapView;

        public override Type ContentType => typeof(PhuCapDialog);
        public override string Name => "CẤU HÌNH PHỤ CẤP";

        public bool IsEnabled => Guid.Empty.Equals(Model.Id);

        private bool _isSaveData;
        public bool IsSaveData
        {
            get => _isSaveData;
            set => SetProperty(ref _isSaveData, value);
        }

        private ObservableCollection<TlDmPhuCapModel> _dataPhuCap;
        public ObservableCollection<TlDmPhuCapModel> DataPhuCap
        {
            get => _dataPhuCap;
            set => SetProperty(ref _dataPhuCap, value);
        }

        public string SelectedCountPhuCap
        {
            get
            {
                int totalCount = DataPhuCap != null ? DataPhuCap.Where(x => x.IsFilter).Count() : 0;
                int totalSelected = DataPhuCap != null ? DataPhuCap.Count(item => item.IsSelected) : 0;
                return string.Format("CHỌN PHỤ CẤP ({0}/{1})", totalSelected, totalCount);
            }
        }

        private bool _selectAllPhuCap;
        public bool SelectAllPhuCap
        {
            get => (DataPhuCap == null || !DataPhuCap.Any()) ? false : DataPhuCap.All(item => item.IsSelected);
            set
            {
                SetProperty(ref _selectAllPhuCap, value);
                if (DataPhuCap != null)
                {
                    DataPhuCap.Select(c => { c.IsSelected = _selectAllPhuCap; return c; }).ToList();
                }
            }
        }

        private string _searchPhuCap;
        public string SearchPhuCap
        {
            get => _searchPhuCap;
            set
            {
                if (SetProperty(ref _searchPhuCap, value))
                {
                    _dataPhuCapView.Refresh();
                    OnPropertyChanged(nameof(SelectedCountPhuCap));
                }
            }
        }

        public PhuCapDialogViewModel(
            IBhDmMucLucNganSachService bhDmMucLucNganSachService,
            TlDmPhuCapService tlDmPhuCapService,
            IMapper mapper,
            ISessionService sessionService,
            ISysAuditLogService log,
            ILog logger)
        {
            _sessionService = sessionService;
            _bhDmMucLucNganSachService = bhDmMucLucNganSachService;
            _tlDmPhuCapService = tlDmPhuCapService;
            _log = log;
            _mapper = mapper;
            _logger = logger;
        }

        public override void Init()
        {
            _sessionInfo = _sessionService.Current;
            IsSaveData = true;
            _searchPhuCap = string.Empty;
            LoadPhuCap();
        }

        private void LoadPhuCap()
        {
            var listMLNS = _tlDmPhuCapService.FindAll().ToList();
            DataPhuCap = _mapper.Map<ObservableCollection<TlDmPhuCapModel>>(listMLNS);
            _dataPhuCapView = CollectionViewSource.GetDefaultView(DataPhuCap);
            _dataPhuCapView.Filter = ListPhuCapFilter;
            SetCheckboxSelected(_dataPhuCap, Model.SMaPhuCap);
        }

        private bool ListPhuCapFilter(object obj)
        {
            bool result = true;
            var item = (TlDmPhuCapModel)obj;
            if (!string.IsNullOrWhiteSpace(_searchPhuCap))
                result = item.DisplayCheckBox.ToLower().Contains(_searchPhuCap!.ToLower());
            item.IsFilter = result;
            return result;
        }

        public static void SetCheckboxSelected(ObservableCollection<TlDmPhuCapModel> data, string value)
        {
            if (string.IsNullOrEmpty(value) || data == null || data.Count == 0)
                return;
            List<string> selectedValues = value.Split(",").ToList();
            foreach (TlDmPhuCapModel item in data)
            {
                item.IsSelected = selectedValues.Contains(item.MaPhuCap);
            }
        }

        public override void OnSave()
        {
            if (Model == null) Model = new BhDmMucLucNganSachModel();
            Model.SMaPhuCap = GetValueSelected(DataPhuCap);
            BhDmMucLucNganSach entity = new BhDmMucLucNganSach();
            _mapper.Map(Model, entity);
            _bhDmMucLucNganSachService.Update(entity);

            DialogHost.Close(SystemConstants.ROOT_DIALOG);
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public static string GetValueSelected(ObservableCollection<TlDmPhuCapModel> data)
        {
            if (data.Count > 0)
            {
                return string.Join(",", data.Where(n => n.IsSelected == true).Select(n => n.MaPhuCap).Distinct().ToList());
            }
            return string.Empty;
        }
    }
}
