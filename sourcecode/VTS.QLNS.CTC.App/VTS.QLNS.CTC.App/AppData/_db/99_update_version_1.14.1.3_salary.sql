/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]    Script Date: 12/03/2024 10:31:34 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]    Script Date: 12/03/2024 10:31:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 09/12/2021
-- Description:	Lấy dữ liệu báo cáo bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_giaithich_chitiet_raquan_nq104]
	@MaDonVi NVARCHAR(max), 
	@Thang int,
	@Nam AS int,
	@donViTinh int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DECLARE @Cols AS NVARCHAR(MAX)
	DECLARE @Query AS NVARCHAR(MAX)

	SET @Cols = 'TCRAQUAN_TT,TCVIECLAM_TT,TIENTAUXE_TT,TIENANDUONG_TT,TIENCTLH_TT,GTKHAC_TT'
	SET @Query =
	'WITH BangLuongThang AS (
			SELECT
				dsCapNhapBangLuong.Thang	AS Thang,
				dsCapNhapBangLuong.Nam		AS Nam,
				bangLuong.Ma_Can_Bo			AS MaCanBo,
				bangLuong.MA_PHU_CAP			AS MaPhuCap,
				bangLuong.GIA_TRI			AS GiaTri
			FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuong
			JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
				ON bangLuong.parent = dsCapNhapBangLuong.Id
			WHERE
				bangLuong.Ma_Phu_Cap IN (SELECT * FROM f_split(''' + @Cols + '''))
				AND dsCapNhapBangLuong.Ma_CachTL=''CACH0''
				AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
				AND dsCapNhapBangLuong.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
				AND dsCapNhapBangLuong.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
				AND dsCapNhapBangLuong.Status=1 
	),
	
	ThongTinCanBo AS (
		SELECT
			canBo.Thang,
			canBo.Nam,
			donVi.Ma_DonVi		AS MaDonVi,
			donVi.Ten_Donvi		AS TenDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
			ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
			ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
			ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
			canBo.Ngay_NN AS NhapNgu,
			canBo.Ngay_XN AS XuatNgu
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		LEFT JOIN TL_DM_CapBac_NQ104 capBac
			ON canBo.Ma_CB104=capBac.Ma_Cb
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Ngay_XN is not null
			AND canBo.Thang=' + CAST(@Thang AS VARCHAR(2)) + '
			AND canBo.Nam=' + CAST(@Nam AS VARCHAR(4)) + '
	)

	SELECT MaDonVi, MaCanBo, TenCanBo, MaCapBac, CapBac, NgayNn, NgayXn, NhapNgu, XuatNgu, Thang, Nam,
		TCRAQUAN_TT,
		TCVIECLAM_TT,
		TIENTAUXE_TT,
		TIENANDUONG_TT,
		TIENCTLH_TT,
		GTKHAC_TT,
		(TCRAQUAN_TT + TCVIECLAM_TT + TIENTAUXE_TT + TIENANDUONG_TT + TIENCTLH_TT - GTKHAC_TT) TIENTONG
	FROM (
		SELECT
			canBo.Thang AS Thang,
			canBo.Nam AS Nam, 
			canBo.MaDonVi,
			canBo.TenDonVi,
			canBo.MaCanBo,
			canBo.TenCanBo,
			canBo.MaCapBac,
			canBo.CapBac,
			canBo.NgayNhapNgu NgayNn,
			canBo.NgayXuatNgu NgayXn,
			canBo.NhapNgu,
			canBo.XuatNgu,
			bangLuong.GiaTri GiaTri,
			bangLuong.MaPhuCap MaPhuCap
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
	ORDER BY MaCapBac DESC, TenCanBo DESC'
	execute(@Query)
END
;
GO
