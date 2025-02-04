/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thtc_get_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dttm_theo_ct]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dttm_theo_ct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dttm_theo_ct]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dttm]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dttm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dttm]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_so_quyet_dinh_dtt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 12/21/2023 9:32:13 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoanthumua_dotnhan_phanbo_find_all]    Script Date: 12/21/2023 9:32:13 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500)
AS
BEGIN
	
	select
		sSoChungTu,
		sSoQuyetDinh,
		dNgayQuyetDinh,
		sDSID_MaDonVi
	from BH_DTT_BHXH_PhanBo_ChungTu
	where iNamLamViec = @NamLamViec
		and sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and Convert(varchar, dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_chung_tu_duoc_xem_luy_ke_dttm]
	@NamLamViec int,
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500)
AS
BEGIN
	
	select
		sSoChungTu,
		sSoQuyetDinh,
		dNgayQuyetDinh,
		sDS_IDMaDonVi
	from BH_DTTM_BHYT_ThanNhan_PhanBo
	where iNamLamViec = @NamLamViec
		and sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and Convert(varchar, dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt]
	@NamLamViec int
AS
BEGIN
	select distinct dtt.sSoQuyetDinh,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTT_BHXH_PhanBo_ChungTu dtt
	where iNamLamViec = @NamLamViec
	--union
	--select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	--	from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dtt_theo_ct]
	@NamLamViec int
AS
BEGIN

	select distinct dtt.sSoChungTu, dtt.sSoQuyetDinh,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTT_BHXH_PhanBo_ChungTu dtt
	where iNamLamViec = @NamLamViec

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dttm]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dttm]
	@NamLamViec int
AS
BEGIN
	select distinct dtt.sSoQuyetDinh,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
	where iNamLamViec = @NamLamViec
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_get_so_quyet_dinh_dttm_theo_ct]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_get_so_quyet_dinh_dttm_theo_ct]
	@NamLamViec int
