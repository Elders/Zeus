@echo off
CALL dotnet restore .\src\Zeus
CALL dotnet publish .\src\Zeus -o .\build\Release\debian.8.2-x64 -r debian.8.2-x64 -f netcoreapp1.0 -c Release
CALL dotnet publish .\src\Zeus -o .\build\Release\win10-x64 -r win10-x64 -f net46 -c Release
CALL dotnet publish .\src\Zeus -o .\build\Release\win10-x86 -r win10-x86 -f net46 -c Release
CALL dotnet publish .\src\Zeus -o .\build\Release\centos.7-x64 -r centos.7-x64 -f netcoreapp1.0 -c Release
CALL dotnet publish .\src\Zeus -o .\build\Release\ubuntu.15.04-x64 -r ubuntu.15.04-x64 -f netcoreapp1.0 -c Release
