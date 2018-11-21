<html>
    <p align="center">
        <img src="https://user-images.githubusercontent.com/10241434/48844282-52a20680-edd4-11e8-8dd4-80036c26ca6e.png">
    <p>
    <h1 align="center">
        HCE.BalsamV
    </h1>
    <h3 align="center">
        HCE Profile Binary Editor
    </h3>
    <p align="center">
        <img src="https://user-images.githubusercontent.com/10241434/48844226-2be3d000-edd4-11e8-85c8-5edde1a98b3b.png">
    <p>
    <p align="center">
        <a href="https://github.com/yumiris/HCE.BalsamV/releases/latest">
            Download
        </a>
    </p>
</html>

# Introduction

BalsamV is an editor for the binary file used by HCE to store the profile configuration. It reads the saved values and
allows the editing of them. The main advantage is more freedom over setting values, including name hacks and custom
resolutions without refresh-rate locks.

This project is currently a work progress. At the moment, all values can be edited **except**:

- KBM/Controller Input mappings
- Video gamma value
- Video refresh rate
- Sound EAX/HW Acceleration

# Usage

1. Download & run the executable.
2. If a HCE profile exists, it will automatically be loaded. If not, please click on `Load` and select your `blam.sav`^.
3. Edit the values accordingly, then click `Save`.

^ - The `blam.sav` binary can be found in `<personal hce data>\savegames\<profile name>\blam.sav`