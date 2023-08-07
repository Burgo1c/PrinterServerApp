
﻿# PRINT SERVER APPLICATION
An automated print system.

## Author
株式会社ロジ・グレス

## Configuration
[App.config] file to store configuration settings. 
(In production use [PrinterServerApp.dll.config] to edit settings).
>WARNING : DO NOT CHANGE KEY NAMES.

## USAGE
1. Run the application by executing the built executable file [PrinterServerApp.exe].
2. The Application will retreive the configuration settings from the [App.config] file.
	* If no settings are found the Application will not start.
3. The Application will retrieve data from the API'S specified in the [App.config] file.
	* The application will make a call to the API if the [*_PDF_PRINT] flag is true.
	* If an error occurs during the HTTP transaction the Application will retry the HTTP transaction, up to the retry amount specified in the [App.config] file.
	* If an error persists the Application will display the error and stop all procedures.
4. The Application will create a PDF from the the byte data received from the API.
5. The PDF will be printed out by the specified printer.
6. A history of the print list will be displayed.

## Error
On an error the Application will output the error details to the error file specified in the [App.config] file.||||||| .r0

﻿# PRINT SERVER APPLICATION
An automated print system.

## Author
株式会社ロジ・グレス

## Configuration
[App.config] file to store configuration settings. 
(In production use [PrinterServerApp.dll.config] to edit settings).
>WARNING : DO NOT CHANGE KEY NAMES.

## USAGE
1. Run the application by executing the built executable file [PrinterServerApp.exe].
2. The Application will retreive the configuration settings from the [App.config] file.
	* If no settings are found the Application will not start.
3. The Application will retreive data from the API'S specified in the [App.config] file.
	* The application will make a call to the API if the [*_PDF_PRINT] flag is true.
	* If an error occurs during the HTTP transaction the Application will retry the HTTP transaction, up to the retry amount specified in the [App.config] file.
	* If an error persists the Application will display the error and stop all procedures.
4. The Application will create a PDF from the the byte data received from the API.
5. The PDF will be printed out by the specified printer.
6. A history of the print list will be displayed.

## Error
On an error the Application will output the error details to the error file specified in the [App.config] file.
