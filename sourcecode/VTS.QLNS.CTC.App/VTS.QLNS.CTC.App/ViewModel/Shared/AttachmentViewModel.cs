using AutoMapper;
using MaterialDesignThemes.Wpf;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using VTS.QLNS.CTC.App.Command;
using VTS.QLNS.CTC.App.Model;
using VTS.QLNS.CTC.App.Service;
using VTS.QLNS.CTC.App.View.Shared;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.ViewModel.Shared
{
    public class AttachmentViewModel : ViewModelBase
    {
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IStorageServiceFactory _storageServiceFactory;
        private readonly IAttachmentService _attachService;

        public override string Name => "TỆP ĐÍNH KÈM";
        public override string Description => "Danh sách Tệp đính kèm";
        public override Type ContentType => typeof(AttachmentView);
        public override PackIconKind IconKind => PackIconKind.Paperclip;

        public Guid ObjectId { get; set; }
        public AttachmentEnum.Type ModuleType { get; set; }

        private ObservableCollection<AttachmentModel> _itemsAttachment;
        public ObservableCollection<AttachmentModel> ItemsAttachment
        {
            get => _itemsAttachment;
            set => SetProperty(ref _itemsAttachment, value);
        }

        private AttachmentModel _selectedAttachment;
        public AttachmentModel SelectedAttachment
        {
            get => _selectedAttachment;
            set => SetProperty(ref _selectedAttachment, value);
        }

        public RelayCommand DownloadFileCommand { get; }
        public RelayCommand DownloadAllFileCommand { get; }
        public RelayCommand ViewFileCommand { get; }

        public AttachmentViewModel(
            IMapper mapper,
            ILog logger,
            IStorageServiceFactory storageServiceFactory,
            IAttachmentService attachService)
        {
            _mapper = mapper;
            _logger = logger;
            _storageServiceFactory = storageServiceFactory;
            _attachService = attachService;

            DownloadFileCommand = new RelayCommand(obj => OnDownloadFile(), obj => SelectedAttachment != null);
            DownloadAllFileCommand = new RelayCommand(obj => OnDownloadAllFile(), obj => ItemsAttachment != null && ItemsAttachment.Count > 0);
            ViewFileCommand = new RelayCommand(obj => OnViewFile(), obj => (SelectedAttachment != null && Path.GetExtension(SelectedAttachment.FilePath).ToLower().Equals(".pdf")));
        }

        public override void Init()
        {
            base.Init();

            LoadData();
        }

        public override void LoadData(params object[] args)
        {
            base.LoadData(args);

            var data = _attachService.FindByModuleAndObjectId((int)ModuleType, ObjectId);
            _itemsAttachment = _mapper.Map<ObservableCollection<AttachmentModel>>(data);
            OnPropertyChanged(nameof(ItemsAttachment));
        }

        private void OnDownloadAllFile()
        {
            if (ItemsAttachment != null && ItemsAttachment.Count > 0)
            {
                // Dowload from storage
                _storageServiceFactory.Instance.DownloadAll(ModuleType, ObjectId);
            }
            else
            {
                // Dowload from local path
            }
        }

        private void OnDownloadFile()
        {
            if (SelectedAttachment != null)
            {
                if (SelectedAttachment.Id != Guid.Empty)
                {
                    _storageServiceFactory.Instance.Download(SelectedAttachment.Id);
                }
                else
                {
                    // Dowload from local path
                }
            }
            else
            {
                // Cần chọn file
            }
        }

        private void OnViewFile()
        {
            if (SelectedAttachment.Id != Guid.Empty)
            {
                _storageServiceFactory.Instance.View(SelectedAttachment.Id);
            }
            else
            {
                // Cần chọn file
            }
        }
    }
}
