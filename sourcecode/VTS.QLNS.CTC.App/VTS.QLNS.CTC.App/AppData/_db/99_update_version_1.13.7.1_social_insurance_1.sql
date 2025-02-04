/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_tong_hop_du_toan_thu_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ke_hoach_thu_mua_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtt_bhxh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtt_bhxh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thongtinkehoachthu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_get_luong_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_get_luong_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khtm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khtm]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_kht]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_kht]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_get_data_khc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_get_data_khc]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
	SELECT 
		ct.iID_DTT_BHXH_DieuChinh_ChiTiet,
		ct.iID_DTT_BHXH_DieuChinh,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sLNS,
		ct.sXauNoiMa,
		ct.sNoiDung,
		ct.fThuBHXH_NLD,
		ct.fThuBHXH_NSD,
		ct.fThuBHYT_NLD,
		ct.fThuBHYT_NSD,
		ct.fThuBHTN_NLD,
		ct.fThuBHTN_NSD,
		ct.fThuBHXH_NLD_QTDauNam,
		ct.fThuBHXH_NSD_QTDauNam,
		ct.fThuBHYT_NLD_QTDauNam,
		ct.fThuBHYT_NSD_QTDauNam,
		ct.fThuBHTN_NLD_QTDauNam,
		ct.fThuBHTN_NSD_QTDauNam,
		ct.fThuBHXH_NLD_QTCuoiNam,
		ct.fThuBHXH_NSD_QTCuoiNam,
		ct.fThuBHYT_NLD_QTCuoiNam,
		ct.fThuBHYT_NSD_QTCuoiNam,
		ct.fThuBHTN_NLD_QTCuoiNam,
		ct.fThuBHTN_NSD_QTCuoiNam,
		ct.fTongThuBHXH_NLD,
		ct.fTongThuBHXH_NSD,
		ct.fTongThuBHYT_NLD,
		ct.fTongThuBHYT_NSD,
		ct.fTongThuBHTN_NLD,
		ct.fTongThuBHTN_NSD,
		ct.fTongCong,
		ct.fThuBHXH_NLD_Tang,
		ct.fThuBHXH_NLD_Giam,
		ct.fThuBHXH_NSD_Tang,
		ct.fThuBHXH_NSD_Giam,
		ct.fThuBHXH_Tang,
		ct.fThuBHXH_Giam,
		ct.fThuBHYT_NLD_Tang,
		ct.fThuBHYT_NLD_Giam,
		ct.fThuBHYT_NSD_Tang,
		ct.fThuBHYT_NSD_Giam,
		ct.fThuBHYT_Tang,
		ct.fThuBHYT_Giam,
		ct.fThuBHTN_NLD_Tang,
		ct.fThuBHTN_NLD_Giam,
		ct.fThuBHTN_NSD_Tang,
		ct.fThuBHTN_NSD_Giam,
		ct.fThuBHTN_Tang,
		ct.fThuBHTN_Giam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu,
		ct.iNamLamViec
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTT_BHXH_DieuChinh bh
			JOIN 
				BH_DTT_BHXH_DieuChinh_ChiTiet bhct ON bh.iID_DTT_BHXH_DieuChinh = bhct.iID_DTT_BHXH_DieuChinh 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				and bh.iNamLamViec = @NamLamViec
		) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]
@YearOfWork int ,
@Date DateTime,
@LoaiDuToanNhan int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			   sSoChungTu,
			   sDSLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fDuToan AS fSoPhanBo
		INTO  tblNhanPhanBo
		FROM BH_DTTM_BHYT_ThanNhan 
		WHERE iNamLamViec = @YearOfWork 
			AND iLoaiDuToan = @LoaiDuToanNhan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))



		--Lấy danh sách dự toán đã được phân bổ
		SELECT ISNULL(sum(pb_ct.fDuToan),0) fDaPhanBo, pb_ct.iID_DTTM_BHYT_ThanNhan
		INTO tblChungTuNhanPhanBoMap
		FROM  BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet as pb_ct 
		WHERE pb_ct.iID_DTTM_BHYT_ThanNhan in (select iID_DTTM_BHYT_ThanNhan from  tblNhanPhanBo)
		GROUP BY pb_ct.iID_DTTM_BHYT_ThanNhan


		-----Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT  npb.iID_DTTM_BHYT_ThanNhan as iID_DTTM_BHYT_ThanNhan,
			    npb.sSoChungTu, 
				npb.sDSLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				ISNULL(npb.fSoPhanBo,0) - ISNULL(npbm.fDaPhanBo,0) AS fSoChuaPhanBo
		FROM tblNhanPhanBo AS npb
		left join tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTTM_BHYT_ThanNhan = npbm.iID_DTTM_BHYT_ThanNhan

	   DROP TABLE tblNhanPhanBo;	
       DROP TABLE tblChungTuNhanPhanBoMap;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khc]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_khc]
	@NamLamViec int,
	@SoChungTu nvarchar(500)
