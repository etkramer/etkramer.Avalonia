﻿using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.VisualTree;
using MiniMvvm;

namespace ControlCatalog.ViewModels
{
    public class MenuPageViewModel
    {
        public Control? View { get; set; }
        public MenuPageViewModel()
        {
            OpenCommand = MiniCommand.CreateFromTask(Open);
            SaveCommand = MiniCommand.Create(Save);
            OpenRecentCommand = MiniCommand.Create<string>(OpenRecent);

            var recentItems = new[]
            {
                new MenuItemViewModel
                {
                    Header = "File1.txt",
                    Command = OpenRecentCommand,
                    CommandParameter = @"c:\foo\File1.txt"
                },
                new MenuItemViewModel
                {
                    Header = "File2.txt",
                    Command = OpenRecentCommand,
                    CommandParameter = @"c:\foo\File2.txt"
                },
            };

            RecentItems = recentItems;
            MenuItems = new[]
            {
                new MenuItemViewModel
                {
                    Header = "_File",
                    Items = new[]
                    {
                        new MenuItemViewModel { Header = "O_pen...", Command = OpenCommand },
                        new MenuItemViewModel { Header = "Save", Command = SaveCommand },
                        new MenuItemViewModel { Header = "-" },
                        new MenuItemViewModel
                        {
                            Header = "Recent",
                            Items = recentItems
                        },
                    }
                },
                new MenuItemViewModel
                {
                    Header = "_Edit",
                    Items = new[]
                    {
                        new MenuItemViewModel { Header = "_Copy" },
                        new MenuItemViewModel { Header = "_Paste" },
                    }
                }
            };
        }

        public IReadOnlyList<MenuItemViewModel> MenuItems { get; set; }
        public IReadOnlyList<MenuItemViewModel> RecentItems { get; set; }
        public MiniCommand OpenCommand { get; }
        public MiniCommand SaveCommand { get; }
        public MiniCommand OpenRecentCommand { get; }

        public async Task Open()
        {
            var window = View?.GetVisualRoot() as Window;
            if (window == null)
                return;
            var result = await window.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions() { AllowMultiple = true });

            if (result != null)
            {
                foreach (var file in result)
                {
                    System.Diagnostics.Debug.WriteLine($"Opened: {file.Name}");
                }
            }
        }

        public void Save()
        {
            System.Diagnostics.Debug.WriteLine("Save");
        }

        public void OpenRecent(string path)
        {
            System.Diagnostics.Debug.WriteLine($"Open recent: {path}");
        }
    }
}
