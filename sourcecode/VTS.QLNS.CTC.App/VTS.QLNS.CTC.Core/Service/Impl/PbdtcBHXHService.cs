using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Criteria;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Core.Repository.Impl;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;


namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class PbdtcBHXHService : IPbdtcBHXHService
    {
        private readonly IPbdtcBHXHRepository _iPbdtcBHXHRepository;
        public PbdtcBHXHService(IPbdtcBHXHRepository iPbdtcBHXHRepository)
        {
            _iPbdtcBHXHRepository = iPbdtcBHXHRepository;
        }

        public IEnumerable<BhPbdtcBHXH> FindByCondition(Expression<Func<BhPbdtcBHXH, bool>> predicate)
        {
            return _iPbdtcBHXHRepository.FindByCondition(predicate);
        }

        public BhPbdtcBHXH FindById(Guid Id)
        {
            return _iPbdtcBHXHRepository.Find(Id);
        }
        public int GetSoChungTuIndexByCondition(int iNamLamViec)
        {
            return _iPbdtcBHXHRepository.GetSoChungTuIndexByCondition(iNamLamViec);
        }

        public int Add(BhPbdtcBHXH item)
        {
            return _iPbdtcBHXHRepository.Add(item);
        }

        public int Update(BhPbdtcBHXH item)
        {
            return _iPbdtcBHXHRepository.Update(item);
        }
        public int Delete(BhPbdtcBHXH item)
        {
            return _iPbdtcBHXHRepository.Delete(item);
        }
        public int LockOrUnLock(Guid id, bool lockStatus)
        {
            return _iPbdtcBHXHRepository.LockOrUnLock(id, lockStatus);
        }

        public IEnumerable<BhPbdtcBHXH> FindDotNhanByChungTuPhanBo(Guid idPhanBo)
        {
            return _iPbdtcBHXHRepository.FindDotNhanByChungTuPhanBo(idPhanBo);
        }

        public IEnumerable<BhPbdtcBHXH> FindBySoQuyetDinh(string soQuyetDinh, int nam)
        {
            return _iPbdtcBHXHRepository.FindBySoQuyetDinh(soQuyetDinh, nam);
        }

        public IEnumerable<BhPbdtcBHXH> FindByLuyKeDot(DateTime? ngayQuyetDinh, int nam)
        {
            return _iPbdtcBHXHRepository.FindByLuyKeDot(ngayQuyetDinh, nam);
        }

        public List<DonVi> FindByDonViForNamLamViec(int namLamViec, Guid? IDLoaiChi, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh, int dotNhan)
        {
            return _iPbdtcBHXHRepository.FindByDonViForNamLamViec(namLamViec, IDLoaiChi, sMaLoaiChi, sSoQuyetDinh, sNgayQuyetDinh,dotNhan);
        }

        public List<DonVi> FindByDonViForInLuyKeNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh,int dotNhan)
        {
            return _iPbdtcBHXHRepository.FindByDonViForInLuyKeNamLamViec(yearOfWork, id, sMaLoaiChi, sSoQuyetDinh, sNgayQuyetDinh, dotNhan);
        }
        public IEnumerable<BhPbdtcBHXhQuery> GetDanhSachPhanBoDuToanChi(int iNamLamViec)
        {
            return _iPbdtcBHXHRepository.GetDanhSachPhanBoDuToanChi(iNamLamViec);
        }

        public List<DonVi> FindByDonViForNamLamViecNormal(int yearOfWork, Guid id, string sMaLoaiChi, string lstIDChungTu, int dotNhan)
        {
            return _iPbdtcBHXHRepository.FindByDonViForNamLamViecNormal(yearOfWork, id, sMaLoaiChi, lstIDChungTu, dotNhan);
        }

        public List<DonVi> FindByDonViForInDotNgayNamLamViec(int yearOfWork, Guid id, string sMaLoaiChi, string sSoQuyetDinh, string sNgayQuyetDinh,int dotNhan)
        {
            return _iPbdtcBHXHRepository.FindByDonViForInDotNgayNamLamViec(yearOfWork, id, sMaLoaiChi, sSoQuyetDinh, sNgayQuyetDinh, dotNhan);
        }

        public IEnumerable<BhPbdtcBHXH> FindByConditionLoaiChi(int yearOfWork, int dotNhan, string sMaLoaiChi)
        {
            return _iPbdtcBHXHRepository.FindByConditionLoaiChi(yearOfWork, dotNhan, sMaLoaiChi);
        }
    }
}
