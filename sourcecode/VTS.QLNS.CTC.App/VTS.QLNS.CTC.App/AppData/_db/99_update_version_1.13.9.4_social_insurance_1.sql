/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 1/26/2024 2:54:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thtc_get_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 1/26/2024 2:54:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDuToan
		from BH_DTT_BHXH_PhanBo_ChungTu dtt
		where iNamLamViec = @NamLamViec
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDuToan
		from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
		where iNamLamViec = @NamLamViec
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh, iLoaiDotNhanPhanBo iLoaiDuToan
		from BH_DTC_PhanBoDuToanChi dtt
		where iNamChungTu = @NamLamViec

END
;
;
GO
