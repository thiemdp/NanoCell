using System;
using System.Collections.Generic;
using System.Text;

namespace NanoCell.UI.Components.Toast
{
    public class ToastService:IToastService
    {
        public ToastService() { }

        /// <summary>
        /// A event that will be invoked when showing a toast
        /// </summary>
        public event Action<ToastStyle, string, string> OnShow;

        /// <summary>
        /// Shows a information toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowInfo(string message, string heading = "")
        {
            ShowToast(ToastStyle.Info, message, heading);
        }

        /// <summary>
        /// Shows a success toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowSuccess(string message, string heading = "")
        {
            ShowToast(ToastStyle.Success, message, heading);
        }

        /// <summary>
        /// Shows a warning toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowWarning(string message, string heading = "")
        {
            ShowToast(ToastStyle.Warning, message, heading);
        }

        /// <summary>
        /// Shows a error toast 
        /// </summary>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowError(string message, string heading = "")
        {
            ShowToast(ToastStyle.Error, message, heading);
        }

        /// <summary>
        /// Shows a toast using the supplied settings
        /// </summary>
        /// <param name="level">Toast style to display</param>
        /// <param name="message">Text to display on the toast</param>
        /// <param name="heading">The text to display as the toasts heading</param>
        public void ShowToast(ToastStyle style, string message, string heading = "")
        {
            OnShow?.Invoke(style, message, heading);
        }
    }
}
