/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/25/2024 6:29:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvi_ngang]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_donvingang_nsdtn]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_excel]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_dtdn_du_toan_ngan_sach_nsdtn_excel]    Script Date: 11/25/2024 6:29:28 PM ******/
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
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]    Script Date: 11/25/2024 6:29:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl1]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@DVT int
AS
BEGIN
	
	declare @CapDonVi int = (select top 1 iCapDonVi from DonVi where iLoai = 0 and inamlamviec = @YearOfWork)

	declare @DsDonVi nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iCapDonVi = 2 and iNamLamViec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDuToan nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 2 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViDoanhNghiep nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 1 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	declare @DsDonViBVTC nvarchar(max) = (
	select distinct STUFF((
		SELECT distinct ',' + iID_MaDonVi
		from DonVi where iKhoi = 3 and inamlamviec = @YearOfWork and iTrangThai = 1
		FOR XML PATH(''),TYPE).value('(./text())[1]','NVARCHAR(MAX)')
	  ,1,1,'') iID_MaDonVi)

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where --ct.NamLamViec = @YearOfWork and 
		ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where --ct.iNamLamViec = @YearOfWork and 
		ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	select
       mlns.iID_MLNS IIdMlns,
       mlns.iID_MLNS_Cha IIdMlnsCha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sMoTa,
       mlns.bHangCha,
	   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
	   sum(isnull(ctbt.TuChi, 0))/@DVT fChiTaiBanThan,
	   sum(isnull(dt.TuChi, 0))/@DVT fKhoiDuToan,
	   sum(isnull(dn.TuChi, 0))/@DVT fKhoiDoanhNghiep,
	   sum(isnull(bvtc.TuChi, 0))/@DVT fBVTC,
	   'THU' sLoai
	from #basemlns mlns
	left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
	left join #basedatathu ctbt ON mlns.sXauNoiMa = ctbt.XauNoiMa and ctbt.iPhanCap = 1 and ctbt.NguonNganSach = @BudgetSource and ctbt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonVi))
	left join #basedatathu dt ON mlns.sXauNoiMa = dt.XauNoiMa and dt.iPhanCap = 1 and dt.NguonNganSach = @BudgetSource and dt.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan))
	left join #basedatathu dn ON mlns.sXauNoiMa = dn.XauNoiMa and dn.iPhanCap = 1 and dn.NguonNganSach = @BudgetSource and dn.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep))
	left join #basedatathu bvtc ON mlns.sXauNoiMa = bvtc.XauNoiMa and bvtc.iPhanCap = 1 and bvtc.NguonNganSach = @BudgetSource and bvtc.Id_DonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC))
	where mlns.sLNS like '8%'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

	UNION

	select
       mlns.iID_MLNS IIdMlns,
       mlns.iID_MLNS_Cha IIdMlnsCha,
       mlns.sXauNoiMa,
       mlns.sLNS,
       mlns.sMoTa,
       mlns.bHangCha,
	   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
	   sum(isnull(ctbt.fTuChi, 0))/@DVT fChiTaiBanThan,
	   sum(isnull(dt.fTuChi, 0))/@DVT fKhoiDuToan,
	   sum(isnull(dn.fTuChi, 0))/@DVT fKhoiDoanhNghiep,
	   sum(isnull(bvtc.fTuChi, 0))/@DVT fBVTC,
	   'CHI' sLoai
	from #basemlns mlns
	left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
	left join #basedatachi ctbt ON mlns.sXauNoiMa = ctbt.sXauNoiMa and ctbt.iPhanCap = 1 and ctbt.iID_MaNguonNganSach = @BudgetSource and ctbt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonVi))
	left join #basedatachi dt ON mlns.sXauNoiMa = dt.sXauNoiMa and dt.iPhanCap = 1 and dt.iID_MaNguonNganSach = @BudgetSource and dt.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDuToan))
	left join #basedatachi dn ON mlns.sXauNoiMa = dn.sXauNoiMa and dn.iPhanCap = 1 and dn.iID_MaNguonNganSach = @BudgetSource and dn.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViDoanhNghiep))
	left join #basedatachi bvtc ON mlns.sXauNoiMa = bvtc.sXauNoiMa and bvtc.iPhanCap = 1 and bvtc.iID_MaNguonNganSach = @BudgetSource and bvtc.iID_MaDonVi in (SELECT * FROM dbo.f_split(@DsDonViBVTC))
	where mlns.sLNS not like '8%'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.bHangCha

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]    Script Date: 11/25/2024 6:29:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2]
	@LNS nvarchar(max),
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

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ctct.Id_ChungTu = ct.Id
	where --ct.NamLamViec = @YearOfWork and 
	ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	where --ct.iNamLamViec = @YearOfWork and 
	ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha
	) temp

	CREATE TABLE #datadonvi (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		and donvi.Id_donvi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		insert into #datadonvi (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi not in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		--(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		--(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]    Script Date: 11/25/2024 6:29:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m01_pl2_donvi]
	@LNS nvarchar(max),
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@FromDate nvarchar(100),
	@ToDate nvarchar(100),
	@MaDonVi nvarchar(max),
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

	select * into #basemlns
	from NS_MucLucNganSach 
	where iNamLamViec = @YearOfWork 
	and bHangChaDuToan IS NOT NULL 
	and iTrangThai = 1 
	and sLNS in (select * from f_split(@LNS))
	
	--Thu
	select ctct.* into #basedatathu
	from TN_DT_ChungTuChiTiet ctct
	join TN_DT_ChungTu ct on ct.Id = ctct.Id_ChungTu
	where --ct.NamLamViec = @YearOfWork and 
	ct.NamNganSach = @YearOfBudget
		and convert(date, ct.NgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.NgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.Id_DonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)
		
	--Chi
	select ctct.* into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ct.iID_DTChungTu = ctct.iID_DTChungTu
	where --ct.iNamLamViec = @YearOfWork and 
	ct.iNamNganSach = @YearOfBudget
		and convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and ctct.iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

	--Data Ban than
	select temp.* into #databanthan from(
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBoBanThan,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		and donvi.Id_donvi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha,
			donvi.Id_donvi

		UNION

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.bHangCha,
		   0 fDuToanNSDuocGiao,
		   0 fChoPhanBo,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBoBanThan,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		and donvi.iID_MaDonVi in (SELECT * FROM f_split(@DonViBanThan))
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.bHangCha
	) temp

	CREATE TABLE #datadonvi1 (
    IIdMlns uniqueidentifier,
    IIdMlnsCha uniqueidentifier,
    sXauNoiMa nvarchar(500),
	sLNS nvarchar(500),
	sL nvarchar(500),
	sK nvarchar(500),
	sM nvarchar(500),
	sTM nvarchar(500),
	sTTM nvarchar(500),
	sNG nvarchar(500),
	sMoTa nvarchar(500),
	bHangCha bit,
	fDuToanNSDuocGiao float,
	FSoPhanBo float,
	sLoai nvarchar(50));

	--Data Don vi
	insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)
		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.TuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.TuChi, 0))/@DVT FSoPhanBo,
		   'THU' sLoai
		from #basemlns mlns
		left join #basedatathu donvi ON mlns.sXauNoiMa = donvi.XauNoiMa
		left join #basedatathu dtnsdg ON mlns.sXauNoiMa = dtnsdg.XauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.NguonNganSach = @BudgetSource
		where mlns.sLNS like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha

		insert into #datadonvi1 (IIdMlns, IIdMlnsCha, sXauNoiMa, sLNS, sMoTa,sL, sK, sM, sTM, sTTM, sNG, bHangCha, fDuToanNSDuocGiao, FSoPhanBo, sLoai)

		select
		   mlns.iID_MLNS IIdMlns,
		   mlns.iID_MLNS_Cha IIdMlnsCha,
		   mlns.sXauNoiMa,
		   mlns.sLNS,
		   mlns.sMoTa,
		   mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
		   mlns.bHangCha,
		   sum(isnull(dtnsdg.fTuChi, 0))/@DVT fDuToanNSDuocGiao,
		   sum(isnull(donvi.fTuChi, 0))/@DVT FSoPhanBo,
		   'CHI' sLoai
		from #basemlns mlns
		left join #basedatachi donvi ON mlns.sXauNoiMa = donvi.sXauNoiMa 
		left join #basedatachi dtnsdg ON mlns.sXauNoiMa = dtnsdg.sXauNoiMa and dtnsdg.iPhanCap = 0 and dtnsdg.iID_MaNguonNganSach = @BudgetSource
		where mlns.sLNS not like '8%'
		group by mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS,
			mlns.sMoTa,
			mlns.sL,
			mlns.sK,
			mlns.sM,
			mlns.sTM,
			mlns.sTTM,
			mlns.sNG,
			mlns.bHangCha
	

	--Ket qua
	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		--(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'THU' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'THU'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'THU'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

	union

	select
		mlns.iID_MLNS IIdMlns,
		mlns.iID_MLNS_Cha IIdMlnsCha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
		--(sum(isnull(banthan.FSoPhanBoBanThan, 0)) + sum(isnull(donvi.FSoPhanBo, 0))) FTongSoPhanBo,
		'CHI' sLoai
	from #basemlns mlns
	left join #datadonvi1 donvi on mlns.sXauNoiMa = donvi.sXauNoiMa and donvi.sLoai = 'CHI'
	left join #databanthan banthan on mlns.sXauNoiMa = banthan.sXauNoiMa and banthan.sLoai = 'CHI'
	group by mlns.iID_MLNS,
		mlns.iID_MLNS_Cha,
		mlns.sXauNoiMa,
		mlns.sLNS,
		mlns.sMoTa,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.bHangCha

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dt_phuong_an_phan_bo_du_toan_m02]    Script Date: 11/25/2024 6:29:28 PM ******/
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
		and ctct.Id_DonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)
		
	--Chi
	select ctct.*, ck.sMa sMaMlck into #basedatachi
	from NS_DT_ChungTuChiTiet ctct
	join NS_DT_ChungTu ct on ctct.iID_DTChungTu = ct.iID_DTChungTu
	join NS_DMCongKhai_MLNS map on map.sNS_XauNoiMa = ctct.sXauNoiMa
	join NS_DanhMucCongKhai ck on ck.Id = map.iID_DMCongKhai and ck.iNamLamViec = ctct.iNamLamViec
	where convert(date, ct.dNgayQuyetDinh, 103) >= convert(date, @FromDate, 103) 
		and convert(date, ct.dNgayQuyetDinh, 103) <= convert(date, @ToDate, 103)
		and ctct.sLNS not like '8%'
		and ctct.iID_MaDonVi not in (select top 1 iID_MaDonVi from DonVi where iNamLamViec = @YearOfWork and iTrangThai = 1 and iLoai = 0)

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
	sLoai nvarchar(50),
	iLoai int,
	iRoot int,
	hasData bit);

	--A. DỰ TOÁN THU
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, FSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'A. DỰ TOÁN THU' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 fSoPhanBoBanThan, 0 fSoPhanBo, 'THU' sLoai, 1 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa),
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) fSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
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
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha
	
	--B. DỰ TOÁN CHI
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select newid() IIdMlns, '00000000-0000-0000-0000-000000000000' IIdMlnsCha, null sMa, N'B. DỰ TOÁN CHI' sMoTa, 1 bHangCha, 0 fDuToanNSDuocGiao, 0 FSoPhanBoBanThan, 0 FSoPhanBo, 'CHI' sLoai, 2 iLoai, 1 iRoot, 1 hasData
	
	insert into #tblresultm02 (IIdMlns, IIdMlnsCha, sMa, sMoTa, bHangCha, fDuToanNSDuocGiao, fSoPhanBoBanThan, fSoPhanBo, sLoai, iLoai, iRoot, hasData)
	select
		mlns.Id IIdMlns,
		mlns.iID_DMCongKhai_Cha IIdMlnsCha,
		mlns.sMa,
		concat(mlns.STT,'. ', mlns.sMoTa) sMoTa,
		mlns.bHangCha,
		sum(isnull(donvi.fDuToanNSDuocGiao, 0)) fDuToanNSDuocGiao,
		sum(isnull(banthan.FSoPhanBoBanThan, 0)) FSoPhanBoBanThan,
		sum(isnull(donvi.FSoPhanBo, 0)) FSoPhanBo,
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
			concat(mlns.STT,'. ', mlns.sMoTa),
			mlns.bHangCha

	update #tblresultm02 set hasData = 0
	where (fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2)
	or (iRoot = 1 and iLoai in (select iLoai from #tblresultm02
		where fDuToanNSDuocGiao = 0 and FSoPhanBoBanThan = 0 and FSoPhanBo = 0 and iRoot = 2))

	select * from #tblresultm02 where hasData = 1
	order by iLoai, iRoot
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_get_donvi_solieu_chitiet_all_loai]    Script Date: 11/25/2024 6:29:28 PM ******/
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
				OR fHangMua <> 0 OR fTuChi <> 0)
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
			   AND chitiet.iLoai in (2, 4)
			   AND (chitiet.fTuChi <> 0
					OR chitiet.fMuaHangCapHienVat <> 0)
			)
ORDER BY iID_MaDonVi 
END
;
;
;
;
;
GO
