/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_canbo_truy_thu]    Script Date: 9/20/2024 1:37:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_canbo_truy_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_canbo_truy_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]    Script Date: 9/20/2024 1:37:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 9/20/2024 1:37:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_truylinh_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 9/20/2024 1:37:20 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_insert]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_insert]    Script Date: 9/20/2024 1:37:20 PM ******/
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

	select distinct temp.MA_CBO, temp.gia_tri, temp.HuongPC_SN, temp.ma_phucap into #canBoPhuCap
	from (
		select MA_CBO, isnull(gia_tri, 0) gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
		union
		select cbpc.MA_CBO, isnull(pc.gia_tri, 0) gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
		from
		(select distinct MA_CBO from #canBoPhuCap_tmp1) cbpc
		cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP in ('BHCN', 'THANHTIEN', 'TM')
		) pc where not exists (select * from #canBoPhuCap_tmp1 t1 where isnull(t1.gia_tri, 0) <> 0 and pc.MA_PHUCAP = t1.MA_PHUCAP and cbpc.MA_CBO = t1.MA_CBO)
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
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_truylinh_dulieu_insert]    Script Date: 9/20/2024 1:37:20 PM ******/
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

	select distinct temp.MA_CBO, temp.gia_tri, temp.HuongPC_SN, temp.ma_phucap into #canBoPhuCap
	from (
		select MA_CBO, isnull(gia_tri, 0) gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
		union
		select cbpc.MA_CBO, isnull(pc.gia_tri, 0) gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
		from
		(select distinct MA_CBO from #canBoPhuCap_tmp1) cbpc
		cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP in ('BHCN', 'THANHTIEN', 'TM')
		) pc where not exists (select * from #canBoPhuCap_tmp1 t1 where isnull(t1.gia_tri, 0) <> 0 and pc.MA_PHUCAP = t1.MA_PHUCAP and cbpc.MA_CBO = t1.MA_CBO)
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
/****** Object:  StoredProcedure [dbo].[sp_tl_get_can_bo_phu_cap_ke_hoach]    Script Date: 9/20/2024 1:37:20 PM ******/
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

	select distinct temp.Ma_CanBo MaCanBo, 
		temp.gia_tri GiaTri, 
		temp.HuongPC_SN HuongPcSn, 
		temp.ma_phucap MaPhuCap
			from (
				select Ma_CanBo, isnull(gia_tri, 0) gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
				union
				select cbpc.Ma_CanBo, isnull(pc.gia_tri, 0) gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
				from
				(select distinct Ma_CanBo from #canBoPhuCap_tmp1) cbpc
				cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP = 'BHCN'
				) pc where not exists (select * from #canBoPhuCap_tmp1 t1 where isnull(t1.gia_tri, 0) <> 0 and pc.MA_PHUCAP = t1.MA_PHUCAP and cbpc.Ma_CanBo = t1.Ma_CanBo)
	) temp

END
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_canbo_truy_thu]    Script Date: 9/20/2024 1:37:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_get_data_canbo_truy_thu] 
	-- Add the parameters for the stored procedure here
	@Thang int ,
	@Nam int ,
	@MaDonVi nvarchar(max) ,
	@iIdBangLuong uniqueidentifier
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temptruythuresult]') AND type in (N'U')) drop table temptruythuresult;
	
	select MA_CBO, gia_tri, HuongPC_SN, ma_phucap
	into #canBoPhuCap_tmp1
	from TL_CanBo_PhuCap where MA_CBO like CONCAT(FORMAT(@Nam, 'D2'), FORMAT(@Thang, 'D2'), '%')

	select distinct --temp.MA_CBO, temp.gia_tri, temp.HuongPC_SN, 
		temp.ma_phucap into #canBoPhuCap
	from (
		select MA_CBO, isnull(gia_tri, 0) gia_tri, HuongPC_SN, ma_phucap from #canBoPhuCap_tmp1
		union
		select cbpc.MA_CBO, isnull(pc.gia_tri, 0) gia_tri, pc.HuongPC_SN, pc.MA_PHUCAP
		from
		(select distinct MA_CBO from #canBoPhuCap_tmp1) cbpc
		cross join (select * from TL_DM_PhuCap where Is_Formula = 1 or MA_PHUCAP in ('BHCN', 'THANHTIEN', 'TM', 'TA_THANG')
		) pc where not exists (select * from #canBoPhuCap_tmp1 t1 where isnull(t1.gia_tri, 0) <> 0 and pc.MA_PHUCAP = t1.MA_PHUCAP and cbpc.MA_CBO = t1.MA_CBO)
		) temp

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	CREATE TABLE #tempDataTruyThu (MaHieuCanBo nvarchar(500), MaCanBo nvarchar(500),SoNgayTruyThu int, MaDonVi Nvarchar(50))


	--Lấy từ bảo hiểm
	select cb.Ma_Hieu_CanBo, cb.Ma_CanBo, sum(cb_bhxh.fSoNgayHuongBHXH) as SoNgayTruyThu,  cb.Parent as MaDonVi INTO #tmpBaoHiem
		from TL_DM_CanBo as cb
		inner join TL_CanBo_CheDoBHXH as cb_bhxh on cb.Ma_CanBo = cb_bhxh.sMaCanBo
		inner join TL_DM_CheDoBHXH as pc on pc.sMaCheDo = cb_bhxh.sMaCheDo
		where cb.Thang = @Thang and cb.Nam = @Nam and 
		pc.bDisplay =1 AND cb.Parent IN (SELECT * FROM f_split(@MaDonVi))
		group by  cb.Ma_Hieu_CanBo,cb.Ma_CanBo, cb.Parent;


	IF  EXISTS ( SELECT 1 FROM  #tmpBaoHiem)
	BEGIN
		INSERT INTO #tempDataTruyThu
		SELECT * FROM #tmpBaoHiem
	END


	--Kiem tra  có truy thu theo cán bộ
	 SELECT cb.Ma_Hieu_CanBo, cb.Ma_CanBo,cbpc.GIA_TRI as SoNgayTruyThu, cb.Parent as MaDonVi INTO #tmpCanBo
	 FROM TL_DM_CanBo cb
	 INNER JOIN TL_CanBo_PhuCap  cbpc  ON cb.Ma_CanBo = cbpc.MA_CBO
	 WHERE  cb.Thang = @Thang AND cb.Nam =@Nam  and MA_PHUCAP = 'TRUYTHU_SN'AND cb.Parent IN (SELECT * FROM f_split(@MaDonVi)) AND cbpc.GIA_TRI > 0;


	IF  EXISTS ( SELECT 1 FROM  #tempDataTruyThu)
	BEGIN
			IF  EXISTS ( SELECT 1 FROM  #tmpCanBo)
			BEGIN
				INSERT INTO #tempDataTruyThu
				SELECT * FROM #tmpCanBo cb
				WHERE cb.Ma_Hieu_CanBo NOT IN (SELECT Ma_Hieu_CanBo FROM #tmpBaoHiem)
			END
	END
	ELSE
	BEGIN
			IF  EXISTS ( SELECT 1 FROM  #tmpCanBo)
			BEGIN
				INSERT INTO #tempDataTruyThu
				SELECT * FROM #tmpCanBo cb
			END
	END
	-----SELECT OUT
	IF(@Thang = 1)
	BEGIN
			SELECT 
				 NEWID() as Id
				,luongThang.Gia_Tri AS GiaTri
				,luongThang.Loai_BL AS LoaiBl
				,'CACH1' AS MaCachTl
				,luongThang.Ma_CB AS MaCb
				,truythu.MaCanBo AS MaCbo
				,luongThang.Ma_DonVi AS MaDonVi
				,luongThang.Ma_Hieu_CanBo AS MaHieuCanBo
				,luongThang.Ma_PhuCap AS MaPhuCap
				,@Nam AS Nam
				,luongThang.Ngay_HT AS NgayHt
				,@iIdBangLuong AS Parent
				,luongThang.So_TT AS SoTt
				,luongThang.Ten_CachTL AS TenCachTl
				,luongThang.Ten_Cbo AS TenCbo
				,@Thang AS Thang
				,luongThang.User_Name AS UserName
				,luongThang.HuongPC_SN  AS HuongPC_SN
				,CAST(truythu.SoNgayTruyThu as numeric(15,4))  as SoNgayTruyThu,
				CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
				cachTinhLuong.CongThuc AS CongThuc
			INTO temptruythuresult
			FROM TL_BangLuong_Thang luongthang
			INNER JOIN #tempDataTruyThu truythu ON luongthang.Ma_Hieu_CanBo = truythu.MaHieuCanBo
			LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
				ON cachTinhLuong.Ma_Cot = luongthang.MA_PHUCAP
			WHERE  luongthang.THANG = 12 AND luongthang.NAM = @Nam - 1 AND luongthang.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	END
	ELSE
	BEGIN
			SELECT 
				 NEWID() as Id
				,luongThang.Gia_Tri AS GiaTri
				,luongThang.Loai_BL AS LoaiBl
				,'CACH1' AS MaCachTl
				,luongThang.Ma_CB AS MaCb
				,truythu.MaCanBo AS MaCbo
				,luongThang.Ma_DonVi AS MaDonVi
				,luongThang.Ma_Hieu_CanBo AS MaHieuCanBo
				,luongThang.Ma_PhuCap AS MaPhuCap
				,@Nam AS Nam
				,luongThang.Ngay_HT AS NgayHt
				,@iIdBangLuong AS Parent
				,luongThang.So_TT AS SoTt
				,luongThang.Ten_CachTL AS TenCachTl
				,luongThang.Ten_Cbo AS TenCbo
				,@Thang AS Thang
				,luongThang.User_Name AS UserName
				,luongThang.HuongPC_SN  AS HuongPC_SN
				,CAST(truythu.SoNgayTruyThu as numeric(15,4))  as SoNgayTruyThu,
				CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated,
				cachTinhLuong.CongThuc AS CongThuc
			INTO temptruythuresult
			FROM TL_BangLuong_Thang luongthang
			INNER JOIN #tempDataTruyThu truythu ON luongthang.Ma_Hieu_CanBo = truythu.MaHieuCanBo
			LEFT JOIN (select Id, Ma_Cot, CongThuc from TL_DM_Cach_TinhLuong_Chuan) cachTinhLuong
				ON cachTinhLuong.Ma_Cot = luongthang.MA_PHUCAP
			WHERE  luongthang.THANG = @Thang - 1 AND luongthang.NAM = @Nam AND luongthang.Ma_DonVi IN (SELECT * FROM f_split(@MaDonVi))
	END

	insert into temptruythuresult (Id, GiaTri, LoaiBl, MaCachTl, MaCb, MaCbo, MaDonVi, MaHieuCanBo, MaPhuCap, Nam, NgayHt, Parent, SoTt, TenCachTl, TenCbo, Thang, UserName, HuongPC_SN, SoNgayTruyThu, IsCalculated, CongThuc)
	
	select NEWID(), 0, null, 'CACH1', null, cr.MaCbo, @MaDonVi, cr.MaHieuCanBo, cr.MA_PHUCAP MaPhuCap, @Nam, null, @iIdBangLuong, null, null, null, @Thang, null, null, null, 1, null
	from
		(select distinct tmp.MaCbo, tmp.MaHieuCanBo, c.MA_PHUCAP
		from temptruythuresult tmp
		cross join (select * from #canBoPhuCap) c) cr
	where not exists (select * from temptruythuresult t where t.MaCbo = cr.MaCbo and t.MaPhuCap = cr.MA_PHUCAP)

	-----
	select * from temptruythuresult;

	DROP TABLE #tempDataTruyThu;
	DROP TABLE #tmpBaoHiem;
	DROP TABLE #tmpCanBo;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temptruythuresult]') AND type in (N'U')) drop table temptruythuresult;

END
;
GO

--Update theo yeu cau cua huent92, 23/9/2024
update TL_DM_Cach_TinhLuong_Chuan set CongThuc = N'LHT_TT+PCCT_TT-GTPT_TT-BHCN_TT-TRICHQUYPCTT_TT-GTKHAC_TT-TRICHQUYKHAC_TT'
where upper(Ma_Cot) = 'LUONGTHUE_TT'