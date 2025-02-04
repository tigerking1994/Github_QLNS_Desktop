/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_update_total]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 9/28/2023 1:48:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcqBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_create_data_summary]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Quy_CheDoBHXH_ChiTiet(
		ID_QTC_Quy_CheDoBHXH_ChiTiet,
		iID_QTC_Quy_CheDoBHXH,
		iID_MucLucNganSach,
		sLoaiTroCap,
		dNgaySua,
		dNgayTao,
		sNguoiSua,
		sNguoiTao,
		fTienDuToanDuyet,
		iSoLuyKeCuoiQuyNay,
		fTienLuyKeCuoiQuyNay,
		iSoSQ_DeNghi,
		fTienSQ_DeNghi,
		iSoQNCN_DeNghi,
		fTienQNCN_DeNghi,
		iSoCNVCQP_DeNghi,
		fTienCNVCQP_DeNghi,
		iSoHSQBS_DeNghi,
		fTienHSQBS_DeNghi,
		iSoLDHD_DeNghi,
		fTienLDHD_DeNghi,
		iTongSo_DeNghi,
		fTongTien_DeNghi,
		fTongTien_PheDuyet,
		iNamChungTu
	)
SELECT 
	   NEWID(),
	   @IdChungTuSummary,
       iID_MucLucNganSach,
       sLoaiTroCap,
	   GETDATE(),
	   GETDATE(),
	   @NguoiTao,
	   '',
	   SUM(fTienDuToanDuyet),
	   SUM(iSoLuyKeCuoiQuyNay),
	   SUM(fTienLuyKeCuoiQuyNay),
	   SUM(iSoSQ_DeNghi),
	   SUM(fTienSQ_DeNghi),
	   SUM(iSoQNCN_DeNghi),
	   SUM(fTienQNCN_DeNghi),
	   SUM(iSoCNVCQP_DeNghi),
	   SUM(fTienCNVCQP_DeNghi),
	   SUM(iSoHSQBS_DeNghi),
	   SUM(fTienHSQBS_DeNghi),
	   SUM(iSoLDHD_DeNghi),
	   SUM(fTienLDHD_DeNghi),
	   SUM(iTongSo_DeNghi),
	   SUM(fTongTien_DeNghi),
	   SUM(fTongTien_PheDuyet),
	   @YearOfWork

FROM BH_QTC_Quy_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Quy_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap

