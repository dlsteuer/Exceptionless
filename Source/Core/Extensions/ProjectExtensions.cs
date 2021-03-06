﻿#region Copyright 2014 Exceptionless

// This program is free software: you can redistribute it and/or modify it 
// under the terms of the GNU Affero General Public License as published 
// by the Free Software Foundation, either version 3 of the License, or 
// (at your option) any later version.
// 
//     http://www.gnu.org/licenses/agpl-3.0.html

#endregion

using System;
using Exceptionless.Models;

namespace Exceptionless.Core.Extensions {
    public static class ProjectExtensions {
        public static NotificationSettings GetNotificationSettings(this Project project, string userId) {
            return project.NotificationSettings.ContainsKey(userId) ? project.NotificationSettings[userId] : null;
        }

        public static void AddDefaultOwnerNotificationSettings(this Project project, string userId, NotificationSettings settings = null) {
            if (project.NotificationSettings.ContainsKey(userId))
                return;

            project.NotificationSettings.Add(userId, settings ?? new NotificationSettings {
                Mode = NotificationMode.New,
                SendDailySummary = true,
                ReportCriticalErrors = true,
                ReportRegressions = true
            });
        }

        public static TimeSpan DefaultTimeZoneOffset(this Project project) {
            return project.DefaultTimeZone().BaseUtcOffset;
        }

        public static TimeZoneInfo DefaultTimeZone(this Project project) {
            if (project == null)
                return TimeZoneInfo.Local;

            TimeZoneInfo tzi;
            try {
                tzi = TimeZoneInfo.FindSystemTimeZoneById(project.TimeZone);
            } catch {
                tzi = TimeZoneInfo.Local;
            }
            return tzi;
        }
    }
}