AS
BEGIN
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	BH_KHC_CheDoBHXH_ChiTiet ctct 
	join
	(select * from BH_KHC_CheDoBHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_CheDoBHXH = ct.id
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	BH_KHC_K_ChiTiet ctct 
	join
	(select * from BH_KHC_K
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_K = ct.iID_BH_KHC_K
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	BH_KHC_KCB_ChiTiet ctct 
	join
	(select * from BH_KHC_KCB
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KCB = ct.iID_BH_KHC_KCB
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS
	----------------
	union
	select
		ct.iID_MaDonVi IIdMaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS IID_MucLucNganSach,
		ctct.fTienKeHoachThucHienNamNay
	from
	BH_KHC_KinhPhiQuanLy_ChiTiet ctct 
	join
	(select * from BH_KHC_KinhPhiQuanLy
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHC_KinhPhiQuanLy = ct.iID_BH_KHC_KinhPhiQuanLy
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_kht]
	@NamLamViec int,
	@SoChungTu nvarchar(100)
AS
BEGIN
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa XauNoiMa,
		ctct.iID_MucLucNganSach,
		ctct.fThu_BHXH_NLD,
		ctct.fThu_BHXH_NSD,
		ctct.fThu_BHYT_NLD,
		ctct.fThu_BHYT_NSD,
		ctct.fThu_BHTN_NLD,
		ctct.fThu_BHTN_NSD
	from
	BH_KHT_BHXH_ChiTiet ctct 
	join
	(select * from BH_KHT_BHXH
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_kht_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_kht_tong_hop] 
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa XauNoiMa,
		ctct.fThu_BHXH_NLD,
		ctct.fThu_BHXH_NSD,
		ctct.fThu_BHYT_NLD,
		ctct.fThu_BHYT_NSD,
		ctct.fThu_BHTN_NLD,
		ctct.fThu_BHTN_NSD
	from BH_KHT_BHXH_ChiTiet ctct
	join
	(select top 1 * from BH_KHT_BHXH
		where iNamLamViec = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		and sTongHop is not null
		and bIsKhoa = 1) ct on ctct.iID_KHT_BHXH = ct.iID_KHT_BHXH
	join BH_DM_MucLucNganSach mlns on ctct.iID_MucLucNganSach = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_khtm] 
	@NamLamViec int,
	@SoChungTu nvarchar(100)
AS
BEGIN
	
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.iID_MLNS,
		ctct.fThanhTien
	from
	BH_KHTM_BHYT_ChiTiet ctct 
	join
	(select * from BH_KHTM_BHYT
		where iNamLamViec = @NamLamViec
		and sSoChungTu in ((SELECT * FROM f_split(@SoChungTu)))) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_get_data_khtm_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_get_data_khtm_tong_hop]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select
		ct.iID_MaDonVi,
		mlns.sXauNoiMa,
		mlns.sMoTa,
		ctct.fThanhTien
	from BH_KHTM_BHYT_ChiTiet ctct
	join
	(select top 1 * from BH_KHTM_BHYT
		where iNamLamViec = @NamLamViec
		and iID_MaDonVi = @MaDonVi
		and sTongHop is not null
		and bKhoa = 1) ct on ctct.iID_KHTM_BHYT = ct.iID_KHTM_BHYT
	join BH_DM_MucLucNganSach mlns on ctct.iID_NoiDung = mlns.iID_MLNS

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_qtt_bhxh_chungtu_chitiet_tao_tonghop]
	@VoucherIds ntext,
	@VoucherId nvarchar(100),
	@YearOfWork int,
	@UserName nvarchar(100)
