/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE IF NOT EXISTS `lang_teacher` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */;
USE `lang_teacher`;

CREATE TABLE IF NOT EXISTS `country` (
  `country_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `country_name` varchar(50) DEFAULT NULL,
  `is_active` tinyint(3) unsigned DEFAULT '1',
  `created_on` datetime DEFAULT NULL,
  `updated_on` datetime DEFAULT NULL,
  PRIMARY KEY (`country_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `dictionary` (
  `dic_id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `language_id` int(10) unsigned NOT NULL DEFAULT '0',
  `user_info_id` int(10) unsigned NOT NULL DEFAULT '0',
  `word` varchar(500) NOT NULL DEFAULT '0',
  `pronouncation` blob NOT NULL,
  `tts` varchar(50) NOT NULL,
  `meaning` varchar(50) NOT NULL DEFAULT '0',
  `parts_of_speech` varchar(50) NOT NULL DEFAULT '0',
  `synonyms` longtext NOT NULL,
  `antonyms` longtext NOT NULL,
  `example` varchar(1000) NOT NULL DEFAULT '0',
  `created_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`dic_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `language_details` (
  `language_id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `language_name` varchar(50) NOT NULL,
  `is_active` tinyint(4) DEFAULT '1',
  `created_on` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`language_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `lesson_details` (
  `lesson_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `lesson_name` varchar(200) DEFAULT NULL,
  `lesson_severity_id` tinyint(3) unsigned DEFAULT NULL,
  `lesson_file_path` varchar(50) DEFAULT NULL,
  `is_active` tinyint(4) DEFAULT NULL,
  `created_on` datetime DEFAULT NULL,
  `updated_on` datetime DEFAULT NULL,
  PRIMARY KEY (`lesson_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `lesson_severity` (
  `severity_id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `severity_type` varchar(50) DEFAULT NULL,
  `is_active` tinyint(3) unsigned DEFAULT '1',
  `created_on` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`severity_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `login_roles` (
  `role_id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `role_type` varchar(50) NOT NULL,
  `is_active` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `created_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `user_language_details` (
  `ul_id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `user_info_id` int(11) unsigned NOT NULL,
  `against_learning_language_id` int(11) unsigned DEFAULT NULL,
  `towards_learning_language_id` int(11) unsigned DEFAULT NULL,
  `last_learning_lesson_id` tinyint(3) unsigned DEFAULT NULL,
  `against_teaching_language_id` int(11) unsigned DEFAULT NULL,
  `towards_teaching_language_id` int(11) unsigned DEFAULT NULL,
  `last_teaching_lesson_id` tinyint(4) unsigned DEFAULT NULL,
  `created_on` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ul_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `user_lesson_details` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `lesson_id` int(10) unsigned DEFAULT NULL,
  `user_info_id` int(10) unsigned DEFAULT NULL,
  `points_earned` int(10) unsigned DEFAULT '0',
  `total_points` int(10) unsigned DEFAULT '0',
  `is_active` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `created_on` datetime DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `user_login_details` (
  `user_details_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `first_name` varchar(50) NOT NULL DEFAULT '0',
  `last_name` varchar(50) NOT NULL DEFAULT '0',
  `email_id` varchar(50) NOT NULL DEFAULT '0',
  `mobile` varchar(50) NOT NULL DEFAULT '0',
  `country_id` smallint(5) unsigned NOT NULL DEFAULT '0',
  `role_id` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `user_name` varchar(50) NOT NULL DEFAULT '0',
  `password` varchar(9999) NOT NULL DEFAULT '0',
  `operating_system_used` varchar(50) DEFAULT 'NA',
  `ip_address` varchar(50) DEFAULT 'NA',
  `is_active` tinyint(4) NOT NULL DEFAULT '1',
  `created_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_on` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`user_details_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
