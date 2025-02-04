/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 7/25/2024 11:43:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh]    Script Date: 7/25/2024 11:43:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_bh_qtc_get_canbo_salary_by_xaunoima_bhxh] 
	-- Add the parameters for the stored procedure here
 	@sXauNoiMa nvarchar(max),
	@sQuarter nvarchar(50) ,
	@Donvi  nvarchar(50),
	@NamLamViec int,
	@sLNS nvarchar(50)
	
AS
BEGIN
	DECLARE @STongHop nvarchar(max);	
	DECLARE @XauNoiMaREPLACE nvarchar(max);
	set @XauNoiMaREPLACE =case when @sXauNoiMa like '9010001%' 
									then REPLACE(@sXauNoiMa,'9010001-' , '') 
									else REPLACE(@sXauNoiMa,'9010002-' , '') end  

	SELECT sMaCheDo 
			, bDisplay
			, case when sXauNoiMaMlnsBHXH like '9010001%' 
			then REPLACE(sXauNoiMaMlnsBHXH,'9010001-' , '')
			else REPLACE(sXauNoiMaMlnsBHXH,'9010002-' , '') end  sXauNoiMaMlnsBHXH
				into #tempTL_DM_CheDoBHXH
			FROM TL_DM_CheDoBHXH
			WHERE bDisplay = 1
	select top(1) bangluong.STongHop into #temp1  FROM TL_BangLuong_ThangBHXH bangluongchitiet 
			JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
			inner join DonVi dv on dv.iID_MaDonVi=bangluong.Ma_CBo
			where
			
				bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
					AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
							FROM #tempTL_DM_CheDoBHXH
							WHERE bDisplay = 1 
								AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
					AND (
						bangluongchitiet.sMaCB LIKE '1%'  
						OR bangluongchitiet.sMaCB LIKE '2%'  
						OR bangluongchitiet.sMaCB = '3.1'  
						OR bangluongchitiet.sMaCB = '3.2'  
						OR bangluongchitiet.sMaCB = '3.3'
						OR bangluongchitiet.sMaCB = '413' 
						OR bangluongchitiet.sMaCB = '415' 
						OR bangluongchitiet.sMaCB = '423' 
						OR bangluongchitiet.sMaCB = '425' 
						OR bangluongchitiet.sMaCB = '43' 
						OR bangluongchitiet.sMaCB LIKE '0%'
			)
			and bangluong.Ma_CachTL = N'CACH2'
			and bangluong.Ma_CBo = @Donvi
			and bangluong.KhoaBangLuong = 1
			and bangluong.Nam = @NamLamViec
			and bangluong.STongHop is not null
			and dv.iNamLamViec=@NamLamViec
			--(
			--	(@sLNS='9010002' and dv.iKhoi!=2)
			--	or (dv.iKhoi=2)
			--)
			
		

	set @STongHop = (select #temp1.STongHop from #temp1);

	select * into #tblSTongHop from splitstring(@STongHop);

	-- Get bang luong chi tiet 
	select * into #temp2 from TL_BangLuong_ThangBHXH bangluongchitiet  where bangluongchitiet.sMaDonVi in (select * from #tblSTongHop)
	and bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
		AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo 
										FROM #tempTL_DM_CheDoBHXH
										WHERE bDisplay = 1 
										AND sXauNoiMaMlnsBHXH = @XauNoiMaREPLACE )
	AND (
		bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
	)

	--- Lấy tiền truy lĩnh---
	SELECT tbl.*, t2.fSoNgayTruyLinh, t2.nGiaTriTruyLinh INTO #temp3
	FROM #temp2 tbl
	LEFT JOIN 
		(SELECT tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam, SUM(ISNULL(canbo.fSoNgayHuongBHXH, 0)) fSoNgayTruyLinh, SUM(ISNULL(tbl.nGiaTri, 0)) as nGiaTriTruyLinh
		FROM TL_BangLuong_ThangBHXH tbl
		INNER JOIN #temp2 t2 ON tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
		LEFT JOIN TL_CanBo_CheDoBHXH canbo ON tbl.sMaCBo = canbo.sMaCanBo and tbl.sMaCheDo=canbo.sMaCheDo
		WHERE tbl.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH where sLoaiTruyLinh IN (select sMaCheDo from #temp2 ))
		GROUP BY tbl.sMaCBo , tbl.sMaDonVi, tbl.iThang, tbl.iNam
		) as t2 ON  tbl.sMaCBo = t2.sMaCBo AND tbl.sMaDonVi = t2.sMaDonVi AND t2.iThang = tbl.iThang AND tbl.iNam = t2.iNam 
	
		
	

	SELECT 
		@sXauNoiMa as SXauNoiMa,
		sMaHieuCanBo as SMaHieuCanBo,
		sTenCbo as STenCanBo,
		sMaCBo As SMaCanBo,
		sMaCB as SMaCapBac,
		capbac.Note aS STenCapBac,
		bangluongchitiet.sMaDonVi as ID_MaPhanHo,
		dv.Ten_DonVi as STenPhanHo,
		canbo.fSoNgayHuongBHXH as ISoNgayHuong,
		canbo.sSoQuyetDinh as SSoQuyetDinh,
		canbo.dNgayQuyetDinh as DNgayQuyetDinh,
		bangluongchitiet.nGiaTri as FSoTien,
		bangluongchitiet.nGiaTriTruyLinh as FTienTruyLinh,
		bangluongchitiet.fSoNgayTruyLinh as ISoNgayTruyLinh,
		canbo.dTuNgay AS DTuNgay,
		canbo.dDenNgay AS DDenNgay,
		bangluongchitiet.sMaCB
	FROM #temp3 bangluongchitiet 
	LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo and bangluongchitiet.sMaCheDo=canbo.sMaCheDo
	LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluongchitiet.sMaDonVi
	where  (
			bangluongchitiet.sMaCB LIKE '1%'  
			OR bangluongchitiet.sMaCB LIKE '2%'  
			OR bangluongchitiet.sMaCB = '3.1'  
			OR bangluongchitiet.sMaCB = '3.2'  
			OR bangluongchitiet.sMaCB = '3.3'
			OR bangluongchitiet.sMaCB = '413' 
			OR bangluongchitiet.sMaCB = '415' 
			OR bangluongchitiet.sMaCB = '423' 
			OR bangluongchitiet.sMaCB = '425' 
			OR bangluongchitiet.sMaCB = '43' 
			OR bangluongchitiet.sMaCB LIKE '0%'
		)
		and canbo.iNam=@NamLamViec
DROP TABLE #temp3,#temp2,#temp1,#tblSTongHop;
	--	SELECT 
	--	N'9010001-010-011-0001-0001-0001-01-02' as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluong.sMaDonVi as ID_MaPhanHo,
	--	dv.Ten_DonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluong.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluong
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluong.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluong.sMaCB
	--LEFT JOIN TL_DM_DonVi dv On dv.Ma_DonVi = bangluong.sMaDonVi
	--WHERE 
	--	bangluong.iThang IN (SELECT * FROM splitstring('1,2,3'))
	--	AND bangluong.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = N'9010001-010-011-0001-0001-0001-01-02' )
	--	AND (
	--		bangluong.sMaCB LIKE '1%'  
	--		OR bangluong.sMaCB LIKE '2%'  
	--		OR bangluong.sMaCB LIKE '3.1%'  
	--		OR bangluong.sMaCB LIKE '3.2%'  
	--		OR bangluong.sMaCB LIKE '3.3%'  
	--		OR bangluong.sMaCB LIKE '43%' 
	--		OR bangluong.sMaCB LIKE '0%' 
	--	)

	-- Get data 
	--SELECT 
	--	@sXauNoiMa as SXauNoiMa,
	--	sMaHieuCanBo as SMaHieuCanBo,
	--	sTenCbo as STenCanBo,
	--	sMaCBo As SMaCanBo,
	--	sMaCB as SMaCapBac,
	--	capbac.Note aS STenCapBac,
	--	bangluongchitiet.sMaDonVi as ID_MaPhanHo,
	--	dv.sTenDonVi as STenPhanHo,
	--	canbo.fSoNgayHuongBHXH as ISoNgayHuong,
	--	canbo.sSoQuyetDinh as SSoQuyetDinh,
	--	canbo.dNgayQuyetDinh as DNgayQuyetDinh,
	--	bangluongchitiet.nGiaTri as FSoTien,
	--	canbo.dTuNgay AS DTuNgay,
	--	canbo.dDenNgay AS DDenNgay

	--FROM TL_BangLuong_ThangBHXH bangluongchitiet 
	--LEFT JOIN TL_CanBo_CheDoBHXH canbo ON bangluongchitiet.sMaCBo = canbo.sMaCanBo
	--LEFT JOIN TL_DM_CapBac capbac ON capbac.Ma_Cb  = bangluongchitiet.sMaCB
	--LEFT JOIN DonVi dv On dv.iID_MaDonVi = bangluongchitiet.sMaDonVi
	--JOIN  TL_DS_CapNhap_BangLuong bangluong on bangluongchitiet.iID_Parent = bangluong.Id
	--WHERE 
	--	bangluongchitiet.iThang IN (SELECT * FROM splitstring(@sQuarter))
	--	AND bangluongchitiet.sMaCheDo IN (SELECT sMaCheDo FROM TL_DM_CheDoBHXH WHERE bDisplay = 1 AND sXauNoiMaMlnsBHXH = @sXauNoiMa )
	--	AND (
	--		bangluongchitiet.sMaCB LIKE '1%'  
	--		OR bangluongchitiet.sMaCB LIKE '2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.1%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.2%'  
	--		OR bangluongchitiet.sMaCB LIKE '3.3%'  
	--		OR bangluongchitiet.sMaCB LIKE '43%' 
	--		OR bangluongchitiet.sMaCB LIKE '0%' 
	--	)
	--	and bangluong.Ma_CachTL = N'CACH2'
	--	and bangluong.Ma_CBo=@Donvi
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		into tbl_qtn_result
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001'  and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
		from
			BH_QTT_BHXH_ChungTu ct
		join BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		where
			ct.iQuyNam = @IQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			--and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1) chungtudonvi 
		on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVi
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit,
sM nvarchar(200),
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

