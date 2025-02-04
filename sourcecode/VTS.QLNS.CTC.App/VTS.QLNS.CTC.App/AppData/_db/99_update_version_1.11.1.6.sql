/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 09/06/2022 8:44:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 09/06/2022 8:44:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 09/06/2022 8:44:43 PM ******/
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

	--print @Query
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 09/06/2022 8:44:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
	@thang int, @nam int, @thangTruoc int, @namTruoc int, @maDonVi nvarchar(MAX), @sM nvarchar(1)
As
Begin
	if @sM = '3'
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		)

		Select 
			canbo.Ten_CanBo TenCanBo,
			CASE 
				WHEN canbo.Ma_TangGiam in ('250', '280') THEN canbothangtruoc.CapBacCu
				ELSE canbo.Ma_CB
			END CapBac,
			CAST('1' as int) as SoLuong,
			CASE 
				WHEN canbo.Ma_TangGiam in ('290') THEN canbothangtruoc.TenDonViCu
				ELSE canbo.Ten_DonVi
			END DonVi,
			mlqs.sMoTa NoiDung
		From TL_DM_CanBo canbo
			Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
			Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			left Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
		Where canbo.Thang = @thang
			And canbo.Nam = @nam
			And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280', '290'))
			And iNamLamViec = @nam
			And bHangCha = 0
			And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
		Order By Ma_DonVi, CapBac
	else
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		),

		
		
		KhongTuyenQuan as (
			Select 
				canbo.Ten_CanBo TenCanBo,
				CAST('1' as int) as SoLuong,
				
				CASE 
					WHEN canbo.Ma_TangGiam in ('350', '380') THEN Vcanbothangtruoc.CapBacCu
					ELSE canbo.Ma_CB
				END CapBac,
				CASE 
					WHEN canbo.Ma_TangGiam in ('390') THEN Vcanbothangtruoc.TenDonViCu
					ELSE canbo.Ten_DonVi
				END DonVi,
				
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
				left Join CanBoThangTruoc Vcanbothangtruoc on canbo.Ma_Hieu_CanBo = canbo.Ma_Hieu_CanBo
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And (sM = @sM OR canbo.Ma_TangGiam in ('350','380','390'))
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam not in ('210', '220')),

		TuyenQuan as (
		Select 
			(CAST(COUNT(*) as nvarchar(MAX)) + N' đồng chí') as TenCanBo, 
			canbo.Ma_CB CapBac, 
			COUNT(*) SoLuong,
			donvi.Ten_DonVi DonVi, 
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And sM = @sM
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam in ('210', '220')
			Group By canbo.Ma_CB, donvi.Ten_DonVi, mlqs.sMoTa
		)
		
		Select *
		From KhongTuyenQuan
		union  all
		select * from  TuyenQuan


		Order By DonVi, CapBac desc

		
End
;
;
GO
