/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/19/2024 4:35:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay]    Script Date: 11/19/2024 4:35:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay]
GO
/****** Object:  StoredProcedure [dbo].[sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay]    Script Date: 11/19/2024 4:35:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_skt_nhap_so_ktra_loainguon_so_sanh_nam_truoc_nam_nay]
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
	and ct.iLoaiNguonNganSach=@NganSach

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
		and ct.iLoaiNguonNganSach=@NganSach

		and nguoidung.iID_MaNguoiDung = @UserName
		and nguoidung.iTrangThai = 1

		and mucluc_now.iNamLamViec = @NamLamViec 
		and mucluc_now.bHangCha=0
		group by mucluc_now.sKyHieu, donvi.iID_MaDonVi,donvi.sTenDonVi


	select * into #tempsKyHieu from 
	(
		Select mucluc.sKyHieu  from ns_skt_mucluc mucluc
		inner join  #tempTable1 tbl1 on mucluc.sKyHieuCu=tbl1.sKyHieu
		where  mucluc.iNamLamViec=@NamLamViec
		and  (isnull(fTuChi,0)!=0 or isnull(fPhanCap,0)!=0 or ISNULL(fMuaHangCapHienVat,0) !=0)
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
/****** Object:  StoredProcedure [dbo].[sp_skt_so_sanh_nam_truoc_nam_nay]    Script Date: 11/19/2024 4:35:08 PM ******/
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
	--and ct.iLoaiNguonNganSach=@NganSach

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
		--and ct.iLoaiNguonNganSach=@NganSach

		and nguoidung.iID_MaNguoiDung = @UserName
		and nguoidung.iTrangThai = 1

		and mucluc_now.iNamLamViec = @NamLamViec 
		and mucluc_now.bHangCha=0
		group by mucluc_now.sKyHieu, donvi.iID_MaDonVi,donvi.sTenDonVi



	select * into #tempsKyHieu from 
	(
		Select mucluc.sKyHieu  from ns_skt_mucluc mucluc
		inner join  #tempTable1 tbl1 on mucluc.sKyHieuCu=tbl1.sKyHieu
		where  mucluc.iNamLamViec=@NamLamViec
		and  (isnull(fTuChi,0)!=0 or isnull(fPhanCap,0)!=0 or ISNULL(fMuaHangCapHienVat,0) !=0)
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/19/2024 5:31:25 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]    Script Date: 11/19/2024 5:31:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_qt_chungtu_chitiet_hd4554_all]
	@chungTuIds nvarchar(max),
	@namLamViec int,
	@namNganSach int,
	@nguonNganSach int,
	@lns nvarchar(max),
	@donVi nvarchar(max),
	@dvt int,
	@thangQuyLoai int,
	@thangQuy nvarchar(50),

	@userName nvarchar(50)
AS
BEGIN


		select 
			sLNS, 
			ctct.iID_MaDonVi as IIDMaDonVi,
			round(Sum(fSoTien)/@dvt,0) as fSoTien ,
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
		and ctct.iThangQuy in (select * from f_split(@thangQuy))
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
;
GO
