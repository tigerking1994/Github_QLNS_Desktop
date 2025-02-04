/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 12/13/2023 2:42:51 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_luong_get_che_do]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_luong_get_che_do]    Script Date: 12/13/2023 2:42:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_luong_get_che_do]
	@MaCanBo nvarchar(100)
AS
BEGIN
	select
		chedo.bDisplay IsDisplay,
		chedo.sMaCheDo,
		chedo.sMaCheDoCha,
		chedo.sTenCheDo,
		chedo.sXauNoiMa,
		case when (chedo.sMaCheDoCha = '' 
					or chedo.sMaCheDoCha is null 
					or chedo.sMaCheDo in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)) then 1 
				else 0 
		end as IsHangCha,
		canbo.*
	from 
	TL_CanBo_CheDoBHXH canbo
	right join TL_DM_CheDoBHXH chedo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	where chedo.sMaCheDo <> 'SONGAYHUONG'
	--and chedo.bDisplay = 1
	order by chedo.iLoaiCheDo, chedo.sXauNoiMa

	--select
	--	chedo.sMaCheDo,
	--	chedo.sMaCheDoCha,
	--	chedo.sTenCheDo,
	--	CONCAT(chedocha.sMaCheDo, '-', chedocha.sTenCheDo) AS ParentName,
	--	canbo.*
	--from 
	--TL_DM_CheDoBHXH chedo 
	--left join TL_CanBo_CheDoBHXH canbo on canbo.sMaCheDo = chedo.sMaCheDo and canbo.sMaCanBo = @MaCanBo
	--left join TL_DM_CheDoBHXH chedocha on chedo.sMaCheDoCha = chedocha.sMaCheDo 
	--and chedo.sMaCheDoCha IN (select sMaCheDo from TL_DM_CheDoBHXH where sMaCheDoCha = '' or sMaCheDoCha is null)
	--where chedo.sMaCheDo <> 'SONGAYHUONG'
	--Order By ParentName
END
GO
