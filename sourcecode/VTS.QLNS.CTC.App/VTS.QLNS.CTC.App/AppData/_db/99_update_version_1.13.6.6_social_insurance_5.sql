/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chi_tiet]    Script Date: 12/12/2023 10:24:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_bhyt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_bhyt_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 12/12/2023 10:24:22 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 12/12/2023 10:24:22 AM ******/
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
					ctct.sLNS,
					ct.iNamLamViec
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
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chi_tiet]    Script Date: 12/12/2023 10:24:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_bhyt_chi_tiet]
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
					ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
					ct.iID_QTTM_BHYT_ChungTu,
					ct.iID_MaDonVi,
					ddv.sTenDonVi,
					ctct.fDuToan,
					ctct.fDaQuyetToan,
					ctct.fConLai,
					ctct.fSoPhaiThu,
					ctct.sGhiChu,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS,
					ctct.iNamLamViec
				FROM 
					BH_QTTM_BHYT_Chung_Tu ct
				JOIN 
					BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					ct.iID_QTTM_BHYT_ChungTu = @ChungTuId
			) ct;
END
GO
