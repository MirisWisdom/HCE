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
using System.Security;
using System.Threading.Tasks;

namespace SPV3.Loader.CLI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = new LoaderConfiguration();
            var loader = new Loader(config, new StatusOutput());

            try
            {
                Task.Run(() =>
                {
                    /**
                     * Implicitly detect the HCE executable and start it without any parameters.
                     */
                    if (args.Length == 0)
                    {
                        loader.Start(ExecutableFactory.Detect());
                        return;
                    }

                    /**
                     * The parameters are expected to be the HCE ones, e.g. `-window`, `-safemode`, etc.
                     * This effectively makes the SPV3 Loader a wrapper around the HCE executable. 
                     */
                    var parameters = new ParametersParser().Parse(string.Join(" ", args));

                    /**
                     * This allows explicit declaration of the path which the HCE executable resides in.
                     * If the path isn't declared, then we implicitly attempt to detect the executable.
                     */
                    var executable = args[0].Contains(Executable.Name)
                        ? new Executable(args[0])
                        : ExecutableFactory.Detect();

                    loader.Start(executable, parameters);
                }).GetAwaiter().GetResult();
            }
            catch (SecurityException e)
            {
                Console.WriteLine(e.ToString());
                Environment.Exit(1);
            }
        }
    }
}