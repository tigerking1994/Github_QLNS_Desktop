using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTS.QLNS.CTC.Core.Domain;

namespace VTS.QLNS.CTC.App.Model.Mapper
{
    public class TlDmDonViNq104Mapper : Profile
    {
        public TlDmDonViNq104Mapper()

        {
            CreateMap<TlDmDonViNq104Model, TlDmDonViNq104>();
            CreateMap<TlDmDonViNq104, TlDmDonViNq104Model>();
        }
    }
}
