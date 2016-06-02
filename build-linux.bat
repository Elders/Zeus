@echo off
CALL dotnet restore .\src\Zeus
CALL dotnet publish .\src\Zeus -o .\docker\build\unix -r debian.8.2-x64 -c Release
