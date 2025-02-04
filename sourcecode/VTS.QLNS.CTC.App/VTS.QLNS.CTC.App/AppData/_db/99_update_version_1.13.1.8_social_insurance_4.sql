/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_bhxh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_du_toan_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_da_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_mlns_get_by_user]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_mlns_get_by_user]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_mlns_get_by_user]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 9/21/2023 4:32:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 9/21/2023 4:32:23 PM ******/
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
	SELECT 
		ct.*
		FROM
			(
				SELECT
					ctct.iID_QTT_BHXH_ChungTu_ChiTiet,
					ct.iID_QTT_BHXH_ChungTu,
					ct.iID_MaDonVi,
					ddv.sTenDonVi,
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
					ctct.sLNS
				FROM 
					BH_QTT_BHXH_ChungTu ct
				JOIN 
					BH_QTT_BHXH_ChungTu_ChiTiet ctct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					ct.iID_QTT_BHXH_ChungTu = @ChungTuId
			) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO BH_QTT_BHXH_ChungTu_ChiTiet
           (iID_QTT_BHXH_ChungTu,
		  iQSBQNam,
		  fLuongChinh,
		  fPCChucVu,
		  fPCTNNghe,
		  fPCTNVuotKhung,
		  fNghiOm,
		  fHSBL,
		  fTongQTLN,
		  fDuToan,
		  fDaQuyetToan,
		  fConLai,
		  fThu_BHXH_NLD,
		  fThu_BHXH_NSD,
		  fTongSoPhaiThuBHXH,
		  fThu_BHYT_NLD,
		  fThu_BHYT_NSD,
		  fTongSoPhaiThuBHYT,
		  fThu_BHTN_NLD,
		  fThu_BHTN_NSD,
		  fTongSoPhaiThuBHTN,
		  fTongCong,
		  sGhiChu,
		  iID_MLNS,
		  iID_MLNS_Cha,
		  sXauNoiMa,
		  sLNS)
    SELECT 
			@VoucherId,
			Sum(ctct.iQSBQNam),
			Sum(ctct.fLuongChinh),
			Sum(ctct.fPCChucVu),
			Sum(ctct.fPCTNNghe),
			Sum(ctct.fPCTNVuotKhung),
			Sum(ctct.fNghiOm),
			Sum(ctct.fHSBL),
			Sum(ctct.fTongQTLN),
			Sum(ctct.fDuToan),
			Sum(ctct.fDaQuyetToan),
			Sum(ctct.fConLai),
			Sum(ctct.fThu_BHXH_NLD),
			Sum(ctct.fThu_BHXH_NSD),
			Sum(ctct.fTongSoPhaiThuBHXH),
			Sum(ctct.fThu_BHYT_NLD),
			Sum(ctct.fThu_BHYT_NSD),
			Sum(ctct.fTongSoPhaiThuBHYT),
			Sum(ctct.fThu_BHTN_NLD),
			Sum(ctct.fThu_BHTN_NSD),
			Sum(ctct.fTongSoPhaiThuBHTN),
			Sum(ctct.fTongCong),
			null,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa
	WHERE ctct.iID_QTT_BHXH_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	AND mlns.iNamLamViec = @YearOfWork
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS;

	-- Danh dau chung tu da tong hop
	UPDATE BH_QTT_BHXH_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTT_BHXH_ChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_mlns_get_by_user]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_mlns_get_by_user] 
	@UserName nvarchar(100),
	@YearOfWork int,
	@LNSExcept nvarchar(max)
AS
BEGIN
	WITH LNSTree
		AS
		(
			SELECT *
			FROM BH_DM_MucLucNganSach
			WHERE 
				sL = ''
				AND iNamLamViec = @YearOfWork
				AND (sLNS <> '' and (sLNS like '901%' or sLNS like '902%'))
				AND ((@LNSExcept <> '' AND sLNS not in (SELECT * FROM f_split(@LNSExcept))) OR (@LNSExcept = ''))
			UNION ALL
			SELECT
				mlnsChild.*
			FROM BH_DM_MucLucNganSach mlnsChild
			INNER JOIN LNSTree
				ON mlnsChild.iID_MLNS_Cha = lnstree.iID_MLNS
			WHERE 
				mlnsChild.sL = '' 
				AND mlnsChild.iNamLamViec = @YearOfWork
				AND ((@LNSExcept <> '' AND mlnsChild.sLNS not in (SELECT * FROM f_split(@LNSExcept))) OR (@LNSExcept = ''))
		)
		SELECT 
			mlns.* 
		FROM ( SELECT DISTINCT * FROM LNSTree) mlns
	 --   INNER JOIN 
		--(
		--	SELECT 
		--		DISTINCT VALUE
		--	FROM 
		--	(
		--		SELECT 
		--			CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1, 
		--			CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3, 
		--			CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5, 
		--			CAST(sLNS AS nvarchar(10)) LNS 
		--		FROM
		--			NS_NguoiDung_LNS 
		--		WHERE 
		--			sMaNguoiDung = @UserName 
		--			AND iNamLamViec = @YearOfWork
		--	) LNS
		--	UNPIVOT
		--	(
		--	  value
		--	  FOR col in (LNS1, LNS3, LNS5, LNS)
		--	) un) lns
		--ON mlns.sLNS = lns.value
		ORDER BY sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
	@NamLamViec int
