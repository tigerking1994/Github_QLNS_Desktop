/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 1/11/2024 6:20:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_giaithich_trocap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/11/2024 6:20:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_getTienthuchien6thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 1/11/2024 6:20:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 1/11/2024 6:20:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_kehoach_chikham_chubenh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 1/12/2024 9:15:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 1/12/2024 10:40:28 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[bh_kehoach_chikham_chubenh_index]    Script Date: 1/11/2024 6:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị lâp kế hoạch chi kham chu benh

-- =============================================
CREATE PROCEDURE [dbo].[bh_kehoach_chikham_chubenh_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 kcb.iID_BH_KHC_KCB 
		, kcb.sSoChungTu
		, kcb.dNgayChungTu
		, kcb.sSoQuyetDinh
		, kcb.dNgayQuyetDinh
		, kcb.iNamLamViec
		, kcb.iID_DonVi
		, kcb.iID_MaDonVi
		, kcb.sMoTa
		, kcb.fTongTienDaThucHienNamTruoc
		, kcb.fTongTienUocThucHienNamTruoc
		, kcb.fTongTienKeHoachThucHienNamNay
		, kcb.sTongHop
		, kcb.iID_TongHopID
		, kcb.iLoaiTongHop
		, kcb.bIsKhoa
		, kcb.dNgaySua
		, kcb.dNgayTao
		, kcb.sNguoiSua
		, kcb.sNguoiTao
		, kcb.dNgayTao
		, kcb.bDaTongHop
		, donVi.sTenDonVi
		, kcb.fTyLeThu as FTyleThu
		-- Tong dự toán todo
	FROM BH_KHC_KCB  kcb
	LEFT JOIN DonVi donVi
		ON kcb.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = kcb.iID_DonVi
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 1/11/2024 6:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int
AS
BEGIN
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPCChucVu float,
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
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = 0;
--END chi tiet

INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
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
			AND iNamLamViec = @NamLamViec) mlns
		LEFT JOIN #tempChiTietDonVi chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		GROUP BY
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa;
	--INSERT CHI TIẾT	
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
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
SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;
DROP TABLE #tempChiTietDonVi;
DROP TABLE #result;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]    Script Date: 1/11/2024 6:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_getTienthuchien6thang]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IID_LoaiChi nvarchar(100),
	@SMaLoaiChi nvarchar(50),
	@SLNS nvarchar(100),
	@DNgayChungTu datetime
