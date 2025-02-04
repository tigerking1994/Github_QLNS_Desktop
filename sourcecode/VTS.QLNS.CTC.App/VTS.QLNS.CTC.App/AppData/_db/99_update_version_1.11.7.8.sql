/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 26/08/2022 8:07:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 26/08/2022 8:07:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_thongtri_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_thongtri_donvi]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 26/08/2022 8:07:07 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_LNS_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 26/08/2022 8:07:07 AM ******/
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
       sum(isnull(ctct.fHangMua, 0)) / @dvt AS fHangMua,
       sum(isnull(ctct.fHangNhap, 0)) / @dvt AS fHangNhap,
       sum(isnull(ctct.fDuPhong, 0)) / @dvt AS fDuPhong,
       sum(isnull(ctct.fPhanCap, 0)) / @dvt AS fPhanCap,
       sum(isnull(ctct.fTuChi, 0)) / @dvt AS fTuChi,
       sum(isnull(ctct.fHienVat, 0)) / @dvt AS fHienVat,
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
	   dmck.sMoTa
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
	GROUP BY ctct.iID_MaDonVi, ctct.iNamLamViec, dv.sTenDonVi, dmck.sMoTa
	--WHERE isnull(ctct.fTuChi, 0) > 0 OR isnull(ctct.fHienVat, 0) > 0 OR isnull(ctct.fPhanCap, 0) > 0 OR isnull(ctct.fHangNhap, 0) > 0 OR isnull(ctct.fHangMua, 0) > 0
	--ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_thongtri_donvi]    Script Date: 26/08/2022 8:07:07 AM ******/
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
	if (@IsInTongHop = 0 OR @strChungTu = '')
		SELECT *
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
		LEFT JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
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
		LEFT JOIN
		  (SELECT sTenDonVi,
				  iID_MaDonVi AS dv_id
		   FROM DonVi
		   WHERE iTrangThai = 1
		     AND (@IKhoi = -1 OR iKhoi = @IKhoi)
			 AND iNamLamViec = @YearOfWork) AS dv ON dv.dv_id = ct.iID_MaDonVi
		ORDER BY iID_MaDonVi
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 26/08/2022 8:07:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
	@iNamKeHoach int, 
	@ngayLap DateTime,
	@maDonViQuanLyId nvarchar(50),
	@nguonVonID int,
	@filterHasQDDT int
