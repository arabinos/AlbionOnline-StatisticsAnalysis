﻿using StatisticsAnalysisTool.Common;
using StatisticsAnalysisTool.Models;
using StatisticsAnalysisTool.Properties;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StatisticsAnalysisTool.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;

        private GameInfoSearchResponse _gameInfoSearchResponse;
        private SearchPlayerResponse _searchPlayer;
        private GameInfoPlayersResponse _gameInfoPlayer;
        
        public MainWindowViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            UpgradeSettings();
            InitLanguage();
        }

        private void UpgradeSettings()
        {
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }
        }

        private void InitLanguage()
        {
            if (LanguageController.SetFirstLanguageIfPossible())
                return;

            MessageBox.Show("ERROR: No language file found!");
            _mainWindow.Close();
        }

        public GameInfoSearchResponse GameInfoSearch {
            get => _gameInfoSearchResponse;
            set {
                _gameInfoSearchResponse = value;
                OnPropertyChanged();
            }
        }

        public SearchPlayerResponse SearchPlayer {
            get => _searchPlayer;
            set {
                _searchPlayer = value;
                OnPropertyChanged();
            }
        }

        public GameInfoPlayersResponse GameInfoPlayers
        {
            get => _gameInfoPlayer;
            set {
                _gameInfoPlayer = value;
                OnPropertyChanged();
            }
        }

        public PlayerModeTranslation PlayerModeTranslation => new PlayerModeTranslation();
        public string DonateUrl => Settings.Default.DonateUrl;
        public string SavedPlayerInformationName => Settings.Default.SavedPlayerInformationName ?? "";
        public string SaveTranslation => LanguageController.Translation("SAVE");

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}