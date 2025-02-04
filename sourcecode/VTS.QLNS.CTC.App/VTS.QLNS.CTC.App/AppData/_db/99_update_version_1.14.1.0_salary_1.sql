/****** Object:  StoredProcedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach]    Script Date: 3/6/2024 1:58:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach] AS' 
END
GO
ALTER Procedure [dbo].[sp_tl_delete_can_bo_phu_cap_ke_hoach]
	@maCanBo varchar(max)
As
Begin
Delete From TL_CanBo_PhuCap_KeHoach
Where Ma_CanBo IN (SELECT * FROM f_split(@maCanBo))
End
;
;
GO
