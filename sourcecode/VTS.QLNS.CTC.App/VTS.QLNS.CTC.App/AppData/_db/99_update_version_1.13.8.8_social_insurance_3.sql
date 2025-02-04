/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_index_bh]    Script Date: 1/17/2024 10:08:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_cp_chungtu_index_bh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_cp_chungtu_index_bh]
GO
/****** Object:  StoredProcedure [dbo].[sp_cp_chungtu_index_bh]    Script Date: 1/17/2024 10:08:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị chứng từ cấp phát BHXH

-- =============================================
CREATE PROCEDURE [dbo].[sp_cp_chungtu_index_bh]
	@YearOfWork int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 ct.iID_CP_ChungTu 
		, ct.sSoChungTu
		, ct.dNgayChungTu
		, ct.sSoQuyetDinh
		, ct.dNgayQuyetDinh
		, ct.iNamChungTu
		, ct.sID_MaDonVi
		, ct.sMoTa
		, ct.sLNS
		, ct.iID_LoaiCap
		, ct.fTienDaCap
		, ct.fTienKeHoachCap
		, ct.fTienDuToan
		, ct.sTongHop
		, ct.iID_TongHop
		, ct.iLoaiTongHop
		, ct.dNgaySua
		, ct.dNgayTao
		, ct.sNguoiSua
		, ct.sNguoiTao
		, ct.dNgayTao
		, ct.bIsKhoa
		, ct.iQuy
		, lc.sTenDanhMucLoaiChi
		, lc.sMaLoaiChi as SMaLoaiChi
		-- Tong dự toán todo
	FROM BH_CP_ChungTu ct
	LEFT JOIN BH_DM_LoaiChi lc on ct.iID_LoaiCap=lc.iID and ct.iNamChungTu=lc.iNamLamViec 
	WHERE ct.iNamChungTu=@YearOfWork
END
;
GO
