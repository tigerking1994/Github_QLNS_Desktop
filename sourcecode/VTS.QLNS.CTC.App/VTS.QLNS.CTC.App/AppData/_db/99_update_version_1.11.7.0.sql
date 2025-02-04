/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 22/08/2022 8:46:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 22/08/2022 8:46:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_von_bo_tri_5_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 22/08/2022 8:46:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 22/08/2022 8:46:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 22/08/2022 4:49:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_report_cap_phat_thanh_toan_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_duoc_duyet_export]    Script Date: 22/08/2022 4:49:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_khv_kehoach_5_nam_duoc_duyet_export]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_khv_kehoach_5_nam_duoc_duyet_export]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 22/08/2022 4:49:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 22/08/2022 4:49:29 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_dexuat_dieuchinh_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_dexuat_dieuchinh_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 22/08/2022 4:49:29 PM ******/
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
			into #tmpData
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

		select 
				cast(1 as bit) as IsHangCha,
				1 as Loai,
				'' as STT,
				dv.iID_MaDonVi as IdDonViQuanLy,
				dv.sTenDonVi as STenDuAn,
				SUM(isnull(ctddct.fHanMucDauTu/@MenhGiaTienTe, 0)) as FHanMucDauTuDuocDuyet,
				(SUM(isnull(ctddct.fVonBoTriTuNamDenNam/@MenhGiaTienTe, 0)) + SUM(isnull(ctddct.fGiaTriBoTri, 0))) as FTongSoDuocDuyet,
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
				inner join
					VDT_KHV_KeHoach5Nam_ChiTiet ctddct
				on
					ctdxct.iID_DuAnID = ctddct.iID_DuAnID and ctdxct.iID_LoaiCongTrinhID = ctddct.iID_LoaiCongTrinhID and ctdxct.iID_NguonVonID = ctddct.iID_NguonVonID
				where 
					dv.iID_MaDonVi in (select * from dbo.splitstring(@lstDonVi)) and dv.iNamLamViec = @namLamViec
					and ctdx.Id in (select * from dbo.splitstring(@VoucherId))
					and ctddct.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))
				group by dv.iID_MaDonVi, dv.sTenDonVi

			union all

				select * from #tmpData

			order by Loai, IdDonViQuanLy

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
	end
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_dotnhan]    Script Date: 15/12/2021 6:35:11 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_tonghop]    Script Date: 22/08/2022 4:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_tonghop]
	 @NamLamViec int,													
	 @IdDonvi nvarchar(2000),
	 @LoaiChungTu nvarchar(50),
	 @DonViTinh int
AS
BEGIN 
	SET NOCOUNT ON;
SELECT chitiet.*, mlns.iID_MLNS AS MlnsId, iID_MLNS_Cha AS MlnsIdParent FROM (	
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
       sMoTa AS MoTa ,
       sXauNoiMa AS XauNoiMa ,
       QuyetToan =sum(ISNULL(QuyetToan, 0))/@DonViTinh ,
       DuToan =sum(isnull(DuToan, 0))/@DonViTinh ,
       TuChi =sum(TuChi)/@DonViTinh ,
	   UocThucHien =sum(fUocThucHien)/@DonViTinh
FROM
  ( 
 
 SELECT sLNS,
        sL,
        sK,
        sM,
        sTM,
        sTTM,
        sNG,
        sMoTa ,
        sXauNoiMa ,
        QuyetToan =0 ,
        DuToan =0 ,
        CASE
            WHEN @LoaiChungTu = '1' THEN fTuChi
            WHEN @LoaiChungTu = '2' THEN fHangNhap + fHangMua + fPhanCap
            ELSE 0
        END AS TuChi ,
		fUocThucHien
   FROM NS_DTDauNam_ChungTuChiTiet
   WHERE iNamLamViec=@NamLamViec
     AND iLoai=3
     AND iLoaiChungTu = @LoaiChungTu
     AND (@IdDonvi IS NULL
          OR iID_MaDonVi in
            (SELECT *
             FROM f_split(@IdDonvi)))-- lay can cu quyet toan, du toan

   UNION ALL SELECT LNS,
                    L,
                    K,
                    M,
                    TM,
                    TTM,
                    NG,
                    MoTa,
                    XauNoiMa ,
                    QuyetToan ,
                    DuToan ,
                    TuChi =0 ,
					UocThucHien = 0
   FROM f_skt_dulieu(@NamLamViec, @IdDonvi, @LoaiChungTu)) AS dt
GROUP BY sLNS,
         sL,
         sK,
         sM,
         sTM,
         sTTM,
         sNG,
         sMoTa,
         sXauNoiMa
HAVING --sum(QuyetToan)<>0
 --or sum(DuToan)<>0
 --or sum(TuChi)<>0
 sum(TuChi)<>0
OR sum(fUocThucHien)<>0
) chitiet  left join (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on chitiet.XauNoiMa = mlns.sXauNoiMa

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_in_kehoachtrunghan]    Script Date: 22/08/2022 4:49:29 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_vdt_khv_kehoach_5_nam_duoc_duyet_export]    Script Date: 22/08/2022 4:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_khv_kehoach_5_nam_duoc_duyet_export]
	@Id nvarchar(max),
	@lct nvarchar(max),
	@IdNguonVon int,
	@type int,
	@MenhGiaTienTe float,
	@lstDonViThucHienDuAn nvarchar(max)
AS
BEGIN
	if(@type = 1)
	begin
		select tbl.* from (

		select
			lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			lct.iID_Parent as IdLoaiCongTrinhParent,
			lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			3 as Loai,
			lct.sTenLoaiCongTrinh as STenDuAn,
			0 as FHanMucDauTu,
			0 as FVonDaGiao,
			0 as FTongVonBoTri,
			0 as FGiaTriKeHoach,
			0 as FVonBoTri,
			'' as GhiChu,
			cast(1 as bit) as IsHangCha,
			NEWID() as Id,
			case
				when lct.iID_Parent is null then 0 else 1
			end LoaiParent
		from f_loai_cong_trinh_get_list_childrent(@lct) lct

		union all

		select 
			dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
			dmlct.iID_Parent as IdLoaiCongTrinhParent,
			dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
			4 as Loai,
			ctct.sTen as STenDuAn,
			(ctct.fHanMucDauTu/@MenhGiaTienTe) as FHanMucDauTu,
			(ctct.fVonDaGiao/@MenhGiaTienTe) as FVonDaGiao,
			((ctct.fVonBoTriTuNamDenNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe) as FTongVonBoTri,
			(ctct.fVonBoTriTuNamDenNam/@MenhGiaTienTe) as FGiaTriKeHoach,
			(ctct.fGiaTriBoTri/@MenhGiaTienTe) as FVonBoTri,
			ctct.sGhiChu as GhiChu,
			cast(0 as bit) as IsHangCha,
			NEWID() as Id,
			2 as LoaiParent
		from 
			f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
			VDT_KHV_KeHoach5Nam_ChiTiet ctct
		on
			dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		where
			ctct.iID_KeHoach5NamID in (select * from dbo.splitstring(@Id))
			and ctct.iID_NguonVonID = @IdNguonVon
			--and ctct.iID_ParentID is null
			) as tbl  order by tbl.IdLoaiCongTrinh, tbl.Loai
	end
	else if(@type = 2)
	begin
		select 
		  dmlct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
		  dmlct.iID_Parent as IdLoaiCongTrinhParent,
		  dmlct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
		  5 as Loai,
		  ctct.iID_KeHoach5NamID,
		  ctct.sTen as STenDuAn,
		  ctct.iID_DonViQuanLyID as IdDonViThucHienDuAn,
		  ctct.fHanMucDauTu/@MenhGiaTienTe as FHanMucDauTu,
		  ctct.fVonDaGiao/@MenhGiaTienTe as FVonDaGiao,
		  (ctct.fVonBoTriTuNamDenNam + ctct.fGiaTriBoTri)/@MenhGiaTienTe as FTongVonBoTri,
		  ctct.fVonBoTriTuNamDenNam/@MenhGiaTienTe as FGiaTriKeHoach,
		  ctct.fGiaTriBoTri/@MenhGiaTienTe as FVonBoTri,
		  ctct.sGhiChu as GhiChu,
		  cast(0 as bit) as IsHangCha,
		  2 as LoaiParent
		  into #tmp
		from 
		  f_loai_cong_trinh_get_list_childrent(@lct) dmlct
		left join
		  VDT_KHV_KeHoach5Nam_ChiTiet ctct
		on
		  dmlct.iID_LoaiCongTrinh = ctct.iID_LoaiCongTrinhID
		where
		  ctct.iID_KeHoach5NamID in (select * from dbo.splitstring(@Id))
		  and ctct.iID_NguonVonID = @IdNguonVon
		  and ctct.iID_MaDonVi in (select * from dbo.splitstring(@lstDonViThucHienDuAn))

		  ---------------------------------------------------------
		  select khct.IdLoaiCongTrinh, khct.IdLoaiCongTrinhParent, khct.SMaLoaiCongTrinh, 4 as Loai, khct.iID_KeHoach5NamID, dv.sTenDonVi as STenDuAn, khct.IdDonViThucHienDuAn,
		  Sum(khct.FHanMucDauTu) as FHanMucDauTu,
		  Sum(khct.FVonDaGiao) as FVonDaGiao,
		  Sum(khct.FTongVonBoTri) as FTongVonBoTri,
		  Sum(khct.FGiaTriKeHoach) as FGiaTriKeHoach,
		  Sum(khct.FVonBoTri) as FVonBoTri,
		  '' as GhiChu,
		  cast(1 as bit) as IsHangCha,
		  khct.LoaiParent
		  into #tmpDuAn
		  from #tmp khct
		  left join VDT_DM_DonViThucHienDuAn dv on khct.IdDonViThucHienDuAn = dv.iID_DonVi
		  group by khct.IdLoaiCongTrinh, khct.IdLoaiCongTrinhParent, khct.SMaLoaiCongTrinh, khct.Loai, khct.iID_KeHoach5NamID, dv.sTenDonVi, khct.IsHangCha,khct.LoaiParent, khct.IdDonViThucHienDuAn

	select tbl_sum.* from (

		  select
		  lct.iID_LoaiCongTrinh as IdLoaiCongTrinh,
		  lct.iID_Parent as IdLoaiCongTrinhParent,
		  lct.sMaLoaiCongTrinh as SMaLoaiCongTrinh,
		  3 as Loai,
		  null as iID_KeHoach5NamID,
		  lct.sTenLoaiCongTrinh as STenDuAn,
		  null as IdDonViThucHienDuAn,
		  0 as FHanMucDauTu,
		  0 as FVonDaGiao,
		  0 as FTongVonBoTri,
		  0 as FGiaTriKeHoach,
		  0 as FVonBoTri,
		  '' as GhiChu,
		  cast(1 as bit) as IsHangCha,
		  case
			when lct.iID_Parent is null then 0 else 1
		  end LoaiParent
		from f_loai_cong_trinh_get_list_childrent(@lct) lct
		union all
		select * from #tmp
		union all
		select * from #tmpDuAn
	) as tbl_sum
		order by tbl_sum.IdLoaiCongTrinh, tbl_sum.iID_KeHoach5NamID, tbl_sum.IdDonViThucHienDuAn, tbl_sum.Loai
	
		drop table #tmp
		drop table #tmpDuAn
	end
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 22/08/2022 4:49:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
@Id varchar(MAX),
@NamLamViec int 
AS 
BEGIN

	SELECT duan.sTenDuAn AS TenDuAn,
		duan.sMaDuAn AS MaDuAn,
		donvi.sTenDonVi AS TenDonVi,
		chudadutu.sTenDonVi AS TenChuDauTu,
		hopdong.sSoHopDong as TenHopDong,
		hopdong.dNgayHopDong AS NgayHopDong,
		hopdong.fTienHopDong AS GiaTriHopDong,
		thanhtoan.sSoDeNghi AS SoDeNghi,
		thanhtoan.iLoaiThanhToan AS LoaiThanhToan,
		nguonvon.sTen AS TenNguonVon,
		thanhtoan.iNamKeHoach AS NamKeHoach,
		isnull(thanhtoan.fGiaTriThanhToanTN, 0) AS ThanhToanTN,
		isnull(thanhtoan.fGiaTriThanhToanNN, 0) AS ThanhToanNN,
		(isnull(thanhtoan.fGiaTriThuHoiTN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocTN, 0)) AS ThuHoiTN,
		(isnull(thanhtoan.fGiaTriThuHoiNN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocNN, 0)) AS ThuHoiNN,
		isnull(thanhtoan.fThueGiaTriGiaTang, 0) AS ThueGiaTriGiaTang,
		isnull(thanhtoan.fChuyenTienBaoHanh, 0) AS ChuyenTienBaoHanh,
		thanhtoan.sGhiChu as NoiDung,
		nhathau.sTenNhaThau AS TenNhaThau,
		nhathau.sSoTaiKhoan AS SoTaiKhoanNhaThau,
		pbv.sSoQuyetDinh as duToanPheDuyet
	FROM VDT_TT_DeNghiThanhToan thanhtoan
	LEFT JOIN VDT_DA_DuAn duan ON thanhtoan.iID_DuAnId = duan.iID_DuAnID
	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec ) donvi ON thanhtoan.iID_MaDonViQuanLy = donvi.iID_MaDonVi
	left join VDT_KHV_PhanBoVon pbv on thanhtoan.iID_PhanBoVonID = pbv.Id
	LEFT JOIN VDT_DA_TT_HopDong hopdong ON thanhtoan.iID_HopDongId = hopdong.Id
	LEFT JOIN NguonNganSach nguonvon ON thanhtoan.iID_NguonVonID = nguonvon.iID_MaNguonNganSach
	LEFT JOIN DM_ChuDauTu chudadutu ON chudadutu.iID_DonVi = duan.iID_ChuDauTuID
	LEFT JOIN VDT_DM_NhaThau nhathau on thanhtoan.iID_NhaThauId = nhathau.Id
	WHERE thanhtoan.Id = @Id
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]    Script Date: 22/08/2022 8:46:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_rpt_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam]
	 @NamLamViec int,
	 @XauNoiMa ntext
