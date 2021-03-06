using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinEvolve.Droid.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		const string NotificationIdKey = "notification_id";
		static readonly int NotificationIdDefault = 0;

		public static int NotificationId
		{
			get { return AppSettings.GetValueOrDefault(NotificationIdKey, NotificationIdDefault); }
			set
			{
				AppSettings.AddOrUpdateValue(NotificationIdKey, value);
			}
		}

		public static int GetUniqueNotificationId()
		{
			return NotificationId++;
		}

	}
}