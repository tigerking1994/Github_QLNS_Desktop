/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 09/12/2022 7:20:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_exclude]    Script Date: 09/12/2022 7:20:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_mlns_exclude]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_mlns_exclude]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 09/12/2022 7:20:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 09/12/2022 7:20:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_data_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_data_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 09/12/2022 7:20:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_data_used_mlns] 
	@YearOfWork int,
	@CodeChain nvarchar(max),
	@Type bit

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 0)

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.LoaiChungTu,
		dulieu.Loai,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.SoQuyetDinh,
		dulieu.NgayQuyetDinh,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT 
	t1.iID_CTDTDauNamChiTiet AS ID,
	t3.iID_CTDTDauNam AS ID_Parent,
	N'DTDN' AS LoaiChungTu,
	'DU_TOAN_DAU_NAM' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	'' AS SoQuyetDinh,
	'' AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + ISNULL(t2.fTuChi, 0)) AS SoTien
	FROM NS_DTDauNam_ChungTuChiTiet t1
	LEFT JOIN NS_DTDauNam_PhanCap t2 ON t1.iID_CTDTDauNamChiTiet = t2.iID_CTDTDauNamChiTiet
	JOIN NS_DTDauNam_ChungTu t3 ON t1.iID_CTDTDauNam = t3.iID_CTDTDauNam
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Nhận DT' AS LoaiChungTu,
	'NHAN_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 0
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Phân bổ DT' AS LoaiChungTu,
	'PHAN_BO_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 1
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_CTNganhChiTiet AS ID,
	t3.iID_CTNganh AS ID_Parent, 
	N'Phân cấp NS ngành' AS LoaiChungTu,
	'PHAN_CAP_NGAN_SACH_NGANH' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	t3.sSoCongVan AS SoQuyetDinh,
	t3.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iiD_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + t2.fHienVat + t2.fPhanCap) AS SoTien
	FROM NS_Nganh_ChungTuChiTiet t1
	LEFT JOIN NS_Nganh_ChungTuChiTiet_PhanCap t2 ON t1.iID_CTNganhChiTiet = t2.iID_CTNganhChiTiet
	JOIN NS_Nganh_ChungTu t3 ON t1.iID_CTNganh = t3.iID_CTNganh
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiChungTu, NgayChungTu
	
	ELSE

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.ThangQuy,
		dulieu.Loai,
		dulieu.LoaiQuyetToan,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT
	t1.iID_QTCTChiTiet AS ID,
	t2.iID_QTChungTu AS ID_Parent,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.iID_MaDonVi AS MaDonVi,
	t2.iThangQuy AS ThangQuy,
	'QUYET_TOAN' AS Loai,
	CASE
		WHEN t2.sLoai = '101' THEN N'Thường xuyên'
		WHEN t2.sLoai = '1' THEN N'Quốc phòng'
		WHEN t2.sLoai = '2' THEN N'Nhà nước'
		WHEN t2.sLoai = '3' THEN N'Ngoại hối'
		WHEN t2.sLoai = '4' THEN N'Kinh phí khác'
		ELSE ''
	END AS LoaiQuyetToan,
	t2.sMoTa AS MoTa,
	t1.fTuChi_PheDuyet AS SoTien
	FROM NS_QT_ChungTuChiTiet t1
	JOIN NS_QT_ChungTu t2 ON t1.iID_QTChungTu = t2.iID_QTChungTu
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	ORDER BY LoaiQuyetToan, NgayChungTu
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 09/12/2022 7:20:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN
	WITH tblDuToanDuocGiao AS (
		SELECT SUM(ctct.fTuChi) AS DuToanDuocGiao, iID_DMCongKhai, ctct.iNamLamViec
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 0
			AND (((@Time = 0 OR @Time = 12) AND ct.iLoaiDuToan = 0) 
			   OR (@Time <> 0 AND MONTH(CAST(ct.dNgayChungTu AS DATE)) <= @Time))
			AND iID_DMCongKhai IN (SELECT * FROM f_split(@ListIdPublic))
		GROUP BY iID_DMCongKhai, ctct.iNamLamViec
	),

	tblSoPhanBo AS (
		SELECT SUM(ctct.fTuChi) AS SoPhanBo, iID_DMCongKhai, ctct.iID_MaDonVi, ctct.iNamLamViec
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 1
			AND (((@Time = 0 OR @Time = 12) AND ct.iLoaiDuToan = 0) 
			   OR (@Time <> 0 AND MONTH(CAST(ct.dNgayChungTu AS DATE)) <= @Time))
			AND iID_DMCongKhai IN (SELECT * FROM f_split(@ListIdPublic))
		GROUP BY iID_DMCongKhai, iID_MaDonVi, ctct.iNamLamViec
	)

	SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
		ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
		dtdg.iID_DMCongKhai,
		dv.iID_MaDonVi,
		dv.sTenDonVi
	FROM tblDuToanDuocGiao dtdg
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	LEFT JOIN NS_DanhMucCongKhai dmck ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec

END
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_exclude]    Script Date: 09/12/2022 7:20:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ns_mlns_exclude]
@iNamLamViec int,
@mlnsexclude t_tbl_string READONLY
AS
BEGIN
	SELECT iID as Id,
		BDuPhong as BDuPhong,
		BHangCha,
		BHangChaDuToan,
		BHangChaQuyetToan,
		BHangMua,
		BHangNhap,
		BHienVat,
		BNgay,
		BPhanCap,
		BSoNguoi,
		BTonKho,
		BTuChi,
		SChiTietToi,
		DNgaySua,
		DNgayTao,
		ILoai,
		ILock,
		ITrangThai,
		iID_MaDonVi as IdMaDonVi,
		iID_MaBQuanLy as IdPhongBan,
		SK as K,
		SL as L,
		sLNS as Lns,
		sM as M,
		iID_MLNS as MlnsId,
		iID_MLNS_Cha as MlnsIdParent,
		sMoTa as MoTa,
		iNamLamViec as NamLamViec,
		sNG as Ng,
		SCPChiTietToi,
		SDuToanChiTietToi,
		SNguoiSua,
		SNguoiTao,
		SNhapTheoTruong,
		SQuyetToanChiTietToi,
		Tag,
		sTM as Tm,
		sTNG as Tng,
		sTNG1 as Tng1,
		sTNG2 as Tng2,
		sTNG3 as Tng3,
		sTTM as Ttm,
		sXauNoiMa as XauNoiMa,
		ILoaiNganSach
	FROM NS_MucLucNganSach as tbl
	LEFT JOIN @mlnsexclude as ex on tbl.sXauNoiMa = ex.sId
	WHERE tbl.iNamLamViec = @iNamLamViec AND ex.sId IS NULL
END
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 09/12/2022 7:20:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
	@iNamLamViec int,
	@iNamNganSach int,
	@iMaNguonNganSach int,
	@iQuarterMonths int,
	@sIdDanhMucCongKhai nvarchar(max),
	@dvt int

AS
BEGIN
	select   sum(isnull(ctct.fTuChi,0))/@dvt as fTuChi, dm_mlns.iID_DMCongKhai as iID_DMCongKhai
		into #temp
		from NS_DT_ChungTuChiTiet as ctct
		inner join NS_DMCongKhai_MLNS as dm_mlns on  dm_mlns.sNS_XauNoiMa = ctct.sXauNoiMa
		inner join NS_DT_ChungTu as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
		where ct.iNamLamViec = @iNamLamViec and ct.iID_MaNguonNganSach = @iMaNguonNganSach and ct.iNamNganSach = @iNamNganSach
		and iLoai = 0
		and ((@iQuarterMonths = 0 and ct.iLoaiDuToan = 0) or (@iQuarterMonths <> 0 and MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths))
		and dm_mlns.iID_DMCongKhai IN (SELECT * FROM f_split(@sIdDanhMucCongKhai))
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
		where dm.iNamLamViec = @iNamLamViec 
		order by sMa
	
END
GO
