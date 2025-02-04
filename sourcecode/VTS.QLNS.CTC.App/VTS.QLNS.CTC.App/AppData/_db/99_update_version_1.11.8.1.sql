/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 30/08/2022 8:01:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_new_2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 30/08/2022 8:01:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_baocaodquyettoanniendo1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 30/08/2022 8:01:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovonchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovonchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 30/08/2022 8:01:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 30/08/2022 8:01:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
	@YearOfWork int,
	@BudgetSource int,
	@AgencyId nvarchar(100),
	@QuarterMonth nvarchar(100),
	@LNS nvarchar(max),
	@Dvt int,
	@IsInTongHop bit, 
	@IKhoi int
AS
BEGIN
declare @strChungTu nvarchar (500)
set @strChungTu=  (select sTongHop + ',' as 'data()' from NS_QT_ChungTu where  iID_MaDonVi in ( SELECT * FROM f_split(@AgencyId))  FOR XML PATH(''));
	
	SELECT * INTO #tempthongtridonvi
		FROM
		  (SELECT iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet
		   WHERE iNamLamViec = @YearOfWork
			 AND iID_MaNguonNganSach = @BudgetSource
			 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
			 AND (@QuarterMonth IS NULL OR iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
		   GROUP BY iID_MaDonVi)AS ct 
		-- lay ten don vi
		JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
	
	if (@IsInTongHop = 0 OR @strChungTu = '')
		select * from #tempthongtridonvi;
	else if (@IsInTongHop = 1 AND EXISTS (SELECT * FROM #tempthongtridonvi where iKhoi is not null))
		select * from #tempthongtridonvi where @IKhoi = -1 OR iKhoi = @IKhoi;
	else

	SELECT *
		FROM
		  (SELECT ctct.iID_MaDonVi,
				  TuChi = SUM(fTuChi_PheDuyet) / @Dvt,
				  cast(0 AS float) AS HienVat,
				  SoNguoi = SUM(fSoNguoi),
				  SoNgay = SUM(fSoNgay),
				  SoLuot = SUM(fSoLuot)
		   FROM NS_QT_ChungTuChiTiet ctct inner join ns_qt_Chungtu ct on  ctct.iID_QTChungTu =  ct.iID_QTChungTu 
		   WHERE ctct.iNamLamViec = @YearOfWork
			 AND ctct.iID_MaNguonNganSach = @BudgetSource
			 --AND (@AgencyId IS NULL OR ctct.iID_MaDonVi in  (select DonVi.iID_MaDonVi from DonVi  where DonVi.iID_Parent in ( SELECT * FROM f_split(@AgencyId))))
			 AND ct.bdatonghop = 1
			 AND (@QuarterMonth IS NULL OR ctct.iThangQuy in (SELECT * FROM f_split(@QuarterMonth)))
			 AND (@LNS IS NULL  OR sLNS in (SELECT *  FROM f_split(@LNS)))
			 AND ct.sSoChungTu in (select * from f_split(Replace(@strChungTu, ' ', '')))
		   GROUP BY ctct.iID_MaDonVi)AS ct 
		-- lay ten don vi
		--LEFT JOIN
		INNER JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id,
				  iKhoi
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovonchitiet]    Script Date: 30/08/2022 8:01:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_find_phanbovonchitiet]
@iIdPhanBoVon uniqueidentifier,
@dNgayLap datetime
AS
BEGIN
	select 
		distinct
		--pbvct.Id,
		da.iID_DuAnID,
		--pbvct.iID_PhanBoVonID,
		da.sTenDuAn,
		da.sMaDuAn,
		da.sTrangThaiDuAn,
		da.sKhoiCong,
		da.sKetThuc,
		da.sMaKetNoi,
		da.iID_MaDonViThucHienDuAnID as IIdMaDonViThucHienDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		lct.sTenLoaiCongTrinh,
		'' as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		pbvct.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		null as iID_CapPheDuyetID,
		pbvct.LNS as sLNS,
		pbvct.L as sL,
		pbvct.K as sK,
		pbvct.M as sM,
		pbvct.TM as sTM,
		pbvct.TTM as sTTM,
		pbvct.NG as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		pbvct.fCapPhatTaiKhoBac as FCapPhatTaiKhoBac,
		pbvct.fCapPhatTaiKhoBacDC as FCapPhatTaiKhoBacDC,
		pbvct.fCapPhatBangLenhChi as FCapPhatBangLenhChi,
		pbvct.fCapPhatBangLenhChiDC as FCapPhatBangLenhChiDC,
		pbvct.fGiaTriThuHoiNamTruocKhoBac as FGiaTriThuHoiNamTruocKhoBac,
		pbvct.fGiaTriThuHoiNamTruocKhoBacDC as FGiaTriThuHoiNamTruocKhoBacDC,
		pbvct.fGiaTriThuHoiNamTruocLenhChi as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoiNamTruocLenhChiDC as FGiaTriThuHoiNamTruocLenhChiDC,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVonID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc
	from
		VDT_KHV_PhanBoVon_ChiTiet pbvct
	inner join
		VDT_KHV_PhanBoVon pbv
	on pbvct.iID_PhanBoVonID = pbv.Id
	left join
		VDT_DA_DuAn da
	on
		pbvct.iID_DuAnID = da.iID_DuAnID
	left join
		VDT_DM_LoaiCongTrinh lct
	on
		pbvct.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi
	where
		pbvct.iID_PhanBoVonID = @iIdPhanBoVon
		--and pbv.bIsGoc = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_baocaodquyettoanniendo1]    Script Date: 30/08/2022 8:01:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_baocaodquyettoanniendo1]
