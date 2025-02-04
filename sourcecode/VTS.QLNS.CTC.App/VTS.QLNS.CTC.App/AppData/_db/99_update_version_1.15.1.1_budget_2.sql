/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/14/2024 5:53:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]    Script Date: 11/14/2024 5:53:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]    Script Date: 11/14/2024 5:53:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_skt_dutoan_daunam_phancap_donvi0_1]
	
@YearOfWork int,
@iID_CTDTDauNam uniqueidentifier

AS
BEGIN
	SET NOCOUNT ON;
--SELECT iID_MLNS AS MucLucID,
--       sLNS AS LNS,
--       sL AS L,
--       sK AS K,
--       sM AS M,
--       sTM AS TM,
--       sTTM AS TTM,
--       sNG AS NG,
--	   sTNG AS TNG,
--       sTNG1 AS TNG1,
--       sTNG2 AS TNG2,
--       sTNG3 AS TNG3,
--       sMoTa AS MoTa,
--       sXauNoiMa AS XauNoiMa,
--       '' AS IdDonViMLNS,
--       NULL AS Id,
--       NULL AS SoLieuChiTietId,
--       '' AS IdDonVi,
--       '' AS TenDonVi,
--       NULL AS MLNSId,
--       0 AS TuChi,
--       '' AS GhiChu,
--       bHangCha
--FROM NS_MucLucNganSach
--WHERE iNamLamViec = @YearOfWork
--  AND bHangCha = 1
--  AND sLNS in
--    (SELECT *
--     FROM  NS_DTDauNam_PhanCap where iID_CTDTDauNam = @iID_CTDTDauNam)
--UNION ALL
SELECT MucLucID,
       sLNS AS LNS,
       sL AS L,
       sK AS K,
       sM AS M,
       sTM AS TM,
       sTTM AS TTM,
       sNG AS NG,
	   sTNG AS TNG,
       sTNG1 AS TNG1,
       sTNG2 AS TNG2,
       sTNG3 AS TNG3,
       sMoTa AS MoTa,
       mlns.sXauNoiMa AS XauNoiMa,
	   phancap.sXauNoiMaGoc AS XauNoiMaGoc,
       IdDonViMLNS,
       Id,
       SoLieuChiTietId,
       isnull(IdDonVi, IdDonViMLNS) AS IdDonVi,
       sTenDonVi TenDonVi,
       MLNSId,
       isnull(TuChi, 0) AS TuChi,
       GhiChu,
       cast(0 AS bit) AS bHangCha
FROM (
        (SELECT mlns.iID_MLNS AS MucLucID,
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
                mlns.sMoTa,
                mlns.sXauNoiMa,
                donvi.iID_MaDonVi AS IdDonViMLNS,
                donvi.sTenDonVi
         FROM (select * from NS_MucLucNganSach where bHangCha = 0) mlns,
              DonVi donvi
         WHERE mlns.iNamLamViec = @YearOfWork
           AND donvi.iNamLamViec = @YearOfWork
           AND donvi.iLoai = '2'
           AND donvi.iTrangThai =1) mlns
      LEFT JOIN
        (SELECT NEWID() AS ID,
                NEWID() AS SoLieuChiTietId,
                iID_MaDonVi AS IdDonVi,
                iID_MLNS AS MLNSId,
                SUM(isnull(fTuChi, 0)) AS TuChi,
                '' AS GhiChu,
				NS_DTDauNam_PhanCap.sXauNoiMa,
				NS_DTDauNam_PhanCap.sXauNoiMaGoc
         FROM NS_DTDauNam_PhanCap
         WHERE iNamLamViec = @YearOfWork AND iID_CTDTDauNam = @iID_CTDTDauNam
         GROUP BY iID_MaDonVi,
                  iID_MLNS,
				  NS_DTDauNam_PhanCap.sXauNoiMa,
				  NS_DTDauNam_PhanCap.sXauNoiMaGoc) phancap ON mlns.sXauNoiMa = phancap.sXauNoiMa
      AND mlns.IdDonViMLNS = phancap.IdDonVi)
