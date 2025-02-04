/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_quanso_nam]    Script Date: 2/8/2023 8:51:27 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_get_donvi_quanso_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_get_donvi_quanso_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 2/8/2023 11:31:21 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 2/8/2023 11:31:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
	@thang int, @nam int, @thangTruoc int, @namTruoc int, @maDonVi nvarchar(MAX), @sM nvarchar(1)
As
Begin
	if @sM = '3'
		-- giảm
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
			--And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280', '290'))
			And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280'))
			And iNamLamViec = @nam
			And bHangCha = 0
			And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
		Order By Ma_DonVi, CapBac
	else
		-- tăng
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
				left Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				--And (sM = @sM OR canbo.Ma_TangGiam in ('350','380', '390'))
				And (sM = @sM OR canbo.Ma_TangGiam in ('350','380'))
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam not in ('210', '220')
				),

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
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_get_donvi_quanso_nam]    Script Date: 2/8/2023 8:51:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_tl_get_donvi_quanso_nam] @nam int
as
begin
select * from tl_dm_donvi
where Ma_DonVi in (SELECT Parent FROM TL_DM_CanBo WHERE Nam = @nam)
end
GO