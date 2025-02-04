using System;
using VTS.QLNS.CTC.App.Service.Impl;

namespace VTS.QLNS.CTC.App.Service
{
    public interface ISessionService
    {
        event Action StateChanged;
        SessionInfo Current { get; set; }
    }
}
