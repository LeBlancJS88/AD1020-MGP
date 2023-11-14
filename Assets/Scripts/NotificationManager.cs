using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    private readonly string channelId = "MyChannel";

    [SerializeField] private Button notificationButton;

    public enum NotificationID
    {
        Basic,
        Button,
    }

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

    private void Start()
    {
        notificationButton.onClick.AddListener(ScheduleNotification);
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            AndroidNotification notification = new()
            {
                Title = "The game misses you!",
                Text = "Come back please!",
                FireTime = DateTime.Now.AddSeconds(30),
                LargeIcon = "cookie_large"
            };
            AndroidNotificationCenter.CancelScheduledNotification((int)NotificationID.Basic);
            AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.Basic);
        }

    }

    public void ScheduleNotification()
    {
        AndroidNotification notification = new()
        {
            Title = "You clicked a button!",
            Text = "Remember when you did that?",
            FireTime = DateTime.Now.AddSeconds(60),
            LargeIcon = "cookie_large"
        };
        AndroidNotificationCenter.CancelScheduledNotification((int)NotificationID.Basic);
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, (int)NotificationID.Button);
    }
}
