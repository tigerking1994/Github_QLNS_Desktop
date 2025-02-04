/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 23/12/2022 7:56:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 23/12/2022 7:56:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]    Script Date: 23/12/2022 7:56:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 23/12/2022 7:56:54 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 23/12/2022 7:56:54 AM ******/
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
			AND ct.iLoai = 0 and iDuLieuNhan = 0
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
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]    Script Date: 23/12/2022 7:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_phan_bo_tong_hop]
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
		 AND iID_CTDuToan_Nhan = @ChungTuId
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
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]    Script Date: 23/12/2022 7:56:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_rpt_chi_tieu_luy_ke_tong_hop]
	@ChungTuId nvarchar(255),
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
		 AND iID_DTChungTu = @ChungTuId
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 23/12/2022 7:56:54 AM ******/
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
		and CT.iLoai = 0 and iDuLieuNhan = 0
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
