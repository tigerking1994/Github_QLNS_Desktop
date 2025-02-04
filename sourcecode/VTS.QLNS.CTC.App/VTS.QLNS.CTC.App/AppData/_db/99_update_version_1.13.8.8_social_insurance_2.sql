/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 1/16/2024 7:33:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 1/16/2024 7:33:23 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]    Script Date: 1/16/2024 7:33:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_chiquybhxh_chitiet]
@IdChungTu uniqueidentifier,
@SLNS nvarchar(max),
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
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM f_split(@SLNS))
		
		
		
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
			qtcn_ct.fTienLDHD_DeNghi,
			qtcn.iID_MaDonVi iIDMaDonVi,
			qtcn.iNamChungTu iNamLamViec
		into #tblQuyetToanQuyChiTiet
		from BH_QTC_Quy_CheDoBHXH_ChiTiet as qtcn_ct
		inner join BH_QTC_Quy_CheDoBHXH  as qtcn on qtcn_ct.iID_QTC_Quy_CheDoBHXH = qtcn.ID_QTC_Quy_CheDoBHXH
		where qtcn_ct.iID_QTC_Quy_CheDoBHXH = @IdChungTu
			AND qtcn.iNamChungTu=@INamLamViec;

		
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
			mucluc.sDuToanChiTietToi,
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
			chi_tiet.fTienLDHD_DeNghi,
			chi_tiet.iIDMaDonVi,
			chi_tiet.iNamLamViec
		from #tblMucLucNganSach as mucluc
		left join #tblQuyetToanQuyChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
		order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanQuyChiTiet;
end
;
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]    Script Date: 1/16/2024 7:33:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_bh_quyet_toan_get_fTienDuToanDuyet_for_pbdtchi]
@INamLamViec int,
@SMaLoaiChi nvarchar(50),
@IDLoaiChi nvarchar(max),
@SMaDonVi nvarchar(50),
@SLNS nvarchar(50),
@DNgayChungTu datetime

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
			danhmuc.bHangCha,
			danhmuc.sDuToanChiTietToi
		into #tblMucLucNganSach
		from BH_DM_MucLucNganSach as danhmuc
		where danhmuc.iNamLamViec = @INamLamViec and danhmuc.sLNs in (SELECT * FROM splitstring(@SLNS))
				and danhmuc.iTrangThai=1
		
	
	-- lay dữ liệu bên phân bổ chi
		select 
			pbdt_ct.iID_MucLucNganSach,
			pbdt_ct.iID_MaDonVi,
			Sum(pbdt_ct.fTienTuChi) as fTienDuToanDuyet
			--Sum(pbdt_ct.fTienTuChi) as fTienDuToanDuyet, ---3
			--0 iSoDuToanDuocDuyet, --2

			--0 iTongSo_ThucChi,
			--0 fTongTien_ThucChi, ---5

			--0 iSoSQ_ThucChi, ---6
			--0 fTienSQ_ThucChi, ---7

			--0 iSoQNCN_ThucChi, ----8
			--0 fTienQNCN_ThucChi,---9

			--0 iSoCNVCQP_ThucChi,---10
			--0 fTienCNVCQP_ThucChi, ----11

			--0 iSoLDHD_ThucChi, ----13
			--0 fTienLDHD_ThucChi, ---14

			--0 iSoHSQBS_ThucChi, ----15

			--0  as fTienThua,
			--0 as  fTienThieu

		into #tblQuyetToanPhanBoChiTiet
		from BH_DTC_PhanBoDuToanChi_ChiTiet as pbdt_ct
		where pbdt_ct.iID_DTC_PhanBoDuToanChi in
		( SELECT ID FROM  BH_DTC_PhanBoDuToanChi
					WHERE sSoQuyetDinh <> ''
				  AND sSoQuyetDinh IS NOT NULL
				  AND iNamChungTu = @INamLamViec
				  AND iID_LoaiDanhMucChi = @IDLoaiChi
				  AND sMaLoaiChi=@SMaLoaiChi
				  AND bIsKhoa=1
				  AND cast(dNgayQuyetDinh AS date) <= cast(@DNgayChungTu AS date))
				AND iID_MaDonVi in  (SELECT * FROM splitstring(@SMaDonVi))
			group by pbdt_ct.iID_MucLucNganSach,
					pbdt_ct.iID_MaDonVi

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
		mucluc.sDuToanChiTietToi,
		mucluc.iID_MLNS as iID_MucLucNganSach,
		mucluc.sMoTa as sLoaiTroCap,

		chi_tiet.iID_MaDonVi,
		chi_tiet.fTienDuToanDuyet
		--chi_tiet.iSoDuToanDuocDuyet,

		--chi_tiet.iTongSo_ThucChi, 
		--chi_tiet.fTongTien_ThucChi,

		--chi_tiet.iSoSQ_ThucChi,
		--chi_tiet.fTienSQ_ThucChi,

		--chi_tiet.iSoQNCN_ThucChi,
		--chi_tiet.fTienQNCN_ThucChi,

		--chi_tiet.iSoCNVCQP_ThucChi,
		--chi_tiet.fTienCNVCQP_ThucChi,

		--chi_tiet.iSoLDHD_ThucChi,
		--chi_tiet.fTienLDHD_ThucChi,

		--chi_tiet.iSoHSQBS_ThucChi,
		--chi_tiet.fTienHSQBS_ThucChi,

		--chi_tiet.fTienThua,
		--chi_tiet.fTienThieu
	from #tblMucLucNganSach as mucluc
	left join #tblQuyetToanPhanBoChiTiet as chi_tiet on mucluc.iID_MLNS = chi_tiet.iID_MucLucNganSach
	order by mucluc.sXauNoiMa
	
	drop table #tblMucLucNganSach;
	drop table #tblQuyetToanPhanBoChiTiet;
end
;
GO
