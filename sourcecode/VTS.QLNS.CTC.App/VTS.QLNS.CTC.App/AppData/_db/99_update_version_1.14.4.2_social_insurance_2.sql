/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_getloigiaithichbangloi]    Script Date: 5/8/2024 8:30:20 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_getloigiaithichbangloi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_getloigiaithichbangloi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_getloigiaithichbangloi]    Script Date: 5/8/2024 8:30:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_getloigiaithichbangloi]
@NamLamViec int,
@MaDonVi nvarchar(max),
@Quy int,
@MaLoaiChi nvarchar(25)
as
begin
		Select
		* 
		from BH_QTC_ChungTuChiTiet_GiaiThich
		where iNamLamViec=@NamLamViec 
		and iID_MaDonVi in (select * from splitstring(@MaDonVi))
		and iQuy=@Quy
		and sMaLoaiChi=@MaLoaiChi;
end
;
;
;
GO
