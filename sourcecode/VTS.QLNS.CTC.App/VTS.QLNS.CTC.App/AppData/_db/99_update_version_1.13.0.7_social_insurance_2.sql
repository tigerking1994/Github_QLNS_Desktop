/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 8/4/2023 6:17:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 8/4/2023 6:17:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 8/4/2023 6:17:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chi_tiet]    Script Date: 8/4/2023 6:17:32 PM ******/
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
		ct.iID_DTC_DieuChinhDuToanChi ,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sM,
		ct.sTM,
		ct.sNoiDung,
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
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTC_DieuChinhDuToanChi bh
			JOIN 
				BH_DTC_DieuChinhDuToanChi_ChiTiet bhct ON bh.iID_DTC_DieuChinhDuToanChi = bhct.iID_DTC_DieuChinhDuToanChi 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
				--AND (@BKhoa = 3 OR bh.bIsKhoa = @BKhoa) 
		) ct;

END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_chungtu_chitiet_tao_tonghop]    Script Date: 8/4/2023 6:17:32 PM ******/
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
    iID_BH_DTC_ChiTiet, iID_DTC_DieuChinhDuToanChi, 
    iID_MucLucNganSach, sM, sTM, sNoiDung, 
    fTienDuToanDuocGiao, fTienThucHien06ThangDauNam, 
    fTienUocThucHien06ThangCuoiNam, 
    fTienUocThucHienCaNam, fTienSoSanhTang, 
    fTienSoSanhGiam, dNgaySua, dNgayTao, 
    sNguoiSua, sNguoiTao
  ) 
SELECT 
  DISTINCT NEWID(), 
  @idChungTu, 
  iID_MucLucNganSach, 
  sM, 
  sTM, 
  sNoiDung, 
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
  iID_DTC_DieuChinhDuToanChi in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) 
GROUP BY 
  sM, 
  sTM, 
  iID_MucLucNganSach, 
  sNoiDung;
--danh dau chung tu da tong hop
update 
  BH_DTC_DieuChinhDuToanChi 
set 
  iLoaiTongHop = 2 
where 
  iID_DTC_DieuChinhDuToanChi in (
    SELECT 
      * 
    FROM 
      f_split(@ListIdChungTuTongHop)
  ) END;
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 8/4/2023 6:17:32 PM ******/
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
		 dcdt.iID_DTC_DieuChinhDuToanChi 
		, dcdt.sSoChungTu
		, dcdt.dNgayChungTu
		, dcdt.sSoQuyetDinh
		, dcdt.dNgayQuyetDinh
		, dcdt.iNamChungTu
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
	LEFT JOIN BH_DanhMucLoaiChi lc on dcdt.iID_LoaiCap=lc.iID and dcdt.iNamChungTu=lc.iNamLamViec
END


GO
