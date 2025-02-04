/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]    Script Date: 10/16/2023 2:27:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]    Script Date: 10/16/2023 2:27:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/16/2023 2:27:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 10/16/2023 2:27:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 10/16/2023 2:27:24 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqKCB_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_create_data_summary]    Script Date: 10/16/2023 2:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_KCB_ChiTiet(
		ID_QTC_Quy_KCB_ChiTiet,
		iID_QTC_Quy_KCB,
		iID_MucLucNganSach,
		sNoiDung,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTien_DuToanNamTruocChuyenSang,
		fTien_DuToanGiaoNamNay,
		fTien_TongDuToanDuocGiao,
		fTienThucChi,
		fTienQuyetToanDaDuyet,
		fTienDeNghiQuyetToanQuyNay,
		fTienXacNhanQuyetToanQuyNay
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sNoiDung,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTien_DuToanNamTruocChuyenSang),
	   SUM(fTien_DuToanGiaoNamNay),
	   SUM(fTien_TongDuToanDuocGiao),
	   SUM(fTienThucChi),
	   SUM(fTienQuyetToanDaDuyet),
	   SUM(fTienXacNhanQuyetToanQuyNay),
	    SUM(fTienXacNhanQuyetToanQuyNay)
FROM BH_QTC_Quy_KCB_ChiTiet
WHERE  iID_QTC_Quy_KCB IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sNoiDung

UPDATE BH_QTC_Quy_KCB SET bDaTongHop = 1 WHERE ID_QTC_Quy_KCB IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqKCB_update_total]    Script Date: 10/16/2023 2:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqKCB_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTien_DuToanNamTruocChuyenSang float;
	declare @FTongTien_DuToanGiaoNamNay float;
	declare @FTongTien_TongDuToanDuocGiao float;
	declare @FTongTienThucChi float;
	declare @FTongTienQuyetToanDaDuyet float;
	declare @FTongTienDeNghiQuyetToanQuyNay float;
	declare @FTongTienXacNhanQuyetToanQuyNay float;

	select 
		@FTongTien_DuToanNamTruocChuyenSang = SUM(fTien_DuToanNamTruocChuyenSang) ,
		@FTongTien_DuToanGiaoNamNay= SUM(fTien_DuToanGiaoNamNay),
		@FTongTien_TongDuToanDuocGiao = SUM(fTien_TongDuToanDuocGiao),
		@FTongTienThucChi = SUM(fTienThucChi),
		@FTongTienQuyetToanDaDuyet = SUM(fTienQuyetToanDaDuyet),
		@FTongTienDeNghiQuyetToanQuyNay = SUM(fTienDeNghiQuyetToanQuyNay),
		@FTongTienXacNhanQuyetToanQuyNay = SUM(fTienXacNhanQuyetToanQuyNay)

	FROM BH_QTC_Quy_KCB_ChiTiet where iID_QTC_Quy_KCB = @VoucherId;


	update BH_QTC_Quy_KCB 
	set fTongTien_DuToanNamTruocChuyenSang = @FTongTien_DuToanNamTruocChuyenSang, 
		fTongTien_DuToanGiaoNamNay = @FTongTien_DuToanGiaoNamNay, 
		fTongTien_TongDuToanDuocGiao = @FTongTien_TongDuToanDuocGiao,
		fTongTienThucChi = @FTongTienThucChi,
		fTongTienQuyetToanDaDuyet = @FTongTienQuyetToanDaDuyet,
		fTongTienDeNghiQuyetToanQuyNay =  @FTongTienDeNghiQuyetToanQuyNay,
		fTongTienXacNhanQuyetToanQuyNay = @FTongTienXacNhanQuyetToanQuyNay,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Quy_KCB = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]    Script Date: 10/16/2023 2:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_kcb]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@SLN nvarchar(max),
