/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 25/07/2022 2:31:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]    Script Date: 25/07/2022 2:31:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ns_rpt_quyettoannam_quyetoan_8568]
	@YearOfWork int,
	@YearOfBudget nvarchar(20),
	@ListLNS nvarchar(MAX),
	@ListUnitId nvarchar(200),
	@DataType int,
	@BudgetSource int,
	@dvt int,
	@PrintType nvarchar(MAX)
as
begin

--Dự toán 
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
	-- dự toán năm trước chuyển sang
	case when (@DataType = 1 and a.iNamNganSach in (1,4)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt
		when (@DataType = 2 and a.iNamNganSach in (1,4)) then a.fHienVat / @dvt else 0 end as DuToanNamTruocChuyenSang,
	-- dự toán tổng số
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan in (1,3,4,5)) then fHienVat / @dvt else 0 end as DuToanTongSo,
	-- dự toán bổ sung sau
	case when (@DataType = 1 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then (a.fTuChi + a.fHangNhap + a.fHangMua + a.fPhanCap) / @dvt 
		when (@DataType = 2 and a.iNamNganSach = 2 and ct.iLoaiDuToan = 5) then fHienVat / @dvt else 0 end as DuToanBoSungSau,

	0 as SoDeNghiQuyetToanNam,
	0 as DuToanChuyenNamSau,
	a.iID_MaDonVi as IdDonVi,
	b.sTenDonVi as TenDonVi,
	a.iNamNganSach as NamNganSach
	from NS_DT_ChungTuChiTiet a
	inner join DonVi b on b.iID_MaDonVi = a.iID_MaDonVi 
	inner join NS_DT_ChungTu ct on ct.iID_DTChungTu = a.iID_DTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))

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
		inner join NS_QT_ChungTu ct on a.iID_QTChungTu = ct.iID_QTChungTu
	where
		b.iNamLamViec = @YearOfWork and
		a.iNamLamViec = @YearOfWork
		and a.iNamNganSach in (select * from f_split(@YearOfBudget))
		and a.sLNS in (select * from f_split(@ListLNS))
		and a.iID_MaDonVi in (select * from f_split(@ListUnitId))
		and ct.bKhoa in (select * from f_split(@PrintType))
end
GO
