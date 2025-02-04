/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 16/12/2022 5:38:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]    Script Date: 16/12/2022 5:38:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 16/12/2022 5:38:03 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_mucluccongkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_mucluccongkhai]    Script Date: 16/12/2022 5:38:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dt_dutoan_mucluccongkhai]
	@ListIdPublic nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Time int
AS
BEGIN
	WITH tblDuToanDuocGiao AS (
		SELECT SUM(ctct.fTuChi) AS DuToanDuocGiao, dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai
		INNER JOIN NS_MucLucNganSach mlns 
		ON mlns.sXauNoiMa = ctct.sXauNoiMa
		AND mlns.iNamLamViec = @YearOfWork
		AND mlns.bHangChaDuToan = 0
		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 0 and iDuLieuNhan = 0
			AND ((@Time = 0  AND ct.iLoaiDuToan = 1) 
			OR @Time = 12
			   OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			   )
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
		GROUP BY dmck.iID_DMCongKhai, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	),

	tblSoPhanBo AS (
		SELECT SUM(ctct.fTuChi) AS SoPhanBo, dmck.iID_DMCongKhai, ctct.iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
		FROM NS_DT_ChungTuChiTiet ctct
		INNER JOIN NS_DMCongKhai_MLNS dmck
		ON ctct.sXauNoiMa = dmck.sNS_XauNoiMa AND ctct.iNamLamViec = dmck.iNamLamViec
	    INNER JOIN NS_DT_ChungTu ct
		ON ctct.iID_DTChungTu = ct.iID_DTChungTu
		INNER JOIN NS_DanhMucCongKhai as ctck
		ON ctck.Id = dmck.iID_DMCongKhai

		WHERE ctct.iNamLamViec = @YearOfWork 
			AND ctct.iNamNganSach = @YearOfBudget 
			AND ctct.iID_MaNguonNganSach = @BudgetSource
			AND ct.iLoai = 1
			AND (
			      (@Time = 0  AND ct.iLoaiDuToan = 1) 
				OR @Time = 12
			    OR (@Time <> 0 and (YEAR(ct.dNgayQuyetDinh) < @YearOfWork or (MONTH(ct.dNgayQuyetDinh) <= @Time and YEAR(ct.dNgayQuyetDinh) = @YearOfWork)))
			)
			AND (ctck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (ctck.Id IN (SELECT * FROM f_split(@ListIdPublic))))
		GROUP BY dmck.iID_DMCongKhai, iID_MaDonVi, ctct.iNamLamViec, ctck.iID_DMCongKhai_Cha
	)

	SELECT 
		dmck.STT AS sSTT,
		dmck.sMoTa AS sMoTa,
		ISNULL(dtdg.DuToanDuocGiao, 0) AS fDuToanDuocGiao,
		ISNULL(dtdg.DuToanDuocGiao, 0) - ISNULL(spb.SoPhanBo, 0) AS fSoChuaPhanBo,
		ISNULL(spb.SoPhanBo, 0) AS fSoPhanBo,
		dmck.Id AS iID_DMCongKhai,
		dmck.iID_DMCongKhai_Cha,
		dv.iID_MaDonVi,
		dv.sTenDonVi
	FROM NS_DanhMucCongKhai dmck
	LEFT JOIN tblDuToanDuocGiao dtdg ON dtdg.iID_DMCongKhai = dmck.Id
	LEFT JOIN tblSoPhanBo spb
	ON dtdg.iID_DMCongKhai = spb.iID_DMCongKhai
	LEFT JOIN DonVi dv ON spb.iID_MaDonVi = dv.iID_MaDonVi AND spb.iNamLamViec = dv.iNamLamViec
	WHERE dmck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@ListIdPublic)) OR (dmck.Id IN (SELECT * FROM f_split(@ListIdPublic)))

END
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]    Script Date: 16/12/2022 5:38:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dutoan_rpt_inchitieu_nganh_donvi_all_mlns]			
	 @NamLamViec int,							
	 @NguonNganSach nvarchar(50),
	 @NamNganSach nvarchar(50),
	 @Nganh nvarchar(2000),
	 @IdChungTu nvarchar(max),
	 @Dvt int,
	 @IsLuyKe int
AS	 
BEGIN 
	SET NOCOUNT ON;

	DECLARE @index int = 0;
	DECLARE @ngayQuyetDinh Date;

	SELECT TOP 1 @index = iSoChungTuIndex,
	@ngayQuyetDinh = CASE WHEN dNgayQuyetDinh is not null THEN  CAST(dNgayQuyetDinh  AS DATE) 
	 ELSE CAST(dNgayChungTu  AS Date) 
	 END

	FROM NS_DT_ChungTu WHERE iID_DTChungTu 
	in (SELECT * FROM f_split(@IdChungtu))
	ORDER BY dNgayQuyetDinh DESC

	SELECT iID_DTChungTu INTO #tempIds 
	FROM NS_DT_ChungTu 
	WHERE iSoChungTuIndex <= @index 
		AND ((dNgayQuyetDinh is not null AND CAST(dNgayQuyetDinh AS DATE) <= @ngayQuyetDinh) or (dNgayQuyetDinh is null AND CAST(dNgayChungTu AS DATE) <= @ngayQuyetDinh))
		AND iNamLamViec = @NamLamViec
		AND iNamNganSach = @NamNganSach 
		AND iID_MaNguonNganSach = @NguonNganSach
		AND iLoai = 1
	 
