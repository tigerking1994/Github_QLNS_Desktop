/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 10/30/2024 3:14:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]    Script Date: 10/31/2024 4:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_phanbo_dutoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_chungtu_chitiet]    Script Date: 10/31/2024 4:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tn_dt_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tn_dt_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]    Script Date: 10/31/2024 4:58:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]    Script Date: 10/30/2024 3:14:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tn_dtdn_chungtu_chitiet_by_don_vi]
	@agencies nvarchar(max) ,
	@LNS nvarchar(max) ,
	@YearOfWork int ,
	@YearOfBudget int  ,
	@BudgetSource int,
	@VoucherType int 
AS
BEGIN
	IF(@VoucherType = 1)
	BEGIN
		SELECT
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		CAST(isnull(ctct.iNamNganSach, @YearOfBudget) as int) as INamNganSach,
		CAST(ctct.iID_MaNguonNganSach AS int)as IIdMaNguonNganSach,
		CAST(ctct.iNamLamViec AS int) INamLamViec,
		mlns.bHangCha,
		mlns.sChiTietToi as ChiTietToi,
		FDuToanNamKeHoach,
		FDuToanNamNay,
		FThucThuNamTruoc,
		FUocThucHienNamNay

	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
		SUM(isnull(fDuToan_NamKeHoach, 0)) as FDuToanNamKeHoach,
		SUM(isnull(fDuToan_NamNay, 0)) as FDuToanNamNay,
		SUM(isnull(fThucThu_NamTruoc, 0)) as FThucThuNamTruoc,
		SUM(isnull(fUocThucHien_NamNay, 0)) as FUocThucHienNamNay,
		iNamLamViec,iNamNganSach, iID_MaNguonNganSach,sXauNoiMa 
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_MaDonVi in (select * from dbo.splitstring(@agencies))
		GROUP BY iNamLamViec, iNamNganSach, iID_MaNguonNganSach, sXauNoiMa
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	ELSE
	BEGIN
		SELECT
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as MoTa,
		CAST(isnull(ctct.iNamNganSach, @YearOfBudget) as int) as INamNganSach,
		CAST(ctct.iID_MaNguonNganSach AS int)as IIdMaNguonNganSach,
		CAST(ctct.iNamLamViec AS int) INamLamViec,
		mlns.bHangCha,
		ctct.iID_MaDonVi IIdMaDonVi,
		ctct.sTenDonVi as STenDonVi,
		mlns.sChiTietToi as ChiTietToi,
		FDuToanNamKeHoach,
		FDuToanNamNay,
		FThucThuNamTruoc,
		FUocThucHienNamNay

	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
		SUM(isnull(fDuToan_NamKeHoach, 0)) as FDuToanNamKeHoach,
		SUM(isnull(fDuToan_NamNay, 0)) as FDuToanNamNay,
		SUM(isnull(fThucThu_NamTruoc, 0)) as FThucThuNamTruoc,
		SUM(isnull(fUocThucHien_NamNay, 0)) as FUocThucHienNamNay,
		iNamLamViec,iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach,sXauNoiMa ,sTenDonVi
		FROM
			TN_DTDN_ChungTuChiTiet
		WHERE
			iNamLamViec = @YearOfWork
			AND iNamNganSach = @YearOfBudget
			AND iID_MaNguonNganSach = @BudgetSource
			AND iID_MaDonVi in (select * from dbo.splitstring(@agencies))
		GROUP BY iNamLamViec, iNamNganSach, iID_MaDonVi, iID_MaNguonNganSach, sXauNoiMa,sTenDonVi
	) ctct ON mlns.sXauNoiMa = ctct.sXauNoiMa
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
	END
	
END
;
;
;

