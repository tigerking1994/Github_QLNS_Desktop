/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 7/9/2024 5:40:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]    Script Date: 7/9/2024 5:40:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_can_bo_che_do_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]    Script Date: 7/9/2024 5:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_can_bo_che_do_index]
	@MaCanBo nvarchar(100)
AS
BEGIN
	select
		canbo.Id,
		chedo.bDisplay IsDisplay,
		chedo.sMaCheDo,
		chedo.sMaCheDoCha,
		chedo.sTenCheDo,
		CONCAT(chedocha.sMaCheDo, ' - ', chedocha.sTenCheDo) AS sTenCheDoCha,
		chedo.sXauNoiMa,
		--case when (chedo.sMaCheDoCha = '' 
		--			or chedo.sMaCheDoCha is null 
		--			or chedo.sMaCheDo in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)) then 1 
		--		else 0 
		--end as IsHangCha,
		canbo.dDenNgay,
		canbo.dTuNgay,
		canbo.fSoNgayNghiPhep as FSoNgayNghiPhep,
		canbo.fSoNgayHuongBHXH,
		canbo.iSoNgayNghi,
		canbo.sMaCanBo,
		canbo.sMaCheDo,
		canbo.sTenCheDo,
		canbo.fSoTien,
		canbo.sSoQuyetDinh,
		canbo.dNgayQuyetDinh,
		canbo.iThangLuongCanCuDong,
		canbo.iNamCanCuDong,
		canbo.fGiaTriCanCu,
		canbo.iNam,
		canbo.iThang
	from 
	TL_DM_CheDoBHXH chedo
	left join TL_CanBo_CheDoBHXH canbo on chedo.sMaCheDo = canbo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	left join TL_DM_CheDoBHXH chedocha on chedo.sMaCheDoCha = chedocha.sMaCheDo
	where chedo.sMaCheDo <> 'SONGAYHUONG'
	and (chedo.sMaCheDoCha IN (select sMaCheDo from TL_DM_CheDoBHXH where sMaCheDoCha = '')
		or chedo.sMaCheDoCha in ('BENHDAINGAY','OMKHAC'))
	order by sTenCheDoCha

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 7/9/2024 5:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
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
	capBac.Ma_Cb MaCapBac,
	canBo.BHTN,
	canBo.PCCV,
	canBo.BNuocNgoai,
	canBo.bTamGiamTamGiu as BTamGiamTamGiu,
	canBo.Ngay_XN as NgayXn,
	canBo.Ma_TangGiam
	INTO #ThongTinCanBo
	FROM TL_DM_CanBo canBo
	INNER JOIN TL_DM_DonVi donVi
	ON canBo.Parent = donVi.Ma_DonVi
	INNER JOIN TL_DM_CapBac capBac
	ON canBo.Ma_CB = capBac.Ma_Cb
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
	canBo.BTamGiamTamGiu ,
	@MaCachTl AS MaCachTl,
	canBoPhuCap.MA_PHUCAP AS MaPhuCap,
	canBo.Ma_TangGiam as MaTangGiam,
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
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb

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
INSERT [dbo].[TL_DM_PhuCap] ([Id], [bGiaTri], [bHuongPc_Sn], [bSaoChep], [Chon], [Cong_Thuc], [Dinh_Dang], [Gia_Tri], [He_So], [HuongPC_SN], [iDinhDang], [iLoai], [Is_Formula], [Is_Readonly], [IThang_ToiDa], [Ma_KMCP], [Ma_PhuCap], [Ma_TTM_Ng], [Numeric_Scale], [Parent], [PhanTram_CT], [Readonly], [Splits], [Ten_Ngan], [Ten_PhuCap], [Tinh_BHXH], [Tinh_TNCN], [Xau_Noi_Ma], [XSort], [fGiaTriLonNhat], [fGiaTriNhoNhat], [fGiaTriPhuCap_KemTheo], [iId_Ma_PhuCap_KemTheo], [iId_PhuCap_KemTheo], [Ten_NganHang]) 
VALUES (NEWID(), 1, 1, 0, 1, NULL, 0, CAST(0.500 AS Numeric(17, 3)), NULL, NULL, NULL, NULL, 0, 1, NULL, NULL, N'TILE_HUONGTGTG', NULL, NULL, N'HETHONG', NULL, NULL, NULL, NULL, N'Tỷ lệ hưởng của đối tượng tạm giam, tạm giữ', 1, NULL, N'HETHONG-TILE_HUONGTGTG', NULL, NULL, NULL, NULL, NULL, NULL, NULL);
