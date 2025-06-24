@echo off
setlocal EnableDelayedExpansion

set /p SHA1=Enter START commit SHA (older):
set /p SHA2=Enter END commit SHA (newer):

echo.

set OUTPUT_FILE=%TEMP%\gitlog_output.txt

if "%SHA1%"=="%SHA2%" (
    echo version %SHA1% > "%OUTPUT_FILE%"
    git show %SHA1% --quiet --pretty=format:"%%s%%n----------------" >> "%OUTPUT_FILE%"
    type "%OUTPUT_FILE%"
) else (
    echo From: %SHA1% To: %SHA2%
    echo ==========================
    echo From: %SHA1% To: %SHA2% > "%OUTPUT_FILE%"
    echo ========================== >> "%OUTPUT_FILE%"
    git log %SHA1%..%SHA2% --pretty=format:"version %%h%%n%%s%%n----------------" >> "%OUTPUT_FILE%"
    type "%OUTPUT_FILE%"
)

echo.
set /p COPY=Copy to clipboard? (y/n): 
if /i "%COPY%"=="y" (
    type "%OUTPUT_FILE%" | clip
    echo Copied to clipboard.
)

del "%OUTPUT_FILE%"
pause
