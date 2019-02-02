/**
 * Copyright (C) 2019 Emilian Roman
 * 
 * This file is part of SPV3.Launcher.
 * 
 * SPV3.Launcher is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * SPV3.Launcher is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with SPV3.Launcher.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SPV3.Launcher.GUI.Annotations;
using SPV3.Loader;

namespace SPV3.Launcher.GUI
{
    /// <summary>
    ///     Main model used by the SPV3.2 Launcher.
    /// </summary>
    public class Main : INotifyPropertyChanged
    {
        /// <summary>
        ///     <see cref="ErrorStringData"/>
        /// </summary>
        private string _errorStringData;

        /// <summary>
        ///     <see cref="ErrorVisibility"/>
        /// </summary>
        private Visibility _errorVisibility = Visibility.Collapsed;

        /// <summary>
        ///     Exception message.
        /// </summary>
        public string ErrorStringData
        {
            get => _errorStringData;
            set
            {
                if (value == _errorStringData) return;
                _errorStringData = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Visibility of the error UI control.
        /// </summary>
        public Visibility ErrorVisibility
        {
            get => _errorVisibility;
            set
            {
                if (value == _errorVisibility) return;
                _errorVisibility = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Invokes the SPV3.Loader's HCE executable loading routine.
        /// </summary>
        public void Load()
        {
            try
            {
                new SPV3.Loader.Loader(new LoaderConfiguration()).Start(ExecutableFactory.Detect());
            }
            catch (Exception e)
            {
                ErrorStringData = $"{e.Source} - {e.Message}";
                ErrorVisibility = Visibility.Visible;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}