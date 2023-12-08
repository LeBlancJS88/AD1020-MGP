using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    private readonly string channelId = "MyChannel";

    private void Awake()
    {
        AndroidNotificationChannel channel = new()
        {
            Id = channelId,
            Name = "Default channel",
            Importance = Importance.Default,
            Description = "Generic notifications"
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            AndroidNotification notification = new()
            {
                Title = "You'll be back...",
                Text = "You'll be wasting your time anyway.",
                FireTime = DateTime.Now.AddSeconds(60),
                LargeIcon = "cookie_large"
            };
            AndroidNotificationCenter.CancelScheduledNotification((int)NotificationID.Basic);
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.Basic);
        }
    }

    public enum NotificationID
    {
        Basic
    }
}