@iIdMaDonVi nvarchar(100),
@iNamKeHoach int,
@iIdNguonVon int
AS
BEGIN
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn INTO #tmp
	FROM VDT_KHV_PhanBoVon as tbl
	INNER JOIN VDT_KHV_PhanBoVon_ChiTiet as dt on tbl.Id = dt.iID_PhanBoVonID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE da.iID_MaChuDauTuID = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamKeHoach AND tbl.iID_NguonVonID = @iIdNguonVon
	UNION ALL
	SELECT DISTINCT da.iID_DuAnID as IIDDuAnID, da.SMaDuAn, da.SDiaDiem, da.STenDuAn
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	WHERE iNamKeHoach = @iNamKeHoach AND (ISNULL(dt.fGiaTriNamTruocChuyenNamSau, 0) <> 0 OR ISNULL(dt.fGiaTriNamNayChuyenNamSau, 0) <> 0)


	-- Tong muc dau tu
	SELECT tmp.IIDDuAnID, SUM(ISNULL(qd.fTongMucDauTuPheDuyet, 0)) as fTongMucDauTu INTO #tmpTongMucDauTu
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	INNER JOIN VDT_DA_QDDauTu as qd on tmp.IIDDuAnID = qd.iID_DuAnID
	WHERE qd.BActive = 1
	GROUP BY tmp.IIDDuAnID

	---- Kho bac
	BEGIN
		-- TongHopDuLieu nam truoc
		SELECT tbl.IIDDuAnID, 
			SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
			SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
			SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
			SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
			SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruocKB
		FROM
		(
			SELECT tmp.IIDDuAnID,
				   (CASE WHEN (sMaDich = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
				   (CASE WHEN (sMaNguon = '403' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

				   (CASE WHEN (sMaDich = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
				   (CASE WHEN (sMaNguon = '413a' AND sMaNguonCha = '301' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

				   (CASE WHEN (sMaNguon = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
				   (CASE WHEN (sMaDich = '211c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('121a', '131')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach-1
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh
		) as tbl
		 GROUP BY tbl.IIDDuAnID

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNayKB
		FROM
		(
			SELECT  tmp.IIDDuAnID,  
					(CASE WHEN (sMaNguon = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '211a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('121a', '131') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,
					
					(CASE WHEN sMaNguon = '111' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '111' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '111' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,
					
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,
					
					(CASE WHEN sMaNguon = '101' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '101' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,
					
					(CASE WHEN (sMaDich = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '201' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,
					
					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '101' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '211a' AND sMaNguonCha = '111' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,

					(CASE WHEN (sMaDich = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
				   (CASE WHEN (sMaNguon = '121a' AND sMaNguonCha = '101' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach
		) as tbl
		GROUP BY tbl.IIDDuAnID
	
	END
	
	-- co quan tai chinh
	BEGIN
		SELECT tbl.IIDDuAnID, 
				SUM(ISNULL(fLuyKeThanhToanNamTruoc, 0)) fLuyKeThanhToanNamTruoc,
				SUM(ISNULL(fLuyKeThanhToanNamTruocDelete, 0)) fLuyKeThanhToanNamTruocDelete,
				
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruoc, 0)) FTamUngTheoCheDoChuaThuHoiNamTruoc,
				SUM(ISNULL(FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) FTamUngTheoCheDoChuaThuHoiNamTruocDelete,
				SUM(ISNULL(fDieuChinhGiamNamTruoc, 0)) fDieuChinhGiamNamTruoc,
				SUM(ISNULL(fDieuChinhGiamNamTruocDelete, 0)) fDieuChinhGiamNamTruocDelete INTO #tmpNamTruoc
			FROM
			(
				-- TongHopDuLieu nam truoc
				SELECT tmp.IIDDuAnID,
					(CASE WHEN (sMaDich = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruoc,
					(CASE WHEN (sMaNguon = '404' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fLuyKeThanhToanNamTruocDelete,

					

					(CASE WHEN (sMaDich = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
					(CASE WHEN (sMaNguon = '414a' AND sMaNguonCha = '302' AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as FTamUngTheoCheDoChuaThuHoiNamTruocDelete,

					(CASE WHEN (sMaNguon = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '100') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruoc,
					(CASE WHEN (sMaDich = '212c' AND (sMaNguonCha IS NULL OR sMaNguonCha NOT IN ('122a', '132')) AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fDieuChinhGiamNamTruocDelete
				FROM (SELECT DISTINCT * FROM #tmp) as tmp
				INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
				WHERE dt.iNamKeHoach = @iNamKeHoach -1
				GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, sMaTienTrinh
			) as tbl
		GROUP BY tbl.IIDDuAnID

		-- TongHopDuLieu nam nay
		SELECT tbl.IIDDuAnID,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNay, 0)) fTamUngNamTruocThuHoiNamNay,
			SUM(ISNULL(fTamUngNamTruocThuHoiNamNayDelete, 0)) fTamUngNamTruocThuHoiNamNayDelete,
			SUM(ISNULL(fKHVNamTruocChuyenNamNay, 0)) fKHVNamTruocChuyenNamNay,
			SUM(ISNULL(fKHVNamTruocChuyenNamNayDelete, 0)) fKHVNamTruocChuyenNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruoc, 0)) fTongThanhToanSuDungVonNamTruoc,
			SUM(ISNULL(fTongThanhToanSuDungVonNamTruocDelete, 0)) fTongThanhToanSuDungVonNamTruocDelete,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruoc, 0)) fTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fTamUngNamNayDungVonNamTruocDelete, 0)) fTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruoc, 0)) fThuHoiTamUngNamNayDungVonNamTruoc,
			SUM(ISNULL(fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) fThuHoiTamUngNamNayDungVonNamTruocDelete,
			SUM(ISNULL(fKHVNamNay, 0)) fKHVNamNay,
			SUM(ISNULL(fKHVNamNayDelete, 0)) fKHVNamNayDelete,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNay, 0)) fTongThanhToanSuDungVonNamNay,
			SUM(ISNULL(fTongThanhToanSuDungVonNamNayDelete, 0)) fTongThanhToanSuDungVonNamNayDelete,
			SUM(ISNULL(fThuHoiUngCacNamTruoc, 0)) fThuHoiUngCacNamTruoc,
			SUM(ISNULL(fThuHoiUngCacNamTruocDelete, 0)) fThuHoiUngCacNamTruocDelete,
			SUM(ISNULL(fKeHoachUngNamNay, 0)) fKeHoachUngNamNay,
			SUM(ISNULL(fKeHoachUngNamNayDelete, 0)) fKeHoachUngNamNayDelete,
			SUM(ISNULL(fTongTamUngNamNay, 0)) fTongTamUngNamNay,
			SUM(ISNULL(fTongTamUngNamNayDelete, 0)) fTongTamUngNamNayDelete,
			SUM(ISNULL(fTongThuHoiTamUngNamNay, 0)) fTongThuHoiTamUngNamNay,
			SUM(ISNULL(fTongThuHoiTamUngNamNayDelete, 0)) fTongThuHoiTamUngNamNayDelete,
			SUM(ISNULL(fThuHoiUngNamTruoc, 0)) fThuHoiUngNamTruoc,
			SUM(ISNULL(fThuHoiUngNamTruocDelete, 0)) fThuHoiUngNamTruocDelete,
			SUM(ISNULL(fTongThuHoiUngNamNay, 0)) fTongThuHoiUngNamNay,
			SUM(ISNULL(fTongThuHoiUngNamNayDelete, 0)) fTongThuHoiUngNamNayDelete INTO #tmpNamNay
		FROM
		(
			SELECT  tmp.IIDDuAnID, 
					(CASE WHEN (sMaNguon = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNay,
					(CASE WHEN (sMaDich = '212a' AND iThuHoiTUCheDo = 1 AND ILoaiUng = 1 AND sMaNguonCha NOT IN ('122a', '132') AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamTruocThuHoiNamNayDelete,

					(CASE WHEN sMaNguon = '112' AND sMaDich = '000' AND sMaTienTrinh = '100' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNay,
					(CASE WHEN sMaDich = '112' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamTruocChuyenNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '112' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamTruocDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo IS NULL AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiTamUngNamNayDungVonNamTruocDelete,

					(CASE WHEN sMaNguon = '102' AND sMaDich = '000' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNay,
					(CASE WHEN sMaDich = '102' AND sMaNguon = '000' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKHVNamNayDelete,

					(CASE WHEN (sMaDich = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNay,
					(CASE WHEN (sMaNguon = '202' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThanhToanSuDungVonNamNayDelete,
					
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngCacNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fKeHoachUngNamNayDelete,

					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNay,
					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '200' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNay,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '102' AND iThuHoiTUCheDo = 2 AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiTamUngNamNayDelete,

					(CASE WHEN (sMaNguon = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '200') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruoc,
					(CASE WHEN (sMaDich = '212a' AND sMaNguonCha = '112' AND iThuHoiTUCheDo = 1 AND sMaTienTrinh = '300') THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fThuHoiUngNamTruocDelete,
					
					(CASE WHEN (sMaDich = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = (CASE WHEN iID_NguonVonID = 1 THEN '100' ELSE '200' END) AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNay,
					(CASE WHEN (sMaNguon = '122a' AND sMaNguonCha = '102' AND sMaTienTrinh = '300' AND iID_NguonVonID = @iIdNguonVon  AND ISNULL(bKeHoach, 1) = (CASE WHEN iID_NguonVonID = 1 THEN ISNULL(bKeHoach, 1) ELSE 0 END)) THEN SUM(ISNULL(fGiaTri, 0)) ELSE 0 END) as fTongThuHoiUngNamNayDelete
			FROM (SELECT DISTINCT * FROM #tmp) as tmp
			INNER JOIN VDT_TongHop_NguonNSDauTu as dt on tmp.IIDDuAnID = dt.iID_DuAnID
			WHERE dt.iNamKeHoach = @iNamKeHoach
			GROUP BY tmp.IIDDuAnID, sMaDich, sMaNguon, sMaNguonCha, iID_NguonVonID, sMaTienTrinh, iThuHoiTUCheDo, ILoaiUng, bKeHoach
		) as tbl
		GROUP BY tbl.IIDDuAnID
	END

	
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(1 as int) as ICoQuanThanhToan,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc,
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNayKB as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruocKB as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID
	UNION ALL
	SELECT tmp.IIDDuAnID, tmp.SMaDuAn, tmp.SDiaDiem, tmp.STenDuAn, CAST(2 as int) as ICoQuanThanhToan,
		(ISNULL(nn.fKHVNamNay, 0) - ISNULL(nn.fKHVNamNayDelete, 0)) as FKHVNamNay, 
		(ISNULL(nn.fKHVNamTruocChuyenNamNay, 0) - ISNULL(nn.fKHVNamTruocChuyenNamNayDelete, 0)) as FKHVNamTruocChuyenNamNay, 
		(ISNULL(nn.fTamUngNamTruocThuHoiNamNay, 0) - ISNULL(nn.fTamUngNamTruocThuHoiNamNayDelete, 0)) as FTamUngNamTruocThuHoiNamNay,
		(ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiTamUngNamNayDungVonNamTruocDelete, 0)) as FThuHoiTamUngNamNayDungVonNamTruoc, 
		(ISNULL(nn.fTongTamUngNamNay, 0) - ISNULL(nn.fTongTamUngNamNayDelete, 0)) as FTongTamUngNamNay, 
		(((ISNULL(nn.fTongThanhToanSuDungVonNamNay, 0) - ISNULL(nn.fThuHoiUngCacNamTruoc, 0) + ISNULL(nn.fTongThuHoiUngNamNay, 0)) 
			- (ISNULL(nn.fTongThanhToanSuDungVonNamNayDelete, 0) - ISNULL(nn.fThuHoiUngCacNamTruocDelete, 0) + ISNULL(nn.fTongThuHoiUngNamNayDelete, 0)))) as FTongThanhToanSuDungVonNamNay,
		((ISNULL(nn.fTongThanhToanSuDungVonNamTruoc, 0) - ISNULL(nn.fThuHoiUngNamTruoc, 0)) - (ISNULL(nn.fTongThanhToanSuDungVonNamTruocDelete, 0) - ISNULL(nn.fThuHoiUngNamTruocDelete, 0))) as FTongThanhToanSuDungVonNamTruoc, 
		(ISNULL(nn.fTongThuHoiTamUngNamNay, 0) - ISNULL(nn.fTongThuHoiTamUngNamNayDelete, 0)) as FTongThuHoiTamUngNamNay, 
		(ISNULL(nn.fTamUngNamNayDungVonNamTruoc, 0) - ISNULL(nn.fTamUngNamNayDungVonNamTruocDelete, 0)) as FTamUngNamNayDungVonNamTruoc,
		(ISNULL(nt.fLuyKeThanhToanNamTruoc, 0) - ISNULL(nt.fLuyKeThanhToanNamTruocDelete, 0)) as FLuyKeThanhToanNamTruoc, 
		(ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruoc, 0) - ISNULL(nt.FTamUngTheoCheDoChuaThuHoiNamTruocDelete, 0)) as FTamUngTheoCheDoChuaThuHoiNamTruoc, 
		ISNULL(dt.fTongMucDauTu, 0) as FTongMucDauTu,
		CAST(0 as float) as FGiaTriTamUngDieuChinhGiam,
		CAST(0 as float) as FGiaTriNamTruocChuyenNamSau,
		CAST(0 as float) as FGiaTriNamNayChuyenNamSau
	FROM (SELECT DISTINCT * FROM #tmp) as tmp
	LEFT JOIN #tmpNamNay as nn on tmp.IIDDuAnID = nn.IIDDuAnID
	LEFT JOIN #tmpNamTruoc as nt on tmp.IIDDuAnID = nt.IIDDuAnID
	LEFT JOIN #tmpTongMucDauTu as dt on tmp.IIDDuAnID = dt.IIDDuAnID

	DROP TABLE #tmpNamTruoc
	DROP TABLE #tmpNamNay
	DROP TABLE #tmpNamTruocKB
	DROP TABLE #tmpNamNayKB
	DROP TABLE #tmp
	DROP TABLE #tmpTongMucDauTu
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]    Script Date: 30/08/2022 8:01:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_new_2]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh,
		pbvdvct.ILoaiDuAn as ILoaiDuAn
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvdvct
	INNER JOIN VDT_DA_DuAn as da on pbvdvct.iID_DuAnID = da.iID_DuAnID
	where
		pbvdvct.iID_PhanBoVon_DonVi_PheDuyet_ID in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

	select
		distinct
		null as IdChungTu,
		null as IdChungTuParent,
		cast(1 as bit) as BActive,
		cast(1 as bit) as IsGoc,
		da.iID_DuAnID as iID_DuAnID,
		da.sTenDuAn as sTenDuAn,
		da.sMaDuAn as sMaDuAn,
		da.sTrangThaiDuAn as sTrangThaiDuAn,
		da.sKhoiCong as sKhoiCong,
		da.sKetThuc as sKetThuc,
		lct.sTenLoaiCongTrinh as sTenLoaiCongTrinh,
		da.sMaKetNoi as sMaKetNoi,
		cda.sTen as sTenCapPheDuyet,
		cast(0 as float) as fGiaTriDauTu,
		tmp_da.iID_LoaiCongTrinh as iID_LoaiCongTrinhID,
		da.iID_CapPheDuyetID as iID_CapPheDuyetID,
		'' as sLNS,
		'' as sL,
		'' as sK,
		'' as sM,
		'' as sTM,
		'' as sTTM,
		'' as sNG,
		cast(0 as float) as fVonDaBoTri,
		cast(0 as float) as fVonConLai,
		cast(0 as float) as fChiTieuBoXungTrongNam,
		cast(0 as float) as fNamTruocChuyenSang,
		cast(0 as float) as fThuUngXDCB,
		cast(0 as float) as fChiTieuNganSach,
		'' as sGhiChu,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatTaiKhoBacDC,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FCapPhatBangLenhChiDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBacDC,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChiDC,
		cast(0 as float) as fChiTieuGoc,
		tmp_da.ILoaiDuAn as ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn
	from
		VDT_DA_DuAn da
	inner join
		#tmp_duan tmp_da
	on da.iID_DuAnID = tmp_da.iID_DuAnID
	left join 
		VDT_DM_PhanCapDuAn cda 
	on da.iID_CapPheDuyetID = cda.iID_PhanCapID
	left join
		VDT_DM_LoaiCongTrinh lct
	on tmp_da.iID_LoaiCongTrinh = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi

	drop table #tmp_duan

End
;
GO
