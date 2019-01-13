# Loading Procedure

The loader conducts four routines upon invocation, all of which are outlined
here.

## Maps Verification

This step compares the lengths (sizes) of the maps on the filesystem against
the maps in the maps directory. It also implicitly checks for their existence.

The maps are checked are the ones that are defined in the manifest binary. If
a map does not exist or its size mismatches the one defined in the manifest,
then a `FileNotFound` or `Security` exception will be thrown, respectively.

## Progress Resolving

This step determines the player's progress in the campaign, to allow the
resuming of it. HCE does not handle this natively, hence this is needed.

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

If the verifications pass, then the provided/[detected](detection.md)
executable will start up. Any arguments passed to the library or the CLI will be
(serialised and) passed to the executable.

If the maps or executable verification fails, then the execution of SPV3 will be
aborted to prevent further errors.