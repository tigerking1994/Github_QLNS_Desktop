/****** Object:  StoredProcedure [dbo].[sp_tn_dtdn_chungtuchitiet_create_data_summary]    Script Date: 10/24/2024 8:13:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dtdn_chungtuchitiet_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dtdn_chungtuchitiet_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dtdn_chungtu_chitiet]    Script Date: 10/24/2024 8:13:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dtdn_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dtdn_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dtdn_chungtu_chitiet]    Script Date: 10/24/2024 8:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dtdn_chungtu_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		isnull(ctct.iNamNganSach, @YearOfBudget) as INamNganSach,
		ctct.iID_MaNguonNganSach as IIdMaNguonNganSach,
		ctct.iNamLamViec INamLamViec,
		mlns.bHangCha,
		ctct.iID_MaDonVi IIdMaDonVi,
		ctct.sTenDonVi as STenDonVi,
		mlns.sChiTietToi as ChiTietToi,
		isnull(ctct.sGhiChu, '') as SGhiChu,
		isnull(ctct.fDuToan_NamKeHoach, 0) as FDuToanNamKeHoach,
		isnull(ctct.fDuToan_NamNay, 0) as FDuToanNamNay,
		isnull(ctct.fThucThu_NamTruoc, 0) as FThucThuNamTruoc,
		isnull(ctct.fUocThucHien_NamNay, 0) as FUocThucHienNamNay,

		ctct.dNgayTao DNgayTao,
		ctct.dNgaySua as DNgaySua,
		isnull(ctct.SNguoiTao, '') as SNguoiTao ,
		isnull(ctct.SNguoiSua, '') as SNguoiSua
	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND Id_ChungTu = @ChungTuId
			--AND LNS in (select * from dbo.splitstring(@LNS))
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dtdn_chungtuchitiet_create_data_summary]    Script Date: 10/24/2024 8:13:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dtdn_chungtuchitiet_create_data_summary]
	@chungTuId nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@ChungTuTongHop nvarchar(max),
	@NguoiTao nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @fDuToan_NamKeHoachSum float
	DECLARE @fDuToan_NamNaySum float
	DECLARE @fThucThu_NamTruocSum float
	DECLARE @fUocThucHien_NamNaySum float
	DECLARE @TenDonVi nvarchar(max)
	SET @TenDonVi = (select sTenDonVi FROM DonVi where iNamLamViec = @YearOfWork and iID_MaDonVi = @AgencyId);

--GetData
SELECT 
	   sXauNoiMa,
       sLNS,
       sL,
       sK,
       sM,
       sTM,
       sTTM,
       sNG,
       sTNG,
       iNamNganSach,
       iID_MaNguonNganSach,
       iNamLamViec,
       bHangCha,
       @AgencyId iID_MaDonVi,
       @TenDonVi sTenDonVi,
       sum(fDuToan_NamKeHoach) fDuToan_NamKeHoach,
       sum(fDuToan_NamNay) fDuToan_NamNay,
       sum(fThucThu_NamTruoc) fThucThu_NamTruoc,
       sum(fUocThucHien_NamNay) fUocThucHien_NamNay,
       '' sGhiChu,
       GETDATE() dNgayTao,
       @NguoiTao sNguoiTao,
       GETDATE() dNgaySua,
       @NguoiTao sNguoiSua,
       0 bKhoa,
	   @chungTuId iD_ChungTu INTO #tempData
FROM TN_DTDN_ChungTuChiTiet
WHERE iNamLamViec = @YearOfWork
  AND iNamNganSach = @YearOfBudget
  AND iID_MaNguonNganSach =@BudgetSource
  AND iD_ChungTu IN (SELECT * FROM f_split(@ChungTuTongHop))	
GROUP BY sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, iNamNganSach, iID_MaNguonNganSach, iNamLamViec,
bHangCha, iD_ChungTu;
--INSERT DATA TONG HOP
INSERT INTO TN_DTDN_ChungTuChiTiet(sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, iNamNganSach, iID_MaNguonNganSach,
iNamLamViec, bHangCha, iID_MaDonVi, sTenDonVi, fDuToan_NamKeHoach, fDuToan_NamNay, fThucThu_NamTruoc, fUocThucHien_NamNay,
 sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNguoiSua, bKhoa, iD_ChungTu)
select * from #tempData;


select        
@fDuToan_NamKeHoachSum = 	   sum(fDuToan_NamKeHoach) ,
@fDuToan_NamNaySum      = sum(fDuToan_NamNay) ,
@fThucThu_NamTruocSum     =  sum(fThucThu_NamTruoc) ,
@fUocThucHien_NamNaySum      = sum(fUocThucHien_NamNay) 
from #tempData;

--update total
UPDATE TN_DTDN_ChungTu
SET 
fTongDuToan_NamKeHoach = @fDuToan_NamKeHoachSum,
fTongDuToan_NamNay = @fDuToan_NamNaySum,
fTongThucThu_NamTruoc = @fThucThu_NamTruocSum,
fTongUocThucHien_NamNay = @fUocThucHien_NamNaySum,
dNgaySua = GETDATE(),
sNguoiSua = @NguoiTao

WHERE Id = @chungTuId;

--update chung tu tong hop
UPDATE TN_DTDN_ChungTu
SET
bDaTongHop = 1,
dNgaySua = GETDATE(),
sNguoiSua = @NguoiTao
WHERE id IN (SELECT * FROM f_split(@ChungTuTongHop))	;


DROP TABLE #tempData;
END
;
;
;
;
;
;
;
GO
;;
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 10/28/2024 10:46:18 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 10/28/2024 10:46:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
	@agencies nvarchar(max) = '51,5103,5106,5109',
	@LNS nvarchar(max) = '801,80101,8010101,8010102,8010103,8010104,8010106,8010107,8010199,80102,8010201,8010202,8010203,8010204,8010205,8010206,8010207,8010299,801,80101,8010101,8010102,8010103,8010104,8010106,8010107,8010199,80102,8010201,8010202,8010203,8010204,8010205,8010206,8010207,8010299,8010102,8010103,8010107,8010199,801,80101,8010101,8010102,8010103,8010104,8010106,8010107,8010199,80102,8010201,8010202,8010203,8010204,8010205,8010206,8010207,8010299',
	@YearOfWork int = 2024,
	@YearOfBudget int = 2,
	@BudgetSource int =1
AS
BEGIN

	SELECT
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		CAST(isnull(ctct.iNamNganSach, @YearOfBudget) as int) as INamNganSach,
		CAST(ctct.iID_MaNguonNganSach AS int)as IIdMaNguonNganSach,
		CAST(ctct.iNamLamViec AS int) INamLamViec,
		mlns.bHangCha,
		ctct.iID_MaDonVi IIdMaDonVi,
		ctct.sTenDonVi as STenDonVi,
		mlns.sChiTietToi as ChiTietToi,
		FDuToanNamKeHoach,
		FDuToanNamNay,
		FThucThuNamTruoc,
		FUocThucHienNamNay

	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
		SUM(isnull(fDuToan_NamKeHoach, 0)) as FDuToanNamKeHoach,
		SUM(isnull(fDuToan_NamNay, 0)) as FDuToanNamNay,
		SUM(isnull(fThucThu_NamTruoc, 0)) as FThucThuNamTruoc,
		SUM(isnull(fUocThucHien_NamNay, 0)) as FUocThucHienNamNay,
		iNamLamViec,iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach,sXauNoiMa ,sTenDonVi
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_MaDonVi in (select * from dbo.splitstring(@agencies))
		GROUP BY iNamLamViec, iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach, sXauNoiMa,sTenDonVi
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO

