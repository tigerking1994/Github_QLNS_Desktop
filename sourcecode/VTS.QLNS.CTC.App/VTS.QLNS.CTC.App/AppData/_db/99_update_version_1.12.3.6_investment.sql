/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]    Script Date: 12/6/2022 4:31:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 12/6/2022 4:31:30 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]    Script Date: 12/6/2022 4:31:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_find_phanbovondonvichitietpheduyet]
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
		pbvct.sGhiChu as sGhiChu,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FCapPhatTaiKhoBac,
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		pbvct.fGiaTriThuHoi as FGiaTriThuHoi,
		case when pbvct.fGiaTrPhanBo is not null then pbvct.fGiaTrPhanBo else pbvdxct.fThanhToan end as FGiaTrPhanBo,
		--isnull(pbvct.Id, NEWID()) as IIdPhanBoVonId,
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID as IIdPhanBoVon,
		pbvct.ILoaiDuAn as ILoaiDuAn,
		pbv.Id as IdChungTu,
		pbv.iID_ParentId as IdChungTuParent,
		pbv.bActive as BActive,
		pbv.bIsGoc as IsGoc,
		--case when pbvct.fThanhToanDeXuat is not null then pbvct.fThanhToanDeXuat else pbvdxct.fThanhToan end as fThanhToanDeXuat
		pbvdxct.fThanhToan as fThanhToanDeXuat
	from
		VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet pbvct
	inner join
		VDT_KHV_PhanBoVon_DonVi_PheDuyet pbv
	on pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = pbv.Id
	inner join 
	VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdxct on pbvdxct.iID_DuAnID = pbvct.iID_DuAnID
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
		pbvct.iID_PhanBoVon_DonVi_PheDuyet_ID = @iIdPhanBoVon
		and pbv.bIsGoc = 1 and pbvdxct.bActive = 1
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]    Script Date: 12/6/2022 4:31:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_vdt_get_duan_in_phanbovon_donvi_pheduyet]
	@idPhanBoVonDeXuat nvarchar(max),
	@nguonVonID int
AS
Begin
	select 
		distinct
		pbvdvct.iID_DuAnID,
		pbvdvct.iID_LoaiCongTrinhID,
		case when ctpd.fThanhToanDeXuat is null then pbvdvct.fThanhToan else ctpd.fThanhToanDeXuat end as fThanhToanDeXuat
		into #tmp_duan
	from VDT_KHV_PhanBoVon_DonVi_ChiTiet pbvdvct
	left join VDT_KHV_PhanBoVon_DonVi_ChiTiet_PheDuyet ctpd on ctpd.iID_DuAnID = pbvdvct.iID_DuAnID
	where
		pbvdvct.iId_PhanBoVon_DonVi in (select  * from dbo.splitstring(@idPhanBoVonDeXuat));

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
		tmp_da.iID_LoaiCongTrinhID as iID_LoaiCongTrinhID,
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
		cast(0 as float) as FCapPhatBangLenhChi,
		cast(0 as float) as FGiaTriThuHoiNamTruocKhoBac,
		cast(0 as float) as FGiaTriThuHoiNamTruocLenhChi,
		cast(0 as float) as fChiTieuGoc,
		cast(0 as float) as FGiaTriThuHoi,
		cast(tmp_da.fThanhToanDeXuat as float) as FGiaTrPhanBo,
		case
			when ((da.sTrangThaiDuAn = N'THUC_HIEN') and (da.bIsKetThuc IS NULL)) then 2 else 1
		end ILoaiDuAn,
		dv.sTenDonVi as STenDonViThucHienDuAn,
		dv.iID_MaDonVi as IIdMaDonViThucHienDuAn,
		tmp_da.fThanhToanDeXuat
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
	on tmp_da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	left join
		VDT_DM_DonViThucHienDuAn dv
	on 
		da.iID_MaDonViThucHienDuAnID = dv.iID_MaDonVi

	drop table #tmp_duan

End
;
;
GO
