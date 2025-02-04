/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhyt_index]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_du_toan_thu_bhyt_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_du_toan_thu_bhyt_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dttm_bhyt_tn_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dttm_bhyt_tn_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dttm_bhyt_tn_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_rpt_get_donvi]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dtc_rpt_get_donvi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dtc_rpt_get_donvi]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_nhandutoanchitrengiao_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]    Script Date: 12/7/2023 5:47:12 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_dutoan_dotnhan_phanbo_find_all]
@YearOfWork int ,
@Date DateTime,
@LoaiDuToanNhan int
AS
BEGIN
	DECLARE @DieuChinh int = 2;
	--Lấy danh sách dự toán nhận phân bổ
		SELECT ID as iID_DTC_DuToanChiTrenGiao,
			   sSoChungTu,
			   sLNS,
			   iID_MaDonVi,
			   dNgayChungTu,
			   sSoQuyetDinh,
			   dNgayQuyetDinh,
			   fTongTienTuChi + fTongTienHienVat AS fSoPhanBo
		INTO  #tblNhanPhanBo
		FROM BH_DTC_DuToanChiTrenGiao 
		WHERE iNamLamViec = @YearOfWork 
			AND iLoaiDotNhanPhanBo = @LoaiDuToanNhan
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))



		--Lấy danh sách dự toán đã được phân bổ
		SELECT ISNULL(sum(pb_ct.fTienTuChi),0) + ISNULL(sum(pb_ct.fTienHienVat),0)  as fDaPhanBo, pb_ct.iID_DTC_DuToanChiTrenGiao AS iID_DTC_DuToanChiTrenGiao
		INTO #tblChungTuNhanPhanBoMap
		FROM  BH_DTC_PhanBoDuToanChi_ChiTiet as pb_ct 
		WHERE pb_ct.iID_DTC_DuToanChiTrenGiao in (select iID_DTC_DuToanChiTrenGiao from  #tblNhanPhanBo)
		and iNamLamViec=@YearOfWork
		GROUP BY pb_ct.iID_DTC_DuToanChiTrenGiao

		-----Lay danh sach du toan nhan phan bo, so phan bo, chua phan bo
		SELECT  npb.iID_DTC_DuToanChiTrenGiao as Id,
			    npb.sSoChungTu, 
				npb.sLNS,
				npb.iID_MaDonVi,
				npb.dNgayChungTu, 
				npb.sSoQuyetDinh, 
				npb.dNgayQuyetDinh, 
				npb.fSoPhanBo, 
				npbm.fDaPhanBo,
				ISNULL(npb.fSoPhanBo,0) - ISNULL(npbm.fDaPhanBo,0) AS fSoChuaPhanBo
		FROM #tblNhanPhanBo AS npb
		left join #tblChungTuNhanPhanBoMap AS npbm ON npb.iID_DTC_DuToanChiTrenGiao = npbm.iID_DTC_DuToanChiTrenGiao

	   DROP TABLE #tblNhanPhanBo;	
       DROP TABLE #tblChungTuNhanPhanBoMap;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_baocao_dutoan_thongbaocaochitieungansach]
@idDonvi nvarchar(50),
@sLns nvarchar(max),
@NamLamViec int

AS
BEGIN

SELECT 
	mucluc.iID_MLNS,
	mucluc.iID_MLNS_Cha,
	mucluc.sXauNoiMa,
	mucluc.sLNS,
	mucluc.sL,
	mucluc.sK,
	mucluc.sM,
	mucluc.sTM,
	mucLuc.sTTM,
	mucluc.sNG,
	mucluc.sMoTa AS sNoiDung,
	mucluc.bHangCha,
	d.fTienHienVat,
	d.fTienTuChi,
	d.fTongTien

	FROM BH_DM_MucLucNganSach AS mucluc
		left join (SELECT  
						sum(c.fTienHienVat) AS fTienHienVat,
						sum(c.fTienTuChi) AS fTienTuChi,
						sum(c.fTongTien) AS fTongTien,
						c.iID_MucLucNganSach,
						c.iID_MaDonVi
		
						FROM 
							(SELECT 
								b.fTongTien,
								b.fTienHienVat,
								b.fTienTuChi,
								b.iID_MucLucNganSach,
								a.sLNS,
								a.iID_MaDonVi

								FROM BH_DTC_DuToanChiTrenGiao AS a
								left join BH_DTC_DuToanChiTrenGiao_ChiTiet AS b on a.ID = b.iID_DTC_DuToanChiTrenGiao
								WHERE a.iID_MaDonVi = @idDonvi and a.iNamLamViec = @NamLamViec) AS c
						GROUP BY c.iID_MucLucNganSach, c.iID_MaDonVi) AS d on  mucluc.iID_MLNS = d.iID_MucLucNganSach
	WHERE mucluc.sLNS in  (SELECT * FROM f_split(@sLns)) and mucluc.iNamLamViec = @NamLamViec
	ORDER BY mucluc.sXauNoiMa
		

END


GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiao_index]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[sp_bhxh_nhandutoanchitrengiao_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	DTC.ID,
	DTC.sSoChungTu,
	DTC.iID_MaDonVi,
	DV.sTenDonVi AS sTenDonVi,
	DTC.dNgayChungTu,
	DTC.sSoQuyetDinh,
	DTC.dNgayQuyetDinh,
	DTC.sLNS,
	DTC.fTongTienTuChi,
	DTC.fTongTienHienVat,
	DTC.fTongTien,
	DTC.iLoaiDotNhanPhanBo,
	DTC.sMoTa,
	DTC.iNamLamViec,
	DTC.sNguoiTao,
	DTC.bIsKhoa,
	BH_DM_LoaiChi.sTenDanhMucLoaiChi AS sLoaiChi
	FROM BH_DTC_DuToanChiTrenGiao DTC
	LEFT JOIN DonVi DV ON DTC.iID_MaDonVi = DV.iID_MaDonVi
	INNER JOIN BH_DM_LoaiChi ON DTC.iID_LoaiDanhMucChi = BH_DM_LoaiChi.iID
	WHERE DV.iNamLamViec = @YearOfWork and DTC.iNamLamViec = @YearOfWork
	ORDER BY DTC.dNgayQuyetDinh DESC
END;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_nhandutoanchitrengiaochitiet_idndt_slns]
@iDNdtctg uniqueidentifier,
@sLns nvarchar(max),
@NamLamViec int,
@IIDDonVi nvarchar(50)

