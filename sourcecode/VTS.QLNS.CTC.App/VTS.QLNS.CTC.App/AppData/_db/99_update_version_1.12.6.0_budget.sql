/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop]    Script Date: 17/02/2023 8:11:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 17/02/2023 8:11:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_LNS_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 17/02/2023 8:11:10 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_dutoan_tonghop]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_dutoan_tonghop]
GO
/****** Object:  UserDefinedFunction [dbo].[f_dutoan_tonghop]    Script Date: 17/02/2023 8:11:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[f_dutoan_tonghop]
(    
      @NamLamViec int,
	  @NamNganSach nvarchar(20),
	  @NguonNganSach nvarchar(20),
	  @NgayChungTu datetime,
	  @IdDonVi nvarchar(max),
	  @lns nvarchar(max)
)
RETURNS TABLE
AS RETURN

	SELECT iID_MLNS,
		   iID_MLNS_Cha,
		   iID_MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat)
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @NamLamViec
	  AND iDuLieuNhan = 0
	  AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
	  AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
	  AND (@IdDonVi IS NULL
		   OR iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)))
	  AND (@lns IS NULL
		   OR sLNS in (SELECT * FROM f_split(@lns)))
	  AND iID_DTChungTu in
		(SELECT iID_DTChungTu
		 FROM NS_DT_ChungTu
		 WHERE iNamLamViec = @NamLamViec
		   AND (@NamNganSach IS NULL OR iNamNganSach = @NamNganSach)
		   AND (@NguonNganSach IS NULL OR iID_MaNguonNganSach = @NguonNganSach)
		   AND CAST(dNgayQuyetDinh AS DATE) <= @NgayChungTu)
	GROUP BY iID_MLNS, iID_MLNS_Cha, iID_MaDonVi;
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 17/02/2023 8:11:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
	@ChungTuId nvarchar(4000),
	@IDDMCongKhai nvarchar(MAX),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@dvt int
AS
BEGIN
	SELECT 
	   --isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       --ctct.iID_DTChungTu,
       --mlns.iID_MLNS,
       --mlns.iID_MLNS_Cha,
       --mlns.sXauNoiMa,
       --mlns.sLNS,
       --mlns.sL,
       --mlns.sK,
       --mlns.sM,
       --mlns.sTM,
       --mlns.sTTM,
       --mlns.sNG,
       --mlns.sTNG,
       --mlns.sTNG1,
       --mlns.sTNG2,
       --mlns.sTNG3,
       --mlns.sMoTa,
       --mlns.bHangCha,
       --ctct.iNamNganSach,
       --ctct.iID_MaNguonNganSach,
       --ctct.iNamLamViec,
       --isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       --isnull(ctct.sGhiChu, '') AS sGhiChu,
       sum(isnull(ctct.fHangMua, 0)) / @dvt AS fHangMua,
       sum(isnull(ctct.fHangNhap, 0)) / @dvt AS fHangNhap,
       sum(isnull(ctct.fDuPhong, 0)) / @dvt AS fDuPhong,
       sum(isnull(ctct.fPhanCap, 0)) / @dvt AS fPhanCap,
       sum(isnull(ctct.fTuChi, 0)) / @dvt AS fTuChi,
       sum(isnull(ctct.fHienVat, 0)) / @dvt AS fHienVat,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   --ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   --isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   --mlns.sChiTietToi,
	   dv.sTenDonVi,
	   --mlns.bHangChaDuToan,
	   dmck.Id as iID_DMCongKhai,
	   dmck.iID_DMCongKhai_Cha as iID_DMCongKhai_Cha,
	   dmck.sMoTa
	FROM 
	(SELECT * FROM NS_DanhMucCongKhai WHERE Id in (select * from f_split(@IDDMCongKhai))) dmck
	LEFT JOIN NS_DMCongKhai_MLNS dmckmlns on dmckmlns.iID_DMCongKhai = dmck.Id and dmckmlns.iNamLamViec = dmck.iNamLamViec
	LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork AND bHangChaDuToan IS NOT NULL and iTrangThai = 1) mlns 
	on mlns.sXauNoiMa = dmckmlns.sNS_XauNoiMa and dmckmlns.iNamLamViec = mlns.iNamLamViec
	--LEFT JOIN (SELECT * FROM NS_DMCongKhai_MLNS WHERE iID_DMCongKhai in (select * from f_split(@IDDMCongKhai))) dmckmlns on mlns.sXauNoiMa = dmckmlns.sNS_XauNoiMa
	LEFT JOIN
	(
		SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iID_MaDonVi IS NOT NULL
			AND iDuLieuNhan = 0
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	GROUP BY ctct.iID_MaDonVi, ctct.iNamLamViec, dv.sTenDonVi, dmck.sMoTa, dmck.Id, dmck.iID_DMCongKhai_Cha
	--WHERE isnull(ctct.fTuChi, 0) > 0 OR isnull(ctct.fHienVat, 0) > 0 OR isnull(ctct.fPhanCap, 0) > 0 OR isnull(ctct.fHangNhap, 0) > 0 OR isnull(ctct.fHangMua, 0) > 0
	--ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop]    Script Date: 17/02/2023 8:11:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop]
	@ChungTuId nvarchar(4000),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100)
AS
BEGIN
	SELECT isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       ctct.iID_DTChungTu,
       mlns.iID_MLNS,
       mlns.iID_MLNS_Cha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sL,
       mlns.sK,
       mlns.sM,
       mlns.sTM,
       mlns.sTTM,
       mlns.sNG,
       mlns.sTNG,
       mlns.sTNG1,
       mlns.sTNG2,
       mlns.sTNG3,
       mlns.sMoTa,
       mlns.bHangCha,
       ctct.iNamNganSach,
       ctct.iID_MaNguonNganSach,
       ctct.iNamLamViec,
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       isnull(ctct.sGhiChu, '') AS sGhiChu,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
       ctct.dNgayTao,
       ctct.sNguoiTao,
       ctct.dNgaySua,
       ctct.sNguoiSua, 
	   ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   mlns.sChiTietToi,
	   dv.sTenDonVi,
	   mlns.bHangChaDuToan
	FROM 
		(select * from NS_MucLucNganSach 
		where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaDuToan is not null and 
		sLNS in 
				(
					SELECT 
						DISTINCT VALUE
					FROM 
					(
						SELECT 
							CAST(LEFT(Item, 1) AS nvarchar(10)) sLNS1, 
							CAST(LEFT(Item, 3) AS nvarchar(10)) sLNS3, 
							CAST(LEFT(Item, 5) AS nvarchar(10)) sLNS5, 
							CAST(Item AS nvarchar(10)) sLNS 
						FROM f_split(@LNS)
					) sLNS
					UNPIVOT
					(
					  value
					  FOR col in (sLNS1, sLNS3, sLNS5, sLNS)
					) un
				)
	) mlns
	LEFT JOIN
	(
		SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iDuLieuNhan = 0
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
;
GO