AS
BEGIN
	INSERT INTO BH_QTT_BHXH_ChungTu_ChiTiet
           (iID_QTT_BHXH_ChungTu,
		  iQSBQNam,
		  fLuongChinh,
		  fPCChucVu,
		  fPCTNNghe,
		  fPCTNVuotKhung,
		  fNghiOm,
		  fHSBL,
		  fTongQTLN,
		  fDuToan,
		  fDaQuyetToan,
		  fConLai,
		  fThu_BHXH_NLD,
		  fThu_BHXH_NSD,
		  fTongSoPhaiThuBHXH,
		  fThu_BHYT_NLD,
		  fThu_BHYT_NSD,
		  fTongSoPhaiThuBHYT,
		  fThu_BHTN_NLD,
		  fThu_BHTN_NSD,
		  fTongSoPhaiThuBHTN,
		  fTongCong,
		  sGhiChu,
		  iID_MLNS,
		  iID_MLNS_Cha,
		  sXauNoiMa,
		  sLNS)
    SELECT 
			@VoucherId,
			Sum(ctct.iQSBQNam),
			Sum(ctct.fLuongChinh),
			Sum(ctct.fPCChucVu),
			Sum(ctct.fPCTNNghe),
			Sum(ctct.fPCTNVuotKhung),
			Sum(ctct.fNghiOm),
			Sum(ctct.fHSBL),
			Sum(ctct.fTongQTLN),
			Sum(ctct.fDuToan),
			Sum(ctct.fDaQuyetToan),
			Sum(ctct.fConLai),
			Sum(ctct.fThu_BHXH_NLD),
			Sum(ctct.fThu_BHXH_NSD),
			Sum(ctct.fTongSoPhaiThuBHXH),
			Sum(ctct.fThu_BHYT_NLD),
			Sum(ctct.fThu_BHYT_NSD),
			Sum(ctct.fTongSoPhaiThuBHYT),
			Sum(ctct.fThu_BHTN_NLD),
			Sum(ctct.fThu_BHTN_NSD),
			Sum(ctct.fTongSoPhaiThuBHTN),
			Sum(ctct.fTongCong),
			null,
			mlns.iID_MLNS,
			mlns.iID_MLNS_Cha,
			mlns.sXauNoiMa,
			mlns.sLNS
	FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
	JOIN BH_DM_MucLucNganSach mlns ON ctct.sXauNoiMa = mlns.sXauNoiMa
	WHERE ctct.iID_QTT_BHXH_ChungTu IN (SELECT * FROM f_split(@VoucherIds))
	AND mlns.iNamLamViec = @YearOfWork
	GROUP BY mlns.iID_MLNS, mlns.iID_MLNS_Cha, mlns.sXauNoiMa, mlns.sLNS;

	-- Danh dau chung tu da tong hop
	UPDATE BH_QTT_BHXH_ChungTu SET bDaTongHop = 1 
	WHERE iID_QTT_BHXH_ChungTu in
		(SELECT *
		 FROM f_split(@VoucherIds))
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtt_get_luong_kht]    Script Date: 12/25/2023 10:34:15 AM ******/
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
	where iNamLamViec = @NamLamViec
	and iLoaiTongHop = 1) ct
	join BH_KHT_BHXH_ChiTiet ctct
	on ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
	group by 
	ctct.iID_MucLucNganSach
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qttm_get_tong_nhan_du_toan_thu_mua]
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	select 
		ctct.iID_MLNS,
		sum(ctct.fDuToan) fDuToan
	from
		BH_DTTM_BHYT_ThanNhan_ChiTiet ctct
		join BH_DTTM_BHYT_ThanNhan ct on ctct.iID_DTTM_BHYT_ThanNhan = ct.iID_DTTM_BHYT_ThanNhan
	where ct.iNamLamViec = @NamLamViec
		and ct.iID_MaDonVi = @MaDonVi
		group by
		ctct.iID_MLNS
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_report_ke_hoach_cap_bo_sung_kcb_bhyt]
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
			sum(ctct.fDaQuyetToan)/@Donvitinh as fDaQuyetToan,
			sum(ctct.fDaCapUng)/@Donvitinh as fDaCapUng, 
			(sum(ctct.fDaCapUng)/@Donvitinh - sum(ctct.fDaQuyetToan)/@Donvitinh) as fThuaThieu, 
			sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung, 
			ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
		inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
		where ctct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and ct.iNamLamViec = @NamLamViec
			and ct.iLoaiTongHop <> 2 and ct.sDSSoChungTuTongHop is null
			and ct.iQuy = @IQuy
		group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_report_thong_tri_cap_bo_sung_kcb_bhyt] 
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@Donvitinh int
AS
BEGIN
	select 
		row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
		mlns.sLNS,
		(mlns.sL + ' - ' + mlns.sK) as sLK,
		mlns.sM,
		mlns.sTM,
		mlns.sTTM,
		mlns.sNG,
		mlns.sMoTa,
		sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung, 
		ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
		csyt.sTenCoSoYTe as sTenCoSoYTe
	from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
	inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
	inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
	inner join BH_DM_MucLucNganSach as mlns on ctct.iID_MLNS = mlns.iID_MLNS
	where ctct.iID_MaCoSoYTe = @IdCsYTe
		and ct.iNamLamViec = @NamLamViec
		and mlns.iNamLamViec = @NamLamViec
		and ct.iLoaiTongHop <> 2 and ct.sDSSoChungTuTongHop is null
		and ct.iQuy = @IQuy
	group by
	mlns.sLNS,
	(mlns.sL + ' - ' + mlns.sK),
	mlns.sM,
	mlns.sTM,
	mlns.sTTM,
	mlns.sNG,
	mlns.sMoTa,
	ctct.iID_MaCoSoYTe,
	csyt.sTenCoSoYTe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_report_tong_hop_cap_bo_sung_kcb_bhyt] 
	@IdCsYTe NVARCHAR(MAX),
	@IQuy int,
	@NamLamViec int,
	@UserName NVARCHAR(100),
	@Donvitinh int
