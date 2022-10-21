using Microsoft.Toolkit.Uwp.Notifications;

namespace FilesDirCmd.Utils;

public static class Tasks
{
    public static void SendNotif(string title, string msg)
    {
        new ToastContentBuilder()
            .AddText(title)
            .AddText(msg)
            .SetToastDuration(ToastDuration.Short)
            .Show();
    }
}