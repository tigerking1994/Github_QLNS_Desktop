/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_capphat_phucap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_capphat_phucap]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_dong]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bang_luong_thang_dong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bang_luong_thang_dong]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 12/10/2022 6:40:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]    Script Date: 12/10/2022 6:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all] 
@Loai varchar(MAX),
@IdDonVi varchar(MAX),
@NamLamViec int, 
@Dvt int,
@LoaiChungTu int 
AS
BEGIN
SELECT DISTINCT mucluc.iID AS Id,
                mucluc.iID_MLSKT AS IDMucLuc,
                mucluc.sSTT AS STT,
                mucluc.sMoTa AS MoTa,
                mucluc.iID_MLSKTCha AS IdParent,
                mucluc.sSTTBC AS STTBC,
                mucluc.sM AS M,
                CAST(0 AS BIT) AS IsHangCha,
                chitiet.*
FROM NS_SKT_MucLuc mucluc
LEFT JOIN
  (SELECT KyHieu ,
          QuyetToan =sum(QuyetToan)/@Dvt ,
          DuToan =sum(DuToan)/@Dvt ,
          TuChi =sum(fTuChi)/@Dvt ,
          TuChi2 =sum(TuChi2)/@Dvt
   FROM
     (SELECT chitiet.sKyHieu AS KyHieu,
             QuyetToan = 0 ,
             DuToan = 0 ,
             fTuChi = SUM(ISNULL(chitiet.fMuaHangCapHienVat, 0) + ISNULL(chitiet.fPhanCap, 0)),
             TuChi2 = 0
      FROM NS_SKT_ChungTuChiTiet chitiet
      LEFT JOIN NS_SKT_MucLuc mucluc ON chitiet.iID_MLSKT = mucluc.iID_MLSKT AND mucluc.iNamLamViec = @NamLamViec
      WHERE  chitiet.iNamLamViec=@NamLamViec
        AND chitiet.iLoai in (SELECT * FROM f_split(@Loai))
        AND chitiet.iLoaiChungTu = @LoaiChungTu
        AND (@IdDonVi IS NULL
             OR chitiet.iID_MaDonVi in
               (SELECT *
                FROM f_split(@IdDonVi)))
	 GROUP BY chitiet.sKyHieu

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan,
                       DuToan,
                       TuChi = 0 ,
                       TuChi2 = 0
      FROM f_skt_cancu(@NamLamViec, @IdDonVi, cASt(@LoaiChungTu AS nvarchar(MAX))) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(SKT_XauNoiMa,@NamLamViec) IS NOT NULL -- lap chi tiet du toan

      UNION ALL SELECT dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) AS KyHieu ,
                       QuyetToan = 0 ,
                       DuToan = 0 ,
                       TuChi = 0 ,
                       TuChi2 = TuChi
      FROM(
		SELECT iID_MaDonVi AS Id_DonVi,
		   XauNoiMa,
		   TuChi =sum(TuChi)
			FROM
			  (SELECT XauNoiMa,
					  iID_MaDonVi,
					  TuChi
				FROM
					(SELECT XauNoiMa = sLNS+'-'+sL+'-'+sK+'-'+sM+'-'+sTM+'-'+sTTM+'-'+sNG, --MoTa,
					iID_MaDonVi,
					TuChi =sum(ISNULL(fHangMua, 0) + ISNULL(fHangNhap, 0) + ISNULL(fPhanCap, 0))
						FROM NS_DTDauNam_ChungTuChiTiet
						WHERE iNamLamViec=@NamLamViec
						AND iLoaiChungTu = @LoaiChungTu
						AND iLoai=3
						AND (@IdDonVi IS NULL
								OR iID_MaDonVi in
								(SELECT *
								FROM f_split(@IdDonVi)))
						GROUP BY sLNS,
								sL,
								sK,
								sM,
								sTM,
								sTTM,
								sNG,
								iID_MaDonVi) AS dt) AS a
		WHERE XauNoiMa IS NOT NULL
		GROUP BY iID_MaDonVi,
					XauNoiMa
		HAVING sum(TuChi)<>0
		  ) AS a
      WHERE dbo.f_get_ky_hieu_by_xaunoima(XauNoiMa,@NamLamViec) IS NOT NULL ) AS a
   GROUP BY KyHieu --order by Tuchi
 ) chitiet ON mucluc.sKyHieu = chitiet.KyHieu
WHERE chitiet.KyHieu IS NOT NULL
  AND mucluc.iNamLamViec = @NamLamViec
ORDER BY sSTTBC END
;
;
;
-- exec [dbo].[sp_rpt_skt_dutoandaunam_sosanh_skt_all]  '4', '112',2022,1000,'2'
;







GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bang_luong_thang_dong]    Script Date: 12/10/2022 6:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bang_luong_thang_dong] 
	@maDonVi nvarchar(MAX),
	@ngach varchar(10),
	@maPhuCap nvarchar(50),
	@thang int, 
	@nam int 
AS 
BEGIN

DECLARE @query AS NVARCHAR(MAX);

SET @query = 'WITH SoLieuBangLuong AS  (
  SELECT Parent, MaCanBo, MaCapBac, LHT_HS, ' + @maPhuCap + ', TenDonVi FROM (
    SELECT
    canBo.Ma_CB AS MaCapBac,
    dsCapNhapBangLuong.Id AS Parent,
      bangLuong.Thang AS Thang,
      bangLuong.Nam AS Nam, 
      donVi.Ten_Donvi TenDonVi,
      canBo.Ma_CanBo MaCanBo,
      canBo.Ten_CanBo AS TenCanBo,
      bangLuong.GIA_TRI AS GiaTri,
      bangLuong.MA_PHUCAP AS MaPhuCap
    FROM TL_BangLuong_Thang bangLuong
    JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent=dsCapNhapBangLuong.Id
    JOIN TL_DM_CanBo canBo ON bangLuong.Ma_CBo=canBo.Ma_CanBo
    JOIN TL_DM_DonVi donVi ON bangLuong.Ma_DonVi=donVi.Ma_DonVi
    WHERE
      dsCapNhapBangLuong.Ma_CachTL=''' + 'CACH0' + '''
      AND dsCapNhapBangLuong.Status=1
      AND canBo.IsDelete=1
      AND canBo.Khong_Luong=0
      AND bangLuong.Ma_PhuCap IN (''' + 'LHT_HS' + ''', ''' + @maPhuCap + ''')
  ) x
  PIVOT
  (
    SUM(GiaTri)
    FOR MaPhuCap IN (LHT_HS, ' + @maPhuCap + ')
  ) pvt
)

SELECT
  canBo.Ma_CB as MaCb,
  bangLuong.LHT_HS AS HeSoLuong,
  canBo.Parent as MaDonVi,
    COUNT(canBo.Ma_CB) AS QuanSo,
    SUM(bangLuong.' + @maPhuCap + ') AS Tien
FROM SoLieuBangLuong bangLuong
INNER JOIN TL_DS_CapNhap_BangLuong dsCapNhatBangLuong
  ON bangLuong.parent = dsCapNhatBangLuong.Id
INNER JOIN TL_DM_CanBo canBo
  ON canBo.Ma_CanBo = bangLuong.MaCanBo
INNER JOIN TL_DM_CapBac capBac
  ON canBo.Ma_CB = capBac.Ma_Cb
WHERE canBo.Parent IN
    (SELECT *
     FROM f_split(''' + @maDonVi + '''))
  AND capBac.XauNoiMa LIKE ''' + @ngach + '%' + '''
  AND dsCapNhatBangLuong.THANG = ' + CAST(@thang AS VARCHAR(2)) + '
  AND dsCapNhatBangLuong.Nam = ' + CAST(@nam AS VARCHAR(4)) + '
  And bangLuong.' + @maPhuCap + ' <> 0
GROUP BY canBo.Ma_CB, bangLuong.LHT_HS, canBo.Parent
ORDER BY canBo.Parent, canBo.Ma_CB'
execute(@query)
END
;
;

                                                                                                                                                                                                                                                      
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 12/10/2022 6:40:19 PM ******/
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

	SET @Cols = 'GTNN,GTPT_SN,GTPT_DG,GTKHAC_TT,LUONGTHUE_TT,THUETNCN_TT,GIAMTHUE_TT,THUEDANOP_TT,LHT_TT,PCCT_TT,THUONG_TT,THUNHAPKHAC_TT,BHCN_TT,LOIICHKHAC_TT,GTPT_TT'
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
	(LHT_TT + PCCT_TT + THUONG_TT + ISNULL(THUNHAPKHAC_TT, 0) - GTPT_TT - GIAMTHUE_TT) TINHTHUE,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac]    Script Date: 12/10/2022 6:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 25/04/2022
