/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtcqn_delete_duplicate_item]    Script Date: 1/12/2024 10:07:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_qtcqn_delete_duplicate_item]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_qtcqn_delete_duplicate_item]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_giai_thich_tro_cap_delete_duplicate_item]    Script Date: 1/12/2024 10:07:38 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_giai_thich_tro_cap_delete_duplicate_item]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_giai_thich_tro_cap_delete_duplicate_item]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_giai_thich_tro_cap_delete_duplicate_item]    Script Date: 1/12/2024 10:07:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_giai_thich_tro_cap_delete_duplicate_item] 
	@VoucherID uniqueidentifier
AS
BEGIN
	delete from BH_QTC_Quy_CTCT_GiaiThichTroCap
	where iiD_QTC_Quy_CTCT_GiaiThichTroCap in (
		select iiD_QTC_Quy_CTCT_GiaiThichTroCap from
		(select iiD_QTC_Quy_CTCT_GiaiThichTroCap,
			ROW_NUMBER()OVER(PARTITION BY sXauNoiMa, iID_MaDonVi, sMaCapbac ORDER BY iiD_QTC_Quy_CTCT_GiaiThichTroCap) row_rank
		from BH_QTC_Quy_CTCT_GiaiThichTroCap
		where iID_QTC_Quy_ChungTu = @VoucherID) tbl_rank
		where tbl_rank.row_rank > 1)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_qtcqn_delete_duplicate_item]    Script Date: 1/12/2024 10:07:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_qtcqn_delete_duplicate_item]
	@VoucherID uniqueidentifier
AS
BEGIN

	delete from BH_QTC_Quy_CheDoBHXH_ChiTiet
	where ID_QTC_Quy_CheDoBHXH_ChiTiet in (
		select ID_QTC_Quy_CheDoBHXH_ChiTiet from
		(select ID_QTC_Quy_CheDoBHXH_ChiTiet,
			ROW_NUMBER()OVER(PARTITION BY sXauNoiMa, iIDMaDonVi ORDER BY ID_QTC_Quy_CheDoBHXH_ChiTiet) row_rank
		from BH_QTC_Quy_CheDoBHXH_ChiTiet
		where iID_QTC_Quy_CheDoBHXH = @VoucherID) tbl_rank
		where tbl_rank.row_rank > 1)

END
GO