AS
Begin
	Select
		da.iID_DuAnID, 
		da.sTenDuAn,
		da.sMaDuAn,
		nv.iID_MaNguonNganSach,
		CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
		da.iID_CapPheDuyetID,
		pc.sTen as sTenCapPheDuyet,
		case 
			when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
		end as iID_LoaiCongTrinhID,
		lct.sTenLoaiCongTrinh, 
		cdt.sTenDonVi as sTenChuDauTu,
		da.iID_DonViTienTeID,
		da.iID_TienTeID,
		da.fTiGiaDonVi,
		da.fTiGia,
		da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
		dvthda.sTenDonVi as STenDonViThucHienDuAn,
		da.dDateCreate INTO #tmp
		from VDT_DA_DuAn da
		inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
		Where  da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId)) And [dbo].[fn_CheckDieuKienDuAn](da.iID_DuAnID,@ngayLap) = 1 
			And 
			( @filterHasQDDT is null  -- search all
			
			or

				( -- search có quyết định đầu tư
					(@filterHasQDDT = 1 and da.iID_DuAnID in (SELECT DISTINCT qqdt.iID_DuAnID FROM VDT_DA_QDDauTu qqdt JOIN VDT_DA_QDDauTu_NguonVon qddtnv ON qqdt.iID_QDDauTuID=qddtnv.iID_QDDauTuID 
													JOIN NguonNganSach nv ON qddtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 
															
													WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID))
					) or
					-- search k có quyết định đầu tư nhưng có chủ trương đầu tư
					(@filterHasQDDT = 0 and da.iID_DuAnID in 
						(SELECT DISTINCT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt 
							join VDT_DA_ChuTruongDauTu_NguonVon ctdtnv on ctdt.iID_ChuTruongDauTuID = ctdtnv.iID_ChuTruongDauTuID
							JOIN NguonNganSach nv ON ctdtnv.iID_NguonVonID=nv.iID_MaNguonNganSach 															
							WHERE  (@nguonVonID IS NULL OR nv.iID_MaNguonNganSach=@nguonVonID)
							and ctdt.iID_DuAnID not in ( select iID_DuAnID from VDT_DA_QDDauTu)
						)
					) or 
					-- search k có chủ trương đầu tư
					(
						@filterHasQDDT = 2 and da.iID_DuAnID in 
							(
								select distinct da.iID_DuAnID from VDT_DA_DuAn da where da.iID_DuAnID not in (select DuAnId as iID_DuAnID from VDT_DA_ChuTruongDauTu_NguonVon) 
							)
					)

				)
			)

	select tmp.* into #tmpData from #tmp as tmp

	--Union ALL

	--Select 
	--	da.iID_DuAnID,
	--	da.sTenDuAn,
	--	da.sMaDuAn,
	--	nv.iID_MaNguonNganSach,
	--	CONCAT(da.sKhoiCong,'-', da.sKetThuc) as sThoiGianThucHien,
	--	da.iID_CapPheDuyetID,
	--	pc.sTen as sTenCapPheDuyet,
	--	case 
	--		when dahm.IdLoaiCongTrinh is not null then dahm.IdLoaiCongTrinh else da.iID_LoaiCongTrinhID
	--	end as iID_LoaiCongTrinhID,
	--	lct.sTenLoaiCongTrinh,
	--	cdt.sTenDonVi as sTenChuDauTu,
	--	da.iID_DonViTienTeID,
	--	da.iID_TienTeID, 
	--	da.fTiGiaDonVi,
	--	da.fTiGia,
	--	da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,
	--	dvthda.sTenDonVi as STenDonViThucHienDuAn,
	--	da.dDateCreate
	--from VDT_DA_DuAn da
	--	inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
	--	LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
	--	LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
	--	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
	--	LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
	--	LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
	--	LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
	--Where 
	--	da.iID_DuAnID in (SELECT ctdt.iID_DuAnID FROM VDT_DA_ChuTruongDauTu ctdt)
	--	and da.iID_DuAnID not in (select tmpexisted.iID_DuAnID from #tmp tmpexisted)
	--	And da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId))

	-- Tong muc dau tu
	SELECT
		da.iID_DuAnID,
		CASE
			when qddt.iID_QDDauTuID is not null 
			then ISNULL(qddt.fTongMucDauTuPheDuyet, 0)
			else ISNULL(ctdt.fTMDTDuKienPheDuyet, 0)
		END fGiaTriDauTu
		INTO #tmpDataPD
	FROM
		#tmpData da
	LEFT JOIN
		VDT_DA_QDDauTu qddt
	ON
		da.iID_DuAnID = qddt.iID_DuAnID AND qddt.bActive = 1
	LEFT JOIN
		VDT_DA_ChuTruongDauTu ctdt
	ON
		da.iID_DuAnID = ctdt.iID_DuAnID AND ctdt.bActive = 1

	--luy ke von nam truoc
	BEGIN
		SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fLuyKeVonNamTruoc,
			--(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0)))) as fLuyKeVonNamTruoc,
			pbvdvct.iID_DuAnID as iID_DuAnID
			INTO #tmpLuyKeNamTruoc
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		--LEFT JOIN
		--	VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		--ON pbvdvct.iID_DuAnID = bcqtndct.iID_DuAnID
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach <= (@iNamKeHoach - 2)
		GROUP BY pbvdvct.iID_DuAnID
	END

	-- kế hoạch vốn năm nay
	BEGIN
		SELECT
			(SUM(isnull(pbvdvct.fCapPhatTaiKhoBac, 0)) + SUM(isnull(pbvdvct.fCapPhatBangLenhChi, 0))) as fKeHoachVonDuocDuyetNamNay,
			pbvdvct.iID_DuAnID as iID_DuAnID,
			pbvdvct.iID_LoaiCongTrinh as iID_LoaiCongTrinh 
			INTO #tmpKeHoachVonDuocDuyetNamNay
		FROM
			VDT_KHV_PhanBoVon_ChiTiet pbvdvct
		INNER JOIN
			VDT_KHV_PhanBoVon pbvdv
		ON pbvdvct.iID_PhanBoVonID = pbvdv.Id
		WHERE 
			--pbvdv.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			pbvdv.iID_NguonVonID = @nguonVonID and pbvdv.iNamKeHoach = (@iNamKeHoach - 1)
		GROUP BY pbvdvct.iID_DuAnID, pbvdvct.iID_LoaiCongTrinh
	END

	-- vôn kéo dài các năm trước
	BEGIN
		SELECT
			(SUM(isnull(bcqtndct.fGiaTriNamTruocChuyenNamSau, 0)) + SUM(isnull(bcqtndct.fGiaTriNamNayChuyenNamSau, 0)) - SUM(isnull(bcqtndct.fGiaTriTamUngDieuChinhGiam, 0))) as fVonKeoDaiCacNamTruoc,
			bcqtndct.iID_DuAnID as iID_DuAnID
			INTO #tmpVonKeoDaiCacNamTruoc
		FROM
			VDT_QT_BCQuyetToanNienDo_ChiTiet_01 bcqtndct
		INNER JOIN
			VDT_QT_BCQuyetToanNienDo bcqtnd
		ON bcqtndct.iID_BCQuyetToanNienDo = bcqtnd.Id
		WHERE 
			--bcqtnd.iID_MaDonViQuanLy = @maDonViQuanLyId and 
			bcqtnd.iID_NguonVonID = @nguonVonID and bcqtnd.iNamKeHoach < (@iNamKeHoach - 1)
		GROUP BY bcqtndct.iID_DuAnID
	END
	
	BEGIN
		SELECT
			khthct.*,
			khth.ILoai
			into #tmpThDd
		FROM
			VDT_KHV_KeHoach5Nam_ChiTiet khthct
		INNER JOIN
			VDT_KHV_KeHoach5Nam khth
		ON khthct.iID_KeHoach5NamID = khth.iID_KeHoach5NamID
		WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen
	END

	SELECT
			tmp.*,
			tbl_tmdt.fGiaTriDauTu as fTongMucDauTuDuocDuyet,
			isnull(lknt.fLuyKeVonNamTruoc, 0) as fLuyKeVonNamTruoc,
			isnull(khnn.fKeHoachVonDuocDuyetNamNay, 0) as fKeHoachVonDuocDuyetNamNay,
			isnull(vkd.fVonKeoDaiCacNamTruoc, 0) as fVonKeoDaiCacNamTruoc,
			cast(0 as float) as fUocThucHien,
			cast(0 as float) as fThuHoiVonUngTruoc,
			cast(0 as float) as fThanhToan,
			cast(0 as float) as FUocThucHienSauDc,
			cast(0 as float) as FThuHoiVonUngTruocSauDc,
			cast(0 as float) as FThanhToanSauDc,
			null as IIDParentId,
			case
				when ((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) then 2 else 1
			end ILoaiDuAn,
			isnull(khvnct.fVonBoTriTuNamDenNam, 0) as FKeHoachTrungHanDuocDuyet
		FROM #tmpData as tmp
		LEFT JOIN #tmpDataPD as tbl_tmdt on tmp.iID_DuAnID = tbl_tmdt.iID_DuAnID
		LEFT JOIN #tmpLuyKeNamTruoc as lknt on tmp.iID_DuAnID = lknt.iID_DuAnID
		LEFT JOIN #tmpKeHoachVonDuocDuyetNamNay as khnn on tmp.iID_DuAnID = khnn.iID_DuAnID and khnn.iID_LoaiCongTrinh = tmp.iID_LoaiCongTrinhID
		LEFT JOIN #tmpVonKeoDaiCacNamTruoc as vkd on tmp.iID_DuAnID = vkd.iID_DuAnID
		LEFT JOIN #tmpThDd khvnct on tmp.iID_DuAnID = khvnct.iID_DuAnID and tmp.iID_LoaiCongTrinhID = khvnct.iID_LoaiCongTrinhID and khvnct.iID_NguonVonID = @nguonVonID
		LEFT JOIN VDT_DA_DuAn da on tmp.iID_DuAnID = da.iID_DuAnID
		where iID_MaNguonNganSach = @nguonVonID
		ORDER BY tmp.dDateCreate desc

	drop table #tmpThDd;
	drop table #tmp;
	drop table #tmpData;
	drop table #tmpDataPD;
	drop table #tmpLuyKeNamTruoc;
	drop table #tmpKeHoachVonDuocDuyetNamNay;
	drop table #tmpVonKeoDaiCacNamTruoc;

End
;
GO

update TL_DM_PhuCap set Ten_PhuCap = N'Tổng tiền ăn thêm' where Ma_PhuCap = 'TA_TT'
