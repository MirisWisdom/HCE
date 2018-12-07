<h1 align="center">Nouveau</h1>
<p align="center">
	<img src="https://user-images.githubusercontent.com/10241434/35606096-3170216a-0688-11e8-9372-6f3bf7d3c685.png"/>
 <br><br>
 The HCE/SPV3 Official Loader
 <br><br>
 <a href="https://github.com/yumiris/Ecran">Ecran Resolution</a>
 •
 <a href="https://github.com/yumiris/Echoic">Echoic Library</a>
 •
 <a href="https://github.com/yumiris/Eaxir">Eaxir Installer</a>
</p>

# Repository

Note that this repository serves as a mirror of the currently public code. The upstream code is hosted privately and will be available in this repository once it's ready for release. The commit frequency and the current code you may see here is not a representative of the actual state of development & code, respectively.

# Introduction
![Screenshot](https://user-images.githubusercontent.com/10241434/35571733-ed4e94a0-060d-11e8-8b65-ec0866fdf9c9.png)

This is the official repository for the HCE/SPV3 Nova Loader - now cleaned up, stable, and open source!

It all started as a small fix for SPV3's booting problems. The fix was two lines of code:

```cs
File.WriteAllBytes(Resources.Eula); // write eula assets
Process.Start(Resources.HaloCeExe); // start hce process
```

# Features
> Note: Due to the recent rewriting of the code-base, there has been a regression in features, such as auto-updating and Chimera installation/configuration. Once the rewriting is done, the features will be re-introduced.

* Installation of the EAX libraries for sound enhancement
  * Extraction of the EAX library files to the HCE directory
  * Enabling Hardware Acceleration and Environmental Sound
* Manipulation of the OpenSauce configurations, including:
  * Toggles for OpenSauce camera, HUD, shader and post processing options
  * OpenSauce FoV calculations using a mind-boggling formula by Mortis
  * HCE launching parameters & video settings (natively and with `-vidmode`)
* HCE gamepad input-action mapping by manipulating and forging the `blam.sav`
* Generation of an `initc.txt` configuration on launch
* Encoding/decoding of the `savegame.bin` for campaign continue mechanism
  * While at it, additional support on top for SPV3-specific maps
* Pretty sections for updates, announcements and common error messages

# Contibutors

* [Mortis](https://discord.gg/vu2eYwy) - Consultancy & FOV Calculation formula
* [Joshua Ezzell](https://joshezzell.artstation.com/) - UI's stunning background images

# Contributing

Bug reports and suggestions are always appreciated! Contributing with code is not available to the public, at the moment.

# License
... currently a work in progress...
