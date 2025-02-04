/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 28/11/2023 6:22:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_dexuat_dieuchinh_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_dexuat_dieuchinh_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 28/11/2023 6:22:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopkhoitaothongtinduan]    Script Date: 28/11/2023 6:22:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_tonghopkhoitaothongtinduan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_tonghopkhoitaothongtinduan]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_tonghopkhoitaothongtinduan]    Script Date: 28/11/2023 6:22:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_tonghopkhoitaothongtinduan]
@iId uniqueidentifier
AS
BEGIN
	-- insert ban ghi revert
	INSERT INTO VDT_TongHop_NguonNSDauTu(iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, sMaNguon, sMaDich, sMaNguonCha, fGiaTri, bIsLog, iStatus, sMaTienTrinh, iThuHoiTUCheDo, iLoaiUng, bKeHoach, iID_LoaiCongTrinh)	
	SELECT tbl.iID_DuAnID, iID_MaDonViQuanLy, iID_NguonVonID, sSoQuyetDinh, dNgayQuyetDinh, iNamKeHoach, iID_ChungTu, tbl.sMaDich, tbl.sMaNguon, tbl.sMaNguonCha, fGiaTri, 1, 2, '300', tbl.iThuHoiTUCheDo, iLoaiUng, bKeHoach, tbl.iID_LoaiCongTrinh
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE tbl.iID_ChungTu = @iId
	AND bIsLog = 0
	AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100') 

	-- khoa but toan da update
	UPDATE tbl 
	SET 
		bIsLog = 1
	FROM VDT_TongHop_NguonNSDauTu as tbl
	WHERE tbl.iID_ChungTu = @iId
		AND bIsLog = 0
		AND sMaTienTrinh COLLATE DATABASE_DEFAULT in ('100') 
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]    Script Date: 28/11/2023 6:22:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_get_all_du_an_kehoachtrunghan_dexuat_chuyentiep_dieuchinh]
	@IdDonVi nvarchar(max)
AS
BEGIN
	SELECT DISTINCT dt.iID_DuAnID INTO #tmpDuAnKHTH
	FROM VDT_KHV_KeHoach5Nam as tbl
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet as dt on tbl.iID_KeHoach5NamID = dt.iID_KeHoach5NamID
	WHERE tbl.bActive = 1

	SELECT
			duan.iID_DuAnID AS IDDuAnID,
			duan.sMaDuAn as SMaDuAn,
			duan.sTenDuAn AS STenDuAn,
			duan.sDiaDiem AS SDiaDiem,
			CAST(duan.sKhoiCong AS int) AS IGiaiDoanTu,
			CAST(duan.sKetThuc AS int) AS IGiaiDoanDen,
			duan.fHanMucDauTu AS FHanMucDauTu,
			donvi.iID_DonVi AS IIdDonViId,
			donvi.iID_MaDonVi AS IIDMaDonVi,
			donvi.sTenDonVi AS STenDonVi,
			null AS IIDLoaiCongTrinhID,
			'' AS STenLoaiCongTrinh,
			null AS IIDNguonVonID,
			'' AS STenNguonVon, 
			duan.Id_DuAnKhthDeXuat
		FROM VDT_DA_DuAn duan
		INNER JOIN #tmpDuAnKHTH as khth on duan.iID_DuAnID = khth.iID_DuAnID
		LEFT JOIN VDT_DM_DonViThucHienDuAn donvi
			ON duan.iID_DonViThucHienDuAnID  = donvi.iID_DonVi
		WHERE
			1=1
			--AND duan.sTrangThaiDuAn = 'THUC_HIEN'
			AND duan.bIsKetThuc IS NULL
			--AND iID_MaDonViQuanLy = @IdDonVi  
		ORDER BY duan.dDateCreate			--them ngay 13/8
	DROP TABLE #tmpDuAnKHTH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 28/11/2023 6:22:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khv_khth_dexuat_dieuchinh_report]
	@type int,
	@VoucherId nvarchar(max),
	@lstDonVi nvarchar(max),
	@MenhGiaTienTe float,
	@IdNguonVon nvarchar(max)
