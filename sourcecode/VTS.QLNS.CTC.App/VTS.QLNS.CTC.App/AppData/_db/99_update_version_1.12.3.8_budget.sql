/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_exclude]    Script Date: 12/12/2022 6:04:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ns_mlns_exclude]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ns_mlns_exclude]
GO
/****** Object:  StoredProcedure [dbo].[sp_ns_mlns_exclude]    Script Date: 12/12/2022 6:04:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ns_mlns_exclude]
@iNamLamViec int,
@mlnsexclude t_tbl_string READONLY
AS
BEGIN
	SELECT iID as Id,
		BDuPhong as BDuPhong,
		BHangCha,
		BHangChaDuToan,
		BHangChaQuyetToan,
		BHangMua,
		BHangNhap,
		BHienVat,
		BNgay,
		BPhanCap,
		BSoNguoi,
		BTonKho,
		BTuChi,
		SChiTietToi,
		DNgaySua,
		DNgayTao,
		ILoai,
		ILock,
		ITrangThai,
		iID_MaDonVi as IdMaDonVi,
		iID_MaBQuanLy as IdPhongBan,
		SK as K,
		SL as L,
		sLNS as Lns,
		sM as M,
		iID_MLNS as MlnsId,
		iID_MLNS_Cha as MlnsIdParent,
		sMoTa as MoTa,
		iNamLamViec as NamLamViec,
		sNG as Ng,
		SCPChiTietToi,
		SDuToanChiTietToi,
		SNguoiSua,
		SNguoiTao,
		SNhapTheoTruong,
		SQuyetToanChiTietToi,
		Tag,
		sTM as Tm,
		sTNG as Tng,
		sTNG1 as Tng1,
		sTNG2 as Tng2,
		sTNG3 as Tng3,
		sTTM as Ttm,
		sXauNoiMa as XauNoiMa,
		ILoaiNganSach
	FROM NS_MucLucNganSach as tbl
	LEFT JOIN @mlnsexclude as ex on tbl.sXauNoiMa = ex.sId
	WHERE tbl.iNamLamViec = @iNamLamViec AND ex.sId IS NULL
END
GO
