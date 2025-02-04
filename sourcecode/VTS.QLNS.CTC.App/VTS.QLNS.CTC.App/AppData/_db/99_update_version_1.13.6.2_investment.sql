/****** Object:  StoredProcedure [dbo].[sp_vdt_da_duan_get_by_loaichungtu]    Script Date: 30/11/2023 11:09:40 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_da_duan_get_by_loaichungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_da_duan_get_by_loaichungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 30/11/2023 3:23:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_da_duan_get_by_loaichungtu]    Script Date: 30/11/2023 11:09:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_da_duan_get_by_loaichungtu]
@sLoaiChungTu nvarchar(1),
@sMaDonViThucHienDuAn nvarchar(250),
@IsAdd bit
AS
BEGIN
	-- Du toan
	IF (@sLoaiChungTu = '1')
	BEGIN
		SELECT da.iID_DuAnID, da.sMaDuAn, da.sMaKetNoi, da.sTenDuAn, da.iID_DonViQuanLyID, da.iID_MaDonViQuanLy, da.iID_ChuDauTuID, da.iID_MaChuDauTuID as IIdMaChuDauTuId, da.sMucTieu, da.sQuyMo, da.sDiaDiem, da.sSuCanThietDauTu, da.sSoTaiKhoan, 
			da.sDiaDiemMoTaiKhoan, da.sDienTichSuDungDat, da.sNguonGocSuDungDat, da.fTongMucDauTuDuKien, da.fTongMucDauTuThamDinh, da.iID_TienTeID, da.iID_DonViTienTeID, da.fTiGiaDonVi, da.fTiGia, da.iID_NhomDuAnID, da.iID_NganhDuAnID, 
			da.iID_LoaiDuAnId, da.iID_HinhThucDauTuID, da.iID_HinhThucQuanLyID, da.iID_NhomQuanLyID, da.iID_LoaiCongTrinhID, da.iID_CapPheDuyetID, da.sKhoiCong, da.sKetThuc, da.dKhoiCongThucTe, da.dKetThucThucTe, 
			da.sTrangThaiDuAn, da.bLaDuAnChinhThuc, da.iID_ParentID, da.sCanBoPhuTrach, da.bIsKetThuc, da.sUserCreate, da.dDateCreate, da.sUserUpdate, da.dDateUpdate, da.sUserDelete, da.dDateDelete, da.bIsDeleted, 
			da.bIsCanBoDuyet, da.bIsDuyet, da.bIsDuPhong, da.fHanMucDauTu, da.iMaDuAnIndex, da.Id_DuAnKhthDeXuat, da.iID_DonViThucHienDuAnID, da.iID_MaDonViThucHienDuAnID, 
			da.fTongMucDauTu as fTongMucDauTu
		FROM VDT_DA_DuAn as da 
		INNER JOIN (SELECT DISTINCT iID_DuAnID FROM VDT_DA_DuToan WHERE (@IsAdd = 0 OR bActive = 1)) as dt on da.iID_DuAnID = dt.iID_DuAnID
		WHERE da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@sMaDonViThucHienDuAn))
	END
	-- Quyet dinh dau tu
	ELSE IF(@sLoaiChungTu = '2')
	BEGIN
		SELECT da.iID_DuAnID, da.sMaDuAn, da.sMaKetNoi, da.sTenDuAn, da.iID_DonViQuanLyID, da.iID_MaDonViQuanLy, da.iID_ChuDauTuID, da.iID_MaChuDauTuID as IIdMaChuDauTuId, da.sMucTieu, da.sQuyMo, da.sDiaDiem, da.sSuCanThietDauTu, da.sSoTaiKhoan, 
			da.sDiaDiemMoTaiKhoan, da.sDienTichSuDungDat, da.sNguonGocSuDungDat, da.fTongMucDauTuDuKien, da.fTongMucDauTuThamDinh, da.iID_TienTeID, da.iID_DonViTienTeID, da.fTiGiaDonVi, da.fTiGia, da.iID_NhomDuAnID, da.iID_NganhDuAnID, 
			da.iID_LoaiDuAnId, da.iID_HinhThucDauTuID, da.iID_HinhThucQuanLyID, da.iID_NhomQuanLyID, da.iID_LoaiCongTrinhID, da.iID_CapPheDuyetID, da.sKhoiCong, da.sKetThuc, da.dKhoiCongThucTe, da.dKetThucThucTe, 
			da.sTrangThaiDuAn, da.bLaDuAnChinhThuc, da.iID_ParentID, da.sCanBoPhuTrach, da.bIsKetThuc, da.sUserCreate, da.dDateCreate, da.sUserUpdate, da.dDateUpdate, da.sUserDelete, da.dDateDelete, da.bIsDeleted, 
			da.bIsCanBoDuyet, da.bIsDuyet, da.bIsDuPhong, da.fHanMucDauTu, da.iMaDuAnIndex, da.Id_DuAnKhthDeXuat, da.iID_DonViThucHienDuAnID, da.iID_MaDonViThucHienDuAnID, 
			da.fTongMucDauTu as fTongMucDauTu
		FROM VDT_DA_DuAn as da 
		INNER JOIN (SELECT DISTINCT iID_DuAnID FROM VDT_DA_QDDauTu WHERE (@IsAdd = 0 OR bActive = 1)) as dt on da.iID_DuAnID = dt.iID_DuAnID
		WHERE da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@sMaDonViThucHienDuAn))
	END
	-- Chu truong dau tu
	ELSE IF(@sLoaiChungTu = '3')
	BEGIN
		SELECT da.iID_DuAnID, da.sMaDuAn, da.sMaKetNoi, da.sTenDuAn, da.iID_DonViQuanLyID, da.iID_MaDonViQuanLy, da.iID_ChuDauTuID, da.iID_MaChuDauTuID as IIdMaChuDauTuId, da.sMucTieu, da.sQuyMo, da.sDiaDiem, da.sSuCanThietDauTu, da.sSoTaiKhoan, 
			da.sDiaDiemMoTaiKhoan, da.sDienTichSuDungDat, da.sNguonGocSuDungDat, da.fTongMucDauTuDuKien, da.fTongMucDauTuThamDinh, da.iID_TienTeID, da.iID_DonViTienTeID, da.fTiGiaDonVi, da.fTiGia, da.iID_NhomDuAnID, da.iID_NganhDuAnID, 
			da.iID_LoaiDuAnId, da.iID_HinhThucDauTuID, da.iID_HinhThucQuanLyID, da.iID_NhomQuanLyID, da.iID_LoaiCongTrinhID, da.iID_CapPheDuyetID, da.sKhoiCong, da.sKetThuc, da.dKhoiCongThucTe, da.dKetThucThucTe, 
			da.sTrangThaiDuAn, da.bLaDuAnChinhThuc, da.iID_ParentID, da.sCanBoPhuTrach, da.bIsKetThuc, da.sUserCreate, da.dDateCreate, da.sUserUpdate, da.dDateUpdate, da.sUserDelete, da.dDateDelete, da.bIsDeleted, 
			da.bIsCanBoDuyet, da.bIsDuyet, da.bIsDuPhong, da.fHanMucDauTu, da.iMaDuAnIndex, da.Id_DuAnKhthDeXuat, da.iID_DonViThucHienDuAnID, da.iID_MaDonViThucHienDuAnID, 
			dt.fTMDTDuKienPheDuyet as fTongMucDauTu
		FROM VDT_DA_DuAn as da 
		INNER JOIN (SELECT DISTINCT iID_DuAnID, fTMDTDuKienPheDuyet FROM VDT_DA_ChuTruongDauTu WHERE (@IsAdd = 0 OR bActive = 1)) as dt on da.iID_DuAnID = dt.iID_DuAnID
		WHERE da.iID_MaDonViThucHienDuAnID in (select * from f_recursive_donvi(@sMaDonViThucHienDuAn))
	END
	ELSE
	BEGIN
		SELECT * FROM VDT_DA_DuAn WHERE 1 = 0
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 30/11/2023 3:23:06 PM ******/
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
		inner JOIN VDT_KHV_KeHoach5Nam_ChiTiet khvct on da.iID_DuAnID = khvct.iID_DuAnID and khvct.bActive = 1
		LEFT JOIN NguonNganSach nv ON khvct.iID_NguonVonID = nv.iID_MaNguonNganSach 
		LEFT JOIN VDT_DA_DuAn_HangMuc dahm on da.iID_DuAnID = dahm.iID_DuAnID  and dahm.IdLoaiCongTrinh = khvct.iID_LoaiCongTrinhID
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
			then ISNULL(qddtnv.fTienPheDuyet, 0)
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

	JOIN VDT_DA_QDDauTu_NguonVon qddtnv

	ON qddt.iID_QDDauTuID = qddtnv.iID_QDDauTuID AND qddtnv.iID_NguonVonID = @nguonVonID

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
		WHERE @iNamKeHoach >= khth.iGiaiDoanTu and @iNamKeHoach <= khth.iGiaiDoanDen and khthct.bActive = 1
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
;
;
;

GO
