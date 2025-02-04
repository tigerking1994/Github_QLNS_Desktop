/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/15/2024 4:50:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_donvi_dieuchinh]    Script Date: 10/15/2024 4:50:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_phanbo_dutoan_get_donvi_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_donvi_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 10/15/2024 4:50:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo]    Script Date: 10/15/2024 4:50:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_danhsach_chungtu_tonghop]    Script Date: 10/15/2024 4:50:38 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dc_danhsach_chungtu_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dc_danhsach_chungtu_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dc_danhsach_chungtu_tonghop]    Script Date: 10/15/2024 4:50:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dc_danhsach_chungtu_tonghop]
	@YearOfWork int,
	@VoucherType int,
	@IdNhan uniqueidentifier,
	@YearOfBudget int,
	@BudgetSourceType int
AS
BEGIN
	
	SELECT 
			ct.*,
			dv.sTenDonVi
		FROM 
			(SELECT DISTINCT dc.* from
				(SELECT * FROM NS_DC_ChungTu 
				WHERE iNamLamViec = @YearOfWork AND iLoaiChungTu = @VoucherType AND iNamNganSach = @YearOfBudget AND iID_MaNguonNganSach = @BudgetSourceType) dc
				cross apply f_split(dc.sDSLNS) as dc_2
				WHERE exists (SELECT 1 from 
				(SELECT * FROM NS_DT_ChungTu 
					WHERE iID_DTChungTu = @IdNhan) dt
				WHERE ',' + dt.sDSLNS + ',' like '%,' + dc_2.Item + ',%')
			) ct
		JOIN 
			(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0) dv
		ON dv.iID_MaDonVi = ct.iID_MaDonVi
		ORDER BY sSoChungTu

END
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo]    Script Date: 10/15/2024 4:50:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(ISNULL(fTuChi, 0)) + SUM(ISNULL(fHienVat, 0)) + SUM(ISNULL(fPhanCap, 0)) + SUM(ISNULL(fHangNhap, 0)) + SUM(ISNULL(fHangMua, 0)) + SUM(ISNULL(fDuPhong, 0)) + SUM(ISNULL(fTonKho, 0)) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 on dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)
	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo,
		   npb.iID_ChungTuDieuChinh
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) > ISNULL(DaPhanBo, 0)
	ORDER BY npb.sSoChungTu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 10/15/2024 4:50:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			--AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND ((CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) AND (@Index <> iSoChungTuIndex)) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND ((CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) AND (@Index <> iSoChungTuIndex)) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo,
		   npb.iID_ChungTuDieuChinh IIDChungTuDieuChinh
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	--WHERE ISNULL(npb.SoPhanBo, 0) <> ISNULL(DaPhanBo, 0) OR ISNULL(npb.SoPhanBo, 0) = 0
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_donvi_dieuchinh]    Script Date: 10/15/2024 4:50:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_donvi_dieuchinh] 
	@IDs nvarchar(max),
	@NamLamViec int
AS
BEGIN

	declare @ChungTuDieuChinh nvarchar(max) = 
	(select distinct STUFF((
		SELECT distinct ',' + ctth.sTongHop
		FROM (select sTongHop from NS_DC_ChungTu
		where iID_DCChungTu in (SELECT * FROM f_split(@IDs))) ctth
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') sTongHop from
	(select sTongHop from NS_DC_ChungTu
	where iID_DCChungTu in (SELECT * FROM f_split(@IDs))) ctth)

	select distinct iID_MaDonVi from NS_DC_ChungTu
	where sSoChungTu in (SELECT * FROM f_split(@ChungTuDieuChinh))
	and iNamLamViec = @NamLamViec

END
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh]    Script Date: 10/15/2024 4:50:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ns_phanbo_dutoan_get_dulieu_dieuchinh] 
	@MaDonVi nvarchar(max),
	@NamLamViec int
AS
BEGIN
	select
		iID_DCCTChiTiet,
		bHangCha,
		dNgaySua,
		dNgayTao,
		fDuKienQtCuoiNam DuKienQtCuoiNam,
		fDuKienQtDauNam DuKienQtDauNam,
		iID_DCChungTu,
		iID_MaDonVi,
		iID_MaNguonNganSach,
		iID_MLNS,
		iID_MLNS_Cha,
		iNamLamViec,
		iNamNganSach,
		sGhiChu,
		sK,
		sL,
		sLNS,
		sM,
		sMoTa,
		sNG,
		sNguoiSua,
		sNguoiTao,
		sTM,
		sTNG,
		sTNG1,
		sTNG2,
		sTNG3,
		sTTM,
		sXauNoiMa,
		fDuToan DuToanNganSachNam,
		fDuToanChuyenNamSau DuToanChuyenNamSau

	from NS_DC_ChungTuChiTiet
	where iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and iNamLamViec = @NamLamViec
END
GO
