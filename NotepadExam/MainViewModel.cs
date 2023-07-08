using PropertyChanged;
using System.Windows;
using System.Windows.Input;

namespace NotepadExam
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        private RelayCommand? createFileCommand;

        public ICommand CreateFileCommand => createFileCommand ??=
            new RelayCommand(o => CreateFile());

        private RelayCommand? openFileCommand;

        public ICommand OpenFileCommand => openFileCommand ??=
            new RelayCommand(o => OpenFile());

        private RelayCommand? saveFileCommand;

        public ICommand SaveFileCommand => saveFileCommand ??=
            new RelayCommand(o => SaveFile());

        private RelayCommand? saveFileAsCommand;

        public ICommand SaveFileAsCommand => saveFileAsCommand ??=
            new RelayCommand(o => SaveFileAs());

        private RelayCommand? closeFileCommand;

        public ICommand CloseFileCommand => closeFileCommand ??=
            new RelayCommand(o => CloseFile());

        private RelayCommand? copySelectedTextCommand;

        public ICommand CopySelectedTextCommand => copySelectedTextCommand ??=
            new RelayCommand(o => CopySelectedText());

        private RelayCommand? cutSelectedTextCommand;

        public ICommand CutSelectedTextCommand => cutSelectedTextCommand ??=
            new RelayCommand(o => CutSelectedText());

        private RelayCommand? pasteTextCommand;

        public ICommand PasteTextCommand => pasteTextCommand ??=
            new RelayCommand(o => PasteText());

        private RelayCommand? clearAllTextCommand;

        public ICommand ClearAllTextCommand => clearAllTextCommand ??=
            new RelayCommand(o => ClearAllText());

        private RelayCommand? selectAllTextCommand;

        public ICommand SelectAllTextCommand => selectAllTextCommand ??=
            new RelayCommand(o => SelectAllText());

        private RelayCommand? undoCommand;

        public ICommand UndoCommand => undoCommand ??=
            new RelayCommand(o => Undo());

        private RelayCommand? redoCommand;

        public ICommand RedoCommand => redoCommand ??=
            new RelayCommand(o => Redo());

        private RelayCommand? editFontCommand;

        public ICommand EditFontCommand => editFontCommand ??=
            new RelayCommand(o => EditFont());

        private RelayCommand? editTextColorCommand;

        public ICommand EditTextColorCommand => editTextColorCommand ??=
            new RelayCommand(o => EditTextColor());

        private RelayCommand? editBackColorCommand;

        public ICommand EditBackColorCommand => editBackColorCommand ??=
            new RelayCommand(o => EditBackColor());

        private void CreateFile()
        {

        }

        private void OpenFile()
        {

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