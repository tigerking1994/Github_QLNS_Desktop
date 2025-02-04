/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_thamdinhquyettoan_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_chung_tu_don_vi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 2/22/2024 4:24:54 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_export_kehoachcaptamung_kcbbhyt]
@IdCsYTe NVARCHAR(MAX),
@ILoai int,
@IQuy int,
@NamLamViec int,
@UserName NVARCHAR(100),
@Donvitinh int,
@Lns nvarchar(1000)
As
begin
	select 
			row_number() over (order by cptu_ct.iID_MaCoSoYTe) as sTT,
			ROUND(sum(ISNULL(cptu_ct.fQTQuyTruoc,0)),0)/@Donvitinh as fQTQuyTruoc, 
			ROUND(sum(ISNULL(cptu_ct.fTamUngQuyNay,0)),0)/@Donvitinh as fTamUngQuyNay, 
			--ROUND(sum(ISNULL(cp_bs.fSoCapBoSung,0) + ISNULL(cp_bs.fDaCapUng,0)),0)/@Donvitinh  as  fLuyKeCapDenCuoiQuy,
			ROUND(sum(ISNULL(cptu_ct.fLuyKeCapDenCuoiQuy,0)),0)/@Donvitinh as fLuyKeCapDenCuoiQuy,
			cptu_ct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapTamUng_KCB_BHYT_ChiTiet as cptu_ct
		inner join BH_CP_CapTamUng_KCB_BHYT as cptu on cptu_ct.iID_BH_CP_CapTamUng_KCB_BHYT = cptu.iID_BH_CP_CapTamUng_KCB_BHYT
		left join (
					select sum(isnull(ct.fSoCapBoSung,0)) as fSoCapBoSung, sum(isnull(ct.fDaCapUng,0)) as fDaCapUng,  iID_MLNS, iID_MaCoSoYTe
					from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ct
					inner join   BH_CP_CapBoSung_KCB_BHYT bs on ct.iID_CTCapPhatBS = bs.iID_CTCapPhatBS
					where bs.iQuy = @iQuy - 1
					group by iID_MLNS, iID_MaCoSoYTe) as cp_bs on cp_bs.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe and cptu_ct.iID_MLNS = cp_bs.iID_MLNS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = cptu_ct.iID_MaCoSoYTe
		where cptu_ct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			--and cptu.bIsTongHop <> 1 and cptu.sDSSoChungTuTongHop is null
			and cptu.iQuy = @IQuy
			and cptu_ct.sLNS In (SELECT * FROM f_split(@Lns))
			AND csyt.iNamLamViec = @NamLamViec
			and cptu.iNamLamViec = @NamLamViec
			--and ((@ILoai = 1 and cptu_ct.sLNS = '9050001') or (@ILoai = 2 and cptu_ct.sLNS = '9050002'))
		group by cptu_ct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
		
end
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi]
	@YearOfWork int,
	@LoaiTongHop int,
	@DaTongHop bit,
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		--AND qtt.bDaTongHop = @DaTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iQuyNamLoai = @LoaiQuyNam
		AND qtt.iLoaiTongHop = @LoaiTongHop
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_chung_tu_don_vi_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100),
	@QuyNam int,
	@LoaiQuyNam int
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
		qtt.iID_QTT_BHXH_ChungTu,
		qtt.sSoChungTu,
		qtt.iID_MaDonVi IIDMaDonVi,
		donvi.sTenDonVi,
		qtt.iQuyNam,
		qtt.iLoaiTongHop,
		qtt.bIsKhoa
	INTO #tblChungTuQTT
	FROM BH_QTT_BHXH_ChungTu qtt
		LEFT JOIN DonVi donvi
		ON qtt.iID_MaDonVi = donvi.iID_MaDonVi
	WHERE donvi.iNamLamViec = @YearOfWork
		AND qtt.iNamLamViec = @YearOfWork
		AND qtt.iLoaiTongHop = @LoaiTongHop
		AND qtt.iQuyNam = @QuyNam
		AND qtt.iQuyNamLoai = @LoaiQuyNam

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTuQTT ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTuQTT;
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam] 
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh FTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and sL is not null and sL <> '' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where --ct.iNamLamViec = @NamLamViec
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			  ,ctct.iID_QTT_BHXH_ChungTu
			  ,ctct.iQSBQNam
			  ,ctct.fLuongChinh
			  ,ctct.fPCChucVu
			  ,ctct.fPCTNNghe
			  ,ctct.fPCTNVuotKhung
			  ,ctct.fNghiOm
			  ,ctct.fHSBL
			  ,ctct.fTongQTLN
			  ,ctct.fDuToan
			  ,ctct.fDaQuyetToan
			  ,ctct.fConLai
			  ,ctct.fThu_BHXH_NLD
			  ,ctct.fThu_BHXH_NSD
			  ,ctct.fTongSoPhaiThuBHXH
			  ,ctct.fThu_BHYT_NLD
			  ,ctct.fThu_BHYT_NSD
			  ,ctct.fTongSoPhaiThuBHYT
			  ,ctct.fThu_BHTN_NLD
			  ,ctct.fThu_BHTN_NSD
			  ,ctct.fTongSoPhaiThuBHTN
			  ,ctct.fTongCong
			  ,ctct.sGhiChu
			  ,ctct.iID_MLNS
			  ,ctct.iID_MLNS_Cha
			  ,ctct.sXauNoiMa
			  ,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 1
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_tong_hop_quy]
	@NamLamViec int,
	@IdDonVi nvarchar(50),
	@Donvitinh int,
	@IsTongHop bit,
	@IQuy int,
	@ILoaiQuy int
AS
BEGIN
	select
		chungtudonvi.iID_QTT_BHXH_ChungTu_ChiTiet,
		mlns.iID_MLNS,
		mlns.sMoTa,
		mlns.iID_MLNS_Cha,
		mlns.bHangCha,
		mlns.sLNS,
		mlns.sL,
		mlns.sK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sTNG,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		@NamLamViec iNamLamViec,
		chungtudonvi.iQSBQNam,
		chungtudonvi.fLuongChinh/@Donvitinh fLuongChinh,
		chungtudonvi.fPCChucVu/@Donvitinh fPCChucVu,
		chungtudonvi.fPCTNNghe/@Donvitinh fPCTNNghe,
		chungtudonvi.fPCTNVuotKhung/@Donvitinh fPCTNVuotKhung,
		chungtudonvi.fNghiOm/@Donvitinh fNghiOm,
		chungtudonvi.fHSBL/@Donvitinh fHSBL,
		(chungtudonvi.fLuongChinh + chungtudonvi.fPCChucVu + chungtudonvi.fPCTNNghe + chungtudonvi.fPCTNVuotKhung + chungtudonvi.fNghiOm + chungtudonvi.fHSBL)/@Donvitinh fTongQTLN,
		chungtudonvi.fDuToan,
		chungtudonvi.fDaQuyetToan,
		chungtudonvi.fConLai,
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH,
		chungtudonvi.fThu_BHYT_NLD/@Donvitinh fThu_BHYT_NLD,
		chungtudonvi.fThu_BHYT_NSD/@Donvitinh fThu_BHYT_NSD,
		(chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD)/@Donvitinh fTongSoPhaiThuBHYT,
		chungtudonvi.fThu_BHTN_NLD/@Donvitinh fThu_BHTN_NLD,
		chungtudonvi.fThu_BHTN_NSD/@Donvitinh fThu_BHTN_NSD,
		(chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongSoPhaiThuBHTN,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHTN_NLD) fTongNLD,
		(chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NSD) fTongNSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD + chungtudonvi.fThu_BHYT_NLD + chungtudonvi.fThu_BHYT_NSD + chungtudonvi.fThu_BHTN_NLD + chungtudonvi.fThu_BHTN_NSD)/@Donvitinh fTongCong
		from
		(select
			BH_DM_MucLucNganSach.iID_MLNS,
			BH_DM_MucLucNganSach.iID_MLNS_Cha,
			BH_DM_MucLucNganSach.bHangCha,
			BH_DM_MucLucNganSach.sLNS,
			BH_DM_MucLucNganSach.sL,
			BH_DM_MucLucNganSach.sK,
			BH_DM_MucLucNganSach.sM,
			BH_DM_MucLucNganSach.sTM,
			BH_DM_MucLucNganSach.sTTM,
			BH_DM_MucLucNganSach.sNG,
			BH_DM_MucLucNganSach.sTNG,
			BH_DM_MucLucNganSach.sXauNoiMa,
			BH_DM_MucLucNganSach.sMoTa
		from BH_DM_MucLucNganSach 
		where sLNS like '902%'
		and iNamLamViec = @NamLamViec) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.iID_QTT_BHXH_ChungTu_ChiTiet
			,ctct.iID_QTT_BHXH_ChungTu
			,ctct.iQSBQNam
			,ctct.fLuongChinh
			,ctct.fPCChucVu
			,ctct.fPCTNNghe
			,ctct.fPCTNVuotKhung
			,ctct.fNghiOm
			,ctct.fHSBL
			,ctct.fTongQTLN
			,ctct.fDuToan
			,ctct.fDaQuyetToan
			,ctct.fConLai
			,ctct.fThu_BHXH_NLD
			,ctct.fThu_BHXH_NSD
			,ctct.fTongSoPhaiThuBHXH
			,ctct.fThu_BHYT_NLD
			,ctct.fThu_BHYT_NSD
			,ctct.fTongSoPhaiThuBHYT
			,ctct.fThu_BHTN_NLD
			,ctct.fThu_BHTN_NSD
			,ctct.fTongSoPhaiThuBHTN
			,ctct.fTongCong
			,ctct.sGhiChu
			,ctct.iID_MLNS
			,ctct.iID_MLNS_Cha
			,ctct.sXauNoiMa
			,ctct.sLNS
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iQuyNamLoai = @ILoaiQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = 2
			--and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		order by mlns.sXauNoiMa
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_thamdinhquyettoan_chitiet]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_thamdinhquyettoan_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IdDonVi nvarchar(max)
AS
BEGIN
	
	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iMa

	SELECT 
		iID,
		ctct.iID_BH_TDQT_ChungTuChiTiet,
		dmtdqp.iMa,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
		ctct.fSoThamDinh,
		ctct.fQuanNhan,
		ctct.fCNVLDHD
	INTO #dmtdqtResult
	FROM #dmtdqt dmtdqp
	
	LEFT JOIN
	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
			AND ctct.iNamLamViec = @INamLamViec - 1
			AND ct.iQuyNam = @INamLamViec - 1
			AND ct.bIsKhoa = 1)) fSoBaoCao
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bKhoa = 1

	UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	) AS temp ON temp.iMa = dmtdqp.iMa

	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct 
	ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_BH_TDQT_ChungTu = @IdChungTu AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi

	--ORDER BY iMa

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
			AND ctct.iNamLamViec = @INamLamViec - 1
			AND ct.iQuyNam = @INamLamViec - 1
			AND ct.bIsKhoa = 1)) fSoBaoCao
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1 

	UNION

	SELECT 221 iMa, (ISNULL(SUM(fThu_BHYT_NLD), 0) + ISNULL(SUM(fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION 

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	--Result
	SELECT * FROM #dmtdqtResult
	ORDER BY iMa;

	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinh_du_toan_chuyen_nam_sau]
	@INamLamViec int,
	@IdDonVi nvarchar(max),
	@DonViTinh int
AS
BEGIN
	
	select * into #tbl_dtcns 
	from BH_ThamDinhQuyetToan_ChungTuChiTiet 
	where iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi)) 
		and iNamLamViec = @INamLamViec
		and iMa in (254, 225, 240)

	select
		2 iLoai,
		row_number() over (order by dtcns.iID_MaDonVi) SSTT,
		dtcns.iID_MaDonVi,
		dv.sTenDonVi,
		isnull(sum(kpql.fSoThamDinh), 0) fKinhPhiQL,
		isnull(sum(kpkcbqy.fSoThamDinh), 0) fKinhPhiKCBQuanY,
		isnull(sum(kpkcbqn.fSoThamDinh), 0) fKinhPhiKCBQuanNhan,
		(isnull(sum(kpql.fSoThamDinh), 0) + isnull(sum(kpkcbqy.fSoThamDinh), 0) + isnull(sum(kpkcbqn.fSoThamDinh), 0)) fTongCong
	from #tbl_dtcns dtcns
	left join #tbl_dtcns kpql on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpql.iID_BH_TDQT_ChungTuChiTiet and kpql.iMa = 254
	left join #tbl_dtcns kpkcbqy on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpkcbqy.iID_BH_TDQT_ChungTuChiTiet and kpkcbqy.iMa = 225
	left join #tbl_dtcns kpkcbqn on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpkcbqn.iID_BH_TDQT_ChungTuChiTiet and kpkcbqn.iMa = 240
	left join DonVi dv on dtcns.iID_MaDonVi = dv.iID_MaDonVi
	where dv.iNamLamViec = @INamLamViec
		and dv.iTrangThai = 1
		and dv.iKhoi = 2 -- Khoi du toan
	group by 
		dtcns.iID_MaDonVi,
		dv.sTenDonVi

	union

	select
		1 iLoai,
		row_number() over (order by dtcns.iID_MaDonVi) SSTT,
		dtcns.iID_MaDonVi,
		dv.sTenDonVi,
		isnull(sum(kpql.fSoThamDinh), 0) fKinhPhiQL,
		isnull(sum(kpkcbqy.fSoThamDinh), 0) fKinhPhiKCBQuanY,
		isnull(sum(kpkcbqn.fSoThamDinh), 0) fKinhPhiKCBQuanNhan,
		(isnull(sum(kpql.fSoThamDinh), 0) + isnull(sum(kpkcbqy.fSoThamDinh), 0) + isnull(sum(kpkcbqn.fSoThamDinh), 0)) fTongCong
	from #tbl_dtcns dtcns
	left join #tbl_dtcns kpql on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpql.iID_BH_TDQT_ChungTuChiTiet and kpql.iMa = 254
	left join #tbl_dtcns kpkcbqy on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpkcbqy.iID_BH_TDQT_ChungTuChiTiet and kpkcbqy.iMa = 225
	left join #tbl_dtcns kpkcbqn on dtcns.iID_BH_TDQT_ChungTuChiTiet = kpkcbqn.iID_BH_TDQT_ChungTuChiTiet and kpkcbqn.iMa = 240
	left join DonVi dv on dtcns.iID_MaDonVi = dv.iID_MaDonVi
	where dv.iNamLamViec = @INamLamViec
		and dv.iTrangThai = 1
		and dv.iKhoi = 1 -- Khoi hach toan
	group by 
		dtcns.iID_MaDonVi,
		dv.sTenDonVi


	drop table #tbl_dtcns;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bh_thamdinhquyettoan_chitiet]