AS
BEGIN
	select 
	ctct.iID_MucLucNganSach iID_MLNS,
	sum(ctct.iQSBQNam) iQSBQNam,
	sum(ctct.fLuongChinh) fLuongChinh,
	sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
	sum(ctct.fPCTNNghe) fPCTNNghe,
	sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
	sum(ctct.fNghiOm) fNghiOm,
	--sum(ctct.fHSBL) fHSBL,
	sum(ctct.fTongQTLN) fTongQTLN
	from
	(select iID_KHT_BHXH from BH_KHT_BHXH
	where iNamChungTu = @NamLamViec
	and iLoaiTongHop = 1) ct
	join BH_KHT_BHXH_ChiTiet ctct
	on ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
	group by 
	ctct.iID_MucLucNganSach
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50),
	@QuyNamLoai int
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
			SUM(ctctdqt.fTongCong) fDaQuyetToan
			FROM BH_QTT_BHXH_ChungTu ctdqt
			JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctctdqt ON ctdqt.iID_QTT_BHXH_ChungTu = ctctdqt.iID_QTT_BHXH_ChungTu
			WHERE ctdqt.iNamLamViec = @NamLamViec
			AND ctdqt.iID_MaDonVi = @MaDonVi
			AND ctdqt.iQuyNam < (SELECT iQuyNam from BH_QTT_BHXH_ChungTu WHERE iID_QTT_BHXH_ChungTu = @ChungTuId)
			AND ctdqt.iQuyNamLoai = @QuyNamLoai
			GROUP BY ctctdqt.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
	ctct.iID_MLNS,
	sum(ctct.fTongCong) fTongCong
	from
	BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi = @MaDonVi
	group by
	ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_bhxh_index]    Script Date: 9/21/2023 4:32:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_thu_bhxh_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	ct.iID_QTT_BHXH_ChungTu,
	ct.iID_MaDonVi IIDMaDonVi,
	dv.sTenDonVi AS sTenDonVi,
	ct.iNamLamViec,
	ct.iLoaiTongHop,
	ct.bDaTongHop,
	ct.bIsKhoa,
	ct.sSoChungTu,
	ct.dNgayChungTu,
	ct.iQuyNam,
	ct.iQuyNamLoai,
	ct.sQuyNamMoTa,
	ct.sMoTa,
	ct.sNguoiTao,
	ct.sNguoiSua,
	ct.dNgayTao,
	ct.dNgaySua,
	ct.sDS_MLNS SDsMlns,
	ct.sTongHop,
	ct.iQSBQNam,
	ct.fLuongChinh,
	ct.fPCChucVu,
	ct.fPCTNNghe,
	ct.fPCTNVuotKhung,
	ct.fNghiOm,
	ct.fHSBL,
	ct.fTongQTLN,
	ct.fDuToan,
	ct.fDaQuyetToan,
	ct.fConLai,
	ct.fThu_BHXH_NLD,
	ct.fThu_BHXH_NSD,
	ct.fTongSoPhaiThuBHXH,
	ct.fThu_BHYT_NLD,
	ct.fThu_BHYT_NSD,
	ct.fTongSoPhaiThuBHYT,
	ct.fThu_BHTN_NLD,
	ct.fThu_BHTN_NSD,
	ct.fTongSoPhaiThuBHTN,
	ct.fTongCong,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD) FSoPhaiThuBHXHNLD,
	(ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FSoPhaiThuBHXHNSD,
	(ct.fThu_BHXH_NLD + ct.fThu_BHYT_NLD + ct.fThu_BHTN_NLD + ct.fThu_BHXH_NSD + ct.fThu_BHYT_NSD + ct.fThu_BHTN_NSD) FTongPhaiThuBHXH
	
	FROM BH_QTT_BHXH_ChungTu ct
	LEFT JOIN DonVi dv
	ON ct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE dv.iNamLamViec = @YearOfWork
END
GO
