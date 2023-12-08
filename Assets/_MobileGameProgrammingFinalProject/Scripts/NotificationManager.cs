using System;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public UIManager uiManager;
    public IdleManager idleManager;

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
            ScheduleBasicNotification();
            ScheduleThreeHourNotification();
            ScheduleSixHourNotification();
        }
    }

    private void ScheduleBasicNotification()
    {
        AndroidNotification notification = new()
        {
            Title = "You'll be back...",
            Text = "You'll be wasting your time anyway.",
            FireTime = DateTime.Now.AddSeconds(5),
            LargeIcon = "logo_large"
        };
        AndroidNotificationCenter.CancelScheduledNotification((int)NotificationID.Basic);
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.Basic);
    }

    private void ScheduleThreeHourNotification()
    {
        if (uiManager.stressGeneratorUnlocked || idleManager.anxietyResourceEnabled)
        {
            AndroidNotification notification = new()
            {
                Title = "Generating Stress...",
                Text = "You've built up 3 hours of stress.",
                FireTime = DateTime.Now.AddHours(3),
                LargeIcon = "resource_icon"
            };
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.ThreeHour);
        }
    }

    private void ScheduleSixHourNotification()
    {
        if (uiManager.stressGeneratorUnlocked || idleManager.anxietyResourceEnabled)
        {
            AndroidNotification notification = new()
            {
                Title = "Idle Mastery",
                Text = "You've built up 6 hours of stress. Time to spend some.",
                FireTime = DateTime.Now.AddHours(6),
                LargeIcon = "resource_icon"
            };
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.SixHour);
        }
    }

    public enum NotificationID
    {
        Basic,
        ThreeHour,
        SixHour
    }
}