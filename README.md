# WSA-APKInstaller
An app for installing APKs to WSA over Android Debug Bridge.

## Setup

1) Download ADB to your computer. Download link: https://dl.google.com/android/repository/platform-tools-latest-windows.zip
2) After downloading, extract the files and move to a folder. The files will stay here forever.
3) Open your paths list and add your folder to it with adding `platform-tools` to end of string. Eg. `C:\adb\platform-tools`
4) Open WSA settings and enable Developer Mode.
5) After enabling it wait a few seconds and it give you an IP and a port.
6) Open your terminal and write `adb connect <ip>:<port>`. Eg. `adb connect 127.0.0.1:58526`
7) Type `adb devices` to confirm that you connected device succesfully. If you get a message like this, you connected successfully:
```
List of devices attached
127.0.0.1:58526 device
```
## Usage
Just drag and drop the .apk file on to the APKInstaller.exe or double click on .apk file and select this app by browsing.
