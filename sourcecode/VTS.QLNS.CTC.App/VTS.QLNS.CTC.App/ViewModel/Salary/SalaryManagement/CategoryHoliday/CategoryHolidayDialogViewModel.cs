using AutoMapper;
using log4net;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Salary.SalaryManagement.CategoryHoliday;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Salary.SalaryManagement.CategoryHoliday
{
    public class CategoryHolidayDialogViewModel : DialogViewModelBase<TlDmNgayNghiModel>
    {
        private readonly ILog _logger;
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        private readonly ITlDmNgayNghiService _service;

        public override string Name
        {
            get
            {
                if (Model.Id == Guid.Empty)
                {
                    return "THÊM NGÀY NGHỈ";
                }
                else
                {
                    return "CẬP NHẬT NGÀY NGHỈ";
                }
            }
        }
        public override string Description => "Ngày nghỉ";
        public override Type ContentType => typeof(CategoryHolidayDialog);
        public override PackIconKind IconKind => PackIconKind.Calendar;
        public bool IsEditable => Model.Id == Guid.Empty;

        private ObservableCollection<TlDmNgayNghi> _itemsNgayNghi;
        public ObservableCollection<TlDmNgayNghi> ItemsNgayNghi
        {
            get => _itemsNgayNghi;
            set => SetProperty(ref _itemsNgayNghi, value);
        }

        public CategoryHolidayDialogViewModel(
            ILog logger,
            IMapper mapper,
            ISessionService sessionService,
            ITlDmNgayNghiService iTlDmNgayNghiService)
        {
            _mapper = mapper;
            _logger = logger;
            _sessionService = sessionService;
            _service = iTlDmNgayNghiService;
        }

        public override void Init()
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public override void OnSave()
        {
            try
            {
                //if (!ValidateViewModelHelper.Validate(Model)) return;
                if (ValidationData()) return;

                TlDmNgayNghi entity;
                if (Model.Id == Guid.Empty)
                {
                    // Thêm mới
                    entity = new TlDmNgayNghi();
                    _mapper.Map(Model, entity);

                    entity.Id = Guid.NewGuid();
                    _service.Add(entity);
                    Model.Id = entity.Id;
                }
                else
                {
                    // Cập nhật
                    entity = _service.FindById(Model.Id);
                    _mapper.Map(Model, entity);
                    _service.Update(entity);
                }
                DialogHost.CloseDialogCommand.Execute(null, null);
                SavedAction?.Invoke(_mapper.Map<TlDmNgayNghiModel>(entity));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        private bool ValidationData()
        {
            StringBuilder messageBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(Model.SMaNgayNghi))
            {
                messageBuilder.AppendFormat(Resources.HolidayCodeRequired);
            }
            if (string.IsNullOrEmpty(Model.STenNgayNghi))
            {
                messageBuilder.AppendFormat(Resources.HolidayNameRequired);
            }
            if (Model.DTuNgay == null)
            {
                messageBuilder.AppendFormat(Resources.StartDateRequired);
            }
            if (Model.DDenNgay == null)
            {
                messageBuilder.AppendFormat(Resources.EndDateRequired);
            }
            if (messageBuilder.Length != 0)
            {
                MessageBox.Show(String.Join("\n", messageBuilder.ToString()), Resources.Alert);
                return true;
            }
            return false;
        }

        public override void OnClose(object obj)
        {
            try
            {
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
