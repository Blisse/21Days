using System;
using System.Windows;

namespace HappyBetter.Views.CustomMessageBoxes
{
    public static class DateAlreadyExistsMessageBox
    {
        private const string Header = "Date already exists";
        private const MessageBoxButton Buttons = MessageBoxButton.OK;

        public static MessageBoxResult Show()
        {
            const string text = "You've already added this date.";
            return MessageBox.Show(text, Header, Buttons);
        }

        public static MessageBoxResult Show(DateTime dateTime)
        {
            var text = String.Format("You already have an entry for {0}.", dateTime.ToString("D"));
            return MessageBox.Show(text, Header, Buttons);
        }
    }
}
