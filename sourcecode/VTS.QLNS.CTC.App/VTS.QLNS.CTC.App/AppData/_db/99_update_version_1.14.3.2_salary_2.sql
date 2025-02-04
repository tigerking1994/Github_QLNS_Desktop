/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/12/2024 8:34:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 4/12/2024 8:34:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	
	--SELECT Ma_Cot, pc.Ma_PhuCap INTO #tmpMapping
	--FROM 
	--(SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
	--INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') ;
	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);


	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan_NQ104 WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap_NQ104 as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.ngay_huong_phu_cap, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo_NQ104 as cb
	INNER JOIN TL_CanBo_PhuCap_Bridge_NQ104 as pc on cb.Ma_CanBo = pc.ma_can_bo
	INNER JOIN #tmpMapping as mp on pc.ma_phu_cap = mp.Ma_PhuCap
	WHERE cb.Parent in (SELECT * FROM f_split(@MaDonVi))
	AND Thang = @thang
	AND Nam = @nam
	GROUP BY mp.Ma_Cot, cb.Ma_CanBo;

	SELECT 
			dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
			dsCapNhapBangLuong.Thang	AS Thang,
			dsCapNhapBangLuong.Nam		AS Nam,
			bangLuong.ma_phu_cap			AS MaPhuCap,
			bangLuong.Ma_CB				AS MaCapBac,
			case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
			SUM( 
			case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc2.ngay_huong_phu_cap, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
			WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
			) AS SoNgay,

			SUM(
				case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc2.ngay_huong_phu_cap, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc2.ngay_huong_phu_cap, 0) * bangLuong.GIA_TRI
				WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * bangLuong.GIA_TRI
				ELSE bangLuong.Gia_Tri END
			) AS GiaTri,
			SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
			INTO #LuongCapBac
			from  (SELECT bangLuongBridge.*,luongThang.Ma_CB FROM TL_BangLuong_Thang_Bridge_NQ104 bangLuongBridge
					INNER JOIN TL_BangLuong_Thang_NQ104 luongThang on bangLuongBridge.ma_can_bo=luongThang.Ma_CBo and bangLuongBridge.parent=luongThang.parent
					WHERE bangLuongBridge.Gia_Tri != 0 ) bangLuong
			INNER JOIN TL_CanBo_PhuCap_Bridge_NQ104 as cbpc2 on bangLuong.ma_can_bo = cbpc2.ma_can_bo  and bangLuong.ma_phu_cap=cbpc2.ma_phu_cap
			INNER JOIN TL_DM_PhuCap_NQ104 as pc on bangLuong.ma_phu_cap = pc.Ma_PhuCap
			LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.ma_phu_cap = cbpc.Ma_Cot and  bangLuong.ma_can_bo=cbpc.Ma_CanBo
			LEFT JOIN 
			(SELECT * FROM TL_DS_CapNhap_BangLuong_NQ104 
			WHERE Status = 1 AND NAM = @nam AND THANG = @thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
			ON bangLuong.parent = dsCapNhapBangLuong.Id 
			LEFT JOIN TL_DM_CapBac_NQ104 capBac ON bangLuong.Ma_CB = capBac.ma_cb	
			GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang
			, dsCapNhapBangLuong.Nam
			, bangLuong.ma_phu_cap
			,bangLuong.Ma_CB, capBac.Parent
	--SELECT
	--	dsCapNhapBangLuong.Ma_CBo	AS MaDonVi,
	--	dsCapNhapBangLuong.Thang	AS Thang,
	--	dsCapNhapBangLuong.Nam		AS Nam,
	--	bangLuong.MA_PHUCAP			AS MaPhuCap,
	--	bangLuong.Ma_CB				AS MaCapBac,
	--	case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
	--	--SUM(case when ISNULL(cbpc.HuongPC_SN, 0) = 0 then dbo.fnTotalDayOfMonth(@thang,@nam) else cbpc.HuongPC_SN end) AS SoNgay,
	--	SUM( 
	--		case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 then cbpc.HuongPC_SN
	--		WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) else ISNULL(cbpc.HuongPC_SN, 0) end
	--	) AS SoNgay,
	--	SUM(
	--		case WHEN pc.Parent IN ('TIENAN', 'TIENAN2') AND ISNULL(cbpc1.HuongPC_SN, 0) <> 0 AND bHuongPc_Sn = 1 THEN ISNULL(cbpc1.HuongPC_SN, 0) * cbpc1.GIA_TRI
	--		WHEN pc.Parent IN ('TIENAN', 'TIENAN2') THEN dbo.fnTotalDayOfMonth(@thang,@nam) * cbpc1.GIA_TRI
	--		ELSE bangLuong.Gia_Tri END
	--	) AS GiaTri,
	--	SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END)	AS SoNguoi
	--INTO #LuongCapBac
	--FROM (SELECT * FROM TL_BangLuong_Thang_NQ104 WHERE NAM = @nam AND THANG = @thang AND Gia_Tri != 0) bangLuong
	--INNER JOIN TL_CanBo_PhuCap_NQ104 as cbpc1 on bangLuong.Ma_CBo = cbpc1.MA_CBO AND bangLuong.Ma_PhuCap = cbpc1.MA_PHUCAP
	--INNER JOIN TL_DM_PhuCap_NQ104 as pc on bangLuong.Ma_PhuCap = pc.Ma_PhuCap
	--LEFT JOIN #tmpSoNgay  as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	--LEFT JOIN 
	--(SELECT * FROM TL_DS_CapNhap_BangLuong_NQ104 WHERE Status = 1 AND NAM = @Nam AND THANG = @Thang AND Ma_CachTL IN (SELECT * FROM f_split(@maCachTl)) AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	--ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	--LEFT JOIN TL_DM_CapBac_NQ104 capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	--GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT
		luongCapBac.MaDonVi,
		phuCapMlns.XauNoiMa,
		phuCapMlns.Ma_Cb,
		SoNguoi,
		SoNgay,
		GiaTri,
		luongCapBac.Thang
		INTO #LuongCapBacMlns
	FROM TL_PhuCap_MLNS_NQ104 phuCapMlns
	JOIN #LuongCapBac luongCapBac ON phuCapMlns.Ma_Cb = luongCapBac.Ngach AND phuCapMlns.Ma_PhuCap = luongCapBac.MaPhuCap
	WHERE
		phuCapMlns.Nam = @nam

	Select Ma_DonVi, XauNoiMa, SUM(DDuToan) DuToan
	INTO #DataDuToan
	From [TL_QT_ChungTuChiTiet_NQ104] ctchitiet
	Join TL_QT_ChungTu_NQ104 chungtu
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
