
-- Đổi lại tên nguồn ngân sách cho đúng dấu gạch ngang
IF EXISTS (SELECT * FROM DanhMuc WHERE sTen = N'5- Ngân sách địa phương' AND sType = 'NS_NguonNganSach')
UPDATE DanhMuc 
SET sTen = N'5 - Ngân sách địa phương'
WHERE sTen = N'5- Ngân sách địa phương' AND sType = 'NS_NguonNganSach'

-- Đổi lại tên nguồn ngân sách cho đúng dấu gạch ngang
IF EXISTS (SELECT * FROM DanhMuc WHERE sTen = N'8- Nguồn dự phòng' AND sType = 'NS_NguonNganSach')
UPDATE DanhMuc 
SET sTen = N'8 - Nguồn dự phòng'
WHERE sTen = N'8- Nguồn dự phòng' AND sType = 'NS_NguonNganSach'

-- Đổi lại tên nguồn ngân sách cho đúng dấu gạch ngang
IF EXISTS (SELECT * FROM DANHMUC WHERE sTen = N'9- BHXH, YT, TN' AND sType = 'NS_NguonNganSach')
UPDATE DanhMuc 
SET sTen =  N'9 - BHXH, YT, TN'
WHERE sTen = N'9- BHXH, YT, TN' AND sType = 'NS_NguonNganSach'

-- Thêm danh mục đơn vị thông tri ban hành là Phòng tài chính
IF NOT EXISTS (SELECT * FROM DanhMuc WHERE iID_MaDanhMuc = 'DV_THONGTRI_BANHANH')
INSERT INTO DanhMuc VALUES(NEWID(), GETDATE(), GETDATE(), 'DV_THONGTRI_BANHANH', 2022, 0, 1, NULL, NULL, N'Phòng tài chính', N'Cấp 3', 'admin', 'admin', 'Tên đơn vị thông tri ban hành', 'DM_CauHinh', NULL)

