/****** Object:  StoredProcedure [dbo].[sp_pbdt_rpt_chitieu_to_bia]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_pbdt_rpt_chitieu_to_bia]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_pbdt_rpt_chitieu_to_bia]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_all]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_nganh_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan_tng]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan_tng]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan_tng]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_tonghop_phanbo_theodot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_LNS_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS]    Script Date: 12/3/2024 6:00:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_LNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh]    Script Date: 12/4/2024 8:25:06 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone]    Script Date: 12/4/2024 9:11:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai_clone]    Script Date: 12/4/2024 9:11:08 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai_clone]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS]
	@ChungTuId nvarchar(4000),
	@LNS nvarchar(4000),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@UnitType int
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
       ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
       ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
       ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
       ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
       ROUND(isnull(ctct.fTuChi, 0)/@UnitType ,0) As fTuChi,
       ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,
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
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork AND bHangChaDuToan IS NOT NULL and iTrangThai = 1 and sLNS in (select * from f_split(@LNS))) mlns
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
	WHERE isnull(ctct.fTuChi, 0) <> 0 OR isnull(ctct.fHienVat, 0) <> 0 OR isnull(ctct.fPhanCap, 0) <> 0 OR isnull(ctct.fHangNhap, 0) <> 0 OR isnull(ctct.fHangMua, 0) <> 0
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 12/3/2024 6:00:52 PM ******/
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
       ROUND(sum(isnull(ctct.fHangMua, 0)) / @dvt,0) AS fHangMua,
       ROUND(sum(isnull(ctct.fHangNhap, 0)) / @dvt,0) AS fHangNhap,
       ROUND(sum(isnull(ctct.fDuPhong, 0)) / @dvt,0) AS fDuPhong,
       ROUND(sum(isnull(ctct.fPhanCap, 0)) / @dvt,0) AS fPhanCap,
       ROUND(sum(isnull(ctct.fTuChi, 0)) / @dvt,0) AS fTuChi,
       ROUND(sum(isnull(ctct.fHienVat, 0)) / @dvt,0) AS fHienVat,
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
	   dmck.sMoTa,
	   dmck.sMa
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
	GROUP BY ctct.iID_MaDonVi, ctct.iNamLamViec, dv.sTenDonVi, dmck.sMoTa, dmck.Id, dmck.iID_DMCongKhai_Cha, dmck.sMa
	--WHERE isnull(ctct.fTuChi, 0) > 0 OR isnull(ctct.fHienVat, 0) > 0 OR isnull(ctct.fPhanCap, 0) > 0 OR isnull(ctct.fHangNhap, 0) > 0 OR isnull(ctct.fHangMua, 0) > 0
	--ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop]    Script Date: 12/3/2024 6:00:52 PM ******/
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
	@SoChungTu nvarchar(100),
	@UnitType int
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
       ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
       ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
       ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
       ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
       ROUND(isnull(ctct.fTuChi, 0)/@UnitType,0) AS fTuChi,
       ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,
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
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_dieuchinh]
	@ChungTuId nvarchar(4000),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@UnitType int
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
	   ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
       ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
       ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
       ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
       ROUND(isnull(ctct.fTuChi, 0)/@UnitType,0) AS fTuChi,
       ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,
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
		(SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iDuLieuNhan = 0) 	
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@VoucherIndex int,
	@IsLuyKe bit,
	@UnitType int
