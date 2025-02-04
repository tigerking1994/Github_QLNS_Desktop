/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 02/09/2022 4:45:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_report_cap_phat_thanh_toan_new]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 02/09/2022 4:45:40 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]    Script Date: 02/09/2022 4:45:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[sp_rpt_skt_phan_bo_kiem_tra_theo_nganh_phu_luc_tong_hop]
	@Nganh varchar(max),
	@MaDonVi varchar(max),
	@NamLamViec int,
	@NamNganSach int,
	@NguonNganSach int,
	@Loai int,
	@dvt int,
	@bTongHop bit
AS
BEGIN
SELECT iID_MLSKT IIdMlskt,
       iID_MLSKTCha IIdMlsktCha,
	   dt_dv.id idDonVi,
	   dt_dv.sTenDonVi,
	   dt_dv.iLoai,
       sKyHieu,
	   sSTT STT,
	   bHangCha,
       sNG,
       sMoTa,
       sNG_Cha sNgCha,
       QuyetToan =SUM(IsNull(A.QuyetToan,0))/@dvt,
       DuToan =SUM(IsNull(A.DuToan,0))/@dvt,
       TuChi =SUM(IsNull(A.TuChi,0))/@dvt,
       PhanCap =SUM(IsNull(A.PhanCap,0))/@dvt,
       MuaHangHienVat =SUM(IsNull(A.MuaHangHienVat,0))/@dvt,
       DacThu =SUM(IsNull(A.PhanCap,0))/@dvt
FROM
  (SELECT ml.iID_MLSKT ,
                ml.iID_MLSKTCha,
                ml.sKyHieu,
                ml.sNG,
                ml.sMoTa,
                ml.sNG_Cha,
				ml.sSTT,
				ml.bHangCha,
                ct.iID_MaDonVi,
				sTenDonVi,
                QuyetToan =0,
                DuToan =0,
                IsNull(ct.fTuChi, 0) TuChi,
                IsNull(ct.fPhanCap, 0) PhanCap,
                IsNull(ct.fMuaHangCapHienVat, 0) MuaHangHienVat,
                IsNull(ct.fPhanCap, 0) DacThu
   FROM NS_SKT_ChungTuChiTiet ct
   LEFT JOIN NS_SKT_ChungTu chungtu ON ct.iID_CTSoKiemTra = chungtu.iID_CTSoKiemTra
   RIGHT JOIN NS_SKT_MucLuc ml ON ct.iID_MLSKT = ml.iID_MLSKT
   AND ml.iNamLamViec = @NamLamViec
   AND ml.iTrangThai = 1
   WHERE ct.iNamLamViec=@NamLamViec
     AND ct.iNamNganSach = @NamNganSach
     AND ct.iID_MaNguonNganSach = @NguonNganSach
     AND ct.iLoai= @Loai
     AND ct.iLoaiChungTu = 1
	 AND (@bTongHop = 1 AND chungtu.bDaTongHop = @bTongHop)
     AND ml.sNG in
       (SELECT *
        FROM f_split(@Nganh)))AS A 
LEFT JOIN
  (SELECT iID_MaDonVi AS id,
          sTenDonVi, iLoai
   FROM DonVi
   WHERE iTrangThai=1
     AND iNamLamViec=@NamLamViec) AS dt_dv ON A.iID_MaDonVi=dt_dv.id
	WHERE (@Loai = 0 and dt_dv.iLoai != 0) or @Loai <> 0
GROUP BY iID_MLSKT,
         iID_MLSKTCha,
		 dt_dv.id,
	     dt_dv.sTenDonVi,
         sKyHieu,
		 sSTT,
		 bHangCha,
         sNG,
         sMoTa,
         sNG_Cha,
		 iLoai
		 order by sKyHieu
END
;
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]    Script Date: 02/09/2022 4:45:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_vdt_report_cap_phat_thanh_toan_new]
@Id varchar(MAX),
@NamLamViec int,
@dvt int
AS 
BEGIN

	SELECT duan.sTenDuAn AS TenDuAn,
		duan.sMaDuAn AS MaDuAn,
		donvi.sTenDonVi AS TenDonVi,
		chudadutu.sTenDonVi AS TenChuDauTu,
		hopdong.sSoHopDong as TenHopDong,
		hopdong.dNgayHopDong AS NgayHopDong,
		hopdong.fTienHopDong/@dvt AS GiaTriHopDong,
		thanhtoan.sSoDeNghi AS SoDeNghi,
		thanhtoan.iLoaiThanhToan AS LoaiThanhToan,
		nguonvon.sTen AS TenNguonVon,
		thanhtoan.iNamKeHoach AS NamKeHoach,
		isnull(thanhtoan.fGiaTriThanhToanTN, 0) AS ThanhToanTN,
		isnull(thanhtoan.fGiaTriThanhToanNN, 0) AS ThanhToanNN,
		(isnull(thanhtoan.fGiaTriThuHoiTN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocTN, 0)) AS ThuHoiTN,
		(isnull(thanhtoan.fGiaTriThuHoiNN, 0) + isnull(thanhtoan.fGiaTriThuHoiUngTruocNN, 0)) AS ThuHoiNN,
		isnull(thanhtoan.fThueGiaTriGiaTang, 0) AS ThueGiaTriGiaTang,
		isnull(thanhtoan.fChuyenTienBaoHanh, 0) AS ChuyenTienBaoHanh,
		thanhtoan.sGhiChu as NoiDung,
		nhathau.sTenNhaThau AS TenNhaThau,
		nhathau.sSoTaiKhoan AS SoTaiKhoanNhaThau,
		pbv.dNgayQuyetDinh as khvNgayQuyetDinh,
		thanhtoan.dNgayDeNghi as thanhtoanNgayDeNghi
	FROM VDT_TT_DeNghiThanhToan thanhtoan
	LEFT JOIN VDT_DA_DuAn duan ON thanhtoan.iID_DuAnId = duan.iID_DuAnID
	LEFT JOIN
	  (SELECT *
	   FROM DonVi
	   WHERE iNamLamViec = @NamLamViec ) donvi ON thanhtoan.iID_MaDonViQuanLy = donvi.iID_MaDonVi
	left join VDT_KHV_PhanBoVon pbv on thanhtoan.iID_PhanBoVonID = pbv.Id
	LEFT JOIN VDT_DA_TT_HopDong hopdong ON thanhtoan.iID_HopDongId = hopdong.Id
	LEFT JOIN NguonNganSach nguonvon ON thanhtoan.iID_NguonVonID = nguonvon.iID_MaNguonNganSach
	LEFT JOIN DM_ChuDauTu chudadutu ON chudadutu.iID_DonVi = duan.iID_ChuDauTuID
	LEFT JOIN VDT_DM_NhaThau nhathau on thanhtoan.iID_NhaThauId = nhathau.Id
	WHERE thanhtoan.Id = @Id
END
GO
