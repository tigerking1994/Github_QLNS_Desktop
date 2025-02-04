/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 14/07/2022 3:52:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 14/07/2022 3:52:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]    Script Date: 14/07/2022 3:52:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 --@@DataType 1 - tu chi 2 - hien vat
CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8063]
	@YearOfWork int,
	@YearOrBudget nvarchar(20),
	@ListLNS nvarchar(200),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@Type nvarchar(10)
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
	case when @DataType = 1 then a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap
		when @DataType = 2 then a.fHienVat end as DuToanSoBaoCao,
	0 as DuToanSoXetDuyet,
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
		a.fTuChi_DeNghi as QuyetToanSoBoCao,
		a.fTuChi_PheDuyet as QuyetToanSoXetDuyet,
		(ISNULL(a.fChuyenNamSau_DaCap,0) + ISNULL(a.fChuyenNamSau_ChuaCap,0)) as XetDuyetDuToanConDuChuyenNamSau,
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
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 14/07/2022 3:52:48 PM ******/
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
	@BudgetSource int
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
	case when @DataType = 1 then a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap
		when @DataType = 2 then a.fHienVat end as DuToanNamTruocChuyenSang,
	0 as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
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
	case when @BudgetSource = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) when @BudgetSource = 2 then fHienVat end as DuToanTongSo,
	0 as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
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
	case when @BudgetSource = 1 then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) when @BudgetSource = 2 then fHienVat end as DuToanBoSungSau,
	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi
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
		case when @BudgetSource = 1 then a.fTuChi_PheDuyet when @BudgetSource = 2 then 0 end as SoDeNghiQuyetToanNam,
		(ISNULL(fChuyenNamSau_ChuaCap,0) + ISNULL(fChuyenNamSau_DaCap,0)) as DuToanChuyenNamSau,
		a.iID_MaDonVi as IdDonVi,
		b.sTenDonVi as TenDonVi
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
