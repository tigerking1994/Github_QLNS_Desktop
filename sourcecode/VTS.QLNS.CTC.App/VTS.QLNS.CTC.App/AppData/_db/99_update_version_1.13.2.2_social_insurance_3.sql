/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 10/2/2023 5:45:47 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]    Script Date: 10/2/2023 5:45:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_create_quyet_toan_chinambhxh_chitiet_theo4quy]
@IdChungTu uniqueidentifier,
@IdMaDonVi nvarchar(50),
@INamLamViec int,
@User  nvarchar(50),
@IsTongHop bit
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
			iSoSQ_ThucChi,
			fTienSQ_ThucChi,
			iSoQNCN_ThucChi,
			fTienQNCN_ThucChi,
			iSoCNVCQP_ThucChi,
			fTienCNVCQP_ThucChi,
			iSoHSQBS_ThucChi,
			fTienHSQBS_ThucChi,
			iSoLDHD_ThucChi,
			fTienLDHD_ThucChi,
			iTongSo_ThucChi,
			fTongTien_ThucChi
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
			tb_qtcy.iSoSQ_ThucChi,
			tb_qtcy.fTienSQ_ThucChi,
			tb_qtcy.iSoQNCN_ThucChi,
			tb_qtcy.fTienQNCN_ThucChi,
			tb_qtcy.iSoCNVCQP_ThucChi,
			tb_qtcy.fTienCNVCQP_ThucChi,
			tb_qtcy.iSoHSQBS_ThucChi,
			tb_qtcy.fTienHSQBS_ThucChi,
			tb_qtcy.iSoLDHD_ThucChi,
			tb_qtcy.fTienLDHD_ThucChi,
			tb_qtcy.iTongSo_ThucChi,
			tb_qtcy.fTongTien_ThucChi

	from 
	(
		select 
				ctqt_quy_chitiet.iID_MucLucNganSach, 
				ctqt_quy_chitiet.sLoaiTroCap,
				SUM(ctqt_quy_chitiet.fTienDuToanDuyet) as fTienDuToanDuyet,
				SUM(ctqt_quy_chitiet.iSoSQ_DeNghi) as iSoSQ_ThucChi,
				SUM(ctqt_quy_chitiet.fTienSQ_DeNghi) as fTienSQ_ThucChi,
				SUM(ctqt_quy_chitiet.iSoQNCN_DeNghi) as iSoQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.fTienQNCN_DeNghi) as fTienQNCN_ThucChi,
				SUM(ctqt_quy_chitiet.iSoCNVCQP_DeNghi) as iSoCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.fTienCNVCQP_DeNghi) as fTienCNVCQP_ThucChi,
				SUM(ctqt_quy_chitiet.iSoHSQBS_DeNghi) as iSoHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.fTienHSQBS_DeNghi) as fTienHSQBS_ThucChi,
				SUM(ctqt_quy_chitiet.iSoLDHD_DeNghi) as iSoLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.fTienLDHD_DeNghi) as fTienLDHD_ThucChi,
				SUM(ctqt_quy_chitiet.iTongSo_DeNghi) as iTongSo_ThucChi,
				SUM(ctqt_quy_chitiet.fTongTien_DeNghi) as fTongTien_ThucChi

		from BH_QTC_Quy_CheDoBHXH as ctqt_quy
		inner join BH_QTC_Quy_CheDoBHXH_ChiTiet as ctqt_quy_chitiet on ctqt_quy.ID_QTC_Quy_CheDoBHXH = ctqt_quy_chitiet.iID_QTC_Quy_CheDoBHXH
		where ctqt_quy.iID_MaDonVi = @IdMaDonVi and ctqt_quy.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and ctqt_quy.bDaTongHop = 0 and ctqt_quy.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  ctqt_quy.sDSSoChungTuTongHop is not null))
		group by ctqt_quy_chitiet.iID_MucLucNganSach, ctqt_quy_chitiet.sLoaiTroCap) as tb_qtcy