AS
BEGIN
	select distinct dtt.sSoChungTu, dtt.sSoQuyetDinh,
		Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
	from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
	where iNamLamViec = @NamLamViec
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTT_BHXH_PhanBo_ChungTu dtt
		where iNamLamViec = @NamLamViec
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
		where iNamLamViec = @NamLamViec
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTC_PhanBoDuToanChi dtt
		where iNamChungTu = @NamLamViec

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int
AS
BEGIN
	declare @DataDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongDuToan float, BhxhNsddDongDuToan float, BHXHTongCongDuToan float, BhtnNldDongDuToan float, BhtnNsddDongDuToan float, BHTNTongCongDuToan float);
	declare @DataHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), BhxhNldDongHachToan float, BhxhNsddDongHachToan float, BHXHTongCongHachToan float, BhtnNldDongHachToan float, BhtnNsddDongHachToan float, BHTNTongCongHachToan float);

	INSERT INTO @DataDuToan (sTenDonVI, idDonVi, BhxhNldDongDuToan, BhxhNsddDongDuToan, BHXHTongCongDuToan, BhtnNldDongDuToan, BhtnNsddDongDuToan, BHTNTongCongDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))
		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiDuToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	INSERT INTO @DataHachToan (sTenDonVI, idDonVi, BhxhNldDongHachToan, BhxhNsddDongHachToan, BHXHTongCongHachToan, BhtnNldDongHachToan, BhtnNsddDongHachToan, BHTNTongCongHachToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   BhxhNldDong = SUM(IsNull(A.ThuBHXHNLDDong,0)),
		   BhxhNsddDong = SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   SUM(IsNull(A.ThuBHXHNLDDong,0)) + SUM(IsNull(A.ThuBHXHNSDDong,0)),
		   BhtnNldDong = SUM(IsNull(A.ThuBHTNNLDDong,0)),
		   BhtnNsddDong = SUM(IsNull(A.ThuBHTNNSDDong,0)),
		   SUM(IsNull(A.ThuBHTNNLDDong,0)) + SUM(IsNull(A.ThuBHTNNSDDong,0))

		FROM
		  (SELECT ml.iID_MLNS ,
				ml.iID_MLNS_Cha,
				ml.sNG,
				ml.sMoTa,
				ml.bHangCha,
				ml.sXauNoiMa,
				ctct.iID_MaDonVi,
				IsNull(ctct.fBHXH_NLD, 0) ThuBHXHNLDDong,
				IsNull(ctct.fBHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fBHTN_NLD, 0) ThuBHTNNLDDong,
				IsNull(ctct.fBHTN_NSD, 0) ThuBHTNNSDDong
		   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
		   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @NamLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @KhoiHachToan
		   WHERE ct.iNamLamViec = @NamLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @NamLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@LstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
		GROUP BY dt_dv.sTenDonVi,
				dt_dv.id;

	SELECT dt.idDonVi, 
	dt.sTenDonVI, 
	IsNull(dt.BhxhNldDongDuToan, 0)/@DVT BhxhNldDongDuToan, 
	IsNull(dt.BhxhNsddDongDuToan, 0)/@DVT BhxhNsddDongDuToan, 
	IsNull(ht.BhxhNldDongHachToan, 0)/@DVT BhxhNldDongHachToan, 
	IsNull(ht.BhxhNsddDongHachToan, 0)/@DVT BhxhNsddDongHachToan,
	IsNull(dt.BHXHTongCongDuToan, 0)/@DVT BHXHTongCongDuToan,
	IsNull(ht.BHXHTongCongHachToan, 0)/@DVT BHXHTongCongHachToan,
	IsNull(dt.BhtnNldDongDuToan, 0)/@DVT BhtnNldDongDuToan, 
	IsNull(dt.BhtnNsddDongDuToan, 0)/@DVT BhtnNsddDongDuToan,
	IsNull(ht.BhtnNldDongHachToan, 0)/@DVT BhtnNldDongHachToan, 
	IsNull(ht.BhtnNsddDongHachToan, 0)/@DVT BhtnNsddDongHachToan,
	IsNull(dt.BHTNTongCongDuToan, 0)/@DVT BHTNTongCongDuToan,
	IsNull(ht.BHTNTongCongHachToan, 0)/@DVT BHTNTongCongHachToan
	FROM @DataDuToan dt
	LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt] 
	@NamLamViec int,
	@LstSelectedUnit ntext,
	@KhoiDuToan nvarchar(50),
	@KhoiHachToan nvarchar(50),
	@SM nvarchar(50),
	@SoQuyetDinh nvarchar(500),
	@NgayQuyetDinh nvarchar(500),
	@DVT int
AS
BEGIN
	declare @BhytDuToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50),BhytNSDDongDuToan float, BhytNLDDongDuToan float, TongBhytDuToan float);
	declare @BhytHachToan table (idDonVi nvarchar(50),sTenDonVI nvarchar(200), sm nvarchar(50), BhytNSDDongHachToan float,BhytNLDDongHachToan float, TongBhytHachToan float);

	INSERT INTO @BhytDuToan (sTenDonVI, idDonVi, sm, BhytNSDDongDuToan, BhytNLDDongDuToan, TongBhytDuToan)
		SELECT 
		   dt_dv.sTenDonVi,
		   dt_dv.id idDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongDuToan, 0)) BhytNSDDongDuToan,
		   SUM(IsNull(A.ThuBHYTNLDDongDuToan, 0)) BhytNLDDongDuToan,
		   SUM(IsNull(A.TongBhytDuToan, 0)) TongBhytDuToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongDuToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongDuToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytDuToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiDuToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @NamLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;

	INSERT INTO @BhytHachToan (idDonVi, sTenDonVI, sm, BhytNSDDongHachToan, BhytNLDDongHachToan, TongBhytHachToan)
		SELECT
			dt_dv.id idDonVi,
		   dt_dv.sTenDonVi,
		   A.sm,
		   SUM(IsNull(A.ThuBHYTNSDDongHachToan, 0)) BhytNSDDongHachToan,
		   SUM(IsNull(A.ThuBHYTNLDDongHachToan, 0)) BhytNLDDongHachToan,
		   SUM(IsNull(A.TongBhytHachToan, 0)) TongBhytHachToan

	FROM
	  (SELECT ml.sm,
			   ml.sMoTa,
			   ctct.iID_MaDonVi,
			   IsNull(ctct.fBHYT_NSD, 0) ThuBHYTNSDDongHachToan,
			   IsNull(ctct.fBHYT_NLD, 0) ThuBHYTNLDDongHachToan,
			   IsNull(ctct.fThuBHYT, 0) TongBhytHachToan
	   FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	   LEFT JOIN BH_DTT_BHXH_PhanBo_ChungTu ct ON ct.iID_DTT_BHXH_PhanBo_ChungTu = ctct.iID_DTT_BHXH_ChungTu
	   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
	   AND ml.iNamLamViec = @NamLamViec
	   AND ml.iTrangThai = 1
	   AND ml.sLNS = @KhoiHachToan
	   AND ml.sM = @SM
	   WHERE ct.iNamLamViec = @namLamViec
	   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh))
	   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))) AS A 
	   JOIN
	  (SELECT iID_MaDonVi AS id,
			  sTenDonVi, iLoai
	   FROM DonVi
	   WHERE iTrangThai = 1
	   AND iNamLamViec = @namLamViec
	   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
	GROUP BY dt_dv.sTenDonVi,
			dt_dv.id,
			A.sm;

	SELECT dt.idDonVi, 
	dt.sTenDonVI,
	IsNull(dt.BhytNLDDongDuToan, 0)/@DVT BhytNldDongDuToan, 
	IsNull(dt.BhytNSDDongDuToan, 0)/@DVT BhytNsddDongDuToan, 
	IsNull(ht.BhytNLDDongHachToan, 0)/@DVT BhytNldDongHachToan, 
	IsNull(ht.BhytNSDDongHachToan, 0)/@DVT BhytNsddDongHachToan,
	IsNull(dt.TongBhytDuToan, 0)/@DVT BHYTTongCongDuToan,
	IsNull(ht.TongBhytHachToan, 0)/@DVT BHYTTongCongHachToan
	FROM @BhytDuToan dt
	LEFT JOIN @BhytHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int
AS
BEGIN
	declare @tbl_HSSV table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HSSV float);
	declare @tbl_LuuHS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_LuuHS float);
	declare @tbl_HVQS table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_HVQS float);
	declare @tbl_SQDuBi table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_SQDuBi float);

	INSERT INTO @tbl_HSSV (IdDonVi, TenDonVI, ThanhTien_HSSV)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_LuuHS (IdDonVi, TenDonVI, ThanhTien_LuuHS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_HVQS (IdDonVi, TenDonVI, ThanhTien_HVQS)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @tbl_SQDuBi (IdDonVi, TenDonVI, ThanhTien_SQDuBi)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   LEFT JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.HSSV/@dvt HSSV, 
		result.LuuHS/@dvt LuuHS,
		result.TongHSSV/@dvt TongHSSV,
		result.HVQS/@dvt HVQS,
		result.SQDuBi/@dvt SQDuBi,
		(result.TongHSSV + result.HVQS + result.SQDuBi)/@dvt TongCongHSSV
		FROM
		(SELECT hssv.idDonVi, 
		hssv.TenDonVI,
		IsNull(hssv.ThanhTien_HSSV, 0) HSSV,
		IsNull(luuhs.ThanhTien_LuuHS, 0) LuuHS,
		(IsNull(hssv.ThanhTien_HSSV + luuhs.ThanhTien_LuuHS, 0)) TongHSSV,
		IsNull(hvsq.ThanhTien_HVQS, 0) HVQS,
		IsNull(sqdb.ThanhTien_SQDuBi, 0) SQDuBi
		FROM @tbl_HSSV hssv
		LEFT JOIN @tbl_LuuHS luuhs ON hssv.idDonVi = luuhs.idDonVi
		LEFT JOIN @tbl_HVQS hvsq ON hssv.idDonVi = hvsq.idDonVi
		LEFT JOIN @tbl_SQDuBi sqdb ON hssv.idDonVi = sqdb.idDonVi) result
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 12/21/2023 9:32:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50),
	@soQuyetDinh nvarchar(500),
	@ngayQuyetDinh nvarchar(500),
	@dvt int
