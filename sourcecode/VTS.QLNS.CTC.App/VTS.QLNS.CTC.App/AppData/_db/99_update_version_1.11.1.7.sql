/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 10/06/2022 6:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_thongtri_detail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_thongtri_detail]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 10/06/2022 6:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_danhsach_chitra_nganhang]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 10/06/2022 6:53:13 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]    Script Date: 10/06/2022 6:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[sp_tl_rpt_chitiet_quanso_tanggiam]
	@thang int, @nam int, @thangTruoc int, @namTruoc int, @maDonVi nvarchar(MAX), @sM nvarchar(1)
As
Begin
	if @sM = '3'
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		)

		Select 
			canbo.Ten_CanBo TenCanBo,
			CASE 
				WHEN canbo.Ma_TangGiam in ('250', '280') THEN canbothangtruoc.CapBacCu
				ELSE canbo.Ma_CB
			END CapBac,
			CAST('1' as int) as SoLuong,
			CASE 
				WHEN canbo.Ma_TangGiam in ('290') THEN canbothangtruoc.TenDonViCu
				ELSE canbo.Ten_DonVi
			END DonVi,
			mlqs.sMoTa NoiDung
		From TL_DM_CanBo canbo
			Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
			Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			left Join CanBoThangTruoc canbothangtruoc on canbo.Ma_Hieu_CanBo = canbothangtruoc.Ma_Hieu_CanBo
		Where canbo.Thang = @thang
			And canbo.Nam = @nam
			And (sM = @sM OR canbo.Ma_TangGiam in ('250', '280', '290'))
			And iNamLamViec = @nam
			And bHangCha = 0
			And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
		Order By Ma_DonVi, CapBac
	else
		With CanBoThangTruoc as (
			Select canbo.Ma_Hieu_CanBo, canbo.Parent MaDonViCu, canbo.Ten_DonVi TenDonViCu, canbo.Ma_CB CapBacCu
			From TL_DM_CanBo canbo
			Where 
				canbo.Thang = @thangTruoc
				And canbo.Nam = @namTruoc
		),

		
		
		KhongTuyenQuan as (
			Select 
				canbo.Ten_CanBo TenCanBo,
				CAST('1' as int) as SoLuong,
				
				CASE 
					WHEN canbo.Ma_TangGiam in ('350', '380') THEN Vcanbothangtruoc.CapBacCu
					ELSE canbo.Ma_CB
				END CapBac,
				CASE 
					WHEN canbo.Ma_TangGiam in ('390') THEN Vcanbothangtruoc.TenDonViCu
					ELSE canbo.Ten_DonVi
				END DonVi,
				
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
				left Join CanBoThangTruoc Vcanbothangtruoc on canbo.Ma_Hieu_CanBo = Vcanbothangtruoc.Ma_Hieu_CanBo
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And (sM = @sM OR canbo.Ma_TangGiam in ('350','380','390'))
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam not in ('210', '220')),

		TuyenQuan as (
		Select 
			(CAST(COUNT(*) as nvarchar(MAX)) + N' đồng chí') as TenCanBo, 
			canbo.Ma_CB CapBac, 
			COUNT(*) SoLuong,
			donvi.Ten_DonVi DonVi, 
			mlqs.sMoTa NoiDung
			From TL_DM_CanBo canbo
				Join NS_QS_MucLuc mlqs On canbo.Ma_TangGiam = mlqs.sKyHieu
				Join TL_DM_DonVi donvi on canbo.Parent = donvi.Ma_DonVi
			Where canbo.Thang = @thang
				And canbo.Nam = @nam
				And sM = @sM
				And iNamLamViec = @nam
				And bHangCha = 0
				And canbo.Parent IN (SELECT * FROM f_split(@maDonVi))
				And canbo.Ma_TangGiam in ('210', '220')
			Group By canbo.Ma_CB, donvi.Ten_DonVi, mlqs.sMoTa
		)
		
		Select *
		From KhongTuyenQuan
		union 
		select * from  TuyenQuan


		Order By DonVi, CapBac desc

		
