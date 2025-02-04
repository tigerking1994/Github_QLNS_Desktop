/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso]    Script Date: 12/01/2023 11:58:41 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_find_canbo_quyettoan_quanso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_find_canbo_quyettoan_quanso]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_find_canbo_quyettoan_quanso]    Script Date: 12/01/2023 11:58:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_tl_find_canbo_quyettoan_quanso] @maDonVi nvarchar(MAX), @thang int, @nam int
as
begin

declare @thangtruoc int, @namtruoc int, @strThang nvarchar(2)
if @thang = 1
	begin
		set @thangtruoc = 12;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam - 1;
	end
else if (@thang > 1 and @thang < 10)
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = concat('0', @thang);
		set @namtruoc = @nam;
	end
else 
	begin
		set @thangtruoc = @thang - 1;
		set @strThang = @thang;
		set @namtruoc = @nam;
end

select	cbThangnay.Id, cbThangnay.BHTN, cbThangnay.bNuocNgoai, cbThangnay.Cb_KeHoach, cbThangnay.Cccd, cbThangnay.DateCreated, cbThangnay.DateModified, cbThangnay.Dia_Chi, cbThangnay.Dien_Thoai, 
		cbThangnay.GTGC, cbThangnay.HeSoLuong, cbThangnay.HsLuongKeHoach, cbThangnay.HsLuongTran, cbThangnay.iTrangThai, cbThangnay.IdLuongTran, cbThangnay.IsDelete, cbThangnay.IsLock, 
		cbThangnay.Is_Nam, cbThangnay.Khong_Luong, cbThangnay.Ma_BL, cbThangnay.Ma_CanBo, cbThangnay.Ma_CB, cbThangnay.Ma_CV, cbThangnay.Ma_DiaBan_HC, cbThangnay.Ma_Hieu_CanBo, 
		cbThangnay.Ma_KhoBac, cbThangnay.Ma_PBan, cbThangnay.MaSo_DV_SDNS, cbThangnay.MaSo_VAT, cbThangnay.Ma_TangGiam, cbThangnay.Ma_TangGiamCu, cbThangnay.MaTK_LQ, cbThangnay.Nam, cbThangnay.Nam_TN, 
		cbThangnay.Nam_VK, cbThangnay.NgayCap_CMT, cbThangnay.Ngay_NhanCB, cbThangnay.Ngay_NN, cbThangnay.NgaySinh, cbThangnay.Ngay_TN, cbThangnay.NgayTruyLinh, cbThangnay.Ngay_XN, cbThangnay.Nhom, 
		cbThangnay.NoiCap_CMT, cbThangnay.NoiCongTac, cbThangnay.PCCV, cbThangnay.Parent, cbThangnay.[Readonly], cbThangnay.So_CMT, cbThangnay.So_SoLuong, cbThangnay.So_TaiKhoan, cbThangnay.Splits, 
		cbThangnay.Ten_CanBo, cbThangnay.Ten_DonVi, cbThangnay.Ten_KhoBac, cbThangnay.Thang, cbThangnay.Thang_TNN, cbThangnay.ThoiHan_TangCb, 
		cbThangnay.TM, cbThangnay.UserCreator, cbThangnay.UserModifier, cbThangnay.bKhongTinhNTN, 
		case 
			when cbThangTruoc.Ma_Cb = cbThangnay.Ma_CB then null 
			else cbThangTruoc.Ma_CB 
		end as Ma_CbCu
from (select * from TL_DM_CanBo where Thang = @thang and Nam = @nam) cbThangnay
left join (select * from TL_DM_CanBo where Thang = @thangtruoc and Nam = @namtruoc) cbThangTruoc on cbThangnay.Ma_CanBo = concat(@nam , @strThang, substring(cbThangTruoc.Ma_CanBo, 7, len(cbThangTruoc.Ma_CanBo) - 6))
end
GO