AS
BEGIN
	DECLARE @tblDuToan table (iID_MLNS uniqueidentifier, fTuChi float, fHienVat float, fDuPhong float, fHangMua float, fHangNhap float, fPhanCap float, fTonKho float)
	-- số liệu dự toán
	IF @IsLuyKe = 0
		INSERT INTO @tblDuToan
		SELECT 
			iID_MLNS,
			ROUND(SUM(fTuChi)/@UnitType,0) AS fTuChi, 
			ROUND(SUM(fHienVat)/@UnitType,0) AS fHienVat,
			ROUND(SUM(fDuPhong)/@UnitType,0) AS fDuPhong,
			ROUND(SUM(fHangMua)/@UnitType,0) AS fHangMua,
			ROUND(SUM(fHangNhap)/@UnitType, 0) AS fHangNhap,
			ROUND(SUM(fPhanCap)/@UnitType ,0) AS fPhanCap,
			ROUND(SUM(fTonKho)/@UnitType ,0)AS fTonKho
		FROM (
			-- nhận dự toán
			SELECT iID_MLNS,
				SUM(fTuChi) AS fTuChi, 
				SUM(fHienVat) AS fHienVat,
				SUM(fDuPhong) AS fDuPhong,
				SUM(fHangMua) AS fHangMua,
				SUM(fHangNhap) AS fHangNhap,
				SUM(fPhanCap) AS fPhanCap,
				SUM(fTonKho) AS fTonKho
			FROM NS_DT_ChungTuChiTiet
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iID_DTChungTu in
					(SELECT DISTINCT iID_CTDuToan_Nhan
					FROM NS_DT_Nhan_PhanBo_Map
					WHERE iID_CTDuToan_PhanBo in (select * from f_split(@ChungTuId)))
				AND iDuLieuNhan = 0 
			group by iID_MLNS
			--UNION  
			---- phân bổ
			--SELECT 
			--	ctct.iID_MLNS, 
			--	0 - SUM(fTuChi) AS fTuChi, 
			--	0 - SUM(fHienVat) AS fHienVat,
			--	0 - SUM(fDuPhong) AS fDuPhong,
			--	0 - SUM(fHangMua) AS fHangMua,
			--	0 - SUM(fHangNhap) AS fHangNhap,
			--	0 - SUM(fPhanCap) AS fPhanCap,
			--	0 - SUM(fTonKho) AS fTonKho
			--FROM NS_DT_ChungTuChiTiet ctct
			--WHERE iID_DTChungTu IN 
			--	(SELECT iID_DTChungTu
			--	FROM NS_DT_ChungTu
			--	WHERE iNamLamViec = @YearOfWork
			--		and iNamNganSach = @YearOfBudget
			--		and iID_MaNguonNganSach = @BudgetSource
			--		and iLoai = 1
			--		AND 
			--		(
			--			(dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--			OR 
			--			(dNgayQuyetDinh IS NULL AND (CAST(dNgayChungTu AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--		)
			--	)
			--GROUP BY ctct.iID_MLNS
		) dt
		GROUP BY iID_MLNS
	ELSE
		INSERT INTO @tblDuToan
		SELECT iID_MLNS,
			ROUND(SUM(fTuChi)/@UnitType,0) AS fTuChi, 
			ROUND(SUM(fHienVat)/@UnitType,0) AS fHienVat,
			ROUND(SUM(fDuPhong)/@UnitType,0) AS fDuPhong,
			ROUND(SUM(fHangMua)/@UnitType,0) AS fHangMua,
			ROUND(SUM(fHangNhap)/@UnitType, 0) AS fHangNhap,
			ROUND(SUM(fPhanCap)/@UnitType ,0) AS fPhanCap,
			ROUND(SUM(fTonKho)/@UnitType ,0)AS fTonKho
		FROM NS_DT_ChungTuChiTiet
		WHERE 
			iID_DTChungTu IN 
			(
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iLoai = 0
				AND ((dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
					OR
					(dNgayQuyetDinh IS NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)))
			)
			AND iDuLieuNhan = 0 
		GROUP BY iID_MLNS

	SELECT 
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
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   mlns.sChiTietToi,
	   mlns.bHangChaDuToan
	FROM NS_MucLucNganSach mlns
	LEFT JOIN @tblDuToan ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND bHangChaDuToan is not null
	  AND mlns.sLNS in
		(SELECT *
		 FROM dbo.f_split(@LNS))
	  AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)
	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 08/12/2021 2:57:30 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_nhan_phan_bo_du_toan_chi_tiet_so_quyet_dinh_clone]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@VoucherIndex int,
	@IsLuyKe bit,
	@UnitType int

AS
BEGIN
	DECLARE @tblDuToan table (iID_MLNS uniqueidentifier, fTuChi float, fHienVat float, fDuPhong float, fHangMua float, fHangNhap float, fPhanCap float, fTonKho float)
	-- số liệu dự toán
	IF @IsLuyKe = 0
		INSERT INTO @tblDuToan
		SELECT 
			iID_MLNS,
			SUM(fTuChi) AS fTuChi, 
			SUM(fHienVat) AS fHienVat,
			SUM(fDuPhong) AS fDuPhong,
			SUM(fHangMua) AS fHangMua,
			SUM(fHangNhap) AS fHangNhap,
			SUM(fPhanCap) AS fPhanCap,
			SUM(fTonKho) AS fTonKho
		FROM (
			-- nhận dự toán
			SELECT iID_MLNS,
				ROUND(SUM(fTuChi)/@UnitType,0) AS fTuChi, 
				ROUND(SUM(fHienVat)/@UnitType,0) AS fHienVat,
				ROUND(SUM(fDuPhong)/@UnitType,0) AS fDuPhong,
				ROUND(SUM(fHangMua)/@UnitType,0) AS fHangMua,
				ROUND(SUM(fHangNhap)/@UnitType,0) AS fHangNhap,
				ROUND(SUM(fPhanCap)/@UnitType,0) AS fPhanCap,
				ROUND(SUM(fTonKho) /@UnitType,0)AS fTonKho
			FROM NS_DT_ChungTuChiTiet
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iID_DTChungTu in
					(SELECT DISTINCT iID_CTDuToan_Nhan
					FROM NS_DT_Nhan_PhanBo_Map
					WHERE iID_CTDuToan_PhanBo in (select * from f_split(@ChungTuId)))
				AND iDuLieuNhan = 0 
			group by iID_MLNS
			--UNION  
			---- phân bổ
			--SELECT 
			--	ctct.iID_MLNS, 
			--	0 - SUM(fTuChi) AS fTuChi, 
			--	0 - SUM(fHienVat) AS fHienVat,
			--	0 - SUM(fDuPhong) AS fDuPhong,
			--	0 - SUM(fHangMua) AS fHangMua,
			--	0 - SUM(fHangNhap) AS fHangNhap,
			--	0 - SUM(fPhanCap) AS fPhanCap,
			--	0 - SUM(fTonKho) AS fTonKho
			--FROM NS_DT_ChungTuChiTiet ctct
			--WHERE iID_DTChungTu IN 
			--	(SELECT iID_DTChungTu
			--	FROM NS_DT_ChungTu
			--	WHERE iNamLamViec = @YearOfWork
			--		and iNamNganSach = @YearOfBudget
			--		and iID_MaNguonNganSach = @BudgetSource
			--		and iLoai = 1
			--		AND 
			--		(
			--			(dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--			OR 
			--			(dNgayQuyetDinh IS NULL AND (CAST(dNgayChungTu AS DATE) <= cast(@VoucherDate AS DATE) AND iSoChungTuIndex < @VoucherIndex))
			--		)
			--	)
			--GROUP BY ctct.iID_MLNS
		) dt
		GROUP BY iID_MLNS
	ELSE
		INSERT INTO @tblDuToan
		SELECT iID_MLNS,
				ROUND(SUM(fTuChi)/@UnitType,0) AS fTuChi, 
				ROUND(SUM(fHienVat)/@UnitType,0) AS fHienVat,
				ROUND(SUM(fDuPhong)/@UnitType,0) AS fDuPhong,
				ROUND(SUM(fHangMua)/@UnitType,0) AS fHangMua,
				ROUND(SUM(fHangNhap)/@UnitType,0) AS fHangNhap,
				ROUND(SUM(fPhanCap)/@UnitType,0) AS fPhanCap,
				ROUND(SUM(fTonKho) /@UnitType,0)AS fTonKho
		FROM NS_DT_ChungTuChiTiet
		WHERE 
			iID_DTChungTu IN 
			(
				SELECT iID_DTChungTu FROM NS_DT_ChungTu
				WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND iLoai = 0
				AND ((dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@VoucherDate AS DATE))
					OR
					(dNgayQuyetDinh IS NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@VoucherDate AS DATE)))
			)
			AND iDuLieuNhan = 0 
		GROUP BY iID_MLNS

	SELECT 
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
	   ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
	   ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
	   ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
	   ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
	   ROUND(isnull(ctct.fTuChi, 0)/@UnitType,0) AS fTuChi,			
	   ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,

	   mlns.sChiTietToi,
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
	LEFT JOIN @tblDuToan ctct 
		ON mlns.iID_MLNS = ctct.iID_MLNS
		AND (isnull(ctct.fTuChi, 0) > 0
		   OR isnull(ctct.fHienVat, 0) > 0
		   OR isnull(ctct.fPhanCap, 0) > 0
		   OR isnull(ctct.fHangNhap, 0) > 0
		   OR isnull(ctct.fHangMua, 0) > 0)


	ORDER BY mlns.sXauNoiMa;
END
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 08/12/2021 2:57:30 PM ******/
SET ANSI_NULLS ON
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]
	@VoucherIds nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@UserName nvarchar(100),
	@UnitType int
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT @CountDonViCha = count(*)
	FROM
	  (SELECT *
	   FROM NguoiDung_DonVi
	   WHERE iID_MaNguoiDung = @UserName
		 AND iNamLamViec = @YearOfWork
		 AND iTrangThai = 1) nddv
	INNER JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND iLoai = 0) dv ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT DISTINCT VALUE INTO #tempLNS
	FROM
	(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
			CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
			CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
			CAST(sLNS AS nvarchar(10)) LNS
	FROM NS_NguoiDung_LNS
	WHERE sMaNguoiDung = @UserName
		AND iNamLamViec = @YearOfWork
		AND sLNS IN
		(SELECT *
			FROM f_split(@LNS))) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un

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
		   ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
	       ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
	       ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
	       ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
	       ROUND(isnull(ctct.fTuChi, 0)/@UnitType,0) AS fTuChi,			
	       ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,

		   ctct.dNgayTao,
		   ctct.sNguoiTao,
		   ctct.dNgaySua,
		   ctct.sNguoiSua, 
		   ctct.iID_CTDuToan_Nhan,
		   isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
		   mlns.sChiTietToi,
		   dv.sTenDonVi,
		   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
		 AND iTrangThai = 1
		 AND bHangChaDuToan IS NOT NULL
		 AND (
			 (@CountDonViCha <> 0
				   AND sLNS in
					 (SELECT *
					  FROM f_split(@LNS))) 
			  OR (sLNS in (select * from #tempLNS)))) mlns
	LEFT JOIN
	  (SELECT *
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iPhanCap = 1
		 AND iID_DTChungTu in (select * from f_split(@VoucherIds)) 
		 AND iDuLieuNhan = 0) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sXauNoiMa, ctct.iID_MaDonVi
END;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi]				
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @IdDonvi nvarchar(2000),
	 @IdChungTu nvarchar(4000),
	 @NgayQuyetDinh datetime,
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;
	 
SELECT LNS1 = Left(mlns.sLNS, 1),
       LNS3 = Left(mlns.sLNS, 3),
       LNS5 = Left(mlns.sLNS, 5),
       mlns.sLNS AS LNS,
       mlns.sL AS L,
       mlns.sK AS K,
       mlns.sM AS M,
       mlns.sTM AS TM,
       mlns.sTTM AS TTM,
       mlns.sNG AS NG,
       mlns.sTNG AS TNG,
	   mlns.sTNG1 AS TNG1,
	   mlns.sTNG2 AS TNG2,
	   mlns.sTNG3 AS TNG3,
       mlns.sXauNoiMa AS XauNoiMa,
       mlns.sMoTa AS MoTa ,
	   ct.iID_MaDonVi as MaDonVi,
	   '' as TenDonVi,
	   mlns.iID_MLNS as MlnsId,
	   mlns.iID_MLNS_Cha as MlnsIdParent,
       TuChi = ROUND(sum(fTuChi)/@Dvt,0) ,
       HienVat = ROUND(sum(fHienVat)/@Dvt,0),
	   DuPhong = ROUND(sum(fDuPhong)/@Dvt,0),
	   HangNhap = ROUND(sum(fHangNhap)/@Dvt,0),
	   HangMua = ROUND(sum(fHangMua)/@Dvt,0),
	   PhanCap = ROUND(sum(fPhanCap)/@Dvt,0),
	   RutKBNN = ROUND(sum(fRutKBNN)/@Dvt,0)
	FROM NS_DT_ChungTuChiTiet ct
		INNER JOIN NS_MucLucNganSach mlns ON ct.sXauNoiMa = mlns.sXauNoiMa
		AND (iID_DTChungTu in (select * from f_split(@IdChungTu)))
		AND mlns.iNamLamViec = @NamLamViec
	WHERE (@IdDonvi IS NULL
       OR ct.iID_MaDonVi in
         (SELECT *
          FROM f_split(@IdDonvi)))
GROUP BY mlns.sLNS,
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
         mlns.sXauNoiMa,
         mlns.sMoTa,
		 mlns.iID_MLNS,
		 mlns.iID_MLNS_Cha,
		 ct.iID_MaDonVi
HAVING sum(fTuChi) <> 0
OR sum(fHienVat) <> 0
OR sum(fDuPhong) <> 0
OR sum(fHangNhap) <> 0
OR sum(fHangMua) <> 0
OR sum(fPhanCap) <> 0
OR sum(fRutKBNN) <> 0
END
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan]	
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @NgayChungTu datetime,
	 @IdChungTu nvarchar(200),
	 @Dvt int
AS	 
BEGIN 
	SET NOCOUNT ON;

SELECT LNS1=left(LNS, 1),
       LNS3=left(LNS, 3),
       LNS5=left(LNS, 5),
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       MoTa,
	   MaDonVi,
	   '' as TenDonVi,
       '' AS TNG,
       XauNoiMa=LNS+'-'+L+'-'+K+'-'+M+'-'+TM+'-'+TTM+'-'+NG ,
       TuChi =ROUND(sum(TuChi - TuChi2)/@Dvt,0) ,
       HienVat =ROUND(sum(HienVat - HienVat2)/@Dvt,0),
	   DuPhong = ROUND(sum(DuPhong - DuPhong2)/@Dvt ,0),
	   HangNhap = ROUND(sum(HangNhap - HangNhap2)/@Dvt,0) ,
	   HangMua = ROUND(sum(HangMua - HangMua2)/@Dvt ,0),
	   PhanCap = ROUND(sum(PhanCap - PhanCap2)/@Dvt,0)
FROM
  (--phan bo toi thoi diem hien tai
SELECT sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa,
	   iID_MaDonVi AS MaDonVi,
       TuChi =sum(fTuChi),
       HienVat =sum(fHienVat),
	   DuPhong = sum(fDuPhong),
	   HangNhap = sum(fHangNhap),
	   HangMua = sum(fHangMua),
	   PhanCap = sum(fPhanCap),
       TuChi2 =0,
       HienVat2 =0,
	   DuPhong2 = 0,
	   HangNhap2 = 0,
	   HangMua2 = 0,
	   PhanCap2 = 0
   FROM NS_DT_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iNamNganSach=@NamNganSach
     AND iID_MaNguonNganSach=@NguonNganSach

     AND iID_DTChungTu in
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE iNamLamViec=@NamLamViec
          AND iNamNganSach=@NamNganSach
          AND iID_MaNguonNganSach=@NguonNganSach
          AND iLoai=0
          AND ((@IdChungTu IS NULL
                AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))
               OR (cast(dNgayChungTu AS date) = cast(@NgayChungTu AS date))))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
			iID_MaDonVi
   HAVING sum(fTuChi)<>0
   OR sum(fHienVat)<>0
   UNION ALL -- so da phan bo cho don vi
