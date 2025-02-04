/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]    Script Date: 27/07/2023 11:18:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]    Script Date: 27/07/2023 11:18:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_config_budget_category]    Script Date: 27/07/2023 11:18:02 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_config_budget_category]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_config_budget_category]
GO
/****** Object:  StoredProcedure [dbo].[sp_config_budget_category]    Script Date: 27/07/2023 11:18:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_config_budget_category]

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE a
	SET a.iID_MLNS_Cha = b.iID_MLNS
	FROM ns_muclucngansach a
	INNER JOIN ns_muclucngansach b
	  ON (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '-', '%')
	  AND ((LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 3)
	  OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 5)
	  OR (LEN(a.sXauNoiMa) - LEN(b.sxaunoima) = 13)))
	  OR (a.sXauNoiMa LIKE CONCAT(b.sXauNoiMa, '%')
	  AND ((LEN(a.sXauNoiMa) = 3
	  AND LEN(b.sxaunoima) = 1)
	  OR (LEN(a.sXauNoiMa) = 7
	  AND LEN(b.sxaunoima) = 3)))
	  AND a.iNamLamViec = b.iNamLamViec
	WHERE a.iID_MLNS_Cha IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]    Script Date: 27/07/2023 11:18:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(max)
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
	   isnull(ctct.fTonKho, 0) AS fTonKho,
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
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_CTDuToan_Nhan IN (SELECT * FROM f_split(@ChungTuId))
		 AND iPhanCap = 1 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]    Script Date: 27/07/2023 11:18:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
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
	   isnull(ctct.fTonKho, 0) AS fTonKho,
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
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS in
		   (SELECT *
			FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId))
		 AND iPhanCap = 0 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
GO
