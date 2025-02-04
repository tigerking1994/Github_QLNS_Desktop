/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 07/02/2023 5:39:16 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]
GO
/****** Object:  StoredProcedure [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu]    Script Date: 07/02/2023 5:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_vdt_getall_duan_notexits_chutruongdautu] 
	@IdChuTruongDauTu nvarchar(200),
	@MaDonVi nvarchar(200)
AS
BEGIN
	SELECT * FROM VDT_DA_DuAn duan
	INNER JOIN DonVi dv
		ON dv.iID_MaDonVi = duan.iID_MaDonViQuanLy
	WHERE
		duan.iID_DuAnID NOT IN   
		(
			SELECT DISTINCT iID_DuAnID FROM VDT_DA_ChuTruongDauTu WHERE iID_DuAnID = duan.iID_DuAnID
			AND iID_ChuTruongDauTuID <> @IdChuTruongDauTu
		)
		AND duan.iID_DuAnID IN 
		(
			SELECT DISTINCT iID_DuAnID from VDT_KHV_KeHoach5Nam_ChiTiet
		)
		----AND dv.iID_MaDonVi = @MaDonVi
		--AND dv.iID_MaDonVi  in (
		---- lay don vi C2
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi
		--	union all  
		--	-- lay don vi C3
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha  
		--	in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	union all  
		--	select  iID_MaDonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha
		--	in (--lay don vi C4
		--		select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_DonViCha 
		--		in (select  iID_DonVi from VDT_DM_DonViThucHienDuAn where  iID_MaDonVi=  @MaDonVi)
		--	)
		--)
		AND dv.iID_MaDonVi in (select * from f_recursive_donvi(@MaDonVi));

END;
;
;

GO


update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6000-6001-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6000-6001-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6000-6001-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6000-6001-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6000-6001-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6000-6049-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6000-6049-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6000-6049-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6000-6049-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6000-6049-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6101-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6101-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6101-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6101-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6101-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6102-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6102-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6102-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6102-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6102-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6103-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6103-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6103-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6103-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6103-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6107-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6107-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6107-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6107-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6107-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6121-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6121-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6121-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6121-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6121-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '1'  where sXauNoiMa ='1010000-010-011-6100-6124-10-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '2'  where sXauNoiMa ='1010000-010-011-6100-6124-20-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.3'  where sXauNoiMa ='1010000-010-011-6100-6124-30-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.1'  where sXauNoiMa ='1010000-010-011-6100-6124-40-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
update NS_MucLucNganSach set sMaCB =  '3.2'  where sXauNoiMa ='1010000-010-011-6100-6124-70-00'  and iNamLamViec = 2023 and bHangChaQuyetToan = 0 
