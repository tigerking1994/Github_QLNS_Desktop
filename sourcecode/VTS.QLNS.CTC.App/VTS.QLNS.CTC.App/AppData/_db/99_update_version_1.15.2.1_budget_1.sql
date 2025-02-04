/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/6/2024 2:39:06 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 12/10/2024 3:04:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 12/10/2024 3:04:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 12/10/2024 3:04:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 12/10/2024 3:04:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
/****** Object:  StoredProcedure [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]    Script Date: 12/6/2024 2:39:06 PM ******/
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_qt_rpt_dutoan_donvi_quy_clone]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@EstimateAgencyId nvarchar(max),
	@AgencyId nvarchar(max),
	@LNS nvarchar(max),
	@QuarterMonth nvarchar(100),
	@QuarterMonthBefore nvarchar(100),
	@VoucherDate datetime,
	@Dvt int
AS
BEGIN
	DECLARE @IsDonViCha bit;
	IF @EstimateAgencyId = @AgencyId
		SET @IsDonViCha = 0
	ELSE
		SET @IsDonViCha = 1
	SELECT sLNS,
		   iID_MLNS,
		   --TenDonVi,
		   ChiTieu = ROUND(SUM(ChiTieu) / @Dvt, 0),
		   TuChi = ROUND(SUM(TuChi) / @Dvt, 0),
		   Quy1 = ROUND(SUM(Quy1) / @Dvt, 0),
		   Quy2 = ROUND(SUM(Quy2) / @Dvt, 0),
		   Quy3 = ROUND(SUM(Quy3) / @Dvt, 0),
		   Quy4 = ROUND(SUM(Quy4) / @Dvt, 0)
		   INTO #tblEstimateSettlement
	FROM
	  (--chi tieu theo don vi
	  SELECT sLNS,
			iID_MLNS,
			iID_MaDonVi,
			ChiTieu = fTuChi + fHangNhap + fHangMua,
			TuChi = 0,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
	   FROM NS_DT_ChungTuChiTiet
	   WHERE iID_DTChungTu in 
		(
			SELECT iID_DTChungTu 
			FROM NS_DT_ChungTu 
			WHERE iNamLamViec = @YearOfWork
				AND iNamNganSach = @YearOfBudget
				AND iID_MaNguonNganSach = @BudgetSource
				AND (sSoQuyetDinh is not null or sSoQuyetDinh <> '')
				AND (cast(dNgayQuyetDinh as DATE) <= cast(@VoucherDate as date)
				OR (YEAR(CAST(dNgayQuyetDinh AS DATE)) - YEAR(CAST(@VoucherDate AS DATE)) = 1
				AND MONTH(CAST(dNgayQuyetDinh AS DATE)) IN (1,2,3) AND MONTH(CAST(@VoucherDate AS DATE)) = 12))
		)
		 AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@EstimateAgencyId)))
		 AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
		 AND IDuLieuNhan = 0
		 
	   UNION ALL 
	   SELECT sLNS,
			iID_MLNS,
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = fTuChi_PheDuyet,
			Quy1 = 0,
			Quy2 = 0,
			Quy3 = 0,
			Quy4 = 0
	   FROM f_quyettoan_clone(@YearOfWork, @YearOfBudget, @BudgetSource, @QuarterMonth, @AgencyId, @LNS)

	   UNION ALL 
	   select sLNS, 
			iID_MLNS, 
			CASE
			    WHEN @IsDonViCha = 1 THEN @EstimateAgencyId
		    ELSE 
				iID_MaDonVi
		    END AS iID_MaDonVi,
			ChiTieu = 0,
			TuChi = 0,
			isnull([1], 0) AS Quy1,
			isnull([2], 0) AS Quy2,
			isnull([3], 0) AS Quy3,
			isnull([4], 0) AS Quy4
			from (
				select sLNS, iID_MLNS, iID_MaDonVi, isnull(fTuChi_PheDuyet, 0) as TuChi,
					case 
						when iThangQuy = 1 then 1
						when iThangQuy = 2 then 1
						when iThangQuy = 3 then 1
						when iThangQuy = 4 then 2
						when iThangQuy = 5 then 2
						when iThangQuy = 6 then 2
						when iThangQuy = 7 then 3
						when iThangQuy = 8 then 3
						when iThangQuy = 9 then 3
						when iThangQuy = 10 then 4
						when iThangQuy = 11 then 4
						when iThangQuy = 12 then 4
					end as quy
				FROM NS_QT_ChungTuChiTiet
				WHERE iID_QTChungTu in
					(SELECT iID_QTChungTu
					FROM NS_QT_ChungTu
					WHERE iNamLamViec = @YearOfWork
						AND INamNganSach = @YearOfBudget
						AND iID_MaNguonNganSach = @BudgetSource
						AND iThangQuy in (select * from f_split(@QuarterMonth))
						--AND iThangQuyLoai = @QuarterMonthType
						)
					AND (@AgencyId IS NULL OR iID_MaDonVi in (SELECT * FROM f_split(@AgencyId)))
					AND (@LNS IS NULL OR sLNS in (SELECT * FROM dbo.splitstring(@LNS)))
			) as data
			pivot 
			(
				SUM(TuChi) for quy IN ( [1], [2], [3], [4] )
			) as Thang
	) AS ct
	LEFT JOIN
	  (SELECT iID_MaDonVi
			  --TenDonVi = iID_MaDonVi + ' - ' + sTenDonVi
	   FROM DonVi
	   WHERE iTrangThai=1
		 AND iNamLamViec = @YearOfWork) AS dv ON dv.iID_MaDonVi = ct.iID_MaDonVi
	GROUP BY 
			 sLNS,
			 --TenDonVi,
			 iID_MLNS
	HAVING SUM(TuChi) <> 0
	OR SUM(ChiTieu) <> 0
	OR SUM(Quy1) <> 0
	OR SUM(Quy2) <> 0
	OR SUM(Quy3) <> 0
	OR SUM(Quy4) <> 0;

	select distinct sLNS into #tblLNS from #tblEstimateSettlement;
	SELECT mlns.iID_MLNS,
		isnull(mlns.iID_MLNS_Cha, '00000000-0000-0000-0000-000000000000') iID_MLNS_Cha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sTNG1,
		mlns.sTNG2,
		mlns.sTNG3,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		mlns.bHangCha as IsHangCha,
		isnull(dt.ChiTieu, 0) as ChiTieu,
		isnull(dt.TuChi, 0) as TuChi,
		isnull(Quy1, 0) as Quy1,
		isnull(Quy2, 0) as Quy2,
		isnull(Quy3, 0) as Quy3,
		isnull(Quy4, 0) as Quy4,
		'' as TenDonVi
	FROM (
		SELECT *
		   FROM NS_MucLucNganSach
		   WHERE iTrangThai=1
			 AND iNamLamViec = @YearOfWork
			 AND sLNS in
				(SELECT DISTINCT VALUE
					FROM
					(SELECT CAST(LEFT(sLNS, 1) AS nvarchar(10)) LNS1,
							CAST(LEFT(sLNS, 3) AS nvarchar(10)) LNS3,
							CAST(LEFT(sLNS, 5) AS nvarchar(10)) LNS5,
							CAST(sLNS AS nvarchar(10)) LNS
					FROM #tblLNS) LNS UNPIVOT (value FOR col in (LNS1, LNS3, LNS5, LNS)) un)) mlns
	LEFT JOIN #tblEstimateSettlement dt
	ON dt.iID_MLNS = mlns.iID_MLNS
	WHERE bHangCha = 1 OR (bHangCha = 0 AND (dt.TuChi <> 0 OR dt.ChiTieu <> 0 OR dt.Quy1 <> 0 OR dt.Quy2 <> 0
		OR dt.Quy3 <> 0 OR dt.Quy4 <> 0))
	ORDER BY mlns.sXauNoiMa
	DROP TABLE #tblEstimateSettlement, #tblLNS;
END
;
;
;
;
;
;
;
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 12/10/2024 3:04:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #NS_DTDauNam_ChungTuChiTiet_tmp from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	select * into #NS_SKT_ChungTuChiTiet_tmp from NS_SKT_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	
	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, ns.bHangCha, skt.sM, map.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS map on skt.sKyHieu = map.sSKT_KyHieu
	join NS_MucLucNganSach ns on map.sNS_XauNoiMa = ns.sXauNoiMa
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChiBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from #NS_DTDauNam_ChungTuChiTiet_tmp ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
		having sum(isnull(ctct.fTuChi, 0)) <> 0
		
	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi
	from #NS_DTDauNam_ChungTuChiTiet_tmp ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
	having sum(isnull(ctct.fTuChi, 0)) <> 0
	
	CREATE TABLE #tempSKT (
		IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
		sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
		sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
		);

	insert into #tempSKT (IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.fTuChi, 0)) fTuChi
	from (
	select
			distinct
			ctct.iID_MaDonVi,
			null IIDMLNS,
			null IIDMLNSCha,
			ml.iID_MLSKT IIDMLSKT,
			ml.iID_MLSKTCha IIDMLSKTCha,
			ml.bHangCha IsHangCha,
			null sLNS,
			null sL,
			null sK,
			ml.sM,
			null sTM,
			null sTTM,
			null sNG,
			null sTNG,
			null sXauNoiMa,
			ml.sSKT_KyHieu sKyHieu,
			case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
			else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
			ctct.fTuChi fMucTienPhanBo,
			0 fTuChi
		from #NS_SKT_ChungTuChiTiet_tmp ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ctct.iLoai in (2, 4)
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
			and isnull(ctct.fTuChi, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
			) temp
		group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, fTuChi
		from #tempSKT

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(temp.fTuChi)/@DVT fTuChi
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sKyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi
	from #NS_SKT_ChungTuChiTiet_tmp ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1 and ml.bCoDinhMuc = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and ctct.sKyHieu not in (select distinct sKyHieu from #tempSKT)
		and isnull(ctct.fTuChi, 0) <> 0
		and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa

	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(dv.fTuChi, 0))))/@DVT TongTuChi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 12/10/2024 3:04:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #NS_DTDauNam_ChungTuChiTiet_tempdt from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	select * into #NS_SKT_ChungTuChiTiet_tempdt from NS_SKT_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	
	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, ns.bHangCha, skt.sM, map.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS map on skt.sKyHieu = map.sSKT_KyHieu
	join NS_MucLucNganSach ns on map.sNS_XauNoiMa = ns.sXauNoiMa
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhapBanThan float, FHangMuaBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from #NS_DTDauNam_ChungTuChiTiet_tempdt ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
		having sum(isnull(ctct.fHangNhap, 0)) <> 0 or (sum(isnull(ctct.fHangMua, 0))) <> 0

		
	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua
	from #NS_DTDauNam_ChungTuChiTiet_tempdt ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
	having sum(isnull(ctct.FHangNhap, 0)) <> 0 or sum(isnull(ctct.FHangMua, 0)) <> 0

	CREATE TABLE #tempSKTDonVi (
		IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
		sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
		sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float
		);

	insert into #tempSKTDonVi (IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0))
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua
	from #NS_SKT_ChungTuChiTiet_tempdt ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and isnull(ctct.fMuaHangCapHienVat, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua
	from #tempSKTDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0))
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sKyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua
	from #NS_SKT_ChungTuChiTiet_tempdt ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1 and ml.bCoDinhMuc = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and ctct.sKyHieu not in (select distinct sKyHieu from #tempSKTDonVi)
		and isnull(ctct.fMuaHangCapHienVat, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa


	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.FHangNhap, 0)))/@DVT FHangNhap,
		(sum(isnull(dv.FHangMua, 0)))/@DVT FHangMua,
		(sum(isnull(banthan.FHangNhapBanThan, 0)))/@DVT FHangNhapBanThan,
		(sum(isnull(banthan.FHangMuaBanThan, 0)))/@DVT FHangMuaBanThan,
		((sum(isnull(banthan.FHangNhapBanThan, 0))) + (sum(isnull(dv.FHangNhap, 0))))/@DVT TongHangNhap,
		((sum(isnull(banthan.FHangMuaBanThan, 0))) + (sum(isnull(dv.FHangMua, 0))))/@DVT TongHangMua
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 12/10/2024 3:04:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #NS_DTDauNam_ChungTuChiTiet_temp from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	select * into #NS_SKT_ChungTuChiTiet_temp from NS_SKT_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	
	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, ns.bHangCha, skt.sM, map.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS map on skt.sKyHieu = map.sSKT_KyHieu
	join NS_MucLucNganSach ns on map.sNS_XauNoiMa = ns.sXauNoiMa
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChiBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from #NS_DTDauNam_ChungTuChiTiet_temp ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
		having sum(isnull(ctct.fTuChi, 0)) <> 0

	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float, IIdMaDonVi nvarchar(500)
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_DTDauNam_ChungTuChiTiet_temp ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa, ctct.iID_MaDonVi
	having sum(isnull(ctct.fTuChi, 0)) <> 0

	CREATE TABLE #tempSKTDonVi (
		IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
		sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
		sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float, IIdMaDonVi nvarchar(500)
		);

	insert into #tempSKTDonVi (IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi, temp.IIdMaDonVi
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_SKT_ChungTuChiTiet_temp ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and isnull(ctct.fTuChi, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi
		from #tempSKTDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi, temp.IIdMaDonVi
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sKyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fTuChi fMucTienPhanBo,
		0 fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_SKT_ChungTuChiTiet_temp ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1 and ml.bCoDinhMuc = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and ctct.sKyHieu not in (select distinct sKyHieu from #tempSKTDonVi)
		and isnull(ctct.fTuChi, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
			) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa, temp.IIdMaDonVi


	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.fTuChi, 0)))/@DVT fTuChi,
		(sum(isnull(banthan.fTuChiBanThan, 0)))/@DVT fTuChiBanThan,
		((sum(isnull(banthan.fTuChiBanThan, 0))) + (sum(isnull(dv.fTuChi, 0))))/@DVT TongTuChi,
		dv.IIdMaDonVi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa, dv.IIdMaDonVi

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 12/10/2024 3:04:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @NamLamViec 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @NamLamViec and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select * into #NS_DTDauNam_ChungTuChiTiet_tmpdt from NS_DTDauNam_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	select * into #NS_SKT_ChungTuChiTiet_tmpdt from NS_SKT_ChungTuChiTiet where iNamLamViec = @NamLamViec and iNamNganSach = @NamNganSach
	
	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, ns.bHangCha, skt.sM, map.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS map on skt.sKyHieu = map.sSKT_KyHieu
	join NS_MucLucNganSach ns on map.sNS_XauNoiMa = ns.sXauNoiMa
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1

	--Ban than
	CREATE TABLE #tbl_banthan (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhapBanThan float, FHangMuaBanThan float
	);

	insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from #NS_DTDauNam_ChungTuChiTiet_tmpdt ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa
		having sum(isnull(ctct.fHangNhap, 0)) <> 0 or sum(isnull(ctct.fHangMua, 0)) <> 0

		
	--Don vi
	CREATE TABLE #datadonvi (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float, IIdMaDonVi nvarchar(500)
	);

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select
		mlns.iID_MLNS IIDMLNS,
		mlns.iID_MLNS_Cha IIDMLNSCha,
		null IIDMLSKT,
		(
			SELECT top 1 skt.iID_MLSKT FROM NS_MucLucNganSach ns
			join NS_MLSKT_MLNS map on map.sNS_XauNoiMa = ns.sXauNoiMa and map.iNamLamViec = @NamLamViec
			join NS_SKT_MucLuc skt on skt.sKyHieu = map.sSKT_KyHieu and skt.iNamLamViec = @NamLamViec and skt.iTrangThai = 1
			WHERE sXauNoiMa = ctct.sXauNoiMa
				AND ns.iNamLamViec = @NamLamViec
				and ns.iTrangThai = 1
		) IIDMLSKTCha,
		mlns.bHangCha IsHangCha,
		mlns.sLNS,
		ctct.sL,
		ctct.sK,
		ctct.sM,
		ctct.sTM,
		ctct.sTTM,
		ctct.sNG,
		ctct.sTNG,
		ctct.sXauNoiMa,
		(select sSKT_KyHieu from #mlns_skt where sNS_XauNoiMa = ctct.sXauNoiMa) sKyHieu,
		mlns.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_DTDauNam_ChungTuChiTiet_tmpdt ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, mlns.sMoTa, ctct.iID_MaDonVi
	having sum(isnull(ctct.fHangNhap, 0)) <> 0 or sum(isnull(ctct.fHangMua, 0)) <> 0

	CREATE TABLE #tempSKTDonVi (
		IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
		sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
		sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, FHangNhap float, FHangMua float, IIdMaDonVi nvarchar(500)
		);

	insert into #tempSKTDonVi (IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0)), temp.IIdMaDonVi
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sSKT_KyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_SKT_ChungTuChiTiet_tmpdt ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and isnull(ctct.fMuaHangCapHienVat, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi
		from #tempSKTDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0)), temp.IIdMaDonVi
	from (
	select
		distinct
		ctct.iID_MaDonVi,
		null IIDMLNS,
		null IIDMLNSCha,
		ml.iID_MLSKT IIDMLSKT,
		ml.iID_MLSKTCha IIDMLSKTCha,
		ml.bHangCha IsHangCha,
		null sLNS,
		null sL,
		null sK,
		ml.sM,
		null sTM,
		null sTTM,
		null sNG,
		null sTNG,
		null sXauNoiMa,
		ml.sKyHieu,
		case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ctct.fMuaHangCapHienVat fMucTienPhanBo,
		0 FHangNhap,
		0 FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from #NS_SKT_ChungTuChiTiet_tmpdt ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1 and ml.bCoDinhMuc = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and ctct.sKyHieu not in (select distinct sKyHieu from #tempSKTDonVi)
		and isnull(ctct.fMuaHangCapHienVat, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa, temp.IIdMaDonVi

	select ml.* into #mlns_base from
	(select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #tbl_banthan
	union
	select distinct IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa from #datadonvi) ml
	
	--Ket qua
	select
		ml.IIDMLNS,
		ml.IIDMLNSCha,
		ml.IsHangCha,
		ml.IIDMLSKT,
		ml.IIDMLSKTCha,
		ml.sLNS,
		ml.sL,
		ml.sK,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sNG,
		ml.sTNG,
		ml.sXauNoiMa,
		ml.sKyHieu,
		ml.sMoTa,
		(sum(isnull(dv.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo,
		(sum(isnull(dv.FHangNhap, 0)))/@DVT FHangNhap,
		(sum(isnull(dv.FHangMua, 0)))/@DVT FHangMua,
		(sum(isnull(banthan.FHangNhapBanThan, 0)))/@DVT FHangNhapBanThan,
		(sum(isnull(banthan.FHangMuaBanThan, 0)))/@DVT FHangMuaBanThan,
		((sum(isnull(banthan.FHangNhapBanThan, 0))) + (sum(isnull(dv.FHangNhap, 0))))/@DVT TongHangNhap,
		((sum(isnull(banthan.FHangMuaBanThan, 0))) + (sum(isnull(dv.FHangMua, 0))))/@DVT TongHangMua,
		dv.IIdMaDonVi
	from #mlns_base ml
	left join #datadonvi dv on isnull(ml.sXauNoiMa, '') = isnull(dv.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(dv.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	left join #tbl_banthan banthan on isnull(ml.sXauNoiMa, '') = isnull(banthan.sXauNoiMa, '') and isnull(ml.IIDMLNS, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLNS, '00000000-0000-0000-0000-000000000000') and isnull(ml.IIDMLSKT, '00000000-0000-0000-0000-000000000000') = isnull(banthan.IIDMLSKT, '00000000-0000-0000-0000-000000000000')
	group by ml.IIDMLNS, ml.IIDMLNSCha, ml.IsHangCha, ml.IIDMLSKT, ml.IIDMLSKTCha, ml.sLNS, ml.sL, ml.sK, ml.sM,
		ml.sTM, ml.sTTM, ml.sNG, ml.sTNG, ml.sXauNoiMa, ml.sKyHieu, ml.sMoTa, dv.IIdMaDonVi

END
;
;
;
GO
