/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 11/12/2024 5:39:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_solieuchitiet_index_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 11/12/2024 5:39:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]    Script Date: 11/12/2024 5:39:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_skt_phan_bo_so_kiem_tra_dv]
	@idDV nvarchar(max),
	@idChungTu nvarchar(max),
	@LoaiChungTu int,
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@dvt int,
	@iLoai int,
	@iLoaiNNS int
AS
BEGIN
SELECT case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	   else ml.SL end AS L ,
	   ml.SK AS K ,
	   ml.sM AS M ,
	   ml.sNG AS NG ,
       ml.sKyHieu AS KyHieu ,
       ml.sMoTa AS MoTa ,
       ml.iID_MLSKT AS IdMucLuc ,
       ml.iID_MLSKTCha AS IdParent ,
       ml.sSTT AS STT ,
       ml.sSTTBC AS SSTTBC ,
       ml.bHangCha ,
	   ct.sGhiChu,
       TuChi =ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       HuyDong =ISNULL(SUM(ct.fHuyDongTonKho), 0)/@dvt ,
       PhanCap =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       MuaHangHienVat =ISNULL(SUM(ct.fMuaHangCapHienVat), 0)/@dvt ,
       DacThu =ISNULL(SUM(ct.fPhanCap), 0)/@dvt ,
       TongCongNSSD = ISNULL(SUM(ct.fTuChi), 0)/@dvt ,
       TongCongNSBD = ISNULL(SUM(ct.fTuChi), 0)/@dvt
FROM NS_SKT_MucLuc ml
LEFT JOIN
(
	SELECT ctct.* FROM NS_SKT_ChungTuChiTiet ctct
	INNER JOIN NS_SKT_ChungTu ctc 
	ON ctct.iID_CTSoKiemTra = ctc.iID_CTSoKiemTra 
	AND ctc.iNamLamViec = ctct.iNamLamViec
	AND ctc.iNamLamViec = @NamLamViec
	AND (@iLoaiNNS = 0 OR ctc.iLoaiNguonNganSach = @iLoaiNNS)
) ct
ON ml.sKyHieu = ct.sKyHieu AND ml.iNamLamViec = ct.iNamLamViec
	AND ct.iNamLamViec = @NamLamViec
	AND ct.iLoai = @iLoai
	AND ct.iLoaiChungTu = @LoaiChungTu
	AND ct.iID_MaDonVi = @idDV
	--AND ct.iID_CTSoKiemTra = @idChungTu
	AND ct.iNamNganSach = @NamNganSach
	AND ct.iID_MaNguonNganSach = @NguonNganSach
WHERE ml.iTrangThai = 1
	AND ml.iNamLamViec = @NamLamViec

GROUP BY case when isnull(ml.sNG_Cha, '') = '' and ml.iID_MLSKTCha <> '00000000-0000-0000-0000-000000000000' then null
	     else ml.SL end,
		 ml.sK , ml.sM , ml.sNG ,
         ml.sKyHieu ,
         ml.sMoTa ,
         ml.iID_MLSKT ,
         ml.iID_MLSKTCha ,
         ml.sSTT ,
         ml.sSTTBC ,
         ml.bHangCha,
		 ct.sGhiChu
ORDER BY ml.sKyHieu;
END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_solieuchitiet_index_3]    Script Date: 11/12/2024 5:39:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_solieuchitiet_index_3]
@YearOfWork int,
@YearOfBudget int,
@BudgetSource int,
@Loai int,
@TypeGet int,
@AgencyId nvarchar(500),
@LoaiChungTu nvarchar(50),
@Lns nvarchar(max),
@VoucherId nvarchar(max)
WITH RECOMPILE

AS
BEGIN
SET NOCOUNT ON;
select * into #lnsTem from f_split(@Lns);

SELECT * INTO #t1
FROM NS_DTDauNam_ChungTuChiTiet
WHERE iNamLamViec = @YearOfWork
AND (iLoai = 3)
AND iNamNganSach = @YearOfBudget
AND iID_MaNguonNganSach =@BudgetSource
AND (iID_MaDonVi = @AgencyId
--OR (@AgencyId = '00'
-- AND bKhoa = 0))
OR (EXISTS (SELECT * FROM DonVi WHERE iLoai = '0' AND iID_MaDonVi = @AgencyId) AND bKhoa = 0))
AND iID_CTDTDauNam = @VoucherId
AND iLoaiChungTu = @LoaiChungTu;