AS
BEGIN
		Create table #TempData6thang
		(
			iID_MaDonVi nvarchar(50),
			iID_MucLucNganSach uniqueidentifier,
			sM nvarchar(50),
			sTM nvarchar(50),
			sNoiDung nvarchar(max),
			iNamLamViec int ,
			fTienThucHien06ThangDauNam float
		);

		---- Get data 6 thang tu qtc quy 
		---- Data 6 thang Chi các chế độ BHXH
		 IF(@SLNS ='9010001,9010002')
		 	INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,sM,sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sLoaiTroCap sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTongTien_DeNghi) as fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_CheDoBHXH ct
				LEFT JOIN BH_QTC_Quy_CheDoBHXH_ChiTiet ctct ON ct.ID_QTC_Quy_CheDoBHXH=ctct.iID_QTC_Quy_CheDoBHXH
				WHERE ct.iNamChungTu=@NamLamViec
				AND ctct.iIDMaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sLoaiTroCap,iNamLamViec;
		 ---- Data 6 thang Chi kinh phí quản lý BHXH, BHYT
		ELSE IF(@SLNS='9010003')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 
		---- Data Chi kinh phí chăm sóc sức khỏe ban đầu HSSV & NLĐ
		ELSE IF(@SLNS='905,9050001,9050002')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KinhPhiQuanLy ct
				LEFT JOIN BH_QTC_Quy_KinhPhiQuanLy_ChiTiet ctct ON ct.ID_QTC_Quy_KinhPhiQuanLy=ctct.iID_QTC_Quy_KinhPhiQuanLy
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi kinh phí KCB tại quân y đơn vị --comment 10/01/2024 		ELSE IF(@SLNS='9010004,9010005')
		ELSE IF(@SLNS='9010004')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iID_MaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KCB ct
				LEFT JOIN BH_QTC_Quy_KCB_ChiTiet ctct ON ct.ID_QTC_Quy_KCB=ctct.iID_QTC_Quy_KCB
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  ctct.iID_MaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		   ---- Data 6 thang Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân
		ELSE IF(@SLNS='9010008')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Chi hỗ trợ BHTN 
		ELSE IF(@SLNS='9010009')
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

		  ---- Data 6 thang Kinh phí mua sắm trang thiết bị y tế 
		   ELSE
		  BEGIN
			INSERT INTO #TempData6thang(ctct.iID_MaDonVi,ctct.iID_MucLucNganSach,ctct.sM,ctct.sTM,ctct.sNoiDung,ctct.iNamLamViec,ctct.fTienThucHien06ThangDauNam)
		   SELECT 
					ctct.iIDMaDonVi,
					ctct.iID_MucLucNganSach,
					'' sM,
					'' sTM,
					ctct.sNoiDung,
					ctct.iNamLamViec,
					SUM(ctct.fTienThucChi) fTienThucHien06ThangDauNam
				FROM 
				BH_QTC_Quy_KPK ct
				LEFT JOIN BH_QTC_Quy_KPK_ChiTiet ctct ON ct.ID_QTC_Quy_KPK=ctct.iID_QTC_Quy_KPK
				WHERE ct.iNamChungTu=@NamLamViec
				AND ct.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND ct.iID_LoaiChi=@IID_LoaiChi
				AND ct.bIsKhoa=1
				AND	ct.iQuyChungTu<=2
				AND cast (ct.dNgayChungTu as date)<= cast(@DNgayChungTu as date)
				GROUP BY  iIDMaDonVi,iID_MucLucNganSach,sNoiDung,iNamLamViec;
		  END 

			SELECT dm.iID_MLNS_Cha as IdParent,
			   dm.iID_MLNS as IID_MucLucNganSach,
			   tbl.fTienThucHien06ThangDauNam,
			   dm.bHangCha as IsHangCha,
			   dm.sCPChiTietToi as SCPChiTietToi,
			   dm.sDuToanChiTietToi as SDuToanChiTietToi,
			   dm.sMoTa SNoiDung
			FROM BH_DM_MucLucNganSach dm
			LEFT JOIN #TempData6thang  tbl 
			on tbl.iID_MucLucNganSach=dm.iID_MLNS
			where dm.iNamLamViec=@NamLamViec and dm.sLNS in (select * from splitstring(@SLNS))
			order by dm.sXauNoiMa
			drop table #TempData6thang
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_giaithich_trocap]    Script Date: 1/11/2024 6:20:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_giaithich_trocap]
	-- Add the parameters for the stored procedure here
	@lstmaCanbo nvarchar(max) ,
	@Thang int ,
	@Nam int ,
	@DonViTinh int ,
	@TypeOutPut int  -- 2: Đơn vị; 1: theo đối tượng
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	--Declare ma phu cap --
	DECLARE @LstMaPhuCapBDN_D14N nvarchar(1000) = 'BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY';
	DECLARE @LstMaPhuCapBDN_T14N nvarchar(1000) = 'BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY';
	DECLARE @LstMaPhuCapOK_D14N nvarchar(1000) = 'OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY';
	DECLARE @LstMaPhuCapOK_T14N nvarchar(1000) = 'OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY';
	DECLARE @NameBDN_D14 nvarchar(1000) = N'Bệnh dài ngày - Dưới 14 ngày';
	DECLARE @NameBDN_T14 nvarchar(1000)=N'Bệnh dài ngày - Từ 14 ngày trở lên';
	DECLARE @NameOK_D14 nvarchar(1000)=N'Ốm khác - Dưới 14 ngày';
	DECLARE @NameOK_T14 nvarchar(1000)=N'Ốm khác - Từ 14 ngày trở lên';

	CREATE TABLE #tempResult(STT nvarchar(6) ,TenChiTieu nvarchar(1000), MaCapBac nvarchar(50),MaCanBo nvarchar(50), TenCanBo  nvarchar(500), MaDonVi nvarchar(50), PCCVBH_TT numeric, PCTNBH_TT numeric, HSBLBH_TT numeric,  LBH_TT numeric, PCTNVKBH_TT numeric, Total numeric, LoaiDoiTuong nvarchar(50), rowNumber int)
    -- Insert statements for procedure here
	-- Bệnh dài ngày dứoi 14 ngày
	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total, LoaiDoiTuong,rowNumber)
	SELECT CAST('1' as nvarchar(6)), @NameBDN_D14 , sMaCB, sMaCBo, sTenCbo,sMaDonVi,
	  BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY,
	  CASE
		WHEN sMaCB LIKE '1%' THEN 'SQ'
		WHEN sMaCB LIKE '2%' THEN 'QNCN'
		WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
		WHEN sMaCB = '43' THEN 'LDHD'
		ELSE
			NULL
	 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_D14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_D14N_PCCVBH_TT,BDN_D14N_PCTNBH_TT,BDN_D14N_HSBLBH_TT,BDN_D14N_LBH_TT,BDN_D14N_PCTNVKBH_TT,BENHDAINGAY_D14NGAY)  
	) AS PivotTable
	UNION
	-- Bệnh dài ngày trên 14 ngày
	SELECT  '2',@NameBDN_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapBDN_T14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (BDN_T14N_PCCVBH_TT,BDN_T14N_PCTNBH_TT,BDN_T14N_HSBLBH_TT,BDN_T14N_LBH_TT,BDN_T14N_PCTNVKBH_TT,BENHDAINGAY_T14NGAY)  
	) AS PivotTable
		UNION

	--Ốm khác dưới 14 ngày
	SELECT  '3',@NameOK_D14,  sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	 FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_D14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_D14N_PCCVBH_TT,OK_D14N_PCTNBH_TT,OK_D14N_HSBLBH_TT,OK_D14N_LBH_TT,OK_D14N_PCTNVKBH_TT,OMKHAC_D14NGAY)  
	) AS PivotTable
		UNION

	-- Ốm khác trên 14 ngày
	SELECT '4', @NameOK_T14, sMaCB, sMaCBo, sTenCbo,sMaDonVi,
			OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY,
	  	  CASE
			WHEN sMaCB LIKE '1%' THEN 'SQ'
			WHEN sMaCB LIKE '2%' THEN 'QNCN'
			WHEN sMaCB LIKE '0%' THEN 'HSQ_BS'
			WHEN sMaCB IN('3.1','3.2','3.3') THEN 'VCQP'
			WHEN sMaCB = '43' THEN 'LDHD'
			ELSE
				NULL
		 END,
	 CASE
		WHEN sMaCB LIKE '1%' THEN 1
		WHEN sMaCB LIKE '2%' THEN 2
		WHEN sMaCB LIKE '0%' THEN 3
		WHEN sMaCB IN('3.1','3.2','3.3') THEN 4
		WHEN sMaCB = '43' THEN 5
		ELSE
			NULL
	 END
	FROM  
	(
	  SELECT SUM(ISNULL(nGiaTri,0)) as nGiaTri, sMaCheDo ,sMaCBo,sMaDonVi  ,sTenCbo,sMaCB
	  FROM TL_BangLuong_ThangBHXH
	  where sMaCBo IN (SELECT * FROM splitstring(@lstmaCanbo))   and sMaCheDo IN (SELECT * FROM splitstring(@LstMaPhuCapOK_T14N)) AND iThang =@Thang and iNam = @Nam
	  group by sMaDonVi,sMaCheDo ,sMaCBo,sTenCbo,sMaCB
	) AS SourceTable  
	PIVOT  
	(  
	  SUM (nGiaTri) 
	  FOR sMaCheDo IN (OK_T14N_PCCVBH_TT,OK_T14N_PCTNBH_TT,OK_T14N_HSBLBH_TT,OK_T14N_LBH_TT,OK_T14N_PCTNVKBH_TT,OMKHAC_T14NGAY)  
	) AS PivotTable;  

	INSERT INTO #tempResult(STT, TenChiTieu, MaCapBac,MaCanBo, TenCanBo, MaDonVi, PCCVBH_TT, PCTNBH_TT, HSBLBH_TT, LBH_TT, PCTNVKBH_TT, Total,LoaiDoiTuong,rowNumber)
	SELECT '0',TenCanBo,MaCapBac, MaCanBo,TenCanBo, MaDonVi, 
			SUM(ISNULL(PCCVBH_TT,0)) PCCVBH_TT,
			SUM(ISNULL(PCTNBH_TT,0)) PCTNBH_TT, 
			SUM(ISNULL(HSBLBH_TT,0)) HSBLBH_TT, 
			SUM(ISNULL(LBH_TT,0)) LBH_TT, 
			SUM(ISNULL(PCTNVKBH_TT,0)) PCTNVKBH_TT,
			SUM(ISNULL(Total,0)) Total,
			LoaiDoiTuong,
			rowNumber
	FROM #tempResult group by MaCanBo, TenCanBo, MaCapBac,MaDonVi, LoaiDoiTuong,rowNumber;
	--SELECT LEFT(MaCapBac, 1),* FROM #tempResult ORDER BY MaCanBo , STT;
	IF(@TypeOutPut = 2)
		BEGIN
		--Lấy Đơn vị
			SELECT 
				0 as Level,
				CAST (NULL as nvarchar(6)) STT,
				CAST (NULL as nvarchar(50)) LoaiDoiTuong,
				donvi.Ten_DonVi as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				0 as rowNumber
				INTO #tempDonVi
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> 0
				GROUP BY rs.MaDonVi, donvi.Ten_DonVi
				ORDER BY rs.MaDonVi;
			-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.MaDonVi,rs.LoaiDoiTuong,donvi.Ten_DonVi

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				rs.MaDonVi MaDonVi, 
				donvi.Ten_DonVi as TenDonVi,
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo 
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi;

				--OUTPUT---
			SELECT rs.*  FROM
			(
			SELECT * FROM #tempDonVi
			UNION 
			SELECT * FROM #tempLoaiDoiTuong
			UNION
			SELECT * FROM #tempCanBo
			) rs
			ORDER BY rs.MaDonVi,rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempDonVi;
			DROP TABLE #tempLoaiDoiTuong;
			DROP TABLE #tempCanBo;
		END
	ELSE
		BEGIN 
