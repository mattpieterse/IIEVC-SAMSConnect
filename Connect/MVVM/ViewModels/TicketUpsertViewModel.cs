using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JetBrains.Annotations;
using Microsoft.Win32;
using SAMS.Connect.Core.Data;
using SAMS.Connect.Core.Models;
using SAMS.Connect.Core.Services;
using SAMS.Connect.MVVM.Models;
using SAMS.Connect.MVVM.Views;
using NavigationService = SAMS.Connect.Core.Services.NavigationService;

namespace SAMS.Connect.MVVM.ViewModels;

[UsedImplicitly]
public sealed partial class TicketUpsertViewModel(
    FluentNavigationService fluentNavigationService,
    TicketsStore store
) : ObservableObject
{
#region Fields

    public TicketForm Form { get; } = new();


    /* ATTRIBUTION:
     * The conversion of enum types to an object list was adapted from StackOverflow.
     * - https://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c
     * - JakePearson (https://stackoverflow.com/users/632/jake-pearson)
     */
    public static ObservableCollection<Department> DepartmentOptions
        => new(Enum.GetValues(typeof(Department)).Cast<Department>());

#endregion

#region ICommands

    /* ATTRIBUTION:
     * Commands and command button bindings were adapted from YouTube.
     * - https://www.youtube.com/watch?v=HDSRG7GvPbo
     * - ToskersCorner (https://www.youtube.com/@ToskersCorner)
     */
    [RelayCommand]
    private async Task CancelFormAsync() {
        var confirmed = await DialogService.ShowConfirmationAsync(
            heading: "Are you sure you want to cancel?",
            message: "If you have any unsaved changes, they will be lost if you choose to go back."
        );

        if (confirmed) {
            await fluentNavigationService.NavigateToAsync<HomeView>();
        }
    }


    [RelayCommand]
    private async Task SubmitFormAsync() {
        Form.Entity.ValidateAll();
        if (!Form.Entity.HasErrors) {
            /* Attribution: Try-Catch Block
             * - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements
             */
            try {
                Form.Entity.FaultAttachments.Clear();
                Form.Entity.FaultAttachments.AddRange(
                    Form.Attachments.Select(o => o.FileName)
                );

                store.Insert(Form.Entity);
            }
            catch (Exception) {
                await DialogService.ShowFailureAsync(
                    "Something went wrong when attempting to create your ticket."
                );

                return;
            }

            await DialogService.ShowSuccessAsync(
                heading: "Your ticket has been created",
                message: "Thank you for building building a brighter tomorrow together."
            );

            await fluentNavigationService.NavigateToAsync<HomeView>();
        }
    }


    [RelayCommand]
    private async Task FileInsertAsync() {
        try {
            /* Attribution: OpenFileDialog
             * - https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog
             */
            var openFileDialog = new OpenFileDialog {
                Title = "Select your supporting images and documents",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true) {
                var selectedFilePath = openFileDialog.FileName;
                if (Form.Attachments.Any(x => x.FileName == selectedFilePath)) {
                    await DialogService.ShowFailureAsync("This file has already been uploaded.");
                    return;
                }

                Form.Attachments.Add(new Attachment(selectedFilePath));
            }
        }
        catch (Exception) {
            await DialogService.ShowFailureAsync("Something went wrong!");
        }
    }


    [RelayCommand(CanExecute = nameof(CanFileRemove))]
    private async Task FileRemoveAsync(
        Attachment? attachment
    ) {
        if (attachment == null) {
            return;
        }

        var confirmed = await DialogService.ShowConfirmationAsync(
            heading: attachment.FileName,
            message: "Are you sure you want to remove this file? It will still be available on your local machine."
        );

        if (confirmed) {
            Form.Attachments.Remove(attachment);
        }
    }


    private static bool CanFileRemove(Attachment? attachment) => attachment is not null;


    /* ATTRIBUTION:
     * The two-way RTB binding helper was adapted from StackOverflow.
     * - https://stackoverflow.com/questions/343468/richtextbox-wpf-binding
     * - Alex Maker (https://stackoverflow.com/users/41407/alex-maker)
     */
    [RelayCommand]
    private void UpdateDescription(
        RichTextBox richTextBox
    ) {
        var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

        Form.Entity.FaultDescription = textRange.Text;

        var bindingExpression = richTextBox.GetBindingExpression(RichTextBoxHelper.DocumentTextProperty);
        bindingExpression?.UpdateSource();
    }

#endregion
}
