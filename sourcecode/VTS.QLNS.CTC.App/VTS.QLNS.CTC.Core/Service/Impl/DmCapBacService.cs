using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class DmCapBacService : IService<DanhMuc>, IDmCapBacService
    {
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly IDmChuKyService _danhMucChuKyService;

        public DmCapBacService(IDanhMucRepository danhMucRepository, IDmChuKyService dmChuKyService)
        {
            _danhMucRepository = danhMucRepository;
            _danhMucChuKyService = dmChuKyService;
        }

        public override void AddOrUpdateRange(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SNguoiSua = authenticationInfo.Principal;
                    }
                }

                if (item.SType == DanhMucChuKy.TEN)
                {
                    var predicateInner = PredicateBuilder.False<DmChuKy>();
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten1);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten2);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten3);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten4);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten5);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten6);
                    var listDanhMucChuKy = _danhMucChuKyService.FindByCondition(predicateInner).ToList();
                    listDanhMucChuKy.ForEach(x =>
                    {
                        if (x.Ten1 == item.IIDMaDanhMuc) x.Ten1MoTa = item.SGiaTri;
                        if (x.Ten2 == item.IIDMaDanhMuc) x.Ten2MoTa = item.SGiaTri;
                        if (x.Ten3 == item.IIDMaDanhMuc) x.Ten3MoTa = item.SGiaTri;
                        if (x.Ten4 == item.IIDMaDanhMuc) x.Ten4MoTa = item.SGiaTri;
                        if (x.Ten5 == item.IIDMaDanhMuc) x.Ten5MoTa = item.SGiaTri;
                        if (x.Ten6 == item.IIDMaDanhMuc) x.Ten6MoTa = item.SGiaTri;
                    });
                    _danhMucChuKyService.UpdateRange(listDanhMucChuKy);
                }
                else if (item.SType == DanhMucChuKy.CHUC_DANH)
                {
                    var predicateInner = PredicateBuilder.False<DmChuKy>();
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh1);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh2);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh3);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh4);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh5);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh6);
                    var listDanhMucChuKy = _danhMucChuKyService.FindByCondition(predicateInner).ToList();
                    listDanhMucChuKy.ForEach(x =>
                    {
                        if (x.ChucDanh1 == item.IIDMaDanhMuc) x.ChucDanh1MoTa = item.SGiaTri;
                        if (x.ChucDanh2 == item.IIDMaDanhMuc) x.ChucDanh2MoTa = item.SGiaTri;
                        if (x.ChucDanh3 == item.IIDMaDanhMuc) x.ChucDanh3MoTa = item.SGiaTri;
                        if (x.ChucDanh4 == item.IIDMaDanhMuc) x.ChucDanh4MoTa = item.SGiaTri;
                        if (x.ChucDanh5 == item.IIDMaDanhMuc) x.ChucDanh5MoTa = item.SGiaTri;
                        if (x.ChucDanh6 == item.IIDMaDanhMuc) x.ChucDanh6MoTa = item.SGiaTri;
                    });
                    _danhMucChuKyService.UpdateRange(listDanhMucChuKy);
                }
            }
            _danhMucRepository.AddOrUpdateRange(listEntities);
        }

        public override IEnumerable<DanhMuc> FindAll(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<DanhMuc> ChuKyChucDanh = _danhMucChuKyService.FindChuKyChucDanh();
            IEnumerable<DanhMuc> ChuKyTen = _danhMucChuKyService.FindChuKyTen();
            return ChuKyChucDanh.Concat(ChuKyTen);
        }

        public override IEnumerable<DanhMuc> FindAllNhomChuKy(AuthenticationInfo authenticationInfo)
        {
            IEnumerable<DanhMuc> ChuKyChucDanh = _danhMucChuKyService.FindNhomChuKyChucDanh();
            IEnumerable<DanhMuc> ChuKyTen = _danhMucChuKyService.FindNhomChuKyTen();
            return ChuKyChucDanh.Concat(ChuKyTen).OrderBy(x => x.IIDMaDanhMuc);
        }

        public override void AddOrUpdateRangeSignatureGroup(IEnumerable<DanhMuc> listEntities, AuthenticationInfo authenticationInfo)
        {
            var time = DateTime.Now;
            foreach (var item in listEntities)
            {
                item.INamLamViec = authenticationInfo.YearOfWork;
                if (item.IsModified)
                {
                    if (Guid.Empty.Equals(item.Id))
                    {
                        item.DNgayTao = time;
                        item.SNguoiTao = authenticationInfo.Principal;
                        item.DNgaySua = null;
                        item.SNguoiSua = null;
                    }
                    else
                    {
                        item.DNgaySua = time;
                        item.SNguoiSua = authenticationInfo.Principal;
                    }
                }

                if (item.SType == DanhMucChuKy.NHOM_TEN)
                {
                    var predicateInner = PredicateBuilder.False<DmChuKy>();
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten1);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten2);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten3);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten4);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten5);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.Ten6);
                    var listDanhMucChuKy = _danhMucChuKyService.FindByCondition(predicateInner).ToList();
                    listDanhMucChuKy.ForEach(x =>
                    {
                        if (x.Ten1 == item.IIDMaDanhMuc) x.Ten1MoTa = item.SGiaTri;
                        if (x.Ten2 == item.IIDMaDanhMuc) x.Ten2MoTa = item.SGiaTri;
                        if (x.Ten3 == item.IIDMaDanhMuc) x.Ten3MoTa = item.SGiaTri;
                        if (x.Ten4 == item.IIDMaDanhMuc) x.Ten4MoTa = item.SGiaTri;
                        if (x.Ten5 == item.IIDMaDanhMuc) x.Ten5MoTa = item.SGiaTri;
                        if (x.Ten6 == item.IIDMaDanhMuc) x.Ten6MoTa = item.SGiaTri;
                    });
                    _danhMucChuKyService.UpdateRange(listDanhMucChuKy);
                }
                else if (item.SType == DanhMucChuKy.NHOM_CHUC_DANH)
                {
                    var predicateInner = PredicateBuilder.False<DmChuKy>();
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh1);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh2);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh3);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh4);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh5);
                    predicateInner = predicateInner.Or(x => item.IIDMaDanhMuc == x.ChucDanh6);
                    var listDanhMucChuKy = _danhMucChuKyService.FindByCondition(predicateInner).ToList();
                    listDanhMucChuKy.ForEach(x =>
                    {
                        if (x.ChucDanh1 == item.IIDMaDanhMuc) x.ChucDanh1MoTa = item.SGiaTri;
                        if (x.ChucDanh2 == item.IIDMaDanhMuc) x.ChucDanh2MoTa = item.SGiaTri;
                        if (x.ChucDanh3 == item.IIDMaDanhMuc) x.ChucDanh3MoTa = item.SGiaTri;
                        if (x.ChucDanh4 == item.IIDMaDanhMuc) x.ChucDanh4MoTa = item.SGiaTri;
                        if (x.ChucDanh5 == item.IIDMaDanhMuc) x.ChucDanh5MoTa = item.SGiaTri;
                        if (x.ChucDanh6 == item.IIDMaDanhMuc) x.ChucDanh6MoTa = item.SGiaTri;
                    });
                    _danhMucChuKyService.UpdateRange(listDanhMucChuKy);
                }
            }
            _danhMucRepository.AddOrUpdateRange(listEntities);
        }
    }
}
