/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhyt_index]    Script Date: 8/15/2023 8:53:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_du_toan_thu_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_du_toan_thu_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dttm_bhyt_tn_chi_tiet]    Script Date: 8/15/2023 8:53:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dttm_bhyt_tn_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dttm_bhyt_tn_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dttm_bhyt_tn_chi_tiet]    Script Date: 8/15/2023 8:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dttm_bhyt_tn_chi_tiet]
	@dttmBHYTId nvarchar(100),
	@NamLamViec int
AS
BEGIN
SELECT 
		ct.iID_DTTM_BHYT_ThanNhan_ChiTiet ,
		ct.iID_DTTM_BHYT_ThanNhan ,
		ct.iID_MaDonVi,
		ct.sLNS,
		ct.iNamChungTu,
		ct.sNoiDung,
		ct.iID_MLNS,
		ct.fDuToan,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTTM_BHYT_ThanNhan bh
			JOIN 
				BH_DTTM_BHYT_ThanNhan_ChiTiet bhct ON bh.iID_DTTM_BHYT_ThanNhan = bhct.iID_DTTM_BHYT_ThanNhan 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_DTTM_BHYT_ThanNhan = @dttmBHYTId
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhyt_index]    Script Date: 8/15/2023 8:53:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_du_toan_thu_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	CT.iID_DTTM_BHYT_ThanNhan,
	CT.sSoChungTu,
	CT.iNamChungTu,
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
	CT.fDuToan ,
	CT.sDSLNS,
	CT.iID_DonVi
	FROM BH_DTTM_BHYT_ThanNhan CT 
	WHERE CT.iNamChungTu = @YearOfWork
	ORDER BY CT.dNgayQuyetDinh DESC;
END;
;
;
GO