UPDATE BH_QTC_Quy_CheDoBHXH SET bDaTongHop = 1 WHERE ID_QTC_Quy_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcqBHXH_update_total]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_qtcqBHXH_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTienDuToanDuyet float;
	declare @ISoLuyKeCuoiQuyNay int;
	declare @FTienLuyKeCuoiQuyNay float;
	declare @ISoSQ_DeNghi int;
	declare @FTienSQ_DeNghi float;
	declare @ISoQNCN_DeNghi int;
	declare @FTienQNCN_DeNghi float;
	declare @ISoCNVCQP_DeNghi int;
	declare @FTienCNVCQP_DeNghi float;
	declare @ISoHSQBS_DeNghi int;
	declare @FTienHSQBS_DeNghi float;
	declare @ISoLDHD_DeNghi int;
	declare @FTienLDHD_DeNghi float;
	declare @ITongSo_DeNghi int;
	declare @FTongTien_DeNghi float;
	declare @FTongTien_PheDuyet float;

	select 
		@FTienDuToanDuyet = SUM(fTienDuToanDuyet) ,
		@ISoLuyKeCuoiQuyNay= SUM(iSoLuyKeCuoiQuyNay),
		@FTienLuyKeCuoiQuyNay = SUM(fTienLuyKeCuoiQuyNay),
		@ISoSQ_DeNghi = SUM(iSoSQ_DeNghi),
		@FTienSQ_DeNghi = SUM(fTienSQ_DeNghi),
		@ISoQNCN_DeNghi = SUM(iSoQNCN_DeNghi),
		@FTienQNCN_DeNghi = SUM(fTienQNCN_DeNghi),
		@ISoCNVCQP_DeNghi = SUM(iSoCNVCQP_DeNghi),
		@FTienCNVCQP_DeNghi = SUM(fTienCNVCQP_DeNghi),
		@ISoHSQBS_DeNghi = SUM(iSoHSQBS_DeNghi),
		@FTienHSQBS_DeNghi = SUM(fTienHSQBS_DeNghi),

		@ISoLDHD_DeNghi = SUM(iSoLDHD_DeNghi),
		@FTienLDHD_DeNghi = SUM(fTienLDHD_DeNghi),

		@ITongSo_DeNghi = SUM(iTongSo_DeNghi),
		@FTongTien_DeNghi = SUM(fTongTien_DeNghi),
		@FTongTien_PheDuyet = SUM(fTongTien_PheDuyet)

	FROM BH_QTC_Quy_CheDoBHXH_ChiTiet where iID_QTC_Quy_CheDoBHXH = @VoucherId;


	update BH_QTC_Quy_CheDoBHXH 
	set fTongTien_DuToanDuyet = @FTienDuToanDuyet, 
		iTongSo_LuyKeCuoiQuyNay = @ISoLuyKeCuoiQuyNay, 
		fTongTien_LuyKeCuoiQuyNay = @FTienLuyKeCuoiQuyNay,
		iTongSoSQ_DeNghi = @ISoSQ_DeNghi,
		fTongTienSQ_DeNghi = @FTienSQ_DeNghi,
		iTongSoQNCN_DeNghi =  @ISoQNCN_DeNghi,
		fTongTienQNCN_DeNghi = @FTienQNCN_DeNghi,
		iTongSoCNVCQP_DeNghi = @ISoCNVCQP_DeNghi,
		fTongTienCNVCQP_DeNghi = @FTienCNVCQP_DeNghi,
		iTongSoHSQBS_DeNghi = @ISoHSQBS_DeNghi,
		fTongTienHSQBS_DeNghi = @FTienHSQBS_DeNghi,

		iTongSoLDHD_DeNghi = @ISoLDHD_DeNghi,
		fTongTienLDHD_DeNghi = @FTienLDHD_DeNghi,

		iTongSo_DeNghi = @ITongSo_DeNghi,
		fTongTien_DeNghi = @FTongTien_DeNghi,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Quy_CheDoBHXH = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9020002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			qtcn_ct.fTienDuToanDuyet,
			qtcn_ct.iSoLuyKeCuoiQuyNay,
			qtcn_ct.fTienLuyKeCuoiQuyNay,
			qtcn_ct.iSoSQ_DeNghi,
			qtcn_ct.fTienSQ_DeNghi,
			qtcn_ct.iSoQNCN_DeNghi,
			qtcn_ct.fTienQNCN_DeNghi,
			qtcn_ct.iSoCNVCQP_DeNghi,
			qtcn_ct.fTienCNVCQP_DeNghi,
			qtcn_ct.iSoHSQBS_DeNghi,
			qtcn_ct.fTienHSQBS_DeNghi,
			qtcn_ct.iTongSo_DeNghi,
			qtcn_ct.fTongTien_DeNghi,
			qtcn_ct.fTongTien_PheDuyet,
			qtcn_ct.iSoLDHD_DeNghi,
			qtcn_ct.fTienLDHD_DeNghi
		into tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu;

		
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
			chi_tiet.ID_QTC_Quy_CheDoBHXH_ChiTiet,
			mucluc.iID_MLNS as iID_MucLucNganSach,
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet,
			chi_tiet.iSoLuyKeCuoiQuyNay,
			chi_tiet.fTienLuyKeCuoiQuyNay,
			chi_tiet.iSoSQ_DeNghi,
			chi_tiet.fTienSQ_DeNghi,
			chi_tiet.iSoQNCN_DeNghi,
			chi_tiet.fTienQNCN_DeNghi,
			chi_tiet.iSoCNVCQP_DeNghi,
			chi_tiet.fTienCNVCQP_DeNghi,
			chi_tiet.iSoHSQBS_DeNghi,
			chi_tiet.fTienHSQBS_DeNghi,
			chi_tiet.iTongSo_DeNghi,
			chi_tiet.fTongTien_DeNghi,
			chi_tiet.fTongTien_PheDuyet

		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtn.ID_QTC_Nam_CheDoBHXH,
		qtn.iID_DonVi,
		qtn.iID_MaDonVi,
		qtn.sSoChungTu,
		qtn.dNgayChungTu,
		qtn.bThucChiTheo4Quy,
		qtn.iNamChungTu,
		qtn.sSoQuyetDinh,
		qtn.dNgayQuyetDinh,
		qtn.sMoTa,
		qtn.bIsKhoa,
		qtn.iLoaiTongHop,
		qtn.sTongHop,
		qtn.fTongTien_DuToanDuyet,
		qtn.iTongSo_LuyKeCuoiQuyNay,
		qtn.fTongTien_LuyKeCuoiQuyNay,
		qtn.iTongSoSQ_DeNghi,
		qtn.fTongTienSQ_DeNghi,
		qtn.iTongSoQNCN_DeNghi,
		qtn.fTongTienQNCN_DeNghi,
		qtn.iTongSoCNVCQP_DeNghi,
		qtn.fTongTienCNVCQP_DeNghi,
		qtn.iTongSoHSQBS_DeNghi,
		qtn.fTongTienHSQBS_DeNghi,
		qtn.iTongSo_DeNghi,
		qtn.fTongTien_DeNghi,
		qtn.fTongTien_PheDuyet,
		isnull(qtn.fTongTien_DuToanDuyet,0) - isnull(qtn.fTongTien_LuyKeCuoiQuyNay,0) as fTongTienThua,
		isnull(qtn.fTongTien_LuyKeCuoiQuyNay,0) - isnull(qtn.fTongTien_DuToanDuyet,0) as fTongTienThieu,
		qtn.bDaTongHop,
		qtn.sDSSoChungTuTongHop,
		dv.sTenDonVi,
		qtn.sNguoiTao
	from BH_QTC_Nam_CheDoBHXH as qtn
	left join DonVi as dv on qtn.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtn.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_danhsachquyettoanquy_index]
