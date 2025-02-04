/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop_summary]
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_dot_summary]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@DonViTinh int
AS
BEGIN
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
	   ct.sSoQuyetDinh,
       isnull(ctct.iPhanCap, 0) / @DonViTinh AS iPhanCap,
       isnull(ctct.fHangMua, 0) / @DonViTinh AS fHangMua,
       isnull(ctct.fHangNhap, 0) / @DonViTinh AS fHangNhap,
       isnull(ctct.fDuPhong, 0) / @DonViTinh AS fDuPhong,
       isnull(ctct.fPhanCap, 0) / @DonViTinh AS fPhanCap,
       isnull(ctct.fTuChi, 0) / @DonViTinh AS fTuChi,
       isnull(ctct.fHienVat, 0) / @DonViTinh AS fHienVat,
	   isnull(ctct.fTonKho, 0) / @DonViTinh AS fTonKho,
	   isnull(ctpb.fTuChi, 0) / @DonViTinh AS fTuChiDaCap,
	   isnull(ctpb.fHienVat, 0) / @DonViTinh AS fHienVatDaCap,
	   ctct.iDuLieuNhan,
	   mlns.sChiTietToi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS IN (SELECT * FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT 
	    iID_DTChungTu,
		ISNULL(sXauNoiMa, 0) sXauNoiMa,
		ISNULL(iPhanCap, 0) iPhanCap,
		ISNULL(iDuLieuNhan, 0) iDuLieuNhan,
		SUM(ISNULL(fTuChi, 0)) fTuChi,
		SUM(ISNULL(fHienVat, 0)) fHienVat,
		SUM(ISNULL(fPhanCap, 0)) fPhanCap,
		SUM(ISNULL(fDuPhong, 0)) fDuPhong,
		SUM(ISNULL(fHangNhap, 0)) fHangNhap,
		SUM(ISNULL(fHangMua, 0)) fHangMua,
		SUM(ISNULL(fTonKho, 0)) fTonKho
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId)) 
		 AND iPhanCap = 0
		 AND iDuLieuNhan = 0
	   GROUP BY sXauNoiMa, iPhanCap, iDuLieuNhan, iID_DTChungTu) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	LEFT JOIN
	  (SELECT 
	    iID_CTDuToan_Nhan,
		ISNULL(sXauNoiMa, 0) sXauNoiMa,
		ISNULL(iPhanCap, 0) iPhanCap,
		ISNULL(iDuLieuNhan, 0) iDuLieuNhan,
		SUM(ISNULL(fTuChi, 0)) fTuChi,
		SUM(ISNULL(fHienVat, 0)) fHienVat,
		SUM(ISNULL(fPhanCap, 0)) fPhanCap,
		SUM(ISNULL(fDuPhong, 0)) fDuPhong,
		SUM(ISNULL(fHangNhap, 0)) fHangNhap,
		SUM(ISNULL(fHangMua, 0)) fHangMua,
		SUM(ISNULL(fTonKho, 0)) fTonKho
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND iID_CTDuToan_Nhan IN (SELECT * FROM f_split(@ChungTuId))
		 AND iPhanCap = 1
		 AND iDuLieuNhan = 0
	   GROUP BY sXauNoiMa, iPhanCap, iDuLieuNhan, iID_CTDuToan_Nhan) ctpb 
	   ON ctpb.iID_CTDuToan_Nhan = ctct.iID_DTChungTu AND ctpb.sXauNoiMa = mlns.sXauNoiMa
	LEFT JOIN
	   (SELECT * FROM NS_DT_ChungTu
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
	   ) ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]    Script Date: 02/01/2024 1:41:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop_summary]
	@ChungTuId nvarchar(max),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@IsPhanBo bit
AS
BEGIN
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
       isnull(ctct.iPhanCap, 0) AS iPhanCap,
       isnull(ctct.fHangMua, 0) AS fHangMua,
       isnull(ctct.fHangNhap, 0) AS fHangNhap,
       isnull(ctct.fDuPhong, 0) AS fDuPhong,
       isnull(ctct.fPhanCap, 0) AS fPhanCap,
       isnull(ctct.fTuChi, 0) AS fTuChi,
       isnull(ctct.fHienVat, 0) AS fHienVat,
	   isnull(ctct.fTonKho, 0) AS fTonKho,
	   ctct.iDuLieuNhan,
	   mlns.sChiTietToi,
	   mlns.bHangChaDuToan
	FROM
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iNamLamViec = @YearOfWork
	     AND bHangChaDuToan IS NOT NULL
		 AND iTrangThai = 1
		 AND sLNS IN (SELECT * FROM f_split(@LNS))) mlns
	LEFT JOIN
	  (SELECT 
		ISNULL(sXauNoiMa, 0) sXauNoiMa,
		ISNULL(iPhanCap, 0) iPhanCap,
		ISNULL(iDuLieuNhan, 0) iDuLieuNhan,
		SUM(ISNULL(fTuChi, 0)) fTuChi,
		SUM(ISNULL(fHienVat, 0)) fHienVat,
		SUM(ISNULL(fPhanCap, 0)) fPhanCap,
		SUM(ISNULL(fDuPhong, 0)) fDuPhong,
		SUM(ISNULL(fHangNhap, 0)) fHangNhap,
		SUM(ISNULL(fHangMua, 0)) fHangMua,
		SUM(ISNULL(fTonKho, 0)) fTonKho
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iNamLamViec = @YearOfWork
		 AND iNamNganSach = @YearOfBudget
		 AND iID_MaNguonNganSach = @BudgetSource
		 AND ((@IsPhanBo = 0 AND iID_DTChungTu IN (SELECT * FROM f_split(@ChungTuId)) AND iPhanCap = 0) 
		 OR (@IsPhanBo = 1 AND iID_CTDuToan_Nhan IN (SELECT * FROM f_split(@ChungTuId)) AND iPhanCap = 1))
		 AND iDuLieuNhan = 0
	   GROUP BY sXauNoiMa, iPhanCap, iDuLieuNhan) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa

	ORDER BY mlns.sXauNoiMa
END
;
;
;
;
;
GO
