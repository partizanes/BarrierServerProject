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

/*Table structure for table `message` */

DROP TABLE IF EXISTS `message`;

CREATE TABLE `message` (
  `userid` tinyint(3) unsigned NOT NULL,
  `msg_priority` int(2) unsigned NOT NULL,
  `msg_color` tinytext NOT NULL,
  `msg` text NOT NULL,
  `msg_datetime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `message` */

insert  into `message`(`userid`,`msg_priority`,`msg_color`,`msg`,`msg_datetime`) values (2,3,'ffff0080','бля-бля-бля))))))))))))))','2013-04-05 14:55:42'),(2,2,'ff00ff40','длпвоорор','2013-04-05 14:57:25'),(2,3,'Aqua','дооолололошошщ','2013-04-05 14:58:49');

/*Table structure for table `operations` */

DROP TABLE IF EXISTS `operations`;

CREATE TABLE `operations` (
  `id` bigint(20) unsigned NOT NULL,
  `operation` tinyint(2) unsigned NOT NULL,
  `count` float(7,3) unsigned NOT NULL,
  `price` mediumint(7) unsigned NOT NULL,
  `inactive` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `operations` */

insert  into `operations`(`id`,`operation`,`count`,`price`,`inactive`) values (14,51,24.862,18900,0),(14,51,2.378,22000,0),(15,51,4.000,3200,0),(15,51,37.000,3650,0),(16,51,46.000,3650,0),(17,51,1.000,13000,0),(19,51,2.000,13000,0),(21,51,4.000,64650,0);

/*Table structure for table `priority` */

DROP TABLE IF EXISTS `priority`;

CREATE TABLE `priority` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `bar` tinytext NOT NULL,
  `name` tinytext NOT NULL,
  `turn_price` mediumint(7) unsigned NOT NULL,
  `count` float(7,3) unsigned NOT NULL,
  `sailed` float(7,3) unsigned DEFAULT '0.000',
  `status` tinyint(2) NOT NULL,
  `status_text` tinytext NOT NULL,
  `current_price_ukm` mediumint(7) unsigned NOT NULL,
  `date` datetime NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;

/*Data for the table `priority` */

insert  into `priority`(`id`,`bar`,`name`,`turn_price`,`count`,`sailed`,`status`,`status_text`,`current_price_ukm`,`date`) values (14,'04377','С/М ШАМПИНЬОНЫ РЕЗАНЫЕ ВЕС.',18900,16.000,27.240,3,'',18900,'2013-04-01 14:11:31'),(15,'460562700586','ПРОД.ЙОГУРТН.2.5% ФРУТТИС КЛУБНИКА 115Г',3650,18.000,41.000,3,'',3200,'2013-03-23 14:11:31'),(16,'460562700588','ПРОД.ЙОГУРТН.2.5% ФРУТТИС ПЕРСИК/МАРАК. ',3650,6.000,46.000,3,'',3200,'2013-03-23 14:11:31'),(17,'590112601101','СЫР ПЛАВЛ.СЛИВОЧНЫЙ 40%  140Г',13000,3.000,1.000,0,'',13000,'2013-03-23 14:11:31'),(18,'590112601056','СЫР ПЛАВЛ.С ВЕТЧИНОЙ 40%  140Г',13000,2.000,NULL,1,'',13000,'2013-03-23 14:11:31'),(19,'590112601198','СЫР ПЛАВЛ.40% С ДОБ.СЫРА  ГОУДА 140Г',13000,4.000,2.000,0,'',13000,'2013-03-23 14:11:31'),(20,'800002000004','ВЕРМУТ ЧИНЗАНО ЭКСТРА ДРАЙ БЕЛЫЙ 1Л',146650,16.000,NULL,1,'',146650,'2013-03-14 14:11:31'),(21,'460404901226','СМС BIMAX 100 ПЯТЕН АВТОМАТ КОЛОР 3КГ',64650,2.000,4.000,3,'',64650,'2013-03-06 14:11:31');

/*Table structure for table `sendpos` */

DROP TABLE IF EXISTS `sendpos`;

CREATE TABLE `sendpos` (
  `id` tinyint(2) unsigned NOT NULL,
  `price` smallint(5) unsigned NOT NULL,
  `kod_isp` tinyint(2) unsigned NOT NULL,
  `datetime` datetime NOT NULL,
  `action` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `sendpos` */

/*Table structure for table `tasks` */

DROP TABLE IF EXISTS `tasks`;

CREATE TABLE `tasks` (
  `tasks_id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `priority_id` bigint(20) unsigned NOT NULL,
  `user_group` int(2) NOT NULL,
  `task_text` longtext NOT NULL,
  `user_id` tinyint(4) NOT NULL,
  `priority` tinyint(4) NOT NULL,
  `date` datetime NOT NULL,
  `inactive` tinyint(1) unsigned NOT NULL,
  UNIQUE KEY `tasks_id` (`tasks_id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

/*Data for the table `tasks` */

insert  into `tasks`(`tasks_id`,`priority_id`,`user_group`,`task_text`,`user_id`,`priority`,`date`,`inactive`) values (1,14,1,' Продажи дороже цены очередности [22000]',0,6,'2013-04-05 14:06:16',0),(2,14,1,' Нужна прогрузка цены на кассу',0,3,'2013-04-05 14:06:16',0),(3,14,2,' Продано больше чем нужно',0,3,'2013-04-05 14:06:16',0),(4,15,1,' Нужна прогрузка цены на кассу',0,3,'2013-04-05 14:07:29',0),(5,15,2,' Продано больше чем нужно',0,3,'2013-04-05 14:07:29',0),(6,16,1,' Нужна прогрузка цены на кассу',2,3,'2013-04-05 14:07:44',0),(7,16,2,' Продано больше чем нужно',0,3,'2013-04-05 14:07:44',0),(8,18,1,' 590112601056 Нет продаж.Число дней: [13] ',0,1,'2013-04-05 14:14:53',0),(9,20,1,' 800002000004 Нет продаж.Число дней: [22] ',0,1,'2013-04-05 14:23:36',0),(10,21,1,' Нужна прогрузка цены на кассу',2,3,'2013-04-05 14:25:31',0),(11,21,2,' Продано больше чем нужно',0,3,'2013-04-05 14:25:31',0),(12,15,1,' Цена на кассе меньше цены очередности [3200]',0,8,'2013-04-05 14:43:54',0),(13,16,1,' Цена на кассе меньше цены очередности [3200]',0,8,'2013-04-05 14:43:54',0),(14,15,2,' Продажи дешевле цены очередности [3200]',0,5,'2013-04-05 21:51:27',0);

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `users` */

insert  into `users`(`id`,`username`,`hash`,`group`,`online`,`ip`,`tasks_count`,`tasks_failed`) values (1,'partizanes','1ed6ad31cf5f6e0d10e18f8db3f08c9f',2,0,'127.0.0.1',NULL,NULL),(2,'user','91ff7d7746319a2d34f93537c3e54173',1,0,'192.168.1.4',NULL,NULL);

/*Table structure for table `version` */

DROP TABLE IF EXISTS `version`;

CREATE TABLE `version` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` text NOT NULL,
  `ver` int(10) unsigned NOT NULL,
  `source` text NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Data for the table `version` */

insert  into `version`(`id`,`name`,`ver`,`source`) values (1,'PrioritySales',2,'\\\\SERVERHOME\\net\\PrioritySales.exe');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