-- Lấy Loại đối tượng
			SELECT 
				1 Level,
	  			CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 'I'
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 'II'
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 'III'
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 'IV'
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 'V'
					ELSE
						NULL
				END AS nvarchar(6))  STT,
				rs.LoaiDoiTuong,
				rs.LoaiDoiTuong as TenChiTieu,
				CAST (NULL as nvarchar(50)) MaCapBac,
				CAST (NULL as nvarchar(50)) MaCanBo,
				CAST (NULL as nvarchar(50)) TenCanBo,
				CAST (NULL as nvarchar(50)) MaDonVi,
				SUM(ISNULL(PCCVBH_TT,0))/@DonViTinh PCCVBH_TT,
				SUM(ISNULL(PCTNBH_TT,0))/@DonViTinh PCTNBH_TT, 
				SUM(ISNULL(HSBLBH_TT,0))/@DonViTinh HSBLBH_TT, 
				SUM(ISNULL(LBH_TT,0))/@DonViTinh LBH_TT, 
				SUM(ISNULL(PCTNVKBH_TT,0))/@DonViTinh PCTNVKBH_TT,
				SUM(ISNULL(Total,0))/@DonViTinh Total,
				CAST( CASE
					WHEN rs.LoaiDoiTuong = 'SQ' THEN 1
					WHEN rs.LoaiDoiTuong = 'QNCN' THEN 2
					WHEN rs.LoaiDoiTuong = 'HSQ_BS' THEN 3
					WHEN rs.LoaiDoiTuong = 'VCQP' THEN 4
					WHEN rs.LoaiDoiTuong = 'LDHD' THEN 5
					ELSE
						NULL
				END AS int)  rowNumber
				INTO #tempLoaiDoiTuong2
				FROM #tempResult rs
				LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
				WHERE rs.STT <> '0'
				GROUP BY rs.LoaiDoiTuong

			-- lấy chi tiết từng đối tượng
			SELECT 
				CASE
					WHEN STT = '0' THEN 2 
					ELSE 3
				END AS Level,
				STT,
				LoaiDoiTuong,
				TenChiTieu,
				MaCapBac, 
				MaCanBo,
				TenCanBo,
				rs.MaDonVi MaDonVi, 
				ISNULL(PCCVBH_TT,0)/@DonViTinh PCCVBH_TT,
				ISNULL(PCTNBH_TT,0)/@DonViTinh PCTNBH_TT, 
				ISNULL(HSBLBH_TT,0)/@DonViTinh HSBLBH_TT, 
				ISNULL(LBH_TT,0)/@DonViTinh LBH_TT, 
				ISNULL(PCTNVKBH_TT,0)/@DonViTinh PCTNVKBH_TT,
				ISNULL(Total,0)/@DonViTinh Total,
				rs.rowNumber
				INTO #tempCanBo2 
				FROM #tempResult rs

				--OUTPUT---
			SELECT rs.*,donvi.Ten_DonVi as TenDonVi  FROM
			(
			SELECT * FROM #tempLoaiDoiTuong2
			UNION
			SELECT * FROM #tempCanBo2
			) rs
			LEFT JOIN TL_DM_DonVi donvi ON donvi.Ma_DonVi = rs.MaDonVi
			ORDER BY rs.rowNumber,rs.MaCanBo

			DROP TABLE #tempLoaiDoiTuong2;
			DROP TABLE #tempCanBo2;		
		END
	DROP TABLE #tempResult;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 1/12/2024 9:15:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
	@YearOfWork int,
	@LoaiTongHop int,
	@DaTongHop bit,
	@QuyNam int
