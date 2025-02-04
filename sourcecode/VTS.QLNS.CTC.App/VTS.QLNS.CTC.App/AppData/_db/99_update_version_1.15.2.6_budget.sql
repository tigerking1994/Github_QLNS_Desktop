/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 12/24/2024 2:41:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]    Script Date: 12/24/2024 2:41:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_chungtu_chitiet_dulieunhan]    Script Date: 12/24/2024 2:41:10 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_chungtu_chitiet_dulieunhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_chungtu_chitiet_dulieunhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_chungtu_chitiet_dulieunhan]    Script Date: 12/24/2024 2:41:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_chungtu_chitiet_dulieunhan]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(MAX),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@UserName nvarchar(100)
AS
BEGIN
	SELECT * INTO #ctct
	FROM NS_DT_ChungTuChiTiet
	WHERE iNamLamViec = @YearOfWork
	  AND iNamNganSach = @YearOfBudget
	  AND iID_MaNguonNganSach = @BudgetSource
	  AND iPhanCap = 0
	  AND iID_DTChungTu = @ChungTuId
	  AND iDuLieuNhan = 1;

	WITH CTE AS
	  (SELECT *
	   FROM NS_MucLucNganSach
	   WHERE iID_MLNS in
		   (SELECT iID_MLNS
			FROM #ctct)
		 AND iNamLamViec = @YearOfWork
	   UNION ALL SELECT p.*
	   FROM CTE c,
			NS_MucLucNganSach p
	   WHERE p.iID_MLNS= c.iID_MLNS_Cha
		 AND p.iNamLamViec = @YearOfWork )
	SELECT DISTINCT * INTO #mlns
	FROM CTE
	ORDER BY sXauNoiMa;

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
		   isnull(ctct.fTuChi, 0) AS fTuChi,
		   isnull(ctct.fRutKBNN, 0) AS fRutKBNN,
		   isnull(ctct.fHienVat, 0) AS fHienVat,
		   isnull(ctct.fHangMua, 0) AS fHangMua,
		   isnull(ctct.fHangNhap, 0) AS fHangNhap,
		   isnull(ctct.fDuPhong, 0) AS fDuPhong,
		   isnull(ctct.fPhanCap, 0) AS fPhanCap,
		   ctct.dNgayTao,
		   ctct.sNguoiTao,
		   ctct.dNgaySua,
		   ctct.sNguoiSua,
		   ctct.iID_CTDuToan_Nhan,
		   --ctct.Id_DotPhanBoTruoc,
		   isnull(ctct.iDuLieuNhan, 1) AS iDuLieuNhan,
		   mlns.sChiTietToi,
		   dv.sTenDonVi,
		   mlns.bHangChaDuToan
	FROM #mlns AS mlns
	LEFT JOIN #ctct AS ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	WHERE mlns.iNamLamViec = @YearOfWork
	  AND mlns.iTrangThai = 1
	  AND mlns.bHangChaDuToan IS NOT NULL
	ORDER BY mlns.sXauNoiMa;

	DROP TABLE #ctct;
	DROP TABLE #mlns;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]    Script Date: 12/24/2024 2:41:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_export_phan_bo_du_toan_chi_tiet]
	@ChungTuId nvarchar(255),
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
       isnull(ctct.fRutKBNN, 0) AS fRutKBNN,
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
		 AND iPhanCap = 1
		 AND iDuLieuNhan = 0
		 AND iID_DTChungTu = @ChungTuId ) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	ORDER BY mlns.sLNS,
			 mlns.sL,
			 mlns.sK,
			 mlns.sM,
			 mlns.sTM,
			 mlns.sTTM,
			 mlns.sNG,
			 mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 12/24/2024 2:41:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@IsPhanBo bit
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
		 AND iID_DTChungTu = @ChungTuId
		 AND ((@IsPhanBo = 0 AND iPhanCap = 0) OR (@IsPhanBo = 1 AND iPhanCap = 1))
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
