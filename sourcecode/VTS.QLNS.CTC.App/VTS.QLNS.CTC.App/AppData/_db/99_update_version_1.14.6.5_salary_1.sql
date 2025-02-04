/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_thue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_data_thue_tncn]
As
SELECT TenCb,
		  MaCanBo,
		  MaCb,
		  Thang,
		  Nam,
		  MaDonVi,
		  TenDonVi,
		  SoSoLuong,
          THUONG_TT AS TienThuong,
          THUNHAPKHAC_TT AS LoiIchKhac,
          PHAITRUKHAC_SUM AS TienThueDuocGiam,
          THUEDANOP_TT AS ThueTNCNDaNop
   FROM
     (SELECT canBo.Ma_CanBo MaCanBo,
             canBo.Ten_CanBo AS TenCb,
			 canbo.Ma_CB as MaCb,
			 canBo.Thang, 
			 canBo.Nam,
			 canBo.Parent as MaDonVi,
			 donvi.Ten_DonVi as TenDonVi,
			 canBo.So_SoLuong as SoSoLuong,
             bangLuong.GIA_TRI AS GiaTri,
             bangLuong.MA_PHUCAP AS MaPhuCap
      FROM Tl_CanBo_PhuCap bangLuong
      JOIN TL_DM_CanBo canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
	  LEFT JOIN TL_DM_DonVi donvi ON canbo.Parent = donvi.Ma_DonVi
      WHERE
		canBo.IsDelete=1
        AND canBo.Khong_Luong=0) x PIVOT (SUM(GiaTri)
                                           FOR MaPhuCap IN (THUONG_TT, THUNHAPKHAC_TT, PHAITRUKHAC_SUM, THUEDANOP_TT)) pvt
      Order By MaDonVi
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
	@Thang int,
	@Nam AS int,
	@MaCachTL NVARCHAR(50),
	@MaDonVi NVARCHAR(MAX),
	@IsExportAll bit,
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'GTNN,GTPT_SN,GTPT_DG,GTKHAC_TT,LUONGTHUE_TT,THUETNCN_TT,GIAMTHUE_TT,THUEDANOP_TT,LHT_TT,PCCT_TT,THUONG_TT,THUNHAPKHAC_TT,BHCN_TT,GTPT_TT,PHAITRUKHAC_SUM'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTL + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			ISNULL(canBo.Ma_CB, ''0'')							AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'')						AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0)							AS HSChucVu,
			chucVu.Ma_Cv										AS MaChucVu,
			chucVu.Ten_Cv										AS TenChucVu
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang = ' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, (GTPT_SN*GTPT_DG) GIAM_TRU_PHU_THUOC, 
	(LHT_TT + PCCT_TT + THUONG_TT + ISNULL(THUNHAPKHAC_TT, 0) - GTPT_TT - BHCN_TT - PHAITRUKHAC_SUM) TINHTHUE,
		' + @Cols + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.HSChucVu,
			canBo.MaChucVu,
			canBo.TenChucVu,
			bangLuong.GiaTri,
			bangLuong.MaPhuCap
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE (1=1)'

	--WHERE (' + CAST(@IsExportAll AS VARCHAR(1)) + ' = 1 OR THUETNCN_TT > 0)'
	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
	execute(@Query)

	--select @Query
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 7/3/2024 5:23:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
	@Nam AS int,
	@MaDonVi NVARCHAR(MAX),
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Query AS NVARCHAR(MAX)

	DECLARE @Cols1 AS NVARCHAR(MAX) DECLARE @Cols2 AS NVARCHAR(MAX)
	SET @Cols1 = 'BHCN_TT,LHT_TT,PCCT_TT,PHAITRUKHAC_SUM'
	SET @Cols2 = 'THUONG_TT,THUNHAPKHAC_TT,GTNN,THUETNCN_TT,THUEDANOP_TT,GTPT_TT';
	SET @Query =
	'
	WITH BangLuongThangCaHai AS (
		SELECT
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.Ma_CBo			AS MaCanBo,
			bangLuong.MA_PHUCAP			AS MaPhuCap,
			bangLuong.GIA_TRI			AS GiaTri,
			bangLuong.Ma_Hieu_CanBo		AS MaHieuCanBo,
			bangLuong.Ma_DonVi          AS MaDonVi
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols1 + '''))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), DanhSachCanBo AS (
		SELECT Parent,
			 Ma_CanBo,
			 Ten_CanBo,
			 Ma_Hieu_CanBo,
			 Ma_CB,
			 Ma_Cv
		FROM 
		(SELECT canBo.Parent,
			 canBo.Ma_CanBo,
			 canBo.Ten_CanBo,
			 canBo.Ma_Hieu_CanBo,
			 canBo.Ma_CB,
			 canbo.Ma_Cv,
			 ROW_NUMBER()
			OVER (PARTITION BY canBo.Parent, canBo.Ma_Hieu_CanBo
		ORDER BY  canBo.Thang DESC) AS RowNum
		FROM TL_DM_CanBo canBo 
		WHERE canBo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))) as dscbt
	WHERE RowNum = 1
	), ThongTinCanBo AS (
		SELECT MaDonVi,
			MaCanBo,
			TenCanBo,
			Ten,
			MaHieuCanBo,
			MaCapBac,
			CapBac,
			HSChucVu,
			MaChucVu,
			TenChucVu
		FROM
		(SELECT
			canBo.Parent										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			canBo.Ma_Hieu_CanBo                                 AS MaHieuCanBo,
			ISNULL(canBo.Ma_CB, ''0'')							AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'')						AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0)							AS HSChucVu,
			chucVu.Ma_Cv										AS MaChucVu,
			chucVu.Ten_Cv										AS TenChucVu,
			ROW_NUMBER()
			OVER (PARTITION BY canBo.Parent, canBo.Ma_Hieu_CanBo
		ORDER BY  canBo.Thang DESC) AS RowNum
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB=capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	) as ttcb 
	WHERE RowNum = 1),

	BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Nam		AS Nam,
          bangLuong.MA_PHUCAP			AS MaPhuCap,
          bangLuong.GIA_TRI				AS GiaTri,
          bangLuong.Ma_Hieu_CanBo		AS MaHieuCanBo,
          bangLuong.Ma_CBo				AS MaCanBo,
		  bangLuong.Ma_DonVi            AS MaDonVi
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols2 + '''))
     AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
     AND dsCapNhapBangLuong.Ma_Cbo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
     AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
     AND dsCapNhapBangLuong.Status=1 ),
	 Cach0 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
		  HSChucVu,
          SUM(GTNN) GTNN,
          SUM(THUETNCN_TT) THUETNCN_TT,
          SUM(THUEDANOP_TT) THUEDANOP_TT,
          SUM(THUONG_TT) THUONG_TT,
          SUM(THUNHAPKHAC_TT) THUNHAPKHAC_TT,
		  SUM(GTPT_TT) GTPT_TT,
		  (SUM(GTPT_TT) - SUM(GTNN)) GTPT_DG_SN
   FROM
     (SELECT canBo.MaDonVi,
             canBo.MaHieuCanBo,
             canBo.TenCanBo,
			 canBo.Ten,
			 canBo.HSChucVu,
			 canBo.MaCapBac,
             canBo.MaCanBo,
             canBo.TenChucVu,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaHieuCanBo = canBo.MaHieuCanBo AND bangLuong.MaDonVi = canBo.MaDonVi) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols2 + ')) pvt
   GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten,
		    HSChucVu),
     Ca2 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
		  HSChucVu,
          SUM(LHT_TT) LHT_TT,
          SUM(PCCT_TT) PCCT_TT,
          SUM(BHCN_TT) BHCN_TT,
		  SUM(PHAITRUKHAC_SUM) PHAITRUKHAC_SUM
   FROM
     (SELECT canBo.MaDonVi,
             canBo.MaHieuCanBo,
             canBo.TenCanBo,
			 canBo.Ten,
			 canBo.HSChucVu,
			 canBo.MaCapBac,
             canBo.MaCanBo,
             canBo.TenChucVu,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThangCaHai bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaHieuCanBo = canBo.MaHieuCanBo AND bangLuong.MaDonVi = canBo.MaDonVi) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols1 + ')) pvt
		GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten,
		    HSChucVu)

			SELECT Cach0.MaDonVi,
       Cach0.MaHieuCanBo,
       Cach0.TenCanBo,
	   Cach0.Ten,
	   Cach0.HSChucVu,
       Ca2.LHT_TT LHT_TT,
       Ca2.PCCT_TT PCCT_TT,
       Ca2.BHCN_TT BHCN_TT,
       Cach0.GTNN GTNN,
       Cach0.THUETNCN_TT THUETNCN_TT,
       Ca2.PHAITRUKHAC_SUM PHAITRUKHAC_SUM,
       Cach0.THUEDANOP_TT THUEDANOP_TT,
       Cach0.THUONG_TT THUONG_TT,
       Cach0.THUNHAPKHAC_TT THUNHAPKHAC_TT,
	   Cach0.GTPT_TT GTPT_TT,
	   Cach0.GTPT_DG_SN GTPT_DG_SN,
	   (LHT_TT + THUONG_TT + PCCT_TT + THUNHAPKHAC_TT - BHCN_TT - GTNN - GTPT_DG_SN - PHAITRUKHAC_SUM) TINHTHUE
		FROM Cach0
		JOIN Ca2 ON Cach0.MaHieuCanBo = Ca2.MaHieuCanBo AND Cach0.MaDonVi = Ca2.MaDonVi
		LEFT JOIN DanhSachCanBo ds ON Cach0.MaHieuCanBo = ds.Ma_Hieu_CanBo AND Cach0.MaDonVi = ds.Parent'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY Cach0.HSChucVu , ds.Ma_Hieu_CanBo , Cach0.Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY ds.Ma_Hieu_CanBo , Cach0.Ten ';
	execute(@Query)
	--select @Query
