using System.Windows;

namespace SAMS.Connect.Core.Services;

public static class DialogService
{
#region Dialogs

    public static async Task ShowSuccessAsync(
        string message,
        string heading = "Success"
    ) {
        await Task.Run(() => {
            
            /* Attribution: Dispatcher
             * - https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcher.invoke
             */
            Application.Current.Dispatcher.Invoke(() => {
                
                /* Attribution: MessageBox
                 * - https://learn.microsoft.com/en-us/dotnet/api/system.windows.messagebox
                 */
                MessageBox.Show(
                    message,
                    heading,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            });
        });
    }


    public static async Task ShowFailureAsync(
        string message,
        string heading = "Oops!"
    ) {
        await Task.Run(() => {
            Application.Current.Dispatcher.Invoke(() => {
                MessageBox.Show(
                    message,
                    heading,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            });
        });
    }


    public static async Task<bool> ShowConfirmationAsync(
        string message,
        string heading = "Are you sure?"
    ) {
        return await Task.Run(() => {
            return Application.Current.Dispatcher.Invoke(() => {
                var result = MessageBox.Show(
                    message,
                    heading,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                return (
                    result is MessageBoxResult.Yes
                );
            });
        });
    }

#endregion
}