AS
BEGIN
	
	declare @namLamViec int;

	select @namLamViec = khnt.NamLamViec from VDT_KHV_KeHoach5Nam_DeXuat khnt where Id in (select top 1 * from dbo.splitstring(@VoucherId))

	if(@type = 2)
	begin
			select
			dmlct.iID_LoaiCongTrinh,
			ctctdx.iID_KeHoach5NamID,
			cast(0 as bit) as IsHangCha,
			2 as Loai,
			'' as STT,
			dvql.iID_MaDonVi as IdDonViQuanLy,
			da.sTenDuAn as STenDuAn,
			-- duoc duyet
			ctctdd.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTuDuocDuyet,
			(isnull(ctctdd.fVonBoTriTuNamDenNam, 0) + isnull(ctctdd.fGiaTriBoTri, 0))/@MenhGiaTienTe as FTongSoDuocDuyet,
			ctctdd.fVonBoTriTuNamDenNam/@MenhGiaTienTe as FVonBoTriTuNamDenNamDuocDuyet,
			ctctdd.fGiaTriBoTri/@MenhGiaTienTe as FVonBoTriSauNamDuocDuyet,
			(cast(da.sKhoiCong as nvarchar(max)) + '-' + cast(da.sKetThuc as nvarchar(max))) as STrangThaiThucHien,
			-- dexuat
			ctctdx.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTuDeXuat,
			(isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe) as FTongSoDeXuat,
			(isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe) as FTongCongDeXuat,
			ctctdx.fGiaTriNamThuNhat/@MenhGiaTienTe as FGiaTriNamThuNhatDeXuat,
			ctctdx.fGiaTriNamThuHai/@MenhGiaTienTe as FGiaTriNamThuHaiDeXuat,
			ctctdx.fGiaTriNamThuBa/@MenhGiaTienTe as FGiaTriNamThuBaDeXuat,
			ctctdx.fGiaTriNamThuTu/@MenhGiaTienTe as FGiaTriNamThuTuDeXuat,
			ctctdx.fGiaTriNamThuNam/@MenhGiaTienTe as FGiaTriNamThuNamDeXuat,
			ctctdx.fGiaTriBoTri/@MenhGiaTienTe as FGiaTriSauNamDeXuat,
			-- chenh lech
			(isnull(ctctdx.fHanMucDauTu, 0)/@MenhGiaTienTe - isnull(ctctdd.fHanMucDauTu, 0)/@MenhGiaTienTe) as FHanMucDauTuChenhLech,
			(
				(
					isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe
				)
				- 
				(
					isnull(ctctdd.fVonBoTriTuNamDenNam, 0)/@MenhGiaTienTe + isnull(ctctdd.fGiaTriBoTri, 0)/@MenhGiaTienTe
				)
			) as FTongSoChenhLech,
			(
				(
					isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe
				)
				- 
				(
					isnull(ctctdd.fVonBoTriTuNamDenNam, 0)/@MenhGiaTienTe
				)
			) as FVonBoTriTuNamDenNamChenhLech,
			(
				isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe - isnull(ctctdd.fGiaTriBoTri, 0)/@MenhGiaTienTe
			) as FVonBoTriSauNamChenhLech,
			ctctdx.sGhiChu as SGhiChu
			into #tmpData
		from
			VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctctdx
		inner join 
			VDT_KHV_KeHoach5Nam_DeXuat ctdx
		on 
			ctctdx.iID_KeHoach5NamID = ctdx.Id
		left join 
			DonVi dvql
		on 
			dvql.iID_MaDonVi = ctdx.iID_MaDonViQuanLy
		left join 
			VDT_DM_LoaiCongTrinh dmlct
		on 
			dmlct.iID_LoaiCongTrinh = ctctdx.iID_LoaiCongTrinhID
		inner join
			VDT_DA_DuAn da
		on
			ctctdx.iID_DuAnID = da.iID_DuAnID
		inner join
			VDT_KHV_KeHoach5Nam_ChiTiet ctctdd
		on
			ctctdd.iID_DuAnID = da.iID_DuAnID and
			ctctdx.iID_MaDonVi = ctctdd.iID_MaDonVi and
			ctctdx.iID_LoaiCongTrinhID = ctctdd.iID_LoaiCongTrinhID
		inner join
			VDT_KHV_KeHoach5Nam khdd
		on ctctdd.iID_KeHoach5NamID = khdd.iID_KeHoach5NamID and khdd.NamLamViec = @namLamViec and khdd.bActive = 1
		left join
			VDT_DM_DonViThucHienDuAn dv
		on dv.iID_DonVi = ctctdx.iID_DonViQuanLyID		
		where
			ctctdx.iID_KeHoach5NamID in (select * from dbo.splitstring(@VoucherId))
			and ctctdd.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))		

		select 
				dmlct.iID_LoaiCongTrinh,
				ctdxct.iID_KeHoach5NamID,
				cast(1 as bit) as IsHangCha,
				1 as Loai,
				'' as STT,
				dv.iID_MaDonVi as IdDonViQuanLy,
				dv.sTenDonVi as STenDuAn,
				SUM(isnull(ctddct.fHanMucDauTu/@MenhGiaTienTe, 0)) as FHanMucDauTuDuocDuyet,
				(SUM(isnull(ctddct.fVonBoTriTuNamDenNam/@MenhGiaTienTe, 0)) + SUM(isnull(ctddct.fGiaTriBoTri/@MenhGiaTienTe, 0))) as FTongSoDuocDuyet,
				SUM(isnull(ctddct.fVonBoTriTuNamDenNam/@MenhGiaTienTe, 0)) as FVonBoTriTuNamDenNamDuocDuyet,
				SUM(isnull(ctddct.fGiaTriBoTri/@MenhGiaTienTe, 0)) as FVonBoTriSauNamDuocDuyet,
				'' as STrangThaiThucHien,

				-- dexuat
				SUM(isnull(ctdxct.FHanMucDauTu/@MenhGiaTienTe, 0)) as FHanMucDauTuDeXuat,
				(SUM(isnull(ctdxct.fGiaTriNamThuNhat/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuHai/@MenhGiaTienTe, 0))
				+ SUM(isnull(ctdxct.fGiaTriNamThuBa/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuTu/@MenhGiaTienTe, 0))
				+ SUM(isnull(ctdxct.fGiaTriNamThuNam/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriBoTri/@MenhGiaTienTe, 0))) as FTongSoDeXuat,
				(SUM(isnull(ctdxct.fGiaTriNamThuNhat/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuHai/@MenhGiaTienTe, 0))
				+ SUM(isnull(ctdxct.fGiaTriNamThuBa/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuTu/@MenhGiaTienTe, 0))
				+ SUM(isnull(ctdxct.fGiaTriNamThuNam/@MenhGiaTienTe, 0))) as FTongCongDeXuat,
				SUM(isnull(ctdxct.fGiaTriNamThuNhat/@MenhGiaTienTe, 0)) as FGiaTriNamThuNhatDeXuat,
				SUM(isnull(ctdxct.fGiaTriNamThuHai/@MenhGiaTienTe, 0)) as FGiaTriNamThuHaiDeXuat,
				SUM(isnull(ctdxct.fGiaTriNamThuBa/@MenhGiaTienTe, 0)) as FGiaTriNamThuBaDeXuat,
				SUM(isnull(ctdxct.fGiaTriNamThuTu/@MenhGiaTienTe, 0)) as FGiaTriNamThuTuDeXuat,
				SUM(isnull(ctdxct.fGiaTriNamThuNam/@MenhGiaTienTe, 0)) as FGiaTriNamThuNamDeXuat,
				SUM(isnull(ctdxct.fGiaTriBoTri/@MenhGiaTienTe, 0)) as FGiaTriSauNamDeXuat,

				--chenh lech
				(SUM(isnull(ctdxct.FHanMucDauTu/@MenhGiaTienTe, 0)) - SUM(isnull(ctddct.fHanMucDauTu/@MenhGiaTienTe, 0))) as FHanMucDauTuChenhLech,
				((
					SUM(isnull(ctdxct.fGiaTriNamThuNhat/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuHai/@MenhGiaTienTe, 0))
					+ SUM(isnull(ctdxct.fGiaTriNamThuBa/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuTu/@MenhGiaTienTe, 0))
					+ SUM(isnull(ctdxct.fGiaTriNamThuNam/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriBoTri/@MenhGiaTienTe, 0))
				)
				- 
				(
					SUM(isnull(ctddct.fVonBoTriTuNamDenNam/@MenhGiaTienTe, 0)) + SUM(isnull(ctddct.fGiaTriBoTri/@MenhGiaTienTe, 0))
				)) as FTongSoChenhLech,
				(
					(SUM(isnull(ctdxct.fGiaTriNamThuNhat/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuHai/@MenhGiaTienTe, 0))
					+ SUM(isnull(ctdxct.fGiaTriNamThuBa/@MenhGiaTienTe, 0)) + SUM(isnull(ctdxct.fGiaTriNamThuTu/@MenhGiaTienTe, 0))
					+ SUM(isnull(ctdxct.fGiaTriNamThuNam/@MenhGiaTienTe, 0)))
					-
					(SUM(isnull(ctddct.fVonBoTriTuNamDenNam/@MenhGiaTienTe, 0)))
				) as FVonBoTriTuNamDenNamChenhLech,
				(SUM(isnull(ctdxct.fGiaTriBoTri/@MenhGiaTienTe, 0)) - SUM(isnull(ctddct.fGiaTriBoTri/@MenhGiaTienTe, 0))) as FVonBoTriSauNamChenhLech,
				'' as SGhiChu

				from
					DonVi dv
				inner join
					VDT_KHV_KeHoach5Nam_DeXuat ctdx
				on 
					dv.iID_MaDonVi = ctdx.iID_MaDonViQuanLy
				inner join
					VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctdxct
				on
					ctdx.Id = ctdxct.iID_KeHoach5NamID
				left join 
					VDT_DM_LoaiCongTrinh dmlct
				on 
					dmlct.iID_LoaiCongTrinh = ctdxct.iID_LoaiCongTrinhID
				inner join
					VDT_KHV_KeHoach5Nam_ChiTiet ctddct
				on
					ctdxct.iID_DuAnID = ctddct.iID_DuAnID and ctdxct.iID_LoaiCongTrinhID = ctddct.iID_LoaiCongTrinhID and ctdxct.iID_NguonVonID = ctddct.iID_NguonVonID
				where 
					dv.iID_MaDonVi in (select * from dbo.splitstring(@lstDonVi)) and dv.iNamLamViec = @namLamViec
					and ctdx.Id in (select * from dbo.splitstring(@VoucherId))
					and ctddct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))
				group by dv.iID_MaDonVi, dv.sTenDonVi, dmlct.iID_LoaiCongTrinh, ctdxct.iID_KeHoach5NamID

			union all

				select * from #tmpData

			order by iID_LoaiCongTrinh, iID_KeHoach5NamID, Loai, IdDonViQuanLy

			drop table #tmpData
	end
	else
	begin
		select
			cast(0 as bit) as IsHangCha,
			2 as Loai,
			'' as STT,
			dv.iID_MaDonVi as IdDonViQuanLy,
			da.sTenDuAn as STenDuAn,
			-- duoc duyet
			ctctdd.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTuDuocDuyet,
			(isnull(ctctdd.fVonBoTriTuNamDenNam, 0) + isnull(ctctdd.fGiaTriBoTri, 0))/@MenhGiaTienTe as FTongSoDuocDuyet,
			ctctdd.fVonBoTriTuNamDenNam/@MenhGiaTienTe as FVonBoTriTuNamDenNamDuocDuyet,
			ctctdd.fGiaTriBoTri/@MenhGiaTienTe as FVonBoTriSauNamDuocDuyet,
			(cast(da.sKhoiCong as nvarchar(max)) + '-' + cast(da.sKetThuc as nvarchar(max))) as STrangThaiThucHien,
			-- dexuat
			ctctdx.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTuDeXuat,
			(isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe) as FTongSoDeXuat,
			(isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
				+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe) as FTongCongDeXuat,
			ctctdx.fGiaTriNamThuNhat/@MenhGiaTienTe as FGiaTriNamThuNhatDeXuat,
			ctctdx.fGiaTriNamThuHai/@MenhGiaTienTe as FGiaTriNamThuHaiDeXuat,
			ctctdx.fGiaTriNamThuBa/@MenhGiaTienTe as FGiaTriNamThuBaDeXuat,
			ctctdx.fGiaTriNamThuTu/@MenhGiaTienTe as FGiaTriNamThuTuDeXuat,
			ctctdx.fGiaTriNamThuNam/@MenhGiaTienTe as FGiaTriNamThuNamDeXuat,
			ctctdx.fGiaTriBoTri/@MenhGiaTienTe as FGiaTriSauNamDeXuat,
			-- chenh lech
			(isnull(ctctdx.fHanMucDauTu, 0)/@MenhGiaTienTe - isnull(ctctdd.fHanMucDauTu, 0)/@MenhGiaTienTe) as FHanMucDauTuChenhLech,
			(
				(
					isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe
				)
				- 
				(
					isnull(ctctdd.fVonBoTriTuNamDenNam, 0)/@MenhGiaTienTe + isnull(ctctdd.fGiaTriBoTri, 0)/@MenhGiaTienTe
				)
			) as FTongSoChenhLech,
			(
				(
					isnull(ctctdx.fGiaTriNamThuNhat, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuHai, 0)/@MenhGiaTienTe
					+ isnull(ctctdx.fGiaTriNamThuBa, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuTu, 0)/@MenhGiaTienTe + isnull(ctctdx.fGiaTriNamThuNam, 0)/@MenhGiaTienTe
				)
				- 
				(
					isnull(ctctdd.fVonBoTriTuNamDenNam, 0)/@MenhGiaTienTe
				)
			) as FVonBoTriTuNamDenNamChenhLech,
			(
				isnull(ctctdx.fGiaTriBoTri, 0)/@MenhGiaTienTe - isnull(ctctdd.fGiaTriBoTri, 0)/@MenhGiaTienTe
			) as FVonBoTriSauNamChenhLech,
			ctctdx.sGhiChu as SGhiChu
		from
			VDT_KHV_KeHoach5Nam_DeXuat_ChiTiet ctctdx
		inner join
			VDT_DA_DuAn da
		on
			ctctdx.iID_DuAnID = da.iID_DuAnID
		inner join
			VDT_KHV_KeHoach5Nam_ChiTiet ctctdd
		on
			ctctdd.iID_DuAnID = da.iID_DuAnID and
			ctctdx.iID_MaDonVi = ctctdd.iID_MaDonVi and
			ctctdx.iID_LoaiCongTrinhID = ctctdd.iID_LoaiCongTrinhID
		inner join
			VDT_KHV_KeHoach5Nam khdd
		on ctctdd.iID_KeHoach5NamID = khdd.iID_KeHoach5NamID and khdd.NamLamViec = @namLamViec and khdd.bActive = 1
		left join
			VDT_DM_DonViThucHienDuAn dv
		on dv.iID_MaDonVi = ctctdx.iID_MaDonVi
		where
			ctctdx.iID_KeHoach5NamID in (select * from dbo.splitstring(@VoucherId))
			and ctctdd.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))
	end
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_dotnhan]    Script Date: 15/12/2021 6:35:11 PM ******/
SET ANSI_NULLS ON
;
;
;
GO