SELECT sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa,
	   iID_MaDonVi AS MaDonVi,
       TuChi =0,
       HienVat =0,
	   DuPhong = 0,
	   HangNhap = 0,
	   HangMua = 0,
	   PhanCap = 0,
       TuChi2 =sum(fTuChi),
       HienVat2 =sum(fHienVat),
	   DuPhong2 = sum(fDuPhong),
	   HangNhap2 = sum(fHangNhap),
	   HangMua2 = sum(fHangMua),
	   PhanCap2 = sum(fPhanCap)
   FROM NS_DT_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iNamNganSach=@NamNganSach
     AND iID_MaNguonNganSach=@NguonNganSach

     AND ((iID_DTChungTu=@IdChungTu)
          OR (@IdChungTu IS NULL
              AND iID_DTChungTu in
                (SELECT iID_DTChungTu
                 FROM NS_DT_ChungTu
                 WHERE iNamLamViec=@NamLamViec
                   AND iNamNganSach=@NamNganSach
                   AND iID_MaNguonNganSach=@NguonNganSach
                   AND iLoai=1
                   AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sMoTa,
			iID_MaDonVi
   HAVING sum(fTuChi)<>0
   OR sum(fHienVat)<>0) AS a
GROUP BY LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         MoTa,
		 MaDonVi
HAVING sum(TuChi - TuChi2)<>0
OR sum(HienVat - HienVat2)<>0
ORDER BY XauNoiMa END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan_tng]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_donvi_dutoan_tng]
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @NgayChungTu datetime,
	 @IdChungTu nvarchar(200),
	 @Dvt int
