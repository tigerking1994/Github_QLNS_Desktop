/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 20/12/2022 6:28:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 20/12/2022 6:28:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 20/12/2022 6:28:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_data_mlns_by_type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_data_mlns_by_type]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_donvi]    Script Date: 20/12/2022 6:28:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_rpt_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_rpt_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 20/12/2022 6:30:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_data_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_data_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_used_mlns]    Script Date: 20/12/2022 6:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[sp_cp_rpt_donvi]    Script Date: 20/12/2022 6:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_cp_rpt_donvi]
@NamLamViec int,
@NamNganSach int,
@NguonNganSach int,
@IdDonvi nvarchar(2000),
@ctID nvarchar(max),
@NgayChungTu DateTime,
@UserName nvarchar(50),
@Dvt int
AS
BEGIN
	select isnull(sum(ctct.fTuChi),0) / @Dvt as fCapPhat, ctct.iID_MaDonVi, ctct.iID_MLNS
	into #temp
	from NS_CP_ChungTuChiTiet as ctct
	inner join NS_CP_ChungTu as ct on ctct.iID_CTCapPhat = ct.iID_CTCapPhat
	where ct.iNamLamViec = @NamLamViec
	and ct.iNamNganSach = @NamNganSach
	and (@NguonNganSach = 0 or ct.iID_MaNguonNganSach = @NguonNganSach)
	and ctct.iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi))
	and ct.iID_CTCapPhat in (select * from f_split(@ctID))	
	group by ctct.iID_MaDonVi, ctct.iID_MLNS;

	select ns.iID_MLNS, ns.iID_MLNS_Cha, ns.sLNS, ns.sL, ns.sK, ns.sM, ns.sTTM, ns.sNG, ns.bHangCha, tm.iID_MaDonVi, ns.sMoTa, tm.fCapPhat, ns.sXauNoiMa
	from  #temp as tm
	inner join NS_MucLucNganSach as ns on tm.iID_MLNS = ns.iID_MLNS
	where ns.iNamLamViec = @NamLamViec

end
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 20/12/2022 6:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_data_mlns_by_type] 
	@CodeChain nvarchar(max),
	@Type nvarchar(max),
	@VoucherID nvarchar(max)

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 'NHAN_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'PHAN_BO_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'DU_TOAN_DAU_NAM')
	BEGIN
	DELETE NS_DTDauNam_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTDTDauNamChiTiet = @VoucherID    
	
	DELETE NS_DTDauNam_PhanCap
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTDTDauNamChiTiet = @VoucherID
	END

	IF (@Type = 'PHAN_CAP_NGAN_SACH_NGANH')
	BEGIN
	DELETE NS_Nganh_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTNganhChiTiet = @VoucherID

	DELETE NS_Nganh_ChungTuChiTiet_PhanCap
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_CTNganhChiTiet = @VoucherID
	END

	IF (@Type = 'QUYET_TOAN')
	DELETE NS_QT_ChungTuChiTiet 
	WHERE sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	AND iID_QTCTChiTiet = @VoucherID

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 20/12/2022 6:28:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN
	WITH tblDuToanDuocGiao AS (
		SELECT SUM(ctct.fTuChi) AS DuToanDuocGiao, dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
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
			AND ct.iLoai = 1 and iDuLieuNhan = 0
			AND ((@Time = 0  AND ct.iLoaiDuToan = 1) 
			OR @Time = 12
			   OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			   )
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
		GROUP BY dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	),

	tblSoPhanBo AS (
		SELECT SUM(ctct.fTuChi) AS SoPhanBo, dmck.iID_DMCongKhai, ctct.iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 20/12/2022 6:28:24 PM ******/
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
		inner join NS_DMCongKhai_MLNS as dm_mlns on  dm_mlns.sNS_XauNoiMa = ctct.sXauNoiMa and  ctct.iNamLamViec = dm_mlns.iNamLamViec
		inner join NS_DT_ChungTu as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
		inner join NS_DanhMucCongKhai as dm on dm.Id =  dm_mlns.iID_DMCongKhai
		where ctct.iNamLamViec = @iNamLamViec and ctct.iID_MaNguonNganSach = @iMaNguonNganSach and ctct.iNamNganSach = @iNamNganSach
		and CT.iLoai = 1 and iDuLieuNhan = 0
		AND ((@iQuarterMonths = 0  AND ct.iLoaiDuToan = 1) 
			OR @iQuarterMonths = 12
			   OR (@iQuarterMonths <> 0 and (YEAR(ct.dNgayQuyetDinh) < @iNamLamViec or (MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths and YEAR(ct.dNgayQuyetDinh) = @iNamLamViec)))
			   )
			AND (dm.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)) OR (dm.Id IN (SELECT * FROM f_split(@sIdDanhMucCongKhai))))
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
GO

/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 20/12/2022 6:30:14 PM ******/
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
	NULL AS NgayQuyetDinh,
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
	ORDER BY LoaiChungTu, NgayChungTu, SoChungTu
	
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
	ORDER BY LoaiQuyetToan, NgayChungTu, SoChungTu
END
;
;

GO
