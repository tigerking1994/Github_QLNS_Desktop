using Microsoft.Extensions.Configuration;
using VTS.QLNS.CTC.App.Helper;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.App.Service.Impl
{
    public class StorageServiceFactory : IStorageServiceFactory
    {
        protected readonly IConfiguration _configuration;
        private readonly LocalStorageService _localStorageService;
        private readonly FtpStorageService _ftpStorageService;
        private readonly IDanhMucService _danhmucService;

        public IStorageService Instance
        {
            get
            {
                var config = _danhmucService.FindByCode(STORAGE_CONFIG.STORAGE_TYPE);
                if (config != null)
                {
                    string storageType = config.SGiaTri;
                    if (StorageTypeEnum.TypeValue.FTP_SERVER.Equals(storageType))
                    {
                        return _ftpStorageService;
                    }
                }
                
                return _localStorageService;
            }
        }

        public StorageServiceFactory(
            IConfiguration configuration,
            LocalStorageService localStorageService,
            FtpStorageService ftpStorageService,
            IDanhMucService danhmucService)
        {
            _configuration = configuration;
            _localStorageService = localStorageService;
            _ftpStorageService = ftpStorageService;
            _danhmucService = danhmucService;
        }
    }
}
