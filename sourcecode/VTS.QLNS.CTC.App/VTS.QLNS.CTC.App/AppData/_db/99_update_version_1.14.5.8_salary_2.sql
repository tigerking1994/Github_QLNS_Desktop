/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_chungtu_chitiet_namkehoach_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_chungtu_chitiet_namkehoach_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 6/5/2024 1:40:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tl_get_data_ct_chi_tiet_theo_cot_nq104] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN
SELECT iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
	   Id_DonVi  AS IdDonVi,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
       Sum(DieuChinh) AS DieuChinh
FROM NS_MucLucNganSach mlns
LEFT JOIN TL_QT_ChungTuChiTiet_NQ104 ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
AND ctct.Id_ChungTu in (SELECT *
   FROM f_split(@idChungTu))
AND ctct.MaCachTl in
  (SELECT *
   FROM f_split(@maCachTl))
WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
GROUP BY iID_MLNS,
         iID_MLNS_Cha,
		 Id_DonVi,
         sXauNoiMa,
         sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
         sTNG1,
         sTNG2,
         sTNG3,
         sMoTa,
         sChiTietToi,
         mlns.bHangCha,
         iNamLamViec
ORDER BY sXauNoiMa END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_namkehoach_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_chungtu_chitiet_namkehoach_nq104]
@maDonVi varchar(50), @thang int, @nam int

As
select iID_MLNS as MlnsId, 
	   iID_MLNS_Cha as MlnsIdParent, 
	   sXauNoiMa as XauNoiMa, 
	   sLNS as Lns, 
	   sL as L, 
	   sK as K, 
	   sM as M, 
	   sTM as TM, 
	   sTTM as TTM, 
	   sNG as Ng, 
	   sTNG as TNG, 
	   sTNG1 as TNG1, 
	   sTNG2 as TNG2, 
	   sTNG3 as TNG3, 
	   sMoTa as Mota, 
	   iNamLamViec as NamLamViec,
	   bHangCha as BHangCha,
	   sChiTietToi as ChiTietToi,
	   Sum(Tong) as TongCong,
	   NULL AS DDuToan,
	   CAST(0 as int) as SoNgay
