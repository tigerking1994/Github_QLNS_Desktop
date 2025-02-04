/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_du_toan_thu_mua]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_tong_du_toan_thu_mua]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_tong_du_toan_thu_mua]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_da_quyet_toan]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_tong_da_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_tong_da_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_quy_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_quy_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_quy_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chi_tiet]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_bhyt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_bhyt_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_du_toan_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_tong_da_quyet_toan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_quy_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_quy_nam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_quy_nam]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 9/27/2023 4:10:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
	@NamLamViec int
AS
BEGIN
	select 
	ctct.iID_MucLucNganSach iID_MLNS,
	sum(ctct.iQSBQNam) iQSBQNam,
	sum(ctct.fLuongChinh) fLuongChinh,
	sum(ctct.fPhuCapChucVu) fPhuCapChucVu,
	sum(ctct.fPCTNNghe) fPCTNNghe,
	sum(ctct.fPCTNVuotKhung) fPCTNVuotKhung,
	sum(ctct.fNghiOm) fNghiOm,
	sum(ctct.fHSBL) fHSBL,
	sum(ctct.fTongQTLN) fTongQTLN
	from
	(select iID_KHT_BHXH from BH_KHT_BHXH
	where iNamChungTu = @NamLamViec
	and iLoaiTongHop = 1) ct
	join BH_KHT_BHXH_ChiTiet ctct
	on ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
	group by 
	ctct.iID_MucLucNganSach
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_quy_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtt_get_quy_nam]
	@NamLamViec int
AS
BEGIN
	select distinct 
		iQuyNam, 
		iQuyNamLoai, 
		sQuyNamMoTa 
	from BH_QTT_BHXH_ChungTu
	where iNamLamViec = @NamLamViec
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_da_quyet_toan]
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50),
	@QuyNamLoai int
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
			SUM(ctctdqt.fTongCong) fDaQuyetToan
			FROM BH_QTT_BHXH_ChungTu ctdqt
			JOIN BH_QTT_BHXH_ChungTu_ChiTiet ctctdqt ON ctdqt.iID_QTT_BHXH_ChungTu = ctctdqt.iID_QTT_BHXH_ChungTu
			WHERE ctdqt.iNamLamViec = @NamLamViec
			AND ctdqt.iID_MaDonVi = @MaDonVi
			AND ctdqt.iQuyNam < (SELECT iQuyNam from BH_QTT_BHXH_ChungTu WHERE iID_QTT_BHXH_ChungTu = @ChungTuId)
			AND ctdqt.iQuyNamLoai = @QuyNamLoai
			GROUP BY ctctdqt.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_tong_du_toan_dtt]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_get_tong_du_toan_dtt] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
	ctct.iID_MLNS,
	sum(ctct.fTongCong) fTongCong
	from
	BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi = @MaDonVi
	group by
	ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
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
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020001' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020001-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		union select NewId() iID_MLNS, (select distinct iID_MLNS_Cha from BH_DM_MucLucNganSach where sLNS = '9020002' and iID_MLNS_Cha is not null and bHangCha = 1) iID_MLNS_Cha, 1 bHangCha, '' sLNS, '' sL, '' sK, '' sM, '' sTM, '' sTTM, '' sNG, '' sTNG, '9020002-010-011-0003' sXauNoiMa, 'C. Truy thu (*)' sMoTa
		) mlns
		left join
		(select
			ct.iID_MaDonVi,
			ct.iNamLamViec,
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where --ct.iNamLamViec = @NamLamViec
			ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_rpt_quyet_toan_thu_nop_bhxh_bhyt_bhtn_quy]
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
		chungtudonvi.fThu_BHXH_NLD/@Donvitinh fThu_BHXH_NLD,
		chungtudonvi.fThu_BHXH_NSD/@Donvitinh fThu_BHXH_NSD,
		(chungtudonvi.fThu_BHXH_NLD + chungtudonvi.fThu_BHXH_NSD)/@Donvitinh fTongSoPhaiThuBHXH
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
			ctct.*
			from
			BH_QTT_BHXH_ChungTu ct
			join
			BH_QTT_BHXH_ChungTu_ChiTiet ctct on ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
			where ct.iNamLamViec = @NamLamViec
			and ct.iQuyNam = @IQuy
			and ct.iID_MaDonVi = @IdDonVi
			and ct.iLoaiTongHop = case when @IsTongHop = 1 then 2 else 1 end
			and ct.bDaTongHop = case when @IsTongHop = 1 then 1 else 0 end
				) chungtudonvi 
			on mlns.iID_MLNS = chungtudonvi.iID_MLNS
		
		order by mlns.sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chi_tiet]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_bhyt_chi_tiet]
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50)
AS
BEGIN
	SELECT 
		ct.*
		FROM
			(
				SELECT
					ctct.iID_QTTM_BHYT_ChungTu_ChiTiet,
					ct.iID_QTTM_BHYT_ChungTu,
					ct.iID_MaDonVi,
					ddv.sTenDonVi,
					ctct.fDuToan,
					ctct.fDaQuyetToan,
					ctct.fConLai,
					ctct.fSoPhaiThu,
					ctct.sGhiChu,
					ctct.iID_MLNS,
					ctct.iID_MLNS_Cha,
					ctct.sXauNoiMa,
					ctct.sLNS
				FROM 
					BH_QTTM_BHYT_Chung_Tu ct
				JOIN 
					BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu 
				LEFT JOIN 
					(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON ct.iID_MaDonVi = ddv.iID_MaDonVi
				WHERE
					ct.iID_QTTM_BHYT_ChungTu = @ChungTuId
			) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_bhyt_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO BH_QTTM_BHYT_Chung_Tu_ChiTiet
           (iID_QTTM_BHYT_ChungTu,
		  fDuToan,
		  fDaQuyetToan,
		  fConLai,
		  fSoPhaiThu,
		  sGhiChu,
		  iID_MLNS,
		  iID_MLNS_Cha,
		  sXauNoiMa,
		  sLNS)
    SELECT 
			@VoucherId,
			Sum(ctct.fDuToan),
			Sum(ctct.fDaQuyetToan),
			Sum(ctct.fConLai),
			Sum(ctct.fSoPhaiThu),
			null,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS
	FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
	JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa
	WHERE ctct.iID_QTTM_BHYT_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	AND mlns.iNamLamViec = @YearOfWork
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS;

	-- Danh dau chung tu da tong hop
	UPDATE BH_QTTM_BHYT_Chung_Tu SET bDaTongHop = 1 
	WHERE iID_QTTM_BHYT_ChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_quy_nam]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qttm_get_quy_nam]
	@NamLamViec int
