using System;
using System.Collections.Generic;
using System.Text;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.Service
{
    public interface IStorageServiceFactory
    {
        public IStorageService Instance { get; }
    }
}