/****** Object:  StoredProcedure [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]    Script Date: 10/31/2024 4:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_can_cu_phan_bo_so_kiem_tra]
	@LoaiChungTu int = 1,
	@IdDonVi varchar(max) = '51',
	@NamLamViec int = 2024,
	@NamNganSach int = 2,
	@MaNguoiNganSach int = 1,
	@IsParent bit =1
AS
BEGIN
	-- Chứng từ cha
	IF (@IsParent = 1)
	BEGIN
		SELECT mlns.SXauNoiMa,
			   mlns.iID_MLNS IdMlns,
			   mlns.iID_MLNS_Cha IdMlnsCha,
			   mlns.bHangCha,
			   sum(isnull(TuChi,0)) TuChi,
			   sum(isnull(HangNhap,0)) HangNhap,
			   sum(isnull(HangMua,0)) HangMua,
			   sum(isnull(PhanCap,0)) PhanCap,
			   sum(isnull(TuChi,0)) MuaHangHienVat,
			   sum(isnull(TuChi,0)) DacThu,
			   sum(isnull(DuPhong,0)) DuPhong,
			   
			   IIdMaDonVi
		FROM NS_MucLucNganSach mlns
		 JOIN
		  (SELECT ctct.SXauNoiMa,
				  ctct.iID_MLNS,
				  ctct.iID_MLNS_Cha,
				  ctct.bHangCha,
				  sum(fTuChi) TuChi,
				  sum(fHangNhap) HangNhap,
				  sum(fHangMua) HangMua,
				  sum(fPhanCap) PhanCap,
				  sum(fTuChi) MuaHangHienVat,
				  sum(fTuChi) DacThu,
				  sum(ctct.fDuPhong) DuPhong,
				  ctct.iID_MaDonVi as IIdMaDonVi
		   FROM NS_DT_ChungTuChiTiet ctct
		   JOIN NS_DT_ChungTu ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
		   WHERE ct.iNamLamViec = @NamLamViec
			 AND ct.iNamNganSach = @NamNganSach
			 AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
			 AND (@LoaiChungTu = 1
				  OR ctct.sLNS in ('1040100',
								   '1040200',
								   '1040300'))
			 AND ct.bKhoa = 1
			-- AND ct.iLoai = 1
			 AND ct.iLoaiDuToan = 1
		   GROUP BY ctct.SXauNoiMa,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,ctct.iID_MaDonVi,
					ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
		GROUP BY mlns.SXauNoiMa,
				 mlns.iID_MLNS,
				 mlns.iID_MLNS_Cha,
				 mlns.bHangCha,
				 ctct.IIdMaDonVi;
	END
	ELSE
		BEGIN
			SELECT mlns.SXauNoiMa,
			   mlns.iID_MLNS IdMlns,
			   mlns.iID_MLNS_Cha IdMlnsCha,
			   mlns.bHangCha,
			   sum(isnull(TuChi,0)) TuChi,
			   sum(isnull(HangNhap,0)) HangNhap,
			   sum(isnull(HangMua,0)) HangMua,
			   sum(isnull(PhanCap,0)) PhanCap,
			   sum(isnull(TuChi,0)) MuaHangHienVat,
			   sum(isnull(TuChi,0)) DacThu,
			   sum(isnull(DuPhong,0)) DuPhong,
			   IIdMaDonVi
			FROM NS_MucLucNganSach mlns
			 JOIN
			  (SELECT ctct.SXauNoiMa,
					  ctct.iID_MLNS,
					  ctct.iID_MLNS_Cha,
					  ctct.bHangCha,
					  sum(fTuChi) TuChi,
					  sum(fHangNhap) HangNhap,
					  sum(fHangMua) HangMua,
					  sum(fPhanCap) PhanCap,
					  sum(fTuChi) MuaHangHienVat,
					  sum(fTuChi) DacThu,
					  sum(ctct.fDuPhong) DuPhong,
					  ctct.iID_MaDonVi as IIdMaDonVi
			   FROM NS_DT_ChungTuChiTiet ctct
			   JOIN NS_DT_ChungTu ct ON ct.iID_DTChungTu = ctct.iID_DTChungTu
			   WHERE ct.iNamLamViec = @NamLamViec
				 AND ct.iNamNganSach = @NamNganSach
				 AND ct.iID_MaNguonNganSach = @MaNguoiNganSach
				 AND (@LoaiChungTu = 1
					  OR ctct.sLNS in ('1040100',
									   '1040200',
									   '1040300'))
				 AND ct.bKhoa = 1
				-- AND ct.iLoai = 1
				 AND ct.iLoaiDuToan = 1
				 AND ctct.iID_MaDonVi = @IdDonVi
			   GROUP BY ctct.SXauNoiMa,
						ctct.iID_MLNS,
						ctct.iID_MLNS_Cha, ctct.iID_MaDonVi,
						ctct.bHangCha) ctct ON mlns.iID_MLNS = ctct.iID_MLNS AND mlns.iNamLamViec = @NamLamViec
			GROUP BY mlns.SXauNoiMa,
					 mlns.iID_MLNS,
					 mlns.iID_MLNS_Cha,
					 mlns.bHangCha,
					 IIdMaDonVi;
		END
END
;
;
;
--select * from NS_DT_ChungTuChiTiet where iNamLamViec = 2024 and iID_MaDonVi = 51
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_chungtu_chitiet]    Script Date: 10/31/2024 4:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_chungtu_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int
AS
BEGIN
	SELECT
		isnull(ctct.Id, NEWID()) AS Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		isnull(ctct.NamNganSach, @YearOfBudget) as NamNganSach,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		mlns.bHangCha BHangCha,
		isnull(ctct.iTrangThai, 0) as ITrangThai,
		isnull(ctct.iPhanCap, @Type) as IPhanCap,
		ctct.Id_DonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan,
		ctct.Id_PhongBanDich,
		isnull(ctct.GhiChu, '') as GhiChu,
		isnull(ctct.TuChi, 0) as TuChi,
		ctct.DateCreated,
		ctct.DateModified,
		isnull(ctct.UserCreator, '') as UserCreator ,
		isnull(ctct.UserModifier, '') as UserModifier,
		isnull(ctct.Log, '') as Log,
		isnull(ctct.Tag, '') as Tag,
		ctct.Id_DotNhan,
		isnull(ctct.B, '') as B,
		mlns.sChiTietToi as SChiTietToi
	FROM (SELECT * FROM f_mlns_by_lns(@YearOfWork, @LNS)) mlns
	--FROM NS_MucLucNganSach mlns
	LEFT JOIN (
		SELECT
			*
		FROM
			TN_DT_ChungTuChiTiet
		WHERE
			NamLamViec = @YearOfWork
			AND NamNganSach = @YearOfBudget
			AND NguonNganSach = @BudgetSource
			AND iPhanCap = @Type
			AND Id_ChungTu = @ChungTuId
			--AND LNS in (select * from dbo.splitstring(@LNS))
	) ctct ON mlns.iID_MLNS = ctct.MLNS_Id
	WHERE
		mlns.iNamLamViec = @YearOfWork
		AND mlns.iTrangThai = 1
		--AND mlns.LNS in (SELECT * FROM dbo.splitstring(@LNS))
	ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]    Script Date: 10/31/2024 4:58:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tn_dt_phanbo_dutoan_chitiet]
	@ChungTuId nvarchar(255),
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int
AS
BEGIN
	declare @VoucherDate datetime,
	@Agencies nvarchar(100)
	SELECT
		@VoucherDate = NgayChungTu,
		@Agencies = Id_DonVi
	FROM TN_DT_ChungTu
	WHERE id = @ChungTuId;

	select 
	tbl_sum.Id,
	tbl_sum.IdChungTu,
	tbl_sum.LNS,
	tbl_sum.XauNoiMa,
	tbl_sum.MLNS_Id,
	tbl_sum.MLNS_Id_Parent,
	tbl_sum.NguonNganSach,
	tbl_sum.NamLamViec,
	tbl_sum.NamNganSach,
	tbl_sum.iTrangThai,
	tbl_sum.iPhanCap,
	tbl_sum.IdDonVi,
	tbl_sum.TenDonVi,
	tbl_sum.IdPhongBan,
	tbl_sum.IdPhongBanDich,
	tbl_sum.GhiChu,
	tbl_sum.DateCreated,
	tbl_sum.DateModified,
	tbl_sum.UserCreator,
	tbl_sum.UserModifier,
	tbl_sum.Log,
	tbl_sum.Tag,
	tbl_sum.IdDotNhan,
	tbl_sum.B,
	SUM(tbl_sum.TuChi) as TuChi,
	tbl_sum.Loai
	
	into #tmpSum
from
	(
	select 
		null as Id,
		null as IdChungTu,
		mlns.sLNS as LNS,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		@BudgetSource as NguonNganSach,
		@YearOfWork as NamLamViec,
		@YearOfBudget as NamNganSach,
		1 as iTrangThai,
		0 as iPhanCap,
		'' as IdDonVi,
		'' as TenDonVi,
		'' as IdPhongBan,
		'' as IdPhongBanDich,
		'' as GhiChu,
		null as DateCreated,
		null as DateModified,
		'' as UserCreator,
		'' as UserModifier,
		'' as Log,
		'' as Tag,
		null as IdDotNhan,
		'' as B,
		ctct.TuChi as TuChi,
		case
			when mlns.bHangCha = 0 then cast(1 as bit) else 0
		end Loai
	from NS_MucLucNganSach mlns
	left join
		(
			select
				*
			from 
				TN_DT_ChungTuChiTiet ctct1
			where 
				ctct1.Id_ChungTu in (
					select
						Id
					from 
						TN_DT_ChungTu
					where
						iLoai = 0
						and NamLamViec = @YearOfWork
						and iTrangThai = 1
						and NamNganSach = @YearOfBudget
						and NguonNganSach = @BudgetSource
						--and FORMAT(cast(NgayChungTu as Date), 'yyyy-MM-dd') <= FORMAT(cast(@VoucherDate as Date), 'yyyy-MM-dd')
						and NgayChungTu <= @VoucherDate
				)
		) ctct
	on mlns.iID_MLNS = ctct.MLNS_Id
	where mlns.iNamLamViec = @YearOfWork and mlns.iTrangThai = 1 and mlns.sLNS in (select distinct * from f_split_lns(@LNS))) as tbl_sum
	where tbl_sum.Loai = 1
	group by 
		tbl_sum.Id,tbl_sum.IdChungTu,tbl_sum.LNS,
		tbl_sum.XauNoiMa,tbl_sum.MLNS_Id,tbl_sum.MLNS_Id_Parent,tbl_sum.NguonNganSach,
		tbl_sum.NamLamViec,tbl_sum.NamNganSach,tbl_sum.iTrangThai,tbl_sum.iPhanCap,
		tbl_sum.IdDonVi,tbl_sum.TenDonVi,tbl_sum.IdPhongBan,tbl_sum.IdPhongBanDich,
		tbl_sum.GhiChu,tbl_sum.DateCreated,tbl_sum.DateModified,
		tbl_sum.UserCreator,tbl_sum.UserModifier,tbl_sum.Log,
		tbl_sum.Tag,tbl_sum.IdDotNhan,tbl_sum.B, tbl_sum.Loai
		
	select 
	null as Id,
	null as IdChungTu,
	tbl_subSum.LNS,
	tbl_subSum.XauNoiMa,
	tbl_subSum.MLNS_Id,
	tbl_subSum.MLNS_Id_Parent,
	tbl_subSum.NguonNganSach,
	tbl_subSum.NamLamViec,
	tbl_subSum.NamNganSach,
	0 as iTrangThai,
	0 as iPhanCap,
	'' as IdDonVi,
	'' as TenDonVi,
	'' as IdPhongBan,
	'' as IdPhongBanDich,
	'' as GhiChu,
	null as DateCreated,
	null as DateModified,
	'' as UserCreator,
	'' as UserModifier,
	'' as Log,
	'' as Tag,
	null as IdDotNhan,
	'' as B,
	SUM(tbl_subSum.TuChi) as TuChi,
	1 as Loai

	into #tmpSubSum 
from
(select
	isnull(ctct.Id, NEWID()) as Id,
	ctct.Id_ChungTu as IdChungTu,
	ctct.LNS,
	ctct.XauNoiMa,
	ctct.MLNS_Id,
	ctct.MLNS_Id_Parent,
	ctct.NguonNganSach,
	ctct.NamLamViec,
	ctct.NamNganSach,
	isnull(ctct.iTrangThai, 0) as iTrangThai,
	isnull(ctct.iPhanCap, 0) as iPhanCap,
	ctct.Id_DonVi as IdDonVi,
	ctct.TenDonVi,
	ctct.Id_PhongBan as IdPhongBan,
	ctct.Id_PhongBanDich as IdPhongBanDich,
	ctct.GhiChu as GhiChu,
	ctct.DateCreated,
	ctct.DateModified,
	isnull(ctct.UserCreator, '') as UserCreator ,
	isnull(ctct.UserModifier, '') as UserModifier,
	isnull(ctct.Log, '') as Log,
	isnull(ctct.Tag, '') as Tag,
	null as IdDotNhan,
	isnull(ctct.B, '') as B,
	ctct.TuChi,
	1 as Loai
from
	TN_DT_ChungTuChiTiet ctct
inner join
	TN_DT_ChungTu ct
on ctct.Id_ChungTu = ct.Id
where
	ct.iLoai = 1
	and ct.NamLamViec = @YearOfWork
	and ct.NamNganSach = @YearOfBudget
	and ct.NguonNganSach = @BudgetSource
	and ct.NgayChungTu < @VoucherDate
	) as tbl_subSum
	
group by 
	tbl_subSum.LNS,
	tbl_subSum.XauNoiMa,
	tbl_subSum.MLNS_Id,
	tbl_subSum.MLNS_Id_Parent,
	tbl_subSum.NguonNganSach,
	tbl_subSum.NamLamViec,
	tbl_subSum.NamNganSach
	
select tbl_total.* into #tmpTotal from
(select
	total.Id,
	total.IdChungTu,
	total.LNS,
	total.XauNoiMa,
	total.MLNS_Id,
	total.MLNS_Id_Parent,
	total.NguonNganSach,
	total.NamLamViec,
	total.NamNganSach,
	total.iTrangThai,
	total.iPhanCap,
	total.IdDonVi,
	total.TenDonVi,
	total.IdPhongBan,
	total.IdPhongBanDich,
	total.GhiChu,
	total.DateCreated,
	total.DateModified,
	total.UserCreator,
	total.UserModifier,
	total.Log,
	total.Tag,
	total.IdDotNhan,
	total.B,
	(isnull(total.TuChi, 0) - isnull(tmp_sub_sum.TuChi, 0)) as TuChi,
	total.Loai
from
	#tmpSum total
left join
	#tmpSubSum tmp_sub_sum
on total.XauNoiMa = tmp_sub_sum.XauNoiMa) as tbl_total
		
		
select tbl_data.* into #tmpData from
(select 
		tmp.LNS,
		tmp.MLNS_Id,
		tmp.MLNS_Id_Parent,
		tmp.TuChi,
		tmp.Loai,
		NEWID() as Id,
		NEWID() as IdChungTu,
		tmp.XauNoiMa,
		tmp.NguonNganSach,
		tmp.NamLamViec,
		tmp.NamNganSach,
		tmp.iTrangThai,
		tmp.iPhanCap,
		tmp.IdDonVi,
		tmp.TenDonVi,
		tmp.IdPhongBan,
		tmp.IdPhongBanDich,
		tmp.GhiChu,
		tmp.DateCreated,
		tmp.DateModified,
		tmp.UserCreator,
		tmp.UserModifier,
		tmp.Log,
		tmp.Tag,
		NEWID() as IdDotNhan,
		tmp.B
	from 
		#tmpTotal tmp

	union all

	select tbl_sub_data.* from
	(select 
		mlns.sLNS as LNS,
		mlns.iID_MLNS as MLNS_Id,
		mlns.iID_MLNS_Cha as MLNS_Id_Parent,
		isnull(ctct.TuChi, 0) as TuChi,
		case 
			when mlns.bHangCha = 1 then 0 else 2
		end Loai,
		isnull(ctct.Id, NEWID()) as Id,
		ctct.Id_ChungTu as IdChungTu,
		mlns.sXauNoiMa as XauNoiMa,
		ctct.NguonNganSach,
		ctct.NamLamViec,
		ctct.NamNganSach,
		ctct.iTrangThai,
		ctct.iPhanCap,
		ctct.Id_DonVi as IdDonVi,
		ctct.TenDonVi,
		ctct.Id_PhongBan as IdPhongBan,
		ctct.Id_PhongBanDich as IdPhongBanDich,
		ctct.GhiChu as GhiChu,
		ctct.DateCreated as DateCreated,
		ctct.DateModified as DateModified,
		ctct.UserCreator as UserCreator,
		ctct.UserModifier as UserModifier,
		ctct.Log,
		ctct.Tag,
		NEWID() as IdDotNhan,
		ctct.B
	from NS_MucLucNganSach mlns
	left join
		(
			select
				*
			from 
				TN_DT_ChungTuChiTiet ctct1
			where 
				ctct1.iPhanCap = 1
				and ctct1.Id_ChungTu in (select * from dbo.splitstring(@ChungTuId))
		) ctct
	on mlns.iID_MLNS = ctct.MLNS_Id
	where mlns.iNamLamViec = @YearOfWork and mlns.iTrangThai = 1 and mlns.sLNS in (select distinct * from f_split_lns(@LNS))) as tbl_sub_data
	where tbl_sub_data.Loai = 2

	) as tbl_data
	order by tbl_data.LNS, tbl_data.Loai


	--select 
	--	*
	--into #mlns from NS_MucLucNganSach where 
	--NamLamViec = @YearOfWork and iTrangThai = 1 and bHangCha = 0 and LNS in (select * from dbo.splitstring(@LNS)) order by XauNoiMa;
	--select * into #dv from NS_DonVi where NamLamViec = @YearOfWork and Id_DonVi in (
	--		select * FROM dbo.f_split((select Id_DonVi FROM TN_DT_ChungTu where Id = @ChungTuId))
	--);

	--select * into #tbl_global from (
	-- select  #mlns.*, #dv.Id_DonVi, #dv.TenDonVi from #mlns, #dv
	-- union all
	-- select 
	--	*
	-- ,'' as Id_DonVi,'' as TenDonVi FROM NS_MucLucNganSach mlns where mlns.bHangCha =1 and mlns.NamLamViec = @YearOfWork and LNS in (select * from dbo.splitstring(@LNS))
	--) tbl

	SELECT
	    mlns.iID_MLNS as MlnsId,
		mlns.iID_MLNS_Cha as MlnsIdParent,
		mlns.sXauNoiMa as XauNoiMa,
		mlns.sLNS as LNS,
		mlns.sL as L,
		mlns.sK as K,
		mlns.sM as M,
		mlns.sTM as TM,
		mlns.sTTM as TTM,
		mlns.sNG as NG,
		mlns.sTNG as TNG,
		mlns.sTNG1 as TNG1,
		mlns.sTNG2 as TNG2,
		mlns.sTNG3 as TNG3,
		isnull(mlns.sMoTa, '') as NoiDung,
		'' as Chuong,
		CASE 
			when tmp1.Loai = 1 then cast(1 as bit) else mlns.bHangCha
		END bHangCha,
		isnull(tmp1.Id, NEWID()) as Id,
		tmp1.IdChungTu,
		tmp1.NguonNganSach,
		tmp1.NamLamViec,
		tmp1.NamNganSach,
		tmp1.iTrangThai,
		tmp1.iPhanCap,
		tmp1.IdDonVi,
		tmp1.TenDonVi,
		tmp1.IdPhongBan,
		tmp1.IdPhongBanDich,
		tmp1.GhiChu,
		tmp1.DateCreated,
		tmp1.DateModified,
		tmp1.UserCreator,
		tmp1.UserModifier,
		tmp1.Log,
		tmp1.Tag,
		tmp1.IdDotNhan,
		tmp1.B,
		isnull(tmp1.TuChi, 0) as TuChi,
		tmp1.Loai,
		mlns.sChiTietToi SChiTietToi
		into #data
	FROM (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork and iTrangThai = 1 and sLNS in (select distinct * from f_split_lns(@LNS))) mlns
	left join #tmpData tmp1
	on mlns.iID_MLNS = tmp1.MLNS_Id
	Order by mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3
	
	select * into #dv from DonVi where iNamLamViec = @YearOfWork and iID_MaDonVi in (select * from f_split(@Agencies))
	insert into #data
	select 
		data1.MlnsId,
		data1.MlnsIdParent,
		data1.XauNoiMa,
		data1.LNS,
		data1.L,
		data1.K,
		data1.M,
		data1.TM,
		data1.TTM,
		data1.NG,
		data1.TNG,
		data1.TNG1,
		data1.TNG2,
		data1.TNG3,
		data1.NoiDung,
		data1.Chuong,
		data1.bHangCha,
		data1.Id,
		data1.IdChungTu,
		data1.NguonNganSach,
		data1.NamLamViec,
		data1.NamNganSach,
		data1.iTrangThai,
		data1.iPhanCap,
		#dv.iID_MaDonVi as IdDonVi,
		#dv.sTenDonVi as TenDonVi,
		data1.IdPhongBan,
		data1.IdPhongBanDich,
		data1.GhiChu,
		data1.DateCreated,
		data1.DateModified,
		data1.UserCreator,
		data1.UserModifier,
		data1.Log,
		data1.Tag,
		data1.IdDotNhan,
		data1.B,
	case 
    when IdDonVi = iID_MaDonVi then data1.TuChi
    when IdDonVi is null then 0
    end as TuChi,
    data1.Loai,
	data1.SChiTietToi
  from 
  (select * from #data where bHangCha = 0) as data1, #dv
  where IdDonVi = iID_MaDonVi or IdDonVi is null
  order by XauNoiMa, iID_MaDonVi

	delete from #data where bHangCha = 0 and IdDonVi is null;

	select distinct * from #data
	order by XauNoiMa, LNS, Loai


	drop table #tmpTotal
	drop table #tmpData
	drop table #tmpSubSum
	drop table #tmpSum
	drop table #data
END
;
;
;
GO
