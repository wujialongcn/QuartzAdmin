@echo OFF

@echo 删除SVN版本控制信息
@echo 确定删除当前文件夹(含子文件夹)的版本控制信息吗? (Y/N)
set /p dialog=
if /i "%dialog%" NEQ "Y" goto :end
::FOR /R [[drive:]path] %variable IN (set) DO command [command-parameters]
::
::    检查以 [drive:]path 为根的目录树，指向每个目录中的
::    FOR 语句。如果在 /R 后没有指定目录，则使用当前
::    目录。如果set仅为一个单点(.)字符，则枚举该目录树(不含文件)。
@echo 开始清除... ...
for /r . %%I in (.) do @if exist "%%I\.svn" (
    @echo "Folder=[%%I\.svn]"
    @rd /s /q "%%I\.svn"
)
:end
@echo Completed.
pause
