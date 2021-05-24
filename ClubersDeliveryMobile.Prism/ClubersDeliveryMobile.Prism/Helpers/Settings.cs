﻿using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;


namespace ClubersDeliveryMobile.Prism.Helpers
{
    public static class Settings
    {
        private const string _user = "user";
        private const string _token = "token";
        private const string _isLogin = "isLogin";
        private const string _isRemembered = "IsRemembered";
        private const string _isAvailable = "isAvailable";
        private const string _isDeliveryStarted = "isAvailable";

        private static readonly bool _boolDefault = false;
        private static readonly string _stringDefault = string.Empty;
        private static ISettings AppSettings => CrossSettings.Current;
        public static bool IsRemembered
        {
            get => AppSettings.GetValueOrDefault(_isRemembered, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isRemembered, value);
        }
        public static bool IsAvailable
        {
            get => AppSettings.GetValueOrDefault(_isAvailable, !_boolDefault);
            set => AppSettings.AddOrUpdateValue(_isAvailable, value);
        }

        public static bool IsDeliveryStarted
        {
            get => AppSettings.GetValueOrDefault(_isDeliveryStarted, !_boolDefault);
            set => AppSettings.AddOrUpdateValue(_isDeliveryStarted, value);
        }
        public static string User
        {
            get => AppSettings.GetValueOrDefault(_user, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_user, value);
        }
        public static string Token
        {
            get => AppSettings.GetValueOrDefault(_token, _stringDefault);
            set => AppSettings.AddOrUpdateValue(_token, value);
        }
        public static bool IsLogin
        {
            get => AppSettings.GetValueOrDefault(_isLogin, _boolDefault);
            set => AppSettings.AddOrUpdateValue(_isLogin, value);
        }
    }
}
