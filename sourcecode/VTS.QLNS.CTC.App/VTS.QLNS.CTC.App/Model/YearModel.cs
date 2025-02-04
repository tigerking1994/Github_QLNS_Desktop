using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using VTS.QLNS.CTC.App.Extensions;
using VTS.QLNS.CTC.Utility.Enum;
using VTS.QLNS.CTC.App.Model.ConvertGenericData;
using VTS.QLNS.CTC.App.Model.Import;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Service;
using VTS.QLNS.CTC.Core.Service.Impl;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.App.Model
{
    public class YearModel
    {
        public int Year { get; set; }

        public string NamThucHienDisplay => string.Concat(Year);
    }
}