from NS_MucLucNganSach
left join
(select TL_PhuCap_MLNS_NQ104.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS_NQ104.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM, SUM(Gia_Tri) as Tong
from TL_PhuCap_MLNS_NQ104
join
(select Ma_PhuCap, Ma_DonVi, THANG, NAM, TL_BangLuong_KeHoach.Ma_CB, TL_DM_CapBac.Parent, Gia_Tri
from [dbo].[TL_BangLuong_KeHoach]
Join [dbo].[TL_DM_CapBac]
On TL_BangLuong_KeHoach.Ma_CB = TL_DM_CapBac.Ma_Cb) as Luong_CapBac
on TL_PhuCap_MLNS_NQ104.Ma_Cb = Luong_CapBac.Parent and TL_PhuCap_MLNS_NQ104.Ma_PhuCap = Luong_CapBac.Ma_PhuCap
Where Ma_DonVi = @maDonVi
And THANG = @thang
And Luong_CapBac.NAM = @nam
And TL_PhuCap_MLNS_NQ104.Nam = @nam
group by TL_PhuCap_MLNS_NQ104.Ma_PhuCap, XauNoiMa, MoTa, TL_PhuCap_MLNS_NQ104.Ma_Cb, Ma_DonVi, THANG, Luong_CapBac.NAM) as Luong_Capbac_Mlns
on NS_MucLucNganSach.sXauNoiMa = Luong_Capbac_Mlns.XauNoiMa
Where sLNS IN ('1', '101', '1010000')
And iNamLamViec = @nam
group by iID_MLNS, iID_MLNS_Cha, sXauNoiMa, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sTNG1, sTNG2, sTNG3, sMoTa, iNamLamViec,sChiTietToi, bHangCha
order by sXauNoiMa
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_chungtu_chitiet_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_ct_chi_tiet_export_nq104]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu_NQ104 as tbl on tmp.Item = tbl.ID;

	select DISTINCT XauNoiMa  into #tmpPcMlns FROM TL_PhuCap_MLNS_NQ104 WHERE Nam = @nam;

	with ctct as (
	  select XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 ISNULL(dt.DDuToan, 0) As DDuToan, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	  from  TL_QT_ChungTuChiTiet_NQ104 as dt 
	  left join TL_QT_ChungTu_NQ104 as tbl on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND (((dt.MaCachTl = '' or dt.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, ISNULL(dt.DDuToan, 0)
	),
	--lstSoNguoi as (
	--	SELECT XauNoiMa,
	--		SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay
	--	from TL_QT_ChungTu_NQ104 as tbl
	--	INNER JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tbl.Id = dt.Id_ChungTu
	--	where tbl.ID in (SELECT ID FROM #tblMaxThang)
	--	AND ((@isHaveCachTinhLuong = 0 AND (dt.MaCachTl = '' or dt.MaCachTl is null)) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	--	group by XauNoiMa
	--)
	--,
	phucapMlns as (
		SELECT TL_PhuCap_MLNS_NQ104.XauNoiMa, min(TL_PhuCap_MLNS_NQ104.Ma_Cb) as Ma_Cb FROM #tmpPcMlns 
		left join TL_PhuCap_MLNS_NQ104 on #tmpPcMlns.XauNoiMa = TL_PhuCap_MLNS_NQ104.XauNoiMa
		WHERE Nam = @nam
		group by TL_PhuCap_MLNS_NQ104.XauNoiMa
	),
	ctTongHop as (
		select DDuToan, sum(DieuChinh) DieuChinh, SUM(SoNguoi) AS SoNguoi,SUM(SoNgay) as SoNgay, XauNoiMa from TL_QT_ChungTuChiTiet_NQ104 ctct
		left join TL_QT_ChungTu_NQ104 ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa, DDuToan
	)
SELECT 
       iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
	   case when @isSummary = 1 then ctTongHop.SoNguoi when @isSummary = 0 then ctct.SoNguoi end as SoNguoi,
	   case when @isSummary = 1 then ctTongHop.SoNgay when @isSummary = 0 then ctct.SoNgay end as SoNgay,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DDuToan

FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
--LEFT JOIN lstSoNguoi as sn on mlns.sXauNoiMa = sn.XauNoiMa 
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
drop table #tmpPcMlns
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_ct_chi_tiet_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_get_data_ct_chi_tiet_nq104] @idChungTu nvarchar(MAX),
                                                       @nam int, @maCachTl nvarchar(50) AS BEGIN

with ctct as (
  select Id as Id, XauNoiMa,MaCachTl,  Sum(TongCong) AS TongCong,
       convert(decimal,Sum(SoNguoi)) as SoNguoi,
	   convert(decimal,Sum(SoNgay)) as SoNgay,
       Sum(DieuChinh) AS DieuChinh,
     Sum(DDuToan) As DDuToan,
	 MaCb
  from TL_QT_ChungTuChiTiet_NQ104 
  where   Id_ChungTu in (SELECT *  FROM f_split(@idChungTu))
    AND (MaCachTl in (SELECT *  FROM f_split(@maCachTl)) or (MaCachTl is null and @maCachTl = ''))
  group by id, XauNoiMa, MaCachTl, MaCb
)
SELECT 
     ctct.Id as Id,
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS Mota,
     MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
    SoNguoi,
       DieuChinh,
     DDuToan, 
	 SoNgay,
	 MaCb,
	 cb.Ten_Cb as LoaiDoiTuong
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
left join TL_DM_CapBac_NQ104 cb on ctct.MaCb = cb.Ma_Cb

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam

order by mlns.sXauNoiMa

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_tl_get_data_giaithich_bangso_export_nq104]
	@lstId nvarchar(max),
	@lstCach nvarchar(100),
	@nam int,
	@isSummary bit,
	@maDonViTongHop nvarchar(100)
AS
BEGIN
	CREATE TABLE #tmp(id nvarchar(100))
	DECLARE @isHaveCachTinhLuong bit = 0

	if(ISNULL(@lstCach, '') <> '')
	BEGIN
		INSERT INTO #tmp(id)
		SELECT *
		FROM f_split(@lstCach)

		SET @isHaveCachTinhLuong = 1
	END

	SELECT tbl.ID, Thang INTO #tblMaxThang
	FROM f_split(@lstId) as tmp
	INNER JOIN TL_QT_ChungTu_NQ104 as tbl on tmp.Item = tbl.ID;


	with ctct as (
	  select dt.XauNoiMa,  MaCachTl, MaCb,  Sum(TongCong) AS TongCong,
		   Sum(DieuChinh) AS DieuChinh,
		 Sum(ISNULL(dt.DDuToan, 0)) As DDuToan
	  from TL_QT_ChungTu_NQ104 as tbl
	  INNER JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tbl.Id = dt.Id_ChungTu
	  where  (ISNULL(@lstId, '') <> '' AND tbl.ID in (SELECT *  FROM f_split(@lstId)))
		AND ((dt.MaCachTl = '' AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND dt.MaCachTl in (SELECT id  FROM #tmp)))
	  group by dt.XauNoiMa, dt.MaCachTl, dt.MaCb
	),
	ctTongHop as (
		select Sum(DDuToan) as DDuToan, sum(DieuChinh) DieuChinh, XauNoiMa from TL_QT_ChungTuChiTiet_NQ104 ctct
		left join TL_QT_ChungTu_NQ104 ct on ctct.Id_ChungTu = ct.ID where CHARINDEX(ct.sTongHop, @lstId, 0) > 0 and NamLamViec = @nam and @maDonViTongHop = ct.Ma_DonVi 
		AND (((ctct.MaCachTl = '' or ctct.MaCachTl is null) AND @isHaveCachTinhLuong = 0) OR (@isHaveCachTinhLuong = 1 AND ctct.MaCachTl in (SELECT id  FROM #tmp)))
		group by XauNoiMa
	)

SELECT 
     iID_MLNS AS MlnsId,
       iID_MLNS_Cha AS MlnsIdParent,
       sXauNoiMa AS XauNoiMa,
       sLNS AS Lns,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS Ng,
       mlns.sTNG AS TNG,
       mlns.sTNG1 AS TNG1,
       mlns.sTNG2 AS TNG2,
       mlns.sTNG3 AS TNG3,
	   ctct.MaCb as MaCb,
       sMoTa AS Mota,
       MaCachTl as MaCachTl,
       iNamLamViec AS NamLamViec,
       mlns.bHangCha AS BHangCha,
       sChiTietToi AS ChiTietToi,
       TongCong,
       case when @isSummary = 1 then ctTongHop.DieuChinh when @isSummary = 0 then ctct.DieuChinh end as DieuChinh,
	   case when @isSummary = 1 then ctTongHop.DDuToan when @isSummary = 0 then  ctct.DDuToan end as DuToan,
	 ctct.MaCb as MaCb,
	 capbac.Parent as MaCbCha
FROM NS_MucLucNganSach mlns  
LEFT JOIN ctct ctct ON mlns.sXauNoiMa = ctct.XauNoiMa
LEFT JOIN TL_DM_CapBac capbac ON ctct.MaCb = capbac.Ma_Cb
LEFT JOIN ctTongHop on mlns.sXauNoiMa = ctTongHop.XauNoiMa

WHERE sLNS IN ('1',
               '101',
               '1010000')
  AND iNamLamViec = @nam
  and bHangCha = 0

order by XauNoiMa

DROP TABLE #tblMaxThang
DROP TABLE #tmp
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]    Script Date: 6/4/2024 3:55:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_quyettoan_chungtu_chitiet_tao_tonghop_nq104]
	@ListIdChungTuTongHop ntext,
	@IdChungTu nvarchar(100),
	@IdDonVi nvarchar(10),
	@TenDonVi nvarchar(250),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int
AS
BEGIN

DECLARE @iIdChungTuOld uniqueidentifier,
	@iIdChungTuDuToanOld uniqueidentifier

SELECT TOP(1) @iIdChungTuOld = ol.ID, @iIdChungTuDuToanOld = case ol.IIdChungTuDuToan when '' then NULL else REPLACE(ol.IIdChungTuDuToan, ';', '') end
FROM TL_QT_ChungTu_NQ104 as ol
INNER JOIN (SELECT * FROM TL_QT_ChungTu_NQ104 WHERE ID = @IdChungTu) as nw 
on ol.Thang = (CASE WHEN nw.Thang = 1 THEN 12 ELSE nw.Thang - 1 END)
	AND ol.Nam = (CASE WHEN nw.Thang = 1 THEN nw.Nam - 1 ELSE nw.Nam END)
	AND ol.Ma_DonVi = nw.Ma_DonVi
	AND ol.sTongHop IS NOT NULL
ORDER BY ol.Ngay_tao DESC

CREATE TABLE #tmp(sXauNoiMa nvarchar(100), DDuToan decimal)

IF(@iIdChungTuOld IS NOT NULL AND @iIdChungTuDuToanOld IS NOT NULL)
BEGIN
	UPDATE TL_QT_ChungTu_NQ104 SET IIdChungTuDuToan = @iIdChungTuDuToanOld WHERE ID = @IdChungTu

	INSERT INTO #tmp(sXauNoiMa, DDuToan)
	SELECT XauNoiMa, Sum(ISNULL(DDuToan, 0))
	FROM TL_QT_ChungTuChiTiet_NQ104 
	WHERE Id_ChungTu = @iIdChungTuOld AND ISNULL(DDuToan, 0) <> 0  AND (MaCachTl = '' or MaCachTl is null)
	group by XauNoiMa
END

--INSERT INTO [dbo].[TL_QT_ChungTu_NQ104ChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
INSERT INTO [dbo].TL_QT_ChungTuChiTiet_NQ104 ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
SELECT @idChungTu,
       MLNS_Id,
       MLNS_Id_Parent,
       XauNoiMa,
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       TNG1,
       TNG2,
       TNG3,
       MoTa,
       NULL,
       @NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       sum(Isnull(TongCong, 0)),
       BHangCha,
       NULL,
       sum(Isnull(SoNgay, 0)),
       sum(Isnull(SoNguoi, 0)),
       sum(Isnull(DieuChinh, 0)),
	   MaCachTl,
	   ISNULL(tmp.DDuToan, 0)
	   --,ct.MaCb
FROM TL_QT_ChungTuChiTiet_NQ104 ct
LEFT JOIN #tmp as tmp on ct.XauNoiMa = tmp.sXauNoiMa
WHERE ct.Id_ChungTu in
    (SELECT *
     FROM f_split(@ListIdChungTuTongHop))
GROUP BY MLNS_Id,
         MLNS_Id_Parent,
         XauNoiMa,
         LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         TNG1,
         TNG2,
         TNG3,
         MoTa,
         BHangCha,
		 MaCachTl,
		 tmp.sXauNoiMa,
		 ISNULL(tmp.DDuToan, 0)
		 --,ct.MaCb

	-- add du toan thang truoc da co
	--INSERT INTO [dbo].[TL_QT_ChungTu_NQ104ChiTiet] ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan], [MaCb])
	INSERT INTO [dbo].TL_QT_ChungTuChiTiet_NQ104 ([Id_ChungTu] , [MLNS_Id] , [MLNS_Id_Parent] , [XauNoiMa] , [LNS] , [L] , [K] , [M] , [TM] , [TTM] , [NG] , [TNG] , [TNG1] , [TNG2] , [TNG3] , [MoTa] , [Chuong] , [NamNganSach] , [NguonNganSach] , [NamLamViec] , [iTrangThai] , [iThangQuy] , [Id_DonVi] , [TenDonVi] , [MucAn] , [GhiChu] , [DateCreated] , [UserCreator] , [DateModified] , [UserModifier] , [TongCong] , [BHangCha] , [ChiTietToi] , [SoNgay] , [SoNguoi] , [DieuChinh] , [MaCachTl], [DDuToan])
	SELECT @idChungTu, 
		ml.iID_MLNS, 
		ml.iID_MLNS_Cha, 
		tmp.sXauNoiMa, 
		ml.sLNS,
		ml.sL, 
		ml.sK, 
		ml.sM, 
		ml.sTM, 
		ml.sTTM, 
		ml.sNG,
		ml.sTNG, 
		ml.sTNG1, 
		ml.sTNG2, 
		ml.sTNG3,
		ml.sMoTa,
		NULL, 
		@NamNganSach,
       @NguonNganSach,
       @NamLamViec,
       1,
       NULL,
       @IdDonVi,
       @TenDonVi,
       NULL,
       NULL,
       GETDATE(),
       NULL,
       NULL,
       NULL,
       Isnull(TongCong, 0),
       ml.bHangCha,
       NULL,
       Isnull(SoNgay, 0),
       Isnull(SoNguoi, 0),
       Isnull(DieuChinh, 0),
	   '',
	   ISNULL(tmp.DDuToan, 0)
	   --,dt.MaCb
	FROM #tmp as tmp
	INNER JOIN NS_MucLucNganSach as ml on ml.iNamLamViec = @NamLamViec AND ml.sXauNoiMa = tmp.sXauNoiMa
	LEFT JOIN TL_QT_ChungTuChiTiet_NQ104 as dt on tmp.sXauNoiMa = dt.XauNoiMa AND dt.Id_ChungTu = @idChungTu
	WHERE dt.XauNoiMa IS NULL AND ISNULL(tmp.DDuToan, 0) <> 0

	DROP TABLE #tmp
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]    Script Date: 6/5/2024 1:40:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tl_rpt_bangthanhtoan_luongthang_2_nq104]
@MaDonVi NVARCHAR(max),
@Thang int,
@Nam AS int,
@IsOrderChucVu AS bit = 0,
@IsGiaTriAm AS bit = 0,
@IsCheckedMaHuongLuong AS bit = 0,
@TyLeHuong AS float
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @Cols AS NVARCHAR(MAX)
DECLARE @Query AS NVARCHAR(MAX)

SET @Cols = 'NLCBHT_TT,NLCVCDHT_TT,LCVCDHT_TT,LCBHT_TT,LBLCBHT_TT,LBLCVCDHT_TT,PCCLKHAC_SUM,NTN,LHT_HS,PCTNVK_HS,HSBL_HS,LHT_TT,PCTNVK_TT,HSBL_TT,PCCV_TT,PCTN_TT,PCKV_TT,PCTRA_SUM,PCCOV_TT,PCDACTHU_SUM,PCKHAC_SUM,LUONGTHANG_SUM,BHCN_TT,THUETNCN_TT,TA_TT,GTKHAC_TT,TRICHLUONG_TT,PHAITRU_SUM,TM,THANHTIEN,BHXHCN_TT,BHYTCN_TT,BHTNCN_TT,PCTAUBP_TT,PCBVBG_TT,PCTEMTHU_TT,TA_TONG,PHAITRUKHAC_SUM,LUONGCOBAN_SUM,CACLOAIPC_SUM'
SET @Query =
'
WITH BangLuongThang AS (
SELECT Thang, Nam, MaCanBo, ' + @Cols + ' FROM (
SELECT
dsCapNhapBangLuong.Thang AS Thang,
dsCapNhapBangLuong.Nam AS Nam,
bangLuong.Ma_Can_Bo AS MaCanBo,
bangLuong.MA_PHU_CAP AS MaPhuCap,
bangLuong.GIA_TRI AS GiaTri
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
loaiNhom.ten_loai TenLoai,
loaiNhom.ten_nhom TenNhom,
capBacLuong.ten_dm CapBacLuong,
canBo.lan_nang_luong_cb LanNangLuongCb,
canBo.lan_nang_luong_cvd LanNangLuongCvd,
dbo.f_split_empty(canbo.Ten_CanBo) AS Ten,
canBo.Thang_TNN AS Tnn,
ISNULL(canBo.Ma_CB104, ''0'') AS MaCapBac,
ISNULL(capBac.Ten_Cb, ''0'') AS CapBac,
chucVu.Ma AS MaChucVu,
chucVu.Ten AS TenChucVu,
CASE WHEN 1 = ' + CAST(@IsCheckedMaHuongLuong AS VARCHAR(10)) + ' THEN CONCAT(canBo.So_TaiKhoan, '' '', canBo.Ma_CanBo) ELSE canBo.So_TaiKhoan END AS Stk,
ISNULL(FORMAT(canBo.Ngay_NN,''MM/yy''), '''') AS NgayNhapNgu,
ISNULL(FORMAT(canBo.Ngay_XN,''MM/yy''), '''') AS NgayXuatNgu,
ISNULL(FORMAT(canBo.Ngay_TN,''MM/yy''), '''') AS NgayTaiNgu,
capBac.Xau_Noi_Ma XauNoiMa
FROM TL_DM_CanBo_NQ104 canBo
INNER JOIN TL_DM_DonVi_NQ104 donVi
ON canBo.Parent=donVi.Ma_DonVi
LEFT JOIN TL_DM_CapBac_NQ104 capBac
ON canBo.Ma_CB104 = capBac.Ma_Cb
LEFT JOIN (SELECT
	con.ten_dm ten_loai,
	con.ma_dm loai,
	chau.ten_dm ten_nhom,
	chau.ma_dm nhom,
	cha.loai_doi_tuong loai_doi_tuong,
	chau.xau_noi_ma
FROM TL_DM_CapBac_Luong_NQ104 cha
LEFT JOIN TL_DM_CapBac_Luong_NQ104 con
ON cha.ma_dm = con.ma_dm_cha 
AND cha.loai_doi_tuong = con.loai_doi_tuong 
AND con.xau_noi_ma like cha.xau_noi_ma + ''-%''
AND con.nam = cha.nam
LEFT JOIN TL_DM_CapBac_Luong_NQ104 chau
ON con.ma_dm = chau.ma_dm_cha 
AND con.loai_doi_tuong = chau.loai_doi_tuong 
AND chau.xau_noi_ma like con.xau_noi_ma + ''-%''
AND chau.nam = con.nam
WHERE cha.loai = 0 AND con.loai = 1 AND chau.loai = 2) loaiNhom
ON loaiNhom.loai = canBo.loai AND loaiNhom.nhom = canBo.nhom_chuyen_mon 
AND canBo.loai_doi_tuong in (select * from f_split(loaiNhom.loai_doi_tuong))

LEFT JOIN (SELECT * FROM TL_DM_CapBac_Luong_NQ104 WHERE loai = 3 and nam = ' + CAST(@Nam AS VARCHAR(4)) + ') capBacLuong
ON (
	(
	canBo.loai_doi_tuong IN (''1'',''3.2'',''4'',''5'') 
	AND canBo.loai_doi_tuong IN 
	(SELECT * FROM f_split(capBacLuong.loai_doi_tuong)) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong
	)
	OR 
	(
	canBo.loai_doi_tuong NOT IN (''1'',''3.2'',''4'',''5'') 
	AND (capBacLuong.ma_dm_cha = loaiNhom.nhom) AND capBacLuong.loai_doi_tuong = loaiNhom.loai_doi_tuong AND capBacLuong.xau_noi_ma like loaiNhom.xau_noi_ma + ''-%''
	)
)
AND capBacLuong.ma_dm = canBo.ma_bac_luong
LEFT JOIN TL_DM_ChucVu_NQ104 chucVu
ON canBo.Ma_Cvd104 = chucVu.Ma AND chucVu.nam = ' + CAST(@Nam AS VARCHAR(4)) + '
WHERE
canBo.IsDelete = 1
AND canBo.Khong_Luong = 0
And (canbo.bNuocNgoai= (CASE WHEN '+ CAST(@TyLeHuong AS VARCHAR(4))  + ' = 0 then 0
										ELSE 1
									END ))
)

SELECT
bangLuong.*,
canBo.MaDonVi,
canBo.TenDonVi,
canBo.TenCanBo,
canBo.Ten,
canBo.MaCapBac,
canBo.CapBac,
canBo.MaChucVu,
canBo.TenChucVu,
canBo.LanNangLuongCb,
canBo.LanNangLuongCvd,
canBo.TenNhom,
canBo.TenLoai,
canBo.CapBacLuong,
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
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
ELSE
SET @Query = @Query +' ORDER BY MaCapBac DESC, Ten ASC';
execute(@Query)
END
;
;
;
;
;
GO
