/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_update_total]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnBHXH_update_total]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnBHXH_update_total]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_qtcnBHXH_create_data_summary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_qtcnBHXH_create_data_summary]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 9/21/2023 4:27:45 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 9/21/2023 4:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50)
as
begin
	insert into BH_QTC_Nam_CheDoBHXH_ChiTiet (	
			ID_QTC_Nam_CheDoBHXH_ChiTiet,
			iID_QTC_Nam_CheDoBHXH, 
			iID_MucLucNganSach, 
			sLoaiTroCap, 
			dNgaySua,
			dNgayTao,
			sNguoiSua,
			sNguoiTao,
			fTienDuToanDuyet,
			--iTongSo_ThucChi,
			fTongTien_ThucChi,
			iSoSQ_ThucChi,
			fTienSQ_ThucChi,
			iSoQNCN_ThucChi,
			fTienQNCN_ThucChi,
			iSoCNVCQP_ThucChi,
			fTienCNVCQP_ThucChi,
			iSoHSQBS_ThucChi,
			fTienHSQBS_ThucChi,
			--fTienThua,
			--fTienThieu,
			--fTiLeThucHienTrenDuToan,
			iSoLDHD_ThucChi,
			fTienLDHD_ThucChi
			)
	select 
			NEWID(),
			@IdChungTu,
			tb_qtcy.iID_MucLucNganSach,
			tb_qtcy.sLoaiTroCap,
			null,
			null,
			null,
			@User,
			tb_qtcy.fTienDuToanDuyet,
			tb_qtcy.fTongTien_ThucChi,
			tb_qtcy.iSoSQ_ThucChi,
			tb_qtcy.fTienSQ_ThucChi,
			tb_qtcy.iSoQNCN_ThucChi,
			tb_qtcy.fTienQNCN_ThucChi,
			tb_qtcy.iSoCNVCQP_ThucChi,
			tb_qtcy.fTienCNVCQP_ThucChi,
			tb_qtcy.iSoHSQBS_ThucChi,
			tb_qtcy.fTienHSQBS_ThucChi,
			tb_qtcy.iSoLDHD_ThucChi,
			tb_qtcy.fTienLDHD_ThucChi

	from 
	(
		select 
				ctqt_quy_chitiet.iID_MucLucNganSach, 
				ctqt_quy_chitiet.sLoaiTroCap,
				SUM(ctqt_quy_chitiet.fTienDuToanDuyet) as fTienDuToanDuyet,
				SUM(ctqt_quy_chitiet.fTienLuyKeCuoiQuyNay) as fTongTien_ThucChi,
				SUM(ctqt_quy_chitiet.iSoSQ_DeNghi) as iSoSQ_ThucChi,
				SUM(ctqt_quy_chitiet.fTienSQ_DeNghi) as fTienSQ_ThucChi,
				SUM(ctqt_quy_chitiet.iSoQNCN_DeNghi) as iSoQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.fTienQNCN_DeNghi) as fTienQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.iSoCNVCQP_DeNghi) as iSoCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.fTienCNVCQP_DeNghi) as fTienCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.iSoHSQBS_DeNghi) as iSoHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.fTienHSQBS_DeNghi) as fTienHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.iTongSo_DeNghi) as iSoLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.fTongTien_DeNghi) as fTienLDHD_ThucChi

		from BH_QTC_Quy_CheDoBHXH as ctqt_quy
		inner join BH_QTC_Quy_CheDoBHXH_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_CheDoBHXH = ctqt_quy_chitiet.iID_QTC_Quy_CheDoBHXH
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
		group by ctqt_quy_chitiet.iID_MucLucNganSach, ctqt_quy_chitiet.sLoaiTroCap) as tb_qtcy

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_create_data_summary]    Script Date: 9/21/2023 4:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bh_qtcnBHXH_create_data_summary]
@IdChungTu nvarchar(MAX),
@NguoiTao nvarchar(500),
@YearOfWork int,
@IdChungTuSummary nvarchar(MAX)
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO BH_QTC_Nam_CheDoBHXH_ChiTiet(ID_QTC_Nam_CheDoBHXH_ChiTiet, iID_QTC_Nam_CheDoBHXH, iID_MucLucNganSach, sLoaiTroCap, dNgaySua, dNgayTao, sNguoiSua, sNguoiTao, fTienDuToanDuyet, iTongSo_ThucChi,
	fTongTien_ThucChi, iSoSQ_ThucChi, fTienSQ_ThucChi, iSoQNCN_ThucChi, fTienQNCN_ThucChi, iSoCNVCQP_ThucChi, fTienCNVCQP_ThucChi, iSoHSQBS_ThucChi, fTienHSQBS_ThucChi, fTienThua,
	fTienThieu,fTiLeThucHienTrenDuToan, iSoLDHD_ThucChi, fTienLDHD_ThucChi )
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
	   SUM(iTongSo_ThucChi),
	   SUM(fTongTien_ThucChi),
	   SUM(iSoSQ_ThucChi),
	   SUM(fTienSQ_ThucChi),
	   SUM(iSoQNCN_ThucChi),
	   SUM(fTienQNCN_ThucChi),
	   SUM(iSoCNVCQP_ThucChi),
	   SUM(fTienCNVCQP_ThucChi),
	   SUM(iSoHSQBS_ThucChi),
	   SUM(fTienHSQBS_ThucChi),
	   SUM(fTienThua),
	   SUM(fTienThieu),
	   SUM(fTiLeThucHienTrenDuToan),
	   SUM(iSoLDHD_ThucChi),
	   SUM(fTienLDHD_ThucChi)
