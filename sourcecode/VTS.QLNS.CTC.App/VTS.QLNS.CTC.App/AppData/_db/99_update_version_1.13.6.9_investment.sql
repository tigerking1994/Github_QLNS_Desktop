/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_hangmuc_by_chungtu]    Script Date: 18/12/2023 5:05:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khlcnt_get_hangmuc_by_chungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khlcnt_get_hangmuc_by_chungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_qddautu]    Script Date: 18/12/2023 5:05:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_dutoan_hangmuc_by_qddautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_dutoan_hangmuc_by_qddautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]    Script Date: 18/12/2023 5:05:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 19/12/2023 11:27:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_dexuat_dieuchinh_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_dexuat_dieuchinh_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_hangmuc_by_chiphi]    Script Date: 19/12/2023 4:33:15 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_hopdong_find_hangmuc_by_chiphi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_hopdong_find_hangmuc_by_chiphi]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan]    Script Date: 18/12/2023 5:05:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_dutoan_hangmuc_by_dutoan] 
	@duToanId nvarchar(200)
AS
BEGIN

select 
				dm.Id as Id,
				dthm.iID_DuToan_HangMuciID as IdDuToanHangMuc,
				dm.sMaHangMuc as MaHangMuc,
				dm.sTenHangMuc as TenHangMuc,
				dthm.iID_ChiPhiID as  IdChiPhi,
				dthm.iID_DuAn_ChiPhi as  IdDuAnChiPhi,
				dm.Id as  IdDuAnHangMuc,
				dthm.iID_DuToanID as  IIdDuToanId,
				ISNULL(dthm.fTienPheDuyet, 0 ) as GiaTriPheDuyet,
				ISNULL(dthm.fTienPheDuyetQDDT, 0 ) as FTienPheDuyetQDDT,
				ISNULL(dthm.fTienPheDuyet, 0 ) - ISNULL(dthm.fTienPheDuyetQDDT, 0 )  as FTienChenhLech,
				dm.iID_ParentID as HangMucParentId,
				CAST(1 as bit) as IsHangMucOld,
				dm.maOrder,
				null as FGiaTriDieuChinh,
				(ISNULL(dthm.fTienPheDuyet, 0 ) - ISNULL(dthm.fGiaTriDieuChinh, 0 )) as GiaTriTruocDieuChinh,
				null as TenChiPhi,
				null as TenLoaiCT,
				dm.IdLoaiCongTrinh ,
				isnull(cast(case when parentId.iID_ParentID is not null or dm.iID_ParentID is null then 1 else 0 end as bit),0)  as IsHangCha
			from VDT_DA_DuToan_DM_HangMuc dm
			inner join VDT_DA_DuToan_HangMuc dthm ON dthm.iID_HangMucID = dm.Id AND dthm.iID_DuToanID = @duToanId

		left join
		(
			select distinct iID_ParentID from VDT_DA_DuToan_DM_HangMuc tb1
			inner join VDT_DA_DuToan_HangMuc tb2 ON tb1.Id = tb2.iID_HangMucID AND tb2.iID_DuToanID = @duToanId
			where  tb1.iID_ParentID is not null ) as parentId ON parentId.iID_ParentID = dm.Id
			order by 
			  CAST(SUBSTRING(maOrder, 1, CHARINDEX('_', maOrder + '_', 1) - 1) AS INT),
			  maOrder;
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_dutoan_hangmuc_by_qddautu]    Script Date: 18/12/2023 5:05:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_dutoan_hangmuc_by_qddautu] 
	@qdDauTuId nvarchar(500)
AS
BEGIN