AS
BEGIN
		select 
			row_number() over (order by ctct.iID_MaCoSoYTe) as sTT,
			sum(ctct.fDaQuyetToan)/@Donvitinh as fDaQuyetToan, 
			(sum(ctct.fDaCapUng)/@Donvitinh - sum(ctct.fDaQuyetToan)/@Donvitinh) as fThuaThieu, 
			sum(ctct.fSoCapBoSung)/@Donvitinh as fSoCapBoSung, 
			ctct.iID_MaCoSoYTe as iID_MaCoSoYTe,
			csyt.sTenCoSoYTe as sTenCoSoYTe
		from BH_CP_CapBoSung_KCB_BHYT_ChiTiet as ctct
		inner join BH_CP_CapBoSung_KCB_BHYT as ct on ctct.iID_CTCapPhatBS = ct.iID_CTCapPhatBS
		inner join DM_CoSoYTe as csyt on csyt.iID_MaCoSoYTe = ctct.iID_MaCoSoYTe
		where ctct.iID_MaCoSoYTe In (SELECT * FROM f_split(@IdCsYTe))
			and ct.iNamLamViec = @NamLamViec
			and ct.iLoaiTongHop = 2 and ct.sDSSoChungTuTongHop is not null
			and ct.iQuy = @IQuy
		group by ctct.iID_MaCoSoYTe, csyt.sTenCoSoYTe
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thongtinkehoachthu_index]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thongtinkehoachthu_index] 
	@YearOfWork int
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamLamViec,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop,
	KHT.sBangLuongKeHoach SBangLuongKeHoach
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamLamViec = @YearOfWork
	ORDER BY KHT.dNgayQuyetDinh DESC
END;
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtt_bhxh_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dtt_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int
AS
BEGIN
	SELECT 
		ct.iID_DTT_BHXH as DttBHXHId,
		ct.*
	FROM
		(
			SELECT
				bh.iID_MaDonVi IIdMaDonVi,
				bh.iNamLamViec,
				ddv.sTenDonVi,
				bhct.iID_DTT_BHXH_ChiTiet,
				bhct.iID_DTT_BHXH,
				bhct.iID_LoaiDoiTuong,
				bhct.sLoaiDoiTuong,
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
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.iID_MLNS,
				bhct.iID_MLNS_Cha,
				bhct.sK,
				bhct.sL,
				bhct.sLNS,
				bhct.sM,
				bhct.sMoTa,
				bhct.sNG,
				bhct.sTM,
				bhct.sTNG,
				bhct.sTNG1,
				bhct.sTNG2,
				bhct.sTNG3,
				bhct.sTTM,
				bhct.sXauNoiMa,
				bhct.sGhiChu,
				bhct.iID_MaDonVi
			FROM 
				BH_DTT_BHXH_ChungTu bh
			JOIN 
				BH_DTT_BHXH_ChungTu_ChiTiet bhct ON bh.iID_DTT_BHXH = bhct.iID_DTT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_DTT_BHXH = @KhtBHXHId
		) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ke_hoach_thu_mua_bhyt_index]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_ke_hoach_thu_mua_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamLamViec,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_kht_bhxh_chi_tiet]
	@KhtBHXHId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_KHT_BHXH as BhKhtBHXHId,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_MaDonVi,
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
				bhct.sNguoiSua,
				bhct.iNamLamViec,
				bhct.sLNS,
				bhct.sXauNoiMa
			FROM 
				BH_KHT_BHXH bh
			JOIN 
				BH_KHT_BHXH_ChiTiet bhct ON bh.iID_KHT_BHXH = bhct.iID_KHT_BHXH 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHT_BHXH = @KhtBHXHId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]
	@YearOfWork int,
	@DaTongHop bit
