/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 11/4/2024 3:19:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitietHD4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitietHD4554_update_month]    Script Date: 11/4/2024 3:19:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitietHD4554_update_month]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitietHD4554_update_month]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitietHD4554_update_month]    Script Date: 11/4/2024 3:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_qt_chungtu_chitietHD4554_update_month]
	@VoucherId nvarchar(100),
	@Thang int,
	@LoaiThang int,
	@UserName nvarchar(100)
AS
BEGIN
	UPDATE
		TN_QuyetToan_ChungTuChiTiet_HD4554
	SET
		iThangQuy = @Thang,
		iThangQuyLoai = @LoaiThang,
		sNguoiSua = @UserName,
		dNgaySua = GetDate()
	WHERE iID_TN_QTChungTu = @VoucherId
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 11/4/2024 3:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.iID_TN_QTChungTu as IIdTnQtChungTu,
		mlns.iID_MLNS as iID_MLNS, 
		mlns.iID_MLNS_Cha as iID_MLNS_Cha,
		isnull(ctct.sNguoiTao, '') as sNguoiTao,
		isnull(ctct.sNguoiSua, '') as sNguoiSua,
		ctct.fSoTien,
		isnull(mlns.sMoTa, '') as sNoidung,
		isnull(ctct.iThangQuyLoai, 0) as IThangQuyLoai,
		isnull(ctct.iThangQuy, 1) as IThangQuy,
		isnull(ctct.bHangCha,mlns.bHangCha) as bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iNguonNganSach, @BudgetSource) as iNguonNganSach,
		mlns.iNamLamViec as iNamLamViec,
		mlns.iTrangThai as ITrangThai,
		isnull(ctct.iID_MaDonVi, '') as IIdMaDonVi,
		isnull(ctct.sGhiChu, '') as GhiChu,
		ctct.dNgayTao as dNgayTao,
		ctct.dNgaySua as dNgaySua,
		isnull(ctct.sK, mlns.sK) as sK,
		isnull(ctct.sLNS, mlns.sLNS) as sLNS,
		isnull(ctct.sL, mlns.sL) as sL,
		isnull(ctct.sM, mlns.sM) as sM,
		isnull(ctct.sNG, mlns.sNG) as sNG,
		isnull(ctct.sTM, mlns.sTM) as sTM,
		isnull(ctct.sTNG, mlns.sTNG) as sTNG,
		isnull(ctct.sTNG1, mlns.sTNG1) as sTNG1,
		isnull(ctct.sTNG2, mlns.sTNG2) as sTNG2,
		isnull(ctct.sTNG3, mlns.sTNG3) as sTNG3,
		isnull(ctct.sTTM, mlns.sTTM) as sTTM,
		mlns.sXauNoiMa
	FROM  (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_QuyetToan_ChungTuChiTiet_HD4554
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iNguonNganSach = @BudgetSource
			AND iID_TN_QTChungTu = @ChungTuId
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.sLNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sXauNoiMa
END
;
;
;
GO
