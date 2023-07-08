using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;

namespace NotepadExam
{
    public class MainViewModel
    {
        private readonly ObservableCollection<TabItem> tabs = new();

        public ICollection<TabItem> Tabs => tabs;

        public int SelectedTabIndex { get; set; } = -1;

        private readonly List<FileInfo> tabFiles = new();

        private RelayCommand? createFileCommand;

        public ICommand CreateFileCommand => createFileCommand ??=
            new(o => CreateFile());

        private RelayCommand? openFileCommand;

        public ICommand OpenFileCommand => openFileCommand ??=
            new(o => OpenFile());

        private RelayCommand? saveFileCommand;

        public ICommand SaveFileCommand => saveFileCommand ??=
            new(o => SaveFile());

        private RelayCommand? saveFileAsCommand;

        public ICommand SaveFileAsCommand => saveFileAsCommand ??=
            new(o => SaveFileAs());

        private RelayCommand? closeFileCommand;

        public ICommand CloseFileCommand => closeFileCommand ??=
            new(o => CloseFile());

        private RelayCommand? copySelectedTextCommand;

        public ICommand CopySelectedTextCommand => copySelectedTextCommand ??=
            new(o => CopySelectedText());

        private RelayCommand? cutSelectedTextCommand;

        public ICommand CutSelectedTextCommand => cutSelectedTextCommand ??=
            new(o => CutSelectedText());

        private RelayCommand? pasteTextCommand;

        public ICommand PasteTextCommand => pasteTextCommand ??=
            new(o => PasteText());

        private RelayCommand? clearAllTextCommand;

        public ICommand ClearAllTextCommand => clearAllTextCommand ??=
            new(o => ClearAllText());

        private RelayCommand? selectAllTextCommand;

        public ICommand SelectAllTextCommand => selectAllTextCommand ??=
            new(o => SelectAllText());

        private RelayCommand? undoCommand;

        public ICommand UndoCommand => undoCommand ??=
            new(o => Undo());

        private RelayCommand? redoCommand;

        public ICommand RedoCommand => redoCommand ??=
            new(o => Redo());

        private RelayCommand? editFontCommand;

        public ICommand EditFontCommand => editFontCommand ??=
            new(o => EditFont());

        private RelayCommand? editTextColorCommand;

        public ICommand EditTextColorCommand => editTextColorCommand ??=
            new(o => EditTextColor());

        private RelayCommand? editBackColorCommand;

        public ICommand EditBackColorCommand => editBackColorCommand ??=
            new(o => EditBackColor());

        public MainViewModel()
        {
            CreateFile();
        }

        private void CreateFile()
        {
            int counter = 1;
            string filePath = Path.Combine("Files", $"New file.rtf");

            if (File.Exists(filePath))
            {
                while (File.Exists(Path.Combine("Files", $"New file {counter}.rtf")))
                    counter++;
                filePath = Path.Combine("Files", $"New file {counter}.rtf");
            }

            FileInfo file = new(filePath);
            file.Create();
            tabFiles.Add(file);

            Tabs.Add(new TabItem
            {
                Header = Path.GetFileName(filePath),
                Content = new RichTextBox { Margin = new(10) }
            });
        }

        private void OpenFile()
        {
            OpenFileDialog dialog = new();
            if (dialog.ShowDialog() ?? false)
            {
                tabFiles.Add(new FileInfo(dialog.FileName));

                Tabs.Add(new TabItem
                {
                    Header = Path.GetFileName(dialog.FileName),
                    Content = new RichTextBox { Margin = new(10) }
                });
            }
        }

        private void SaveFile()
        {

        }

        private void SaveFileAs()
        {

        }

        private void CloseFile()
        {

        }

        private void CopySelectedText()
        {

        }

        private void CutSelectedText()
        {

        }

        private void PasteText()
        {
            
        }

        private void ClearAllText()
        {

        }

        private void SelectAllText()
        {

        }

        private void Undo()
        {

        }

        private void Redo()
        {

        }

        private void EditFont()
        {

        }

        private void EditTextColor()
        {

        }

        private void EditBackColor()
        {

        }
    }
}