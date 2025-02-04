/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/20/2024 1:29:52 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/20/2024 6:01:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]    Script Date: 11/20/2024 6:01:17 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]
GO

/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/20/2024 1:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1
	
	CREATE TABLE #datatemp (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
	);

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0)))/@DVT fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(temp.fTuChi)/@DVT fTuChi
	from (
	select
		distinct
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
		0 fTuChi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select * from #datatemp order by sKyHieu, sXauNoiMa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/20/2024 1:29:52 PM ******/
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

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.fTuChi, 0)) fTuChi
		from (
		select
			distinct
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
			0 fTuChi
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			) temp
		group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
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
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.fTuChi, 0)) fTuChi
	from (
	select
		distinct
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
		0 fTuChi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/20/2024 1:29:52 PM ******/
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

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhapBanThan, 0)) FHangNhapBanThan, sum(isnull(temp.FHangMuaBanThan, 0)) FHangNhapBanThan
		from (
		select distinct
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
			0 FHangNhapBanThan,
			0 FHangMuaBanThan
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua
	from NS_DTDauNam_ChungTuChiTiet ctct
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
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0))
	from (
	select
		distinct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/20/2024 1:29:52 PM ******/
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

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChiBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChiBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi
		from (
		select
			distinct
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
			0 fTuChi
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 1
			and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fTuChi, 0))) fTuChi,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_DTDauNam_ChungTuChiTiet ctct
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
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa, ctct.iID_MaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0))) fMucTienPhanBo, sum(temp.fTuChi) fTuChi, temp.IIdMaDonVi
	from (
	select
		distinct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 1
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi


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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/20/2024 1:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@DVT int
AS
BEGIN

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.bCoDinhMuc = 1
		and skt.iNamLamViec = @NamLamViec
		and ns.iNamLamViec = @NamLamViec
		and skt.iTrangThai = 1
		and ns.iTrangThai = 1
	
	CREATE TABLE #datatemp (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fHangNhap float, fHangMua float
	);

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0)))/@DVT fHangNhap,
		(sum(isnull(ctct.fHangMua, 0)))/@DVT fHangMua
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(isnull(temp.fHangNhap, 0))/@DVT fTuChi, sum(isnull(temp.fHangMua, 0))/@DVT fTuChi
	from (
	select
		distinct
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
		0 fHangNhap,
		0 fHangMua
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select * from #datatemp order by sKyHieu, sXauNoiMa

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/20/2024 1:29:52 PM ******/
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

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhapBanThan,
		(sum(isnull(ctct.fHangMua, 0))) FHangMuaBanThan
		from NS_DTDauNam_ChungTuChiTiet ctct
		join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
		join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
		left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
		group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa
	
		insert into #tbl_banthan(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhapBanThan, FHangMuaBanThan)
		select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
		sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhapBanThan, 0)), sum(isnull(temp.FHangMuaBanThan, 0)) 
		from (
		select
			distinct
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
			0 FHangNhapBanThan,
			0 FHangMuaBanThan
		from NS_SKT_ChungTuChiTiet ctct
		join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
		join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
		where ct.iNamLamViec = @NamLamViec
			and ct.iNamNganSach = @NamNganSach
			and ct.iLoaiChungTu = 2
			and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa


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
		ctct.sMoTa,
		0 fMucTienPhanBo,
		(sum(isnull(ctct.fHangNhap, 0))) FHangNhap,
		(sum(isnull(ctct.fHangMua, 0))) FHangMua,
		ctct.iID_MaDonVi IIdMaDonVi
	from NS_DTDauNam_ChungTuChiTiet ctct
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
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa, ctct.iID_MaDonVi

	insert into #datadonvi(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, FHangNhap, FHangMua, IIdMaDonVi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	sum(isnull(temp.fMucTienPhanBo, 0)) fMucTienPhanBo, sum(isnull(temp.FHangNhap, 0)), sum(isnull(temp.FHangMua, 0)), temp.IIdMaDonVi
	from (
	select
		distinct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ((ctct.iLoai = 4 and @NguonNganSach <> 2) or (ctct.iLoai = 2 and @NguonNganSach = 2))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa, temp.IIdMaDonVi

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
GO

/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]    Script Date: 11/20/2024 6:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_PAPBDT_m02_get_dm_congkhai]
	@XauNoiMa nvarchar(max),
	@NamLamViec int
