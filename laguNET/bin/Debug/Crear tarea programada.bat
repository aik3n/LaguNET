:: crear tarea programada para que inicie con windows

:: Directorio del .bat cuando se ejecuta normal
::echo %CD% 
:: Directorio del .bat cuando se ejecuta como admin
::echo %~dp0


@echo off
echo INFORMACION:
echo - EJECUTAR EN MODO ADMINISTRADOR
echo - NO FUNCIONA CON LAS RUTAS CON ESPACIOS EN BLANCO
PAUSE

:: Borra todos los .xml por si estubieran ya creados
::DEL "%~dp0laguNET.xml"
::DEL "%~dp0laguNET2.xml"
::DEL "%~dp0laguNET3.xml"
DEL "%~dp0*.xml"
@echo on



:: Se crea una tarea programada al iniciar usuario, 
:: por defecto esta activado la opcion "No iniciar solo si estas enchufado"
SCHTASKS /CREATE /SC ONLOGON /RL HIGHEST /TN "\LaguNET" /TR "%~dp0laguNET.exe"

:: Exportar la tarea a xml
SCHTASKS /QUERY /XML /TN "\LaguNET" >> "%~dp0laguNET.xml"

:: Borrar la tarea sin preguntar
SCHTASKS /DELETE /TN "\LaguNET" /F


:: Se modifica el .xml para quitar la opcion "No iniciar solo si estas enchufado"
:: se crea un nuevo .xml modificado
@echo off

::
:: Edita la clave:  <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
::
setlocal EnableDelayedExpansion
set file=%~dp0laguNET.xml

(for /F "delims=" %%a in ('type "%file%"') do (
   set "line=%%a"
   set "newLine=!line:DisallowStartIfOnBatteries>=!"
   if "!newLine!" neq "!line!" (
      set "newLine=<DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>"
   )
   echo !newLine!
)) > "%~dp0laguNET2.xml"


::
:: Edita la clave: <StopIfGoingOnBatteries>true</StopIfGoingOnBatteries>
::
setlocal EnableDelayedExpansion
set file=%~dp0laguNET2.xml

(for /F "delims=" %%a in ('type "%file%"') do (
   set "line=%%a"
   set "newLine=!line:StopIfGoingOnBatteries>=!"
   if "!newLine!" neq "!line!" (
      set "newLine=<StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>"
   )
   echo !newLine!
)) > "%~dp0laguNET3.xml"


:: Se importa la tarea desde el .xml modificado
SCHTASKS /CREATE /XML "%~dp0laguNET3.xml" /TN "\LaguNET" 

:: Se borran todos los .xml que ya no hacen falta
::DEL "%~dp0laguNET.xml"
::DEL "%~dp0laguNET2.xml"
::DEL "%~dp0laguNET3.xml"
DEL "%~dp0*.xml"
pause

:: RECURSOS
:: https://www.windowscentral.com/how-create-task-using-task-scheduler-command-prompt
:: https://stackoverflow.com/questions/17054275/changing-tag-data-in-an-xml-file-using-windows-batch-file
:: https://www.tenforums.com/general-support/168101-batch-run-admin-current-path-instead-system32-folder.html
:: https://stackoverflow.com/questions/5553040/batch-file-for-loop-with-spaces-in-dir-name