-- Description:	Báo cáo giải thích chi tiết lương theo ngạch, cấp bậc
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangluong_thang_giaithich_chitiet_theo_ngach_capbac] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@MaPhuCapCount NVARCHAR(MAX),
	@DonViTinh int,
	@IsSummary bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)
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
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			donVi.Ma_DonVi		AS MaDonVi,
			canBo.Ma_Hieu_CanBo	AS MaHieuCanBo,
			capBac.Ma_Cb			AS MaCapBac,
			CASE
				WHEN capBac.XauNoiMa LIKE ''1%'' OR capBac.XauNoiMa LIKE ''4%'' THEN capBac.Lht_Hs
				ELSE canBo.HeSoLuong
			END AS HeSoLuong
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND canBo.Parent IN (SELECT * FROM f_split( ''' + @MaDonVi + '''))
	), SoLieuBaoCao AS (
		SELECT
			CASE WHEN ' + CAST(@IsSummary AS VARCHAR(1)) + ' = 1 THEN NULL ELSE canBo.MaDonVi END	AS MaDonVi,
			canBo.MaCapBac																			AS MaCapBac,
			canBo.HeSoLuong																			AS HeSoLuong,
			bangLuongThang.MaPhuCap																	AS MaPhuCap,
			COUNT(canBo.MaCanBo)																	AS SoNguoi,
			SUM(bangLuongThang.GiaTri) / ' + CAST(@DonViTinh AS VARCHAR(100)) + '					AS GiaTri
		FROM ThongTinCanBo canBo
		INNER JOIN BangLuongThang bangLuongThang
			ON bangLuongThang.MaCanBo = canBo.MaCanBo
		GROUP BY MaDonVi, MaCapBac, HeSoLuong, MaPhuCap
	), CanBoLuongCapBac AS (
		SELECT
			bangLuong.Ma_CB			AS MaCapBac,
			bangLuong.GIA_TRI		AS HeSoLuong
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap = ''LHT_HS''
			AND bangLuong.Gia_Tri <> 0
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	)

	SELECT ''' + @MaCachTl + ''' AS CachTl, (SELECT COUNT(*) FROM CanBoLuongCapBac canBo WHERE canBo.MaCapBac = pvt.MaCapBac AND canBo.HeSoLuong = pvt.HeSoLuong) AS SoNguoi, pvt.* FROM (
		SELECT
			MaDonVi,
			MaCapBac,
			HeSoLuong,
			DATA,
			COLUMN_NAME + MaPhuCap AS PIV_COL
		FROM SoLieuBaoCao
		CROSS APPLY (
			VALUES (''COUNT_'', SoNguoi), ('''', GiaTri) 
		) CS(COLUMN_NAME, DATA)
	) a
	PIVOT (
		SUM(DATA)
		FOR PIV_COL IN (' + @MaPhuCap + ', ' + @MaPhuCapCount + ')
	) pvt ORDER BY MaDonVi, MaCapBac, HeSoLuong'

	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]    Script Date: 12/10/2022 6:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@IsOrderChucVu AS bit = 0,
	@IsGiaTriAm AS bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
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
		) x
		PIVOT
		(
			SUM(GiaTri)
			FOR MaPhuCap IN (' + @Cols + ')
		) pvt
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
			capBac.XauNoiMa
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
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_capphat_phucap]    Script Date: 12/10/2022 6:40:19 PM ******/
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
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)
	SELECT MaDonVi, MaCanBo, TenCanBo, Tnn, MaCapBac, CapBac, HSChucVu, MaChucVu, TenChucVu, Stk, NgayNhapNgu, NgayXuatNgu, NgayTaiNgu, ThangTnn, ' + @Cols + ' FROM (
		SELECT
			bangLuong.Thang AS Thang,
			bangLuong.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
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
			CASE WHEN bangLuong.MaPhuCap = ' + '''NTN''' + ' THEN dbo.f_luong_ntn(canBo.NgayNhapNguDate, canBo.NgayXuatNguDate, canBo.NgayTaiNguDate, canBo.ThangTnn, 6, 2022) ELSE bangLuong.GiaTri END AS GiaTri,
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
	ORDER BY HSChucVu DESC, MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac]    Script Date: 12/10/2022 6:40:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_phucap_khac]
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@MaPhuCap NVARCHAR(MAX),
	@DonViTinh int,
	@IsOrderChucVu bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Query AS NVARCHAR(MAX)

	SET @Query =
	'
	WITH BangLuongThang AS (
		SELECT
			dsCapNhapBangLuong.Thang										AS Thang,
			dsCapNhapBangLuong.Nam											AS Nam,
			bangLuong.Ma_CBo												AS MaCanBo,
			bangLuong.MA_PHUCAP												AS MaPhuCap,
			bangLuong.GIA_TRI / ' + CAST(@DonViTinh AS VARCHAR(100)) + '	AS GiaTri
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @MaPhuCap + '''))
			AND dsCapNhapBangLuong.Ma_CachTL=''' + @MaCachTl + '''
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
			ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
			donVi.Ma_DonVi		AS MaDonVi,
			capBac.Ma_Cb			AS MaCapBac,
			capBac.XauNoiMa
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
		  ON canBo.Parent = donVi.Ma_DonVi
		INNER JOIN TL_DM_CapBac capBac
		  ON canBo.Ma_CB = capBac.Ma_Cb
		LEFT JOIN TL_DM_ChucVu chucVu
		  ON canBo.Ma_CV=chucVu.Ma_Cv
		WHERE
			canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND donVi.Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, Ten, HSChucVu, MaCapBac, XauNoiMa, ' + @MaPhuCap + ' FROM (
		SELECT
			canBo.MaDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.Ten,
			canBo.HSChucVu,
			canBo.MaCapBac,
			bangLuong.MaPhuCap,
			bangLuong.GiaTri,
			canBo.XauNoiMa
		FROM BangLuongThang bangLuong
		INNER JOIN ThongTinCanBo canBo
			ON bangLuong.MaCanBo = canBo.MaCanBo
	) x
	PIVOT
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @MaPhuCap + ')
	) pvt'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY HSChucVu , MaCapBac , Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY MaCapBac , Ten ';
	execute(@Query)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 12/10/2022 6:40:19 PM ******/
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
			bangLuong.Ma_Hieu_CanBo AS MaHieuCanBo
		FROM TL_BangLuong_Thang bangLuong
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuong.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols1 + '''))
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
			AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi										AS MaDonVi,
			canBo.Ma_CanBo										AS MaCanBo,
			canBo.Ten_CanBo										AS TenCanBo,
			dbo.f_split_empty(canbo.Ten_CanBo)                  AS Ten,
			canBo.Ma_Hieu_CanBo                                 AS MaHieuCanBo,
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
			AND canBo.Nam = ' + CAST(@Nam AS VARCHAR(4)) + '
	),
	BangLuongThang AS
  (SELECT dsCapNhapBangLuong.Nam AS Nam,
          bangLuong.MA_PHUCAP AS MaPhuCap,
          bangLuong.GIA_TRI AS GiaTri,
          bangLuong.Ma_Hieu_CanBo AS MaHieuCanBo,
          bangLuong.Ma_CBo AS MaCanBo
   FROM TL_BangLuong_Thang bangLuong
   JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong ON bangLuong.parent = dsCapNhapBangLuong.Id
   WHERE bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols2 + '''))
     AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
     AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
     AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
     AND dsCapNhapBangLuong.Status=1 ),
	 Cach0 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
		  HSChucVu,
		  MaCapBac,
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
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols2 + ')) pvt
   GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten,
		    HSChucVu,
		    MaCapBac),
     Ca2 AS
  (SELECT MaDonVi,
          MaHieuCanBo,
          TenCanBo,
		  Ten,
		  HSChucVu,
		  MaCapBac,
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
      INNER JOIN ThongTinCanBo canBo ON bangLuong.MaCanBo = canBo.MaCanBo) x PIVOT (SUM(GiaTri)
                                                                                    FOR MaPhuCap IN (' + @Cols1 + ')) pvt
		GROUP BY MaDonVi,
            MaHieuCanBo,
            TenCanBo,
			Ten,
		    HSChucVu,
		    MaCapBac)

			SELECT Cach0.MaDonVi,
       Cach0.MaHieuCanBo,
       Cach0.TenCanBo,
	   Cach0.Ten,
	   Cach0.HSChucVu,
	   Cach0.MaCapBac,
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
		JOIN Ca2 ON Cach0.MaHieuCanBo = Ca2.MaHieuCanBo'

	IF @IsOrderChucVu = 1
	SET @Query = @Query +' ORDER BY Cach0.HSChucVu , Cach0.MaCapBac , Cach0.Ten ';
	ELSE 
	SET @Query = @Query +' ORDER BY Cach0.MaCapBac , Cach0.Ten ';
	execute(@Query)
	--select @Query