AS
BEGIN
	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamLamViec,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamLamViec = @YearOfWork
	AND KHT.bDaTongHop = @DaTongHop
	ORDER BY KHT.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop] 
	@YearOfWork int,
	@LoaiTongHop int,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;

	SELECT
	KHT.iID_KHT_BHXH,
	KHT.sSoChungTu,
	KHT.iNamLamViec,
	KHT.dNgayChungTu,
	KHT.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHT.sMoTa,
	KHT.bIsKhoa,
	KHT.fTongKeHoach,
	KHT.sSoQuyetDinh,
	KHT.dNgayQuyetDinh,
	KHT.sNguoiTao,
	KHT.sNguoiSua,
	KHT.dNgayTao,
	KHT.dNgaySua,
	KHT.iLoaiTongHop,
	KHT.sTongHop,
	KHT.fBHXH_NLD FThuBHXHNLDDong,
	KHT.fBHXH_NSD FThuBHXHNSDDong,
	KHT.fTongBHXH FThuBHXH,
	KHT.fBHYT_NLD FThuBHYTNLDDong,
	KHT.fBHYT_NSD FThuBHYTNSDDong,
	KHT.fTongBHYT FTongBHYT,
	KHT.fBHTN_NLD FThuBHTNNLDDong,
	KHT.fBHTN_NSD FThuBHTNNSDDong,
	KHT.fTongBHTN FThuBHTN,
	KHT.fTong,
	KHT.bDaTongHop
	into #tblChungTu
	FROM BH_KHT_BHXH KHT
	LEFT JOIN DonVi DV
	ON KHT.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHT.iNamLamViec = @YearOfWork
	AND KHT.iLoaiTongHop = @LoaiTongHop
	ORDER BY KHT.dNgayQuyetDinh DESC

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTu ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.iID_MaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTu ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.iID_MaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bIsKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTu;
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chi_tiet]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chi_tiet]
	@KhtmBHYTId nvarchar(100),
	@NamLamViec int,
	@MaDonVi nvarchar(100)
AS
BEGIN
	SELECT 
		ct.iID_KHTM_BHYT IdKhtmBHYT,
		ct.*
	FROM
		(
			SELECT
				ddv.iID_DonVi,
				ddv.iID_MaDonVi,
				bh.sMoTa SMoTa,
				bhct.iID_KHTM_BHYT_ChiTiet,
				bhct.iID_KHTM_BHYT,
				bhct.iID_NoiDung,
				bhct.sTenNoiDung,
				bhct.iSoNguoi,
				bhct.iSoThang,
				bhct.fDinhMuc,
				bhct.fThanhTien,
				bhct.sGhiChu,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				ddv.sTenDonVi,
				bhct.iNamLamViec,
				bhct.sLNS,
				bhct.sXauNoiMa
			FROM 
				BH_KHTM_BHYT bh
			JOIN 
				BH_KHTM_BHYT_ChiTiet bhct ON bh.iID_KHTM_BHYT = bhct.iID_KHTM_BHYT 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_KHTM_BHYT = @khtmBHYTId
				and bh.iID_MaDonVi = @MaDonVi
		) ct;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]
	@YearOfWork int,
	@LoaiTongHop int,
	@DaTongHop bit,
	@UserName nvarchar(100)
AS
BEGIN
	DECLARE @CountDonViCha int;
	SELECT 
		@CountDonViCha = count(*) FROM (SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName AND iNamLamViec = @YearOfWork AND iTrangThai = 1) nddv
	INNER JOIN
		(SELECT * FROM DonVi WHERE iNamLamViec = @YearOfWork AND iTrangThai = 1 AND iLoai = 0) dv
	ON dv.iID_MaDonVi = nddv.iID_MaDonVi;
	
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamLamViec,
	KHTM.dNgayChungTu,
	KHTM.iID_MaDonVi AS IIDMaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	KHTM.sMoTa,
	KHTM.bKhoa,
	KHTM.fTongKeHoach,
	KHTM.sSoQuyetDinh,
	KHTM.dNgayQuyetDinh,
	KHTM.sNguoiTao,
	KHTM.sNguoiSua,
	KHTM.dNgayTao,
	KHTM.dNgaySua,
	KHTM.iLoaiTongHop,
	KHTM.sTongHop,
	KHTM.iTongSoNguoi,
	KHTM.iTongSoThang,
	KHTM.fTongDinhMuc,
	KHTM.fTongThanhTien,
	KHTM.bDaTongHop
	into #tblChungTu
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHTM.iNamLamViec = @YearOfWork
	AND KHTM.iLoaiTongHop = @LoaiTongHop
	AND KHTM.bDaTongHop = @DaTongHop
	ORDER BY KHTM.dNgayQuyetDinh DESC

	IF @CountDonViCha = 0
		SELECT 
			ct.*
		FROM #tblChungTu ct
		INNER JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		ORDER BY sSoChungTu desc;
	ELSE
		SELECT 
			ct.*
		FROM #tblChungTu ct
		LEFT JOIN 
			(SELECT * FROM NguoiDung_DonVi WHERE iID_MaNguoiDung = @UserName and iNamLamViec = @YearOfWork and iTrangThai = 1) dv
		ON ct.IIDMaDonVi = dv.iID_MaDonVi
		WHERE ((dv.iID_MaDonVi IS NULL AND ct.bKhoa = 1) OR (dv.iID_MaDonVi IS NOT NULL))
		ORDER BY sSoChungTu desc;

	drop table #tblChungTu;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_du_toan_thu_chi]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@dvt int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_KHT_BHXH_ChiTiet ctct
