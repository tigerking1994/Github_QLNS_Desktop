using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VTS.QLNS.CTC.Core.Domain;
using VTS.QLNS.CTC.Core.Extensions;
using Dapper;

namespace VTS.QLNS.CTC.Core.Repository.Impl
{
    public class VdtDaHopDongGoiThauNhaThauRepository : Repository<VdtDaHopDongGoiThauNhaThau>, IVdtDaHopDongGoiThauNhaThauRepository
    {
        private readonly ApplicationDbContextFactory _contextFactory;

        public VdtDaHopDongGoiThauNhaThauRepository(ApplicationDbContextFactory contextFactory)
             : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void DeleteHopDongDetail(Guid iIdHopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                ctx.Database.ExecuteSqlCommand("EXECUTE dbo.sp_vdt_delete_hopdong_detail @iIdHopDongID",
                    new SqlParameter("@iIdHopDongID", iIdHopDongId));
            }
        }

        public void DeleteHopDongGoiThauNhaThau(List<Guid> listGoiThauNhaThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var executeQuery = "EXECUTE dbo.sp_vdt_delete_hopdong_detail @iIdHopDongID";
                DataTable dt = DBExtension.ConvertDataToGuidTable(listGoiThauNhaThauId);
                var parameters = new[]
                {
                    new SqlParameter("@lstId", dt.AsTableValuedParameter("t_tbl_uniqueidentifier"))
                };
                ctx.Database.ExecuteSqlCommand(executeQuery, parameters);
            }
        }

        public IEnumerable<VdtDaHopDongGoiThauNhaThau> ListGoiThauNhaThauByGoiThauId(Guid goiThauId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                return ctx.VdtDaHopDongGoiThauNhaThaus.Where(x => x.IIDGoiThauID == goiThauId).ToList();
            }
        }

        public double CalculateTotalUsedValueOfGoiThau(Guid GoiThauIds, Guid hopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var rs = from v in ctx.VdtDaHopDongGoiThauNhaThaus
                         join hd in ctx.VdtDaTtHopDongs on v.IIDHopDongID equals hd.Id
                         where hd.BActive.Value == true && v.IIDGoiThauID == GoiThauIds && hd.Id != hopDongId
                         select v;
                var total = rs.Sum(v => v.FGiaTri.HasValue ? v.FGiaTri.Value : 0);
                return total;
            }
        }

        public double CalculateTotalUsedValueOfChiPhi(Guid chiphiId, Guid hopDongId)
        {
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                var rs = from c in ctx.Set<VdtDaHopDongGoiThauChiPhi>()
                         join v in ctx.VdtDaHopDongGoiThauNhaThaus on c.IIdHopDongGoiThauNhaThauId equals v.Id
                         join hd in ctx.VdtDaTtHopDongs on v.IIDHopDongID equals hd.Id
                         where hd.BActive.Value == true && c.IIdChiPhiId == chiphiId && hd.Id != hopDongId
                         select c;
                var total = rs.Sum(v => v.FGiaTri.HasValue ? v.FGiaTri.Value : 0);
                return total;
            }
        }

        public void SaveHopDong(VdtDaTtHopDong vdtDaTtHopDong)
        {
            VdtDaTtHopDong hopdong = null;
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                hopdong = ctx.VdtDaTtHopDongs.FirstOrDefault(t => t.Id.Equals(vdtDaTtHopDong.Id));
            }

            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                if (hopdong == null)
                {
                    ctx.Add(vdtDaTtHopDong);
                }
                else
                {
                    ctx.Update(vdtDaTtHopDong);
                }
                // remove hopdonggoithaunhathau, hopdonggoithauchiphi, hopdonggoithauhangmuc
                IEnumerable<VdtDaHopDongGoiThauNhaThau> vdtDaHopDongGoiThauNhaThaus = ctx.VdtDaHopDongGoiThauNhaThaus.Where(t => t.IIDHopDongID.Equals(vdtDaTtHopDong.Id)).ToList();
                foreach (var item in vdtDaHopDongGoiThauNhaThaus)
                {
                    IEnumerable<VdtDaHopDongGoiThauChiPhi> vdtDaHopDongGoiThauChiPhis = ctx.Set<VdtDaHopDongGoiThauChiPhi>().Where(t => item.Id.Equals(t.IIdHopDongGoiThauNhaThauId)).ToList();
                    foreach (var cp in vdtDaHopDongGoiThauChiPhis)
                    {
                        IEnumerable<VdtDaHopDongGoiThauHangMuc> vdtDaHopDongGoiThauHangMucs = ctx.VdtDaHopDongGoiThauHangMucs.Where(t => t.IIDHopDongGoiThauNhaThauID.Equals(item.Id)
                            && t.IIDChiPhiID.Equals(cp.IIdChiPhiId)).ToList();
                        ctx.VdtDaHopDongGoiThauHangMucs.RemoveRange(vdtDaHopDongGoiThauHangMucs);
                    }
                    ctx.Set<VdtDaHopDongGoiThauChiPhi>().RemoveRange(vdtDaHopDongGoiThauChiPhis);
                }
                ctx.VdtDaHopDongGoiThauNhaThaus.RemoveRange(vdtDaHopDongGoiThauNhaThaus);
                

                foreach (var goithau in vdtDaTtHopDong.ListGoiThau)
                {
                    goithau.Id = Guid.NewGuid();
                    foreach (var chiphi in goithau.ListChiPhi)
                    {
                        chiphi.IIdHopDongGoiThauNhaThauId = goithau.Id;
                        foreach (var hm in chiphi.ListHangMuc)
                        {
                            hm.IIDHopDongGoiThauNhaThauID = goithau.Id;
                        }
                        ctx.VdtDaHopDongGoiThauHangMucs.AddRange(chiphi.ListHangMuc);
                    }
                    ctx.Set<VdtDaHopDongGoiThauChiPhi>().AddRange(goithau.ListChiPhi);
                    goithau.IIDHopDongID = vdtDaTtHopDong.Id;
                }
                ctx.VdtDaHopDongGoiThauNhaThaus.AddRange(vdtDaTtHopDong.ListGoiThau);
                ctx.SaveChanges();
            }
        }

        public void SaveHopDongDC(VdtDaTtHopDong vdtDaTtHopDongDC, VdtDaTtHopDong vdtDaTtHopDongGoc)
        {
            bool isUpdated = true;
            using (ApplicationDbContext ctx = _contextFactory.CreateDbContext())
            {
                isUpdated = ctx.VdtDaTtHopDongs.Any(t => t.Id.Equals(vdtDaTtHopDongDC.Id));
                ctx.Update(vdtDaTtHopDongGoc);
                if (!isUpdated)
                {
                    ctx.Add(vdtDaTtHopDongDC);
                }
                else
                {
                    ctx.Update(vdtDaTtHopDongDC);
                    // remove hopdonggoithaunhathau, hopdonggoithauchiphi, hopdonggoithauhangmuc , hopdong danh muc hang muc
                    IEnumerable<VdtDaHopDongGoiThauNhaThau> vdtDaHopDongGoiThauNhaThaus = ctx.VdtDaHopDongGoiThauNhaThaus.Where(t => t.IIDHopDongID.Equals(vdtDaTtHopDongDC.Id)).ToList();
                    foreach (var item in vdtDaHopDongGoiThauNhaThaus)
                    {
                        IEnumerable<VdtDaHopDongGoiThauChiPhi> vdtDaHopDongGoiThauChiPhis = ctx.Set<VdtDaHopDongGoiThauChiPhi>().Where(t => item.Id.Equals(t.IIdHopDongGoiThauNhaThauId)).ToList();
                        IEnumerable<VdtDaHopDongGoiThauHangMuc> vdtDaHopDongGoiThauHangMucs = ctx.VdtDaHopDongGoiThauHangMucs.Where(t => t.IIDHopDongGoiThauNhaThauID.Equals(item.Id)).ToList();
                        List<VdtDaHopDongDmHangMuc> vdtDaHopDongDmHangMucs = ctx.VdtDaHopDongDmHangMucs.Where(t => t.IIDHopDongGoiThauNhaThauID.Equals(item.Id)).ToList();
                        ctx.Set<VdtDaHopDongGoiThauChiPhi>().RemoveRange(vdtDaHopDongGoiThauChiPhis);
                        ctx.VdtDaHopDongGoiThauHangMucs.RemoveRange(vdtDaHopDongGoiThauHangMucs);
                        ctx.VdtDaHopDongDmHangMucs.RemoveRange(vdtDaHopDongDmHangMucs);
                    }
                    ctx.VdtDaHopDongGoiThauNhaThaus.RemoveRange(vdtDaHopDongGoiThauNhaThaus);
                }
                // save goithau nhathau, goithau chi phi, goithau hangmuc
                foreach (var goithau in vdtDaTtHopDongDC.ListGoiThau)
                {
                    goithau.Id = Guid.NewGuid();
                    foreach (var chiphi in goithau.ListChiPhi)
                    {
                        chiphi.IIdHopDongGoiThauNhaThauId = goithau.Id;
                        foreach (var hm in chiphi.ListHangMuc)
                        {
                            hm.IIDHopDongGoiThauNhaThauID = goithau.Id;
                        }
                        foreach (var hm in chiphi.ListVdtDaHopDongDmHangMuc)
                        {
                            hm.IIDHopDongGoiThauNhaThauID = goithau.Id;
                        }
                        ctx.VdtDaHopDongGoiThauHangMucs.AddRange(chiphi.ListHangMuc);
                        ctx.VdtDaHopDongDmHangMucs.AddRange(chiphi.ListVdtDaHopDongDmHangMuc);
                    }
                    ctx.Set<VdtDaHopDongGoiThauChiPhi>().AddRange(goithau.ListChiPhi);
                    goithau.IIDHopDongID = vdtDaTtHopDongDC.Id;
                }
                ctx.VdtDaHopDongGoiThauNhaThaus.AddRange(vdtDaTtHopDongDC.ListGoiThau);
                ctx.SaveChanges();
            }
        }
    }
}
