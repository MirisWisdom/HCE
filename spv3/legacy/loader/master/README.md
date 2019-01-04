<html>
    <h1 align="center">
        SPV3.Loader
    </h1>
    <p align="center">
        Loading of the SPV3 mod & resuming of the campaign 
    </p>
</html>

# Introduction

This repository contains the source & documentation for SPV3.Loader.

The loader is a wrapper for the HCE executable. It extends the loading procedure
whilst accepting the same start-up parameters as the HCE executable.

The additional functionality, at the moment, includes:

- detection of the HCE executable's path;
- verification of the HCE executable's validity;
- checkpoint resuming of the SPV3 campaign;
- maps data verification on the filesystem.

## CLI Usage

```powershell
# Implicit detection of HCE executable
.\SPV3.Loader.CLI.exe -window -screenshot

# Explicit HCE path & parameters declarations
.\SPV3.Loader.CLI.exe C:\haloce.exe -nosound -console
```

## Documentation

- [Loading Procedure](doc/loading.md)
- [Startup Parameters](doc/parameters.md)
- [Executable Detection](doc/detection.md)