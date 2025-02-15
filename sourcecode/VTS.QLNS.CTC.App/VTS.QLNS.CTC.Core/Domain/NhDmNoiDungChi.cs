﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VTS.QLNS.CTC.Core.Domain
{
    public partial class NhDmNoiDungChi : EntityBase
    {
        public override Guid Id { get; set; }
        public string SMaNoiDungChi { get; set; }
        public string STenNoiDungChi { get; set; }
        public string SMoTa { get; set; }
    }
}
