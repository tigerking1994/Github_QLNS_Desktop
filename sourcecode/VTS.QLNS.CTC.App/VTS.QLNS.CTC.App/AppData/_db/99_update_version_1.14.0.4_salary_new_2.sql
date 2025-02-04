/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_bangluong_thang_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_danhsach_canbo_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_chitiet_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_chitiet_nq104]
	@Id UNIQUEIDENTIFIER
AS
DECLARE @cols AS NVARCHAR(MAX);
DECLARE @query  AS NVARCHAR(MAX);;

SET @cols = STUFF(
  (
    SELECT
      DISTINCT ',' + QUOTENAME(phucap.Ma_PhuCap) 
    FROM TL_DM_PhuCap phucap
        FOR XML PATH(''), TYPE
  ).value('.', 'NVARCHAR(MAX)'),1,1,'')
  
SET @query = '
	WITH ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo	AS MaCanBo,
			canBo.Ten_CanBo	AS TenCanBo,
			canBo.IsDelete AS IsDelete,
			canbo.Ngay_XN,
			canbo.Ma_TangGiam,
			donVi.Ma_DonVi	AS MaDonVi,
			donVi.Ten_DonVi	AS TenDonVi
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		WHERE canBo.Khong_Luong = 0
	)
	SELECT Thang, Nam, MaCanBo, TenCanBo, TenDonVi, ' + @cols + ' FROM (
		SELECT
			bangLuongThang.THANG		AS Thang,
			bangLuongThang.NAM			AS Nam,
			canBo.MaCanBo				AS MaCanBo,
			canBo.TenCanBo				AS TenCanBo,
			canBo.TenDonVi				AS TenDonVi,
			bridge.gia_tri GiaTri,
			bridge.ma_phu_cap MaPhuCap
		FROM TL_BangLuong_Thang_NQ104 bangLuongThang
		LEFT JOIN TL_BangLuong_Thang_Bridge_NQ104 bridge ON bangLuongThang.Ma_CBo = bridge.ma_can_bo
		INNER JOIN ThongTinCanBo canBo
			ON bangLuongThang.Ma_CBo = canBo.MaCanBo
		WHERE
			(canBo.IsDelete = 1 or (canbo.Ma_TangGiam = ''320'' and month(canbo.Ngay_XN) = bangLuongThang.THANG and year(canbo.Ngay_XN) = bangLuongThang.NAM))
			and bangLuongThang.parent = + ''' + CONVERT(NVARCHAR(100), @Id) + '''
	) x
	PIVOT 
	(
		SUM(GiaTri)
		FOR MaPhuCap IN (' + @cols + ')
	) p '
