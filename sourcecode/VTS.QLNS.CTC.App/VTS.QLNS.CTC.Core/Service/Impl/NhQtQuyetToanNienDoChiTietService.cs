using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;
using VTS.QLNS.CTC.Utility.Enum;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class NhQtQuyetToanNienDoChiTietService : INhQtQuyetToanNienDoChiTietService
    {
        private readonly INhQtQuyetToanNienDoChiTietRepository _repository;
        private readonly INhQtQuyetToanNienDoRepository _repositoryQTND;
        private readonly INhDmNhiemVuChiRepository _nhDmNhiemVuChiRepository;
        private readonly INhThTongHopService _nhThTongHopService;

        public NhQtQuyetToanNienDoChiTietService(
            INhQtQuyetToanNienDoChiTietRepository repository,
            INhQtQuyetToanNienDoRepository repositoryQTND,
            INhDmNhiemVuChiRepository nhDmNhiemVuChiRepository,
            INhThTongHopService nhThTongHopService)
        {
            _repository = repository;
            _repositoryQTND = repositoryQTND;
            _nhDmNhiemVuChiRepository = nhDmNhiemVuChiRepository;
            _nhThTongHopService = nhThTongHopService;
        }

        public void AddOrUpdate(IEnumerable<NhQtQuyetToanNienDoChiTiet> entities)
        {
            using (var transactionScope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                List<NhQtQuyetToanNienDoChiTiet> lstAdded = entities.Where(x => x.IsAdded && !x.IsDeleted).ToList();
                if (lstAdded.Any())
                {
                    _repository.AddRange(lstAdded);
                }

                List<NhQtQuyetToanNienDoChiTiet> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (lstModified.Any())
                {
                    _repository.UpdateRange(lstModified);
                }

                List<NhQtQuyetToanNienDoChiTiet> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (lstDeleted.Any())
                {
                    _repository.RemoveRange(lstDeleted);
                }

                transactionScope.Complete();
            }
        }

        public void DeleteQTNDChiTiet(NhQtQuyetToanNienDo entity)
        {
            var getList = _repository.GetDetailQuyetToanNienDoDetail(entity.Id, null, null);
            foreach(var item in getList)
            {
                var getDetail = _repository.Find(item.Id);
                _repository.Delete(getDetail);
            }
        }

        public IEnumerable<NhQtQuyetToanNienDoChiTiet> FindByQuyetToanNienDoId(Guid quyetToanNienDoId)
        {
            return _repository.FindAll(x => x.IIdQuyetToanNienDoId == quyetToanNienDoId);
        }

        public NhQtQuyetToanNienDoChiTiet FetchData(Guid quyetToanNienDoId, Guid hopDongId)
        {
            return _repository.FetchData(quyetToanNienDoId, hopDongId).FirstOrDefault();
        }

        public List<NhQTQuyetToanNienDoChiTietQuery> getListQTNDDetailChiTiet(Guid quyetToanNienDoId, Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND)
        {
            var qtnd = _repositoryQTND.Find(quyetToanNienDoId);
            var listData = new List<NhQTQuyetToanNienDoChiTietQuery>();
            var listResult = new List<NhQTQuyetToanNienDoChiTietQuery>();
            var listDetail = _repository.GetDetailQuyetToanNienDoDetail(quyetToanNienDoId, donViTinhUSD, donViTinhUSD).ToList();

            if (listDetail.Any())
            {
                listData = listDetail;
            }
            else
            {
                listData = _repository.GetDetailQuyetToanNienDoCreate(donViId, nam, donViTinhUSD, donViTinhUSD).ToList();
            }
            var dataTongHopHandler = LoadDataNhTongHop(nam ?? 0, listData);
            var listTitle = listData.Where(x => x.IIdParentId != null).ToList();
            var getAllChuongTrinh = listData.Where(x => x.IIdKhttNhiemVuChiId != null && x.IIdParentId == null)
                .Select(x => new
                {
                    x.STenNhiemVuChi,
                    x.IIdKhttNhiemVuChiId,
                    x.FKeHoachTtcpUsd,
                    x.FKeHoachBqpUsd,
                    x.IsData,

                }).OrderBy(x => x.STenNhiemVuChi).Distinct().ToList();
            var getAllChuongTrinhNoiDungChi = listData.Where(x => x.IIdKhttNhiemVuChiId != null && x.IIdParentId == null)
                .Select(x => new
                {
                    x.IIdKhttNhiemVuChiId,
                    x.ILoaiNoiDungChi,
                    x.IsData,

                }).OrderBy(x => x.IIdKhttNhiemVuChiId).Distinct().ToList();
            var iCountChuongTrinh = 0;
            double? fKeHoachBqpUsd = 0, fQtKinhPhiDuocCapTongSoUsd = 0;
            foreach (var chuongTrinh in getAllChuongTrinh)
            {
                iCountChuongTrinh++;

                //var lstDataChuongTrinhTH = listData.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId)
                var khttNoiDungChi = getAllChuongTrinhNoiDungChi.Where(x => x.IIdKhttNhiemVuChiId.Equals(chuongTrinh.IIdKhttNhiemVuChiId));
                dataTongHopHandler.ForAll(x =>
                {
                    if (x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId)
                    {
                        if (khttNoiDungChi.Count() == 1)
                            x.ILoaiNoiDungChi = khttNoiDungChi.FirstOrDefault().ILoaiNoiDungChi;
                    }
                });
                var lstDataChuongTrinhTH = dataTongHopHandler.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId)
                    .Select(x => new
                    {
                        x.IIdKhttNhiemVuChiId,
                        x.IIdHopDongId,
                        x.IID_DuAnID,
                        x.ILoaiNoiDungChi,
                        x.FQtKinhPhiDuyetCacNamTruocVnd,
                        x.FQtKinhPhiDuyetCacNamTruocUsd,
                        x.FQtKinhPhiDuocCapNamNayUsd,
                        x.FQtKinhPhiDuocCapNamNayVnd,
                        x.FLuyKeKinhPhiDuocCapVnd,
                        x.FLuyKeKinhPhiDuocCapUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                    }).Distinct().ToList();
                var lstDataChuongTrinh = listData.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId).ToList();
                if (khttNoiDungChi.Count() == 1)
                {
                    if (khttNoiDungChi.First().ILoaiNoiDungChi == (int)LoaiNoiDungChi.Type.CHI_BANG_NOI_TE)
                    {
                        lstDataChuongTrinh.ForAll(x =>
                        {
                            x.FQtKinhPhiDuocCapNamNayUsd = 0;
                        });
                    }
                    else
                    {
                        lstDataChuongTrinh.ForAll(x =>
                        {
                            x.FQtKinhPhiDuocCapNamNayVnd = 0;

                        });
                    }
                }

                var newObj = new NhQTQuyetToanNienDoChiTietQuery()
                {
                    STT = ConvertLetter(iCountChuongTrinh),
                    STenNoiDungChi = chuongTrinh.STenNhiemVuChi,
                    SLevel = "0",
                    IIdKhttNhiemVuChiId = chuongTrinh.IIdKhttNhiemVuChiId,
                    FKeHoachTtcpUsd = chuongTrinh.FKeHoachTtcpUsd,
                    FKeHoachBqpUsd = chuongTrinh.FKeHoachBqpUsd,
                    IsData = false,
                    IIdTiGiaId = qtnd.IIdTiGiaId,
                    IsParent = true,
                    FQtKinhPhiDuyetCacNamTruocVnd = lstDataChuongTrinhTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                    FQtKinhPhiDuyetCacNamTruocUsd = lstDataChuongTrinhTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                    FQtKinhPhiDuocCapNamTruocChuyenSangVnd = lstDataChuongTrinh.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                    FQtKinhPhiDuocCapNamTruocChuyenSangUsd = lstDataChuongTrinh.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                    FQtKinhPhiDuocCapNamNayVnd = lstDataChuongTrinhTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                    FQtKinhPhiDuocCapNamNayUsd = lstDataChuongTrinhTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),
                    FQtKinhPhiDuocCapTongSoVnd = lstDataChuongTrinh.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                    FQtKinhPhiDuocCapTongSoUsd = lstDataChuongTrinh.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                    FDeNghiQtNamNayVnd = lstDataChuongTrinh.Sum(x => x.FDeNghiQtNamNayVnd),
                    FDeNghiQtNamNayUsd = lstDataChuongTrinh.Sum(x => x.FDeNghiQtNamNayUsd),
                    FDeNghiChuyenNamSauVnd = lstDataChuongTrinh.Sum(x => x.FDeNghiChuyenNamSauVnd),
                    FDeNghiChuyenNamSauUsd = lstDataChuongTrinh.Sum(x => x.FDeNghiChuyenNamSauUsd),
                    FThuaThieuKinhPhiTrongNamVnd = lstDataChuongTrinh.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                    FThuaThieuKinhPhiTrongNamUsd = lstDataChuongTrinh.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                    FThuaNopNsnnVnd = lstDataChuongTrinh.Sum(x => x.FThuaNopNsnnVnd),
                    FThuaNopNsnnUsd = lstDataChuongTrinh.Sum(x => x.FThuaNopNsnnUsd),
                    FLuyKeKinhPhiDuocCapVnd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                    FLuyKeKinhPhiDuocCapUsd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                    FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),//24
                    FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),//25
                    FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),//26
                    FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstDataChuongTrinhTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),//27
                    FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = lstDataChuongTrinh.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                    FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = lstDataChuongTrinh.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                    FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = lstDataChuongTrinh.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                    FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = lstDataChuongTrinh.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                };
                fKeHoachBqpUsd = newObj.FKeHoachBqpUsd == null ? 0 : newObj.FKeHoachBqpUsd;
                fQtKinhPhiDuocCapTongSoUsd = newObj.FQtKinhPhiDuocCapTongSoUsd == null ? 0 : newObj.FQtKinhPhiDuocCapTongSoUsd;
                newObj.FKeHoachChuaGiaiNganUsd = (fKeHoachBqpUsd - fQtKinhPhiDuocCapTongSoUsd) == 0 ? null : (fKeHoachBqpUsd - fQtKinhPhiDuocCapTongSoUsd);
                listResult.Add(newObj);
                var getListDuAn = lstDataChuongTrinh.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID != null).ToList();
                var getListHopDong = lstDataChuongTrinh.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID == null && x.IIdHopDongId != null).ToList();
                var getListNone = qtnd.ILoaiQuyetToan == 1
                    ? lstDataChuongTrinh.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID == null && x.IIdHopDongId == null).ToList()
                    : new List<NhQTQuyetToanNienDoChiTietQuery>();
                var iCountDuAn = 0;

                if (getListDuAn.Any())
                {
                    var getNameDuAn = getListDuAn
                        .Select(x => new
                        {
                            x.STenDuAn,
                            x.IID_DuAnID,
                            x.FHopDongDuAnVnd,
                            x.FHopDongDuAnUsd,
                            x.IsData,
                            x.IIdMlnsId,
                            x.IIdMucLucNganSachId,
                            x.FLuyKeKinhPhiDuocCapUsd,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                            x.FQtKinhPhiDuocCapNamNayUsd,
                            x.FQtKinhPhiDuocCapNamNayVnd,
                        }
                        ).Distinct().ToList();
                    foreach (var hopDongDuAn in getNameDuAn)
                    {
                        iCountDuAn++;
                        var listDuAn = getListDuAn.Where(x => x.IID_DuAnID == hopDongDuAn.IID_DuAnID).ToList();
                        //var lstDuAnTH = listData.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID == hopDongDuAn.IID_DuAnID)
                        var lstDuAnTH = dataTongHopHandler.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID == hopDongDuAn.IID_DuAnID)
                            .Select(x => new
                            {
                                x.IIdKhttNhiemVuChiId,
                                x.IIdHopDongId,
                                x.IID_DuAnID,
                                x.IIdMlnsId,
                                x.IIdMucLucNganSachId,
                                x.ILoaiNoiDungChi,
                                x.FQtKinhPhiDuyetCacNamTruocVnd,
                                x.FQtKinhPhiDuyetCacNamTruocUsd,
                                x.FLuyKeKinhPhiDuocCapVnd,
                                x.FLuyKeKinhPhiDuocCapUsd,
                                x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                                x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                                x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                                x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                                x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                                x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                                x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                                x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                                x.FQtKinhPhiDuocCapNamNayUsd,
                                x.FQtKinhPhiDuocCapNamNayVnd,
                            }).Distinct().ToList();
                        var newObjHopDongDuAn = new NhQTQuyetToanNienDoChiTietQuery()
                        {
                            STT = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString())),
                            STenNoiDungChi = hopDongDuAn.STenDuAn,
                            FHopDongVnd = hopDongDuAn.FHopDongDuAnVnd,
                            FHopDongUsd = hopDongDuAn.FHopDongDuAnUsd,
                            SLevel = "1",
                            ISum = 1,
                            IIdParentId = hopDongDuAn.IID_DuAnID,
                            IIdKhttNhiemVuChiId = chuongTrinh.IIdKhttNhiemVuChiId,
                            IID_DuAnID = hopDongDuAn.IID_DuAnID,
                            IIdTiGiaId = qtnd.IIdTiGiaId,
                            IsData = false,
                            BIsSaveTongHop = false,
                            IIdMlnsId = hopDongDuAn.IIdMlnsId,
                            IIdMucLucNganSachId = hopDongDuAn.IIdMucLucNganSachId,
                            FQtKinhPhiDuyetCacNamTruocVnd = lstDuAnTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                            FQtKinhPhiDuyetCacNamTruocUsd = lstDuAnTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                            FQtKinhPhiDuocCapNamTruocChuyenSangVnd = listDuAn.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                            FQtKinhPhiDuocCapNamTruocChuyenSangUsd = listDuAn.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                            FQtKinhPhiDuocCapNamNayVnd = lstDuAnTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                            FQtKinhPhiDuocCapNamNayUsd = lstDuAnTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),
                            FQtKinhPhiDuocCapTongSoVnd = listDuAn.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                            FQtKinhPhiDuocCapTongSoUsd = listDuAn.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                            FDeNghiQtNamNayVnd = listDuAn.Sum(x => x.FDeNghiQtNamNayVnd),
                            FDeNghiQtNamNayUsd = listDuAn.Sum(x => x.FDeNghiQtNamNayUsd),
                            FDeNghiChuyenNamSauVnd = listDuAn.Sum(x => x.FDeNghiChuyenNamSauVnd),
                            FDeNghiChuyenNamSauUsd = listDuAn.Sum(x => x.FDeNghiChuyenNamSauUsd),
                            FThuaThieuKinhPhiTrongNamVnd = listDuAn.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                            FThuaThieuKinhPhiTrongNamUsd = listDuAn.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                            FThuaNopNsnnVnd = listDuAn.Sum(x => x.FThuaNopNsnnVnd),
                            FThuaNopNsnnUsd = listDuAn.Sum(x => x.FThuaNopNsnnUsd),
                            FLuyKeKinhPhiDuocCapVnd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                            FLuyKeKinhPhiDuocCapUsd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstDuAnTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),
                            FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = lstDuAnTH.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                            FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = lstDuAnTH.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                            FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = lstDuAnTH.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                            FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = lstDuAnTH.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                        };
                        listResult.Add(newObjHopDongDuAn);
                        listResult.AddRange(ReturnLoaiChi(qtnd.IIdTiGiaId, chuongTrinh.IIdKhttNhiemVuChiId, hopDongDuAn.IID_DuAnID, true, false, listDuAn, listTitle, dataTongHopHandler));

                    }
                }
                if (getListHopDong.Any())
                {
                    iCountDuAn++;
                    //var getThisList = listTitle.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId && x.IID_DuAnID == null && x.IIdHopDongId != null).ToList();

                    var lstHopDongTH = dataTongHopHandler.Where(x => x.IIdKhttNhiemVuChiId == chuongTrinh.IIdKhttNhiemVuChiId).Select(x => new
                    {
                        x.IIdKhttNhiemVuChiId,
                        x.IIdHopDongId,
                        x.IID_DuAnID,
                        x.IIdMlnsId,
                        x.IIdMucLucNganSachId,
                        x.ILoaiNoiDungChi,
                        x.FQtKinhPhiDuyetCacNamTruocVnd,
                        x.FQtKinhPhiDuyetCacNamTruocUsd,
                        x.FLuyKeKinhPhiDuocCapVnd,
                        x.FLuyKeKinhPhiDuocCapUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                        x.FQtKinhPhiDuocCapNamNayUsd,
                        x.FQtKinhPhiDuocCapNamNayVnd,
                    }).Distinct().ToList();
                    var newObjHopDong = new NhQTQuyetToanNienDoChiTietQuery()
                    {
                        STT = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString())),
                        STenNoiDungChi = "Chi hợp đồng",
                        SLevel = "1",
                        ISum = 1,
                        IsChiKhac = false,
                        IIdKhttNhiemVuChiId = chuongTrinh.IIdKhttNhiemVuChiId,
                        IsData = false,
                        IIdTiGiaId = qtnd.IIdTiGiaId,
                        FQtKinhPhiDuyetCacNamTruocVnd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                        FQtKinhPhiDuyetCacNamTruocUsd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                        FQtKinhPhiDuocCapNamTruocChuyenSangVnd = getListHopDong.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                        FQtKinhPhiDuocCapNamTruocChuyenSangUsd = getListHopDong.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                        FQtKinhPhiDuocCapNamNayVnd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                        FQtKinhPhiDuocCapNamNayUsd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),
                        FQtKinhPhiDuocCapTongSoVnd = getListHopDong.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                        FQtKinhPhiDuocCapTongSoUsd = getListHopDong.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                        FDeNghiQtNamNayVnd = getListHopDong.Sum(x => x.FDeNghiQtNamNayVnd),
                        FDeNghiQtNamNayUsd = getListHopDong.Sum(x => x.FDeNghiQtNamNayUsd),
                        FDeNghiChuyenNamSauVnd = getListHopDong.Sum(x => x.FDeNghiChuyenNamSauVnd),
                        FDeNghiChuyenNamSauUsd = getListHopDong.Sum(x => x.FDeNghiChuyenNamSauUsd),
                        FThuaThieuKinhPhiTrongNamVnd = getListHopDong.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                        FThuaThieuKinhPhiTrongNamUsd = getListHopDong.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                        FThuaNopNsnnVnd = getListHopDong.Sum(x => x.FThuaNopNsnnVnd),
                        FThuaNopNsnnUsd = getListHopDong.Sum(x => x.FThuaNopNsnnUsd),
                        FLuyKeKinhPhiDuocCapVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                        FLuyKeKinhPhiDuocCapUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),
                        FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = getListHopDong.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                        FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = getListHopDong.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                        FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = getListHopDong.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                        FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = getListHopDong.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                    };
                    listResult.Add(newObjHopDong);
                    listResult.AddRange(ReturnLoaiChi(qtnd.IIdTiGiaId, chuongTrinh.IIdKhttNhiemVuChiId, null, true, true, getListHopDong, listTitle, dataTongHopHandler));
                    //
                }
                if (getListNone.Any())
                {
                    iCountDuAn++;
                    var lstNoneTH = getListNone.Select(x => new
                    {
                        x.IIdKhttNhiemVuChiId,
                        x.IIdHopDongId,
                        x.IID_DuAnID,
                        x.IIdMlnsId,
                        x.IIdMucLucNganSachId,
                        x.ILoaiNoiDungChi,
                        x.FQtKinhPhiDuyetCacNamTruocVnd,
                        x.FQtKinhPhiDuyetCacNamTruocUsd,
                        x.FLuyKeKinhPhiDuocCapVnd,
                        x.FLuyKeKinhPhiDuocCapUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                        x.FQtKinhPhiDuocCapNamNayUsd,
                        x.FQtKinhPhiDuocCapNamNayVnd,
                    }).Distinct().ToList();
                    var newObjKhac = new NhQTQuyetToanNienDoChiTietQuery()
                    {
                        STT = ConvertLaMa(Decimal.Parse(iCountDuAn.ToString())),
                        STenNoiDungChi = "Chi khác",
                        SLevel = "1",
                        ISum = 1,
                        IsChiKhac = true,
                        IIdKhttNhiemVuChiId = chuongTrinh.IIdKhttNhiemVuChiId,
                        IsData = false,
                        IIdTiGiaId = qtnd.IIdTiGiaId,
                        FQtKinhPhiDuyetCacNamTruocVnd = lstNoneTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                        FQtKinhPhiDuyetCacNamTruocUsd = lstNoneTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                        FQtKinhPhiDuocCapNamTruocChuyenSangVnd = getListNone.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                        FQtKinhPhiDuocCapNamTruocChuyenSangUsd = getListNone.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                        FQtKinhPhiDuocCapNamNayVnd = lstNoneTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                        FQtKinhPhiDuocCapNamNayUsd = lstNoneTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),//11
                        FQtKinhPhiDuocCapTongSoVnd = getListNone.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                        FQtKinhPhiDuocCapTongSoUsd = getListNone.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                        FDeNghiQtNamNayVnd = getListNone.Sum(x => x.FDeNghiQtNamNayVnd),
                        FDeNghiQtNamNayUsd = getListNone.Sum(x => x.FDeNghiQtNamNayUsd),
                        FDeNghiChuyenNamSauVnd = getListNone.Sum(x => x.FDeNghiChuyenNamSauVnd),
                        FDeNghiChuyenNamSauUsd = getListNone.Sum(x => x.FDeNghiChuyenNamSauUsd),
                        FThuaThieuKinhPhiTrongNamVnd = getListNone.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                        FThuaThieuKinhPhiTrongNamUsd = getListNone.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                        FThuaNopNsnnVnd = getListNone.Sum(x => x.FThuaNopNsnnVnd),
                        FThuaNopNsnnUsd = getListNone.Sum(x => x.FThuaNopNsnnUsd),
                        FLuyKeKinhPhiDuocCapVnd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                        FLuyKeKinhPhiDuocCapUsd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstNoneTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),
                        FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = getListNone.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                        FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = getListNone.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                        FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = getListNone.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                        FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = getListNone.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                    };
                    listResult.Add(newObjKhac);
                    listResult.AddRange(ReturnLoaiChi(qtnd.IIdTiGiaId, chuongTrinh.IIdKhttNhiemVuChiId, null, false, false, getListNone, listTitle, dataTongHopHandler));
                }
            }
            return listResult;
        }

        public bool checkListAddOrUpdateQTNDChiTiet(Guid quyetToanNienDoId, Guid? donViId, int? nam, int? donViTinhUSD, int? donViTinhVND)
        {
            var listDetail = _repository.GetDetailQuyetToanNienDoDetail(quyetToanNienDoId, donViTinhUSD, donViTinhUSD).ToList();
            if (listDetail.Any())
            {
                return true;
            }
            return false;
        }

        public List<NhQTQuyetToanNienDoChiTietQuery> LoadDataNhTongHop(int iNamDeNghi, List<NhQTQuyetToanNienDoChiTietQuery> listData)
        {
            var listIdDuAn = listData.Where(x => x.IID_DuAnID != null).Select(x => x.IID_DuAnID).ToList();
            var listIdHopDong = listData.Where(x => x.IIdHopDongId != null).Select(x => x.IIdHopDongId).ToList();
            var lstMaNguon = NHConstants.MA_TH_QTND.Split(',').ToList();
            lstMaNguon.Select(x => x.Trim()).ToList();
            var predicate = PredicateBuilder.True<NHTHTongHop>();
            predicate = predicate.And(x => x.INamKeHoach == iNamDeNghi || x.INamKeHoach == iNamDeNghi - 1);
            predicate = predicate.And(x => lstMaNguon.Contains(x.SMaNguon));
            predicate = predicate.Or(x => lstMaNguon.Contains(x.SMaNguonCha));
            if (listIdDuAn.Any())
            {
                predicate = predicate.And(x => listIdDuAn.Contains(x.IIdDuAnId));
            }
            if (listIdHopDong.Any())
            {
                predicate = predicate.And(x => listIdHopDong.Contains(x.IIdHopDongId));
            }
            return CalculateDataTongHop(_nhThTongHopService.FindByCondition(predicate), iNamDeNghi);
        }

        private List<NhQTQuyetToanNienDoChiTietQuery> CalculateDataTongHop(IEnumerable<NHTHTongHop> listData, int iNamKeHoach)
        {
            List<NhQTQuyetToanNienDoChiTietQuery> listDataHanders = new List<NhQTQuyetToanNienDoChiTietQuery>();

            if (listData.Any())
            {
                var listHopDong = listData.Where(x => x.IIdHopDongId != null).GroupBy(g => g.IIdHopDongId).Select(x => x.First());
                var listDuAn = listData.Where(x => x.IIdDuAnId != null && !listHopDong.Where(x => x.IIdDuAnId != null || x.IIdDuAnId != Guid.Empty).Select(x => x.IIdDuAnId).Contains(x.IIdDuAnId)).GroupBy(x => x.IIdDuAnId).Select(x => x.First());
                var lstMaNguonPlus = new List<string>();
                var lstMaNguonMinus = new List<string>();
                var lstMaNguon = new List<string>();
                foreach (var item in listHopDong)
                {
                    var dataHandlerQtnd = new NhQTQuyetToanNienDoChiTietQuery();
                    var dataChiTiet = listData.Where(x => x.IIdHopDongId == item.IIdHopDongId);
                    // col 5,6
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_301, NhTongHopConstants.MA_304 };
                    var dataCol5 = dataChiTiet.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataCol5Usd = dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataCol6Vnd = dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // col 9,10
                    var dataCol9 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_305 || x.SMaNguonCha == NhTongHopConstants.MA_305) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataCol9Usd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriUsd);
                    var dataCol9Vnd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriVnd);

                    // col 11
                    lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                    //data plus 11
                    var dataPlus = dataChiTiet.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus11Usd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlus11Vnd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // data minus 11
                    var dataMinus = dataChiTiet.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataMinus11Usd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinus12Vnd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataCol11Usd = dataPlus11Usd - dataMinus11Usd - dataCol9Usd;
                    var dataCol12Vnd = dataPlus11Vnd - dataMinus12Vnd - dataCol9Vnd;

                    //Col 21 = col11 + nguon(306)n-1
                    var dataCol21 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataPlus21Usd = dataCol21.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) - dataCol21.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd);
                    var dataPlus21Vnd = dataCol21.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) - dataCol21.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd);
                    var dataCol21Usd = dataCol11Usd + dataPlus21Usd;
                    var dataCol22Vnd = dataCol12Vnd + dataPlus21Vnd;

                    //Col 24,25
                    lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_141, NhTongHopConstants.MA_142, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    var data24Plus = dataChiTiet.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus24Usd = data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlus25Vnd = data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // data minus == minus11
                    var dataCol24Usd = dataPlus24Usd - dataMinus11Usd;
                    var dataCol25Vnd = dataPlus25Vnd - dataMinus12Vnd;
                    //Col 26,27 = nguon 308(n-1) + 307(n)
                    var dataNguon308 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataPlus308Usd = dataNguon308.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd) - dataNguon308.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd);
                    var dataPlus308Vnd = dataNguon308.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd) - dataNguon308.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd);
                    var dataNguon307 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_307 || x.SMaNguonCha == NhTongHopConstants.MA_307) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus307Usd = dataNguon307.Where(x => x.SMaNguon == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriUsd) - dataNguon307.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriUsd);
                    var dataPlus307Vnd = dataNguon307.Where(x => x.SMaNguon == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriVnd) - dataNguon307.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriVnd);
                    var dataCol26Usd = dataPlus308Usd + dataPlus307Usd;
                    var dataCol27Vnd = dataPlus308Vnd + dataPlus307Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuyetCacNamTruocUsd = dataCol5Usd;
                    dataHandlerQtnd.FQtKinhPhiDuyetCacNamTruocVnd = dataCol6Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamTruocChuyenSangUsd = dataCol9Usd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamTruocChuyenSangVnd = dataCol9Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamNayUsd = dataCol11Usd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamNayVnd = dataCol12Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapUsd = dataCol21Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapVnd = dataCol22Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapVnd = dataCol22Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = dataCol24Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = dataCol25Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = dataCol26Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = dataCol27Vnd;
                    dataHandlerQtnd.Id = item.Id;
                    dataHandlerQtnd.IIdHopDongId = item.IIdHopDongId;
                    dataHandlerQtnd.IID_DuAnID = item.IIdDuAnId;
                    dataHandlerQtnd.IIdKhttNhiemVuChiId = item.IIDKHTTNhiemVuChiID;
                    dataHandlerQtnd.IIdQuyetToanNienDoId = item.IIdChungTu;
                    //dataHandlerQtnd.ILoaiNoiDungChi = item.lo;


                    //dataHandlerQtnd.
                    listDataHanders.Add(dataHandlerQtnd);

                }

                foreach (var item in listDuAn)
                {
                    var dataHandlerQtnd = new NhQTQuyetToanNienDoChiTietQuery();
                    var dataChiTiet = listData.Where(x => x.IIdDuAnId == item.IIdDuAnId);
                    // col 5,6
                    lstMaNguon = new List<string> { NhTongHopConstants.MA_301, NhTongHopConstants.MA_304 };
                    var dataCol5 = dataChiTiet.Where(x => (lstMaNguon.Contains(x.SMaNguon) || lstMaNguon.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataCol5Usd = dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataCol6Vnd = dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataCol5.Where(x => lstMaNguon.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // col 9,10
                    var dataCol9 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_305 || x.SMaNguonCha == NhTongHopConstants.MA_305) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataCol9Usd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriUsd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriUsd);
                    var dataCol9Vnd = dataCol9.Where(x => x.SMaNguon == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriVnd) - dataCol9.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_305).Sum(x => x.FGiaTriVnd);

                    // col 11
                    lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_101, NhTongHopConstants.MA_102, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    lstMaNguonMinus = new List<string> { NhTongHopConstants.MA_131, NhTongHopConstants.MA_132 };
                    //data plus 11
                    var dataPlus = dataChiTiet.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus11Usd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlus11Vnd = dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataPlus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // data minus 11
                    var dataMinus = dataChiTiet.Where(x => (lstMaNguonMinus.Contains(x.SMaNguon) || lstMaNguonMinus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataMinus11Usd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataMinus12Vnd = dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - dataMinus.Where(x => lstMaNguonMinus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    var dataCol11Usd = dataPlus11Usd - dataMinus11Usd - dataCol9Usd;
                    var dataCol12Vnd = dataPlus11Vnd - dataMinus12Vnd - dataCol9Vnd;

                    //Col 21 = dataPlus 11 + nguon(306)n-1 - col9
                    var dataCol21 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_306 || x.SMaNguonCha == NhTongHopConstants.MA_306) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataPlus21Usd = dataCol21.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd) - dataCol21.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriUsd);
                    var dataPlus21Vnd = dataCol21.Where(x => x.SMaNguon == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd) - dataCol21.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_306).Sum(x => x.FGiaTriVnd);
                    var dataCol21Usd = dataPlus11Usd + dataPlus21Usd - dataCol9Usd;
                    var dataCol22Vnd = dataPlus11Vnd + dataPlus21Vnd - dataCol9Vnd;

                    //Col 24,25
                    lstMaNguonPlus = new List<string> { NhTongHopConstants.MA_306, NhTongHopConstants.MA_142, NhTongHopConstants.MA_111, NhTongHopConstants.MA_112, NhTongHopConstants.MA_121, NhTongHopConstants.MA_122 };
                    var data24Plus = dataChiTiet.Where(x => (lstMaNguonPlus.Contains(x.SMaNguon) || lstMaNguonPlus.Contains(x.SMaNguonCha)) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus24Usd = data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriUsd) - data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriUsd);
                    var dataPlus25Vnd = data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguon)).Sum(x => x.FGiaTriVnd) - data24Plus.Where(x => lstMaNguonPlus.Contains(x.SMaNguonCha)).Sum(x => x.FGiaTriVnd);
                    // data minus == minus11
                    var dataCol24Usd = dataPlus24Usd - dataMinus11Usd;
                    var dataCol25Vnd = dataPlus25Vnd - dataMinus12Vnd;
                    //Col 26,27 = nguon 308(n-1) + 307(n)
                    var dataNguon308 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_308 || x.SMaNguonCha == NhTongHopConstants.MA_308) && x.INamKeHoach == iNamKeHoach - 1);
                    var dataPlus308Usd = dataNguon308.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd) - dataNguon308.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriUsd);
                    var dataPlus308Vnd = dataNguon308.Where(x => x.SMaNguon == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd) - dataNguon308.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_308).Sum(x => x.FGiaTriVnd);
                    var dataNguon307 = dataChiTiet.Where(x => (x.SMaNguon == NhTongHopConstants.MA_307 || x.SMaNguonCha == NhTongHopConstants.MA_307) && x.INamKeHoach == iNamKeHoach);
                    var dataPlus307Usd = dataNguon307.Where(x => x.SMaNguon == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriUsd) - dataNguon307.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriUsd);
                    var dataPlus307Vnd = dataNguon307.Where(x => x.SMaNguon == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriVnd) - dataNguon307.Where(x => x.SMaNguonCha == NhTongHopConstants.MA_307).Sum(x => x.FGiaTriVnd);
                    var dataCol26Usd = dataPlus308Usd + dataPlus307Usd;
                    var dataCol27Vnd = dataPlus308Vnd + dataPlus307Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuyetCacNamTruocUsd = dataCol5Usd;
                    dataHandlerQtnd.FQtKinhPhiDuyetCacNamTruocVnd = dataCol6Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamTruocChuyenSangUsd = dataCol9Usd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamTruocChuyenSangVnd = dataCol9Vnd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamNayUsd = dataCol11Usd;
                    dataHandlerQtnd.FQtKinhPhiDuocCapNamNayVnd = dataCol12Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapUsd = dataCol21Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapVnd = dataCol22Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDuocCapVnd = dataCol22Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = dataCol24Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = dataCol25Vnd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = dataCol26Usd;
                    dataHandlerQtnd.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = dataCol27Vnd;
                    dataHandlerQtnd.Id = item.Id;
                    dataHandlerQtnd.IIdHopDongId = item.IIdHopDongId;
                    dataHandlerQtnd.IID_DuAnID = item.IIdDuAnId;
                    dataHandlerQtnd.IIdKhttNhiemVuChiId = item.IIDKHTTNhiemVuChiID;
                    dataHandlerQtnd.IIdQuyetToanNienDoId = item.IIdChungTu;
                    listDataHanders.Add(dataHandlerQtnd);

                }

            }
            return listDataHanders;
        }

        public List<NhQTQuyetToanNienDoChiTietQuery> ReturnLoaiChi(Guid? iIdTiGiaId, Guid? idChuongTrinh, Guid? idDuAn, bool isDuAn, bool haveDuAn, List<NhQTQuyetToanNienDoChiTietQuery> list, List<NhQTQuyetToanNienDoChiTietQuery> listTittle, List<NhQTQuyetToanNienDoChiTietQuery> listTongHop)
        {
            List<NhQTQuyetToanNienDoChiTietQuery> returnData = new List<NhQTQuyetToanNienDoChiTietQuery>();
            var listLoaiChiPhi = list.Select(x => new { x.ILoaiNoiDungChi }).Distinct().OrderBy(x => x.ILoaiNoiDungChi)
                  .ToList();
            var countLoaiChiPhi = 0;
            foreach (var loaiChiPhi in listLoaiChiPhi)
            {
                countLoaiChiPhi++;
                var listChiPhi = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi).ToList();
                var lstChiPhiTH = listTongHop.Where(x => x.IIdKhttNhiemVuChiId == idChuongTrinh && x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi).Select(x => new
                {
                    x.IIdKhttNhiemVuChiId,
                    x.IIdHopDongId,
                    x.IID_DuAnID,
                    x.ILoaiNoiDungChi,
                    x.IIdMlnsId,
                    x.IIdMucLucNganSachId,
                    x.FQtKinhPhiDuyetCacNamTruocVnd,
                    x.FQtKinhPhiDuyetCacNamTruocUsd,
                    x.FLuyKeKinhPhiDuocCapVnd,
                    x.FLuyKeKinhPhiDuocCapUsd,
                    x.FQtKinhPhiDuocCapNamNayVnd,
                    x.FQtKinhPhiDuocCapNamNayUsd,
                    x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                    x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                    x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                    x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                    x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                    x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                    x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                    x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                }).Distinct().ToList();
                var newObjLoaiChiPhi = new NhQTQuyetToanNienDoChiTietQuery()
                {
                    STT = countLoaiChiPhi.ToString(),
                    SLevel = "2",
                    STenNoiDungChi = loaiChiPhi.ILoaiNoiDungChi == 1 ? "Chi ngoại tệ" : "Chi trong nước",
                    IsData = false,
                    ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi,
                    IID_DuAnID = idDuAn,
                    IIdKhttNhiemVuChiId = idChuongTrinh,
                    IIdTiGiaId = iIdTiGiaId,
                    IsChiKhac = !isDuAn,
                    FQtKinhPhiDuyetCacNamTruocVnd = lstChiPhiTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                    FQtKinhPhiDuyetCacNamTruocUsd = lstChiPhiTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                    FQtKinhPhiDuocCapNamTruocChuyenSangVnd = listChiPhi.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                    FQtKinhPhiDuocCapNamTruocChuyenSangUsd = listChiPhi.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                    FQtKinhPhiDuocCapNamNayVnd = lstChiPhiTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                    FQtKinhPhiDuocCapNamNayUsd = lstChiPhiTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),
                    FQtKinhPhiDuocCapTongSoVnd = listChiPhi.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                    FQtKinhPhiDuocCapTongSoUsd = listChiPhi.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                    FDeNghiQtNamNayVnd = listChiPhi.Sum(x => x.FDeNghiQtNamNayVnd),
                    FDeNghiQtNamNayUsd = listChiPhi.Sum(x => x.FDeNghiQtNamNayUsd),
                    FDeNghiChuyenNamSauVnd = listChiPhi.Sum(x => x.FDeNghiChuyenNamSauVnd),
                    FDeNghiChuyenNamSauUsd = listChiPhi.Sum(x => x.FDeNghiChuyenNamSauUsd),
                    FThuaThieuKinhPhiTrongNamVnd = listChiPhi.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                    FThuaThieuKinhPhiTrongNamUsd = listChiPhi.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                    FThuaNopNsnnVnd = listChiPhi.Sum(x => x.FThuaNopNsnnVnd),
                    FThuaNopNsnnUsd = listChiPhi.Sum(x => x.FThuaNopNsnnUsd),
                    FLuyKeKinhPhiDuocCapVnd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                    FLuyKeKinhPhiDuocCapUsd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                    FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),
                    FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),
                    FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),
                    FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstChiPhiTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),
                    FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = listChiPhi.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                    FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = listChiPhi.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                    FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = listChiPhi.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                    FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = listChiPhi.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                };
                returnData.Add(newObjLoaiChiPhi);

                if (isDuAn)
                {
                    var listNameHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi)
                        .Select(x => new
                        {
                            x.STenHopDong,
                            x.IIdHopDongId,
                            x.FQtKinhPhiDuyetCacNamTruocUsd,
                            x.FLuyKeKinhPhiDuocCapVnd,
                            x.FLuyKeKinhPhiDuocCapUsd,
                            x.FQtKinhPhiDuyetCacNamTruocVnd,
                            x.FHopDongVnd,
                            x.FHopDongUsd,
                            x.IsData,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                            //x.FQtKinhPhiDuocCapNamNayVnd ,
                            //x.FQtKinhPhiDuocCapNamNayUsd ,
                        })
                        .Distinct().ToList();
                    var countHopDong = 0;
                    foreach (var nameHopDong in listNameHopDong)
                    {
                        countHopDong++;
                        var listHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi && x.IIdHopDongId == nameHopDong.IIdHopDongId).ToList();
                        var lstHopDongTH = listTongHop.Where(x => x.IIdKhttNhiemVuChiId == idChuongTrinh && x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi && x.IIdHopDongId == nameHopDong.IIdHopDongId).Select(x => new
                        {
                            x.IIdKhttNhiemVuChiId,
                            x.IIdHopDongId,
                            x.IID_DuAnID,
                            x.IIdMlnsId,
                            x.IIdMucLucNganSachId,
                            x.ILoaiNoiDungChi,
                            x.FQtKinhPhiDuyetCacNamTruocVnd,
                            x.FQtKinhPhiDuyetCacNamTruocUsd,
                            x.FLuyKeKinhPhiDuocCapVnd,
                            x.FLuyKeKinhPhiDuocCapUsd,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd,
                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd,
                            x.FQtKinhPhiDuocCapNamNayVnd,
                            x.FQtKinhPhiDuocCapNamNayUsd,
                        }).Distinct().ToList();
                        var newObjHopDongDuAn = new NhQTQuyetToanNienDoChiTietQuery() 
                        {
                            STT = countLoaiChiPhi.ToString() + "." + countHopDong.ToString(),
                            STenNoiDungChi = nameHopDong.IIdHopDongId != null ? nameHopDong.STenHopDong : "Không phát sinh hợp đồng",
                            FHopDongVnd = nameHopDong.FHopDongVnd,
                            FHopDongUsd = nameHopDong.FHopDongUsd,
                            SLevel = "3",
                            ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi,
                            IIdParentId = nameHopDong.IIdHopDongId,
                            IIdKhttNhiemVuChiId = idChuongTrinh,
                            IIdHopDongId = nameHopDong.IIdHopDongId,
                            IID_DuAnID = idDuAn,
                            IsData = false,
                            BIsSaveTongHop = false,
                            IIdTiGiaId = iIdTiGiaId,
                            IID_ThanhToan_ChiTietID = listHopDong.FirstOrDefault().IID_ThanhToan_ChiTietID,

                            FQtKinhPhiDuyetCacNamTruocVnd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocVnd),
                            FQtKinhPhiDuyetCacNamTruocUsd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuyetCacNamTruocUsd),
                            FQtKinhPhiDuocCapNamTruocChuyenSangVnd = listHopDong.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangVnd),
                            FQtKinhPhiDuocCapNamTruocChuyenSangUsd = listHopDong.Sum(x => x.FQtKinhPhiDuocCapNamTruocChuyenSangUsd),
                            FQtKinhPhiDuocCapNamNayVnd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuocCapNamNayVnd),
                            FQtKinhPhiDuocCapNamNayUsd = lstHopDongTH.Sum(x => x.FQtKinhPhiDuocCapNamNayUsd),
                            FQtKinhPhiDuocCapTongSoVnd = listHopDong.Sum(x => x.FQtKinhPhiDuocCapTongSoVnd),
                            FQtKinhPhiDuocCapTongSoUsd = listHopDong.Sum(x => x.FQtKinhPhiDuocCapTongSoUsd),
                            FDeNghiQtNamNayVnd = listHopDong.Sum(x => x.FDeNghiQtNamNayVnd),
                            FDeNghiQtNamNayUsd = listHopDong.Sum(x => x.FDeNghiQtNamNayUsd),
                            FDeNghiChuyenNamSauVnd = listHopDong.Sum(x => x.FDeNghiChuyenNamSauVnd),
                            FDeNghiChuyenNamSauUsd = listHopDong.Sum(x => x.FDeNghiChuyenNamSauUsd),
                            FThuaThieuKinhPhiTrongNamVnd = listHopDong.Sum(x => x.FThuaThieuKinhPhiTrongNamVnd),
                            FThuaThieuKinhPhiTrongNamUsd = listHopDong.Sum(x => x.FThuaThieuKinhPhiTrongNamUsd),
                            FThuaNopNsnnVnd = listHopDong.Sum(x => x.FThuaNopNsnnVnd),
                            FThuaNopNsnnUsd = listHopDong.Sum(x => x.FThuaNopNsnnUsd),
                            FLuyKeKinhPhiDuocCapVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDuocCapVnd),
                            FLuyKeKinhPhiDuocCapUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDuocCapUsd),
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd),
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd),
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd),
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstHopDongTH.Sum(x => x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd),
                            FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = listHopDong.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd),
                            FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = listHopDong.Sum(x => x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd),
                            FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = listHopDong.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd),
                            FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = listHopDong.Sum(x => x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd),
                        };
                     
                        returnData.Add(newObjHopDongDuAn);
                        listHopDong.ForEach(x =>
                        {
                            x.SLevel = "4";
                            x.FKeHoachBqpUsd = null;
                            x.FKeHoachTtcpUsd = null;
                            x.ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi;
                            x.IIdHopDongId = nameHopDong.IIdHopDongId;
                            x.IID_DuAnID = idDuAn;
                            x.IIdKhttNhiemVuChiId = idChuongTrinh;
                            x.IIdTiGiaId = iIdTiGiaId;
                            x.FHopDongUsd = null;
                            x.FHopDongVnd = null;
                            x.FHopDongDuAnUsd = null;
                            x.FHopDongDuAnVnd = null;
                            x.FQtKinhPhiDuyetCacNamTruocVnd = null;
                            x.FQtKinhPhiDuyetCacNamTruocUsd = null;
                            x.FLuyKeKinhPhiDuocCapUsd = null;
                            x.FLuyKeKinhPhiDuocCapVnd = null;
                            x.FKeHoachChuaGiaiNganUsd = null;
                            x.FKeHoachChuaGiaiNganVnd = null;
                            x.BIsSaveTongHop = true;
                            x.FQtKinhPhiDuocCapNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FQtKinhPhiDuocCapNamNayUsd) : 0;
                            x.FQtKinhPhiDuocCapNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FQtKinhPhiDuocCapNamNayVnd) : 0;
                            x.IsChiKhac = !isDuAn;
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd) : 0;
                            x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd) : 0;
                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd) : 0;

                            x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd) : 0;

                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd) : 0;

                            x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd) : 0;

                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd) : 0;

                            x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd) : 0;
                        });

                        var lstAddResult = listHopDong.GroupBy(g => new { g.IIdMucLucNganSachId, g.IIdHopDongId }).Select(x => new NhQTQuyetToanNienDoChiTietQuery
                        {
                            SLNS = x.FirstOrDefault().SLNS,
                            SL = x.FirstOrDefault().SL,
                            SK = x.FirstOrDefault().SK,
                            SM = x.FirstOrDefault().SM,
                            STM = x.FirstOrDefault().STM,
                            STTM = x.FirstOrDefault().STTM,
                            STenDuAn = x.FirstOrDefault().STenDuAn,
                            STenHopDong = x.FirstOrDefault().STenHopDong,
                            STenNhiemVuChi = x.FirstOrDefault().STenNhiemVuChi,
                            STenNoiDungChi = x.FirstOrDefault().STenNoiDungChi,
                            IIdMlnsId = x.FirstOrDefault().IIdMlnsId,
                            IIdMucLucNganSachId = x.FirstOrDefault().IIdMucLucNganSachId,
                            SLevel = "4",
                            FKeHoachBqpUsd = null,
                            FKeHoachTtcpUsd = null,
                            ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi,
                            IIdHopDongId = nameHopDong.IIdHopDongId,
                            IID_DuAnID = idDuAn,
                            IIdKhttNhiemVuChiId = idChuongTrinh,
                            IIdTiGiaId = iIdTiGiaId,
                            FHopDongUsd = null,
                            FHopDongVnd = null,
                            FHopDongDuAnUsd = null,
                            FHopDongDuAnVnd = null,
                            FQtKinhPhiDuyetCacNamTruocVnd = x.FirstOrDefault().FQtKinhPhiDuyetCacNamTruocVnd,
                            FQtKinhPhiDuyetCacNamTruocUsd = x.FirstOrDefault().FQtKinhPhiDuyetCacNamTruocUsd,
                            FLuyKeKinhPhiDuocCapUsd = x.FirstOrDefault().FLuyKeKinhPhiDuocCapUsd,
                            FLuyKeKinhPhiDuocCapVnd = x.FirstOrDefault().FLuyKeKinhPhiDuocCapVnd,
                            FKeHoachChuaGiaiNganUsd = x.FirstOrDefault().FKeHoachChuaGiaiNganUsd,
                            FKeHoachChuaGiaiNganVnd = x.FirstOrDefault().FKeHoachChuaGiaiNganVnd,
                            BIsSaveTongHop = true,
                            FQtKinhPhiDuocCapNamNayUsd = 0,
                            FQtKinhPhiDuocCapNamNayVnd = 0,
                            IsChiKhac = !isDuAn,
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                            FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                            FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                        });
                        returnData.AddRange(lstAddResult);
                    }
                }
                else
                {
                    var listHopDong = list.Where(x => x.ILoaiNoiDungChi == loaiChiPhi.ILoaiNoiDungChi).ToList();
                    var lstHopDongTH = listTongHop.Where(w => w.IIdKhttNhiemVuChiId == idChuongTrinh);
                    listHopDong.ForEach(x =>
                    {
                        x.SLevel = "4";
                        x.FKeHoachBqpUsd = null;
                        x.FKeHoachTtcpUsd = null;
                        x.ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi;
                        x.BIsSaveTongHop = true;
                        x.IIdTiGiaId = iIdTiGiaId;
                        x.IIdKhttNhiemVuChiId = idChuongTrinh;
                        x.IsChiKhac = !isDuAn;
                        x.FQtKinhPhiDuocCapNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FQtKinhPhiDuocCapNamNayUsd) : 0;
                        x.FQtKinhPhiDuocCapNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FQtKinhPhiDuocCapNamNayVnd) : 0;
                        x.IsChiKhac = !isDuAn;
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd) : 0;
                        x.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd) : 0;
                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd) : 0;

                        x.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd) : 0;

                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayUsd) : 0;

                        x.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngKinhPhiKhoiDauDenHetNamNayVnd) : 0;

                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayUsd) : 0;

                        x.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd = lstHopDongTH.Any(y => y.IIdHopDongId == x.IIdHopDongId) ? lstHopDongTH.Where(y => y.IIdHopDongId == x.IIdHopDongId).Sum(s => s.FLuyKeSoDuTamUngCheDoKhoiDauDenHetNamNayVnd) : 0;
                    });
                    var lstAddResult = listHopDong.GroupBy(g => new { g.IIdMucLucNganSachId, g.IIdHopDongId }).Select(x => new NhQTQuyetToanNienDoChiTietQuery
                    {
                        SLNS = x.FirstOrDefault().SLNS,
                        SL = x.FirstOrDefault().SL,
                        SK = x.FirstOrDefault().SK,
                        SM = x.FirstOrDefault().SM,
                        STM = x.FirstOrDefault().STM,
                        STTM = x.FirstOrDefault().STTM,
                        STenDuAn = x.FirstOrDefault().STenDuAn,
                        STenHopDong = x.FirstOrDefault().STenHopDong,
                        STenNhiemVuChi = x.FirstOrDefault().STenNhiemVuChi,
                        STenNoiDungChi = x.FirstOrDefault().STenNoiDungChi,
                        IIdMlnsId = x.FirstOrDefault().IIdMlnsId,
                        IIdMucLucNganSachId = x.FirstOrDefault().IIdMucLucNganSachId,
                        SLevel = "4",
                        FKeHoachBqpUsd = null,
                        FKeHoachTtcpUsd = null,
                        ILoaiNoiDungChi = loaiChiPhi.ILoaiNoiDungChi,
                        IIdHopDongId = x.FirstOrDefault().IIdHopDongId,
                        IID_DuAnID = idDuAn,
                        IIdKhttNhiemVuChiId = idChuongTrinh,
                        IIdTiGiaId = iIdTiGiaId,
                        FHopDongUsd = null,
                        FHopDongVnd = null,
                        FHopDongDuAnUsd = null,
                        FHopDongDuAnVnd = null,
                        FQtKinhPhiDuyetCacNamTruocVnd = null,
                        FQtKinhPhiDuyetCacNamTruocUsd = null,
                        FLuyKeKinhPhiDuocCapUsd = x.FirstOrDefault().FLuyKeKinhPhiDuocCapUsd,
                        FLuyKeKinhPhiDuocCapVnd = x.FirstOrDefault().FLuyKeKinhPhiDuocCapVnd,
                        FKeHoachChuaGiaiNganUsd = null,
                        FKeHoachChuaGiaiNganVnd = null,
                        BIsSaveTongHop = true,
                        FQtKinhPhiDuocCapNamNayUsd = 0,
                        FQtKinhPhiDuocCapNamNayVnd = 0,
                        IsChiKhac = !isDuAn,
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganTrongNamNayUsd,
                        FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganTrongNamNayVnd,
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayUsd,
                        FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd = x.FirstOrDefault().FLuyKeKinhPhiDaGiaiNganKhoiDauDenHetNamNayVnd,
                    });

                    returnData.AddRange(lstAddResult);
                }

            }
            return returnData;
        }

        private string ConvertLetter(int input)
        {
            StringBuilder res = new StringBuilder((input - 1).ToString());
            for (int j = 0; j < res.Length; j++)
                res[j] += (char)(17); // '0' is 48, 'A' is 65
            return res.ToString();
        }
        private string ConvertLaMa(decimal num)
        {
            string strRet = string.Empty;
            decimal _Number = num;
            Boolean _Flag = true;
            string[] ArrLama = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            int[] ArrNumber = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            int i = 0;
            while (_Flag)
            {
                while (_Number >= ArrNumber[i])
                {
                    _Number -= ArrNumber[i];
                    strRet += ArrLama[i];
                    if (_Number < 1)
                        _Flag = false;
                }
                i++;
            }
            return strRet;
        }

    }
}