End
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_tl_rpt_danhsach_chitra_nganhang]    Script Date: 10/06/2022 6:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TungNH
-- Create date: 23/04/2022
-- Description:	Lấy dữ liệu cho báo cáo chi trả cá nhân
-- =============================================
CREATE PROCEDURE [dbo].[sp_tl_rpt_danhsach_chitra_nganhang] 
	@Thang int,
	@Nam int,
	@MaDonVi NVARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    WITH BangLuongThang AS (
		SELECT
			bangLuongThang.Ma_CBo	AS MaCanBo,
			bangLuongThang.Gia_Tri	AS GiaTri
		FROM TL_BangLuong_Thang bangLuongThang
		JOIN TL_DS_CapNhap_BangLuong dsCapNhapBangLuong
			ON bangLuongThang.parent = dsCapNhapBangLuong.Id
		WHERE
			bangLuongThang.Ma_PhuCap = 'THANHTIEN'
			AND dsCapNhapBangLuong.Ma_CachTL='CACH0'
			AND dsCapNhapBangLuong.Ma_CBo IN (SELECT * FROM f_split(@MaDonVi))
			AND dsCapNhapBangLuong.Thang=@Thang
			AND dsCapNhapBangLuong.Nam=@Nam
			AND dsCapNhapBangLuong.Status=1
	), ThongTinCanBo AS (
		SELECT
			donVi.Ma_DonVi		AS MaDonvi,
			canBo.Ma_CanBo		AS MaCanBo,
			canBo.Ten_CanBo		AS TenCanBo,
			canBo.So_TaiKhoan	AS SoTaiKhoan
		FROM TL_DM_CanBo canBo
		INNER JOIN TL_DM_DonVi donVi
			ON canBo.Parent=donVi.Ma_DonVi
		WHERE
			canBo.IsDelete = 1
			AND canBo.Khong_Luong = 0
			AND canBo.Thang=@Thang
			AND canBo.Nam=@Nam
			AND canBo.TM = 1
	)

	SELECT
		canBo.MaDonvi,
		canBo.MaCanBo,
		canBo.TenCanBo,
		canBo.SoTaiKhoan,
		bangLuongThang.GiaTri
	FROM BangLuongThang bangLuongThang
	INNER JOIN ThongTinCanBo canBo
		ON bangLuongThang.MaCanBo = canBo.MaCanBo
