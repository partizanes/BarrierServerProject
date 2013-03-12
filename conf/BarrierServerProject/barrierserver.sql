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

insert  into `state`(`barcode`,`name`,`price`,`count`,`sailed`,`status`,`date`,`flag`) values ('30176        ','ХЕК (ТУШКА) С/М 300+                              ',38250,2,'0.000',0,'2013-03-10 08:50:50',0),('481012801011 ','ВАФЕЛЬНЫЙ БАТОНЧИК СЕМКА ХАЛВИЧ С АРАХИС          ',2250,18,'0.000',0,'2013-03-10 15:33:07',0),('544900000028 ','НАПИТОК Б/А КОКА-КОЛА РЕТ 2Л                      ',9900,10,'0.000',0,'2013-02-10 18:12:35',0),('544900000028 ','НАПИТОК Б/А КОКА-КОЛА РЕТ 2Л                      ',9900,18,'0.000',0,'2013-03-10 18:18:07',0),('481353800045 ','НЕКТАР СОЧНЫЙ ФРУКТ МУЛЬТИФРУКТОВЫЙ 2Л            ',16350,10,'0.000',0,'2013-03-10 18:21:40',0),('544900000028 ','НАПИТОК Б/А КОКА-КОЛА РЕТ 2Л                      ',9900,12,'0.000',0,'2013-03-10 18:37:39',0),('460068211199 ','ПИВО КАРЛСБЕРГ СВЕТЛОЕ 0.5Л                       ',12500,18,'0.000',0,'2013-03-12 10:18:03',0),('30176        ','ХЕК (ТУШКА) С/М 300+                              ',23150,8,'0.000',0,'2013-03-12 10:18:03',0),('481006400232 ','ПЕЧЕНЬЕ СЛОДЫЧ С ШОКОЛАДОМ 450Г                   ',7680,8,'0.000',0,'2013-03-01 19:07:29',0),('460704243877 ','НЕКТАР ДОБРЫЙ АПЕЛЬСИНОВЫЙ 2Л                     ',17900,18,'0.000',0,'2013-01-01 19:07:29',0),('481041003192 ','КОНФЕТЫ ТРЮФЕЛЬНЫЕ 100Г                           ',14600,8,'0.000',0,'2013-01-01 19:07:29',0),('481006400232 ','ПЕЧЕНЬЕ СЛОДЫЧ С ШОКОЛАДОМ 450Г                   ',7680,81,'0.000',0,'2013-01-12 19:17:54',0);

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `barcode` text NOT NULL,
  `group` int(2) NOT NULL,
  `text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`id`,`barcode`,`group`,`text`,`user_id`,`priority`) values (13,'544900000028',1,'Товар долго не продается.Число дней: 30',0,1),(14,'481006400232',1,'Товар долго не продается.Число дней: 11',0,1),(15,'460704243877',1,'Товар долго не продается.Число дней: 70',0,1),(16,'481041003192',1,'Товар долго не продается.Число дней: 70',0,1),(17,'481006400232',1,'Товар долго не продается.Число дней: 59',0,1);

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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`status`,`ip`,`tasks_count`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,1,'127.0.0.1',0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
