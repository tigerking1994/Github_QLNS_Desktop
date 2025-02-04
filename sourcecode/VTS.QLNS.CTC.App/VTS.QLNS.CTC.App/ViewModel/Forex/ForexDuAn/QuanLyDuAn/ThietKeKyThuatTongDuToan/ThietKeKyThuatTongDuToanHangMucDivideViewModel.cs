using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.Forex.ForexDuAn.QuanLyDuAn.ThietKeKyThuatTongDuToan;

namespace VTS.QLNS.CTC.App.ViewModel.Forex.ForexDuAn.QuanLyDuAn.ThietKeKyThuatTongDuToan
{
    public class ThietKeKyThuatTongDuToanHangMucDivideViewModel : DetailViewModelBase<VdtDuToanModel, DuToanDetailModel>
    {
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler ClosePopup;

        public override string Name => "THIẾT KẾ KỸ THUẬT VÀ TỔNG DỰ TOÁN CHI TIẾT";
        public override string Title => "Quản lý thiết kế kỹ thuật và tổng dự toán";
        public override Type ContentType => typeof(ThietKeKyThuatTongDuToanHangMucDivide);

        //private ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> _dataDuToanHangMucParent;
        //public ObservableCollection<VTS.QLNS.CTC.App.Model.DuToanDetailModel> DataDuToanHangMucParent
        //{
        //    get => _dataDuToanHangMucParent;
        //    set => SetProperty(ref _dataDuToanHangMucParent, value);
        //}

        private List<NhDaDuToanHangMucModel> _dataDuToanHangMucParent;
        public List<NhDaDuToanHangMucModel> DataDuToanHangMucParent
        {
            get => _dataDuToanHangMucParent;
            set => SetProperty(ref _dataDuToanHangMucParent, value);
        }

        private DuToanDetailModel _duToanHangMucPhanChiaSelected;
        public DuToanDetailModel DuToanHangMucPhanChiaSelected
        {
            get => _duToanHangMucPhanChiaSelected;
            set => SetProperty(ref _duToanHangMucPhanChiaSelected, value);
        }

        //private bool _selectAllHangMucPhanChia;
        //public bool SelectAllHangMucPhanChia
        //{
        //    get => (DataDuToanHangMucParent == null || !DataDuToanHangMucParent.Any()) ? false : DataDuToanHangMucParent.All(item => item.IsChecked);
        //    set
        //    {
        //        SetProperty(ref _selectAllHangMucPhanChia, value);
        //        if (DataDuToanHangMucParent != null)
        //        {
        //            DataDuToanHangMucParent.Select(c => { c.IsChecked = _selectAllHangMucPhanChia; return c; }).ToList();
        //        }
        //    }
        //}

        public Guid HangMucPhanChiaId { get; set; }
        public string HangMucPhanChiaName { get; set; }
        public double? HangMucPhanChiaValue { get; set; }

        private double? _conLai;
        public double? ConLai
        {
            get => _conLai;
            set => SetProperty(ref _conLai, value);
        }

        public ThietKeKyThuatTongDuToanHangMucDivideViewModel()
        {
           
        }

        public override void Init()
        {
            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);
            if (DataDuToanHangMucParent.Count > 0)
            {
                foreach (var item in DataDuToanHangMucParent)
                {
                    item.PropertyChanged += DetailModel_PropertyChanged;
                }
            }
            CalculateConLaiHangMucPhanChia();
            OnPropertyChanged(nameof(DataDuToanHangMucParent));
        }

        public override void OnSave(object obj)
        {
            foreach (var item in DataDuToanHangMucParent)
            {
                item.IIdHangMucPhanChiaId = HangMucPhanChiaId;
            }
            OnPropertyChanged(nameof(DataDuToanHangMucParent));
            SavedAction?.Invoke(null);
            System.Windows.Window window = obj as System.Windows.Window;
            window.Close();
        }

        private void DetailModel_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            DuToanDetailModel objectSender = (DuToanDetailModel)sender;
            if (DuToanHangMucPhanChiaSelected == null)
            {
                return;
            }
            if (args.PropertyName == nameof(DuToanDetailModel.FGiaTriPhanChia))
            {
                CalculateConLaiHangMucPhanChia();
                if (objectSender.FGiaTriPhanChia > HangMucPhanChiaValue || ConLai < 0)
                {
                    MessageBox.Show(Resources.MsgErrHangMucPhanChiaVuotQuaGTTKTC, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    objectSender.FGiaTriPhanChia = 0;
                    return ;
                }
            }

            //objectSender.IsModified = true;
            OnPropertyChanged(nameof(DataDuToanHangMucParent));
        }

        private void CalculateConLaiHangMucPhanChia()
        {
            //double tongHangMuc = 0;

            //List<NhDaDuToanHangMucModel> listHangMuc = DataDuToanHangMucParent.Where(x => x.FGiaTriPhanChia > 0 && x.IsChecked).ToList();
            //ConLai = 0;
            //if (listHangMuc != null && listHangMuc.Count > 0)
            //{
            //    tongHangMuc = listHangMuc.Sum(x => x.FGiaTriPhanChia.Value);
            //}
            //ConLai = HangMucPhanChiaValue - tongHangMuc;
        }
    }
}
