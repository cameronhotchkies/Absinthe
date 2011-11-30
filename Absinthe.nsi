Name "0x90.org's Absinthe Installer"

Outfile AbsintheSetup.exe

InstallDir $PROGRAMFILES\0x90.org\Absinthe

XPStyle on

PageEx "license"
	LicenseData LICENSE
	LicenseForceSelection checkbox
PageExEnd

;Page license
Page components
Page directory
Page instfiles

UninstPage uninstConfirm
UninstPage instfiles

Section "Absinthe"
	SetOutPath $INSTDIR
	File "Absinthe.exe"
	File "README"
	File "Absinthe.exe.manifest"
	File "Absinthe.Core.dll"
	File "msvcr70.dll"
	CreateDirectory "$SMPROGRAMS\0x90.org\Absinthe"
	CreateDirectory "$SMPROGRAMS\0x90.org\Absinthe\plugins"
	CreateShortCut "$SMPROGRAMS\0x90.org\Absinthe\Absinthe.lnk"  "$INSTDIR\Absinthe.exe" "" "" ;"$INSTDIR\MyMod\Help\MyMod.ico"
	CreateShortCut "$SMPROGRAMS\0x90.org\Absinthe\Uninstaller.lnk"  "$INSTDIR\Uninstaller.exe" "Uninstall" "" ;"$INSTDIR\MyMod\Help\MyMod.ico"
	; WX.Net should probably be detected and installed properly (GAC/Sys32)
	File "wx.NET.dll"
	File "wx-c.dll"
	SetOutPath "$INSTDIR\plugins"
	File "Absinthe.Plugins.dll"
SectionEnd

Section "Liquor Cabinet Shared Files"
	SetOutPath $INSTDIR
	File "LiquorCabinet.Shared.dll"
SectionEnd

Section "-Uninstaller"
	WriteUninstaller $INSTDIR\Uninstaller.exe
SectionEnd

Section "Uninstall"
	Delete $INSTDIR\Uninst.exe ;delete self
	Delete $INSTDIR\Absinthe.exe 
	Delete $INSTDIR\Absinthe.exe.manifest
	Delete $INSTDIR\README
	Delete $SMPROGRAMS\0x90.org\Absinthe\Absinthe.lnk
	RMDir /r $INSTDIR
	RMDir $PROGRAMFILES\0x90.org
	RMDir $SMPROGRAMS\0x90.org
SectionEnd

Function .onInit
	System::Call 'kernel32::CreateMutexA(i 0, i 0, t "myMutex") i .r1 ?e'
	Pop $R0
 
	StrCmp $R0 0 +3
	   MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
	   Abort
	
	MessageBox MB_YESNO "This will install 0x90.org's Absinthe. Do you wish to continue?" IDYES gogogo
		Abort
	gogogo:
	   Call IsDotNETInstalled
           Pop $0
           StrCmp $0 1 keepgoing nodotnet
	nodotnet:
	   MessageBox MB_OK "Absinthe requires the .NET framework to be installed. Please install that first"
	   abort
	keepgoing:
FunctionEnd

 ; IsDotNETInstalled
 ;
 ; Usage:
 ;   Call IsDotNETInstalled
 ;   Pop $0
 ;   StrCmp $0 1 found.NETFramework no.NETFramework

 Function IsDotNETInstalled
   Push $0
   Push $1
   Push $2
   Push $3
   Push $4

   ReadRegStr $4 HKEY_LOCAL_MACHINE \
     "Software\Microsoft\.NETFramework" "InstallRoot"
   # remove trailing back slash
   Push $4
   Exch $EXEDIR
   Exch $EXEDIR
   Pop $4
   # if the root directory doesn't exist .NET is not installed
   IfFileExists $4 0 noDotNET

   StrCpy $0 0

   EnumStart:

     EnumRegKey $2 HKEY_LOCAL_MACHINE \
       "Software\Microsoft\.NETFramework\Policy"  $0
     IntOp $0 $0 + 1
     StrCmp $2 "" noDotNET

     StrCpy $1 0

     EnumPolicy:

       EnumRegValue $3 HKEY_LOCAL_MACHINE \
         "Software\Microsoft\.NETFramework\Policy\$2" $1
       IntOp $1 $1 + 1
        StrCmp $3 "" EnumStart
         IfFileExists "$4\$2.$3" foundDotNET EnumPolicy

   noDotNET:
     StrCpy $0 0
     Goto done

   foundDotNET:
     StrCpy $0 1

   done:
     Pop $4
     Pop $3
     Pop $2
     Pop $1
     Exch $0
 FunctionEnd

