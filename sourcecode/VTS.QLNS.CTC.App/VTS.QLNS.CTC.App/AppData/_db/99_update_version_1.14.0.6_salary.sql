/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 04/03/2024 8:04:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert_nq104]    Script Date: 04/03/2024 8:04:43 AM ******/
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
	LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan_NQ104) cachTinhLuong
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
