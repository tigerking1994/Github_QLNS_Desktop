/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 21/07/2022 8:48:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_qt_chutuchitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_qt_chutuchitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 21/07/2022 8:48:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 21/07/2022 8:48:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 21/07/2022 8:48:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 21/07/2022 8:48:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_clone_year_donVi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_clone_year_donVi]
GO
/****** Object:  StoredProcedure [dbo].[sp_clone_year_donVi]    Script Date: 21/07/2022 8:48:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_clone_year_donVi]
	@source int,
	@dest int,
	@userCreate nvarchar(200)
as
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
/****** Object:  StoredProcedure [dbo].[sp_clone_year_mlns]    Script Date: 21/07/2022 8:48:30 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 21/07/2022 8:48:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
	@YearOfWork int,
	@YearOrBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@dvt int
as
begin

select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when @DataType = 2 then (a.fHienVat / @dvt) end as DuToanSoBaoCao,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when @DataType = 2 then (a.fHienVat)/@dvt end as DuToanSoXetDuyet,
	0 as QuyetToanSoBaoCao,
	0 as QuyetToanSoXetDuyet,
	0 as XetDuyetDuToanConDuChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iID_MaNguonNganSach = @BudgetSource

	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanSoBaoCao,
		0 as DuToanSoXetDuyet,
		case when @DataType = 1 then a.fTuChi_DeNghi / @dvt
		when @DataType = 2 then 0 end as QuyetToanSoBoCao,
		fTuChi_PheDuyet / @dvt as QuyetToanSoXetDuyet,
		(ISNULL(a.fChuyenNamSau_DaCap,0) + ISNULL(a.fChuyenNamSau_ChuaCap,0)) / @dvt as XetDuyetDuToanConDuChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOrBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iID_MaNguonNganSach = @BudgetSource
		
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 21/07/2022 8:48:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
	@YearOfWork int,
	@YearOfBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@dvt int
as
begin

--Dự toán năm trước chuyển sang
select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when @DataType = 2 then a.fHienVat / @dvt end as DuToanNamTruocChuyenSang,
	0 as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (1,4)
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
-- dự toán tổng số
	union all
	select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	0 as DuToanNamTruocChuyenSang,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt when @DataType = 2 then fHienVat / @dvt end as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach = 2
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iLoaiDuToan in (1,3,4,5)

-- bổ sung sau 30/09
	union all
	select 
	a.iID_MLNS,
	a.iID_MLNS_Cha as MLNS_Id_Parent,
	a.sLNS as LNS,
	a.sL as L,
	a.sK as K,
	a.sM as M,
	a.sTM as TM,
	a.sTTM as TTM,
	a.sNG as NG,
	a.sTNG1 as TNG1,
	a.sTNG2 as TNG2,
	a.sTNG3 as TNG3,
	a.sXauNoiMa as XauNoiMa,
	a.sMoTa as MoTa,
	cast (0 as bit) as IsHangCha,
	0 as DuToanNamTruocChuyenSang,
	0 as DuToanTongSo,
	case when @DataType = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt when @DataType = 2 then fHienVat / @dvt end as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach = 2
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		--and a.iLoaiDuToan = 5

-- số đề nghị quyết toán năm & số chuyển năm sau
	union all

	select
		a.iID_MLNS,
		a.iID_MLNS_Cha as MLNS_Id_Parent,
		a.sLNS as LNS,
		a.sL as L,
		a.sK as K,
		a.sM as M,
		a.sTM as TM,
		a.sTTM as TTM,
		a.sNG as NG,
		a.sTNG1 as TNG1,
		a.sTNG2 as TNG2,
		a.sTNG3 as TNG3,
		a.sXauNoiMa as XauNoiMa,
		a.sMoTa as MoTa,
		cast (0 as bit) as IsHangCha,
		0 as DuToanNamTruocChuyenSang,
		0 as DuToanTongSo,
		0 as DuToanBoSungSau,
		-- thiếu cột fHienVat trong bảng NS_QT_ChungTuChiTiet
		case when @DataType = 1 then a.fTuChi_PheDuyet when @DataType = 2 then 0 end as SoDeNghiQuyetToanNam,
		(ISNULL(fChuyenNamSau_ChuaCap,0) + ISNULL(fChuyenNamSau_DaCap,0)) / @dvt as DuToanChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi,
		a.iNamNganSach as NamNganSach
	from NS_QT_ChungTuChiTiet a
		inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOfBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
end
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_qt_chutuchitiet]    Script Date: 21/07/2022 8:48:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_qt_qt_chutuchitiet]
  @strIdDonVi NVARCHAR (2000),
  @strThang NVARCHAR (50),
  @strNam int,
  @strThangTruoc NVARCHAR (50),
  @strNamTruoc int
AS
BEGIN

if (SELECT count (*)  FROM f_split(@strThang)) = 1
begin 

with Thang as (
		select ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, MoTa, NamLamViec, 
		sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from  thang  order by  xaunoima;
end 

else
WITH ThoiGianTruoc as (
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThangTruoc)) = 1 then MoTa
			when (select count(*) from f_split(@strThangTruoc)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThangTruoc)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in (SELECT * FROM f_split(@strThangTruoc))
and ctct.NamLamViec = @strNamTruoc
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and XauNoiMa in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
),
ThoiGianNay  as(
select  ctct.MLNS_Id_Parent MlnsIdParent, XauNoiMa, 
		case 
			when (select count(*) from f_split(@strThang)) = 1 then MoTa
			when (select count(*) from f_split(@strThang)) = 3 then replace(MoTa, N'tháng', N'quý')
			when (select count(*) from f_split(@strThang)) = 12 then replace(MoTa, N'tháng', N'năm')
		end as MoTa,
		NamLamViec, sum(ctct.TongSo ) TongSo, sum(ctct.ThieuUy) ThieuUy, sum(ctct.TrungUy) TrungUy, sum(ctct.ThuongUy) ThuongUy, sum(ctct.DaiUy) DaiUy,sum(ctct.ThieuTa) ThieuTa, sum(ctct.TrungTa) TrungTa, sum(ctct.ThuongTa) ThuongTa, sum(ctct.DaiTa) DaiTa, sum(ctct.Tuong) Tuong,
		sum(ctct.BinhNhi) BinhNhi, sum(ctct.BinhNhat) BinhNhat, sum(ctct.HaSi) HaSi, sum(ctct.TrungSi) TrungSi, sum(ctct.ThuongSi ) ThuongSi,
		sum(ctct.ThieuUyCn) ThieuUyCn, sum(ctct.TrungUycn) TrungUycn, sum(ctct.ThuongUyCn) ThuongUyCn, sum(ctct.DaiUyCn) DaiUyCn,sum(ctct.ThieuTaCn) ThieuTaCn, sum(ctct.TrungTaCn) TrungTaCn, sum(ctct.ThuongTaCn) ThuongTaCn,
		sum(ctct.CNQP) CNQP, sum(ctct.LDHD) LDHD, sum(ctct.VCQP) VCQP, sum(ctct.QNCN) Qncn
from  tl_qs_chungtuchitiet ctct 
where 1=1  
and ctct.Thang in  (SELECT * FROM f_split(@strThang))
and ctct.NamLamViec = @strNam
and ctct.Id_DonVi in  (SELECT * FROM f_split(@strIdDonVi))
and  XauNoiMa not in ('100','500')
group by  xaunoima, mota , NamLamViec, MLNS_Id_Parent
)
select * from ThoiGianTruoc  
union all 
select * from ThoiGianNay 
order by xaunoima;
END
GO

INSERT [dbo].[TL_Map_Column_Config] ([id], [Is_Map_PhuCap], [Is_Map_Value], [Map_Expression], [New_Column], [Old_Column], [Use_PhuCap_Value]) VALUES (N'ca5afcb5-52f8-4cb3-a13c-a4065acbc686', 0, 1, NULL, N'BHuongThangTnn', N'COTN', 0)
GO