select 
				null as Id,
				null as IdDuToanHangMuc,
				dm.sMaHangMuc as MaHangMuc,
				dm.sTenHangMuc as TenHangMuc,
				qdhm.iID_ChiPhiID as  IdChiPhi,
				qdhm.iID_DuAn_ChiPhi as  IdDuAnChiPhi,
				dm.Id as  IdDuAnHangMuc,
				null as  IIdDuToanId,
				CAST(0 as float) as GiaTriPheDuyet,
				ISNULL(qdhm.fTienPheDuyet, 0 ) as FTienPheDuyetQDDT,
				null as FTienChenhLech,
				dm.iID_ParentID as HangMucParentId,
				CAST(1 as bit) as IsHangMucOld,
				dm.maOrder,
				null as FGiaTriDieuChinh,
				null as GiaTriTruocDieuChinh,
				null as TenChiPhi,
				null as TenLoaiCT,
				dm.IdLoaiCongTrinh ,
				isnull(cast(case when parentId.iID_ParentID is not null or dm.iID_ParentID is null then 1 else 0 end as bit),0)  as IsHangCha
			from VDT_DA_QDDauTu_DM_HangMuc dm
			inner join VDT_DA_QDDauTu_HangMuc qdhm ON qdhm.iID_HangMucID = dm.Id AND qdhm.iID_QDDauTuID = @qdDauTuId

		left join
		(
			select distinct iID_ParentID from VDT_DA_QDDauTu_DM_HangMuc tb1
			inner join VDT_DA_QDDauTu_HangMuc tb2 ON tb1.Id = tb2.iID_HangMucID AND tb2.iID_QDDauTuID = @qdDauTuId
			where  tb1.iID_ParentID is not null ) as parentId ON parentId.iID_ParentID = dm.Id
			order by 
			  CAST(SUBSTRING(maOrder, 1, CHARINDEX('_', maOrder + '_', 1) - 1) AS INT),
			  maOrder;
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khlcnt_get_hangmuc_by_chungtu]    Script Date: 18/12/2023 5:05:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khlcnt_get_hangmuc_by_chungtu]
@iIdChungTu uniqueidentifier,
@sLoaiChungTu nvarchar(1)
AS
BEGIN
	-- DuToan
	IF(@sLoaiChungTu = '1')
	BEGIN
		SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, NULL as IIdChiPhiGocId, tbl.iID_DuAn_ChiPhi as IIdChiPhiId, tbl.iID_HangMucID as IIdHangMucId, hm.iID_ParentID as IIdParentId,
				CAST(0 as bit) as IsHangCha, hm.sTenHangMuc as SNoiDung, hm.maOrder as SMaOrder, tbl.fTienPheDuyet as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau
		FROM VDT_DA_DuToan_HangMuc as tbl
		INNER JOIN VDT_DA_DuToan_DM_HangMuc as hm on tbl.iID_HangMucID = hm.Id
		WHERE tbl.iID_DuToanID = @iIdChungTu
		--ORDER BY hm.maOrder
		ORDER BY 
			  CAST(SUBSTRING(hm.maOrder, 1, CHARINDEX('_', hm.maOrder + '_', 1) - 1) AS INT),
			  hm.maOrder
	END
	-- QdDauTu
	ELSE IF(@sLoaiChungTu = '2')
	BEGIN
		SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, NULL as IIdChiPhiGocId, tbl.iID_DuAn_ChiPhi as IIdChiPhiId, tbl.iID_HangMucID as IIdHangMucId, hm.iID_ParentID as IIdParentId,
				CAST(0 as bit) as IsHangCha, hm.sTenHangMuc as SNoiDung, hm.maOrder as SMaOrder, tbl.fTienPheDuyet as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau
		FROM VDT_DA_QDDauTu_HangMuc as tbl
		INNER JOIN VDT_DA_QDDauTu_DM_HangMuc as hm on tbl.iID_HangMucID = hm.Id
		WHERE tbl.iID_QDDauTuID = @iIdChungTu
		--ORDER BY hm.maOrder
		ORDER BY 
			  CAST(SUBSTRING(hm.maOrder, 1, CHARINDEX('_', hm.maOrder + '_', 1) - 1) AS INT),
			  hm.maOrder
	END
	-- Chu truong dau tu
	ELSE
	BEGIN
		SELECT CAST(0 as bit) as IsChecked, null as IIdGoiThauId, NULL as IIdNguonVonId, null as IIdChiPhiGocId, null as IIdChiPhiId, null as IIdHangMucId, null as IIdParentId,
				CAST(0 as bit) as IsHangCha, null as SNoiDung, null as SMaOrder, CAST(0 as float) as FGiaTriDuocDuyet, CAST(0 as float) as FGiaTriGoiThau
		FROM VDT_DA_GoiThau_HangMuc
		WHERE 0=1
	END
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 19/12/2023 11:27:59 AM ******/
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

			order by IdDonViQuanLy, Loai, STenDuAn

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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_hopdong_find_hangmuc_by_chiphi]    Script Date: 19/12/2023 4:33:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_vdt_hopdong_find_hangmuc_by_chiphi]
	-- Add the parameters for the stored procedure here
	@ChiPhiId uniqueidentifier,
	@GoiThauId uniqueidentifier,
	@IdHopDong uniqueidentifier,
	@IdHopDongGoiThauNhaThau uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select t1.*, ISNULL(t3.sTenHangMuc,t5.sTenHangMuc) as sTenHangMuc, ISNULL(t3.maOrder, t5.maOrder) as maOrder, ISNULL(t4.fGiaTri,0) as fGiatriSuDung, t1.fTienGoiThau - ISNULL(t2.fgiatri, 0) as FGiaTriConLai, 
    ISNULL(t3.iID_ParentID, t5.iID_ParentID) as iID_ParentID, (t1.fTienGoiThau - ISNULL(t2.fgiatri, 0)) as fTienCoTheSD
    from VDT_DA_GoiThau_HangMuc t1
    left join
    (select iID_ChiPhiID, iID_HangMucID, sum(s1.fgiatri) as fgiatri 
    from VDT_DA_HopDong_GoiThau_HangMuc s1
    join VDT_DA_HopDong_GoiThau_NhaThau s2 ON S1.iID_HopDongGoiThauNhaThauID = s2.Id
    join vdt_da_tt_hopdong s3 on s2.iid_hopdongid = s3.id
    where s2.iID_HopDongID != @IdHopDong and s3.bactive = 1
    group by iID_ChiPhiID, iID_HangMucID) t2
    on t1.iID_ChiPhiID = t2.iID_ChiPhiID and t1.iID_HangMucID = t2.iID_HangMucID
    left join VDT_DA_DuToan_DM_HangMuc t3 on t3.id = t1.iID_HangMucID
	left join VDT_DA_QDDauTu_DM_HangMuc t5 on t5.Id = t1.iID_HangMucID
    left join (select * from VDT_DA_HopDong_GoiThau_HangMuc where iID_HopDongGoiThauNhaThauID = @IdHopDongGoiThauNhaThau) t4
    on t1.iID_ChiPhiID = t4.iID_ChiPhiID and t1.iID_HangMucID = t4.iID_HangMucID
    where t1.fTienGoiThau > 0 and (t2.iID_ChiPhiID is null or t1.fTienGoiThau > t2.fgiatri) and t1.iID_ChiPhiID = @ChiPhiId and t1.iID_GoiThauID = @GoiThauId 
	order by 
	  CAST(SUBSTRING(ISNULL(t3.maOrder, t5.maOrder), 1, CHARINDEX('_', ISNULL(t3.maOrder, t5.maOrder) + '_', 1) - 1) AS INT),
	  ISNULL(t3.maOrder, t5.maOrder)
END
;
;
GO
