/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 21/12/2022 8:22:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_giaithich_bangso_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_giaithich_bangso_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 21/12/2022 8:22:05 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 23/12/2022 10:10:45 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 23/12/2022 2:15:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 23/12/2022 2:21:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 27/12/2022 11:05:58 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_canbo_phucap_saochep]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_canbo_phucap_saochep]    Script Date: 27/12/2022 11:05:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 04/05/2022
-- Description:	Sao chép cán bộ sang tháng mới
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_canbo_phucap_saochep]
	@MaCanBo NVARCHAR(MAX),
	@FromYear int,
	@FromMonth int,
	@ToYear int,
	@ToMonth int,
	@IsCopyValue bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	WITH DsCanBo AS (
		SELECT
			Ma_CanBo,
			Ngay_NN,
			Ngay_XN,
			Ngay_TN,
			Thang_TNN,
			Ma_Hieu_CanBo
		FROM TL_DM_CanBo
		WHERE
			Ma_CanBo IN (SELECT * FROM f_split(@MaCanBo))
	), HsPhuCapTruyLinh AS (
		SELECT
			cboPhuCap.MA_CBO,
			cboPhuCap.MA_PHUCAP + '_CU' AS MA_PHUCAP,
			cboPhuCap.GIA_TRI
		FROM TL_CanBo_PhuCap cboPhuCap
		INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
		WHERE cboPhuCap.MA_PHUCAP IN ('LHT_HS', 'PCCV_HS', 'PCTHUHUT_HS', 'PCCOV_HS', 'PCCU_HS')
	)

	SELECT
		NEWID()					AS Id,
		FORMAT(@ToYear, 'D4') + FORMAT(@ToMonth, 'D2') + cbo.Ma_Hieu_CanBo	AS MaCbo,
		cboPhuCap.MA_PHUCAP		AS MaPhuCap,
		CASE
			WHEN cboPhuCap.MA_PHUCAP IN ('LCS', 'GTNN', 'GTPT_DG', 'TA_DG') THEN phuCap.Gia_Tri 
			WHEN cboPhuCap.MA_PHUCAP IN ('LHT_HS_CU', 'PCCV_HS_CU', 'PCTHUHUT_HS_CU', 'PCCOV_HS_CU', 'PCCU_HS_CU') THEN phuCapTruyLinh.GIA_TRI
			WHEN cboPhuCap.MA_PHUCAP = 'TTL' THEN 0
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND phuCap.IThang_ToiDa IS NOT NULL AND cboPhuCap.ISoThang_Huong >= phuCap.IThang_ToiDa THEN 0
			WHEN cboPhuCap.MA_PHUCAP = 'NTN' THEN (select dbo.f_luong_ntn(cbo.Ngay_NN, cbo.Ngay_XN, cbo.Ngay_TN, cbo.Thang_TNN, @ToMonth, @ToYear))
			WHEN ISNULL(phuCap.bSaoChep, 0) = 0 OR (@IsCopyValue = 1 AND phuCap.bSaoChep = 1) THEN cboPhuCap.GIA_TRI ELSE 0
			--cboPhuCap.bSaoChep IS NOT NULL AND (@IsCopyValue = 0 OR cboPhuCap.bSaoChep = 0) THEN 0 ELSE cboPhuCap.GIA_TRI
		END						AS GiaTri,
		cboPhuCap.HE_SO			AS HeSo,
		cboPhuCap.MA_KMCP		AS MaKmcp,
		cboPhuCap.CONG_THUC		AS CongThuc,
		cboPhuCap.PHANTRAM_CT	AS PhanTramCt,
		cboPhuCap.CHON			AS Chon,
		cboPhuCap.HuongPC_SN	AS HuongPcSn,
		0						AS Flag,
		cboPhuCap.DateStart		AS DateStart,
		cboPhuCap.DateEnd		AS DateEnd,
		CASE
			WHEN cboPhuCap.ISoThang_Huong IS NOT NULL AND (phuCap.IThang_ToiDa IS NULL OR cboPhuCap.ISoThang_Huong < phuCap.IThang_ToiDa) THEN cboPhuCap.ISoThang_Huong + 1
			ELSE cboPhuCap.ISoThang_Huong
		END						AS ISoThang_Huong,
		cboPhuCap.bSaoChep		AS BSaoChep
	FROM TL_CanBo_PhuCap cboPhuCap
	INNER JOIN DsCanBo cbo ON cboPhuCap.MA_CBO = cbo.Ma_CanBo
	LEFT JOIN HsPhuCapTruyLinh phuCapTruyLinh ON cboPhuCap.MA_CBO = phuCapTruyLinh.MA_CBO AND cboPhuCap.MA_PHUCAP = phuCapTruyLinh.MA_PHUCAP
	LEFT JOIN TL_DM_PhuCap phuCap ON cboPhuCap.MA_PHUCAP = phuCap.Ma_PhuCap
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]    Script Date: 23/12/2022 2:21:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = ol.IIdChungTuDuToan
FROM TL_QT_ChungTu as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT DISTINCT XauNoiMa, ISNULL(DDuToan, 0)
	FROM TL_QT_ChungTuChiTiet 
	WHERE Id_ChungTu = @iIdChungTuOld AND ISNULL(DDuToan, 0) <> 0
