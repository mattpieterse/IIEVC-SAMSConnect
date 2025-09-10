using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;

namespace SAMS.Connect.MVVM.Models;

/* ATTRIBUTION:
 * The two-way RTB binding helper was adapted from StackOverflow.
 * - https://stackoverflow.com/questions/343468/richtextbox-wpf-binding
 * - Alex Maker (https://stackoverflow.com/users/41407/alex-maker)
 */
public static class RichTextBoxHelper
{
    private static bool _isUpdating;


    public static readonly DependencyProperty DocumentTextProperty =
        DependencyProperty.RegisterAttached(
            "DocumentText",
            typeof(string),
            typeof(RichTextBoxHelper),
            new FrameworkPropertyMetadata(
                string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnDocumentTextPropertyChanged,
                null,
                false,
                UpdateSourceTrigger.Explicit
            )
        );


    public static string GetDocumentText(DependencyObject obj) {
        return (string) obj.GetValue(DocumentTextProperty);
    }


    public static void SetDocumentText(DependencyObject obj, string value) {
        obj.SetValue(DocumentTextProperty, value);
    }


    private static void OnDocumentTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (_isUpdating) {
            return;
        }

        if (d is not RichTextBox rtb) {
            return;
        }

        rtb.TextChanged -= Rtb_TextChanged;

        var text = (string) e.NewValue;
        var doc = new FlowDocument();
        doc.Blocks.Add(new Paragraph(new Run(text)));
        rtb.Document = doc;

        rtb.TextChanged += Rtb_TextChanged;
    }


    private static void Rtb_TextChanged(object sender, TextChangedEventArgs e) {
        if (sender is not RichTextBox rtb) {
            return;
        }

        _isUpdating = true;

        var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
        var text = textRange.Text;

        SetDocumentText(rtb, text);

        var binding = BindingOperations.GetBindingExpression(rtb, DocumentTextProperty);
        binding?.UpdateSource();

        _isUpdating = false;
    }


    static RichTextBoxHelper() {
        EventManager.RegisterClassHandler(typeof(RichTextBox),
            TextBoxBase.TextChangedEvent,
            new TextChangedEventHandler(Rtb_TextChanged)
        );
    }
}