END
;
;
;
;
;
;
GO



/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_chitra_nganhang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_bangluong_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_tao_bangluong_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_tao_bangluong_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]    Script Date: 7/8/2024 8:28:55 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]
GO
/****** Object:  StoredProcedure [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[rpt_ds_chi_tra_ca_nhan_ngan_hang]
	-- Add the parameters for the stored procedure here
	@thang int,
	@nam int,
	@maDonVi varchar(500),
	@IsReduceBHXH bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Bang luong BHXH
	SELECT DISTINCT base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT,
		(isnull(LHT_TT.GiaTri_LHT_TT, 0) + isnull(PCCV_TT.GiaTri_PCCVBH_TT, 0) + isnull(PCTN_TT.GiaTri_PCTNBH_TT, 0) + isnull(BHXHCN_TT.GiaTri_BHXHCN_TT, 0) + isnull(BHYTCN_TT.GiaTri_BHYTCN_TT, 0) + isnull(PCTNVK_TT.GiaTri_PCTNVKBH_TT, 0) + isnull(HSBL_TT.GiaTri_HSBLBH_TT, 0)) PHAITRU_SUM
	INTO #BangLuongBHXH
	FROM TL_BangLuong_ThangBHXH base
	LEFT JOIN (
	SELECT LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	FROM TL_BangLuong_ThangBHXH LHT_TT
	WHERE LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	GROUP BY LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	LEFT JOIN (
	SELECT sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	FROM TL_BangLuong_ThangBHXH
	WHERE iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	GROUP BY sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	WHERE base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))

    -- Insert statements for procedure here
	SELECT
		canBo.Parent MaDonVi,
		canBo.Ma_CanBo MaCanBo,
		canBo.Ten_CanBo TenCanBo,
		ISNULL(chucVu.HeSo_Cv, 0) HSChucVu,
		capBac.Ma_Cb MaCapBac,
		canBo.So_TaiKhoan SoTaiKhoan,
		canBo.Ten_KhoBac NganHang,
		CASE WHEN @IsReduceBHXH = 1 THEN CEILING(SUM(ISNULL(bangLuong.Gia_Tri, 0)) - SUM(ISNULL(bhxh.PHAITRU_SUM, 0)))
		ELSE CEILING(SUM(ISNULL(bangLuong.Gia_Tri, 0)))
		END AS THANHTIEN
	FROM TL_BangLuong_Thang bangLuong
	INNER JOIN TL_DS_CapNhap_BangLuong dsCapNhatBangLuong
		ON bangLuong.parent = dsCapNhatBangLuong.Id
	INNER JOIN TL_DM_CanBo canBo
		ON canBo.Ma_CanBo = bangLuong.Ma_CBo
	LEFT JOIN TL_DM_ChucVu chucVu
		ON canBo.Ma_CV = chucVu.Ma_Cv
	LEFT JOIN TL_DM_CapBac capBac
		ON canBo.Ma_CB = capBac.Ma_Cb
	LEFT JOIN #BangLuongBHXH bhxh
		ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi
	WHERE
		bangLuong.Ma_PhuCap = 'THANHTIEN'
		AND canBo.TM = 1
		AND ISNULL(bangLuong.Gia_Tri, 0) > 0
		AND dsCapNhatBangLuong.Thang = @thang
		AND dsCapNhatBangLuong.Nam = @nam
		AND canBo.Parent in (SELECT * FROM dbo.splitstring(@maDonVi))
	GROUP BY canBo.Parent, canBo.Ma_CanBo, canBo.Ten_CanBo, ISNULL(chucVu.HeSo_Cv, 0), capBac.Ma_Cb, canBo.So_TaiKhoan, canBo.Ten_KhoBac
	ORDER BY MaDonVi DESC, HSChucVu DESC, MaCapBac DESC, TenCanBo DESC
END

/****** Object:  StoredProcedure [dbo].[sp_khluachonnhathau_get_nguonvon_by_lcnt_update]    Script Date: 15/12/2021 6:36:38 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_tao_bangluong_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_get_donvi_tao_bangluong_bhxh] 
	@nam int, 
	@thang int, 
	@cachTinhLuong varchar(20)
As
Begin
	WITH 
	DonViBangLuong 
	as (Select distinct(donVi.Id), donVi.Ma_DonVi, donVi.Parent_id, donVi.Ten_DonVi, donVi.XauNoiMa, donVi.iTrangThai From TL_DM_DonVi as donVi
	Left Join TL_DS_CapNhap_BangLuong on donVi.Ma_DonVi = TL_DS_CapNhap_BangLuong.Ma_CBo 
	And TL_DS_CapNhap_BangLuong.Thang = @thang
	And TL_DS_CapNhap_BangLuong.Nam = @nam
	And TL_DS_CapNhap_BangLuong.Ma_CachTL = @cachTinhLuong
	Where TL_DS_CapNhap_BangLuong.Id is Null
	AND donVi.iTrangThai = 1),
	DonViCanBo 
	as (Select distinct(donVi.Id), donVi.Ma_DonVi, donVi.Parent_id, donVi.Ten_DonVi, donVi.XauNoiMa From TL_DM_DonVi as donVi
	Left Join TL_DM_CanBo on donVi.Ma_DonVi = TL_DM_CanBo.Parent And TL_DM_CanBo.IsDelete = 1
	And TL_DM_CanBo.Thang = @thang
	And TL_DM_CanBo.Nam = @nam
	Join TL_CanBo_CheDoBHXH chedo on TL_DM_CanBo.Ma_CanBo = chedo.sMaCanBo
	Where Ma_CanBo is not null)

	Select distinct(DonViBangLuong.Id), DonViBangLuong.Ma_DonVi, DonViBangLuong.Parent_id, DonViBangLuong.Ten_DonVi, DonViBangLuong.XauNoiMa,
		DonViBangLuong.iTrangThai From
	DonViBangLuong
	Join DonViCanBo on DonViBangLuong.Id = DonViCanBo.Id
End
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@IsNew AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)
DECLARE @ThangTruoc AS int
DECLARE @NamTruoc AS int

IF @Thang = 1 
BEGIN
	SET @ThangTruoc = 12;
	SET @NamTruoc = @Nam - 1;
END
ELSE 
BEGIN
	SET @ThangTruoc =  @Thang - 1;
	SET @NamTruoc = @Nam;
END

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = ' + CAST(@ThangTruoc AS VARCHAR(2)) + '
				And canbo.Nam = ' + CAST(@NamTruoc AS VARCHAR(4)) + '
				AND canbo.Parent IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		),
blt AS (
	SELECT * FROM TL_BangLuong_Thang
	WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	AND ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_DonVi)))
),
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
canBo.Nam_TN AS NTN,
dbo.f_luong_ntn(canBo.Ngay_NN, canBo.Ngay_XN, canBo.Ngay_TN, canBo.Thang_TNN, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') AS NamTn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
--canBo.NTN,
canBo.XauNoiMa
into tbl_luong_bhxh
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

UPDATE tbl_luong_bhxh SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh]') AND type in (N'U')) drop table tbl_luong_bhxh;

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_tru_bhxh_2]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'PCNU_TT,PCCOV_HS,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
SET @Query =
'
WITH 
BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND LHT_TT.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND LHT_TT.sMaCheDo in (''OK_D14N_LBH_TT'', ''OK_T14N_LBH_TT'', ''BDN_D14N_LBH_TT'', ''BDN_T14N_LBH_TT'', ''CONOM_LBH_TT'', ''KT_LBH_TT'', ''NAMNGHIVIEC_LBH_TT'', ''KHHGD_LBH_TT'')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCCVBH_TT'', ''OK_T14N_PCCVBH_TT'', ''BDN_D14N_PCCVBH_TT'', ''BDN_T14N_PCCVBH_TT'', ''CONOM_PCCVBH_TT'', ''KT_PCCVBH_TT'', ''NAMNGHIVIEC_PCCVBH_TT'', ''KHHGD_PCCVBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNBH_TT'', ''OK_T14N_PCTNBH_TT'', ''BDN_D14N_PCTNBH_TT'', ''BDN_T14N_PCTNBH_TT'', ''CONOM_PCTNBH_TT'', ''KT_PCTNBH_TT'', ''NAMNGHIVIEC_PCTNBH_TT'', ''KHHGD_PCTNBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_BHXHCN_TT'', ''OK_T14N_BHXHCN_TT'', ''BDN_D14N_BHXHCN_TT'', ''BDN_T14N_BHXHCN_TT'', ''CONOM_LBH_BHXHCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''BDN_D14N_BHYTCN_TT'', ''OK_D14N_BHYTCN_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_PCTNVKBH_TT'', ''OK_T14N_PCTNVKBH_TT'', ''BDN_D14N_PCTNVKBH_TT'', ''BDN_T14N_PCTNVKBH_TT'', ''CONOM_PCTNVKBH_TT'', ''KT_PCTNVKBH_TT'', ''NAMNGHIVIEC_PCTNVKBH_TT'', ''KHHGD_PCTNVKBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
		AND sMaCheDo in (''OK_D14N_HSBLBH_TT'', ''OK_T14N_HSBLBH_TT'', ''BDN_D14N_HSBLBH_TT'', ''BDN_T14N_HSBLBH_TT'', ''CONOM_HSBLBH_TT'', ''KT_HSBLBH_TT'', ''NAMNGHIVIEC_HSBLBH_TT'', ''KHHGD_HSBLBH_TT'')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = ' + CAST(@Thang AS VARCHAR(2)) + '
		AND base.iNam = ' + CAST(@Nam AS VARCHAR(4)) + '
		AND base.sMaDonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
CASE
	WHEN bangLuong.Ma_PhuCap = ''LHT_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCCV_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHXHCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''BHYTCN_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''PCTNVK_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
	WHEN bangLuong.Ma_PhuCap = ''HSBL_TT'' THEN isnull(bangLuong.Gia_Tri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
	ELSE bangLuong.Gia_Tri
END AS GiaTri
FROM TL_BangLuong_Thang bangLuong
JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
LEFT JOIN BangLuongBHXH bhxh ON bangLuong.Ma_CBo = bhxh.sMaCbo AND bangLuong.Ma_DonVi = bhxh.sMaDonVi AND bangLuong.NAM = bhxh.iNam AND bangLuong.THANG = bhxh.iThang
WHERE
bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
AND dsCapNhapBangLuong.Status=1
) x
PIVOT
(
SUM(GiaTri)
FOR MaPhuCap IN (' + @Cols + ')
) pvt
), ThongTinCanBo AS (
SELECT
donVi.Ma_DonVi AS MaDonVi,
donVi.Ten_Donvi AS TenDonvi,
canBo.Ma_CanBo AS MaCanBo,
canBo.Ten_CanBo AS TenCanBo,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma_Cv AS MaChucVu,
chucVu.Ten_Cv AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac capBac
ON canBo.Ma_CB=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu chucVu
ON canBo.Ma_CV=chucVu.Ma_Cv
WHERE
(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = '+ CAST(@Thang AS VARCHAR(2)) +' and year(canbo.Ngay_XN) = '+ CAST(@Nam AS VARCHAR(4)) +'))
AND canBo.Khong_Luong = 0
AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.Tnn,
canBo.XauNoiMa
into tbl_luong_bhxh_2
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)

UPDATE tbl_luong_bhxh_2 SET 
PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) * isnull(PCCOV_HS, 0)

UPDATE tbl_luong_bhxh_2 SET
LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0) + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

UPDATE tbl_luong_bhxh_2 SET
PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(PHAITRUKHAC_SUM, 0)

UPDATE tbl_luong_bhxh_2 SET
BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

UPDATE tbl_luong_bhxh_2 SET
THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

select * from tbl_luong_bhxh_2;

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_luong_bhxh_2]') AND type in (N'U')) drop table tbl_luong_bhxh_2;

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 23/04/2022
-- Description:	Lấy dữ liệu cho báo cáo chi trả cá nhân
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang] 
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@IsReduceBHXH bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT,
		(isnull(LHT_TT.GiaTri_LHT_TT, 0) + isnull(PCCV_TT.GiaTri_PCCVBH_TT, 0) + isnull(PCTN_TT.GiaTri_PCTNBH_TT, 0) + isnull(BHXHCN_TT.GiaTri_BHXHCN_TT, 0) + isnull(BHYTCN_TT.GiaTri_BHYTCN_TT, 0) + isnull(PCTNVK_TT.GiaTri_PCTNVKBH_TT, 0) + isnull(HSBL_TT.GiaTri_HSBLBH_TT, 0)) PHAITRU_SUM
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
	),
	BangLuongThang AS (
		SELECT
			bangLuongThang.Ma_CBo AS MaCanBo,
			CASE WHEN @IsReduceBHXH = 1 THEN SUM(ISNULL(bangLuongThang.Gia_Tri, 0)) - SUM(ISNULL(bhxh.PHAITRU_SUM, 0)) 
			ELSE SUM(ISNULL(bangLuongThang.Gia_Tri, 0))
			END AS GiaTri
		FROM TL_BangLuong_Thang bangLuongThang
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuongThang.parent = dsCapNhapBangLuong.Id
		LEFT JOIN BangLuongBHXH bhxh ON bangLuongThang.Ma_CBo = bhxh.sMaCbo AND bangLuongThang.Ma_DonVi = bhxh.sMaDonVi
		WHERE
			bangLuongThang.Ma_PhuCap = 'THANHTIEN'
			AND dsCapNhapBangLuong.Ma_CachTL in ('CACH0', 'CACH5')
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
		GROUP BY bangLuongThang.Ma_CBo
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.So_TaiKhoan	AS SoTaiKhoan,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			ISNULL(canBo.Ma_CB, '0') AS MaCapBac
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_ChucVu chucVu
			ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=@Thang
			AND canBo.Nam=@Nam
			AND canBo.TM = 1
	)

	SELECT
		canBo.MaDonvi,
		canBo.MaCanBo,
		canBo.TenCanBo,
		canBo.SoTaiKhoan,
		bangLuongThang.GiaTri
	FROM BangLuongThang bangLuongThang
	INNER JOIN ThongTinCanBo canBo
		ON bangLuongThang.MaCanBo = canBo.MaCanBo
	ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo ASC
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_don_vi_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@maCachTl nvarchar(5) 
AS 
	DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),

     CanBoLuongNgach AS
  (SELECT MaCanBo,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
              CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi
	  ) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt)

SELECT DoiTuong,
       MaDonVi,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
	   INTO #tbl_luong_bhxh
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi
ORDER BY MaDonVi

	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;

;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@maCachTl nvarchar(5) 
AS 
DECLARE 
@Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id --AND bangLuong.Ma_CB like @ngach + '%'

   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canbo.Ma_CanBo MaCanBo,
          donvi.Ma_DonVi MaDonVi,
          donvi.Ten_DonVi DoiTuong,
          case when capbac.Parent = '3.3' then '3' else capbac.Parent end as Ngach
   FROM TL_DM_CanBo canBo
   LEFT JOIN TL_DM_DonVi donvi ON canBo.Parent = donvi.Ma_DonVi
   LEFT JOIN TL_DM_CapBac capbac ON canBo.Ma_CB = capbac.Ma_Cb
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam),
     CanBoLuongNgach AS
  (SELECT MaCanBo,
          Ngach,
          DoiTuong,
          MaDonVi,
          LHT_TT,
          HSBL_TT,
          PCTNVK_TT,
          PCCV_TT,
          PCTN_TT,
          PCKV_TT,
          PCCOV_TT,
          PCDACTHU_SUM,
          PCTRA_SUM,
          PCKHAC_SUM,
          LUONGTHANG_SUM,
          BHXHCN_TT,
          BHYTCN_TT,
          BHTNCN_TT,
          TA_TONG,
          THUETNCN_TT,
          TRICHLUONG_TT,
          GTKHAC_TT,
          PHAITRU_SUM,
          THANHTIEN,
		  TM
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
             canBo.Ngach,
             CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi
	  ) x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt)

SELECT DoiTuong,
       MaDonVi,
       Ngach,
       COUNT(*) SoNguoi,
       SUM(LHT_TT)/@donViTinh LHT_TT,
       SUM(HSBL_TT)/@donViTinh HSBL_TT,
       SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
       SUM(PCCV_TT)/@donViTinh PCCV_TT,
       SUM(PCTN_TT)/@donViTinh PCTN_TT,
       SUM(PCKV_TT)/@donViTinh PCKV_TT,
       SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
       SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
       SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
       SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
       SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
       SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
       SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
       SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
       SUM(TA_TONG)/@donViTinh TA_TONG,
       SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
       SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
       SUM(GTKHAC_TT/@donViTinh) GTKHAC_TT,
       SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
       SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
       SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN,
       @IsParent IsParent
	   INTO #tbl_luong_bhxh
	FROM CanBoLuongNgach
	GROUP BY DoiTuong,
			MaDonVi,
			Ngach


	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh]    Script Date: 7/8/2024 8:28:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_tru_bhxh] 
	@thang int, 
	@nam int, 
	@maDonVi nvarchar(MAX),
	@donViTinh int, 
	@isSummary bit, 
	@maCachTl nvarchar(5)
AS 

DECLARE @Cols AS NVARCHAR(MAX)
SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN,TM';

WITH BangLuongBHXH AS (
	select distinct base.sMaCbo, base.sTenCbo, base.sMaDonVi, base.iNam, base.iThang,
		LHT_TT.GiaTri_LHT_TT,
		PCCV_TT.GiaTri_PCCVBH_TT,
		PCTN_TT.GiaTri_PCTNBH_TT,
		BHXHCN_TT.GiaTri_BHXHCN_TT,
		BHYTCN_TT.GiaTri_BHYTCN_TT,
		PCTNVK_TT.GiaTri_PCTNVKBH_TT,
		HSBL_TT.GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH base
	left join (
	select LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang, sum(nGiaTri) GiaTri_LHT_TT
	from TL_BangLuong_ThangBHXH LHT_TT
	where LHT_TT.iThang = @Thang
		AND LHT_TT.iNam = @Nam
		AND LHT_TT.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND LHT_TT.sMaCheDo in ('OK_D14N_LBH_TT', 'OK_T14N_LBH_TT', 'BDN_D14N_LBH_TT', 'BDN_T14N_LBH_TT', 'CONOM_LBH_TT', 'KT_LBH_TT', 'NAMNGHIVIEC_LBH_TT', 'KHHGD_LBH_TT')
	group by LHT_TT.sMaCbo, LHT_TT.sTenCbo, LHT_TT.sMaDonVi, LHT_TT.iNam, LHT_TT.iThang) LHT_TT on base.sMaCbo = LHT_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCCVBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCCVBH_TT', 'OK_T14N_PCCVBH_TT', 'BDN_D14N_PCCVBH_TT', 'BDN_T14N_PCCVBH_TT', 'CONOM_PCCVBH_TT', 'KT_PCCVBH_TT', 'NAMNGHIVIEC_PCCVBH_TT', 'KHHGD_PCCVBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCCV_TT on base.sMaCbo = PCCV_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNBH_TT', 'OK_T14N_PCTNBH_TT', 'BDN_D14N_PCTNBH_TT', 'BDN_T14N_PCTNBH_TT', 'CONOM_PCTNBH_TT', 'KT_PCTNBH_TT', 'NAMNGHIVIEC_PCTNBH_TT', 'KHHGD_PCTNBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTN_TT on base.sMaCbo = PCTN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHXHCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_BHXHCN_TT', 'OK_T14N_BHXHCN_TT', 'BDN_D14N_BHXHCN_TT', 'BDN_T14N_BHXHCN_TT', 'CONOM_BHXHCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHXHCN_TT on base.sMaCbo = BHXHCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_BHYTCN_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('BDN_D14N_BHYTCN_TT', 'OK_D14N_BHYTCN_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) BHYTCN_TT on base.sMaCbo = BHYTCN_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_PCTNVKBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_PCTNVKBH_TT', 'OK_T14N_PCTNVKBH_TT', 'BDN_D14N_PCTNVKBH_TT', 'BDN_T14N_PCTNVKBH_TT', 'CONOM_PCTNVKBH_TT', 'KT_PCTNVKBH_TT', 'NAMNGHIVIEC_PCTNVKBH_TT', 'KHHGD_PCTNVKBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) PCTNVK_TT on base.sMaCbo = PCTNVK_TT.sMaCbo
	left join (
	select sMaCbo, sTenCbo, sMaDonVi, iNam, iThang, sum(nGiaTri) GiaTri_HSBLBH_TT
	from TL_BangLuong_ThangBHXH
	where iThang = @Thang
		AND iNam = @Nam
		AND sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
		AND sMaCheDo in ('OK_D14N_HSBLBH_TT', 'OK_T14N_HSBLBH_TT', 'BDN_D14N_HSBLBH_TT', 'BDN_T14N_HSBLBH_TT', 'CONOM_HSBLBH_TT', 'KT_HSBLBH_TT', 'NAMNGHIVIEC_HSBLBH_TT', 'KHHGD_HSBLBH_TT')
	group by sMaCbo, sTenCbo, sMaDonVi, iNam, iThang) HSBL_TT on base.sMaCbo = HSBL_TT.sMaCbo

	where base.iThang = @Thang
		AND base.iNam = @Nam
		AND base.sMaDonVi IN (SELECT * FROM f_split(@MaDonVi))
),
BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
		  dsCapNhapBangLuong.Ma_CBo MaDonVi,
          bangLuong.GIA_TRI AS GiaTri
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN
       (SELECT *
        FROM f_split(@Cols))
     AND dsCapNhapBangLuong.Ma_CachTL=@maCachTl
     AND dsCapNhapBangLuong.Ma_CBo IN
       (SELECT *
        FROM f_split(@maDonVi))
     AND dsCapNhapBangLuong.Thang=@thang
     AND dsCapNhapBangLuong.Nam=@nam
     AND dsCapNhapBangLuong.Status=1 ),

     ThongTinCanBo AS
  (SELECT canBo.Ma_CanBo AS MaCanBo,
          canBo.Ten_CanBo AS TenCanBo,
          ISNULL(canBo.Ma_CB, '0') AS MaCapBac,
          ISNULL(capBac.TenCapBac, '0') AS TenNgach,
          ISNULL(capBac.MaCapBacCha, '0') AS MaNgach
   FROM TL_DM_CanBo canBo
   INNER JOIN TL_DM_DonVi donVi ON canBo.Parent=donVi.Ma_DonVi AND canBo.Parent IN (SELECT * FROM f_split(@maDonVi))
   LEFT JOIN
     (SELECT capbaccon.Ma_Cb MaCapBac,
             capbaccha.Ma_Cb MaCapBacCha,
             capbaccha.Note TenCapBac
      FROM TL_DM_CapBac capbaccon
      LEFT JOIN TL_DM_CapBac capbaccha ON capbaccon.Parent = capbaccha.Ma_Cb) capBac ON canBo.Ma_CB=capBac.MaCapBac
   LEFT JOIN TL_DM_ChucVu chucVu ON canBo.Ma_CV=chucVu.Ma_Cv
   WHERE canBo.IsDelete = 1
     AND canBo.Khong_Luong = 0
	 AND canBo.Thang = @thang
	 AND canBo.Nam = @nam)

	 SELECT TenNgach,
          MaNgach,
		  MaDonVi,
          COUNT(MaCanBo) AS SoNguoi,
          SUM(LHT_TT)/@donViTinh LHT_TT,
		   SUM(HSBL_TT)/@donViTinh HSBL_TT,
		   SUM(PCTNVK_TT)/@donViTinh PCTNVK_TT,
		   SUM(PCCV_TT)/@donViTinh PCCV_TT,
		   SUM(PCTN_TT)/@donViTinh PCTN_TT,
		   SUM(PCKV_TT)/@donViTinh PCKV_TT,
		   SUM(PCCOV_TT)/@donViTinh PCCOV_TT,
		   SUM(PCDACTHU_SUM)/@donViTinh PCDACTHU_SUM,
		   SUM(PCTRA_SUM)/@donViTinh PCTRA_SUM,
		   SUM(PCKHAC_SUM)/@donViTinh PCKHAC_SUM,
		   SUM(LUONGTHANG_SUM)/@donViTinh LUONGTHANG_SUM,
		   SUM(BHXHCN_TT)/@donViTinh BHXHCN_TT,
		   SUM(BHYTCN_TT)/@donViTinh BHYTCN_TT,
		   SUM(BHTNCN_TT)/@donViTinh BHTNCN_TT,
		   SUM(TA_TONG)/@donViTinh TA_TONG,
		   SUM(THUETNCN_TT)/@donViTinh THUETNCN_TT,
		   SUM(TRICHLUONG_TT)/@donViTinh TRICHLUONG_TT,
		   SUM(GTKHAC_TT)/@donViTinh GTKHAC_TT,
		   SUM(PHAITRU_SUM)/@donViTinh PHAITRU_SUM,
		   SUM(CASE WHEN TM = 0 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN_NH,
		   SUM(CASE WHEN TM = 1 THEN THANHTIEN ELSE 0 END)/@donViTinh THANHTIEN
	INTO #tbl_luong_bhxh
	FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaCapBac,
             canBo.TenNgach,
             canBo.MaNgach,
             CASE
				WHEN bangLuong.MaPhuCap = 'LHT_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_LHT_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCCV_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCCVBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHXHCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHXHCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'BHYTCN_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_BHYTCN_TT, 0)
				WHEN bangLuong.MaPhuCap = 'PCTNVK_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_PCTNVKBH_TT, 0)
				WHEN bangLuong.MaPhuCap = 'HSBL_TT' THEN isnull(bangLuong.GiaTri, 0) - isnull(bhxh.GiaTri_HSBLBH_TT, 0)
				ELSE bangLuong.GiaTri
			END AS GiaTri,
             bangLuong.MaPhuCap,
			 CASE When @isSummary = 1 Then '1' Else bangLuong.MaDonVi end as MaDonVi
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo
	  LEFT JOIN BangLuongBHXH bhxh ON bangLuong.MaCanBo = bhxh.sMaCbo AND bangLuong.MaDonVi = bhxh.sMaDonVi) 
	  x PIVOT (SUM(GiaTri)
      FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN, TM)) pvt
	GROUP BY TenNgach,
         MaNgach, MaDonVi
	Order By MaDonVi, MaNgach

	UPDATE #tbl_luong_bhxh SET 
	PCCOV_TT = (isnull(LHT_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(HSBL_TT, 0)) --* isnull(PCCOV_HS, 0)

	UPDATE #tbl_luong_bhxh SET
	LUONGTHANG_SUM = isnull(LHT_TT, 0) + isnull(PCTN_TT, 0) + isnull(PCTNVK_TT, 0) + isnull(PCCV_TT, 0) + isnull(PCCOV_TT, 0) + isnull(PCTRA_SUM, 0) + isnull(PCDACTHU_SUM, 0) + isnull(PCKHAC_SUM, 0) + isnull(HSBL_TT, 0) + isnull(PCKV_TT, 0)-- + isnull(PCBVBG_TT, 0) + isnull(PCNU_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	PHAITRU_SUM = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0) + isnull(THUETNCN_TT, 0) + isnull(TA_TONG, 0) + isnull(TRICHLUONG_TT, 0) + isnull(GTKHAC_TT, 0)

	--UPDATE #tbl_luong_bhxh SET
	--BHCN_TT = isnull(BHXHCN_TT, 0) + isnull(BHYTCN_TT, 0) + isnull(BHTNCN_TT, 0)

	UPDATE #tbl_luong_bhxh SET
	THANHTIEN = isnull(LUONGTHANG_SUM, 0) - isnull(PHAITRU_SUM, 0)

	select * from #tbl_luong_bhxh;


;
;
;
GO


delete from TL_DM_Cach_TinhLuong_Chuan where Ma_Cot = 'TRICHQUYPCTT_TT'
GO
update TL_DM_PhuCap set Is_Formula = 0, Chon=1, bGiaTri=1 where Ma_PhuCap = 'TRICHQUYPCTT_TT'
GO
update TL_DM_PhuCap set Chon=0 where Ma_PhuCap = 'TRICHLUONG22_SN'
GO
update TL_DM_Cach_TinhLuong_Chuan set CongThuc='LHT_TT+PCCT_TT-GTPT_TT-BHCN_TT-PHAITRUKHAC_SUM' where Ma_Cot = 'LUONGTHUE_TT'
GO