END

INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   ISNULL(tmp.DDuToan, 0),
	   ct.MaCb
FROM TL_QT_ChungTuChiTiet ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa,
		 ISNULL(tmp.DDuToan, 0),
		 ct.MaCb

	-- add du toan thang truoc da co
	INSERT INTO [dbo].[TL_QT_ChungTuChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
	SELECT @idChungTu, 
		ml.iID_MLNS, 
		ml.iID_MLNS_Cha, 
		tmp.sXauNoiMa, 
		ml.sLNS,
		ml.sL, 
		ml.sK, 
		ml.sM, 
		ml.sTM, 
		ml.sTTM, 
		ml.sNG,
		ml.sTNG, 
		ml.sTNG1, 
		ml.sTNG2, 
		ml.sTNG3,
		ml.sMoTa,
		NULL, 
		@NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       Isnull(TongCong, 0),
       ml.bHangCha,
       NULL,
       Isnull(SoNgay, 0),
       Isnull(SoNguoi, 0),
       Isnull(DieuChinh, 0),
	   '',
	   ISNULL(tmp.DDuToan, 0),
	   dt.MaCb
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet]    Script Date: 23/12/2022 2:15:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN

with ctct as (
  select Id as Id, XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
	   convert(decimal,Sum(SoNgay)) as SoNgay,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan,
	 MaCb
  from TL_QT_ChungTuChiTiet 
  where   Id_ChungTu in (SELECT *  FROM f_split(@idChungTu))
    AND MaCachTl in (SELECT *  FROM f_split(@maCachTl))
  group by id, XauNoiMa, MaCachTl, MaCb
)
SELECT 
     ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan, 
	 SoNgay,
	 MaCb,
	 cb.Ten_Cb as LoaiDoiTuong
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
left join tl_dm_capbac cb on ctct.MaCb = cb.Ma_Cb

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by mlns.sXauNoiMa

