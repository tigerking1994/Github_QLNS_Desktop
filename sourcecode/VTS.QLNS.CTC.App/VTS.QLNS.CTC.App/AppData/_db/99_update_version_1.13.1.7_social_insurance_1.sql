/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhyt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhyt]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_kht_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_kht_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_khtm_bhyt_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 9/14/2023 5:25:41 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_kht_bhxh_chung_tu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	KHT.iNamChungTu,
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
	AND KHT.iNamChungTu = @YearOfWork
	AND KHT.bDaTongHop = @DaTongHop
	ORDER BY KHT.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_kht_bhxh_chung_tu_chi_tiet_tong_hop]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	KHT.iNamChungTu,
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
	AND KHT.iNamChungTu = @YearOfWork
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
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet]    Script Date: 9/14/2023 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet]
	@YearOfWork int,
	@DaTongHop bit
AS
BEGIN
	SELECT
	KHTM.iID_KHTM_BHYT,
	KHTM.sSoChungTu,
	KHTM.iNamChungTu,
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
	FROM BH_KHTM_BHYT KHTM
	LEFT JOIN DonVi DV
	ON KHTM.iID_MaDonVi = DV.iID_MaDonVi
	WHERE DV.iNamLamViec = @YearOfWork
	AND KHTM.iNamChungTu = @YearOfWork
	AND KHTM.bDaTongHop = @DaTongHop
	ORDER BY KHTM.dNgayQuyetDinh DESC
