/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 07/11/2023 2:18:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlnsmlskt]    Script Date: 07/11/2023 2:18:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlnsmlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlnsmlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlnsmlskt]    Script Date: 07/11/2023 2:18:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlnsmlskt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	--delete NS_MLSKT_MLNS where iNamLamViec = @dest
	insert into NS_MLSKT_MLNS 
		  ([iID_MLSKT_MLNS]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iNamLamViec]
      ,[iTrangThai]
      ,[Log]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sNS_XauNoiMa]
      ,[sSKT_KyHieu]
      ,[Tag])
select 
	   NEWID()
      ,[dNgaySua]
      ,[dNgayTao]
      ,@dest
      ,[iTrangThai]
      ,[Log]
      ,[sNguoiSua]
      ,@userCreate
      ,[sNS_XauNoiMa]
      ,[sSKT_KyHieu]
      ,[Tag] from NS_MLSKT_MLNS c 
	  where c.iNamLamViec= @source 
		and not exists (select 1 from NS_MLSKT_MLNS where 
			c.sNS_XauNoiMa = sNS_XauNoiMa 
			and c.sSKT_KyHieu = sSKT_KyHieu
			and inamlamviec = @dest
			)
		--and c.sNS_XauNoiMa in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)
		--and c.sSKT_KyHieu  in (select b.sKyHieu from NS_SKT_MucLuc b where b.iNamLamViec = @dest)

	if (@dest = 2024) 
	begin
	-- Cập nhật lại bảng map
	update map
	set map.sSKT_KyHieu = skt.sKyHieu
	from ns_mlskt_mlns map 
	join ns_skt_mucluc skt 
	on map.sSKT_KyHieu = skt.sKyHieuCu
	and map.inamlamviec = skt.inamlamviec
	and map.sSKT_KyHieu <> skt.sKyHieu
	where map.inamlamviec = 2024;

	WITH CTE AS(
	   SELECT [sSKT_KyHieu], [sNS_XauNoiMa],
		   RN = ROW_NUMBER()OVER(PARTITION BY sSKT_KyHieu, sNS_XauNoiMa ORDER BY sSKT_KyHieu, sNS_XauNoiMa)
	   FROM NS_MLSKT_MLNS
	   WHERE iNamLamViec = 2024
	)

	DELETE FROM CTE WHERE RN > 1

	end



end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 07/11/2023 2:18:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlskt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into NS_SKT_MucLuc
	([iID]
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,[dNguoiTao]
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
      ,[iNamLamViec]
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sKyHieuCu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
	)
	select
		newid()
      ,[bHangCha]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[dNguoiSua]
      ,@userCreate
      ,[iID_MLSKT]
      ,[iID_MLSKTCha]
      ,@dest
      ,[iTrangThai]
      ,[KyHieuCha]
      ,[Log]
      ,[Muc]
      ,[sKyHieu]
      ,[sKyHieuCu]
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
	  from NS_SKT_MucLuc b where b.iNamLamViec = @source and 
		not exists (select 1 from NS_SKT_MucLuc c where c.iNamLamViec = @dest and (c.sKyHieu = b.sKyHieu or c.sKyHieuCu = b.sKyHieu))

	update d
	set
		d.dNgaySua = getdate(),
		d.bHangCha = s.bHangCha,
		d.dNguoiSua = @userCreate,
		d.sM = s.sM,
		d.sMoTa = case when (d.sMoTa is null or d.sMoTa = '') then s.sMoTa else d.sMoTa end,
		d.sNG_Cha = s.sNG_Cha,
		d.sNG = s.sNG,
		d.sSTT = s.sSTT,
		d.sSTTBC = s.sSTTBC,
		d.[Tag] = s.[Tag],
		d.iID_MLSKT = s.iID_MLSKT
	from
	NS_SKT_MucLuc as s
	inner join NS_SKT_MucLuc as d
	on s.sKyHieu = d.sKyHieu
	where s.iNamLamViec = @source and d.iNamLamViec = @dest

	-- cap nhat parent
	update NS_SKT_MucLuc
	set
		iID_MLSKTCha = dbo.f_findParentMucLucSkt(@dest,sKyHieu)
	where iNamLamViec = @dest
end
;
;
GO
