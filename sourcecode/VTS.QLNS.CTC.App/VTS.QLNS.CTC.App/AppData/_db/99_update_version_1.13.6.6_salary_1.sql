/****** Object:  StoredProcedure [dbo].[sp_tl_update_QLThuNop_bhxh_hsqcs]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_update_QLThuNop_bhxh_hsqcs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_update_QLThuNop_bhxh_hsqcs]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quanly_thunop_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_QuanLyThuNop_bhxh]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_tao_QuanLyThuNop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_tao_QuanLyThuNop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_QlThuNop]    Script Date: 12/8/2023 5:36:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_QlThuNop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_QlThuNop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_QlThuNop]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_delete_QlThuNop]
	@idDsbangLuong NVARCHAR(MAX)
AS
BEGIN
	DELETE FROM TL_QuanLyThuNop_BHXH_ChiTiet 
	WHERE TL_QuanLyThuNop_BHXH_ChiTiet.iId_ParentId IN (SELECT * FROM f_split(@idDsbangLuong))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER Procedure [dbo].[sp_tl_delete_bang_luong]
--	@idBangLuong nvarchar(MAX)
--As
--Begin
--	Delete From [dbo].[TL_DS_CapNhap_BangLuong]
--	Where Id IN (SELECT *
--	FROM f_split(@idBangLuong))
--	Delete From [dbo].[TL_BangLuong_Thang]
--	Where parent IN (SELECT *
--	FROM f_split(@idBangLuong))
--End

CREATE Procedure [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]
	@thang int, 
	@nam int,
	@maDonVi nvarchar(MAX),
	@maCachTl nvarchar(10)
As
Begin
	Delete From [dbo].[TL_QuanLyThuNop_BHXH]
	Where iThang = @thang and iNam = @nam and iID_MaDonVi in (select * from f_split(@maDonVi)) and sMa_CachTL = @maCachTl
	Delete From [dbo].[TL_QuanLyThuNop_BHXH_ChiTiet]
	Where iThang = @thang and iNam = @nam and iID_MaDonVi in (select * from f_split(@maDonVi)) and sMa_CachTL = @maCachTl
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_QuanLyThuNop_bhxh]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE Procedure [dbo].[sp_tl_get_donvi_tao_QuanLyThuNop_bhxh] @nam int, 
  @thang int, 
  @cachTinhLuong varchar(20) 
  As 
  Begin 
  WITH DonViBangLuong as (
    Select 
      distinct(donVi.Id), 
      donVi.Ma_DonVi, 
      donVi.Parent_id, 
      donVi.Ten_DonVi, 
      donVi.XauNoiMa, 
      donVi.iTrangThai 
    From 
      TL_DM_DonVi as donVi 
      Left Join TL_QuanLyThuNop_BHXH on donVi.Ma_DonVi = TL_QuanLyThuNop_BHXH.iID_MaDonVi 
      And TL_QuanLyThuNop_BHXH.iThang = @thang 
      And TL_QuanLyThuNop_BHXH.iNam = @nam 
      And TL_QuanLyThuNop_BHXH.sMa_CachTL = @cachTinhLuong 
    Where 
      TL_QuanLyThuNop_BHXH.Id is Null 
      AND donVi.iTrangThai = 1
  ), 
  DonViCanBo as (
    Select 
      distinct(donVi.Id), 
      donVi.Ma_DonVi, 
      donVi.Parent_id, 
      donVi.Ten_DonVi, 
      donVi.XauNoiMa 
    From 
      TL_DM_DonVi as donVi 
      Left Join TL_DM_CanBo on donVi.Ma_DonVi = TL_DM_CanBo.Parent 
      And TL_DM_CanBo.IsDelete = 1 
      And TL_DM_CanBo.Thang = @thang 
      And TL_DM_CanBo.Nam = @nam 
    Where 
      Ma_CanBo is not null
  ) 
Select 
  distinct(DonViBangLuong.Id), 
  DonViBangLuong.Ma_DonVi, 
  DonViBangLuong.Parent_id, 
  DonViBangLuong.Ten_DonVi, 
  DonViBangLuong.XauNoiMa, 
  DonViBangLuong.iTrangThai 
From 
  DonViBangLuong 
  Join DonViCanBo on DonViBangLuong.Id = DonViCanBo.Id 
End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		DungNV
-- Create date: 08/12/2023
-- Description:	Lấy dữ liệu cho thêm mới quản lý thu nộp BHXH
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert] 
	@Thang int , 
	@Nam int ,
	@MaDonVi NVARCHAR(MAX) ,
	@MaCachTl NVARCHAR(50) ,
	@SoNgay int = 30
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN @SoNgay * GIA_TRI 
					ELSE 0
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		WHERE
			canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 4)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
	WHERE canBoPhuCap.MA_PHUCAP IN ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT') -- Chỉ lấy những mã này cho BHXH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select MA_CBO, gia_tri, HuongPC_SN, ma_phucap
	into #canBoPhuCap
	from TL_CanBo_PhuCap where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	SELECT
		canBo.Ma_CanBo AS MaCanBo,
		canBo.Ten_CanBo AS TenCanBo,
		donVi.Ma_DonVi MaDonVi,
		canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
		capBac.Ma_Cb MaCapBac,
		canBo.BHTN,
		canBo.PCCV,
		canBo.BNuocNgoai
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo canBo
	INNER JOIN TL_DM_DonVi donVi
		ON canBo.Parent = donVi.Ma_DonVi
	INNER  JOIN TL_DM_CapBac capBac
		ON canBo.Ma_CB = capBac.Ma_Cb
	WHERE
		canBo.Thang = @Thang
		AND canBo.Nam = @Nam
		AND canBo.Ma_CanBo IN (SELECT MA_CBO FROM #canBoPhuCap WHERE MA_PHUCAP LIKE '%TTL%' AND GIA_TRI > 0 GROUP BY MA_CBO)
		AND donVi.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_TruyLinh WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') 

	select * into #tmpPhuCapLuongTruyLinh
	FROM (
		select ma_cot as Ma_PC from #tmp
		union 
		select ma_phucap as Ma_PC from #tmp
	) AS c

	SELECT
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		canBo.BNuocNgoai			,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM #canBoPhuCap canBoPhuCap
	INNER JOIN #tmpPhuCapLuongTruyLinh phuCapLuongTruyLinh 
		ON phuCapLuongTruyLinh.Ma_PC = canBoPhuCap.MA_PHUCAP
	INNER JOIN #ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN TL_DM_Cach_TinhLuong_TruyLinh cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	WHERE canBoPhuCap.MA_PHUCAP IN ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT') -- Chỉ lấy những mã này cho BHXH

	DROP TABLE #ThongTinCanBo
	DROP TABLE #tmp
	DROP TABLE #tmpPhuCapLuongTruyLinh
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]
 	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap phucap
	where phucap.Ma_PhuCap in ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT')

        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.iThang		AS Thang,
			bangLuongThang.iNam			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bangLuongThang.Gia_Tri		AS GiaTri,
			bangLuongThang.sMa_PhuCap	AS MaPhuCap
		FROM TL_QuanLyThuNop_BHXH_ChiTiet bangLuongThang
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.sMa_CBo = canBo.MaCanBo
		WHERE
			bangLuongThang.iId_ParentId = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_update_QLThuNop_bhxh_hsqcs]    Script Date: 12/8/2023 5:36:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_update_QLThuNop_bhxh_hsqcs]
@iThang int,
@inam int,
@iIdMaDonVi t_tbl_string READONLY
AS
BEGIN
	CREATE TABLE #tmpReplace(Ma_PhuCapGoc nvarchar(500), Ma_PhuCapReplace nvarchar(500))
	INSERT INTO #tmpReplace(Ma_PhuCapGoc, Ma_PhuCapReplace) VALUES
	('BHXHDV_TT', 'BHXHDVCS_TT'),
	('BHYTDV_TT', 'BHYTDVCS_TT')

	SELECT tbl.Id, tbl.sMa_CBo, tmp.Ma_PhuCapGoc, Gia_Tri INTO #tblUpdate
	FROM @iIdMaDonVi as dv
	INNER JOIN TL_QuanLyThuNop_BHXH_ChiTiet as tbl on dv.sId = tbl.iID_MaDonVi
	INNER JOIN #tmpReplace as tmp on tbl.sMa_PhuCap = tmp.Ma_PhuCapReplace
	WHERE tbl.iThang = @iThang AND tbl.iNam = @inam AND sMa_CB LIKE N'0%'

	UPDATE tbl
	SET
		Gia_Tri = tmp.Gia_Tri
	FROM #tblUpdate as tmp
	INNER JOIN TL_QuanLyThuNop_BHXH_ChiTiet as tbl on tmp.sMa_CBo = tbl.sMa_CBo AND tbl.sMa_PhuCap = tmp.Ma_PhuCapGoc

	DROP TABLE #tblUpdate
	DROP TABLE #tmpReplace
END
GO
