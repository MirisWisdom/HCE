/**
 * Copyright (C) 2019 Emilian Roman
 * 
 * This file is part of SPV3.Loader.
 * 
 * SPV3.Loader is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * SPV3.Loader is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with SPV3.Loader.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using HCE.BalsamV;
using SPV3.Domain;
using SPV3.Resume;
using File = SPV3.Domain.File;

namespace SPV3.Loader
{
    /// <summary>
    ///     Starts up the inbound executable with the inbound loader configuration.
    /// </summary>
    public class Loader
    {
        /// <summary>
        ///     Configuration used for the executable loading routine.
        /// </summary>
        private readonly LoaderConfiguration _configuration;

        /// <summary>
        ///     Optional IStatus object for outputting loading data to.
        /// </summary>
        private readonly IStatus _status;

        /// <summary>
        ///     Loader constructor.
        /// </summary>
        /// <param name="configuration">
        ///     Configuration used for the executable loading routine.
        /// </param>
        /// <param name="status">
        ///     Optional IStatus object for outputting loading data to.
        /// </param>
        public Loader(LoaderConfiguration configuration, IStatus status = null)
        {
            _configuration = configuration;
            _status = status;
        }

        /// <summary>
        ///     Public wrapper for the executable invocation, data verification, and and campaign progress update.
        /// </summary>
        /// <param name="executable">
        ///     Executable-type instance representing the HCE executable to load.
        /// </param>
        /// <param name="parameters">
        ///     Optional parameters used for initialising the HCE executable process.
        /// </param>
        public void Start(Executable executable, Parameters parameters = null)
        {
            Notify("============================");
            Notify("Initiated loading routine...");
            Notify("============================");

            VerifyMapLengths();
            HandleCheckpoint();
            InvokeExecutable(executable, parameters);

            Notify("============================");
            Notify("Completed loading routine...");
            Notify("============================");
        }

        /// <summary>
        ///     Verifies the map lengths by comparing them to the lengths defined in the manifest.
        /// </summary>
        /// <exception cref="NullReferenceException">
        ///     Maps package not found in the manifest.
        /// </exception>
        /// <exception cref="SecurityException">
        ///     Map length mismatches the expected one.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     Map does not exist in the maps folder.
        /// </exception>
        private void VerifyMapLengths()
        {
            Notify("----------------------------");
            Notify("Initiated maps data check...");
            Notify("----------------------------");

            IEnumerable<Entry> GetMaps()
            {
                var manifest = new ManifestRepository((File) Manifest.Name.Value).Load();
                var packages = manifest.Packages;

                foreach (var package in packages)
                {
                    if (package.Directory == null) continue;
                    if (package.Directory.Name == "maps")
                        return package.Entries;
                }

                throw new NullReferenceException("No maps package found in the manifest.");
            }

            foreach (var map in GetMaps())
            {
                var mapFile = Path.Combine("maps", map.Name);
                var mapName = (string) map.Name;
                var mapSize = (int) map.Size;

                /**
                 * Makes the output more symmetrical, to satisfy the Yumi.
                 */
                var padding = new string(' ', 32 - mapName.Length);

                /**
                 * Checks the existence & sizes for the length.
                 */
                if (System.IO.File.Exists(mapFile))
                {
                    Notify($"Checking {mapName} {padding} <= " + mapSize);

                    if (new FileInfo(mapFile).Length == map.Size)
                        Notify($"Verified {mapName} {padding} <= " + mapSize);
                    else
                        throw new SecurityException($"Map '{mapName}' length mismatches the expected one.");
                }
                else
                {
                    throw new FileNotFoundException($"Map '{mapName}' does not exist in the maps folder.");
                }
            }

            Notify("----------------------------");
            Notify("Completed maps data check...");
            Notify("----------------------------");
        }

        /// <summary>
        ///     Updates the initc.txt in the current directory with the detected profile's campaign progress.
        /// </summary>
        private void HandleCheckpoint()
        {
            if (_configuration.SkipResume)
                return;

            Notify("----------------------------");
            Notify("Initiated progress handle...");
            Notify("----------------------------");

            /**
             * Loader is assumed to be running from the working directory, where both the executable & initc reside in.
             */
            Notify("Infer init in working dir...");
            var initc = new Initc((File) "initc.txt");

            /**
             * Savegame binary ends up being the one detected on the filesystem.
             * Detection is implicitly done by attempting to figure out the profile's name.
             */
            Notify("Infer savegame in profile...");
            string saveName;

            try
            {
                saveName = LastprofFactory.DetectOnSystem().Name;
            }
            catch (FileNotFoundException)
            {
                Notify("Could not resolve profile...");
                Notify("Assuming fresh initiation...");
                return;
            }

            var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var gamesHce = Path.Combine("My Games", "Halo CE");
            var savePath = Path.Combine(personal, gamesHce, "savegames", saveName, "savegame.bin");
            var saveGame = new Savegame((File) savePath);

            Progress progress;

            try
            {
                Notify("Infer saved progress data...");
                progress = saveGame.Load();
            }
            catch (ArgumentException)
            {
                Notify("Could not decode the data...");
                Notify("Assuming fresh checkpoint...");
                Notify("Will skip checkpoint commit!");
                return;
            }

            Notify("Commit checkpoint to init...");
            initc.Save(progress);

            Notify("----------------------------");
            Notify("Completed progress handle...");
            Notify("----------------------------");
        }

        /// <summary>
        ///     Invokes the inbound executable as a process, with the inbound parameters. Pre-invocation verification
        ///     will be conducted, unless set to skip in the configuration.
        /// </summary>
        /// <param name="executable">
        ///     HCE executable to verify and invoke.
        /// </param>
        /// <param name="parameters">
        ///     Parameters for the start-up process.
        /// </param>
        /// <exception cref="SecurityException">
        ///     Executable failed to pass the verification routine.
        /// </exception>
        /// <exception cref="FormatException">
        ///     Could not infer executable name from the path.
        ///     - or -
        ///     Could not infer working directory from the path.
        /// </exception>
        private void InvokeExecutable(Executable executable, Parameters parameters)
        {
            Notify("----------------------------");
            Notify("Initiated game invocation...");
            Notify("----------------------------");

            if (!_configuration.SkipVerification)
            {
                Notify("Verifying Halo executable...");

                if (!executable.Verify())
                    throw new SecurityException("Executable failed to pass the verification routine.");

                Notify("Executable verifying passed!");
            }


            var exeName = Path.GetFileName(executable.Path) ??
                          throw new FormatException("Could not infer executable name from the path.");

            var workDir = Path.GetDirectoryName(executable.Path) ??
                          throw new FormatException("Could not infer working directory from the path.");

            Notify("Parsing inbound arguments...");
            var exeArgs = parameters == null
                ? string.Empty
                : new ParametersSerialiser().Serialise(parameters);

            Notify("Attempting to execute HCE...");
            new Process
            {
                StartInfo =
                {
                    FileName = exeName,
                    WorkingDirectory = workDir,
                    Arguments = exeArgs
                }
            }.Start();

            Notify("----------------------------");
            Notify("Completed game invocation...");
            Notify("----------------------------");
        }

        /// <summary>
        ///     Invokes IStatus.CommitStatus() with inbound data.
        /// </summary>
        /// <param name="data">
        ///     Data to output to the IStatus object.
        /// </param>
        private void Notify(string data)
        {
            _status?.CommitStatus(data);
        }
    }
}