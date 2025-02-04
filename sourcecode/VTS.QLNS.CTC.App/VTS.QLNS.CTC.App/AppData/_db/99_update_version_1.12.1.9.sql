/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chungtu]    Script Date: 28/10/2022 8:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_ldtdn_delete_chungtu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_ldtdn_delete_chungtu]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 28/10/2022 8:03:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]    Script Date: 28/10/2022 8:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_ns_ldtdn_delete_chitiet_by_delete_mlns]
@iID_CTDTDauNam AS uniqueidentifier ,
@sMLNS AS nvarchar(max),
@iNamLamViec int

As
Begin

	--Xóa chứng từ đầu năm chi tiết 
	Delete NS_DTDauNam_ChungTuChiTiet 
	where iID_CTDTDauNam = @iID_CTDTDauNam 
	and sLNS  in  (SELECT * FROM f_split(@sMLNS))
	and iNamLamViec = @iNamLamViec

End;

GO
/****** Object:  StoredProcedure [dbo].[sp_ns_ldtdn_delete_chungtu]    Script Date: 28/10/2022 8:03:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_ns_ldtdn_delete_chungtu]
@iID_CTSoKiemTra AS uniqueidentifier ,
@iID_MaDonVi AS nvarchar(max),
@iNamLamViec int

as
begin
    declare @istonghop int;
	--Kiểm tra nếu chứng từ được xóa có đơn vị là đơn vị tổng hợp thì thực hiện xóa chứng từ và chi tiết chứng từ
	set @istonghop = (select  top 1 iLoai from DonVi where iNamLamViec = @iNamLamViec and iID_MaDonVi = @iID_MaDonVi)
	if (@istonghop is not null and @istonghop = 0)
		begin
			delete NS_SKT_ChungTuChiTiet where iID_CTSoKiemTra = @iID_CTSoKiemTra and iNamLamViec = @iNamLamViec
			delete NS_SKT_ChungTu where iID_CTSoKiemTra = @iID_CTSoKiemTra and iNamLamViec = @iNamLamViec
		end
	--Nếu chứng từ được xóa có đơn vị không là đơn vị tổng hợp thì chỉ xóa chứng từ chi tiết theo đơn vị
	else
	  delete NS_SKT_ChungTuChiTiet where iID_CTSoKiemTra = @iID_CTSoKiemTra and iNamLamViec = @iNamLamViec and iID_MaDonVi = @iID_MaDonVi
end
GO
