/*
SQLyog Ultimate v8.62 
MySQL - 5.5.18 : Database - barrierserver
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

/*Table structure for table `sendprice` */

DROP TABLE IF EXISTS `sendprice`;

CREATE TABLE `sendprice` (
  `id` tinyint(2) unsigned NOT NULL,
  `price` smallint(5) unsigned NOT NULL,
  `kod_isp` tinyint(2) unsigned NOT NULL,
  `datetime` datetime NOT NULL,
  `action` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sendprice` */

insert  into `sendprice`(`id`,`price`,`kod_isp`,`datetime`,`action`) values (98,11000,23,'2013-02-04 14:56:05','К3'),(98,11000,23,'2013-02-04 14:56:18','К3'),(98,11000,12,'2013-02-06 13:32:37','К3'),(98,11000,12,'2013-02-06 13:33:42','К3'),(98,11000,24,'2013-02-11 09:22:38','К3'),(98,11000,23,'2013-02-14 09:25:57','К3'),(100,11000,23,'2013-02-04 14:56:05','К3'),(100,11000,23,'2013-02-04 14:56:18','К3'),(100,11000,12,'2013-02-06 13:32:37','К3'),(100,11000,12,'2013-02-06 13:33:42','К3'),(100,11000,24,'2013-02-11 09:22:38','К3'),(100,11000,23,'2013-02-14 09:25:57','К3');

/*Table structure for table `state` */

DROP TABLE IF EXISTS `state`;

CREATE TABLE `state` (
  `barcode` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `count` float(7,3) NOT NULL,
  `sailed` float(7,3) NOT NULL,
  `status` tinyint(1) unsigned NOT NULL,
  `date` datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `state` */

insert  into `state`(`barcode`,`name`,`price`,`sailprice`,`count`,`sailed`,`status`,`date`) values ('481353800042 ','НЕКТАР СОЧНЫЙ ФРУКТ АПЕЛЬСИНОВЫЙ 2Л               ',20000,0,5.000,0.000,1,'2013-02-01 15:46:46'),('544900000484 ','НАПИТОК Б/А ФАНТА АПЕЛЬСИН РЕТ 2Л                 ',11000,11000,5.000,6.000,3,'2013-02-01 15:46:46'),('481007400015 ','МИН.ВОДА МИНСК-4 2Л                               ',5450,0,8.000,0.000,0,'2013-03-30 21:25:27');

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `barcode` text NOT NULL,
  `sailprice` mediumint(7) NOT NULL,
  `group` int(2) NOT NULL,
  `text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=101 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`id`,`barcode`,`sailprice`,`group`,`text`,`user_id`,`priority`,`date`) values (99,'481353800042',0,1,'долго не продается.Число дней: 57',0,1,'2013-02-01 15:46:46'),(100,'544900000484',11000,2,'Продано больше чем было в очередности,необходима прогрузка цены на кассу',0,3,'2013-02-01 15:46:46');

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `username` tinytext NOT NULL,
  `hash` tinytext NOT NULL,
  `group` tinyint(2) DEFAULT NULL,
  `status` tinyint(1) DEFAULT NULL,
  `ip` tinytext,
  `tasks_count` int(11) DEFAULT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`status`,`ip`,`tasks_count`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',0),(4,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'127.0.0.1',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
