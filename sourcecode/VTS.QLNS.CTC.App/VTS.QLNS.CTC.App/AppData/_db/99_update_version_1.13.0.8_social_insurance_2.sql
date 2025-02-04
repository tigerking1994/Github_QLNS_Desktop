/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 8/9/2023 4:07:02 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dt_dieuchinh_dutoan_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dt_dieuchinh_dutoan_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_dt_dieuchinh_dutoan_index]    Script Date: 8/9/2023 4:07:02 PM ******/
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
	LEFT JOIN BH_DM_LoaiChi lc on dcdt.iID_LoaiCap=lc.iID and dcdt.iNamChungTu=lc.iNamLamViec
END


GO
