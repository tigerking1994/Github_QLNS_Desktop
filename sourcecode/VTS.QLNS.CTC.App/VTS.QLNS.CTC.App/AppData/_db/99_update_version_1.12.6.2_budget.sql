/****** Object:  StoredProcedure [dbo].[sp_tt_get_pheduyetthanhtoanchitiet_by_parentid]    Script Date: 24/02/2023 8:30:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tt_get_pheduyetthanhtoanchitiet_by_parentid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tt_get_pheduyetthanhtoanchitiet_by_parentid]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 24/02/2023 8:30:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_tonghop_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 24/02/2023 8:30:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_tao_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_tao_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 24/02/2023 8:30:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qs_capnhat_chitiet_year_begin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 24/02/2023 8:30:42 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_du_toan_chi_tieu_LNS_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
GO
/****** Object:  StoredProcedure [dbo].[rpt_du_toan_chi_tieu_LNS_1]    Script Date: 24/02/2023 8:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[rpt_du_toan_chi_tieu_LNS_1]
	@ChungTuId nvarchar(4000),
	@IDDMCongKhai nvarchar(MAX),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@VoucherDate datetime,
	@SoChungTu nvarchar(100),
	@dvt int
AS
BEGIN
	SELECT 
	   --isnull(ctct.iID_DTCTChiTiet, NEWID()) AS iID_DTCTChiTiet,
       --ctct.iID_DTChungTu,
       --mlns.iID_MLNS,
       --mlns.iID_MLNS_Cha,
       --mlns.sXauNoiMa,
       --mlns.sLNS,
       --mlns.sL,
       --mlns.sK,
       --mlns.sM,
       --mlns.sTM,
       --mlns.sTTM,
       --mlns.sNG,
       --mlns.sTNG,
       --mlns.sTNG1,
       --mlns.sTNG2,
       --mlns.sTNG3,
       --mlns.sMoTa,
       --mlns.bHangCha,
       --ctct.iNamNganSach,
       --ctct.iID_MaNguonNganSach,
       --ctct.iNamLamViec,
       --isnull(ctct.iPhanCap, 0) AS iPhanCap,
       ctct.iID_MaDonVi,
       --isnull(ctct.sGhiChu, '') AS sGhiChu,
       sum(isnull(ctct.fHangMua, 0)) / @dvt AS fHangMua,
       sum(isnull(ctct.fHangNhap, 0)) / @dvt AS fHangNhap,
       sum(isnull(ctct.fDuPhong, 0)) / @dvt AS fDuPhong,
       sum(isnull(ctct.fPhanCap, 0)) / @dvt AS fPhanCap,
       sum(isnull(ctct.fTuChi, 0)) / @dvt AS fTuChi,
       sum(isnull(ctct.fHienVat, 0)) / @dvt AS fHienVat,
       --ctct.dNgayTao,
       --ctct.sNguoiTao,
       --ctct.dNgaySua,
       --ctct.sNguoiSua, 
	   --ctct.iID_CTDuToan_Nhan,
	   --ctct.Id_DotPhanBoTruoc,
	   --isnull(ctct.iDuLieuNhan, 0) as iDuLieuNhan,
	   --mlns.sChiTietToi,
	   dv.sTenDonVi,
	   --mlns.bHangChaDuToan,
	   dmck.Id as iID_DMCongKhai,
	   dmck.iID_DMCongKhai_Cha as iID_DMCongKhai_Cha,
	   dmck.sMoTa,
	   dmck.sMa
	FROM 
	(SELECT * FROM NS_DanhMucCongKhai WHERE Id in (select * from f_split(@IDDMCongKhai))) dmck
	LEFT JOIN NS_DMCongKhai_MLNS dmckmlns on dmckmlns.iID_DMCongKhai = dmck.Id and dmckmlns.iNamLamViec = dmck.iNamLamViec
	LEFT JOIN (SELECT * FROM NS_MucLucNganSach WHERE iNamLamViec = @YearOfWork AND bHangChaDuToan IS NOT NULL and iTrangThai = 1) mlns 
	on mlns.sXauNoiMa = dmckmlns.sNS_XauNoiMa and dmckmlns.iNamLamViec = mlns.iNamLamViec
	--LEFT JOIN (SELECT * FROM NS_DMCongKhai_MLNS WHERE iID_DMCongKhai in (select * from f_split(@IDDMCongKhai))) dmckmlns on mlns.sXauNoiMa = dmckmlns.sNS_XauNoiMa
	LEFT JOIN
	(
		SELECT
			*
		FROM NS_DT_ChungTuChiTiet
		WHERE
			iID_DTChungTu in (SELECT * FROM dbo.f_split(@ChungTuId))
			AND iID_MaDonVi IS NOT NULL
			AND iDuLieuNhan = 0
	) ctct ON mlns.iID_MLNS = ctct.iID_MLNS
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1) dv
	ON dv.iID_MaDonVi = ctct.iID_MaDonVi
	GROUP BY ctct.iID_MaDonVi, ctct.iNamLamViec, dv.sTenDonVi, dmck.sMoTa, dmck.Id, dmck.iID_DMCongKhai_Cha, dmck.sMa
	--WHERE isnull(ctct.fTuChi, 0) > 0 OR isnull(ctct.fHienVat, 0) > 0 OR isnull(ctct.fPhanCap, 0) > 0 OR isnull(ctct.fHangNhap, 0) > 0 OR isnull(ctct.fHangMua, 0) > 0
	--ORDER BY mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_capnhat_chitiet_year_begin]    Script Date: 24/02/2023 8:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_capnhat_chitiet_year_begin]
	@YearOfWork int,
	@IdMaDonVi nvarchar(50)
AS
BEGIN
	update ct1 set 
		ct1.fSoThieuUy  = ct2.fSoThieuUy ,
		ct1.fSoTrungUy  = ct2.fSoTrungUy ,
		ct1.fSoThuongUy  = ct2.fSoThuongUy ,
		ct1.fSoDaiUy  = ct2.fSoDaiUy ,
		ct1.fSoThieuTa  = ct2.fSoThieuTa ,
		ct1.fSoTrungTa  = ct2.fSoTrungTa ,
		ct1.fSoThuongTa  = ct2.fSoThuongTa ,
		ct1.fSoDaiTa  = ct2.fSoDaiTa ,
		ct1.fSoTuong = ct2.fSoTuong,
		ct1.fSoTSQ  = ct2.fSoTSQ ,
		ct1.fSoBinhNhi  = ct2.fSoBinhNhi ,
		ct1.fSoBinhNhat  = ct2.fSoBinhNhat ,
		ct1.fSoHaSi  = ct2.fSoHaSi ,
		ct1.fSoTrungSi  = ct2.fSoTrungSi ,
		ct1.fSoThuongSi  = ct2.fSoThuongSi ,
		ct1.fSoQNCN  = ct2.fSoQNCN ,
		ct1.fSoCNVQP  = ct2.fSoCNVQP ,
		ct1.fSoLDHD  = ct2.fSoLDHD ,
		ct1.fSoCNVQPCT  = ct2.fSoCNVQPCT ,
		ct1.fSoQNVQPHD  = ct2.fSoQNVQPHD ,
		ct1.fTongSo  = ct2.fTongSo ,
		ct1.fSoSQ_KH  = ct2.fSoSQ_KH ,
		ct1.fSoHSQBS_KH  = ct2.fSoHSQBS_KH ,
		ct1.fSoCNVQP_KH  = ct2.fSoCNVQP_KH ,
		ct1.fSoLDHD_KH  = ct2.fSoLDHD_KH ,
		ct1.fSoQNCN_KH  = ct2.fSoQNCN_KH ,
		ct1.fSoVCQP  = ct2.fSoVCQP ,
		ct1.fSoCY_H  = ct2.fSoCY_H ,
		ct1.fSoCY_KT = ct2.fSoCY_KT
	from 
	NS_QS_ChungTuChiTiet as ct1
	INNER JOIN (
		select * from NS_QS_ChungTuChiTiet 
		where iThangQuy = 12 and iNamLamViec = @YearOfWork 
			and ((@IdMaDonVi <> '' and iID_MaDonVi = @IdMaDonVi) or @IdMaDonVi = '')
	) as ct2 on ct1.iID_MaDonVi = ct2.iID_MaDonVi and ct1.sKyHieu = ct2.sKyHieu
	where ct1.iThangQuy = 0;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qs_tao_chitiet]    Script Date: 24/02/2023 8:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qs_tao_chitiet]
	@IdChungTu nvarchar(100),
	@YearOfWork int,
	@Thang int,
	@User nvarchar(100)
AS
BEGIN

	select iID_MaDonVi into #dv 
	from DonVi 
	where iLoai = 1
	and iNamLamViec = @YearOfWork

	IF EXISTS (SELECT * FROM #dv) AND @Thang <> 0
		BEGIN
			select * into #qs1 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data1 from #qs1, #dv
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data1
			drop table #qs1, #dv, #data1
		END

	ELSE IF EXISTS (SELECT * FROM #dv) AND @Thang = 0
		BEGIN
			select * into #qs2 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork
			select * into #data2 from #qs2, #dv

			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data2
			drop table #qs2, #dv, #data2
		END
	ELSE IF @Thang <> 0
		BEGIN
			select iID_MaDonVi into #dv2 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork

			select * into #qs3 
			from NS_QS_MucLuc 
			where sKyHieu in ('100', '700') and iNamLamViec = @YearOfWork

			select * into #data3 from #qs3, #dv2
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data3
			drop table #qs, #dv2, #data3
		END
	ELSE 
		BEGIN
			select iID_MaDonVi into #dv4 
			from DonVi 
			where iLoai = 0
			and iNamLamViec = @YearOfWork
		
			select * into #qs4 
			from NS_QS_MucLuc 
			where iNamLamViec = @YearOfWork

			select * into #data4 from #qs4, #dv4
			insert into NS_QS_ChungTuChiTiet
				(iID_QSChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, iThangQuy, iID_MaDonVi, iNamLamViec, 
				fSoThieuUy, fSoTrungUy, fSoThuongUy, fSoDaiUy, fSoThieuTa, fSoTrungTa, fSoThuongTa, fSoDaiTa, fSoTuong,
				fSoTSQ, fSoBinhNhi, fSoBinhNhat, fSoHaSi, fSoTrungSi, fSoThuongSi, fSoQNCN, fSoCNVQP, fSoLDHD, 
				fSoCNVQPCT, fSoQNVQPHD, fTongSo, fSoSQ_KH, fSoHSQBS_KH, fSoCNVQP_KH, fSoLDHD_KH, fSoQNCN_KH, fSoVCQP, 
				fSoCY_H, fSoCY_KT, sGhiChu, dNgayTao, sNguoiTao, dNgaySua, sNgaySua)

				select @IdChungTu, iID_MLNS, iID_MLNS_Cha, sKyHieu, sMoTa, bHangCha, @Thang, iID_MaDonVi, @YearOfWork, 
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0, 0, 
					0, 0, null, getdate(), @User, null, null
				from #data4
			drop table #qs4, #dv4, #data4
		END

	
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_tonghop_donvi]    Script Date: 24/02/2023 8:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_tonghop_donvi]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@AgencyId nvarchar(max),
	@EstimateAgencyId nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@LNS nvarchar(max),
	@VoucherDate date,
	@Dvt int
AS
BEGIN
	select iID_MLNS, 
		sum(isnull(DuToan, 0)) as DuToan, 
		sum(isnull(QuyetToan, 0)) as QuyetToan, 
		sum(isnull(TrongKy, 0)) as TrongKy 
		into #tblData from 
	 (
		SELECT iID_MLNS,
			DuToan = sum(fTuChi),
			QuyetToan = 0,
			TrongKy = 0
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		AND IDuLieuNhan = 0
		GROUP BY iID_MLNS
		union all
		select iID_MLNS,
			DuToan = 0,
			QuyetToan = TuChi,
			TrongKy = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonthBefore, @AgencyId, @LNS)
		union all
		select iID_MLNS,
			DuToan = 0,
			QuyetToan = 0,
			TrongKy = TuChi
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)
		) dt
	where (DuToan <> 0 or QuyetToan <> 0 or TrongKy <> 0)
	group by iID_MLNS

	select iID_MLNS,
		iID_MaDonVi,
		sum(QuyetToanDonVi) as QuyetToanDonVi,
		sum(DuToanDonVi) as DuToanDonVi
		into #tblDataDonVi
	from (
		select iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = TuChi,
			DuToanDonVi = 0
		from f_quyettoan_tonghop(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)
		union all
		SELECT iID_MLNS,
			iID_MaDonVi,
			QuyetToanDonVi = 0,
			DuToanDonVi = sum(fTuChi)
		FROM NS_DT_ChungTuChiTiet
		WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
		)
		AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
		AND (@LNS IS NULL OR sLNS in (select * from f_split(@LNS)))
		AND IDuLieuNhan = 0
		GROUP BY iID_MLNS, iID_MaDonVi
	) dataDonVi
	group by iID_MLNS, iID_MaDonVi


	select dt.*, 
		dv.iID_MaDonVi, 
		isnull(dv.QuyetToanDonVi, 0) as QuyetToanDonVi,
		isnull(dv.DuToanDonVi, 0) as DuToanDonVi
		into #result 
	from #tblData dt
	left join #tblDataDonVi dv
	on dt.iID_MLNS = dv.iID_MLNS
	ORDER BY iID_MLNS

	select 
		mlns.iID_MLNS AS MlnsId,
		mlns.iID_MLNS_Cha AS MlnsIdCha,
		mlns.sLNS AS LNS,
		mlns.sL AS L,
		mlns.sK AS K,
		mlns.sM AS M,
		mlns.sTM AS TM,
		mlns.sTTM AS TTM,
		mlns.sNG AS NG,
		mlns.sTNG AS TNG,
		mlns.sTNG1 AS TNG1,
		mlns.sTNG2 AS TNG2,
		mlns.sTNG3 AS TNG3,
		mlns.bHangCha AS IsHangCha,
		mlns.sXauNoiMa AS XauNoiMa, 
		mlns.sMoTa AS MoTa, 
		(isnull(rs.DuToan, 0) / @Dvt) AS DuToan, 
		((isnull(rs.QuyetToan, 0) + isnull(rs.TrongKy, 0)) / @Dvt) AS QuyetToan, 
		(isnull(rs.TrongKy, 0) / @Dvt) AS TrongKy, 
		(isnull(rs.QuyetToanDonVi, 0) / @Dvt) AS QuyetToanDonVi, 
		(isnull(rs.DuToanDonVi, 0) / @Dvt) AS DuToanDonVi, 
		case
			when rs.iID_MaDonVi is null and bHangCha = 0 then @EstimateAgencyId
			else isnull(rs.iID_MaDonVi, '')
		end as IdMaDonVi
		
	from (select * from NS_MucLucNganSach where iNamLamViec = @YearOfWork) mlns
	left join #result rs
	on mlns.iID_MLNS = rs.iID_MLNS
	WHERE bHangCha = 1 OR (DuToan <> 0 OR QuyetToan <> 0 OR TrongKy <> 0 OR QuyetToanDonVi <> 0)
	order by sXauNoiMa

	drop table #tblData, #tblDataDonVi, #result
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tt_get_pheduyetthanhtoanchitiet_by_parentid]    Script Date: 24/02/2023 8:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_tt_get_pheduyetthanhtoanchitiet_by_parentid]
@uIdPheDuyet uniqueidentifier
AS
BEGIN
	SELECT (CASE 
				WHEN tbl.iLoaiThanhToan = 2 THEN 2 
				WHEN ISNULL(dt.fGiaTriThanhToanNN, 0) != 0 OR ISNULL(dt.fGiaTriThanhToanTN, 0) != 0 THEN 1
				WHEN ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) != 0 OR ISNULL(dt.fGiaTriThuHoiNamTruocTN, 0) != 0 THEN 4
				WHEN ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) != 0 OR ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) != 0 THEN 5
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN, 0) != 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0) != 0 THEN 6
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0) != 0 OR ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0) != 0 THEN 7
				ELSE 0 
			END) as ILoaiDeNghi,
			(CASE WHEN dt.iLoaiKeHoachVon >=3 THEN 1 
				WHEN dt.iLoaiKeHoachVon <=2 THEN 2 
			END) as iLoaiNamKeHoach,
			dt.iID_KeHoachVonID as IIDKeHoachVonID, 
			(CASE 
				WHEN ISNULL(dt.fGiaTriThanhToanNN, 0) != 0 THEN ISNULL(dt.fGiaTriThanhToanNN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiNamTruocNN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiNamNayNN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiNamNayNN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocNN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiUngTruocNamNayNN, 0)
				ELSE 0 
			END) as FGiaTriNgoaiNuoc,
			(CASE 
				WHEN ISNULL(dt.fGiaTriThanhToanTN, 0) != 0 THEN ISNULL(dt.fGiaTriThanhToanTN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiNamTruocTN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiNamTruocTN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiNamNayTN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiNamNayTN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiUngTruocNamTruocTN, 0)
				WHEN ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0) != 0 THEN ISNULL(dt.fGiaTriThuHoiUngTruocNamNayTN, 0)
				ELSE 0 
			END) as FGiaTriTrongNuoc, 
			dt.SGhiChu,
			ml.sLNS as LNS, ml.sL as L, ml.sK as K, ml.sM as M, ml.sTM as TM, ml.sTTM as TTM, ml.sNG as NG,
			dt.iID_MucID as IIdMuc, dt.iID_TieuMucID as IIdTieuMuc, dt.iID_TietMucID as IIdTietMuc, dt.iID_NganhID as IIdNganh
	FROM VDT_TT_DeNghiThanhToan as tbl
	INNER JOIN VDT_TT_PheDuyetThanhToan_ChiTiet as dt on tbl.Id = dt.iID_DeNghiThanhToanID
	LEFT JOIN NS_MucLucNganSach as ml on  dt.iID_MucID = ml.iID
									OR dt.iID_TieuMucID = ml.iID
									OR dt.iID_TietMucID = ml.iID
									OR dt.iID_NganhID = ml.iID
	WHERE tbl.Id = @uIdPheDuyet
END
;
;
GO