AS
BEGIN

SELECT 
	A.sLNS,
	A.sTM,
	A.sTTM,
	A.sM,
	A.sNG,
	A.sMoTa AS sNoiDung,
	A.sXauNoiMa,
	A.iID_MLNS,
	A.iID_MLNS_Cha,
	A.bHangCha,
	A.bHangCha as isHangCha,
	B.ID,
	B.iID_DTC_DuToanChiTrenGiao,
	A.iID_MLNS AS iID_MucLucNganSach,
	B.fTongTien,
	B.fTienHienVat,
	B.fTienTuChi,
	A.iNamlamViec,
	@IIDDonVi as iID_MaDonVi,
	 B.dNgaySua,
	 B.dNgayTao,
	 B.sNguoiSua,
	 B.sNguoiTao
	FROM BH_DM_MucLucNganSach AS A
	LEFT JOIN ( SELECT ctct.ID
					, ctct.iID_DTC_DuToanChiTrenGiao
					, ctct.iID_MucLucNganSach
					,ctct.sLNS
					, ctct.sM
					, ctct.sTM
					, ctct.sTTM
					, ctct.sNG
					, ctct.sNoiDung
					, ctct.fTongTien
					, ctct.fTienTuChi
					, ctct.fTienHienVat
					, ctct.dNgaySua
					, ctct.dNgayTao
					, ctct.sNguoiSua
					, ctct.sNguoiTao
					, CT.iNamLamViec
					,CT.iID_MaDonVi 
					FROM BH_DTC_DuToanChiTrenGiao_ChiTiet ctct
				LEFT JOIN BH_DTC_DuToanChiTrenGiao CT ON ctct.iID_DTC_DuToanChiTrenGiao=CT.ID 
				WHERE ctct.iID_DTC_DuToanChiTrenGiao = @iDNdtctg
					 and ct.iID_MaDonVi=@IIDDonVi 
					 And CT.iNamLamViec=@NamLamViec) AS B
	ON B.iID_MucLucNganSach = A.iID_MLNS
	WHERE   A.sLNS IN (SELECT * FROM f_split(@sLns))
		AND A.iNamlamViec=@NamLamViec
	order by A.sXauNoiMa