END
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_thongtri_detail]    Script Date: 10/06/2022 6:53:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_thongtri_detail]
@iIdThongTriId uniqueidentifier,
@sMaDonViQuanLy nvarchar(50),
@iLoaiThongTri int,
@iNamkeHoach int,
@dNgayThongTri DATE,
@sMaNguonVon nvarchar(max)
AS
BEGIN
	IF @iLoaiThongTri in (1,2)
	BEGIN
		SELECT
			(CASE tbl.iLoaiThanhToan WHEN 1 THEN 
					(CASE WHEN dt.colName in ('fGiaTriThanhToanTN', 'fGiaTriThanhToanNN') 
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_CTT_KPQP' WHEN 2 THEN 'TT_Cap_KPNN' ELSE 'TT_Cap_KPK' END)
						WHEN dt.colName in ('fGiaTriThuHoiNamTruocTN', 'fGiaTriThuHoiNamTruocNN', 'fGiaTriThuHoiNamNayTN', 'fGiaTriThuHoiNamNayNN', 'fGiaTriThuHoiUngTruocNamTruocTN', 'fGiaTriThuHoiUngTruocNamTruocNN', 'fGiaTriThuHoiUngTruocNamNayTN', 'fGiaTriThuHoiUngTruocNamNayNN')
							THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_ThuUng_KPQP' WHEN 2 THEN 'TT_ThuUng_KPNN' ELSE 'TT_ThuUng_KPK' END)
						END)
				WHEN 0 THEN (CASE tbl.iID_NguonVonID WHEN 1 THEN 'TT_TamUng_KPQP' WHEN 2 THEN 'TT_TamUng_KPNN' ELSE 'TT_TamUng_KPK' END) END) as SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.FGiaTri as FSoTien,
			tbl.iID_DuAnId as IIdDuAnId,
			tbl.iID_NhaThauId as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,

			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			tbl.sTenDonViThuHuong as SDonViThuHuong INTO #tmpThanhToan
		FROM VDT_TT_DeNghiThanhToan as tbl
		INNER JOIN (
				SELECT iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, SUM(dt.fGiaTri) as fGiaTri, colName
				from 
				(select iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN from VDT_TT_PheDuyetThanhToan_ChiTiet) as tbl
				UNPIVOT
				(fGiaTri FOR colName IN (fGiaTriThanhToanTN, fGiaTriThanhToanNN, fGiaTriThuHoiNamTruocTN, fGiaTriThuHoiNamTruocNN, fGiaTriThuHoiNamNayTN, fGiaTriThuHoiNamNayNN, fGiaTriThuHoiUngTruocNamTruocTN, fGiaTriThuHoiUngTruocNamTruocNN, fGiaTriThuHoiUngTruocNamNayTN, fGiaTriThuHoiUngTruocNamNayNN)) as dt
				GROUP BY iID_DeNghiThanhToanID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, colName
				HAVING SUM(dt.fGiaTri) <> 0
			) as dt on tbl.Id = dt.iID_DeNghiThanhToanID
		INNER JOIN VDT_DA_DuAn as da on tbl.iID_DuAnId = da.iID_DuAnID
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		LEFT JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		WHERE tbl.iLoaiThanhToan = (CASE WHEN @iLoaiThongTri = 1 THEN 1 ELSE 0 END)
			AND ( tbl.iID_ThongTriThanhToanID = @iIdThongTriId)
			--AND tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			--AND tbl.iNamKeHoach = @iNamkeHoach
			--AND CAST(tbl.dNgayDeNghi as DATE) <= CAST(@dNgayThongTri as DATE)
			--AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))

		SELECT SMaKieuThongTri, SSoThongTri, SUM(ISNULL(FSoTien, 0)) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpThanhToan
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpThanhToan
	END 
	ELSE
	BEGIN
		SELECT (CASE dt.colName WHEN 'fCapPhatTaiKhoBac' THEN 'hop_thuc' 
					ELSE 'kinh_phi' END) SMaKieuThongTri,
			NULL as SSoThongTri,
			dt.iID_DuAnId as IIdDuAnId,
			NULL as IIdNhaThauId,
			dt.iID_MucID as IIdMucId,
			dt.iID_TieuMucID as IIdTieuMucId,
			dt.iID_TietMucID as IIdTietMucId,
			dt.iID_NganhID as IIdNganhId,
			da.iID_LoaiCongTrinhID as IIdLoaiCongTrinhId,
			NULL as IIdLoaiNguonVonId,
			da.iID_CapPheDuyetID as IIdCapPheDuyetId,
			da.STenDuAn,
			ml.SLns,
			ml.SL,
			ml.SK,
			ml.SM,
			ml.STm,
			ml.STtm,
			ml.SNg,
			NULL as SDonViThuHuong INTO #tmpKHV
		FROM VDT_KHV_PhanBoVon as tbl
		INNER JOIN (
			SELECT iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			from 
			(select iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, fCapPhatTaiKhoBac, fCapPhatBangLenhChi from VDT_KHV_PhanBoVon_ChiTiet) as tbl
			UNPIVOT
			(fGiaTri FOR colName IN (fCapPhatTaiKhoBac, fCapPhatBangLenhChi)) as dt
			GROUP BY iID_PhanBoVonID, iID_DuAnID, iID_MucID, iID_TieuMucID, iID_TietMucID, iID_NganhID, iID_LoaiCongTrinh, colName
			HAVING SUM(ISNULL(fGiaTri, 0)) > 0
		) as dt on tbl.Id = dt.iID_PhanBoVonID
		
		INNER JOIN VDT_DA_DuAn as da on dt.iID_DuAnId = da.iID_DuAnID
		INNER JOIN NS_MucLucNganSach as ml on dt.iID_MucID = ml.iID OR dt.iID_TieuMucID = ml.iID OR dt.iID_TietMucID = ml.iID OR dt.iID_NganhID = ml.iID
		INNER JOIN NguonNganSach as nv on tbl.iID_NguonVonID = nv.iID_MaNguonNganSach
		WHERE tbl.iID_MaDonViQuanLy = @sMaDonViQuanLy
			AND tbl.iNamKeHoach = @iNamkeHoach
			AND CAST(tbl.dNgayQuyetDinh as DATE) <= CAST(@dNgayThongTri as DATE)
			AND (@sMaNguonVon IS NULL OR LOWER(nv.sMoTa) = LOWER(@sMaNguonVon))
			AND dt.colName = (CASE WHEN @iLoaiThongTri = 3 THEN 'fCapPhatBangLenhChi' ELSE 'fCapPhatTaiKhoBac' END)

		SELECT SMaKieuThongTri, SSoThongTri, CAST(0 as float) as FSoTien, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 
		FROM #tmpKHV
		GROUP BY SMaKieuThongTri, SSoThongTri, IIdDuAnId, IIdNhaThauId, IIdMucId, IIdTieuMucId, IIdTietMucId, IIdNganhId, IIdLoaiCongTrinhId, IIdLoaiNguonVonId,
			IIdCapPheDuyetId, STenDuAn, SLns, SL, SK, SM, STm, STtm, SNg, SDonViThuHuong 

		DROP TABLE #tmpKHV
	END
END
;
GO
