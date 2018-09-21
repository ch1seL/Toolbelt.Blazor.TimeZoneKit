﻿using System;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.JSInterop;

namespace Toolbelt.Blazor.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods releated with TimeZoneKit.
    /// </summary>
    public static class TimeZoneKitExtension
    {
        /// <summary>
        /// Set "TimeZoneInfo.Local" to actual local time zone. (include "UseSystemTimeZones()")
        /// </summary>
        public static void UseLocalTimeZone(this IBlazorApplicationBuilder app)
        {
            app.UseSystemTimeZones();
            JSRuntime.Current.InvokeAsync<string>("eval", "DotNet.invokeMethod('Toolbelt.Blazor.TimeZoneKit','InitLocalTimeZone', (function(){try { return ''+ Intl.DateTimeFormat().resolvedOptions().timeZone; } catch(e) {} return 'UTC';}()))");
        }

        /// <summary>
        /// Set "TimeZoneInfo.Local" to the time zone that specified id. (include "UseSystemTimeZones()")
        /// </summary>
        public static void UseLocalTimeZone(this IBlazorApplicationBuilder app, string timeZoneId)
        {
            app.UseSystemTimeZones();
            TimeZoneKit.TimeZoneKit.SetLocalTimeZone(timeZoneId);
        }

        /// <summary>
        /// Set "TimeZoneInfo.Local" to the time zone that specified IANA name. (include "UseSystemTimeZones()")
        /// </summary>
        public static void UseLocalTimeZoneByIANAName(this IBlazorApplicationBuilder app, string ianaTimeZoneName)
        {
            app.UseSystemTimeZones();
            TimeZoneKit.TimeZoneKit.SetLocalTimeZoneByIANAName(ianaTimeZoneName);
        }

        /// <summary>
        /// Ensure "TimeZoneInfo.GetSystemTimeZones()"
        /// </summary>
        public static void UseSystemTimeZones(this IBlazorApplicationBuilder app)
        {
            if (TimeZoneInfo.GetSystemTimeZones().Count == 0)
            {
                TimeZoneKit.TimeZoneKit.SetSystemTimeZones(TimeZoneKit.TimeZoneKit.CreateSystemTimeZones());
            }
        }
    }
}
