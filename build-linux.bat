@echo off
CALL dotnet restore .\src\unix
CALL dotnet publish .\src\unix -o .\docker\build\unix -r debian.8.2-x64
