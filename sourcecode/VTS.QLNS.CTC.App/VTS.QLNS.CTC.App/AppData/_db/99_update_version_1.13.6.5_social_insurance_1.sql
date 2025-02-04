/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_luong_bhxh_import_qtc_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bhxh_thtc_get_so_quyet_dinh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 12/8/2023 11:19:00 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bh_qtcq_ctct_gttrocap_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]
GO
/****** Object:  StoredProcedure [dbo].[bh_qtcq_ctct_gttrocap_index]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Doantc
-- Create date: 13/06/2023
-- Description:	Lấy danh sách hiển thị giai thich 
-- =============================================
CREATE PROCEDURE [dbo].[bh_qtcq_ctct_gttrocap_index]

@YearWork int,
@IdQTCQuyCheDoBHXH uniqueidentifier,
@SXauNoiMa nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		 gttc.iiD_QTC_Quy_CTCT_GiaiThichTroCap
		, gttc.iID_QTC_Quy_ChungTu as IID_QTC_Quy_ChungTu
		, gttc.iNamLamViec
		, gttc.iQuy
		, gttc.sNguoiSua
		, gttc.sNguoiTao
		, gttc.dNgaySua
		, gttc.dNgayTao
		, gttc.iSoNgayHuong
		, gttc.sMa_Hieu_Can_Bo AS SMaHieuCanBo
		, gttc.iiD_MaPhanHo AS  ID_MaPhanHo
		, gttc.sMaCapBac
		, gttc.sTenCapBac
		, gttc.fSoTien
		, gttc.iiD_MaPhanHo
		, gttc.sSoQuyetDinh
		, gttc.sTenCanBo
		, gttc.sXauNoiMa
		, gttc.dNgayQuyetDinh
		, gttc.iiD_MaDonVi AS ID_MaDonVi
		, gttc.sSoSoBHXH
		, gttc.dTuNgay
		, gttc.dDenNgay
		, gttc.fTienLuongThangDongBHXH
		-- Tong dự toán todo
	FROM BH_QTC_Quy_CTCT_GiaiThichTroCap gttc
	WHERE gttc.iNamLamViec=@YearWork
			AND gttc.iID_QTC_Quy_ChungTu=@IdQTCQuyCheDoBHXH
			AND gttc.sXauNoiMa=@SXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bhxh_thtc_get_so_quyet_dinh]
	@NamLamViec int
AS
BEGIN
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTT_BHXH_PhanBo_ChungTu dtt
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTTM_BHYT_ThanNhan_PhanBo dtt
	union
	select distinct  dtt.sSoQuyetDinh, Convert(varchar, dtt.dNgayQuyetDinh, 101) sNgayQuyetDinh
		from BH_DTC_PhanBoDuToanChi dtt

END
GO
/****** Object:  StoredProcedure [dbo].[sp_luong_bhxh_import_qtc_bhxh]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_luong_bhxh_import_qtc_bhxh]
	@YearOfWork int,
	@Months nvarchar(20)
AS
BEGIN
	select chedo.sXauNoiMaMlnsBHXH, luong.*
	into luong_temp
	from TL_BangLuong_ThangBHXH luong
	left join TL_DM_CheDoBHXH chedo
		on luong.sMaCheDo = chedo.sMaCheDo
	where 
		luong.iNam = @YearOfWork
		and luong.iThang in (SELECT * FROM f_split(@Months))
		and luong.sMaCheDo in 
		(select distinct sMaCheDo from TL_DM_CheDoBHXH
		where sMaCheDoCha is not null and sMaCheDoCha <> ''
		and sMaCheDo not in (select distinct sMaCheDoCha from TL_DM_CheDoBHXH)
		and (upper(sMaCheDo) not like '%_HS%' and upper(sMaCheDo) not like '%_HESO%'))

	--Thong tin luong Si quan
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_sq
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '1%'
			group by
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong QNCN
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_qncn
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '2%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong HSQ_BS
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hsq_bs
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB like '0%'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong VCQP
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_vcqp
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB in ('3.1', '3.2', '3.3')
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Thong tin luong hdld
	select distinct
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi,
		sum(SoTien) SoTien
	into luong_temp_hdld
	from
		(select sXauNoiMaMlnsBHXH, sMaCheDo,
			count(sMaHieuCanBo) SoNguoi,
			isnull(sum(nGiaTri), 0) SoTien
			from luong_temp
			where sMaCB = '43'
			group by 
				sXauNoiMaMlnsBHXH,
				sMaCheDo,
				sMaHieuCanBo) temp
	group by
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo,
		temp.SoNguoi

	--Ket qua
	select
		temp.sXauNoiMaMlnsBHXH,
		temp.sMaCheDo MaCheDo,
		sq.SoNguoi SoNguoiSQ,
		sq.SoTien SoTienSQ,
		qncn.SoTien SoTienQNCN,
		qncn.SoNguoi SoNguoiQNCN,
		hsq.SoTien SoTienHSQ,
		hsq.SoNguoi SoNguoiHSQ,
		vcqp.SoTien SoTienVCQP,
		vcqp.SoNguoi SoNguoiVCQP,
		hdld.SoTien SoTienHDLD,
		hdld.SoNguoi SoNguoiHDLD
	from luong_temp temp
	left join luong_temp_sq sq on temp.sMaCheDo = sq.sMaCheDo
	left join luong_temp_qncn qncn on sq.sMaCheDo = qncn.sMaCheDo
	left join luong_temp_hsq_bs hsq  on sq.sMaCheDo = hsq.sMaCheDo
	left join luong_temp_vcqp vcqp on sq.sMaCheDo = vcqp.sMaCheDo
	left join luong_temp_hdld hdld on sq.sMaCheDo = hdld.sMaCheDo

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp]') AND type in (N'U'))
	drop table luong_temp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_sq]') AND type in (N'U'))
	drop table luong_temp_sq;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_qncn]') AND type in (N'U'))
	drop table luong_temp_qncn;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hsq_bs]') AND type in (N'U'))
	drop table luong_temp_hsq_bs;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_vcqp]') AND type in (N'U'))
	drop table luong_temp_vcqp;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[luong_temp_hdld]') AND type in (N'U'))
	drop table luong_temp_hdld;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_qtcq_giai_thich_che_do_om_dau]
	@NamLamViec int,
	@DVT int,
	@Quy int,
	@DonVi nvarchar(200)
AS
BEGIN
	---Bệnh dài ngày
	select gt.* into TBL_BenhDaiNgay from BH_QTC_Quy_CTCT_GiaiThichTroCap gt
	where (sXauNoiMa like '9010001-010-011-0001-0001-0001-01%' or sXauNoiMa like '9010002-010-011-0001-0001-0001-01%')
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_BDN_SiQuan from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_BDN_SiQuan) > 2
		update TBL_BDN_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_BDN_QNCN from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_BDN_QNCN) > 2
		update TBL_BDN_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_BDN_HSQ_BS from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_BDN_HSQ_BS) > 2
		update TBL_BDN_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_BDN_VCQP from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_VCQP) > 2
		update TBL_BDN_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_BDN_HDLD from
	(select 1 bHangCha, N'A' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'A' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'A' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_BenhDaiNgay
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_BDN_HDLD) > 2
		update TBL_BDN_HDLD set bHasData = 1

	----------------------------
	---Ốm khác
	select * into TBL_OmKhac from BH_QTC_Quy_CTCT_GiaiThichTroCap
	where (sXauNoiMa like '9010001-010-011-0001-0001-0001-02%' or sXauNoiMa like '9010002-010-011-0001-0001-0001-02%')
		and iNamLamViec = @NamLamViec and iQuy = @Quy and iiD_MaDonVi in (SELECT * FROM f_split(@DonVi))

	--Sy quan
	select * into TBL_OK_SiQuan from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'I' STT, 'SQ' DoiTuong, 'SQ' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Sỹ quan' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  1 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'SQ' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '1%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) sq

	 if (select count(1) from TBL_OK_SiQuan) > 2
		update TBL_OK_SiQuan set bHasData = 1

	 --QNCN
	select * into TBL_OK_QNCN from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 2 RowNum, N'I' STT, 'QNCN' DoiTuong, 'QNCN' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Quân nhân chuyên nghiệp' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  2 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'QNCN' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '2%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) qncn

	 if (select count(1) from TBL_OK_QNCN) > 2
		update TBL_OK_QNCN set bHasData = 1

	 --HSQ_BS
	select * into TBL_OK_HSQ_BS from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'A' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 3 RowNum, N'I' STT, 'HSQ_BS' DoiTuong, 'HSQ_BS' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'HSQ, BS' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  3 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HSQ_BS' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac like '0%' 
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) hsq

	 if (select count(1) from TBL_OK_HSQ_BS) > 2
		update TBL_OK_HSQ_BS set bHasData = 1

	 --VCQP
	select * into TBL_OK_VCQP from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 4 RowNum, N'I' STT, 'VCQP' DoiTuong, 'VCQP' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'CC, CN và VC quốc phòng' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  4 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'VCQP' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac in ('3.1', '3.2', '3.3')
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_VCQP) > 2
		update TBL_OK_VCQP set bHasData = 1

	 --HDLD
	select * into TBL_OK_HDLD from
	(select 1 bHangCha, N'B' LoaiTC, 1 RowNum, N'B' STT, null DoiTuong, null LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Không thuộc Danh mục bệnh cần chữa trị dài ngày' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 1 bHangCha, N'B' LoaiTC, 5 RowNum, N'I' STT, 'HDLD' DoiTuong, 'HDLD' LoaiDoiTuong, null sMa_Hieu_Can_Bo, N'Hợp đồng lao động' sTenCanBo, null sTenCapBac, null sSoSoBHXH, null fTienLuongThangDongBHXH, null dTuNgay, null dDenNgay, null iSoNgayHuong, null fSoTien, '' iiD_MaPhanHo, 0 bHasData
	union all
	select 0 bHangCha, 'B' LoaiTC,  5 RowNum, CAST(ROW_NUMBER() OVER (ORDER BY sMa_Hieu_Can_Bo) AS VARCHAR(6)) STT, '', 'HDLD' LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, sum(iSoNgayHuong) iSoNgayHuong, sum(fSoTien) fSoTien, iiD_MaPhanHo, 0 bHasData
	from TBL_OmKhac
	 where sMaCapBac = '43'
	 group by sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iiD_MaPhanHo) vcqp

	 if (select count(1) from TBL_OK_HDLD) > 2
		update TBL_OK_HDLD set bHasData = 1
	-----------------------------------
	--Ket qua
	select TBL_GTCD_RESULT.* into TBL_GTCD_RESULT from(
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien,iiD_MaPhanHo, bHasData 
	from TBL_BDN_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_BDN_HDLD
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_SiQuan
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_QNCN
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HSQ_BS
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_VCQP
	union all
	select bHangCha, LoaiTC, RowNum, STT, DoiTuong, LoaiDoiTuong, sMa_Hieu_Can_Bo, sTenCanBo, sTenCapBac, sSoSoBHXH, fTienLuongThangDongBHXH, dTuNgay, dDenNgay, iSoNgayHuong, fSoTien, iiD_MaPhanHo, bHasData 
	from TBL_OK_HDLD) TBL_GTCD_RESULT

	select distinct
		gt.bHangCha IsHangCha, 
		gt.LoaiTC, 
		gt.RowNum, 
		gt.STT, 
		gt.DoiTuong,
		gt.LoaiDoiTuong,
		case 
			when gt.bHangCha = 0 then concat(gt.sMa_Hieu_Can_Bo, ' - ', donvi.Ten_DonVi)
			else ''
		end as sMa_Hieu_Can_Bo,
		gt.sTenCanBo, 
		gt.sTenCapBac, 
		gt.sSoSoBHXH, 
		gt.fTienLuongThangDongBHXH, 
		gt.dTuNgay, 
		gt.dDenNgay, 
		gt.iSoNgayHuong, 
		gt.fSoTien/@DVT fSoTien, 
		gt.bHasData from TBL_GTCD_RESULT gt
		left join TL_DM_DonVi donvi on gt.iiD_MaPhanHo = donvi.Ma_DonVi and donvi.iTrangThai = 1
	where bHasData = 1
	order by LoaiTC, RowNum, LoaiDoiTuong, DoiTuong desc

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BenhDaiNgay]') AND type in (N'U')) drop table TBL_BenhDaiNgay;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OmKhac]') AND type in (N'U')) drop table TBL_OmKhac;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_SiQuan]') AND type in (N'U')) drop table TBL_BDN_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_QNCN]') AND type in (N'U')) drop table TBL_BDN_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HSQ_BS]') AND type in (N'U')) drop table TBL_BDN_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_VCQP]') AND type in (N'U')) drop table TBL_BDN_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_BDN_HDLD]') AND type in (N'U')) drop table TBL_BDN_HDLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_SiQuan]') AND type in (N'U')) drop table TBL_OK_SiQuan;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_QNCN]') AND type in (N'U')) drop table TBL_OK_QNCN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HSQ_BS]') AND type in (N'U')) drop table TBL_OK_HSQ_BS;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_VCQP]') AND type in (N'U')) drop table TBL_OK_VCQP;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_OK_HDLD]') AND type in (N'U')) drop table TBL_OK_HDLD;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_GTCD_RESULT]') AND type in (N'U')) drop table TBL_GTCD_RESULT;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_chi]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200)
AS
BEGIN
	---CHI---
	select ctct.* into TBL_DTC from BH_DTC_PhanBoDuToanChi_ChiTiet ctct
	join BH_DTC_PhanBoDuToanChi ct on ctct.iID_DTC_PhanBoDuToanChi = ct.ID
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Chi các chế độ BHXH DUTOAN
	select * into TBL_CHI_CHEDO_DUTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010001-010-011-0008%'
	union all 
	select 10 rowNum, 1 bHangCha, 2 stt, N'Kinh phí quản lý BHXH, BHYT' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010003%'
	union all 
	select 11 rowNum, 1 bHangCha, 3 stt, N'Kinh phí KCB tại quân y đơn vị' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010004%' or sXauNoiMa like '9010005%'
	union all 
	select 12 rowNum, 1 bHangCha, 4 stt, N'Kinh phí KCB tại Trường sa - DK' sMoTa, null fTongSo, sum(fTongTien) fDuToan, null fHachToan
	from TBL_DTC where sXauNoiMa like '9010006%' or sXauNoiMa like '9010007%'
	) chidutoan



	--Chi các chế độ BHXH hạch toán
	select * into TBL_CHI_CHEDO_HACHTOAN from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Chi các chế độ BHXH' sMoTa, null fTongSoChi, null fDuToan, null fHachToan
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Ốm đau' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0001%'
	union all 
	select 3 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Thai sản' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0002%'
	union all 
	select 4 rowNum, 0 bHangCha, null stt, N'- Trợ cấp tai nạn lao động, BNN' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0003%'
	union all 
	select 5 rowNum, 0 bHangCha, null stt, N'- Trợ cấp hưu trí' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0004%'
	union all 
	select 6 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Phục viên' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0005%'
	union all 
	select 7 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Xuất ngũ' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0006%'
	union all 
	select 8 rowNum, 0 bHangCha, null stt, N'- Trợ cấp thôi việc' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0007%'
	union all 
	select 9 rowNum, 0 bHangCha, null stt, N'- Trợ cấp Tử tuất' sMoTa, null fTongSo, null fDuToan, sum(fTongTien) fHachToan
	from TBL_DTC where sXauNoiMa like '9010002-010-011-0008%') chihachtoan

	select dt.rowNum, 
			dt.bHangCha, 
			dt.stt, 
			dt.sMoTa, 
			(isnull(dt.fDuToan, 0) + isnull(ht.fHachToan, 0))/@DVT fTongSoChi, 
			dt.fDuToan/@DVT fDuToan, 
			ht.fHachToan/@DVT fHachToan
	into TBL_DTC_RESULT
	from TBL_CHI_CHEDO_DUTOAN dt
	left join TBL_CHI_CHEDO_HACHTOAN ht on dt.rowNum = ht.rowNum

	update TBL_DTC_RESULT set fTongSoChi = (select sum(fTongSoChi) from TBL_DTC_RESULT where bHangCha = 0),
							fDuToan = (select sum(fDuToan) from TBL_DTC_RESULT where bHangCha = 0),
							fHachToan = (select sum(fHachToan) from TBL_DTC_RESULT where bHangCha = 0)
						where bHangCha = 1 and stt = 1

	--result
	select * from TBL_DTC_RESULT

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC]') AND type in (N'U')) drop table TBL_DTC;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_DUTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_DUTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_CHI_CHEDO_HACHTOAN]') AND type in (N'U')) drop table TBL_CHI_CHEDO_HACHTOAN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTC_RESULT]') AND type in (N'U')) drop table TBL_DTC_RESULT;

END
GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]    Script Date: 12/8/2023 11:19:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_bhxh_tong_hop_thu_chi_thu]
	@NamLamViec int,
	@DonVis nvarchar(600),
	@SoQuyetDinh nvarchar(200),
	@DVT int,
	@NgayQuyetDinh nvarchar(200)
AS
BEGIN
	---THU---
	--Dữ liệu phân bổ dự toán thu BHXH
	select ctct.* into TBL_DTT from BH_DTT_BHXH_PhanBo_ChungTuChiTiet ctct
	join BH_DTT_BHXH_PhanBo_ChungTu ct on ctct.iID_DTT_BHXH_ChungTu = ct.iID_DTT_BHXH_PhanBo_ChungTu
	where ctct.iNamLamViec = @NamLamViec 
		and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) 
		and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
		and Convert(varchar, ct.dNgayQuyetDinh, 101) in (SELECT * FROM f_split(@NgayQuyetDinh))
	--Thu BHXH
	select * into TBL_THU_BHXH from
	(select 1 rowNum, 1 bHangCha, '1' stt, N'Thu BHXH' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 2 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 3 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHXH_NLD, 0)) + sum(isnull(fBHXH_NSD, 0))) fTongSo, sum(fBHXH_NLD) fNLD, sum(fBHXH_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhxh

	update TBL_THU_BHXH set fTongSo = (select sum(fTongSo) from TBL_THU_BHXH where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHXH where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHXH where bHangCha = 0)
						where bHangCha = 1

	--Thu BHTN
	select * into TBL_THU_BHTN from
	(select 4 rowNum, 1 bHangCha, '2' stt, N'Thu BHTN' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 5 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001%'
	union all
	select 6 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHTN_NLD, 0)) + sum(isnull(fBHTN_NSD, 0))) fTongSo, sum(fBHTN_NLD) fNLD, sum(fBHTN_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002%') thubhtn

	update TBL_THU_BHTN set fTongSo = (select sum(fTongSo) from TBL_THU_BHTN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHTN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHTN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT quân nhân
	select * into TBL_THU_BHYT_QN from
	(select 7 rowNum, 1 bHangCha, '3' stt, N'Thu BHYT quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 8 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0001%'
	union all
	select 9 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020002-010-011-0001%') thubhytquannhan

	update TBL_THU_BHYT_QN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_QN where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_QN where bHangCha = 0)
						where bHangCha = 1

	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_NLD from
	(select 10 rowNum, 1 bHangCha, '4' stt, N'Thu BHYT người lao động' sMoTa, null fTongSo, null fNLD, null fNSD, null sLoai
	union all
	select 11 rowNum, 0 bHangCha, null stt, N'Đơn vị dự toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%'
	union all
	select 12 rowNum, 0 bHangCha, null stt, N'Đơn vị hạch toán' sMoTa, (sum(isnull(fBHYT_NLD, 0)) + sum(isnull(fBHYT_NSD, 0))) fTongSo, sum(fBHYT_NLD) fNLD, sum(fBHYT_NSD) fNSD, null sLoai
	from TBL_DTT where sXauNoiMa like '9020001-010-011-0002%') thubhytnld

	update TBL_THU_BHYT_NLD set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_NLD where bHangCha = 0),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_NLD where bHangCha = 0)
						where bHangCha = 1
	
	--Dữ liệu phân bổ dự toán thu mua BHYT
	select ctct.* into TBL_DTTM from BH_DTTM_BHYT_ThanNhan_PhanBo_ChiTiet ctct
	join  BH_DTTM_BHYT_ThanNhan_PhanBo ct on ctct.iID_DTTM_BHYT_ThanNhan_PhanBo = ct.iID_DTTM_BHYT_ThanNhan_PhanBo
	where ctct.iNamLamViec = @NamLamViec and ct.sSoQuyetDinh in (SELECT * FROM f_split(@SoQuyetDinh)) and ctct.iid_MaDonVi in (SELECT * FROM f_split(@DonVis))
	--Thu BHYT người lao động
	select * into TBL_THU_BHYT_TN from
	(select 13 rowNum, 1 bHangCha, '5' stt, N'Thu BHYT thân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN' sLoai
	union all
	select 14 rowNum, 1 bHangCha, 'a' stt, N'Quân nhân' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	union all
	select 15 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0000%'
	union all
	select 16 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_QN' sLoai
	from TBL_DTTM where sXauNoiMa like '9030001-010-011-0001%'
	union all
	select 17 rowNum, 1 bHangCha, 'b' stt, N'Công nhân, VCQP' sMoTa, null fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	union all
	select 18 rowNum, 0 bHangCha, null stt, N'- Đơn vị dự toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0000%'
	union all
	select 19 rowNum, 0 bHangCha, null stt, N'- Đơn vị hạch toán' sMoTa, sum(fDuToan) fTongSo, null fNLD, null fNSD, N'BHYT_THANNHAN_VCQP' sLoai
	from TBL_DTTM where sXauNoiMa like '9030002-010-011-0001%'
	) thubhytnld

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_QN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_QN'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 0 and sLoai = 'BHYT_THANNHAN_VCQP')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN_VCQP'

	update TBL_THU_BHYT_TN set fTongSo = (select sum(fTongSo) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNLD = (select sum(fNLD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN'),
							fNSD = (select sum(fNSD) from TBL_THU_BHYT_TN where bHangCha = 1 and sLoai <> 'BHYT_THANNHAN')
						where bHangCha = 1 and sLoai = 'BHYT_THANNHAN'

	select * into TBL_THU from
	(select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHXH
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHTN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_QN
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_NLD
	union all
	select rowNum, bHangCha, stt, sMoTa, fTongSo, fNLD, fNSD from TBL_THU_BHYT_TN) tblthu

	--Result
	select
	rowNum, 
	bHangCha, 
	stt, 
	sMoTa, 
	fTongSo/@DVT fTongSo, 
	fNLD/@DVT fNLD, 
	fNSD/@DVT fNSD
	from TBL_THU

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTT]') AND type in (N'U')) drop table TBL_DTT;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHXH]') AND type in (N'U')) drop table TBL_THU_BHXH;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHTN]') AND type in (N'U')) drop table TBL_THU_BHTN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_QN]') AND type in (N'U')) drop table TBL_THU_BHYT_QN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_NLD]') AND type in (N'U')) drop table TBL_THU_BHYT_NLD;

	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_DTTM]') AND type in (N'U')) drop table TBL_DTTM;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU_BHYT_TN]') AND type in (N'U')) drop table TBL_THU_BHYT_TN;
	IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBL_THU]') AND type in (N'U')) drop table TBL_THU;

END
GO
