/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_du_toan_thu_index]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_dtt_get_lns]    Script Date: 8/17/2023 8:51:50 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_bh_dieu_chinh_dtt_get_lns]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_bh_dieu_chinh_dtt_get_lns]
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_dtt_get_lns]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_dtt_get_lns]
	@namLamViec int,
	@donVi nvarchar(max),
	@ngayChungTu date,
	@userName nvarchar(100)
AS
BEGIN
	WITH tblLNS AS (
		SELECT DISTINCT sLNS
		FROM BH_DTT_BHXH_PhanBo_ChungTuChiTiet
		WHERE iID_MaDonVi in (select * from f_split(@donVi))
			AND iID_DTT_BHXH_ChungTu in 
				(SELECT iID_DTT_BHXH_PhanBo_ChungTu 
				FROM BH_DTT_BHXH_PhanBo_ChungTu 
				WHERE iNamLamViec = @namLamViec 
					AND sSoQuyetDinh IS NOT NULL
					AND sSoQuyetDinh <> ''
					AND bKhoa = 1
					AND cast(dNgayQuyetDinh as date) <= cast(@ngayChungTu as date))
	), 
	LNSTree AS (
		SELECT *
		FROM BH_DM_MucLucNganSach
		WHERE 
			sL = ''
			AND iNamLamViec = @namLamViec
			AND sLNS in (SELECT * FROM tblLNS)
		UNION ALL
		SELECT mlnsParent.*
		FROM BH_DM_MucLucNganSach mlnsParent
		INNER JOIN LNSTree
			ON mlnsParent.iID_MLNS = lnstree.iID_MLNS_Cha
		WHERE 
			mlnsParent.sL = '' 
			AND mlnsParent.iNamLamViec = @namLamViec
	)
	SELECT mlns.* 
	FROM (SELECT DISTINCT * FROM LNSTree) mlns
	WHERE mlns.iNamlamviec=@namLamViec
	AND mlns.sLNS like '902%'
	ORDER BY sXauNoiMa
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100)
AS
BEGIN
	SELECT 
		ct.iID_DTT_BHXH_DieuChinh_ChiTiet,
		ct.iID_DTT_BHXH_DieuChinh,
		ct.iID_MaDonVi,
		ct.iID_MucLucNganSach,
		ct.sLNS,
		ct.sXauNoiMa,
		ct.sNoiDung,
		ct.fThuBHXH_NLD,
		ct.fThuBHXH_NSD,
		ct.fThuBHYT_NLD,
		ct.fThuBHYT_NSD,
		ct.fThuBHTN_NLD,
		ct.fThuBHTN_NSD,
		ct.fThuBHXH_NLD_QTDauNam,
		ct.fThuBHXH_NSD_QTDauNam,
		ct.fThuBHYT_NLD_QTDauNam,
		ct.fThuBHYT_NSD_QTDauNam,
		ct.fThuBHTN_NLD_QTDauNam,
		ct.fThuBHTN_NSD_QTDauNam,
		ct.fThuBHXH_NLD_QTCuoiNam,
		ct.fThuBHXH_NSD_QTCuoiNam,
		ct.fThuBHYT_NLD_QTCuoiNam,
		ct.fThuBHYT_NSD_QTCuoiNam,
		ct.fThuBHTN_NLD_QTCuoiNam,
		ct.fThuBHTN_NSD_QTCuoiNam,
		ct.fTongThuBHXH_NLD,
		ct.fTongThuBHXH_NSD,
		ct.fTongThuBHYT_NLD,
		ct.fTongThuBHYT_NSD,
		ct.fTongThuBHTN_NLD,
		ct.fTongThuBHTN_NSD,
		ct.fTongCong,
		ct.fThuBHXH_NLD_Tang,
		ct.fThuBHXH_NLD_Giam,
		ct.fThuBHXH_NSD_Tang,
		ct.fThuBHXH_NSD_Giam,
		ct.fThuBHXH_Tang,
		ct.fThuBHXH_Giam,
		ct.fThuBHYT_NLD_Tang,
		ct.fThuBHYT_NLD_Giam,
		ct.fThuBHYT_NSD_Tang,
		ct.fThuBHYT_NSD_Giam,
		ct.fThuBHYT_Tang,
		ct.fThuBHYT_Giam,
		ct.fThuBHTN_NLD_Tang,
		ct.fThuBHTN_NLD_Giam,
		ct.fThuBHTN_NSD_Tang,
		ct.fThuBHTN_NSD_Giam,
		ct.fThuBHTN_Tang,
		ct.fThuBHTN_Giam,
		ct.sTenDonVi,
		ct.dNgaySua,
		ct.dNgayTao,
		ct.sNguoiSua,
		ct.sNguoiTao,
		ct.sGhiChu
	FROM
		(
			SELECT
				bh.iID_MaDonVi,
				bh.sMoTa,
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTT_BHXH_DieuChinh bh
			JOIN 
				BH_DTT_BHXH_DieuChinh_ChiTiet bhct ON bh.iID_DTT_BHXH_DieuChinh = bhct.iID_DTT_BHXH_DieuChinh 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bh.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bh.iID_MaDonVi = @IdDonVi
		) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_bh_dieu_chinh_du_toan_thu_index]

