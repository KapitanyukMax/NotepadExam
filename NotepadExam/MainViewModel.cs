using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Forms;

using MessageBox = System.Windows.MessageBox;
using RichTextBox = System.Windows.Controls.RichTextBox;
using DataFormats = System.Windows.DataFormats;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using System.Windows.Media;
using System.Diagnostics;

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

        private RichTextBox? GetCurrentRTB()
        {
            if (SelectedTabIndex == -1)
                return null;

            return tabs[SelectedTabIndex].Content as RichTextBox ??
                new RichTextBox { Margin = new(10) };
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
                var rtb = new RichTextBox { Margin = new(10) };

                try
                {
                    using FileStream stream = new(dialog.FileName, FileMode.Open);
                    TextRange range = new(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                    range.Load(stream, DataFormats.Rtf);
                }
                catch (IOException)
                {
                    MessageBox.Show("File is used by another process!");
                    return;
                }

                Tabs.Add(new TabItem
                {
                    Header = Path.GetFileName(dialog.FileName),
                    Content = rtb
                });

                tabFiles.Add(new(dialog.FileName));
            }
        }

        private void SaveFile()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            using FileStream stream = tabFiles[SelectedTabIndex].OpenWrite();
            TextRange range = new(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            range.Save(stream, DataFormats.Rtf);
        }

        private void SaveFileAs()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            SaveFileDialog dialog = new();
            if (dialog.ShowDialog() ?? false)
            {
                using FileStream stream = new(dialog.FileName, FileMode.Create);
                TextRange range = new(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                range.Save(stream, DataFormats.Rtf);
            }
        }

        private void CloseFile()
        {
            if (SelectedTabIndex == -1)
                return;

            var res = MessageBox.Show("Do you want to save changes?",
                "Save changes", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
                SaveFile();

            tabFiles.RemoveAt(SelectedTabIndex);
            tabs.RemoveAt(SelectedTabIndex);
        }

        private void CopySelectedText()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.Copy();
        }

        private void CutSelectedText()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.Cut();
        }

        private void PasteText()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.Paste();
        }

        private void ClearAllText()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.SelectAll();
            rtb.Selection.Text = string.Empty;
        }

        private void SelectAllText()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.SelectAll();
        }

        private void Undo()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.Undo();
        }

        private void Redo()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            rtb.Redo();
        }

        private void EditFont()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            FontDialog fd = new FontDialog();
            var result = fd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Debug.WriteLine(fd.Font);

                rtb.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty,
                    new FontFamily(fd.Font.Name));
                rtb.Selection.ApplyPropertyValue(TextElement.FontSizeProperty,
                    fd.Font.Size * 96.0 / 72.0);
                rtb.Selection.ApplyPropertyValue(TextElement.FontWeightProperty,
                    fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular);
                rtb.Selection.ApplyPropertyValue(TextElement.FontStyleProperty,
                    fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal);
            }
        }

        private void EditTextColor()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                rtb.Selection.ApplyPropertyValue(TextElement.ForegroundProperty,
                    new SolidColorBrush(Color.FromArgb(
                        cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B)));
        }

        private void EditBackColor()
        {
            var rtb = GetCurrentRTB();
            if (rtb == null)
                return;

            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
                rtb.Selection.ApplyPropertyValue(TextElement.BackgroundProperty,
                    new SolidColorBrush(Color.FromArgb(
                        cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B)));
        }
    }
}