WHERE TuChi > 0
ORDER BY mlns.sXauNoiMa, IdDonViMLNS END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/14/2024 5:53:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]
	@Loai nvarchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@LoaiChungTu int,
	@IdDonVi nvarchar(max),
	@NganSach nvarchar(5),
	@UserName nvarchar(100),
	@LoaiBaoCao int,
	@KieuBaoCao int,
	@DonViTinh int
AS
BEGIN

	-- Nam cu
	select sum(ctct.fTuChi) fTuChi,sum(ctct.fMuaHangCapHienVat) fMuaHangCapHienVat,sum(ctct.fPhanCap) fPhanCap, donvi.sTenDonVi sTenDonVi1,donvi.iID_MaDonVi,mucluc_before.sKyHieu
	into #tempTable1
	from 
	ns_skt_mucluc  mucluc_before 
	left join NS_SKT_ChungTuChiTiet ctct on mucluc_before.sKyHieu=ctct.sKyHieu
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	where ctct.iNamLamViec = @NamLamViec - 1
	and donvi.iTrangThai = 1
	and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	and ctct.iNamLamViec = @NamLamViec - 1
	and ctct.iNamNganSach = @NamNganSach
	and ctct.iID_MaNguonNganSach = @NguonNganSach
	and ctct.iLoai = @Loai

	and ct.iNamLamViec = @NamLamViec - 1
	and ct.iNamNganSach = @NamNganSach
	and ct.iID_MaNguonNganSach = @NguonNganSach
	and ct.iLoaiChungTu = @LoaiChungTu

	and nguoidung.iID_MaNguoiDung = @UserName
	and nguoidung.iTrangThai = 1

	and mucluc_before.iNamLamViec = @NamLamViec - 1
	and mucluc_before.bHangCha=0

	group by mucluc_before.sKyHieu, donvi.iID_MaDonVi,donvi.sTenDonVi


	select sum(ctct.fTuChi) fTuChi,sum(ctct.fMuaHangCapHienVat) fMuaHangCapHienVat,sum(ctct.fPhanCap) fPhanCap, donvi.sTenDonVi sTenDonVi1,donvi.iID_MaDonVi,mucluc_now.sKyHieu
	into #tempTable2
	from 
		ns_skt_mucluc  mucluc_now
		left join NS_SKT_ChungTuChiTiet ctct on mucluc_now.sKyHieu=ctct.sKyHieu
		join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
		join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
		join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
		where ctct.iNamLamViec = @NamLamViec 
		and donvi.iTrangThai = 1
		and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

		and ctct.iNamLamViec = @NamLamViec 
		and ctct.iNamNganSach = @NamNganSach
		and ctct.iID_MaNguonNganSach = @NguonNganSach
		and ctct.iLoai = @Loai

		and ct.iNamLamViec = @NamLamViec 
		and ct.iNamNganSach = @NamNganSach
		and ct.iID_MaNguonNganSach = @NguonNganSach
		and ct.iLoaiChungTu = @LoaiChungTu

		and nguoidung.iID_MaNguoiDung = @UserName
		and nguoidung.iTrangThai = 1

		and mucluc_now.iNamLamViec = @NamLamViec 
		and mucluc_now.bHangCha=0
		group by mucluc_now.sKyHieu, donvi.iID_MaDonVi,donvi.sTenDonVi


	select * into #tempsKyHieu from 
	(
		select sKyHieu from #tempTable1
		where isnull(fTuChi,0)!=0 or isnull(fPhanCap,0)!=0 or ISNULL(fMuaHangCapHienVat,0) !=0
		Union 
		select sKyHieu from #tempTable2
		where isnull(fTuChi,0)!=0 or isnull(fPhanCap,0)!=0 or ISNULL(fMuaHangCapHienVat,0) !=0
	) tblsKyHieu

	select tbl_kyhieu.*,dv.iID_MaDonVi, dv.sTenDonVi sTenDonVi into #tempSKyHieuDonVi from #tempsKyHieu tbl_kyhieu, DonVi dv
	where dv.iNamLamViec=@NamLamViec
	and dv.iID_MaDonVi in (select * from f_split(@IdDonVi))
	order by sKyHieu

	select 
	mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL sL,
	mucluc.SK sK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.sMoTa  MoTa,
	mucluc.bHangCha,
	mucluc.sKyHieuCu,
	dv.iID_MaDonVi,
	dv.sTenDonVi
	into #tempTableMLDV
	from ns_skt_mucluc mucluc 
	left join #tempSKyHieuDonVi dv on mucluc.sKyHieu=dv.sKyHieu
	where mucluc.iNamLamViec=@NamLamViec
	order by mucluc.sKyHieu

	select mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL,
	mucluc.SK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.MoTa,
	mucluc.bHangCha,
	case when @LoaiBaoCao = 1 then sum(tbl_before.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(tbl_before.fMuaHangCapHienVat) / @DonViTinh
	else sum(tbl_before.fPhanCap) / 1 end soLieuCot1,

	case when @LoaiBaoCao = 1 then sum(tbl_now.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(tbl_now.fMuaHangCapHienVat) / @DonViTinh
	else sum(tbl_now.fPhanCap) / @DonViTinh end soLieuCot2,

	sum(tbl_before.fTuChi) fTuChiNamTruoc,
	sum(tbl_now.fTuChi) fTuChiNamNay,
	sum(tbl_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	sum(tbl_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	sum(tbl_before.fPhanCap) fDacThuNamTruoc,
	sum(tbl_now.fPhanCap) fDacThuNamNay
	into #tempTable3
	from #tempTableMLDV mucluc 
	left join #tempTable1 tbl_before on mucluc.iID_MaDonVi=tbl_before.iID_MaDonVi and mucluc.sKyHieuCu=tbl_before.sKyHieu
	left join #tempTable2 tbl_now on mucluc.iID_MaDonVi=tbl_now.iID_MaDonVi and mucluc.sKyHieu=tbl_now.sKyHieu
	group by mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL,
	mucluc.SK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.MoTa,
	mucluc.bHangCha

	select mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL sL,
	mucluc.SK sK,
	'' sM,
	'' sNG,
	--mucluc.sM,
	--mucluc.sNG,
	case when isnull(tbl_now.sTenDonVi1, '') = '' and isnull(tbl_before.sTenDonVi1, '') = '' then mucluc.MoTa
	when isnull(tbl_now.sTenDonVi1, '') = '' then concat('   + ', tbl_before.sTenDonVi1)
	else concat('   + ', tbl_now.sTenDonVi1)
	end as MoTa,
	mucluc.bHangCha,
	case when @LoaiBaoCao = 1 then sum(tbl_before.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(tbl_before.fMuaHangCapHienVat) / @DonViTinh
	else sum(tbl_before.fPhanCap) / @DonViTinh end soLieuCot1,

	case when @LoaiBaoCao = 1 then sum(tbl_now.fTuChi) / @DonViTinh
	when @LoaiBaoCao = 2 then sum(tbl_now.fMuaHangCapHienVat) / @DonViTinh
	else sum(tbl_now.fPhanCap) / @DonViTinh end soLieuCot2,

	sum(tbl_before.fTuChi) fTuChiNamTruoc,
	sum(tbl_now.fTuChi) fTuChiNamNay,
	sum(tbl_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	sum(tbl_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	sum(tbl_before.fPhanCap) fDacThuNamTruoc,
	sum(tbl_now.fPhanCap) fDacThuNamNay
	into #tempTable4
	from #tempTableMLDV mucluc 
	left join #tempTable1 tbl_before on mucluc.iID_MaDonVi=tbl_before.iID_MaDonVi and mucluc.sKyHieuCu=tbl_before.sKyHieu
	left join #tempTable2 tbl_now on mucluc.iID_MaDonVi=tbl_now.iID_MaDonVi and mucluc.sKyHieu=tbl_now.sKyHieu

	group by mucluc.sKyHieu,
	mucluc.iID_MLSKT,
	mucluc.iID_MLSKTCha,
	mucluc.SL,
	mucluc.SK,
	mucluc.sM,
	mucluc.sNG,
	mucluc.MoTa,
	mucluc.bHangCha,
	tbl_now.iID_MaDonVi,
	tbl_now.sTenDonVi1,
	tbl_before.sTenDonVi1



	--select 
	--mucluc.sKyHieu,
	--mucluc.iID_MLSKT,
	--mucluc.iID_MLSKTCha,
	--mucluc.SL sL,
	--mucluc.SK sK,
	--mucluc.sM,
	--mucluc.sNG,
	--mucluc.sMoTa MoTa,
	--mucluc.bHangCha,
	--case when @LoaiBaoCao = 1 then sum(chitiet_before.fTuChi) / @DonViTinh
	--when @LoaiBaoCao = 2 then sum(chitiet_before.fMuaHangCapHienVat) / @DonViTinh
	--else sum(chitiet_before.fPhanCap) / @DonViTinh end soLieuCot1,

	--case when @LoaiBaoCao = 1 then sum(chitiet_now.fTuChi) / @DonViTinh
	--when @LoaiBaoCao = 2 then sum(chitiet_now.fMuaHangCapHienVat) / @DonViTinh
	--else sum(chitiet_now.fPhanCap) / @DonViTinh end soLieuCot2,

	--sum(chitiet_before.fTuChi) fTuChiNamTruoc,
	--sum(chitiet_now.fTuChi) fTuChiNamNay,
	--sum(chitiet_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	--sum(chitiet_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	--sum(chitiet_before.fPhanCap) fDacThuNamTruoc,
	--sum(chitiet_now.fPhanCap) fDacThuNamNay
	--into #tempTable1
	--from ns_skt_mucluc mucluc
	--left join 
	--(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	--join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	--join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	--join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	--where ctct.iNamLamViec = @NamLamViec - 1
	--and donvi.iTrangThai = 1
	--and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	--and ctct.iNamLamViec = @NamLamViec - 1
	--and ctct.iNamNganSach = @NamNganSach
	--and ctct.iID_MaNguonNganSach = @NguonNganSach
	--and ctct.iLoai = @Loai

	--and ct.iNamLamViec = @NamLamViec - 1
	--and ct.iNamNganSach = @NamNganSach
	--and ct.iID_MaNguonNganSach = @NguonNganSach
	--and ct.iLoaiChungTu = @LoaiChungTu

	--and nguoidung.iID_MaNguoiDung = @UserName
	--and nguoidung.iTrangThai = 1

	--) chitiet_before on ((@NamLamViec = 2024 and mucluc.sKyHieuCu = chitiet_before.sKyHieu) or (@NamLamViec <> 2024 and mucluc.sKyHieu = chitiet_before.sKyHieu))
	--left join 
	--(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	--join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	--join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	--join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	--where ctct.iNamLamViec = @NamLamViec
	--and donvi.iTrangThai = 1
	--and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	--and ctct.iNamLamViec = @NamLamViec
	--and ctct.iNamNganSach = @NamNganSach
	--and ctct.iID_MaNguonNganSach = @NguonNganSach
	--and ctct.iLoai = @Loai

	--and ct.iNamLamViec = @NamLamViec
	--and ct.iNamNganSach = @NamNganSach
	--and ct.iID_MaNguonNganSach = @NguonNganSach
	--and ct.iLoaiChungTu = @LoaiChungTu

	--) chitiet_now on mucluc.sKyHieu = chitiet_now.sKyHieu and chitiet_before.iID_MaDonVi = chitiet_now.iID_MaDonVi

	----where @LoaiNhap in (select * from f_split(mucluc.sLoaiNhap))
	--where mucluc.iNamLamViec = @NamLamViec

	--group by mucluc.sKyHieu,
	--mucluc.iID_MLSKT,
	--mucluc.iID_MLSKTCha,
	--mucluc.SL,
	--mucluc.SK,
	--mucluc.sM,
	--mucluc.sNG,
	--mucluc.sMoTa,
	--mucluc.bHangCha
	--order by mucluc.sKyHieu

	--select 
	--mucluc.sKyHieu,
	--mucluc.iID_MLSKT,
	--mucluc.iID_MLSKTCha,
	--mucluc.SL sL,
	--mucluc.SK sK,
	--'' sM,
	--'' sNG,
	----mucluc.sM,
	----mucluc.sNG,
	--case when isnull(chitiet_now.sTenDonVi1, '') = '' and isnull(chitiet_before.sTenDonVi1, '') = '' then mucluc.sMoTa
	--when isnull(chitiet_now.sTenDonVi1, '') = '' then concat('   + ', chitiet_before.sTenDonVi1)
	--else concat('   + ', chitiet_now.sTenDonVi1)
	--end as MoTa,
	--mucluc.bHangCha,
	--case when @LoaiBaoCao = 1 then sum(chitiet_before.fTuChi) / @DonViTinh
	--when @LoaiBaoCao = 2 then sum(chitiet_before.fMuaHangCapHienVat) / @DonViTinh
	--else sum(chitiet_before.fPhanCap) / @DonViTinh end soLieuCot1,

	--case when @LoaiBaoCao = 1 then sum(chitiet_now.fTuChi) / @DonViTinh
	--when @LoaiBaoCao = 2 then sum(chitiet_now.fMuaHangCapHienVat) / @DonViTinh
	--else sum(chitiet_now.fPhanCap) / @DonViTinh end soLieuCot2,

	--sum(chitiet_before.fTuChi) fTuChiNamTruoc,
	--sum(chitiet_now.fTuChi) fTuChiNamNay,
	--sum(chitiet_before.fMuaHangCapHienVat) fMuaHangCapHienVatNamTruoc,
	--sum(chitiet_now.fMuaHangCapHienVat) fMuaHangCapHienVatNamNay,
	--sum(chitiet_before.fPhanCap) fDacThuNamTruoc,
	--sum(chitiet_now.fPhanCap) fDacThuNamNay
	--into #tempTable2
	--from ns_skt_mucluc mucluc
	--left join 
	--(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	--join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	--join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	--join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	--where ctct.iNamLamViec = @NamLamViec - 1
	--and donvi.iTrangThai = 1
	--and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	--and ctct.iNamLamViec = @NamLamViec - 1
	--and ctct.iNamNganSach = @NamNganSach
	--and ctct.iID_MaNguonNganSach = @NguonNganSach
	--and ctct.iLoai = @Loai

	--and ct.iNamLamViec = @NamLamViec - 1
	--and ct.iNamNganSach = @NamNganSach
	--and ct.iID_MaNguonNganSach = @NguonNganSach
	--and ct.iLoaiChungTu = @LoaiChungTu

	--and nguoidung.iID_MaNguoiDung = 'admin'
	--and nguoidung.iTrangThai = 1

	--) chitiet_before on ((@NamLamViec = 2024 and mucluc.sKyHieuCu = chitiet_before.sKyHieu) or (@NamLamViec <> 2024 and mucluc.sKyHieu = chitiet_before.sKyHieu))
	--left join 
	--(select ctct.*, donvi.sTenDonVi sTenDonVi1 from NS_SKT_ChungTuChiTiet ctct 
	--join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra and ctct.iNamLamViec = ct.iNamLamViec
	--join DonVi donvi on ctct.iID_MaDonVi = donvi.iID_MaDonVi and ct.iNamLamViec = donvi.iNamLamViec
	--join NguoiDung_DonVi nguoidung on nguoidung.iID_MaDonVi = donvi.iID_MaDonVi and nguoidung.iNamLamViec = donvi.iNamLamViec
	--where ctct.iNamLamViec = @NamLamViec
	--and donvi.iTrangThai = 1
	--and donvi.iID_MaDonVi in (select * from f_split(@IdDonVi))

	--and ctct.iNamLamViec = @NamLamViec
	--and ctct.iNamNganSach = @NamNganSach
	--and ctct.iID_MaNguonNganSach = @NguonNganSach
	--and ctct.iLoai = @Loai

	--and ct.iNamLamViec = @NamLamViec
	--and ct.iNamNganSach = @NamNganSach
	--and ct.iID_MaNguonNganSach = @NguonNganSach
	--and ct.iLoaiChungTu = @LoaiChungTu

	--) chitiet_now on mucluc.sKyHieu = chitiet_now.sKyHieu and chitiet_before.iID_MaDonVi = chitiet_now.iID_MaDonVi

	----where @LoaiNhap in (select * from f_split(mucluc.sLoaiNhap))
	--where mucluc.iNamLamViec = @NamLamViec

	--group by mucluc.sKyHieu,
	--mucluc.iID_MLSKT,
	--mucluc.iID_MLSKTCha,
	--mucluc.SL,
	--mucluc.SK,
	--mucluc.sM,
	--mucluc.sNG,
	--mucluc.sMoTa,
	--mucluc.bHangCha,
	--chitiet_now.iID_MaDonVi,
	--chitiet_now.sTenDonVi1,
	--chitiet_before.sTenDonVi1
	--order by mucluc.sKyHieu

	if (@KieuBaoCao = 1)
	select * from #tempTable3
	order by sKyHieu, sNG desc, MoTa
	else
	select * from #tempTable3
	union all
	select * from #tempTable4
	order by sKyHieu, sNG desc, MoTa

	drop table #tempsKyHieu
	drop table #tempSKyHieuDonVi
	drop table #tempTable1
	drop table #tempTable2
	drop table #tempTable3
	drop table #tempTable4
	drop table #tempTableMLDV

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



INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'b66bffe9-640b-45c9-ad5f-24a0eb1ed764', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SKT_NhanSoKiemTra_NSDTN_TONGHOP', NULL, N'rptNS_SKT_NhanSoKiemTra_NSDTN_TONGHOP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'TỔNG HỢP PHÂN BỔ SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM', NULL, N'(Kèm theo Quyết định số ........., ngày …/…/…..)', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'88df2561-30c4-4352-b3e7-38af719bf36d', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SKT_NhanSoKiemTra_NSSD', NULL, N'rptNS_SKT_NhanSoKiemTra_NSSD', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO PHÂN BỔ SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM ', NULL, N'(Kèm theo Quyết định số ........., ngày …/…/…..)', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'c9584a04-6b49-417e-b087-6cc158bee2e9', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SKT_NhanSoKiemTra_NSSD_TONGHOP', NULL, N'rptNS_SKT_NhanSoKiemTra_NSSD_TONGHOP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'TỔNG HỢP PHÂN BỔ SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM ', NULL, N'(Kèm theo Quyết định số ........., ngày …/…/…..)', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

INSERT [dbo].[DM_ChuKy] ([Id], [bDanhSach], [ChucDanh1], [ChucDanh1_MoTa], [ChucDanh2], [ChucDanh2_MoTa], [ChucDanh3], [ChucDanh3_MoTa], [ChucDanh4], [ChucDanh4_MoTa], [ChucDanh5], [ChucDanh5_MoTa], [ChucDanh6], [ChucDanh6_MoTa], [DateCreated], [DateModified], [INamLamViec], [iTrangThai], [Id_Code], [Id_Old_Type], [Id_Type], [KyHieu], [Log], [MoTa], [sKinhGuiCQTTBQP], [sKinhGuiKBNN], [sLoai], [Tag], [Ten], [Ten1], [Ten1_MoTa], [Ten2], [Ten2_MoTa], [Ten3], [Ten3_MoTa], [Ten4], [Ten4_MoTa], [Ten5], [Ten5_MoTa], [Ten6], [Ten6_MoTa], [ThuaLenh1], [ThuaLenh1_MoTa], [ThuaLenh2], [ThuaLenh2_MoTa], [ThuaLenh3], [ThuaLenh3_MoTa], [ThuaLenh4], [ThuaLenh4_MoTa], [ThuaLenh5], [ThuaLenh5_MoTa], [ThuaLenh6], [ThuaLenh6_MoTa], [TieuDe1], [TieuDe1_MoTa], [TieuDe2], [TieuDe2_MoTa], [TieuDe3_MoTa], [UserCreator], [UserModifier], [LoaiDVBanHanh1], [LoaiDVBanHanh2], [TenDVBanHanh1], [TenDVBanHanh2], [ThuaUyQuyen1], [ThuaUyQuyen1_MoTa], [ThuaUyQuyen2], [ThuaUyQuyen2_MoTa], [ThuaUyQuyen3], [ThuaUyQuyen3_MoTa]) VALUES (N'a1ebbad7-835a-41c8-b78c-ac185724a8af', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'rptNS_SKT_NhanSoKiemTra_NSDTN', NULL, N'rptNS_SKT_NhanSoKiemTra_NSDTN', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'1', N'BÁO CÁO PHÂN BỔ SỐ KIỂM TRA DỰ TOÁN NGÂN SÁCH NĂM ', NULL, N'(Kèm theo Quyết định số ........., ngày …/…/…..)', NULL, NULL, NULL, NULL, NULL, N'', N'', NULL, NULL, NULL, NULL, NULL, NULL)
GO

/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitietHD4554_update_month]    Script Date: 11/15/2024 8:49:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitietHD4554_update_month]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitietHD4554_update_month]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]    Script Date: 11/15/2024 8:49:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/15/2024 8:49:56 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_qt_chungtu_chitiet_hd4554_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_qt_chungtu_chitiet_hd4554_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/15/2024 8:49:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_qt_chungtu_chitiet_hd4554_all]
	@chungTuIds nvarchar(max),
	@namLamViec int,
	@namNganSach int,
	@nguonNganSach int,
	@lns nvarchar(max),
	@donVi nvarchar(max),
	@dvt int,
	@thangQuyLoai int,
	@thangQuy nvarchar(20),

	@userName nvarchar(50)
AS
BEGIN


		select 
			sLNS, 
			ctct.iID_MaDonVi as IIDMaDonVi,
			Sum(fSoTien) as fSoTien ,
			dv.sTenDonVi,
			--ctct.iID_TN_QTChungTu,
			ctct.sM,
			ctct.sL,
			ctct.sK,
			ctct.sM,
			ctct.sTM,
			ctct.sTNG,
			ctct.sTNG1,
			ctct.sTNG2,
			ctct.sTNG3,
			ctct.sTTM,
			ctct.sXauNoiMa
		from TN_QuyetToan_ChungTuChiTiet_HD4554 ctct
		left join DonVi dv on ctct.iID_MaDonVi=dv.iID_MaDonVi
		where ctct.iNamLamViec = @namLamViec 
		and ctct.iNamNganSach = @namNganSach 
		and ctct.iNguonNganSach = @nguonNganSach
		and ctct.iID_MaDonVi in (select * from f_split(@donVi))
		and iID_TN_QTChungTu in  (select * from f_split(@chungTuIds))
		and dv.iNamLamViec=@namLamViec
		and dv.iTrangThai=1
		and ctct.iThangQuyLoai =@thangQuyLoai
		and ctct.iThangQuy in (select * from splitstring(@thangQuy))
		group by sLNS,ctct.iID_MaDonVi,dv.sTenDonVi,ctct.sM,
			ctct.sL,
			ctct.sK,
			ctct.sM,
			ctct.sTM,
			ctct.sTNG,
			ctct.sTNG1,
			ctct.sTNG2,
			ctct.sTNG3,
			ctct.sTTM,
			ctct.sXauNoiMa
END
;
;
;
;
;
;
;
;
;
;
;
;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]    Script Date: 11/15/2024 8:49:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_qt_chungtu_chitiet_HD4554_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfBudget int,
	@BudgetSource int,
	@YearOfWork int,
	@Type nvarchar(10),
	@QuarterMonthType int,
	@QuarterMonth int,
	@AgencyId nvarchar(10),
	@UserName nvarchar(100)
AS
BEGIN

INSERT INTO [dbo].[TN_QuyetToan_ChungTuChiTiet_HD4554]
           ([Id]
           ,[bHangCha]
           ,[dNgaySua]
           ,[dNgayTao]
           ,[fSoTien]
           ,[iID_MaDonVi]
           ,[iID_TN_QTChungTu]
           ,[iNamLamViec]
           ,[iNamNganSach]
           ,[iNguonNganSach]
           ,[iThangQuy]
           ,[iThangQuyLoai]
           ,[sGhiChu]
           ,[sK]
           ,[sL]
           ,[sLNS]
           ,[sM]
           ,[sNG]
           ,[sNguoiSua]
           ,[sNguoiTao]
           ,[sTM]
           ,[sTNG]
           ,[sTNG1]
           ,[sTNG2]
           ,[sTNG3]
           ,[sTTM]
           ,[sXauNoiMa])
	 SELECT
          NEWID(),
          mlns.bHangCha,
		  null,
		  GETDATE(),
		  SUM(fSoTien),
		  @AgencyId,
          @VoucherId,
          @YearOfWork,
		  @YearOfBudget,
          @BudgetSource,
          @QuarterMonth,
          @QuarterMonthType,
          null,
           mlns.sK,
           mlns.sL,
           mlns.sLNS,
           mlns.sM,
           mlns.sNG, 
           null,
           @UserName,
           mlns.sTM, 
           mlns.sTNG, 
           mlns.sTNG1, 
           mlns.sTNG2, 
           mlns.sTNG3,
           
		   mlns.sTTM, 
           mlns.sXauNoiMa

	FROM TN_QuyetToan_ChungTuChiTiet_HD4554 ctct
	INNER JOIN NS_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa AND mlns.iNamLamViec = ctct.iNamLamViec
	INNER JOIN TN_QuyetToan_ChungTu_HD4554 ct ON ctct.iID_TN_QTChungTu = ct.Id
	WHERE ct.Id IN (SELECT * FROM f_split(@VoucherIds)) 
	GROUP BY mlns.sXauNoiMa, mlns.sLNS, mlns.sL, mlns.sK, mlns.sM, mlns.sTM, mlns.sTTM, mlns.sNG, mlns.sTNG, mlns.sTNG1, mlns.sTNG2, mlns.sTNG3, mlns.bHangCha;

	-- Danh dau chung tu da tong hop
	UPDATE TN_QuyetToan_ChungTu_HD4554 SET bDaTongHop = 1 
	WHERE Id in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_qt_chungtu_chitietHD4554_update_month]    Script Date: 11/15/2024 8:49:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[sp_qt_chungtu_chitietHD4554_update_month]
	@VoucherId nvarchar(100),
	@Thang int,
	@LoaiThang int,
	@UserName nvarchar(100)
AS
BEGIN
	UPDATE
		TN_QuyetToan_ChungTuChiTiet_HD4554
	SET
		iThangQuy = @Thang,
		iThangQuyLoai = @LoaiThang,
		sNguoiSua = @UserName,
		dNgaySua = GetDate()
	WHERE iID_TN_QTChungTu = @VoucherId
END
;
;
;
GO
