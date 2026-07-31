ALTER TABLE `phieuchi` ADD COLUMN `PaymentType` ENUM('CASH','TRANSFER') DEFAULT 'CASH' NULL AFTER `SoTien`; 


DELIMITER $$

USE `qlmamnon_20250919`$$

DROP PROCEDURE IF EXISTS `getPhieuChiByDateRange`$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `getPhieuChiByDateRange`(IN `@fromDate` DATE, IN `@toDate` DATE, IN `@phanloaichiids` TEXT)
    NO SQL
BEGIN SET @sql = CONCAT('SELECT * FROM `phieuchi` pc WHERE pc.Ngay>="', DATE_FORMAT(`@fromDate`, '%Y-%m-%d'), '" AND pc.Ngay<="', DATE_FORMAT(`@toDate`, '%Y-%m-%d'), '" AND pc.PhanLoaiChiId IN (', `@phanloaichiids`,')'); PREPARE stmt FROM @sql; EXECUTE stmt; DEALLOCATE PREPARE stmt; END$$

DELIMITER ;