@INamLamViec int,
@Search nvarchar(max)
As
Begin
	select 
		qtq.ID_QTC_Quy_CheDoBHXH,
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
		qtq.fTongTien_DuToanDuyet,
		qtq.iTongSo_LuyKeCuoiQuyNay,
		qtq.fTongTien_LuyKeCuoiQuyNay,
		qtq.iTongSoSQ_DeNghi,
		qtq.fTongTienSQ_DeNghi,
		qtq.iTongSoQNCN_DeNghi,
		qtq.fTongTienQNCN_DeNghi,
		qtq.iTongSoCNVCQP_DeNghi,
		qtq.fTongTienCNVCQP_DeNghi,
		qtq.iTongSoHSQBS_DeNghi,
		qtq.fTongTienHSQBS_DeNghi,
		qtq.iTongSo_DeNghi,
		qtq.fTongTien_DeNghi,
		qtq.fTongTien_PheDuyet,
		dv.sTenDonVi,
		qtq.bDaTongHop,
		qtq.sDSSoChungTuTongHop
	from BH_QTC_Quy_CheDoBHXH as qtq
	left join DonVi as dv on qtq.iID_DonVi = dv.iID_DonVi
	where dv.iNamLamViec = @INamLamViec
	and (@Search is null or ( @Search is not null and  qtq.sSoChungTu like N'%'+@Search+ '%'))

	

