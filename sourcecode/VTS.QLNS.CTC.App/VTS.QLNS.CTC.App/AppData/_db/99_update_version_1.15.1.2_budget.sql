IF NOT EXISTS (SELECT * FROM [dbo].[DM_ChuKy] WHERE Id_Type = 'rptNS_Plan_SoSanh_SKTDTDN_NSSD')
INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) 
VALUES (NEWID(), 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_Plan_SoSanh_SKTDTDN_NSSD', NULL, N'rptNS_Plan_SoSanh_SKTDTDN_NSSD', NULL, NULL, NULL, NULL, NULL, N'LAP_DU_TOAN_NGAN_SACH', NULL, N'Báo cáo đối chiếu số kiểm tra - dự toán chi tiết năm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'ĐỐI CHIẾU SỐ KIỂM TRA VÀ SỐ DỰ TOÁN CHI TIẾT NĂM 2025', N'1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
ELSE
UPDATE DM_ChuKy
SET TieuDe1_MoTa = N'ĐỐI CHIẾU SỐ KIỂM TRA VÀ SỐ DỰ TOÁN CHI TIẾT NĂM 2025'
WHERE Id_Type = 'rptNS_Plan_SoSanh_SKTDTDN_NSSD'
GO

update NS_MucLucNganSach
set bHienVat = 0
where inamlamviec = 2025
go

/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_sosanh_sktdtdn]    Script Date: 11/18/2024 4:33:57 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_sosanh_sktdtdn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_sosanh_sktdtdn]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_sosanh_sktdtdn]    Script Date: 11/18/2024 4:33:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ns_rpt_sosanh_sktdtdn]
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@MaNguonNganSach int,
	@MaDonVi nvarchar(max),
	@LoaiChungTu int,
	@InTheoTongHop bit,
	@DonViTinh int
AS
BEGIN

	select skt.iID_MLSKTCha, skt.iID_MLSKT, skt.sSTT + ' ' + skt.sMoTa as sMoTa, skt.bHangCha, skt.sM, ns.*
	into #mlns_skt 
	from NS_SKT_MucLuc skt
	join NS_MLSKT_MLNS ns on skt.sKyHieu = ns.sSKT_KyHieu
	where skt.iNamLamViec = @NamLamViec
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
		case when @LoaiChungTu = 1 then (sum(isnull(ctct.fTuChi, 0)))/@DonViTinh
		else (sum(isnull(ctct.fHangNhap, 0)))/@DonViTinh + (sum(isnull(ctct.fHangMua, 0)))/@DonViTinh end as fTuChi
	from NS_DTDauNam_ChungTuChiTiet ctct
	join NS_DTDauNam_ChungTu ct on ctct.iID_CTDTDauNam = ct.iID_CTDTDauNam
	join #mlns_skt ml on ctct.sXauNoiMa = ml.sNS_XauNoiMa
	left join NS_MucLucNganSach mlns on ctct.sXauNoiMa = mlns.sXauNoiMa and mlns.iNamLamViec = @NamLamViec and mlns.iTrangThai = 1
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = @LoaiChungTu
		and ct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
	group by mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.bHangCha, mlns.sLNS, ctct.sL, ctct.sK, ctct.sM, ctct.sTM, ctct.sTTM, ctct.sNG, ctct.sTNG, ctct.sXauNoiMa, ctct.sMoTa

	insert into #datatemp(IIDMLNS, IIDMLNSCha, IIDMLSKT, IIDMLSKTCha, IsHangCha, sLNS, sL, sK, sM, sTM, sTTM, sNG, sTNG, sXauNoiMa,sKyHieu, sMoTa, fMucTienPhanBo, fTuChi)
	select temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa,
	(sum(isnull(temp.fMucTienPhanBo, 0)))/@DonViTinh fMucTienPhanBo, sum(temp.fTuChi)/@DonViTinh fTuChi
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
		--case when cast(ml.sM as int) % 50 = 0 then concat(N'Nội dung định mức ', ml.sM, ': ', ml.sMoTa)
		--else concat(N'Nội dung định mức ', (cast(ml.sM as int)/50)*50, ' - ', ml.sM, ': ', ml.sMoTa) end sMoTa,
		ml.sMoTa,
		case when @LoaiChungTu = 1 then ctct.fTuChi
		else ctct.fMuaHangCapHienVat end as fMucTienPhanBo,
		0 fTuChi
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ct.iId_CTSoKiemTra = ct.iId_CTSoKiemTra
	join #mlns_skt ml on ctct.sKyHieu = ml.sSKT_KyHieu
	where ct.iNamLamViec = @NamLamViec
		and ct.iNamNganSach = @NamNganSach
		and ct.iLoaiChungTu = @LoaiChungTu
		and ((ctct.iLoai = 4 and @InTheoTongHop = 0) or (ctct.iLoai = 3 and @InTheoTongHop = 1))
		and ctct.iID_MaDonVi in (SELECT * FROM f_split(@MaDonVi))
		and (ct.iLoaiNguonNganSach = @NguonNganSach or @NguonNganSach = 0)
		and ct.iID_MaNguonNganSach = @MaNguonNganSach
		) temp
	group by temp.IIDMLNS, temp.IIDMLNSCha, temp.IIDMLSKT, temp.IIDMLSKTCha, temp.IsHangCha, temp.sLNS, temp.sL, temp.sK, temp.sM, temp.sTM, temp.sTTM, temp.sNG, temp.sTNG, temp.sXauNoiMa, temp.sSKT_KyHieu, temp.sMoTa

	select * from #datatemp 
	where fMucTienPhanBo <> 0 or fTuChi <> 0
	order by sKyHieu, sXauNoiMa

END
;

GO