END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 23/12/2022 10:10:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	FROM 
	(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
	INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	INNER JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	WITH LuongCapBac AS (
		SELECT
			dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.Ma_CB				AS MaCapBac,
			capBac.Parent				AS Ngach,
			SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
			SUM(
				CASE WHEN pc.Ma_PhuCap IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN (dbo.fnTotalDayOfMonth(@thang,@nam)*bangLuong.Gia_Tri)
					WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN ISNULL(cbpc1.HuongPC_SN, 0) * bangLuong.Gia_Tri
					ELSE bangLuong.Gia_Tri END
			)		AS GiaTri,
			COUNT(bangLuong.Ma_CBo)		AS SoNguoi
		FROM TL_BangLuong_Thang bangLuong
		INNER JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
		INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
		LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		JOIN TL_DM_CapBac capBac
			ON bangLuong.Ma_CB = capBac.Ma_Cb
		WHERE
			dsCapNhapBangLuong.Ma_CachTL IN (SELECT * FROM f_split(@maCachTl))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
			AND bangLuong.Gia_Tri != 0
		GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
	), LuongCapBacMlns AS (
		SELECT
			luongCapBac.MaDonVi,
			phuCapMlns.XauNoiMa,
			phuCapMlns.Ma_Cb,
			SoNguoi,
			SoNgay,
			GiaTri
		FROM TL_PhuCap_MLNS phuCapMlns
		JOIN LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
		WHERE
			phuCapMlns.Nam = @nam
	),

	DataDuToan as (
		Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
		From TL_QT_ChungTuChiTiet ctchitiet
		Join TL_QT_ChungTu chungtu
		on chungtu.ID = ctchitiet.Id_ChungTu
		Where Nam = @nam
		And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
		Group By XauNoiMa, Ma_DonVi
	)

	SELECT
		luong.MaDonVi					AS MaDonVi,
		NEWID()							AS Id,
		iID_MLNS						AS MlnsId, 
		iID_MLNS_Cha					AS MlnsIdParent, 
		sXauNoiMa						AS XauNoiMa, 
		sLNS							AS Lns, 
		sL								AS L, 
		sK								AS K, 
		sM								AS M, 
		sTM								AS TM, 
		sTTM							AS TTM, 
		sNG								AS Ng, 
		sTNG							AS TNG, 
		sTNG1							AS TNG1, 
		sTNG2							AS TNG2, 
		sTNG3							AS TNG3, 
		sMoTa							AS Mota, 
		iNamLamViec						AS NamLamViec,
		bHangCha						AS BHangCha,
		sChiTietToi						AS ChiTietToi,
		CONVERT(decimal, SUM(SoNguoi))	AS SoNguoi,
		SUM(ISNULL(SoNgay, 0))			AS SoNgay,
		SUM(GiaTri)						AS TongCong,
		SUM(GiaTri)						AS DieuChinh,
		@maCachTl						AS MaCachTl,
		ISNULL(dataDuToan.DuToan, 0)	AS DDuToan,
		luong.Ma_Cb						AS MaCb
	FROM NS_MucLucNganSach mlns
	JOIN LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;

GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export]    Script Date: 21/12/2022 8:22:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;

	select DISTINCT XauNoiMa  into #tmpPcMlns FROM TL_PhuCap_MLNS WHERE Nam = @nam;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl
	),
	lstSoNguoi as (
		SELECT XauNoiMa,
			SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
		from TL_QT_ChungTu as tbl
		INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
		where tbl.ID in (SELECT TOP(1) ID FROM #tblMaxThang ORDER BY Thang DESC)
		AND ((@isHaveCachTinhLuong = 0 AND dt.MaCachTl = '') OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa
	)
	,
	phucapMlns as (
		SELECT TL_PhuCap_MLNS.XauNoiMa, min(TL_PhuCap_MLNS.Ma_Cb) as Ma_Cb FROM #tmpPcMlns 
		left join TL_PhuCap_MLNS on #tmpPcMlns.XauNoiMa = TL_PhuCap_MLNS.XauNoiMa
		WHERE Nam = @nam
		group by TL_PhuCap_MLNS.XauNoiMa
	)


SELECT 
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
	SoNgay,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa 

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
drop table #tmpPcMlns
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export]    Script Date: 21/12/2022 8:22:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_giaithich_bangso_export]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu as tbl on tmp.Item = tbl.ID;


	with ctct as (
	  select dt.XauNoiMa,  MaCachTl, MaCb,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.MaCb
	)

SELECT 
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
	   ctct.MaCb as MaCb,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
       DieuChinh,
     DDuToan
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
;
;

GO
