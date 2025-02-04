/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_pheduyet_loaichiphi]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_pheduyet_loaichiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_pheduyet_loaichiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_pheduyet_chiphi]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_all_pheduyet_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_all_pheduyet_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_delete_chutruongdautu_hangmuc]    Script Date: 11/15/2023 4:56:18 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_delete_chutruongdautu_hangmuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_delete_chutruongdautu_hangmuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_delete_chutruongdautu_hangmuc]    Script Date: 11/15/2023 4:56:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[sp_vdt_delete_chutruongdautu_hangmuc] 
	@id nvarchar(100)
AS
BEGIN

	DELETE dmhm 
	FROM VDT_DA_QDDauTu_DM_HangMuc dmhm
		INNER JOIN VDT_DA_QDDauTu_HangMuc qdhm ON qdhm.iID_HangMucID = dmhm.Id 
		where qdhm.iID_QDDauTuID = @id
		
	DELETE VDT_DA_QDDauTu_HangMuc WHERE iID_QDDauTuID = @id

END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_pheduyet_chiphi]    Script Date: 11/15/2023 4:56:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_pheduyet_chiphi] 
	@qdDauTuId nvarchar(200)
AS
BEGIN

	--select * from
	select 
		dacp.iID_DuAn_ChiPhi as Id,
		dacp.iThuTu as IThuTu,
		dacp.sTenChiPhi as  TenChiPhi,
		qdcp.iID_QDDauTu_ChiPhiID as IdQDChiPhi,
		qdcp.iID_ChiPhiID as IdChiPhi,
		qdcp.iID_QDDauTuID as IdQDDauTu,
		qdcp.fTienPheDuyet as GiaTriPheDuyet,
		qdcp.iID_DuAn_ChiPhi as IdChiPhiDuAn,
		dacp.iID_ChiPhi_Parent as IdChiPhiDuAnParent,
		null as GiaTriDieuChinh,
		null as GiaTriTruocDieuChinh,
		isnull(cast(case when dacp.iID_ChiPhi_Parent is  null then 1 else 0 end as bit),0)  as IsLoaiChiPhi,
		isnull(cast(case when parentId.iID_ChiPhi_Parent is not null or dacp.iID_ChiPhi_Parent is null then 1 else 0 end as bit),0)  as IsHangCha,
		cast(1 as bit) as IsEditHangMuc,
		dacp.sMaChiPhi
	from VDT_DA_QDDauTu_ChiPhi qdcp
		inner join VDT_DM_DuAn_ChiPhi dacp ON qdcp.iID_DuAn_ChiPhi = dacp.iID_DuAn_ChiPhi
		left join
		(
			select distinct tb2.iID_ChiPhi_Parent from VDT_DA_QDDauTu_ChiPhi tb1
			inner join VDT_DM_DuAn_ChiPhi tb2 ON tb1.iID_DuAn_ChiPhi = tb2.iID_DuAn_ChiPhi AND tb1.iID_QDDauTuID = @qdDauTuId and tb2.iID_ChiPhi_Parent is not null
		) as parentId ON parentId.iID_ChiPhi_Parent = dacp.iID_DuAn_ChiPhi

		where qdcp.iID_QDDauTuID = @qdDauTuId 

		
		order by iThuTu,dacp.iID_ChiPhi_Parent,dacp.sTenChiPhi
		
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_all_pheduyet_loaichiphi]    Script Date: 11/15/2023 4:56:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_get_all_pheduyet_loaichiphi] 
	
AS
BEGIN
	select
		null as Id,
		tbl.iThuTu as IThuTu,
		tbl.sTenChiPhi as  TenChiPhi,
		null as IdQDChiPhi,
		tbl.iID_ChiPhi as IdChiPhi,
		null as IdQDDauTu,
		CAST(0 AS float) as GiaTriPheDuyet,
		NEWID() as IdChiPhiDuAn,
		null as IdChiPhiDuAnParent,
		null as GiaTriDieuChinh,
		null as GiaTriTruocDieuChinh,
		cast(1 as bit) as IsLoaiChiPhi,
		cast(1 as bit) as IsHangCha,
		cast(1 as bit) as IsEditHangMuc,
		tbl.sMaChiPhi as sMaChiPhi
		
	from VDT_DM_ChiPhi as tbl
	--INNER JOIN VDT_DM_ChiPhi as dt on  tbl.iID_ChiPhi = dt.iID_ChiPhi
	--where bHangCha = 1
	order by IThuTu
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 11/15/2023 4:56:18 PM ******/
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
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 11/15/2023 4:56:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max), --- ID hợp đồng hoặc chi phí theo loại thanh toán theo hợp đồng hoặc chi phí
	@NgayDeNghi datetime,
	@NguonVonId int,
	@NamKeHoach int,
	@iCoQuanThanhToan int,
	@loaiCoQuanTaiChinh int,
	@loaiKhv int 
