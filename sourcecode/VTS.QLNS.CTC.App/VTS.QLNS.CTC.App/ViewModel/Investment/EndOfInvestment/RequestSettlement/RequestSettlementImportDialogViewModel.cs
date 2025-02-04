using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Model.Control;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Investment.EndOfInvestment.RequestSettlement;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.ViewModel.Investment.EndOfInvestment.RequestSettlement
{


    public class RequestSettlementImportDialogViewModel : DialogViewModelBase<ChungTuDeNghiQuyetToanImportModel>
    {
        public override string Name => "Thông tin chi tiết import đề nghị quyết toán dự án hoàn thành";
        public override string Title => "Thông tin chi tiết import đề nghị quyết toán dự án hoàn thành";
        public override string Description => "Thông tin chi tiết import đề nghị quyết toán dự án hoàn thành";
        public override Type ContentType => typeof(View.Investment.EndOfInvestment.RequestSettlement.RequestSettlementImportDialog);
        public override PackIconKind IconKind => PackIconKind.Projector;

        private ObservableCollection<DeNghiQuyetToanNguonVonImportModel> _dataImport;
        public ObservableCollection<DeNghiQuyetToanNguonVonImportModel> DataImport
        {
            get => _dataImport;
            set => SetProperty(ref _dataImport, value);
        }

        private ObservableCollection<DeNghiQuyetToanChiPhiImportModel> _dataChiPhiImport;
        public ObservableCollection<DeNghiQuyetToanChiPhiImportModel> DataChiPhiImport
        {
            get => _dataChiPhiImport;
            set => SetProperty(ref _dataChiPhiImport, value);
        }

        public RequestSettlementImportDialogViewModel()
        {
        }

        public override void Init()
        {
            try
            {
                if (Model != null && Model.ListNguonVon != null && Model.ListNguonVon.Count > 0)
                {
                    DataImport = new ObservableCollection<DeNghiQuyetToanNguonVonImportModel>(Model.ListNguonVon);
                }
                if (Model != null && Model.ListChiPhi != null && Model.ListChiPhi.Count > 0)
                {
                    DataChiPhiImport = new ObservableCollection<DeNghiQuyetToanChiPhiImportModel>(Model.ListChiPhi);
                }
                OnPropertyChanged(nameof(DataImport));
                OnPropertyChanged(nameof(DataChiPhiImport));
            }
            catch (Exception ex)
            {
                //_logger.Error(ex.Message, ex);
            }
        }
    }
}