SELECT LNS1 = Left(sLNS, 1),
       LNS3 = Left(sLNS, 3),
       LNS5 = Left(sLNS, 5),
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
	   concat(sLNS,'-',sL,'-',sK,'-',sM,'-',sTM,'-',sTTM,'-',sNG) AS XauNoiMa,

	   ctct.iID_MaDonVi as MaDonVi,
	   dv.sTenDonVi as TenDonVi,

       TuChi = sum(fTuChi)/@Dvt,
       HienVat = sum(fHienVat)/@Dvt,
	   DuPhong = sum(fDuPhong)/@Dvt,
	   HangNhap = sum(fHangNhap)/@Dvt,
	   HangMua = sum(fHangMua)/@Dvt,
	   PhanCap = sum(fPhanCap)/@Dvt,
	   TonKho = sum(fTonKho)/@Dvt
	into #tempCtct
	FROM (
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE (@IsLuyKe = 0 AND iID_DTChungTu in (select * FROM f_split(@IdChungtu)))
			OR (@IsLuyKe = 1 AND iID_DTChungTu in (SELECT * FROM #tempIds))
		UNION ALL
		SELECT * FROM NS_DT_ChungTuChiTiet
			WHERE
			(@IsLuyKe = 0 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo in (select * FROM f_split(@IdChungtu)))
				AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
			OR (@IsLuyKe = 1 AND iID_DTChungTu IN (SELECT iID_CTDuToan_Nhan FROM NS_DT_Nhan_PhanBo_Map WHERE iID_CTDuToan_PhanBo IN (SELECT * FROM #tempIds))
			AND iID_DTChungTu IN (SELECT iID_DTChungTu FROM NS_DT_ChungTu WHERE iLoai = 1))
		) ctct
	LEFT JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec) dv
	ON ctct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE (@Nganh IS NULL
       OR sNG in
         (SELECT *
          FROM f_split(@Nganh)))
		GROUP BY sLNS,
				 sL,
				 sK,
				 sM,
				 sTM,
				 sTTM,
				 sNG,
				 ctct.iID_MaDonVi,
				 dv.sTenDonVi--,
		HAVING sum(fTuChi) <> 0
		OR sum(fHienVat) <> 0
		OR sum(fDuPhong) <> 0
		OR sum(fHangNhap) <> 0
		OR sum(fHangMua) <> 0
		OR sum(fTonKho) <> 0
		OR sum(fPhanCap) <> 0;



		SELECT #tempCtct.*, mlns.sMoTa AS MoTa, mlns.iID_MLNS AS MlnsId, mlns.iID_MLNS_Cha AS MlnsIdParent FROM #tempCtct 
		LEFT JOIN (select * FROM NS_MucLucNganSach where iNamLamViec = @NamLamViec) mlns on #tempCtct.XauNoiMa = mlns.sXauNoiMa

		ORDER BY LNS1, LNS3, LNS5, LNS, L, K, M, TM, TTM, NG asc, MaDonVi desc;

		DROP TABLE #tempIds;
		DROP TABLE #tempCtct;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]    Script Date: 16/12/2022 5:38:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_skt_rpt_dutoandaunam_congkhai_02CKNS]
	@iNamLamViec int,
	@iNamNganSach int,
	@iMaNguonNganSach int,
	@iQuarterMonths int,
	@sIdDanhMucCongKhai nvarchar(max),
	@dvt int

AS
BEGIN
	select   sum(isnull(ctct.fTuChi,0))/@dvt as fTuChi, dm_mlns.iID_DMCongKhai as iID_DMCongKhai
		into #temp
		from NS_DT_ChungTuChiTiet as ctct
		inner join NS_DMCongKhai_MLNS as dm_mlns on  dm_mlns.sNS_XauNoiMa = ctct.sXauNoiMa
		inner join NS_DT_ChungTu as ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
		inner join NS_DanhMucCongKhai as dm on dm.Id =  dm_mlns.iID_DMCongKhai
		where ct.iNamLamViec = @iNamLamViec and ct.iID_MaNguonNganSach = @iMaNguonNganSach and ct.iNamNganSach = @iNamNganSach
		and CT.iLoai = 0 and iDuLieuNhan = 0
		and ((@iQuarterMonths = 0 and ct.iLoaiDuToan = 1) or (@iQuarterMonths <> 0 and (YEAR(ct.dNgayQuyetDinh) < @iNamLamViec or (MONTH(ct.dNgayQuyetDinh) <= @iQuarterMonths and YEAR(ct.dNgayQuyetDinh) = @iNamLamViec) ) ))
		and( dm.Id  IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)) or  dm.iID_DMCongKhai_Cha  IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)))
		group by dm_mlns.iID_DMCongKhai

	

	select 
		dm.Id as Id_DanhMuc,
		dm.iID_DMCongKhai_Cha as Id_DanhMucCha,
		dm.STT as STT,
		dm.sMoTa as sMoTa,
		dm.bHangCha as bHangCha,
		dm.sMa as sMa,
		fTuChi as fTuChi
		from NS_DanhMucCongKhai as dm
		left join #temp as temp on dm.Id = temp.iID_DMCongKhai
		where dm.iNamLamViec = @iNamLamViec and( dm.Id  IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)) or  dm.iID_DMCongKhai_Cha  IN (SELECT * FROM f_split(@sIdDanhMucCongKhai)))
		order by sMa
	
END
;
GO
