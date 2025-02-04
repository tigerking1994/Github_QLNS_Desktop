/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]    Script Date: 11/1/2024 11:53:31 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]    Script Date: 11/1/2024 11:53:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_skt_phuong_an_phan_bo_skt_02b]
	@Nganh varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Khoi int,
	@DVT int,
	@BTongHop bit,
	@loaiNNS int
AS
BEGIN
	
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NS_SKT_MucLuc_tmp]') AND type in (N'U')) drop table NS_SKT_MucLuc_tmp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02B]') AND type in (N'U')) drop table DonViTempSKT02B;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02BN1]') AND type in (N'U')) drop table DonViTempSKT02BN1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n_1]') AND type in (N'U')) drop table skt_n_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n1]') AND type in (N'U')) drop table result_skt_n1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n]') AND type in (N'U')) drop table skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n]') AND type in (N'U')) drop table result_skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_dt]') AND type in (N'U')) drop table result_dt;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[donvimucluc]') AND type in (N'U')) drop table donvimucluc;

	IF (@Khoi = 0)
		BEGIN
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02B FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02BN1 FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec - 1
		END
	ELSE
		BEGIN
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02B FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec AND iKhoi = @Khoi
			SELECT iID_MaDonVi, sTenDonVi, iLoai INTO DonViTempSKT02BN1 FROM DonVi WHERE iTrangThai = 1 AND iNamLamViec = @NamLamViec - 1 AND iKhoi = @Khoi
		END

	select * into NS_SKT_MucLuc_tmp from NS_SKT_MucLuc ml
	where ml.iTrangThai = 1
		and (ml.iNamLamViec = @NamLamViec - 1 or ml.iNamLamViec = @NamLamViec)
		and ml.sNG in (SELECT * FROM f_split(@Nganh))

	-- Data năm hiện hành, năm n-1
	select ctct.iID_MaDonVi, ctct.sTenDonVi, ctct.sKyHieu, ctct.iLoai, ctct.fTuChi
	into skt_n_1
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra
	join DonViTempSKT02BN1 dt_dv ON ctct.iID_MaDonVi = dt_dv.iID_MaDonVi
	where ctct.iLoaiChungTu = 1
		and ctct.iNamLamViec = @NamLamViec - 1
		and ct.iID_MaNguonNganSach = @NguonNganSach
		and ct.iNamNganSach = @NamNganSach
		and (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
		and ((@BTongHop = 0) OR (@BTongHop = 1 AND ct.bDaTongHop = @BTongHop))

	--Result năm n-1
	select 
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
	   ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sSTT STT,
       ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   skt.iID_MaDonVi IdDonVi,
	   skt.sTenDonVi,
       sum(isnull(skt.fTuChi, 0)) fSoKiemTraNS
	into result_skt_n1
	from NS_SKT_MucLuc_tmp ml
	left join skt_n_1 skt on ml.sKyHieu = skt.sKyHieu
	where skt.iLoai = 4
		and ml.iNamLamViec = @NamLamViec - 1
	group by ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sSTT,
       ml.sSTTBC,
       ml.bHangCha,
	   skt.iID_MaDonVi,
	   skt.sTenDonVi

	-- Data dự toán năm n-1
	select map.sSKT_KyHieu,
		dt_ctct.iID_MaDonVi,
		dv.sTenDonVi,
		sum(isnull(dt_ctct.fTuChi, 0)) fDuToanDauNam
	into result_dt
	from NS_DT_ChungTuChiTiet dt_ctct
	join NS_MLSKT_MLNS map on dt_ctct.sXauNoiMa = map.sNS_XauNoiMa
	Join NS_DT_ChungTu dt_ct on dt_ctct.iID_DTChungTu = dt_ct.iID_DTChungTu
	join DonVi dv on dt_ctct.iID_MaDonVi = dv.iID_MaDonVi and dv.iNamLamViec = 2024 and dv.iTrangThai = 1
	where dt_ct.iLoaiDuToan = 1
		and dt_ct.iLoai = 1
		and dt_ctct.iNamLamViec = @NamLamViec - 1
		and map.iNamLamViec = @NamLamViec - 1
		and dt_ct.iid_MaNguonNganSach = @NguonNganSach
		and dt_ct.iNamNganSach = @NamNganSach
	group by map.sSKT_KyHieu, dt_ctct.iID_MaDonVi, dv.sTenDonVi

	-----------------------------------------------
	-- Data năm kế hoạch, năm n
	select ctct.iID_MaDonVi, ctct.sTenDonVi, ctct.sKyHieu, ctct.fTuChi
	into skt_n
	from NS_SKT_ChungTuChiTiet ctct
	join NS_SKT_ChungTu ct on ctct.iID_CTSoKiemTra = ct.iID_CTSoKiemTra
	join DonViTempSKT02B dt_dv on ctct.iID_MaDonVi = dt_dv.iID_MaDonVi
	where ctct.iLoaiChungTu = 1
		and ctct.iLoai = 4
		and ctct.iNamLamViec = @NamLamViec
		and ct.iID_MaNguonNganSach = @NguonNganSach
		and ct.iNamNganSach = @NamNganSach
		and (@loaiNNS = 0 OR ct.iLoaiNguonNganSach = @loaiNNS)
		and ((@BTongHop = 0) OR (@BTongHop = 1 AND ct.bDaTongHop = @BTongHop))

	--Result năm n
	select 
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sSTT STT,
       ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   n.iID_MaDonVi IdDonVi,
	   n.sTenDonVi,
       sum(isnull(n.fTuChi, 0)) fSoDuKienPB
	into result_skt_n
	from NS_SKT_MucLuc_tmp ml
	join skt_n n on ml.sKyHieu = n.sKyHieu
	where ml.iNamLamViec = @NamLamViec
	group by ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT,
       ml.iID_MLSKTCha,
       ml.sSTT,
       ml.sSTTBC,
       ml.bHangCha,
	   n.iID_MaDonVi,
	   n.sTenDonVi
	--------------------------------------
	select distinct dt.* into donvimucluc from
	(select IdDonVi, sKyHieu, sTenDonVi from result_skt_n1
	union
	select iID_MaDonVi, sSKT_KyHieu, sTenDonVi from result_dt
	union
	select IdDonVi, sKyHieu, sTenDonVi from result_skt_n) dt

	select 
	   ml.sL,
	   ml.sK,
	   ml.sM,
	   ml.sNG,
       ml.sKyHieu sKyHieu,
       ml.sMoTa,
       ml.iID_MLSKT IIdMlskt,
       ml.iID_MLSKTCha IIdMlsktCha,
       ml.sSTT STT,
       ml.sSTTBC SSTTBC,
       ml.bHangCha,
	   ml.IdDonVi,
	   ml.sTenDonVi,
	   n1.fSoKiemTraNS fSoKiemTraNS,
	   dt.fDuToanDauNam fDuToanDauNam,
       n.fSoDuKienPB fSoDuKienPB,
	   case when (isnull(n.fSoDuKienPB, 0) - isnull(n1.fSoKiemTraNS, 0)) > 0 then (isnull(n.fSoDuKienPB, 0) - isnull(n1.fSoKiemTraNS, 0))
	   else null end fTang,
	   case when (isnull(n1.fSoKiemTraNS, 0) - isnull(n.fSoDuKienPB, 0)) > 0 then (isnull(n1.fSoKiemTraNS, 0) - isnull(n.fSoDuKienPB, 0))
	   else null end fGiam
	from 
	(select ml.*, dv.IdDonVi, dv.sTenDonVi from NS_SKT_MucLuc_tmp ml
	join donvimucluc dv on ml.sKyHieu = dv.sKyHieu) ml
	left join result_skt_n1 n1 on ml.sKyHieu = n1.sKyHieu and n1.IdDonVi = ml.IdDonVi
	left join result_dt dt on ml.IdDonVi = dt.iID_MaDonVi and ml.sKyHieu = dt.sSKT_KyHieu
	left join result_skt_n n on n.sKyHieu = ml.sKyHieu and n.IdDonVi = ml.IdDonVi
	where ml.bHangCha = 0
		and isnull(ml.IdDonVi, '') <> '' 
	order by ml.sKyHieu

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NS_SKT_MucLuc_tmp]') AND type in (N'U')) drop table NS_SKT_MucLuc_tmp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02B]') AND type in (N'U')) drop table DonViTempSKT02B;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DonViTempSKT02BN1]') AND type in (N'U')) drop table DonViTempSKT02BN1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n_1]') AND type in (N'U')) drop table skt_n_1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n1]') AND type in (N'U')) drop table result_skt_n1;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[skt_n]') AND type in (N'U')) drop table skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_skt_n]') AND type in (N'U')) drop table result_skt_n;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[result_dt]') AND type in (N'U')) drop table result_dt;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[donvimucluc]') AND type in (N'U')) drop table donvimucluc;

END
GO
