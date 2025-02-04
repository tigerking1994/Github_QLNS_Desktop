/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet_nam]    Script Date: 1/12/2024 3:59:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chi_tiet_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chi_tiet_nam]    Script Date: 1/12/2024 3:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chi_tiet_nam] 
	@ChungTuIds nvarchar(max),
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
					Sum(ctct.iQSBQNam) iQSBQNam,
					Sum(ctct.fLuongChinh) fLuongChinh,
					Sum(ctct.fPCChucVu) fPCChucVu,
					Sum(ctct.fPCTNNghe) fPCTNNghe,
					Sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
					Sum(ctct.fNghiOm) fNghiOm,
					Sum(ctct.fHSBL) fHSBL,
					Sum(ctct.fTongQTLN) fTongQTLN,
					Sum(ctct.fDuToan) fDuToan,
					Sum(ctct.fDaQuyetToan) fDaQuyetToan,
					Sum(ctct.fConLai) fConLai,
					Sum(ctct.fThu_BHXH_NLD) fThu_BHXH_NLD,
					Sum(ctct.fThu_BHXH_NSD) fThu_BHXH_NSD,
					Sum(ctct.fTongSoPhaiThuBHXH) fTongSoPhaiThuBHXH,
					Sum(ctct.fThu_BHYT_NLD) fThu_BHYT_NLD,
					Sum(ctct.fThu_BHYT_NSD) fThu_BHYT_NSD,
					Sum(ctct.fTongSoPhaiThuBHYT) fTongSoPhaiThuBHYT,
					Sum(ctct.fThu_BHTN_NLD) fThu_BHTN_NLD,
					Sum(ctct.fThu_BHTN_NSD) fThu_BHTN_NSD,
					Sum(ctct.fTongSoPhaiThuBHTN) fTongSoPhaiThuBHTN,
					Sum(ctct.fTongCong) fTongCong,
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
					ct.iID_QTT_BHXH_ChungTu IN (SELECT * FROM f_split(@ChungTuIds))
				GROUP BY ctct.iID_QTT_BHXH_ChungTu_ChiTiet ,ct.iID_QTT_BHXH_ChungTu,ct.iID_MaDonVi,ddv.sTenDonVi,ctct.sGhiChu,ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,ctct.sXauNoiMa,ctct.sLNS,ct.iNamLamViec
			) ct;
END
;
GO
