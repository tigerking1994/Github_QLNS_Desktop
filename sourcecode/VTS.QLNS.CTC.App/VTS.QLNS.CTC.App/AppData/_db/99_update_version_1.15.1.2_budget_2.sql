/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 11/18/2024 4:19:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_qt_chungtu_chitietHD4554]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_qt_chungtu_chitietHD4554]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_qt_chungtu_chitietHD4554]    Script Date: 11/18/2024 4:19:30 PM ******/
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
		mlns.bHangCha as bHangCha,
		isnull(ctct.iNamNganSach, @YearOfBudget) as iNamNganSach,
		isnull(ctct.iNguonNganSach, @BudgetSource) as iNguonNganSach,
		mlns.iNamLamViec as iNamLamViec,
		mlns.iTrangThai as ITrangThai,
		isnull(ctct.iID_MaDonVi, '') as IIdMaDonVi,
		isnull(ctct.sGhiChu, '') as GhiChu,
		ctct.dNgayTao as dNgayTao,
		ctct.dNgaySua as dNgaySua,
		 mlns.sK as sK,
		 mlns.sLNS as sLNS,
		 mlns.sL as sL,
		 mlns.sM as sM,
		 mlns.sNG as sNG,
		 mlns.sTM as sTM,
		 mlns.sTNG as sTNG,
		 mlns.sTNG1 as sTNG1,
		 mlns.sTNG2 as sTNG2,
		 mlns.sTNG3 as sTNG3,
		 mlns.sTTM as sTTM,
		mlns.sXauNoiMa,
		mlns.sQuyetToanChiTietToi,
		mlns.sChiTietToi

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
;
GO
