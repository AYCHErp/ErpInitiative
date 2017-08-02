@echo off
bundler\SqlBundler.exe ..\..\..\..\ "db/PostgreSQL/2.x/2.0" true
copy in.sql in-sample.sql
del in.sql
copy in-sample.sql ..\..\in-sample.sql