AS
BEGIN
	SET NOCOUNT ON;

    SELECT
	DISTINCT 
		dcdt.iID_DTT_BHXH_DieuChinh
		  ,dcdt.iNamLamViec
		  ,dcdt.sSoChungTu
		  ,dcdt.dNgayChungTu
		  ,dcdt.sMoTa
		  ,dcdt.iID_MaDonVi
		  ,dcdt.iID_DonVi
		  ,dcdt.bIsKhoa
		  ,dcdt.sLNS
		  ,dcdt.iLoaiTongHop
		  ,dcdt.sTongHop
		  ,dcdt.sNguoiTao
		  ,dcdt.sNguoiSua
		  ,dcdt.dNgayTao
		  ,dcdt.dNgaySua
		  ,dcdt.fThuBHXH_NLD
		  ,dcdt.fThuBHXH_NSD
		  ,dcdt.fThuBHYT_NLD
		  ,dcdt.fThuBHYT_NSD
		  ,dcdt.fThuBHTN_NLD
		  ,dcdt.fThuBHTN_NSD
		  ,dcdt.fThuBHXH_NLD_QTDauNam
		  ,dcdt.fThuBHXH_NSD_QTDauNam
		  ,dcdt.fThuBHYT_NLD_QTDauNam
		  ,dcdt.fThuBHYT_NSD_QTDauNam
		  ,dcdt.fThuBHTN_NLD_QTDauNam
		  ,dcdt.fThuBHTN_NSD_QTDauNam
		  ,dcdt.fThuBHXH_NLD_QTCuoiNam
		  ,dcdt.fThuBHXH_NSD_QTCuoiNam
		  ,dcdt.fThuBHYT_NLD_QTCuoiNam
		  ,dcdt.fThuBHYT_NSD_QTCuoiNam
		  ,dcdt.fThuBHTN_NLD_QTCuoiNam
		  ,dcdt.fThuBHTN_NSD_QTCuoiNam
		  ,dcdt.fTongThuBHXH_NLD
		  ,dcdt.fTongThuBHXH_NSD
		  ,dcdt.fTongThuBHYT_NLD
		  ,dcdt.fTongThuBHYT_NSD
		  ,dcdt.fTongThuBHTN_NLD
		  ,dcdt.fTongThuBHTN_NSD
		  ,dcdt.fTongCong
		  ,dcdt.fThuBHXH_NLD_Tang
		  ,dcdt.fThuBHXH_NLD_Giam
		  ,dcdt.fThuBHXH_NSD_Tang
		  ,dcdt.fThuBHXH_NSD_Giam
		  ,dcdt.fThuBHXH_Tang
		  ,dcdt.fThuBHXH_Giam
		  ,dcdt.fThuBHYT_NLD_Tang
		  ,dcdt.fThuBHYT_NLD_Giam
		  ,dcdt.fThuBHYT_NSD_Tang
		  ,dcdt.fThuBHYT_NSD_Giam
		  ,dcdt.fThuBHYT_Tang
		  ,dcdt.fThuBHYT_Giam
		  ,dcdt.fThuBHTN_NLD_Tang
		  ,dcdt.fThuBHTN_NLD_Giam
		  ,dcdt.fThuBHTN_NSD_Tang
		  ,dcdt.fThuBHTN_NSD_Giam
		  ,dcdt.fThuBHTN_Tang
		  ,dcdt.fThuBHTN_Giam
		  ,dcdt.iID_TongHopID
		  ,dcdt.bDaTongHop
		  , donVi.sTenDonVi
		  , dcdt.bIsKhoa
		-- Tong dự toán todo
	FROM BH_DTT_BHXH_DieuChinh dcdt
	LEFT JOIN DonVi donVi
		ON dcdt.iID_MaDonVi = donVi.iID_MaDonVi AND donVi.iID_DonVi = dcdt.iID_DonVi;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_bh_phan_bo_dtt_dieu_chinh_chi_tiet]
	@NamLamViec int,
	@IdDonVi nvarchar(100),
	@ngayChungTu date
