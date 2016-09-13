@echo off

call :clean .\build

CALL dotnet restore .\src\Zeus

call :publish debian.8.2-x64 netcoreapp1.0

call :publish win10-x64 net46

call :publish win10-x86 net46

call :publish centos.7-x64 netcoreapp1.0

call :publish ubuntu.15.04-x64 netcoreapp1.0


















EXIT /B 0

:publish
SET runtime=%~1
SET framework=%~2

SET toolLocation=%LocalAppData%\%tool%\%tool%.exe
if "%runtime%"=="" (
    echo "Runtime name not set"
	EXIT /B 0
)

if "%framework%"=="" (
    echo "Framework Url not set"
	EXIT /B 0
)
echo "Building for %runtime% targeting %framework%"

CALL dotnet publish .\src\Zeus -o .\build\Release\%runtime% -r %runtime% -f %framework% -c Release

set zip="C:\Program Files\7-Zip\7z.exe"

echo Publishing .\build\Release\%runtime%.zip

%zip% a -tzip .\build\Release\zeus-%runtime%.zip .\build\Release\%runtime%
EXIT /B 0

:clean
set targetdir=%~1
del /q %targetdir%\*
for /d %%x in (%targetdir%\*) do @rd /s /q ^"%%x^"
EXIT /B 0

:use-tool
SET tool=%~1
SET url=%~2

if "%~3"=="" (
    SET toolLocation=%LocalAppData%\%tool%\%tool%.exe
) else (
	SET toolLocation=%LocalAppData%\%tool%\%tool%.%~3
)

SET toolLocation=%LocalAppData%\%tool%\%tool%.exe
if "%tool%"=="" (
    echo "Tool name not set"
	EXIT /B 0
)

if "%url%"=="" (
    echo "Tool Url not set"
	EXIT /B 0
)
echo Using %tool%
IF NOT EXIST %toolLocation% (
	echo Downloading %url%
	IF NOT EXIST %LocalAppData%\%tool% md %LocalAppData%\%tool% 
	@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest '%url%' -OutFile '%toolLocation%'"
) else ( echo %toolLocation% already exists )
set %tool%=%toolLocation%
set path=%path%;%LocalAppData%\%tool%

EXIT /B 0