AS
BEGIN
	
	select distinct ck.* into #treeChild
	from NS_DanhMucCongKhai ck
	join NS_DMCongKhai_MLNS map on ck.id = map.iID_DMCongKhai
	where ck.iNamLamViec = @NamLamViec
		and map.iNamLamViec = @NamLamViec
		and map.sNS_XauNoiMa in (SELECT * FROM f_split(@XauNoiMa));

	WiTH #treeParent
	AS
	(
		SELECT * FROM NS_DanhMucCongKhai where sMa IN (select DISTINCT sMaCha FROM #treeChild) AND iNamLamViec = @NamLamViec
		UNION ALL
		SELECT pr.* from NS_DanhMucCongKhai pr  
		INNER JOIN #treeParent child ON pr.sMa = child.sMaCha AND pr.iNamLamViec = @NamLamViec
	)

	SELECT DISTINCT * FROM #treeParent;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/20/2024 6:01:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
	@MaCongKhai nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	declare @DonViBanThan NVARCHAR(MAX) = (SELECT SUBSTRING(( SELECT ',' + iID_MaDonVi AS [text()] 
											FROM DonVi pr
											WHERE iNamLamViec = @YearOfWork 
													and iLoai = 1
													and iCapDonVi = (SELECT TOP 1 iCapDonVi FROM DonVi WHERE iNamLamViec = @YearOfWork and iLoai = 0)
											FOR XML PATH (''), TYPE
												).value('text()[1]','nvarchar(max)'), 2, 1000))

	select ck.* into #basemlns
	from NS_DanhMucCongKhai ck
	where (ck.iID_DMCongKhai_Cha IN (SELECT * FROM f_split(@MaCongKhai)) OR (ck.Id IN (SELECT * FROM f_split(@MaCongKhai))))
		and ck.iNamLamViec = @YearOfWork

	--Thu
	select ctct.*, ck.sMa sMaMlck into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.XauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.NamLamViec
	where convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.LNS like '8%'
		
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'

	CREATE TABLE #databanthanm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	sLoai nvarchar(50));

	--Data Ban than
	insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT fSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathu dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #databanthanm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachi dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	CREATE TABLE #datadonvim02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)
		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatathu dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

		insert into #datadonvim02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBo, sLoai)

		select
		   mlns.Id IIdMlns,
		   mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		   mlns.sMa,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sMa = donvi.sMaMlck
		left join #basedatachi dtnsdg ON mlns.sMa = dtnsdg.sMaMlck and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	--Ket qua

	CREATE TABLE #tblresultm02 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sMa nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	fSoPhanBoBanThan float,
	fSoPhanBo float,
	fTongSoPhanBo float,
	sLoai nvarchar(50),
	iLoai int,
	iRoot int,
	hasData bit);

	--A. DỰ TOÁN THU
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, FSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'A. DỰ TOÁN THU' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 fSoPhanBoBanThan, 0 fSoPhanBo, 0 FTongSoPhanBo, 'THU' sLoai, 1 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) fSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) fTongSoPhanBo,
		'THU' sLoai,
		1 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'THU'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'THU'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha
	
	--B. DỰ TOÁN CHI
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'B. DỰ TOÁN CHI' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 FSoPhanBoBanThan, 0 FSoPhanBo, 0 FTongSoPhanBo, 'CHI' sLoai, 2 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, fTongSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		mlns.sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai,
		2 iLoai,
		2 iRoot,
		1 hasData
	from #basemlns mlns
	left join #datadonvim02 donvi on mlns.sMa = donvi.sMa and donvi.sLoai = 'CHI'
	left join #databanthanm02 banthan on mlns.sMa = banthan.sMa and banthan.sLoai = 'CHI'
	group by mlns.Id,
			mlns.iID_DMCongKhai_Cha,
			mlns.sMa,
			mlns.sMoTa,
			mlns.bHangCha

	update #tblresultm02 set hasData = 0
	where (fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2)
	or (iRoot = 1 and iLoai in (select iLoai from #tblresultm02
		where fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2))

	select * from #tblresultm02 where hasData = 1
	order by iLoai, iRoot
END
;
GO
