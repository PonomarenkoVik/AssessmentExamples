using EngineUIComponents.Services;
using System;
using System.Windows;
using System.Windows.Threading;

namespace EngineStartup
{
    internal class ExceptionHandler
    {
        private const string m_ApplicationName = "Engine database app";
        private bool m_UnhandledExceptionProcessed;
        private readonly object m_SyncLock = new object();
        private readonly IMessageDialogService _messageDialogService;
        private static ExceptionHandler ms_Instance;

        private ExceptionHandler(IMessageDialogService messageDialogService)
        {
            this._messageDialogService = messageDialogService;
        }

        public static void Register(IMessageDialogService messageDialogService)
        {
            ms_Instance = new ExceptionHandler(messageDialogService);
            AppDomain.CurrentDomain.UnhandledException += ms_Instance.ProcessUnhandledException;
            Application.Current.DispatcherUnhandledException += ms_Instance.ProcessUnhandledException;
        }

        private void ProcessUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            lock (m_SyncLock)
            {
                if (m_UnhandledExceptionProcessed) return;
                m_UnhandledExceptionProcessed = true;
            }

            ProcessUnhandledException((Exception)e.ExceptionObject);
            TryShutdownApplication();
        }

        private void ProcessUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            lock (m_SyncLock)
            {
                if (m_UnhandledExceptionProcessed)
                {
                    e.Handled = true;
                    return;
                }
                m_UnhandledExceptionProcessed = true;
            }

            ProcessUnhandledException(e.Exception);

            e.Handled = true;
            TryShutdownApplication();
        }

        private static void TryShutdownApplication()
        {
            try
            {
                if (Application.Current != null && Application.Current.CheckAccess())
                {
                    Application.Current.Shutdown(-1);
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
            catch (Exception e)
            {
               //Logging
            }
        }

        private void ProcessUnhandledException(Exception exception)
        {
            ShowUnhandledExceptionDialog(exception);
        }

        private void ShowUnhandledExceptionDialog(Exception exception)
        {
            var title = string.Format("Error - {0}", m_ApplicationName);
            if (Application.Current?.MainWindow != null)
            {
                _messageDialogService.ShowErrorDialog(exception);
            }
            else
            {
                const string message = "An unexpected error occurred in the application. Check the System Event Log for more detailed information.";
                MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
