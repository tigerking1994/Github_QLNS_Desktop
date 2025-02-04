/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 13/03/2023 8:30:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 13/03/2023 8:30:57 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khv_khth_dexuat_dieuchinh_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khv_khth_dexuat_dieuchinh_report]
GO
/****** Object:  StoredProcedure [dbo].[sp_khv_khth_dexuat_dieuchinh_report]    Script Date: 13/03/2023 8:30:57 AM ******/
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
			and ctctdd.iID_NguonVonID in (select * from dbo.splitstring(@IdNguonVon))
	end
END
/****** Object:  StoredProcedure [dbo].[sp_dt_danhsach_dotnhan]    Script Date: 15/12/2021 6:35:11 PM ******/
SET ANSI_NULLS ON
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 13/03/2023 8:30:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu] 
	@IdChuTruongDauTu nvarchar(200),
	@MaDonVi nvarchar(200),
	@NamLV int
AS
BEGIN
	SELECT * FROM VDT_DA_DuAn duan
	INNER JOIN DonVi dv
		ON dv.iID_MaDonVi = duan.iID_MaDonViQuanLy
	WHERE
		duan.iID_DuAnID NOT IN   
		(
			SELECT DISTINCT iID_DuAnID FROM VDT_DA_ChuTruongDauTu WHERE iID_DuAnID = duan.iID_DuAnID
			AND iID_ChuTruongDauTuID <> @IdChuTruongDauTu
		)
		AND duan.iID_DuAnID IN 
		(
			SELECT DISTINCT iID_DuAnID from VDT_KHV_KeHoach5Nam_ChiTiet
		)
		----AND dv.iID_MaDonVi = @MaDonVi
		--AND dv.iID_MaDonVi  in (
		---- lay don vi C2
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi
		--	union all  
		--	-- lay don vi C3
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha  
		--	in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	union all  
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha
		--	in (--lay don vi C4
		--		select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha 
		--		in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	)
		--)
		AND dv.iID_MaDonVi = @MaDonVi
		AND dv.iNamLamViec = @NamLV;
END;
;
;



GO