execute(@query)
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]
@Thang int,
@Nam int,
@MaDonVi NVARCHAR(MAX),
@MaCachTl NVARCHAR(50),
@SoNgay int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.

	--DECLARE @Cols AS NVARCHAR(MAX)

	--SET @Cols = STUFF(
	--(
	--SELECT
	--	DISTINCT ',' + phucap.data
	--FROM TL_CanBo_PhuCap_NQ104 phucap
	--	FOR XML PATH(''), TYPE
	--).value('.', 'NVARCHAR(MAX)'),1,1,'')

	--select * into #temp from parseJSON(@Cols)

	--select CanBo as MA_CBO,
	--[MaPhuCap] MA_PHUCAP, CONVERT(DECIMAL(18,2),replace([GiaTri], ',', '')) gia_tri, CONVERT(DECIMAL(18,2),replace([NgayHuongPhuCap], ',', '')) HuongPC_SN
	--INTO #canBoPhuCap
	--from (
	--SELECT x1.StringValue CanBo, x4.parent_id, x4.name, x4.StringValue StringValue FROM #temp x1
	--JOIN #temp x2 on x1.Parent_ID = x2.Parent_ID and x1.Element_ID <> x2.Element_ID
	--join #temp x3 on x2.Object_ID = x3.Parent_ID
	--join #temp x4 on x3.Object_ID = x4.Parent_ID
	----where x1.name = 'MaCanBo' and x1.StringValue like (CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%'))
	--) as sourcetable
	--pivot
	--(
	--	min(stringvalue)
	--	for name in ([MaPhuCap], [GiaTri], [NgayHuongPhuCap])
	--) as pivottable


	select ma_can_bo MA_CBO, gia_tri, ngay_huong_phu_cap HuongPC_SN, ma_phu_cap ma_phucap
	into #canBoPhuCap
	from TL_CanBo_PhuCap_Bridge_NQ104 where ma_can_bo like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')



	SELECT
	MA_CBO MaCanBo,
	(CASE WHEN pc.Parent = 'TIENAN' THEN 'TA_TT'
	WHEN pc.Parent = 'TIENAN2' THEN 'TA_TT2' ELSE '' END) AS PARENT,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA1,
	SUM (
		--CASE WHEN PARENT <> N'TIENAN2' THEN 0
		--WHEN canBoPhuCap.MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		--ELSE ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		--END
		CASE WHEN PARENT <> N'TIENAN2' THEN 0
		WHEN ISNULL(canBoPhuCap.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(canBoPhuCap.HuongPC_SN, 0) * canBoPhuCap.GIA_TRI
		ELSE ISNULL(@SoNgay, 0) * canBoPhuCap.GIA_TRI
		END
	) GiaTriTA2
	INTO #SoLieuTienAn
	FROM #canBoPhuCap canBoPhuCap
	INNER JOIN TL_DM_PhuCap as pc on canBoPhuCap.MA_PHUCAP = pc.Ma_PhuCap
	--WHERE
	--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
	WHERE
	pc.PARENT IN ('TIENAN', 'TIENAN2') and canBoPhuCap.Gia_Tri <> 0 and canBoPhuCap.MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')
	GROUP BY canBoPhuCap.MA_CBO, pc.PARENT


	SELECT
	canBo.Ma_CanBo AS MaCanBo,
	canBo.Ten_CanBo AS TenCanBo,
	donVi.Ma_DonVi MaDonVi,
	canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
	capBac.ma_cb MaCapBac,
	canBo.BHTN,
	canBo.PCCV,
	canBo.BNuocNgoai,
	canBo.Ngay_XN as NgayXn
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo canBo
	INNER JOIN TL_DM_DonVi donVi
	ON canBo.Parent = donVi.Ma_DonVi
	INNER JOIN TL_DM_CapBac_NQ104 capBac
	ON canBo.ma_cb104 = capBac.Ma_Cb
	WHERE
	canBo.Thang = @Thang
	AND canBo.Nam = @Nam
	AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	AND (canBo.IsDelete = 1 or (canbo.Ma_TangGiam = '320' and month(canbo.Ngay_XN) <= @thang and year(canbo.Ngay_XN) = @Nam))
	AND canBo.Khong_Luong <> 1


SELECT
	newid() AS Id,
	@Thang AS Thang,
	@Nam AS Nam,
	canBo.MaCanBo AS MaCbo,
	canBo.TenCanBo AS TenCbo,
	canBo.MaDonVi AS MaDonVi,
	canBo.BNuocNgoai ,
	@MaCachTl AS MaCachTl,
	canBoPhuCap.MA_PHUCAP AS MaPhuCap,
	CASE
	WHEN canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in ('1', '2', '3') THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCVIECLAM' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' AND canbo.NgayXn is null THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) <> @NAM THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'THANG_TCXN' and canbo.NgayXn is not null and year(canbo.NgayXn) = @NAM and month(canbo.NgayXn) <> @Thang THEN 0
	WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTriTA1
	WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT2' THEN soLieuTienAn.GiaTriTA2
	WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
	ELSE canBoPhuCap.GIA_TRI
	END AS GiaTri,
	canBo.MaHieuCanBo AS MaHieuCanBo,
	canBo.MaCapBac AS MaCb,
	canBoPhuCap.HuongPC_SN AS HuongPcSn,
	cachTinhLuong.CongThuc AS CongThuc,
	CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	--,canBoPhuCap.bCapNhat as IsCapNhat
FROM #canBoPhuCap canBoPhuCap
	INNER JOIN #ThongTinCanBo canBo
	ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN #SoLieuTienAn soLieuTienAn
	ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo AND canBoPhuCap.MA_PHUCAP = soLieuTienAn.Parent
	LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
	ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac_NQ104 cb on canBo.MaCapBac = cb.ma_cb

	--DELETE blt
	--FROM TL_BangLuong_Thang blt
	--INNER JOIN #ThongTinCanBo ttcb ON blt.Ma_CBo = ttcb.MaCanBo
	--INNER JOIN TL_CanBo_PhuCap cbpc ON cbpc.MA_CBO = blt.Ma_CBo
	--WHERE bCapNhat = 1

	--UPDATE cbpc
	--SET bCapNhat = 0
	--FROM TL_CanBo_PhuCap cbpc
	--INNER JOIN #ThongTinCanBo ttcb ON cbpc.MA_CBO = ttcb.MaCanBo

drop table #SoLieuTienAn
drop table #ThongTinCanBo
drop table #canBoPhuCap

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_danhsach_canbo_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_find_danhsach_canbo_nq104]
AS
	Select
	  canbo.[Id]
      ,[BHTN]
      ,[bNuocNgoai]
      ,[Cb_KeHoach]
      ,[Cccd]
      ,[DateCreated]
      ,[DateModified]
      ,[Dia_Chi]
      ,[Dien_Thoai]
      ,[GTGC]
      ,[HeSoLuong]
      ,[HsLuongKeHoach]
      ,[HsLuongTran]
      ,canbo.[iTrangThai]
      ,[IdLuongTran]
      ,[IsDelete]
      ,[IsLock]
      ,[Is_Nam]
      ,[Khong_Luong]
      ,[Ma_BL]
      ,[Ma_CanBo]
      ,canbo.[Ma_CB]
      ,[Ma_CbCu]
      ,canbo.[ma_cvd104] as Ma_CV
      ,[Ma_DiaBan_HC]
      ,[Ma_Hieu_CanBo]
      ,[Ma_KhoBac]
      ,[Ma_PBan]
      ,[MaSo_DV_SDNS]
      ,[MaSo_VAT]
      ,[Ma_TangGiam]
      ,[Ma_TangGiamCu]
      ,[MaTK_LQ]
      ,[Nam]
      ,[Nam_TN]
      ,[Nam_VK]
      ,[NgayCap_CMT]
      ,[Ngay_NhanCB]
      ,[Ngay_NN]
      ,[NgaySinh]
      ,[Ngay_TN]
      ,[NgayTruyLinh]
      ,[Ngay_XN]
      ,[Nhom]
      ,[NoiCap_CMT]
      ,[NoiCongTac]
      ,[PCCV]
      ,canbo.[Parent]
      ,canbo.[Readonly]
      ,[So_CMT]
      ,[So_SoLuong]
      ,[So_TaiKhoan]
      ,canbo.[Splits]
      ,[Ten_CanBo]
      ,[Ten_KhoBac]
      ,[Thang]
      ,[Thang_TNN]
      ,[ThoiHan_TangCb]
      ,[TM]
      ,[UserCreator]
      ,[UserModifier]
      ,[bKhongTinhNTN],
	  donvi.Ten_DonVi as Ten_DonVi,
	  ISNULL(capbac.ten_cb, '') CapBac,
	  ISNULL(chucvu.ten, '') ChucVu,
	  dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
	  canbo.bTinhBHXH BTinhBHXH, --anhnh156
	  canbo.dan_toc DanToc,
	  canbo.loai_doi_tuong LoaiDoiTuong,
	  canbo.ma_cb104 MaCb104,
	  canbo.loai Loai,
	  canbo.nhom_chuyen_mon NhomChuyenMon,
	  canbo.ma_bac_luong MaBacLuong,
	  canbo.tien_luong_cb TienLuongCb,
	  canbo.ma_cvd104 MaCvd104,
	  canbo.tien_luong_cvd TienLuongCvd,
	  canbo.ngay_nhan_cb_tu_ngay NgayNhanCbTuNgay,
	  canbo.ngay_nhan_cb_den_ngay NgayNhanCbDenNgay,
	  canbo.ngay_nhan_cvd_tu_ngay NgayNhanCvdTuNgay,
	  canbo.ngay_nhan_cvd_den_ngay NgayNhanCvdDenNgay,
	  canbo.so_thang_tinh_bao_luu_cvd SoThangTinhBaoLuuCvd,
	  canbo.so_thang_tinh_bao_luu_cb SoThangTinhBaoLuuCb,
	  canbo.tien_bao_luu_cb TienBaoLuuCb,
	  canbo.tien_bao_luu_cvd TienBaoLuuCvd
	From TL_DM_CanBo canbo
	Left join TL_DM_CapBac_NQ104 capbac on capbac.Ma_Cb = canbo.ma_cb104
	Left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi 
	Left join TL_DM_ChucVu_NQ104 chucvu on canbo.ma_cvd104 = chucvu.ma
	Where canbo.IsDelete = 1
	--Order By canbo.Parent, canbo.Ma_CV desc, canbo.Ma_CB desc
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_bangluong_thang_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_donvi_bangluong_thang_nq104] @thang int, @nam int, @cachTinhLuong varchar(20)
AS
BEGIN

