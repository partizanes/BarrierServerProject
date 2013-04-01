/*
SQLyog Ultimate v8.62 
MySQL - 5.5.18 : Database - barrierserver2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver2` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver2`;

/*Table structure for table `operations` */

DROP TABLE IF EXISTS `operations`;

CREATE TABLE `operations` (
  `id` bigint(20) unsigned NOT NULL,
  `operation` tinyint(2) unsigned NOT NULL,
  `count` tinyint(3) unsigned NOT NULL,
  `price` tinyint(3) unsigned NOT NULL,
  `inactive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `operations` */

/*Table structure for table `out_operation` */

DROP TABLE IF EXISTS `out_operation`;

CREATE TABLE `out_operation` (
  `id` bigint(20) NOT NULL,
  `operation` int(2) NOT NULL,
  `count` int(5) NOT NULL,
  `price` mediumint(7) NOT NULL,
  `time_oper` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `out_operation` */

/*Table structure for table `priority` */

DROP TABLE IF EXISTS `priority`;

CREATE TABLE `priority` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `bar` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `turn_price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(2) NOT NULL,
  `status_text` tinytext NOT NULL,
  `current_price_ukm` mediumint(7) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

/*Data for the table `priority` */

insert  into `priority`(`id`,`bar`,`name`,`turn_price`,`count`,`sailed`,`status`,`status_text`,`current_price_ukm`,`date`) values (1,'481353800219','НЕКТАР СОЧНЫЙ ФРУКТ МУЛЬТИВИТАМИННЫЙ 2Л',19000,8.000,0.000,0,'',0,'2013-04-01 11:29:41'),(2,'4813538002196','Нектар мультфруктовый',15000,8.000,0.000,0,' ',0,'2013-04-01 20:00:00'),(3,'4813538002196','Нектар мультфруктовый',15000,8.000,0.000,0,' ',0,'2013-04-01 20:00:00'),(4,'4813538002196','Нектар мультфруктовый',15000,8.000,0.000,0,' ',0,'2013-04-01 20:00:00'),(5,'4813538002196','Нектар мультфруктовый',15000,8.000,0.000,0,' ',0,'2013-04-01 20:00:00'),(6,'481353800219','НЕКТАР СОЧНЫЙ ФРУКТ МУЛЬТИВИТАМИННЫЙ 2Л',19000,5.000,0.000,0,'',0,'2013-04-01 12:34:44');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) DEFAULT NULL,
  `online` tinyint(1) DEFAULT NULL,
  `ip` tinytext,
  `tasks_count` int(11) DEFAULT NULL,
  `tasks_failed` int(11) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`,`tasks_failed`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',0,0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',0,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
