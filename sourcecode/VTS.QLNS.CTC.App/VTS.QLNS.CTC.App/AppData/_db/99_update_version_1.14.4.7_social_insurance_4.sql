/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan]    Script Date: 5/16/2024 11:37:16 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan]    Script Date: 5/16/2024 11:37:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan]
@INamLamViec int
AS
BEGIN
	SELECT
		ct.iID_BH_TDQT_ChungTu,
		ct.iNamLamViec,
		ct.iID_MaDonVi,
		ct.fSoBaoCao,
		ct.fSoThamDinh,
		ct.fQuanNhan,
		ct.fCNVLDHD,
		ct.sSoChungTu,
		ct.dNgayChungTu,
		ct.bDaTongHop,
		ct.sTongHop,
		ct.bKhoa,
		ct.sNguoiTao,
		ct.sNguoiSua,
		ct.dNgayTao,
		ct.dNgaySua,
		ct.sMoTa,
		dv.sTenDonVi,
		ct.sGiaiThichChenhLech
	FROM BH_ThamDinhQuyetToan_ChungTu ct
	INNER JOIN DonVi dv ON ct.iID_MaDonVi = dv.iID_MaDonVi AND ct.iNamLamViec = dv.iNamLamViec
	WHERE ct.iNamLamViec = @INamLamViec
END
;
;
GO
