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

﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using SPV3.Loader;

namespace SPV3.Launcher.GUI.Loader
{
    public class LoaderViewModel : INotifyPropertyChanged
    {
        private string _launchErrorMessage = string.Empty;
        private Visibility _launchErrorVisibility = Visibility.Collapsed;

        public string LaunchErrorMessage
        {
            get => _launchErrorMessage;
            set
            {
                _launchErrorMessage = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility LaunchErrorVisibility
        {
            get => _launchErrorVisibility;
            set
            {
                _launchErrorVisibility = value;
                NotifyPropertyChanged();
            }
        }

        public BitmapImage Background
        {
            get
            {
                var randomNumber = new Random().Next(1, 47);
                var backgroundPath = new Uri($"pack://application:,,,/Assets/Backgrounds/{randomNumber}.jpg");

                return new BitmapImage(backgroundPath)
                {
                    CacheOption = BitmapCacheOption.OnLoad
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LaunchHalo()
        {
            new SPV3.Loader.Loader(new LoaderConfiguration()).Start(ExecutableFactory.Detect());
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}