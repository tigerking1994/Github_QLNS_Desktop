/****** Object:  StoredProcedure [dbo].[sp_bh_nhan_dtt_dieu_tiet]    Script Date: 5/22/2024 11:07:06 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_nhan_dtt_dieu_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_nhan_dtt_dieu_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_nhan_dtt_dieu_tiet]    Script Date: 5/22/2024 11:07:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_nhan_dtt_dieu_tiet]
	@NamLamViec int,
	@ngayChungTu date
AS
BEGIN
	SELECT 
		ct.iID_MLNS,
		sum(ct.fThu_BHXH_NLD) fBHXHNLD,
		sum(ct.fThu_BHXH_NLD) fBHXHNSD,
		sum(ct.fThu_BHYT_NLD) fBHYTNLD,
		sum(ct.fThu_BHYT_NSD) fBHYTNSD,
		sum(ct.fThu_BHTN_NLD) fBHTNNLD,
		sum(ct.fThu_BHTN_NSD) fBHTNNSD
	FROM
		(
			SELECT
				ddv.sTenDonVi,
				bhct.iID_MLNS,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD,
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD
			FROM 
				BH_DTT_BHXH_ChungTu bh
			JOIN 
				BH_DTT_BHXH_ChungTu_ChiTiet bhct ON bh.iID_DTT_BHXH = bhct.iID_DTT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iLoaiDuToan = 1
				and bh.iNamLamViec = @NamLamViec
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
		) ct
		group by ct.iID_MLNS
END
;
;
;
GO