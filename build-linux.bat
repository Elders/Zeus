@echo off
CALL dotnet restore .\src\unix\SysHealth.Linux.Console
CALL dotnet publish .\src\unix\SysHealth.Linux.Console -o .\docker\build\unix -r debian.8.2-x64