----------------END DETAIL AGENCY----------------
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu fPhuCapChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,mlns.sM
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.sSoChungTu in (select * from splitstring(@sSoChungTuTH))
			and ct.iLoaiTongHop = 1
			and ct.bDaTongHop = 1;

----------------END DETAIL----------------
----------------INSERT TOTAL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(isnull(chungtudonvi.fLuongChinh, 0) + isnull(chungtudonvi.fPCChucVu, 0) + isnull(chungtudonvi.fPCTNNghe, 0) + isnull(chungtudonvi.fPCTNVuotKhung, 0) + isnull(chungtudonvi.fNghiOm, 0) + isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongSoPhaiThuBHTN,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0)) fTongNLD,
		(isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0)) fTongNSD,
		(isnull(chungtudonvi.fThu_BHXH_NLD, 0) + isnull(chungtudonvi.fThu_BHXH_NSD, 0) + isnull(chungtudonvi.fThu_BHYT_NLD, 0) + isnull(chungtudonvi.fThu_BHYT_NSD, 0) + isnull(chungtudonvi.fThu_BHTN_NLD, 0) + isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fTongCong,
		null,
		null
		FROM
			(select
				BH_DM_MucLucNganSach.iID_MLNS,
				BH_DM_MucLucNganSach.iID_MLNS_Cha,
				BH_DM_MucLucNganSach.bHangCha,
				BH_DM_MucLucNganSach.sLNS,
				BH_DM_MucLucNganSach.sL,
				BH_DM_MucLucNganSach.sK,
				BH_DM_MucLucNganSach.sM,
				BH_DM_MucLucNganSach.sTM,
				BH_DM_MucLucNganSach.sTTM,
				BH_DM_MucLucNganSach.sNG,
				BH_DM_MucLucNganSach.sTNG,
				BH_DM_MucLucNganSach.sXauNoiMa,
				BH_DM_MucLucNganSach.sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS like '902%'
			AND iNamLamViec = @NamLamViec			
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		LEFT JOIN(
			select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
			--and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
sM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVis
	group by sLNS

	--Update so truy thu
	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa, MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int
AS
BEGIN
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sM nvarchar(200),
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPhuCapChucVu float,
fPCTNNghe float,
fPCTNVuotKhung float,
fNghiOm float,
fHSBL float,
fTongQTLN float,
fDuToan float,
fDaQuyetToan float,
fConLai float,
fThu_BHXH_NLD float,
fThu_BHXH_NSD float,
fTongSoPhaiThuBHXH float,
fThu_BHYT_NLD float,
fThu_BHYT_NSD float,
fTongSoPhaiThuBHYT float,
fThu_BHTN_NLD float,
fThu_BHTN_NSD float,
fTongSoPhaiThuBHTN float,
fTongNLD float,
fTongNSD float,
fTongCong float,
MaDonVi nvarchar(50),
TenDonVi nvarchar(50)
);

--- GET CHI TIẾT ĐƠN VỊ
		select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,mlns.sM
			,ctct.sXauNoiMa
			,ctct.sLNS
			,mlns.sMoTa
			INTO #tempChiTietDonVi
			from
			BH_QTT_BHXH_ChungTu ct
			INNER JOIN
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			LEFT JOIN BH_DM_MucLucNganSach mlns ON  mlns.sXauNoiMa = ctct.sXauNoiMa AND mlns.iNamLamViec = @NamLamViec
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0;
--END chi tiet

INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
--INSERT TOTAL
select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPCChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan) fDuToan,
		sum(chungtudonvi.fDaQuyetToan) fDaQuyetToan,
		sum(chungtudonvi.fConLai) fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))) fTongNLD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))) fTongNSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong,
		null,
		null
		FROM
			(select
				BH_DM_MucLucNganSach.iID_MLNS,
				BH_DM_MucLucNganSach.iID_MLNS_Cha,
				BH_DM_MucLucNganSach.bHangCha,
				BH_DM_MucLucNganSach.sLNS,
				BH_DM_MucLucNganSach.sL,
				BH_DM_MucLucNganSach.sK,
				BH_DM_MucLucNganSach.sM,
				BH_DM_MucLucNganSach.sTM,
				BH_DM_MucLucNganSach.sTTM,
				BH_DM_MucLucNganSach.sNG,
				BH_DM_MucLucNganSach.sTNG,
				BH_DM_MucLucNganSach.sXauNoiMa,
				BH_DM_MucLucNganSach.sMoTa
			from BH_DM_MucLucNganSach 
			where sLNS like '902%'
			AND iNamLamViec = @NamLamViec
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa

			) mlns

		LEFT JOIN #tempChiTietDonVi chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		GROUP BY
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa;
	--INSERT CHI TIẾT	
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
SM,
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPhuCapChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
fTongNLD ,
fTongNSD ,
fTongCong ,
MaDonVi, 
TenDonVi 
)
SELECT
NULL ,
NULL ,
0 bHangCha , 
SM,
sXauNoiMa , 
dv.sTenDonVi,
#tempChiTietDonVi.iNamLamViec ,
iQSBQNam ,
fLuongChinh ,
fPCChucVu ,
fPCTNNghe ,
fPCTNVuotKhung ,
fNghiOm ,
fHSBL ,
fTongQTLN ,
fDuToan ,
fDaQuyetToan ,
fConLai ,
fThu_BHXH_NLD ,
fThu_BHXH_NSD ,
fTongSoPhaiThuBHXH ,
fThu_BHYT_NLD ,
fThu_BHYT_NSD ,
fTongSoPhaiThuBHYT ,
fThu_BHTN_NLD ,
fThu_BHTN_NSD ,
fTongSoPhaiThuBHTN ,
0 fTongNLD ,
0 fTongNSD ,
fTongCong ,
dv.iID_MaDonVi, 
dv.sTenDonVi as TenDonVi 
FROM #tempChiTietDonVi 
LEFT JOIN DonVi dv ON dv.iID_MaDonVi = #tempChiTietDonVi.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec;

