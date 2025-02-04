/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_clean_salary_data]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_clean_salary_data]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_clean_salary_data]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_cach1]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_cach1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_cach1]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 7/10/2024 4:33:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 7/10/2024 4:33:32 PM ******/
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
	into #canBoPhuCap_tmp1
	from TL_CanBo_PhuCap where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	select temp.MA_CBO, temp.gia_tri, temp.HuongPC_SN, temp.ma_phucap into #canBoPhuCap
	from (
		select MA_CBO, gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
		union
		select cbpc.MA_CBO, pc.gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
		from
		(select distinct MA_CBO from #canBoPhuCap_tmp1) cbpc
		cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP in ('BHCN', 'THANHTIEN', 'TM')) pc
		) temp

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
drop table #canBoPhuCap_tmp1

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 7/10/2024 4:33:32 PM ******/
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

	select MA_CBO, gia_tri, HuongPC_SN, ma_phucap
	into #canBoPhuCap_tmp1
	from TL_CanBo_PhuCap where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	select temp.MA_CBO, temp.gia_tri, temp.HuongPC_SN, temp.ma_phucap into #canBoPhuCap
	from (
		select MA_CBO, gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
		union
		select cbpc.MA_CBO, pc.gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
		from
		(select distinct MA_CBO from #canBoPhuCap_tmp1) cbpc
		cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP in ('BHCN', 'THANHTIEN', 'TM')) pc
		) temp

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
		AND canBo.Ma_CanBo IN (SELECT MA_CBO FROM #canBoPhuCap WHERE MA_PHUCAP LIKE '%TTL%' AND GIA_TRI > 0 GROUP BY MA_CBO)
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
	FROM #canBoPhuCap canBoPhuCap
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet]    Script Date: 7/10/2024 4:33:32 PM ******/
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
	
	--SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	--FROM 
	--(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
	--INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	LEFT JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	LEFT JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
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
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM( 
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
		) AS SoNgay,
		CASE WHEN @checkTongHop > 1 THEN
			ROUND(isnull(SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
			), 0) - isnull(SUM(bangLuongTruyThu.Gia_Tri), 0), 0)
		ELSE
		SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
		) END AS GiaTri,
		SUM(CASE WHEN @checkTongHop > 1 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) - sum (case WHEN bangLuongTruyThu.Ma_CachTL = 'CACH1' THEN 1 ELSE 0 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	LEFT JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN (SELECT * FROM TL_BangLuong_Thang_TruyThu WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0 AND Ma_CachTL ='CACH1') bangLuongTruyThu 
		ON bangLuong.Ma_CBo = bangLuongTruyThu.Ma_CBo and bangLuong.Ma_PhuCap = bangLuongTruyThu.Ma_PhuCap and bangLuong.Ma_DonVi = bangLuongTruyThu.Ma_DonVi
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




GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_cach1]    Script Date: 7/10/2024 4:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_cach1]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.HuongPC_SN, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	LEFT JOIN TL_CanBo_PhuCap as pc on cb.Ma_CanBo = pc.MA_CBO
	LEFT JOIN #tmpMapping as mp on pc.MA_PHUCAP = mp.Ma_PhuCap
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
		--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
		SUM( 
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
		) AS SoNgay,
		
		SUM(
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
			ELSE bangLuong.Gia_Tri END
		) AS GiaTri,
		SUM(CASE WHEN @checkTongHop > 1 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang_TruyThu WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0 and Ma_CachTL = 'CACH1') bangLuong
	LEFT JOIN TL_CanBo_PhuCap as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	INNER JOIN TL_DM_PhuCap as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	LEFT JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
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
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_clean_salary_data]    Script Date: 7/10/2024 4:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_clean_salary_data]
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_CanBo_PhuCap_BK]') AND type in (N'U')) drop table TL_CanBo_PhuCap_BK;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_BangLuong_Thang_BK]') AND type in (N'U')) drop table TL_BangLuong_Thang_BK;

	-- 1. Bảng TL_CanBo_PhuCap
	----
	SELECT * INTO TL_CanBo_PhuCap_BK 
	FROM TL_CanBo_PhuCap
	WHERE ISNULL(GIA_TRI, 0) <> 0
	OR MA_PHUCAP IN ('THUONG_TT','THUEDANOP_TT','GIAMTHUE_TT','THUNHAPKHAC_TT','TIENTAUXE_TT','TIENANDUONG_TT','TIENCTLH_TT','GTKHAC_TT','THANG_TCXN','THANG_TCVIECLAM');
	---- 
	TRUNCATE TABLE TL_CanBo_PhuCap;
	----
	--ALTER INDEX IX_TL_CanBo_PhuCap_MA_CBO ON TL_CanBo_PhuCap DISABLE;
	--ALTER INDEX IDX_TL_CanBo_PhuCap ON TL_CanBo_PhuCap DISABLE;
	----
	INSERT INTO TL_CanBo_PhuCap
	(
		Id,
		bSaoChep,
		CHON,
		CONG_THUC,
		DateEnd,
		DateStart,
		flag,
		GIA_TRI,
		HE_SO,
		HuongPC_SN,
		ISoThang_Huong,
		MA_CBO,
		MA_KMCP,
		MA_PHUCAP,
		PHANTRAM_CT,
		bCapNhat
	)
	SELECT
		Id,
		bSaoChep,
		CHON,
		CONG_THUC,
		DateEnd,
		DateStart,
		flag,
		GIA_TRI,
		HE_SO,
		HuongPC_SN,
		ISoThang_Huong,
		MA_CBO,
		MA_KMCP,
		MA_PHUCAP,
		PHANTRAM_CT,
		bCapNhat
	FROM TL_CanBo_PhuCap_BK
	----
	--ALTER INDEX IX_TL_CanBo_PhuCap_MA_CBO ON TL_CanBo_PhuCap REBUILD;
	--ALTER INDEX IDX_TL_CanBo_PhuCap ON TL_CanBo_PhuCap REBUILD;

	-- 2. Bảng TL_BangLuong_Thang
	---- 
	SELECT * INTO TL_BangLuong_Thang_BK 
	FROM TL_BangLuong_Thang
	WHERE ISNULL(GIA_TRI, 0) <> 0;
	---- 
	TRUNCATE TABLE TL_BangLuong_Thang;
	----
	--ALTER INDEX IDX_TL_BangLuong_Thang ON TL_BangLuong_Thang DISABLE;
	--ALTER INDEX idx_bangluong ON TL_BangLuong_Thang DISABLE;
	----
	INSERT INTO TL_BangLuong_Thang
	(
		Id,
		Gia_Tri,
		Loai_BL,
		Ma_CachTL,
		Ma_CB,
		Ma_CBo,
		Ma_DonVi,
		Ma_Hieu_CanBo,
		Ma_PhuCap,
		NAM,
		Ngay_HT,
		parent,
		So_TT,
		Ten_CachTL,
		Ten_Cbo,
		THANG,
		User_Name,
		HuongPC_SN
	)
	SELECT
		Id,
		Gia_Tri,
		Loai_BL,
		Ma_CachTL,
		Ma_CB,
		Ma_CBo,
		Ma_DonVi,
		Ma_Hieu_CanBo,
		Ma_PhuCap,
		NAM,
		Ngay_HT,
		parent,
		So_TT,
		Ten_CachTL,
		Ten_Cbo,
		THANG,
		User_Name,
		HuongPC_SN
	FROM TL_BangLuong_Thang_BK
	----
	--ALTER INDEX IDX_TL_BangLuong_Thang ON TL_BangLuong_Thang REBUILD;
	--ALTER INDEX idx_bangluong ON TL_BangLuong_Thang REBUILD;

	-- Luong ke hoach --
	-- 3. Bảng TL_CanBo_PhuCap
	----
	SELECT * INTO TL_CanBo_PhuCap_KeHoach_BK 
	FROM TL_CanBo_PhuCap_KeHoach
	WHERE ISNULL(GIA_TRI, 0) <> 0
	OR MA_PHUCAP IN ('THUONG_TT','THUEDANOP_TT','GIAMTHUE_TT','THUNHAPKHAC_TT','TIENTAUXE_TT','TIENANDUONG_TT','TIENCTLH_TT','GTKHAC_TT','THANG_TCXN','THANG_TCVIECLAM');
	---- 
	TRUNCATE TABLE TL_CanBo_PhuCap_KeHoach;

	INSERT INTO TL_CanBo_PhuCap_KeHoach
	(
		Id,
		DateEnd,
		DateStart,
		Gia_Tri,
		HuongPC_SN,
		ISoThang_Huong,
		Ma_CanBo,
		Ma_PhuCap
	)
	SELECT
		Id,
		DateEnd,
		DateStart,
		Gia_Tri,
		HuongPC_SN,
		ISoThang_Huong,
		Ma_CanBo,
		Ma_PhuCap
	FROM TL_CanBo_PhuCap_KeHoach_BK

	-- 4. Bảng TL_BangLuong_KeHoach
	---- 
	SELECT * INTO TL_BangLuong_KeHoach_BK 
	FROM TL_BangLuong_KeHoach
	WHERE ISNULL(GIA_TRI, 0) <> 0;
	---- 
	TRUNCATE TABLE TL_BangLuong_KeHoach;

	INSERT INTO TL_BangLuong_KeHoach
	(
		Id,
		Gia_Tri,
		Ma_CachTL,
		Ma_CanBo,
		Ma_CB,
		Ma_DonVi,
		Ma_Hieu_CanBo,
		Ma_PhuCap,
		Nam,
		parent,
		Ten_CanBo,
		Thang
	)
	SELECT
		Id,
		Gia_Tri,
		Ma_CachTL,
		Ma_CanBo,
		Ma_CB,
		Ma_DonVi,
		Ma_Hieu_CanBo,
		Ma_PhuCap,
		Nam,
		parent,
		Ten_CanBo,
		Thang
	FROM TL_BangLuong_KeHoach_BK

	-- Xóa bảng backup
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_CanBo_PhuCap_BK]') AND type in (N'U')) drop table TL_CanBo_PhuCap_BK;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_BangLuong_Thang_BK]') AND type in (N'U')) drop table TL_BangLuong_Thang_BK;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_CanBo_PhuCap_KeHoach_BK]') AND type in (N'U')) drop table TL_CanBo_PhuCap_KeHoach_BK;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TL_BangLuong_KeHoach_BK]') AND type in (N'U')) drop table TL_BangLuong_KeHoach_BK;

	--Thu gọn data file
	DECLARE @DBName NVARCHAR(MAX);
	DECLARE @DBFileName NVARCHAR(MAX);
	DECLARE @sqlSHRINKFILE NVARCHAR(MAX)

	SET @DBName = DB_NAME();
	SET @DBFileName = (SELECT TOP 1 NAME from sys.master_files
	WHERE database_id = DB_ID(@DBName)
	AND type = 0);

	BEGIN
		SET @sqlSHRINKFILE = 'DBCC SHRINKFILE (' + @DBFileName + ')';
		EXEC sp_executesql @sqlSHRINKFILE;
	END

	--Rebuild indexs
	DECLARE @tableName NVARCHAR(256)
	DECLARE @sql NVARCHAR(MAX)

	DECLARE tableCursor CURSOR FOR
	SELECT DISTINCT QUOTENAME(SCHEMA_NAME(t.schema_id)) + '.' + QUOTENAME(t.name)
	FROM sys.tables AS t
		INNER JOIN sys.indexes AS i ON t.object_id = i.object_id

	OPEN tableCursor
	FETCH NEXT FROM tableCursor INTO @tableName

	WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @sql = 'ALTER INDEX ALL ON ' + @tableName + ' REBUILD'
		EXEC sp_executesql @sql
		FETCH NEXT FROM tableCursor INTO @tableName
	END

	CLOSE tableCursor
	DEALLOCATE tableCursor

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]    Script Date: 7/10/2024 4:33:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]
	@MaCanBo NVARCHAR(500)
