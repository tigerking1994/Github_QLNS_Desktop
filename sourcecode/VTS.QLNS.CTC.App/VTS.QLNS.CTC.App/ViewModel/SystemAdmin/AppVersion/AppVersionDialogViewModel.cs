using AutoMapper;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using System.Windows.Forms;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Properties;
using VTS.QLNS.CTC.App.View.SystemAdmin.AppVersion;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;

namespace VTS.QLNS.CTC.App.ViewModel.SystemAdmin.AppVersion
{
    public class AppVersionDialogViewModel : DialogViewModelBase<AppVersionModel>
    {
        private readonly IMapper _mapper;
        private readonly IAppVersionService _appVersionService;

        public override Type ContentType => typeof(AppVersionDialog);
        public override PackIconKind IconKind => PackIconKind.CellphoneSystemUpdate;
        public override string Name => "Cập nhật phiên bản";
        public override string Description => "Cập nhật phiên bản";

        public RelayCommand UploadFileCommand { get; }
        public RelayCommand ExecuteFileCommand { get; }

        public AppVersionDialogViewModel(IAppVersionService appVersionService, IMapper mapper)
        {
            _mapper = mapper;
            _appVersionService = appVersionService;

            UploadFileCommand = new RelayCommand(obj => OnUploadFile());
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnSave()
        {
            IsLoading = true;
            BackgroundWorkerHelper.Run((s, e) =>
            {
                var entity = _mapper.Map<HtAppVersion>(Model);
                if (!Model.Id.Equals(Guid.Empty))
                {
                    _appVersionService.Update(entity);
                }
                else
                {
                    _appVersionService.Add(entity);
                }
                e.Result = _mapper.Map<AppVersionModel>(entity);
            },
            (s, e) =>
            {
                SavedAction?.Invoke(e.Result);
                IsLoading = false;
                // Invoke message
                MessageBox.Show(Resources.MsgSaveDone, Resources.NotifiTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogHost.Close("RootDialog");
            });
        }

        private void OnUploadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn tệp";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = ".exe";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Model.FileName = openFileDialog.FileName;
            FileInfo fi = new FileInfo(Model.FileName);
            Model.Filestream = File.ReadAllBytes(Model.FileName);
            Model.FileSize = fi.Length;
        }

        public override void OnClose(object obj)
        {
            DialogHost.Close("RootDialog");
        }
    }
}
