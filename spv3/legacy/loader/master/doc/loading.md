# Loading Procedure

The loader conducts three routines upon invocation, all of which are outlined
here.

## Progress Resolving

The first one is determining the player's progress in the campaign.

This is done by:

1. Detecting the profile on the filesystem using `HCE.BalsamV`;
2. Deserialising the mission & difficulty off the profile's `savegame.bin`;
3. Serialising the data to the `initc.txt` inside the working directory.

In circumstances where a profile does not exist or the campaign hasn't been
started, this process is halted and the `initc.txt` remains untouched.

## Executable Verification

This step verifies the HCE executable to avoid the execution of arbitrary files
that share the same name as the original executable.

If the executable doesn't pass the verification, then an exception will be
be thrown. If the CLI is used, then the program will quit.

## Executable Initiation

If the verification passes, then the provided/[detected](detection.md)
executable will start up. Any arguments passed to the library or the CLI will be
(serialised and) passed to the executable.