END
;
;
GO

if not exists (select * from TL_DM_PhuCap where Ma_PhuCap = 'PCBAOVU_HS' and Xau_Noi_Ma = 'PCTRA_HS-PCBAOVU_HS')
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 0, 'PCBAOVU_HS', 'PCTRA_HS', N'Hệ số phụ cấp báo vụ', 1, 'PCTRA_HS-PCBAOVU_HS');

if not exists (select * from TL_DM_PhuCap where Ma_PhuCap = 'PCCOYEU_HS' and Xau_Noi_Ma = 'PCTRA_HS-PCCOYEU_HS')
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 0, 'PCCOYEU_HS', 'PCTRA_HS', N'Hệ số phụ cấp trách nhiệm cơ yếu', 1, 'PCTRA_HS-PCCOYEU_HS')

if not exists (select * from TL_DM_PhuCap where Ma_PhuCap = 'PCCOYEU_TT' and Xau_Noi_Ma = 'PCTRA_TT-PCCOYEU_TT')
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, iLoai, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 2, 1, 0, 'PCCOYEU_TT', 'PCTRA_TT', N'Phụ cấp báo vụ', 1, 'PCTRA_TT-PCCOYEU_TT');

if not exists (select * from TL_DM_PhuCap where Ma_PhuCap = 'PCBAOVU_TT' and Xau_Noi_Ma = 'PCTRA_TT-PCBAOVU_TT')
insert into TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Dinh_Dang, Gia_Tri, iDinhDang, iLoai, Is_Formula, Is_Readonly, Ma_PhuCap, Parent, Ten_PhuCap, Tinh_BHXH, Xau_Noi_Ma)
values (1, 1, 1, 1, 0, 0, 0, 2, 1, 0, 'PCBAOVU_TT', 'PCTRA_TT', N'Phụ cấp trách nhiệm cơ yếu', 1, 'PCTRA_TT-PCBAOVU_TT')

