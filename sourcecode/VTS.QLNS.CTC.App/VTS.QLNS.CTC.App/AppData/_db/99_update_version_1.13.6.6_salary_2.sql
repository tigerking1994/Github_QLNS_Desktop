/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/12/2023 8:24:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quanly_thunop_bhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]    Script Date: 12/12/2023 8:24:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]    Script Date: 12/12/2023 8:24:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_insert_qlthunop_bhxh_chitiet_tonghop]    Script Date: 12/12/2023 8:24:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_insert_qlthunop_bhxh_chitiet_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_insert_qlthunop_bhxh_chitiet_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]    Script Date: 12/12/2023 8:24:48 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_tl_quan_ly_thu_nop_bhxh]    Script Date: 12/12/2023 8:24:48 AM ******/
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
	@maCachTl nvarchar(10),
	@IsAggregate bit,
	@IdAggregate uniqueidentifier

As
Begin
IF(@IsAggregate = 1)
BEGIN

	DECLARE @lstIds  nvarchar(max) = (SELECT STongHop FROM TL_QuanLyThuNop_BHXH WHERE Id = @IdAggregate);
	UPDATE TL_QuanLyThuNop_BHXH
	SET IsTongHop = 0
	WHERE Id in (SELECT * FROM splitstring(@lstIds));
	--Delete detail
	Delete From [dbo].[TL_QuanLyThuNop_BHXH_ChiTiet]
	WHERE iId_ParentId = @IdAggregate;
	-- DElete entity
	Delete From [dbo].[TL_QuanLyThuNop_BHXH]
			WHERE Id = @IdAggregate;

END
ELSE
BEGIN
	Delete From [dbo].[TL_QuanLyThuNop_BHXH]
	Where iThang = @thang and iNam = @nam and iID_MaDonVi in (select * from f_split(@maDonVi)) and sMa_CachTL = @maCachTl
	Delete From [dbo].[TL_QuanLyThuNop_BHXH_ChiTiet]
	Where iThang = @thang and iNam = @nam and iID_MaDonVi in (select * from f_split(@maDonVi)) and sMa_CachTL = @maCachTl
END

End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_insert_qlthunop_bhxh_chitiet_tonghop]    Script Date: 12/12/2023 8:24:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_insert_qlthunop_bhxh_chitiet_tonghop]
	-- Add the parameters for the stored procedure here
	@iIdParent uniqueidentifier,
	@ListIdChungTus nvarchar(max),
	@MaDonVi nvarchar(50),
	@NamLamViec int,
	@Thang int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	-- Insert statements for procedure here
	BEGIN
	INSERT INTO TL_QuanLyThuNop_BHXH_ChiTiet(Id,Gia_Tri,iLoai,sMa_CachTL, sMa_CB, sMa_CBo, iID_MaDonVi, sMa_Hieu_CanBo, sMa_PhuCap, iNam, dNgay_HT, iId_ParentId, iSo_TT, sTen_CachTL, sTen_Cbo, iThang,sUserName)
	SELECT NEWID(), Gia_Tri, iLoai, sMa_CachTL, sMa_CB, sMa_CBo, @MaDonVi, sMa_Hieu_CanBo, sMa_PhuCap, @NamLamViec, dNgay_HT,@iIdParent, iSo_TT, sTen_CachTL, sTen_Cbo, @Thang, sUserName 
	FROM TL_QuanLyThuNop_BHXH_ChiTiet WHERE iId_ParentId IN (SELECT * FROM splitstring(@ListIdChungTus));
	END

	UPDATE TL_QuanLyThuNop_BHXH
	SET IsTongHop = 1
	WHERE Id IN  (SELECT * FROM splitstring(@ListIdChungTus));
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThunop_bhxh_dulieu_insert]    Script Date: 12/12/2023 8:24:48 AM ******/
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
	where canBoPhuCap.Ma_PhuCap in ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT', 'BHCN', 'LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')
END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_qlThuNop_bhxh_truylinh_dulieu_insert]    Script Date: 12/12/2023 8:24:48 AM ******/
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
	WHERE canBoPhuCap.Ma_PhuCap in ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT', 'BHCN', 'LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')

	DROP TABLE #ThongTinCanBo
	DROP TABLE #tmp
	DROP TABLE #tmpPhuCapLuongTruyLinh
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quanly_thunop_bhxh_chitiet]    Script Date: 12/12/2023 8:24:48 AM ******/
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
	where phucap.Ma_PhuCap in ('BHXHCN_TT', 'BHXHDV_TT', 'BHTNCN_TT', 'BHTNDV_TT','BHYTCN_TT', 'BHYTDV_TT' , 'BHCN_TT' , 'BHYTCNCS_TT','BHYTDVCS_TT', 'BHCN', 'LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')

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