AS
BEGIN
	declare @TNQN_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_DuToan float);
	declare @TN_CNVQP_DuToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_DuToan float);
	declare @TNQN_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TNQN_HachToan float);
	declare @TN_CNVQP_HachToan table (IdDonVi nvarchar(50),TenDonVI nvarchar(200), ThanhTien_TN_CNVQP_HachToan float);

	INSERT INTO @TNQN_DuToan (IdDonVi, TenDonVI, ThanhTien_TNQN_DuToan)
	SELECT
		dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = 0
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TN_CNVQP_DuToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_DuToan)
	SELECT 
	   dt_dv.id,
		dt_dv.sTenDonVi,
	   SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		  (SELECT
				   ml.sMoTa,
				   ctct.iID_MaDonVi,
				   IsNull(ctct.fDuToan, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
		   JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = 0
		   WHERE ct.iNamLamViec = @namLamViec
		   AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
		   AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
		   ) AS A 
		   JOIN
		  (SELECT iID_MaDonVi AS id,
				  sTenDonVi, iLoai
		   FROM DonVi
		   WHERE iTrangThai = 1
		   AND iNamLamViec = @namLamViec
		   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;

	INSERT INTO @TNQN_HachToan (IdDonVi, TenDonVI, ThanhTien_TNQN_HachToan)
	SELECT 
			dt_dv.id,
			dt_dv.sTenDonVi,
		  SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
			 (SELECT
					  ml.sMoTa,
					  ctct.iID_MaDonVi,
					  IsNull(ctct.fDuToan, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			  JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = 1
			  WHERE ct.iNamLamViec = @namLamViec
				AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
				AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			  ) AS A 
			   JOIN
			  (SELECT iID_MaDonVi AS id,
					  sTenDonVi, iLoai
			   FROM DonVi
			   WHERE iTrangThai = 1
			   AND iNamLamViec = @namLamViec
			   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
			GROUP BY
			dt_dv.sTenDonVi,
			dt_dv.id;

	INSERT INTO @TN_CNVQP_HachToan (IdDonVi, TenDonVI, ThanhTien_TN_CNVQP_HachToan)
	SELECT 
		dt_dv.id,
		dt_dv.sTenDonVi,
		SUM(IsNull(A.ThanhTien, 0)) ThanhTien
		FROM
		(SELECT
					ml.sMoTa,
					ctct.iID_MaDonVi,
					IsNull(ctct.fDuToan, 0) ThanhTien,
					ml.sLNS
			FROM BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
			JOIN BH_DTTM_BHYT_ThanNhan_PhanBo ct ON ct.iID_DTTM_BHYT_ThanNhan_PhanBo = ctct.iID_DTTM_BHYT_ThanNhan_PhanBo
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MLNS = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = 1
			WHERE ct.iNamLamViec = @namLamViec
			AND ct.sSoQuyetDinh in (SELECT * FROM f_split(@soQuyetDinh))
			AND Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@ngayQuyetDinh))
			) AS A 
			JOIN
			(SELECT iID_MaDonVi AS id,
					sTenDonVi, iLoai
			FROM DonVi
			WHERE iTrangThai = 1
			AND iNamLamViec = @namLamViec
			AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi = dt_dv.id
		GROUP BY
		dt_dv.sTenDonVi,
		dt_dv.id;
		
		SELECT result.idDonVi, 
		result.TenDonVI STenDonVi, 
		result.TN_QN_DT/@dvt TNQNDuToan, 
		result.TN_CNVQP_DT/@dvt TNCNVQPDuToan,
		result.TongDuToan/@dvt TongDuToan,
		result.TN_QN_HT/@dvt TNQNHachToan,
		result.TN_CNVQP_HT/@dvt TNCNVQPHachToan,
		result.TongHachToan/@dvt TongHachToan,
		(result.TongDuToan + result.TongHachToan)/@dvt TongCongThanNhan
		FROM
		(SELECT tnqn_dt.idDonVi, 
		tnqn_dt.TenDonVI,
		IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) TN_QN_DT,
		IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0) TN_CNVQP_DT,
		(IsNull(tnqn_dt.ThanhTien_TNQN_DuToan, 0) + IsNull(tncn_dt.ThanhTien_TN_CNVQP_DuToan, 0)) TongDuToan,
		IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) TN_QN_HT,
		IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0) TN_CNVQP_HT,
		(IsNull(tnqn_ht.ThanhTien_TNQN_HachToan, 0) + IsNull(tncn_ht.ThanhTien_TN_CNVQP_HachToan, 0)) TongHachToan
		FROM @TNQN_DuToan tnqn_dt
		LEFT JOIN @TN_CNVQP_DuToan tncn_dt ON tnqn_dt.idDonVi = tncn_dt.idDonVi
		LEFT JOIN @TNQN_HachToan tnqn_ht ON tnqn_dt.idDonVi = tnqn_ht.idDonVi
		LEFT JOIN @TN_CNVQP_HachToan tncn_ht ON tnqn_dt.idDonVi = tncn_ht.idDonVi) result
END
;
GO