update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 'PCANQP_HS*(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)'
where Ma_CachTL = 'CACH0' and Ma_Cot = 'PCANQP_TT'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = '(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCTHUHUT_HS'
where Ma_Cot='PCTHUHUT_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = '(LHT_TT+PCCV_TT+PCTNVK_TT+HSBL_TT)*PCTHUHUT_HS'
where Ma_Cot='PCTHUHUT_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = '(LHT_TT+PCCV_TT+HSBL_TT)*PCDTQUANSU_HS'
where Ma_Cot='PCDTQUANSU_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 'PCTHD_HS*(LHT_TT+HSBL_TT)'
where Ma_Cot='PCTHD_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 'PCTNVK_HS*(LHT_TT+HSBL_TT)'
where Ma_Cot='PCTNVK_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = 'LHT_TT+HSBL_TT+PCCT_TT-GTPT_TT-BHCN_TT'
where Ma_Cot='LUONGTHUE_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = '(LHT_TT+HSBL_TT)*TRICHLUONG_SN/26'
where Ma_Cot='TRICHQUYKHAC_TT' and Ma_CachTL = 'CACH0'
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = '(LHT_TT+HSBL_TT-BHCN_TT-THUETNCN_TT)*TRICHLUONG22_SN/22'
where Ma_Cot='TRICHQUYPCTT_TT' and Ma_CachTL = 'CACH0'

update TL_DM_PhuCap set bSaoChep = 0
where Parent in ('BHCN', 'BHDV', 'HETHONG', 'LPC_HS', 'LPC_TT', 'PCDACTHU_HS', 'PCDACTHU_TT', 'PCTRA_HS', 'PCTRA_TT', 'TIENAN', 'TRUYLINH')

IF NOT EXISTS (SELECT * FROM DANHMUC WHERE sTen = N'5 - Ngân sách địa phương' AND sType = 'NS_NguonNganSach')
INSERT INTO [dbo].[DanhMuc]
           ([iID_DanhMuc]
           ,[iID_MaDanhMuc]
           ,[iTrangThai]
           ,[sTen]
           ,[sType]
           )
     VALUES
          (newid(), 5, 1, N'5 - Ngân sách địa phương', 'NS_NguonNganSach')

IF NOT EXISTS (SELECT * FROM DANHMUC WHERE sTen = N'8 - Nguồn Dự phòng' AND sType = 'NS_NguonNganSach')
INSERT INTO [dbo].[DanhMuc]
           ([iID_DanhMuc]
           ,[iID_MaDanhMuc]
           ,[iTrangThai]
           ,[sTen]
           ,[sType]
           )
     VALUES
          (newid(), 8, 1, N'8 - Nguồn Dự phòng', 'NS_NguonNganSach')

IF NOT EXISTS (SELECT * FROM DANHMUC WHERE sTen = N'9 - BHXH, YT, TN' AND sType = 'NS_NguonNganSach')
INSERT INTO [dbo].[DanhMuc]
           ([iID_DanhMuc]
           ,[iID_MaDanhMuc]
           ,[iTrangThai]
           ,[sTen]
           ,[sType]
           )
     VALUES
          (newid(), 9, 1, N'9 - BHXH, YT, TN', 'NS_NguonNganSach')


