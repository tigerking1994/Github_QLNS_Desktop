GO
delete from TL_DM_Cach_TinhLuong_Chuan where Ma_Cot = 'TRICHQUY_SUM'
INSERT [dbo].[TL_DM_Cach_TinhLuong_Chuan] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (newid(), N'TRICHQUYKHAC_TT+TRICHQUYPCTT_TT', N'CACH0', N'TRICHQUY_SUM', NULL, NULL, 2024, NULL, NULL, N'Tổng trích lương đóng quỹ', 10)
GO
delete from TL_DM_PhuCap where Ma_PhuCap = 'TRICHQUY_SUM'
INSERT [dbo].[TL_DM_PhuCap] ([Id], [bGiaTri], [bHuongPc_Sn], [bSaoChep], [Chon], [Cong_Thuc], [Dinh_Dang], [Gia_Tri], [He_So], [HuongPC_SN], [iDinhDang], [iLoai], [Is_Formula], [Is_Readonly], [IThang_ToiDa], [Ma_KMCP], [Ma_PhuCap], [Ma_TTM_Ng], [Numeric_Scale], [Parent], [PhanTram_CT], [Readonly], [Splits], [Ten_Ngan], [Ten_PhuCap], [Tinh_BHXH], [Tinh_TNCN], [Xau_Noi_Ma], [XSort], [fGiaTriLonNhat], [fGiaTriNhoNhat], [fGiaTriPhuCap_KemTheo], [iId_Ma_PhuCap_KemTheo], [iId_PhuCap_KemTheo], [Ten_NganHang]) VALUES (newid(), 1, 1, 1, 1, NULL, 0, CAST(0.000 AS Numeric(17, 3)), NULL, NULL, 0, NULL, 1, 0, NULL, NULL, N'TRICHQUY_SUM', NULL, NULL, N'SUM', NULL, NULL, NULL, NULL, N'Tổng trích lương đóng quỹ', 1, NULL, N'SUM-TRICHQUY_SUM', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 10/2/2024 9:02:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 10/2/2024 9:02:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 10/2/2024 9:02:46 AM ******/
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

	SET @Cols = 'GTNN,GTPT_SN,GTPT_DG,GTKHAC_TT,LUONGTHUE_TT,THUETNCN_TT,GIAMTHUE_TT,THUEDANOP_TT,LHT_TT,PCCT_TT,THUONG_TT,THUNHAPKHAC_TT,BHCN_TT,GTPT_TT,TRICHQUY_SUM'
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
	(ISNULL(LHT_TT, 0) + ISNULL(PCCT_TT, 0) + ISNULL(THUONG_TT, 0) + ISNULL(THUNHAPKHAC_TT, 0) - ISNULL(GTPT_TT, 0) - ISNULL(BHCN_TT, 0) - ISNULL(GIAMTHUE_TT, 0) - ISNULL(TRICHQUY_SUM,0)) TINHTHUE,
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
;

-- =============================================
-- Author:		TungNH
-- Create date: 20/12/2021
-- Description:	Lấy dữ liệu nộp thuế thu nhập cá nhân theo năm
-- =============================================
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 10/2/2024 9:02:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
	@Nam AS int,
	@MaDonVi NVARCHAR(MAX),
	@IsOrderChucVu bit = 0
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Query AS NVARCHAR(MAX)

	DECLARE @Cols1 AS NVARCHAR(MAX) DECLARE @Cols2 AS NVARCHAR(MAX)
	SET @Cols1 = 'BHCN_TT,LHT_TT,PCCT_TT,TRICHQUY_SUM'
	SET @Cols2 = 'THUONG_TT,THUNHAPKHAC_TT,GTNN,THUETNCN_TT,THUEDANOP_TT,GTPT_TT,GIAMTHUE_TT';
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
		  SUM(GIAMTHUE_TT) GIAMTHUE_TT,
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
		  SUM(TRICHQUY_SUM) TRICHQUY_SUM
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
       Ca2.TRICHQUY_SUM TRICHQUY_SUM,
       Cach0.THUEDANOP_TT THUEDANOP_TT,
       Cach0.THUONG_TT THUONG_TT,
	   Cach0.GIAMTHUE_TT GIAMTHUE_TT,
       Cach0.THUNHAPKHAC_TT THUNHAPKHAC_TT,
	   Cach0.GTPT_TT GTPT_TT,
	   Cach0.GTPT_DG_SN GTPT_DG_SN,
	   (isnull(LHT_TT, 0) + isnull(THUONG_TT, 0) + isnull(PCCT_TT, 0) + isnull(THUNHAPKHAC_TT, 0) - isnull(BHCN_TT, 0) - isnull(GTNN, 0) - isnull(GTPT_DG_SN, 0) - isnull(GIAMTHUE_TT, 0) - isnull(TRICHQUY_SUM, 0)) TINHTHUE
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
;
GO