/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]    Script Date: 06/12/2022 5:02:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoan_giaithichbangso]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]    Script Date: 06/12/2022 5:02:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 06/12/2022 5:02:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_delete_data_mlns_by_type]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_delete_data_mlns_by_type]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 06/12/2022 5:08:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 06/12/2022 5:02:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_check_data_used_mlns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_check_data_used_mlns]
GO
/****** Object:  StoredProcedure [dbo].[sp_check_data_used_mlns]    Script Date: 06/12/2022 5:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_check_data_used_mlns] 
	@YearOfWork int,
	@CodeChain nvarchar(max),
	@Type bit

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 0)

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.LoaiChungTu,
		dulieu.Loai,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.SoQuyetDinh,
		dulieu.NgayQuyetDinh,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT 
	t1.iID_CTDTDauNamChiTiet AS ID,
	t3.iID_CTDTDauNam AS ID_Parent,
	N'Dự toán đầu năm' AS LoaiChungTu,
	'DU_TOAN_DAU_NAM' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	'' AS SoQuyetDinh,
	'' AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + t2.fTuChi) AS SoTien
	FROM NS_DTDauNam_ChungTuChiTiet t1
	LEFT JOIN NS_DTDauNam_PhanCap t2 ON t1.iID_CTDTDauNamChiTiet = t2.iID_CTDTDauNamChiTiet
	JOIN NS_DTDauNam_ChungTu t3 ON t1.iID_CTDTDauNam = t3.iID_CTDTDauNam
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Nhận dự toán' AS LoaiChungTu,
	'NHAN_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 0
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_DTCTChiTiet AS ID,
	t2.iID_DTChungTu AS ID_Parent,
	N'Phân bổ dự toán' AS LoaiChungTu,
	'PHAN_BO_DU_TOAN' AS Loai,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.sSoQuyetDinh AS SoQuyetDinh,
	t2.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iID_MaDonVi AS MaDonVi,
	t2.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHienVat + t1.fHangNhap + t1.fHangMua + t1.fPhanCap) AS SoTien
	FROM NS_DT_ChungTuChiTiet t1
	JOIN NS_DT_ChungTu t2 ON t1.iID_DTChungTu = t2.iID_DTChungTu AND t2.iLoai = 1
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	UNION

	SELECT
	t1.iID_CTNganhChiTiet AS ID,
	t3.iID_CTNganh AS ID_Parent, 
	N'Phân cấp ngân sách ngành' AS LoaiChungTu,
	'PHAN_CAP_NGAN_SACH_NGANH' AS Loai,
	t3.sSoChungTu AS SoChungTu,
	t3.dNgayChungTu AS NgayChungTu,
	t3.sSoCongVan AS SoQuyetDinh,
	t3.dNgayQuyetDinh AS NgayQuyetDinh,
	t1.iiD_MaDonVi AS MaDonVi,
	t3.sMoTa AS MoTa,
	(t1.fTuChi + t1.fHangNhap + t1.fHangMua + t1.fPhanCap + t2.fHienVat + t2.fPhanCap) AS SoTien
	FROM NS_Nganh_ChungTuChiTiet t1
	LEFT JOIN NS_Nganh_ChungTuChiTiet_PhanCap t2 ON t1.iID_CTNganhChiTiet = t2.iID_CTNganhChiTiet
	JOIN NS_Nganh_ChungTu t3 ON t1.iID_CTNganh = t3.iID_CTNganh
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))

	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
	
	ELSE

	SELECT
		dulieu.ID,
		dulieu.ID_Parent,
		dulieu.SoChungTu,
		dulieu.NgayChungTu,
		dulieu.MaDonVi,
		donvi.sTenDonVi AS TenDonVi,
		dulieu.ThangQuy,
		dulieu.Loai,
		dulieu.LoaiQuyetToan,
		dulieu.MoTa,
		dulieu.SoTien
	FROM (
	SELECT
	t1.iID_QTCTChiTiet AS ID,
	t2.iID_QTChungTu AS ID_Parent,
	t2.sSoChungTu AS SoChungTu,
	t2.dNgayChungTu AS NgayChungTu,
	t2.iID_MaDonVi AS MaDonVi,
	t2.iThangQuy AS ThangQuy,
	'QUYET_TOAN' AS Loai,
	CASE
		WHEN t2.sLoai = '101' THEN N'Thường xuyên'
		WHEN t2.sLoai = '1' THEN N'Quốc phòng'
		WHEN t2.sLoai = '2' THEN N'Nhà nước'
		WHEN t2.sLoai = '3' THEN N'Ngoại hối'
		WHEN t2.sLoai = '4' THEN N'Kinh phí khác'
		ELSE ''
	END AS LoaiQuyetToan,
	t2.sMoTa AS MoTa,
	t1.fTuChi_PheDuyet AS SoTien
	FROM NS_QT_ChungTuChiTiet t1
	JOIN NS_QT_ChungTu t2 ON t1.iID_QTChungTu = t2.iID_QTChungTu
	WHERE t1.iNamLamViec = @YearOfWork AND t1.sXauNoiMa IN (SELECT * FROM f_split(@CodeChain))
	) dulieu
	LEFT JOIN DonVi donvi ON dulieu.MaDonVi = donvi.iID_MaDonVi AND donvi.iNamLamViec = @YearOfWork
END
;
;

GO
/****** Object:  StoredProcedure [dbo].[sp_delete_data_mlns_by_type]    Script Date: 06/12/2022 5:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_delete_data_mlns_by_type] 
	@CodeChain nvarchar(max),
	@Type nvarchar(max),
	@VoucherID nvarchar(max)

AS
BEGIN

	SET NOCOUNT ON;

	IF (@Type = 'NHAN_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa = @CodeChain
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'PHAN_BO_DU_TOAN')
	DELETE NS_DT_ChungTuChiTiet 
	WHERE sXauNoiMa = @CodeChain
	AND iID_DTCTChiTiet = @VoucherID

	IF (@Type = 'DU_TOAN_DAU_NAM')
	BEGIN
	DELETE NS_DTDauNam_ChungTuChiTiet 
	WHERE sXauNoiMa = @CodeChain
	AND iID_CTDTDauNamChiTiet = @VoucherID    
	
	DELETE NS_DTDauNam_PhanCap
	WHERE sXauNoiMa = @CodeChain
	AND iID_CTDTDauNamChiTiet = @VoucherID
	END

	IF (@Type = 'PHAN_CAP_NGAN_SACH_NGANH')
	BEGIN
	DELETE NS_Nganh_ChungTuChiTiet 
	WHERE sXauNoiMa = @CodeChain
	AND iID_CTNganhChiTiet = @VoucherID

	DELETE NS_Nganh_ChungTuChiTiet_PhanCap
	WHERE sXauNoiMa = @CodeChain
	AND iID_CTNganhChiTiet = @VoucherID
	END

	IF (@Type = 'QUYET_TOAN')
	DELETE NS_QT_ChungTuChiTiet 
	WHERE sXauNoiMa = @CodeChain
	AND iID_QTCTChiTiet = @VoucherID

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]    Script Date: 06/12/2022 5:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangloi]
@sLoai nvarchar(50),
@iID_MaDonVi nvarchar(50),
@iThang int,
@iQuy int,
@iNamLamViec int,
@iNamNganSach int,
@iID_NguonNganSach int,
@isTongHop int,
@explainId nvarchar(50)
as 
begin

select top 1 sMoTa_KienNghi as SMoTaTinhHinh, sMoTa_TinhHinh as SMoTaKienNghi
			into #temp
			from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
			inner join  NS_QT_ChungTu ct on ctgt.iID_QTChungTu = ct.iID_QTChungTu
			where ctgt.iNamLamViec = @iNamLamViec
			and ct.iNamNganSach = @iNamNganSach
			and ct.iID_MaNguonNganSach = @iID_NguonNganSach
			and ct.iThangQuyLoai = @iQuy and ct.iThangQuy = @iThang
			and ctgt.iID_QTChungTu in ( select iID_QTChungTu from NS_QT_ChungTu where iID_MaDonVi = @iID_MaDonVi and sLoai = @sLoai)  

if @isTongHop = 1
	BEGIN
		select top 1 sMoTa_KienNghi, sMoTa_TinhHinh
		into #temp_tonghop
		from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
		where iID_GiaiThich = @explainId;

		if ((select count(*) from #temp_tonghop) = 1)
			begin
				select * from #temp_tonghop;
				drop table #temp_tonghop;
			end
			
		else
		   select * from #temp;
    END
else
   begin
		select * from #temp;
   end

drop table #temp
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]    Script Date: 06/12/2022 5:02:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ns_rpt_quyettoan_giaithichbangso]
@sLoai nvarchar(50),
@iID_MaDonVi nvarchar(50),
@iThang int,
@iQuy int,
@iNamLamViec int,
@iNamNganSach int,
@iID_NguonNganSach int,
@isTongHop int,
@explainId nvarchar(50)
as 
begin
	select 
				   --Tiền lương tháng này
				   sum(fLuong_SiQuan) as FLuongSiQuan, sum(fLuong_QNCN) as FLuongQncn, sum(fLuong_CNVQP) as FLuongCnvqp, sum(fLuong_HD) as FLuongHd,
				   sum(fPhuCap_SiQuan) as FPhuCapSiQuan, sum(fPhuCap_QNCN) as FPhuCapQncn, sum(fPhuCap_CNVQP) as FPhuCapCnvqp, sum(fPhuCap_HD) as FPhuCapHd,

				   --Tiền lương của những ngày nghỉ không lương
				   sum(fLuong_SiQuan_Tru) as FLuongSiQuanTru, sum(fLuong_QNCN_Tru) as FLuongQncnTru, sum(fLuong_CNVQP_Tru) as FLuongCnvqpTru, sum(fLuong_HD_Tru) as FLuongHdTru,
				   sum(fPhuCap_SiQuan_Tru) as FPhuCapSiQuanTru, sum(fPhuCap_QNCN_Tru) as FPhuCapQncnTru, sum(fPhuCap_CNVQP_Tru) as FPhuCapCnvqpTru, sum(fPhuCap_HD_Tru) as FPhuCapHdTru,

				   --Tiền lương của những ngày hưởng trợ cấp BHXH
				   sum(fLuongBHXH_SiQuan_Tru) as FLuongBhxhSiQuanTru, sum(fLuongBHXH_QNCN_Tru) as FLuongBhxhQncnTru, sum(fLuongBHXH_CNVQP_Tru) as FLuongBhxhCnvqpTru, sum(fLuongBHXH_HD_Tru) as FLuongBhxhHdTru,
				   sum(fPhuCapBHXH_HD_Tru) as FPhuCapBhxhSiQuanTru, sum(fPhuCapBHXH_QNCN_Tru) as FPhuCapBhxhQncnTru, sum(fPhuCapBHXH_CNVQP_Tru) as FPhuCapBhxhCnvqpTru, sum(fPhuCapBHXH_HD_Tru) as FPhuCapBhxhHdTru,

				   --Tiền lương quyết toán tháng này
				   sum(fLuong_SiQuan_QT) as FLuongSiQuanQt, sum(fLuong_QNCN_QT) as FLuongQncnQt, sum(fLuong_CNVQP_QT) as FLuongCnvqpQt, sum(fLuong_HD_QT) as FLuongHdQt,
				   sum(fPhuCap_SiQuan_QT) as FPhuCapSiQuanQt, sum(fPhuCap_QNCN_QT) as FPhuCapQncnQt, sum(fPhuCap_CNVQP_QT) as FPhuCapCnvqpQt, sum(fLuong_HD_QT) as FPhuCapHdQt,

				   --Kinh phí không thực hiện tự chủ
				   sum(fKinhPhi_LuongPC_Khac) as FKinhPhiLuongPcKhac,
				   sum(fKinhPhi_PhuCap_HSQBS) as FKinhPhiPhuCapHsqbs,
				   sum(fKinhPhi_An) as FKinhPhiAn,

				   --Quân số cung cấp tiền ăn
				   sum(fNgayAn) as FNgayAn,
				   sum(fNgayAn_Cong) as FNgayAnCong,
				   sum(fNgayAn_Tru) as FNgayAnTru,
				   sum(fNgayAn_QT) as FNgayAnQt,

				   --Ra quân trong tháng
				   sum(fRaQuan_SiQuan_Nguoi_XuatNgu) as FRaQuanSiQuanNguoiXuatNgu, sum(fRaQuan_SiQuan_Tien_XuatNgu) as FRaQuanSiQuanTienXuatNgu, sum(fRaQuan_QNCN_Nguoi_XuatNgu) as FRaQuanQncnNguoiXuatNgu, 
				   sum(fRaQuan_QNCN_Tien_XuatNgu) as FRaQuanQncnTienXuatNgu, sum(fRaQuan_CNVQP_Nguoi_XuatNgu) as FRaQuanCnvqpNguoiXuatNgu, sum(fRaQuan_CNVQP_Tien_XuatNgu) as FRaQuanCnvqpTienXuatNgu,
				   sum(fRaQuan_HSQCS_Nguoi_XuatNgu) as FRaQuanHsqcsNguoiXuatNgu , sum(fRaQuan_HSQCS_Tien_XuatNgu) as FRaQuanHsqcsTienXuatNgu,

				   sum(fRaQuan_SiQuan_Nguoi_Huu) as FRaQuanSiQuanNguoiHuu, sum(fRaQuan_SiQuan_Tien_Huu) as FRaQuanSiQuanTienHuu, sum(fRaQuan_QNCN_Nguoi_Huu) as FRaQuanQncnNguoiHuu, 
				   sum(fRaQuan_QNCN_Tien_Huu) as FRaQuanQncnTienHuu, sum(fRaQuan_CNVQP_Nguoi_Huu) as FRaQuanCnvqpNguoiHuu, sum(fRaQuan_CNVQP_Tien_Huu) as FRaQuanCnvqpTienHuu,
				   sum(fRaQuan_HSQCS_Nguoi_Huu) as FRaQuanHsqcsNguoiHuu , sum(fRaQuan_HSQCS_Tien_Huu) as FRaQuanHsqcsTienHuu,
		   
				   sum(fRaQuan_SiQuan_Nguoi_ThoiViec) as FRaQuanSiQuanNguoiThoiViec, sum(fRaQuan_SiQuan_Tien_ThoiViec) as FRaQuanSiQuanTienThoiViec, sum(fRaQuan_QNCN_Nguoi_ThoiViec) as FRaQuanQncnNguoiThoiViec, 
				   sum(fRaQuan_QNCN_Tien_ThoiViec) as FRaQuanQncnTienThoiViec, sum(fRaQuan_HSQCS_Nguoi_ThoiViec) as FRaQuanCnvqpNguoiThoiViec, sum(fRaQuan_CNVQP_Tien_ThoiViec) as FRaQuanCnvqpTienThoiViec,
				   sum(fRaQuan_HSQCS_Nguoi_ThoiViec) as FRaQuanHsqcsNguoiThoiViec , sum(fRaQuan_HSQCS_Tien_ThoiViec) as FRaQuanHsqcsTienThoiViec


			from  NS_QT_ChungTuChiTiet_GiaiThich ctgt
			inner join  NS_QT_ChungTu ct on ctgt.iID_QTChungTu = ct.iID_QTChungTu
			where ctgt.iNamLamViec = @iNamLamViec
			and ct.iNamNganSach = @iNamNganSach
			and ct.iID_MaNguonNganSach = @iID_NguonNganSach
			and ( (@iQuy = 0 and ct.iThangQuy = @iThang) or
				  (@iQuy = 1 and ((@iThang = 3 and ct.iThangQuy in (1,2,3)) or (@iThang = 6 and ct.iThangQuy in (4,5,6)) or (@iThang = 9 and ct.iThangQuy in (7,8,9)) or (@iThang = 12 and ct.iThangQuy in (10,11,12))))
				)
			and ctgt.iID_QTChungTu in ( select iID_QTChungTu from NS_QT_ChungTu where iID_MaDonVi = @iID_MaDonVi and sLoai = @sLoai)  
			group by ctgt.iID_MaDonVi
	
end
GO

/****** Object:  StoredProcedure [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]    Script Date: 06/12/2022 5:08:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_dt_dutoan_dotnhan_phanbo_find_all]
	@YearOfWork int,
	@YearOfBudget int,
	@BudgetSource int,
	@Type int,
	@VoucherType int,
	@Date datetime,
	@Index int
AS
BEGIN
	DECLARE @DieuChinh int = 4;
	WITH tblNhanPhanBo AS (
		SELECT *, 
			   fTongTuChi + fTongHienVat + fTongPhanCap + fTongHangNhap + fTongHangMua + fTongDuPhong + fTongTonKho AS SoPhanBo
		FROM NS_DT_ChungTu 
		WHERE iNamLamViec = @YearOfWork 
			AND iNamNganSach = @YearOfBudget 
			AND iID_MaNguonNganSach = @BudgetSource
			AND iLoai = @Type 
			AND iLoaiChungTu = @VoucherType
			AND (fTongTuChi <> 0 OR fTongHienVat <> 0 OR fTongPhanCap <> 0 OR fTongHangNhap <> 0 OR fTongHangMua <> 0 OR fTongDuPhong <> 0 OR fTongTonKho <> 0)
			AND (
				(dNgayQuyetDinh IS NOT NULL AND CAST(dNgayQuyetDinh AS DATE) <= CAST(@Date AS DATE)) 
				OR 
				(dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND CAST(dNgayChungTu AS DATE) <= CAST(@Date AS DATE)))
	),
	tblChungTuNhanPhanBoMap AS (
		SELECT * 
		FROM NS_DT_Nhan_PhanBo_Map 
		WHERE iID_CTDuToan_Nhan IN (SELECT iID_DTChungTu FROM tblNhanPhanBo)
	),
	tblDaPhanBo AS (
		SELECT ctpb.iID_CTDuToan_Nhan, 
			   SUM(fTuChi) + SUM(fHienVat) + SUM(fPhanCap) + SUM(fHangNhap) + SUM(fHangMua) + SUM(fDuPhong) + SUM(fTonKho) AS DaPhanBo 
		FROM tblChungTuNhanPhanBoMap ctpb
		LEFT JOIN 
			(SELECT dtctct.*, dtct.iLoaiDuToan, dtct.iID_DotNhan FROM NS_DT_ChungTuChiTiet dtctct
			 LEFT JOIN NS_DT_ChungTu dtct
			 ON dtctct.iID_DTChungTu = dtct.iID_DTChungTu
			 WHERE dtctct.iNamLamViec = @YearOfWork 
				   AND dtct.iLoai = 1
				   AND dtct.iLoaiChungTu = 1
				   AND (dNgayQuyetDinh IS NOT NULL AND (CAST(dNgayQuyetDinh AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayQuyetDinh AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex))) 
				   OR (dNgayQuyetDinh IS NULL AND dNgayChungTu IS NOT NULL AND (CAST(dNgayChungTu AS DATE) < CAST(@Date AS DATE) OR (CAST(dNgayChungTu AS DATE) = CAST(@Date AS DATE) AND @Index > iSoChungTuIndex)))
				   AND dtctct.iNamNganSach = @YearOfBudget 
				   AND dtctct.iID_MaNguonNganSach = @BudgetSource) ctct
		ON ctpb.iID_CTDuToan_Nhan = ctct.iID_CTDuToan_Nhan
		WHERE ctct.iID_DTChungTu IS NOT NULL
		and 
		(
			(ctpb.iID_CTDuToan_PhanBo = ctct.iID_DTChungTu AND ctct.iLoaiDuToan <> @DieuChinh)
			OR
			(ctct.iLoaiDuToan = @DieuChinh AND ctct.iID_DotNhan = ctpb.iID_CTDuToan_PhanBo)
		)
		GROUP BY ctpb.iID_CTDuToan_Nhan
	)

	SELECT 
		   npb.iID_DTChungTu AS Id,
		   npb.sSoChungTu AS SSoChungTu,
		   npb.dNgayChungTu AS DNgayChungTu,
		   npb.sDSLNS AS SDslns,
		   npb.sDSID_MaDonVi AS SDsidMaDonVi,
		   npb.sSoQuyetDinh AS SSoQuyetDinh,
		   npb.dNgayQuyetDinh AS DNgayQuyetDinh,
		   ISNULL(npb.SoPhanBo, 0) AS SoPhanBo,
		   ISNULL(DaPhanBo, 0) AS DaPhanBo,
		   ISNULL(npb.SoPhanBo, 0) - ISNULL(DaPhanBo, 0) AS SoChuaPhanBo 
	FROM tblNhanPhanBo npb
	LEFT JOIN tblDaPhanBo dpb
	ON npb.iID_DTChungTu = dpb.iID_CTDuToan_Nhan
	WHERE ISNULL(npb.SoPhanBo, 0) <> ISNULL(DaPhanBo, 0) OR ISNULL(npb.SoPhanBo, 0) = 0
	ORDER BY npb.sSoChungTu

END
;
;
;
;
;
;
GO
