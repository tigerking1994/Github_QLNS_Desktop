/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 1/2/2024 10:52:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 1/2/2024 10:52:24 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnKCB_create_data_summary]    Script Date: 1/2/2024 10:52:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcnKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@IDMaDonVi nvarchar(500),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet(
ID_QTC_Nam_KCB_QuanYDonVi_ChiTiet,
iID_QTC_Nam_KCB_QuanYDonVi,
iID_MucLucNganSach,
sNoiDung,
sXauNoiMa,
iID_MaDonVi,
iNamLamViec,
dNgaySua,
dNgayTao,
sNguoiSua,
sNguoiTao,
fTien_DuToanNamTruocChuyenSang,
fTien_DuToanGiaoNamNay,
fTien_TongDuToanDuocGiao,
fTien_ThucChi,
fTienThua,
fTienThieu)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   sXauNoiMa,
	   @IDMaDonVi,
	   iNamLamViec,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   Sum(fTien_DuToanNamTruocChuyenSang),
	   Sum(fTien_DuToanGiaoNamNay),
	   Sum(fTien_TongDuToanDuocGiao),
	   Sum(fTien_ThucChi),
	  CASE WHEN Sum(fTienThua) > Sum(fTienThieu) THEN Sum(fTienThua) - Sum(fTienThieu) ELSE 0 END,
	  CASE WHEN Sum(fTienThua) < Sum(fTienThieu) THEN Sum(fTienThieu) - Sum(fTienThua) ELSE 0 END
	   
FROM BH_QTC_Nam_KCB_QuanYDonVi_ChiTiet
WHERE  iID_QTC_Nam_KCB_QuanYDonVi IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung,sXauNoiMa,iNamLamViec

UPDATE BH_QTC_Nam_KCB_QuanYDonVi SET bDaTongHop = 1 WHERE ID_QTC_Nam_KCB_QuanYDonVi IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]    Script Date: 1/2/2024 10:52:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannamKCB_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtn.ID_QTC_Nam_KCB_QuanYDonVi,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamLamViec,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanNamTruocChuyenSang,
		qtn.fTongTien_DuToanGiaoNamNay,
		qtn.fTongTien_TongDuToanDuocGiao,
		qtn.fTongTien_ThucChi,
		qtn.fTongTienThua,
		qtn.fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao,
		qtn.sDSLNS
	from BH_QTC_Nam_KCB_QuanYDonVi as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtn.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
