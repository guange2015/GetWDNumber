@echo on

@rem É¾³ýSVN°æ±¾¿ØÖÆÄ¿Â¼

@rem for /r . %%a in (.) do @if exist "%%a\.svn" @echo "%%a\.svn"
@for /r . %%a in (.) do @if exist "%%a\.svn" rd /s /q "%%a\.svn"

@echo delete .svn completed.


@rem for /r . %%a in (.) do @if exist "%%a\Debug" @echo "%%a\Debug"
@for /r . %%a in (.) do @if exist "%%a\Debug" rd /s /q "%%a\Debug"
@echo delete debug completed.

@rem for /r . %%a in (.) do @if exist "%%a\Release" @echo "%%a\Release"
@for /r . %%a in (.) do @if exist "%%a\Release" rd /s /q "%%a\Release"
@echo delete Release completed.

@pause