@INamLamViec int,
@IdDonVi nvarchar(max),
@DonViTinh int
AS
BEGIN

	declare @LoaiDonVi nvarchar(50) = (select iLoai from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi = @IdDonVi);

	SELECT * INTO #dmtdqt
	FROM BH_DM_ThamDinhQuyetToan WHERE iNamLamViec = @INamLamViec AND iTrangThai = 1
	ORDER BY iMa
	SELECT 
		iID,
		iMa,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		iTrangThai,
		iNamLamViec,
		sNguoiTao,
		sNguoiSua,
		SUM(fSoBaoCao) fSoBaoCao,
		SUM(fSoThamDinh) fSoThamDinh,
		SUM(fQuanNhan) fQuanNhan,
		SUM(fCNVLDHD) fCNVLDHD
	INTO #dmtdqtResult
	FROM
	(SELECT 
		iID,
		dmtdqp.iMa,
		iMaCha,
		sSTT,
		sNoiDung,
		sXauNoiMa,
		iKieuChu,
		dmtdqp.iTrangThai,
		dmtdqp.iNamLamViec,
		dmtdqp.sNguoiTao, dmtdqp.sNguoiSua,
		CASE WHEN ctct.fSoBaoCao is null or ctct.fSoBaoCao = 0 THEN temp.fSoBaoCao
			 ELSE ctct.fSoBaoCao
		END AS fSoBaoCao,
		ctct.fSoThamDinh,
		ctct.fQuanNhan,
		ctct.fCNVLDHD
	FROM #dmtdqt dmtdqp
	
	LEFT JOIN
	(
	SELECT 7 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 8 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 9 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 10 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 12 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 13 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 14 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 15 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 19 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 20 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 21 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1


	UNION

	SELECT 22 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 24 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 25 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 26 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 27 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 29 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 30 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 34 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 35 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 36 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 37 iMa, SUM(ctct.fThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 39 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 40 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 41 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 42 iMa, SUM(ctct.fTruyThu_BHXH_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 46 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 47 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 48 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 49 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 51 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 52 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 53 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 54 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 56 iMa, SUM(ctct.fThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 57 iMa, SUM(ctct.fTruyThu_BHXH_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 70 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 71 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 73 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 74 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002', '9020001-010-011-0002-0000', '9020001-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 77 iMa, SUM(ctct.fThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 78 iMa, SUM(ctct.fTruyThu_BHTN_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 80 iMa, SUM(ctct.fThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	UNION

	SELECT 81 iMa, SUM(ctct.fTruyThu_BHTN_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002', '9020002-010-011-0002-0000', '9020002-010-011-0002-0001')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 95 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 96 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 98 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 99 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 102 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 103 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 105 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 106 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 110 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 111 iMa, SUM(ctct.fThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 113 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 114 iMa, SUM(ctct.fTruyThu_BHYT_NLD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 17 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 118 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 120 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0000'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 121 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0002-0001'
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 135 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 136 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020001-010-011-0001-0000', '9020001-010-011-0001-0001', '9020001-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 133 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0000'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 134 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020001-010-011-0001-0001'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 138 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 139 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 140 iMa, SUM(ctct.fThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa = '9020002-010-011-0001-0002'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 141 iMa, SUM(ctct.fTruyThu_BHYT_NSD) fSoBaoCao FROM BH_QTT_BHXH_CTCT_GiaiThich ctct
	LEFT JOIN BH_QTT_BHXH_ChungTu ct 
	ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
	WHERE sXauNoiMa IN ('9020002-010-011-0001-0000', '9020002-010-011-0001-0001', '9020002-010-011-0001-0002')
	AND iLoaiGiaiThich = 2
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 151 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030001-010-011-0001%' OR sXauNoiMa LIKE '9030001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 155 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa LIKE '9030002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 159 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 163 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030003'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 167 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030006'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 171 iMa, SUM(ctct.fSoPhaiThu) fSoBaoCao FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
	ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
	WHERE sXauNoiMa = '9030004'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 180 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	--WHERE (ctct.sXauNoiMa LIKE '9010001%' OR ctct.sXauNoiMa LIKE '9010002%')
	WHERE (ctct.sXauNoiMa LIKE '901%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 184 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 185 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 186 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 187 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 188 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 189 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 190 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 193 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 194 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 195 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 196 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 197 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0005%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 198 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0006%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 199 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010002-010-011-0007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 200 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0008%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 183 iMa, SUM(ctct.fTongTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
	ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
	WHERE sXauNoiMa LIKE '9010001-010-011-0001%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 207 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa like '9050001-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 208 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 209 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 213 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(fTien_ThucChi), 0) FROM BH_QTC_Nam_KPK_ChiTiet WHERE sXauNoiMa LIKE '9050002-010%' AND iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND iNamLamViec = @INamLamViec - 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 214 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa like '9050002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 215 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa like '9050002%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 220 iMa, 0.1 * (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD), 0) + ISNULL(SUM(ctct.fThu_BHYT_NSD), 0)
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
			AND ctct.iNamLamViec = @INamLamViec - 1
			AND ct.iQuyNam = @INamLamViec - 1
			AND ct.bIsKhoa = 1)) fSoBaoCao
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bKhoa = 1

	UNION

	SELECT 221 iMa, (ISNULL(SUM(ctct.fBHYT_NLD), 0) + ISNULL(SUM(ctct.fBHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	JOIN BH_DTT_BHXH_PhanBo_ChungTu ct on ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bKhoa = 1

	UNION

	SELECT 222 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 223 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
	ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 228 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 230 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 231 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010009-010%' 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct on ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN  BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 237 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 238 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010006%' OR sXauNoiMa LIKE '9010007%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 242 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010010%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 243 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KPK_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KPK ct 
	ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
	WHERE sXauNoiMa LIKE '9010010-010%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	JOIN BH_DTC_PhanBoDuToanChi ct on ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	LEFT JOIN BH_DTC_PhanBoDuToanChi ct 
	ON ct.ID = ctct.iID_DTC_PhanBoDuToanChi
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 250 iMa, ISNULL(SUM(ctct.fTienKeHoachCap), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct on ID_QTC_Nam_KinhPhiQuanLy = ID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec - 1
	AND ct.bIsKhoa = 1

	UNION

	SELECT 251 iMa, SUM(ctct.fTienKeHoachCap) fSoBaoCao FROM BH_CP_ChungTu_ChiTiet ctct
	LEFT JOIN BH_CP_ChungTu ct 
	ON ct.iID_CP_ChungTu = ctct.iID_CP_ChungTu
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.sID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ct.iNamChungTu = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 252 iMa, SUM(ctct.fTien_ThucChi) fSoBaoCao FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
	LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
	ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
	WHERE sXauNoiMa LIKE '9010003%'
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	) AS temp ON temp.iMa = dmtdqp.iMa

	LEFT JOIN BH_ThamDinhQuyetToan_ChungTuChiTiet ctct ON dmtdqp.iMa = ctct.iMa 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec
	LEFT JOIN BH_ThamDinhQuyetToan_ChungTu ct 
	ON ctct.iID_BH_TDQT_ChungTu = ct.iID_BH_TDQT_ChungTu 
	AND ct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ct.iNamLamViec = @INamLamViec
	LEFT JOIN (SELECT * from DonVi WHERE iNamLamViec = 2023 AND iTrangThai = 1) dv ON dv.iID_MaDonVi = ctct.iID_MaDonVi) r
	GROUP BY 
	r.iID,
	r.iMa,
	r.iMaCha,
	r.sSTT,
	r.sNoiDung,
	r.sXauNoiMa,
	r.iKieuChu,
	r.iTrangThai,
	r.iNamLamViec,
	r.sNguoiTao,
	r.sNguoiSua

	--Lấy dữ liệu chứng từ đơn vị cha
	SELECT dvc.iMa, dvc.fSoBaoCao INTO #dmtdqtCha FROM
	(
	SELECT 178 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010001%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 179 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010002%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 218 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010004%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 220 iMa, 0.1 * (ISNULL(SUM(fThu_BHYT_NLD + fThu_BHYT_NSD), 0) - (SELECT ISNULL(SUM(ctct.fThu_BHYT_NLD + ctct.fThu_BHYT_NSD), 0) 
		FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
		JOIN BH_QTT_BHXH_ChungTu ct ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
		WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%') 
			AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) 
			AND ctct.iNamLamViec = @INamLamViec - 1
			AND ct.iQuyNam = @INamLamViec - 1
			AND ct.bIsKhoa = 1)) fSoBaoCao
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa like '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1 

	UNION

	SELECT 221 iMa, (ISNULL(SUM(fThu_BHYT_NLD), 0) + ISNULL(SUM(fThu_BHYT_NSD), 0)) * 0.1 fSoBaoCao
	FROM BH_DTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DTT_BHXH_ChungTu ct ON ct.iID_DTT_BHXH = ctct.iID_DTT_BHXH
	WHERE (ctct.sXauNoiMa LIKE '9020001-010-011-0001%' OR ctct.sXauNoiMa LIKE '9020002-010-011-0001%')
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION 

	SELECT 228 iMa, ISNULL(SUM(fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010009-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 229 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	LEFT JOIN BH_DTC_DuToanChiTrenGiao ct 
	ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE 
	sXauNoiMa LIKE '9010009%' 
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1

	UNION

	SELECT 235 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KPK_ChiTiet ctct
		JOIN BH_QTC_Nam_KPK ct ON ct.ID_QTC_Nam_KPK = ctct.iID_QTC_Nam_KPK
		WHERE ctct.sXauNoiMa LIKE '9010006-010%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 AND ct.bIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 236 iMa, SUM(ctct.fTienTuChi) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct 
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010006%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 247 iMa, ISNULL(SUM(ctct.fTienTuChi), 0) - (SELECT ISNULL(SUM(ctct.fTien_ThucChi), 0) 
		FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
		JOIN BH_QTC_Nam_KinhPhiQuanLy ct ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
		WHERE ctct.sXauNoiMa LIKE '9010003%' AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi)) AND ctct.iNamLamViec = @INamLamViec - 1 and sM is not null and sM <> '' AND ct.BIsKhoa = 1) fSoBaoCao 
	FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec - 1
	AND ct.bIsKhoa = 1
	
	UNION

	SELECT 248 iMa, SUM(ctct.fTienTuChi) fSoBaoCao FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
	JOIN BH_DTC_DuToanChiTrenGiao ct ON ct.ID = ctct.iID_DTC_DuToanChiTrenGiao
	WHERE ctct.sXauNoiMa LIKE '9010003%'
	AND ctct.iID_MaDonVi IN (SELECT * FROM f_split(@IdDonVi))
	AND ctct.iNamLamViec = @INamLamViec
	AND ct.bIsKhoa = 1
	) dvc

	IF @LoaiDonVi = 0
		UPDATE dmtdqtResult
		SET dmtdqtResult.fSoBaoCao = dmtdqtCha.fSoBaoCao
		FROM #dmtdqtResult AS dmtdqtResult
		JOIN #dmtdqtCha AS dmtdqtCha ON dmtdqtResult.iMa = dmtdqtCha.iMa;

	SELECT dvcl.iMa, dvcl.fSoBaoCao INTO #dmtdqtCal FROM
	(
	SELECT 61 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 60) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) else 0 end as fSoBaoCao
	UNION
	SELECT 64 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 63) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 3) else 0 end as fSoBaoCao
	UNION
	SELECT 65 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 3) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 62)) as fSoBaoCao
	UNION
	SELECT 85 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 84) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) else 0 end as fSoBaoCao
	UNION
	SELECT 88 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 87) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 67) else 0 end as fSoBaoCao
	UNION
	SELECT 89 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 67) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 86)) as fSoBaoCao
	UNION
	SELECT 125 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 124) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) else 0 end as fSoBaoCao
	UNION
	SELECT 128 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 127) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 91) else 0 end as fSoBaoCao
	UNION
	SELECT 129 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 91) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 126)) as fSoBaoCao
	UNION
	SELECT 145 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 144) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) else 0 end as fSoBaoCao
	UNION
	SELECT 148 iMa, case when (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) <> 0 then (select fSoBaoCao FROM #dmtdqtResult where iMa = 147) / (select fSoBaoCao FROM #dmtdqtResult where iMa = 131) else 0 end as fSoBaoCao
	UNION
	SELECT 149 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 131) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 146)) as fSoBaoCao
	UNION
	SELECT 153 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 151) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 152)) as fSoBaoCao
	UNION
	SELECT 157 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 155) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 156)) as fSoBaoCao
	UNION
	SELECT 161 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 159) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 160)) as fSoBaoCao
	UNION
	SELECT 165 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 163) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 164)) as fSoBaoCao
	UNION
	SELECT 169 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 167) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 168)) as fSoBaoCao
	UNION
	SELECT 173 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 171) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 172)) as fSoBaoCao
	UNION
	SELECT 202 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 180) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 181)) as fSoBaoCao
	UNION
	SELECT 210 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 209) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 206)) as fSoBaoCao
	UNION
	SELECT 216 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 212) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 215)) as fSoBaoCao
	UNION
	SELECT 224 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 223) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 222)) as fSoBaoCao
	UNION
	SELECT 225 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 218) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 223)) as fSoBaoCao
	UNION
	SELECT 232 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 230) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 231)) as fSoBaoCao
	UNION
	SELECT 239 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 237) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 238)) as fSoBaoCao
	UNION
	SELECT 240 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 234) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 238)) as fSoBaoCao
	UNION
	SELECT 244 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 242) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 243)) as fSoBaoCao
	UNION
	SELECT 253 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 249) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 252)) as fSoBaoCao
	UNION
	SELECT 254 iMa, ((select fSoBaoCao FROM #dmtdqtResult where iMa = 246) - (select fSoBaoCao FROM #dmtdqtResult where iMa = 252)) as fSoBaoCao
	) dvcl

	--Result
	SELECT dmtdqtResult.iID, dmtdqtResult.iMa, dmtdqtResult.iMaCha, dmtdqtResult.sSTT, dmtdqtResult.sNoiDung, dmtdqtResult.sXauNoiMa, dmtdqtResult.iKieuChu, dmtdqtResult.iTrangThai,
	dmtdqtResult.iNamLamViec, dmtdqtResult.sNguoiTao, dmtdqtResult.sNguoiSua,
	CASE WHEN dmtdqtResult.fSoBaoCao is null or dmtdqtResult.fSoBaoCao = 0 THEN dmtdqtCal.fSoBaoCao/@DonViTinh
		ELSE dmtdqtResult.fSoBaoCao/@DonViTinh
	END AS fSoBaoCao,
	dmtdqtResult.fSoThamDinh/@DonViTinh fSoThamDinh,
	dmtdqtResult.fQuanNhan/@DonViTinh fQuanNhan,
	dmtdqtResult.fCNVLDHD/@DonViTinh fCNVLDHD
	FROM #dmtdqtResult dmtdqtResult
	LEFT JOIN #dmtdqtCal dmtdqtCal ON dmtdqtResult.iMa = dmtdqtCal.iMa
	ORDER BY iMa
	
	DROP TABLE #dmtdqt;
	DROP TABLE #dmtdqtCha;
	DROP TABLE #dmtdqtResult;
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_bhxh_dieu_chinh_dtt_chi_tiet_doi_tuong]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union all
	select 6 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 7 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 8 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 9 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 10 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ
	union all
	select
	11 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union all
	select 12 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan
	union all
	select 13 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select 14 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select 15 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 16 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union all
	select
	18 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 19 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 20 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 21 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 22 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 23 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
		(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	24 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union all
	select 25 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select 26 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select 27 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select 28 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select 29 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHXH_NSD), 0) DttDauNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHXH' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	36 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union all
	select
	40 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		41 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
		(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		42 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
		(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	union all
	select
	43 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		44 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
		(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' -- CC,CN, VCQP
	union all
	select
		45 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
		(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' -- LĐHĐ

	-- Khối hạch toán
	union
	select
	47 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	49 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union all
	select
		50 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
		(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		51 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
		(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ

	union all
	select
	52 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)

	union all
	select
		53 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
		(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000'  -- CC,CN, VCQP
	union all
	select
		54 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
		(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHYT' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001'  -- LĐHĐ
	--BHTN
	--Lấy dữ liệu khối dự toán
	union all
	select
	59 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		60 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' -- Sĩ quan
	union all
	select
		61 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' -- QNCN
	union all
	select
		62 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' -- HSQ-BS
	union all
	select
		63 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		64 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	union all
	select
	65 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	union all
	select
		66 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0000' --Sĩ quan 
	union all
	select
		67 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0001' --QNCN
	union all
	select
		68 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0001-0002' --HSQ-BS
	union all
	select
		69 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		70 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020001-010-011-0002-0001' --LĐHĐ

	--Lấy dữ liệu khối hạch toán
	union all
	select
	72 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		73 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		74 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		75 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		76 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		77 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
		(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NLD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ

	union all
	select
	78 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	1 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán

	union all
	select
		79 STT,
		'' MaSo,
		N'  Sĩ quan' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0000' --Sĩ quan
	union all
	select
		80 STT,
		'' MaSo,
		N'  QNCN' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0001' --QNCN
	union all
	select
		81 STT,
		'' MaSo,
		N'  HSQ-BS' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0001-0002' --HSQ-BS
	union all
	select
		82 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0000' --CC, CN, VCQP
	union all
	select
		83 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(fThuBHTN_NSD), 0) DttDauNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
		((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
		(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
		'HT' Khoi,
		'BHTN' Thu,
		'NSD' Type,
		0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
		ctct.iID_MaDonVi = @MaDonVi
		and ctct.iNamLamViec = @NamLamViec
		and ctct.sXauNoiMa = '9020002-010-011-0002-0001' --LĐHĐ
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	and MaSo in (5, 8)

	union all
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	and MaSo in (6, 9)

	union all
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	and MaSo in (5, 6)

	union all
	select
	17 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	and MaSo in (8, 9)

	--BHYT
	union all
	select
	31 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	and MaSo in (16, 21)

	union all
	select
	33 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	and MaSo in (18, 23)

	union all
	select
	34 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	and MaSo in (19, 24)

	union all
	select
	37 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and STT in (40, 43) and MaSo in (18, 19)

	union all
	select
		38 STT,
		'' MaSo,
		N'  CC, CN, VCQP' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (41,44)

	union all
	select
		39 STT,
		'' MaSo,
		N'  LĐHĐ' NoiDung,
		isnull(sum(DttDauNam), 0) DttDauNam,
		isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
		isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
		isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
		((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
		(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
		'DT' Khoi,
		'BHYT' Thu,
		'' Type,
		1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
		and Khoi = 'DT'
		and STT in (42,45)

	union all
	select
	48 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and STT in (49,52) and MaSo in (23, 24)
	--BHTN
	union all
	select
	56 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	and MaSo in (29, 32)

	union all
	select
	57 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	and MaSo in (30, 33)

	union all
	select
	58 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	and MaSo in (29, 30)

	union all
	select
	71 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	and MaSo in (32, 33)
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	and MaSo in ('4=5+6', '7=8+9')

	union all
	select
	55 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	and MaSo in ('28=29+30', '31=32+33')

	union all
	select
	32 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	and MaSo in ('13=18+23', '14=19+24')

	union all
	select
	35 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (36, 37) and MaSo in ('16','17=18+19')
	union all
	select
	46 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (47, 48) and MaSo in ('21','22=23+24')
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (36)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (36)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (36)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (47)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (47)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (47)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	30 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (35, 46) and MaSo in ('15=16+17','20=21+22')
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]    Script Date: 2/22/2024 4:24:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh_unit]
	@MaDonVi NVARCHAR(255),
	@DonViTinh int,
	@NamLamViec int
AS
BEGIN

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

	--BHXH
	--Lấy dữ liệu NLĐ, NSD của khối dự toán
	select child.* into tbl_child from
	(
	select
	5 STT,
	N'5' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	6 STT,
	N'6' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu NLĐ, NSD khối hạch toán
	union
	select
	8 STT,
	N'8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHXH_NLD), 0) DttDauNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NLD), 0)) Tang,
	(isnull(sum(fThuBHXH_NLD), 0) - (isnull(sum(fThuBHXH_NLD_QTDauNam), 0) + isnull(sum(fThuBHXH_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	9 STT,
	N'9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHXH_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHXH_NSD), 0) - (isnull(sum(fThuBHXH_NSD_QTDauNam), 0) + isnull(sum(fThuBHXH_NSD_QTCuoiNam), 0))) Giam,
	N'HT' Khoi,
	N'BHXH' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	-----------------------------------------
	--BHYT
	-- Khối dự toán
	union
	select
	16 STT,
	N'16' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0001-0000',  -- Sĩ quan
	'9020001-010-011-0001-0001'	, -- QNCN
	'9020001-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	18 STT,
	N'18' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	19 STT,
	N'19' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	and ctct.sXauNoiMa in
	(
	'9020001-010-011-0002-0000',  -- CC,CN, VCQP
	'9020001-010-011-0002-0001'  -- LĐHĐ
	)
	-- Khối hạch toán
	union
	select
	21 STT,
	N'21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	(isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) DttDauNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0)) Dtt6ThangDauNam,
	(isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) Dtt6ThangCuoiNam,
	(isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0) - isnull(sum(fThuBHYT_NSD), 0))) Tang,
	((isnull(sum(fThuBHYT_NLD), 0) + isnull(sum(fThuBHYT_NSD), 0)) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0001-0000',  -- Sĩ quan
	'9020002-010-011-0001-0001'	, -- QNCN
	'9020002-010-011-0001-0002'  -- HSQ, BS
	)
	union
	select
	23 STT,
	N'23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(fThuBHYT_NLD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NLD), 0)) Tang,
	(isnull(sum(fThuBHYT_NLD), 0) - (isnull(sum(fThuBHYT_NLD_QTDauNam), 0) + isnull(sum(fThuBHYT_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	union
	select
	24 STT,
	N'24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHYT_NSD), 0) DttDauNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHYT_NSD), 0)) Tang,
	(isnull(sum(fThuBHYT_NSD), 0) - (isnull(sum(fThuBHYT_NSD_QTDauNam), 0) + isnull(sum(fThuBHYT_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	and ctct.sXauNoiMa in
	(
	'9020002-010-011-0002-0000',  -- CC,CN, VCQP
	'9020002-010-011-0002-0001'  -- LĐHĐ
	)
	--BHTN
	--Lấy dữ liệu khối dự toán
	union
	select
	29 STT,
	N'29' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán
	union
	select
	30 STT,
	N'30' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) BhxhNsd6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) BhxhNsd6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHXH_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020001' -- Khối dự toán

	--Lấy dữ liệu khối hạch toán
	union
	select
	32 STT,
	N'32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(fThuBHTN_NLD), 0) DttDauNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NLD), 0)) Tang,
	(isnull(sum(fThuBHTN_NLD), 0) - (isnull(sum(fThuBHTN_NLD_QTDauNam), 0) + isnull(sum(fThuBHTN_NLD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NLD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	union
	select
	33 STT,
	N'33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(fThuBHTN_NSD), 0) BhxhNsdDauNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0) TongCong,
	((isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0)) - isnull(sum(fThuBHTN_NSD), 0)) Tang,
	(isnull(sum(fThuBHTN_NSD), 0) - (isnull(sum(fThuBHTN_NSD_QTDauNam), 0) + isnull(sum(fThuBHTN_NSD_QTCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'NSD' Type,
	0 BHangCha
	from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
	where 
	ctct.iID_MaDonVi = @MaDonVi
	and ctct.iNamLamViec = @NamLamViec
	and ctct.sLNS = '9020002' -- Khối hạch toán
	) child
	-----------------------------------------------------------------
	--Lấy dữ liệu mục lục con
	--BHXH
	select child_ml.* into tbl_child_cat from
	(
	select
	2 STT,
	N'2=5+8' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NLD'
	union
	select
	3 STT,
	N'3=6+9' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	N'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Type = 'NSD'
	union
	select
	4 STT,
	N'4=5+6' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'DT'
	union
	select
	7 STT,
	N'7=8+9' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHXH' Thu,
	'BHXH' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHXH'
	and Khoi = 'HT'
	--BHYT
	union
	select
	11 STT,
	N'11=16+21' MaSo,
	N'- BHYT quân nhân' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'QN' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'QN'
	union
	select
	13 STT,
	N'13=18+23' MaSo,
	N'+ Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NLD'
	union
	select
	14 STT,
	N'14=19+24' MaSo,
	N'+ Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Type = 'NSD'
	union
	select
	17 STT,
	N'17=18+19' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'DT'
	and MaSo in (18,19)
	union
	select
	22 STT,
	N'22=23+24' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHYT' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHYT'
	and Khoi = 'HT'
	and MaSo in (23,24)
	--BHTN
	union
	select
	26 STT,
	N'26=29+32' MaSo,
	N'- Người lao động đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NLD'
	union
	select
	27 STT,
	N'27=30+33' MaSo,
	N'- Người sử dụng LĐ đóng' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHTN' Thu,
	'' Type,
	0 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Type = 'NSD'
	union
	select
	28 STT,
	N'28=29+30' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'DT'
	union
	select
	31 STT,
	N'31=32+33' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'HT' Khoi,
	'BHTN' Thu,
	'BHTN' Type,
	1 BHangCha
	from tbl_child
	where Thu = 'BHTN'
	and Khoi = 'HT'
	) child_ml

	select tmp.* into temp1
	from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_child_cat
	) tmp
	-----------------------------------------------------------------
	-- Lấy dữ liệu mục lục cha
	select parent.* into tbl_parent from (
	select
	1 STT,
	N'1=4+7' MaSo,
	N'1. Thu Bảo hiểm xã hội' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHXH'
	and Type = 'BHXH'
	union
	select
	25 STT,
	N'25=28+31' MaSo,
	N'3. Thu Bảo hiểm thất nghiệp' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHXH' Thu,
	'' Type,
	1 BHangCha
	from tbl_child_cat
	where Thu = 'BHTN'
	and Type = 'BHTN'
	union
	select
	12 STT,
	N'12=13+14' MaSo,
	N'- BHYT CC, VC, CNQP và LĐHĐ' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'LDHD' Type,
	0 BHangCha
	from tbl_child_cat
	where Thu = 'BHYT'
	and Type = 'LDHD'
	union
	select
	15 STT,
	N'15=16+17' MaSo,
	N'a) Khối dự toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (16, 17)
	union
	select
	20 STT,
	N'20=21+22' MaSo,
	N'b) Khối hạch toán' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'DT' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_child_cat
	where STT in (21, 22)
	) parent

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (16)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (16)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (16)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 15

	update tbl_parent 
	set DttDauNam = DttDauNam + (select DttDauNam from tbl_child where STT in (21)),
		Dtt6ThangDauNam = Dtt6ThangDauNam + (select Dtt6ThangDauNam from tbl_child where STT in (21)),
		Dtt6ThangCuoiNam = Dtt6ThangCuoiNam + (select Dtt6ThangCuoiNam from tbl_child where STT in (21)),
		TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	update tbl_parent 
	set TongCong = Dtt6ThangDauNam + Dtt6ThangCuoiNam,
		Tang = (Dtt6ThangDauNam + Dtt6ThangCuoiNam) - DttDauNam,
		Giam = DttDauNam - (Dtt6ThangDauNam + Dtt6ThangCuoiNam)
	where STT = 20

	--------------------------------------------

	select result.* into tbl_ddt_bhxh from
	(
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from temp1
	union
	select STT, MaSo, NoiDung, DttDauNam, Dtt6ThangDauNam, Dtt6ThangCuoiNam, TongCong, Tang, Giam, Khoi, Thu, Type, BHangCha from tbl_parent
	union
	-- Lấy tổng số thu BHYT
	select
	10 STT,
	N'10=15+20' MaSo,
	N'2. Thu Bảo hiểm y tế' NoiDung,
	isnull(sum(DttDauNam), 0) DttDauNam,
	isnull(sum(Dtt6ThangDauNam), 0) Dtt6ThangDauNam,
	isnull(sum(Dtt6ThangCuoiNam), 0) Dtt6ThangCuoiNam,
	(isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) TongCong,
	((isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0)) - isnull(sum(DttDauNam), 0)) Tang,
	(isnull(sum(DttDauNam), 0) - (isnull(sum(Dtt6ThangDauNam), 0) + isnull(sum(Dtt6ThangCuoiNam), 0))) Giam,
	'' Khoi,
	'BHYT' Thu,
	'BHYT' Type,
	1 BHangCha
	from tbl_parent
	where STT in (15, 20)
	) result
	order by result.STT;

	select STT, MaSo, NoiDung, DttDauNam/@DonViTinh DttDauNam, Dtt6ThangDauNam/@DonViTinh Dtt6ThangDauNam, Dtt6ThangCuoiNam/@DonViTinh Dtt6ThangCuoiNam, TongCong/@DonViTinh TongCong, Tang/@DonViTinh Tang, Giam/@DonViTinh Giam, Khoi, Thu, Type, BHangCha from tbl_ddt_bhxh;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child]') AND type in (N'U')) drop table tbl_child;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_child_cat]') AND type in (N'U')) drop table tbl_child_cat;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) drop table temp1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_parent]') AND type in (N'U')) drop table tbl_parent;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ddt_bhxh]') AND type in (N'U')) drop table tbl_ddt_bhxh;

END
;
;
;
GO




DELETE FROM [dbo].[BH_DM_ThamDinhQuyetToan]
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e2787df3-2fb6-4e3f-bec9-8607900fa74e', 1, 1, NULL, 2023, 1, N'admin', N'admin', N'THU, NỘP BHXH, BHTN, BHYT', N'A.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f4de9cf2-a90a-4fde-a575-897c14580f52', 1, 2, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM XÃ HỘI', N'I.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'406c6dc4-de4f-46b6-9fee-1bdab9a16bfa', 1, 3, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'06b4f812-744a-42af-9048-07de1284c59c', 1, 4, 3, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ce5e283b-0a91-4298-bd87-949be2f51ddb', 2, 5, 4, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd4282489-8b5b-4775-a855-703a369046a0', 3, 6, 5, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f5fb0c15-9daa-4ece-952b-5331c1481c5b', 3, 7, 6, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c07903c2-4772-4304-8d12-e06fad89963d', 3, 8, 6, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e9140f77-3a5c-4b6e-95e3-3ba3d044b8b0', 3, 9, 6, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'86f298f9-345c-4c62-9f5e-5021b94fbde6', 3, 10, 6, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0f6024f8-f6c0-4061-b1e6-07dd9f6325ff', 3, 11, 5, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'97c6bf37-e3d6-4e75-9cf0-bc8e28315691', 3, 12, 11, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'399a53a5-8a95-4ac8-959f-b8d11acc13e8', 3, 13, 11, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ab861fac-c730-4f0a-bf4d-365a0c7ecb40', 3, 14, 11, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'56121e01-83ed-4362-b4ef-9036043044f9', 3, 15, 11, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'71124ce2-94ac-4404-8b20-a321964f0fc1', 2, 16, 4, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'01ee4ad7-5897-41b6-81f0-57ff092cf65c', 3, 17, 16, 2023, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'365e556e-d01c-4fd3-a38c-f317e6176466', 3, 18, 17, 2023, 1, N'admin', N'admin', N'Trong năm', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'42dcd991-8778-4061-8c19-4ffd161e019b', 3, 19, 18, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bbb9ca41-8ddb-4bb3-b750-c46b1b1dd128', 3, 20, 18, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b72e752c-d3f5-4267-bb34-42bd30e11a80', 3, 21, 18, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9badf6bf-d307-4cd1-8ccd-6068e56166c3', 3, 22, 18, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'17415007-5050-4cb6-bb0c-11b9a15a3a54', 3, 23, 17, 2023, 1, N'admin', N'admin', N'Truy thu', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'68e4f597-e0db-4a37-9b44-5dce0a82e744', 3, 24, 23, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'25ffd520-86d4-48c0-b49e-53cfac28ca61', 3, 25, 23, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6f0772b5-6145-4caa-8cbd-0e9939cda87f', 3, 26, 23, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2ca2ebe8-8206-48ac-8bf2-f61c2ebc5aa3', 3, 27, 23, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1b2ea7a8-bce6-4908-8662-edbde3982048', 3, 28, 16, 2023, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'712d528b-1eab-4368-adff-45c6bb00e874', 3, 29, 28, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c83cf085-fcd6-4104-aaa9-22764457cfce', 3, 30, 28, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6b9372dd-560a-4f66-b253-0a4666258b1d', 1, 31, 3, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e438087d-38e6-4da2-a67e-b7339c62da2a', 2, 32, 31, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2a8cb71e-1489-4f66-8e2c-a808c82820d7', 3, 33, 32, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'be4a9e7f-3961-4df6-85df-ff1ecf791c53', 3, 34, 33, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0c8771a0-1c36-4c97-9a8c-bf6ac4e944b8', 3, 35, 33, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'be73b8ee-cccc-4cd4-ab69-136ab4ae0fff', 3, 36, 33, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'144290a4-a1b9-4f78-a1d4-fe1034a75e9a', 3, 37, 33, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'67479577-235e-4a36-96b4-c79466bd4481', 3, 38, 32, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3cecb0c3-71cb-4a86-9063-0facf934bad2', 3, 39, 38, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'59601511-6e69-4cd4-9baa-2358de979c74', 3, 40, 38, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9ff4725e-e83d-4959-961e-04bbe115590c', 3, 41, 38, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'05d7b658-4770-4c75-a8c2-48fae15f2e3d', 3, 42, 38, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'353fa996-11c5-45ce-9e6b-8c4c755d4109', 2, 43, 31, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1c948705-3936-41c9-8065-fb5eb4dc5349', 3, 44, 43, 2023, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'80754983-544a-4270-b63e-3332e624a18c', 3, 45, 44, 2023, 1, N'admin', N'admin', N'Trong năm', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4870f959-dbf4-455b-8ea2-17eb9ebb26e2', 3, 46, 45, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e0405cd4-e71b-47bf-a1d9-f5ae92866a1d', 3, 47, 45, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3bc39fd1-5fb6-4efd-a201-793c02777e31', 3, 48, 45, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'638e8423-1394-46f3-ad6a-2603021ffe68', 3, 49, 45, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fd0e5468-116d-48b4-a504-b969eefd1bfa', 3, 50, 44, 2023, 1, N'admin', N'admin', N'Truy thu', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'39765b93-2a4b-4777-84ce-bd557aee08e9', 3, 51, 50, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'db8f5eec-41f0-4c51-92a3-4045f92fc3e1', 3, 52, 50, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0bcf3e82-e97c-4b4e-a3f1-aec01f162556', 3, 53, 50, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd5fe2a21-fa82-4b95-939e-451ca50bffd4', 3, 54, 50, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e454bd9a-9dbf-4b0f-b38f-6d9150501ee6', 3, 55, 43, 2023, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'36905e30-3d34-46ef-b224-8b0536a6ba0b', 3, 56, 55, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd7ed7c1f-11df-4de0-b21a-6277d5826777', 3, 57, 55, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2d30f2da-a3d9-41cc-8793-2b6622ca1bb8', 3, 58, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bb961a9d-190d-4694-a619-3dc8cbfdfba5', 2, 59, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'11061d8e-1516-4aca-bea2-245c73792282', 3, 60, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ede2573d-e0b5-4f57-8ebf-06290c9073f6', 3, 61, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3138e386-9e70-4e91-992c-4fbbf184d008', 2, 62, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e8968b18-846b-42c0-8775-35f3833982ef', 3, 63, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0c2e0098-0983-43ab-9626-e498926bb0e2', 3, 64, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9c84690d-25e5-4dfb-a2aa-2ad0fb59a986', 1, 65, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a5809ee9-67cf-41f9-9f87-36ff898d8386', 1, 66, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM THẤT NGHIỆP', N'II.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'09283e0d-9881-46b8-b8d5-80c3ff12c3d8', 1, 67, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8c349955-6b35-429f-905b-2bd48b639ed2', 1, 68, 67, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1aaa4250-fb00-4a74-ac0e-370e1050add8', 2, 69, 68, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'61bd030b-463c-40e7-875f-96ad3bd4ba73', 3, 70, 69, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7d69fc9f-3a4c-481d-8dfd-aebdee47c569', 3, 71, 69, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f9186bcf-cb9d-4e02-86f3-47fb13226f06', 2, 72, 68, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'296c6e40-24a6-466d-8a93-cb0c2c93381d', 3, 73, 72, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'678c1ebd-90f0-4d7d-ac62-91f4b4104f74', 3, 74, 72, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2cd98471-8ad0-4bbc-abe3-a76967196b55', 1, 75, 67, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'efef742b-de53-49e1-94b9-e622c585a0d8', 2, 76, 75, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b027a65a-ea0b-458c-8ca6-239e3493ac77', 3, 77, 76, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e6b5126c-01b1-491b-95bb-85368577abf2', 3, 78, 76, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4615f000-2e1b-4507-9eea-1d95dc362a57', 2, 79, 75, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fed645fd-225c-4336-8fd0-958fd60ba556', 3, 80, 79, 2023, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'66bf1c8b-d249-4e3d-a4a2-374b05195eff', 3, 81, 79, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3dfa9e23-9198-4156-b2f8-e0b300712a6b', 1, 82, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'184d3c0e-1222-44d7-95e2-489a52f19e77', 2, 83, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c9c8bc00-3013-4b49-ad28-1ba22206b167', 3, 84, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f3543cf1-64d9-4e5c-a01f-afa98be403e2', 3, 85, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a4069c50-e64f-4972-a590-da62fef70e56', 2, 86, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0f7277d8-e4c4-4d32-8129-10a16cca8adb', 3, 87, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8b2bb1bb-0dc5-4e19-bda9-d6d4bcb87d4e', 3, 88, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2596a55b-fea5-4040-b65b-83f9d372f05b', 1, 89, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'feaa7797-0a54-44b8-ac68-f5b8b1f7a7ce', 1, 90, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ NGƯỜI LĐ', N'III.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'31fc3f8b-9db4-4d9d-a443-fec93c8ee8f5', 1, 91, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8c98782a-c353-4b33-9257-80e0c7ce5ce8', 1, 92, 91, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f5378bb0-ecbb-4f52-b8cf-a2b62180c19d', 2, 93, 92, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9121b4c2-6f23-46cc-bf96-392498d62dff', 3, 94, 93, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1b4e91fe-fe0f-4790-86fd-4951d7a00e0d', 3, 95, 94, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4326af6e-2077-438b-823c-60238d30f5f7', 3, 96, 94, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e09d6897-afb2-4250-94e4-47009985ec72', 3, 97, 93, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'cb87d2cc-18d2-4128-adba-4c266664d504', 3, 98, 97, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7edb71c9-431b-4967-9a39-82b54303190e', 3, 99, 97, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b97f6d6b-67bc-494a-9f6c-e4085410738e', 2, 100, 92, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c5f7f613-8bc2-4a57-8d1f-1984f693b581', 3, 101, 100, 2023, 1, N'admin', N'admin', N'Trong năm', N'b.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fdb39d84-6855-4af9-bac6-8871273d1251', 3, 102, 101, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'08ac24a3-30e6-4883-afcd-bf7b1d822e8e', 3, 103, 101, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'cd6f3976-6de7-4958-8a71-ffbb7cf4c8b8', 3, 104, 100, 2023, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8101ae8b-0b00-44ae-9b59-41f2a56392f1', 3, 105, 104, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd0ff7e27-32db-4635-aae8-f1afe313a5a1', 3, 106, 104, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0f2f9ed3-bd90-41db-b43b-9ec8086bedc6', 1, 107, NULL, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aba2c2a7-3613-4501-ab26-4ecd1e080bb3', 2, 108, 107, 2023, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3a50fb37-8af6-40d7-b648-de1a4affbdd0', 3, 109, 108, 2023, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'753948ab-41d9-4d70-982c-6198e2062ce2', 3, 110, 109, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'41034b8b-1a11-4000-9204-77f97ed2fb46', 3, 111, 109, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'81abbd45-deb4-46a7-8cc0-af8839b30049', 3, 112, 108, 2023, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f799ae74-ebd5-481b-b0c6-cf379b5318bf', 3, 113, 112, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'daeea9d9-82a2-4b80-833e-774ee6f5b35d', 3, 114, 112, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f926125a-b4ac-43f7-b6f9-3be9a35fe93d', 2, 115, 107, 2023, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd1dbce07-052a-41f9-a6a2-5e17a7030971', 3, 116, 115, 2023, 1, N'admin', N'admin', N'Trong năm', N'b.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'08b1db99-32db-4ffc-a494-38a064d976cd', 3, 117, 116, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'03e068ec-e129-4a85-917d-b552e689c500', 3, 118, 116, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6fe70e21-f2d1-4467-995e-c7bdf2ffefee', 3, 119, 115, 2023, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b84d65ec-b4e6-4508-95df-ad8e381843e8', 3, 120, 119, 2023, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6e4052e0-b35a-46d6-b980-851396bc35d0', 3, 121, 119, 2023, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'425f43a3-8ed7-45be-b79c-77015ac64395', 1, 122, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd3549b69-b4d8-49e0-a796-f486e0fd4d33', 2, 123, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2717752d-3565-4589-a752-369b8ece378d', 3, 124, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f256b07e-70a4-4342-96f4-f1be75c20671', 3, 125, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9ce1f167-ae0a-426b-a5b3-3bb507f9e120', 2, 126, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'584a09d3-f76b-4d62-ac7a-9b48111f2455', 3, 127, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aa41a6b0-24f9-4dd1-bf29-d1414d670c3b', 3, 128, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'25d6ab1a-9f55-477c-a8b8-0c67da293f76', 1, 129, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'13641572-6542-40a9-8917-c38b4fd879c1', 1, 130, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QUÂN NHÂN', N'IV. ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1bd25fc7-8ebc-4f82-b723-41b75189cbed', 1, 131, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b4094dcd-d1fd-415f-a3f2-1de7a1261967', 1, 132, 131, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd4a74db7-1ac9-4906-95ed-4c4bf234196d', 3, 133, 132, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2c8d5f6b-1c97-462a-bb50-28bd0feeacb6', 3, 134, 132, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'723fe616-5470-4100-8901-61e83ae7aafa', 3, 135, 132, 2023, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e865e8c8-e1ad-4281-9a90-d7280ec4eab4', 3, 136, 132, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ec5a0bdf-73df-4937-ba7f-77d0dd0ad178', 1, 137, 131, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b6c2eebd-e4af-42dc-bcf2-96382d1df85c', 3, 138, 137, 2023, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bd1e50b3-4aa1-4f1a-9502-09ecbf1b4f28', 3, 139, 137, 2023, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'496d86c4-996e-4a78-8e38-e917e57124ab', 3, 140, 137, 2023, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0eb4bfa5-959a-46c4-9b10-ad4957a0d4a4', 3, 141, 137, 2023, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'18781955-b9a1-45e0-bf16-390bfa6c24fe', 1, 142, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f063e3fc-2e2b-4bba-89cd-b5ee4148480d', 2, 143, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2907c02c-178f-4b87-b93b-61775cd4b698', 3, 144, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1d06adf6-0073-4982-b2a4-78385fa63f6f', 3, 145, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'556d3de5-febe-4575-be1d-b9719eb6f3b6', 2, 146, NULL, 2023, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f19dfa76-7567-4ca4-a520-81fb8856e19f', 3, 147, NULL, 2023, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'87aef7c7-d0ae-405a-aa8a-af1b4e5a4f89', 3, 148, NULL, 2023, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'51db7c7a-ecde-44f6-9cb6-7f8ffc99db4a', 1, 149, NULL, 2023, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b3428fd8-642d-4e38-b449-300cd8719d9c', 1, 150, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TNQN', N'V.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7beaf682-5af6-4b88-90f5-b68ffceab194', 3, 151, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8ea73040-4b4e-4397-902c-4f866588b4e8', 3, 152, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd7d63ea8-b232-437d-a337-e228d2842e49', 3, 153, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9907b672-987d-4a47-8e6d-e237d753841c', 1, 154, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TN CN, VCQP', N'VI.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ded822e6-02d5-4335-a806-9215c8d9be66', 3, 155, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b78bb17a-c774-4a93-9971-b9a04b11dfdd', 3, 156, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8502f1d7-9542-4da9-8647-19511309cf8e', 3, 157, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'19253430-83b8-41d8-bc05-863d7a2d1f9f', 1, 158, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QS XÃ PHƯỜNG', N'VII.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a62a65d9-30fe-41c3-9218-25b50a19d18a', 3, 159, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'60215cf3-df8e-493c-b36a-5c2c9dbe0525', 3, 160, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'501542b2-ac96-4681-97d7-f15c809d1dfb', 3, 161, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5e1cfa63-5da1-48bf-a1bd-60589b74abb7', 1, 162, NULL, 2023, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ HS, SV', N'VIII.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'279883f4-1875-4366-9f5b-81564c5128b8', 3, 163, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'083a4c23-e7f3-46f7-b0f6-1a0d0f0df860', 3, 164, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a116c3b2-2a49-445b-97a8-27c9db1d0c58', 3, 165, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2d3740f9-b74d-4d76-9b51-f8b66a1e88df', 1, 166, NULL, 2023, 1, N'admin', N'admin', N'BHYT Lưu học sinh', N'IX.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3cca05bf-1a36-4685-a4e8-4f053604df52', 3, 167, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e83a174f-38b7-4bdc-a468-daaea85d3b24', 3, 168, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'65f8dae6-9381-4692-97a3-d2804ee01003', 3, 169, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'613e060f-61f0-49f0-96a7-ff2cb01fe767', 1, 170, NULL, 2023, 1, N'admin', N'admin', N'BHYT Sĩ quan dự bị', N'X.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b0956214-18ec-4221-a914-37d9a2be2802', 3, 171, NULL, 2023, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3c7adb02-b61e-482e-b14b-21a42dbd8d09', 3, 172, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e4e8125b-2e59-4844-ac08-a2ef44f5a965', 3, 173, NULL, 2023, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'09d00a1d-0bd2-4d80-a35b-7a05a5928c5d', 1, 174, NULL, 2023, 1, N'admin', N'admin', N'BHYT CÁC ĐỐI TƯỢNG KHÁC (nếu có)', N'XI.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'341723af-f5ac-48d7-aaad-783b7d9a5576', 1, 175, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ', N'B.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a8e121ac-8a46-4113-b256-3bade62434f4', 1, 176, NULL, 2023, 1, N'admin', N'admin', N'CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI', N'I.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd5cea85c-df2a-4953-b0e1-daecb713dc51', 2, 177, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd85404c9-ffca-4a86-9f7d-b729ed42d023', 3, 178, 177, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1c2ba237-339f-4f8e-9179-49cb5c0395aa', 3, 179, 177, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b0aab09a-7ed4-4b91-bf44-066a624e826d', 2, 180, NULL, 2023, 1, N'admin', N'admin', N'Số cấp kinh phí', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'305e3ddc-d05b-4704-adf7-ecc5ee50e99d', 2, 181, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bf895110-ea64-4e91-86fa-4ad948ad39fa', 2, 182, 181, 2023, 1, N'admin', N'admin', N'Khối dự toán', N'3.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f95efcde-c5e4-4841-b907-935b2ecf5804', 3, 183, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'70ddfc1b-2a42-465b-b9a4-4562b2eb2a22', 3, 184, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'86af35b0-3339-4271-a4bb-fcb67a5ba556', 3, 185, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'da5dd1f1-b303-48c6-b66b-998d88eb8281', 3, 186, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0abae97c-d745-4a1d-959f-1d0b375a0b50', 3, 187, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd25e3f89-9f4c-4ce5-a1c0-f48e09587870', 3, 188, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'23dac86a-791d-4188-8623-67b043815392', 3, 189, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd92d15dc-4f50-486e-a104-1071b0bc2267', 3, 190, 182, 2023, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ea3549bf-d19e-4e0b-91fd-f39dd48f5c18', 2, 192, 181, 2023, 1, N'admin', N'admin', N'Khối hạch toán', N'3.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b27ff0d7-c7c1-453d-b154-81a0adf8d669', 3, 193, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7d4f643a-cc34-41e6-9cd1-3e3e1111b624', 3, 194, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7b2b246a-b396-40cb-ade4-04c0244d3c76', 3, 195, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ddeba7b6-022a-4271-9214-4b502a158765', 3, 196, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3f5d9565-da6c-45ec-9326-f7273c6447f0', 3, 197, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9c471147-9fe6-4815-8834-21c7a29acd99', 3, 198, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ff6aa4fe-4413-4a3c-8b40-eedec5a4042b', 3, 199, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3efc6e11-e973-4167-b65b-b0228c67dd2f', 3, 200, 192, 2023, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3a923e15-9164-4425-bca5-cd6344159fb5', 2, 202, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'23cb1ff9-87b8-4523-ab36-59b2d83a5217', 1, 203, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM Y TẾ', N'II.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'486bb6bd-31c3-4269-bf56-e65b7c833cce', 1, 204, NULL, 2023, 1, N'admin', N'admin', N'Chăm sóc sức khỏe ban đầu', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'29460366-ef3c-4d95-baaf-d770ae48fd37', 1, 205, NULL, 2023, 1, N'admin', N'admin', N'Người lao động', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e814021b-7ab9-4edc-9bdb-1a7aff7c59ac', 3, 206, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'545bba9f-c3c5-490c-8881-400c29d35fc9', 3, 207, 206, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'34e4d295-10c1-4228-9945-f822e8cd1d1b', 3, 208, 206, 2023, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b8391488-f831-4031-842d-f876df5bb725', 3, 209, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f4813548-55bc-4529-828c-0c9032438de5', 3, 210, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1d4130a3-019b-448d-b33e-7b6447ae6c6e', 1, 211, NULL, 2023, 1, N'admin', N'admin', N'Học sinh, sinh viên', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'99b98f08-8660-4df4-8aa5-f1b49a45576d', 3, 212, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'95104f15-5460-4e40-94a7-16a1f97c29fb', 3, 213, 212, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd937e479-a624-4d8c-b2e1-84a8519e8569', 3, 214, 212, 2023, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'35b75490-aff3-4919-a155-f41aef6a6e7d', 3, 215, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8dc97202-7a00-4c1e-8348-aa9dfd914fd4', 3, 216, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7e6dda54-f728-4b4d-bdcd-04270cb0c7f1', 1, 217, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí KCB tại quân y đơn vị (10%)', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'81799a15-c9ed-4c22-97a9-afc7e4502042', 3, 218, NULL, 2023, 1, N'admin', N'admin', N'Dự toán Bộ giao', N'2.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'778489ad-e747-4c49-8237-cefa59b680b4', 3, 219, NULL, 2023, 1, N'admin', N'admin', N'Tính 10% số thu', N'2.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ad2b8819-edc3-4ce4-a2b1-dce6028e8480', 3, 220, 219, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd38aa59f-1b71-45f1-b38e-a16ebd2cae08', 3, 221, 219, 2023, 1, N'admin', N'admin', N'Năm nay', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5fb0fe36-1e9c-4c09-8c9d-4291e351aa59', 3, 222, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0a612718-7e18-4865-bf44-9a7aa9e6fc6b', 3, 223, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'2.4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3e3bce82-09f3-435b-b0a9-bf8c72f56f5f', 3, 224, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'2.5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'84c354b5-e98f-4081-a8bd-4c5d0890d58c', 3, 225, NULL, 2023, 1, N'admin', N'admin', N'Dự toán (10%) còn thừa', N'2.6.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aa48bb46-c822-46cd-a8a0-50deba4ce78d', 1, 226, NULL, 2023, 1, N'admin', N'admin', N'Chi mua sắm trang thiết bị y tế', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'61d91b79-da78-4b6f-8a96-60f1ab5c12ad', 3, 227, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'3.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0ba43f59-2cca-447f-8987-ab8470a5f53a', 3, 228, 227, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0a66b1b5-a3a0-4c14-88c0-99e82c3d20be', 3, 229, 227, 2023, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'69e9ed52-0463-4832-8bdf-c40e8ab7e060', 3, 230, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'3.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aac4320a-e58b-4b7e-9323-90bf4c09b548', 3, 231, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1c09d043-8f61-4d97-ba3f-74e88d802d20', 3, 232, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.4', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0175fdff-50dc-4982-9542-22db465fd130', 1, 233, NULL, 2023, 1, N'admin', N'admin', N'Chi khám chữa bệnh tại TS - DK', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3a857e14-b78f-46f5-8522-2b8b664582b3', 3, 234, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'4.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'54cbda4f-e4c1-4795-b385-1395e3f40e34', 3, 235, 234, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'- ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'255bca64-be6d-409c-a7bc-34a3795c94ab', 3, 236, 234, 2023, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'34dbd63c-643c-4229-9324-ba3fe44058b6', 3, 237, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'4.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a6a24ea0-b5eb-4123-ac44-e2a38aee5aaa', 3, 238, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'4.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e36999bb-078d-4939-8849-5193cef6c0cd', 3, 239, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'4.4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'60f90cef-8b92-487f-84ec-c68865c48354', 3, 240, NULL, 2023, 1, N'admin', N'admin', N'Dự toán còn thừa', N'4.5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9873ffa8-eb19-4b53-a290-a29cca2f279b', 1, 241, NULL, 2023, 1, N'admin', N'admin', N'CHI BẢO HIỂM THẤT NGHIỆP', N'III.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2142b60a-8252-477b-8ca5-53306286ec82', 3, 242, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bd5492ae-ae8c-4d98-b485-201a9f3a8e0c', 3, 243, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1f5d1e5e-c693-42cf-9b39-023104383fcb', 3, 244, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'891da486-be8b-46bd-b920-e565d311a278', 1, 245, NULL, 2023, 1, N'admin', N'admin', N'KINH PHÍ QUẢN LÝ BHXH, BHYT', N'IV. ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5bb4d2ca-cf09-4a8c-b582-0d38ec69463f', 3, 246, NULL, 2023, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7f7ab121-152e-4e0a-81af-10c860866bc4', 3, 247, 246, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b5304a63-c47a-499c-a911-0bf4170500d7', 3, 248, 246, 2023, 1, N'admin', N'admin', N'Năm nay', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aaad2dad-3674-4b74-bb5e-49d3ed3abc66', 3, 249, NULL, 2023, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b09686d6-3173-494d-8847-528ca63dfbba', 3, 250, 249, 2023, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'2.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9fd44425-0389-4aa2-9837-570667967d80', 3, 251, 249, 2023, 1, N'admin', N'admin', N'Năm nay ', N'2.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c1e7252e-aa49-4875-acb7-e7b2c3aba715', 3, 252, NULL, 2023, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b54dff89-af01-4c38-bfb9-6edd75e53846', 3, 253, NULL, 2023, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f0a37e93-b6e4-481c-9852-d2e83fbe1823', 3, 254, NULL, 2023, 1, N'admin', N'admin', N'Dự toán còn thừa', N'5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'41a9e0d1-7238-484c-9796-7e74ce8c3ab1', 1, 255, NULL, 2023, 1, N'admin', N'admin', N'XÁC ĐỊNH CƠ SỞ TÍNH NỘP BHYT TỪ QUỸ BHXH', N'C.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5a12c326-71c6-4f09-956c-0f0d2cd904ea', 3, 256, NULL, 2023, 1, N'admin', N'admin', N'Số tháng hưởng trợ cấp ốm đau thuộc danh mục bệnh cần chữa trị dài ngày không tính đóng BHXH', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'37c4e2b2-42a6-436b-b29d-f2a9c37edf52', 3, 257, NULL, 2023, 1, N'admin', N'admin', N'Số tiền trợ cấp sinh con và nuôi con nuôi', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a882a1bc-61c0-4882-a048-bafa2ae82108', 1, 258, NULL, 2023, 1, N'admin', N'admin', N'GIẢI THÍCH SỐ LIỆU CHÊNH LỆCH (nếu có)', N'D.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c53e4915-cc33-4a74-938c-e818351f0f0c', 1, 1, NULL, 2024, 1, N'admin', N'admin', N'THU, NỘP BHXH, BHTN, BHYT', N'A.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e8a29103-27c5-4230-b5fc-7fb07d47671a', 1, 2, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM XÃ HỘI', N'I.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8e7155ef-9bb8-4f49-bad1-1d82ccbaf0cd', 1, 3, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd6996df4-b40c-453b-b1d3-ba6f84541b99', 1, 4, 3, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9bff9fa8-bd70-4a88-b200-78be54c87087', 2, 5, 4, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'dfecc10a-6a50-41b5-8edc-eb9c8896844a', 3, 6, 5, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3d844721-69fc-400e-ba72-228f4a1fe9f1', 3, 7, 6, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd30abfd4-f837-4841-b89b-a99e416f7459', 3, 8, 6, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3867bb47-47cc-4d34-ada2-8d31a3eef1c4', 3, 9, 6, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'278021ab-a014-4f30-9d1d-0e1bf8737883', 3, 10, 6, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'48f5e9be-fa4d-49bf-8b2a-1bc1bc2ab9b8', 3, 11, 5, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1215533d-3336-4054-a8d0-2130fdc76d0c', 3, 12, 11, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bd63ca9c-430f-415d-a069-e33e2fc03913', 3, 13, 11, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'495c663d-dacb-42fc-9d50-a03552c3ab12', 3, 14, 11, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b51685af-6a57-44ab-a2de-d4469ad2d3b4', 3, 15, 11, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5d12adec-946c-459a-b389-e70265498b95', 2, 16, 4, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b099a908-e1a4-40f8-a52f-f84b80c779fd', 3, 17, 16, 2024, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'976de8f2-f1f9-4ee5-9ed5-eea3f4630f07', 3, 18, 17, 2024, 1, N'admin', N'admin', N'Trong năm', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1a98082e-0c2d-4f37-84ee-c829469c30f8', 3, 19, 18, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f37db09d-5538-4b62-94c1-04cc21e05d82', 3, 20, 18, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'320aa068-0626-4766-a104-4ae9ba80d812', 3, 21, 18, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a16e03bd-8444-4cc1-ac4f-a358a1ece5e3', 3, 22, 18, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ac26d384-11b4-491f-b252-1ac48b0dbf23', 3, 23, 17, 2024, 1, N'admin', N'admin', N'Truy thu', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd1925f4b-cb89-4f86-9c8a-404fb0f8d02c', 3, 24, 23, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e3e1b716-b081-4159-94dc-89eb2764fb22', 3, 25, 23, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e41cc31a-ae43-45cb-900b-de234dd1cdce', 3, 26, 23, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6898ceb0-14bc-484d-969a-cb07f6f2bc1b', 3, 27, 23, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b2a4a4aa-cb45-47c2-8a5c-9e6687667882', 3, 28, 16, 2024, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'dc0a58c7-c702-4cc2-864f-990493464fa5', 3, 29, 28, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'57ba56eb-f424-4a97-bd20-8f216d10d83a', 3, 30, 28, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4fffff82-34e3-4209-84b6-d8c473a9d29f', 1, 31, 3, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd71035a3-ffcc-4e7f-ace5-1adfc6acd8cc', 2, 32, 31, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a5824961-9c42-469e-88ab-b9238fb6db0f', 3, 33, 32, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'674363d4-1ead-4971-a7f1-baef57fe5b7c', 3, 34, 33, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ada6e7db-e83d-42ee-9714-f0ef61ac8941', 3, 35, 33, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'28486bd7-daf1-43cb-9555-0828fcd559d2', 3, 36, 33, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b4c0a910-a558-4019-986c-4013348286a4', 3, 37, 33, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9a5362de-551c-469c-8c1d-5f3bba3e7f25', 3, 38, 32, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1397a25b-2cb3-4f1d-bbd2-5d83e0c4780b', 3, 39, 38, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'227c2898-bc98-436c-acab-2b531b0ad131', 3, 40, 38, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ddb0dc3d-15f4-4132-8ced-5341554debc9', 3, 41, 38, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0fbb5e72-8052-4ecf-8933-373008780d62', 3, 42, 38, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a218da60-a5b5-4002-90e3-203e23246fd5', 2, 43, 31, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9381a691-c31c-4746-b401-4ab5f1893652', 3, 44, 43, 2024, 1, N'admin', N'admin', N'Người hưởng lương', N'b.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9da85f88-feca-4c01-8bc8-6830f591d9d9', 3, 45, 44, 2024, 1, N'admin', N'admin', N'Trong năm', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c83285e6-97c2-408d-9253-6678a11f64f5', 3, 46, 45, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'482627a9-7d6e-45d7-8987-9a0de8e5d18d', 3, 47, 45, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1fc50ce3-6a3a-49a1-828e-79a2e0e1772a', 3, 48, 45, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ef9fdc57-80fc-4c52-bdf4-835cb34671a1', 3, 49, 45, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'89a8eda3-f5e5-4aec-96a6-6b80ca0b2fd6', 3, 50, 44, 2024, 1, N'admin', N'admin', N'Truy thu', N'*', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0d146310-f622-45be-ae2c-8af9376c69cc', 3, 51, 50, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a122e951-0bc3-44f3-a787-02dab523e9d0', 3, 52, 50, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'32ed8696-0c92-49c3-a714-b90db494994b', 3, 53, 50, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'82fda8f5-294c-4177-849b-9d9f5c711551', 3, 54, 50, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3f637d4a-ef55-43a6-af20-114af3630737', 3, 55, 43, 2024, 1, N'admin', N'admin', N'Người hưởng PC (HSQ, BS)', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'39d4eb72-2b79-4bf6-84fe-e384e8bd2cd9', 3, 56, 55, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1b8439db-2b2f-457b-a3fd-83d4a1fe5627', 3, 57, 55, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'796006cb-7aec-4edd-86a8-f4040501e2a6', 3, 58, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3d13025c-1ff6-463b-9344-4bf9f0c5e51a', 2, 59, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7f176438-7ba2-47ec-b80d-ca625252d012', 3, 60, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a9038043-ac42-42d3-a2ba-84d4a761d245', 3, 61, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'81b2ddff-6a33-427b-b0e7-45548c17914b', 2, 62, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a030c832-600d-4d76-8ca2-2f2c47df2f9c', 3, 63, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2d2ec00e-971b-4b18-8a81-9ddb6255a8e5', 3, 64, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'78e47d15-4f26-44d1-99dd-0ab30f8d386c', 1, 65, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'239dee5d-4b7c-4d5e-a116-91c6b49804ab', 1, 66, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM THẤT NGHIỆP', N'II.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'81a6e8cb-2fe9-4c5b-8537-c5e4a244f5b3', 1, 67, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'79a000ca-e365-41a6-a49a-e356f892dc3d', 1, 68, 67, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'dd673239-beef-4efb-8cef-552c58a40ee0', 2, 69, 68, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'be052e23-854b-4b0b-a856-313919b92d3d', 3, 70, 69, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ed635939-d5ea-4abe-ba35-8177cef1189a', 3, 71, 69, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5cd568f4-08e5-4b72-9ded-43bce43173c8', 2, 72, 68, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8778dcbf-f7a0-4f70-a8f3-05e70d9b65ac', 3, 73, 72, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4df70045-4cfa-44ab-980d-34d90a545109', 3, 74, 72, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6768990d-c966-49ca-a253-1ced6bced3fe', 1, 75, 67, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'74946fc3-5804-43e7-af98-ccd9e2e7c36f', 2, 76, 75, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2ef0636c-fbb4-42f8-b9f8-fa14588de69f', 3, 77, 76, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bed9fb55-e9eb-4bbd-8ce9-b5421b4704f3', 3, 78, 76, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'211711bf-f0ee-465c-ade4-f3a764e9133b', 2, 79, 75, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8bd54996-493f-46d0-91b5-a08ad2d94cb4', 3, 80, 79, 2024, 1, N'admin', N'admin', N'Trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8f927b36-a475-4fbc-8778-6eda11bb432c', 3, 81, 79, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8864a1fe-9016-4dd4-b885-dd56300f163f', 1, 82, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e0d200d2-2c8a-4049-8a0a-a9bdb697febe', 2, 83, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a9e51b07-abef-4154-8174-d131ab0e0b3a', 3, 84, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6bb3f4c7-144e-4491-b824-c16a8f5af95f', 3, 85, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'33c66331-7e9f-439d-877f-5011964a798e', 2, 86, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'82b1f3b8-8081-4055-83a5-7701f33f3f72', 3, 87, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'970634e5-6587-4ce0-aa0f-66400eeba5f5', 3, 88, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b9f01688-bf71-448c-b22b-f7d446efd589', 1, 89, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ae3c3bca-2dad-4288-98c8-cb5f7a7c975d', 1, 90, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ NGƯỜI LĐ', N'III.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3cba1202-4e6f-4f51-8362-3f94fd4651c9', 1, 91, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'df7a7d1b-a8e9-4ca4-adf0-78e883d21370', 1, 92, 91, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c8611de2-8e60-4dec-91f4-90ef53b55edc', 2, 93, 92, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'cdc0ae44-f6b7-4c8b-8ddc-b48c4b1cb87e', 3, 94, 93, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c37d57f7-9000-4e20-8a5c-8f140500a09e', 3, 95, 94, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'87c00cc1-1708-426a-b440-d477edce1fd6', 3, 96, 94, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'72fc45cb-2c71-4d81-bca5-445969ee1c18', 3, 97, 93, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'17ac9d06-faac-4397-ad86-a7d2a0327ba7', 3, 98, 97, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a3013e61-adbf-4e5e-9d62-63e790c47c40', 3, 99, 97, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'cce8653b-40bc-4b58-8bb3-b620d22f9465', 2, 100, 92, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8a56e818-e96e-4758-bb45-98cce5bad687', 3, 101, 100, 2024, 1, N'admin', N'admin', N'Trong năm', N'b.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4ae063ad-0456-49da-be6f-dbb771c5e0e2', 3, 102, 101, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3391944f-8d74-4518-bb59-6ef64b82023b', 3, 103, 101, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f9b4aa43-13c5-492e-b18a-e95d84be0bf8', 3, 104, 100, 2024, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4e14f7c0-47fe-4fc4-8890-bc9175966e81', 3, 105, 104, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5015ef89-b18a-4021-b48b-06b25cf44029', 3, 106, 104, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ac0d5fb2-bfaa-4873-8ed9-7e4362700515', 1, 107, NULL, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6d4f57a2-65d0-4566-bffe-6f337da883cd', 2, 108, 107, 2024, 1, N'admin', N'admin', N'Người lao động', N'a.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'74715d81-4361-4da3-9aa8-f2cc634ce61b', 3, 109, 108, 2024, 1, N'admin', N'admin', N'Trong năm', N'a.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd5d76f45-7244-4672-af4c-8ef200ea898e', 3, 110, 109, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e09ac71b-4004-4fdb-a5dd-7e83ea3e21a7', 3, 111, 109, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'51bf7e39-51e7-4cf5-99b8-6fee711bcf08', 3, 112, 108, 2024, 1, N'admin', N'admin', N'Truy thu', N'a2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1ba48b28-7373-4c19-a59b-ef5a03b888c5', 3, 113, 112, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'635e9f9a-da4a-44dd-9446-a24ca9fc5905', 3, 114, 112, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5170d6a4-b529-41d8-9792-4493c30d008e', 2, 115, 107, 2024, 1, N'admin', N'admin', N'Người sử dụng lao động', N'b.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c710b46e-64b6-4e41-b183-a1a63321c651', 3, 116, 115, 2024, 1, N'admin', N'admin', N'Trong năm', N'b.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fb6b57f6-87a1-4714-9a76-f72ec8ecdbc2', 3, 117, 116, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f055f2e4-9124-44da-b4b6-001922c81375', 3, 118, 116, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6fcdb994-904f-469f-a697-98fb4df20890', 3, 119, 115, 2024, 1, N'admin', N'admin', N'Truy thu', N'b.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f4bd1a73-2adc-4fbc-920c-8ab740aef483', 3, 120, 119, 2024, 1, N'admin', N'admin', N'CC, CN, VCQP', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'84e228c2-d994-42e1-8dec-1688e054d1fa', 3, 121, 119, 2024, 1, N'admin', N'admin', N'LĐHĐ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'13ea74a8-f435-4c4b-b14b-b02381ea80fa', 1, 122, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'af83936e-7072-41a9-8724-fda139856c20', 2, 123, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'68599d8e-6bc5-4c98-9cf5-6920e3d0701b', 3, 124, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f9cc0921-dd23-4f5d-82c0-738f5889f64e', 3, 125, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd6d728c6-9503-4790-88d8-940eab00fef1', 2, 126, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'459d7e29-fdfd-4667-a65d-0034fd34e9fe', 3, 127, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2826de5c-62f4-4e94-a98f-060185474692', 3, 128, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bf79813f-9c49-43df-b89a-46770a83cbde', 1, 129, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2f53ccfd-aa43-4dde-85db-ebf7dbce0c3b', 1, 130, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QUÂN NHÂN', N'IV. ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'671f218d-0026-4445-b84b-a09774b41d2c', 1, 131, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e5324a68-3064-4280-a447-4f4c31d1b39e', 1, 132, 131, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e59d6e30-d35c-4baf-9f9f-4534dff7ec02', 3, 133, 132, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0f52d64f-5835-4851-9c87-1ee108dd14b8', 3, 134, 132, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c4fee7f9-ec9a-436d-aa26-e4b60f822402', 3, 135, 132, 2024, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'98f1167e-5f0e-4bb2-8ba1-b85423fad450', 3, 136, 132, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8ba0b60f-6c38-4922-90f3-71307b29304a', 1, 137, 131, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'93644242-5778-4b5d-9432-8c03ae7cde5a', 3, 138, 137, 2024, 1, N'admin', N'admin', N'Sĩ quan', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fa61a8e7-3739-4422-a4b6-b41045ee120a', 3, 139, 137, 2024, 1, N'admin', N'admin', N'QNCN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1313661b-dc6f-4f8c-946f-9d301f822fad', 3, 140, 137, 2024, 1, N'admin', N'admin', N'HSQ, BS', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'79a6161f-d27d-42b2-a611-fda580455072', 3, 141, 137, 2024, 1, N'admin', N'admin', N'Truy thu', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5a4f1ede-1ed3-4c1d-88a7-2f0f60e76784', 1, 142, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fa4b145d-7074-4734-8661-54f930c9e958', 2, 143, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày 31/12/….', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'47c45a4a-4ba8-491f-a5ec-efef02fc6802', 3, 144, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'057896d1-a5df-4d39-9aed-1022d5bb2a99', 3, 145, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'79655f3d-2f4a-43d5-9287-34697508bff9', 2, 146, NULL, 2024, 1, N'admin', N'admin', N'Đến ngày thẩm định', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'70f643ba-7111-46c3-81ec-4308f1451ed2', 3, 147, NULL, 2024, 1, N'admin', N'admin', N'Số tiền', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b3fd713f-a0dc-47f1-8162-35eaad47f023', 3, 148, NULL, 2024, 1, N'admin', N'admin', N'Tỷ lệ so với số phải nộp (%)', N'+', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8628d258-00de-4860-9b28-6d34ea84c544', 1, 149, NULL, 2024, 1, N'admin', N'admin', N'Số nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'40ad4352-e862-41ce-8c42-0bf0168b4970', 1, 150, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TNQN', N'V.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'29d44145-0bf0-4468-94b9-b13c5d818a32', 3, 151, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b3b63df4-f775-4d9f-aa18-5b51895f568b', 3, 152, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b244c32b-18a8-4507-a96b-6e5ba0cb0290', 3, 153, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'989840ef-551d-4e5f-888d-0db1a4ad2399', 1, 154, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ TN CN, VCQP', N'VI.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c4316d7d-710c-4b31-ab22-1a9f21596ada', 3, 155, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'74f1ac2b-7155-48e3-b7fd-7b8a5aa7b65f', 3, 156, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'aa265d90-ee94-4676-8713-1a31af36d42c', 3, 157, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'115d48fc-0ecf-4fc9-a00d-1378afe7d4e4', 1, 158, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ QS XÃ PHƯỜNG', N'VII.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9fc98bda-0519-47f3-b4b9-7ff6074261e0', 3, 159, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9cb780df-e590-42a3-8ae0-538253e1b599', 3, 160, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'40dbffe1-5ffd-4185-b734-a858d7bdb574', 3, 161, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3e3cbc21-0ef3-40a9-87b8-b34416c80372', 1, 162, NULL, 2024, 1, N'admin', N'admin', N'BẢO HIỂM Y TẾ HS, SV', N'VIII.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'8eb55252-a096-4efe-80a2-72f022d01242', 3, 163, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'59c03c94-c8a3-48d6-9be7-a151b3c72183', 3, 164, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'afc1c2b8-39b0-4ad5-b46c-8ab3dacc1e7d', 3, 165, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4b629fe9-c246-4b26-af7c-62df8b067743', 1, 166, NULL, 2024, 1, N'admin', N'admin', N'BHYT Lưu học sinh', N'IX.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e041dff1-34ac-455d-a029-1220e122513e', 3, 167, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2b6aa6d0-e048-4544-90e1-86ae6e738d57', 3, 168, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'12fd2f4a-38b7-4b74-a40a-de68abf965c3', 3, 169, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'70f71ac7-6e3f-47d3-bdcf-82bf57f38912', 1, 170, NULL, 2024, 1, N'admin', N'admin', N'BHYT Sĩ quan dự bị', N'X.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bebe8e07-d3ac-4800-bb85-47c065997a9e', 3, 171, NULL, 2024, 1, N'admin', N'admin', N'Số phải nộp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c324ccab-1978-47c7-a502-1d64d5f84178', 3, 172, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'217aadb0-3f5d-4174-844c-40d9aae6ffc1', 3, 173, NULL, 2024, 1, N'admin', N'admin', N'Số đã nộp thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'09ee50d8-662e-4e8a-baeb-dfced75f66ee', 1, 174, NULL, 2024, 1, N'admin', N'admin', N'BHYT CÁC ĐỐI TƯỢNG KHÁC (nếu có)', N'XI.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b4ae5cc4-8725-4a43-8ede-8a6550bbe69c', 1, 175, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM XÃ HỘI, BẢO HIỂM Y TẾ', N'B.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'13d63355-5472-4ecb-8c09-0b8a19b2d134', 1, 176, NULL, 2024, 1, N'admin', N'admin', N'CHI CÁC CHẾ ĐỘ BẢO HIỂM XÃ HỘI', N'I.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2bf3cabc-f9fd-48a7-a6e5-981f48541df6', 2, 177, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1478a605-1a9b-4ae4-a933-2461cd2eb30c', 3, 178, 177, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1d5f384c-a168-4cbf-9265-dfdd40aac46e', 3, 179, 177, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'98d81c9a-3218-452c-893e-ea04cda1ca54', 2, 180, NULL, 2024, 1, N'admin', N'admin', N'Số cấp kinh phí', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'669b6ee3-8595-45e5-8c5c-bdbca73511ca', 2, 181, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'85b68490-c49c-4d94-a974-fbb5af800ecb', 2, 182, 181, 2024, 1, N'admin', N'admin', N'Khối dự toán', N'3.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3a79052e-627f-49a1-8cf0-d1c0131de618', 3, 183, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'49bb21d9-1af1-4ea8-ad72-f2ceb366f9f2', 3, 184, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e1981dd2-0de2-437a-bd18-056d24dc8b4e', 3, 185, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9a26e6ba-c811-4544-a48a-8ec63f57a1c9', 3, 186, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'2450c1aa-7ce8-4397-8925-64d1395fd523', 3, 187, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd7842f12-28d3-46b1-9272-93739b2a236a', 3, 188, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'91c8488d-4003-4aa8-bf46-f63042c9fdd4', 3, 189, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f2be2226-1b55-43c0-a888-8afc52cf7013', 3, 190, 182, 2024, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ba3b7edb-4252-4270-9c30-72c3772884cf', 2, 192, 181, 2024, 1, N'admin', N'admin', N'Khối hạch toán', N'3.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'126cb1c2-b37e-4b9e-b147-f2ef36996596', 3, 193, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp ốm đau', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'dfe9c2a5-d425-4184-bb44-2df7f8508960', 3, 194, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp thai sản', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e2abc574-3570-4b26-a403-21316efb2faf', 3, 195, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp TNLĐ, BNN', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9b51dc24-0a66-4b74-8cee-600811691657', 3, 196, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp hưu trí', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e7b5460c-47e7-4d62-b7fe-dd5eef5cf560', 3, 197, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp phục viên', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5b928dcc-2aeb-40e2-9c6a-5a40f481a8de', 3, 198, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp xuất ngũ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'379ebec4-ab1d-4cfe-ba16-273f2315337d', 3, 199, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp thôi việc', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6940163f-4387-44d3-9025-17462807c946', 3, 200, 192, 2024, 1, N'admin', N'admin', N'Trợ cấp tử tuất', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'47934426-eb9b-45a0-863d-b2071ec06714', 2, 202, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4b0e8931-0005-4796-a3bc-80b8da5326cf', 1, 203, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM Y TẾ', N'II.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'63bac372-8676-4cf5-9d3f-57e8be27477a', 1, 204, NULL, 2024, 1, N'admin', N'admin', N'Chăm sóc sức khỏe ban đầu', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'217c715f-6f36-46fc-9746-76779277b576', 1, 205, NULL, 2024, 1, N'admin', N'admin', N'Người lao động', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'19220f24-28fb-43f2-89ec-1829fdc49c98', 3, 206, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4c2bbe42-8668-43dd-866f-6a6410037aff', 3, 207, 206, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e389c4e9-0284-46c1-861d-6494bccf79e6', 3, 208, 206, 2024, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'916eecbc-b3a0-4bca-9ac0-e6400ea072df', 3, 209, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd49fc048-ca01-4ff5-89a8-62859832aefe', 3, 210, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6724b359-a657-459b-a530-1813522ba0b6', 1, 211, NULL, 2024, 1, N'admin', N'admin', N'Học sinh, sinh viên', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b84d57c4-e001-45e8-8c31-72c8b607c8be', 3, 212, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được sử dụng', N'a)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0ca6ca2a-d174-4a76-8c71-e18b87d3cccf', 3, 213, 212, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b63fd0ca-f5be-4b47-9767-81c7dbf9370f', 3, 214, 212, 2024, 1, N'admin', N'admin', N'Được cấp trong năm', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'74c3f325-aefe-4f9b-b2db-e4e43da6b2e2', 3, 215, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'b)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e7bba7aa-9504-467d-99c9-61367a242016', 3, 216, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'c)', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'47589376-9e6e-47a7-be8a-d0ace656283a', 1, 217, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí KCB tại quân y đơn vị (10%)', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'71f89f60-9f7e-4824-9fb5-31d6f43e897c', 3, 218, NULL, 2024, 1, N'admin', N'admin', N'Dự toán Bộ giao', N'2.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'd29c8b0e-28ca-4997-a2d2-bb83b8eea2cd', 3, 219, NULL, 2024, 1, N'admin', N'admin', N'Tính 10% số thu', N'2.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9fbd4142-e432-4248-a087-b905fa2c5ce7', 3, 220, 219, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'820bf179-2603-44f3-ad25-001bf546d8a2', 3, 221, 219, 2024, 1, N'admin', N'admin', N'Năm nay', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'fcfe5504-bdf7-4f77-8bf1-df760c0f324f', 3, 222, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c9e943e1-bd0c-4caf-85e8-bc727274e68d', 3, 223, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'2.4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'91d03e07-daa3-424d-ac07-d29cc83a29db', 3, 224, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'2.5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'a3155d00-bd97-4792-a5a0-42dbff5e460f', 3, 225, NULL, 2024, 1, N'admin', N'admin', N'Dự toán (10%) còn thừa', N'2.6.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6135ac51-a4ba-4af5-afc9-00e03697bc7d', 1, 226, NULL, 2024, 1, N'admin', N'admin', N'Chi mua sắm trang thiết bị y tế', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'e71a95ad-48b5-42d1-87a0-c81bdeeb4f5f', 3, 227, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'3.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'35ac389c-9b41-4752-9161-fdcfea9454ab', 3, 228, 227, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bd6e6a4d-b7a2-4b02-b488-cb710d182eef', 3, 229, 227, 2024, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0623fe47-e0cf-42e6-8771-675061909d1e', 3, 230, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'3.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'84c518f2-bff6-46ac-bc3b-25e4caf7d7b2', 3, 231, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'487cda89-ad83-4bcc-b58c-e4cddf1c74e4', 3, 232, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.4', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'b0fd27e0-8a60-42c1-86f9-7692c76c60b6', 1, 233, NULL, 2024, 1, N'admin', N'admin', N'Chi khám chữa bệnh tại TS - DK', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bcefbf0d-7e29-4b0a-9ff9-aba928f60bb1', 3, 234, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'4.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6ae623c0-56e0-4cfc-a487-74e31f299c3c', 3, 235, 234, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang ', N'- ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'bcf2f202-9d83-4c0f-b968-5dfe25170a79', 3, 236, 234, 2024, 1, N'admin', N'admin', N'Năm nay ', N'-', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'21620bc6-98f8-4797-b80d-f63792b00e2e', 3, 237, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'4.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6f3d26c9-6dd9-408b-a2d6-b84dc1cb6249', 3, 238, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'4.3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5a0afe5a-3d7b-44a4-9e3b-8e2157dc0607', 3, 239, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'4.4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6722f1e5-939f-484f-84da-d37e2bc63dc1', 3, 240, NULL, 2024, 1, N'admin', N'admin', N'Dự toán còn thừa', N'4.5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'5992aae2-1ca4-445c-a927-836f7f79dcad', 1, 241, NULL, 2024, 1, N'admin', N'admin', N'CHI BẢO HIỂM THẤT NGHIỆP', N'III.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c261857d-1a01-496d-8fa2-22cbfdcb1292', 3, 242, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'56109f81-d1bc-497d-9521-38522af8bafb', 3, 243, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3966cbe7-4f08-4a88-a27e-759f42bf3d5e', 3, 244, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí thừa (+), thiếu (-)', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7217ad6f-b410-43d9-8242-6cc94bd8869d', 1, 245, NULL, 2024, 1, N'admin', N'admin', N'KINH PHÍ QUẢN LÝ BHXH, BHYT', N'IV. ', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9f972bb1-7422-46d0-8d96-b3c53009c1f9', 3, 246, NULL, 2024, 1, N'admin', N'admin', N'Dự toán', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'ec100ed3-f560-4f7c-b79a-e36d6eeb8afd', 3, 247, 246, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'1.1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'4cf7291c-2ab6-4783-b5b1-867823ab5e68', 3, 248, 246, 2024, 1, N'admin', N'admin', N'Năm nay', N'1.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'1c69a2d6-f56a-46f3-afb9-d082ec816dc6', 3, 249, NULL, 2024, 1, N'admin', N'admin', N'Số kinh phí được cấp', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c18aa862-6742-4621-93e7-ab1fbfc4df15', 3, 250, 249, 2024, 1, N'admin', N'admin', N'Năm trước chuyển sang', N'2.1', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6d0c5c77-9133-4649-997c-57b7b7757fbb', 3, 251, 249, 2024, 1, N'admin', N'admin', N'Năm nay ', N'2.2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'c55b471f-0570-40d8-97c7-091fd144a22e', 3, 252, NULL, 2024, 1, N'admin', N'admin', N'Số quyết toán', N'3.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'f278284d-bec5-4e36-adb7-a47a7c15bf94', 3, 253, NULL, 2024, 1, N'admin', N'admin', N'Kinh phí cấp thừa (+), thiếu (-)', N'4.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'9d45e3eb-dbbd-4456-a8dd-3b181550ce1c', 3, 254, NULL, 2024, 1, N'admin', N'admin', N'Dự toán còn thừa', N'5.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'7c819899-02d8-42c2-a745-5d77c6b9e02b', 1, 255, NULL, 2024, 1, N'admin', N'admin', N'XÁC ĐỊNH CƠ SỞ TÍNH NỘP BHYT TỪ QUỸ BHXH', N'C.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'0cf29224-4177-4c87-a78a-332b1105605a', 3, 256, NULL, 2024, 1, N'admin', N'admin', N'Số tháng hưởng trợ cấp ốm đau thuộc danh mục bệnh cần chữa trị dài ngày không tính đóng BHXH', N'1.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'6b8a9f32-ce4e-4538-9b86-574a4b0b4f68', 3, 257, NULL, 2024, 1, N'admin', N'admin', N'Số tiền trợ cấp sinh con và nuôi con nuôi', N'2.', NULL)
GO
INSERT [dbo].[BH_DM_ThamDinhQuyetToan] ([iID], [iKieuChu], [iMa], [iMaCha], [iNamLamViec], [iTrangThai], [sNguoiSua], [sNguoiTao], [sNoiDung], [sSTT], [sXauNoiMa]) VALUES (N'3bfc4996-d0da-46a6-988d-a75bc01fa018', 1, 258, NULL, 2024, 1, N'admin', N'admin', N'GIẢI THÍCH SỐ LIỆU CHÊNH LỆCH (nếu có)', N'D.', NULL)
GO


/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/22/2024 5:21:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]    Script Date: 2/22/2024 5:21:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_quyet_toan_thu_chi_bhxh_bhyt_bhtn]
	@NamLamViec int ,
	@LstMaDonVi nvarchar(max),
	@DVT int
AS
BEGIN
	CREATE TABLE #result(STT nvarchar(50), IIdChungTu uniqueidentifier, IIdParent uniqueidentifier, SNoiDung nvarchar(200), ILevel int, IThuTu int, FSoTien float)
	DECLARE @IIdQTT uniqueidentifier = NewID();
	DECLARE @IIdQTC uniqueidentifier = NewID();
	DECLARE @IIdThuBHYT uniqueidentifier = NewID();
	DECLARE @IIdChiKcbBHYT uniqueidentifier = NewID();

	INSERT INTO #result(STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien)
	(
		--I Quyết toán thu
		SELECT 'I', @IIdQTT, NULL, N'Quyết toán thu', 1, 1, 0
		
		UNION ALL
		SELECT '1', NEWID(), @IIdQTT, N'Thu bảo hiểm xã hội (Phụ lục II)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (7,8,9,10,12,13,14,15, 19, 20, 21, 22, 24, 25, 26, 27, 29, 30,34, 35, 36, 37, 39, 40, 41, 42, 46, 47, 48, 49, 51, 52, 53, 54, 56, 57)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTT, N'Thu bảo hiểm thất nghiệp (Phụ lục III)', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (70, 71, 73, 74,77, 78, 80, 81)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', @IIdThuBHYT, @IIdQTT, N'Thu bảo hiểm y tế', 2, 3, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT quân nhân (Phụ lục IV)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (133, 134, 135, 136, 138, 139, 140, 141)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT người lao động (Phụ lục V)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (95, 96, 98, 99, 102, 103, 105, 106, 110, 111, 113, 114, 117, 118, 120, 121)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân quân nhân (Phụ lục VI)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 151
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT thân nhân CN', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 155
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT học sinh', 3, 5, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 163
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT lưu học sinh (Phụ lục VII)', 3, 6, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 167
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT HV QS xã phường (Phụ lục VII)', 3, 7, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 159
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdThuBHYT, N'BHYT SQ dự bị (Phụ lục VII)', 3, 8, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 171
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		--II Quyết toán chi
		UNION ALL
		SELECT 'II', @IIdQTC, NULL, N'Quyết toán chi', 1, 2, 0

		UNION ALL
		SELECT '1', NEWID(), @IIdQTC, N'Chi các chế độ BHXH (Phụ lục VIII)', 2, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa in (183, 184, 185, 186, 187, 188, 189, 190, 193, 194, 195, 196, 197, 198, 199, 200)
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '2', NEWID(), @IIdQTC, N'Chi KP quản lý BHXH', 2, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 252
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '3', NEWID(), @IIdQTC, N'Chi mua sắm TTB y tế (Phụ lục X)', 2, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 231
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '4', @IIdChiKcbBHYT, @IIdQTC, N'Chi KCB BHYT', 2, 4, 0

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)', 3, 1, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 238
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại quân y đơn vị (Phụ lục XII)', 3, 2, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 223
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)', 3, 3, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 209
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)', 3, 4, SUM(ISNULL(ctct.fSoThamDinh, 0))
		FROM BH_ThamDinhQuyetToan_ChungTuChiTiet ctct
		LEFT JOIN DonVi dv ON ctct.iID_MaDonVi = dv.iID_MaDonVi AND dv.iNamLamViec = @NamLamViec
		WHERE ctct.iNamLamViec = @NamLamViec 
			AND ctct.iMa = 215
			AND ctct.iID_MaDonVi IN (SELECT * FROM splitstring(@LstMaDonVi))
			AND dv.iTrangThai = 1
			AND dv.iKhoi = 2 --Khối dự toán

		UNION ALL
		SELECT '-', NEWID(), @IIdChiKcbBHYT, N'Chi KCB tại các cơ sở y tế (Phụ lục XV)', 3, 5, SUM(ISNULL(ctct.fQuyetToanQuyNay, 0))
		FROM BH_QTC_CapKinhPhi_KCB_ChiTiet ctct
		JOIN BH_QTC_CapKinhPhi_KCB ct ON ct.iID_ChungTu = ctct.iID_ChungTu
		WHERE ct.iNamLamViec = @NamLamViec 
			AND ct.iQuy = @NamLamViec
	)

	SELECT STT, IIdChungTu, IIdParent, SNoiDung, ILevel, IThuTu, FSoTien/@DVT FSoTien from #result

	DROP TABLE #result;

END
;
GO



DELETE FROM [dbo].[BH_DM_MucLucNganSach]
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'17d5947f-3be6-464f-a61d-00185ecaf4b0', N'9010001-010-011-0008-0001-0001-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.453' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'c1003754-545f-4a6b-acb1-ceb56a4a19c2', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2ea5bd76-f911-4e3b-ba62-00b4da71e298', N'9010003-010-011-6950', N'9010003', N'010', N'011', N'6950', N'', N'', N'', N'', N'Mua sắm tài sản dùng cho công tác chuyên môn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87309c2d-5489-4e52-90c2-a97779e3b5c0', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6fa6381f-0d10-4ac3-a33f-013b1d55dbf1', N'9010004-010-011-0002-0003', N'9010004', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.753' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d2fc884-12a6-45b6-8ecc-a2744f87ef34', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'43bceb51-1192-493a-bb5c-024bb4dee68a', N'9020002-010-011-0002-0001', N'9020002', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.457' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'88f8961b-fae7-41e4-8d7d-030cdb5761b0', N'9010001-010-011-0002-0001-0002-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.410' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'35853acc-f014-4116-8852-03c14a44c0ea', N'9010001-010-011-0004', N'9010001', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:34.100' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6c52487f-9da5-4010-8e76-04abb4d526bd', N'9010001-010-011-0002', N'9010001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:29.047' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cf2a4292-97c5-4d8e-bf8e-05bb48287aa8', N'9010010-010-011-0002-0005', N'9010010', N'010', N'011', N'0002', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd60de627-81e8-4052-91b0-53e6e5431eac', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fdca872b-3229-497b-981e-0640bfc3fa61', N'9010003-010-011-7000-7049', N'9010003', N'010', N'011', N'7000', N'7049', N'', N'', N'', N'Chi phí khác ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.213' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a6aa1af6-475c-4853-836f-c0bde339c5b8', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'35d461af-b0e0-4f6f-a96f-06475090a2c4', N'9010003-010-011-6650-6652', N'9010003', N'010', N'011', N'6650', N'6652', N'', N'', N'', N'Bồi dưỡng giảng viên, báo cáo viên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.393' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25a9efc8-4107-42ac-b3fa-00d1875a92d1', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3daa09df-70d9-4f1c-8dff-06b289affac5', N'9010001-010-011-0001-0003', N'9010001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.773' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dfc0c1a7-64a7-4cc6-ae50-06f8b209ba7f', N'9010001-010-011-0005-0001-0002-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.077' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'36c7cef2-d61f-4073-9fd1-f866a1375b19', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'27e3555b-4841-4ded-b3bc-07afb2b87501', N'9010010-010-011-0002-0003', N'9010010', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c086c298-f851-4958-bc76-4e4b37c77970', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f24e50e3-2291-4654-ac6d-08356551b87b', N'9010003-010-011-6600-6601', N'9010003', N'010', N'011', N'6600', N'6601', N'', N'', N'', N'Cước phí điện thoại (không bao gồm khoán điện thoại); thuê bao đường điện thoại; fax', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.377' AS DateTime), NULL, 0, NULL, NULL, NULL, N'943d7e3d-f8ff-4f8b-bab8-7a43dda8e71f', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'981e6bf6-6f9c-4005-9674-095158881be8', N'9010004-010-011-0002-0000', N'9010004', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.200' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8795dd68-21ff-4a22-b857-bec3d01f77cf', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'89a0fb98-3ebc-491b-b384-098e00265338', N'9010003-010-011-7750-7799', N'9010003', N'010', N'011', N'7750', N'7799', N'', N'', N'', N'Chi các khoản khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'65c08712-f281-4123-8efe-8848d21b220b', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9f9eb99f-e415-4af3-bc31-09f76a751807', N'9010002-010-011-0002-0001-0002-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:41.610' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'522cfce4-4187-45a1-b5d2-3bc01146f2a5', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'89633c55-ede4-4e99-9ca9-0a8dab73d8e1', N'9010001-010-011-0008-0001-0003-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'ebc5a51a-320c-4466-8a04-bfeb88e3c881', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'165aba4c-a1de-46a4-ba75-0b015b94e459', N'9010003-010-011-6650-6656', N'9010003', N'010', N'011', N'6650', N'6656', N'', N'', N'', N'Thuê phiên dịch, biên dịch phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.423' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e48cd68f-5928-4231-97eb-4eba9a644b2f', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2d4c28db-a64d-426a-8545-0b0e4bc28753', N'9010003-010-011-7000-7049-0005', N'9010003', N'010', N'011', N'7000', N'7049', N'0005', N'', N'', N'- Chi hỗ trợ bệnh viện, bệnh xá KCB BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:25.060' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'1c72986d-0c37-4e1e-b997-75b6d26bee60', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6dba18f0-314f-44f4-b9c1-0b26d503bbde', N'9010001-010-011-0003-0001-0001-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.103' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b6c195d7-cf0e-4add-8961-b2224e9f21b4', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'222f66ed-6ea5-4327-8742-0bcd0274aa6f', N'9010001-010-011-0008-0001-0002-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.690' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'01e7c76b-294b-4440-b555-d0b33af107cc', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6c3087f2-bdce-428a-85ff-0c0d68c978c7', N'901', N'901', N'', N'', N'', N'', N'', N'', N'', N'Chi các chế độ BHXH', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:59:39.573' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'519bcd42-015f-44ad-9ff4-0c643a0c5a6e', N'9010003-010-011-6650-6657', N'9010003', N'010', N'011', N'6650', N'6657', N'', N'', N'', N'Các khoản thuê mướn khác phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dea8ce64-0669-4f7c-9e53-09e0d5ffaa64', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c0b0f916-88eb-48a0-b3f3-0cd631467cfd', N'9010002-010-011-0005-0001-0002-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:46.663' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f9a5e84d-2048-4acc-9028-163e4a23a8f0', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8990568f-cdb2-4637-a5a5-0de0da68875d', N'9010001-010-011-0002-0001-0001-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:29.470' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7ae0d5ea-7a76-4d7e-9805-0dfb7c3e4c7c', N'9010004-010-011-0001-0001', N'9010004', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'48112d8e-a78f-4a4c-8492-94cf989a8f7a', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f305618b-bb50-4d22-ac5e-0e291a1b8f83', N'9010002-010-011-0004', N'9010002', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:45.060' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'69183e88-7f92-473e-a228-47d369f839e7', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b3f0763f-caa1-42f4-9d34-0e419ae73359', N'9010002-010-011-0008-0001-0003-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.900' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0f9b4ef0-dd90-4833-bbf5-ebff80e2a0a3', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f3aadea5-3ad5-4fb9-9480-0e464df32e25', N'9010002-010-011-0003-0001-0001-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.097' AS DateTime), NULL, 0, NULL, NULL, NULL, N'51e03272-5f23-4dc9-95f1-c5cd7ee125c2', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'73b2d5c5-eb5f-4ec7-bd49-0e7f478fad7d', N'9010003-010-011-6700-6749', N'9010003', N'010', N'011', N'6700', N'6749', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7bb79ca4-d725-4cf4-a28b-e2e3445e74d1', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'38e13b78-da28-4d8f-8992-0f340e8735be', N'9010001-010-011-0002-0001-0002-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.147' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9e0daf79-90d6-4ff0-a576-1028e6dd8c46', N'9010003-010-011-6600-6608', N'9010003', N'010', N'011', N'6600', N'6608', N'', N'', N'', N'Phim ảnh; ấn phẩm truyền thông; sách báo, tạp chí, thư viện ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ab29c1d-3216-4124-b989-42946c7cec46', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a43677d6-b6fb-448a-b2b7-10fe8f459faa', N'9030001-010-011-0001-0002', N'9030001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.493' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e4f18ba-73bc-4614-bd34-680af8bb4456', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9bed93d1-2291-4d38-b785-11c6e915a2f3', N'9010002-010-011-0002-0001-0002-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:41.610' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'522cfce4-4187-45a1-b5d2-3bc01146f2a5', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3ab052bf-a24c-4d4b-a01b-12cfe980668d', N'9010003-010-011-6650-6651', N'9010003', N'010', N'011', N'6650', N'6651', N'', N'', N'', N'In, mua tài liệu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.217' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3267c7d1-633c-4a4a-ac43-72be46a2d6ae', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'372d1ee3-441b-476c-b19a-13b25c11c9fd', N'9020001-010-011-0001-0000', N'9020001', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.220' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'1', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'801a2e6e-d337-430c-a1ff-14f96d9d85c0', N'9010002-010-011-0003', N'9010002', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:42.870' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'927a25e2-8a0b-4e1b-9c24-151bf11aaea2', N'9010006-010-011-0001-0003', N'9010006', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:40.710' AS DateTime), CAST(N'2023-11-29T08:59:28.967' AS DateTime), NULL, 0, NULL, NULL, NULL, N'35d2bc4b-f59b-4f9b-a96f-bf30529d48f7', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'48ab4354-db41-4d1e-87a0-15534f07af68', N'9030005', N'9030005', N'', N'', N'', N'', N'', N'', N'', N'V. BHYT học viên đào tạo sĩ quan dự bị từ 03 tháng trở lên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.890' AS DateTime), NULL, 0, NULL, NULL, NULL, N'68018115-052e-4d82-b8ab-e40648c1cf49', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5e02f580-d370-4118-b9cf-15607a412d98', N'9010003-010-011-7750-7799-0001', N'9010003', N'010', N'011', N'7750', N'7799', N'0001', N'', N'', N'- Chi thưởng cho tập thể, cá nhân thực hiện tốt công tác chi trả', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:26.017' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'994d7914-cf71-4767-99e4-697f10de2b78', N'65c08712-f281-4123-8efe-8848d21b220b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e9e76cdf-4107-4bbb-b3d1-15aabd868a76', N'9010004', N'9010004', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:26.243' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'99399384-55e9-45fd-b3cc-168faba252a4', N'9010001-010-011-0007', N'9010001', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:35.660' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'58a28603-1d42-40be-bbb3-56f7c15d69ef', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'20c8af7e-086c-45ac-9ded-16bea4344e90', N'9010001-010-011-0003-0001-0007-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:58:33.370' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8371001c-2996-42e3-8dcf-7a5ab4a0914c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e7604c61-6f21-4d02-8980-16e717373219', N'9010002-010-011-0002-0001-0003-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con(ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:41.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf731c26-af8a-4708-82de-c03bd8b38715', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f1fbdb78-732b-44eb-b677-17fbae28f817', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.503' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3a0a865f-65be-4e48-b99f-1895c322c533', N'9020002-010-011-0001', N'9020002', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.020' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c3086c8c-f8cb-4b26-886d-18e9306880ee', N'9010003-010-011-7000-7049-0001', N'9010003', N'010', N'011', N'7000', N'7049', N'0001', N'', N'', N'- Chi hỗ trợ cán bộ, nhân viên chuyên trách làm công tác BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:24.427' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'302f226e-4c2d-4c58-895b-313e987860ce', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'93cd381a-6465-47a4-88ab-1be66a332c55', N'9010002-010-011-0001-0001-0001-02-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.950' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6d2d85ba-b319-4704-8839-2f10cfdcb670', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ac813b7e-9662-4480-b034-1d57157580b2', N'9010003-010-011-6650-6699', N'9010003', N'010', N'011', N'6650', N'6699', N'', N'', N'', N'Chi phí khác ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9a6ab999-351c-4878-aa57-0807366a6f70', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b3dd39ee-673c-481c-8a3f-1d7952748676', N'9010003-010-011-6900', N'9010003', N'010', N'011', N'6900', N'', N'', N'', N'', N'Sửa chữa, duy trì tài sản phục vụ công tác chuyên môn và các công trình cơ sở hạ tầng ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.203' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87c7737b-0150-452a-b22e-02b356bd590f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'eb633061-6143-402b-8648-1d82225b3078', N'9020002-010-011-0001-0002', N'9020002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.877' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bfb54864-171a-443e-979d-1d8728a8b622', N'9010001-010-011-0003-0001-0002-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.327' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'795d2728-9ff9-4213-93f9-5e4bd22903e7', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ea02d9e3-6dfc-4852-9c2e-1e050d289ad4', N'9010001-010-011-0002-0001-0001-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:29.343' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1b54b3ec-989b-4828-acdd-1e36a08ed851', N'9010001-010-011-0004', N'9010001', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:34.100' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bf035163-9a82-445d-9088-1ebfac19fa17', N'9010010-010-011-0002-0001', N'9010010', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:14:36.670' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd3587b7-ba65-4bed-9cb3-6487d54a09e1', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6969d497-3eb4-4447-b65c-1edf76a8c134', N'9010001-010-011-0004-0001-0001-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.270' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5cca7dd1-b4e8-4273-86cc-ea4ff207409b', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6ca39a71-c278-4f55-934a-1fafdcb63f38', N'9010002-010-011-0001-0001-0001-01-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:37.923' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'bc6c9002-f7cc-47ad-8fe1-f80cab19ea01', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f60d23a6-ee8e-46d1-ae1a-206672fbbe95', N'9010010-010-011-0001-0003', N'9010010', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aea11eec-3be8-47eb-bb7c-7d7b339ba635', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bee75139-14f3-4002-a099-216e170fcd90', N'9010001-010-011-0002-0001-0001-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:29.623' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a28c1320-589d-4344-97f3-218775f2b723', N'9010006-010-011-0002', N'9010006', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:55.943' AS DateTime), CAST(N'2023-11-29T08:59:29.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'043d854e-bca5-4ee9-905a-8b78d15b9887', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'16104345-1099-43cf-81ee-226b68af03c8', N'9010003-010-011-7000-7049-0004', N'9010003', N'010', N'011', N'7000', N'7049', N'0004', N'', N'', N'- kiểm tra, xác minh, giám sát, quản lý đối tượng hưởng tại đơn vị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.940' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'd400e9c7-319a-4c0f-b449-a30ac04b8c41', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8f791e2e-d980-4d02-9295-23653f17885d', N'9010001-010-011-0003-0001-0001-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.103' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b6c195d7-cf0e-4add-8961-b2224e9f21b4', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ae1fda9a-e813-4eba-b2cc-241d3abbadd6', N'9010002-010-011-0003-0001-0007-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:58:44.337' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8dd7347a-25eb-4be1-b097-c0c6214a40e3', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f0a7201e-87ae-477d-bb49-24d9ae92c796', N'9010002-010-011-0001-0001-0001-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ Ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:54:34.480' AS DateTime), CAST(N'2023-11-29T08:58:38.367' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0c64a47a-ae28-472f-82f7-a2d818d107ba', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'aa236ee0-94e5-445d-b90a-250815450ca2', N'9030002-010-011-0001', N'9030002', N'010', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0989f265-2645-430c-9a78-b811b0a2f37f', N'9ad55244-03c4-4c97-ba26-27af54495842', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fdf9723a-257b-4e0d-85d3-26294415bee0', N'9030002-010-011-0000', N'9030002', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.127' AS DateTime), NULL, 0, NULL, NULL, NULL, N'49f6be60-0af9-41f8-8a0e-cac8b9b2dcfe', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'17382555-d450-4020-9c3a-26ee3b61d396', N'9030003', N'9030003', N'', N'', N'', N'', N'', N'', N'', N'III. BHYT học viên đào tạo cán bộ QS cấp xã, phường', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.467' AS DateTime), NULL, 0, NULL, NULL, NULL, N'449dec2d-4376-41c9-96fe-c054e152d4bb', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6a87caca-cca2-4865-b0fd-2758259fe9a7', N'9010003-010-011-6950', N'9010003', N'010', N'011', N'6950', N'', N'', N'', N'', N'Mua sắm tài sản dùng cho công tác chuyên môn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87309c2d-5489-4e52-90c2-a97779e3b5c0', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ab24d077-e4b0-420c-a9eb-27e623b818d5', N'9010010-010-011-0001', N'9010010', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-06T11:10:08.130' AS DateTime), NULL, 0, NULL, NULL, NULL, N'900d7456-023a-41c3-9380-c7054e410b71', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'03516089-0db2-440c-98e6-2809b0a2068c', N'9010003-010-011-6600', N'9010003', N'010', N'011', N'6600', N'', N'', N'', N'', N'Thông tin, tuyên truyền, liên lạc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'eff7df70-92d7-4db4-9ca8-2994b976272e', N'9040002', N'9040002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT thân nhân quân nhân và người lao động', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'503d8588-e537-4c72-a921-a437c1845d9e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'824e46f2-d57a-47ea-bd1e-2a7a6883c901', N'9010002-010-011-0002-0001-0001-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:40.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5caff718-8b78-4152-bb10-2b353fbd41de', N'9010003-010-011-6550-6599', N'9010003', N'010', N'011', N'6550', N'6599', N'', N'', N'', N'Vật tư văn phòng khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3a14e0af-be8c-4ee3-9279-afc0eb517462', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'111293b7-91b6-4c1a-b23d-2c0f32376d53', N'9010004', N'9010004', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại quân y đơn vị 10%', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:26.243' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'72fe1f07-f2e8-41dd-9d72-2d0d28c4a797', N'9010006-010-011-0002-0003', N'9010006', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd5d6fc7-35a0-41b7-a997-dd04923cfc69', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3,2', N'', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'af41decf-e834-426b-93cb-2f8f1dfa2772', N'9010003-010-011-7000-7012', N'9010003', N'010', N'011', N'7000', N'7012', N'', N'', N'', N'Chi phí hoạt động nghiệp vụ chuyên ngành ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.937' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7b143cca-0dad-4003-8708-d479f83fdd53', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'353debe4-7d38-4d02-9172-307ddbca8beb', N'9010001-010-011-0001-0001-0001-02-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.103' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'd0b64142-699e-4f9e-a6f5-824b03051c0e', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b557ba35-fdef-4555-aa9b-31b152e00a92', N'9010003-010-011-6600-6608', N'9010003', N'010', N'011', N'6600', N'6608', N'', N'', N'', N'Phim ảnh; ấn phẩm truyền thông; sách báo, tạp chí, thư viện ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.073' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6ab29c1d-3216-4124-b989-42946c7cec46', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3dcf2810-7e3f-4b04-9aa8-31ee1ef13c3b', N'9030001-010-011-0002-0001', N'9030001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2303978-ca39-4c7c-8ad1-2215f1d48fab', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9c243a97-86c9-4149-b256-32643968d4e9', N'9010003-010-011-6600-6649', N'9010003', N'010', N'011', N'6600', N'6649', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4edb565-5853-4cc0-8811-5fc92f1f306e', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1d185efe-1557-4ec6-b9c0-331e1019ab93', N'9010003-010-011-6550', N'9010003', N'010', N'011', N'6550', N'', N'', N'', N'', N'Vật tư văn phòng', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.157' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'04c9e8f3-a281-4567-a527-3357654662cb', N'9010003-010-011-6650', N'9010003', N'010', N'011', N'6650', N'', N'', N'', N'', N'Hội nghị', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c31a5537-d04a-4726-a71c-33f2777cc283', N'9010002-010-011-0002-0001-0001-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:40.837' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0cb5283f-1f2e-4c82-b7d9-9cab5c46c2cf', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'62f9bbb6-6851-4e2a-a836-34322693bbaf', N'9010002-010-011-0002', N'9010002', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:39.853' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'6f830377-5b2f-44d8-9a64-9d46c4270a80', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ca1fa978-66d3-4ff6-9018-3432a9f3d44b', N'9010002-010-011-0006-0001-0001-00', N'9010002', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:47.160' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fd8cb880-dd35-4db5-a71d-e47c97168672', N'd8fd23af-c23c-4b8f-8858-05fe8e304970', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'60ea82c3-fd48-4cf7-94ab-343f017bb533', N'9010010-010-011-0002-0003', N'9010010', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c086c298-f851-4958-bc76-4e4b37c77970', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'32cc84fc-e5b6-449d-863e-350cb892bfcb', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.153' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'1bf2509b-91d5-4974-aff0-7f7c426cddc4', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c3917bfd-1e6b-4f50-b5ae-35efbd4067c6', N'9010003-010-011-6750-6751', N'9010003', N'010', N'011', N'6750', N'6751', N'', N'', N'', N'Thuê phương tiện vận chuyển ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.817' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ed591dc-2b4a-44f6-9f2f-51dbac62e209', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fec0a03f-c32a-4470-8a0b-36c81355b119', N'9010006-010-011-0002-0000', N'9010006', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:29.763' AS DateTime), NULL, 0, NULL, NULL, NULL, N'40d58a93-4702-4a2b-8168-81a6fe18f12d', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0d90423a-ab94-403c-982e-3763c5987c32', N'9010002-010-011-0007-0001-0001-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:47.957' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'2907ac93-5edc-4c3b-a86d-37336769b417', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fe08da97-5b24-4414-84a4-38918e4c4335', N'9030006', N'9030006', N'', N'', N'', N'', N'', N'', N'', N'VI. BHYT người nước ngoài đang học trong các trường QĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1499a32d-8556-4ad5-bff4-81a307916605', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5f72e48e-d45e-4beb-ba07-392fccff60a1', N'9010001-010-011-0001-0001-0001-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-30T08:15:51.127' AS DateTime), CAST(N'2023-11-29T08:58:26.330' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', N'a655d431-de68-4238-b921-55850d8bba6b', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, N'TNG', NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'eb9dc4a8-4a21-41a0-b273-395fe9f16401', N'9010003-010-011-6700-6749', N'9010003', N'010', N'011', N'6700', N'6749', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.500' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7bb79ca4-d725-4cf4-a28b-e2e3445e74d1', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'276600cc-0fc4-4caa-aff9-3a2f8a7014ee', N'9010001-010-011-0008-0001-0003-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'ebc5a51a-320c-4466-8a04-bfeb88e3c881', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cc6daf1f-9e7e-4868-930d-3abf32a496bf', N'9010006-010-011-0002-0002', N'9010006', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.210' AS DateTime), NULL, 0, NULL, NULL, NULL, N'45438d5c-982d-41d5-aa67-d28213290663', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e8e4513e-2e6b-4b7a-b074-3b75b9292508', N'9010001-010-011-0003-0001-0006-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.137' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'6a99dbd6-a4ca-49f3-b3d3-0014c20a9a2d', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c38224c8-a0e4-4e34-8ff5-3bab12ed48f9', N'9010002-010-011-0001-0001', N'9010002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.437' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'80182334-ba66-48b7-ac6c-3bcea96f3afa', N'9010010-010-011-0001-0005', N'9010010', N'010', N'011', N'0001', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1e857155-0d72-4768-9fdd-7e0bb8c59200', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'358b27d0-c9ea-4e6d-913c-3c4f0bf9626b', N'9010001-010-011-0002-0001-0004-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:31.680' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3c12c48e-b0e8-49a5-bc06-3c5e0790916d', N'9040001', N'9040001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT quân nhân', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6173fe68-9e45-4c21-92a3-309afb77f73e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd738e1a6-6ded-4421-acc7-3cd2556932d4', N'9030004', N'9030004', N'', N'', N'', N'', N'', N'', N'', N'IV. BHYT học sinh, sinh viên hệ dân sự', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf9bf98b-3dde-44a9-8a71-dc0e83df2b5e', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'69ffdc53-d705-44b7-9c7d-3e6663a6ce83', N'9010002-010-011-0003-0001-0003-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0426ed87-f0f1-4142-8275-c21ed650e5a7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f983ffa7-e58c-4b46-a4b4-3ea58e416a36', N'9020001-010-011-0001-0000', N'9020001', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.220' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e06ad864-6b29-4f0e-bd3b-3e617e9dfda3', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'1', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ef6c2d51-9059-45f3-877d-3f2fe95ae1f8', N'9010002-010-011-0002', N'9010002', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:39.853' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'6f830377-5b2f-44d8-9a64-9d46c4270a80', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'21742a94-3c2e-4a56-b196-3fa5bc96f9f0', N'9020001-010-011-0001-0002', N'9020001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:31.870' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'4', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4712ce9-78ba-43fa-9027-3fbedab35231', N'9030004', N'9030004', N'', N'', N'', N'', N'', N'', N'', N'IV. BHYT học sinh, sinh viên hệ dân sự', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf9bf98b-3dde-44a9-8a71-dc0e83df2b5e', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bf6abfc1-28e8-435d-ab83-43699600317b', N'9030002-010-011-0001', N'9030002', N'010', N'011', N'0001', N'', N'', N'', N'', N'2. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.243' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0989f265-2645-430c-9a78-b811b0a2f37f', N'9ad55244-03c4-4c97-ba26-27af54495842', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e00ea3dd-9830-4096-9dad-43ac91f6b318', N'9010003-010-011-6650-6655', N'9010003', N'010', N'011', N'6650', N'6655', N'', N'', N'', N'Thuê hội trường, phương tiện vận chuyển', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a7bfb1a0-f712-45bc-9a4d-41f850a4fd52', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dc5444e9-93c6-4269-99d9-465d2e7f9cef', N'9010002-010-011-0002-0001-0003-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.340' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'01eb6c66-54b8-4f62-9b59-b0ea749cb180', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f10258c4-9866-4db4-8894-46b4da8d373b', N'9010003-010-011-7000-7049-0003', N'9010003', N'010', N'011', N'7000', N'7049', N'0003', N'', N'', N'- Đối chiếu danh sách, bảng lương, đôn đốc thu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'039a4045-76a1-4ae0-927d-e42c4a021223', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'24b50998-665f-4bf1-ad0d-46be0e521eb9', N'9010001-010-011-0003-0001-0003-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.567' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4c020fb4-019c-4296-ba41-f4bf9488a33e', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'59ebde12-19f9-4fe5-9671-47b2b70868d3', N'9010003-010-011-7000-7001', N'9010003', N'010', N'011', N'7000', N'7001', N'', N'', N'', N'Chi phí hàng hóa, vật tư ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ea128ac8-2407-4705-a4ba-5fc7c3b0c26a', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5ee996ed-22b2-422e-a621-47d6c7539044', N'9010009-010-011-0002', N'9010009', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e0756e6f-a6bf-42cb-972e-f40b1ce667e9', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'89c0d0c2-b593-4be8-bc7f-4921d5ce6c2d', N'9030001-010-011-0001', N'9030001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:34.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'800fbf3c-386c-40c7-a6c3-2b1251a94009', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6cf97360-9948-4f60-a77e-49748bf84da4', N'9010001-010-011-0001', N'9010001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:25.797' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'acdb04d9-2ee2-4c3c-b9ef-4a2051a0227d', N'9020002-010-011-0002-0000', N'9020002', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c0e26acc-1590-456a-93ce-4c2d72e5ff97', N'9010003-010-011-6700', N'9010003', N'010', N'011', N'6700', N'', N'', N'', N'', N'Công tác phí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.353' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4af2b9e5-b4c8-44a3-97c1-4d60e0581543', N'9020002-010-011-0002-0001', N'9020002', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.457' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9eb683c3-3d96-44c9-9b10-4f947da34008', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0c0060c6-4595-40f6-935f-4f2fe796dd16', N'9010003-010-011-6700-6704', N'9010003', N'010', N'011', N'6700', N'6704', N'', N'', N'', N'Khoán công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73da6baf-f315-4c97-ac06-346fe53255f0', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cbc103eb-65f0-41d8-9783-50860347b308', N'9010003-010-011-6900-6913', N'9010003', N'010', N'011', N'6900', N'6913', N'', N'', N'', N'Tài sản và thiết bị văn phòng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9f4276eb-b9d2-4554-a96c-45e8c7d0ff64', N'87c7737b-0150-452a-b22e-02b356bd590f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bc4cb332-e24c-4f5f-a5fd-510ba91ee25a', N'9010002-010-011-0008-0001-0003-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0003', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.900' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0f9b4ef0-dd90-4833-bbf5-ebff80e2a0a3', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd43037b4-4896-4221-b898-513cc94644a7', N'9010010-010-011-0002', N'9010010', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:16:44.000' AS DateTime), CAST(N'2023-12-06T11:13:39.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'61dbbe45-bf9f-4ed5-b5d4-51f07d46bd79', N'9010006-010-011-0002-0003', N'9010006', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd5d6fc7-35a0-41b7-a997-dd04923cfc69', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3,2', N'', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dde4e093-1b77-4880-ab57-5286e4bb9285', N'9030003', N'9030003', N'', N'', N'', N'', N'', N'', N'', N'III. BHYT học viên đào tạo cán bộ QS cấp xã, phường', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.467' AS DateTime), NULL, 0, NULL, NULL, NULL, N'449dec2d-4376-41c9-96fe-c054e152d4bb', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c08a38c6-dd5d-4011-95a9-529213fb7ce1', N'9010003-010-011-6600-6605', N'9010003', N'010', N'011', N'6600', N'6605', N'', N'', N'', N'Thuê bao kênh vệ tinh; thuê bao cáp truyền hình; cước phí Internet, thuê đường truyền mạng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd6d5d9a4-d07f-406a-bf3c-08d6efb181b0', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'88b41bd0-7e76-4e17-9738-52a81e48c6e6', N'9050001-010-011-0001', N'9050001', N'010', N'011', N'0001', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu người lao động ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T17:04:13.200' AS DateTime), CAST(N'2023-11-29T08:59:39.073' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a519c162-3f2e-47ef-9cdf-2da72cfa40b7', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a9ffb064-9126-4ca4-9c22-52bca0cc4c6f', N'9010006-010-011-0001-0000', N'9010006', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.380' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e3b3fc59-1030-41d6-ae90-3c3dd8fe6d1c', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd13c13bb-beff-46b0-bd54-52e0134dda84', N'9010002-010-011-0008-0001-0001-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.577' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b66ca2a3-645d-4879-a0ca-6e4cb3c9b442', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'88ae5b21-20b5-4269-b86f-535357851b4d', N'9030001-010-011-0002-0001', N'9030001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2303978-ca39-4c7c-8ad1-2215f1d48fab', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'751dba7d-6d54-4d0b-8a94-5401fa5ac8f6', N'9010002-010-011-0001-0001', N'9010002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.437' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7bc47a23-8c2b-43df-a58e-54dbe96c2be3', N'9010003-010-011-6550', N'9010003', N'010', N'011', N'6550', N'', N'', N'', N'', N'Vật tư văn phòng', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.157' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'51936b47-9812-422c-969d-550d280aa2c0', N'9010002-010-011-0002-0001-0003-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.340' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'01eb6c66-54b8-4f62-9b59-b0ea749cb180', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'47a7d822-8faf-4ba9-b652-551c87c95c67', N'9010010-010-011-0001-0004', N'9010010', N'010', N'011', N'0001', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aa54fe2b-7318-4dca-baa9-43a4f7ac9a57', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8dd80c9c-aa3f-418d-b87a-55864f640558', N'9010003-010-011-6600-6606', N'9010003', N'010', N'011', N'6600', N'6606', N'', N'', N'', N'Tuyên truyền (phát thanh, truyền hình)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f42345ce-969b-482c-a5f5-570b0d4cdf36', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e98f7dcc-9ee3-4673-a441-5659650db6a3', N'9010001-010-011-0003-0001-0003-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.567' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4c020fb4-019c-4296-ba41-f4bf9488a33e', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8cd73281-f90e-40a9-bd47-56c3f86f8ad1', N'9010001-010-011-0001', N'9010001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:25.797' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'63e1e32a-460b-4696-aa73-441934842ac0', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6c72d51b-54f5-4dc8-bc21-5720a5be6605', N'9010004-010-011-0002-0001', N'9010004', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.350' AS DateTime), NULL, 0, NULL, NULL, NULL, N'600f74a3-06c7-4c4e-8859-69a8ab9d212c', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e807ce48-f585-4ebb-9461-5734e7d59e8e', N'9010003-010-011-6750', N'9010003', N'010', N'011', N'6750', N'', N'', N'', N'', N'Chi phí thuê mướn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.703' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8f8bc768-d3de-4f34-b8c8-57c258e92b9d', N'9010002-010-011-0003', N'9010002', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:42.870' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'653686ff-0f35-4a68-afc2-57ed2abc139a', N'9010003-010-011-7000-7049-0002', N'9010003', N'010', N'011', N'7000', N'7049', N'0002', N'', N'', N'- Chi phối hợp kiểm tra, thanh tra, phúc tra, giám sát công tác thu, chi BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.600' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'874cd699-e8bb-4d5d-96cc-fc2ef63e3f26', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2d1277aa-8419-4d93-a730-58cfb5b93c72', N'9010003', N'9010003', N'', N'', N'', N'', N'', N'', N'', N'Chi kinh phí quản lý BHXH, BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:40.060' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'885f40fa-4c4d-4ef2-ab31-b075853b028f', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'caef417e-5e89-42da-b5dd-5919b19fb2a4', N'9010001-010-011-0003-0001-0009-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.873' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'77ec4558-40fd-4d3b-864d-df96aceccb53', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3c438d1e-874b-4843-9811-5954fee00a9b', N'9010003-010-011-6650-6652', N'9010003', N'010', N'011', N'6650', N'6652', N'', N'', N'', N'Bồi dưỡng giảng viên, báo cáo viên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:18.393' AS DateTime), NULL, 0, NULL, NULL, NULL, N'25a9efc8-4107-42ac-b3fa-00d1875a92d1', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4b9acea9-4992-4d70-b181-59d021663071', N'9010002-010-011-0007-0001-0002-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.113' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b23ab43a-001b-4ce2-9f39-0be3ef79c50e', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'61c22526-faac-4f83-9a70-5aaca587da4b', N'9010003-010-011-6600-6618', N'9010003', N'010', N'011', N'6600', N'6618', N'', N'', N'', N'Khoán điện thoại', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'781048d2-925a-4fc9-b0c7-752d42b49bd4', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7f1aadc8-a7f4-4463-96da-5b35961b15cb', N'9010002-010-011-0001-0003', N'9010002', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:39.520' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'866cedc7-d4a3-4b0e-bbb9-ebb5b227b413', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5844a613-f59f-48e6-863d-5c22bd28bf8e', N'9020001-010-011-0001-0001', N'9020001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.440' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'2', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'78fbdd54-2755-4550-9aa4-5cd97f3a561f', N'9010002-010-011-0007', N'9010002', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:47.750' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'774d1074-d499-4f15-9ac0-40b04ad1ba17', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2af4d1d3-214d-47e6-b5af-5ce743d30fba', N'9020001-010-011-0001', N'9020001', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'240b80d7-8604-4ef3-9a63-5d20e9b6850b', N'9010001-010-011-0006-0001-0001-00', N'9010001', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.400' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'08c2bc4d-af7e-406e-83f3-74f9a1a353d1', N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'78b5eee4-8a0d-4898-b7d2-5d6cbb8fd89f', N'9010003-010-011-6650-6651', N'9010003', N'010', N'011', N'6650', N'6651', N'', N'', N'', N'In, mua tài liệu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.217' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3267c7d1-633c-4a4a-ac43-72be46a2d6ae', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e0acad29-8136-4b60-9d83-5dc5ab246c10', N'9030001-010-011-0001-0003', N'9030001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.803' AS DateTime), NULL, 0, NULL, NULL, NULL, N'100ef748-1f6f-4891-944e-4a11b59a0fcf', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'460b101e-b9f7-4fc5-bcd0-5e8c6da7850b', N'9010002-010-011-0003-0001-0004-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'46c2b17e-c44e-43df-9c08-ce6f6ffbf415', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'af4fc847-1bd1-4d47-b4f8-5f54f4129c5d', N'9020001-010-011-0001', N'9020001', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.963' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'10882797-7fb6-4e10-b786-5fe6fb9839a8', N'9010001-010-011-0001-0001', N'9010001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-26T10:19:33.593' AS DateTime), CAST(N'2023-11-29T08:58:26.100' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TTM', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd29703ea-ac61-48f8-9ee3-601c4769771d', N'9020002-010-011-0001-0000', N'9020002', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bfd3a22b-8b4f-4dc0-84ef-6094a0a3bf44', N'9030001-010-011-0002-0002', N'9030001', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'adcb8096-366c-4729-8a51-1b40eaf78af2', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'090baaea-dd55-4ef3-9fa5-60c62882785a', N'9010003-010-011-7000', N'9010003', N'010', N'011', N'7000', N'', N'', N'', N'', N'Chi phí nghiệp vụ c.môn của từng ngành', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0e72edc7-a432-4548-b653-60cb9cde2351', N'9030001-010-011-0001-0003', N'9030001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.803' AS DateTime), NULL, 0, NULL, NULL, NULL, N'100ef748-1f6f-4891-944e-4a11b59a0fcf', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'60b35933-e8ff-40bc-9d8d-6250c01e06ca', N'9010002-010-011-0002-0001-0001-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:40.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ed189e6c-1241-43ac-81a4-626cf840cdca', N'9030001-010-011-0002-0003', N'9030001', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.643' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77b53c15-3232-4207-83be-759cbeeb098b', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cee7afe7-fe19-4b7c-978d-626ee5a58c97', N'9010002-010-011-0002-0001-0002-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.190' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f89c0f16-bd77-480d-866c-62e4f0bb7cc3', N'9010003-010-011-6600-6605', N'9010003', N'010', N'011', N'6600', N'6605', N'', N'', N'', N'Thuê bao kênh vệ tinh; thuê bao cáp truyền hình; cước phí Internet, thuê đường truyền mạng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.823' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd6d5d9a4-d07f-406a-bf3c-08d6efb181b0', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cccb6c14-3ca5-4315-b564-63006b646f35', N'9010010', N'9010010', N'', N'', N'', N'', N'', N'', N'', N'Chi hỗ trợ người lao động tham gia BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2004b2e2-58b0-43b9-9f87-64639eccaa4e', N'9030001', N'9030001', N'', N'', N'', N'', N'', N'', N'', N'I. Bảo hiểm y tế thân nhân quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c52e8ab-654e-4b86-afbc-165823b677a2', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8a0086ec-4ac8-4f13-96a2-6483557b7fc4', N'9010006-010-011-0002-0000', N'9010006', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:29.763' AS DateTime), NULL, 0, NULL, NULL, NULL, N'40d58a93-4702-4a2b-8168-81a6fe18f12d', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'643ca4c3-4e8b-48e9-9424-648bf25ece37', N'9010001-010-011-0003-0001-0005-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.960' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'e981466a-d098-40b2-be26-78989d31bd5c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7df6aadd-369d-4a8c-a455-6528c0117ced', N'9030001-010-011-0002-0002', N'9030001', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.483' AS DateTime), NULL, 0, NULL, NULL, NULL, N'adcb8096-366c-4729-8a51-1b40eaf78af2', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4512faa3-6a42-46a2-a58b-6543c3123813', N'9010002-010-011-0007', N'9010002', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:47.750' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'774d1074-d499-4f15-9ac0-40b04ad1ba17', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'82c4c83a-31e7-4857-a040-655851115772', N'905', N'905', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí CSSK ban đầu NLĐ và HSSV', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-02T17:10:10.803' AS DateTime), CAST(N'2023-11-29T08:59:41.117' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'2cdf8f93-5d04-45f8-afcc-5100068321e4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'M', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'22c3105a-ea01-40c4-a5cb-65ac1c7f5cc9', N'9010001-010-011-0005', N'9010001', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:34.693' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1da67c9f-2025-4bd0-9d24-20fa64651658', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e8f534be-b185-4e79-bf74-66aef439bd82', N'9010002-010-011-0008', N'9010002', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:48.327' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'e56cedb7-ed03-485b-beab-c152f42ebadd', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'29355601-8477-4510-ae7c-66af347ce556', N'9010001-010-011-0001-0001-0001-02-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.920' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'ba487675-57b4-4b66-a1f4-27d7b6fbb495', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'92dba78d-5153-4b2e-aaff-67c2e4074aaf', N'9010004-010-011-0001-0002', N'9010004', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:30.947' AS DateTime), CAST(N'2023-11-29T08:59:41.627' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b7898495-5a08-4079-834f-f12d2e7939d6', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cad86ef8-d4f7-495b-9521-67c89c5dda93', N'9010006', N'9010006', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:27.963' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b9013d9c-4fbb-426e-8cc8-6809657957f1', N'9010003-010-011-6750', N'9010003', N'010', N'011', N'6750', N'', N'', N'', N'', N'Chi phí thuê mướn ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.703' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'58651844-ec72-4563-bc8f-680e2889fbf4', N'9030001-010-011-0002', N'9030001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:36.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a1c989de-eac9-4153-8a60-a34ac387bb88', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'39abfaca-2af1-4080-976b-690663816169', N'9010003-010-011-6650-6653', N'9010003', N'010', N'011', N'6650', N'6653', N'', N'', N'', N'Tiền vé máy bay, tàu xe (đối với đại biểu là khách)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.687' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'5fd8d523-3afd-45ff-85bc-80c96d602cb8', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'55fdfed6-e60a-41db-b259-6966f9e929db', N'9010002-010-011-0004', N'9010002', N'010', N'011', N'0004', N'', N'', N'', N'', N'4. Hưu trí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:45.060' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'69183e88-7f92-473e-a228-47d369f839e7', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4066d74-973c-4709-9627-6a695941d9a8', N'9010003-010-011-7750-7756', N'9010003', N'010', N'011', N'7750', N'7756', N'', N'', N'', N'Chi các khoản phí và lệ phí ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc5851db-3788-461b-b364-acc87fa0cca0', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f5d1dcbe-e9ba-495b-a306-6ab17e959e2b', N'9010002-010-011-0004-0001-0002-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:45.550' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'939834ed-9ed7-406a-8cc6-b8d4b2930c66', N'69183e88-7f92-473e-a228-47d369f839e7', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'888004a8-a4a1-40c4-a511-6b88cb1ec9b7', N'9010010-010-011-0002-0002', N'9010010', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:16:44.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f657c41-02bf-45e6-9e47-776c9040b46f', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7b9180d4-c30c-4662-9698-6c5e2bdea7e2', N'9010010-010-011-0001-0004', N'9010010', N'010', N'011', N'0001', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aa54fe2b-7318-4dca-baa9-43a4f7ac9a57', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2db7cd5b-b2a0-4a18-9e9a-6cb64b7983b9', N'9010002-010-011-0001-0002', N'9010002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:39.173' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'63e1bfb4-2e48-4b3e-b773-0be4cd4a194e', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e5efb28a-4113-4468-b04d-6cd72babffe9', N'9010003-010-011-6900', N'9010003', N'010', N'011', N'6900', N'', N'', N'', N'', N'Sửa chữa, duy trì tài sản phục vụ công tác chuyên môn và các công trình cơ sở hạ tầng ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.203' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87c7737b-0150-452a-b22e-02b356bd590f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4c38dfd8-092a-46e2-ab4f-6d3072354d94', N'9010001-010-011-0002-0001-0002-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.410' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e1859298-643f-4f14-a387-231474454b25', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'685bb2a6-8467-4446-8b75-6da4fb9e7d63', N'9010001-010-011-0001-0002', N'9010001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.497' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ad9e8ce2-573b-4abf-83c1-6dc2a0351b6f', N'9010001-010-011-0002-0001-0001-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:29.470' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b9de30f6-3364-47e3-ab8a-a476893817df', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3cfeceb8-68de-4aa9-bec2-6e1f657c8b50', N'9010010-010-011-0001-0002', N'9010010', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'884e38f9-ce43-49f6-9f77-37969ab18a81', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e0716aab-8dbc-4f7c-8f05-6e5559264c01', N'9010003-010-011-7000-7049-0004', N'9010003', N'010', N'011', N'7000', N'7049', N'0004', N'', N'', N'- kiểm tra, xác minh, giám sát, quản lý đối tượng hưởng tại đơn vị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.940' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'd400e9c7-319a-4c0f-b449-a30ac04b8c41', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'99590a09-90db-4e20-9057-6eb41c08d3a1', N'9010002-010-011-0005-0001-0001-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:46.340' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'da87b860-e7a5-4a0a-a1ae-d03ea1b81de9', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8660ec00-f284-4cb0-850f-6f93dca1a662', N'9010002-010-011-0001', N'9010002', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:37.250' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'62ab59f2-7516-408f-9c0a-71c8030ff3d4', N'9010003-010-011-6550-6552', N'9010003', N'010', N'011', N'6550', N'6552', N'', N'', N'', N'Mua sắm công cụ, dụng cụ văn phòng', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bf5ffd8c-e50f-4843-a003-156c9e2a5eba', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4d6db3fc-67ef-4332-9446-71f701301025', N'9020002-010-011-0001-0002', N'9020002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.877' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ceec043e-4468-4128-9b09-18cf4b53b025', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4a82d7a8-6e16-4d69-ab4e-72006802e6fb', N'9010001-010-011-0007-0001-0002-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.067' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f10c833a-c6a4-48d9-82ee-51682f8abe0b', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'45af5d64-a392-4783-a9b0-723468cb02a4', N'9010001-010-011-0005-0001-0001-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b4187139-caae-46f6-a079-343810e05db2', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'13221d72-241c-4ac5-abe6-72e69da219b5', N'9010002-010-011-0003-0001-0008-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.560' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5ce55373-18e4-43c6-965d-508d72db9ab5', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6b31cd94-4946-48bc-9846-73ee72cf03fc', N'9010002', N'9010002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.747' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7a097777-89ac-4d76-a856-d1534c4070a9', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cac1f3c0-0e89-4ec8-a67a-747ef0b1104d', N'9030002-010-011-0000', N'9030002', N'010', N'011', N'0000', N'', N'', N'', N'', N'1. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.127' AS DateTime), NULL, 0, NULL, NULL, NULL, N'49f6be60-0af9-41f8-8a0e-cac8b9b2dcfe', N'9ad55244-03c4-4c97-ba26-27af54495842', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9af17729-3658-4aed-ace6-7584b68ba1f9', N'9010001-010-011-0003-0001-0008-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.600' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'294076d4-8afb-404d-929d-3d6ecc396771', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd85ea20c-d3ce-4bf1-ad63-76d451d2bef2', N'9010003-010-011-7000-7049', N'9010003', N'010', N'011', N'7000', N'7049', N'', N'', N'', N'Chi phí khác ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.213' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a6aa1af6-475c-4853-836f-c0bde339c5b8', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6d0c2ab0-4924-45ea-8ae3-76fae6576d32', N'9010002-010-011-0001-0001-0001-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.650' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ebe3f5e-f6e8-4268-b744-583b24731221', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dd513dfb-b4cb-411f-85ee-76fc182276aa', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1af7699f-403e-426b-88d4-77bd891214a8', N'9010001-010-011-0006-0001-0001-00', N'9010001', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.400' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'08c2bc4d-af7e-406e-83f3-74f9a1a353d1', N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'59144718-1133-4ca2-b13a-77be25d00d7c', N'9010002-010-011-0003-0001-0001-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0001', N'00', N'', N'- Chi giám định mức suy giảm KNLĐ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.097' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'51e03272-5f23-4dc9-95f1-c5cd7ee125c2', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'19ac0b94-9034-445a-b845-77db2b916513', N'9010003-010-011-7750-7799', N'9010003', N'010', N'011', N'7750', N'7799', N'', N'', N'', N'Chi các khoản khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.717' AS DateTime), NULL, 0, NULL, NULL, NULL, N'65c08712-f281-4123-8efe-8848d21b220b', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b84cec52-6864-49d6-9653-77e6b2d79573', N'9010003-010-011-6700-6704', N'9010003', N'010', N'011', N'6700', N'6704', N'', N'', N'', N'Khoán công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.230' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73da6baf-f315-4c97-ac06-346fe53255f0', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a3cc3174-7e34-494c-9292-7847b2c7f133', N'9010003-010-011-6600-6649', N'9010003', N'010', N'011', N'6600', N'6649', N'', N'', N'', N'Khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.610' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd4edb565-5853-4cc0-8811-5fc92f1f306e', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'58d50836-ef9a-4a38-bbab-78cec2dcb9ee', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'72d7a943-3288-4175-be6f-cf5190d2b908', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'40b11e43-4bcb-40ee-89e3-7973824b04b8', N'902', N'902', N'', N'', N'', N'', N'', N'', N'', N'Thu BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.757' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cdd4399b-5a90-4c34-8597-799be35410f1', N'9030001-010-011-0001-0002', N'9030001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'b. Bảo hiểm y tế thân nhân QNCN (thân nhân CMCY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.493' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8e4f18ba-73bc-4614-bd34-680af8bb4456', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'744fe211-8755-457b-ade7-79c85b06e461', N'9010003-010-011-6900-6913', N'9010003', N'010', N'011', N'6900', N'6913', N'', N'', N'', N'Tài sản và thiết bị văn phòng ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:22.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9f4276eb-b9d2-4554-a96c-45e8c7d0ff64', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'19e89c79-dc76-4fd7-b5ae-7a469d0b356a', N'9020002-010-011-0001-0001', N'9020002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5c177532-2698-43ac-b573-7ab596bf8f8f', N'9010010-010-011-0002-0006', N'9010010', N'010', N'011', N'0002', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4d4aa749-e38b-4b43-b3fc-fe999026fe4a', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9c9a5719-ba19-43d0-809c-7b67d7aa3daf', N'9010003-010-011-7750-7756', N'9010003', N'010', N'011', N'7750', N'7756', N'', N'', N'', N'Chi các khoản phí và lệ phí ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bc5851db-3788-461b-b364-acc87fa0cca0', N'c60199d0-3566-4c28-91d5-cbde4cd4f792', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'85364497-e902-404a-917b-7c2c4a742944', N'9010001-010-011-0005-0001-0001-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b4187139-caae-46f6-a079-343810e05db2', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'89468b22-3e35-4707-ab9d-7c33b54f8818', N'9010010-010-011-0001-0001', N'9010010', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2577e235-3102-478d-8471-3bf14f0b8aca', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'35c1ab01-b901-4886-854a-7d60efeb1289', N'9010003-010-011-6750-6799', N'9010003', N'010', N'011', N'6750', N'6799', N'', N'', N'', N'Chi phí thuê mướn khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5b7a1ead-90a1-42c1-91f3-ef688f45f268', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8cbcc0e7-7a38-495c-9300-7d7bae05b236', N'9010003-010-011-6700-6702', N'9010003', N'010', N'011', N'6700', N'6702', N'', N'', N'', N'Phụ cấp công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.787' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4436044d-d91e-4f5f-819d-f9aa1290e0f9', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e9ecd0c6-2470-4593-8b03-7e63500f0a24', N'9010001-010-011-0004-0001-0002-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.500' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'18fcd280-fd4c-405c-b1a1-25d438da44ba', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7e5c06cf-2799-46b1-9637-7ec0a1c280bc', N'9010003-010-011-6600', N'9010003', N'010', N'011', N'6600', N'', N'', N'', N'', N'Thông tin, tuyên truyền, liên lạc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.143' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7649b9cf-001a-4192-b4c2-7f0510f6963d', N'9010003-010-011-6700-6703', N'9010003', N'010', N'011', N'6700', N'6703', N'', N'', N'', N'Tiền thuê phòng ngủ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5d24dd88-913a-4eb5-b2e6-b0e767ea80a4', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b81bb933-e1be-4ef9-8afa-7f297157e12e', N'9010001-010-011-0002-0001-0003-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:30.657' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2b24ed05-a417-4855-a5f4-7f4b8530e29a', N'9010002-010-011-0002-0001-0001-00-02', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:40.837' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0cb5283f-1f2e-4c82-b7d9-9cab5c46c2cf', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a2172914-986d-4f57-a2e4-7f895de6ba50', N'901', N'901', N'', N'', N'', N'', N'', N'', N'', N'Chi các chế độ BHXH', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T19:06:02.973' AS DateTime), CAST(N'2023-11-29T08:59:39.573' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'656f17cd-0836-4183-a1dc-7f94b56f3023', N'9010002-010-011-0001-0001-0001-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:37.650' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ebe3f5e-f6e8-4268-b744-583b24731221', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4b1389ce-23ed-4713-82dc-8151eac0e615', N'9010002-010-011-0006', N'9010002', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:46.940' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'd8fd23af-c23c-4b8f-8858-05fe8e304970', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6374fc07-c6b7-4921-8d13-81525009c53c', N'9010002-010-011-0002-0001-0004-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.603' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'097b3961-33a9-4e67-a9cb-10cdf604d8dc', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f2abadac-093a-4b69-b332-815faecbae8d', N'9010010-010-011-0002-0004', N'9010010', N'010', N'011', N'0002', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e2eb178-63f3-496b-8457-179787e01d72', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8ffe18f6-8353-4bc1-b947-8199627cd9aa', N'9020002-010-011-0002', N'9020002', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6ff27db9-eb51-4eec-ac4f-8268a6801e82', N'9010006-010-011-0001-0002', N'9010006', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1ac0849-a18f-4c20-89e2-0ce336d197bf', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd2930d1a-7b03-4916-b6d9-82b6a22fdec7', N'9050002-010-011-0002', N'9050002', N'010', N'011', N'0002', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu học sinh sinh viên ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T17:04:13.200' AS DateTime), CAST(N'2023-11-29T08:59:39.300' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'cf3d919f-997c-4f35-abed-40d1ef5f1f51', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cb776730-b094-4904-89ef-833d984e0680', N'9010002-010-011-0005-0001-0002-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.663' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f9a5e84d-2048-4acc-9028-163e4a23a8f0', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'045dbe48-489c-4c88-915f-84b57098e43f', N'9010001-010-011-0002-0001-0002-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:49:01.630' AS DateTime), CAST(N'2023-11-29T08:58:29.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1f349d44-40c8-4b43-9068-85d7d6503ca2', N'9010006-010-011-0001', N'9010006', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:51.650' AS DateTime), CAST(N'2023-11-29T08:59:28.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8088e295-2f9c-45b3-a799-d88940922713', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5a419ea9-eff5-41c6-bfea-865abfc0742e', N'9010001-010-011-0005-0001-0002-00', N'9010001', N'010', N'011', N'0005', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.077' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'36c7cef2-d61f-4073-9fd1-f866a1375b19', N'1da67c9f-2025-4bd0-9d24-20fa64651658', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd6930338-4746-4b52-8c85-8864718a597c', N'9010002-010-011-0007-0001-0002-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.113' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'b23ab43a-001b-4ce2-9f39-0be3ef79c50e', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cc505ac2-a200-45f5-b021-890f1fb8c020', N'9010008', N'9010008', N'', N'', N'', N'', N'', N'', N'', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'73a85b99-cf80-4eab-b985-a83c70988b82', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ec5dfa16-2a81-42ae-be22-893bb13c0b05', N'9010003-010-011-6650-6658', N'9010003', N'010', N'011', N'6650', N'6658', N'', N'', N'', N'Chi bù tiền ăn', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.953' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0548f574-a5fb-4df5-a08f-a0a4cbad5b00', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd0b4eb49-1af3-49c3-9aea-8a72f808bd73', N'9010004-010-011-0002-0002', N'9010004', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.587' AS DateTime), NULL, 0, NULL, NULL, NULL, N'863a6a36-ed30-4f8f-b0f6-757aab07aee5', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4114503c-719b-4f87-b700-8a8575b406cb', N'9010002-010-011-0003-0001-0004-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.653' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'46c2b17e-c44e-43df-9c08-ce6f6ffbf415', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'55080c8a-9502-4e3c-b1d5-8aa430ee434b', N'9010001-010-011-0001-0001-0001-02-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:28.103' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'd0b64142-699e-4f9e-a6f5-824b03051c0e', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f4251289-70c0-478e-98e5-8bad0ad08693', N'9030001-010-011-0001', N'9030001', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:34.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'800fbf3c-386c-40c7-a6c3-2b1251a94009', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd9b9cc85-3fdf-4395-94a0-8c04a01147fd', N'9010001-010-011-0003-0001-0008-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.600' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'294076d4-8afb-404d-929d-3d6ecc396771', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd9c13760-9cd7-4f8f-831b-8c11a7428706', N'9010002-010-011-0002-0001-0002-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:41.447' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'55bbcc88-4a81-4a66-b648-b3049330001c', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'af1ef3ac-3a40-455d-a7df-8c69adf16d08', N'9010003-010-011-6900-6912', N'9010003', N'010', N'011', N'6900', N'6912', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:59:22.427' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'da8365b6-7e62-42eb-acf5-b7978c7298c5', N'87c7737b-0150-452a-b22e-02b356bd590f', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ca728222-85db-48be-8eab-8d938a6d0fb2', N'9010004-010-011-0002', N'9010004', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:45:06.687' AS DateTime), CAST(N'2023-11-29T08:59:26.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'16067072-1a6e-4850-8092-8db4de2cbfa3', N'9010002-010-011-0002-0001-0002-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:41.190' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bdd3fb72-88d9-43a8-beae-8f4eca891284', N'9020001-010-011-0001-0002', N'9020001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'3. HSQ, BS', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:31.870' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'9394f8c1-479d-438a-813a-22c105b5e731', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'4', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'636461d4-6483-4a88-9e26-90bb4307c434', N'9010010', N'9010010', N'', N'', N'', N'', N'', N'', N'', N'Chi hỗ trợ người lao động tham gia BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1a2ed1f1-30e1-4b6e-b681-90f5f3b0de9f', N'9010008-010-011-0001', N'9010008', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f118fa46-bf8c-48c7-b32a-9bde405d806a', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'66820df9-59cc-40da-bd28-915ed5e860b0', N'9010009', N'9010009', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí mua sắm trang thiết bị y tế', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'09129a72-434f-452c-8b6c-d5159b6724c2', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a01a344d-e34f-4354-b65c-91a9a9a735cc', N'9010003-010-011-6550-6553', N'9010003', N'010', N'011', N'6550', N'6553', N'', N'', N'', N'Khoán văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.710' AS DateTime), NULL, 0, NULL, NULL, NULL, N'86b2f605-c16b-4158-90d5-c6a9dfd22c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bba4ed9e-9b42-4fb5-bc77-92709859f04d', N'9010001-010-011-0002-0001-0003-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:31.017' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1e51f443-b698-4028-b0d6-92df75ce50e6', N'9010004-010-011-0001', N'9010004', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:40.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73357a2d-1879-452f-a85f-836a3090a537', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6b0c5934-284e-45ba-bac8-931045e71aec', N'9010001-010-011-0002-0001-0001-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'02', N'+ Lao động nam (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:29.623' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'e23d1638-f729-4e97-82c3-62a1c02fd5a3', N'232a6a14-37ff-40d7-9753-21a3f0131208', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fbe0e30c-a71d-4e7f-b824-932ae88c5fb6', N'9010001-010-011-0004-0001-0002-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:34.500' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'18fcd280-fd4c-405c-b1a1-25d438da44ba', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'054b6512-9ff8-411b-ace6-933423b28749', N'9020002-010-011-0001-0000', N'9020002', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'1. Sĩ quan', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.370' AS DateTime), NULL, 0, NULL, NULL, NULL, N'87d5e78c-2370-40b6-b7a8-ec285ed08504', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'49a3d812-f84b-4387-84d8-939fe64e6a69', N'9010002-010-011-0003-0001-0003-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0003', N'00', N'', N'- Trợ cấp hàng tháng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.483' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0426ed87-f0f1-4142-8275-c21ed650e5a7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a33a6536-52c5-407f-bda9-947bad039ecd', N'9010003-010-011-6700-6701', N'9010003', N'010', N'011', N'6700', N'6701', N'', N'', N'', N'Tiền vé máy bay, tàu xe', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.543' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7d577619-8dea-4632-a2f5-5f53e7cdbff7', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'999a896f-9564-4a3e-a733-947ffa5d8c4a', N'9010003', N'9010003', N'', N'', N'', N'', N'', N'', N'', N'Chi kinh phí quản lý BHXH, BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:40.060' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'885f40fa-4c4d-4ef2-ab31-b075853b028f', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e02a0f52-ec10-4ef4-88a7-94d4a43070b1', N'9010001-010-011-0001-0002', N'9010001', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.497' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'a297c80b-7b65-40e6-823f-fbed19e77501', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'aa70686c-e442-46e8-a948-96697a10af84', N'9010001-010-011-0003-0001-0007-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:33.370' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8371001c-2996-42e3-8dcf-7a5ab4a0914c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c3c400c6-8575-43c0-a4b2-96840e68d303', N'904', N'904', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'43bed3d4-1922-4d7c-8541-96e7f4ab2e2c', N'9010003-010-011-6650-6656', N'9010003', N'010', N'011', N'6650', N'6656', N'', N'', N'', N'Thuê phiên dịch, biên dịch phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.423' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e48cd68f-5928-4231-97eb-4eba9a644b2f', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4b474cb-996f-4362-90ee-97316bb6fb86', N'9010001-010-011-0001-0001-0001-02-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:27.920' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'ba487675-57b4-4b66-a1f4-27d7b6fbb495', N'4ecb27b3-f09b-4972-9d22-965886faab0a', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4699daaf-de8f-4d88-b974-985414cb47b5', N'9010003-010-011-7000-7049-0003', N'9010003', N'010', N'011', N'7000', N'7049', N'0003', N'', N'', N'- Đối chiếu danh sách, bảng lương, đôn đốc thu', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:24.820' AS DateTime), NULL, 0, NULL, NULL, NULL, N'039a4045-76a1-4ae0-927d-e42c4a021223', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5b3fec03-fa59-4152-a85a-987d03442990', N'9010001-010-011-0001-0003', N'9010001', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:28.773' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'280b18fa-ca4b-4e46-bb55-1bd81e962e7c', N'63e1e32a-460b-4696-aa73-441934842ac0', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f3c0e696-7773-4867-bbcd-989f0acf6ea1', N'9020002', N'9020002', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:32.730' AS DateTime), NULL, 0, NULL, NULL, NULL, N'72d7a943-3288-4175-be6f-cf5190d2b908', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'621001c0-6bbd-47e1-ab4d-98d4e6a3ed43', N'9010003-010-011-6550-6553', N'9010003', N'010', N'011', N'6550', N'6553', N'', N'', N'', N'Khoán văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.710' AS DateTime), NULL, 0, NULL, NULL, NULL, N'86b2f605-c16b-4158-90d5-c6a9dfd22c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'39f14397-ab1a-424e-9e63-99c9a8929da1', N'903', N'903', N'', N'', N'', N'', N'', N'', N'', N'Thu BHYT thân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5a1234af-171a-44c9-98ed-9da7deff0188', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'76bda775-4cff-41d4-b187-99e8d98cf86c', N'9010003-010-011-6550-6599', N'9010003', N'010', N'011', N'6550', N'6599', N'', N'', N'', N'Vật tư văn phòng khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.870' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3a14e0af-be8c-4ee3-9279-afc0eb517462', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b0bcd4cd-243f-4961-aea7-9a424ee69529', N'9010001-010-011-0003-0001-0002-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.327' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'795d2728-9ff9-4213-93f9-5e4bd22903e7', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1fa5b2ed-7a33-442b-bc70-9a61a4112f4c', N'9010010-010-011-0001-0006', N'9010010', N'010', N'011', N'0001', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a949a418-6684-4027-a429-974935a161f5', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b03cf3cc-a6e9-4fdc-aedb-9b0c5510fd92', N'9010002-010-011-0002-0001-0002-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:41.447' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'55bbcc88-4a81-4a66-b648-b3049330001c', N'9d0f9113-0bbc-4b8d-8e9b-60e41686b5ff', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8ae3806f-647b-4681-87fc-9b6b990dce96', N'9010002-010-011-0001-0002', N'9010002', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Con ốm (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:39.173' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'63e1bfb4-2e48-4b3e-b773-0be4cd4a194e', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4cd56d29-9e83-40ca-8666-9c64ecfccb8f', N'9010002-010-011-0003-0001-0005-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.910' AS DateTime), NULL, 0, NULL, NULL, NULL, N'22055e87-e713-4b77-97de-65672022c594', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'54dbac47-c1f6-4241-b8e7-9c6b3d55ce54', N'9010010-010-011-0001', N'9010010', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-06T11:10:08.130' AS DateTime), NULL, 0, NULL, NULL, NULL, N'900d7456-023a-41c3-9380-c7054e410b71', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a3dcd5cd-a46a-4139-b9ea-9e45e556b33a', N'9010003-010-011-6950-6956', N'9010003', N'010', N'011', N'6950', N'6956', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5da67169-8f6e-41e6-a6e4-85d9430d8d2a', N'87309c2d-5489-4e52-90c2-a97779e3b5c0', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'14fcddbf-0401-403e-b252-9f0adfa7237b', N'9010002-010-011-0007-0001-0001-00', N'9010002', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.957' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2907ac93-5edc-4c3b-a86d-37336769b417', N'774d1074-d499-4f15-9ac0-40b04ad1ba17', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e0605355-380d-4bf7-966f-a02bd52df49c', N'9010001-010-011-0003', N'9010001', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:31.820' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f477c796-ef6f-4115-8dab-a03b14142b34', N'9020001-010-011-0002-0000', N'9020001', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:32.320' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6c04bce4-1434-4da6-a378-a062f7f83bdb', N'9010001-010-011-0002-0001-0003-00-02', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'02', N'+ Lao động nam', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:31.017' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'af4633bb-d405-401b-b7c9-c6108917fc89', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'de13da50-974d-4f6d-b789-a0d20f768f41', N'9010010-010-011-0001-0005', N'9010010', N'010', N'011', N'0001', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1e857155-0d72-4768-9fdd-7e0bb8c59200', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0ce1da1a-c023-40c9-adac-a19f6ca3f4ea', N'9010003-010-011-7000-7049-0001', N'9010003', N'010', N'011', N'7000', N'7049', N'0001', N'', N'', N'- Chi hỗ trợ cán bộ, nhân viên chuyên trách làm công tác BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:24.427' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'302f226e-4c2d-4c58-895b-313e987860ce', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'350ef9f3-6644-47f9-b613-a20a4b04560b', N'9010002-010-011-0001-0001-0001-02-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'02', N'* Từ 14 ngày trở lên/tháng(ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.950' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6d2d85ba-b319-4704-8839-2f10cfdcb670', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c17df8de-1269-47f4-9ac5-a2cc0cbcf7fb', N'9020001-010-011-0002-0000', N'9020001', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T13:34:15.757' AS DateTime), CAST(N'2023-11-29T08:59:32.320' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'0bbae696-0592-4757-8431-f2f40f460b7a', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'3', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'db941517-8748-452c-b4de-a2ced8acac0b', N'9010001-010-011-0001-0001-0001-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:27.740' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4ecb27b3-f09b-4972-9d22-965886faab0a', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6e01500a-e977-4f36-8db9-a2d18c592d74', N'9010001-010-011-0007-0001-0001-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:35.883' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fcc27636-fe76-4224-bb5c-0743a91b7e6f', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'73d652cd-3715-436e-a04b-a2db2fb019a0', N'9010004-010-011-0001-0002', N'9010004', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:30.947' AS DateTime), CAST(N'2023-11-29T08:59:41.627' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b7898495-5a08-4079-834f-f12d2e7939d6', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bef14723-1515-4aec-885b-a48b117fb967', N'9010004-010-011-0001-0000', N'9010004', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:41.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'443cbbb4-f504-43b2-ac0f-77acfeac84c2', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f33465aa-cc10-4e01-ba09-a49cb2f39d00', N'9010002-010-011-0008', N'9010002', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:48.327' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'e56cedb7-ed03-485b-beab-c152f42ebadd', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4ff45972-01ed-41a3-9229-a4b83810bb50', N'9020001-010-011-0001-0001', N'9020001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:31.440' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0d4fa8e6-6deb-4b99-924d-5532cf4181a6', N'8de3ff2f-c2af-4608-afde-cc7e82d173a9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'2', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'aeb8aa28-0d1f-4bba-990c-a5352182f615', N'902', N'902', N'', N'', N'', N'', N'', N'', N'', N'Thu BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.757' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b18a18d3-c116-4384-a437-a5b4b059b0fd', N'9010003-010-011-6650', N'9010003', N'010', N'011', N'6650', N'', N'', N'', N'', N'Hội nghị', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:52.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5c3afcf1-0e53-4dbd-a79e-a67da2c0eb6e', N'9010006-010-011-0001-0002', N'9010006', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.750' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e1ac0849-a18f-4c20-89e2-0ce336d197bf', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'01558ef4-1e8f-4f27-ac6a-a69bf2e4701d', N'9010001-010-011-0001-0001-0001-01-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.573' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'f9022301-b3c7-4279-b634-632b69936503', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ba1931ba-dcb9-4a8e-8319-a71fa7d88219', N'9010002-010-011-0003-0001-0006-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.117' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'898acb1c-49f6-4395-9452-418352b0eed1', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1b8f26b9-48d0-4be3-85fa-a73602586abe', N'9010001-010-011-0002-0001-0003-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:30.657' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4dbd0c0-2b6f-4ec6-898c-a78d219e6a56', N'9010003-010-011-6650-6657', N'9010003', N'010', N'011', N'6650', N'6657', N'', N'', N'', N'Các khoản thuê mướn khác phục vụ hội nghị', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.733' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dea8ce64-0669-4f7c-9e53-09e0d5ffaa64', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bc02fc55-9cb4-413a-b3bd-a813853538f0', N'9010001-010-011-0003-0001-0005-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.960' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'e981466a-d098-40b2-be26-78989d31bd5c', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd9c6b79a-7604-4737-965e-a92c3f18fa0b', N'9010010-010-011-0001-0002', N'9010010', N'010', N'011', N'0001', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'884e38f9-ce43-49f6-9f77-37969ab18a81', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e1868847-3800-4f95-94f5-aa1ceb8e2da2', N'9010006-010-011-0002-0002', N'9010006', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.210' AS DateTime), NULL, 0, NULL, NULL, NULL, N'45438d5c-982d-41d5-aa67-d28213290663', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'23b9108b-fb4f-48d7-bbb3-aa383e590a5c', N'9010003-010-011-6700', N'9010003', N'010', N'011', N'6700', N'', N'', N'', N'', N'Công tác phí', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.353' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'95580b2f-c61f-456b-985b-abc3729e7ed5', N'9010003-010-011-7750', N'9010003', N'010', N'011', N'7750', N'', N'', N'', N'', N'Chi khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c60199d0-3566-4c28-91d5-cbde4cd4f792', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c1b0be3c-0ae1-477c-83ea-abdc16d0ee0b', N'9010001-010-011-0003-0001-0004-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:32.733' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'30b10c02-7a67-41b4-afe2-a4652321a14b', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4b45fdd9-9824-43d1-9fb8-ac1eca70d9f6', N'9010002-010-011-0003-0001-0006-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.117' AS DateTime), NULL, 0, NULL, NULL, NULL, N'898acb1c-49f6-4395-9452-418352b0eed1', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c53a568c-3f96-463e-a98e-ac2ca6474acf', N'9010002-010-011-0001-0003', N'9010002', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dưỡng sức, phục hồi SK (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:39.520' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'866cedc7-d4a3-4b0e-bbb9-ebb5b227b413', N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1d0df9ae-5948-4ca6-9102-adf665979d24', N'9010003-010-011-6600-6603', N'9010003', N'010', N'011', N'6600', N'6603', N'', N'', N'', N'Cước phí bưu chính', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:28:37.960' AS DateTime), CAST(N'2023-11-29T08:58:50.620' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'd57050f2-75a6-43e1-b637-cd3a4fe8b4c6', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f8be4ca0-ea90-4332-9ab9-aefd4c17f0bb', N'9030005', N'9030005', N'', N'', N'', N'', N'', N'', N'', N'V. BHYT học viên đào tạo sĩ quan dự bị từ 03 tháng trở lên', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:37.890' AS DateTime), NULL, 0, NULL, NULL, NULL, N'68018115-052e-4d82-b8ab-e40648c1cf49', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2d0da105-bd5b-4abd-8f2a-af9ac07c1f30', N'9010001-010-011-0005', N'9010001', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:34.693' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1da67c9f-2025-4bd0-9d24-20fa64651658', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f4eaa5da-6c86-4a43-bf34-afed97b9aeee', N'9010001-010-011-0008', N'9010001', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:36.280' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'f707d446-bee8-4299-b361-16ec2fb1d471', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd50389c5-bc83-4b68-80da-b04e75ba73c2', N'9010003-010-011-6600-6601', N'9010003', N'010', N'011', N'6600', N'6601', N'', N'', N'', N'Cước phí điện thoại (không bao gồm khoán điện thoại); thuê bao đường điện thoại; fax', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.377' AS DateTime), NULL, 0, NULL, NULL, NULL, N'943d7e3d-f8ff-4f8b-bab8-7a43dda8e71f', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b0644b48-7539-48f4-ac15-b0a6a1e561b7', N'9010006-010-011-0001-0000', N'9010006', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:30:37.537' AS DateTime), CAST(N'2023-11-29T08:59:28.380' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e3b3fc59-1030-41d6-ae90-3c3dd8fe6d1c', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bfdcf04f-066c-420e-95e7-b0d2c49cb97c', N'9010001-010-011-0008-0001-0002-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.690' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'01e7c76b-294b-4440-b555-d0b33af107cc', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a79e2439-6982-4785-bb78-b0d9a29b0c7a', N'9010002-010-011-0008-0001-0002-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:48.770' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'af6267f2-5472-4ea7-bb18-c36596b39611', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7baa5851-b2c2-4fd5-ae00-b0e36447081f', N'9010002-010-011-0003-0001-0002-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.320' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'171536b5-4867-40ab-a842-7284eeca8409', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd3fbfbdb-d3f7-4865-ba04-b1a0b1e3e1fa', N'9010006-010-011-0001-0003', N'9010006', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:40.710' AS DateTime), CAST(N'2023-11-29T08:59:28.967' AS DateTime), NULL, 0, NULL, NULL, NULL, N'35d2bc4b-f59b-4f9b-a96f-bf30529d48f7', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e3413f14-7fa4-43be-8cc4-b215d8fd9451', N'9', N'9', N'', N'', N'', N'', N'', N'', N'', N'Thu, chi  BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:38.863' AS DateTime), CAST(N'2023-11-29T08:58:25.193' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'25aa0569-2843-4e00-bd95-b48558a6e028', NULL, 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f3478520-9504-4581-b533-b22033d085be', N'9010002', N'9010002', N'', N'', N'', N'', N'', N'', N'', N'II. KHỐI HẠCH TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:39.747' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7a097777-89ac-4d76-a856-d1534c4070a9', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c6eafe9d-815e-4705-bb3e-b248407d3a6b', N'9010001-010-011-0001-0001-0001-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'', N'+ Thuộc DM bệnh cần chữa trị dài ngày', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:26.330' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', N'a655d431-de68-4238-b921-55850d8bba6b', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'19ad4aed-91a2-4fdf-9dfb-b32e8fe88199', N'9040001', N'9040001', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT quân nhân', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6173fe68-9e45-4c21-92a3-309afb77f73e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cae8aa0d-6c5c-4591-a1b9-b344dd0e9f24', N'9020001-010-011-0002-0001', N'9020001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:53:19.803' AS DateTime), CAST(N'2023-11-29T08:59:32.567' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'43', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'85563473-cd99-4115-9dab-b4136e780058', N'9030001-010-011-0002-0003', N'9030001', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'c. Bảo hiểm y tế thân nhân HSQ-CS (thân nhân học viên cơ yếu)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:36.643' AS DateTime), NULL, 0, NULL, NULL, NULL, N'77b53c15-3232-4207-83be-759cbeeb098b', N'a1c989de-eac9-4153-8a60-a34ac387bb88', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cd2e7ef9-9195-4b71-91f7-b4892df1a257', N'9010003-010-011-6600-6603', N'9010003', N'010', N'011', N'6600', N'6603', N'', N'', N'', N'Cước phí bưu chính', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:28:37.960' AS DateTime), CAST(N'2023-11-29T08:58:50.620' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'd57050f2-75a6-43e1-b637-cd3a4fe8b4c6', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f9f441b9-3b70-4108-bbd6-b55078c7e65b', N'9010001-010-011-0003-0001-0006-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0006', N'00', N'', N'- Trợ cấp chết do TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.137' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'6a99dbd6-a4ca-49f3-b3d3-0014c20a9a2d', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c58bb406-8160-4aaf-91b8-b5a09a95dd52', N'9010006-010-011-0001', N'9010006', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:51.650' AS DateTime), CAST(N'2023-11-29T08:59:28.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8088e295-2f9c-45b3-a799-d88940922713', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1c9bf25b-5fb1-42f9-a76e-b6f9398dd680', N'9010009-010-011-0001', N'9010009', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b45699c0-25f5-4f79-b26f-bed14c2fb846', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8ef8788d-c72e-4bce-a6cc-b70f130ff33d', N'9010001-010-011-0002-0001-0002-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.147' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'b79bd552-a5b7-4336-bdaf-27e4d99a04af', N'568c1146-2660-4db8-ad31-6e78d8c0daca', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c132e0cf-509d-4ab6-b213-b79f46456f0b', N'9030001-010-011-0002', N'9030001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T18:33:52.740' AS DateTime), CAST(N'2023-11-29T08:59:36.087' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a1c989de-eac9-4153-8a60-a34ac387bb88', N'4c52e8ab-654e-4b86-afbc-165823b677a2', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'362620c8-b4a7-4b48-a854-b7a5748713d9', N'9010001', N'9010001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:25.503' AS DateTime), NULL, 0, NULL, NULL, NULL, N'91465483-df1b-4262-9436-d87f8808cfac', N'18baf083-e5e0-4246-bc88-27eeb6ab28d4', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'963e3345-f7d5-4615-9e18-b8b1e451ba9f', N'9010001-010-011-0001-0001-0001-01-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:27:10.080' AS DateTime), CAST(N'2023-11-29T08:58:27.573' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'f9022301-b3c7-4279-b634-632b69936503', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd75ef7ea-6511-4d30-9349-bafb398520bb', N'9010001-010-011-0007-0001-0001-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:35.883' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'fcc27636-fe76-4224-bb5c-0743a91b7e6f', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'99453931-e699-46c1-8aa2-bb8a3d17228a', N'9010003-010-011-6650-6653', N'9010003', N'010', N'011', N'6650', N'6653', N'', N'', N'', N'Tiền vé máy bay, tàu xe (đối với đại biểu là khách)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.687' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'5fd8d523-3afd-45ff-85bc-80c96d602cb8', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'9a85709b-b1db-4505-ad15-bb9aee729d38', N'9010008-010-011-0002', N'9010008', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'330680b8-3b02-4633-a591-6f71c88312bd', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'483e0efb-01b5-484b-9ab8-bbbc6a04ad57', N'9010001-010-011-0001-0001-0001-01-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:27.283' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'930fd4fd-705b-40cf-b578-bbd80f9359b2', N'9010008-010-011-0002', N'9010008', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'330680b8-3b02-4633-a591-6f71c88312bd', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'87934fec-1ba0-4fd2-9e71-bc5eb23d67cf', N'9010003-010-011-6700-6701', N'9010003', N'010', N'011', N'6700', N'6701', N'', N'', N'', N'Tiền vé máy bay, tàu xe', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.543' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7d577619-8dea-4632-a2f5-5f53e7cdbff7', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'609ed863-c851-4e09-b28f-bcd595ee1d56', N'9010003-010-011-6550-6552', N'9010003', N'010', N'011', N'6550', N'6552', N'', N'', N'', N'Mua sắm công cụ, dụng cụ văn phòng', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.510' AS DateTime), NULL, 0, NULL, NULL, NULL, N'bf5ffd8c-e50f-4843-a003-156c9e2a5eba', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2cbeaabd-f0e6-4bb7-8796-bcd5c70cda4a', N'9010002-010-011-0003-0001-0005-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0005', N'00', N'', N'- Trợ cấp phục vụ (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:43.910' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'22055e87-e713-4b77-97de-65672022c594', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4289bcd1-741a-4228-9b41-bcfda258a81e', N'9010002-010-011-0003-0001-0009-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.833' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8aec0461-a021-47fd-845f-c1512a792cb7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'66588ba1-217a-43ce-bc83-bd326d78a67d', N'9010009', N'9010009', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí mua sắm trang thiết bị y tế', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'09129a72-434f-452c-8b6c-d5159b6724c2', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4045c436-1d01-40d2-9824-be074dbd85f6', N'9010010-010-011-0002-0006', N'9010010', N'010', N'011', N'0002', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4d4aa749-e38b-4b43-b3fc-fe999026fe4a', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'91cdce49-2a8a-430a-b83d-be3461c4628f', N'9010004-010-011-0002-0000', N'9010004', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.200' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8795dd68-21ff-4a22-b857-bec3d01f77cf', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'fc32f6a2-ce5f-4eb7-ba79-bf1e34ecf707', N'9010001-010-011-0001-0001-0001-02', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:46:04.787' AS DateTime), CAST(N'2023-11-29T08:58:27.740' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'4ecb27b3-f09b-4972-9d22-965886faab0a', N'a655d431-de68-4238-b921-55850d8bba6b', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e03482fe-9ff4-4b41-81d2-bf43297a17d8', N'9010008', N'9010008', N'', N'', N'', N'', N'', N'', N'', N'Chi từ nguồn kết dư Quỹ KCB BHYT quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-12-04T14:23:10.140' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'73a85b99-cf80-4eab-b985-a83c70988b82', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'2927facf-12d0-4c9d-8c7b-c1fadee1f879', N'9030006', N'9030006', N'', N'', N'', N'', N'', N'', N'', N'VI. BHYT người nước ngoài đang học trong các trường QĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1499a32d-8556-4ad5-bff4-81a307916605', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'82c6efe7-df32-4bc7-a3ad-c234ba01a2a7', N'9010001-010-011-0002-0001-0001-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'', N'- Sinh con, nuôi con nuôi (tháng)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:29.343' AS DateTime), NULL, 0, NULL, NULL, NULL, N'232a6a14-37ff-40d7-9753-21a3f0131208', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b3410624-cb34-4d32-9f35-c2e61ff37250', N'9010006-010-011-0001-0001', N'9010006', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:45.493' AS DateTime), CAST(N'2023-11-29T08:59:28.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78660144-1b68-4aff-a71c-65b41ae48bbe', N'8088e295-2f9c-45b3-a799-d88940922713', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f905a872-36c8-4e1b-b1db-c347151c5ee8', N'9010002-010-011-0002-0001-0001-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:40.443' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'aaa3dd36-bf5d-4615-9e90-83db2e3cde00', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 2)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'98a07181-5b57-4419-9f08-c355be19a957', N'9010001-010-011-0006', N'9010001', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:35.230' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0f8488df-0fed-4d82-ade8-c3a3c58609fd', N'9010006-010-011-0001-0001', N'9010006', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-04T14:58:45.493' AS DateTime), CAST(N'2023-11-29T08:59:28.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'78660144-1b68-4aff-a71c-65b41ae48bbe', N'8088e295-2f9c-45b3-a799-d88940922713', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ca05acda-6404-4188-8683-c40dff63dcc4', N'9010010-010-011-0001-0003', N'9010010', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'Mức 3: 2.400.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'aea11eec-3be8-47eb-bb7c-7d7b339ba635', N'900d7456-023a-41c3-9380-c7054e410b71', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'58ebbf85-6dce-4ce8-b136-c51b803498e2', N'9010003-010-011-7000-7012', N'9010003', N'010', N'011', N'7000', N'7012', N'', N'', N'', N'Chi phí hoạt động nghiệp vụ chuyên ngành ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.937' AS DateTime), NULL, 0, NULL, NULL, NULL, N'7b143cca-0dad-4003-8708-d479f83fdd53', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f2b2842c-ca8f-49ab-a4fc-c61e701a7a92', N'9010010-010-011-0001-0006', N'9010010', N'010', N'011', N'0001', N'0006', N'', N'', N'', N'Mức 6: 3.300.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:18:23.540' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a949a418-6684-4027-a429-974935a161f5', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f33ad8be-3681-4824-bb35-c6fdb8ba6664', N'9010003-010-011-7000-7049-0002', N'9010003', N'010', N'011', N'7000', N'7049', N'0002', N'', N'', N'- Chi phối hợp kiểm tra, thanh tra, phúc tra, giám sát công tác thu, chi BHXH, BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:24.600' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'874cd699-e8bb-4d5d-96cc-fc2ef63e3f26', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4e40760f-144b-4ec8-83c9-c7b55a9dd4e5', N'9010006', N'9010006', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB tại Trường Sa - DK ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:57:37.167' AS DateTime), CAST(N'2023-11-29T08:59:27.963' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f2e0ecfa-c06f-48f3-bb74-c8fb3cf1b928', N'9010001-010-011-0001-0001', N'9010001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Bản thân ốm (ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:26.100' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a655d431-de68-4238-b921-55850d8bba6b', N'63e1e32a-460b-4696-aa73-441934842ac0', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'379656dd-1cc2-40e7-a89e-c9998f949f53', N'904', N'904', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dfecf400-54d3-4e5d-bbbe-c9acfb4c075e', N'9010010-010-011-0002-0004', N'9010010', N'010', N'011', N'0002', N'0004', N'', N'', N'', N'Mức 4: 2.650.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2e2eb178-63f3-496b-8457-179787e01d72', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4e1bcb7-8858-41ab-8600-c9f8bbe47dbe', N'9030001-010-011-0001-0001', N'9030001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.260' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f5b01bd6-57b9-4523-9ed0-b8157a89e171', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8c8e6e91-3f25-41da-b1d7-ca0ecdf246a6', N'9010001-010-011-0002', N'9010001', N'010', N'011', N'0002', N'', N'', N'', N'', N'2. Trợ cấp thai sản', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:29.047' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'09ef0e0c-5c71-4783-b280-646d504af7b5', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b7bde0b4-1cd7-4f1c-a4f1-cb0c75df3a28', N'9010003-010-011-6650-6699', N'9010003', N'010', N'011', N'6650', N'6699', N'', N'', N'', N'Chi phí khác ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.070' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9a6ab999-351c-4878-aa57-0807366a6f70', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c1313f22-432a-42d0-bc1b-cb9692e77f66', N'9010001-010-011-0003', N'9010001', N'010', N'011', N'0003', N'', N'', N'', N'', N'3. Tai nạn lao động, bệnh NN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:31.820' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a4dcbfba-84b8-4fd2-b545-cbc0d8662877', N'9010010-010-011-0002-0001', N'9010010', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:14:36.670' AS DateTime), NULL, 0, NULL, NULL, NULL, N'dd3587b7-ba65-4bed-9cb3-6487d54a09e1', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd69242ab-afe2-4585-96c2-ccdca3afbaea', N'9010004-010-011-0002-0001', N'9010004', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.350' AS DateTime), NULL, 0, NULL, NULL, NULL, N'600f74a3-06c7-4c4e-8859-69a8ab9d212c', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ba413e5b-21d8-42e5-912a-cd14e0d31683', N'9010002-010-011-0005', N'9010002', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:45.767' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6b9be279-f554-4ffb-8ea5-cd3fe5e4b194', N'9010003-010-011-6550-6551', N'9010003', N'010', N'011', N'6550', N'6551', N'', N'', N'', N'Văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'566a0314-25dd-4ab6-9f33-b3779c438c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6ea05dc4-db4a-4013-9d08-ce0a8650b510', N'9010003-010-011-6650-6654', N'9010003', N'010', N'011', N'6650', N'6654', N'', N'', N'', N'Tiền thuê phòng ngủ (đối với đại biểu là khách mời)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.850' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'751a5278-b174-4ea7-a278-968e3c7c8894', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1e1dcd11-5c0c-4987-88a4-cec1a09ff779', N'9050002-010-011-0002', N'9050002', N'010', N'011', N'0002', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu học sinh sinh viên ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T19:44:54.827' AS DateTime), CAST(N'2023-11-29T08:59:39.300' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'cf3d919f-997c-4f35-abed-40d1ef5f1f51', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'862be0cd-0488-497e-b0ec-ced5294403c6', N'9010002-010-011-0004-0001-0002-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:45.550' AS DateTime), NULL, 0, NULL, NULL, NULL, N'939834ed-9ed7-406a-8cc6-b8d4b2930c66', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'58fa2f20-ca8e-40f8-a37b-cf038506e233', N'9010002-010-011-0002-0001-0004-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.603' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'097b3961-33a9-4e67-a9cb-10cdf604d8dc', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd30a8df7-014c-4632-bdf3-cf812f9b2864', N'9020001-010-011-0002', N'9020001', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'94015955-8d31-4063-bf17-d09ded695036', N'9010003-010-011-7000-7049-0005', N'9010003', N'010', N'011', N'7000', N'7049', N'0005', N'', N'', N'- Chi hỗ trợ bệnh viện, bệnh xá KCB BHYT', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:25.060' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'1c72986d-0c37-4e1e-b997-75b6d26bee60', N'a6aa1af6-475c-4853-836f-c0bde339c5b8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f0ab1ba4-95c0-424a-a7e5-d178e440be47', N'9010003-010-011-6650-6655', N'9010003', N'010', N'011', N'6650', N'6655', N'', N'', N'', N'Thuê hội trường, phương tiện vận chuyển', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.150' AS DateTime), NULL, 0, NULL, NULL, NULL, N'a7bfb1a0-f712-45bc-9a4d-41f850a4fd52', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'484ec5d7-3de2-4fbb-b486-d25f18074467', N'9010003-010-011-6750-6799', N'9010003', N'010', N'011', N'6750', N'6799', N'', N'', N'', N'Chi phí thuê mướn khác', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.990' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5b7a1ead-90a1-42c1-91f3-ef688f45f268', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'58c5c71c-ed7b-4f45-8cba-d2a545eadd2d', N'9010002-010-011-0001-0001-0001-02-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.600' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0a70e747-0f95-4925-b6bd-0b91f4cf8ed1', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'41c3d25c-b136-4df3-bb9f-d3421936ffb8', N'9010002-010-011-0002-0001-0003-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:42.150' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6f83cc9b-2907-4417-b19b-72f705f090cb', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5be44130-a84d-49f0-bdba-d3852022326c', N'903', N'903', N'', N'', N'', N'', N'', N'', N'', N'Thu BHYT thân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:40.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5a1234af-171a-44c9-98ed-9da7deff0188', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'96d24a10-56a7-4fac-b21f-d4ac2c653241', N'9010002-010-011-0008-0001-0001-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b66ca2a3-645d-4879-a0ca-6e4cb3c9b442', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1e797e96-c0ff-4220-95f6-d4e9ef4b0c47', N'9010003-010-011-7000', N'9010003', N'010', N'011', N'7000', N'', N'', N'', N'', N'Chi phí nghiệp vụ c.môn của từng ngành', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.653' AS DateTime), NULL, 0, NULL, NULL, NULL, N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b26f9196-136a-4774-8090-d59846aab3c9', N'9010004-010-011-0002', N'9010004', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:45:06.687' AS DateTime), CAST(N'2023-11-29T08:59:26.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'1c8a28c9-87d6-4a7f-b27a-d61da4bf09f1', N'9010001-010-011-0006', N'9010001', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:35.230' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a0d4eadb-0deb-45a6-b3fc-2f00cebd9613', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f5296931-d706-4862-8203-d6a3522694bf', N'9010008-010-011-0001', N'9010008', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f118fa46-bf8c-48c7-b32a-9bde405d806a', N'73a85b99-cf80-4eab-b985-a83c70988b82', 2024, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'113662c6-7d9c-42eb-96b0-d6be38fd62b2', N'9010004-010-011-0001-0000', N'9010004', N'010', N'011', N'0001', N'0000', N'', N'', N'', N'- Thuốc', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:41.410' AS DateTime), NULL, 0, NULL, NULL, NULL, N'443cbbb4-f504-43b2-ac0f-77acfeac84c2', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'eb4047c0-b7df-4ecd-b116-d8c138e84d01', N'9010002-010-011-0005-0001-0001-00', N'9010002', N'010', N'011', N'0005', N'0001', N'0001', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:46.340' AS DateTime), NULL, 0, NULL, NULL, NULL, N'da87b860-e7a5-4a0a-a1ae-d03ea1b81de9', N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'81a4dcd8-178d-4a5b-8290-d93865af85ba', N'9040002', N'9040002', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí KCB BHYT thân nhân quân nhân và người lao động', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:38.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'503d8588-e537-4c72-a921-a437c1845d9e', N'd06ce6a9-2a2e-4a7b-bad9-a88c61ac4cfe', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'97e7cb0b-afcf-4b9b-8dc6-d9639a67b0ef', N'9010002-010-011-0004-0001-0001-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:45.277' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'3d61c53a-3c3d-4a96-8dda-3565c77a2923', N'69183e88-7f92-473e-a228-47d369f839e7', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'cf84f16e-a7ad-4556-a006-db91e788dea2', N'9020002-010-011-0002', N'9020002', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.047' AS DateTime), NULL, 0, NULL, NULL, NULL, N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'41779ba1-73da-41be-9e49-dbccad2e32b5', N'9010004-010-011-0001-0003', N'9010004', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.863' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f95d9588-9394-4f73-a57f-23a464fdd002', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'70cce940-7eb3-47d6-8f6c-dca68cc2142f', N'9030001', N'9030001', N'', N'', N'', N'', N'', N'', N'', N'I. Bảo hiểm y tế thân nhân quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:34.690' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4c52e8ab-654e-4b86-afbc-165823b677a2', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'be595d8b-e63d-45c6-81c8-dd5284ceb5c8', N'9010006-010-011-0002', N'9010006', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:55.943' AS DateTime), CAST(N'2023-11-29T08:59:29.490' AS DateTime), NULL, 0, NULL, NULL, NULL, N'043d854e-bca5-4ee9-905a-8b78d15b9887', N'78861c3d-d2be-4c22-8a9a-2ccc7a63502a', 2023, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'51e23309-2699-41f2-93e6-de27abea37c5', N'9010003-010-011-6900-6912', N'9010003', N'010', N'011', N'6900', N'6912', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:02:15.827' AS DateTime), CAST(N'2023-11-29T08:59:22.427' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'da8365b6-7e62-42eb-acf5-b7978c7298c5', N'87c7737b-0150-452a-b22e-02b356bd590f', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a976b247-393f-46e2-af57-de6bc3e0be66', N'9010003-010-011-6600-6618', N'9010003', N'010', N'011', N'6600', N'6618', N'', N'', N'', N'Khoán điện thoại', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:51.367' AS DateTime), NULL, 0, NULL, NULL, NULL, N'781048d2-925a-4fc9-b0c7-752d42b49bd4', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'00d47971-16e2-48d0-9a55-df6e7ff59061', N'9010002-010-011-0003-0001-0009-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.833' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8aec0461-a021-47fd-845f-c1512a792cb7', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ffb7f995-c6f4-4c2c-8090-e0a761b7f935', N'9010001-010-011-0002-0001-0003-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:30.823' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'0479e598-32cc-49b6-b8fb-e14cfe629c60', N'9010001-010-011-0002-0001-0004-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0004', N'00', N'', N'- Dưỡng sức, phục hồi SK', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:31.680' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8e092c28-89d2-460b-ab60-5aafbf9e010a', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f4e80f28-6f2c-4f84-b13c-e1d4f49574d1', N'9010001-010-011-0002-0001-0002-00', N'9010001', N'010', N'011', N'0002', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:49:01.630' AS DateTime), CAST(N'2023-11-29T08:58:29.913' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'568c1146-2660-4db8-ad31-6e78d8c0daca', N'09ef0e0c-5c71-4783-b280-646d504af7b5', 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8e417fd9-f13e-401d-85b9-e2094532786e', N'9030002', N'9030002', N'', N'', N'', N'', N'', N'', N'', N'II. Bảo hiểm y tế thân nhân CN, VCQP(CY khác)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ad55244-03c4-4c97-ba26-27af54495842', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2023, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a05959d0-6c8d-4b14-8d0f-e2945c765165', N'9010001-010-011-0008-0001-0001-00', N'9010001', N'010', N'011', N'0008', N'0001', N'0001', N'00', N'', N'- Mai táng phí (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:36.453' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'c1003754-545f-4a6b-acb1-ceb56a4a19c2', N'f707d446-bee8-4299-b361-16ec2fb1d471', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bb51e919-76a4-4455-a769-e2e46e12a7e3', N'9010003-010-011-6700-6703', N'9010003', N'010', N'011', N'6700', N'6703', N'', N'', N'', N'Tiền thuê phòng ngủ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.970' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5d24dd88-913a-4eb5-b2e6-b0e767ea80a4', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a52fb8af-ea27-4ea7-9c86-e34e5d366dea', N'9010002-010-011-0004-0001-0001-00', N'9010002', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:02.850' AS DateTime), CAST(N'2023-11-29T08:58:45.277' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'3d61c53a-3c3d-4a96-8dda-3565c77a2923', N'69183e88-7f92-473e-a228-47d369f839e7', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bdb85a6f-d247-41e6-b23e-e3ac8a7dac22', N'9010010-010-011-0001-0001', N'9010010', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'Mức 1: 1.800.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:13:07.083' AS DateTime), CAST(N'2023-12-04T14:30:37.537' AS DateTime), NULL, 0, NULL, NULL, NULL, N'2577e235-3102-478d-8471-3bf14f0b8aca', N'900d7456-023a-41c3-9380-c7054e410b71', 2023, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'a683d288-54b0-436f-bfee-e4307b658f8d', N'9020002-010-011-0001', N'9020002', N'010', N'011', N'0001', N'', N'', N'', N'', N'A. Quân nhân', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:33.020' AS DateTime), NULL, 0, NULL, NULL, NULL, N'8639c115-bdb3-49d5-9018-a1c9a3d24896', N'72d7a943-3288-4175-be6f-cf5190d2b908', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'241f590d-0688-4413-a52b-e4656fc6f40c', N'9010002-010-011-0002-0001-0003-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:42.150' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'6f83cc9b-2907-4417-b19b-72f705f090cb', N'cf731c26-af8a-4708-82de-c03bd8b38715', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'25d00a95-e7b8-4538-9652-e46c67e91580', N'9010003-010-011-6650-6658', N'9010003', N'010', N'011', N'6650', N'6658', N'', N'', N'', N'Chi bù tiền ăn', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:19.953' AS DateTime), NULL, 0, NULL, NULL, NULL, N'0548f574-a5fb-4df5-a08f-a0a4cbad5b00', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'91108b08-acea-4eaa-b65a-e511f80c12b0', N'9010002-010-011-0001-0001-0001-01-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:37.923' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'bc6c9002-f7cc-47ad-8fe1-f80cab19ea01', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3667cc11-eb91-4299-aa62-e586ed598b2b', N'9010003-010-011-7750', N'9010003', N'010', N'011', N'7750', N'', N'', N'', N'', N'Chi khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:25.280' AS DateTime), NULL, 0, NULL, NULL, NULL, N'c60199d0-3566-4c28-91d5-cbde4cd4f792', N'885f40fa-4c4d-4ef2-ab31-b075853b028f', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'862859fd-5211-4b35-876d-e5efb9d081c6', N'9010003-010-011-6650-6654', N'9010003', N'010', N'011', N'6650', N'6654', N'', N'', N'', N'Tiền thuê phòng ngủ (đối với đại biểu là khách mời)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:42:25.003' AS DateTime), CAST(N'2023-11-29T08:59:18.850' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'751a5278-b174-4ea7-a278-968e3c7c8894', N'13a6bbc8-d2ba-4c81-9d29-a6c42bbd2a2b', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'abb33827-4957-4f01-8262-e7431a952233', N'9010004-010-011-0001', N'9010004', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:46:02.047' AS DateTime), CAST(N'2023-11-29T08:59:40.577' AS DateTime), NULL, 0, NULL, NULL, NULL, N'73357a2d-1879-452f-a85f-836a3090a537', N'ffb45fa2-8a4b-4918-ba6a-19d9c3da8044', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c4b885b4-6b44-4450-acce-e76eacbc6fcc', N'9010002-010-011-0006', N'9010002', N'010', N'011', N'0006', N'', N'', N'', N'', N'6. Trợ cấp Xuất ngũ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:46.940' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'd8fd23af-c23c-4b8f-8858-05fe8e304970', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'11a56bdf-48fd-4aa8-a00b-e7fda8b8aaf0', N'9010003-010-011-6550-6551', N'9010003', N'010', N'011', N'6550', N'6551', N'', N'', N'', N'Văn phòng phẩm', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:49.330' AS DateTime), NULL, 0, NULL, NULL, NULL, N'566a0314-25dd-4ab6-9f33-b3779c438c61', N'2e93d30f-796f-4f33-938b-7d2dd2d38c3a', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7200f48b-6735-47cd-8a42-e806e86b90ca', N'9010001-010-011-0008', N'9010001', N'010', N'011', N'0008', N'', N'', N'', N'', N'8. Tử tuất', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-11T18:56:37.513' AS DateTime), CAST(N'2023-11-29T08:58:36.280' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'f707d446-bee8-4299-b361-16ec2fb1d471', N'91465483-df1b-4262-9436-d87f8808cfac', 2023, N'M', N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'94bfe02c-94f5-454a-a885-e82dbc5bd4c2', N'9010010-010-011-0002-0002', N'9010010', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'Mức 2: 2.100.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:16:44.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'6f657c41-02bf-45e6-9e47-776c9040b46f', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'12a3b318-33b8-4537-9743-e8b8700b31c4', N'9010001-010-011-0001-0001-0001-01-01', N'9010001', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:27.283' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'5c1357a4-f877-4c19-9bf6-f7c85e8c2f99', N'33eaed8b-c9a6-49aa-96ee-1a9652c99ce9', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'48311a02-fbab-492b-8ceb-e97b75248170', N'9020001', N'9020001', N'', N'', N'', N'', N'', N'', N'', N'I. KHỐI DỰ TOÁN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:30.790' AS DateTime), NULL, 0, NULL, NULL, NULL, N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', N'f2bf9995-7b34-4d49-b973-3e135bf6e28a', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'727498ba-b42e-4451-9e87-ea0320d16e0e', N'9010001-010-011-0003-0001-0004-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0004', N'00', N'', N'- Trợ cấp phục hồi chức năng (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:32.733' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'30b10c02-7a67-41b4-afe2-a4652321a14b', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'61eb3802-d497-4fce-8a47-ebbfdb08a0e4', N'905', N'905', N'', N'', N'', N'', N'', N'', N'', N'Kinh phí CSSK ban đầu NLĐ và HSSV', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T21:51:32.197' AS DateTime), CAST(N'2023-11-29T08:59:41.117' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'2cdf8f93-5d04-45f8-afcc-5100068321e4', N'25aa0569-2843-4e00-bd95-b48558a6e028', 2024, N'LNS', N'LNS', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'edfdbaad-9af8-466d-8a1b-ecf3a6323cb2', N'9010009-010-011-0002', N'9010009', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:19:58.153' AS DateTime), CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e0756e6f-a6bf-42cb-972e-f40b1ce667e9', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2024, NULL, NULL, N'admin', N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c4246f8b-b3e7-49bb-a074-ecf64628bd42', N'9010004-010-011-0002-0003', N'9010004', N'010', N'011', N'0002', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.753' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3d2fc884-12a6-45b6-8ecc-a2744f87ef34', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'523fcf16-f6a7-4624-9b64-ed9d7dfe72d2', N'9010002-010-011-0001-0001-0001-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'', N'+ Ốm khác', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:54:34.480' AS DateTime), CAST(N'2023-11-29T08:58:38.367' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'0c64a47a-ae28-472f-82f7-a2d818d107ba', N'06a74e94-f3ba-4d73-8f80-9ae9563e1000', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b7532d61-47da-4a89-9d36-edb8f19daa70', N'9', N'9', N'', N'', N'', N'', N'', N'', N'', N'Thu, chi  BHXH, BHYT, BHTN', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T15:55:38.863' AS DateTime), CAST(N'2023-11-29T08:58:25.193' AS DateTime), N'SLNS', 0, NULL, NULL, NULL, N'25aa0569-2843-4e00-bd95-b48558a6e028', NULL, 2024, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ac4198d3-4ba2-4e3c-a27c-ee521015ea50', N'9020001-010-011-0002-0001', N'9020001', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'2. LĐHĐ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T10:53:19.803' AS DateTime), CAST(N'2023-11-29T08:59:32.567' AS DateTime), N'STM', 0, NULL, NULL, NULL, N'84ae6d38-5bfb-4c0e-96dc-dcdf8be8cfb7', N'4ce7992c-f413-434c-b785-1b99d6cbd025', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, N'43', N'PCCV_TT,HSBL_TT,PCTN_TT,PCTNVK_TT,LHT_TT', 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'14c337eb-d770-4f7f-8be1-ee937ca0acc9', N'9010002-010-011-0003-0001-0007-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0007', N'00', N'', N'- Chi hỗ trợ phòng ngừa (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:44.337' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'8dd7347a-25eb-4be1-b097-c0c6214a40e3', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 3)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7a99526b-a769-40cc-ade9-f02a09a57aad', N'9010010-010-011-0002', N'9010010', N'010', N'011', N'0002', N'', N'', N'', N'', N'II. Khối hạch toán', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:16:44.000' AS DateTime), CAST(N'2023-12-06T11:13:39.497' AS DateTime), NULL, 0, NULL, NULL, NULL, N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', N'ff9fd0c4-cef8-45a5-b979-915efb0c36f1', 2023, NULL, NULL, N'admin', N'admin', NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'e0af603b-1799-432e-8fbe-f03b7af8baf5', N'9030001-010-011-0001-0001', N'9030001', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'a. Bảo hiểm y tế thân nhân sĩ quan (thân nhân hàm CY)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:35.260' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f5b01bd6-57b9-4523-9ed0-b8157a89e171', N'800fbf3c-386c-40c7-a6c3-2b1251a94009', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd71aa1e4-845e-412e-a6f4-f052b2e8f4b1', N'9010002-010-011-0003-0001-0008-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0008', N'00', N'', N'- Chi hỗ trợ chuyển đổi nghề nghiệp (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:58:44.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5ce55373-18e4-43c6-965d-508d72db9ab5', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'f73f936e-a6eb-4b13-a2d6-f05347ec10c9', N'9010002-010-011-0002-0001-0003-00', N'9010002', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'', N'- Khám thai, KHH GĐ, nam nghỉ việc khi vợ sinh con(ngày)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-11-30T13:44:35.153' AS DateTime), CAST(N'2023-11-29T08:58:41.933' AS DateTime), NULL, 0, NULL, NULL, NULL, N'cf731c26-af8a-4708-82de-c03bd8b38715', N'6f830377-5b2f-44d8-9a64-9d46c4270a80', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'22a52907-c838-404b-b7da-f0b9bbf4d72f', N'9010003-010-011-7750-7799-0001', N'9010003', N'010', N'011', N'7750', N'7799', N'0001', N'', N'', N'- Chi thưởng cho tập thể, cá nhân thực hiện tốt công tác chi trả', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-15T09:59:06.103' AS DateTime), CAST(N'2023-11-29T08:59:26.017' AS DateTime), N'STTM', 0, NULL, NULL, NULL, N'994d7914-cf71-4767-99e4-697f10de2b78', N'65c08712-f281-4123-8efe-8848d21b220b', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'77fb3425-855b-4ff8-b8fb-f1efce235e2b', N'9010002-010-011-0003-0001-0002-00', N'9010002', N'010', N'011', N'0003', N'0001', N'0002', N'00', N'', N'- Trợ cấp 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:43.320' AS DateTime), NULL, 0, NULL, NULL, NULL, N'171536b5-4867-40ab-a842-7284eeca8409', N'2d5d9fc0-6e68-4f70-834c-3ef277099bf6', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'100c6ab2-25f9-403f-9b4c-f252dd55d6d9', N'9010002-010-011-0001', N'9010002', N'010', N'011', N'0001', N'', N'', N'', N'', N'1. Trợ cấp ốm đau', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:37.250' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'64e4b8a9-87f6-46f8-9e2b-7b32aa4f6f32', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'37b84743-0317-4d12-9166-f2d18480a21a', N'9010003-010-011-6700-6702', N'9010003', N'010', N'011', N'6700', N'6702', N'', N'', N'', N'Phụ cấp công tác phí', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:20.787' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4436044d-d91e-4f5f-819d-f9aa1290e0f9', N'd9c3f3ea-9ba6-4767-8691-c9eee1d2b0aa', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'dbd4cbae-8291-49fe-82a4-f2f7ad432009', N'9010006-010-011-0002-0001', N'9010006', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e8d7397c-9105-4d17-a4c8-678332804d6c', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'6b08e069-2228-4b52-8b5e-f33cc920e0eb', N'9010003-010-011-7000-7001', N'9010003', N'010', N'011', N'7000', N'7001', N'', N'', N'', N'Chi phí hàng hóa, vật tư ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.780' AS DateTime), NULL, 0, NULL, NULL, NULL, N'ea128ac8-2407-4705-a4ba-5fc7c3b0c26a', N'06c2e5e2-42f0-4ff6-a18c-bf19f1ed707d', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c473e853-777f-484d-9888-f34af10bc814', N'9010003-010-011-6750-6751', N'9010003', N'010', N'011', N'6750', N'6751', N'', N'', N'', N'Thuê phương tiện vận chuyển ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:21.817' AS DateTime), NULL, 0, NULL, NULL, NULL, N'1ed591dc-2b4a-44f6-9f2f-51dbac62e209', N'8fc998eb-21f2-4c1c-9711-1e010b7da6ba', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd5e8c047-a025-4347-9a9b-f4877b9e684d', N'9010001-010-011-0002-0001-0003-00-01', N'9010001', N'010', N'011', N'0002', N'0001', N'0003', N'00', N'01', N'+ Lao động nữ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:01:38.837' AS DateTime), CAST(N'2023-11-29T08:58:30.823' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0e323456-0843-4c30-b020-e686be1f879b', N'd27fbd1b-73ad-48a0-bcba-111b296a6b1c', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'4ae74663-2dbc-4bae-b2de-f5da2c8f8351', N'9010004-010-011-0001-0003', N'9010004', N'010', N'011', N'0001', N'0003', N'', N'', N'', N'- Dụng cụ y tế', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.863' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f95d9588-9394-4f73-a57f-23a464fdd002', N'73357a2d-1879-452f-a85f-836a3090a537', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'5d549df5-b306-4e1a-9978-f622fb585564', N'9010010-010-011-0002-0005', N'9010010', N'010', N'011', N'0002', N'0005', N'', N'', N'', N'Mức 5: 2.900.000', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:18:23.540' AS DateTime), NULL, 0, NULL, NULL, NULL, N'd60de627-81e8-4052-91b0-53e6e5431eac', N'39323a9e-cec8-46c2-aa6e-c0d1be72b2ce', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'11dbd638-89b5-495f-9816-f7a26189a9d2', N'9010002-010-011-0002-0001-0001-00-01', N'9010002', N'010', N'011', N'0002', N'0001', N'0001', N'00', N'01', N'+ Lao động nữ (tháng)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:40.443' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'aaa3dd36-bf5d-4615-9e90-83db2e3cde00', N'429c47a4-26ac-4bd3-97f1-7a89b917e6a1', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'aba0b8ad-8c26-4e2a-9e93-f7db61714654', N'9010002-010-011-0008-0001-0002-00', N'9010002', N'010', N'011', N'0008', N'0001', N'0002', N'00', N'', N'- Tuất 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:48.770' AS DateTime), NULL, 0, NULL, NULL, NULL, N'af6267f2-5472-4ea7-bb18-c36596b39611', N'e56cedb7-ed03-485b-beab-c152f42ebadd', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'3c70011d-a79a-464b-ae78-f9651b736eaf', N'9010006-010-011-0002-0001', N'9010006', N'010', N'011', N'0002', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:48:24.547' AS DateTime), CAST(N'2023-11-29T08:59:30.000' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e8d7397c-9105-4d17-a4c8-678332804d6c', N'043d854e-bca5-4ee9-905a-8b78d15b9887', 2023, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'12e1b25b-ed38-412d-a1b3-f97556672caf', N'9010003-010-011-6950-6956', N'9010003', N'010', N'011', N'6950', N'6956', N'', N'', N'', N'Các thiết bị công nghệ thông tin ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:23.080' AS DateTime), NULL, 0, NULL, NULL, NULL, N'5da67169-8f6e-41e6-a6e4-85d9430d8d2a', N'87309c2d-5489-4e52-90c2-a97779e3b5c0', 2024, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8f20d3ea-a972-4960-b6f1-f9d9d68ebbeb', N'9010002-010-011-0006-0001-0001-00', N'9010002', N'010', N'011', N'0006', N'0001', N'0001', N'00', N'', N'- Trợ cấp XN 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:47.160' AS DateTime), NULL, 0, NULL, NULL, NULL, N'fd8cb880-dd35-4db5-a71d-e47c97168672', N'd8fd23af-c23c-4b8f-8858-05fe8e304970', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'538a21fd-48b9-4915-b4c7-f9dd6922a43b', N'9010004-010-011-0001-0001', N'9010004', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'- Vật tư y tế (bông băng, bơm, kim tiêm)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-27T10:55:16.607' AS DateTime), CAST(N'2023-11-29T08:59:41.233' AS DateTime), NULL, 0, NULL, NULL, NULL, N'48112d8e-a78f-4a4c-8492-94cf989a8f7a', N'73357a2d-1879-452f-a85f-836a3090a537', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'b2523e22-b65e-4b76-9e10-fa7a415855b5', N'9010002-010-011-0001-0001-0001-01-02', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'01', N'02', N'* Từ 14 ngày trở lên/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:38.153' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'1bf2509b-91d5-4974-aff0-7f7c426cddc4', N'9ebe3f5e-f6e8-4268-b744-583b24731221', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'bd9d4b77-6fc7-495a-80bd-fb1863ad1f87', N'9010003-010-011-6600-6606', N'9010003', N'010', N'011', N'6600', N'6606', N'', N'', N'', N'Tuyên truyền (phát thanh, truyền hình)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:58:50.883' AS DateTime), NULL, 0, NULL, NULL, NULL, N'f42345ce-969b-482c-a5f5-570b0d4cdf36', N'4ec9c459-3a9b-4106-a0bf-f0550e0ec73f', 2023, NULL, NULL, NULL, NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'7d38ea02-1baf-484b-b544-fb19d6596da3', N'9010002-010-011-0005', N'9010002', N'010', N'011', N'0005', N'', N'', N'', N'', N'5. Trợ cấp Phục viên', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:45.767' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'1ebce22c-8a98-40cf-bd20-d85a59e9c7bc', N'7a097777-89ac-4d76-a856-d1534c4070a9', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'ea716a14-c0ed-4c7f-a554-fb4deb95497a', N'9010001-010-011-0004-0001-0001-00', N'9010001', N'010', N'011', N'0004', N'0001', N'0001', N'00', N'', N'-  Trợ cấp 1 lần', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:34.270' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'5cca7dd1-b4e8-4273-86cc-ea4ff207409b', N'b43a61cb-2ad7-4944-ab5a-4ab5cfaef299', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'71fb9458-f068-4301-8406-fb5603cfe1c3', N'9020002-010-011-0001-0001', N'9020002', N'010', N'011', N'0001', N'0001', N'', N'', N'', N'2. QNCN', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:33.560' AS DateTime), NULL, 0, NULL, NULL, NULL, N'e89cbafd-a9b0-4b68-890b-d1d7db5b4824', N'8639c115-bdb3-49d5-9018-a1c9a3d24896', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'290247c6-691f-4bcf-8479-fbc7fe028fdf', N'9010004-010-011-0002-0002', N'9010004', N'010', N'011', N'0002', N'0002', N'', N'', N'', N'- Dịch vụ kỹ thuật', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-10T11:44:26.223' AS DateTime), CAST(N'2023-11-29T08:59:27.587' AS DateTime), NULL, 0, NULL, NULL, NULL, N'863a6a36-ed30-4f8f-b0f6-757aab07aee5', N'29147692-3954-4a2b-af22-bcbdb4dbc4ba', 2024, NULL, N'', N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8704ccaf-4357-4dc3-94e8-fc404a98c921', N'9010001-010-011-0007-0001-0002-00', N'9010001', N'010', N'011', N'0007', N'0001', N'0002', N'00', N'', N'- Trợ cấp KV 1 lần (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:36.067' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'f10c833a-c6a4-48d9-82ee-51682f8abe0b', N'58a28603-1d42-40be-bbb3-56f7c15d69ef', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8fb319c9-721f-453a-8195-fca1ba800625', N'9030002', N'9030002', N'', N'', N'', N'', N'', N'', N'', N'II. Bảo hiểm y tế thân nhân CN, VCQP(CY khác)', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-11-29T08:59:36.940' AS DateTime), NULL, 0, NULL, NULL, NULL, N'9ad55244-03c4-4c97-ba26-27af54495842', N'5a1234af-171a-44c9-98ed-9da7deff0188', 2024, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'c1a233ad-f800-4756-ac3e-fca57f3e68d5', N'9050001-010-011-0001', N'9050001', N'010', N'011', N'0001', N'', N'', N'', N'', N'Kinh phí chăm sóc sức khỏe ban đầu người lao động ', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-16T19:44:54.827' AS DateTime), CAST(N'2023-11-29T08:59:39.073' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'a519c162-3f2e-47ef-9cdf-2da72cfa40b7', N'2cdf8f93-5d04-45f8-afcc-5100068321e4', 2024, NULL, N'', N'admin', NULL, NULL, N'', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'daa59436-3c92-402c-9158-fd48c52ad042', N'9020002-010-011-0002-0000', N'9020002', N'010', N'011', N'0002', N'0000', N'', N'', N'', N'1. CC,CN, VCQP', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:34.297' AS DateTime), NULL, 0, NULL, NULL, NULL, N'3bdd2a48-b367-464b-b253-2406afc558f1', N'30fb2fc1-3a2d-4e69-bfa4-3cf1e164bdb8', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'd620eaf0-476b-4cb6-98b1-fd778e6559ee', N'9010002-010-011-0001-0001-0001-02-01', N'9010002', N'010', N'011', N'0001', N'0001', N'0001', N'02', N'01', N'* Dưới 14 ngày/tháng (ngày)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-05T09:33:14.260' AS DateTime), CAST(N'2023-11-29T08:58:38.600' AS DateTime), N'STNG', 0, NULL, NULL, NULL, N'0a70e747-0f95-4925-b6bd-0b91f4cf8ed1', N'0c64a47a-ae28-472f-82f7-a2d818d107ba', 2024, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, 1)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'938e06e7-57b7-4688-a745-fe0bde854a44', N'9020001-010-011-0002', N'9020001', N'010', N'011', N'0002', N'', N'', N'', N'', N'B. Người LĐ', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2023-12-06T11:06:10.747' AS DateTime), CAST(N'2023-11-29T08:59:32.103' AS DateTime), NULL, 0, NULL, NULL, NULL, N'4ce7992c-f413-434c-b785-1b99d6cbd025', N'eb1f7599-1ee7-4fef-be5e-e1ad25602a66', 2023, NULL, NULL, N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'91d541af-048b-4701-9435-fea3bf071ab1', N'9010009-010-011-0001', N'9010009', N'010', N'011', N'0001', N'', N'', N'', N'', N'I. Khối dự toán', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', NULL, CAST(N'2023-12-06T11:19:44.027' AS DateTime), NULL, 0, NULL, NULL, NULL, N'b45699c0-25f5-4f79-b26f-bed14c2fb846', N'09129a72-434f-452c-8b6c-d5159b6724c2', 2023, NULL, NULL, NULL, N'admin', NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'8ab2353c-57a6-4aad-8cbb-ff54a8cdea01', N'9010001-010-011-0007', N'9010001', N'010', N'011', N'0007', N'', N'', N'', N'', N'7. Trợ cấp thôi việc', 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-02-19T07:51:38.587' AS DateTime), CAST(N'2023-11-29T08:58:35.660' AS DateTime), N'SM', 0, NULL, NULL, NULL, N'58a28603-1d42-40be-bbb3-56f7c15d69ef', N'91465483-df1b-4262-9436-d87f8808cfac', 2024, NULL, N'M', N'admin', NULL, NULL, NULL, NULL, N'', N'', N'', NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[BH_DM_MucLucNganSach] ([iID], [sXauNoiMa], [sLNS], [sL], [sK], [sM], [sTM], [sTTM], [sNG], [sTNG], [sMoTa], [bHangCha], [iTrangThai], [bDuPhong], [bHangChaDuToan], [bHangChaQuyetToan], [bHangMua], [bHangNhap], [bHienVat], [bNgay], [bPhanCap], [bSoNguoi], [bTonKho], [bTuChi], [sChiTietToi], [dNgaySua], [dNgayTao], [iLoai], [iLock], [iID_MaDonVi], [iID_MaBQuanLy], [Log], [iID_MLNS], [iID_MLNS_Cha], [iNamLamViec], [sCPChiTietToi], [sDuToanChiTietToi], [sNguoiSua], [sNguoiTao], [sNhapTheoTruong], [sQuyetToanChiTietToi], [Tag], [sTNG1], [sTNG2], [sTNG3], [iLoaiNganSach], [sMaCB], [sMaPhuCap], [bHangChaDuToanDieuChinh], [sDuToanDieuChinhChiTietToi], [iDonViTinh]) VALUES (N'49b9d4cc-1f73-4ddc-91ce-ffd82940aff3', N'9010001-010-011-0003-0001-0009-00', N'9010001', N'010', N'011', N'0003', N'0001', N'0009', N'00', N'', N'- DS, PHSK sau TNLĐ, BNN (người)', 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NG', CAST(N'2024-01-12T09:06:02.833' AS DateTime), CAST(N'2023-11-29T08:58:33.873' AS DateTime), N'SNG', 0, NULL, NULL, NULL, N'77ec4558-40fd-4d3b-864d-df96aceccb53', N'c7647e43-8aca-4882-98f4-3ec7e0bc8714', 2023, NULL, NULL, N'admin', NULL, NULL, N'TNG', NULL, N'', N'', N'', NULL, NULL, NULL, 0, NULL, NULL)
GO


