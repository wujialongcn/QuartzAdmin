@echo OFF

@echo ɾ��SVN�汾������Ϣ
@echo ȷ��ɾ����ǰ�ļ���(�����ļ���)�İ汾������Ϣ��? (Y/N)
set /p dialog=
if /i "%dialog%" NEQ "Y" goto :end
::FOR /R [[drive:]path] %variable IN (set) DO command [command-parameters]
::
::    ����� [drive:]path Ϊ����Ŀ¼����ָ��ÿ��Ŀ¼�е�
::    FOR ��䡣����� /R ��û��ָ��Ŀ¼����ʹ�õ�ǰ
::    Ŀ¼�����set��Ϊһ������(.)�ַ�����ö�ٸ�Ŀ¼��(�����ļ�)��
@echo ��ʼ���... ...
for /r . %%I in (.) do @if exist "%%I\.svn" (
    @echo "Folder=[%%I\.svn]"
    @rd /s /q "%%I\.svn"
)
:end
@echo Completed.
pause