@IsTongHop bit,
@IQuy int,
@Donvitinh int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs  in (select * from dbo.splitstring(@SLN)) 
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			Sum(qtcn_ct.fTien_DuToanNamTruocChuyenSang) as fTien_DuToanNamTruocChuyenSang,
			Sum(qtcn_ct.fTien_DuToanGiaoNamNay) as fTien_DuToanGiaoNamNay,
			Sum(qtcn_ct.fTien_TongDuToanDuocGiao) as fTien_TongDuToanDuocGiao,
			sum(qtcn_ct.fTienThucChi) as fTienThucChi ,
			sum(qtcn_ct.fTienQuyetToanDaDuyet) as fTienQuyetToanDaDuyet ,
			sum(qtcn_ct.fTienDeNghiQuyetToanQuyNay) as fTienDeNghiQuyetToanQuyNay,
			sum(qtcn_ct.fTienXacNhanQuyetToanQuyNay) as fTienXacNhanQuyetToanQuyNay
		into tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sNoiDung

		
	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang,
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay
		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]    Script Date: 10/16/2023 2:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquyKCB_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int
as
begin
	---Lấy danh sách mục lục ngân sách
		select 
			danhmuc.iID_MLNS as iID_MLNS,
			danhmuc.iID_MLNS_Cha,
			danhmuc.sLNS,
			danhmuc.sL,
			danhmuc.sK,
			danhmuc.sM,
			danhmuc.sTM,
			danhmuc.sTTM,
			danhmuc.sNG,
			danhmuc.sTNG,
			danhmuc.sXauNoiMa,
			danhmuc.sMoTa,
			danhmuc.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9030001', '9030002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_KCB_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sNoiDung,
			qtcn_ct.fTien_DuToanNamTruocChuyenSang,
			qtcn_ct.fTien_DuToanGiaoNamNay,
			qtcn_ct.fTien_TongDuToanDuocGiao,
			qtcn_ct.fTienThucChi,
			qtcn_ct.fTienQuyetToanDaDuyet,
			qtcn_ct.fTienDeNghiQuyetToanQuyNay,
			qtcn_ct.fTienXacNhanQuyetToanQuyNay
		into tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_KCB_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_KCB  as qtcn on qtcn_ct.iID_QTC_Quy_KCB = qtcn.ID_QTC_Quy_KCB
		where qtcn_ct.iID_QTC_Quy_KCB = @IdChungTu;

		
	---Kết quả hiển thị trả về
		select 
			mucluc.iID_MLNS,
			mucluc.iID_MLNS_Cha,
			mucluc.sLNS,
			mucluc.sL,
			mucluc.sK,
			mucluc.sM,
			mucluc.sTM,
			mucluc.sTTM,
			mucluc.sNG,
			mucluc.sTNG,
			mucluc.sXauNoiMa,
			mucluc.bHangCha,
			chi_tiet.ID_QTC_Quy_KCB_ChiTiet,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sNoiDung,
			chi_tiet.fTien_DuToanNamTruocChuyenSang,
			chi_tiet.fTien_DuToanGiaoNamNay,
			chi_tiet.fTien_TongDuToanDuocGiao,
			chi_tiet.fTienThucChi,
			chi_tiet.fTienQuyetToanDaDuyet,
			chi_tiet.fTienDeNghiQuyetToanQuyNay,
			chi_tiet.fTienXacNhanQuyetToanQuyNay

		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]    Script Date: 10/16/2023 2:27:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquyKCB_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtq.ID_QTC_Quy_KCB,
		qtq.iID_DonVi,
		qtq.iID_MaDonVi,
		qtq.sSoChungTu,
		qtq.dNgayChungTu,
		qtq.sSoQuyetDinh,
		qtq.dNgayQuyetDinh,
		qtq.iQuyChungTu,
		qtq.iNamChungTu,
		qtq.sMoTa,
		qtq.dNgaySua,
		qtq.dNgayTao,
		qtq.sNguoiSua,
		qtq.sNguoiTao,
		qtq.sTongHop,
		qtq.iID_TongHopID,
		qtq.iLoaiTongHop,
		qtq.bIsKhoa,
		qtq.fTongTien_DuToanNamTruocChuyenSang,
		qtq.fTongTien_DuToanGiaoNamNay,
		qtq.fTongTien_TongDuToanDuocGiao,
		qtq.fTongTienThucChi,
		qtq.fTongTienQuyetToanDaDuyet,
		qtq.fTongTienDeNghiQuyetToanQuyNay,
		qtq.fTongTienXacNhanQuyetToanQuyNay,
		dv.sTenDonVi,
		qtq.bDaTongHop,
		qtq.sDSSoChungTuTongHop
	from BH_QTC_Quy_KCB as qtq
	left join DonVi as dv on qtq.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtq.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