end
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_baocaoquyettoanchiquy_bhxh]
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
			qtcn_ct.sLoaiTroCap,
			Sum(qtcn_ct.fTienDuToanDuyet) as fTienDuToanDuyet,
			Sum(qtcn_ct.iSoLuyKeCuoiQuyNay) as iSoLuyKeCuoiQuyNay,
			Sum(qtcn_ct.fTienLuyKeCuoiQuyNay) as fTienLuyKeCuoiQuyNay,
			sum(qtcn_ct.iSoSQ_DeNghi) as iSoSQ_DeNghi ,
			sum(qtcn_ct.fTienSQ_DeNghi) as fTienSQ_DeNghi ,
			sum(qtcn_ct.iSoQNCN_DeNghi) as iSoQNCN_DeNghi,
			sum(qtcn_ct.fTienQNCN_DeNghi) as fTienQNCN_DeNghi ,
			sum(qtcn_ct.iSoCNVCQP_DeNghi) as iSoCNVCQP_DeNghi ,
			sum(qtcn_ct.fTienCNVCQP_DeNghi) as fTienCNVCQP_DeNghi,
			sum(qtcn_ct.iSoHSQBS_DeNghi) as iSoHSQBS_DeNghi,
			sum(qtcn_ct.fTienHSQBS_DeNghi) as fTienHSQBS_DeNghi,
			sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
			sum(qtcn_ct.fTongTien_DeNghi) as fTongTien_DeNghi,
			sum(qtcn_ct.fTongTien_PheDuyet) as fTongTien_PheDuyet,
			sum(qtcn_ct.iSoLDHD_DeNghi) as iSoLDHD_DeNghi,
			sum(qtcn_ct.fTienLDHD_DeNghi) as fTienLDHD_DeNghi
		into tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn.iID_MaDonVi in (select * from dbo.splitstring(@IdMaDonVi)) 
		and qtcn.iNamChungTu = @INamLamViec
		and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
		and qtcn.iQuyChungTu = @IQuy
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
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chinambhxh_chitiet]    Script Date: 10/2/2023 5:45:47 PM ******/
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
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in ('9010001', '9010002')
		
		
		
	---Lấy thông tin chi tiết chứng từ
		select 
			qtcn_ct.ID_QTC_Nam_CheDoBHXH_ChiTiet,
			qtcn_ct.iID_MucLucNganSach,
			qtcn_ct.sLoaiTroCap,

			qtcn_ct.fTienDuToanDuyet, ---3
			qtcn_ct.iSoDuToanDuocDuyet, --2

			qtcn_ct.iTongSo_ThucChi,
			qtcn_ct.fTongTien_ThucChi, ---5

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

			isnull(qtcn_ct.fTienDuToanDuyet,0) - isnull(qtcn_ct.fTongTien_ThucChi,0)  as fTienThua,
			isnull(qtcn_ct.fTongTien_ThucChi,0) - isnull(qtcn_ct.fTienDuToanDuyet,0) as  fTienThieu

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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 10/2/2023 5:45:47 PM ******/
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
			chi_tiet.fTongTien_PheDuyet,
			chi_tiet.iSoLDHD_DeNghi,
			chi_tiet.fTienLDHD_DeNghi

		from tblMucLucNganSach as mucluc
		left join tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table tblMucLucNganSach;
	drop table tblQuyetToanQuyChiTiet;
end


GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_danhsachquyettoannam_index]    Script Date: 10/2/2023 5:45:47 PM ******/
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
		isnull(qtn.fTongTien_DuToanDuyet,0) - isnull(qtn.fTongTien_DeNghi,0) as fTongTienThua,
		isnull(qtn.fTongTien_DeNghi,0) - isnull(qtn.fTongTien_DuToanDuyet,0) as fTongTienThieu,
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
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapomdau_bhxh]
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

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 


		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			tblDonvi.sTenDonVi,
			Sum(iSoNgayDuoi14BenhDaiNgay) as iSoNgayDuoi14BenhDaiNgay,
			Sum(fSoTienDuoi14BenhDaiNgay) as fSoTienDuoi14BenhDaiNgay,
			Sum(iSoNgayTren14BenhDaiNgay) as iSoNgayTren14BenhDaiNgay,
			Sum(fSoTienTren14BenhDaiNgay) as fSoTienTren14BenhDaiNgay,
			Sum(iSoNgayDuoi14OmKhac) as iSoNgayDuoi14OmKhac,
			Sum(fSoTienDuoi14OmKhac) as fSoTienDuoi14OmKhac,
			Sum(iSoNgayTren14OmKhac) as iSoNgayTren14OmKhac,
			Sum(fSoTienTren14OmKhac) as fSoTienTren14OmKhac,
			Sum(iSoNgayConOm) as iSoNgayConOm,
			Sum(fSoTienConOm) as fSoTienConOm,
			Sum(iSoNgayPHSK) as iSoNgayPHSK,
			Sum(fSoTienPHSK) as fSoTienPHSK,
			isnull(Sum(fSoTienDuoi14BenhDaiNgay),0) + isnull(Sum(fSoTienTren14BenhDaiNgay),0) + isnull(Sum(fSoTienDuoi14OmKhac),0) + isnull(Sum(fSoTienTren14OmKhac),0) + isnull(Sum(fSoTienConOm),0)
			+ isnull(Sum(fSoTienPHSK),0) as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then iTongSo_DeNghi else 0 end iSoNgayTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-01-02' then fTongTien_DeNghi else 0 end fSoTienTren14BenhDaiNgay,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then iTongSo_DeNghi else 0 end iSoNgayDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-01' then fTongTien_DeNghi else 0 end fSoTienDuoi14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then iTongSo_DeNghi else 0 end iSoNgayTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0001-01-02-02' then fTongTien_DeNghi else 0 end fSoTienTren14OmKhac,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then iTongSo_DeNghi else 0 end iSoNgayConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0002-02-00' then fTongTien_DeNghi else 0 end fSoTienConOm,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then iTongSo_DeNghi else 0 end iSoNgayPHSK,
				case when as_qtct.sXauNoiMa = '010-011-0001-0003-03-00' then fTongTien_DeNghi else 0 end fSoTienPHSK
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join tblMucLucNganSach on tblMucLucNganSach.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				group by qtcn.iID_MaDonVi,case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join tblDonvi on tblDonvi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, tblDonvi.sTenDonVi
		
		

		drop table tblMucLucNganSach;
		drop table tblDonvi