AS
BEGIN
	select distinct 
		iQuyNam, 
		iQuyNamLoai, 
		sQuyNamMoTa 
	from BH_QTTM_BHYT_Chung_Tu
	where iNamLamViec = @NamLamViec
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_da_quyet_toan]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_get_tong_da_quyet_toan]
	@ChungTuId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(50),
	@QuyNamLoai int
AS
BEGIN
	SELECT ctctdqt.iID_MLNS,
			SUM(ctctdqt.fSoPhaiThu) fDaQuyetToan
			FROM BH_QTTM_BHYT_Chung_Tu ctdqt
			JOIN BH_QTTM_BHYT_Chung_Tu_ChiTiet ctctdqt ON ctdqt.iID_QTTM_BHYT_ChungTu = ctctdqt.iID_QTTM_BHYT_ChungTu
			WHERE ctdqt.iNamLamViec = @NamLamViec
			AND ctdqt.iID_MaDonVi = @MaDonVi
			AND ctdqt.iQuyNam < (SELECT iQuyNam from BH_QTTM_BHYT_Chung_Tu WHERE iID_QTTM_BHYT_ChungTu = @ChungTuId)
			AND ctdqt.iQuyNamLoai = @QuyNamLoai
	GROUP BY ctctdqt.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_du_toan_thu_mua]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_get_tong_du_toan_thu_mua]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
	ctct.iID_MLNS,
	sum(ctct.fDuToan) fDuToan
	from
	BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	where ctct.iNamLamViec = @NamLamViec
	and ctct.iID_MaDonVi = @MaDonVi
	group by
	ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_quyet_toan_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	ct.iID_QTTM_BHYT_ChungTu,
	ct.iID_MaDonVi IIDMaDonVi,
	dv.sTenDonVi AS sTenDonVi,
	ct.iNamLamViec,
	ct.iLoaiTongHop,
	ct.bDaTongHop,
	ct.bIsKhoa,
	ct.sSoChungTu,
	ct.dNgayChungTu,
	ct.iQuyNam,
	ct.iQuyNamLoai,
	ct.sQuyNamMoTa,
	ct.sMoTa,
	ct.sNguoiTao,
	ct.sNguoiSua,
	ct.dNgayTao,
	ct.dNgaySua,
	ct.sDS_MLNS SDsMlns,
	ct.sTongHop,
	ct.fDuToan,
	ct.fDaQuyetToan,
	ct.fConLai,
	ct.fSoPhaiThu
	
	FROM BH_QTTM_BHYT_Chung_Tu ct
	LEFT JOIN DonVi dv
	ON ct.iID_MaDonVi = dv.iID_MaDonVi
	WHERE dv.iNamLamViec = @YearOfWork
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 9/27/2023 4:10:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_KHT_BHXHChiTiet,
				bhct.iID_KHT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sTenLoaiDoiTuong,
				bhct.iQSBQNam,
				bhct.fLuongChinh,
				bhct.fPhuCapChucVu,
				bhct.fPCTNNghe,
				bhct.fPCTNVuotKhung,
				bhct.fNghiOm,
				bhct.fHSBL,
				bhct.fTongQTLN,
				bhct.fThu_BHXH_NLD,
				bhct.fThu_BHXH_NSD, 
				bhct.fTongThuBHXH, 
				bhct.fThu_BHYT_NLD,
				bhct.fThu_BHYT_NSD,
				bhct.fTongThuBHYT,
				bhct.fThu_BHTN_NLD,
				bhct.fThu_BHTN_NSD,
				bhct.fTongThuBHTN,
				bhct.fTongCong,
				bhct.iID_MucLucNganSach,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
		) ct;

END
;
;
GO
