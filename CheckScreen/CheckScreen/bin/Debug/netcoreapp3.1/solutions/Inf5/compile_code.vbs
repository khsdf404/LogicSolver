Set oShell = CreateObject ("Wscript.Shell")
Dim strArgs
strArgs = "cmd /c D:\YandexDisk\1__Dir\Projects\Simulation\Files\Compiler\csc.exe /target:winexe Code.cs "
oShell.Run strArgs, 0, false