end

GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_giaithichtrocapthaisan_bhxh]
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

		---Lấy danh sách đơn vị được chọn
		select 
			iID_DonVi,
			iID_MaDonVi,
			sTenDonVi
		into tblDonvi
		from DonVi where iNamLamViec = @INamLamViec and iID_MaDonVi  in (select * from dbo.splitstring(@IdMaDonVi)) 

		---Lấy thông tin chi tiết chứng từ
		select
			ROW_NUMBER() OVER(ORDER BY as_qtct_1.iID_MaDonVi ASC) AS sTT,
			as_qtct_1.iID_MaDonVi,
			tblDonvi.sTenDonVi,
			Sum(iSoNgaySinhConNNuoiCon) as iSoNgaySinhConNNuoiCon,
			Sum(fSoTienSinhConNNuoiCon) as fSoTienSinhConNNuoiCon,
			Sum(iSoNgaySinhTroCapSinhCon) as iSoNgaySinhTroCapSinhCon,
			Sum(fSoTienSinhTroCapSinhCon) as fSoTienSinhTroCapSinhCon,
			Sum(iSoNgayKhamThaiKHHGD) as ISoNgayKhamThaiKHHGD,
			Sum(fSoTienKhamThaiKHHGD) as fSoTienKhamThaiKHHGD,
			Sum(iSoNgayPHSKThaiSan) as iSoNgayPHSKThaiSan,
			Sum(fSoTienPHSKThaiSan) as fSoTienPHSKThaiSan,
			isnull(Sum(fSoTienSinhConNNuoiCon),0) + isnull(Sum(fSoTienSinhTroCapSinhCon),0) + isnull(Sum(fSoTienKhamThaiKHHGD),0) + isnull(Sum(fSoTienPHSKThaiSan),0)  as fTongTien
			
		from
			(
			select
				as_qtct.iID_MaDonVi,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-01-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhConNNuoiCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then iTongSo_DeNghi else 0 end iSoNgaySinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-02-00-02' then fTongTien_DeNghi else 0 end fSoTienSinhTroCapSinhCon,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then iTongSo_DeNghi else 0 end iSoNgayKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-01' or as_qtct.sXauNoiMa = '010-011-0002-0001-03-00-02' then fTongTien_DeNghi else 0 end fSoTienKhamThaiKHHGD,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then iTongSo_DeNghi else 0 end iSoNgayPHSKThaiSan,
				case when as_qtct.sXauNoiMa = '010-011-0002-0001-04-00' then fTongTien_DeNghi else 0 end fSoTienPHSKThaiSan
			from
			(
				select 
					qtcn.iID_MaDonVi,
					case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end sXauNoiMa,
					Sum(qtcn_ct.iTongSo_DeNghi) as iTongSo_DeNghi,
					Sum(qtcn_ct.fTongTien_DeNghi)  as fTongTien_DeNghi
				from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
				inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
				inner join tblMucLucNganSach on tblMucLucNganSach.iID_MLNS = qtcn_ct.iID_MucLucNganSach
				and ((@IsTongHop = 0 and qtcn.bDaTongHop = 0 and qtcn.sDSSoChungTuTongHop is null) or (@IsTongHop = 1 and  qtcn.sDSSoChungTuTongHop is not null))
				and qtcn.iQuyChungTu = @IQuy
				group by qtcn.iID_MaDonVi,case when sLNS = '9010001' then REPLACE(sXauNoiMa,'9010001-' , '') else REPLACE(sXauNoiMa,'9010002-' , '') end

			) as as_qtct) as as_qtct_1

			inner join tblDonvi on tblDonvi.iID_MaDonVi = as_qtct_1.iID_MaDonVi
			group by as_qtct_1.iID_MaDonVi, tblDonvi.sTenDonVi
		
		

		drop table tblMucLucNganSach;
		drop table tblDonvi


end

GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_quyettoanchicacchedo_bhxh]    Script Date: 10/2/2023 5:45:47 PM ******/
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
		where   iNamLamViec = @INamLamViec  and (sLNS  in ('9010001', '9010002'))


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
			where tblResult.level = 4 and  tblResult.sLNS = '9010002'
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