DELETE FROM [DM_CoSoYTe]
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd3140ec0-1fe4-4f06-9077-00010982744c', N'97011', 2024, N'Bệnh xá Trường Sĩ quan KTQS/TCKT', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd18e33f6-98d2-4a09-9950-01169ed9313a', N'97035', 2024, N'Bệnh xá Sư đoàn 3/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8be3fde8-c33e-445a-9287-013a96acc520', N'19015', 2024, N'Bệnh viện Quân y 91/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ccc1f969-8542-4255-a1b2-037046f0e798', N'97057', 2024, N'Bệnh xá Lữ 101/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'63d3a153-da4a-41b1-93bf-045374d4396b', N'31016', 2024, N'Viện Y học Hải quân/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ff2d78ee-f6d0-4dce-8034-059268f413cb', N'97216', 2024, N'Bệnh xá Trường Quân sự/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'81d6b0eb-ee8b-4e9f-8747-089d66ce5746', N'97021', 2024, N'Bệnh xá Học viện Hải quân/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'a44df2cf-0d82-4dcf-a352-0914e566e87d', N'01019', 2024, N'Viện Y học cổ truyền Quân đội      ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c3c55fdd-e88e-468a-b117-0964b7c0e880', N'97412', 2024, N'Bệnh xá Sư đoàn 341/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'cb3f1f5b-67b2-4a3b-b124-0aacdd8660ad', N'97305', 2024, N'Bệnh xá BCHQS tỉnh Hưng Yên/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'43309367-d10e-4095-a68e-0efafd286a22', N'97039', 2024, N'Bệnh xá Vùng 3/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'3d7643ec-150e-451b-9c01-0f5a675fd0e8', N'22048', 2024, N'Bệnh xá BCHQS Quảng Ninh/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fee3fbed-ad03-486d-b453-101ad91f29fa', N'97416', 2024, N'Bệnh xá Lữ đoàn 80/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'514dab61-3028-44e9-ac71-12d3fe6d1893', N'01917', 2024, N'Bệnh viện Bỏng Quốc gia Lê Hữu Trác', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ce21b29b-53b6-4bae-bbe8-14c0491d02c9', N'74021', 2024, N'Bệnh viện Quân y 4/QĐ4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'a98d502f-1133-4b66-9029-16f44994d2c8', N'97702', 2024, N'Bệnh xá BCHQS Bình Thuận/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'6607cd3d-a78c-4d05-a1dd-186c4195b404', N'97509', 2024, N'Bệnh xá BCHQS tỉnh Gia Lai/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'052cb01f-b8ec-4ce2-a659-1893048fd2a3', N'97613', 2024, N'Bệnh xá Sư đoàn 7/QĐ4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'88f39325-1f6a-42d9-96b3-191f7b01010f', N'79034', 2024, N'Bệnh viện Quân y 175', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8b798926-d306-4fa5-8c41-1a9dcde6e84b', N'48006', 2024, N'Bệnh viện Quân y 17/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'55fc98e5-42c9-4b14-bf6c-1b4bf2242d01', N'97907', 2024, N'Bệnh xá BCHQS Kiên Giang/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1d33d320-e035-4b64-8a0f-1df57d175138', N'97402', 2024, N'Bệnh xá BCHQS tỉnh Thanh Hóa/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'121311bc-0669-4c57-b25d-1e4f9702d285', N'97914', 2024, N'PKĐK QDY/Sư đoàn BB4/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'3a983f4c-2054-40cc-8359-1ea300880e35', N'97104', 2024, N'Bệnh xá Sư đoàn 346/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'6dcb6be7-56d2-43ba-8d0c-1f57d3ff0f2b', N'97395', 2024, N'Bệnh xá Sư đoàn 363/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'7032ac22-5e2e-472c-8298-21158f05963a', N'97812', 2024, N'Bệnh xá Sư đoàn 370/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'41176253-5880-4857-b6cb-21e5d893743e', N'97109', 2024, N'Bệnh xá Trường Sĩ quan Lục quân 2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'208bf1eb-998e-46d9-a76c-23f690ada593', N'97629', 2024, N'Bệnh xá Trường Sĩ quan Pháo binh/BCPB', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'56314426-74ce-4a28-8a5f-26d5ea71e8ea', N'19157', 2024, N'Bệnh xá 43/CHC/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c6eb98aa-0355-4983-bef2-27f27955ebb2', N'97609', 2024, N'Bệnh xá Lữ đoàn 202/BCTTG', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'60c63e50-b391-4aa2-be79-28a6901da638', N'08202', 2024, N'Bệnh xá Z113/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'901a555d-dc50-4563-b023-2af37bb4f7ca', N'26009', 2024, N'Bệnh viện Quân y 109/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'81161ff4-62e7-43f9-9fab-2bc07cd6d663', N'01927', 2024, N'Viện Y học phóng xạ và u bướu Quân đội/TCHC', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'bf44113c-20b5-49e3-ad0b-2cb13479e6d1', N'97413', 2024, N'Bệnh xá Lữ đoàn 16/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'dd92bd40-b0c6-41c4-a249-2d1c1749c75b', N'97053', 2024, N'Bệnh xá Học viện Quốc phòng', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'968f21f9-e4b5-48c2-b2ff-2db486956402', N'97040', 2024, N'Bệnh xá Trung đoàn 152/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'b08dbc70-89b2-4ace-8920-2f94d6573c38', N'36048', 2024, N'Bệnh xá BCHQS Nam Định/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'122f2155-0896-4b6d-8b6f-2f9825f26dad', N'97641', 2024, N'Bệnh xá Lữ đoàn 201/BCTTG', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c60b109f-0743-42c8-bf43-30d407fdb77c', N'97078', 2024, N'Bệnh xá Trường CĐ CNQP/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'4b167b66-7b1f-4173-b4d6-30e9b2521e25', N'97411', 2024, N'Bệnh xá Quân dân y Đoàn KTQP 92/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'f6c27ba9-46ce-4f31-92d7-31221f3764fa', N'97937', 2024, N'Bệnh xá Sư đoàn 375/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'2575ee79-bcc5-4ef9-8604-3225d239e204', N'97218', 2024, N'Bệnh xá Lữ đoàn 543/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'4d3ec54b-d306-43e4-923f-3313e2fe7cd2', N'97037', 2024, N'Đội Điều trị 486 Vùng 4/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'7adeb5e3-5e8a-4c53-85dd-35de1bd10d95', N'97615', 2024, N'BX Trường TCKT TTG/BCTTG', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e3c72482-36f3-49f7-9f85-35f88b3b42fd', N'97700', 2024, N'Bệnh xá Sư đoàn 5/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'bc4d03a1-33c3-497e-ac62-3ae3c1842145', N'97064', 2024, N'Bệnh xá Lữ đoàn 189/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'48d2ba1b-3b06-403d-983e-3e58481c12de', N'97317', 2024, N'Bệnh xá trường Quân sự/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd89f81df-5463-42e3-ae6a-419482133b18', N'64020', 2024, N'Bệnh viện Quân y 211/QĐ3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'860b7b7d-6896-44a3-9a7c-435698637bcc', N'97002', 2024, N'Đội Điều trị 78 vùng 5/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c5370758-5a2f-4b04-bb04-47b1d16ff581', N'97108', 2024, N'Bệnh xá Trường Sĩ quan Lục quân 1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fafa0e1a-b2a0-4d44-b358-495fead85421', N'97056', 2024, N'Bệnh xá Bộ Quốc phòng/BTTM', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'9dd0997a-af1d-4c5d-84ed-4ab7b6d1b7e0', N'92002', 2024, N'Bệnh viện Quân y 121/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'6eced121-0e33-45fa-81f0-4b0c6b3a06b2', N'01014', 2024, N'Bệnh viện Trung ương Quân đội 108', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'12ed8bad-c3e9-4e26-acf4-4bf2ad08d6a1', N'97089', 2024, N'Bệnh xá Tổng cục II', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'b2a26e75-b17d-4d63-a424-4f6cd565fc15', N'97028', 2024, N'Bệnh xá Z125/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e0f7a9d6-1e03-44fd-b762-5086a957c9c7', N'97200', 2024, N'Bệnh xá Sư đoàn  316/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'15539831-5ef4-4bca-a816-52648c53b767', N'01018', 2024, N'Viện Y học Phòng không Không quân/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'39b46618-371d-4bd2-be1b-56754fc865fe', N'97400', 2024, N'Bệnh xá Sư đoàn 324/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'303da3d8-6c16-4fbc-82da-57909c33ea47', N'97103', 2024, N'Bệnh xá Lữ đoàn 382/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'798ba403-c978-4f6a-9c08-5843d363df84', N'08203', 2024, N'Bệnh xá Z129/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e8904810-e753-4ddc-adbe-5b23c24c889c', N'04213', 2024, N'Bệnh xá BCHQS tỉnh Cao Bằng/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'f4e17f40-7e33-4857-8544-5b995e8b73d4', N'97934', 2024, N'Bệnh xá Sư đoàn 365/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'4c9664c9-4242-4025-9f23-5ef7d2e57307', N'97209', 2024, N'Bệnh xá Trung đoàn 82/f355/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'6179ddd7-fa9d-4143-ab84-5f5e6b4f98bb', N'97027', 2024, N'Bệnh xá Z117/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'3860f55f-e9a6-474c-a13b-61c544a8d191', N'97033', 2024, N'Bệnh xá Z195/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'7e25fa7c-6f02-40e1-97f4-621561ae3bb3', N'97058', 2024, N'Bệnh xá XNLH 751/TCKT', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'71483829-9b5b-4a01-b839-65e77b6fb2c3', N'97503', 2024, N'Bệnh xá BCHQS tỉnh Quảng Ngãi/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd924355e-45ba-4b5c-8470-669533d5eee6', N'97643', 2024, N'Bệnh xá Lữ đoàn 45/BCPB', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8d5115d3-b9be-41c4-aaab-689e75225883', N'97704', 2024, N'Bệnh xá BCHQS Long An/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'858b8cbf-9563-47b7-ab20-68c32474f1f3', N'97904', 2024, N'Bệnh xá Sư đoàn 330/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'feca3cfa-e4dc-43c0-af33-68c3919188ae', N'01015', 2024, N'Bệnh viện Quân y 354/TCHC', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'0cdff4f7-ec39-41fb-bc63-6d4fc93fafd9', N'97034', 2024, N'Bệnh xá Z183/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'230d6091-571d-4ce5-8508-6f98799965b3', N'97502', 2024, N'Bệnh xá BCHQS Quảng Nam/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'315a6974-4301-421b-99ed-71407cc413a6', N'27010', 2024, N'Bệnh viện Quân y 110/QK1 ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ce6ae223-3931-4c01-91ee-72254914e249', N'97630', 2024, N'Bệnh xá Lữ đoàn 204/BCPB', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'99b19a12-b258-4e82-b88b-722d69d018e9', N'01388', 2024, N'Bệnh xá Học viện Kỹ thuật Quân sự', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'2e3d8b25-b848-4a7b-b4f0-728804d1a8cb', N'97916', 2024, N'Bệnh xá Lữ đoàn 962/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1b3f6349-393d-4937-a9d9-745b62aba5b2', N'97301', 2024, N'Bệnh xá Sư đoàn 395/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'eb027245-aeaf-4b2e-a168-75aecd6f444f', N'97042', 2024, N'Bệnh xá Vùng 1/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c23ee267-a9a2-4fe4-9bc2-76e1e7535d43', N'97500', 2024, N'Bệnh xá Sư đoàn 2/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'c532f330-bad7-4da3-a353-7b4e81b4a47b', N'97705', 2024, N'TTYT Quân dân y tỉnh Tây Ninh/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'80d06d4d-69e2-4666-98b2-7fbbd810f973', N'97105', 2024, N'Bệnh xá QDY Đoàn KTQP 799/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fcdfa689-8ba4-4930-b1eb-80028440e64a', N'97314', 2024, N'Bệnh xá Lữ đoàn 214/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8a5d1d7f-9d67-4949-9fe5-809d01528d44', N'79061', 2024, N'Bệnh xá Sư đoàn 9/QĐ4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'a41f36af-fafe-4baf-950c-81d7b1117797', N'97211', 2024, N'Bệnh xá BCHQS Lai Châu/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd730929c-e40d-44e3-bc69-843a14aefdc5', N'62126', 2024, N'Bệnh xá Sư đoàn 10/QĐ3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'eefe931e-3aa8-47c6-8115-860483f0cfec', N'56012', 2024, N'Bệnh viện Quân y 87/TCHC', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'9b7abc7a-d602-4435-8852-86cdfeea50b9', N'97302', 2024, N'Bệnh xá BCHQS tỉnh Hà Nam/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'612ff77d-007d-4298-a89b-873dc2987164', N'97309', 2024, N'Bệnh xá Lữ đoàn 603/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'981dd5e7-6118-4cf5-8c93-885ec9695798', N'97401', 2024, N'Bệnh xá BCHQS tỉnh Hà Tĩnh/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'cf4fb5e0-b825-40b7-a0db-88c75ab3b845', N'01819', 2024, N'Bệnh viện Quân y 105/TCHC', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'13743e8b-e9ab-473d-9236-8b9e6cc46607', N'97616', 2024, N'BX Lữ đoàn 490/BCPB', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'b8b9ccc9-74f0-4057-b20c-8d22e5a55e10', N'79057', 2024, N'Bệnh viện Quân dân y Miền Đông/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'00ce7bb4-a75e-467c-90e0-8d4245b4b5fe', N'97205', 2024, N'Bệnh xá BCHQS tỉnh Yên Bái/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd13c0af0-333f-4b4b-8362-8eb71e129237', N'30014', 2024, N'Bệnh viện Quân y 7/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ffbba242-e22a-4546-8c65-8ed0582f2d70', N'97023', 2024, N'Bệnh xá Z127/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'da90c15e-da3c-4494-85e5-927c448969a5', N'46005', 2024, N'Bệnh viện Quân y 268/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'05dd4f26-8423-4def-ad63-955bd38b9fb4', N'97201', 2024, N'Bệnh xá BCHQS tỉnh Phú Thọ/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'53b00d27-93a1-46db-9717-9757801addaa', N'97403', 2024, N'Bệnh xá BCHQS tỉnh Quảng Bình/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'29eeb67b-439d-4c42-a396-986b7dc22056', N'97032', 2024, N'Bệnh xá Z199/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e1259f1f-516a-4738-9e57-99318684a8ee', N'97607', 2024, N'Bệnh xá Sư đoàn 309/QĐ4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'7cd3bb0b-7b76-4857-b8af-9a5d39a8f700', N'97306', 2024, N'Bệnh xá BCHQS tỉnh Thái Bình/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8bcc8b42-0a71-4e93-afdc-9d85512ed342', N'97507', 2024, N'Bệnh xá BCHQS tỉnh Phú Yên/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'dd6ea870-006b-497e-aae3-9e0acaa6e355', N'97936', 2024, N'Bệnh xá Học viện Phòng không -Không quân/QCPKKQ', CAST(N'2024-02-22T11:01:18.893' AS DateTime), NULL, 1, N'admin', NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'0e0a41ac-f4a0-47b9-a832-9e49d0e80ee8', N'97093', 2024, N'Bệnh xá BTL Vùng 2/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'73c154f1-bd7b-467e-b454-a033b283ff23', N'97204', 2024, N'Bệnh xá Quân dân y ĐKTQP 379/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'09f308fb-8208-4061-9792-a04a2f6e2007', N'97417', 2024, N'Bệnh xá Sư đoàn 968/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'362a6730-c841-426d-ac40-a06fcf491a0d', N'97202', 2024, N'Bệnh xá BCHQS tỉnh Điện Biên/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'3fe7f67b-4af2-4008-abdb-a0fd4c6cdf81', N'82020', 2024, N'Bệnh viện Quân y 120/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'17fd0462-03a9-454e-bb96-a4f0743f1ba5', N'97020', 2024, N'Bệnh xá Z121/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'be663a9b-63d5-412c-b8af-a76d75f834b6', N'97404', 2024, N'Bệnh xá BCHQS tỉnh Quảng Trị/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'eea9f248-2887-4b02-972f-a85e042a7a98', N'97816', 2024, N'Bệnh xá Sư đoàn 372/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'daf52d96-d6a5-4ca0-889a-a879aac60288', N'97902', 2024, N'BX Lữ đoàn 6/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'9472c6aa-84a8-4c6f-a3dc-ac782ea92b6e', N'52004', 2024, N'Bệnh viện Quân y 13/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e01904a9-ec50-4a43-8d2c-ad943738ccbb', N'97203', 2024, N'BX Lữ đoàn 604/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'87bb8e28-09c1-4fce-8dd3-b3c5cc6ea8c6', N'97018', 2024, N'Bệnh xá Z111/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ca6a518a-de17-4325-bce6-b3caa9984057', N'79419', 2024, N'TTYT Tân Cảng/TCT Tân Cảng/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'2d4cb556-e658-454a-9197-b3d81d4a2ac1', N'97025', 2024, N'Bệnh xá Z115/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'4ce454e9-321f-4cd4-bc0e-b4a54683d7bb', N'97640', 2024, N'BX Trường SQ Tăng thiết giáp/BCTTG', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1267d632-6a4c-4980-b1fb-b5a9e25eb934', N'75021', 2024, N'Bệnh viện Quân y 7B/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'107858c0-c771-46ea-8d69-b635d0187b66', N'97415', 2024, N'Bệnh xá Trường Quân sự/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'5d994bea-8c94-4010-8670-b744bafec2b1', N'97602', 2024, N'Bệnh xá Sư đoàn 304/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8f950156-93dc-471f-90ef-b77b724bcd86', N'97051', 2024, N'Bệnh xá Sư đoàn 377/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fcc720c5-b1a0-49a2-877b-b88bf6d91384', N'97316', 2024, N'Bệnh xá Lữ đoàn 454/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'69808e52-5533-459c-8a32-bc1622e42083', N'97031', 2024, N'Bệnh xá Z176/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e2c89cda-f52b-4961-8987-bf4bb0fb4b8d', N'97082', 2024, N'PKĐK Trung tâm An điều dưỡng tàu ngầm/QCHQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'496ea338-9ec6-4f36-bb1b-bf8946f90db9', N'68010', 2024, N'Bệnh xá Học viện Lục quân', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'99e7735a-1ee9-4f70-8357-c028837cb116', N'97511', 2024, N'Bệnh xá CT Cà phê 15/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'2413ccbf-bad7-4bdf-83d2-c346e68d6b4e', N'64246', 2024, N'Bệnh viện Quân y 15/BĐ15 ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'2ade1c14-1a23-4062-bfef-c7c75b452247', N'40026', 2024, N'Bệnh viện Quân y 4/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'5d667216-0ad0-4680-a319-cd7d754f183a', N'97208', 2024, N'Bệnh xá Lữ đoàn 297/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ddfa849a-f85e-4c19-8c59-d09dc57b5e48', N'97414', 2024, N'Bệnh xá Lữ đoàn 283/QK4', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1c8477f0-d2b4-495f-ac86-d10147c3f406', N'79016', 2024, N'Bệnh viện Quân y 7A/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ef272ca6-4870-41fe-97ec-d3721b617cca', N'97818', 2024, N'Bệnh viện Quân dân y 16/BĐ16', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'72918091-1d9a-4834-873d-d672d4bdb314', N'97320', 2024, N'Bệnh xá Lữ đoàn 242/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8031e98a-b1de-453d-ab91-d68d3fee182e', N'97030', 2024, N'Bệnh xá Z175/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fc44ec61-ec9e-4364-a19d-d8233a040f18', N'97094', 2024, N'Bệnh xá Sư đoàn 361/QCPKKQ', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'f8256b37-b4a3-4cb0-ae80-d9f25b712e07', N'97206', 2024, N'Bệnh xá BCHQS tỉnh Tuyên Quang/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'18a0125e-d919-4997-8f3c-da380b4d4142', N'97090', 2024, N'Bệnh xá Học viện Chính trị', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1994d142-4e57-4edd-8335-da6ff8946b1c', N'97024', 2024, N'Bệnh xá Z131/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'01ece3d2-e567-4252-ab64-e024ca9cf7f9', N'97015', 2024, N'Bệnh xá Trường Sĩ quan Chính trị', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'3b155b37-27fd-4064-a3f5-e07db9aaf970', N'97321', 2024, N'Bệnh xá Lữ đoàn 405/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'e7ed6ca0-14d6-474a-bbb1-e1637f865911', N'97910', 2024, N'BX Lữ đoàn 950/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'cd603254-8b64-4cd0-924a-e2ce7887a3f0', N'97906', 2024, N'Bệnh xá BCHQS tỉnh Bến Tre/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'b2b47931-596e-45ea-afb7-e334edc46021', N'97708', 2024, N'PKĐK Quân dân y Bình Dương/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ad10394c-cf4b-497f-af9d-e459b0366c46', N'20004', 2024, N'Bệnh xá BCHQS tỉnh Lạng Sơn/QK1', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'1b2fa867-a207-4cb0-82d3-e4bdcfa068b4', N'97605', 2024, N'Bệnh xá Sư đoàn 320/QĐ3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'9448b437-593d-483d-9f0b-e5f549fc9eae', N'97022', 2024, N'Bệnh xá Z114/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'81ccaaae-0133-4f40-94e1-e824b9533a70', N'97029', 2024, N'Bệnh xá Z143/TC CNQP', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'7a76acf7-9fbc-4357-987b-e923dc8ed1ac', N'97304', 2024, N'Bệnh xá Lữ đoàn 513/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'd624165b-6f18-4d88-b3c0-eb0f217599a9', N'97207', 2024, N'Bệnh xá Sư đoàn 355/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'8a26af2d-52b3-4bdd-bf38-ec50188bd0f7', N'97701', 2024, N'Bệnh xá BCHQS Bà Rịa-Vũng Tàu/QK7', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'ad32d01b-4202-46e2-870e-ef6fd2fb986f', N'97215', 2024, N'Bệnh xá Lữ đoàn 168/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fdb416c1-9747-42a8-9dea-f2e66ed5c0c1', N'62124', 2024, N'Bệnh xá BCHQS tỉnh Kon Tum/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'55333b47-6b04-470d-87b1-f8303f1d5907', N'97506', 2024, N'Bệnh xá BCHQS Đắk Lắk/QK5', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'43199139-1bf8-4699-a4c0-f95cedb9ac85', N'96129', 2024, N'Bệnh viện Quân dân y Cà Mau/QK9', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'fb709be6-b189-4819-8805-fac88e093474', N'97060', 2024, N'Bệnh xá Học viện Hậu cần', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'97f10e17-bd60-401c-b336-fcac338765d5', N'97307', 2024, N'Bệnh xá BCHQS tỉnh Hòa Bình/QK3', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'746b029c-ef86-4074-849f-fd9601a81b1c', N'14013', 2024, N'Bệnh viện Quân y 6/QK2', NULL, NULL, 1, NULL, NULL)
GO
INSERT [DM_CoSoYTe] ([iID_CoSoYTe], [iID_MaCoSoYTe], [iNamLamViec], [sTenCoSoYTe], [dNgaySua], [dNgayTao], [iTrangThai], [sNguoiSua], [sNguoiTao]) VALUES (N'298c8fc1-68ed-4bb3-b009-ff143d56bf0c', N'01016', 2024, N'Bệnh viện Quân y 103', NULL, NULL, 1, NULL, NULL)
GO