AS
BEGIN 
	SET NOCOUNT ON;
	 select * FROM f_get_parent_mlns_by_lns_xau_noi_ma_report_dutoandaunam(@NamLamViec,@XauNoiMa) Order by sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG
END
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_vdt_get_von_bo_tri_5_nam]    Script Date: 22/08/2022 8:46:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_von_bo_tri_5_nam]
	@lstId nvarchar(max),
	@YearPlan int
AS
BEGIN
	select  khthddct.fHanMucDauTu
	from VDT_KHV_KeHoach5Nam khthdd
	INNER JOIN VDT_KHV_KeHoach5Nam_ChiTiet khthddct
		on khthdd.iID_KeHoach5NamID = khthddct.iID_KeHoach5NamID
	where khthdd.iGiaiDoanTu <= @YearPlan AND khthdd.iGiaiDoanDen >= @YearPlan 
	and khthddct.iID_DuAnID = @lstId 
	and khthddct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]    Script Date: 22/08/2022 8:46:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--exec sp_vdt_lay_gia_tri_denghi_thanh_toan 0, '346dc9b1-5053-4ebe-a585-6765ab155f10', '2022/04/27', 1, 2022, 1

CREATE PROCEDURE [dbo].[sp_vdt_lay_gia_tri_denghi_thanh_toan_edit_3]
	@bThanhToanTheoHopDong bit,
	@iIdChungTu varchar(max),
	@NgayDeNghi datetime,
	@NguonVonId int,
	@NamKeHoach int,
	@iCoQuanThanhToan int,
	@loaiCoQuanTaiChinh int
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
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngUngTruocTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(2,4) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngUngTruocNN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanTN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanTN, 0)) ELSE SUM(0) END) as TamUngCheDoTN,
		(case WHEN iLoaiThanhToan = 0 AND ISNULL(dt.fGiaTriThanhToanNN,0) <> 0 AND khv.iLoai in(1,3) THEN SUM(ISNULL(dt.fGiaTriThanhToanNN, 0)) ELSE SUM(0) END) as TamUngCheDoNN
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
GO
INSERT [dbo].[TL_DM_PhuCap] ([Id], [bGiaTri], [bHuongPc_Sn], [bSaoChep], [Chon], [Cong_Thuc], [Dinh_Dang], [Gia_Tri], [He_So], [HuongPC_SN], [iDinhDang], [iLoai], [Is_Formula], [Is_Readonly], [IThang_ToiDa], [Ma_KMCP], [Ma_PhuCap], [Ma_TTM_Ng], [Numeric_Scale], [Parent], [PhanTram_CT], [Readonly], [Splits], [Ten_Ngan], [Ten_PhuCap], [Tinh_BHXH], [Tinh_TNCN], [Xau_Noi_Ma], [XSort]) VALUES (N'8d20efb4-baf5-495b-980a-4e09f9ef2377', 0, 0, 0, 0, NULL, 0, CAST(6.000 AS Numeric(17, 3)), NULL, NULL, 0, NULL, 0, 1, NULL, NULL, N'THANG_TCVIECLAM', NULL, NULL, N'CHIKHAC', NULL, NULL, NULL, NULL, N'Số tháng hưởng trợ cấp việc làm', 0, NULL, N'CHIKHAC-THANG_TCVIECLAM', NULL)
GO

INSERT [dbo].[TL_DM_Cach_TinhLuong_Chuan] ([Id], [CongThuc], [Ma_CachTL], [Ma_Cot], [Ma_KMCP], [Ma_KMCP1], [Nam], [NoiDung], [Ten_CachTL], [Ten_Cot], [Thang]) VALUES (N'f7487cba-6200-4e81-b23a-a3b729ad6281', N'THANG_TCVIECLAM*LCS', N'CACH0', N'TCVIECLAM_TT', NULL, NULL, 2022, NULL, NULL, N'Trợ cấp việc làm', 8)
GO

update TL_DM_PhuCap
set Gia_Tri = 0
where Ma_PhuCap = 'TCVIECLAM_TT'

update TL_DM_PhuCap set Gia_Tri=0 where Ma_PhuCap='PCTEMTHU_TT'