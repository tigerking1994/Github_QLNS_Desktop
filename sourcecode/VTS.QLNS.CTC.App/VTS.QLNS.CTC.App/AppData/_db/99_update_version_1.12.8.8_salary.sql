/****** Object:  StoredProcedure [dbo].[sp_tl_delete_bang_luong]    Script Date: 6/7/2023 4:32:55 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_bang_luong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_bang_luong]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_bang_luong]    Script Date: 6/7/2023 4:32:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ALTER Procedure [dbo].[sp_tl_delete_bang_luong]
--	@idBangLuong nvarchar(MAX)
--As
--Begin
--	Delete From [dbo].[TL_DS_CapNhap_BangLuong]
--	Where Id IN (SELECT *
--	FROM f_split(@idBangLuong))
--	Delete From [dbo].[TL_BangLuong_Thang]
--	Where parent IN (SELECT *
--	FROM f_split(@idBangLuong))
--End

CREATE Procedure [dbo].[sp_tl_delete_bang_luong]
	@thang int, 
	@nam int,
	@maDonVi nvarchar(MAX),
	@maCachTl nvarchar(10)
As
Begin
	Delete From [dbo].[TL_DS_CapNhap_BangLuong]
	Where thang = @thang and nam = @nam and ma_cbo in (select * from f_split(@maDonVi)) and Ma_CachTL = @maCachTl
	Delete From [dbo].[TL_BangLuong_Thang]
	Where thang = @thang and nam = @nam and Ma_DonVi in (select * from f_split(@maDonVi)) and Ma_CachTL = @maCachTl
End
;
;
GO