select Parent into #tmpCB from TL_DM_CanBo where left (Ma_CB104, 1) <> 0 and Thang = @thang and Nam = @nam group by Parent

select donvi.* from #tmpCB canbo
left join TL_DS_CapNhap_BangLuong_NQ104 bangluong  on canbo.Parent = bangluong.Ma_CBo 
left join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
where bangluong.Thang = @thang and bangluong.Nam = @nam and bangluong.Ma_CachTL = @cachTinhLuong

drop table #tmpCB

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]    Script Date: 28/02/2024 10:21:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_nq104]
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

SET @Cols = 'NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCKVCS_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM'
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
	SELECT gia_tri, ma_phu_cap ma_phucap, ma_can_bo ma_cbo, ma_hieu_can_bo Ma_Hieu_CanBo, parent FROM TL_BangLuong_Thang_Bridge_NQ104
	--WHERE THANG = ' + CAST(@Thang AS VARCHAR(2)) + ' 
	--AND NAM = ' + CAST(@Nam AS VARCHAR(4)) + ' 
	--AND Ma_DonVi IN (SELECT * FROM f_split(''' + @MaDonVi + '''))
	WHERE ( ' + CAST(@IsNew AS VARCHAR(2)) + '= 0 OR (ma_hieu_can_bo NOT IN (SELECT Ma_Hieu_CanBo FROM CanBoThangTruoc WHERE MaDonViCu = Ma_Don_Vi)))
),
BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_CBo AS MaCanBo,
bangLuong.MA_PHUCAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
FROM blt bangLuong
JOIN TL_DS_CapNhap_BangLuong_NQ104 dsCapNhapBangLuong
ON bangLuong.parent = dsCapNhapBangLuong.Id
WHERE
--bangLuong.Ma_PhuCap IN (SELECT * FROM f_split(''' + @Cols + '''))
 dsCapNhapBangLuong.Ma_CachTL=''CACH0''
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
--ISNULL(chucVu.HeSo_Cv, 0) AS HSChucVu,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.xau_noi_ma XauNoiMa
FROM TL_DM_CanBo canBo
INNER JOIN TL_DM_DonVi donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104=capBac.Ma_Cb
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_Cvd104=chucVu.Ma
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
--canBo.HSChucVu,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.Stk,
canBo.NgayNhapNgu,
canBo.NgayXuatNgu,
canBo.NgayTaiNgu,
canBo.NamTN,
canBo.Tnn,
canBo.NTN,
canBo.XauNoiMa
FROM BangLuongThang bangLuong
INNER JOIN ThongTinCanBo canBo
ON bangLuong.MaCanBo = canBo.MaCanBo'
If @IsGiaTriAm = 1
SET @Query = @Query + ' WHERE bangLuong.THANHTIEN < 0';
IF @IsOrderChucVu = 1
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
GO
