/****** Object:  StoredProcedure [dbo].[sp_restore_mlskt]    Script Date: 11/6/2024 10:08:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_restore_mlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_restore_mlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_wrong_skt_chitiet]    Script Date: 11/6/2024 10:08:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_remove_wrong_skt_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_remove_wrong_skt_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_remove_wrong_skt_chitiet]    Script Date: 11/6/2024 10:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_remove_wrong_skt_chitiet]
	@Year int
AS
BEGIN

	SET NOCOUNT ON;

	delete from ns_skt_chungtuchitiet
	where skyhieu not in (select skyhieu from ns_skt_mucluc_2425 where inamlamviec = @Year)
	and inamlamviec = @Year
	
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_restore_mlskt]    Script Date: 11/6/2024 10:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[sp_restore_mlskt]
	@Year int
AS
BEGIN

	SET NOCOUNT ON;

	declare @returnvalue nvarchar(max);
	declare @countchitiet int;
	declare @countchitietmlskt int;

	set @countchitiet = (select count(sKyHieu) from ns_skt_chungtuchitiet where inamlamviec = @Year);
	set @countchitietmlskt = (select count(ctct.sKyHieu) from ns_skt_chungtuchitiet ctct
							join ns_skt_mucluc ml on ctct.sKyHieu = ml.sKyHieu and ctct.iNamLamViec = ml.iNamLamViec
							where ctct.iNamLamViec = @Year);

	if (@countchitiet <> @countchitietmlskt)
	begin
		select * into #temp from ns_skt_chungtuchitiet ctct
		where ctct.skyhieu not in (select skyhieu from ns_skt_mucluc_2425 where inamlamviec = @Year)
		and ctct.inamlamviec = @Year
		set @returnvalue = (Select
		STUFF(','+(
		select distinct ', ' + skyhieu FROM #temp im FOR xml path('')
		, type ).value('.', 'varchar(max)'),1,2,''))
	end
	--set @returnvalue = 'EXISTS_WRONG_DATA'
	else

		begin
			delete from ns_skt_mucluc where inamlamviec = @Year;
			delete from ns_mlskt_mlns where inamlamviec = @Year;
			insert into ns_mlskt_mlns
			select * from ns_mlskt_mlns_2425 where inamlamviec = @Year
			insert into ns_skt_mucluc
			select * from ns_skt_mucluc_2425 where inamlamviec = @Year
			set @returnvalue = 'DONE'
		end

	select @returnvalue
END
;
;
GO
