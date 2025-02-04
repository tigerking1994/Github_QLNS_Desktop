/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 5/22/2024 8:25:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 5/22/2024 8:25:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 5/22/2024 8:25:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_KHT_BHXHChiTiet,
				bhct.iID_KHT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sTenLoaiDoiTuong,
				bhct.iQSBQNam,
				bhct.fLuongChinh,
				bhct.fPhuCapChucVu,
				bhct.fPCTNNghe,
				bhct.fPCTNVuotKhung,
				bhct.fNghiOm,
				bhct.fHSBL,
				(isnull(bhct.fLuongChinh, 0) + isnull(bhct.fPhuCapChucVu, 0) + isnull(bhct.fPCTNNghe, 0) + isnull(bhct.fPCTNVuotKhung, 0) + isnull(bhct.fNghiOm, 0) + isnull(bhct.fHSBL, 0)) FTongQuyTienLuongNam,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				(isnull(bhct.fThu_BHXH_NLD, 0) + isnull(bhct.fThu_BHXH_NSD, 0)) fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				(isnull(bhct.fThu_BHYT_NLD, 0) + isnull(bhct.fThu_BHYT_NSD, 0)) fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
				(isnull(bhct.fThu_BHTN_NLD, 0) + isnull(bhct.fThu_BHTN_NSD, 0)) fTongThuBHTN,
				(isnull(bhct.fThu_BHXH_NLD, 0) + isnull(bhct.fThu_BHXH_NSD, 0) + isnull(bhct.fThu_BHYT_NLD, 0) + isnull(bhct.fThu_BHYT_NSD, 0) + isnull(bhct.fThu_BHTN_NLD, 0) + isnull(bhct.fThu_BHTN_NSD, 0)) fTongCong,
				bhct.iID_MucLucNganSach,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.iNamLamViec,
				bhct.sLNS,
				bhct.sXauNoiMa
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]    Script Date: 5/22/2024 8:25:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_chungtu_chitiet_bhxh_cach3]
	@maDonVi VARCHAR(MAX),
	@thang INT,
	@nam INT,
	@maCachTl NVARCHAR(50)
AS
BEGIN
	DECLARE @thangTruoc INT;
	DECLARE @namTruoc INT;

	IF @thang = 1
		SET @thangTruoc = 12;
		SET @namTruoc = @nam - 1;
	IF @thang <> 1
		SET @thangTruoc = @thang - 1;
		SET @namTruoc = @nam;

	DECLARE @checkTongHop INT; 
	SELECT @checkTongHop = count(*) FROM f_split(@maCachTl);

	SELECT Ma_Cot, pc.Ma_PhuCap into #tmp from (
		SELECT Ma_Cot, CONCAT('|', REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(CAST(CongThuc as nvarchar(max)),'+','|'),'-','|'),'*','|'),'/','|'),'(','|'),')','|'), '|') as CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE CongThuc IS NOT NULL) as tbl
		INNER  JOIN TL_DM_PhuCap as pc on tbl.CongThuc LIKE CONCAT('%|', pc.Ma_PhuCap, '|%') or pc.Parent in ('TIENAN', 'TIENAN2')

	select * into #tmpMapping
	FROM (
		select Ma_Cot, Ma_Cot as Ma_PhuCap from #tmp
		union 
		select Ma_PhuCap as Ma_Cot, Ma_PhuCap as Ma_PhuCap from #tmp
	) AS c

	SELECT mp.Ma_Cot, cb.Ma_CanBo, SUM(ISNULL(pc.fSoNgayHuongBHXH, 0)) as HuongPC_SN INTO #tmpSoNgay
	FROM TL_DM_CanBo as cb
	INNER JOIN TL_CanBo_CheDoBHXH as pc on cb.Ma_CanBo = pc.sMaCanBo
	INNER JOIN #tmpMapping as mp on pc.sMaCheDo = mp.Ma_PhuCap
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
		case when bangLuong.Ma_CB in ('3.1', '3.2', '3.3', '3.4', '3.5', '413', '415') then bangLuong.Ma_CB else capBac.Parent end AS Ngach,
		SUM(ISNULL(cbpc.HuongPC_SN, 0)) AS SoNgay,
		SUM(bangLuong.Gia_Tri) AS GiaTri,
		SUM(CASE WHEN @checkTongHop = 2 AND dsCapNhapBangLuong.Ma_CachTL ='CACH5' THEN 0 ELSE 1 END) AS SoNguoi
	INTO #LuongCapBac
	FROM (SELECT * FROM TL_BangLuong_Thang WHERE NAM = @namTruoc AND THANG = @thangTruoc AND Gia_Tri != 0 AND Ma_PhuCap in ('LHT_TT', 'PCCV_TT', 'PCTN_TT', 'PCTNVK_TT', 'HSBL_TT')) bangLuong
	INNER JOIN (select * from TL_CanBo_CheDoBHXH where inam = @nam and  ithang = @thang and sMaCheDo in ('TAINANLD_DUONGSUCPHSK', 'THAISAN_DUONGSUCPHSK', 'OMDAU_DUONGSUCPHSK', 'OMKHAC_T14NGAY', 'BENHDAINGAY_T14NGAY','CONOM', 'KHAMTHAI', 'KHHGD', 'NAMNGHIKHIVOSINHCON')) as cbpc1 on bangLuong.Ma_Hieu_CanBo = SUBSTRING(cbpc1.sMaCanBo, 7, 2)
	LEFT JOIN #tmpSoNgay as cbpc on bangLuong.Ma_PhuCap = cbpc.Ma_Cot AND bangLuong.Ma_CBo = cbpc.Ma_CanBo
	JOIN 
	(SELECT * FROM TL_DS_CapNhap_BangLuong WHERE Status = 1 AND NAM = @namTruoc AND THANG = @thangTruoc AND Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))) dsCapNhapBangLuong 
	ON bangLuong.parent = dsCapNhapBangLuong.Id AND bangLuong.THANG = dsCapNhapBangLuong.Thang and bangLuong.NAM = dsCapNhapBangLuong.Nam 
	LEFT JOIN TL_DM_CapBac capBac ON bangLuong.Ma_CB = capBac.Ma_Cb		
	GROUP BY dsCapNhapBangLuong.Ma_CBo, dsCapNhapBangLuong.Thang, dsCapNhapBangLuong.Nam, bangLuong.MA_PHUCAP,bangLuong.Ma_CB, capBac.Parent
		
	SELECT distinct
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
		luong.XauNoiMa					AS XauNoiMa, 
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
		AND luong.Thang = @thangTruoc
	GROUP BY MaDonVi, iID_MLNS, iID_MLNS_Cha, luong.XauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec, sChiTietToi, bHangCha, DuToan, luong.Ma_Cb, luong.Thang
	ORDER BY MaDonVi, luong.XauNoiMa

	DROP TABLE #LuongCapBac
	DROP TABLE #DataDuToan
	DROP TABLE #LuongCapBacMlns
	DROP TABLE #tmpSoNgay
	DROP TABLE #tmpMapping
END
;
GO
