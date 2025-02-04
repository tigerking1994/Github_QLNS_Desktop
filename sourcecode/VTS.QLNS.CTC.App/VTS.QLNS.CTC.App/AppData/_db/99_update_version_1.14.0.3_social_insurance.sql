/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 2/23/2024 4:45:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 2/23/2024 4:45:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
	SELECT 
		ct.iID_DTT_BHXH_DieuChinh_ChiTiet,
		ct.iID_DTT_BHXH_DieuChinh,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sLNS,
		ct.sXauNoiMa,
		ct.sNoiDung,
		ct.fThuBHXH_NLD,
		ct.fThuBHXH_NSD,
		ct.fThuBHYT_NLD,
		ct.fThuBHYT_NSD,
		ct.fThuBHTN_NLD,
		ct.fThuBHTN_NSD,
		ct.fThuBHXH_NLD_QTDauNam,
		ct.fThuBHXH_NSD_QTDauNam,
		ct.fThuBHYT_NLD_QTDauNam,
		ct.fThuBHYT_NSD_QTDauNam,
		ct.fThuBHTN_NLD_QTDauNam,
		ct.fThuBHTN_NSD_QTDauNam,
		ct.fThuBHXH_NLD_QTCuoiNam,
		ct.fThuBHXH_NSD_QTCuoiNam,
		ct.fThuBHYT_NLD_QTCuoiNam,
		ct.fThuBHYT_NSD_QTCuoiNam,
		ct.fThuBHTN_NLD_QTCuoiNam,
		ct.fThuBHTN_NSD_QTCuoiNam,
		ct.fTongThuBHXH_NLD,
		ct.fTongThuBHXH_NSD,
		ct.fTongThuBHYT_NLD,
		ct.fTongThuBHYT_NSD,
		ct.fTongThuBHTN_NLD,
		ct.fTongThuBHTN_NSD,
		ct.fTongCong,
		ct.fThuBHXH_NLD_Tang,
		ct.fThuBHXH_NLD_Giam,
		ct.fThuBHXH_NSD_Tang,
		ct.fThuBHXH_NSD_Giam,
		(isnull(ct.fThuBHXH_NLD_Tang, 0) + isnull(ct.fThuBHXH_NSD_Tang, 0)) fThuBHXH_Tang,
		(isnull(ct.fThuBHXH_NLD_Giam, 0) + isnull(ct.fThuBHXH_NSD_Giam, 0)) fThuBHXH_Giam,
		ct.fThuBHYT_NLD_Tang,
		ct.fThuBHYT_NLD_Giam,
		ct.fThuBHYT_NSD_Tang,
		ct.fThuBHYT_NSD_Giam,
		(isnull(ct.fThuBHYT_NLD_Tang, 0) + isnull(ct.fThuBHYT_NSD_Tang, 0)) fThuBHYT_Tang,
		(isnull(ct.fThuBHYT_NLD_Giam, 0) + isnull(ct.fThuBHYT_NSD_Giam, 0)) fThuBHYT_Giam,
		ct.fThuBHTN_NLD_Tang,
		ct.fThuBHTN_NLD_Giam,
		ct.fThuBHTN_NSD_Tang,
		ct.fThuBHTN_NSD_Giam,
		(isnull(ct.fThuBHTN_NLD_Tang, 0) + isnull(ct.fThuBHTN_NSD_Tang, 0)) fThuBHTN_Tang,
		(isnull(ct.fThuBHTN_NLD_Giam, 0) + isnull(ct.fThuBHTN_NSD_Giam, 0)) fThuBHTN_Giam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu,
		ct.iNamLamViec
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTT_BHXH_DieuChinh bh
			JOIN 
				BH_DTT_BHXH_DieuChinh_ChiTiet bhct ON bh.iID_DTT_BHXH_DieuChinh = bhct.iID_DTT_BHXH_DieuChinh 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				and bh.iNamLamViec = @NamLamViec
		) ct;
END
;
GO
