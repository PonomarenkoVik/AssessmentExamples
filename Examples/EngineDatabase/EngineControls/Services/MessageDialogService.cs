using System;
using System.Windows;

namespace EngineUIComponents.Services
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
        void ShowInfoDialog(string text);
        void ShowErrorDialog(Exception ex, string title = null);
        void ShowErrorDialog(Exception ex, string mess, string title = null);
    }

    public class MessageDialogService : IMessageDialogService
    {
        public MessageDialogResult ShowOkCancelDialog(string text, string title)
        {
            var result = MessageBox.Show(Application.Current.MainWindow, text, title, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK, MessageBoxOptions.None);
            return result == MessageBoxResult.OK
              ? MessageDialogResult.OK
              : MessageDialogResult.Cancel;
        }

        public void ShowInfoDialog(string text) => MessageBox.Show(Application.Current.MainWindow, text, "Info", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.None);

        public void ShowErrorDialog(Exception ex, string title = null) => MessageBox.Show(Application.Current.MainWindow, ex.Message, title ?? "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);

        public void ShowErrorDialog(Exception ex, string mess, string title = null) => MessageBox.Show(Application.Current.MainWindow, $"{mess} {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.None);  
    }

    public enum MessageDialogResult
    {
        OK,
        Cancel
    }
}
