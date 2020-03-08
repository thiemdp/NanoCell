using System;
using Syncfusion.EJ2.Blazor.Notifications;
using Syncfusion.EJ2.Blazor;
using Microsoft.AspNetCore.Components;

namespace NanoCell.UI.Components.Toast
{
    public partial class Toast
    {
        EjsToast ToastObj;
        [Inject] private IToastService ToastService { get; set; }
        [Parameter] public string InfoClass { get; set; } = "e-toast-info";
        [Parameter] public string InfoIconClass { get; set; } = "e-info toast-icons";
        [Parameter] public string SuccessClass { get; set; } = "e-toast-success";
        [Parameter] public string SuccessIconClass { get; set; } = "e-success toast-icons";
        [Parameter] public string WarningClass { get; set; } = "e-toast-warning";
        [Parameter] public string WarningIconClass { get; set; } = "e-warning toast-icons";
        [Parameter] public string ErrorClass { get; set; } = "e-toast-danger";
        [Parameter] public string ErrorIconClass { get; set; } = "e-error toast-icons";
        [Parameter] public bool ShowCloseButton { get; set; } = true;
        [Parameter] public bool ShowProgressBar { get; set; } = true;
        [Parameter] public string Width { get; set; } = "auto";
        public string ShowEffect { get; set; } = "SlideRightIn";
        public string HideEffect { get; set; } = "SlideRightOut";
        [Parameter] public ToastPosition Position { get; set; } = ToastPosition.BottomRight;
        string XValue = "50";
        string YValue = "50";
        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            ToastSettings();
        }

        private void ToastSettings()
        {
            switch (Position)
            {
                case ToastPosition.TopLeft:
                    XValue = "Left"; YValue = "Top"; break;
                case ToastPosition.TopRight:
                    XValue = "Right"; YValue = "Top"; break;
                case ToastPosition.TopCenter:
                    XValue = "Center"; YValue = "Top"; break;
                case ToastPosition.TopFullWidth:
                    Width = "100%"; XValue = "Center"; YValue = "Top"; break;
                case ToastPosition.BottomLeft:
                    XValue = "Left"; YValue = "Bottom"; break;
                case ToastPosition.BottomRight:
                    XValue = "Right"; YValue = "Bottom"; break;
                case ToastPosition.BottomCenter:
                    XValue = "Center"; YValue = "Bottom"; break;
                case ToastPosition.BottomFullWidth:
                    Width = "100%"; XValue = "Center"; YValue = "Bottom"; break;
            }
        }
        private void ShowToast(ToastStyle style, string message, string heading)
        {
            InvokeAsync(() =>
            {
                var settings = BuildToastSettings(style, message, heading);
                 ToastObj.Show(settings);
            });

        }

        private ToastModel BuildToastSettings(ToastStyle style, string message, string heading)
        {
            switch (style)
            {
                case ToastStyle.Info:
                    return new ToastModel {Title= (string.IsNullOrWhiteSpace(heading) ? "Info":heading), Content = message ,CssClass = InfoClass,Icon = InfoIconClass,Width=Width,ShowCloseButton=ShowCloseButton,ShowProgressBar=ShowProgressBar,NewestOnTop=true };
                case ToastStyle.Success:
                    return new ToastModel { Title = (string.IsNullOrWhiteSpace(heading) ? "Success" : heading), Content = message, CssClass = SuccessClass, Icon = SuccessIconClass, Width = Width, ShowCloseButton = ShowCloseButton, ShowProgressBar = ShowProgressBar, NewestOnTop = true };
                case ToastStyle.Warning:
                    return new ToastModel { Title = (string.IsNullOrWhiteSpace(heading) ? "Warning" : heading), Content = message, CssClass = WarningClass, Icon = WarningIconClass, Width = Width, ShowCloseButton = ShowCloseButton, ShowProgressBar = ShowProgressBar, NewestOnTop = true };
                case ToastStyle.Error:
                    return new ToastModel { Title = (string.IsNullOrWhiteSpace(heading) ? "Error" : heading), Content = message, CssClass = ErrorClass, Icon = ErrorIconClass, Width = Width, ShowCloseButton = ShowCloseButton, ShowProgressBar = ShowProgressBar, NewestOnTop = true };
            }
            throw new InvalidOperationException();
        }
    }
}
