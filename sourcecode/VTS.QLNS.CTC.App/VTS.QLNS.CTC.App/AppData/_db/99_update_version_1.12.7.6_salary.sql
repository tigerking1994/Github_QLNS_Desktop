
UPDATE TL_DM_CapBac set Lht_Hs = '8.1' WHERE Ma_Cb = '1240'
UPDATE TL_DM_CapBac set Lht_Hs = '7.4' WHERE Ma_Cb = '1230'

/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 5/4/2023 7:18:35 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]    Script Date: 5/5/2023 9:40:17 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 5/5/2023 9:54:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 5/9/2023 2:56:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 5/9/2023 2:56:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert] 
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
		AND canBo.Ma_CanBo IN (SELECT MA_CBO FROM TL_CanBo_PhuCap WHERE MA_PHUCAP LIKE '%TTL%' AND GIA_TRI > 0 GROUP BY MA_CBO)
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
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN #tmpPhuCapLuongTruyLinh phuCapLuongTruyLinh 
		ON phuCapLuongTruyLinh.Ma_PC = canBoPhuCap.MA_PHUCAP
	INNER JOIN #ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN TL_DM_Cach_TinhLuong_TruyLinh cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	DROP TABLE #ThongTinCanBo
	DROP TABLE #tmp
	DROP TABLE #tmpPhuCapLuongTruyLinh
END
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 5/5/2023 9:54:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,LHT_TT,PCCV_TT,PCTHD_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,PCTRA_SUM,LUONGTHANG_SUM,PHAITRU_SUM,TM,THANHTIEN,PCNU_TT,HSBL_HS,PCTNVK_HS'
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
			AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			canBo.Thang_TNN		AS Tnn,
			ISNULL(canBo.Ma_CB, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			chucVu.Ma_Cv AS MaChucVu,
			chucVu.Ten_Cv AS TenChucVu,
			CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) AS Stk,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
			canBo.Ngay_NN AS NgayNhapNguDate,
			canBo.Ngay_XN AS NgayXuatNguDate,
			canBo.Ngay_TN AS NgayTaiNguDate,
			CASE WHEN canBo.Thang_TNN IS NULL THEN 0 ELSE canBo.Thang_TNN END AS ThangTnn,
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
	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, XauNoiMa, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
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
			canBo.NgayNhapNguDate,
			canBo.NgayXuatNguDate,
			canBo.NgayTaiNguDate,
			canBo.ThangTnn,
			canBo.Tnn,
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, '+ CAST(@Thang AS VARCHAR(2)) +', '+ CAST(@Nam AS VARCHAR(4)) +') ELSE bangLuong.GiaTri END AS GiaTri,
			bangLuong.MaPhuCap,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @Cols + ')
	) pvt
	WHERE MaCapBac LIKE ''0%''
	ORDER BY HSChucVu DESC, MaCapBac DESC, Ten ASC'
	execute(@Query)
END
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi]    Script Date: 5/5/2023 9:40:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_tonghop_luong_theo_ngach_don_vi] @thang int, @nam int, @maDonVi nvarchar(MAX),
                                                                                                 @donViTinh int, @maCachTl nvarchar(5) AS DECLARE @Cols AS NVARCHAR(MAX) DECLARE @IsParent AS Bit
SET @IsParent = 0;


SET @Cols = 'LHT_TT,HSBL_TT,PCTNVK_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCCOV_TT,PCDACTHU_SUM,PCTRA_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,TA_TONG,THUETNCN_TT,TRICHLUONG_TT,GTKHAC_TT,PHAITRU_SUM,THANHTIEN';

WITH BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Thang AS Thang,
          dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.Ma_CBo AS MaCanBo,
          bangLuong.MA_PHUCAP AS MaPhuCap,
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
          THANHTIEN
   FROM
     (SELECT bangLuong.Thang AS Thang,
             bangLuong.Nam AS Nam,
             canBo.MaCanBo,
             canBo.MaDonVi,
             canBo.DoiTuong,
             canBo.Ngach,
             bangLuong.GiaTri,
             bangLuong.MaPhuCap
      FROM BangLuongThang bangLuong
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (LHT_TT, HSBL_TT, PCTNVK_TT, PCCV_TT, PCTN_TT, PCKV_TT, PCCOV_TT, PCDACTHU_SUM, PCTRA_SUM, PCKHAC_SUM, LUONGTHANG_SUM, BHXHCN_TT, BHYTCN_TT, BHTNCN_TT, TA_TONG, THUETNCN_TT, TRICHLUONG_TT, GTKHAC_TT, PHAITRU_SUM, THANHTIEN)) pvt)

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
       SUM(THANHTIEN)/@donViTinh THANHTIEN,
       @IsParent IsParent
FROM CanBoLuongNgach
GROUP BY DoiTuong,
         MaDonVi,
         Ngach
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 5/4/2023 7:18:35 PM ******/
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

	SELECT
		dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
		dsCapNhapBangLuong.Thang	AS Thang,
		dsCapNhapBangLuong.Nam		AS Nam,
		bangLuong.MA_PHUCAP			AS MaPhuCap,
		bangLuong.Ma_CB				AS MaCapBac,
		case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
		) AS GiaTri,
		COUNT(bangLuong.Ma_CBo)		AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	INNER JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	LEFT JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE Status = 1 AND NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From TL_QT_ChungTuChiTiet ctchitiet
	Join TL_QT_ChungTu chungtu
	on chungtu.ID = ctchitiet.Id_ChungTu
	Where Nam = @nam
	And Ma_DonVi IN (SELECT * FROM f_split(@maCachTl))
	Group By XauNoiMa, Ma_DonVi

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
		luong.Ma_Cb						AS MaCb,
		luong.Thang as Thang
	FROM NS_MucLucNganSach mlns
	JOIN #LuongCapBacMlns luong
		ON mlns.sXauNoiMa = luong.XauNoiMa
	LEFT JOIN #DataDuToan dataDuToan
		ON luong.XauNoiMa = dataDuToan.XauNoiMa and luong.MaDonVi = dataDuToan.Ma_DonVi
	WHERE
		sLNS IN ('1', '101', '1010000')
		AND iNamLamViec = @nam
		AND luong.Thang = @thang
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, sXauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
;
;
;
GO