END
GO
/****** Object:  StoredProcedure [dbo].[sp_khtm_bhyt_chung_tu_chi_tiet_tong_hop]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	KHTM.iNamChungTu,
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
	AND KHTM.iNamChungTu = @YearOfWork
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
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhxh]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50),
	@dvt int
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
                ct.iID_MaDonVi,
                IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   WHERE ct.iNamChungTu = @namLamViec
	 --AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
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
                ct.iID_MaDonVi,
                IsNull(ctct.fThu_BHXH_NLD, 0) ThuBHXHNLDDong,
                IsNull(ctct.fThu_BHXH_NSD, 0) ThuBHXHNSDDong,
				IsNull(ctct.fThu_BHTN_NLD, 0) ThuBHTNNLDDong,
                IsNull(ctct.fThu_BHTN_NSD, 0) ThuBHTNNSDDong
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   WHERE ct.iNamChungTu = @namLamViec
	 --AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
   JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec
   AND iID_MaDonVi in (SELECT * FROM f_split(@lstSelectedUnit))) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.BhxhNldDongDuToan, 0)/@dvt BhxhNldDongDuToan, 
IsNull(dt.BhxhNsddDongDuToan, 0)/@dvt BhxhNsddDongDuToan, 
IsNull(ht.BhxhNldDongHachToan, 0)/@dvt BhxhNldDongHachToan, 
IsNull(ht.BhxhNsddDongHachToan, 0)/@dvt BhxhNsddDongHachToan,
IsNull(dt.BHXHTongCongDuToan, 0)/@dvt BHXHTongCongDuToan,
IsNull(ht.BHXHTongCongHachToan, 0)/@dvt BHXHTongCongHachToan,
IsNull(dt.BhtnNldDongDuToan, 0)/@dvt BhtnNldDongDuToan, 
IsNull(dt.BhtnNsddDongDuToan, 0)/@dvt BhtnNsddDongDuToan,
IsNull(ht.BhtnNldDongHachToan, 0)/@dvt BhtnNldDongHachToan, 
IsNull(ht.BhtnNsddDongHachToan, 0)/@dvt BhtnNsddDongHachToan,
IsNull(dt.BHTNTongCongDuToan, 0)/@dvt BHTNTongCongDuToan,
IsNull(ht.BHTNTongCongHachToan, 0)/@dvt BHTNTongCongHachToan
FROM @DataDuToan dt
LEFT JOIN @DataHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_kht_du_toan_thu_bhyt]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@khoiDuToan nvarchar(50),
	@khoiHachToan nvarchar(50),
	@sm nvarchar(50),
	@dvt int
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
           ct.iID_MaDonVi,
		   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongDuToan,
		   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongDuToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytDuToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiDuToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 --AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
           ct.iID_MaDonVi,
		   IsNull(ctct.fThu_BHYT_NSD, 0) ThuBHYTNSDDongHachToan,
		   IsNull(ctct.fThu_BHYT_NLD, 0) ThuBHYTNLDDongHachToan,
		   IsNull(ctct.fTongThuBHYT, 0) TongBhytHachToan
   FROM BH_KHT_BHXH_ChiTiet ctct
   LEFT JOIN BH_KHT_BHXH ct ON ct.iID_KHT_BHXH = ctct.iID_KHT_BHXH
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS = @khoiHachToan
   AND ml.sM = @sm
   WHERE ct.iNamChungTu = @namLamViec
	 --AND ct.bDaTongHop = @daTongHop
	 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
IsNull(dt.BhytNLDDongDuToan, 0)/@dvt BhytNldDongDuToan, 
IsNull(dt.BhytNSDDongDuToan, 0)/@dvt BhytNsddDongDuToan, 
IsNull(ht.BhytNLDDongHachToan, 0)/@dvt BhytNldDongHachToan, 
IsNull(ht.BhytNSDDongHachToan, 0)/@dvt BhytNsddDongHachToan,
IsNull(dt.TongBhytDuToan, 0)/@dvt BHYTTongCongDuToan,
IsNull(ht.TongBhytHachToan, 0)/@dvt BHYTTongCongHachToan
FROM @BhytDuToan dt
LEFT JOIN @BhytHachToan ht ON dt.idDonVi = ht.idDonVi;

END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_hssv_hvqs]    Script Date: 9/14/2023 5:25:41 PM ******/
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
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@hSSV nvarchar(50),
	@luuHS nvarchar(50),
	@hVSQ nvarchar(50),
	@sQDuBi nvarchar(50),
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hSSV
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @luuHS
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @hVSQ
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @sQDuBi
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]    Script Date: 9/14/2023 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_rpt_khtm_du_toan_thu_bhyt_than_nhan]
	@namLamViec int,
	@loaiChungTu int,
	@daTongHop bit,
	@lstSelectedUnit ntext,
	@thanNhanQuanNhan nvarchar(50),
	@thanNhanCNVQP nvarchar(50),
	@smDuToan nvarchar(50),
	@smHachToan nvarchar(50),
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanQuanNhan
		   AND ml.sM = @smDuToan
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
				   ct.iID_MaDonVi,
				   IsNull(ctct.fThanhTien, 0) ThanhTien,
				   ml.sLNS
		   FROM BH_KHTM_BHYT_ChiTiet ctct
		   LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
		   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
		   AND ml.iNamLamViec = @namLamViec
		   AND ml.iTrangThai = 1
		   AND ml.sLNS = @thanNhanCNVQP
		   AND ml.sM = @smDuToan
		   WHERE ct.iNamChungTu = @namLamViec
			 --AND ct.bDaTongHop = @daTongHop
			 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
					  ct.iID_MaDonVi,
					  IsNull(ctct.fThanhTien, 0) ThanhTien,
					  ml.sLNS
			  FROM BH_KHTM_BHYT_ChiTiet ctct
			  LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
			  RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
			  AND ml.iNamLamViec = @namLamViec
			  AND ml.iTrangThai = 1
			  AND ml.sLNS = @thanNhanQuanNhan
			  AND ml.sM = @smHachToan
			  WHERE ct.iNamChungTu = @namLamViec
				 --AND ct.bDaTongHop = @daTongHop
				 AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
					ct.iID_MaDonVi,
					IsNull(ctct.fThanhTien, 0) ThanhTien,
					ml.sLNS
			FROM BH_KHTM_BHYT_ChiTiet ctct
			LEFT JOIN BH_KHTM_BHYT ct ON ct.iID_KHTM_BHYT = ctct.iID_KHTM_BHYT
			RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_NoiDung = ml.iID_MLNS
			AND ml.iNamLamViec = @namLamViec
			AND ml.iTrangThai = 1
			AND ml.sLNS = @thanNhanCNVQP
			AND ml.sM = @smHachToan
			WHERE ct.iNamChungTu = @namLamViec
				--AND ct.bDaTongHop = @daTongHop
				AND ct.iLoaiTongHop = @loaiChungTu) AS A 
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
GO
