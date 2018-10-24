#if __IOS__
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Uno.Extensions;
using Uno.Logging;

namespace Windows.UI.ViewManagement
{
	partial class ApplicationView
	{
		private static bool UseSafeAreaInsets => UIDevice.CurrentDevice.CheckSystemVersion(11, 0);

		internal void SetCoreBounds(UIKit.UIWindow keyWindow, Foundation.Rect windowBounds)
		{
			var statusBarHeight = !UIApplication.SharedApplication.StatusBarHidden
				? UIApplication.SharedApplication.StatusBarFrame.Size.Height
				: 0;

			// Not respecting its own documentation. https://developer.apple.com/documentation/uikit/uiview/2891103-safeareainsets?language=objc
			// iOS returns all zeros for SafeAreaInsets on non-iPhoneX phones. (ignoring nav bars or status bars)
			// For that reason, we will set the window's visible bounds to the SafeAreaInsets only for iPhones with notches,
			// other phones will have insets that consider the status bar
			var inset = UseSafeAreaInsets
				? keyWindow.SafeAreaInsets
				: new UIEdgeInsets(0, 0, 0, 0);

			var topPadding = Math.Max(inset.Top, statusBarHeight);

			var newVisibleBounds = new Foundation.Rect(
				x: windowBounds.Left + inset.Left,
				y: windowBounds.Top + topPadding,
				width: windowBounds.Width - inset.Right - inset.Left,
				height: windowBounds.Height - topPadding - inset.Bottom
			);

			if (VisibleBounds != newVisibleBounds)
			{
				VisibleBounds = newVisibleBounds;

				if (this.Log().IsEnabled(Microsoft.Extensions.Logging.LogLevel.Debug))
				{
					this.Log().Debug($"Updated visible bounds {VisibleBounds}, SafeAreaInsets: {inset}");
				}

				VisibleBoundsChanged?.Invoke(this, null);
			}
		}
	}
}
#endif