FROM BH_QTC_Nam_CheDoBHXH_ChiTiet
WHERE  iID_QTC_Nam_CheDoBHXH IN
    (SELECT *
     FROM f_split(@IdChungTu))
group by iID_MucLucNganSach, sLoaiTroCap

UPDATE BH_QTC_Nam_CheDoBHXH SET bDaTongHop = 1 WHERE ID_QTC_Nam_CheDoBHXH IN (SELECT * FROM f_split(@IdChungTu));
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_qtcnBHXH_update_total]    Script Date: 9/21/2023 4:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[sp_bh_qtcnBHXH_update_total] 
	@VoucherId nvarchar(255),
	@UserModify nvarchar(100)
AS
BEGIN
	declare @FTongTienDuToanDuyet float;

	declare @ITongSoLuyKeCuoiQuyNay float;
	declare @FTongTienLuyKeCuoiQuyNay float;

	declare @ITongSoSQDeNghi float;
	declare @FTongTienSQDeNghi float;

	declare @ITongSoQNCNDeNghi float;
	declare @FTongTienQNCNDeNghi float;

	declare @ITongSoCNVCQPDeNghi float;
	declare @FTongTienCNVCQPDeNghi float;

	declare @ITongSoDeNghi float;
	declare @FTongTienDeNghi float;

	declare @ITongSoHSQBSDeNghi float;
	declare @FTongTienHSQBSDeNghi float;


	select 
		@FTongTienDuToanDuyet = SUM(fTienDuToanDuyet) ,

		@ITongSoLuyKeCuoiQuyNay= SUM(iTongSo_ThucChi),
		@FTongTienLuyKeCuoiQuyNay = SUM(fTongTien_ThucChi),

		@ITongSoSQDeNghi = SUM(iSoSQ_ThucChi) ,
		@FTongTienSQDeNghi = SUM(fTienSQ_ThucChi) ,

		@ITongSoQNCNDeNghi = SUM(iSoQNCN_ThucChi) ,
		@FTongTienQNCNDeNghi = SUM(fTienQNCN_ThucChi) ,

		@ITongSoCNVCQPDeNghi = SUM(iSoCNVCQP_ThucChi) ,
		@FTongTienCNVCQPDeNghi = SUM(fTienCNVCQP_ThucChi) ,

		@ITongSoDeNghi = SUM(iSoLDHD_ThucChi) ,
		@FTongTienDeNghi = SUM(fTienLDHD_ThucChi) ,

		@ITongSoHSQBSDeNghi = SUM(iSoHSQBS_ThucChi) ,
		@FTongTienHSQBSDeNghi = SUM(fTienHSQBS_ThucChi) 

	FROM BH_QTC_Nam_CheDoBHXH_ChiTiet where iID_QTC_Nam_CheDoBHXH = @VoucherId;


	update BH_QTC_Nam_CheDoBHXH 
	set fTongTien_DuToanDuyet = @FTongTienDuToanDuyet, 
		iTongSo_LuyKeCuoiQuyNay = @ITongSoLuyKeCuoiQuyNay, 
		fTongTien_LuyKeCuoiQuyNay = @FTongTienLuyKeCuoiQuyNay,
		iTongSoSQ_DeNghi = @ITongSoSQDeNghi,
		fTongTienSQ_DeNghi = @FTongTienSQDeNghi,
		iTongSoQNCN_DeNghi =  @ITongSoCNVCQPDeNghi,
		fTongTienQNCN_DeNghi = @FTongTienQNCNDeNghi,
		iTongSoCNVCQP_DeNghi = @ITongSoCNVCQPDeNghi,
		fTongTienCNVCQP_DeNghi = @FTongTienCNVCQPDeNghi,
		iTongSoHSQBS_DeNghi = @ITongSoHSQBSDeNghi,
		fTongTienHSQBS_DeNghi = @FTongTienHSQBSDeNghi,
		iTongSo_DeNghi = @ITongSoDeNghi,
		fTongTien_DeNghi = @FTongTienDeNghi,
		dNgaySua = GETDATE(), 
		sNguoiSua = @UserModify  
	where ID_QTC_Nam_CheDoBHXH = @VoucherId; 
