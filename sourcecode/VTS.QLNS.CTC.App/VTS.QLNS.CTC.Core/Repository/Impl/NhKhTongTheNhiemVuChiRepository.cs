using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Domain.Query;
using VTS.QLNS.CTC.Core.Extensions;
using System.Text;
using VTS.QLNS.CTC.Utility;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class NhKhTongTheNhiemVuChiRepository : Repository<NhKhTongTheNhiemVuChi>, INhKhTongTheNhiemVuChiRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public NhKhTongTheNhiemVuChiRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public NhKhTongTheNhiemVuChiQuery FindOneByIdKhTongTheAndIdNhiemVuChi(Guid idKhTongThe, Guid idNhiemVuChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    nvc.sMaNhiemVuChi, ";
                //sql += "    nvc.sTenNhiemVuChi, ";
                //sql += "    nvc.iLoaiNhiemVuChi  ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "WHERE";
                //sql += "   tt_nvc.iID_KHTongTheID = @IdKhTongThe ";
                //sql += "   AND tt_nvc.iID_NhiemVuChiID = @IdNhiemVuChi ";

                //var parameters = new object[]
                //{
                //     new SqlParameter("@IdKhTongThe", idKhTongThe),
                //     new SqlParameter("@IdNhiemVuChi", idNhiemVuChi)
                //};
                //return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).FirstOrDefault();

                string sql = "EXECUTE dbo.sp_nh_find_one_by_IdKhTongThe_and_IdNhiemVuChi @IdKhTongThe, @IdNhiemVuChi";
                var parameters = new object[]
                {
                     new SqlParameter("@IdKhTongThe", idKhTongThe),
                     new SqlParameter("@IdNhiemVuChi", idNhiemVuChi)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).FirstOrDefault();
            }
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonVi(Guid idKhTongThe, string maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    donvi.sTenDonVi AS STenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    nvc.sMaNhiemVuChi, ";
                //sql += "    nvc.sTenNhiemVuChi, ";
                //sql += "    nvc.iLoaiNhiemVuChi  ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "WHERE ";
                //sql += "    tt_nvc.iID_KHTongTheID = @IdKhTongThe ";
                //sql += "    AND tt_nvc.iID_MaDonViThuHuong = @MaDonVi ";

                string sql = "EXECUTE dbo.sp_nh_find_by_IdKhTongThe_and_maDonVi @IdKhTongThe, @MaDonVi";
                var parameters = new object[]
                {
                     new SqlParameter("@IdKhTongThe", idKhTongThe),
                     new SqlParameter("@MaDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndMaDonViID(Guid idKhTongThe, Guid maDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string executeSql = "EXECUTE dbo.sp_FindByIdKhTongTheAndMaDonViID @IdKhTongThe, @MaDonVi";
                var parameters = new object[]
                {
                    new SqlParameter("@IdKhTongThe", idKhTongThe),
                     new SqlParameter("@MaDonVi", maDonVi)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(executeSql, parameters).ToList();
            }
        }

        public IEnumerable<NhDaGoiThauQuery> FindByIdNhiemVuChi(Guid idNhiemVuChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT * ";
                //sql += "FROM NH_DA_GoiThau gt ";
                //sql += "join NH_KHTongThe_NhiemVuChi nvc ";
                //sql += "on gt.iID_KHTT_NhiemVuChiID = nvc.ID ";
                //sql += "join NH_DM_NhiemVuChi dmnvc ";
                //sql += "on nvc.iID_NhiemVuChiID = dmnvc.ID ";
                //sql += "WHERE ";
                //sql += "dmnvc.Id =@idNhiemVuChi ";
                string sql = "EXECUTE dbo.sp_nh_find_by_IdNhiemVuChi @idNhiemVuChi";
                var parameter = new object[]
                {
                    new SqlParameter("@idNhiemVuChi",idNhiemVuChi)
                };
                return ctx.FromSqlRaw<NhDaGoiThauQuery>(sql, parameter).ToList();

            }
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongTheAndIdDonVi(Guid idKhTongThe, Guid idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    donvi.sTenDonVi AS STenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    nvc.sMaNhiemVuChi, ";
                //sql += "    nvc.sTenNhiemVuChi, ";
                //sql += "    nvc.iLoaiNhiemVuChi  ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "WHERE ";
                //sql += "    tt_nvc.iID_KHTongTheID = @IdKhTongThe ";
                //sql += "    AND tt_nvc.iID_DonViThuHuongID = @IdDonVi ";

                string sql = "EXECUTE dbo.sp_nh_find_by_IdKhTongThe_and_IdDonVi @IdKhTongThe, @IdDonVi";
                var parameters = new object[]
                {
                     new SqlParameter("@IdKhTongThe", idKhTongThe),
                     new SqlParameter("@IdDonVi", idDonVi)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllDonViByKhTongTheId(Guid idKhTongThe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT DISTINCT ";
                //sql += "tt_nvc.iID_KHTongTheID, ";
                //sql += "tt_nvc.iID_DonViThuHuongID as Id, ";
                ////sql += "tt_nvc.iID_NhiemVuChiID, ";
                //sql += "DV.iID_DonVi, ";
                //sql += "DV.iID_MaDonVi as IIDMaDonVi, ";
                //sql += "DV.sTenDonVi as TenDonVi, ";
                //sql += "DV.iNamLamViec as NamLamViec ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                ////sql += "JOIN DonVi DV ON tt_nvc.iID_DonViThuHuongID = DV.iID_DonVi ";
                //sql += "JOIN DonVi DV ON tt_nvc.iID_MaDonViThuHuong = DV.iID_MaDonVi ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "WHERE  ";
                //sql += "tt_nvc.iID_KHTongTheID = @IdKhTongThe ";
                string sql = "EXECUTE dbo.sp_nh_find_all_donVi_by_KhTongTheId @IdKhTongThe";
                var parameters = new object[]
                {
                    new SqlParameter("@IdKhTongThe", idKhTongThe)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdKhTongThe(Guid idKhTongThe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    nvc.sMaNhiemVuChi, ";
                //sql += "    nvc.sTenNhiemVuChi, ";
                //sql += "    nvc.iLoaiNhiemVuChi ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "WHERE tt_nvc.iID_KHTongTheID = @IdKhTongThe ";
                string sql = "EXECUTE dbo.sp_nh_find_by_IdKhTongThe @IdKhTongThe";
                var parameters = new object[]
                {
                    new SqlParameter("@IdKhTongThe", idKhTongThe)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }
        public IEnumerable<NhKhTongThe> FindKhTongTheByNvChiId(Guid idNvChi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "select * from NH_KHTongThe kh";
                //sql += "inner join NH_KHTongThe_NhiemVuChi nv ";
                //sql += "on nv.iID_KHTongTheID = kh.ID ";
                //sql += "where nv.ID = @IdNvChi ";
                string sql = "EXECUTE dbo.sp_nh_find_KhTongThe_by_NvChiId @IdNvChi";
                var parameters = new object[]
                {
                    new SqlParameter("@IdNvChi", idNvChi)
                };
                return ctx.FromSqlRaw<NhKhTongThe>(sql, parameters).ToList();
            }
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindAllNvcByIdKhTongTheGiaiDoan(Guid idKhTongThe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    FORMATMESSAGE( '%s - %s', DonVi.iID_MaDonVi, DonVi.sTenDonVi ) sTenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    nvc.sMaNhiemVuChi, ";
                //sql += "    nvc.sTenNhiemVuChi, ";
                //sql += "    nvc.iLoaiNhiemVuChi ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN NH_KHTongThe khtt ON khtt.ID = tt_nvc.iID_KHTongTheID  ";
                //sql += "JOIN DonVi DonVi ON DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID  ";
                //sql += "WHERE khtt.iID_ParentID = @IdKhTongThe ";

                string sql = "EXECUTE dbo.sp_nh_find_all_nvc_by_IdKhTongThe_giaiDoan @IdKhTongThe";
                var parameters = new object[]
                {
                    new SqlParameter("@IdKhTongThe", idKhTongThe)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }

        public NhKhTongTheNhiemVuChiQuery FindById(Guid id)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT ";
                //sql += "    tt_nvc.ID, ";
                //sql += "    tt_nvc.iID_KHTongTheID, ";
                //sql += "    tt_nvc.iID_NhiemVuChiID, ";
                //sql += "    tt_nvc.iID_DonViThuHuongID, ";
                //sql += "    donvi.sTenDonVi AS STenDonVi, ";
                //sql += "    donvi.iID_MaDonVi AS SMaDonViThuHuong, ";
                //sql += "    tt_nvc.fGiaTriKH_TTCP AS FGiaTriKhTTCP, ";
                //sql += "    tt_nvc.fGiaTriKH_BQP AS FGiaTriKhBQP, ";
                //sql += "    nvc.sMaNhiemVuChi,";
                //sql += "    nvc.sTenNhiemVuChi,";
                //sql += "    nvc.iLoaiNhiemVuChi ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi tt_nvc ";
                //sql += "JOIN NH_DM_NhiemVuChi nvc ON tt_nvc.iID_NhiemVuChiID = nvc.ID ";
                //sql += "JOIN DonVi donvi on DonVi.iID_DonVi = tt_nvc.iID_DonViThuHuongID ";
                //sql += "WHERE tt_nvc.ID = @Id";

                string sql = "EXECUTE dbo.sp_nh_find_KHTongThe_nhiemVuChi_by_Id @Id";
                var parameters = new object[]
                {
                    new SqlParameter("@Id", id)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).FirstOrDefault();
            }
        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindKHTongTheAndDmNhiemVuChi(Guid idKhTongThe)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                //string sql = "";
                //sql += "SELECT  ";
                //sql += "    n.ID ";
                //sql += "   ,n.iID_KHTongTheID ";
                //sql += "   ,n.iID_NhiemVuChiID ";
                //sql += "   ,n.iID_DonViThuHuongID ";
                //sql += "   ,n.fGiaTriKH_TTCP AS FGiaTriKhTTCP ";
                //sql += "   ,n.fGiaTriKH_BQP AS FGiaTriKhBQP ";
                //sql += "   ,n.fGiaTriKH_BQP_VND AS FGiaTriKhBQPVnd ";
                //sql += "   ,n.iID_MaDonViThuHuong ";
                //sql += "   ,n.sMaOrder ";
                //sql += "   ,d.iID_ParentID AS ParentNhiemVuChiId ";
                //sql += "   ,d.sTenNhiemVuChi AS STenNhiemVuChi ";
                //sql += "FROM NH_KHTongThe_NhiemVuChi AS n ";
                //sql += "INNER JOIN NH_DM_NhiemVuChi AS d ";
                //sql += "ON n.iID_NhiemVuChiID = d.ID ";
                //sql += "WHERE n.iID_KHTongTheID = @idKhTongThe ";
                //sql += "ORDER BY sMaOrder ";

                string sql = "EXECUTE dbo.sp_nh_find_KHTongThe_and_DmNhiemVuChi @idKhTongThe";
                var parameters = new object[]
                {
                    new SqlParameter("@idKhTongThe", idKhTongThe)
                };

                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }

        private int GetIndexNVC(List<NhDmNhiemVuChi> lstDmNvc)
        {

            int index = 1;
            if (!lstDmNvc.IsEmpty())
            {
                var lstNvcOrder = lstDmNvc.Where(x => !string.IsNullOrEmpty(x.SMaNhiemVuChi)).ToList();
                if (!lstNvcOrder.IsEmpty())
                {
                    if (lstNvcOrder.Any(x => x.SMaNhiemVuChi.Split('.').Count() >= 3))
                    {
                        var lstIndexOrder = lstNvcOrder.Where(x => x.SMaNhiemVuChi.Split('.').Count() >= 3 && int.TryParse((x.SMaNhiemVuChi.Split('.')[2]), out int a));
                        var lstIndex = lstIndexOrder.Select(s => int.Parse(s.SMaNhiemVuChi.Split('.')[2]));
                        index = lstIndex.Max() + 1;
                    }
                    else
                    {
                        index = lstDmNvc.Count + 1;
                    }
                }
                else
                {
                    index = lstDmNvc.Count + 1;
                }
            }
            return index;
        }

        public void AddOrUpdate(Guid khTongTheId, IEnumerable<NhKhTongTheNhiemVuChi> entities)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                List<NhKhTongTheNhiemVuChi> lstAdded = entities.Where(s => s.IsAdded && !s.IsDeleted).ToList();
                if (lstAdded.Any())
                {
                    var listNhiemVuchis = ctx.NhDmNhiemVuChis.ToList();
                    int index = GetIndexNVC(listNhiemVuchis);

                    foreach (var item in lstAdded)
                    {
                        item.Id = Guid.NewGuid();
                        item.IIdKhTongTheId = khTongTheId;
                        // Kiểm tra danh mục chưa tồn tại thì tạo mới
                        var dmNhiemVuChi = ctx.Set<NhDmNhiemVuChi>().Find(item.IIdNhiemVuChiId);
                        if (dmNhiemVuChi == null)
                        {
                            ctx.Set<NhDmNhiemVuChi>().Add(new NhDmNhiemVuChi
                            {
                                Id = item.IIdNhiemVuChiId,
                                SMaNhiemVuChi = $"NVC.{item.SMaDonViThuHuong}.{StringUtils.ConvertMaOrderNew(item.SMaOrder, !item.ParentNhiemVuChiId.IsNullOrEmpty() ? index - 1 : index)}",
                                STenNhiemVuChi = item.STenNhiemVuChi,
                                ILoaiNhiemVuChi = null,
                                SMaOrder = item.SMaOrder,
                                IIdParentId = item.ParentNhiemVuChiId,
                            });
                            index = item.ParentNhiemVuChiId.IsNullOrEmpty() ? index + 1 : index;
                        }
                    }
                    ctx.AddRange(lstAdded);
                    ctx.SaveChanges();
                }

                List<NhKhTongTheNhiemVuChi> lstModified = entities.Where(x => x.IsModified && !x.IsAdded && !x.IsDeleted).ToList();
                if (lstModified.Any())
                {
                    List<NhDmNhiemVuChi> dmNhiemVuChis = new List<NhDmNhiemVuChi>();
                    var listNVCUpdate = ctx.NhDmNhiemVuChis.Where(x => lstModified.Select(s => s.IIdNhiemVuChiId).Contains(x.Id)).ToList();
                    listNVCUpdate.ForEach(x =>
                    {
                        x.STenNhiemVuChi = lstModified.Any(w => w.IIdNhiemVuChiId == x.Id) ? lstModified.FirstOrDefault(w => w.IIdNhiemVuChiId == x.Id).STenNhiemVuChi : x.STenNhiemVuChi;
                    });
                    ctx.UpdateRange(lstModified);
                    ctx.UpdateRange(listNVCUpdate);
                    ctx.SaveChanges();
                }

                List<NhKhTongTheNhiemVuChi> lstDeleted = entities.Where(x => x.IsDeleted && !x.IsAdded).ToList();
                if (lstDeleted.Any())
                {
                    ctx.RemoveRange(lstDeleted);
                    ctx.SaveChanges();
                }
            }

        }

        public IEnumerable<NhKhTongTheNhiemVuChiQuery> FindByIdDonVi(Guid? idDonVi)
        {
            using (var ctx = _contextFactory.CreateDbContext())
            {
                string sql = "EXECUTE sp_nh_khtongthe_nhiemvuchi_bydonvi @iDDonVi";
                var parameters = new[]
                {
                    new SqlParameter("@iDDonVi", idDonVi)
                };
                return ctx.FromSqlRaw<NhKhTongTheNhiemVuChiQuery>(sql, parameters).ToList();
            }
        }

       
    }
}
