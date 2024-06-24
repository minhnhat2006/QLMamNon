-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 06, 2017 at 04:39 AM
-- Server version: 5.7.15-log
-- PHP Version: 5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `qlmamnon`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `countBangThuTienGenHistoryByLopAndNgayTinh`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
SELECT COUNT(*) FROM `bangthutiengenhistory` WHERE (lop IS NULL OR LopId = lop) AND NgayTinh = ngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteHocSinhLopByHocSinhIds`(IN `hocsinhids` TEXT, IN `deleteddate` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('UPDATE `hocsinh_lop` SET `DateLeft`="', DATE_FORMAT(deleteddate, '%Y-%m-%d'), '", `DateDeleted`="', DATE_FORMAT(NOW(), '%Y-%m-%d'), '"  WHERE HocSinhId IN (', hocsinhids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteKhoiTruongByKhoiId`(IN `khoi` INT)
    MODIFIES SQL DATA
DELETE FROM khoi_truong WHERE KhoiId = khoi$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteLopKhoiByLopId`(IN `lop` INT)
    MODIFIES SQL DATA
DELETE FROM lop_khoi WHERE LopId = lop$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteViewBanGiaoTaiSan`(IN `taisanlopid` INT)
    MODIFIES SQL DATA
UPDATE `taisan_lop` SET `taisan_lop`.`NgayHetHan`=NOW() WHERE `taisan_lop`.`TaiSanLopId`=taisanlopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `deleteViewTaiSan`(IN `taisanid` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

SET phieuchiid := (SELECT ts.PhieuChiId FROM TaiSan ts WHERE ts.TaiSanId=taisanid);


DELETE FROM `phieuchi` WHERE `phieuchi`.`PhieuChiId`=phieuchiid;

DELETE FROM `TaiSan` WHERE `TaiSan`.`TaiSanId`=taisanid;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getBangThuTienKhoanThuByBangThuTienIds`(IN `bangthutienids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `bangthutien_khoanthu` WHERE 
                  `BangThuTienId` IN (', bangthutienids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhByLopAndNgay`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
SELECT h.*
FROM hocsinh h INNER JOIN hocsinh_lop hl ON h.HocSinhId = hl.HocSinhId
WHERE (lop IS NULL OR hl.LopId = lop) AND DateJoin <= ngay AND (DateLeft IS NULL OR DateLeft >= ngay)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhByParams`(IN `ngay` DATE, IN `khoihoc` INT, IN `lophoc` INT, IN `ngayvaolop` DATE)
    READS SQL DATA
SELECT h.*
FROM hocsinh h LEFT JOIN hocsinh_lop hl ON h.HocSinhId = hl.HocSinhId
    LEFT JOIN lop_khoi lk ON hl.LopId = lk.LopId
WHERE (khoihoc IS NULL OR lk.KhoiId = khoihoc)
    AND (lophoc IS NULL OR hl.LopId = lophoc)
    AND (ngay IS NULL OR hl.DateJoin <= ngay)
    AND (ngay IS NULL OR hl.DateLeft IS NULL OR hl.DateLeft > ngay)
    AND (ngayvaolop IS NULL OR hl.DateJoin = ngayvaolop)
    AND (ngayvaolop IS NULL OR hl.DateLeft IS NULL OR hl.DateLeft > ngayvaolop)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getHocSinhLopByHocSinhIdsAndNgay`(IN `hocsinhids` TEXT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `hocsinh_lop` WHERE `HocSinhId` IN (',hocsinhids, ') AND DateJoin <="', DATE_FORMAT(ngay, '%Y-%m-%d'),'" AND (DateLeft IS NULL OR DateLeft > "', DATE_FORMAT(ngay, '%Y-%m-%d'),'")');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoanThuByIds`(IN `khoanthuids` TEXT)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `khoanthu` WHERE 
                  `KhoanThuId` IN (', khoanthuids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoanThuHangNamByParams`(IN `khoanthuids` TEXT, IN `khoi` INT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT * FROM `khoanthuhangnam` WHERE `KhoiId` = ',khoi, ' AND `KhoanThuId` IN (', khoanthuids, ') AND NgayTinh <="', DATE_FORMAT(ngay, '%Y-%m-%d'),'" AND (NgayKetThuc IS NULL OR NgayKetThuc > "', DATE_FORMAT(ngay, '%Y-%m-%d'),'")');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getKhoiTruongByKhoiId`(IN `khoi` INT)
    READS SQL DATA
SELECT *
FROM khoi_truong
WHERE KhoiId=khoi$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getLopKhoiByLopId`(IN `lop` INT)
    READS SQL DATA
SELECT *
FROM lop_khoi
WHERE LopId=lop$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiById`(IN `phieuchiid` INT)
    NO SQL
SELECT * FROM `phieuchi` pc WHERE pc.`PhieuChiId`=phieuchiid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiListForReportBCHDTC`(IN `tungay` DATE, IN `denngay` DATE, IN `phanloaichiids` TEXT)
    NO SQL
BEGIN

IF (phanloaichiids = "") THEN

SET @sql = CONCAT('SELECT `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`, `plc`.`MaPhanLoai`, SUM(`pc`.`SoTien`) AS `SoTien`
FROM `phanloaichi` plc INNER JOIN `phieuchi` pc ON plc.`PhanLoaiChiId`=pc.`PhanLoaiChiId`
WHERE pc.`Ngay`>="',DATE_FORMAT(tungay, '%Y-%m-%d'),'" AND pc.`Ngay`<="',DATE_FORMAT(denngay, '%Y-%m-%d'),'" GROUP BY `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`');

ELSE

SET @sql = CONCAT('SELECT `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`,`plc`.`MaPhanLoai`, SUM(`pc`.`SoTien`) AS `SoTien`
FROM `phanloaichi` plc INNER JOIN `phieuchi` pc ON plc.`PhanLoaiChiId`=pc.`PhanLoaiChiId`
WHERE pc.`Ngay`>="',DATE_FORMAT(tungay, '%Y-%m-%d'),'" AND pc.`Ngay`<="',DATE_FORMAT(denngay, '%Y-%m-%d'),'" AND  pc.`PhanLoaiChiId` IN(',phanloaichiids, ') GROUP BY `pc`.`PhanLoaiChiId`, `plc`.`DienGiai`');

END IF;


PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuByHocSinhIdAndNgayTinh`(IN `hocsinhid` INT, IN `ngaytinh` DATE)
    NO SQL
SELECT pt.*

FROM `phieuthu` pt

WHERE (hocsinhid < 0 OR pt.HocSinhId=hocsinhid) AND YEAR(pt.Ngay)=YEAR(ngaytinh) AND MONTH(pt.Ngay)=MONTH(ngaytinh)

ORDER BY pt.Ngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuThuForReportBCHDTC`(IN `tungay` DATE, IN `denngay` DATE)
    NO SQL
SELECT SUM(`pt`.`SoTien`) AS `SoTien`
FROM `phieuthu` pt
WHERE pt.`Ngay`>=tungay AND pt.`Ngay`<=denngay$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSoNgayNghiThangByNgayAndHocSinhIds`(IN `hocsinhids` TEXT, IN `ngay` DATE)
    NO SQL
BEGIN

SET @sql = CONCAT('SELECT HocSinhId, SoNgayNghiThang FROM `viewhoctap` WHERE MONTH(NgayTinh)=', MONTH(ngay), ' AND YEAR(NgayTinh)=', YEAR(ngay), ' AND HocSinhId IN (', hocsinhids, ')');

PREPARE stmt FROM @sql;

EXECUTE stmt;

DEALLOCATE PREPARE stmt;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getSoTienTruyThuByHocSinhId`(IN `hocsinhid` INT)
    NO SQL
SELECT btt.`SoTienTruyThu` FROM `bangthutien` btt WHERE btt.HocSinhId=hocsinhid ORDER BY `NgayTinh` DESC LIMIT 1$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBanGiaoTaiSanByLopId`(IN `lopid` INT)
    NO SQL
SELECT *
FROM ViewBanGiaoTaiSan vbgts
WHERE vbgts.LopId=lopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBanGiaoTaiSanByYear`(IN `year` INT)
    NO SQL
SELECT * FROM `viewbangiaotaisan` vbgts WHERE (vbgts.LopId IS NOT NULL) AND YEAR(vbgts.NgayBanGiao)=year
ORDER BY vbgts.Ten, vbgts.TaiSanId, vbgts.NgayBanGiao$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewBangThuTienByNgayTinhAndLop`(IN `ngay` DATE, IN `lop` INT)
    NO SQL
SELECT *
FROM viewbangthutien
WHERE YEAR(`NgayTinh`) = YEAR(ngay) AND MONTH(`NgayTinh`) = MONTH(ngay) AND (lop IS NULL OR `LopId` = lop)
ORDER BY `LopId`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewHocTapByLopAndNgay`(IN `lop` INT, IN `ngay` DATE)
    NO SQL
BEGIN


IF NOT EXISTS(SELECT * FROM `baocaothanggenhistory` bccgh WHERE bccgh.`NgayTinh`=ngay AND bccgh.`LopId`=lop) THEN

  INSERT INTO `baocaothanggenhistory`(`LopId`, `NgayTinh`) VALUES (lop, ngay);

  INSERT INTO `baocaothang`(`NgayTinh`, `HocTapId`, `SoNgayNghi`) (
    SELECT ngay AS `NgayTinh`, ht.HocTapId, 0 AS `SoNgayNghi`
    FROM `hoctap` ht INNER JOIN `hocsinh_lop` hsl ON ht.HocSinhLopId=hsl.HocSinhLopId
    WHERE hsl.LopId = lop AND ngay >= hsl.DateJoin AND (hsl.DateLeft IS NULL OR hsl.DateLeft > ngay)
  );

END IF;


SELECT * FROM `viewhoctap`
WHERE LopId = lop AND (ngay IS NULL OR (ngay >= DateJoin AND (DateLeft IS NULL OR DateLeft > ngay))) AND (`NgayTinh` IS NULL OR `NgayTinh` = ngay);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getViewTaiSanForBanGiaoTaiSan`()
    NO SQL
SELECT `TaiSanId`, `Ten`, `NgayNhap`, `DonViTinh`, `DonGia`, `SoChungTu`, `NgayChungTu`, `ThanhTien`, `PhieuChiId`, `PhanLoaiChiId`, SUM(`SoLuong`) AS SoLuong

FROM `viewtaisanforbangiaotaisan`

GROUP BY `TaiSanId`

HAVING SoLuong > 0$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertHocSinhToLop`(IN `hocsinh` INT, IN `lop` INT, IN `ngayvaolop` DATE)
    MODIFIES SQL DATA
BEGIN

UPDATE `hocsinh_lop` SET `DateLeft` = ngayvaolop, `DateDeleted` = NOW() WHERE `HocSinhId` = hocsinh AND `DateLeft` IS NULL;

IF NOT EXISTS(
    SELECT * FROM `hocsinh_lop` WHERE `HocSinhId` = hocsinh AND `LopId` = lop AND `DateJoin` = ngayvaolop AND `DateLeft` IS NULL) THEN
    INSERT INTO `hocsinh_lop`(`HocSinhId`, `LopId`, `DateJoin`) VALUES (hocsinh, lop, ngayvaolop);
END IF;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertViewBanGiaoTaiSai`(IN `taisanid` INT, IN `lopid` INT, IN `ngaybangiao` DATE, IN `ngayhethan` DATE, IN `ghichu` TEXT CHARSET utf8, IN `soluongbangiao` DOUBLE)
    MODIFIES SQL DATA
INSERT INTO `taisan_lop`(`taisan_lop`.`TaiSanId`, `taisan_lop`.`LopId`, `taisan_lop`.`NgayBanGiao`, `taisan_lop`.`NgayHetHan`, `taisan_lop`.`GhiChu`, `taisan_lop`.`SoLuong`) VALUES (taisanid, lopid, ngaybangiao, ngayhethan, ghichu,soluongbangiao)$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `insertViewTaiSan`(IN `ten` TEXT, IN `ngaynhap` DATE, IN `donvitinh` TEXT, IN `soluong` INT, IN `dongia` BIGINT, IN `thanhtien` BIGINT, IN `sochungtu` TEXT, IN `ngaychungtu` DATE, IN `phanloaichi` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

INSERT INTO `phieuchi`(`Ngay`, `SoTien`, `PhanLoaiChiId`) VALUES (ngaynhap, thanhtien, phanloaichi);

SET phieuchiid := LAST_INSERT_ID();

INSERT INTO `taisan`(`Ten`, `DonViTinh`, `SoLuong`, `DonGia`, `SoChungTu`, `NgayChungTu`, `PhieuChiId`) VALUES (ten, donvitinh, soluong, dongia, sochungtu, ngaychungtu, phieuchiid);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `selectViewBanGiaoTaiSan`()
    NO SQL
SELECT `TaiSanId`, `Ten`, `DonViTinh`, `SoLuong`, `DonGia`, `SoChungTu`, `NgayChungTu`, `PhieuChiId`, `LopId`, `NgayBanGiao`, `NgayHetHan`, `GhiChu`, `TaiSanLopId`, `SoLuongBanGiao`, `NgayNhap` FROM `viewbangiaotaisan` WHERE `NgayHetHan` IS NULL OR `NgayHetHan`> NOW()$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateSoTienTruyThuByHocSinhAndNgayUpdated`(IN `hocsinhid` INT, IN `ngayupdated` DATE, IN `sotientruythuadded` BIGINT)
    MODIFIES SQL DATA
UPDATE `bangthutien` SET `bangthutien`.`SoTienTruyThu`=`bangthutien`.`SoTienTruyThu`+sotientruythuadded WHERE `bangthutien`.`HocSinhId`=hocsinhid AND `bangthutien`.`NgayTinh`>ngayupdated$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewBanGiaoTaiSan`(IN `taisanlopid` INT, IN `lopid` INT, IN `ngaybangiao` DATE, IN `ngayhethan` DATE, IN `ghichu` TEXT CHARSET utf8, IN `soluongbangiao` DOUBLE)
    MODIFIES SQL DATA
UPDATE `taisan_lop` SET `taisan_lop`.`LopId`=lopid,`taisan_lop`.`NgayBanGiao`=ngaybangiao,`taisan_lop`.`NgayHetHan`=ngayhethan,`taisan_lop`.`GhiChu`=ghichu, `taisan_lop`.`SoLuong`=soluongbangiao WHERE `taisan_lop`.`TaiSanLopId`=taisanlopid$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewHocTap`(IN `hoctapid` INT, IN `hocsinhlopid` INT, IN `songaynghithang` INT, IN `ngay` DATE)
    NO SQL
BEGIN

DECLARE _hoctapid INT DEFAULT 0;

IF (hoctapid IS NULL) THEN
  INSERT INTO `hoctap`(`HocSinhLopId`) VALUES (hocsinhlopid);
  SET _hoctapid := LAST_INSERT_ID();

ELSE

  SET _hoctapid := hoctapid;

END IF;



IF NOT EXISTS(
  SELECT * FROM `baocaothang` WHERE `baocaothang`.`HocTapId`=_hoctapid AND `baocaothang`.`NgayTinh`=ngay ) THEN

  INSERT INTO `baocaothang`(`HocTapId`, `NgayTinh`, `SoNgayNghi`) VALUES (_hoctapid, ngay, songaynghithang);

ELSE

  UPDATE `baocaothang` SET `baocaothang`.`SoNgayNghi`=songaynghithang WHERE `baocaothang`.`HocTapId`=_hoctapid AND `baocaothang`.`NgayTinh`=ngay;

END IF;

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `updateViewTaiSan`(IN `ten` TEXT, IN `ngaynhap` DATE, IN `donvitinh` TEXT, IN `soluong` INT, IN `dongia` BIGINT, IN `thanhtien` BIGINT, IN `sochungtu` TEXT, IN `ngaychungtu` DATE, IN `phanloaichi` INT, IN `taisanid` INT)
    MODIFIES SQL DATA
BEGIN

DECLARE phieuchiid INT DEFAULT 0;

SET phieuchiid := (SELECT ts.PhieuChiId FROM TaiSan ts WHERE ts.TaiSanId=taisanid);

UPDATE `phieuchi` SET `phieuchi`.`Ngay`=ngaynhap,`phieuchi`.`SoTien`=thanhtien,`phieuchi`.`PhanLoaiChiId`=phanloaichi WHERE `phieuchi`.`PhieuChiId`=phieuchiid;

UPDATE `taisan` SET `taisan`.`Ten`=ten,`taisan`.`DonViTinh`=donvitinh,`taisan`.`SoLuong`=soluong,`taisan`.`DonGia`=dongia,`taisan`.`SoChungTu`=sochungtu,`taisan`.`NgayChungTu`=ngaychungtu WHERE `taisan`.`TaiSanId`=taisanid;

END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `bangthutien`
--

CREATE TABLE IF NOT EXISTS `bangthutien` (
  `BangThuTienId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `SXThangTruoc` int(11) DEFAULT NULL,
  `SoTienSXThangTruoc` bigint(20) DEFAULT NULL,
  `AnSangThangTruoc` int(11) DEFAULT NULL,
  `SoTienAnSangThangTruoc` bigint(20) DEFAULT NULL,
  `SoTienAnSangThangNay` bigint(20) NOT NULL,
  `SoTienAnToiThangTruoc` bigint(20) DEFAULT NULL,
  `AnToiThangTruoc` int(11) NOT NULL,
  `SoTienAnToiThangNay` bigint(20) NOT NULL,
  `SoTienDoDung` bigint(20) DEFAULT '0',
  `SoTienNangKhieu` bigint(20) DEFAULT '0',
  `SoTienTruyThu` bigint(20) DEFAULT '0',
  `SoTienDieuHoa` bigint(20) DEFAULT '0',
  `NgayTinh` date NOT NULL,
  `STT` int(11) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `GhiChu` text COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`BangThuTienId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=143 ;

-- --------------------------------------------------------

--
-- Table structure for table `bangthutiengenhistory`
--

CREATE TABLE IF NOT EXISTS `bangthutiengenhistory` (
  `BTTGenHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `NgayTinh` date NOT NULL,
  `LopId` int(11) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`BTTGenHistoryId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=39 ;

-- --------------------------------------------------------

--
-- Table structure for table `bangthutien_khoanthu`
--

CREATE TABLE IF NOT EXISTS `bangthutien_khoanthu` (
  `BTTKTId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoanThuId` int(11) NOT NULL,
  `BangThuTienId` int(11) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  PRIMARY KEY (`BTTKTId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=665 ;

-- --------------------------------------------------------

--
-- Table structure for table `bangtinhphi`
--

CREATE TABLE IF NOT EXISTS `bangtinhphi` (
  `BangTinhPhiId` int(11) NOT NULL AUTO_INCREMENT,
  `SoNgayNghiMin` int(11) NOT NULL,
  `SoNgayNghiMax` int(11) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `KhoanThuId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  PRIMARY KEY (`BangTinhPhiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=37 ;

--
-- Dumping data for table `bangtinhphi`
--

INSERT INTO `bangtinhphi` (`BangTinhPhiId`, `SoNgayNghiMin`, `SoNgayNghiMax`, `SoTien`, `KhoanThuId`, `KhoiId`) VALUES
(1, 6, 8, 105000, 2, 1),
(2, 9, 12, 105000, 2, 1),
(3, 13, 18, 79000, 2, 1),
(4, 6, 8, 173000, 3, 1),
(5, 9, 12, 173000, 3, 1),
(6, 13, 18, 130000, 3, 1),
(7, 6, 8, 390000, 4, 1),
(8, 9, 12, 260000, 4, 1),
(9, 13, 18, 195000, 4, 1),
(10, 6, 8, 105000, 2, 2),
(11, 9, 12, 105000, 2, 2),
(12, 13, 18, 79000, 2, 2),
(13, 6, 8, 173000, 3, 2),
(14, 9, 12, 173000, 3, 2),
(15, 13, 18, 130000, 3, 2),
(16, 6, 8, 390000, 4, 2),
(17, 9, 12, 260000, 4, 2),
(18, 13, 18, 195000, 4, 2),
(19, 6, 8, 122000, 2, 3),
(20, 9, 12, 122000, 2, 3),
(21, 13, 18, 91500, 2, 3),
(22, 6, 8, 177000, 3, 3),
(23, 9, 12, 177000, 3, 3),
(24, 13, 18, 132500, 3, 3),
(25, 6, 8, 410000, 4, 3),
(26, 9, 12, 273000, 4, 3),
(27, 13, 18, 205000, 4, 3),
(28, 6, 8, 132000, 2, 4),
(29, 9, 12, 132000, 2, 4),
(30, 13, 18, 9900, 2, 4),
(31, 6, 8, 187000, 3, 4),
(32, 9, 12, 187000, 3, 4),
(33, 13, 18, 140000, 3, 4),
(34, 6, 8, 430000, 4, 4),
(35, 9, 12, 287000, 4, 4),
(36, 13, 18, 215000, 4, 4);

-- --------------------------------------------------------

--
-- Table structure for table `baocaothang`
--

CREATE TABLE IF NOT EXISTS `baocaothang` (
  `BaoCaoThangId` int(11) NOT NULL AUTO_INCREMENT,
  `NgayTinh` date NOT NULL,
  `HocTapId` int(11) NOT NULL,
  `SoNgayNghi` int(11) NOT NULL,
  PRIMARY KEY (`BaoCaoThangId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=32 ;

-- --------------------------------------------------------

--
-- Table structure for table `baocaothanggenhistory`
--

CREATE TABLE IF NOT EXISTS `baocaothanggenhistory` (
  `BCTGenHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `LopId` int(11) NOT NULL,
  `NgayTinh` date NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`BCTGenHistoryId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=8 ;

-- --------------------------------------------------------

--
-- Table structure for table `hocsinh`
--

CREATE TABLE IF NOT EXISTS `hocsinh` (
  `HocSinhId` int(11) NOT NULL AUTO_INCREMENT,
  `HoDem` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `Ten` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `GioiTinh` tinyint(4) NOT NULL,
  `NgaySinh` date DEFAULT NULL,
  `NgayVaoLop` date DEFAULT NULL,
  `DanToc` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienCuTru` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienThoai` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoTenCha` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoTenMe` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DienThoaiMe` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TinhThanhPhoId` int(11) DEFAULT NULL,
  `QuanHuyenId` int(11) DEFAULT NULL,
  `PhuongXaId` int(11) DEFAULT NULL,
  `DiaChi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ThoiHoc` tinyint(1) NOT NULL DEFAULT '0',
  `CreatedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`HocSinhId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=201 ;

--
-- Dumping data for table `hocsinh`
--

INSERT INTO `hocsinh` (`HocSinhId`, `HoDem`, `Ten`, `GioiTinh`, `NgaySinh`, `NgayVaoLop`, `DanToc`, `DienCuTru`, `DienThoai`, `HoTenCha`, `HoTenMe`, `DienThoaiMe`, `TinhThanhPhoId`, `QuanHuyenId`, `PhuongXaId`, `DiaChi`, `ThoiHoc`, `CreatedDate`) VALUES
(1, 'Đặng Minh', 'Huy', 1, '2014-02-07', '2017-01-01', '', '', '0906481753', 'Đặng Văn Dũng', 'Dg T Kim Nhanh', '0905319793', 1, 3, 26, '46 Nguyễn Thị Định', 0, '2017-06-05 00:00:00'),
(2, 'Lê Hồ Bảo', 'Quyên', 0, '2014-06-10', '2017-01-01', '', '', '0935810231', 'Lê Thanh Hải', 'Hồ Thị Hằng', '0905535663', 1, 3, 28, '21 Hồ Sỉ Tân', 0, '2017-06-05 00:00:00'),
(3, 'Nguyễn Tuấn', 'Kiệt', 1, '2014-05-26', '2017-01-01', '', '', '0905144055', 'Nguyễn Duy Mạnh', 'Nguyễn Thị Oanh', '0905594908', 1, 3, 26, '864 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(4, 'Mai Xuân', 'Phúc', 1, '2014-03-22', '2017-01-01', '', '', '01234258477', 'Mai Xuân Ước', 'Ngô Thị Sa', '0935144005', 1, 3, 26, '69 An Tân', 0, '2017-06-05 00:00:00'),
(5, 'Bùi Quỳnh', 'Hân', 0, '2014-06-17', '2017-01-01', '', '', '0935987897', 'Bùi Văn Vũ', 'Đặng Thị Lai', '0935709735', 1, 3, 26, '22 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(6, 'Đặng Hồng', 'Ngân', 0, '2014-05-14', '2017-01-01', '', '', '0905044288', 'Đặng  Văn Xin', 'Mai Thị thành', '0914083434', 1, 3, 26, '12 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(7, 'Ngô Thị Ngọc', 'Thy', 0, '2014-11-11', '2017-01-01', '', '', '0905877616', 'Ngô Văn Thương', 'Hà H.T Việt Trân', '0935303611', 1, 3, 26, '23 Nguyễn Thế Lộc', 0, '2017-06-05 00:00:00'),
(8, 'Đoàn Huy', 'Hoàng', 1, '2015-05-27', '2017-01-01', '', '', '0935345036', 'Đoàn Văn Trí', 'Nguyễn Thị Hường', '01214261982', 1, 3, 28, '48 Hoàng Quốc Việt', 0, '2017-06-05 00:00:00'),
(9, 'Lê Phạm Công', 'Hiếu', 1, '2014-02-03', '2017-01-01', '', '', '01227473223', 'Lê Vũ', 'Võ T Kim Hoa', '01224581359', 1, 3, 28, 'Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(10, 'Ngô Minh', 'Huy', 1, '2014-09-07', '2017-01-01', '', '', '0906153612', 'Ngô Xuân Phúc', 'Huỳnh Thi Lan', '0935804811', 1, 3, 26, 'Tổ 65 An Tân', 0, '2017-06-05 00:00:00'),
(11, 'Bùi Ng Duy', 'Độ', 1, '2015-04-27', '2017-01-01', '', '', '0905198175', 'Bùi Duy Độ', 'Nguyễn Thị Em', '01289160789', 1, 3, 26, '50 An Hải 18', 0, '2017-06-05 00:00:00'),
(12, 'Lê Thảo', 'Vy', 0, '2015-08-05', '2017-01-01', '', '', '', 'Lê Tiến Phong', 'Huỳnh T K Tuyết', '0905162827', 1, 3, 28, 'Tổ 1 Nại Tú 1', 0, '2017-06-05 00:00:00'),
(13, 'Mai Xuân', 'Phú', 1, '2015-04-16', '2017-01-01', '', '', '0905336090', 'Mai Xuân Tuấn', 'Võ T Mỹ Phương', '01202334489', 1, 3, 26, '103 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(14, 'Phùng Minh', 'Phúc', 1, '2015-04-16', '2017-01-01', '', '', '0986997975', 'Phùng Ngọc Minh', 'Ng Bá M Phương', '01202334489', 1, 3, 26, '06 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(15, 'Ng Lê Uyên', 'Nhi', 0, '2014-12-18', '2017-01-01', '', '', '0905339750', 'Nguyễn Mạnh Việt', 'Lê T Bích Trâm', '0905385581', 1, 3, 24, '03 Nại Thịnh 3', 0, '2017-06-05 00:00:00'),
(16, 'Phan H. Khánh', 'Phong', 1, '2015-06-16', '2017-01-01', '', '', '0935631647', 'Phan Thanh Hải', 'Ngô Thị T Hồng', '0906446543', 1, 3, 28, '81 Nghĩa Địa Lê', 0, '2017-06-05 00:00:00'),
(17, 'Trần Ngô Minh', 'Nhật', 1, '2015-07-09', '2017-01-01', '', '', '', 'Trần Văn Lợi', 'Lê T P Thanh', '0905487175', 1, 3, 28, '146 Lý Đạo Thành', 0, '2017-06-05 00:00:00'),
(18, 'Lê Nguyễn Hà', 'My', 0, '2015-07-23', '2017-01-01', '', '', '', 'Lê Văn Tốt', 'Nguyễn Thị Nguyệt', '0924056980', 1, 3, 26, '88 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(19, 'Trần Mộc', 'Miên', 0, '2015-11-10', '2017-01-01', '', '', '0901151525', 'Trần Trung Hậu', 'Huỳnh T B Trâm', '0905936529', 1, 3, 28, 'Tổ 2 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(20, 'Nguyễn Công', 'Khanh', 1, '2015-10-22', '2017-01-01', '', '', '0905041016', 'Nguyễn Minh Trung', 'Hoàng Thị Thu', '01262654894', 1, 3, 28, '81 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(21, 'Hồ H. Thanh', 'Yến', 0, '2011-04-09', '2017-01-01', '', '', '905787034', 'Hồ Mã Hoàng', 'Hoàng Thị', '1264618619', 1, 3, 26, 'Tổ 10', 0, '2017-06-05 00:00:00'),
(22, 'Trần Ng Tuấn', 'Huy', 1, '2011-09-22', '2017-01-01', '', '', '906446281', 'Trần Văn Quy', 'Nguyễn Thị Hồng', '932483380', 1, 3, 28, 'Chung Cư A1', 0, '2017-06-05 00:00:00'),
(23, 'Lê Ng. Ngọc', 'Ly', 0, '2011-09-14', '2017-01-01', '', '', '9635692269', 'Lê Toàn', 'Ng T.Bích Hòa', '905459827', 1, 3, 28, 'Tổ 7', 0, '2017-06-05 00:00:00'),
(24, 'Dương Thiên', 'Bảo', 1, '2011-01-01', '2017-01-01', '', '', '0935721043', 'Dg  Đình Thanh', 'Lê T.Bích Tuân', '01283670788', 1, 3, 28, 'Tô 2 Trần Q. Toản', 0, '2017-06-05 00:00:00'),
(25, 'Bùi Văn', 'Thiện', 1, '2011-06-25', '2017-01-01', '', '', '0905344347', 'Bùi Văn Nở', 'Lê Thị Tơ', '012569886077', 1, 3, 28, 'Địa Bảo', 0, '2017-06-05 00:00:00'),
(26, 'Ng. T Anh', 'Dương', 1, '2011-01-11', '2017-01-01', '', '', '0905989860', 'Ng Văn Thiện', 'Võ Thị Kim Huệ', '01222475655', 1, 3, 28, 'Trần Hưng Đạo', 0, '2017-06-05 00:00:00'),
(27, 'Vũ Đăng', 'Khoa', 1, '2011-07-10', '2017-01-01', '', '', '', '', 'Võ T.Kim Hiếu', '0905324488', 1, 3, 28, 'Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(28, 'Đỗ Thu', 'Phương', 0, '2012-01-28', '2017-01-01', '', '', '983770444', 'Đỗ Văn Tiến', 'Trần T Hải Yên', '922911592', 1, 3, 26, '42 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(29, 'Phạm Ng Kỳ', 'Thư', 0, '2011-06-21', '2017-01-01', '', '', '935690234', 'Phạm Duy Kỳ', 'nguyễn Thi Vân', '936702123', 1, 3, 28, 'P510 Kcc Sunhome', 0, '2017-06-05 00:00:00'),
(30, 'Hồ Thanh', 'Thảo', 0, '2012-03-21', '2017-01-01', '', '', '935762068', 'Hồ Vinh', 'Đặng T Hồng Ánh', '905849601', 1, 3, 26, '336 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(31, 'Ngô Bảo', 'Trân', 0, '2011-08-12', '2017-01-01', '', '', '0902205245', 'Ngô Văn Mạnh', 'Đỗ Thị Mỹ Dung', '0905859287', 1, 3, 26, 'Tổ 29 An Hải 10', 0, '2017-06-05 00:00:00'),
(32, 'Ngô Anh', 'Tiến', 1, '2012-08-16', '2017-01-01', '', '', '', 'Ngô V Th Tuấn', 'Võ Thi Kim Anh', '934940410', 1, 3, 28, 'Tổ 24 Dg Văn Nga', 0, '2017-06-05 00:00:00'),
(33, 'Đặng Trần Nhật', 'Minh', 1, '2012-07-07', '2017-01-01', '', '', '', 'Đg Ngọc Thoại', 'Trần T Hồng Hạnh', '964547077', 1, 3, 26, 'Tổ 21 An Đồn', 0, '2017-06-05 00:00:00'),
(34, 'Nguyễn T Thùy', 'Trâm', 0, '2012-11-15', '2017-01-01', '', '', '932490442', 'Ng Thành Thái', 'Lê Thị Hòa', '934812254', 1, 3, 26, '34 Ng Thị Định', 0, '2017-06-05 00:00:00'),
(35, 'Nguyễn Huỳnh', 'Giao', 0, '2012-08-19', '2017-01-01', '', '', '0905626994', 'Nguyễn Tấn', 'Huỳnh Thị Đào', '0908983904', 1, 3, 30, 'Tổ 10 Thọ Quang', 0, '2017-06-05 00:00:00'),
(36, 'Mai Hoàng', 'Vân', 0, '2011-05-18', '2017-01-01', '', '', '01262614303', 'Mai Văn Dũng', 'Huỳnh Thị Lệ', '', 1, 3, 28, 'Đại Địa Bảo- vũng thùng', 0, '2017-06-05 00:00:00'),
(37, 'Ngô Tr.Phương', 'Uyên', 0, '2011-09-07', '2017-01-01', '', '', '982910673', 'Ngô Minh Thân', 'Trịnh Thị Bình', '988788126', 1, 3, 28, 'Tổ 2', 0, '2017-06-05 00:00:00'),
(38, 'Ng . T. Hoàng', 'Ngân', 0, '2011-04-04', '2017-01-01', '', '', '912132191', 'Ng. Văn Thương', 'Ng.T.Thanh Thanh', '918504314', 1, 3, 28, '72 Nguyễn Chí Diễu', 0, '2017-06-05 00:00:00'),
(39, 'Lê Văn', 'Hiếu', 1, '2011-07-27', '2017-01-01', '', '', '0989437137', 'Lê Văn Hải', 'Đặng Thị Tuyết', '', 1, 3, 28, 'Tổ 135 Kc cư làng cá', 0, '2017-06-05 00:00:00'),
(40, 'Nguyễn Anh', 'Kiệt', 1, '2011-02-09', '2017-01-01', '', '', '1289160355', 'Nguyễn Văn Lên', 'Nguyễn Thị Ny Na', '01227993036', 1, 3, 28, 'Tổ 145 Kc cư làng cá', 0, '2017-06-05 00:00:00'),
(41, 'Lê T Thanh', 'Hằng', 0, '2011-10-02', '2017-01-01', '', '', '0946927853', 'Lê Văn Giang', 'Nguyễn Thị Bé', '', 1, 3, 28, 'Tổ 145 Kc cư làng cá', 0, '2017-06-05 00:00:00'),
(42, 'Đặng Ngọc Gia', 'Bảo', 1, '2011-02-23', '2017-01-01', '', '', '01205969490', 'Đặng Văn Mỹ', 'Trần Thị Hương', '0905291643', 1, 3, 26, 'Số 20 An Hải 11', 0, '2017-06-05 00:00:00'),
(43, 'Phan Lê Gia', 'Vũ', 1, '2011-07-31', '2017-01-01', '', '', '0975285488', 'Phan Văn Phúc', 'Lê Thị Kim Hạnh', '0925989148', 1, 3, 28, '157 Hoa Lư', 0, '2017-06-05 00:00:00'),
(44, 'Nguyễn Trung', 'Kiên', 1, '2011-09-04', '2017-01-01', '', '', '905072447', 'Ng. Mạnh Tiến', 'Ng.Thị Tho', '986884927', 1, 3, 26, '42 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(45, 'Nguyễn Nhất', 'Win', 1, '2011-07-01', '2017-01-01', '', '', '01638421713', 'Ng Đức Nam', 'Phạm Thị Chín', '0905755784', 1, 3, 28, 'Tổ 37', 0, '2017-06-05 00:00:00'),
(46, 'Mai Lê Thanh', 'Nam', 1, '2012-01-29', '2017-01-01', '', '', '0905678783', 'Mai Thanh Việt', 'Lê Thị Đào', '0905016591', 1, 3, 28, 'Tổ 19 Bùi Lam', 0, '2017-06-05 00:00:00'),
(47, 'Lê Văn Khải', 'Minh', 1, '2012-06-16', '2017-01-01', '', '', '0935747412', 'Lê Văn Thương', 'Ng T Kim Huệ', '01276589683', 1, 3, 28, 'Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(48, 'Lê Thị Như', 'Quỳnh', 0, '2012-04-17', '2017-01-01', '', '', '932521932', 'Lê Văn Sử', 'Ngô T Thu Thanh', '', 1, 3, 28, 'K ccu làng Cá', 0, '2017-06-05 00:00:00'),
(49, 'Huỳnh Ngọc Anh', 'Thư', 0, '2020-12-11', '2017-01-01', '', '', '982220620', 'H Ngọc Trung', '', '1265381048', 1, 3, 28, '40 Đào Duy Kỳ', 0, '2017-06-05 00:00:00'),
(50, 'Nguyễn', 'Lợi', 1, '2011-05-19', '2017-01-01', '', '', '1655461466', 'Nguyễn Mỹ', 'Lưu T.Hồng Thắm', '905300212', 1, 3, 28, '56 Nguyễn Hiền', 0, '2017-06-05 00:00:00'),
(51, 'Đặng Ngọc', 'Vĩ', 1, '2012-07-28', '2017-01-01', '', '', '905339113', 'Đặng Ngọc Trí', 'Trần Thị Trí', '905954550', 1, 3, 28, '05 Nại Thịnh 2', 0, '2017-06-05 00:00:00'),
(52, 'Ng Đức Anh', 'Khoa', 1, '2011-03-09', '2017-01-01', '', '', '', 'Nguyễn A Tuấn', 'Hồ Thị Mùi', '935158882', 1, 1, 22, 'Tổ 22 Hải Châu 2', 0, '2017-06-05 00:00:00'),
(53, 'Nguyễn Quý', 'Lâm', 1, '2011-02-17', '2017-01-01', '', '', '935282515', 'Nguyễn Văn Nở', 'Huỳnh Thị Mười', '932452153', 1, 3, 28, '48 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(54, 'Lê Đặng Gia', 'Hân', 0, '2012-08-05', '2017-01-01', '', '', '1225427077', 'Lê Văn Núi', 'ĐặngTThúy Cương', '1219342630', 1, 3, 26, '11 Bùi Lâm', 0, '2017-06-05 00:00:00'),
(55, 'Lê Ng Hạnh', 'Nguyên', 1, '2012-06-18', '2017-01-01', '', '', '935100790', 'Lê Thiện Tín', 'Nguyễn Thị Thúy', '935620668', 1, 3, 26, 'K 37/41', 0, '2017-06-05 00:00:00'),
(56, 'Trần Minh', 'Khôi', 1, '2011-01-12', '2017-01-01', '', '', '', 'Trần Sương', 'Trần Thị Hồng', '', 1, 3, 26, 'Tổ 35 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(57, 'Ng Thạc Hoàng', 'Anh', 0, '2011-01-27', '2017-01-01', '', '', '978324455', 'Ng Thạc Văn', 'Nguyễn Thị Biên', '978315455', 1, 3, 26, 'P 512 CC sunhome', 0, '2017-06-05 00:00:00'),
(58, 'Võ Kim Khánh', 'Huyền', 0, '2012-02-09', '2017-01-01', '', '', '', 'Võ Bá Phụng', 'Dương Thị Ân', '1266645091', 1, 3, 28, 'Tổ 10 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(59, 'Nguyễn Lê', 'Nguyên', 1, '2013-09-09', '2017-01-01', '', '', '0905542272', 'Nguyễn Thanh Hải', 'Lê Thị Kim Thoa', '01216642626', 1, 3, 28, '54 Võ Trường toản', 0, '2017-06-05 00:00:00'),
(60, 'Trần T Thúy', 'Kiều', 0, '2013-04-07', '2017-01-01', '', '', '01227907388', 'Điền Nam', 'Trần T .Bé Hà', '01228595919', 1, 3, 28, 'Tổ 144Kcc làng cá', 0, '2017-06-05 00:00:00'),
(61, 'Đỗ Trần L Nhật', 'Minh', 1, '2013-03-04', '2017-01-01', '', '', '0901126299', 'Đỗ Văn Út', 'Lê Thị Ái', '0932451547', 1, 3, 28, '54 Đường Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(62, 'Ng Dương Linh', 'Đan', 0, '2013-11-18', '2017-01-01', '', '', '0905126948', 'Nguyễn Tấn Bình', 'Dương T Mỹ Hạnh', '0905381058', 1, 3, 28, 'Tổ 07 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(63, 'Lê Quốc', 'Phước', 1, '2013-05-31', '2017-01-01', '', '', '', 'Lê Quốc Bảo', 'Trần Thị Lê Thủy', '0977882978', 1, 3, 26, '23 Võ  Trường Toản', 0, '2017-06-05 00:00:00'),
(64, 'Nguyễn Thị Anh', 'Thư', 0, '2013-12-23', '2017-01-01', '', '', '0905135757', 'Nguyễn Tuấn Anh', 'Ngô T Kim Bằng', '0975707573', 1, 3, 26, '75 Ngô Trí Hòa', 0, '2017-06-05 00:00:00'),
(65, 'Trần Lê Long', 'Nhật', 1, '2013-01-11', '2017-01-01', '', '', '0905434212', 'Lê Thành', 'Trần Thị Hồng', '0905447024', 1, 3, 28, 'Tổ 152 N Khắc cần', 0, '2017-06-05 00:00:00'),
(66, 'Ng Vũ Hồng', 'Ân', 1, '2013-12-27', '2017-01-01', '', '', '01645070297', 'Nguyễn Phượng', 'Vũ Thị Hương', '01649758648', 1, 3, 28, '09 Trần Thánh Tông', 0, '2017-06-05 00:00:00'),
(67, 'Trần Trung', 'Nghĩa', 1, '2013-04-13', '2017-01-01', '', '', '0905888246', 'Trần Cao Thái Sơn', 'Lê Thị Hà', '0905631278', 1, 3, 28, '89 Ngô Trí Hòa', 0, '2017-06-05 00:00:00'),
(68, 'Lê Minh', 'Quân', 1, '2014-01-10', '2017-01-01', '', '', '1205969450', 'Lê Văn Vương', 'Huỳnh Thị Miêu', '1262715184', 1, 3, 28, 'Tổ 128', 0, '2017-06-05 00:00:00'),
(69, 'Hoàng Phúc', 'Nguyên', 1, '2013-04-04', '2017-01-01', '', '', '0934792815', '', '', '0914288856', 1, 3, 28, '301Kccư A1Vũng Thùng', 0, '2017-06-05 00:00:00'),
(70, 'Trần Ng Anh', 'Thư', 0, '2013-08-08', '2017-01-01', '', '', '905629466', 'Trần Hữu Nghĩa', 'Ng T Cẩm Dung', '122858318', 1, 3, 28, 'P510 Ccu so 4', 0, '2017-06-05 00:00:00'),
(71, 'Trần Thị Khởi', 'My', 0, '2013-11-08', '2017-01-01', '', '', '935760963', 'Trần Phó', 'Ng T N Phương', '1202768917', 1, 3, 28, '10 Nại Nghĩa 4', 0, '2017-06-05 00:00:00'),
(72, 'Ng Huỳnh Linh', 'Đan', 0, '2013-01-27', '2017-01-01', '', '', '0909509579', 'Ng Hữu Ngọc', 'Huỳnh T Tuyết', '0984524480', 1, 3, 28, 'P101KCCA2 Ng Tri Hòa', 0, '2017-06-05 00:00:00'),
(73, 'Nguyễn Bảo', 'Duy', 1, '2013-10-13', '2017-01-01', '', '', '', 'Ng Văn Phước', 'Đặng H Linh', '1863143177', 1, 3, 28, '16 Hoành Quốc Việt', 0, '2017-06-05 00:00:00'),
(74, 'Ngô Như', 'Quỳnh', 0, '2013-12-04', '2017-01-01', '', '', '1222528982', 'Nguyễn Văn Trông', 'Ng Phước AThư', '905582548', 1, 3, 26, '95 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(75, 'Nguyễn Chí', 'Bảo', 1, '2014-11-11', '2017-01-01', '', '', '913132191', 'Nguyễn V Thương', 'Ng T T Thanh', '918504314', 1, 3, 26, '72 Nguyễn Chí Diễu', 0, '2017-06-05 00:00:00'),
(76, 'Trịnh Công', 'Danh', 1, '2015-05-14', '2017-01-01', '', '', '', 'Trịnh Cao Công', 'Huỳnh Thị Bé', '', 1, 3, 28, 'Nguyễn Khắc Cần', 0, '2017-06-05 00:00:00'),
(77, 'Hồ Hoàng', 'Huy', 1, '2014-04-08', '2017-01-01', '', '', '934120467', 'Hồ Văn Qúy', 'Hoàng Thị Lưu', '', 1, 3, 28, '20 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(78, 'Trần H Bảo', 'Trâm', 0, '2015-01-10', '2017-01-01', '', '', '905349623', 'Nguyễn Xuân Tình', 'Lê T Thu Thủy', '935209371', 1, 3, 26, 'K187/08 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(79, 'Vũ Lê Bảo', 'An', 0, '2013-08-04', '2017-01-01', '', '', '978009861', 'Vũ Lê Trung', 'Nguyễn Thị Đào', '1655893650', 1, 3, 29, '23A Trương Định', 0, '2017-06-05 00:00:00'),
(80, 'Huỳnh T Diệu', 'Anh', 0, '2014-03-07', '2017-01-01', '', '', '906119748', 'Huỳnh Văn Mỹ', 'Trần Thị Kỹ', '905258912', 1, 3, 26, '01 An Hải 5', 0, '2017-06-05 00:00:00'),
(81, 'Nguyễn Xuân', 'Pháp', 1, '2013-10-07', '2017-01-01', '', '', '905349623', 'Nguyễn Xuân Tình', 'Lê Thị Thu Thủy', '935209371', 1, 3, 26, '478 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(82, 'Lê Ng Minh', 'Trí', 1, '2014-04-01', '2017-01-01', '', '', '935100790', 'Lê Thiện Tín', 'Nguyễn Thị Thủy', '935620668', 1, 3, 24, 'K37/41 Lương Thế Vinh', 0, '2017-06-05 00:00:00'),
(83, 'Đặng N Quỳnh', 'Nhi', 0, '2014-02-07', '2017-01-01', '', '', '905877687', 'Đặng Duy Tường', 'Đỗ Thị Quỳnh', '905630526', 1, 3, 26, '27 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(84, 'Hồ Ng Ngọc', 'Anh', 0, '2014-01-22', '2017-01-01', '', '', '', '', 'Hồ Thị Vui', '935158882', 1, 3, NULL, 'Lô 37KDC Phan Bá Phiến', 0, '2017-06-05 00:00:00'),
(85, 'Hà Nguyễn Gia', 'Hân', 0, '2015-01-31', '2017-01-01', '', '', '', 'Hà Trung Kiên', 'Nguyễn T K Lai', '905191660', 1, 1, 6, '88 Lê Duẫn', 0, '2017-06-05 00:00:00'),
(86, 'Nguyễn Gia', 'Hân', 0, '2015-01-31', '2017-01-01', '', '', '', 'Nguyễn Văn Lạc', 'Ng T Kiều Diễm', '905111250', 1, 3, 26, 'Tổ 11 An Hải Bắc', 0, '2017-06-05 00:00:00'),
(87, 'Ng H Bảo', 'Phương', 0, '2014-04-27', '2017-01-01', '', '', '', 'Nguyễn Công Linh', 'Huỳnh Thị Mẫn', '984010984', 1, 3, 26, 'Tổ 94 An Hải Bắc', 0, '2017-06-05 00:00:00'),
(88, 'Đỗ Ngọc Hồng', 'Phúc', 0, '2012-02-29', '2017-01-01', '', '', '0905269314', 'Đỗ Văn Lưu', 'Đinh T.Ng. Trâm', '0935018212', 1, 3, 28, 'Lô 23 Nại Nghĩa', 0, '2017-06-05 00:00:00'),
(89, 'Phan Minh', 'Tú', 1, '2012-06-06', '2017-01-01', '', '', '0905159010', 'Phan Minh Đông', 'Đỗ T Lan Nhi', '0905811797', 1, 3, 26, '22 Đỗ Xung Hợp', 0, '2017-06-05 00:00:00'),
(90, 'Ng Trần Gia', 'Bảo', 1, '2012-10-06', '2017-01-01', '', '', '0905456332', 'Ng Văn Cúc', 'Đoàn Thị Thu', '0935999710', 1, 3, 28, '03 Lê Cảnh Tân', 0, '2017-06-05 00:00:00'),
(91, 'Phạm Thanh B', 'Châu', 0, '2012-03-17', '2017-01-01', '', '', '0905026672', 'Phạm T . Long', 'Đinh T T Hằng', '0932471531', 1, 3, 26, 'Tổ 58 An Đồn', 0, '2017-06-05 00:00:00'),
(92, 'Lê Hoàng', 'Phước', 1, '2012-03-11', '2017-01-01', '', '', '', 'Ng Hoàng Linh', 'Lê Thị Như  Ngọc', '0932577047', 1, 3, 26, '24 Hoàng Việt', 0, '2017-06-05 00:00:00'),
(93, 'Phạm Quốc', 'Long', 1, '2012-09-10', '2017-01-01', '', '', '0905276905', 'Phạm Q Dương', 'Lê Thị H Nhung', '01213982130', 1, 3, 26, 'Tổ 24 An Tân', 0, '2017-06-05 00:00:00'),
(94, 'Trần Trí', 'Diễn', 1, '2012-03-30', '2017-01-01', '', '', '0905738860', 'Trần Văn Cường', 'Phan Thị Kim Liên', '01282664911', 1, 3, 28, 'Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(95, 'Trần Văn Phi', 'Long', 1, '2012-04-01', '2017-01-01', '', '', '0905909614', 'Trần Văn Hà', 'Huỳnh Thị Sẻ', '01225634651', 1, 3, 28, 'Tổ 63 Dương V Nga', 0, '2017-06-05 00:00:00'),
(96, 'Đoàn Đỗ Phương', 'Vy', 0, '2012-03-05', '2017-01-01', '', '', '01664586186', 'Đoàn Công Hiền', 'Đỗ T Thu Hồng', '0905447024', 1, 3, 26, 'Tổ 206 An cư', 0, '2017-06-05 00:00:00'),
(97, 'Huỳnh Phan T.', 'Hiếu', 1, '2012-07-20', '2017-01-01', '', '', '', 'Huỳnh Văn Dưỡng', 'Tô Thị Hạnh', '01686209234', 1, 3, 30, 'T160 VịnhMânQuan', 0, '2017-06-05 00:00:00'),
(98, 'Nguyễn Quốc', 'Thiên', 1, '2012-06-16', '2017-01-01', '', '', '0905983565', 'Nguyễn Thuận', 'Trần Mỹ Hòa', '0905758553', 1, 3, 28, 'Khu C Cư liền kề', 0, '2017-06-05 00:00:00'),
(99, 'Lê Nhật', 'Hoàng', 1, '2013-10-08', '2017-01-01', '', '', '', 'Lê Tú Vũ', 'Ng T Kim Thảo', '0905709088', 1, 3, 26, '107 NgTrung Trực', 0, '2017-06-05 00:00:00'),
(100, 'Lê Tấn', 'Kiệt', 1, '2013-02-11', '2017-01-01', '', '', '0905737126', 'Lê Tấn Đạt', 'Ng T Thanh thúy', '01289442282', 1, 3, 28, '132 Khu C cư', 0, '2017-06-05 00:00:00'),
(101, 'Đinh T.Thanh', 'Ngân', 0, '2012-05-13', '2017-01-01', '', '', '0982495429', 'Đinh Văn Lý', 'Nguyễn Thị Mến', '0982818821', 1, 3, 28, 'Tổ 51 KC Cư', 0, '2017-06-05 00:00:00'),
(102, 'Mai Ng Phượng', 'Nhi', 0, '2012-09-03', '2017-01-01', '', '', '0905688877', 'Mai Văn Hùng', 'Ng T Thu Sương', '01222434914', 1, 3, 28, '2B-711 khu c cư', 0, '2017-06-05 00:00:00'),
(103, 'Lê Ng Ngọc', 'Yến', 0, '2012-10-14', '2017-01-01', '', '', '01633109838', 'Lê Viết Trình', 'Nguyễn T. Nguyệt', '0905587293', 1, 3, 26, 'Số 2 Nguyễn Tuân', 0, '2017-06-05 00:00:00'),
(104, 'Phan Thủy', 'Tiên', 0, '2012-12-02', '2017-01-01', '', '', '0905258449', 'Phan Phước Chương', 'Nguyễn Thị Thủy', '0905731776', 1, 3, 26, '07 Trương Hán Siêu', 0, '2017-06-05 00:00:00'),
(105, 'Hoàng Đ Khánh', 'Nhi', 0, '2012-10-16', '2017-01-01', '', '', '0905051206', 'Hoàng Thế Thân', 'Đỗ Hoàng Phú', '0913988253', 1, 3, 24, '105 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(106, 'Cao Ng Tuyết', 'Hạnh', 0, '2012-09-04', '2017-01-01', '', '', '', 'Cao Văn Nam', 'Nguyễn Thị Hồng', '1228340654', 1, 3, 28, '137 Kcc làng Cá', 0, '2017-06-05 00:00:00'),
(107, 'Trương Ng M', 'Hằng', 0, '2012-10-08', '2017-01-01', '', '', '0909570290', 'Trương Ngọc Sơn', 'Ng T Phương Thảo', '0905434391', 1, 3, 26, '145 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(108, 'Nguyễn Bảo', 'Trung', 1, '2012-09-29', '2017-01-01', '', '', '1226273251', 'Ng Thanh Tùng', '', '', 1, 3, 26, '75 Ngô Chí Diễu', 0, '2017-06-05 00:00:00'),
(109, 'Hồ Đỗ Kim', 'Ánh', 0, '2012-12-09', '2017-01-01', '', '', '1216670716', 'Hồ Mã Long', 'Đỗ T Lệ Trang', '932650584', 1, 3, 26, 'K28 Số 12 Ng T Lộc', 0, '2017-06-05 00:00:00'),
(110, 'Mai Quang', 'Bảo', 1, '2012-07-31', '2017-01-01', '', '', '0906271932', 'Mai Quang Nhân', 'Ng T Bích Ngọc', '0996738759', 1, 3, 28, 'Tổ 146/p207 KCC 2c', 0, '2017-06-05 00:00:00'),
(111, 'Ng Nhật Minh', 'Thư', 0, '2012-05-19', '2017-01-01', '', '', '0906448912', 'Ng Văn Cường', 'Đinh T D Hương', '', 1, 3, 26, 'Tổ 45 B', 0, '2017-06-05 00:00:00'),
(112, 'Ng Nhật Minh', 'Anh', 0, '2012-05-19', '2017-01-01', '', '', '0906448912', 'Ng Văn  Cường', 'Đinh T D Hương', '', 1, 3, 26, 'Tổ 45 B', 0, '2017-06-05 00:00:00'),
(113, 'Phạm Ng Minh', 'Thư', 0, '2012-07-18', '2017-01-01', '', '', '988205552', 'Phạm Tấn Tài', 'Ng Thị Lê Hân', '1289471928', 1, 3, 28, 'Số 46 Lê Hữu Kiều', 0, '2017-06-05 00:00:00'),
(114, 'Phạm Nhã', 'Trúc', 0, '2012-01-28', '2017-01-01', '', '', '', '', 'Phạm Thị SaLem', '905098755', 1, 3, 28, 'D5 Lô 25 Vân Đồn', 0, '2017-06-05 00:00:00'),
(115, 'Đặng T Khánh', 'Ngọc', 0, '2012-08-15', '2017-01-01', '', '', '982936637', 'Đăng Ngọc Hiếu', 'Lê Thị Biên', '1224440227', 1, 3, 26, 'Tổ 58 An Đồn', 0, '2017-06-05 00:00:00'),
(116, 'Mai Võ Mạnh', 'Quân', 1, '2012-01-01', '2017-01-01', '', '', '906434228', 'Mai Ngọc Phương', 'Võ Thị Quanh', '905888235', 1, 3, 28, '29 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(117, 'Nguyễn Văn', 'Bình', 1, '2012-06-29', '2017-01-01', '', '', '935572223', 'Ng Văn Thanh', 'Mai Thị Phương', '935932880', 1, 3, 26, '41 Lê Văn Thêm', 0, '2017-06-05 00:00:00'),
(118, 'Nguyễn Anh', 'Thư', 0, '2012-11-21', '2017-01-01', '', '', '906446179', 'Ng Quang Ánh', 'Nguyễn Thùy Hương', '905523637', 1, 3, 28, '16 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(119, 'Lê Văn', 'Việt', 1, '2012-07-30', '2017-01-01', '', '', '905908800', 'Lê Văn Vinh', 'Bùi Thị Kim Anh', '905633034', 1, 3, 28, '41 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(120, 'Phạm Thảo', 'Nguyên', 0, '2013-01-14', '2017-01-01', '', '', '0936001751', 'Phạm Phú Viên', 'Đặng Thị Lệ', '0905669896', 1, 3, 28, '08 Nguyễn Khắc cần', 0, '2017-06-05 00:00:00'),
(121, 'Trần Gia', 'Bảo', 1, '2012-10-10', '2017-01-01', '', '', '906426516', 'Trần Văn Dự', 'Trần Thị Lai', '', 1, 3, 28, 'Tổ 33 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(122, 'Nguyễn Thái', 'Bình', 1, '2012-01-01', '2017-01-01', '', '', '', 'Ng Ngọc Dũng', 'Phạm T Mỹ Lành', '935497223', 1, 3, 26, '39 Ng Tuân', 0, '2017-06-05 00:00:00'),
(123, 'Phạm Quốc', 'Long', 1, '2012-09-10', '2017-01-01', '', '', '', 'Phạm Q Dương', 'Lê Thị H Nhung', '1213982130', 1, 3, 26, 'Tổ 24 An Tân', 0, '2017-06-05 00:00:00'),
(124, 'Nguyễn Duy', 'Thái', 1, '2012-10-20', '2017-01-01', '', '', '', 'Nguyễn Duy Tâm', 'Đoàn Thi Ngọc', '905016278', 1, 3, 30, '102 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(125, 'Nguyễn Tấn', 'Thành', 1, '2012-12-21', '2017-01-01', '', '', '935756711', 'Ng Tấn Sỹ', 'Nguyên Thị Hoa', '905790626', 1, 3, 26, '15 An Hải 5', 0, '2017-06-05 00:00:00'),
(126, 'Nguyễn H Khánh', 'An', 0, '2012-10-16', '2017-01-01', '', '', '1268677493', 'Ng Thành Hưng', 'NguyễnThị K Hòa', '1289467751', 1, 3, 28, '02 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(127, 'Nguyễn Gia', 'Phong', 1, '2013-01-19', '2017-01-01', '', '', '934966154', 'Nguyễn Văn Tây', 'Ng Thị Thu Lòng', '935885725', 1, 3, 28, '145 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(128, 'Trần Đặng Tấn', 'Phát', 1, '2013-10-17', '2017-01-01', '', '', '', 'Đặng Hồng Nam', 'Trần Thị Thủy', '905080300', 1, 3, 26, 'Tổ 59 An Đồn', 0, '2017-06-05 00:00:00'),
(129, 'Nguyễn Anh', 'Quân', 1, '2012-11-29', '2017-01-01', '', '', '1268677493', 'Nguyễn Công Hưng', 'Đặng Thị Yến', '905915184', 1, 3, 26, '50 An Hải 18', 0, '2017-06-05 00:00:00'),
(130, 'Nguyễn T Ngọc', 'Anh', 0, '2013-09-08', '2017-01-01', '', '', '', 'Nguyễn Xuân', 'Lê Thị Ut', '932145373', 1, 3, 28, 'CC làng cá', 0, '2017-06-05 00:00:00'),
(131, 'Đỗ Duy', 'Hoàng', 1, '2012-03-20', '2017-01-01', '', '', '', 'Đỗ Duy Đức', 'Lê Thị Thắm', '935625095', 1, 3, 28, '37 Nguyễn Thị Ba', 0, '2017-06-05 00:00:00'),
(132, 'Nguyễn Ngọc', 'Duy', 1, '2011-01-02', '2017-01-01', '', '', '01266607878', 'Ng. Ngọc Trí', 'Trần Thị Vân', '0904730442', 1, 3, 28, 'KCC L/cá Khúc Hạo', 0, '2017-06-05 00:00:00'),
(133, 'Lê Quang', 'Minh', 1, '2011-11-06', '2017-01-01', '', '', '935793532', 'Lê Quang Lợi', 'TrầnTThúy Hằng', '1202102016', 1, 3, 26, 'Tổ 12 An Tân', 0, '2017-06-05 00:00:00'),
(134, 'Nguyễn Vũ', 'Khoa', 1, '2011-05-10', '2017-01-01', '', '', '905479530', 'Nguyễn Văn Vũ', 'Phạm Thị Thúy', '905184116', 1, 3, 28, '141 Hoa Lư - v.Thùng', 0, '2017-06-05 00:00:00'),
(135, 'Phan Minh', 'Kiệt', 1, '2011-09-27', '2017-01-01', '', '', '935335533', 'Phan Minh Toàn', 'Ng Hà Bảo Vy', '905534343', 1, 3, 26, 'Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(136, 'Ng Lê Mai', 'Dương', 1, '2011-10-08', '2017-01-01', '', '', '905287847', 'Ng. Thành Tuấn', 'Lê T. Hồng Hạnh', '905761561', 1, 3, 26, '188-AHB', 0, '2017-06-05 00:00:00'),
(137, 'Trần Ng. Bảo', 'Ngọc', 0, '2011-09-30', '2017-01-01', '', '', '01222576800', 'Trần V Trường', '', '', 1, 3, 28, '26 Dg Thừa Lâm', 0, '2017-06-05 00:00:00'),
(138, 'Nguyễn Tuấn', 'Huy', 1, '2011-09-22', '2017-01-01', '', '', '0905652886', 'Ng Thanh Tuấn', 'Huỳnh Thị Bước', '', 1, 3, 28, 'Cc Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(139, 'Dương Hải', 'Yến', 0, '2011-10-29', '2017-01-01', '', '', '905599406', 'Dương Văn Sáu', 'Phan Thị Thu', '935922414', 1, 3, 26, '137 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(140, 'Ng Kiều Nhã', 'Phương', 0, '2011-07-23', '2017-01-01', '', '', '1269943208', 'Ng Văn Phước', 'Lê Thị Thảo', '935456391', 1, 3, 28, 'L68- Bùi Huy Bích', 0, '2017-06-05 00:00:00'),
(141, 'Phan M .Ngọc', 'Phúc', 1, '2011-10-11', '2017-01-01', '', '', '935854866', 'Phan Minh Phát', 'Võ T.Nguyệt Nga', '935888967', 1, 3, 26, '6 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(142, 'Phan M.Ngọc', 'Trân', 0, '2011-09-05', '2017-01-01', '', '', '1684207809', 'Phan Minh Khoa', 'Nguyễn T. Huệ', '932441122', 1, 3, 26, 'Tổ 28', 0, '2017-06-05 00:00:00'),
(143, 'Ng Mai Thục', 'Nhi', 0, '2012-07-05', '2017-01-01', '', '', '905132132', 'Ng. Trọng Bình', 'Mai T.Xuân Anh', '902177132', 1, 3, 26, '01 Nguyến Thế Lộc', 0, '2017-06-05 00:00:00'),
(144, 'Ng.Như Thùy', 'Linh', 0, '2011-04-01', '2017-01-01', '', '', '01678799115', 'Nguyễn Tâm', 'Lê Thị  Nguyệt', '01654549141', 1, 3, 28, 'CC Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(145, 'Lê Quốc', 'Nhân', 1, '2011-08-06', '2017-01-01', '', '', '01208185593', 'Lê Văn Lực', 'Lê Thị Sen', '', 1, 3, 28, 'Tô 2 Nại Tú', 0, '2017-06-05 00:00:00'),
(146, 'Lê Hữu Trung', 'Kiên', 1, '2011-02-21', '2017-01-01', '', '', '0934839463', 'Lê Hữu Thắng', 'Phú Thị Minh', '0906494803', 1, 3, 26, 'An Đồn', 0, '2017-06-05 00:00:00'),
(147, 'Trần Tuấn', 'Tú', 1, '2012-04-05', '2017-01-01', '', '', '1287555330', 'Trần tuấn Thiện', 'phan Thị Thạnh', '905749330', 1, 3, 28, '68 Nguyễn Hiền', 0, '2017-06-05 00:00:00'),
(148, 'Phan H Khánh', 'Như', 0, '2012-03-04', '2017-01-01', '', '', '935631647', 'Phan Thanh Hải', 'Ngô T Thu Hồng', '906446543', 1, 3, 28, 'Số 5 Nguyễn Địa Lô', 0, '2017-06-05 00:00:00'),
(149, 'Nguyễn Bảo Lam', 'Vi', 0, '2012-03-26', '2017-01-01', '', '', '906414688', 'Ng Huy Hoàng', 'Ng Thị Thành', '935686927', 1, 3, 28, '49 Hoàng Quốc Việt', 0, '2017-06-05 00:00:00'),
(150, 'Trần Phước', 'Nguyên', 1, '2012-09-28', '2017-01-01', '', '', '903531541', 'Trần Phước Tuân', 'Võ Thị Thảo', '905900923', 1, 3, 28, 'KC Cư NHĐ', 0, '2017-06-05 00:00:00'),
(151, 'Huỳnh Tr Xuân', 'Ngọc', 1, '2012-10-30', '2017-01-01', '', '', '', 'Lê Văn Cường', 'Kiều Thị Hồ Sự', '1216852303', 1, 3, 28, 'Tổ 09 Nại Nghĩa', 0, '2017-06-05 00:00:00'),
(152, 'Lê Kiều Bảo', 'Ngọc', 0, '2012-10-29', '2017-01-01', '', '', '932564640', 'Huỳnh Bá Linh', 'TrầnT Thúy Trinh', '932578385', 1, 3, 28, 'KCcu làng cá', 0, '2017-06-05 00:00:00'),
(153, 'Ng Trần Vũ', 'Nguyên', 1, '2011-09-26', '2017-01-01', '', '', '0905674223', 'Nguyễn V Thêm', 'Trần T Ánh Hồng', '', 1, 3, 29, '43 Lê Phụ Trần', 0, '2017-06-05 00:00:00'),
(154, 'Võ Như', 'Tài', 1, '2011-07-05', '2017-01-01', '', '', '', 'Võ Văn Vang', 'Huỳnh Thúy Liên', '1214460446', 1, 3, 28, '04 Nại Nghĩa7', 0, '2017-06-05 00:00:00'),
(155, 'Nguyễn Huyền', 'Châu', 0, '2011-11-05', '2017-01-01', '', '', '', 'Nguyễn Văn Sáng', 'Lê Thị Ngân', '0932441648', 1, 3, 28, 'Tổ 29 - Bùi Huy Bich', 0, '2017-06-05 00:00:00'),
(156, 'Đặng Hoàng', 'Mỹ', 1, '2011-12-12', '2017-01-01', '', '', '12215601399', 'Đặng Văn Thị', 'Cao Thị Thu', '935419652', 1, 3, 28, 'K Ccu làng cá', 0, '2017-06-05 00:00:00'),
(157, 'Mai Tuấn', 'Anh', 1, '2011-11-09', '2017-01-01', '', '', '987334996', 'Mai Văn Dũng', 'Ngô Thị Hải', '987334351', 1, 3, 26, 'Tổ 90', 0, '2017-06-05 00:00:00'),
(158, 'Lê Ng Phúc', 'Luân', 1, '2011-10-06', '2017-01-01', '', '', '905635272', 'Lê Văn Xin', 'Ng Thi Thu vân', '1228583177', 1, 3, 28, '06 Lê Hữu Kiều', 0, '2017-06-05 00:00:00'),
(159, 'Nguyễn T. Bảo', 'Châu', 0, '2011-02-26', '2017-01-01', '', '', '983811509', 'Ng Đình Ninh', 'Ng Thị Hương', '', 1, 3, 28, '77 Ngô Trí Hòa', 0, '2017-06-05 00:00:00'),
(160, 'Nguyễn Hoàng', 'Long', 1, '2011-05-17', '2017-01-01', '', '', '', 'Ng Hữu Thà', 'Ng T Thùy Trang', '905134905', 1, 3, 26, 'NguyễnChí Diễu', 0, '2017-06-05 00:00:00'),
(161, 'Ng Lê Diễm', 'Quỳnh', 0, '2012-10-06', '2017-01-01', '', '', '', 'Nguyễn Linh Sơn', 'Lê Thị Nhơn', '905111874', 1, 3, 28, 'Tô 6 Nại Tú 3', 0, '2017-06-05 00:00:00'),
(162, 'Đoàn Nhật', 'Tài', 1, '2011-11-13', '2017-01-01', '', '', '905476828', 'Đoàn Văn Thiên', 'PhTThùy Phương', '', 1, 3, 28, '309 Khúc Hạo', 0, '2017-06-05 00:00:00'),
(163, 'Mai Dương', 'Phong', 1, '2012-09-30', '2017-01-01', '', '', '905760027', 'Mai Văn Dưỡng', 'Nguyễn T Tuyết', '908870978', 1, 3, 26, '101 Ng Trung Trực', 0, '2017-06-05 00:00:00'),
(164, 'Đặng N Quỳnh', 'Giao', 0, '2012-04-12', '2017-01-01', '', '', '905877687', 'Đặng Duy Tường', 'Đỗ Thị Quỳnh', '905603527', 1, 3, 26, '27 Võ Trường Toản', 0, '2017-06-05 00:00:00'),
(165, 'Đỗ Thị Bảo', 'Ngọc', 0, '2012-09-14', '2017-01-01', '', '', '905760027', 'Đỗ Huy Nhơn', 'Trương Thị Tám', '932455604', 1, 3, 28, 'Tổ 70 Lê Phụ Trần', 0, '2017-06-05 00:00:00'),
(166, 'Đặng T.Khánh', 'Huyền', 0, '2011-04-08', '2017-01-01', '', '', '', 'Đặng Văn Cu', 'Ng Thị Thu Hà', '935107637', 1, 3, 28, 'Tổ 9 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(167, 'PhạmL.Khánh', 'Quỳnh', 0, '2011-07-13', '2017-01-01', '', '', '1223450373', 'Phạm Văn Giàu', 'Lê Thị Thắng', '', 1, 3, 28, 'Tổ 20 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(168, 'Hoàng Lê Gia', 'Huy', 1, '2012-09-23', '2017-01-01', '', '', '', 'Hoàng Sinh Tùng', 'Lê T B Phượng', '935135215', 1, 3, 29, '122 Trần Nhân Tông', 0, '2017-06-05 00:00:00'),
(169, 'Võ Như', 'Phát', 1, '2011-11-01', '2017-01-01', '', '', '913141206', 'Võ Như Khương', 'Phan Thị Thu', '935922414', 1, 3, 29, 'K 229 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(170, 'Đỗ Thanh', 'Phong', 1, '2011-10-19', '2017-01-01', '', '', '', 'Đoỗ Trọng Nguyên', 'Đặng T T Tâm', '909199503', 1, 3, 26, '24 Đỗ Xuân Hợp', 0, '2017-06-05 00:00:00'),
(171, 'Lê Quốc', 'Nhân', 1, '2011-08-06', '2017-01-01', '', '', '', 'Lê Văn Lực', 'Nguyễn T Thu Hà', '935107637', 1, 3, 28, 'Tổ 2 Nại Tú', 0, '2017-06-05 00:00:00'),
(172, 'Nguyễn Minh', 'Quân', 1, '2013-04-21', '2017-01-01', '', '', '01212275771', 'Ng Minh Hải', 'Từ Thị Phượng', '0938225160', 1, 3, 28, 'Tổ 09 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(173, 'Nguyễn T Bảo', 'Ngọc', 0, '2013-06-15', '2017-01-01', '', '', '0977701119', 'Ng Quốc Phương', 'Dương Ánh Loan', '0938225160', 1, 3, 28, '04 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(174, 'Phạm Ngô T T.', 'Ngân', 0, '2013-03-25', '2017-01-01', '', '', '0905570541', 'Phạm Văn Đăng', 'Ngô Thị Lợi', '01227428481', 1, 3, 28, '29 Đỗ Hoành', 0, '2017-06-05 00:00:00'),
(175, 'Bùi Văn Trọng', 'Nghĩa', 1, '2013-01-12', '2017-01-01', '', '', '0935987897', 'Bùi Văn Vũ', 'Đặng Thị  Lai', '0935709735', 1, 3, 26, '22 Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(176, 'Ng Phùng Thảo', 'My', 0, '2013-10-17', '2017-01-01', '', '', '0905449759', 'Ng Hữu Thiệt', 'Phùng TThu Hương', '093507765', 1, 3, 28, 'Khu C cư Bluehuose', 0, '2017-06-05 00:00:00'),
(177, 'Nguyễn Quang', 'Minh', 1, '2013-03-21', '2017-01-01', '', '', '935492144', 'Ng Đăng Huy', 'Bùi Thị Thanh Lan', '905492144', 1, 3, 26, '21 Đỗ Xuân Hợp', 0, '2017-06-05 00:00:00'),
(178, 'Lê Pham Bảo', 'Ngọc', 0, '2013-04-16', '2017-01-01', '', '', '1266675717', 'Lê Văn Tài', 'Phạm T Phương Phú', '905361644', 1, 3, 28, '46 Lê Hữu Kiều', 0, '2017-06-05 00:00:00'),
(179, 'Lê Văn Duy', 'Khang', 1, '2013-01-08', '2017-01-01', '', '', '', 'Lê Văn Tốt', '', '', 1, 3, 26, '88  Lê Phụng Hiểu', 0, '2017-06-05 00:00:00'),
(180, 'Phạm Thúy', 'Hạnh', 0, '2013-08-15', '2017-01-01', '', '', '979161363', 'Phạm Xuân Sinh', 'Ng Thị Lượng', '01262752259', 1, 3, 28, '69 Hoàng Quốc Việt', 0, '2017-06-05 00:00:00'),
(181, 'Ng Trần Bảo', 'Trân', 0, '2013-11-24', '2017-01-01', '', '', '905674223', 'Ng Văn Thêm', 'Trần T Ánh Hồng', '0935756030', 1, 3, 29, '43 Lê Phụ Trần', 0, '2017-06-05 00:00:00'),
(182, 'Trần Tuấn', 'Kiệt', 1, '2013-05-01', '2017-01-01', '', '', '905630100', 'Trần Văn Giang', 'Đặng Thị Hồng', '1223479114', 1, 3, 28, '26 Dương Lâm', 0, '2017-06-05 00:00:00'),
(183, 'Võ Nguyễn Bảo', 'Anh', 0, '2013-12-12', '2017-01-01', '', '', '3931507', 'Võ Tấn Đông', 'Võ Thị  Nhât Uyên', '1204052149', 1, 3, 26, '24 Trương Hán Siêu', 0, '2017-06-05 00:00:00'),
(184, 'Trần Anh', 'Khôi', 1, '2013-06-11', '2017-01-01', '', '', '906478505', 'Trần Bá Toản', 'Võ Thị Tuyết Mận', '1267172521', 1, 3, 28, '51 Nại hiên đông 15', 0, '2017-06-05 00:00:00'),
(185, 'Nguyễn Lê', 'Vy', 0, '2014-01-20', '2017-01-01', '', '', '905323757', 'Nguyễn T Chiến', 'Lê Thị T. Tuyền', '', 1, 3, 30, 'K 49/14 Võ Duy Ninh', 0, '2017-06-05 00:00:00'),
(186, 'Bùi Phương', 'Linh', 0, '2013-01-06', '2017-01-01', '', '', '', 'Bùi Phg Hoàng', 'Phạm Thị Mỹ Thọ', '1227493336', 1, 3, 28, 'Khu C cư làng cá', 0, '2017-06-05 00:00:00'),
(187, 'Hà Hoài', 'An', 0, '2013-10-20', '2017-01-01', '', '', '906111542', 'Hà Văn Dũng', 'Ngô Thị H Thương', '935480241', 1, 3, 26, 'Tổ 53 An Đồn', 0, '2017-06-05 00:00:00'),
(188, 'Lê Hồng', 'Phước', 1, '2013-02-04', '2017-01-01', '', '', '', 'Lê Hồng Khai', 'Lê Thị Hiển', '943627567', 1, 4, 36, '80 Lý Nhân Tông', 0, '2017-06-05 00:00:00'),
(189, 'Võ NgNguyên', 'Khang', 1, '2014-01-24', '2017-01-01', '', '', '986131311', 'Võ Ngọc Tuyền', 'Nguyễn Thị Dung', '915820206', 1, 3, 28, 'Tổ 43 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(190, 'Lê Kỳ', 'Nam', 1, '2014-04-25', '2017-01-01', '', '', '', 'Lê Văn Thắng', 'Ngô Thị Lài', '905287118', 1, 3, 28, 'Tổ 39 Tôn Quang Phiệt', 0, '2017-06-05 00:00:00'),
(191, 'Ng Hoàng Lan', 'Uyên', 0, '2014-07-17', '2017-01-01', '', '', '908720972', 'Nguyễn H Quốc', 'Nguyễn Thị Bé Lan', '905899411', 1, 3, 28, '16 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(192, 'Phan ĐHoàng', 'Dương', 1, '2013-12-03', '2017-01-01', '', '', '905877848', 'Phan Đình Hoàng', 'Lê Thị Quyên', '905552509', 1, 3, 26, '102 Nguyễn Thị Định', 0, '2017-06-05 00:00:00'),
(193, 'Huỳnh Thị Tố', 'Tâm', 0, '2014-11-04', '2017-01-01', '', '', '934721700', 'Huỳnh Văn Nhớ', 'Nguyễn Thị Bình', '1677143131', 1, 3, 28, 'Tổ 11 Nại Hiên Đông', 0, '2017-06-05 00:00:00'),
(194, 'Phan Th Nhã', 'Trúc', 0, '2013-12-22', '2017-01-01', '', '', '', 'P Thanh Cảnh', 'Nguyễn Thị Thị', '906549780', 1, 3, 28, '68 Nguyễn Hiền', 0, '2017-06-05 00:00:00'),
(195, 'Nguyễn Tâm', 'Như', 0, '2014-07-05', '2017-01-01', '', '', '1282699789', 'Ng Đình Bảo', 'Phạm Thị Thủy', '935101700', 1, 3, 26, '415 Ngô Quyền', 0, '2017-06-05 00:00:00'),
(196, 'Từ Tuấn', 'Tú', 1, '2015-04-09', '2017-01-01', '', '', '', 'Từ Văn Tây', 'Lê T Việt Trinh', '905251941', 1, 3, 28, '55 Nại Nghĩa 7', 0, '2017-06-05 00:00:00'),
(197, 'Đặng Ngọc Bảo', 'Ân', 1, '2013-06-06', '2017-01-01', '', '', '905542431', 'Đặng Ngọc Vũ', 'Ngô T Thu Uyên', '905492952', 1, 3, 28, '26 Nguyễn Trung Trực', 0, '2017-06-05 00:00:00'),
(198, 'Trần Thị Bảo', 'Ngọc', 0, '2014-07-28', '2017-01-01', '', '', '1649840904', 'Trần Văn Bảo', 'Nguyễn T Phi Yến', '1664982723', 1, 3, 28, '47 Nại Nghĩa', 0, '2017-06-05 00:00:00'),
(199, 'Lê Gia', 'Bảo', 1, '2014-02-10', '2017-01-01', '', '', '906702758', 'Lê Hồ Vĩnh', 'Dđòa Thị Ngọc Loan', '906045510', 1, 3, 30, '133 Phan Bá Phiến', 0, '2017-06-05 00:00:00'),
(200, 'Mai Ngọc', 'Khang', 1, '2014-04-07', '2017-01-01', '', '', '1202433242', 'Mai Văn Ngọc', 'Trần Thị Trịnh', '936221443', 1, 3, 28, 'Tổ 43 Nại Hiên Đông', 0, '2017-06-05 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `hocsinh_lop`
--

CREATE TABLE IF NOT EXISTS `hocsinh_lop` (
  `HocSinhLopId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `DateJoin` date NOT NULL,
  `DateLeft` date DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`HocSinhLopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=201 ;

--
-- Dumping data for table `hocsinh_lop`
--

INSERT INTO `hocsinh_lop` (`HocSinhLopId`, `HocSinhId`, `LopId`, `DateJoin`, `DateLeft`, `DateCreated`, `DateDeleted`) VALUES
(1, 1, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(2, 2, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(3, 3, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(4, 4, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(5, 5, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(6, 6, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(7, 7, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(8, 8, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(9, 9, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(10, 10, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(11, 11, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(12, 12, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(13, 13, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(14, 14, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(15, 15, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(16, 16, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(17, 17, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(18, 18, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(19, 19, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(20, 20, 7, '2017-01-01', NULL, '2017-06-05 04:44:00', NULL),
(21, 21, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(22, 22, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(23, 23, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(24, 24, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(25, 25, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(26, 26, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(27, 27, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(28, 28, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(29, 29, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(30, 30, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(31, 31, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(32, 32, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(33, 33, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(34, 34, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(35, 35, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(36, 36, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(37, 37, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(38, 38, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(39, 39, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(40, 40, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(41, 41, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(42, 42, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(43, 43, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(44, 44, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(45, 45, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(46, 46, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(47, 47, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(48, 48, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(49, 49, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(50, 50, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(51, 51, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(52, 52, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(53, 53, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(54, 54, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(55, 55, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(56, 56, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(57, 57, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(58, 58, 2, '2017-01-01', NULL, '2017-06-05 07:22:07', NULL),
(59, 59, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(60, 60, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(61, 61, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(62, 62, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(63, 63, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(64, 64, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(65, 65, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(66, 66, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(67, 67, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(68, 68, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(69, 69, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(70, 70, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(71, 71, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(72, 72, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(73, 73, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(74, 74, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(75, 75, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(76, 76, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(77, 77, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(78, 78, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(79, 79, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(80, 80, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(81, 81, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(82, 82, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(83, 83, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(84, 84, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(85, 85, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(86, 86, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(87, 87, 5, '2017-01-01', NULL, '2017-06-05 07:35:49', NULL),
(88, 88, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(89, 89, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(90, 90, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(91, 91, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(92, 92, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(93, 93, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(94, 94, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(95, 95, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(96, 96, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(97, 97, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(98, 98, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(99, 99, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(100, 100, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(101, 101, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(102, 102, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(103, 103, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(104, 104, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(105, 105, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(106, 106, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(107, 107, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(108, 108, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(109, 109, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(110, 110, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(111, 111, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(112, 112, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(113, 113, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(114, 114, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(115, 115, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(116, 116, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(117, 117, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(118, 118, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(119, 119, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(120, 120, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(121, 121, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(122, 122, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(123, 123, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(124, 124, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(125, 125, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(126, 126, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(127, 127, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(128, 128, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(129, 129, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(130, 130, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(131, 131, 3, '2017-01-01', NULL, '2017-06-05 07:50:44', NULL),
(132, 132, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(133, 133, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(134, 134, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(135, 135, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(136, 136, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(137, 137, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(138, 138, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(139, 139, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(140, 140, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(141, 141, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(142, 142, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(143, 143, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(144, 144, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(145, 145, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(146, 146, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(147, 147, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(148, 148, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(149, 149, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(150, 150, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(151, 151, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(152, 152, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(153, 153, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(154, 154, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(155, 155, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(156, 156, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(157, 157, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(158, 158, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(159, 159, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(160, 160, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(161, 161, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(162, 162, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(163, 163, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(164, 164, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(165, 165, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(166, 166, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(167, 167, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(168, 168, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(169, 169, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(170, 170, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(171, 171, 1, '2017-01-01', NULL, '2017-06-05 08:04:32', NULL),
(172, 172, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(173, 173, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(174, 174, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(175, 175, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(176, 176, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(177, 177, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(178, 178, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(179, 179, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(180, 180, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(181, 181, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(182, 182, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(183, 183, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(184, 184, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(185, 185, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(186, 186, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(187, 187, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(188, 188, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(189, 189, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(190, 190, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(191, 191, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(192, 192, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(193, 193, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(194, 194, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(195, 195, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(196, 196, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(197, 197, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(198, 198, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(199, 199, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL),
(200, 200, 6, '2017-01-01', NULL, '2017-06-05 08:19:48', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `hoctap`
--

CREATE TABLE IF NOT EXISTS `hoctap` (
  `HocTapId` int(11) NOT NULL AUTO_INCREMENT,
  `HocSinhLopId` int(11) NOT NULL,
  `Hoc2Buoi` tinyint(1) DEFAULT NULL,
  `ChuyenCan` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AnTaiTruong` tinyint(1) DEFAULT NULL,
  `HocLienTuc` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TheoDoiCanNang` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SDDCanNang` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TheoDoiChieuCao` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SDDChieuCao` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HoanThanhCTGDMN` tinyint(1) DEFAULT NULL,
  `DanhGia` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`HocTapId`),
  KEY `HocSinhId` (`HocSinhLopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=9 ;

-- --------------------------------------------------------

--
-- Table structure for table `khoanthu`
--

CREATE TABLE IF NOT EXISTS `khoanthu` (
  `KhoanThuId` int(11) NOT NULL AUTO_INCREMENT,
  `Ten` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `SoTien` bigint(20) DEFAULT NULL,
  `IsBatBuoc` tinyint(1) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`KhoanThuId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

--
-- Dumping data for table `khoanthu`
--

INSERT INTO `khoanthu` (`KhoanThuId`, `Ten`, `SoTien`, `IsBatBuoc`, `DateCreated`, `DateDeleted`) VALUES
(1, 'Tiền ăn & sữa', NULL, 1, '2017-03-07 07:57:23', NULL),
(2, 'Phụ phí', NULL, 1, '2017-03-07 07:57:23', NULL),
(3, 'Bán trú', NULL, 1, '2017-03-07 07:57:23', NULL),
(4, 'Học phí', NULL, 1, '2017-03-07 07:57:23', NULL),
(5, 'Ăn sáng', NULL, 1, '2017-03-09 04:03:36', NULL),
(6, 'Ăn tối', NULL, 1, '2017-03-21 09:14:37', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `khoanthuhangnam`
--

CREATE TABLE IF NOT EXISTS `khoanthuhangnam` (
  `KhoanThuHangNamId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoanThuId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  `NgayTinh` date DEFAULT NULL,
  `NgayKetThuc` date DEFAULT NULL,
  `IsBatBuoc` tinyint(1) NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  PRIMARY KEY (`KhoanThuHangNamId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=25 ;

--
-- Dumping data for table `khoanthuhangnam`
--

INSERT INTO `khoanthuhangnam` (`KhoanThuHangNamId`, `KhoanThuId`, `KhoiId`, `NgayTinh`, `NgayKetThuc`, `IsBatBuoc`, `SoTien`) VALUES
(1, 1, 1, '2015-01-01', NULL, 1, 450000),
(2, 2, 1, '2015-01-01', NULL, 1, 158000),
(3, 3, 1, '2015-01-01', NULL, 1, 260000),
(4, 4, 1, '2015-01-01', NULL, 1, 390000),
(5, 1, 2, '2015-01-01', NULL, 1, 420000),
(6, 2, 2, '2015-01-01', NULL, 1, 158000),
(7, 3, 2, '2015-01-01', NULL, 1, 260000),
(8, 4, 2, '2015-01-01', NULL, 1, 390000),
(9, 1, 3, '2015-01-01', NULL, 1, 400000),
(10, 2, 3, '2015-01-01', NULL, 1, 183000),
(11, 3, 3, '2015-01-01', NULL, 1, 265000),
(12, 4, 3, '2015-01-01', NULL, 1, 410000),
(13, 1, 4, '2015-01-01', NULL, 1, 380000),
(14, 2, 4, '2015-01-01', NULL, 1, 198000),
(15, 3, 4, '2015-01-01', NULL, 1, 280000),
(16, 4, 4, '2015-01-01', NULL, 1, 430000),
(17, 5, 1, '2015-01-01', NULL, 1, 200000),
(18, 5, 2, '2015-01-01', NULL, 1, 200000),
(19, 5, 3, '2015-01-01', NULL, 1, 200000),
(20, 5, 4, '2015-01-01', NULL, 1, 200000),
(21, 6, 1, '2015-01-01', NULL, 1, 200000),
(22, 6, 2, '2015-01-01', NULL, 1, 200000),
(23, 6, 3, '2015-01-01', NULL, 1, 200000),
(24, 6, 4, '2015-01-01', NULL, 1, 200000);

-- --------------------------------------------------------

--
-- Table structure for table `khoi`
--

CREATE TABLE IF NOT EXISTS `khoi` (
  `KhoiId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`KhoiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=5 ;

--
-- Dumping data for table `khoi`
--

INSERT INTO `khoi` (`KhoiId`, `Name`, `Description`) VALUES
(1, 'Lớn', 'Khối lớn'),
(2, 'Nhỡ', 'Khối nhỡ'),
(3, 'Bé', 'Khối bé'),
(4, 'Trẻ', 'Khối trẻ');

-- --------------------------------------------------------

--
-- Table structure for table `khoi_truong`
--

CREATE TABLE IF NOT EXISTS `khoi_truong` (
  `KhoiTruongId` int(11) NOT NULL AUTO_INCREMENT,
  `KhoiId` int(11) NOT NULL,
  `TruongId` int(11) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`KhoiTruongId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=10 ;

--
-- Dumping data for table `khoi_truong`
--

INSERT INTO `khoi_truong` (`KhoiTruongId`, `KhoiId`, `TruongId`, `DateCreated`, `DateDeleted`) VALUES
(6, 1, 1, '2017-03-01 07:21:15', NULL),
(7, 2, 1, '2017-03-01 07:21:15', NULL),
(8, 3, 1, '2017-03-01 07:21:15', NULL),
(9, 4, 1, '2017-03-01 07:21:15', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lop`
--

CREATE TABLE IF NOT EXISTS `lop` (
  `LopId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`LopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=11 ;

--
-- Dumping data for table `lop`
--

INSERT INTO `lop` (`LopId`, `Name`, `Description`, `DateCreated`, `DateDeleted`) VALUES
(1, 'Lớn 1', 'Lớp lớn 1', '2016-12-31 17:00:00', NULL),
(2, 'Lớn 2', 'Lớp lớn 2', '2016-12-31 17:00:00', NULL),
(3, 'Nhỡ 1', 'Lớp nhỡ 1', '2016-12-31 17:00:00', NULL),
(4, 'Nhỡ 2', 'Lớp nhỡ 2', '2016-12-31 17:00:00', NULL),
(5, 'Bé 1', 'Lớp bé 1', '2016-12-31 17:00:00', NULL),
(6, 'Bé 2', 'Lớp bé 2', '2016-12-31 17:00:00', NULL),
(7, 'Trẻ 1', 'Lớp trẻ 1', '2016-12-31 17:00:00', NULL),
(10, 'Trẻ 2', 'Lớp trẻ 2', '2017-03-15 08:31:04', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `lop_khoi`
--

CREATE TABLE IF NOT EXISTS `lop_khoi` (
  `LopKhoiId` int(11) NOT NULL AUTO_INCREMENT,
  `LopId` int(11) NOT NULL,
  `KhoiId` int(11) NOT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` date DEFAULT NULL,
  PRIMARY KEY (`LopKhoiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=58 ;

--
-- Dumping data for table `lop_khoi`
--

INSERT INTO `lop_khoi` (`LopKhoiId`, `LopId`, `KhoiId`, `DateCreated`, `DateDeleted`) VALUES
(48, 1, 1, '2017-03-15 08:36:05', NULL),
(49, 2, 1, '2017-03-15 08:36:05', NULL),
(50, 3, 2, '2017-03-15 08:36:05', NULL),
(51, 4, 2, '2017-03-15 08:36:05', NULL),
(52, 5, 3, '2017-03-15 08:36:05', NULL),
(53, 6, 3, '2017-03-15 08:36:05', NULL),
(54, 7, 4, '2017-03-15 08:36:06', NULL),
(57, 10, 4, '2017-03-15 08:36:33', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `phanloaichi`
--

CREATE TABLE IF NOT EXISTS `phanloaichi` (
  `PhanLoaiChiId` int(11) NOT NULL AUTO_INCREMENT,
  `MaPhanLoai` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `DienGiai` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`PhanLoaiChiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=9 ;

--
-- Dumping data for table `phanloaichi`
--

INSERT INTO `phanloaichi` (`PhanLoaiChiId`, `MaPhanLoai`, `DienGiai`) VALUES
(1, 'TP', 'Thực phẩm'),
(3, 'DCHT', 'Dụng cụ học tập'),
(4, 'TP', 'Sữa'),
(5, 'PP', 'Giấy VS'),
(6, 'Phụ phí', 'Nước xả vải'),
(7, 'Phụ phí', 'Nước lau sàn'),
(8, 'Phụ phí', 'Nước sinh hoạt');

-- --------------------------------------------------------

--
-- Table structure for table `phieuchi`
--

CREATE TABLE IF NOT EXISTS `phieuchi` (
  `PhieuChiId` int(11) NOT NULL AUTO_INCREMENT,
  `MaPhieu` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Ngay` date NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  `PhanLoaiChiId` int(11) NOT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhieuChiId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=15 ;

-- --------------------------------------------------------

--
-- Table structure for table `phieuthu`
--

CREATE TABLE IF NOT EXISTS `phieuthu` (
  `PhieuThuId` int(11) NOT NULL AUTO_INCREMENT,
  `Ngay` date NOT NULL,
  `SoTien` bigint(20) NOT NULL,
  `MaPhieu` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  `HocSinhId` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`PhieuThuId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=14 ;

-- --------------------------------------------------------

--
-- Table structure for table `phuongxa`
--

CREATE TABLE IF NOT EXISTS `phuongxa` (
  `PhuongXaId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `QuanHuyenId` int(11) NOT NULL,
  PRIMARY KEY (`PhuongXaId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=52 ;

--
-- Dumping data for table `phuongxa`
--

INSERT INTO `phuongxa` (`PhuongXaId`, `Name`, `QuanHuyenId`) VALUES
(1, 'Hải Châu 1', 1),
(2, 'Hải Châu 2', 1),
(3, 'Bình Hiên', 1),
(4, 'Thanh Bình', 1),
(5, 'Thuận Phước', 1),
(6, 'Thạch Thang', 1),
(7, 'Phước Ninh', 1),
(8, 'Hòa Thuận Tây', 1),
(9, 'Hòa Thuận Đông', 1),
(10, 'Nam Dương', 1),
(11, 'Bình Thuận', 1),
(12, 'Hòa Cường Bắc', 1),
(13, 'Hòa Cường Nam', 1),
(14, 'An Khê', 2),
(15, 'Thanh Khê Đông', 2),
(16, 'Xuân Hà', 2),
(17, 'Tam Thuận', 2),
(18, 'Tân Chính', 2),
(19, 'Thạc Gián', 2),
(20, 'Chính Gián', 2),
(21, 'Hòa Khê', 2),
(22, 'Vĩnh Trung', 2),
(23, 'Thanh Khê Tây', 2),
(24, 'AN HẢI ĐÔNG', 3),
(25, 'An Hải Tây', 3),
(26, 'An Hải Băc', 3),
(27, 'Phước Mỹ', 3),
(28, 'Nại Hiên Đông', 3),
(29, 'Mân Thái', 3),
(30, 'Thọ Quang', 3),
(31, 'Hòa An', 4),
(32, 'Hòa Phát', 4),
(33, 'Hòa Thọ Đông', 4),
(34, 'Hòa Thọ Tây', 4),
(35, 'Hòa Xuân', 4),
(36, 'Khuê Trung', 4),
(37, 'Hòa Hải', 5),
(38, 'Mỹ An', 5),
(39, 'Khuê Mỹ', 5),
(40, 'Hòa Quý', 5),
(41, 'Hòa Bắc', 6),
(42, 'Hòa Châu', 6),
(43, 'Hòa Khương', 6),
(44, 'Hòa Liên', 6),
(45, 'Hòa Nhơn', 6),
(46, 'Hòa Ninh', 6),
(47, 'Hòa Phong', 6),
(48, 'Hòa Phú', 6),
(49, 'Hòa Phước', 6),
(50, 'Hòa Sơn', 6),
(51, 'Hòa Tiến', 6);

-- --------------------------------------------------------

--
-- Table structure for table `quanhuyen`
--

CREATE TABLE IF NOT EXISTS `quanhuyen` (
  `QuanHuyenId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ThanhPhoId` int(11) NOT NULL,
  PRIMARY KEY (`QuanHuyenId`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

--
-- Dumping data for table `quanhuyen`
--

INSERT INTO `quanhuyen` (`QuanHuyenId`, `Name`, `ThanhPhoId`) VALUES
(1, 'Hải Châu', 1),
(2, 'Thanh Khê', 1),
(3, 'Sơn Trà', 1),
(4, 'Cẩm Lệ', 1),
(5, 'Ngũ Hành Sơn', 1),
(6, 'Hòa Vang', 1);

-- --------------------------------------------------------

--
-- Table structure for table `schoolsetting`
--

CREATE TABLE IF NOT EXISTS `schoolsetting` (
  `SchoolSettingId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `Value` text COLLATE utf8_unicode_ci NOT NULL,
  `DateCreated` date NOT NULL,
  `DateDeleted` date NOT NULL,
  PRIMARY KEY (`SchoolSettingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `taisan`
--

CREATE TABLE IF NOT EXISTS `taisan` (
  `TaiSanId` int(11) NOT NULL AUTO_INCREMENT,
  `Ten` text COLLATE utf8_unicode_ci NOT NULL,
  `DonViTinh` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` bigint(20) NOT NULL,
  `SoChungTu` text COLLATE utf8_unicode_ci NOT NULL,
  `NgayChungTu` date NOT NULL,
  `PhieuChiId` int(11) NOT NULL,
  PRIMARY KEY (`TaiSanId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=8 ;

-- --------------------------------------------------------

--
-- Table structure for table `taisan_lop`
--

CREATE TABLE IF NOT EXISTS `taisan_lop` (
  `TaiSanLopId` int(11) NOT NULL AUTO_INCREMENT,
  `TaiSanId` int(11) NOT NULL,
  `LopId` int(11) NOT NULL,
  `SoLuong` double DEFAULT NULL,
  `NgayBanGiao` date DEFAULT NULL,
  `NgayHetHan` date DEFAULT NULL,
  `GhiChu` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`TaiSanLopId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=7 ;

-- --------------------------------------------------------

--
-- Table structure for table `thanhpho`
--

CREATE TABLE IF NOT EXISTS `thanhpho` (
  `ThanhPhoId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`ThanhPhoId`),
  UNIQUE KEY `Name` (`Name`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `thanhpho`
--

INSERT INTO `thanhpho` (`ThanhPhoId`, `Name`) VALUES
(1, 'Đà Nẵng');

-- --------------------------------------------------------

--
-- Table structure for table `truong`
--

CREATE TABLE IF NOT EXISTS `truong` (
  `TruongId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Description` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`TruongId`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci AUTO_INCREMENT=2 ;

--
-- Dumping data for table `truong`
--

INSERT INTO `truong` (`TruongId`, `Name`, `Description`) VALUES
(1, 'MN Hồng Minh', 'Trường mầm non Hồng Minh');

-- --------------------------------------------------------

--
-- Stand-in structure for view `viewbangiaotaisan`
--
CREATE TABLE IF NOT EXISTS `viewbangiaotaisan` (
`TaiSanId` int(11)
,`Ten` text
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` text
,`NgayChungTu` date
,`PhieuChiId` int(11)
,`LopId` int(11)
,`NgayBanGiao` date
,`NgayHetHan` date
,`GhiChu` text
,`TaiSanLopId` int(11)
,`SoLuongBanGiao` double
,`NgayNhap` date
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewbangthutien`
--
CREATE TABLE IF NOT EXISTS `viewbangthutien` (
`BangThuTienId` int(11)
,`HocSinhId` int(11)
,`LopId` int(11)
,`SXThangTruoc` int(11)
,`SoTienSXThangTruoc` bigint(20)
,`AnSangThangTruoc` int(11)
,`SoTienAnSangThangTruoc` bigint(20)
,`SoTienAnSangThangNay` bigint(20)
,`AnToiThangTruoc` int(11)
,`SoTienAnToiThangTruoc` bigint(20)
,`SoTienAnToiThangNay` bigint(20)
,`SoTienNangKhieu` bigint(20)
,`SoTienTruyThu` bigint(20)
,`SoTienDoDung` bigint(20)
,`SoTienDieuHoa` bigint(20)
,`NgayTinh` date
,`STT` int(11)
,`IsDeleted` tinyint(1)
,`DateCreated` timestamp
,`SoTienAnSangConLai` bigint(15)
,`SoTienAnToiConLai` bigint(15)
,`ThanhTien` bigint(15)
,`TienAnSua` bigint(15)
,`PhuPhi` bigint(15)
,`BanTru` bigint(15)
,`HocPhi` bigint(15)
,`KhoanThuChinh` bigint(15)
,`HoTen` char(0)
,`NgayNopLan1` binary(0)
,`SoTienNopLan1` bigint(15)
,`NgayNopLan2` binary(0)
,`SoTienNopLan2` bigint(15)
,`GhiChu` text
,`Lop` varchar(1)
,`SoBienLai` varchar(1)
,`TongThu` varchar(1)
,`TienAn` bigint(15)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewhoctap`
--
CREATE TABLE IF NOT EXISTS `viewhoctap` (
`HocSinhId` int(11)
,`HoDem` varchar(50)
,`Ten` varchar(50)
,`GioiTinh` tinyint(4)
,`NgaySinh` date
,`NgayVaoLop` date
,`DanToc` varchar(50)
,`DienCuTru` varchar(50)
,`DienThoai` varchar(50)
,`HoTenCha` varchar(255)
,`HoTenMe` varchar(255)
,`TinhThanhPhoId` int(11)
,`QuanHuyenId` int(11)
,`PhuongXaId` int(11)
,`DiaChi` varchar(255)
,`ThoiHoc` tinyint(1)
,`CreatedDate` datetime
,`SoNgayNghiThang` int(11)
,`NgayTinh` date
,`HocTapId` int(11)
,`LopId` int(11)
,`LopName` varchar(255)
,`HocSinhLopId` int(11)
,`DateJoin` date
,`DateLeft` date
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewtaisan`
--
CREATE TABLE IF NOT EXISTS `viewtaisan` (
`TaiSanId` int(11)
,`Ten` text
,`NgayNhap` date
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` text
,`NgayChungTu` date
,`ThanhTien` bigint(20)
,`PhieuChiId` int(11)
,`PhanLoaiChiId` int(11)
);
-- --------------------------------------------------------

--
-- Stand-in structure for view `viewtaisanforbangiaotaisan`
--
CREATE TABLE IF NOT EXISTS `viewtaisanforbangiaotaisan` (
`TaiSanId` int(11)
,`Ten` mediumtext
,`NgayNhap` date
,`DonViTinh` varchar(20)
,`SoLuong` double
,`DonGia` bigint(20)
,`SoChungTu` mediumtext
,`NgayChungTu` date
,`ThanhTien` bigint(20)
,`PhieuChiId` int(11)
,`PhanLoaiChiId` bigint(20)
);
-- --------------------------------------------------------

--
-- Structure for view `viewbangiaotaisan`
--
DROP TABLE IF EXISTS `viewbangiaotaisan`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewbangiaotaisan` AS select `ts`.`TaiSanId` AS `TaiSanId`,`ts`.`Ten` AS `Ten`,`ts`.`DonViTinh` AS `DonViTinh`,`ts`.`SoLuong` AS `SoLuong`,`ts`.`DonGia` AS `DonGia`,`ts`.`SoChungTu` AS `SoChungTu`,`ts`.`NgayChungTu` AS `NgayChungTu`,`ts`.`PhieuChiId` AS `PhieuChiId`,`tsl`.`LopId` AS `LopId`,`tsl`.`NgayBanGiao` AS `NgayBanGiao`,`tsl`.`NgayHetHan` AS `NgayHetHan`,`tsl`.`GhiChu` AS `GhiChu`,`tsl`.`TaiSanLopId` AS `TaiSanLopId`,`tsl`.`SoLuong` AS `SoLuongBanGiao`,`pc`.`Ngay` AS `NgayNhap` from ((`taisan` `ts` join `phieuchi` `pc` on((`ts`.`PhieuChiId` = `pc`.`PhieuChiId`))) left join `taisan_lop` `tsl` on((`ts`.`TaiSanId` = `tsl`.`TaiSanId`)));

-- --------------------------------------------------------

--
-- Structure for view `viewbangthutien`
--
DROP TABLE IF EXISTS `viewbangthutien`;

CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewbangthutien` AS select `bangthutien`.`BangThuTienId` AS `BangThuTienId`,`bangthutien`.`HocSinhId` AS `HocSinhId`,`bangthutien`.`LopId` AS `LopId`,`bangthutien`.`SXThangTruoc` AS `SXThangTruoc`,`bangthutien`.`SoTienSXThangTruoc` AS `SoTienSXThangTruoc`,`bangthutien`.`AnSangThangTruoc` AS `AnSangThangTruoc`,`bangthutien`.`SoTienAnSangThangTruoc` AS `SoTienAnSangThangTruoc`,`bangthutien`.`SoTienAnSangThangNay` AS `SoTienAnSangThangNay`,`bangthutien`.`AnToiThangTruoc` AS `AnToiThangTruoc`,`bangthutien`.`SoTienAnToiThangTruoc` AS `SoTienAnToiThangTruoc`,`bangthutien`.`SoTienAnToiThangNay` AS `SoTienAnToiThangNay`,`bangthutien`.`SoTienNangKhieu` AS `SoTienNangKhieu`,`bangthutien`.`SoTienTruyThu` AS `SoTienTruyThu`,`bangthutien`.`SoTienDoDung` AS `SoTienDoDung`,`bangthutien`.`SoTienDieuHoa` AS `SoTienDieuHoa`,`bangthutien`.`NgayTinh` AS `NgayTinh`,`bangthutien`.`STT` AS `STT`,`bangthutien`.`IsDeleted` AS `IsDeleted`,`bangthutien`.`DateCreated` AS `DateCreated`,100000000000000 AS `SoTienAnSangConLai`,100000000000000 AS `SoTienAnToiConLai`,100000000000000 AS `ThanhTien`,100000000000000 AS `TienAnSua`,100000000000000 AS `PhuPhi`,100000000000000 AS `BanTru`,100000000000000 AS `HocPhi`,100000000000000 AS `KhoanThuChinh`,'' AS `HoTen`,NULL AS `NgayNopLan1`,100000000000000 AS `SoTienNopLan1`,NULL AS `NgayNopLan2`,100000000000000 AS `SoTienNopLan2`,`bangthutien`.`GhiChu` AS `GhiChu`,' ' AS `Lop`,' ' AS `SoBienLai`,' ' AS `TongThu`,100000000000000 AS `TienAn` from `bangthutien`;

-- --------------------------------------------------------

--
-- Structure for view `viewhoctap`
--
DROP TABLE IF EXISTS `viewhoctap`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewhoctap` AS select `hs`.`HocSinhId` AS `HocSinhId`,`hs`.`HoDem` AS `HoDem`,`hs`.`Ten` AS `Ten`,`hs`.`GioiTinh` AS `GioiTinh`,`hs`.`NgaySinh` AS `NgaySinh`,`hs`.`NgayVaoLop` AS `NgayVaoLop`,`hs`.`DanToc` AS `DanToc`,`hs`.`DienCuTru` AS `DienCuTru`,`hs`.`DienThoai` AS `DienThoai`,`hs`.`HoTenCha` AS `HoTenCha`,`hs`.`HoTenMe` AS `HoTenMe`,`hs`.`TinhThanhPhoId` AS `TinhThanhPhoId`,`hs`.`QuanHuyenId` AS `QuanHuyenId`,`hs`.`PhuongXaId` AS `PhuongXaId`,`hs`.`DiaChi` AS `DiaChi`,`hs`.`ThoiHoc` AS `ThoiHoc`,`hs`.`CreatedDate` AS `CreatedDate`,`bct`.`SoNgayNghi` AS `SoNgayNghiThang`,`bct`.`NgayTinh` AS `NgayTinh`,`ht`.`HocTapId` AS `HocTapId`,`l`.`LopId` AS `LopId`,`l`.`Name` AS `LopName`,`hsl`.`HocSinhLopId` AS `HocSinhLopId`,`hsl`.`DateJoin` AS `DateJoin`,`hsl`.`DateLeft` AS `DateLeft` from ((((`hocsinh` `hs` left join `hocsinh_lop` `hsl` on((`hs`.`HocSinhId` = `hsl`.`HocSinhId`))) left join `hoctap` `ht` on((`hsl`.`HocSinhLopId` = `ht`.`HocSinhLopId`))) join `lop` `l` on((`hsl`.`LopId` = `l`.`LopId`))) left join `baocaothang` `bct` on((`ht`.`HocTapId` = `bct`.`HocTapId`)));

-- --------------------------------------------------------

--
-- Structure for view `viewtaisan`
--
DROP TABLE IF EXISTS `viewtaisan`;

CREATE ALGORITHM=MERGE DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewtaisan` AS select `ts`.`TaiSanId` AS `TaiSanId`,`ts`.`Ten` AS `Ten`,`pc`.`Ngay` AS `NgayNhap`,`ts`.`DonViTinh` AS `DonViTinh`,`ts`.`SoLuong` AS `SoLuong`,`ts`.`DonGia` AS `DonGia`,`ts`.`SoChungTu` AS `SoChungTu`,`ts`.`NgayChungTu` AS `NgayChungTu`,`pc`.`SoTien` AS `ThanhTien`,`pc`.`PhieuChiId` AS `PhieuChiId`,`pc`.`PhanLoaiChiId` AS `PhanLoaiChiId` from (`taisan` `ts` join `phieuchi` `pc` on((`ts`.`PhieuChiId` = `pc`.`PhieuChiId`))) WITH CASCADED CHECK OPTION;

-- --------------------------------------------------------

--
-- Structure for view `viewtaisanforbangiaotaisan`
--
DROP TABLE IF EXISTS `viewtaisanforbangiaotaisan`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `viewtaisanforbangiaotaisan` AS (select `viewtaisan`.`TaiSanId` AS `TaiSanId`,`viewtaisan`.`Ten` AS `Ten`,`viewtaisan`.`NgayNhap` AS `NgayNhap`,`viewtaisan`.`DonViTinh` AS `DonViTinh`,`viewtaisan`.`SoLuong` AS `SoLuong`,`viewtaisan`.`DonGia` AS `DonGia`,`viewtaisan`.`SoChungTu` AS `SoChungTu`,`viewtaisan`.`NgayChungTu` AS `NgayChungTu`,`viewtaisan`.`ThanhTien` AS `ThanhTien`,`viewtaisan`.`PhieuChiId` AS `PhieuChiId`,`viewtaisan`.`PhanLoaiChiId` AS `PhanLoaiChiId` from `viewtaisan`) union (select `viewbangiaotaisan`.`TaiSanId` AS `TaiSanId`,`viewbangiaotaisan`.`Ten` AS `Ten`,`viewbangiaotaisan`.`NgayNhap` AS `NgayNhap`,`viewbangiaotaisan`.`DonViTinh` AS `DonViTinh`,(`viewbangiaotaisan`.`SoLuongBanGiao` * -(1)) AS `SoLuong`,`viewbangiaotaisan`.`DonGia` AS `DonGia`,`viewbangiaotaisan`.`SoChungTu` AS `SoChungTu`,`viewbangiaotaisan`.`NgayChungTu` AS `NgayChungTu`,0 AS `ThanhTien`,`viewbangiaotaisan`.`PhieuChiId` AS `PhieuChiId`,0 AS `PhanLoaiChiId` from `viewbangiaotaisan` where (isnull(`viewbangiaotaisan`.`NgayHetHan`) or (`viewbangiaotaisan`.`NgayHetHan` >= now())));

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