END



GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
SELECT 
		ct.iID_BH_DTC_ChiTiet ,
		ct.iID_BH_DTC ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iNamLamViec,
		ct.fTienDuToanDuocGiao,
		ct.fTienThucHien06ThangDauNam,
		ct.fTienUocThucHien06ThangCuoiNam,
		ct.fTienUocThucHienCaNam,
		ct.fTienSoSanhTang,
		ct.fTienSoSanhGiam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi in (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  -- Create date: <Create Date,,>
  -- Description:  <Description,,>
  -- =============================================
  CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop] @ListIdChungTuTongHop ntext, 
  @Nguoitao nvarchar(50), 
  @IdChungTu nvarchar(100), 
  @NamLamViec int AS BEGIN INSERT INTO [dbo].BH_DTC_DieuChinhDuToanChi_ChiTiet (
    iID_BH_DTC_ChiTiet,
	iID_BH_DTC, 
    iID_MucLucNganSach,
	sM,
	sTM,
	sNoiDung,
	sXauNoiMa,
	iNamLamViec,
	iID_MaDonVi,
    fTienDuToanDuocGiao,
	fTienThucHien06ThangDauNam,
    fTienUocThucHien06ThangCuoiNam,
    fTienUocThucHienCaNam,
	fTienSoSanhTang, 
    fTienSoSanhGiam,
	dNgaySua,
	dNgayTao, 
    sNguoiSua,
	sNguoiTao
  ) 
SELECT 
  DISTINCT NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi,
  sum(fTienDuToanDuocGiao), 
  sum(fTienThucHien06ThangDauNam), 
  sum(fTienUocThucHien06ThangCuoiNam), 
  sum(fTienUocThucHienCaNam), 
  sum(fTienSoSanhTang), 
  sum(fTienSoSanhGiam), 
  Null, 
  GETDATE(), 
  Null, 
  @Nguoitao 
FROM 
  BH_DTC_DieuChinhDuToanChi_ChiTiet 
WHERE 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung,
  sXauNoiMa,
  iNamLamViec,
  iID_MaDonVi;
--danh dau chung tu da tong hop
update 
  BH_DTC_DieuChinhDuToanChi 
set 
  iLoaiTongHop = 2 
where 
  iID_BH_DTC in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị dự toán điều chỉnh dự toán

-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 dcdt.iID_BH_DTC 
		, dcdt.sSoChungTu
		, dcdt.dNgayChungTu
		, dcdt.sSoQuyetDinh
		, dcdt.dNgayQuyetDinh
		, dcdt.iNamLamViec
		, dcdt.iID_DonVi
		, dcdt.iID_MaDonVi
		, dcdt.sMoTa
		, dcdt.sLNS
		, dcdt.iID_LoaiCap
		, dcdt.fTienDuToanDuocGiao
		, dcdt.fTienThucHien06ThangDauNam
		, dcdt.fTienUocThucHien06ThangCuoiNam
		, dcdt.fTienUocThucHienCaNam
		, dcdt.fTienSoSanhTang
		, dcdt.fTienSoSanhGiam
		, dcdt.sTongHop
		, dcdt.iID_TongHopID
		, dcdt.iLoaiTongHop
		, dcdt.dNgaySua
		, dcdt.dNgayTao
		, dcdt.sNguoiSua
		, dcdt.sNguoiTao
		, dcdt.dNgayTao
		, donVi.sTenDonVi
		, dcdt.iID_LoaiCap
		, dcdt.bIsKhoa
		, lc.sTenDanhMucLoaiChi
		-- Tong dự toán todo
	FROM BH_DTC_DieuChinhDuToanChi dcdt
	LEFT JOIN DonVi donVi
		ON dcdt.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = dcdt.iID_DonVi
	LEFT JOIN BH_DM_LoaiChi lc on dcdt.iID_LoaiCap=lc.iID and dcdt.iNamLamViec=lc.iNamLamViec
