namespace PersianUIControlsMaui.Models;


public abstract class BaseConfig
{
    internal static List<string> Icons = new List<string>()
        {
            "\uf128", //question ?
            "\uf12a", //exclamation !
            "\uf00d", //close ×
            "\uf071", //warning 
            "\uf1eb", //Wifit
            "\uf06b", //Gift
            "\uf00d", //Cancel
            "\uf00c", //Accept
            "\uf03e", //Gallery
            "\uf030", //Camera
            "\uf0d6", //Money
            "\uf0ea", //Copy
            "\uf10b", //Mobile
            "\uf016", //New
            "\uf06e", //Eye
            "\uf232", //WhatsApp
            "\uf095", //Phone
            "\uf1fc", //Brush
            "\uf290", //ShoppingBag
            "\uf27a", //Message
        };
    internal static List<string> IconsColor = new List<string>()
        {
            "#b32e91", //question ?
            "#28649b", //exclamation !
            "#bf2731", //close ×
            "#ea8823", //warning 
            "#28649b", //Wifit
            "\uf06b", //Gift
            "#bf2731", //Cancel
            "#1fb070", //Accept
            "\uf03e", //Gallery
            "\uf030", //Camera
            "\uf0d6", //Money
            "\uf0ea", //Copy
            "#1fb070", //Mobile
            "#bf2731", //New
            "#1fb070", //Eye
            "#00e676", //WhatsApp
            "#222222", //Phone
            "#b32e91", //Brush
            "#b32e91", //ShoppingBag
            "#b32e91", //Message
        };

    Color dialogColor = null;
    public string Title { get; set; }
    public string Message { get; set; }
    public MessageIcon Icon { get; set; }
    public string FontIcon { get { return Icons[((int)Icon)]; } }
    public Color DialogColor { get { return dialogColor ?? Color.FromArgb(IconsColor[((int)Icon)]); } set { dialogColor = value; } }
    public bool CloseWhenBackgroundIsClicked { get; set; } = true;
    public Color BackgroundColor { get; set; } = Colors.White;
}
public class PromptResult
{
    public object Value { get; set; }
    public bool IsOk { get; set; }
}
public class PromptConfig : BaseConfig
{
    public string Placeholder { get; set; }
    public string DefaultValue { get; set; }
    public MessageIcon AcceptIcon { get; set; } = MessageIcon.ACCEPT;
    public string AcceptFontIcon { get { return Icons[((int)AcceptIcon)]; } }
    public MessageIcon CancelIcon { get; set; } = MessageIcon.CANCEL;
    public string CancelFontIcon { get { return Icons[((int)CancelIcon)]; } }
    public string AcceptText { get; set; } = "ثبت";
    public string CancelText { get; set; } = "انصراف";
    public Action<PromptResult> OnAction { get; set; }
    public string AppendText { get; set; }
    public bool CloseAfterAccept { get; set; } = true;
}
public class CustomDialogConfig : BaseConfig
{
    public MessageIcon AcceptIcon { get; set; } = MessageIcon.ACCEPT;
    public string AcceptFontIcon { get { return Icons[((int)AcceptIcon)]; } }
    public MessageIcon CancelIcon { get; set; } = MessageIcon.CANCEL;
    public string CancelFontIcon { get { return Icons[((int)CancelIcon)]; } }
    public string AcceptText { get; set; } = "ثبت";
    public string CancelText { get; set; } = "انصراف";
    public Action<bool> OnAction { get; set; }

    /// <summary>
    /// default value is true
    /// </summary>
    public bool CloseAfterAccept { get; set; } = true;
    public View Content { get; set; }

    /// <summary>
    /// default value is true
    /// </summary>
    public bool Cancelable { get; set; } = true;
}
public class ConfirmConfig : BaseConfig
{
    public MessageIcon CancelIcon { get; set; } = MessageIcon.CANCEL;
    public MessageIcon AcceptIcon { get; set; } = MessageIcon.ACCEPT;
    public string AcceptFontIcon { get { return Icons[((int)AcceptIcon)]; } }
    public string CancelFontIcon { get { return Icons[((int)CancelIcon)]; } }
    public string AcceptText { get; set; } = "ثبت";
    public string CancelText { get; set; } = "انصراف";
    public Action<bool> OnAction { get; set; }
}
public class AlertConfig : BaseConfig
{
    public MessageIcon AcceptIcon { get; set; } = MessageIcon.ACCEPT;
    public string AcceptFontIcon { get { return Icons[((int)AcceptIcon)]; } }
    public string AcceptText { get; set; } = "ثبت";
}
public class ToastConfig : BaseConfig
{
    public CommunityToolkit.Maui.Core.ToastDuration Duration { get; set; } = CommunityToolkit.Maui.Core.ToastDuration.Short;
}
public class SnackbarConfig: BaseConfig
{
    public Action OnAction { get; set; }
    public string AcceptText { get; set; } = "باشه";
    public TimeSpan Duration { get; set; } = new TimeSpan(0, 0, 15);

}
public enum MessageIcon
{
    QUESTION = 0,
    INFORMATION,
    ERROR,
    WARNING,
    WIFI,
    GIFT,
    CANCEL,
    ACCEPT,
    GALLERY,
    CAMERA,
    Money,
    COPY,
    MOBILE,
    New,
    Eye,
    WHATSAPP,
    PHONE,
    BRUSH,
    ShoppingBag,
    Message
}