AS	 
BEGIN 
	SET NOCOUNT ON;

SELECT LNS1=left(LNS, 1),
       LNS3=left(LNS, 3),
       LNS5=left(LNS, 5),
       LNS,
       L,
       K,
       M,
       TM,
       TTM,
       NG,
       TNG,
       XauNoiMa,
       MoTa ,
	   MaDonVi,
	   '' as TenDonVi,
	   TuChi =ROUND(sum(TuChi - TuChi2)/@Dvt,0) ,
       HienVat =ROUND(sum(HienVat - HienVat2)/@Dvt,0),
	   DuPhong = ROUND(sum(DuPhong - DuPhong2)/@Dvt ,0),
	   HangNhap = ROUND(sum(HangNhap - HangNhap2)/@Dvt,0) ,
	   HangMua = ROUND(sum(HangMua - HangMua2)/@Dvt ,0),
	   PhanCap = ROUND(sum(PhanCap - PhanCap2)/@Dvt,0)


FROM
  (SELECT sLNS AS LNS,
          sL AS L,
          sK AS K,
          sM AS M,
          sTM AS TM,
          sTTM AS TTM,
          sNG AS NG,
          sTNG AS TNG,
          sXauNoiMa AS XauNoiMa,
          sMoTa AS MoTa,
		  iID_MaDonVi AS MaDonVi,
		   TuChi =sum(fTuChi),
		   HienVat =sum(fHienVat),
		   DuPhong = sum(fDuPhong),
		   HangNhap = sum(fHangNhap),
		   HangMua = sum(fHangMua),
		   PhanCap = sum(fPhanCap),
		   TuChi2 =0,
		   HienVat2 =0,
		   DuPhong2 = 0,
		   HangNhap2 = 0,
		   HangMua2 = 0,
		   PhanCap2 = 0
   FROM NS_DT_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iNamNganSach=@NamNganSach
     AND iID_MaNguonNganSach=@NguonNganSach
     AND iID_DTChungTu in
       (SELECT iID_DTChungTu
        FROM NS_DT_ChungTu
        WHERE iNamLamViec=@NamLamViec
          AND iNamNganSach=@NamNganSach
          AND iID_MaNguonNganSach=@NguonNganSach
          AND iLoai=0
          AND ((@IdChungTu IS NULL
                AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))
               OR (cast(dNgayChungTu AS date) = cast(@NgayChungTu AS date))))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sTNG,
            sXauNoiMa,
            sMoTa,
			iID_MaDonVi
   HAVING sum(fTuChi)<>0
   OR sum(fHienVat)<>0
   UNION ALL -- so da phan bo cho don vi
 SELECT sLNS AS LNS,
        sL AS L,
        sK AS K,
        sM AS M,
        sTM AS TM,
        sTTM AS TTM,
        sNG AS NG,
        sTNG AS TNG,
        sXauNoiMa AS XauNoiMa,
        sMoTa AS MoTa,
		iID_MaDonVi AS MaDonVi,
		TuChi =0,
		HienVat =0,
		DuPhong = 0,
		HangNhap = 0,
		HangMua = 0,
		PhanCap = 0,
		TuChi2 =sum(fTuChi),
		HienVat2 =sum(fHienVat),
		DuPhong2 = sum(fDuPhong),
		HangNhap2 = sum(fHangNhap),
		HangMua2 = sum(fHangMua),
		PhanCap2 = sum(fPhanCap)
   FROM NS_DT_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iNamNganSach=@NamNganSach
     AND iID_MaNguonNganSach=@NguonNganSach
     AND ((iID_DTChungTu=@IdChungTu)
          OR (@IdChungTu IS NULL
              AND iID_DTChungTu in
                (SELECT iID_DTChungTu
                 FROM NS_DT_ChungTu
                 WHERE iNamLamViec=@NamLamViec
                   AND iNamNganSach=@NamNganSach
                   AND iID_MaNguonNganSach=@NguonNganSach
                   AND iLoai=1
                   AND cast(dNgayChungTu AS date) <= cast(@NgayChungTu AS date))))
   GROUP BY sLNS,
            sL,
            sK,
            sM,
            sTM,
            sTTM,
            sNG,
            sTNG,
            sXauNoiMa,
            sMoTa,
			iID_MaDonVi
   HAVING sum(fTuChi)<>0
   OR sum(fHienVat)<>0) AS a