END


GO
/****** Object:  StoredProcedure [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dt_phanbo_dieuchinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@loaiDanhMucCapChi nvarchar(100),
	@ngayChungTu date
AS
BEGIN
SELECT 
		ct.ID ,
		ct.iID_DTC_PhanBoDuToanChi ,
		ct.iID_DonVi,
		ct.iID_MucLucNganSach,
		ct.iID_DTC_DuToanChiTrenGiao,
		ct.sLNS,
		ct.sM,
		ct.sTM,
		ct.sTTM,
		ct.sNG,
		ct.sNoiDung,
		ct.sXauNoiMa,
		ct.iID_MaDonVi,
		ct.iNamLamViec,
		ct.iID_LoaiCap,
		ISNULL(ct.fTongTien, 0) as FTongTien,
		ISNULL(ct.fTienTuChi, 0) as FTienTuChi,
		ISNULL(ct.fTienHienVat, 0) as FTienHienVat,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_PhanBoDuToanChi bh
			JOIN 
				BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bIsKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
				AND bh.iID_LoaiDanhMucChi=@loaiDanhMucCapChi
				AND bh.iNamChungTu=@NamLamViec
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dtc_rpt_get_donvi]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dtc_rpt_get_donvi]
@NamLamViec int,
@IDLoaiChi uniqueidentifier
AS BEGIN
SET NOCOUNT ON;

SELECT 
	donvi.iID_DonVi AS Id,
	donvi.iID_MaDonVi as IIDMaDonVi,
	donvi.sTenDonVi as TenDonVi,
	donvi.sKyHieu as KyHieu,
	donvi.sMoTa as MoTa,
	donvi.iLoai as Loai,
	donvi.iNamLamViec as NamLamViec,
	donvi.iTrangThai as iTrangThai,
	donvi.dNgayTao as DNgayTao,
	donvi.sNguoiTao as SNguoiTao,
	donvi.dNgaySua as DNgaySua,
	donvi.dNgaySua as SNguoiSua,
	donvi.*

FROM BH_DTC_DieuChinhDuToanChi chungtu
LEFT JOIN DonVi donvi ON chungtu.iID_MaDonVi = donvi.iID_MaDonVi
WHERE chungtu.iNamLamViec = @NamLamViec
  AND chungtu.iID_MaDonVi <> ''
  AND chungtu.iID_MaDonVi IS NOT NULL
  AND donvi.iNamLamViec = @NamLamViec
  AND chungtu.iID_LoaiCap=@IDLoaiChi
  --AND chungtu.iLoaiTongHop=@LoaiChungTu
ORDER BY cast(chungtu.dNgayChungTu AS date) END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_dttm_bhyt_tn_chi_tiet]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_dttm_bhyt_tn_chi_tiet]
	@dttmBHYTId nvarchar(100),
	@NamLamViec int
AS
BEGIN
SELECT 
		ct.iID_DTTM_BHYT_ThanNhan_ChiTiet ,
		ct.iID_DTTM_BHYT_ThanNhan ,
		ct.iID_MaDonVi,
		ct.sLNS,
		ct.iNamLamViec,
		ct.sNoiDung,
		ct.iID_MLNS,
		ct.fDuToan,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sXauNoiMa,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.iID_DTTM_BHYT_ThanNhan_ChiTiet,
				bhct.iID_DTTM_BHYT_ThanNhan,
				bhct.sNguoiTao,
				bhct.sNguoiSua,
				bhct.dNgayTao,
				bhct.dNgaySua,
				bhct.iNamLamViec,
				bhct.iID_MLNS,
				bhct.sGhiChu,
				bhct.fDuToan,
				bhct.sLNS,
				bhct.sNoiDung,
				bhct.sXauNoiMa
			FROM 
				BH_DTTM_BHYT_ThanNhan bh
			JOIN 
				BH_DTTM_BHYT_ThanNhan_ChiTiet bhct ON bh.iID_DTTM_BHYT_ThanNhan = bhct.iID_DTTM_BHYT_ThanNhan 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_DTTM_BHYT_ThanNhan = @dttmBHYTId
			and	bh.iNamLamViec=@NamLamViec
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;



GO
/****** Object:  StoredProcedure [dbo].[sp_du_toan_thu_bhyt_index]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_du_toan_thu_bhyt_index]
	@YearOfWork int
AS
BEGIN
	SELECT
	CT.iID_DTTM_BHYT_ThanNhan,
	CT.sSoChungTu,
	CT.iNamLamViec,
	CT.dNgayChungTu,
	CT.iID_MaDonVi,
	CT.sMoTa,
	CT.bIsKhoa,
	CT.iLoaiDuToan,
	CT.sSoQuyetDinh,
	CT.dNgayQuyetDinh,
	CT.sNguoiTao,
	CT.sNguoiSua,
	CT.dNgayTao,
	CT.dNgaySua,
	CT.fDuToan ,
	CT.sDSLNS,
	CT.iID_DonVi
	FROM BH_DTTM_BHYT_ThanNhan CT 
	WHERE CT.iNamLamViec = @YearOfWork
	ORDER BY CT.dNgayQuyetDinh DESC;
END;
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dtc_dieuchinh_chitiet_donvi]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@IDLoaiChi uniqueidentifier,
	@SLNS nvarchar(max),
	@ngayChungTu date, 
	@Dvt int
AS
BEGIN

	SELECT ML.*
			into #tblml
	FROM BH_DM_MucLucNganSach ML
	WHERE ML.sLNS IN  (SELECT * FROM splitstring(@SLNS))
			AND ML.iNamLamViec=@NamLamViec
	ORDER BY sXauNoiMa

	SELECT 
		ml.iID_MLNS as iID_MucLucNganSach,
		ml.sM,
		ml.SLNS,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa as sNoiDung,
		ml.sXauNoiMa,
		ml.iID_MLNS_Cha as IdParent,
		ml.bHangCha,
		ct.iNamLamViec,
		ISNULL(SUM(ct.fTienDuToanDuocGiao),0)/ @Dvt fTienDuToanDuocGiao,
		ISNULL(SUM(ct.fTienThucHien06ThangDauNam),0)/ @Dvt fTienThucHien06ThangDauNam,
		ISNULL(SUM(ct.fTienUocThucHien06ThangCuoiNam),0)/ @Dvt fTienUocThucHien06ThangCuoiNam,
		ISNULL(SUM(ct.fTienUocThucHienCaNam),0) /@Dvt fTienUocThucHienCaNam,
		ISNULL(SUM(ct.fTienSoSanhTang),0)/ @Dvt fTienSoSanhTang,
		ISNULL(SUM(ct.fTienSoSanhGiam),0)/ @Dvt fTienSoSanhGiam

	FROM
	#tblml ml 
	LEFT JOIN
		(
			SELECT
				--bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_BH_DTC = bhct.iID_BH_DTC 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi IN (SELECT * FROM splitstring(@IdDonVi))
				AND	bh.iNamLamViec=@NamLamViec
				AND BH.iID_LoaiCap=@IDLoaiChi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct
		ON ct.iID_MucLucNganSach=ml.iID_MLNS
		WHERE ml.iNamLamViec=@NamLamViec
		GROUP BY 
		ml.iID_MLNS,
		ml.iID_MLNS_Cha,
		ml.bHangCha,
		ml.SLNS,
		ml.sM,
		ml.sTM,
		ml.sTTM,
		ml.sMoTa,
		ml.sXauNoiMa,
		ct.iNamLamViec
		ORDER BY sXauNoiMa;
		
		DROP TABLE #tblml


		----- PHÂN BO
	--	SELECT
	--	ct.iID_MaDonVi,
	--	ct.iID_MucLucNganSach,
	--	ct.sM,
	--	ct.sTM,
	--	ct.sNoiDung,
	--	ct.sXauNoiMa,
	--	ct.iNamLamViec,
	--	ct.sTenDonVi,
	--	ISNULL(SUM(ct.fTongTien), 0) as fTienDuToanDuocGiao,
	--	0 fTienThucHien06ThangDauNam,
	--	0 fTienUocThucHien06ThangCuoiNam,
	--	0 fTienUocThucHienCaNam,
	--	0 fTienSoSanhTang,
	--	0 fTienSoSanhGiam

	--	into #tblpbdt
	--FROM
	--	(
	--		SELECT
	--			bh.sMoTa,
	--			ddv.sTenDonVi,
	--			bhct.*
	--		FROM 
	--			BH_DTC_PhanBoDuToanChi bh
	--		JOIN 
	--			BH_DTC_PhanBoDuToanChi_ChiTiet bhct ON bh.ID = bhct.iID_DTC_PhanBoDuToanChi 
	--		LEFT JOIN 
	--			(SELECT * FROM DonVi WHERE iNamLamViec = 2023 AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
	--		WHERE
	--			bhct.iID_MaDonVi IN (SELECT * FROM splitstring('001'))
	--			and bh.bIsKhoa = 1
	--			AND bh.sSoQuyetDinh IS NOT NULL
	--			AND bh.sSoQuyetDinh <> ''
	--			AND cast(bh.DngayChungTu as date) <= cast('2023-12-05' as date)
	--			AND bh.iID_LoaiDanhMucChi='1E909740-3235-4BE4-B992-6C7D101EC384'
	--			AND bh.iNamChungTu=2023
	--	) ct

	--	GROUP BY ct.iID_MaDonVi,
	--	ct.iID_MucLucNganSach,
	--	ct.sM,
	--	ct.sTM,
	--	ct.sNoiDung,
	--	ct.sXauNoiMa,
	--	ct.iNamLamViec,
	--	ct.sTenDonVi;


		--DROP TABLE #tblpbdt
END
;
;


GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]    Script Date: 12/7/2023 5:47:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_rpt_khc_k_chungtu_truongsDK_tonghop]
	@listTenDonVi ntext,
	@namLamViec int,
	@LNS nvarchar(200),
	@IDLoaichi uniqueidentifier,
	@Dvt int
AS
BEGIN
declare @DataKhoi table (idDonVi nvarchar(50),sTenDonVI nvarchar(200),
FTongTienKeHoachThucHienNamNay float
);

INSERT INTO @DataKhoi (idDonVi,sTenDonVI , 
FTongTienKeHoachThucHienNamNay 
)
	SELECT 
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   FTongTienKeHoachThucHienNamNay=SUM(IsNull(A.fTienKeHoachThucHienNamNay,0))/ @Dvt
FROM
  (SELECT 
				ct.iID_DonVi,
				ct.iID_MaDonVi,
				ctct.fTienKeHoachThucHienNamNay
   FROM BH_KHC_K_ChiTiet ctct
   LEFT JOIN BH_KHC_K ct ON ct.iID_BH_KHC_K = ctct.iID_KHC_K
   RIGHT JOIN BH_DM_MucLucNganSach ml ON ctct.iID_MucLucNganSach = ml.iID_MLNS
   AND ml.iNamLamViec = @namLamViec--@namLamViec
   AND ml.iTrangThai = 1
   AND ml.sLNS IN  (SELECT * FROM f_split(@LNS))
   WHERE ct.iNamChungTu = @namLamViec
		AND ct.iIDLoaiChi=@IDLoaichi
	) AS A 
   LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai = 1
   AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   --AND iNamLamViec = @namLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
   
GROUP BY dt_dv.sTenDonVi,
		dt_dv.id,
		dt_dv.iLoai;

SELECT dt.idDonVi, 
dt.sTenDonVI, 
IsNull(dt.FTongTienKeHoachThucHienNamNay, 0) FTongTienKeHoachThucHienNamNay
FROM @DataKhoi dt
where dt.idDonVi in 
    (SELECT *
     FROM f_split(@listTenDonVi))
END
;


GO