END
;
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]    Script Date: 9/21/2023 4:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchicacchedo_bhxh]
@INamLamViec int,
@IdMaDonVi nvarchar(max),
@Lns nvarchar(max),
@Donvitinh int,
@IsTongHop int
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


	---Lấy danh sách chi tiết
		select	
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoDuToanDuocDuyet) as iSoDuToanDuocDuyet,
			Sum(qtcn_ct.fTongTien_ThucChi) as fTongTien_ThucChi,
			Sum(qtcn_ct.iTongSo_ThucChi) as iTongSo_ThucChi,
			Sum(qtcn_ct.iSoSQ_ThucChi) as iSoSQ_ThucChi,
			Sum(qtcn_ct.fTienSQ_ThucChi) as fTienSQ_ThucChi,
			Sum(qtcn_ct.iSoQNCN_ThucChi) as iSoQNCN_ThucChi,
			Sum(qtcn_ct.fTienQNCN_ThucChi) as fTienQNCN_ThucChi,
			Sum(qtcn_ct.iSoCNVCQP_ThucChi) as iSoCNVCQP_ThucChi,
			Sum(qtcn_ct.fTienCNVCQP_ThucChi) as fTienCNVCQP_ThucChi,
			Sum(qtcn_ct.iSoLDHD_ThucChi) as iSoLDHD_ThucChi,
			Sum(qtcn_ct.fTienLDHD_ThucChi) as fTienLDHD_ThucChi,
			Sum(qtcn_ct.iSoHSQBS_ThucChi) as iSoHSQBS_ThucChi,
			Sum(qtcn_ct.fTienHSQBS_ThucChi) as fTienHSQBS_ThucChi,
			Sum(qtcn_ct.fTienDuToanDuyet) - Sum(qtcn_ct.fTongTien_ThucChi) as fTienThua,
			Sum(qtcn_ct.fTongTien_ThucChi) - Sum(qtcn_ct.fTienDuToanDuyet) as fTienThieu
			
		into tbl_qtcn_chitiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as  qtcn_ct
		inner join  BH_QTC_Nam_CheDoBHXH as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		group by qtcn_ct.iID_MucLucNganSach,qtcn_ct.sLoaiTroCap



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
			mucluc.sMoTa as sLoaiTroCap,
			chi_tiet.fTienDuToanDuyet, 
			chi_tiet.fTongTien_ThucChi,
			chi_tiet.iTongSo_ThucChi,
			chi_tiet.iSoSQ_ThucChi,
			chi_tiet.fTienSQ_ThucChi,
			chi_tiet.iSoQNCN_ThucChi,
			chi_tiet.fTienQNCN_ThucChi,
			chi_tiet.iSoCNVCQP_ThucChi,
			chi_tiet.fTienCNVCQP_ThucChi,
			chi_tiet.iSoLDHD_ThucChi,
			chi_tiet.fTienLDHD_ThucChi,
			chi_tiet.iSoHSQBS_ThucChi,
			chi_tiet.fTienHSQBS_ThucChi
		from tblMucLucNganSach as mucluc
		left join tbl_qtcn_chitiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa


		drop table tblMucLucNganSach;
		drop table tbl_qtcn_chitiet;

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 9/21/2023 4:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
@IdChungTu uniqueidentifier,
@INamLamViec int,
@IsTongHop4Quy bit
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
			qtcn_ct.ID_QTC_Nam_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,

			qtcn_ct.fTienDuToanDuyet, ---3
			qtcn_ct.iSoDuToanDuocDuyet, --2

			isnull(qtcn_ct.iSoSQ_ThucChi,0) + isnull(qtcn_ct.iSoQNCN_ThucChi,0) + isnull(qtcn_ct.iSoCNVCQP_ThucChi,0) ---4
			+ isnull(qtcn_ct.iSoLDHD_ThucChi,0) + isnull(qtcn_ct.iSoHSQBS_ThucChi,0) as iTongSo_ThucChi,
			isnull(qtcn_ct.fTienSQ_ThucChi,0) + isnull(qtcn_ct.fTienQNCN_ThucChi,0) + isnull(qtcn_ct.fTienCNVCQP_ThucChi,0) 
			+ isnull(qtcn_ct.fTienLDHD_ThucChi,0) + isnull(qtcn_ct.fTienHSQBS_ThucChi,0) as fTongTien_ThucChi, ---5

			qtcn_ct.iSoSQ_ThucChi, ---6
			qtcn_ct.fTienSQ_ThucChi, ---7

			qtcn_ct.iSoQNCN_ThucChi, ----8
			qtcn_ct.fTienQNCN_ThucChi,---9

			qtcn_ct.iSoCNVCQP_ThucChi,---10
			qtcn_ct.fTienCNVCQP_ThucChi, ----11

			qtcn_ct.iSoLDHD_ThucChi, ----13
			qtcn_ct.fTienLDHD_ThucChi, ---14

			qtcn_ct.iSoHSQBS_ThucChi, ----15
			qtcn_ct.fTienHSQBS_ThucChi, ---16

			isnull(qtcn_ct.fTienDuToanDuyet,0) - isnull(qtcn_ct.iSoSQ_ThucChi,0) + isnull(qtcn_ct.iSoQNCN_ThucChi,0) + isnull(qtcn_ct.iSoCNVCQP_ThucChi,0) ---17
			+ isnull(qtcn_ct.iSoLDHD_ThucChi,0) + isnull(qtcn_ct.iSoHSQBS_ThucChi,0)  as fTienThua,
			isnull(qtcn_ct.iSoSQ_ThucChi,0) + isnull(qtcn_ct.iSoQNCN_ThucChi,0) + isnull(qtcn_ct.iSoCNVCQP_ThucChi,0) --18
			+ isnull(qtcn_ct.iSoLDHD_ThucChi,0) + isnull(qtcn_ct.iSoHSQBS_ThucChi,0) - isnull(qtcn_ct.fTienDuToanDuyet,0) as  fTienThieu

		into tblQuyetToanNamChiTiet
		from BH_QTC_Nam_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Nam_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Nam_CheDoBHXH = qtcn.ID_QTC_Nam_CheDoBHXH
		where qtcn_ct.iID_QTC_Nam_CheDoBHXH = @IdChungTu;

		
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
		chi_tiet.ID_QTC_Nam_CheDoBHXH_ChiTiet,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,

		chi_tiet.fTienDuToanDuyet, 
		chi_tiet.iSoDuToanDuocDuyet,

		chi_tiet.iTongSo_ThucChi, 
		chi_tiet.fTongTien_ThucChi,

		chi_tiet.iSoSQ_ThucChi,
		chi_tiet.fTienSQ_ThucChi,

		chi_tiet.iSoQNCN_ThucChi,
		chi_tiet.fTienQNCN_ThucChi,

		chi_tiet.iSoCNVCQP_ThucChi,
		chi_tiet.fTienCNVCQP_ThucChi,

		chi_tiet.iSoLDHD_ThucChi,
		chi_tiet.fTienLDHD_ThucChi,

		chi_tiet.iSoHSQBS_ThucChi,
		chi_tiet.fTienHSQBS_ThucChi,

		chi_tiet.fTienThua,
		chi_tiet.fTienThieu
	from tblMucLucNganSach as mucluc
	left join tblQuyetToanNamChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanNamChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 9/21/2023 4:27:45 PM ******/
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
