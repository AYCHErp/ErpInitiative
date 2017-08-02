@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/SQL Server/2.x/2.0" false
copy in.sql in-blank.sql
del in.sql
copy in-blank.sql ..\..\in-blank.sql