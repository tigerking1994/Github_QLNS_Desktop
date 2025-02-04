/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_muclucDanhMuc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_muclucDanhMuc]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlqs]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlnsmlskt]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlnsmlskt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlnsmlskt]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlcauhinh]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlcauhinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlcauhinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_donVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_donVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_BQuanLy]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_BQuanLy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_BQuanLy]
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucSkt]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_findParentMucLucSkt]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_findParentMucLucSkt]
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucNganSach]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_findParentMucLucNganSach]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_findParentMucLucNganSach]
GO
/****** Object:  UserDefinedFunction [dbo].[f_check_duplicate_donVi]    Script Date: 12/07/2022 9:38:11 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[f_check_duplicate_donVi]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[f_check_duplicate_donVi]
GO
/****** Object:  UserDefinedFunction [dbo].[f_check_duplicate_donVi]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[f_check_duplicate_donVi] 
	(@source int,
	@dest int)
returns int
as
begin
	declare @isSource int;
	declare @isDest int;
	declare @result int;
	select @isSource = count(iID_DonVi) from DonVi where iLoai = '0' and iNamLamViec = @source;
	select @isDest = count(iID_DonVi) from DonVi where iLoai = '0' and iNamLamViec = @dest;
	if(@isDest > 0 and @isSource > 0)
		begin
			set @result = 1;
		end
	else
		begin
			set @result = 0;
		end
	return @result;
end;
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucNganSach]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[f_findParentMucLucNganSach] 
	( @dest int, @xauNoiMa nvarchar(100))
returns uniqueidentifier
as
begin
	declare @result nvarchar(100);
	select top 1 @result = iID_MLNS from dbo.NS_MucLucNganSach ns where ns.iNamLamViec = @dest and (@xauNoiMa like CONCAT(ns.sXauNoiMa,'-%')) order by LEN(ns.sXauNoiMa) desc;
	return @result
end
GO
/****** Object:  UserDefinedFunction [dbo].[f_findParentMucLucSkt]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[f_findParentMucLucSkt] 
	( @dest int, @kyHieu nvarchar(100))
returns uniqueidentifier
as
begin
	declare @result nvarchar(100);
	select top 1 @result = iID_MLSKT from dbo.NS_SKT_MucLuc ns where ns.iNamLamViec = @dest and (@kyHieu like CONCAT(ns.sKyHieu,'-%')) order by LEN(ns.sKyHieu) desc;
	return @result
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_BQuanLy]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_BQuanLy]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into DM_BQuanLy
	([iID_BQuanLy]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaBQuanLy]
      ,[iNamLamViec]
      ,[iTrangThai]
      ,[sKyHieu]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTenBQuanLy]
	)
	select
		newid()
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaBQuanLy]
      ,@dest
      ,[iTrangThai]
      ,[sKyHieu]
      ,[sMoTa]
      ,[sNguoiSua]
      ,@userCreate
      ,[sTenBQuanLy]
	  from DM_BQuanLy b where b.iNamLamViec = @source and (b.iID_MaBQuanLy not in (select iID_MaBQuanLy from DM_BQuanLy c where c.iNamLamViec = @dest))

	update d
	set
		d.dNgaySua = getdate(),
		d.sMoTa = s.sMoTa,
		d.iTrangThai = s.iTrangThai,
		d.sKyHieu = s.sKyHieu,
		d.sTenBQuanLy = s.sTenBQuanLy
	from
	DM_BQuanLy as s
	inner join DM_BQuanLy as d
	on s.iID_MaBQuanLy = d.iID_MaBQuanLy
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_donVi]
	@source int,
	@dest int,
	@userCreate nvarchar(200),
	@isCopyDonViSuDung bit
as
begin
	if(@isCopyDonViSuDung = 0)
		begin
			insert into DonVi 
			 ([iID_DonVi]
			  ,[bCoNSNganh]
			  ,[dNgaySua]
			  ,[dNgayTao]
			  ,[iID_MaDonVi]
			  ,[iTrangThai]
			  ,[iID_Parent]
			  ,[IsPhongBan]
			  ,[iKhoi]
			  ,[sKyHieu]
			  ,[iLoai]
			  ,[LoaiNganSach]
			  ,[Log]
			  ,[sMoTa]
			  ,[iNamLamViec]
			  ,[sNguoiSua]
			  ,[sNguoiTao]
			  ,[Tag]
			  ,[sTenDonVi]
			  ,[iCapDonVi])
			select 
				   newid()
			  ,[bCoNSNganh]
			  ,[dNgaySua]
			  ,[dNgayTao]
			  ,[iID_MaDonVi]
			  ,[iTrangThai]
			  ,[iID_Parent]
			  ,[IsPhongBan]
			  ,[iKhoi]
			  ,[sKyHieu]
			  ,[iLoai]
			  ,[LoaiNganSach]
			  ,[Log]
			  ,[sMoTa]
			  ,@dest
			  ,[sNguoiSua]
			  ,@userCreate
			  ,[Tag]
			  ,[sTenDonVi]
			  ,[iCapDonVi] from DonVi c 
			  where c.iNamLamViec= @source and (c.iID_MaDonVi not in (select b.iID_MaDonVi from DonVi b where b.iNamLamViec = @dest)) and c.iLoai <> '0' 
		end
	else
		begin
			insert into DonVi 
			 ([iID_DonVi]
			  ,[bCoNSNganh]
			  ,[dNgaySua]
			  ,[dNgayTao]
			  ,[iID_MaDonVi]
			  ,[iTrangThai]
			  ,[iID_Parent]
			  ,[IsPhongBan]
			  ,[iKhoi]
			  ,[sKyHieu]
			  ,[iLoai]
			  ,[LoaiNganSach]
			  ,[Log]
			  ,[sMoTa]
			  ,[iNamLamViec]
			  ,[sNguoiSua]
			  ,[sNguoiTao]
			  ,[Tag]
			  ,[sTenDonVi]
			  ,[iCapDonVi])
			select 
				   newid()
			  ,[bCoNSNganh]
			  ,[dNgaySua]
			  ,[dNgayTao]
			  ,[iID_MaDonVi]
			  ,[iTrangThai]
			  ,[iID_Parent]
			  ,[IsPhongBan]
			  ,[iKhoi]
			  ,[sKyHieu]
			  ,[iLoai]
			  ,[LoaiNganSach]
			  ,[Log]
			  ,[sMoTa]
			  ,@dest
			  ,[sNguoiSua]
			  ,@userCreate
			  ,[Tag]
			  ,[sTenDonVi]
			  ,[iCapDonVi] from DonVi c 
			  where c.iNamLamViec= @source and (c.iID_MaDonVi not in (select b.iID_MaDonVi from DonVi b where b.iNamLamViec = @dest)) and c.iLoai = '0'
		end

	update d
	set
		d.bCoNSNganh = s.bCoNSNganh,
		d.dNgaySua = getdate(),
		d.sNguoiSua = @userCreate,
		d.iTrangThai = s.iTrangThai,
		d.iID_Parent = s.iID_Parent,
		d.IsPhongBan = s.IsPhongBan,
		d.iKhoi = s.iKhoi,
		d.sKyHieu = s.sKyHieu,
		d.iLoai = s.iLoai,
		d.LoaiNganSach = s.LoaiNganSach,
		d.[Log] = s.[Log],
		d.sMoTa = s.sMoTa,
		d.[Tag] = s.[Tag],
		d.sTenDonVi = s.sTenDonVi,
		d.iCapDonVi = s.iCapDonVi

	from
	DonVi as s
	inner join DonVi as d
	on s.iID_MaDonVi = d.iID_MaDonVi
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlcauhinh]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_mlcauhinh]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into DanhMuc
	([iID_DanhMuc]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaDanhMuc]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	)
	select
		[iID_DanhMuc]
      ,null
      ,GETDATE()
      ,[iID_MaDanhMuc]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	  from DanhMuc b where b.iNamLamViec = @source and b.sType='DM_CauHinh' and (b.iID_MaDanhMuc not in (select iID_MaDanhMuc from DanhMuc c where c.iNamLamViec = @dest))

	 --UPDATE
	update d
	set
		d.dNgaySua = getdate(),
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.[Log] = s.[Log],
		d.NganSachNganh = s.NganSachNganh,
		d.sGiaTri = s.sGiaTri,
		d.sMoTa = s.sMoTa,
		d.sNguoiSua = @userCreate,
		d.sType = 'DM_CauHinh',
		d.[Tag] = s.[Tag]

	from
	DanhMuc as s
	inner join DanhMuc as d
	on s.iID_MaDanhMuc = d.iID_MaDanhMuc
	where s.iNamLamViec = @source and d.iNamLamViec = @dest and d.sType = 'DM_CauHinh' and s.sType = 'DM_CauHinh'

end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_mlns]
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
		  ,[sXauNoiMa])
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
		  ,[sXauNoiMa] from NS_MucLucNganSach c 
	  where c.iNamLamViec= @source and c.sXauNoiMa not in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)
	-- Cap nhat lai gia tri
	update d
	set
		d.[sMoTa] = s.sMoTa,
		d.[sDuToanChiTietToi] = case when (d.sChiTietToi is null or d.sChiTietToi = '') then s.sDuToanChiTietToi else d.sDuToanChiTietToi end,
		d.[sQuyetToanChiTietToi] = case when (d.sQuyetToanChiTietToi is null or d.sQuyetToanChiTietToi = '') then s.sQuyetToanChiTietToi else d.sQuyetToanChiTietToi end,
		d.[sCPChiTietToi] = case when(d.sCPChiTietToi is null or d.sCPChiTietToi = '') then s.sCPChiTietToi else s.sCPChiTietToi end
	from
	NS_MucLucNganSach as s
	inner join NS_MucLucNganSach as d
	on s.iID_MLNS = d.iID_MLNS
	where s.iNamLamViec = @source and d.iNamLamViec = @dest
	-- cap nhat parent
	update NS_MucLucNganSach
	set
	iID_MLNS_Cha = dbo.f_findParentMucLucNganSach(@dest,sXauNoiMa)
	where iNamLamViec = @dest
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlnsmlskt]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_mlnsmlskt]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	delete NS_MLSKT_MLNS where iNamLamViec = @dest
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
		  newid()
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
		and c.sNS_XauNoiMa  in (select b.sXauNoiMa from NS_MucLucNganSach b where b.iNamLamViec = @dest)
		and c.sSKT_KyHieu  in (select b.sKyHieu from NS_SKT_MucLuc b where b.iNamLamViec = @dest)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlqs]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_mlqs]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
begin
	insert into NS_QS_MucLuc
	([iID_QSMucLuc]
      ,[bHangCha]
      ,[iID_MLNS]
      ,[iID_MLNS_Cha]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[sHienThi]
      ,[sKyHieu]
      ,[sM]
      ,[sMoTa]
      ,[sTM]
	)
	select
		newid()
      ,[bHangCha]
      ,[iID_MLNS]
      ,[iID_MLNS_Cha]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[sHienThi]
      ,[sKyHieu]
      ,[sM]
      ,[sMoTa]
      ,[sTM]
	  from NS_QS_MucLuc b where b.iNamLamViec = @source and (b.sKyHieu not in (select sKyHieu from NS_QS_MucLuc c where c.iNamLamViec = @dest))

	 --UPDATE
	update d
	set
		d.bHangCha = s.bHangCha,
		d.iID_MLNS = s.iID_MLNS,
		d.iID_MLNS_Cha = s.iID_MLNS_Cha,
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.sM = s.sM,
		d.sTM = s.sTM
	from
	NS_QS_MucLuc as s
	inner join NS_QS_MucLuc as d
	on s.sKyHieu = d.sKyHieu
	where s.iNamLamViec = @source and d.iNamLamViec = @dest

end
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlskt]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_mlskt]
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
      ,[sLoaiNhap]
      ,[sM]
      ,[sMoTa]
      ,[sNG_Cha]
      ,[sNG]
      ,[sSTT]
      ,[sSTTBC]
      ,[Tag]
	  from NS_SKT_MucLuc b where b.iNamLamViec = @source and (b.sKyHieu not in (select sKyHieu from NS_SKT_MucLuc c where c.iNamLamViec = @dest))

	update d
	set
		d.dNgaySua = getdate(),
		d.bHangCha = s.bHangCha,
		d.dNguoiSua = @userCreate,
		d.sM = s.sM,
		d.sMoTa = s.sMoTa,
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
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_muclucDanhMuc]    Script Date: 12/07/2022 9:38:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_clone_year_muclucDanhMuc]
	@source int,
	@dest int,
	@userCreate nvarchar(200),
	@type nvarchar(200),
	@isCopyGiaTri int
as
begin
	insert into DanhMuc
	([iID_DanhMuc]
      ,[dNgaySua]
      ,[dNgayTao]
      ,[iID_MaDanhMuc]
      ,[iNamLamViec]
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,[sGiaTri]
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	)
	select
		newid()
      ,null
      ,GETDATE()
      ,[iID_MaDanhMuc]
      ,@dest
      ,[iThuTu]
      ,[iTrangThai]
      ,[Log]
      ,[NganSachNganh]
      ,case when(@isCopyGiaTri = 1) then [sGiaTri] else null end
      ,[sMoTa]
      ,[sNguoiSua]
      ,[sNguoiTao]
      ,[sTen]
      ,[sType]
      ,[Tag]
	  from DanhMuc b where b.iNamLamViec = @source and b.sType= @type and (b.iID_MaDanhMuc not in (select iID_MaDanhMuc from DanhMuc c where c.iNamLamViec = @dest))

	 --UPDATE
	update d
	set
		d.dNgaySua = getdate(),
		d.iThuTu = s.iThuTu,
		d.iTrangThai = s.iTrangThai,
		d.[Log] = s.[Log],
		d.NganSachNganh = s.NganSachNganh,
		d.sGiaTri = s.sGiaTri,
		d.sMoTa = s.sMoTa,
		d.sNguoiSua = @userCreate,
		d.sType = @type,
		d.[Tag] = s.[Tag]

	from
	DanhMuc as s
	inner join DanhMuc as d
	on s.iID_MaDanhMuc = d.iID_MaDanhMuc
	where s.iNamLamViec = @source and d.iNamLamViec = @dest and d.sType = @type and s.sType = @type

end
GO
