/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]    Script Date: 10/01/2024 2:57:04 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_tonghop_phanbo_theodot]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_tonghop_phanbo_theodot]    Script Date: 10/01/2024 2:57:04 PM ******/
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
	@UserName nvarchar(100)
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
			  OR (
			  --@CountDonViCha = 0
				 --AND 
				  sLNS in
					(SELECT DISTINCT VALUE
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
							 FROM f_split(@LNS)) ) LNS UNPIVOT (value
																FOR col in (LNS1, LNS3, LNS5, LNS)) un))) ) mlns
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
GO
