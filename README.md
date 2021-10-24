# ScinceBase

Инструкция

1.	БД
	-	Убедиться в наличии установленной MySql 8.0 [ссылка](https://dev.mysql.com/downloads/windows/installer/8.0.html)
	-	Первый вариант (консоль)
		-	Перейти в папку с установленной mysql, стандартный путь: C:\Program Files\MySQL\MySQL Server 8.0\bin  и выполнить в консоли команду mysql.exe -u<имя_пользователя> -p
		-	Ввести пароль, прописать   create database smagin;
		-	exit
		-	mysql.exe -u<имя_пользователя> -p --default-character-set=utf8  smagin < /path/dump/mysql_dump.sql
	-	Второй вариант (через Toad или аналогичный ему клиент)	[ссылка](https://www.toadworld.com/)
2.	Сайт
  -	Убедиться в наличии установленного Net Core 3.1 [ссылка](https://dotnet.microsoft.com/download/dotnet-core/3.1)
	-	Убедиться в наличии установленной node js [инстукция](https://metanit.com/web/nodejs/1.1.php)
	-	Перейти в папку ScinceBase\Web\Web в консоли и выполнить команду npm install для загрузки зависимостей
	-	Выполнить сборку бандлов командой webpack-prod (для любого браузера) или webpack-dev-chrome-only (только для Хрома)
	-	Запустить проект на IIS или Kestrel
