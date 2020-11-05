<h1 align="center">SPV3.Launcher</h1>
<p align="center">
	<img src="https://user-images.githubusercontent.com/10241434/49625813-02e94f00-fa12-11e8-9c30-cfc578a9a23e.png"/>
 <br><br>
 Source code of the official SPV3.2 Launcher
 <br><br>
 <a href="https://github.com/yumiris/SPV3.Settings">Settings</a>
 •
 <a href="https://github.com/yumiris/SPV3.Loader">Loader</a>
 •
 <a href="https://github.com/yumiris/SPV3.Installer">Installer</a>
</p>

# Introduction

This repository contains the source code for the SPV3.2 Launcher and its components. Currently, the codebase is
undergoing a significant rewrite to ensure that robustness, maintainability & modularity is significantly improved.

The commits for the previous SPV3 loader revisions are present within this repository's history, for preservation
purposes.

# Components

![hierarchy](https://user-images.githubusercontent.com/10241434/49678570-236fe280-fac0-11e8-923d-5d8f383d453c.png)

This section outlines the major components used by the SPV3 Launcher. The components have their own dedicated
repositories; however, adapted copies of them are present in the `lib` directory.

- [**SPV3.Loader**](https://github.com/yumiris/SPV3.Loader): Wrapper around the HCE executable for extending the loading
  procedure.
- [**SPV3.Settings**](https://github.com/yumiris/SPV3.Settings): Configuration program for SPV3.
- [**SPV3.Shaders**](https://github.com/yumiris/SPV3.Shaders): Shader specification & configuration program for SPV3.
- [**SPV3.Installer**](https://github.com/yumiris/SPV3.Installer): Installation source code & specification for SPV3.

# Attributions

* [**Mortis**](https://discord.gg/vu2eYwy) for consultancy & FOV calculation formula;
* [**Joshua Ezzell**](https://joshezzell.artstation.com/) for the UI's stunning background images;
* [**SbdJazz**](https://github.com/SubhadeepJasu) for the concept UIs & shader implementations.