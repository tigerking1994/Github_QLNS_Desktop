/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 3/25/2024 11:00:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet]    Script Date: 3/25/2024 11:00:22 PM ******/
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
					ct.iNamLamViec,
					@LCS as LCS
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
;
GO
