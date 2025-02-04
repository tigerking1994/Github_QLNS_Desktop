/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan_donvi]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_da_quyet_toan_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_don_vi_da_nhap_lieu]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_don_vi_da_nhap_lieu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_don_vi_da_nhap_lieu]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet_donvi]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 4/5/2024 12:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet] 
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	DECLARE @LCS float;
	SELECT @LCS = fGiaTri FROM BH_DM_CauHinhThamSo
	WHERE iNamLamViec = @NamLamViec AND sMa = 'LCS';

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @MaDonVi;

	SELECT 
		ct.*
		FROM
			(
				SELECT
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet,
					ct.iID_QTT_BHXH_ChungTu,
					iID_MaDonVi = case when @isCha = 1 and ct.iLoaiTongHop = 2 then ct.iID_MaDonVi else ctct.iID_MaDonVi end,
					sTenDonVi = case when @isCha = 1 and ct.iLoaiTongHop = 2 then ddv.sTenDonVi else ddv2.TenDonViCTCT end,
					--ddv.sTenDonVi,
					ctct.iQSBQNam,
					ctct.fLuongChinh,
					ctct.fPCChucVu,
					ctct.fPCTNNghe,
					ctct.fPCTNVuotKhung,
					ctct.fNghiOm,
					ctct.fHSBL,
					ctct.fTongQTLN,
					ctct.fDuToan,
					ctct.fDaQuyetToan,
					ctct.fConLai,
					ctct.fThu_BHXH_NLD,
					ctct.fThu_BHXH_NSD,
					ctct.fTongSoPhaiThuBHXH,
					ctct.fThu_BHYT_NLD,
					ctct.fThu_BHYT_NSD,
					ctct.fTongSoPhaiThuBHYT,
					ctct.fThu_BHTN_NLD,
					ctct.fThu_BHTN_NSD,
					ctct.fTongSoPhaiThuBHTN,
					ctct.fTongCong,
					ctct.sGhiChu,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS,
					ct.iNamLamViec,
					@LCS as LCS
				FROM 
					BH_QTT_BHXH_ChungTu ct
				JOIN 
					BH_QTT_BHXH_ChungTu_ChiTiet ctct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu 
				LEFT JOIN 
					(SELECT iID_MaDonVi, sTenDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				LEFT JOIN 
					(SELECT iID_MaDonVi, sTenDonVi as TenDonViCTCT FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv2 ON ctct.iID_MaDonVi = ddv2.iID_MaDonVi
				WHERE
					ctct.iID_QTT_BHXH_ChungTu = @ChungTuId 
					and ((@isCha = 1 and ct.iLoaiTongHop = 2 and ct.iID_MaDonVi = @MaDonVi) or (@isCha = 0 and ct.iLoaiTongHop = 1 and ctct.iID_MaDonVi=@MaDonVi) or (@isCha = 1 and ct.iLoaiTongHop = 1))
			) ct;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet_donvi]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet_donvi] 
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	DECLARE @LCS float;
	SELECT @LCS = fGiaTri FROM BH_DM_CauHinhThamSo
	WHERE iNamLamViec = @NamLamViec AND sMa = 'LCS';

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @MaDonVi;

	SELECT 
		ct.*
		FROM
			(
				SELECT
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet,
					ct.iID_QTT_BHXH_ChungTu,
					iID_MaDonVi = case when @isCha = 1 and ct.iLoaiTongHop = 2 then ct.iID_MaDonVi else ctct.iID_MaDonVi end,
					sTenDonVi = case when @isCha = 1 and ct.iLoaiTongHop = 2 then ddv.sTenDonVi else ddv2.TenDonViCTCT end,
					--ddv.sTenDonVi,
					ctct.iQSBQNam,
					ctct.fLuongChinh,
					ctct.fPCChucVu,
					ctct.fPCTNNghe,
					ctct.fPCTNVuotKhung,
					ctct.fNghiOm,
					ctct.fHSBL,
					ctct.fTongQTLN,
					ctct.fDuToan,
					ctct.fDaQuyetToan,
					ctct.fConLai,
					ctct.fThu_BHXH_NLD,
					ctct.fThu_BHXH_NSD,
					ctct.fTongSoPhaiThuBHXH,
					ctct.fThu_BHYT_NLD,
					ctct.fThu_BHYT_NSD,
					ctct.fTongSoPhaiThuBHYT,
					ctct.fThu_BHTN_NLD,
					ctct.fThu_BHTN_NSD,
					ctct.fTongSoPhaiThuBHTN,
					ctct.fTongCong,
					ctct.sGhiChu,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS,
					ct.iNamLamViec,
					@LCS as LCS
				FROM 
					BH_QTT_BHXH_ChungTu ct
				JOIN 
					BH_QTT_BHXH_ChungTu_ChiTiet ctct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu 
				LEFT JOIN 
					(SELECT iID_MaDonVi, sTenDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				LEFT JOIN 
					(SELECT iID_MaDonVi, sTenDonVi as TenDonViCTCT FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv2 ON ctct.iID_MaDonVi = ddv2.iID_MaDonVi
				WHERE
					((@isCha = 1 and ct.iLoaiTongHop = 2 and ct.iID_MaDonVi = @MaDonVi) or (@isCha = 0 and ct.iLoaiTongHop = 1 and ctct.iID_MaDonVi=@MaDonVi) or (@isCha = 1 and ct.iLoaiTongHop = 1))
			) ct;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_don_vi_da_nhap_lieu]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_bh_qtt_bhxh_don_vi_da_nhap_lieu]
	@NamLamViec int,
	@UserName nvarchar(100),
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;
	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@QuyNam = 3 and @LoaiQuyNam = 1) SET @SMonths = '1,2,3'
	IF (@QuyNam = 6 and @LoaiQuyNam = 1) SET @SMonths = '4,5,6'
	IF (@QuyNam = 9 and @LoaiQuyNam = 1) SET @SMonths = '7,8,9'
	IF (@QuyNam = 12 and @LoaiQuyNam = 1) SET @SMonths = '10,11,12'

	IF @LoaiQuyNam = 0 SET @SMonths = @QuyNam SET @SLoaiQuy = @LoaiQuyNam
	IF @LoaiQuyNam = 1 SET @SLoaiQuy = '0,1'

	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @NamLamViec AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qttct.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTTCT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN BH_QTT_BHXH_ChungTu_ChiTiet qttct on qtt.iID_QTT_BHXH_ChungTu = qttct.iID_QTT_BHXH_ChungTu
		LEFT JOIN DonVi donvi
		ON qttct.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @NamLamViec
		AND qtt.iNamLamViec = @NamLamViec
		AND qtt.iQuyNam in (SELECT * FROM f_split(@SMonths))
		AND qtt.iQuyNamLoai in (SELECT * FROM f_split(@SLoaiQuy))

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTTCT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTTCT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @NamLamViec and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTTCT;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;

	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iQuyNamLoai = @LoaiQuyNam

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan_donvi]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan_donvi]
	@NamLamViec int,
	@MaDonVi nvarchar(50),
	@QuyNam int,
	@QuyNamLoai int
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
			SUM(ctctdqt.fTongCong) fDaQuyetToan
			FROM BH_QTT_BHXH_ChungTu ctdqt
			JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctctdqt ON ctdqt.iID_QTT_BHXH_ChungTu = ctctdqt.iID_QTT_BHXH_ChungTu
			WHERE ctdqt.iNamLamViec = @NamLamViec
			AND ctctdqt.iID_MaDonVi = @MaDonVi
			AND ((ctdqt.IQuyNam = @QuyNam AND @QuyNamLoai = 0) OR (ctdqt.IQuyNam <= @QuyNam AND @QuyNamLoai = 1))
			GROUP BY ctctdqt.iID_MLNS
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
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
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

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
		mlns.sMoTa,
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
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
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
		and iNamLamViec = @NamLamViec) mlns
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
			and ct.iQuyNamLoai = @ILoaiQuy
			--and ct.iID_MaDonVi = @IdDonVi
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_thang]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVi;

	select
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
		mlns.sMoTa,
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
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
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
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec
			,sum(isnull(ctct.iQSBQNam, 0)) iQSBQNam
			,sum(isnull(ctct.fLuongChinh, 0)) fLuongChinh
			,sum(isnull(ctct.fPCChucVu, 0)) fPCChucVu
			,sum(isnull(ctct.fPCTNNghe, 0)) fPCTNNghe
			,sum(isnull(ctct.fPCTNVuotKhung, 0)) fPCTNVuotKhung
			,sum(isnull(ctct.fNghiOm, 0)) fNghiOm
			,sum(isnull(ctct.fHSBL, 0)) fHSBL
			,sum(isnull(ctct.fTongQTLN, 0)) fTongQTLN
			,sum(isnull(ctct.fDuToan, 0)) fDuToan
			,sum(isnull(ctct.fDaQuyetToan, 0)) fDaQuyetToan
			,sum(isnull(ctct.fConLai, 0)) fConLai
			,sum(isnull(ctct.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD
			,sum(isnull(ctct.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH
			,sum(isnull(ctct.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD
			,sum(isnull(ctct.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT
			,sum(isnull(ctct.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD
			,sum(isnull(ctct.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD
			,sum(isnull(ctct.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN
			,sum(isnull(ctct.fTongCong, 0)) fTongCong
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVi) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVi))
			--and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			group by
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_MLNS,
			ctct.iID_MLNS_Cha,
			ctct.sXauNoiMa,
			ctct.sLNS
			) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
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
			AND iNamLamViec = @NamLamViec			
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha FROM BH_DM_MucLucNganSach WHERE  sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;
DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr 	where pr.iNamLamViec = @NamLamViec
																		and pr.iQuyNam = @IQuy
																		and pr.iQuyNamLoai = @ILoaiQuy
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
			and ct.iQuyNamLoai = @ILoaiQuy
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
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
			--and ct.iID_MaDonVi = @IdDonVis
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_th_chi_tiet_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(100),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	DECLARE @sSoChungTuTH nvarchar(1000) = (SELECT TOP 1 sTongHop FROM BH_QTT_BHXH_ChungTu pr where pr.iNamLamViec = @NamLamViec
																			and pr.iQuyNam in (SELECT * FROM f_split(@SMonths))
																			and pr.iQuyNamLoai = @ILoaiQuy
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
				ct.iNamLamViec
				,ctct.iID_QTT_BHXH_ChungTu
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
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
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
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
			mlns.sXauNoiMa,
			mlns.sMoTa,
			@NamLamViec iNamLamViec,
			sum(isnull(chungtudonvi.iQSBQNam, 0)) iQSBQNam,
			sum(isnull(chungtudonvi.fLuongChinh, 0))/@Donvitinh fLuongChinh,
			sum(isnull(chungtudonvi.fPCChucVu, 0))/@Donvitinh fPCChucVu,
			sum(isnull(chungtudonvi.fPCTNNghe, 0))/@Donvitinh fPCTNNghe,
			sum(isnull(chungtudonvi.fPCTNVuotKhung, 0))/@Donvitinh fPCTNVuotKhung,
			sum(isnull(chungtudonvi.fNghiOm, 0))/@Donvitinh fNghiOm,
			sum(isnull(chungtudonvi.fHSBL, 0))/@Donvitinh fHSBL,
			sum(isnull((chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL), 0))/@Donvitinh fTongQTLN,
			sum(isnull(chungtudonvi.fDuToan, 0))/@Donvitinh fDuToan,
			sum(isnull(chungtudonvi.fDaQuyetToan, 0))/@Donvitinh fDaQuyetToan,
			sum(isnull(chungtudonvi.fConLai, 0))/@Donvitinh fConLai,
			sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0))/@Donvitinh fThu_BHXH_NLD,
			sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))/@Donvitinh fThu_BHXH_NSD,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD), 0))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0))/@Donvitinh fThu_BHYT_NLD,
			sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))/@Donvitinh fThu_BHYT_NSD,
			sum(isnull((chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD), 0))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))/@Donvitinh fThu_BHTN_NLD,
			sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))/@Donvitinh fThu_BHTN_NSD,
			sum(isnull((chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongSoPhaiThuBHTN,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD), 0))/@Donvitinh fTongNLD,
			sum(isnull((chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongNSD,
			sum(isnull((chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD), 0))/@Donvitinh fTongCong,
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
				ct.iNamLamViec
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
				,ctct.iID_MLNS
				,ctct.iID_MLNS_Cha
				,ctct.sXauNoiMa
				,ctct.sLNS
				from
				BH_QTT_BHXH_ChungTu ct
				join
				BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
				where ct.iNamLamViec = @NamLamViec
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi = @IdDonVis
				and ((@isCha = 0 and ctct.iID_MaDonVi =@IdDonVis) or (@isCha = 1 and ct.iID_MaDonVi =@IdDonVis))
				and ct.iLoaiTongHop = 2	
			)chungtudonvi 
				on mlns.iID_MLNS = chungtudonvi.iID_MLNS
				group by
					mlns.iID_MLNS,
					mlns.iID_MLNS_Cha,
					mlns.bHangCha,
					mlns.sXauNoiMa,
					mlns.sMoTa
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
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
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
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
			UNION SELECT NewId() iID_MLNS, (SELECT DISTINCT iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa

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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_quy]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int
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
				and ct.iQuyNamLoai = @ILoaiQuy
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
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_Chi_tiet_don_vi_thang]
	@NamLamViec int ,
	@IdDonVis nvarchar(1000),
	@Donvitinh int ,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	
	DECLARE @SMonths NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	CREATE TABLE #result(
		iID_MLNS uniqueidentifier,
		iID_MLNS_Cha uniqueidentifier,
		bHangCha bit, 
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
				,ctct.iQSBQNam iQSBQNam
				,ctct.fLuongChinh fLuongChinh
				,ctct.fPCChucVu fPCChucVu
				,ctct.fPCTNNghe fPCTNNghe
				,ctct.fPCTNVuotKhung fPCTNVuotKhung
				,ctct.fNghiOm fNghiOm
				,ctct.fHSBL fHSBL
				,ctct.fTongQTLN fTongQTLN
				,ctct.fDuToan fDuToan
				,ctct.fDaQuyetToan fDaQuyetToan
				,ctct.fConLai fConLai
				,ctct.fThu_BHXH_NLD fThu_BHXH_NLD
				,ctct.fThu_BHXH_NSD fThu_BHXH_NSD
				,ctct.fTongSoPhaiThuBHXH fTongSoPhaiThuBHXH
				,ctct.fThu_BHYT_NLD fThu_BHYT_NLD
				,ctct.fThu_BHYT_NSD fThu_BHYT_NSD
				,ctct.fTongSoPhaiThuBHYT fTongSoPhaiThuBHYT
				,ctct.fThu_BHTN_NLD fThu_BHTN_NLD
				,ctct.fThu_BHTN_NSD fThu_BHTN_NSD
				,ctct.fTongSoPhaiThuBHTN fTongSoPhaiThuBHTN
				,ctct.fTongCong fTongCong
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
				and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
				and ct.iQuyNamLoai = @ILoaiQuy
				--and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))
				and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
				and ct.iLoaiTongHop = 1
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
			sum(chungtudonvi.fDuToan)/@Donvitinh fDuToan,
			sum(chungtudonvi.fDaQuyetToan)/@Donvitinh fDaQuyetToan,
			sum(chungtudonvi.fConLai)/@Donvitinh fConLai,
			sum(chungtudonvi.fThu_BHXH_NLD)/@Donvitinh fThu_BHXH_NLD,
			sum(chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fThu_BHXH_NSD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
			sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
			sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
			(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
			sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
			sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
			(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
			(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
			(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
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
		dt.sXauNoiMa , 
		dv.sTenDonVi,
		dt.iNamLamViec ,
		sum(isnull(dt.iQSBQNam, 0)) iQSBQNam,
		sum(isnull(dt.fLuongChinh, 0)) fLuongChinh,
		sum(isnull(dt.fPCChucVu, 0)) fPCChucVu,
		sum(isnull(dt.fPCTNNghe, 0)) fPCTNNghe,
		sum(isnull(dt.fPCTNVuotKhung, 0)) fPCTNVuotKhung,
		sum(isnull(dt.fNghiOm, 0)) fNghiOm,
		sum(isnull(dt.fHSBL, 0)) fHSBL,
		sum(isnull(dt.fTongQTLN, 0)) fTongQTLN,
		sum(isnull(dt.fDuToan, 0)) fDuToan,
		sum(isnull(dt.fDaQuyetToan, 0)) fDaQuyetToan,
		sum(isnull(dt.fConLai, 0)) fConLai,
		sum(isnull(dt.fThu_BHXH_NLD, 0)) fThu_BHXH_NLD,
		sum(isnull(dt.fThu_BHXH_NSD, 0)) fThu_BHXH_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHXH, 0)) fTongSoPhaiThuBHXH,
		sum(isnull(dt.fThu_BHYT_NLD, 0)) fThu_BHYT_NLD,
		sum(isnull(dt.fThu_BHYT_NSD, 0)) fThu_BHYT_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHYT, 0)) fTongSoPhaiThuBHYT,
		sum(isnull(dt.fThu_BHTN_NLD, 0)) fThu_BHTN_NLD,
		sum(isnull(dt.fThu_BHTN_NSD, 0)) fThu_BHTN_NSD,
		sum(isnull(dt.fTongSoPhaiThuBHTN, 0)) fTongSoPhaiThuBHTN,
		0 fTongNLD ,
		0 fTongNSD ,
		sum(isnull(dt.fTongCong, 0)) ,
		dv.iID_MaDonVi, 
		dv.sTenDonVi as TenDonVi 
	FROM #tempChiTietDonVi dt
	LEFT JOIN DonVi dv ON dv.iID_MaDonVi = dt.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
	group by
		dt.sXauNoiMa, 
		dv.sTenDonVi,
		dt.iNamLamViec,
		dv.iID_MaDonVi, 
		dv.sTenDonVi;

	SELECT * FROM #result ORDER BY sXauNoiMa , #result.MaDonVi;

	DROP TABLE #tempChiTietDonVi;
	DROP TABLE #result;

END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_nam]    Script Date: 4/5/2024 12:26:42 PM ******/
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
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020001' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 0 bHangCha, '9020002' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
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
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_quy]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	
	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
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
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHXH,
		sum(chungtudonvi.fThu_BHYT_NLD)/@Donvitinh fThu_BHYT_NLD,
		sum(chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fThu_BHYT_NSD,
		(sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHYT,
		sum(chungtudonvi.fThu_BHTN_NLD)/@Donvitinh fThu_BHTN_NLD,
		sum(chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fThu_BHTN_NSD,
		(sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongSoPhaiThuBHTN,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)))/@Donvitinh fTongNLD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongNSD,
		(sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0)))/@Donvitinh fTongCong
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
		and iNamLamViec = @NamLamViec) mlns
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
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]    Script Date: 4/5/2024 12:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_don_vi_thang]
	@NamLamViec int,
	@IdDonVis nvarchar(50),
	@Donvitinh int,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN

	DECLARE @SMonths NVARCHAR(50)
	DECLARE @SLoaiQuy NVARCHAR(50)

	IF (@IQuy = 3) SET @SMonths = '1,2,3'
	IF (@IQuy = 6) SET @SMonths = '4,5,6'
	IF (@IQuy = 9) SET @SMonths = '7,8,9'
	IF (@IQuy = 12) SET @SMonths = '10,11,12'

	declare @isCha bit;
	set @isCha = 0;
	SELECT @isCha = 1 from DONVI
	where iNamLamViec = @NamLamViec and iLoai = 0 and iTrangThai = 1 and iID_MaDonVi = @IdDonVis;

	select
		mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		sum(chungtudonvi.iQSBQNam) iQSBQNam,
		(sum(chungtudonvi.fLuongChinh))/@Donvitinh fLuongChinh,
		(sum(chungtudonvi.fPCChucVu))/@Donvitinh fPhuCapChucVu,
		(sum(chungtudonvi.fPCTNNghe))/@Donvitinh fPCTNNghe,
		(sum(chungtudonvi.fPCTNVuotKhung))/@Donvitinh fPCTNVuotKhung,
		(sum(chungtudonvi.fNghiOm))/@Donvitinh fNghiOm,
		(sum(chungtudonvi.fHSBL))/@Donvitinh fHSBL,
		((sum(isnull(chungtudonvi.fLuongChinh, 0)) + sum(isnull(chungtudonvi.fPCChucVu, 0)) + sum(isnull(chungtudonvi.fPCTNNghe, 0)) + sum(isnull(chungtudonvi.fPCTNVuotKhung, 0)) + sum(isnull(chungtudonvi.fNghiOm, 0)) + sum(isnull(chungtudonvi.fHSBL, 0))))/@Donvitinh fTongQTLN,
		(sum(chungtudonvi.fDuToan))/@Donvitinh fDuToan,
		(sum(chungtudonvi.fDaQuyetToan))/@Donvitinh fDaQuyetToan,
		(sum(chungtudonvi.fConLai))/@Donvitinh fConLai,
		(sum(chungtudonvi.fThu_BHXH_NLD))/@Donvitinh fThu_BHXH_NLD,
		(sum(chungtudonvi.fThu_BHXH_NSD))/@Donvitinh fThu_BHXH_NSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHXH,
		(sum(chungtudonvi.fThu_BHYT_NLD))/@Donvitinh fThu_BHYT_NLD,
		(sum(chungtudonvi.fThu_BHYT_NSD))/@Donvitinh fThu_BHYT_NSD,
		((sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHYT,
		(sum(chungtudonvi.fThu_BHTN_NLD))/@Donvitinh fThu_BHTN_NLD,
		(sum(chungtudonvi.fThu_BHTN_NSD))/@Donvitinh fThu_BHTN_NSD,
		((sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongSoPhaiThuBHTN,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0))))/@Donvitinh fTongNLD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongNSD,
		((sum(isnull(chungtudonvi.fThu_BHXH_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHXH_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHYT_NSD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NLD, 0)) + sum(isnull(chungtudonvi.fThu_BHTN_NSD, 0))))/@Donvitinh fTongCong
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
		and iNamLamViec = @NamLamViec) mlns
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
			and ct.iQuyNam in (SELECT * FROM f_split(@SMonths))
			and ct.iQuyNamLoai = @ILoaiQuy
			and ((@isCha = 0 and ctct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))) or (@isCha = 1 and ct.iID_MaDonVi in (SELECT * FROM f_split(@IdDonVis))))
			and ct.iLoaiTongHop = 1
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		group by
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.bHangCha,
			mlns.sXauNoiMa,
			mlns.sMoTa
		order by mlns.sXauNoiMa
END
;
;
;
;
GO