GROUP BY LNS,
         L,
         K,
         M,
         TM,
         TTM,
         NG,
         TNG,
         XauNoiMa,
         MoTa,
		 MaDonVi
HAVING sum(TuChi - TuChi2)<>0
OR sum(HienVat - HienVat2)<>0
ORDER BY XauNoiMa END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_all]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_all]			
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @Nganh nvarchar(2000),
	 @IdChungTu nvarchar(200),
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;

	DECLARE @index int = 0;
	DECLARE @ngayQuyetDinh Date;

	SELECT TOP 1 @index = iSoChungTuIndex,
	@ngayQuyetDinh = CASE WHEN dNgayQuyetDinh is not null THEN  CAST(dNgayQuyetDinh  AS DATE) 
	 ELSE CAST(dNgayChungTu  AS Date) 
	 END

	FROM NS_DT_ChungTu WHERE iID_DTChungTu 
	in (SELECT * FROM f_split(@IdChungtu))
	ORDER BY dNgayQuyetDinh DESC

	SELECT iID_DTChungTu INTO #tempIds 
	FROM NS_DT_ChungTu 
	WHERE iSoChungTuIndex <= @index 
		AND ((dNgayQuyetDinh is not null AND CAST(dNgayQuyetDinh AS DATE) <= @ngayQuyetDinh) or (dNgayQuyetDinh is null AND CAST(dNgayChungTu AS DATE) <= @ngayQuyetDinh))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach 
		AND iID_MaNguonNganSach = @NguonNganSach
		AND iLoai = 1
	 
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sTNG AS TNG,
	   sTNG1 AS TNG1,
	   sTNG2 AS TNG2,
	   sTNG3 AS TNG3,
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa ,
	   '' as MaDonVi,
	   '' as TenDonVi,
	   iID_MLNS as MlnsId,
	   iID_MLNS_Cha as MlnsIdParent,
       TuChi = ROUND( sum(fTuChi)/@Dvt,0),
       HienVat = ROUND(sum(fHienVat)/@Dvt,0),
	   DuPhong = ROUND(sum(fDuPhong)/@Dvt,0),
	   HangNhap = ROUND( sum(fHangNhap)/@Dvt,0),
	   HangMua = ROUND(sum(fHangMua)/@Dvt,0),
	   PhanCap = ROUND(sum(fPhanCap)/@Dvt,0),
	   TonKho = ROUND(sum(fTonKho)/@Dvt,0)
	FROM (
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE (@IsLuyKe = 0 AND iID_DTChungTu in (select * FROM f_split(@IdChungtu)))
			OR (@IsLuyKe = 1 AND iID_DTChungTu in (SELECT * FROM #tempIds))
		UNION ALL
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE
			(@IsLuyKe = 0 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo in (select * FROM f_split(@IdChungtu)))
				AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
			OR (@IsLuyKe = 1 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo IN (SELECT * FROM #tempIds))
			AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
		) ctct
	WHERE (@Nganh IS NULL
       OR sNG in
         (SELECT *
          FROM f_split(@Nganh)))
GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
		 sTNG1,
		 sTNG2,
		 sTNG3,
         sXauNoiMa,
         sMoTa,
		 iID_MLNS,
		 iID_MLNS_Cha
HAVING sum(fTuChi) <> 0
OR sum(fHienVat) <> 0
OR sum(fDuPhong) <> 0
OR sum(fHangNhap) <> 0
OR sum(fHangMua) <> 0
OR sum(fPhanCap) <> 0
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]			
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @Nganh nvarchar(2000),
	 @IdChungTu nvarchar(max),
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;

	DECLARE @index int = 0;
	DECLARE @ngayQuyetDinh Date;

	SELECT TOP 1 @index = iSoChungTuIndex,
	@ngayQuyetDinh = CASE WHEN dNgayQuyetDinh is not null THEN  CAST(dNgayQuyetDinh  AS DATE) 
	 ELSE CAST(dNgayChungTu  AS Date) 
	 END

	FROM NS_DT_ChungTu WHERE iID_DTChungTu 
	in (SELECT * FROM f_split(@IdChungtu))
	ORDER BY dNgayQuyetDinh DESC

	SELECT iID_DTChungTu INTO #tempIds 
	FROM NS_DT_ChungTu 
	WHERE iSoChungTuIndex <= @index 
		AND ((dNgayQuyetDinh is not null AND CAST(dNgayQuyetDinh AS DATE) <= @ngayQuyetDinh) or (dNgayQuyetDinh is null AND CAST(dNgayChungTu AS DATE) <= @ngayQuyetDinh))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach 
		AND iID_MaNguonNganSach = @NguonNganSach
		AND iLoai = 1
	 
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sTNG AS TNG,
	   sTNG1 AS TNG1,
	   sTNG2 AS TNG2,
	   sTNG3 AS TNG3,
       sXauNoiMa AS XauNoiMa,
       ctct.sMoTa AS MoTa ,
	   ctct.iID_MaDonVi as MaDonVi,
	   dv.sTenDonVi as TenDonVi,
	   iID_MLNS as MlnsId,
	   iID_MLNS_Cha as MlnsIdParent,
       TuChi = ROUND( sum(fTuChi)/@Dvt,0),
       HienVat = ROUND(sum(fHienVat)/@Dvt,0),
	   DuPhong = ROUND(sum(fDuPhong)/@Dvt,0),
	   HangNhap = ROUND( sum(fHangNhap)/@Dvt,0),
	   HangMua = ROUND(sum(fHangMua)/@Dvt,0),
	   PhanCap = ROUND(sum(fPhanCap)/@Dvt,0),
	   TonKho = ROUND(sum(fTonKho)/@Dvt,0)
	into #tempCtct
	FROM (
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE (@IsLuyKe = 0 AND iID_DTChungTu in (select * FROM f_split(@IdChungtu)))
			OR (@IsLuyKe = 1 AND iID_DTChungTu in (SELECT * FROM #tempIds))
		UNION ALL
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE
			(@IsLuyKe = 0 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo in (select * FROM f_split(@IdChungtu)))
				AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
			OR (@IsLuyKe = 1 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo IN (SELECT * FROM #tempIds))
			AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
		) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE (@Nganh IS NULL
       OR sNG in
         (SELECT *
          FROM f_split(@Nganh)))
GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
		 sTNG1,
		 sTNG2,
		 sTNG3,
         sXauNoiMa,
         ctct.sMoTa,
		 ctct.iID_MaDonVi,
		 dv.sTenDonVi,
		 iID_MLNS,
		 iID_MLNS_Cha
HAVING sum(fTuChi) <> 0
OR sum(fHienVat) <> 0
OR sum(fDuPhong) <> 0
OR sum(fHangNhap) <> 0
OR sum(fHangMua) <> 0
OR sum(fTonKho) <> 0
OR sum(fPhanCap) <> 0;

SELECT * FROM #tempCtct
UNION ALL
SELECT
		LNS1,
		LNS3,
		LNS5,
		LNS,
        L,
        K,
        M,
        TM,
        TTM,
        NG,
        TNG,
		TNG1,
		TNG2,
		TNG3,
        XauNoiMa,
        MoTa,
		'',
		'',
		MlnsId,
		MlnsIdParent,
		TuChi = SUM(TuChi),
        HienVat = SUM(HienVat),
	    DuPhong = SUM(DuPhong),
	    HangNhap = SUM(HangNhap),
	    HangMua = SUM(HangMua),
	    PhanCap = SUM(PhanCap),
		TonKho = SUM(TonKho)
FROM #tempCtct
GROUP BY 
LNS1,
		LNS3,
		LNS5,
		LNS,
        L,
        K,
        M,
        TM,
        TTM,
        NG,
        TNG,
		TNG1,
		TNG2,
		TNG3,
        XauNoiMa,
        MoTa,
		MlnsId,
		MlnsIdParent
ORDER BY XauNoiMa asc, MaDonVi desc;

DROP TABLE #tempIds;
DROP TABLE #tempCtct;
END
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_all]    Script Date: 14/12/2021 10:47:08 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]			
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @Nganh nvarchar(2000),
	 @IdChungTu nvarchar(max),
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;

	DECLARE @index int = 0;
	DECLARE @ngayQuyetDinh Date;

	SELECT TOP 1 @index = iSoChungTuIndex,
	@ngayQuyetDinh = CASE WHEN dNgayQuyetDinh is not null THEN  CAST(dNgayQuyetDinh  AS DATE) 
	 ELSE CAST(dNgayChungTu  AS Date) 
	 END

	FROM NS_DT_ChungTu WHERE iID_DTChungTu 
	in (SELECT * FROM f_split(@IdChungtu))
	ORDER BY dNgayQuyetDinh DESC

	SELECT iID_DTChungTu INTO #tempIds 
	FROM NS_DT_ChungTu 
	WHERE iSoChungTuIndex <= @index 
		AND ((dNgayQuyetDinh is not null AND CAST(dNgayQuyetDinh AS DATE) <= @ngayQuyetDinh) or (dNgayQuyetDinh is null AND CAST(dNgayChungTu AS DATE) <= @ngayQuyetDinh))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach 
		AND iID_MaNguonNganSach = @NguonNganSach
		AND iLoai = 1
	 
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
	   concat(sLNS,'-',sL,'-',sK,'-',sM,'-',sTM,'-',sTTM,'-',sNG) AS XauNoiMa,

	   ctct.iID_MaDonVi as MaDonVi,
	   dv.sTenDonVi as TenDonVi,

       TuChi = ROUND( sum(fTuChi)/@Dvt,0),
       HienVat = ROUND(sum(fHienVat)/@Dvt,0),
	   DuPhong = ROUND(sum(fDuPhong)/@Dvt,0),
	   HangNhap = ROUND( sum(fHangNhap)/@Dvt,0),
	   HangMua = ROUND(sum(fHangMua)/@Dvt,0),
	   PhanCap = ROUND(sum(fPhanCap)/@Dvt,0),
	   TonKho = ROUND(sum(fTonKho)/@Dvt,0)
	into #tempCtct
	FROM (
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE (@IsLuyKe = 0 AND iID_DTChungTu in (select * FROM f_split(@IdChungtu)))
			OR (@IsLuyKe = 1 AND iID_DTChungTu in (SELECT * FROM #tempIds))
		UNION ALL
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE
			(@IsLuyKe = 0 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo in (select * FROM f_split(@IdChungtu)))
				AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
			OR (@IsLuyKe = 1 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo IN (SELECT * FROM #tempIds))
			AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
		) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE (@Nganh IS NULL
       OR sNG in
         (SELECT *
          FROM f_split(@Nganh)))
		GROUP BY sLNS,
				 sL,
				 sK,
				 sM,
				 sTM,
				 sTTM,
				 sNG,
				 ctct.iID_MaDonVi,
				 dv.sTenDonVi--,
		HAVING sum(fTuChi) <> 0
		OR sum(fHienVat) <> 0
		OR sum(fDuPhong) <> 0
		OR sum(fHangNhap) <> 0
		OR sum(fHangMua) <> 0
		OR sum(fTonKho) <> 0
		OR sum(fPhanCap) <> 0;



		SELECT #tempCtct.*, mlns.sMoTa AS MoTa, mlns.iID_MLNS AS MlnsId, mlns.iID_MLNS_Cha AS MlnsIdParent FROM #tempCtct 
		LEFT JOIN (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on #tempCtct.XauNoiMa = mlns.sXauNoiMa

		ORDER BY LNS1, LNS3, LNS5, LNS, L, K, M, TM, TTM, NG asc, MaDonVi desc;

		DROP TABLE #tempIds;
		DROP TABLE #tempCtct;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_pbdt_rpt_chitieu_to_bia]    Script Date: 12/3/2024 6:00:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_pbdt_rpt_chitieu_to_bia]												
	 @IdChungtu nvarchar(4000),
	 @DonViTinh int
AS
BEGIN 
	SET NOCOUNT ON;
	SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sTNG AS TNG,
	   sTNG1 as TNG1,
	   sTNG2 as TNG2,
	   sTNG3 as TNG3,
       sXauNoiMa AS XauNoiMa,
       sMoTa AS MoTa ,
	   '' as MaDonVi,
	   '' as TenDonVi,
	   iId_Mlns as MlnsId,
	   iId_Mlns_Cha as MlnsIdParent,
	   TonKho = ROUND(sum(fTonKho) / @DonViTinh,0),
       TuChi = ROUND(sum(fTuChi) / @DonViTinh,0) ,
       HienVat = ROUND(sum(fHienVat) / @DonViTinh,0),
	   DuPhong = ROUND(sum(fDuPhong) / @DonViTinh,0),
	   HangNhap = ROUND(sum(fHangNhap) / @DonViTinh,0),
	   HangMua = ROUND(sum(fHangMua) / @DonViTinh,0),
	   PhanCap = ROUND(sum(fPhanCap) / @DonViTinh,0)
	FROM (
	SELECT * FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu in (select * from f_split(@IdChungtu))
		) ctct
	GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sTNG,
		 sTNG1,
	     sTNG2,
	     sTNG3,
         sXauNoiMa,
         sMoTa,
		 iId_Mlns,
		 iId_Mlns_Cha
	HAVING sum(fTuChi) <> 0
		OR sum(fHienVat) <> 0
		OR sum(fDuPhong) <> 0
		OR sum(fHangNhap) <> 0
		OR sum(fHangMua) <> 0
		OR sum(fPhanCap) <> 0
END
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all]    Script Date: 14/12/2021 10:46:53 AM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh]    Script Date: 12/4/2024 8:25:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_tong_hop_used_so_quyet_dinh]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@UnitType int
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
	   ROUND(isnull(ctct.fHangMua, 0)/@UnitType,0) AS fHangMua,
	   ROUND(isnull(ctct.fHangNhap, 0)/@UnitType,0) AS fHangNhap,
	   ROUND(isnull(ctct.fDuPhong, 0)/@UnitType,0) AS fDuPhong,
	   ROUND(isnull(ctct.fPhanCap, 0)/@UnitType,0) AS fPhanCap,
	   ROUND(isnull(ctct.fTuChi, 0)/@UnitType,0) AS fTuChi,			
	   ROUND(isnull(ctct.fHienVat, 0)/@UnitType,0) AS fHienVat,
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
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and bHangChaDuToan is not null and sLNS in (select * from f_split(@LNS))) mlns
	LEFT JOIN
	(
		SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (select * from f_split(@ChungTuId))
			AND iID_DTChungTu IN (select * from f_split(@ChungTuId))
			AND iID_CTDuToan_Nhan is not null
			AND iDuLieuNhan = 0
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE
		isnull(ctct.fTuChi, 0) <> 0 OR isnull(ctct.fHienVat, 0) <> 0 OR isnull(ctct.fPhanCap, 0) <> 0 OR isnull(ctct.fHangNhap, 0) <> 0 OR isnull(ctct.fHangMua, 0) <> 0
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai_clone]    Script Date: 12/4/2024 9:11:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai_clone]
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int,
	@VoucherIds nvarchar(max),
	@UnitType int
AS
BEGIN
	WITH tblDuToanDuocGiao AS (
		SELECT ROUND(SUM(ctct.fTuChi)/@UnitType,0) AS DuToanDuocGiao, dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai
		
		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 0 and iDuLieuNhan = 0
			AND ((@Time = 0  AND ct.iLoaiDuToan = 1) 
			OR @Time = 12
			   OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			   )
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
			AND ct.iID_DTChungTu in (SELECT * From f_split(@VoucherIds))
		GROUP BY dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	),

	tblSoPhanBo AS (
		SELECT ROUND(SUM(ctct.fTuChi)/@UnitType,0) AS SoPhanBo, dmck.iID_DMCongKhai, ctct.iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 1
			AND (
			      (@Time = 0  AND ct.iLoaiDuToan = 1) 
				OR @Time = 12
			    OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			)
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
			AND ct.iID_DotNhan in (SELECT * From f_split(@VoucherIds))

		GROUP BY dmck.iID_DMCongKhai, iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	)

	SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
		ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
		dmck.Id AS iID_DMCongKhai,
		dmck.iID_DMCongKhai_Cha,
		dv.iID_MaDonVi,
		dv.sTenDonVi
	FROM NS_DanhMucCongKhai dmck
	LEFT JOIN tblDuToanDuocGiao dtdg ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec
	WHERE dmck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (dmck.Id IN (SELECT * FROM f_split(@ListIdPublic)))

END
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone]    Script Date: 12/4/2024 9:11:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS_Clone]
	@iNamLamViec int,
	@iNamNganSach int,
	@iMaNguonNganSach int,
	@iQuarterMonths int,
	@sIdDanhMucCongKhai nvarchar(max),
	@dvt int,
	@sIdDotNhan nvarchar(max)
AS
BEGIN
	select   ROUND(sum(isnull(ctct.fTuChi,0))/@dvt,0) as fTuChi, dm_mlns.iID_DMCongKhai as iID_DMCongKhai
		into #temp
		from NS_DT_ChungTuChiTiet as ctct
		inner join NS_DMCongKhai_MLNS as dm_mlns on  dm_mlns.sNS_XauNoiMa = ctct.sXauNoiMa and  ctct.iNamLamViec = dm_mlns.iNamLamViec
		inner join NS_DT_ChungTu as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
		inner join NS_DanhMucCongKhai as dm on dm.Id =  dm_mlns.iID_DMCongKhai
		where ctct.iNamLamViec = @iNamLamViec and ctct.iID_MaNguonNganSach = @iMaNguonNganSach and ctct.iNamNganSach = @iNamNganSach
		and CT.iLoai = 0 and iDuLieuNhan = 0
		AND ((@iQuarterMonths = 0  AND ct.iLoaiDuToan = 1) 
			OR @iQuarterMonths = 12
			   OR (@iQuarterMonths <> 0 and (YEAR(ct.dNgayQuyetDinh) < @iNamLamViec or (MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths and YEAR(ct.dNgayQuyetDinh) = @iNamLamViec)))
			   )
			AND (dm.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)) OR (dm.Id IN (SELECT * FROM f_split(@sIdDanhMucCongKhai))))
		AND ct.iID_DTChungTu in (SELECT * FROM f_split(@sIdDotNhan))
		group by dm_mlns.iID_DMCongKhai

	

	select 
		dm.Id as Id_DanhMuc,
		dm.iID_DMCongKhai_Cha as Id_DanhMucCha,
		dm.STT as STT,
		dm.sMoTa as sMoTa,
		dm.bHangCha as bHangCha,
		dm.sMa as sMa,
		fTuChi as fTuChi
		from NS_DanhMucCongKhai as dm
		left join #temp as temp on dm.Id = temp.iID_DMCongKhai
		WHERE dm.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)) OR (dm.Id IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)))
		order by sMa
	
END
;
;
;
GO
