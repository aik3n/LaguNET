:: crear tarea programada para que inicie con windows

:: Directorio del .bat cuando se ejecuta normal
::echo %CD% 
:: Directorio del .bat cuando se ejecuta como admin
::echo %~dp0



SCHTASKS /CREATE /SC ONLOGON /RL HIGHEST /TN "\LaguNET" /TR " %~dp0laguNET.exe "

Pause


:: RECURSOS
:: https://www.windowscentral.com/how-create-task-using-task-scheduler-command-prompt
:: https://www.tenforums.com/general-support/168101-batch-run-admin-current-path-instead-system32-folder.html