AS
BEGIN
	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		--AND qtt.bDaTongHop = @DaTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iLoaiTongHop = @LoaiTongHop
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 1/12/2024 10:40:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int  
AS
BEGIN
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iID_MaDonVi = @IdDonVis
																		and pr.iLoaiTongHop = 2
																		and pr.bDaTongHop = 0)
CREATE TABLE #result(
iID_MLNS uniqueidentifier,
iID_MLNS_Cha uniqueidentifier,
bHangCha bit, 
sXauNoiMa nvarchar(200), 
sMoTa nvarchar(200), 
iNamLamViec int,
iQSBQNam int,
fLuongChinh float,
fPCChucVu float,
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
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
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
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong,
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
			AND iNamLamViec = @NamLamViec) mlns
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
			and ct.iID_MaDonVi = @IdDonVis
			and ct.iLoaiTongHop = 2
--			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end			
		)chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		ORDER BY mlns.sXauNoiMa;


----------------END INSERT DETAIL----------------
----------------INSERT DETAIL----------------
INSERT INTO #result
(
iID_MLNS ,
iID_MLNS_Cha ,
bHangCha , 
sXauNoiMa , 
sMoTa , 
iNamLamViec ,
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
SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

DROP TABLE #tempChiTietDonVi;
DROP TABLE #result;
END
;