AS
BEGIN
	SELECT 
		ct.*
	FROM
		(
			SELECT
				ddv.sTenDonVi,
				bhct.*
			FROM 
				BH_DTT_BHXH_PhanBo_ChungTu bh
			JOIN 
				BH_DTT_BHXH_PhanBo_ChungTuChiTiet bhct ON bh.iID_DTT_BHXH_PhanBo_ChungTu = bhct.iID_DTT_BHXH_ChungTu 
			LEFT JOIN 
				(SELECT * FROM DonVi WHERE iNamLamViec = @NamLamViec AND iTrangThai = 1 ) ddv ON bhct.iID_MaDonVi = ddv.iID_MaDonVi
			WHERE
				bhct.iID_MaDonVi = @IdDonVi
				and bh.bKhoa = 1
				AND bh.sSoQuyetDinh IS NOT NULL
				AND bh.sSoQuyetDinh <> ''
				AND cast(bh.DngayChungTu as date) <= cast(@ngayChungTu as date)
		) ct;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_dieu_chinh_dtt_tao_tonghop_chungtu_chitiet]
	@ListIdChungTuTongHop ntext, 
	@Nguoitao nvarchar(50), 
	@IdChungTu nvarchar(100), 
	@NamLamViec int 
AS 
BEGIN 
	INSERT INTO [dbo].BH_DTT_BHXH_DieuChinh_ChiTiet (
    iID_DTT_BHXH_DieuChinh_ChiTiet, iID_DTT_BHXH_DieuChinh, iID_MucLucNganSach, sLNS, sNoiDung,
	fThuBHXH_NLD, fThuBHXH_NSD, fThuBHYT_NLD, fThuBHYT_NSD, fThuBHTN_NLD, fThuBHTN_NSD,
	fThuBHXH_NLD_QTDauNam, fThuBHXH_NSD_QTDauNam, fThuBHYT_NLD_QTDauNam, fThuBHYT_NSD_QTDauNam, fThuBHTN_NLD_QTDauNam, fThuBHTN_NSD_QTDauNam,
	fThuBHXH_NLD_QTCuoiNam, fThuBHXH_NSD_QTCuoiNam, fThuBHYT_NLD_QTCuoiNam, fThuBHYT_NSD_QTCuoiNam, fThuBHTN_NLD_QTCuoiNam, fThuBHTN_NSD_QTCuoiNam,
	fTongThuBHXH_NLD, fTongThuBHXH_NSD, fTongThuBHYT_NLD, fTongThuBHYT_NSD, fTongThuBHTN_NLD, fTongThuBHTN_NSD,
	fThuBHXH_NLD_Tang, fThuBHXH_NSD_Tang, fThuBHXH_Tang, fThuBHYT_NLD_Tang, fThuBHYT_NSD_Tang, fThuBHYT_Tang, fThuBHTN_NLD_Tang, fThuBHTN_NSD_Tang, fThuBHTN_Tang,
	fThuBHXH_NLD_Giam, fThuBHXH_NSD_Giam, fThuBHXH_Giam, fThuBHYT_NLD_Giam, fThuBHYT_NSD_Giam, fThuBHYT_Giam, fThuBHTN_NLD_Giam, fThuBHTN_NSD_Giam, fThuBHTN_Giam,
    dNgaySua, dNgayTao, sNguoiSua, sNguoiTao)

	SELECT 
	DISTINCT NEWID(), @idChungTu, iID_MucLucNganSach, sLNS, sNoiDung, 
	sum(fThuBHXH_NLD), sum(fThuBHXH_NSD), sum(fThuBHYT_NLD), sum(fThuBHYT_NSD), sum(fThuBHTN_NLD), sum(fThuBHTN_NSD),
	sum(fThuBHXH_NLD_QTDauNam), sum(fThuBHXH_NSD_QTDauNam), sum(fThuBHYT_NLD_QTDauNam), sum(fThuBHYT_NSD_QTDauNam), sum(fThuBHTN_NLD_QTDauNam), sum(fThuBHTN_NSD_QTDauNam),
	sum(fThuBHXH_NLD_QTCuoiNam), sum(fThuBHXH_NSD_QTCuoiNam), sum(fThuBHYT_NLD_QTCuoiNam), sum(fThuBHYT_NSD_QTCuoiNam), sum(fThuBHTN_NLD_QTCuoiNam), sum(fThuBHTN_NSD_QTCuoiNam),
	sum(fTongThuBHXH_NLD), sum(fTongThuBHXH_NSD), sum(fTongThuBHYT_NLD), sum(fTongThuBHYT_NSD), sum(fTongThuBHTN_NLD), sum(fTongThuBHTN_NSD),
	sum(fThuBHXH_NLD_Tang), sum(fThuBHXH_NSD_Tang), sum(fThuBHXH_Tang), sum(fThuBHYT_NLD_Tang), sum(fThuBHYT_NSD_Tang), sum(fThuBHYT_Tang), sum(fThuBHTN_NLD_Tang), sum(fThuBHTN_NSD_Tang), sum(fThuBHTN_Tang),
	sum(fThuBHXH_NLD_Giam), sum(fThuBHXH_NSD_Giam), sum(fThuBHXH_Giam), sum(fThuBHYT_NLD_Giam), sum(fThuBHYT_NSD_Giam), sum(fThuBHYT_Giam), sum(fThuBHTN_NLD_Giam), sum(fThuBHTN_NSD_Giam), sum(fThuBHTN_Giam),
	Null, GETDATE(), Null, @Nguoitao 
	FROM 
	  BH_DTT_BHXH_DieuChinh_ChiTiet 
	WHERE 
	  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop)) 
	GROUP BY 
	  sLNS,
	  iID_MucLucNganSach, 
	  sNoiDung;

	  --danh dau chung tu da tong hop
		update 
		  BH_DTT_BHXH_DieuChinh 
		set 
		  iLoaiTongHop = 2,
		  bDaTongHop = 1
		where 
		  iID_DTT_BHXH_DieuChinh in (SELECT * FROM f_split(@ListIdChungTuTongHop))
