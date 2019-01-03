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
using System.Diagnostics;
using System.IO;
using System.Security;
using HCE.BalsamV;
using SPV3.Resume;

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
        ///     Loader constructor.
        /// </summary>
        /// <param name="configuration">
        ///     <see cref="_configuration" />
        /// </param>
        public Loader(LoaderConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Public wrapper for the executable invocation and campaign progress update. 
        /// </summary>
        /// <param name="executable">
        ///     Executable-type instance representing the HCE executable to load.
        /// </param>
        /// <param name="parameters">
        ///     Optional parameters used for initialising the HCE executable process.
        /// </param>
        public void Start(Executable executable, Parameters parameters = null)
        {
            HandleCheckpoint();
            InvokeExecutable(executable, parameters);
        }

        /// <summary>
        ///     Updates the initc.txt in the current directory with the detected profile's campaign progress.
        /// </summary>
        private void HandleCheckpoint()
        {
            if (_configuration.SkipResume)
                return;

            /**
             * Loader is assumed to be running from the working directory, where both the executable & initc reside in.
             */
            var initc = new Initc((SPV3.Domain.File) "initc.txt");

            /**
             * Savegame binary ends up being the one detected on the filesystem.
             * Detection is implicitly done by attempting to figure out the profile's name.
             */
            var saveName = LastprofFactory.DetectOnSystem().Name;
            var personal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var gamesHce = Path.Combine("My Games", "Halo CE");
            var savePath = Path.Combine(personal, gamesHce, "savegames", saveName, "savegame.bin");
            var saveGame = new Savegame((SPV3.Domain.File) savePath);

            initc.Save(saveGame.Load());
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
            if (!_configuration.SkipVerification)
                if (!executable.Verify())
                    throw new SecurityException("Executable failed to pass the verification routine.");

            var exeName = Path.GetFileName(executable.Path) ??
                          throw new FormatException("Could not infer executable name from the path.");

            var workDir = Path.GetDirectoryName(executable.Path) ??
                          throw new FormatException("Could not infer working directory from the path.");

            var exeArgs = parameters == null
                ? string.Empty
                : new ParametersSerialiser().Serialise(parameters);

            new Process
            {
                StartInfo =
                {
                    FileName = exeName,
                    WorkingDirectory = workDir,
                    Arguments = exeArgs
                }
            }.Start();
        }
    }
}