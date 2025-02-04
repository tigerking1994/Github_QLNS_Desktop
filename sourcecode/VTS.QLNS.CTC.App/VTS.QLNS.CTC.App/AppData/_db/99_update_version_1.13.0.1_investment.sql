/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet_clone]    Script Date: 12/07/2023 5:17:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_insert_phanbovonchitiet_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_insert_phanbovonchitiet_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]    Script Date: 12/07/2023 5:17:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_kehoachvonungchitiet_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]    Script Date: 12/07/2023 5:17:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 12/07/2023 5:17:19 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 12/07/2023 5:17:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
	@iNamKeHoach int, 
	--@ngayLap DateTime,
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
		da.dDateCreate, 
		dahm.iID_DuAn_HangMucID INTO #tmp
		from VDT_DA_DuAn da
		inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID
		LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh or dahm.IdLoaiCongTrinh = lct.iID_LoaiCongTrinh
		LEFT JOIN VDT_DM_PhanCapDuAn as pc on da.iID_CapPheDuyetID = pc.iID_PhanCapID
		LEFT JOIN DM_ChuDauTu as cdt on da.iID_ChuDauTuID = cdt.iID_DonVi
		LEFT JOIN VDT_DM_DonViThucHienDuAn dvthda on da.iID_MaDonViThucHienDuAnID = dvthda.iID_MaDonVi
		Where  da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@maDonViQuanLyId)) 
		--And [dbo].[fn_CheckDieuKienDuAn](da.iID_DuAnID,@ngayLap) = 1 
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

			and da.iID_DuAnID not in (select iID_DuAnID from VDT_QT_QuyetToan)
			AND (@nguonVonID is null Or (dahm.iID_NguonVonID = @nguonVonID and khvct.iID_NguonVonID = @nguonVonID))
			

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
				when (((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) 
				or (select count(*) from VDT_KT_KhoiTao_DuLieu_ChiTiet ktdlct where ktdlct.iID_DuAnID = tmp.iID_DuAnID) > 0) then 2 else 1
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]    Script Date: 12/07/2023 5:17:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_kehoachvonungdx_detail]
@sTongHop nvarchar(1200),
@iIdDonViQuanLy nvarchar(50),
@dNgayLap date
AS
BEGIN
	select * into #lstDonVi from f_recursive_donvi(@iIdDonViQuanLy);
	IF(ISNULL(@sTongHop, '') = '')
	BEGIN
		SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet,th.iID_NguonVonID  INTO #tmp
		--SELECT da.iID_DuAnID, SUM(ISNULL(tbl.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet  INTO #tmp
		FROM VDT_DA_QDDauTu as tbl
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID AND da.iID_MaDonViThucHienDuAnID in (select * from #lstDonVi)
		INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as th on tbl.iID_DuAnID = th.iID_DuAnID
		WHERE tbl.bActive = 1 AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayLap as DATE)
		GROUP BY da.iID_DuAnID,th.iID_NguonVonID
		--GROUP BY da.iID_DuAnID

		SELECT tbl.*, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy,da.sMaDuAn,
				da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, null as sGhiChu , 
				null as iID_DonViQuanLyID, null as iID_MaDonViQuanLy, NULL as sTenDonVi,
				CAST(0 as float) as fGiaTriDeNghi,hm.iID_DuAn_HangMucID as ID_DuAn_HangMuc, hm.sTenHangMuc as sTenHangMuc
		FROM #tmp as tbl 
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID AND da.iID_MaDonViThucHienDuAnID in (select * from #lstDonVi)
		LEFT JOIN VDT_DA_DuAn_HangMuc hm on da.iID_DuAnID = hm.iID_DuAnID

		DROP TABLE #tmp
	END
	ELSE
	BEGIN
		SELECT Item INTO #tmpChungTu
		FROM f_split(@sTongHop)

		SELECT dt.iID_DuAnID, tbl.iID_DonViQuanLyID, tbl.iID_MaDonViQuanLy, SUM(ISNULL(fGiaTriDeNghi, 0)) as fGiaTriDeNghi INTO #tmpTH
		FROM #tmpChungTu as tmp
		INNER JOIN VDT_KHV_KeHoachVonUng_DX as tbl on tmp.Item = tbl.sSoDeNghi
		INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
		GROUP BY dt.iID_DuAnID, tbl.iID_DonViQuanLyID, tbl.iID_MaDonViQuanLy

		SELECT tmp.iID_DuAnID, SUM(ISNULL(dt.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet,th.iID_NguonVonID INTO #tmpQDDT
		--SELECT tmp.iID_DuAnID, SUM(ISNULL(dt.fTongMucDauTuPheDuyet,0)) as fTongMucDauTuPheDuyet, INTO #tmpQDDT
		FROM #tmpTH as tmp
		LEFT JOIN VDT_DA_QDDauTu as dt on tmp.iID_DuAnID = dt.iID_DuAnID
		LEFT JOIN VDT_KHV_KeHoach5Nam_ChiTiet as th on tmp.iID_DuAnID = th.iID_DuAnID
		WHERE dt.bActive = 1 AND dt.iID_DuAnID IS NOT NULL AND th.iID_DuAnID IS NOT NULL
		GROUP BY tmp.iID_DuAnID,th.iID_NguonVonID
		--GROUP BY da.iID_DuAnID

		SELECT da.sMaDuAn, da.sTenDuAn, tmp.iID_DuAnID, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy, ISNULL(qd.fTongMucDauTuPheDuyet, 0) as fTongMucDauTuPheDuyet,
			tmp.fGiaTriDeNghi, da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGiaDonVi, da.fTiGia, NULL as sGhiChu, 
			dv.sTenDonVi, tmp.iID_DonViQuanLyID, tmp.iID_MaDonViQuanLy, hm.iID_DuAn_HangMucID as ID_DuAn_HangMuc, hm.sTenHangMuc as sTenHangMuc
		FROM #tmpTH as tmp
		INNER JOIN VDT_DA_DuAn as da on tmp.iID_DuAnID = da.iID_DuAnID
		LEFT JOIN VDT_DA_DuAn_HangMuc hm on da.iID_DuAnID = hm.iID_DuAnID
		LEFT JOIN #tmpQDDT as qd on tmp.iID_DuAnID = qd.iID_DuAnID
		INNER JOIN DonVi as dv on tmp.iID_DonViQuanLyID = dv.iID_DonVi

		DROP TABLE #tmpTH
		DROP TABLE #tmpChungTu
		DROP TABLE #lstDonVi
	END
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]    Script Date: 12/07/2023 5:17:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_kehoachvonungchitiet_detail]
@iIdKeHoachVonUng uniqueidentifier
AS
BEGIN
	DECLARE @iIdKeHoachVonUngDeXuat uniqueidentifier = (SELECT iID_KeHoachUngDeXuatID FROM VDT_KHV_KeHoachVonUng WHERE Id = @iIdKeHoachVonUng)

	SELECT dt.iID_DuAnID, dt.ID_DuAn_HangMuc, SUM(ISNULL(dt.fGiaTriDeNghi, 0)) as fGiaTriDeNghi INTO #tmpDX
	FROM VDT_KHV_KeHoachVonUng_DX as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_DX_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	WHERE tbl.Id = @iIdKeHoachVonUngDeXuat
	GROUP BY dt.iID_DuAnID, dt.ID_DuAn_HangMuc

	SELECT dt.iID_DuAnID, da.sTenDuAn, da.sMaKetNoi, da.sTrangThaiDuAn as sTrangThaiDuAnDangKy, da.sMaDuAn, dx.id_duan_hangmuc, dahm.sTenHangMuc, 
			da.iID_DonViTienTeID, da.iID_TienTeID, da.fTiGia, da.fTiGiaDonVi, dt.fCapPhatBangLenhChi, dt.fCapPhatTaiKhoBac , dt.sGhiChu,
			(SELECT SUM(ISNULL(fTongMucDauTuPheDuyet,0)) FROM VDT_DA_QDDauTu WHERE iID_DuAnID =dt.iID_DuAnID AND dNgayQuyetDinh <= tbl.dNgayQuyetDinh AND bActive = 1) as fTongMucDauTuPheDuyet,
			dt.iID_MucID, dt.iID_TieuMucID, dt.iID_TietMucID, dt.iID_NganhID,
			ml.sLNS as sLNS, ml.sL as sL, ml.sK as sK, ml.sM as sM, ml.sTM as sTM, ml.sTTM as sTTM, ml.sNG as sNG, dx.fGiaTriDeNghi
	FROM VDT_KHV_KeHoachVonUng as tbl 
	INNER JOIN VDT_KHV_KeHoachVonUng_ChiTiet as dt on tbl.Id = dt.iID_KeHoachUngID
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN #tmpDX as dx on dt.iID_DuAnID = dx.iID_DuAnID
	left join vdt_da_duan_hangmuc dahm on dahm.iID_DuAn_HangMucID = dx.id_duan_hangmuc

	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iId
									OR dt.iID_TieuMucID = ml.iId
									OR dt.iID_TietMucID = ml.iId
									OR dt.iID_NganhID = ml.iId
	WHERE tbl.Id = @iIdKeHoachVonUng
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_insert_phanbovonchitiet_clone]    Script Date: 12/07/2023 5:17:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_insert_phanbovonchitiet_clone]
@bIsEdit bit,
@sUserLogin nvarchar(100),
@tbl_PhanBoVonChiTiet t_tbl_phanbovonchitiet5 READONLY,
@sTypeError int OUTPUT
AS
BEGIN
	set @sTypeError = 0
	DECLARE @iIdPhanBoVon uniqueidentifier = (SELECT TOP(1) iID_PhanBoVonID FROM @tbl_PhanBoVonChiTiet)
	DECLARE @iNamKeHoach int = (SELECT TOP(1)iNamKeHoach FROM VDT_KHV_PhanBoVon WHERE id = @iIdPhanBoVon)
	--DECLARE @lstIdParent nvarchar(max);

	DECLARE @iCountPass int =  (SELECT COUNT(tbl.sLNS)
									FROM @tbl_PhanBoVonChiTiet as tbl
									INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS
																	AND ISNULL(tbl.sL, '') = ml.sL 
																	AND ISNULL(tbl.sK, '') = ml.sK
																	AND ISNULL(tbl.sM, '')= ml.sM 
																	AND ISNULL(tbl.sTM, '') = ml.sTM 
																	--AND ISNULL(tbl.sTTM, '') = ml.sTTM
																	--AND ISNULL(tbl.sNG, '') = ml.sNG 
																	AND ml.iNamLamViec = @iNamKeHoach
																	AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)

									)
	DECLARE @iCountInput int = (SELECT COUNT(*)
								FROM @tbl_PhanBoVonChiTiet )

	IF(@iCountPass < @iCountInput) 
	BEGIN
		SET @sTypeError = 1
		RETURN
	END

	IF(@bIsEdit = 1)
	BEGIN 
		DELETE VDT_KHV_PhanBoVon_ChiTiet WHERE iID_PhanBoVonID = @iIdPhanBoVon
		--select @lstIdParent = (cast(pbvct1.iId_Parent as nvarchar(1000)) + ',') from VDT_KHV_PhanBoVon_ChiTiet pbvct1 WHERE iID_PhanBoVonID = @iIdPhanBoVon and pbvct1.iId_Parent is not null
		--delete VDT_KHV_PhanBoVon_ChiTiet where iID_PhanBoVonID in (select * from dbo.splitstring(@lstIdParent))
	END

	INSERT INTO VDT_KHV_PhanBoVon_ChiTiet(Id, iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
											--new--
											fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
											iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu, LNS, L, K, M, TM, TTM, NG, fCapPhatTaiKhoBacDc, fCapPhatBangLenhChiDc, fGiaTriThuHoiNamTruocKhoBacDc,
											fGiaTriThuHoiNamTruocLenhChiDc, iId_Parent, bActive, ILoaiDuAn, iID_LoaiCongTrinh, iID_DuAn_HangMucID)
	SELECT NEWID(), iID_PhanBoVonID, iID_DuAnID, 
			(CASE WHEN ISNULL(tbl.sTM, '') = '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_MucID, 
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') = '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TieuMucID,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') = '' THEN ml.iID ELSE NULL END) as iID_TietMucID,
			(CASE WHEN ISNULL(tbl.sTM, '') <> '' AND ISNULL(tbl.sTTM, '') <> '' AND ISNULL(tbl.sNG, '') <> '' THEN ml.iID ELSE NULL END) as iID_NganhID,
			sTrangThaiDuAnDangKy, fGiaTrDeNghi, fGiaTrPhanBo, fGiaTriThuHoi, 
			--new--
			fCapPhatTaiKhoBac, fCapPhatBangLenhChi,fGiaTriThuHoiNamTruocKhoBac, fGiaTriThuHoiNamTruocLenhChi, fThanhToanDeXuat,
			iID_DonViTienTeID, iID_TienTeID, fTiGiaDonVi, fTiGia, sGhiChu,
			tbl.sLNS, tbl.sL, tbl.sK, tbl.sM, tbl.sTM, tbl.sTTM, tbl.sNG,
			tbl.fCapPhatTaiKhoBacDc, tbl.fCapPhatBangLenhChiDc, tbl.fGiaTriThuHoiNamTruocKhoBacDc, tbl.fGiaTriThuHoiNamTruocLenhChiDc, tbl.iID_Parent, 1, tbl.ILoaiDuAn, tbl.IIdLoaiCongTrinh, tbl.IID_DuAn_HangMucID
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN NS_MucLucNganSach as ml on ml.sLNS = tbl.sLNS 
									AND ISNULL(TBL.SL, '') = ML.SL 
									AND ISNULL(TBL.SK, '') = ML.SK
									AND ISNULL(TBL.SM, '')= ML.SM 
									AND ISNULL(TBL.STM, '') = ML.STM 
	--								AND ISNULL(tbl.sTTM, '') = ml.sTTM 
	--								AND ISNULL(tbl.sNG, '') = ml.sNG 
									AND ml.iNamLamViec = @iNamKeHoach
									AND ml.sXauNoiMa = (tbl.sLNS +'-' + tbl.sL+'-' + tbl.sK+'-' + tbl.sM+'-' + tbl.sTM)
									
	DECLARE @fGiaTriDeNghi float = (SELECT SUM(fGiaTrDeNghi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoi float = (SELECT SUM(fGiaTriThuHoi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTrPhanBo float = (SELECT SUM(fGiaTrPhanBo) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatTaiKhoBac float = (SELECT SUM(fCapPhatTaiKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fCapPhatBangLenhChi float = (SELECT SUM(fCapPhatBangLenhChi) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocKhoBac float = (SELECT SUM(fGiaTriThuHoiNamTruocKhoBac) FROM @tbl_PhanBoVonChiTiet)
	DECLARE @fGiaTriThuHoiNamTruocLenhChi float = (SELECT SUM(fGiaTriThuHoiNamTruocLenhChi) FROM @tbl_PhanBoVonChiTiet)

	SELECT @fGiaTriDeNghi = (@fGiaTriDeNghi + ISNULL(fGiaTrDeNghi, 0)),
			@fGiaTriThuHoi = (@fGiaTriThuHoi + ISNULL(fGiaTriThuHoi, 0)),
			@fGiaTrPhanBo = (@fGiaTrPhanBo + ISNULL(fGiaTrPhanBo, 0)),
			@fCapPhatTaiKhoBac = (@fCapPhatTaiKhoBac + ISNULL(fCapPhatTaiKhoBac, 0)),
			@fCapPhatBangLenhChi = (@fCapPhatBangLenhChi + ISNULL(fCapPhatBangLenhChi, 0)),
			@fGiaTriThuHoiNamTruocKhoBac = (@fGiaTriThuHoiNamTruocKhoBac + ISNULL(fGiaTriThuHoiNamTruocKhoBac, 0)),
			@fGiaTriThuHoiNamTruocLenhChi = (@fGiaTriThuHoiNamTruocLenhChi + ISNULL(fGiaTriThuHoiNamTruocLenhChi, 0))

	FROM VDT_KHV_PhanBoVon 
	WHERE Id = (SELECT iID_ParentId FROM VDT_KHV_PhanBoVon WHERE Id = @iIdPhanBoVon)

	UPDATE VDT_KHV_PhanBoVon
	set 
	fGiaTrDeNghi = @fGiaTriDeNghi,
	fGiaTriThuHoi = @fGiaTriThuHoi,
	fGiaTrPhanBo = @fGiaTrPhanBo,
	fCapPhatTaiKhoBac = @fCapPhatTaiKhoBac,
	fCapPhatBangLenhChi = @fCapPhatBangLenhChi,
	fGiaTriThuHoiNamTruocKhoBac = @fGiaTriThuHoiNamTruocKhoBac,
	fGiaTriThuHoiNamTruocLenhChi = @fGiaTriThuHoiNamTruocLenhChi
	WHERE Id = @iIdPhanBoVon

	UPDATE da
	SET
		sTrangThaiDuAn = 'THUC_HIEN'
	FROM @tbl_PhanBoVonChiTiet as tbl
	INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnID = da.iID_DuAnID
	WHERE da.sTrangThaiDuAn = 'KhoiTao'
END
;
;
;
GO
