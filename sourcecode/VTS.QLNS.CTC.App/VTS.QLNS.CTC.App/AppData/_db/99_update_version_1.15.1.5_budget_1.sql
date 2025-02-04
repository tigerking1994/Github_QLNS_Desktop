/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/22/2024 5:19:31 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/22/2024 5:19:31 PM ******/
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
	having sum(isnull(ctct.fTuChi, 0)) <> 0

	CREATE TABLE #tempSKT (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fTuChi float
	);

		insert into #tempSKT(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
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
			ml.sSKT_KyHieu sKyHieu,
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
			and ctct.iLoai in (2, 4)
			and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
			and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
			and ct.iID_MaNguonNganSach = @MaNguonNganSach
			and isnull(ctct.fTuChi, 0) <> 0
			and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
			) temp
		group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, fTuChi
	from #tempSKT

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
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


	select * from #datatemp order by sKyHieu, sXauNoiMa

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/22/2024 5:19:31 PM ******/
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
		from NS_SKT_ChungTuChiTiet ctct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/22/2024 5:19:31 PM ******/
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
	from NS_SKT_ChungTuChiTiet ctct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/22/2024 5:19:31 PM ******/
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
	from NS_SKT_ChungTuChiTiet ctct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
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
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/22/2024 5:19:31 PM ******/
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
	having sum(isnull(ctct.fHangNhap, 0)) <> 0 or (sum(isnull(ctct.fHangMua, 0))) <> 0

	CREATE TABLE #tempSKT (
    IIDMLNS uniqueidentifier, IIDMLNSCha uniqueidentifier, IIDMLSKT uniqueidentifier, IIDMLSKTCha uniqueidentifier, IsHangCha bit,
	sLNS nvarchar(50), sL nvarchar(50), sK nvarchar(50), sM nvarchar(50), sTM nvarchar(50), sTTM nvarchar(50), sNG nvarchar(50), sTNG nvarchar(50), sXauNoiMa nvarchar(500),
	sKyHieu nvarchar(500), sMoTa nvarchar(max), fMucTienPhanBo float, fHangNhap float, fHangMua float
	);

	insert into #tempSKT(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(isnull(temp.fHangNhap, 0))/@DVT fTuChi, sum(isnull(temp.fHangMua, 0))/@DVT fTuChi
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
		0 fHangNhap,
		0 fHangMua
	from NS_SKT_ChungTuChiTiet ctct
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
	

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa, sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua
	from #tempSKT

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fHangNhap, fHangMua)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DVT fMucTienPhanBo, sum(isnull(temp.fHangNhap, 0))/@DVT fTuChi, sum(isnull(temp.fHangMua, 0))/@DVT fTuChi
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
		0 fHangNhap,
		0 fHangMua
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = 2
		and ctct.iLoai in (2, 4)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		and ctct.sKyHieu not in (select distinct sKyHieu from #tempSKT)
		and isnull(ctct.fMuaHangCapHienVat, 0) <> 0
		and ctct.iID_CTSoKiemTra in (select iID_CTSoKiemTra from NS_SKT_ChungTu where iNamLamViec = @NamLamViec and (iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0))
	) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sKyHieu, temp.sMoTa

	select * from #datatemp order by sKyHieu, sXauNoiMa

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/22/2024 5:19:31 PM ******/
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
	from NS_SKT_ChungTuChiTiet ctct
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
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join NS_SKT_MucLuc ml on ctct.sKyHieu = ml.sKyHieu and ml.iNamLamViec = @NamLamViec and ml.iTrangThai = 1
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
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 11/22/2024 5:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetOfSource int,
	@ILoaiNNS int,
	@ILoaiChungTu int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM DonVi
	WHERE iNamLamViec = @YearOfWork
	  AND iTrangThai = 1
	  AND iLoai <> 0
	  AND iID_MaDonVi in
		(SELECT DISTINCT chitiet.iID_MaDonVi
		 FROM NS_DTDauNam_ChungTuChiTiet chitiet
		 INNER JOIN NS_DTDauNam_ChungTu chungtu 
		 ON chitiet.iID_CTDTDauNam = chungtu.iID_CTDTDauNam
		 AND chitiet.iNamLamViec = chungtu.iNamLamViec
		 AND (@ILoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @ILoaiNNS) 
		 WHERE chitiet.iNamLamViec = @YearOfWork
		   AND chitiet.iNamNganSach = @YearOfBudget
		   AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
		   AND chungtu.iLoaiChungTu = @ILoaiChungTu
		   AND (fHangNhap <> 0
				OR fHangMua <> 0)
			union
			SELECT DISTINCT chitiet.iID_MaDonVi
			 FROM NS_SKT_ChungTuChiTiet chitiet
			 INNER JOIN NS_SKT_ChungTu chungtu 
			 ON chitiet.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
			 AND chitiet.iNamLamViec = chungtu.iNamLamViec
			 AND (@ILoaiNNS = 0 OR chungtu.iLoaiNguonNganSach = @ILoaiNNS) 
			 WHERE chitiet.iNamLamViec = @YearOfWork
			   AND chitiet.iNamNganSach = @YearOfBudget
			   AND chitiet.iID_MaNguonNganSach = @BudgetOfSource
			   AND chungtu.iLoaiChungTu = @ILoaiChungTu
			   AND chitiet.iLoai = 4
			   AND (chitiet.fTuChi <> 0
					OR chitiet.fMuaHangCapHienVat <> 0)
			)
ORDER BY iID_MaDonVi 
END
;
;
;
;
GO
