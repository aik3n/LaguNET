:: crear tarea programada para que inicie con windows

:: Directorio del .bat cuando se ejecuta normal
::echo %CD% 
:: Directorio del .bat cuando se ejecuta como admin
::echo %~dp0

::color 02
::mode con cols=100 lines=20

SCHTASKS /CREATE /SC ONLOGON /RL HIGHEST /TN "\LaguNET" /TR " %~dp0laguNET.exe " /RU SYSTEM
@echo off
echo ------------------------------------------------------------------------------
echo A continuacion se abrira el programador de tareas...
echo .
echo Configurar programador de tareas:
echo Paso 1. Doble click en la tarea "LaguNET" para editar
echo Paso 2. en el apartado "condiciones" 
echo Paso 3. desclickar opciones de energia 
echo Paso 4. Guardar 
echo ------------------------------------------------------------------------------
echo.
pause

taskschd.msc /s


:: RECURSOS
:: https://www.windowscentral.com/how-create-task-using-task-scheduler-command-prompt
:: https://www.tenforums.com/general-support/168101-batch-run-admin-current-path-instead-system32-folder.html