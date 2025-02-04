/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi]    Script Date: 7/22/2024 4:34:22 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi]    Script Date: 7/22/2024 4:34:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach_theo_nam_donvi]
	@Nam nvarchar(50),
	@DonVi nvarchar(500)
AS
BEGIN
	
	DELETE FROM Tl_CanBo_PhuCap_KeHoach 
	WHERE Ma_CanBo like + @Nam + '%'
		AND Ma_CanBo IN (SELECT Ma_CanBo FROM TL_DM_CanBo_KeHoach WHERE Parent = @DonVi AND Nam = @Nam);

	DELETE FROM TL_DM_CanBo_KeHoach WHERE Nam = @Nam AND Parent = @DonVi;
		
END
GO
