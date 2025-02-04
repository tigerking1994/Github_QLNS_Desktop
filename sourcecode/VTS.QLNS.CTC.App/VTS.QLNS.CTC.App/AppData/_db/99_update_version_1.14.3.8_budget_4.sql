update ns_skt_mucluc
set skyhieucu = skyhieu
where iNamLamViec > 2024

/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 25/04/2024 4:13:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 25/04/2024 4:13:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_mlns]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into NS_MucLucNganSach 
		  ([iID]
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,[dNgayTao]
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,[iID_MLNS_Cha]
		  ,[sMoTa]
		  ,[iNamLamViec]
		  ,[sNG]
		  ,[sCPChiTietToi]
		  ,[sDuToanChiTietToi]
		  ,[sNguoiSua]
		  ,[sNguoiTao]
		  ,[sNhapTheoTruong]
		  ,[sQuyetToanChiTietToi]
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa]
		  ,[sMaCB])
		select 
		   newid()
		  ,[bDuPhong]
		  ,[bHangCha]
		  ,[bHangChaDuToan]
		  ,[bHangChaQuyetToan]
		  ,[bHangMua]
		  ,[bHangNhap]
		  ,[bHienVat]
		  ,[bNgay]
		  ,[bPhanCap]
		  ,[bSoNguoi]
		  ,[bTonKho]
		  ,[bTuChi]
		  ,[sChiTietToi]
		  ,[dNgaySua]
		  ,getdate()
		  ,[iLoai]
		  ,[iLock]
		  ,[iTrangThai]
		  ,[iID_MaDonVi]
		  ,[iID_MaBQuanLy]
		  ,[sK]
		  ,[sL]
		  ,[sLNS]
		  ,[Log]
		  ,[sM]
		  ,[iID_MLNS]
		  ,null
		  ,[sMoTa]
		  ,@dest
		  ,[sNG]
		  ,null
		  ,null
		  ,[sNguoiSua]
		  ,@userCreate
		  ,[sNhapTheoTruong]
		  ,null
		  ,[Tag]
		  ,[sTM]
		  ,[sTNG]
		  ,[sTNG1]
		  ,[sTNG2]
		  ,[sTNG3]
		  ,[sTTM]
		  ,[sXauNoiMa]
		  ,[sMaCB] from NS_MucLucNganSach c 
	  where c.iNamLamViec= @source and c.sXauNoiMa not in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)

	-- Cập nhật lại một số trường giá trị
	update d
	set
		d.[sMoTa] = s.sMoTa,
		d.[sMaCB] = s.sMaCB,
		d.[sDuToanChiTietToi] = case when (d.sDuToanChiTietToi is null or d.sDuToanChiTietToi = '') then s.sDuToanChiTietToi else d.sDuToanChiTietToi end,
		d.[sQuyetToanChiTietToi] = case when (d.sQuyetToanChiTietToi is null or d.sQuyetToanChiTietToi = '') then s.sQuyetToanChiTietToi else d.sQuyetToanChiTietToi end,
		d.[sCPChiTietToi] = case when(d.sCPChiTietToi is null or d.sCPChiTietToi = '') then s.sCPChiTietToi else s.sCPChiTietToi end
	from
	NS_MucLucNganSach as s
	inner join NS_MucLucNganSach as d
	on s.iID_MLNS = d.iID_MLNS
	where s.iNamLamViec = @source and d.iNamLamViec = @dest


	/* 
	Cập nhật lại ID cha theo cách cũ
	update NS_MucLucNganSach
	set
	iID_MLNS_Cha = dbo.f_findParentMucLucNganSach(@dest,sXauNoiMa)
	where iNamLamViec = @dest
	and (sL is not null and sL <> '') or (sK is not null and sK <> '') 
	*/

	-- Cập nhật lại ID cha tối ưu
	select top 200000 iID_MLNS, sXauNoiMa into #temp
	from NS_MucLucNganSach 
	where iNamLamViec = @dest
	order by sXauNoiMa desc;
	--create nonclustered index idx on #temp (sXauNoiMa);
	update mlns_table
	set
	iID_MLNS_Cha = 
		(select top 1 iID_MLNS 
		from #temp mlns_dictionary
		where (mlns_table.sXauNoiMa like mlns_dictionary.sXauNoiMa + '-%'))
	from NS_MucLucNganSach mlns_table
	where iNamLamViec = @dest
	and (coalesce(sL, '') <> '' or coalesce(sK, '') <> '')
	drop table #temp

	-- Cập nhật lại bHangCha mục lục ngân sách

	update NS_MucLucNganSach
	set NS_MucLucNganSach.bHangCha = 0
	where iNamLamViec = @dest
	and (coalesce(sL, '') <> '' or coalesce(sK, '') <> '')

	update mlns_table
	set mlns_table.bHangCha = 1
	from NS_MucLucNganSach mlns_table
	inner join NS_MucLucNganSach mlns_table_clone
	on mlns_table.iID_MLNS = mlns_table_clone.iID_MLNS_Cha
	and mlns_table.iNamLamViec = @dest
	and mlns_table_clone.iNamLamViec = @dest

end
;
;
;
GO
