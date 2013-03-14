﻿-- Создаем пользователей
CREATE USER 'BarrierServer'@'%' IDENTIFIED BY '***REMOVED***';
CREATE USER 'BarrierServerR'@'%' IDENTIFIED BY '***REMOVED***';
CREATE USER 'PrioritySail'@'%' IDENTIFIED BY '***REMOVED***';
CREATE USER 'PrioritySailR'@'%' IDENTIFIED BY '***REMOVED***';
-- Полный доступ к своей базе
GRANT  ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, EXECUTE, INDEX, INSERT, LOCK TABLES, REFERENCES, SELECT, SHOW VIEW, TRIGGER, UPDATE ON  `barrierserver`.* TO 'BarrierServer'@'%' WITH GRANT OPTION;
GRANT  ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, EXECUTE, INDEX, INSERT, LOCK TABLES, REFERENCES, SELECT, SHOW VIEW, TRIGGER, UPDATE ON  `barrierserver`.* TO 'BarrierServerR'@'%' WITH GRANT OPTION;
GRANT  ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, EXECUTE, INDEX, INSERT, LOCK TABLES, REFERENCES, SELECT, SHOW VIEW, TRIGGER, UPDATE ON  `barrierserver`.* TO 'PrioritySail'@'%' WITH GRANT OPTION;
GRANT  ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, EXECUTE, INDEX, INSERT, LOCK TABLES, REFERENCES, SELECT, SHOW VIEW, TRIGGER, UPDATE ON  `barrierserver`.* TO 'PrioritySailR'@'%' WITH GRANT OPTION;
-- Права на чтение из базы укм
GRANT  SELECT ON  `ukmserver`.* TO 'BarrierServer'@'%' ;
GRANT  SELECT ON  `ukmserver`.* TO 'BarrierServerR'@'%' ;
GRANT  SELECT ON  `ukmserver`.* TO 'PrioritySail'@'%' ;
GRANT  SELECT ON  `ukmserver`.* TO 'PrioritySailR'@'%' ;