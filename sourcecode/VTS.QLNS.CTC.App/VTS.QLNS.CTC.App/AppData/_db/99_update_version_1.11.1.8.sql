/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 13/06/2022 6:31:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 13/06/2022 6:31:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 13/06/2022 6:31:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_nh_quanli_giao_duan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_nh_quanli_giao_duan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_nh_quanli_giao_duan_index]    Script Date: 13/06/2022 6:31:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_nh_quanli_giao_duan_index] 
AS
BEGIN
	WITH SoLieuDieuChinh AS 
	 (
		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp 
		WHERE 
			pbvcp.iID_ParentId is not null

		UNION ALL

		SELECT 
			pbvcp.Id , pbvcp.iID_ParentId
		FROM 
			VDT_KHV_PhanBoVon_ChiPhi pbvcp JOIN SoLieuDieuChinh ctpr ON pbvcp.iID_ParentId = ctpr.Id 
		WHERE pbvcp.iID_ParentId is not null
	  ),SoLanDieuChinh AS (
		   SELECT
			sdc.Id,sdc.iID_ParentId,  COUNT(sdc.Id) AS iSoLanDieuChinh
		  FROM 
			SoLieuDieuChinh sdc
		  GROUP BY sdc.iID_ParentId,sdc.Id
	  )
	select pbvcp.Id as Id, 
		pbvcp.sSoQuyetDinh as SSoQuyetDinh,
		pbvcp.dNgayQuyetDinh as DNgayQuyetDinh,
		pbvcp.iID_DuAnID as IIdDuAnId,
		pbvcp.iNamKeHoach as INamKeHoach,
		pbvcp.iID_LoaiNguonVonID as IIdLoaiNguonVonId,
		pbvcp.iID_DonViID as IIdDonViId,
		pbvcp.iID_MaDonVi as IIdMaDonVi,
		pbvcp.sLoaiDieuChinh as SLoaiDieuChinh,
		pbvcp.iID_ParentId as IIdParentId,
		pbvcp.bActive as BActive,
		pbvcp.bIsGoc as BIsGoc,
		pbvcp.fGiaTriDuocDuyet as FGiaTriDuocDuyet,
		pbvcp.iLoai as ILoai,
		pbvcp.iID_PhanBoGoc_ChiPhiID as IIdPhanBoGocChiPhiId,
		isnull(tbl.iSoLanDieuChinh,0) AS ILanDieuChinh ,
		pbvcp.bKhoa as BKhoa,
		dv.sTenDonVi as STenDonVi,
		nns.sTen as STenNguonVon,
		pbvcp.sUserCreate,
		pbvcp.dDateCreate,
		pbvcp.sUserUpdate,
		pbvcp.dDateUpdate,
		pbvcp.sUserDelete,
		pbvcp.dDateDelete,
		case
			when tbl.iID_ParentId is null then ''
			else (select pbvcp.sSoQuyetDinh from VDT_KHV_PhanBoVon_ChiPhi pbvcp where pbvcp.Id = tbl.iID_ParentId)
		end DieuChinhTu
	from VDT_KHV_PhanBoVon_ChiPhi pbvcp
	left join DonVi dv on pbvcp.iID_DonViID = dv.iID_DonVi  
	left join NguonNganSach nns on pbvcp.iID_LoaiNguonVonID = nns.iID_MaNguonNganSach 
	left join SoLanDieuChinh tbl ON tbl.Id = pbvcp.Id
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 13/06/2022 6:31:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
	@thang int, @nam int, @thangTruoc int, @namTruoc int, @maDonVi nvarchar(MAX), @sM nvarchar(1)
As
Begin
	if @sM = '3'
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		)

		Select 
			canbo.Ten_CanBo TenCanBo,
			CASE 
				WHEN canbo.Ma_TangGiam in ('250', '280') THEN canbothangtruoc.CapBacCu
				ELSE canbo.Ma_CB
			END CapBac,
			CAST('1' as int) as SoLuong,
			CASE 
				WHEN canbo.Ma_TangGiam in ('290') THEN canbothangtruoc.TenDonViCu
				ELSE canbo.Ten_DonVi
			END DonVi,
			mlqs.sMoTa NoiDung
		From TL_DM_CanBo canbo
			Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
			Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
		Where canbo.Thang = @thang
			And canbo.Nam = @nam
			And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280', '290'))
			And iNamLamViec = @nam
			And bHangCha = 0
			And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
		Order By Ma_DonVi, CapBac
	else
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		),

		KhongTuyenQuan as (
			Select 
				canbo.Ten_CanBo TenCanBo,
				
				CASE 
					WHEN canbo.Ma_TangGiam in ('350', '380') THEN canbothangtruoc.CapBacCu
					ELSE canbo.Ma_CB
				END CapBac,
				CAST('1' as int) as SoLuong,
				CASE 
					WHEN canbo.Ma_TangGiam in ('390') THEN canbothangtruoc.TenDonViCu
					ELSE canbo.Ten_DonVi
				END DonVi,
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
				Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbo.Ma_Hieu_CanBo
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And (sM = @sM OR canbo.Ma_TangGiam in ('350','380','390'))
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam not in ('210', '220')),

		TuyenQuan as (
		Select 
			(CAST(COUNT(*) as nvarchar(MAX)) + N' đồng chí') as TenCanBo, 
			canbo.Ma_CB CapBac, 
			COUNT(*) SoLuong,
			donvi.Ten_DonVi DonVi, 
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And sM = @sM
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam in ('210', '220')
			Group By canbo.Ma_CB, donvi.Ten_DonVi, mlqs.sMoTa
		)

		Select *
		From KhongTuyenQuan
		Union
		Select *
		From TuyenQuan
		--Order By DonVi, CapBac desc
		Order By DonVi, CapBac
End
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 13/06/2022 6:31:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtri_index]
@iIdLoaiThongTri uniqueidentifier
AS
BEGIN
	SELECT tbl.Id,sMaThongTri,dNgayThongTri,iNamThongTri,sNguoiLap,sTruongPhong,sThuTruongDonVi,sMaNguonVon,iID_DonViID,iID_MaDonViID,tbl.sMoTa,sUserCreate,dDateCreate,sUserUpdate,dDateUpdate,sUserDelete,dDateDelete,
	tbl.sMaLoaiCongTrinh,iID_LoaiThongTriID,iID_NhomQuanLyID,bIsCanBoDuyet,bIsDuyet,bThanhToan,ISNULL(tbl.ILoaiThongTri, 1) as ILoaiThongTri, dv.sTenDonVi as sTenDonVi, NULL as dNgayLapGanNhat, lct.sTenLoaiCongTrinh, tbl.INamNganSach
	FROM VDT_ThongTri as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tbl.sMaLoaiCongTrinh = lct.sMaLoaiCongTrinh
	WHERE dDateDelete IS NULL AND tbl.iID_LoaiThongTriID = @iIdLoaiThongTri
	ORDER BY dDateCreate DESC
END
GO