END

GO
/****** Object:  StoredProcedure [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]    Script Date: 8/17/2023 8:51:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_rpt_dieu_chinh_du_toan_thu_bhxh]
AS
BEGIN
	--BHXH
-- Khối dự toán
select
N'1. Thu Bảo hiểm xã hội' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'a) Khối dự toán' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
(sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD) Tang,
sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020001' -- Khối dự toán
union all
select
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
(sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD) Tang,
sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020001' -- Khối dự toán

-- Khối dự hạch toán
union all
select
N'b) Khối hạch toán' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'- Người lao động đóng' NoiDung,
sum(fThuBHXH_NLD) DttDauNam,
sum(fThuBHXH_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam) TongCong,
(sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD) Tang,
sum(fThuBHXH_NLD) - (sum(fThuBHXH_NLD_QTDauNam) + sum(fThuBHXH_NLD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020002' -- Khối hạch toán
union all
select
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHXH_NSD) BhxhNsdDauNam,
sum(fThuBHXH_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHXH_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam) TongCong,
(sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD) Tang,
sum(fThuBHXH_NSD) - (sum(fThuBHXH_NSD_QTDauNam) + sum(fThuBHXH_NSD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020002' -- Khối hạch toán
-----------------------------------------
--BHYT
-- Khối dự toán
union all
select
N'2. Thu Bảo hiểm y tế' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'a) Khối dự toán' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'- Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHYT_NLD) Tang,
sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020001' -- Khối dự toán
union all
select
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) BhxhNsdDauNam,
sum(fThuBHYT_NSD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
(sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHYT_NSD) Tang,
sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020001' -- Khối dự toán

-- Khối dự hạch toán
union all
select
N'b) Khối hạch toán' NoiDung,
null DttDauNam,
null Dtt6ThangDauNam,
null Dtt6ThangCuoiNam,
null TongCong,
null Tang,
null Giam
union all
select
N'- Người lao động đóng' NoiDung,
sum(fThuBHYT_NLD) DttDauNam,
sum(fThuBHYT_NLD_QTDauNam) Dtt6ThangDauNam,
sum(fThuBHYT_NLD_QTCuoiNam) Dtt6ThangCuoiNam,
sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam) TongCong,
(sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) - sum(fThuBHXH_NLD) Tang,
sum(fThuBHYT_NLD) - (sum(fThuBHYT_NLD_QTDauNam) + sum(fThuBHYT_NLD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020002' -- Khối hạch toán
union all
select
N'- Người sử dụng LĐ đóng' NoiDung,
sum(fThuBHYT_NSD) BhxhNsdDauNam,
sum(fThuBHYT_NSD_QTDauNam) BhxhNsd6ThangDauNam,
sum(fThuBHYT_NSD_QTCuoiNam) BhxhNsd6ThangCuoiNam,
sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam) TongCong,
(sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) - sum(fThuBHXH_NSD) Tang,
sum(fThuBHYT_NSD) - (sum(fThuBHYT_NSD_QTDauNam) + sum(fThuBHYT_NSD_QTCuoiNam)) Giam
from BH_DTT_BHXH_DieuChinh_ChiTiet ctct
where ctct.sLNS = '9020002' -- Khối hạch toán
END
GO
