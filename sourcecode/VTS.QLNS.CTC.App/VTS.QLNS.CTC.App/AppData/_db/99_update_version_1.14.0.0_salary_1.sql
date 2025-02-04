/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 2/20/2024 5:37:53 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 2/20/2024 5:37:53 PM ******/
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
	SET @Cols1 = 'BHCN_TT,LHT_TT,PCCT_TT'
	SET @Cols2 = 'THUONG_TT,THUNHAPKHAC_TT,GTNN,GIAMTHUE_TT,THUETNCN_TT,THUEDANOP_TT,GTPT_TT';
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
			donVi.Ma_DonVi										AS MaDonVi,
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
          SUM(GIAMTHUE_TT) GIAMTHUE_TT,
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
          SUM(BHCN_TT) BHCN_TT
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
       Cach0.GIAMTHUE_TT GIAMTHUE_TT,
       Cach0.THUEDANOP_TT THUEDANOP_TT,
       Cach0.THUONG_TT THUONG_TT,
       Cach0.THUNHAPKHAC_TT THUNHAPKHAC_TT,
	   Cach0.GTPT_TT GTPT_TT,
	   Cach0.GTPT_DG_SN GTPT_DG_SN,
	   (LHT_TT + THUONG_TT + PCCT_TT + THUNHAPKHAC_TT - BHCN_TT - GTNN - GTPT_DG_SN - GIAMTHUE_TT) TINHTHUE
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
GO
