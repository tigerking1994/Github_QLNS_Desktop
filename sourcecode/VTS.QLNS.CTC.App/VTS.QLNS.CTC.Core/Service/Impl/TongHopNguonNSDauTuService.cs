using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Repository;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Service.Impl
{
    public class TongHopNguonNSDauTuService : ITongHopNguonNSDauTuService
    {
        private readonly ITongHopNguonNSDauTuRepository _repository;
        public TongHopNguonNSDauTuService(ITongHopNguonNSDauTuRepository repository)
        {
            _repository = repository;
        }

        public void InsertTongHopNguonDauTu_Tang(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, Guid? iIDQuyetDinhOld = null)
        {
            if (!iIDQuyetDinhOld.HasValue)
                iIDQuyetDinhOld = Guid.NewGuid();
            _repository.InsertTongHopNguonDauTu_Tang(sLoai, iTypeExecute, iIdQuyetDinh, iIDQuyetDinhOld.Value);
        }

        public void InsertTongHopNguonDauTu_ThanhToan_Giam(string sLoai, int iTypeExecute, Guid iIdQuyetDinh, List<TongHopNguonNSDauTuQuery> lstChungTu, List<TongHopNguonNSDauTuQuery> lstNguon, List<TongHopNguonNSDauTuQuery> lstChungTuAppend = null, double? fThueGiaTriGiaTangDuocDuyet = null, double? fChuyenTienBaoHanhDuocDuyet = null)
        {
            if (lstChungTu == null || lstChungTu.Count == 0) return;
            List<TongHopNguonNSDauTuQuery> lstChungTuUpdate = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstNguonVon = _repository.GetNguonVonTongHopNguonDauTuByCondition(lstNguon);
            Dictionary<Guid, double> dicNguonVon = new Dictionary<Guid, double>();

            foreach (var objChungTu in lstChungTu)
            {
                var lstIdRemove = new List<Guid>();
                //double fThanhToan = (objChungTu.fGiaTri ?? 0) - (fThueGiaTriGiaTangDuocDuyet ?? 0) - (fChuyenTienBaoHanhDuocDuyet ?? 0);
                double fThanhToan = (objChungTu.fGiaTri ?? 0);
                foreach (var objNguonVon in lstNguonVon)
                {
                    if (!dicNguonVon.ContainsKey(objNguonVon.Id))
                        dicNguonVon.Add(objNguonVon.Id, objNguonVon.fGiaTri ?? 0);

                    if (objNguonVon.iID_DuAnID == objChungTu.iID_DuAnID
                        && objNguonVon.iID_ChungTu == objChungTu.iId_MaNguonCha
                        && objNguonVon.IIdLoaiCongTrinh == objChungTu.IIdLoaiCongTrinh)
                    {
                        if (objChungTu.sMaDich == LOAI_CHUNG_TU.CHU_DAU_TU)
                        {
                            lstChungTuUpdate.Add(new TongHopNguonNSDauTuQuery()
                            {
                                fGiaTri = objChungTu.fGiaTri,
                                iID_ChungTu = objChungTu.iID_ChungTu,
                                iID_DuAnID = objChungTu.iID_DuAnID,
                                sMaDich = objChungTu.sMaDich,
                                sMaNguon = objChungTu.sMaNguon,
                                sMaNguonCha = objChungTu.sMaNguonCha,
                                iId_MaNguonCha = objChungTu.iId_MaNguonCha,
                                iThuHoiTUCheDo = objChungTu.iThuHoiTUCheDo,
                                ILoaiUng = objChungTu.ILoaiUng,
                                iStatus = (int)NguonVonStatus.DaSuDung,
                                IIDMucID = objChungTu.IIDMucID,
                                IIDTieuMucID = objChungTu.IIDTieuMucID,
                                IIDTietMucID = objChungTu.IIDTietMucID,
                                IIDNganhID = objChungTu.IIDNganhID,
                                IIdLoaiCongTrinh = objChungTu.IIdLoaiCongTrinh
                            });
                            break;
                        }

                        if (fThanhToan >= dicNguonVon[objNguonVon.Id])
                        {
                            lstChungTuUpdate.Add(new TongHopNguonNSDauTuQuery()
                            {
                                fGiaTri = dicNguonVon[objNguonVon.Id],
                                iID_ChungTu = objChungTu.iID_ChungTu,
                                iID_DuAnID = objChungTu.iID_DuAnID,
                                sMaDich = objChungTu.sMaDich,
                                sMaNguon = objChungTu.sMaNguon,
                                sMaNguonCha = objChungTu.sMaNguonCha,
                                iId_MaNguonCha = objChungTu.iId_MaNguonCha,
                                ILoaiUng = objChungTu.ILoaiUng,
                                iStatus = (int)NguonVonStatus.DaSuDung,
                                IIDMucID = objChungTu.IIDMucID,
                                IIDTieuMucID = objChungTu.IIDTieuMucID,
                                IIDTietMucID = objChungTu.IIDTietMucID,
                                IIDNganhID = objChungTu.IIDNganhID,
                                IIdLoaiCongTrinh = objChungTu.IIdLoaiCongTrinh
                            });
                            fThanhToan = fThanhToan - dicNguonVon[objNguonVon.Id];
                            objNguonVon.iStatus = (int)NguonVonStatus.DaSuDung;
                            lstIdRemove.Add(objNguonVon.Id);
                        }
                        else
                        {
                            lstChungTuUpdate.Add(new TongHopNguonNSDauTuQuery()
                            {
                                fGiaTri = fThanhToan,
                                iID_ChungTu = objChungTu.iID_ChungTu,
                                iID_DuAnID = objChungTu.iID_DuAnID,
                                sMaDich = objChungTu.sMaDich,
                                sMaNguon = objChungTu.sMaNguon,
                                sMaNguonCha = objChungTu.sMaNguonCha,
                                iId_MaNguonCha = objChungTu.iId_MaNguonCha,
                                ILoaiUng = objChungTu.ILoaiUng,
                                iStatus = (int)NguonVonStatus.DaSuDung,
                                IIDMucID = objChungTu.IIDMucID,
                                IIDTieuMucID = objChungTu.IIDTieuMucID,
                                IIDTietMucID = objChungTu.IIDTietMucID,
                                IIDNganhID = objChungTu.IIDNganhID,
                                IIdLoaiCongTrinh = objChungTu.IIdLoaiCongTrinh
                            });
                            dicNguonVon[objNguonVon.Id] = dicNguonVon[objNguonVon.Id] - fThanhToan;
                            break;
                        }
                    }
                }
                lstNguonVon = lstNguonVon.Where(n => !lstIdRemove.Contains(n.Id)).ToList();
            }

            if (lstChungTuAppend != null)
            {
                lstChungTuUpdate.AddRange(lstChungTuAppend);
            }

            if (lstChungTuUpdate.Count != 0)
            {
                _repository.InsertTongHopNguonDauTu(iIdQuyetDinh, sLoai, lstChungTuUpdate);
            }
        }

        public void InsertTongHopNguonDauTu_KHVN_Giam(int iNamKeHoach, string sLoai, int iTypeExecute, Guid iIdQuyetDinh, List<TongHopNguonNSDauTuQuery> lstChungTu, double? fThueGiaTriGiaTangDuocDuyet = null, double? fChuyenTienBaoHanhDuocDuyet = null)
        {
            if (lstChungTu == null || lstChungTu.Count == 0) return;
            List<TongHopNguonNSDauTuQuery> lstChungTuUpdate = new List<TongHopNguonNSDauTuQuery>();
            List<TongHopNguonNSDauTuQuery> lstNguonVon = _repository.GetNguonVonTongHopNguonDauTuKHVN(iNamKeHoach);
            Dictionary<Guid, double> dicNguonVon = new Dictionary<Guid, double>();

            foreach (var objChungTu in lstChungTu)
            {
                var lstIdRemove = new List<Guid>();
                double fThanhToan = objChungTu.fGiaTri ?? 0;
                //double fThanhToan = (objChungTu.fGiaTri ?? 0) - (fThueGiaTriGiaTangDuocDuyet ?? 0) - (fChuyenTienBaoHanhDuocDuyet ?? 0);
                foreach (var objNguonVon in lstNguonVon)
                {
                    if (!dicNguonVon.ContainsKey(objNguonVon.Id))
                        dicNguonVon.Add(objNguonVon.Id, objNguonVon.fGiaTri ?? 0);

                    if (objNguonVon.iID_DuAnID == objChungTu.iID_DuAnID && objNguonVon.sMaNguon == objChungTu.sMaDich)
                    {
                        if (fThanhToan >= dicNguonVon[objNguonVon.Id])
                        {
                            lstChungTuUpdate.Add(new TongHopNguonNSDauTuQuery()
                            {
                                fGiaTri = dicNguonVon[objNguonVon.Id],
                                iID_ChungTu = objChungTu.iID_ChungTu,
                                iID_DuAnID = objChungTu.iID_DuAnID,
                                sMaDich = objChungTu.sMaDich,
                                sMaNguon = objChungTu.sMaNguon,
                                sMaNguonCha = objChungTu.sMaNguonCha,
                                iId_MaNguonCha = objNguonVon.iID_ChungTu,
                                iStatus = (int)NguonVonStatus.DaSuDung,
                                IIDMucID = objChungTu.IIDMucID,
                                IIDTieuMucID = objChungTu.IIDTieuMucID,
                                IIDTietMucID = objChungTu.IIDTietMucID,
                                IIDNganhID = objChungTu.IIDNganhID
                            });
                            fThanhToan = fThanhToan - dicNguonVon[objNguonVon.Id];
                            objNguonVon.iStatus = (int)NguonVonStatus.DaSuDung;
                            lstIdRemove.Add(objNguonVon.Id);
                        }
                        else
                        {
                            lstChungTuUpdate.Add(new TongHopNguonNSDauTuQuery()
                            {
                                fGiaTri = fThanhToan,
                                iID_ChungTu = objChungTu.iID_ChungTu,
                                iID_DuAnID = objChungTu.iID_DuAnID,
                                sMaDich = objChungTu.sMaDich,
                                sMaNguon = objChungTu.sMaNguon,
                                sMaNguonCha = objChungTu.sMaNguonCha,
                                iId_MaNguonCha = objNguonVon.iID_ChungTu,
                                iStatus = (int)NguonVonStatus.DaSuDung,
                                IIDMucID = objChungTu.IIDMucID,
                                IIDTieuMucID = objChungTu.IIDTieuMucID,
                                IIDTietMucID = objChungTu.IIDTietMucID,
                                IIDNganhID = objChungTu.IIDNganhID
                            });
                            dicNguonVon[objNguonVon.Id] = dicNguonVon[objNguonVon.Id] - fThanhToan;
                            break;
                        }
                    }
                }
                lstNguonVon = lstNguonVon.Where(n => !lstIdRemove.Contains(n.Id)).ToList();
            }

            if (lstChungTuUpdate.Count != 0)
            {
                _repository.InsertTongHopNguonDauTu(iIdQuyetDinh, sLoai, lstChungTuUpdate);
            }
        }

        public IEnumerable<VdtTongHopNguonNsdauTu> FindByCondition(Expression<Func<VdtTongHopNguonNsdauTu, bool>> predicate)
        {
            return _repository.FindAll(predicate);
        }

        public IEnumerable<VdtBaoCaoKetQuaGiaiNganChiKPDTQuery> GetBcKetQuaGiaiNganChiPhiKinhPhiDT(string iIdDonViQuanLy, int iNamKeHoach, int iIdNguonVonId)
        {
            return _repository.GetBcKetQuaGiaiNganChiPhiKinhPhiDT(iIdDonViQuanLy, iNamKeHoach, iIdNguonVonId);
        }

        public void InsertTongHopNguonDauTuQuyetToan(Guid iIDChungTu, List<TongHopNguonNSDauTuQuery> lstData)
        {
            _repository.InsertTongHopNguonDauTuQuyetToan(iIDChungTu, lstData);
        }

        public void DeleteTongHopNguonDauTu(string sLoai, Guid iIdQuyetDinh)
        {
            _repository.DeleteTongHopNguonDauTu_Giam(sLoai, iIdQuyetDinh);
        }
    }
}
