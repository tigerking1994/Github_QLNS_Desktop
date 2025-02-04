/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 11/6/2024 9:07:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 11/6/2024 9:07:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
	@agencies nvarchar(max) ,
	@LNS nvarchar(max) ,
	@YearOfWork int ,
	@YearOfBudget int  ,
	@BudgetSource int,
	@VoucherType int ,
	@DonViTinh int
AS
BEGIN
	IF(@VoucherType = 1)
	BEGIN
		SELECT
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		CAST(isnull(ctct.iNamNganSach, @YearOfBudget) as int) as INamNganSach,
		CAST(ctct.iID_MaNguonNganSach AS int)as IIdMaNguonNganSach,
		CAST(ctct.iNamLamViec AS int) INamLamViec,
		mlns.bHangCha,
		mlns.sChiTietToi as ChiTietToi,
		FDuToanNamKeHoach,
		FDuToanNamNay,
		FThucThuNamTruoc,
		FUocThucHienNamNay

	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
		SUM(isnull(fDuToan_NamKeHoach, 0)) / @DonViTinh as FDuToanNamKeHoach,
		SUM(isnull(fDuToan_NamNay, 0)) / @DonViTinh as FDuToanNamNay,
		SUM(isnull(fThucThu_NamTruoc, 0)) / @DonViTinh as FThucThuNamTruoc,
		SUM(isnull(fUocThucHien_NamNay, 0)) /@DonViTinh as FUocThucHienNamNay,
		iNamLamViec,iNamNganSach, iID_MaNguonNganSach,sXauNoiMa 
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_MaDonVi in (select * from dbo.splitstring(@agencies))
		GROUP BY iNamLamViec, iNamNganSach, iID_MaNguonNganSach, sXauNoiMa
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	ELSE
	BEGIN
		SELECT
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		CAST(isnull(ctct.iNamNganSach, @YearOfBudget) as int) as INamNganSach,
		CAST(ctct.iID_MaNguonNganSach AS int)as IIdMaNguonNganSach,
		CAST(ctct.iNamLamViec AS int) INamLamViec,
		mlns.bHangCha,
		ctct.iID_MaDonVi IIdMaDonVi,
		ctct.sTenDonVi as STenDonVi,
		mlns.sChiTietToi as ChiTietToi,
		FDuToanNamKeHoach,
		FDuToanNamNay,
		FThucThuNamTruoc,
		FUocThucHienNamNay

	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
		SUM(isnull(fDuToan_NamKeHoach, 0)) /@DonViTinh as FDuToanNamKeHoach,
		SUM(isnull(fDuToan_NamNay, 0))/@DonViTinh as FDuToanNamNay,
		SUM(isnull(fThucThu_NamTruoc, 0))/@DonViTinh as FThucThuNamTruoc,
		SUM(isnull(fUocThucHien_NamNay, 0))/@DonViTinh as FUocThucHienNamNay,
		iNamLamViec,iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach,sXauNoiMa ,sTenDonVi
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_MaDonVi in (select * from dbo.splitstring(@agencies))
		GROUP BY iNamLamViec, iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach, sXauNoiMa,sTenDonVi
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	
END
;
;
;
GO