AS
BEGIN
	DECLARE @uIdEmpty uniqueidentifier = CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER)
	DECLARE @fLuyKeTTKLHTNN float
	DECLARE @fLuyKeTTKLHTTN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoNN float
	DECLARE @fLuyKeTUChuaThuHoiCheDoTN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocNN float
	DECLARE @fLuyKeTUChuaThuHoiUngTruocTN float

	SELECT
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as ThanhToanTN,
		(CASE WHEN iLoaiThanhToan = 1 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as ThanhToanNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocTN, 0) + ISNULL(fGiaTriThuHoiNamNayTN, 0)) ELSE SUM(0) END) as ThuHoiUngTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiNamTruocNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiNamNayNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiNamTruocNN, 0) + ISNULL(fGiaTriThuHoiNamNayNN, 0)) ELSE SUM(0) END) as ThuHoiUngNN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayTN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocTN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocTN,
		(CASE WHEN iLoaiThanhToan = 1 AND (ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN,0) <> 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN,0) <> 0) THEN SUM(ISNULL(fGiaTriThuHoiUngTruocNamNayNN, 0) + ISNULL(fGiaTriThuHoiUngTruocNamTruocNN, 0)) ELSE SUM(0) END) as ThuHoiUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 2 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN
		INTO #tmp
	FROM VDT_TT_DeNghiThanhToan tbl
	INNER JOIN VDT_TT_DeNghiThanhToan_KHV as khv on tbl.Id = khv.iID_DeNghiThanhToanID
	LEFT JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tbl.iID_HopDongID ELSE tbl.iID_ChiPhiID END)
		  AND 
		  (
			  tbl.dNgayPheDuyet <= @NgayDeNghi 
			  and iNamKeHoach <= @NamKeHoach
		  )
		  AND (tbl.iCoQuanThanhToan = @iCoQuanThanhToan and (@iCoQuanThanhToan = 1 or (@iCoQuanThanhToan = 2 and tbl.loaiCoQuanTaiChinh = @loaiCoQuanTaiChinh)))
		  AND tbl.iID_NguonVonID = @NguonVonId
		  		  and (
						iLoaiThanhToan = 0 or
						-- nếu loại thanh toán là thanh toán -> tính tổng những đề nghị trước có loại kế hoạch vốn năm là 1,3 hoặc 2,4 phụ thuộc loại đang chọn
						(@loaiKhv in (1,3) and khv.iLoai in (1,3))
						or
						(@loaiKhv in (2,4) and khv.iLoai in (2,4))
						or @loaiKhv = 0
					)
	GROUP BY iLoaiThanhToan,dt.fGiaTriThanhToanTN, dt.fGiaTriThanhToanNN, dt.fGiaTriThuHoiNamTruocTN, dt.fGiaTriThuHoiNamNayTN, dt.fGiaTriThuHoiNamTruocNN, dt.fGiaTriThuHoiNamNayNN, khv.iLoai,
		fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamNayNN, fGiaTriThuHoiUngTruocNamTruocNN

	SELECT @fLuyKeTTKLHTNN = SUM(ISNULL(tt.fLuyKeTTKLHTNN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTNN_KHVU, 0)),
		@fLuyKeTTKLHTTN = SUM(ISNULL(tt.fLuyKeTTKLHTTN_KHVN, 0) + ISNULL(tt.fLuyKeTTKLHTTN_KHVU, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiCheDoTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVN, 0)) ,
		@fLuyKeTUChuaThuHoiUngTruocNN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiNN_KHVU, 0)),
		@fLuyKeTUChuaThuHoiUngTruocTN = SUM(ISNULL(tt.fLuyKeTUChuaThuHoiTN_KHVU, 0))
	FROM VDT_KT_KhoiTao_DuLieu as tbl
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet as dt on tbl.Id = dt.iID_KhoiTaoDuLieuID
	INNER JOIN VDT_KT_KhoiTao_DuLieu_ChiTiet_ThanhToan as tt on dt.Id = tt.iId_KhoiTaoDuLieuChiTietId
	WHERE @iIdChungTu = (CASE WHEN @bThanhToanTheoHopDong = 1 THEN tt.iID_HopDongID ELSE @uIdEmpty END)
		-- AND dt.iID_NguonVonID = @NguonVonId
		AND dt.iCoQuanThanhToan = @iCoQuanThanhToan
	

	SELECT (ISNULL(@fLuyKeTTKLHTTN, 0) + ISNULL(SUM(ISNULL(ThanhToanTN, 0)), 0)) as ThanhToanTN,
			(ISNULL(@fLuyKeTTKLHTNN, 0) + ISNULL(SUM(ISNULL(ThanhToanNN, 0)), 0)) as ThanhToanNN,
			ISNULL(SUM(ISNULL(ThuHoiUngTN, 0)), 0) as ThuHoiUngTN,
			ISNULL(SUM(ISNULL(ThuHoiUngNN, 0)), 0) as ThuHoiUngNN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoTN, 0) + ISNULL(SUM(ISNULL(TamUngCheDoTN, 0)), 0)) as TamUngTN,
			(ISNULL(@fLuyKeTUChuaThuHoiCheDoNN, 0) +ISNULL(SUM(ISNULL(TamUngCheDoNN, 0)), 0)) as TamUngNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocNN, 0)), 0) as ThuHoiUngUngTruocNN,
			ISNULL(SUM(ISNULL(ThuHoiUngUngTruocTN, 0)), 0) as ThuHoiUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocTN, 0) + ISNULL(SUM(ISNULL(TamUngUngTruocTN, 0)), 0)) as TamUngUngTruocTN,
			(ISNULL(@fLuyKeTUChuaThuHoiUngTruocNN, 0) +ISNULL(SUM(ISNULL(TamUngUngTruocNN, 0)), 0)) as TamUngUngTruocNN
	FROM  #tmp
	DROP TABLE #tmp
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 11/15/2023 4:56:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtri_detail]
@iIdThongTriId uniqueidentifier,
@sMaDonViQuanLy nvarchar(50),
@iLoaiThongTri int,
@iNamkeHoach int,
@dNgayThongTri DATE,
@sMaNguonVon nvarchar(max)
AS
BEGIN
	IF @iLoaiThongTri in (1,2)
	BEGIN
		SELECT
			(CASE tbl.iLoaiThanhToan WHEN 1 THEN 
					(CASE WHEN dt.colName in ('fGiaTriThanhToanTN', 'fGiaTriThanhToanNN') 
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_CTT_KPQP' WHEN 2 THEN 'TT_Cap_KPNN' ELSE 'TT_Cap_KPK' END)
						WHEN dt.colName in ('fGiaTriThuHoiNamTruocTN', 'fGiaTriThuHoiNamTruocNN', 'fGiaTriThuHoiNamNayTN', 'fGiaTriThuHoiNamNayNN', 'fGiaTriThuHoiUngTruocNamTruocTN', 'fGiaTriThuHoiUngTruocNamTruocNN', 'fGiaTriThuHoiUngTruocNamNayTN', 'fGiaTriThuHoiUngTruocNamNayNN')
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_ThuUng_KPQP' WHEN 2 THEN 'TT_ThuUng_KPNN' ELSE 'TT_ThuUng_KPK' END)
						END)
				WHEN 0 THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_TamUng_KPQP' WHEN 2 THEN 'TT_TamUng_KPNN' ELSE 'TT_TamUng_KPK' END) END) as SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.FGiaTri as FSoTien,
			tbl.iID_DuAnId as IIdDuAnId,
			tbl.iID_NhaThauId as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			dahm.IdLoaiCongTrinh as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,

			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			tbl.sTenDonViThuHuong as SDonViThuHuong INTO #tmpThanhToan
		FROM VDT_TT_DeNghiThanhToan as tbl
		INNER JOIN (
				SELECT iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, SUM(dt.fGiaTri) as fGiaTri, colName
				from 
				(select iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN from VDT_TT_PheDuyetThanhToan_ChiTiet) as tbl
				UNPIVOT
				(fGiaTri FOR colName IN (fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN)) as dt
				GROUP BY iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, colName
				HAVING SUM(dt.fGiaTri) <> 0
			) as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
		LEFT JOIN VDT_DA_DuAn_HangMuc as dahm on dahm.iID_DuAnID = da.iID_DuAnID and dahm.iID_DuAn_HangMucID = tbl.ID_DuAn_HangMuc
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		WHERE tbl.iLoaiThanhToan = (CASE WHEN @iLoaiThongTri = 1 THEN 1 ELSE 2 END)
			AND ( tbl.iID_ThongTriThanhToanID = @iIdThongTriId)
			--AND tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			--AND tbl.iNamKeHoach = @iNamkeHoach
			--AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayThongTri as DATE)
			--AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))

		SELECT SMaKieuThongTri, SSoThongTri, SUM(ISNULL(FSoTien, 0)) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpThanhToan
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpThanhToan
	END 
	ELSE
	BEGIN
		SELECT (CASE dt.colName WHEN 'fCapPhatTaiKhoBac' THEN 'hop_thuc' 
					ELSE 'kinh_phi' END) SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.iID_DuAnId as IIdDuAnId,
			NULL as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,
			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			NULL as SDonViThuHuong INTO #tmpKHV
		FROM VDT_KHV_PhanBoVon as tbl
		INNER JOIN (
			SELECT iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			from 
			(select iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac, fCapPhatBangLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
			UNPIVOT
			(fGiaTri FOR colName IN (fCapPhatTaiKhoBac, fCapPhatBangLenhChi)) as dt
			GROUP BY iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			HAVING SUM(ISNULL(fGiaTri, 0)) > 0
		) as dt on tbl.Id = dt.iID_PhanBoVonID
		
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnId = da.iID_DuAnID
		INNER JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		WHERE tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			AND tbl.iNamKeHoach = @iNamkeHoach
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayThongTri as DATE)
			AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))
			AND dt.colName = (CASE WHEN @iLoaiThongTri = 3 THEN 'fCapPhatBangLenhChi' ELSE 'fCapPhatTaiKhoBac' END)

		SELECT SMaKieuThongTri, SSoThongTri, CAST(0 as float) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpKHV
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpKHV
	END
END
;
;
GO
