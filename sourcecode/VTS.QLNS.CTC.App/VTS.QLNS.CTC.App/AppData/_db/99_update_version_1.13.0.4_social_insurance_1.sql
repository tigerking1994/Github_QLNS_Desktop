/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhxh_index]    Script Date: 7/25/2023 9:06:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_du_toan_thu_bhxh_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_du_toan_thu_bhxh_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_chi_tiet]    Script Date: 7/25/2023 9:06:46 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_chi_tiet]    Script Date: 7/25/2023 9:06:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dtt_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int
AS
BEGIN
SELECT 
		ct.iID_DTT_BHXH as DttBHXHId,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi IIdMaDonVi,
				bh.iNamLamViec,
				ddv.sTenDonVi,
				bhct.iID_DTT_BHXH_ChiTiet,
				bhct.iID_DTT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sLoaiDoiTuong,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				bhct.fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				bhct.fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
				bhct.fTongThuBHTN,
				bhct.fTongCong,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.iID_MLNS,
				bhct.iID_MLNS_Cha,
				bhct.sK,
				bhct.sL,
				bhct.sLNS,
				bhct.sM,
				bhct.sMoTa,
				bhct.sNG,
				bhct.sTM,
				bhct.sTNG,
				bhct.sTNG1,
				bhct.sTNG2,
				bhct.sTNG3,
				bhct.sTTM,
				bhct.sXauNoiMa,
				bhct.sGhiChu
			FROM 
				BH_DTT_BHXH_ChungTu bh
			JOIN 
				BH_DTT_BHXH_ChungTu_ChiTiet bhct ON bh.iID_DTT_BHXH = bhct.iID_DTT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_DTT_BHXH = @KhtBHXHId
		) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhxh_index]    Script Date: 7/25/2023 9:06:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_du_toan_thu_bhxh_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	CT.iID_DTT_BHXH,
	CT.sSoChungTu,
	CT.iNamLamViec,
	CT.dNgayChungTu,
	CT.iID_MaDonVi,
	CT.sMoTa,
	CT.bIsKhoa,
	CT.iLoaiDuToan,
	CT.sSoQuyetDinh,
	CT.dNgayQuyetDinh,
	CT.sNguoiTao,
	CT.sNguoiSua,
	CT.dNgayTao,
	CT.dNgaySua,
	CT.fBHXH_NLD FThuBHXHNLDDong,
	CT.fBHXH_NSD FThuBHXHNSDDong,
	CT.fTongBHXH FThuBHXH,
	CT.fBHYT_NLD FThuBHYTNLDDong,
	CT.fBHYT_NSD FThuBHYTNSDDong,
	CT.fTongBHYT FTongBHYT,
	CT.fBHTN_NLD FThuBHTNNLDDong,
	CT.fBHTN_NSD FThuBHTNNSDDong,
	CT.fTongBHTN FThuBHTN,
	CT.fDuToan,
	CT.sDSLNS
	FROM BH_DTT_BHXH_ChungTu CT
	WHERE CT.iNamLamViec = @YearOfWork
	ORDER BY CT.dNgayQuyetDinh DESC;
END;
;
;
GO
