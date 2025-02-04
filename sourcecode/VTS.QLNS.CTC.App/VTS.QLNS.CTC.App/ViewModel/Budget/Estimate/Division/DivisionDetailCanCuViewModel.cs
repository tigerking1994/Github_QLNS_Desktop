using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Budget.Estimate.Division;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.Budget.Estimate.Division
{
    public class DivisionDetailCanCuViewModel : DetailViewModelBase<DtChungTuModel, LbChungTuCanCuModel>
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly ILbChungTuService _chungTuService;
        private readonly ISessionService _sessionService;

        public override string Name => "Phân bổ dự toán ngành";
        public override string Description => "Chọn danh sách phân bổ dự toán ngành";
        public override Type ContentType => typeof(DivisionDetailCanCu);

        public bool? IsAllItemsSelected
        {
            get
            {
                var selected = Items.Select(item => item.IsSelected).Distinct().ToList();
                return selected.Count == 1 ? selected.Single() : (bool?)null;
            }
            set
            {
                if (value.HasValue)
                {
                    SelectAll(value.Value, Items);
                    OnPropertyChanged(nameof(IsAllItemsSelected));
                }
            }
        }

        // Join ,
        public List<LbChungTuCanCuModel> ChungTuSelected { get; set; }

        public DivisionDetailCanCuViewModel(IMapper mapper,
            ILog logger,
            ILbChungTuService chungTuService,
            ISessionService sessionService)
        {
            _mapper = mapper;
            _logger = logger;
            _chungTuService = chungTuService;
            _sessionService = sessionService;
        }

        public override void Init()
        {
            base.Init();
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            try
            {
                base.LoadData(args);

                int namLamViec = _sessionService.Current.YearOfWork;
                string idDonVi = _sessionService.Current.IdDonVi;
                DateTime ngayChungTu = Model.DNgayQuyetDinh.HasValue ? Model.DNgayQuyetDinh.Value : Model.DNgayChungTu.Value;
                var data = _chungTuService.FindByCondition(namLamViec, Model.Id, idDonVi);
                Items = _mapper.Map<ObservableCollection<LbChungTuCanCuModel>>(data);
                foreach (var model in Items)
                {
                    // Set selected default
                    if (ChungTuSelected != null)
                    {
                        model.IsSelected = ChungTuSelected.Any(x => x.Id == model.Id);
                    }

                    model.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == nameof(LbChungTuCanCuModel.IsSelected))
                            OnPropertyChanged(nameof(IsAllItemsSelected));
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            DialogHost.CloseDialogCommand.Execute(null, null);
            ChungTuSelected = Items.Where(x => x.IsSelected).ToList();
            SavedAction?.Invoke(ChungTuSelected);
        }

        public override void OnCancel()
        {
            base.OnCancel();
        }

        private void SelectAll(bool select, ObservableCollection<LbChungTuCanCuModel> items)
        {
            foreach (var model in items)
            {
                model.IsSelected = select;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            ChungTuSelected = null;
        }
    }
}
