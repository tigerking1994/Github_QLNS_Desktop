/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 21/09/2022 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen]    Script Date: 21/09/2022 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]    Script Date: 21/09/2022 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtrichitiet]    Script Date: 21/09/2022 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getthongtrichitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getthongtrichitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_INSERT]    Script Date: 21/09/2022 5:04:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_bangluong_thang_dulieu_INSERT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_INSERT]
GO

/****** Object:  StoredProcedure [dbo].[sp_tl_bangluong_thang_dulieu_INSERT]    Script Date: 21/09/2022 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 21/04/2022
-- Description:	Lấy dữ liệu cho thêm mới bảng lương tháng
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_bangluong_thang_dulieu_INSERT] 
	@Thang int, 
	@Nam int,
	@MaDonVi NVARCHAR(MAX),
	@MaCachTl NVARCHAR(50),
	@SoNgay int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result SETs FROM
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH SoLieuTienAn AS (
		SELECT
			MA_CBO MaCanBo,
			SUM (
				CASE
					--WHEN MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG') THEN ISNULL(HuongPC_SN, 0) * GIA_TRI
					WHEN MA_PHUCAP IN ('TA_BB_DG', 'TA_TRUCTRAI_DG') THEN 30 * GIA_TRI 
					ELSE ISNULL(HuongPC_SN, 0) * GIA_TRI
				END
			) GiaTri
		FROM TL_CanBo_PhuCap canBoPhuCap
		--WHERE
			--canBoPhuCap.MA_PHUCAP IN ('TA_TRUCQY_DG1', 'TA_TRUCQY_DG2', 'TA_TRUCQY_DG3', 'TA_TRUCQY_DG4', 'TA_DOCHAI_DG', 'TA_OM_DG', 'TA_BB_DG', 'TA_TRUCTRAI_DG')
		WHERE 
			canBoPhuCap.MA_PHUCAP IN (SELECT Ma_PhuCap FROM TL_DM_PhuCap WHERE PARENT LIKE 'TIENAN')
		GROUP BY canBoPhuCap.MA_CBO
	), ThongTinCanBo AS (
		SELECT
			canBo.Ma_CanBo AS MaCanBo,
			canBo.Ten_CanBo AS TenCanBo,
			donVi.Ma_DonVi MaDonVi,
			canBo.Ma_Hieu_CanBo AS MaHieuCanBo,
			capBac.Ma_Cb MaCapBac,
			canBo.BHTN,
			canBo.PCCV
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent = donVi.Ma_DonVi
		INNER  JOIN TL_DM_CapBac capBac
			ON canBo.Ma_CB = capBac.Ma_Cb
		WHERE
			canBo.Thang = @Thang
			AND canBo.Nam = @Nam
			AND canBo.Parent IN (SELECT * FROM f_split(@MaDonVi))
	)

	SELECT
		newid()					AS Id,
		@Thang					AS Thang,
		@Nam					AS Nam,
		canBo.MaCanBo			AS MaCbo,
		canBo.TenCanBo			AS TenCbo,
		canBo.MaDonVi			AS MaDonVi,
		@MaCachTl				AS MaCachTl,
		canBoPhuCap.MA_PHUCAP	AS MaPhuCap,
		CASE
			WHEN (canBoPhuCap.MA_PHUCAP = 'TCVIECLAM_TT' AND cb.Parent in (1, 2, 3)) THEN 0 
			WHEN (canBoPhuCap.MA_PHUCAP = 'NTN' AND canBoPhuCap.GIA_TRI < 5) OR (canBoPhuCap.MA_PHUCAP = 'BHTNCN_HS' AND canBo.BHTN = 0) OR (canBoPhuCap.MA_PHUCAP = 'PCCOV_HS' AND canBo.PCCV = 0) THEN 0
			WHEN canBoPhuCap.MA_PHUCAP = 'TA_TT' THEN soLieuTienAn.GiaTri
			WHEN canBoPhuCap.MA_PHUCAP = 'TRICHLUONG_TT' THEN (canBoPhuCap.GIA_TRI / 1000) * 1000
			ELSE canBoPhuCap.GIA_TRI
		END						AS GiaTri,
		canBo.MaHieuCanBo		AS MaHieuCanBo,
		canBo.MaCapBac			AS MaCb,
		canBoPhuCap.HuongPC_SN	AS HuongPcSn,
		cachTinhLuong.CongThuc	AS CongThuc,
		CASE WHEN cachTinhLuong.Id IS NULL THEN 1 ELSE 0 END AS IsCalculated
	FROM TL_CanBo_PhuCap canBoPhuCap
	INNER JOIN ThongTinCanBo canBo
		ON canBo.MaCanBo = canBoPhuCap.MA_CBO
	LEFT JOIN SoLieuTienAn soLieuTienAn
		ON canBoPhuCap.MA_CBO = soLieuTienAn.MaCanBo
	LEFT JOIN TL_DM_Cach_TinhLuong_Chuan cachTinhLuong
		ON cachTinhLuong.Ma_Cot = canBoPhuCap.MA_PHUCAP
	left join TL_DM_CapBac cb on canBo.MaCapBac = cb.Ma_Cb
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtrichitiet]    Script Date: 21/09/2022 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getthongtrichitiet]
@iIdQuyetToanId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID as IIdDuAnId, da.STenDuAn, 
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId, lct.STenLoaiCongTrinh,
		lct.LNS as SLns, lct.L as SL, lct.K as SK, lct.M as SM, lct.TM as STm, lct.TTM as STtm, lct.NG as SNg,ml.iID as IIdMucLucNganSach,
		ISNULL(dt.FGiaTriNamNayChuyenNamSau, 0) as FSoTien, NULL as IIdMucId, NULL as IIdTieuMucId, NULL as IIdTietMucId, NULL as IIdNganhId
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	INNER JOIN VDT_QT_BCQuyetToanNienDo_ChiTiet_01 as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on da.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	LEFT JOIN NS_MucLucNganSach as ml on ISNULL(lct.LNS, '') = ISNULL(ml.sLNS, '') AND ISNULL(lct.L, '') = ISNULL(ml.sL, '') AND ISNULL(lct.K, '') = ISNULL(ml.sK, '') 
		AND ISNULL(lct.M, '') = ISNULL(ml.sM, '') AND ISNULL(lct.TM, '') = ISNULL(ml.sTM, '') AND ISNULL(lct.TTM, '') = ISNULL(ml.sTTM, '') AND ISNULL(lct.NG, '') = ISNULL(ml.sNG, '') 
		AND ml.sTNG IS NULL AND ml.sTNG1 IS NULL AND ml.sTNG2 IS NULL AND ml.sTNG3 IS NULL AND ml.iNamLamViec = tbl.iNamKeHoach
	WHERE tbl.Id = @iIdQuyetToanId
END

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]    Script Date: 21/09/2022 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getthongtriquyettoanchitiet_by_id]
@iIdThongTriId uniqueidentifier
AS
BEGIN
	SELECT dt.iID_DuAnID as IIdDuAnId, da.STenDuAn, 
		da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId, lct.STenLoaiCongTrinh,
		ml.sLNS as SLns, ml.sL as SL, ml.sK as SK, ml.sM as SM, ml.sTM as STm, ml.sTTM as STtm, ml.sNG as SNg, ml.iID as IIdMucLucNganSach,
		ISNULL(dt.fSoTien, 0) as FSoTien, dt.iID_MucID as IIdMucId, dt.iID_TieuMucID as IIdTieuMucId, dt.iID_TietMucID as IIdTietMucId, dt.iID_NganhID as IIdNganhId
	FROM VDT_ThongTri_ChiTiet as dt
	INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnID = da.iID_DuAnID
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on dt.iID_LoaiCongTrinhID = lct.iID_LoaiCongTrinh
	LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
	WHERE dt.iID_ThongTriID = @iIdThongTriId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen]    Script Date: 21/09/2022 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_qt_get_quyettoanniendo_in_thongtriscreen]
@iIdThongTri uniqueidentifier,
@iIdMaDonVi nvarchar(200),
@iNamThongTri int,
@iIdNguonVon int
AS
BEGIN
	CREATE TABLE #tmpUpdate(iID_BCQuyetToanNienDo uniqueidentifier)

	INSERT INTO #tmpUpdate(iID_BCQuyetToanNienDo)
	SELECT iID_BCQuyetToanNienDo
	FROM VDT_ThongTri
	WHERE (1 <> 1) OR Id = @iIdThongTri

	INSERT INTO #tmpUpdate(iID_BCQuyetToanNienDo)
	SELECT tbl.Id
	FROM VDT_QT_BCQuyetToanNienDo as tbl
	LEFT JOIN VDT_ThongTri as dt on tbl.Id = dt.iID_BCQuyetToanNienDo
	WHERE dt.iID_BCQuyetToanNienDo IS NULL AND tbl.iID_MaDonViQuanLy = @iIdMaDonVi AND tbl.iNamKeHoach = @iNamThongTri AND tbl.iID_NguonVonID = @iIdNguonVon

	SELECT tbl.*
	FROM #tmpUpdate as tmp
	INNER JOIN VDT_QT_BCQuyetToanNienDo as tbl on tmp.iID_BCQuyetToanNienDo = tbl.Id

	DROP TABLE #tmpUpdate
END

GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_index]    Script Date: 21/09/2022 5:04:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_vdt_thongtri_index]
@iIdLoaiThongTri uniqueidentifier,
@openFROMPheDuyetThanhToan int

AS
BEGIN
	SELECT tbl.Id,sMaThongTri,dNgayThongTri,iNamThongTri,sNguoiLap,sTruongPhong,sThuTruongDonVi,sMaNguonVon,iID_DonViID,iID_MaDonViID,tbl.sMoTa,sUserCreate,dDateCreate,sUserUpdate,dDateUpdate,sUserDelete,dDateDelete, 
	tbl.sMaLoaiCongTrinh,iID_LoaiThongTriID,iID_NhomQuanLyID,bIsCanBoDuyet,bIsDuyet,bThanhToan,ISNULL(tbl.ILoaiThongTri, 1) as ILoaiThongTri, dv.sTenDonVi as sTenDonVi, NULL as dNgayLapGanNhat, lct.sTenLoaiCongTrinh, tbl.INamNganSach,
	tbl.iID_BCQuyetToanNienDo as IIdBcQuyetToanNienDo
	FROM VDT_ThongTri as tbl
	LEFT JOIN DonVi as dv on tbl.iID_DonViID = dv.iID_DonVi
	LEFT JOIN VDT_DM_LoaiCongTrinh as lct on tbl.sMaLoaiCongTrinh = lct.sMaLoaiCongTrinh
	WHERE dDateDelete IS NULL AND tbl.iID_LoaiThongTriID = @iIdLoaiThongTri
	-- thông tri quyết toán
	and (@openFROMPheDuyetThanhToan = 2 
	or (
		-- thông tri mở phê duyệt thanh toán -> thanh toán or tạm ứng
		(@openFROMPheDuyetThanhToan = 1 and iLoaiThongTri in (1,2))
		or 
		-- thông tri mở từ quản lý thông tri -> kinh phí or cấp hợp thức
		(@openFROMPheDuyetThanhToan = 0 and iLoaiThongTri in (3,4))
	))
	ORDER BY dDateCreate DESC
END
;
;
GO


UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc ='PC10+PCTQ_TT+PCKHAC2_TT+PCKHAC3_TT+PCDT_TT+PCTHANHTRA_TT+PCNU_TT+PCTEMTHU_TT+PCDTQUANSU_TT+PCDTN_TT+PCCU_TT+PCHOIPHUNU_TT+PCCONGDOAN_TT+PCANQP_TT+PCGS_TT+PCTS_TT+PCPGS_TT+PCBCV_TT'
WHERE Ma_Cot='PCKHAC_SUM'

UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc ='BHXHCN_TT+BHYTCN_TT+BHTNCN_TT+THUETNCN_TT+TA_TONG+GTKHAC_TT+TRICHLUONG_TT'
WHERE Ma_Cot='PHAITRU_SUM'

UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc ='TA_THANG+TA_TT'
WHERE Ma_Cot='TA_TONG'
UPDATE TL_DM_PhuCap SET Ten_PhuCap = N'Tổng tiền ăn bị trừ đầu tháng'
WHERE Ma_PhuCap = 'TA_TT'

UPDATE TL_DM_PhuCap SET Gia_Tri = 1 WHERE Ma_PhuCap = 'TILE_HUONG'

UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc ='GTKHAC_TT+TRICHLUONG_TT+TA_TONG'
WHERE Ma_Cot='PHAITRUKHAC_SUM'

--lưu ý: 1 vài đơn vị đã thêm phụ cấp và công thức rồi. viết thêm câu điều kiện nếu trong bảng TL_DM_PhuCap đã có PCKVCS_TT, PCKVCS_HS và TL_DM_Cach_TinhLuong_Chuan đã có công thức cho PCKVCS_TT rồi thì k INSERT nữa


IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap = 'PCKVCS_HS')
INSERT INTO TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_PhuCap, Ma_TTM_Ng, Numeric_Scale, Parent, PhanTram_CT, Readonly, Splits, Ten_Ngan, Ten_PhuCap, Tinh_BHXH, Tinh_TNCN, Xau_Noi_Ma, XSort)
values (1, 1, NULL, 1, NULL, 0, 0, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, 'PCKVCS_HS', NULL, NULL, 'LPC_HS', NULL, NULL, NULL, NULL, N'Hệ số phụ cấp khu vực chiến sĩ', 1, NULL, 'LPC_HS-PCKVCS_HS', NULL);

IF NOT EXISTS (SELECT * FROM TL_DM_PhuCap WHERE Ma_PhuCap = 'PCKVCS_TT')
INSERT INTO TL_DM_PhuCap (bGiaTri, bHuongPc_Sn, bSaoChep, Chon, Cong_Thuc, Dinh_Dang, Gia_Tri, He_So, HuongPC_SN, iDinhDang, iLoai, Is_Formula, Is_Readonly, IThang_ToiDa, Ma_KMCP, Ma_PhuCap, Ma_TTM_Ng, Numeric_Scale, Parent, PhanTram_CT, Readonly, Splits, Ten_Ngan, Ten_PhuCap, Tinh_BHXH, Tinh_TNCN, Xau_Noi_Ma, XSort)
values (1, 1, NULL, 1, NULL, 0, 0, NULL, NULL, 0, 1, 0, 0, NULL, NULL, 'PCKVCS_TT', NULL, NULL, 'LPC_TT', NULL, NULL, NULL, NULL, N'Phụ cấp khu vực chiến sĩ', 1, NULL, 'LPC_TT-PCKVCS_TT', NULL)

IF NOT EXISTS (SELECT CongThuc FROM TL_DM_Cach_TinhLuong_Chuan WHERE Ma_Cot = 'PCKVCS_TT')
INSERT INTO TL_DM_Cach_TinhLuong_Chuan (CongThuc, Ma_CachTL, Ma_Cot, Nam, NoiDung, Ten_CachTL, Ten_Cot, Thang)
values ('LCS*0.4*PCKVCS_HS', 'CACH0', 'PCKVCS_TT', NULL, N'Phụ cấp khu vực dành cho chiến sĩ', NULL, N'Phụ cấp khu vực chiến sĩ', NULL)

UPDATE TL_DM_Cach_TinhLuong_Chuan SET CongThuc = 'PCKV_HS*LCS+PCKVCS_TT' WHERE Ma_CachTL= 'CACH0' and Ma_Cot = 'PCKV_TT'