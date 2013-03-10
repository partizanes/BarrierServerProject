/*
SQLyog Ultimate v8.62 
MySQL - 5.0.84-community-nt : Database - barrierserver
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`barrierserver` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `barrierserver`;

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `barcode` longtext NOT NULL,
  `name` longtext NOT NULL,
  `price` bigint(20) NOT NULL,
  `count` double NOT NULL,
  `sailed` decimal(10,3) NOT NULL,
  `status` int(11) NOT NULL,
  `date` datetime NOT NULL,
  `flag` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`count`,`sailed`,`status`,`date`,`flag`) values ('30176        ','ХЕК (ТУШКА) С/М 300+                              ',38250,2,'0.934',0,'2013-03-10 08:50:50',0),('481012801011 ','ВАФЕЛЬНЫЙ БАТОНЧИК СЕМКА ХАЛВИЧ С АРАХИС          ',2250,18,'0.000',0,'2013-03-10 15:33:07',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