End
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 9/28/2023 1:48:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdDonVi NVARCHAR(MAX),
@DonViTinh int,
@LNS NVARCHAR(MAX),
@IsTongHop int
as
begin
	--- Lấy danh sách các đơn vi được chọn
	select 
		ROW_NUMBER() OVER(PARTITION BY DonVi.iKhoi  ORDER BY DonVi.iID_MaDonVi ASC) AS sTT,
		DonVi.iID_DonVi,
		DonVi.iID_MaDonVi,
		DonVi.sTenDonVi,
		DonVi.iKhoi
	into  tblDonVi
	from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  IN (SELECT * FROM f_split(@IdDonVi)) ;


	--- Lấy danh sách mục lục ngân sách
	select 
		BH_DM_MucLucNganSach.iID_MLNS as iID_MLNS,
		BH_DM_MucLucNganSach.iID_MLNS_Cha,
		BH_DM_MucLucNganSach.sLNS,
		BH_DM_MucLucNganSach.sL,
		BH_DM_MucLucNganSach.sK,
		BH_DM_MucLucNganSach.sM,
		BH_DM_MucLucNganSach.sTM,
		BH_DM_MucLucNganSach.sTTM,
		BH_DM_MucLucNganSach.sNG,
		BH_DM_MucLucNganSach.sTNG,
		BH_DM_MucLucNganSach.sXauNoiMa,
		BH_DM_MucLucNganSach.sMoTa as sNoiDung,
		BH_DM_MucLucNganSach.bHangCha
		into tblMucLucNganSach
		from BH_DM_MucLucNganSach 
		where   iNamLamViec = @INamLamViec  and (sLNS  in ('9010001', '9020002'))


	--- hiển thị mục lục ngân sách theo đơn vị
	select  
		case when tblMucLucNganSach.sLNS = '9010001' then N'     Khối dự toán' else N'     Khối hạch toán' end sTenDonVi,
		tblMucLucNganSach.sLNS,
		tblDonVi.iID_DonVi,
		tblDonVi.iID_MaDonVi,
		tblDonVi.iKhoi
	into donvi_MLNS
	from tblMucLucNganSach cross join tblDonVi 
	where tblMucLucNganSach.iID_MLNS_Cha is null

	---Lấy thông tin quyết toán chi tiết 
	select 
		tbl_qtc.iKhoi,
		tbl_qtc.iID_MaDonVi,
		tbl_qtc.sLNS,
		tbl_qtc.sL,
		tbl_qtc.sK,
		tbl_qtc.sM,
		case when tbl_qtc.sM = 1 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapOmDau,
		case when tbl_qtc.sM = 2 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThaiSan,
		case when tbl_qtc.sM = 3 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTaiNanNN,
		case when tbl_qtc.sM = 4 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapHuuTri,
		case when tbl_qtc.sM = 5 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapPhucVien,
		case when tbl_qtc.sM = 6 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapXuatNgu,
		case when tbl_qtc.sM = 7 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapThoiViec,
		case when tbl_qtc.sM = 8 then tbl_qtc.fTienDuToanDuyet else 0 end fTroCapTuTuat
	into tbl_qtcn_chitiet
	from
	(
		select 
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			tblDonVi.iKhoi,
			tblDonVi.iID_MaDonVi,
			tblMucLucNganSach.sLNS,
			tblMucLucNganSach.sL,
			tblMucLucNganSach.sK,
			tblMucLucNganSach.sM

		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		inner join tblMucLucNganSach on qtcn_ct.iID_MucLucNganSach = tblMucLucNganSach.iID_MLNS
		inner join tblDonVi on qtcn.iID_MaDonVi = tblDonVi.iID_MaDonVi
		where qtcn.iNamChungTu = @INamLamViec 
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by tblDonVi.iKhoi, tblDonVi.iID_MaDonVi, tblMucLucNganSach.sLNS, tblMucLucNganSach.sL,tblMucLucNganSach.sK,tblMucLucNganSach.sM
	) as tbl_qtc


	--- Lấy dữ liệu cấp nhỏ nhất - cấp 4
	select 
		null as STT,
		donvi_MLNS.sTenDonVi,
		donvi_MLNS.iID_MaDonVi,
		donvi_MLNS.iKhoi,
		donvi_MLNS.sLNS,
		sum(tbl_qtcn_chitiet.fTroCapOmDau) as fTroCapOmDau,
		sum(tbl_qtcn_chitiet.fTroCapThaiSan) as fTroCapThaiSan,
		sum(tbl_qtcn_chitiet.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(tbl_qtcn_chitiet.fTroCapHuuTri) as fTroCapHuuTri,
		sum(tbl_qtcn_chitiet.fTroCapPhucVien) as fTroCapPhucVien,
		sum(tbl_qtcn_chitiet.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(tbl_qtcn_chitiet.fTroCapThoiViec) as fTroCapThoiViec,
		sum(tbl_qtcn_chitiet.fTroCapTuTuat) as fTroCapTuTuat,
		4 as level,
		0 as bHangCha
	into tbl_cap4
	from donvi_MLNS 
	left join tbl_qtcn_chitiet on donvi_MLNS.iID_MaDonVi = tbl_qtcn_chitiet.iID_MaDonVi and donvi_MLNS.iKhoi = tbl_qtcn_chitiet.iKhoi
	and tbl_qtcn_chitiet.sLNS = donvi_MLNS.sLNS
	group by donvi_MLNS.sTenDonVi, donvi_MLNS.iID_MaDonVi, donvi_MLNS.iKhoi, donvi_MLNS.sLNS
	order by donvi_MLNS.iKhoi,donvi_MLNS.iID_MaDonVi

	--- Lấy dữ liệu cấp 3
	select 
		tblDonVi.sTT,
		tblDonVi.sTenDonVi ,
		tblDonVi.iID_MaDonVi,
		tblDonVi.iKhoi,
		'' as sLNS, 
		sum(tbl_cap4.fTroCapOmDau) as fTroCapOmDau,
		sum(tbl_cap4.fTroCapThaiSan) as fTroCapThaiSan,
		sum(tbl_cap4.fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(tbl_cap4.fTroCapHuuTri) as fTroCapHuuTri,
		sum(tbl_cap4.fTroCapPhucVien) as fTroCapPhucVien,
		sum(tbl_cap4.fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(tbl_cap4.fTroCapThoiViec) as fTroCapThoiViec,
		sum(tbl_cap4.fTroCapTuTuat) as fTroCapTuTuat,
		3 as level,
		0 as bHangCha
	into tbl_cap3
	from tblDonVi
	left join tbl_cap4 on tblDonVi.iID_MaDonVi = tbl_cap4.iID_MaDonVi and tblDonVi.iKhoi = tbl_cap4.iKhoi
	group by tblDonVi.sTT, tblDonVi.sTenDonVi, tblDonVi.iID_MaDonVi, tblDonVi.iKhoi

	---Lấy dữ liệu đơn vị cấp 2
	select 
		null as STT,
		case when tbl_cap4.sLNS = '9010001' then N'   +Khối dự toán' else N'   +Khối hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		tbl_cap4.iKhoi,
		tbl_cap4.sLNS as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		2 as level,
		1 as bHangCha
	into tbl_cap2
	from tbl_cap4
	group by tbl_cap4.iKhoi, tbl_cap4.sLNS


	---Lấy dữ liệu đơn vị cấp 1
	select 
		null as STT,
		case when tbl_cap4.iKhoi = 2 then N'   A.Đơn vị Dự toán' else N'   B.Đơn vị Hạch toán' end sTenDonVi,
		'' as iID_MaDonVi,
		tbl_cap4.iKhoi,
		'' as sLNS, 
		sum(fTroCapOmDau) as fTroCapOmDau,
		sum(fTroCapThaiSan) as fTroCapThaiSan,
		sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
		sum(fTroCapHuuTri) as fTroCapHuuTri,
		sum(fTroCapPhucVien) as fTroCapPhucVien,
		sum(fTroCapXuatNgu) as fTroCapXuatNgu,
		sum(fTroCapThoiViec) as fTroCapThoiViec,
		sum(fTroCapTuTuat) as fTroCapTuTuat,
		1 as level,
		1 as bHangCha
	into tbl_cap1
	from tbl_cap4
	group by tbl_cap4.iKhoi

	---Hiển thị kết quả trả về
	select 
		sTT,
		sTenDonVi,
		iID_MaDonVi,
		iKhoi,
		sLNS,
		fTroCapOmDau,
		fTroCapThaiSan,
		fTroCapTaiNanNN,
		fTroCapHuuTri,
		fTroCapPhucVien,
		fTroCapXuatNgu,
		fTroCapThoiViec,
		fTroCapTuTuat,
		level,
		bHangCha
	into tblResult
	from
		(
		select * from tbl_cap1
		union all 
		select * from tbl_cap2
		union all 
		select * from tbl_cap3
		union all 
		select * from tbl_cap4) as tblrt
	where isnull(fTroCapOmDau,0) != 0 or isnull(fTroCapThaiSan,0) != 0 or isnull(fTroCapTaiNanNN,0) != 0 or isnull(fTroCapHuuTri,0) != 0 or isnull(fTroCapPhucVien,0) != 0
			or isnull(fTroCapXuatNgu,0) != 0 or isnull(fTroCapThoiViec,0) != 0 or isnull(fTroCapTuTuat,0) != 0
	order by iKhoi desc,iID_MaDonVi,level, sLNS
	
	----insert dòng tổng cộng
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'          Tổng cộng' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					7 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 1
		) as tbltongcong


	---- Insert dòng dự toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Dự toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					8 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and tblResult.sLNS = '9010001'
		) as tbldutoan



	---- Insert dòng Hạch toán
	insert into tblResult (sTenDonVi, iID_MaDonVi, iKhoi, fTroCapOmDau, fTroCapThaiSan, fTroCapTaiNanNN, fTroCapHuuTri, fTroCapPhucVien, fTroCapXuatNgu, fTroCapThoiViec, fTroCapTuTuat,
							level, bHangCha)
	select * from
		(
			select  N'        Khối Hạch toán' as sTenDonVi,
					'' as iID_MaDonVi,
					null as iKhoi,
					sum(fTroCapOmDau) as fTroCapOmDau,
					sum(fTroCapThaiSan) as fTroCapThaiSan,
					sum(fTroCapTaiNanNN) as fTroCapTaiNanNN,
					sum(fTroCapHuuTri) as fTroCapHuuTri,
					sum(fTroCapPhucVien) as fTroCapPhucVien,
					sum(fTroCapXuatNgu) as fTroCapXuatNgu,
					sum(fTroCapThoiViec) as fTroCapThoiViec,
					sum(fTroCapTuTuat) as fTroCapTuTuat,
					9 as level,
					1 as bHangCha
			from tblResult
			where tblResult.level = 4 and  tblResult.sLNS = '9020002'
		) as tbldutoan


	select  * from tblResult order by iKhoi desc,iID_MaDonVi,level, sLNS


	drop table tblDonVi;
	drop table  tblMucLucNganSach;
	drop table donvi_MLNS;
	drop table tbl_qtcn_chitiet;
	drop table tbl_cap4;
	drop table tbl_cap3;
	drop table tbl_cap2;
	drop table tbl_cap1;
	drop table tblResult;
end
GO