LEFT JOIN BH_KHT_BHXH ct 
ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fThanhTien, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_KHTM_BHYT_ChiTiet ctct
LEFT JOIN BH_KHTM_BHYT ct 
ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_NoiDung = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVQP, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS, 0)) +
			SUM(ISNULL(ctct.fTienLDHD, 0)) +
			SUM(ISNULL(ctct.fTienQNCN, 0)) +
			SUM(ISNULL(ctct.fTienSQ, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_KHC_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_KHC_CheDoBHXH ct 
ON ct.ID = ctct.iID_KHC_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTienCanBo, 0)) +
			SUM(ISNULL(ctct.fTienQuanLuc, 0)) +
			SUM(ISNULL(ctct.fTienTaiChinh, 0)) +
			SUM(ISNULL(ctct.fTienQuanY, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_KHC_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_KHC_KinhPhiQuanLy ct 
ON ct.iID_BH_KHC_KinhPhiQuanLy = ctct.iID_KHC_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_KHC_KCB_ChiTiet ctct
LEFT JOIN BH_KHC_KCB ct 
ON ct.iID_BH_KHC_KCB = ctct.iID_KHC_KCB
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTienKeHoachThucHienNamNay, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_KHC_K_ChiTiet ctct
LEFT JOIN BH_KHC_K ct 
ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 


SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @dvt AS N'1.1.0,1,Dự toán thu BHXH',
		SUM(ISNULL(fSoTienThuBHTN, 0))/ @dvt AS N'1.2.0,2,Dự toán thu BHTN',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @dvt AS N'1.3.0,3,Dự toán thu BHYT quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @dvt AS N'1.4.0,4,Dự toán thu BHYT người lao động'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Dự toán thu BHXH], 
	  [1.2.0,2,Dự toán thu BHTN], 
	  [1.3.0,3,Dự toán thu BHYT quân nhân], 
	  [1.4.0,4,Dự toán thu BHYT người lao động])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @dvt AS N'1.5.1,-,Thân nhân quân nhân',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @dvt AS N'1.5.2,-,Thân nhân người lao động'
		--SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		--SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.5.1,-,Thân nhân quân nhân], 
	  [1.5.2,-,Thân nhân người lao động])
	  --[1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  --[1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  --[1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  --[1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @dvt AS N'2.1.0,1,Dự toán chi các chế độ BHXH'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Dự toán chi các chế độ BHXH])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @dvt AS N'2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Dự toán chi kinh phí quản lý BHXH, BHYT])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @dvt AS N'2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Dự toán chi kinh phí KCB tại quân y đơn vị])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		--SUM(ISNULL(fSoTienChiTTB, 0)) AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) AS N'2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK'
		--SUM(ISNULL(fSoTienChiNLD, 0)) AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		--SUM(ISNULL(fSoTienChiHSSV, 0)) AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		--SUM(ISNULL(fSoTienChiCSYT, 0)) AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      --([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  ([2.4.0,4,Dự toán chi kinh phí KCB Trường Sa - DK]) 
	  --[2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  --[2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  --[2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,A,Dự toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,B,Dự toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.5.0,5,Dự toán thu BHYT TN' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.5%')
--UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]    Script Date: 12/25/2023 10:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_tong_hop_quyet_toan_thu_chi]
@NamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@IsTongHop int
AS
BEGIN
	
SELECT
	--ml.sLNS,
	--ml.sM,
	--ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHXH_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHXH_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHXH,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002')
			THEN 
				SUM(ISNULL(ctct.fThu_BHTN_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHTN_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHTN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '1'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_QN,
	CASE 
		WHEN ml.sLNS IN ('9020001', '9020002') AND ml.sM = '2'
			THEN 
				SUM(ISNULL(ctct.fThu_BHYT_NSD, 0)) +
				SUM(ISNULL(ctct.fThu_BHYT_NLD, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_NLD
	INTO #temp1
FROM BH_QTT_BHXH_ChungTu_ChiTiet ctct
LEFT JOIN BH_QTT_BHXH_ChungTu ct 
ON ct.iID_QTT_BHXH_ChungTu = ctct.iID_QTT_BHXH_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS = '9030001'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_TNQN,
	CASE 
		WHEN ml.sLNS = '9030002'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_CNVQP,
	CASE 
		WHEN ml.sLNS = '9030003'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HVQS,
	CASE 
		WHEN ml.sLNS = '9030004'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_HSSV,
	CASE 
		WHEN ml.sLNS = '9030005'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_SQDB,
	CASE 
		WHEN ml.sLNS = '9030006'
		THEN SUM(ISNULL(ctct.fSoPhaiThu, 0))
		ELSE 0 
	END AS fSoTienThuBHYT_LUU_HS
	INTO #temp2
FROM BH_QTTM_BHYT_Chung_Tu_ChiTiet ctct
LEFT JOIN BH_QTTM_BHYT_Chung_Tu ct 
ON ct.iID_QTTM_BHYT_ChungTu = ctct.iID_QTTM_BHYT_ChungTu
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MLNS = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010001', '9010002')
		THEN 
			SUM(ISNULL(ctct.fTienCNVCQP_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienHSQBS_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienLDHD_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienQNCN_ThucChi, 0)) +
			SUM(ISNULL(ctct.fTienSQ_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCheDo
	INTO #temp3
FROM BH_QTC_Nam_CheDoBHXH_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_CheDoBHXH ct 
ON ct.ID_QTC_Nam_CheDoBHXH = ctct.iID_QTC_Nam_CheDoBHXH
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010003')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiKinhPhiQuanLy
	INTO #temp4
FROM BH_QTC_Nam_KinhPhiQuanLy_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KinhPhiQuanLy ct 
ON ct.ID_QTC_Nam_KinhPhiQuanLy = ctct.iID_QTC_Nam_KinhPhiQuanLy
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010006', '9010007')
		THEN 
			SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiQuanYDonVi
	INTO #temp5
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KCB_QuanYDonVi ct 
ON ct.ID_QTC_Nam_KCB_QuanYDonVi = ctct.iID_QTC_Nam_KCB_QuanYDonVi
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

SELECT
	--ml.sLNS,
	--ml.sM,
	ml.sMoTa,
	--ct.iID_MaDonVi,
	--dv.sTenDonVi
	CASE 
		WHEN ml.sLNS IN ('9010009')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTTB,
	CASE 
		WHEN ml.sLNS IN ('9010004', '9010005')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiTSDK,
	CASE 
		WHEN ml.sLNS IN ('9050001')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiNLD,
	CASE 
		WHEN ml.sLNS IN ('9050002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiHSSV,
	CASE 
		WHEN ml.sLNS IN ('9040001', '9040002')
		THEN SUM(ISNULL(ctct.fTien_ThucChi, 0))
		ELSE 0 
	END AS fSoTienChiCSYT
	INTO #temp6
FROM BH_QTC_Nam_KPK_ChiTiet ctct
LEFT JOIN BH_QTC_Nam_KPK ct 
ON ct.ID_QTC_Nam_KPK = ctct.ID_QTC_Nam_KPK_ChiTiet
INNER JOIN BH_DM_MucLucNganSach ml 
ON ctct.iID_MucLucNganSach = ml.iID_MLNS AND ct.iNamLamViec = ml.iNamLamViec
	AND ml.iTrangThai = 1
	--AND ml.sLNS = '9020001'
	--AND ml.sM = ''
INNER JOIN 
(SELECT iID_MaDonVi, sTenDonVi, iLoai
	FROM DonVi
	WHERE iTrangThai = 1
	AND iNamLamViec = 2023
	AND iID_MaDonVi in (SELECT * FROM f_split(@IdDonVi))
) dv 
ON dv.iID_MaDonVi = ct.iID_MaDonVi
WHERE ct.iNamLamViec = 2023
	--AND ct.iLoaiTongHop = CASE WHEN 1 = 1 THEN 2 ELSE 1 END
	--AND ct.bDaTongHop = CASE WHEN 1 = 1 then 1 ELSE 0 END
GROUP BY ml.sLNS, ml.sM, ml.sMoTa

IF NOT EXISTS(SELECT 1 FROM #temp1) INSERT INTO #temp1 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp2) INSERT INTO #temp2 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp3) INSERT INTO #temp3 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp4) INSERT INTO #temp4 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp5) INSERT INTO #temp5 DEFAULT VALUES 
IF NOT EXISTS(SELECT 1 FROM #temp6) INSERT INTO #temp6 DEFAULT VALUES 




SELECT NoiDung, SoTien  
INTO #temp7
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHXH, 0)) / @DonViTinh AS N'1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)',
		SUM(ISNULL(fSoTienThuBHTN, 0)) / @DonViTinh AS N'1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)',
		SUM(ISNULL(fSoTienThuBHYT_QN, 0)) / @DonViTinh AS N'1.3.1,-,BHYT quân nhân (Phụ lục IV)',
		SUM(ISNULL(fSoTienThuBHYT_NLD, 0)) / @DonViTinh AS N'1.3.2,-,BHYT người lao động (Phụ lục V)'
   FROM #temp1) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.1.0,1,Thu bảo hiểm xã hội (Phụ lục II)], 
	  [1.2.0,2,Thu bảo hiểm thất nghiệp (Phụ lục III)], 
	  [1.3.1,-,BHYT quân nhân (Phụ lục IV)], 
	  [1.3.2,-,BHYT người lao động (Phụ lục V)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienThuBHYT_TNQN, 0)) / @DonViTinh AS N'1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_CNVQP, 0)) / @DonViTinh AS N'1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)',
		SUM(ISNULL(fSoTienThuBHYT_HVQS, 0)) / @DonViTinh AS N'1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_HSSV, 0)) / @DonViTinh AS N'1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_SQDB, 0)) / @DonViTinh AS N'1.3.8,-,BHYT SQ dự bị (Phụ lục VII)',
		SUM(ISNULL(fSoTienThuBHYT_LUU_HS, 0)) / @DonViTinh AS N'1.3.6,-,BHYT lưu học sinh (Phụ lục VII)'
   FROM #temp2) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([1.3.3,-,BHYT thân nhân quân nhân (Phụ lục VI)], 
	  [1.3.4,-,BHYT thân nhân CN, viên chức QP (Phụ lục VI)], 
	  [1.3.7,-,BHYT HV QS xã phường (Phụ lục VII)], 
	  [1.3.5,-,BHYT học sinh, sinh viên (Phụ lục VII)], 
	  [1.3.8,-,BHYT SQ dự bị (Phụ lục VII)], 
	  [1.3.6,-,BHYT lưu học sinh (Phụ lục VII)])  
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiCheDo, 0)) / @DonViTinh AS N'2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)'
   FROM #temp3) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.1.0,1,Chi các chế độ BHXH (Phụ lục VIII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiKinhPhiQuanLy, 0)) / @DonViTinh AS N'2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)'
   FROM #temp4) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.2.0,2,Chi KP quản lý BHXH, BHYT (Phụ lục IX)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiQuanYDonVi, 0)) / @DonViTinh AS N'2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)'
   FROM #temp5) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.4.2,-,Chi KCB tại quân y đơn vị (Phụ lục XII)])
) AS unpvt

UNION 

SELECT NoiDung, SoTien  
FROM   
   (SELECT 
		SUM(ISNULL(fSoTienChiTTB, 0)) / @DonViTinh AS N'2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)',
		SUM(ISNULL(fSoTienChiTSDK, 0)) / @DonViTinh AS N'2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)',
		SUM(ISNULL(fSoTienChiNLD, 0)) / @DonViTinh AS N'2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)',
		SUM(ISNULL(fSoTienChiHSSV, 0)) / @DonViTinh AS N'2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)',
		SUM(ISNULL(fSoTienChiCSYT, 0)) / @DonViTinh AS N'2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)'
   FROM #temp6) p  
UNPIVOT  
   (SoTien FOR NoiDung IN   
      ([2.3.0,3,Chi mua sắm TTB y tế (Phụ lục X)], 
	  [2.4.1,-,Chi KCB cho quân nhân tại TS-DK (Phụ lục XI)], 
	  [2.4.3,-,Chi kinh phí CSSK BĐ người lao động (Phụ lục XIII)], 
	  [2.4.4,-,Chi kinh phí CSSK BĐ HSSV (Phụ lục XIV)], 
	  [2.4.5,-,Chi KCB tại các cơ sở y tế (Phụ lục XV)])
) AS unpvt



SELECT * FROM #temp7
UNION SELECT N'1.0.0,I,Quyết toán thu' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.%')
UNION SELECT N'2.0.0,II,Quyết toán chi' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.%')
UNION SELECT N'1.3.0,3,Thu bảo hiểm y tế' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '1.3%')
UNION SELECT N'2.4.0,4,Chi KCB BHYT' AS NoiDung, (SELECT SUM(SoTien) FROM #temp7 WHERE NoiDung LIKE '2.4%')


DROP TABLE #temp1
DROP TABLE #temp2
DROP TABLE #temp3
DROP TABLE #temp4
DROP TABLE #temp5
DROP TABLE #temp6
DROP TABLE #temp7

END
GO
