using System.Windows;

namespace HappyBetter.Views.CustomMessageBoxes
{
    public static class DateAlreadyExistsMessageBox
    {
        public static MessageBoxResult Show()
        {
            const string text = "This date already exists!";
            const string header = "An error occurred.";
            const MessageBoxButton buttons = MessageBoxButton.OK;
            return MessageBox.Show(text, header, buttons);
        }
    }
}