SELECT sNS_XauNoiMa, sSKT_KyHieu INTO #t2
FROM NS_MLSKT_MLNS
WHERE iNamLamViec = @YearOfWork;
CREATE INDEX IX_t2
ON #t2 (sNS_XauNoiMa,sSKT_KyHieu)

select IsNULL(Sum(fTuChi),0) as fTuChi, iID_CTDTDauNamChiTiet INTO #t3
from NS_DTDauNam_PhanCap
group by iID_CTDTDauNamChiTiet;

SELECT DISTINCT isnull(chitiet.iID_CTDTDauNamChiTiet, NEWID()) AS Id,
chitiet.iID_CTDTDauNamChiTiet AS IdDb,
mlns.iID_MLNS AS MlnsId,
mlns.iID_MLNS_Cha AS MlnsIdParent,
mlns.sXauNoiMa AS XauNoiMa,
mlns.sLNS AS LNS,
mlns.sL AS L,
mlns.sK AS K,
mlns.sM AS M,
mlns.sTM AS TM,
mlns.sTTM AS TTM,
mlns.sNG AS NG,
mlns.sTNG AS TNG,
mlns.sTNG1 AS TNG1,
mlns.sTNG2 AS TNG2,
mlns.sTNG3 AS TNG3,
mlns.sMoTa AS MoTa,
'' AS Chuong,
mlns.bHangCha,
mlns.sChiTietToi AS ChiTietToi,
chitiet.iID_MaDonVi AS IdDonVi,
chitiet.sTenDonVi AS TenDonVi,
chitiet.fTuChi AS ChiTiet,
chitiet.fUocThucHien AS UocThucHien,
chitiet.fHangNhap AS HangNhap,
chitiet.fHangMua AS HangMua,
ISNULL(chitiet.fPhanCap,0) AS PhanCap,
case when chitiet_phancap.fTuChi is null then ISNULL(chitiet.fPhanCap,0) else ISNULL(chitiet.fPhanCap,0) - ISNULL(chitiet_phancap.fTuChi,0) end as PhanCapConLai,
ISNull(chitiet_phancap.fTuChi,0) as TuChi,
chitiet.fChuaPhanCap AS ChuaPhanCap,
chitiet.fMucTienPhanBo AS MucTienPhanBo,
map.sSKT_KyHieu AS SKT_KyHieu,
mlns.bHangChaDuToan
FROM NS_MucLucNganSach mlns
LEFT JOIN
#t1 chitiet ON mlns.sXauNoiMa = chitiet.sXauNoiMa
LEFT JOIN
#t2 map ON mlns.sXauNoiMa = map.sNS_XauNoiMa

LEFT JOIN
#t3 as chitiet_phancap ON chitiet_phancap.iID_CTDTDauNamChiTiet = chitiet.iID_CTDTDauNamChiTiet
--inner join lnsTem ON mlns.sLNS = LNSTEM.Item
WHERE mlns.iNamLamViec = @YearOfWork
AND mlns.iTrangThai = 1
AND mlns.bHangChaDuToan IS NOT NULL
AND (mlns.sLNS = '1'
OR ((mlns.sLNS like '104%'
AND @LoaiChungTu = '2')
OR (mlns.sLNS not like '104%'
AND @LoaiChungTu = '1')))
--AND mlns.sLNS IN (SELECT * from f_split(@Lns))
AND mlns.sLNS IN (select * from #lnsTem)
--AND EXISTS (SELECT * AS sLNS from f_split(@Lns) where Item = mlns.sLNS)
ORDER BY mlns.sLNS,
mlns.sL,
mlns.sK,
mlns.sM,
mlns.sTM,
mlns.sTTM,
mlns.sNG,
mlns.sTNG;

drop table #lnsTem;
drop table #t1;
drop table #t2;
drop table #t3;

END
GO