AS
BEGIN
	
	select Ma_CanBo, gia_tri, HuongPC_SN, ma_phucap
		into #canBoPhuCap_tmp1
		from TL_CanBo_PhuCap_KeHoach where Ma_CanBo = @MaCanBo

	select temp.Ma_CanBo MaCanBo, 
		temp.gia_tri GiaTri, 
		temp.HuongPC_SN HuongPcSn, 
		temp.ma_phucap MaPhuCap
			from (
				select Ma_CanBo, gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
				union
				select cbpc.Ma_CanBo, pc.gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
				from
				(select distinct Ma_CanBo from #canBoPhuCap_tmp1) cbpc
				cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP = 'BHCN') pc
	) temp

END
GO


/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_bang_luong]    Script Date: 7/11/2024 9:15:47 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_can_bo_phu_cap_bang_luong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_can_bo_phu_cap_bang_luong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_bang_luong]    Script Date: 7/11/2024 9:15:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_get_can_bo_phu_cap_bang_luong]
	@MaCanBo NVARCHAR(500)
AS
BEGIN
	
	select Id, MA_CBO, gia_tri, HuongPC_SN, ma_phucap, bCapNhat
		into #canBoPhuCap_tmp1
		from TL_CanBo_PhuCap where MA_CBO = @MaCanBo

	select temp.Id,
		temp.MA_CBO MaCbo, 
		temp.gia_tri GiaTri, 
		temp.HuongPC_SN HuongPcSn, 
		temp.ma_phucap MaPhuCap,
		temp.bCapNhat
			from (
				select Id, MA_CBO, gia_tri, HuongPC_SN, ma_phucap, bCapNhat from #canBoPhuCap_tmp1
				union
				select null, cbpc.MA_CBO, pc.gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP, cbpc.bCapNhat
				from
				(select distinct MA_CBO, bCapNhat from #canBoPhuCap_tmp1) cbpc
				cross join (select * from TL_DM_PhuCap where Ma_PhuCap not in (select Ma_PhuCap from #canBoPhuCap_tmp1)
				--where Is_Formula = 1 or MA_PHUCAP = 'BHCN'
				) pc
	) temp
	
END
GO


/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 7/12/2024 10:04:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 7/12/2024 10:04:26 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangke_trichthue_tncn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangke_trichthue_tncn]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangke_trichthue_tncn]    Script Date: 7/12/2024 10:04:26 AM ******/
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
	(ISNULL(LHT_TT, 0) + ISNULL(PCCT_TT, 0) + ISNULL(THUONG_TT, 0) + ISNULL(THUNHAPKHAC_TT, 0) - ISNULL(GTPT_TT, 0) - ISNULL(BHCN_TT, 0) - ISNULL(PHAITRUKHAC_SUM, 0)) TINHTHUE,
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
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_quyettoan_nam_thue_tncn]    Script Date: 7/12/2024 10:04:26 AM ******/
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
	   (isnull(LHT_TT, 0) + isnull(THUONG_TT, 0) + isnull(PCCT_TT, 0) + isnull(THUNHAPKHAC_TT, 0) - isnull(BHCN_TT, 0) - isnull(GTNN, 0) - isnull(GTPT_DG_SN, 0) - isnull(PHAITRUKHAC_SUM, 0)) TINHTHUE
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