-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into #tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update #result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from #tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from #tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from #tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

-- Ket qua
SELECT * FROM #result ORDER BY sXauNoiMa , MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #tbl_qtn_truythu;
DROP TABLE #result;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sM,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		sum(chungtudonvi.fLuongChinh)/@Donvitinh fLuongChinh,
		sum(chungtudonvi.fPCChucVu)/@Donvitinh fPhuCapChucVu,
		sum(chungtudonvi.fPCTNNghe)/@Donvitinh fPCTNNghe,
		sum(chungtudonvi.fPCTNVuotKhung)/@Donvitinh fPCTNVuotKhung,
		sum(chungtudonvi.fNghiOm)/@Donvitinh fNghiOm,
		sum(chungtudonvi.fHSBL)/@Donvitinh fHSBL,
		(sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0)))/@Donvitinh fTongQTLN,
		sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
		sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
		sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
		sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
		sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh FTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
		into tbl_qtn_result
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa
		group by
			mlns.iID_MLNS,
			mlns.sMoTa,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sM,
			mlns.sXauNoiMa
		

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPhuCapChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan/@Donvitinh fDuToan,
		chungtudonvi.fDaQuyetToan/@Donvitinh fDaQuyetToan,
		chungtudonvi.fConLai/@Donvitinh fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		into tbl_qtn_result
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020001' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS from BH_DM_MucLucNganSach where sLNS = '9020002' and (sL = '' or sL is null)
				and (sk = '' or sk is null)
				and iID_MLNS_Cha is not null 
				and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
				) chungtudonvi 
			on mlns.sXauNoiMa = chungtudonvi.sXauNoiMa

	-- Lay data truy thu
	select sLNS,
		sum(isnull(fTruyThu_BHXH_NLD, 0)) fTruyThu_BHXH_NLD,
		sum(isnull(fTruyThu_BHXH_NSD, 0)) fTruyThu_BHXH_NSD,
		(sum(isnull(fTruyThu_BHXH_NLD, 0)) + sum(isnull(fTruyThu_BHXH_NSD, 0))) fTongTruyThu_BHXH,
		sum(isnull(fTruyThu_BHYT_NLD, 0)) fTruyThu_BHYT_NLD,
		sum(isnull(fTruyThu_BHYT_NSD, 0)) fTruyThu_BHYT_NSD,
		(sum(isnull(fTruyThu_BHYT_NLD, 0)) + sum(isnull(fTruyThu_BHYT_NSD, 0))) fTongTruyThu_BHYT,
		sum(isnull(fTruyThu_BHTN_NLD, 0)) fTruyThu_BHTN_NLD,
		sum(isnull(fTruyThu_BHTN_NSD, 0)) fTruyThu_BHTN_NSD,
		(sum(isnull(fTruyThu_BHTN_NLD, 0)) + sum(isnull(fTruyThu_BHTN_NSD, 0))) fTongTruyThu_BHTN
	into tbl_qtn_truythu
	from BH_QTT_BHXH_CTCT_GiaiThich
	where iLoaiGiaiThich = 2
		and iQuyNam = @IQuy
		and iID_MaDonVi = @IdDonVi
	group by sLNS

	--Update so truy thu
	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020001'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020001'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020001'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020001')
	where sXauNoiMa = '9020001-010-011-0003'

	update tbl_qtn_result
	set fThu_BHXH_NLD = (select fTruyThu_BHXH_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHXH_NSD = (select fTruyThu_BHXH_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		FTongSoPhaiThuBHXH = (select fTongTruyThu_BHXH from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NLD = (select fTruyThu_BHYT_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHYT_NSD = (select fTruyThu_BHYT_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHYT = (select fTongTruyThu_BHYT from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NLD = (select fTruyThu_BHTN_NLD from tbl_qtn_truythu where sLNS = '9020002'),
		fThu_BHTN_NSD = (select fTruyThu_BHTN_NSD from tbl_qtn_truythu where sLNS = '9020002'),
		fTongSoPhaiThuBHTN = (select fTongTruyThu_BHTN from tbl_qtn_truythu where sLNS = '9020002'),
		fTongCong = (select isnull(fTongTruyThu_BHXH, 0) + isnull(fTongTruyThu_BHYT, 0) + isnull(fTongTruyThu_BHTN, 0) from tbl_qtn_truythu where sLNS = '9020002')
	where sXauNoiMa = '9020002-010-011-0003'

	-- Ket qua
	select * from tbl_qtn_result order by sXauNoiMa;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_result]') AND type in (N'U')) drop table tbl_qtn_result
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_qtn_truythu]') AND type in (N'U')) drop table tbl_qtn_truythu

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_khoi]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@IQuy int,
@Donvitinh int
as
begin
	---Lấy thông tin chi tiết giai thich tro cap du toan
		SELECT gt.* ,dv.sTenDonVi
			into #TBL_TroCapThaiSanDuToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND dv.iKhoi=2
								AND ct.iNamChungTu=@INamLamViec
								AND ct.iQuyChungTu = @IQuy 

	---Lấy thông tin chi tiết giai thich tro cap hach toan
		SELECT gt.*,dv.sTenDonVi 
			into #TBL_TroCapThaiSanHachToan 
						FROM BH_QTC_Quy_CheDoBHXH_ChiTiet gt
						Inner JOIN BH_QTC_Quy_CheDoBHXH ct ON gt.iID_QTC_Quy_CheDoBHXH=ct.ID_QTC_Quy_CheDoBHXH
						Inner JOIN DonVi dv on dv.iID_MaDonVi=ct.iID_MaDonVi
						WHERE (gt.sXauNoiMa like '9010001-010-011-0002%' or gt.sXauNoiMa like '9010002-010-011-0002%')
								AND gt.iNamLamViec = @INamLamViec 
								AND dv.iNamLamViec=@INamLamViec
								AND dv.iID_MaDonVi  IN (select * from dbo.splitstring(@IdMaDonVi)) 
								AND ct.iNamChungTu=@INamLamViec
								AND dv.iKhoi=1
								AND ct.iQuyChungTu = @IQuy 

		select 
		N'I. Khối dự toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempDuToan

		-- SQ Du Toan
		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQDuToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNDuToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPDuToan

		select 
		N'4. HSQ, BS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSDuToan

		select 
		N'5. LĐHĐ' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDDuToan

----- Hach Toan
		select 
		N'II. Khối hạch toán' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,3 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHachToan

		select 
		N'1. Sĩ quan' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempSQHachToan

		select 
		N'2. QNCN' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempQNCNHachToan

		select 
		N'3. CNVCQP' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempCNVCQPHachToan

		select 
		N'4. HSQ, BS' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempHSQBSHachToan

		select 
		N'5. LĐHĐ' as sTT
		,'' iID_MaDonVi
		,'' sTenDonVi
		,0 iSoNgaySinhConNNuoiCon
		,0 fSoTienSinhConNNuoiCon
		,0 iSoNgaySinhTroCapSinhCon
		,0 fSoTienSinhTroCapSinhCon
		,0 ISoNgayKhamThaiKHHGD
		,0 fSoTienKhamThaiKHHGD
		,0 iSoNgayPHSKThaiSan
		,0 fSoTienPHSKThaiSan
		,2 as type
		,1 IsHangCha
		,1 IsParent
		,1 BHangCha
		into #tempLDHDHachToan

		--- Lay thong tin giai thich theo khoi du toan


		---Du Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan
				
				INTO #tempDetalSQDuToanSum
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa


		SELECT 
			CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQDuToan 
				FROM #tempDetalSQDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNDuToan 
				FROM #tempDetalQNCNDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		---Du Toan CNVCQP
		SELECT 

				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPDuToan 
				FROM #tempDetalCNVCQPDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi


		-- Du Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSDuToan 
				FROM #tempDetalHSQBSDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Du Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDDuToanSum 
			FROM #TBL_TroCapThaiSanDuToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa
		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDDuToan 
				FROM #tempDetalLDHDDuToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		--- Lay thong tin giai thich theo khoi Hach Toan
		---Hach Toan SQ
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoSQ_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoSQ_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienSQ_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienSQ_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalSQHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalSQHachToan 
				FROM #tempDetalSQHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi
		---Hach Toan QNCN
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoQNCN_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienQNCN_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienQNCN_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalQNCNHachToanSum 
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalQNCNHachToan 
				FROM #tempDetalQNCNHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		---Hach Toan CNVCQP
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoCNVCQP_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienCNVCQP_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienCNVCQP_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalCNVCQPHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalCNVCQPHachToan 
				FROM #tempDetalCNVCQPHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan HSQBS
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoHSQBS_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienHSQBS_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienHSQBS_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalHSQBSHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalHSQBSHachToan 
				FROM #tempDetalHSQBSHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		-- Hach Toan LDHD
		SELECT 
				gt.iIDMaDonVi iID_MaDonVi,
				gt.sTenDonVi,
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end )/30 iSoNgaySinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0001-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0001-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhConNNuoiCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgaySinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0002-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0002-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienSinhTroCapSinhCon,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayKhamThaiKHHGD,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0003-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0003-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienKhamThaiKHHGD,

				
				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.iSoLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.iSoLDHD_DeNghi) else 0 end ) iSoNgayPHSKThaiSan,

				(case when gt.sXauNoiMa like '9010001-010-011-0002-0001-0004-00%'then sum(gt.fTienLDHD_DeNghi) else 0 end  +
				case when gt.sXauNoiMa like '9010002-010-011-0002-0001-0004-00%' then sum(gt.fTienLDHD_DeNghi) else 0 end ) fSoTienPHSKThaiSan

				INTO #tempDetalLDHDHachToanSum
			FROM #TBL_TroCapThaiSanHachToan gt
			Group by gt.iIDMaDonVi,
				gt.sTenDonVi,gt.sXauNoiMa

		SELECT 
				CONVERT(varchar(10),(ROW_NUMBER() OVER(ORDER BY iID_MaDonVi ASC))) AS sTT,
				iID_MaDonVi,
				sTenDonVi,
				SUM(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon,
				SUM(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon,
				SUM(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon,
				SUM(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon,
				SUM(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD,
				SUM(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD,
				SUM(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan,
				SUM(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
				,1 as typ
				,0 IsHangCha
				,null IsParent
				,0 BHangCha
				INTO #tempDetalLDHDHachToan 
				FROM #tempDetalLDHDHachToanSum
				Group by iID_MaDonVi,
				sTenDonVi

		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQDuToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQDuToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNDuToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNDuToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPDuToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPDuToan) DetailCNVCQP

			update #tempHSQBSDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSDuToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSDuToan) DetailHSQBS

			update #tempLDHDDuToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDDuToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDDuToan) DetailLDHD
		----- Update Du Toan : SQ,QNCN,CNVCQP,HSQBS,LDHD
					update SQ
			set SQ.iSoNgaySinhConNNuoiCon=ISNULL(DetailSQ.iSoNgaySinhConNNuoiCon,0),
			SQ.fSoTienSinhConNNuoiCon=ISNULL(DetailSQ.fSoTienSinhConNNuoiCon,0),
			SQ.iSoNgaySinhTroCapSinhCon=ISNULL(DetailSQ.iSoNgaySinhTroCapSinhCon,0),
			SQ.fSoTienSinhTroCapSinhCon=ISNULL(DetailSQ.fSoTienSinhTroCapSinhCon,0),
			SQ.iSoNgayKhamThaiKHHGD=ISNULL(DetailSQ.iSoNgayKhamThaiKHHGD,0),
			SQ.fSoTienKhamThaiKHHGD=ISNULL(DetailSQ.fSoTienKhamThaiKHHGD,0),
			SQ.iSoNgayPHSKThaiSan=ISNULL(DetailSQ.iSoNgayPHSKThaiSan,0),
			SQ.fSoTienPHSKThaiSan=ISNULL(DetailSQ.fSoTienPHSKThaiSan,0)
			FROM #tempSQHachToan SQ,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 2 type
			FROM #tempDetalSQHachToan) DetailSQ
			where DetailSQ.type=SQ.type

			update QNCN
			set QNCN.iSoNgaySinhConNNuoiCon=ISNULL(DetailQNCN.iSoNgaySinhConNNuoiCon,0),
			QNCN.fSoTienSinhConNNuoiCon=ISNULL(DetailQNCN.fSoTienSinhConNNuoiCon,0),
			QNCN.iSoNgaySinhTroCapSinhCon=ISNULL(DetailQNCN.iSoNgaySinhTroCapSinhCon,0),
			QNCN.fSoTienSinhTroCapSinhCon=ISNULL(DetailQNCN.fSoTienSinhTroCapSinhCon,0),
			QNCN.iSoNgayKhamThaiKHHGD=ISNULL(DetailQNCN.iSoNgayKhamThaiKHHGD,0),
			QNCN.fSoTienKhamThaiKHHGD=ISNULL(DetailQNCN.fSoTienKhamThaiKHHGD,0),
			QNCN.iSoNgayPHSKThaiSan=ISNULL(DetailQNCN.iSoNgayPHSKThaiSan,0),
			QNCN.fSoTienPHSKThaiSan=ISNULL(DetailQNCN.fSoTienPHSKThaiSan,0)
			FROM #tempQNCNHachToan QNCN,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalQNCNHachToan) DetailQNCN

			update CNVCQP
			set CNVCQP.iSoNgaySinhConNNuoiCon=ISNULL(DetailCNVCQP.iSoNgaySinhConNNuoiCon,0),
			CNVCQP.fSoTienSinhConNNuoiCon=ISNULL(DetailCNVCQP.fSoTienSinhConNNuoiCon,0),
			CNVCQP.iSoNgaySinhTroCapSinhCon=ISNULL(DetailCNVCQP.iSoNgaySinhTroCapSinhCon,0),
			CNVCQP.fSoTienSinhTroCapSinhCon=ISNULL(DetailCNVCQP.fSoTienSinhTroCapSinhCon,0),
			CNVCQP.iSoNgayKhamThaiKHHGD=ISNULL(DetailCNVCQP.iSoNgayKhamThaiKHHGD,0),
			CNVCQP.fSoTienKhamThaiKHHGD=ISNULL(DetailCNVCQP.fSoTienKhamThaiKHHGD,0),
			CNVCQP.iSoNgayPHSKThaiSan=ISNULL(DetailCNVCQP.iSoNgayPHSKThaiSan,0),
			CNVCQP.fSoTienPHSKThaiSan=ISNULL(DetailCNVCQP.fSoTienPHSKThaiSan,0)
			FROM #tempCNVCQPHachToan CNVCQP,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalCNVCQPHachToan) DetailCNVCQP

			update #tempHSQBSHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailHSQBS.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailHSQBS.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailHSQBS.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailHSQBS.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailHSQBS.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailHSQBS.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailHSQBS.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailHSQBS.fSoTienPHSKThaiSan,0)
			FROM #tempHSQBSHachToan HSQBS,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalHSQBSHachToan) DetailHSQBS

			update #tempLDHDHachToan
			set iSoNgaySinhConNNuoiCon=ISNULL(DetailLDHD.iSoNgaySinhConNNuoiCon,0),
			fSoTienSinhConNNuoiCon=ISNULL(DetailLDHD.fSoTienSinhConNNuoiCon,0),
			iSoNgaySinhTroCapSinhCon=ISNULL(DetailLDHD.iSoNgaySinhTroCapSinhCon,0),
			fSoTienSinhTroCapSinhCon=ISNULL(DetailLDHD.fSoTienSinhTroCapSinhCon,0),
			iSoNgayKhamThaiKHHGD=ISNULL(DetailLDHD.iSoNgayKhamThaiKHHGD,0),
			fSoTienKhamThaiKHHGD=ISNULL(DetailLDHD.fSoTienKhamThaiKHHGD,0),
			iSoNgayPHSKThaiSan=ISNULL(DetailLDHD.iSoNgayPHSKThaiSan,0),
			fSoTienPHSKThaiSan=ISNULL(DetailLDHD.fSoTienPHSKThaiSan,0)
			FROM #tempLDHDHachToan LDHD,
			(SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			FROM #tempDetalLDHDHachToan) DetailLDHD

---Tra ve kqua DuToan
			SELECT * INTO #tempkqDuToan FROM
			(
					SELECT * FROM #tempDuToan
					UNION ALL
					SELECT * FROM  #tempSQDuToan
					UNION ALL
					SELECT * FROM  #tempDetalSQDuToan
					UNION ALL
					SELECT * FROM  #tempQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNDuToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPDuToan
					UNION ALL
					SELECT * FROM  #tempHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSDuToan
					UNION ALL
					SELECT * FROM  #tempLDHDDuToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDDuToan
			) TEMPDuToan

------ Update total khoi du toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalDuToan
			FROM #tempkqDuToan
			WHERE type=2

			update T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqDuToan T1, #tempTotalDuToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'I. Khối dự toán'

---------  Tra ve kqua HachToan
			SELECT * INTO #tempkqHachToan FROM
			(
					SELECT * FROM #tempHachToan
					UNION ALL
					SELECT * FROM  #tempSQHachToan
					UNION ALL
					SELECT * FROM  #tempDetalSQHachToan
					UNION ALL
					SELECT * FROM  #tempQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempDetalQNCNHachToan
					UNION ALL
					SELECT * FROM  #tempCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempDetalCNVCQPHachToan
					UNION ALL
					SELECT * FROM  #tempHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempDetalHSQBSHachToan
					UNION ALL
					SELECT * FROM  #tempLDHDHachToan
					UNION ALL
					SELECT * FROM  #tempDetalLDHDHachToan
			) TEMPHachToan

			------ Update total khoi hach toan
			SELECT 
			Sum(iSoNgaySinhConNNuoiCon) iSoNgaySinhConNNuoiCon
			, Sum(fSoTienSinhConNNuoiCon) fSoTienSinhConNNuoiCon
			, Sum(iSoNgaySinhTroCapSinhCon) iSoNgaySinhTroCapSinhCon
			, Sum(fSoTienSinhTroCapSinhCon) fSoTienSinhTroCapSinhCon
			, Sum(iSoNgayKhamThaiKHHGD) iSoNgayKhamThaiKHHGD
			, Sum(fSoTienKhamThaiKHHGD) fSoTienKhamThaiKHHGD
			, Sum(iSoNgayPHSKThaiSan) iSoNgayPHSKThaiSan
			, Sum(fSoTienPHSKThaiSan) fSoTienPHSKThaiSan
			, 3 type
			INTO #tempTotalHachToan
			FROM #tempkqHachToan
			WHERE type=2

			UPDATE  T1
			set T1.iSoNgaySinhConNNuoiCon=T2.iSoNgaySinhConNNuoiCon,
			T1.fSoTienSinhConNNuoiCon=T2.fSoTienSinhConNNuoiCon,
			T1.iSoNgaySinhTroCapSinhCon=T2.iSoNgaySinhTroCapSinhCon,
			T1.fSoTienSinhTroCapSinhCon=T2.fSoTienSinhTroCapSinhCon,
			T1.iSoNgayKhamThaiKHHGD=T2.iSoNgayKhamThaiKHHGD,
			T1.fSoTienKhamThaiKHHGD=T2.fSoTienKhamThaiKHHGD,
			T1.iSoNgayPHSKThaiSan=T2.iSoNgayPHSKThaiSan,
			T1.fSoTienPHSKThaiSan=T2.fSoTienPHSKThaiSan
			FROM #tempkqHachToan T1, #tempTotalHachToan T2
			WHERE T1.type=T2.type
			and T1.STT=N'II. Khối hạch toán'

		SELECT * INTO #tempKQAll FROM
			(
					SELECT * FROM #tempkqDuToan
					UNION ALL
					SELECT * FROM  #tempkqHachToan
					
			) TEMPKQAll

		select sTT
		, iID_MaDonVi
		,sTenDonVi
		,iSoNgaySinhConNNuoiCon
		,fSoTienSinhConNNuoiCon/@Donvitinh fSoTienSinhConNNuoiCon
		,iSoNgaySinhTroCapSinhCon
		,fSoTienSinhTroCapSinhCon/@Donvitinh fSoTienSinhTroCapSinhCon
		,iSoNgayKhamThaiKHHGD
		,fSoTienKhamThaiKHHGD/@Donvitinh fSoTienKhamThaiKHHGD
		,iSoNgayPHSKThaiSan
		,fSoTienPHSKThaiSan/@Donvitinh fSoTienPHSKThaiSan
		,(fSoTienSinhConNNuoiCon+fSoTienSinhTroCapSinhCon+fSoTienKhamThaiKHHGD+fSoTienPHSKThaiSan)/@Donvitinh as fTongTien
		,IsHangCha
		,BHangCha
		,type
		,IsParent
		from #tempKQAll

		drop table #TBL_TroCapThaiSanDuToan;
		drop table #TBL_TroCapThaiSanHachToan;
		drop table #tempDuToan
		drop table #tempSQDuToan
		drop table #tempQNCNDuToan
		drop table #tempCNVCQPDuToan
		drop table #tempHSQBSDuToan
		drop table #tempLDHDDuToan
		DROP TABLE #tempDetalSQDuToan
		DROP TABLE #tempDetalQNCNDuToan
		DROP TABLE #tempDetalCNVCQPDuToan
		DROP TABLE #tempDetalHSQBSDuToan
		DROP TABLE #tempDetalLDHDDuToan
		DROP TABLE #tempTotalDuToan
		DROP TABLE #tempDetalSQDuToanSum
		DROP TABLE #tempDetalQNCNDuToanSum
		DROP TABLE #tempDetalCNVCQPDuToanSum
		DROP TABLE #tempDetalHSQBSDuToanSum
		DROP TABLE #tempDetalLDHDDuToanSum


		drop table #tempHachToan
		drop table #tempSQHachToan
		drop table #tempQNCNHachToan
		drop table #tempCNVCQPHachToan
		drop table #tempHSQBSHachToan
		drop table #tempLDHDHachToan
		DROP TABLE #tempDetalSQHachToan
		DROP TABLE #tempDetalQNCNHachToan
		DROP TABLE #tempDetalCNVCQPHachToan
		DROP TABLE #tempDetalHSQBSHachToan
		DROP TABLE #tempDetalLDHDHachToan
		DROP TABLE #tempTotalHachToan

		DROP TABLE #tempDetalSQHachToanSum
		DROP TABLE #tempDetalQNCNHachToanSum
		DROP TABLE #tempDetalCNVCQPHachToanSum
		DROP TABLE #tempDetalHSQBSHachToanSum
		DROP TABLE #tempDetalLDHDHachToanSum


		DROP TABLE #tempkqDuToan
		DROP TABLE #tempkqHachToan
		DROP TABLE #tempKQAll

end
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]    Script Date: 7/26/2024 2:24:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_kinhphi_chedo_bhxh]
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	select 
		dv.sTenDonVi as sTenDonVi,
		ct.sMoTa,
		dv.iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #data
	from
	(select 
	iID_MaDonVi,
	case when iMa > 182 and iMa < 191 then N'Khối dự toán'
	when iMa > 192 and iMa < 201 then N'Khối hạch toán'
	else N'Khối khác' end as sMoTa,
	case when ima = 183 or ima = 193 then fSoThamDinh
			else 0 end as fOmDau,
	case when ima = 184 or ima = 194 then fSoThamDinh
			else 0 end as fThaiSan,
	case when ima = 185 or ima = 195 then fSoThamDinh
			else 0 end as fTNLDBNN,
	case when ima = 186 or ima = 196 then fSoThamDinh
			else 0 end as fHuuTri,
	case when ima = 187 or ima = 197 then fSoThamDinh
			else 0 end as fPhucVien,
	case when ima = 188 or ima = 198 then fSoThamDinh
			else 0 end as fXuatNgu,
	case when ima = 189 or ima = 199 then fSoThamDinh
			else 0 end as fThoiViec,
	case when ima = 190 or ima = 200 then fSoThamDinh
			else 0 end as fTuTuat
	from BH_ThamDinhQuyetToan_ChungTuChiTiet
	where 
	iID_MaDonVi in (select * from f_split(@IdDonVi)) and 
	iNamLamViec = @INamLamViec) ct
	left join (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	where fOmDau > 0 or fThaiSan > 0 or fTNLDBNN > 0 or fHuuTri > 0 or fPhucVien > 0 or fXuatNgu > 0 or fThoiViec > 0 or fTuTuat > 0
	group by ct.iID_MaDonVi, dv.sTenDonVi, ct.sMoTa, dv.iKhoi

	select 
	sSTT = '',
	sTenDonVi = N'A. Đơn vị dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA
	from (select * from #data where iKhoi = 2) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA1
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowA2
	from (select * from #data where iKhoi = 2 and sMoTa = N'Khối hạch toán') ct

	select 
	sSTT = '',
	sTenDonVi = N'B. Đơn vị hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB
	from (select * from #data where iKhoi = 1) ct

		select 
	sSTT = '',
	sTenDonVi = N'+ Khối dự toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB1
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối dự toán') ct

			select 
	sSTT = '',
	sTenDonVi = N'+ Khối hạch toán',
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 1
	into #rowB2
	from (select * from #data where iKhoi = 1 and sMoTa = N'Khối hạch toán') ct

				select 
	sSTT = '',
	sTenDonVi,
	iKhoi,
		sum(ct.fOmDau) fOmDau,
		sum(ct.fThaiSan) fThaiSan,
		sum(ct.fTNLDBNN) fTNLDBNN,
		sum(ct.fHuuTri) fHuuTri,
		sum(ct.fPhucVien) fPhucVien,
		sum(ct.fXuatNgu) fXuatNgu,
		sum(ct.fThoiViec) fThoiViec,
		sum(ct.fTuTuat) fTuTuat,
		iKieuChu = 3
	into #rowDV
	from #data ct
	group by ct.sTenDonVi, ct.sTenDonVi, ct.iKhoi

	select * 
	into #rowDV1
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu 
		from #rowDV where iKhoi = 2) dv1
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 2) 
	order by sTenDonVi, sMoTa

	select *
	into #rowDV2
	from (select 
		ROW_NUMBER() OVER (ORDER BY sTenDonVi) AS sSTT,
		sTenDonVi,
		sMoTa = null,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
		from #rowDV where iKhoi = 1) dv2
	union all (select 
		'' AS sSTT,
		sTenDonVi,
		sMoTa,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu = 2
	from #data where iKhoi = 1) 
	order by sTenDonVi, sMoTa

	select * from #rowA
	union all select * from #rowA1
	union all select * from #rowA2
	union all (select 
		sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV1)
	union all select * from #rowB
	union all select * from #rowB1
	union all select * from #rowB2
	union all (select 
			sSTT,
		isnull('       ' + sMoTa, sTenDonVi) sTenDonVi,
		fOmDau,
		fThaiSan,
		fTNLDBNN,
		fHuuTri,
		fPhucVien,
		fXuatNgu,
		fThoiViec,
		fTuTuat,
		iKieuChu
	from #rowDV2)
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 7/29/2024 10:40:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 7/29/2024 10:40:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 7/29/2024 10:40:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX),
@MaDonVi nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamLamViec,
		sXauNoiMa,
		iIDMaDonVi
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTienDuToanDuyet),
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork,
	   sXauNoiMa,
	   @MaDonVi

FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Quy_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap,sXauNoiMa;

INSERT INTO [dbo].[BH_QTC_Quy_CTCT_GiaiThichTroCap]
           ([iiD_QTC_Quy_CTCT_GiaiThichTroCap]
           ,[dNgayQuyetDinh]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[fSoTien]
           ,[iiD_MaDonVi]
           ,[iiD_MaPhanHo]
           ,[iID_QTC_Quy_ChungTu]
           ,[iNamLamViec]
           ,[iQuy]
           ,[iSoNgayHuong]
           ,[sMa_Hieu_Can_Bo]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sSoQuyetDinh]
           ,[sTenCanBo]
           ,[sTenCapBac]
           ,[sTenPhanHo]
           ,[sXauNoiMa]
           ,[dDenNgay]
           ,[dTuNgay]
           ,[sSoSoBHXH]
           ,[fTienLuongThangDongBHXH]
           ,[sMaCapBac]
           ,[fTienTruyLinh]
           ,[iSoNgayTruyLinh])
	 SELECT 
	   NEWID(),
	   dNgayQuyetDinh,
       null,
	   GETDATE(),
       fSoTien,
	   @MaDonVi,
	   iiD_MaPhanHo,
	   @IdChungTuSummary,
	   @YearOfWork,
	   iQuy,
	   iSoNgayHuong,
	   sMa_Hieu_Can_Bo,
	   '',
	   @NguoiTao,
	   sSoQuyetDinh,
	   sTenCanBo,
	   sTenCapBac,
	   sTenPhanHo,
	   sXauNoiMa,
	   dDenNgay,
	   dTuNgay,
	   sSoSoBHXH,
	   fTienLuongThangDongBHXH,
	   sMaCapBac,
	   fTienTruyLinh,
	   iSoNgayTruyLinh

FROM BH_QTC_Quy_CTCT_GiaiThichTroCap
WHERE  iID_QTC_Quy_ChungTu IN
    (SELECT *
     FROM f_split(@IdChungTu))

UPDATE BH_QTC_Quy_CheDoBHXH SET iLoaiTongHop=2 , bDaTongHop=1  WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 7/29/2024 10:40:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
	@SLNS nvarchar (MAX),
	@INamLamViec INT,
	@MaDonVi nvarchar (100),
	@Loai BIT
	AS BEGIN
	DECLARE
		@quy INT;
	SELECT
		@quy = iquychungtu 
	FROM
		BH_QTC_Quy_CheDoBHXH 
	WHERE
		ID_QTC_Quy_CheDoBHXH = @IdChungTu;

		-- lấy ra đơn vị từ đơn vị của chứng từ
	SELECT iID_MaDonVi, iID_MaDonVi + ' - ' + sTenDonVi as sTenDonVi into #tblDonVi
	FROM DonVi 
	WHERE 
		INamLamViec = @INamLamViec 
		AND iTrangThai=1
		AND iID_MaDonVi IN (SELECT * FROM f_split(@MaDonVi))

---Lấy danh sách mục lục ngân sách
	SELECT
		danhmuc.iID_MLNS AS iID_MLNS,
		danhmuc.iID_MLNS_Cha,
		danhmuc.sLNS,
		danhmuc.sL,
		danhmuc.sK,
		danhmuc.sM,
		danhmuc.sTM,
		danhmuc.sTTM,
		danhmuc.sNG,
		danhmuc.sTNG,
		danhmuc.sXauNoiMa,
		danhmuc.sMoTa,
		danhmuc.bHangCha,
		danhmuc.sDuToanChiTietToi,
		danhmuc.bHangChaDuToan,
		dv.iID_MaDonVi,
		dv.sTenDonVi
		INTO #tblMucLucNganSach 
	FROM
		BH_DM_MucLucNganSach AS danhmuc ,#tblDonVi dv
	WHERE
		danhmuc.iNamLamViec = @INamLamViec 
		AND danhmuc.sLNs IN (SELECT * FROM f_split (@SLNS)) 
		AND danhmuc.iTrangThai=1

		---Lấy thông tin chi tiết chứng từ
	SELECT
		qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		qtcn_ct.iID_QTC_Quy_CheDoBHXH,
		qtcn_ct.iID_MucLucNganSach,
		qtcn_ct.sLoaiTroCap,
		qtcn_ct.fTienDuToanDuyet,
		(
			isnull(qtcn_ct_truoc.fTienCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.fTienSQ_DeNghi, 0) 
		) fTienLuyKeCuoiQuyTruoc,
		(
			isnull(qtcn_ct_truoc.iSoCNVCQP_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoHSQBS_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoLDHD_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoQNCN_DeNghi, 0) + isnull(qtcn_ct_truoc.iSoSQ_DeNghi, 0) 
		) iSoLuyKeCuoiQuyTruoc,
		qtcn_ct.iSoSQ_DeNghi,
		qtcn_ct.fTienSQ_DeNghi,
		qtcn_ct.iSoQNCN_DeNghi,
		qtcn_ct.fTienQNCN_DeNghi,
		qtcn_ct.iSoCNVCQP_DeNghi,
		qtcn_ct.fTienCNVCQP_DeNghi,
		qtcn_ct.iSoHSQBS_DeNghi,
		qtcn_ct.fTienHSQBS_DeNghi,
--qtcn_ct.iTongSo_DeNghi,
--qtcn_ct.fTongTien_DeNghi,
		qtcn_ct.fTongTien_PheDuyet,
		qtcn_ct.iSoLDHD_DeNghi,
		qtcn_ct.fTienLDHD_DeNghi,
		qtcn.iID_MaDonVi iIDMaDonVi,
		qtcn.iNamChungTu iNamLamViec INTO #tblQuyetToanQuyChiTiet 
	FROM
		BH_QTC_Quy_CheDoBHXH_ChiTiet AS qtcn_ct
		INNER JOIN BH_QTC_Quy_CheDoBHXH AS qtcn ON qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		LEFT JOIN (
		SELECT SUM
			(isnull(fTienCNVCQP_DeNghi, 0)) fTienCNVCQP_DeNghi,
			SUM (isnull(fTienHSQBS_DeNghi, 0)) fTienHSQBS_DeNghi,
			SUM (isnull(fTienLDHD_DeNghi, 0)) fTienLDHD_DeNghi,
			SUM (isnull(fTienQNCN_DeNghi, 0)) fTienQNCN_DeNghi,
			SUM (isnull(fTienSQ_DeNghi, 0)) fTienSQ_DeNghi,
			SUM (isnull(iSoCNVCQP_DeNghi, 0)) iSoCNVCQP_DeNghi,
			SUM (isnull(ctct.fTongTien_PheDuyet, 0)) fTongTien_PheDuyet,
			SUM (isnull(iSoHSQBS_DeNghi, 0)) iSoHSQBS_DeNghi,
			SUM (isnull(iSoLDHD_DeNghi, 0)) iSoLDHD_DeNghi,
			SUM (isnull(iSoQNCN_DeNghi, 0)) iSoQNCN_DeNghi,
			SUM (isnull(iSoSQ_DeNghi, 0)) iSoSQ_DeNghi,
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach
		FROM
			(
				SELECT fTienCNVCQP_DeNghi, fTienHSQBS_DeNghi, fTienLDHD_DeNghi, fTienQNCN_DeNghi, fTienSQ_DeNghi, iSoCNVCQP_DeNghi, fTongTien_PheDuyet, iSoHSQBS_DeNghi,
					iSoLDHD_DeNghi,  iSoQNCN_DeNghi, iSoSQ_DeNghi, iID_QTC_Quy_CheDoBHXH, iID_MucLucNganSach
				FROM BH_QTC_Quy_CheDoBHXH_ChiTiet ct
				INNER JOIN BH_DM_MucLucNganSach dmns on ct.sXauNoiMa = dmns.sXauNoiMa
				where dmns.bHangCha = 0 and dmns.iNamLamViec = @INamLamViec
			)ctct
			INNER JOIN BH_QTC_Quy_CheDoBHXH ct ON ctct.iID_QTC_Quy_CheDoBHXH = ct.ID_QTC_Quy_CheDoBHXH 
		WHERE
			ct.iQuyChungTu < @quy 
		GROUP BY
			ct.iID_MaDonVi,
			ct.iNamChungTu,
			ctct.iID_MucLucNganSach
		) qtcn_ct_truoc ON qtcn.iID_MaDonVi = qtcn_ct_truoc.iID_MaDonVi 
		AND qtcn.iNamChungTu = qtcn_ct_truoc.iNamChungTu 
		AND qtcn_ct.iID_MucLucNganSach = qtcn_ct_truoc.iID_MucLucNganSach
	WHERE
		qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu 
		AND qtcn.iNamChungTu=@INamLamViec;
---Kết quả hiển thị trả về
	IF
		(@Loai = 1) SELECT
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.bHangChaDuToan,
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		@IdChungTu iID_QTC_Quy_CheDoBHXH,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0)) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		mucluc.iID_MaDonVi iIDMaDonVi,
		mucluc.sTenDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
			SELECT ctct.sXauNoiMa, SUM(ctct.fTienTuChi)  fTienTuChi
			FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct 
			JOIN BH_DTC_PhanBoDuToanChi ct ON ctct.iID_DTC_PhanBoDuToanChi = ct.ID 
				WHERE ctct.iID_MaDonVi = @MaDonVi 
				AND BIsKhoa = 1
				AND ct.iNamChungTu = @INamLamViec
				GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa ELSE SELECT
		mucluc.iID_MLNS,
		mucluc.iID_MLNS_Cha,
		mucluc.sLNS,
		mucluc.sL,
		mucluc.sK,
		mucluc.sM,
		mucluc.sTM,
		mucluc.sTTM,
		mucluc.sNG,
		mucluc.sTNG,
		mucluc.sXauNoiMa,
		mucluc.bHangCha,
		mucluc.bHangChaDuToan,
		mucluc.sDuToanChiTietToi,
		chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
		@IdChungTu iID_QTC_Quy_CheDoBHXH,
		mucluc.iID_MLNS AS iID_MucLucNganSach,
		mucluc.sMoTa AS sLoaiTroCap,
		(isnull(dt.fTienTuChi, 0) ) fTienDuToanDuyet,
		chi_tiet.iSoLuyKeCuoiQuyTruoc,
		chi_tiet.fTienLuyKeCuoiQuyTruoc,
		chi_tiet.iSoSQ_DeNghi,
		chi_tiet.fTienSQ_DeNghi,
		chi_tiet.iSoQNCN_DeNghi,
		chi_tiet.fTienQNCN_DeNghi,
		chi_tiet.iSoCNVCQP_DeNghi,
		chi_tiet.fTienCNVCQP_DeNghi,
		chi_tiet.iSoHSQBS_DeNghi,
		chi_tiet.fTienHSQBS_DeNghi,
--chi_tiet.iTongSo_DeNghi,
--chi_tiet.fTongTien_DeNghi,
		chi_tiet.fTongTien_PheDuyet,
		chi_tiet.iSoLDHD_DeNghi,
		chi_tiet.fTienLDHD_DeNghi,
		mucluc.iID_MaDonVi iIDMaDonVi,
		mucluc.sTenDonVi,
		chi_tiet.iNamLamViec 
	FROM
		#tblMucLucNganSach AS mucluc
		LEFT JOIN (
					SELECT ctct.sXauNoiMa,
							SUM(ctct.fTienTuChi) fTienTuChi
							FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
							JOIN BH_DTC_DuToanChiTrenGiao ct ON ctct.iID_DTC_DuToanChiTrenGiao = ct.ID 
		WHERE ct.iID_MaDonVi = @MaDonVi
		AND BIsKhoa = 1
		AND ct.iNamLamViec = @INamLamViec
		GROUP BY ctct.sXauNoiMa) dt ON dt.sXauNoiMa = mucluc.sXauNoiMa
		LEFT JOIN #tblQuyetToanQuyChiTiet AS chi_tiet ON mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach 
	ORDER BY
		mucluc.sXauNoiMa DROP TABLE #tblMucLucNganSach;
	DROP TABLE #tblQuyetToanQuyChiTiet;
	
END;
;
;
;
;
;
;
;
;
;
;
GO
