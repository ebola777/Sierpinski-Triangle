namespace SierpinskiTriangle.Utilities
{
    using System;
    using System.Windows.Forms;

    using SierpinskiTriangle.Lang;

    public static class ErrorHandling
    {
        #region Public Methods and Operators

        public static void ShowException(Exception ex)
        {
            MessageBox.Show(
                string.Format("{0}\n\n{1}", ex.Message, ex),
                CoreLang.MessageBox_Caption_Error,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        #endregion

        #region Methods

        internal static void Exit(ErrorCode errCode)
        {
            Environment.Exit((int)errCode);
        }

        internal static DialogResult ShowError(
            string text,
            string caption,
            ErrorCode errCode,
            MessageBoxButtons button,
            MessageBoxIcon icon)
        {
            return MessageBox.Show(text, string.Format("{0} 0x{1:X4}", caption, (int)errCode), button, icon);
        }

        #endregion
    }
}