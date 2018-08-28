using System;
using System.Drawing;
using Uno.Extensions;
using Uno.UI;
using Uno.UI.Views.Controls;
using Uno.UI.DataBinding;
using System.Linq;
using Windows.UI.Xaml.Input;

#if XAMARIN_IOS_UNIFIED
using View = UIKit.UIView;
using Color = UIKit.UIColor;
using Font = UIKit.UIFont;
using UIKit;
using CoreGraphics;
#elif XAMARIN_IOS
using View = MonoTouch.UIKit.UIView;
using Color = MonoTouch.UIKit.UIColor;
using Font = MonoTouch.UIKit.UIFont;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
using CGPoint = System.Drawing.PointF;
using nint = System.Int32;
using CGSize = System.Drawing.SizeF;
#endif


namespace Windows.UI.Xaml.Controls
{
	public partial class Control
	{
		public Control ()
		{
			InitializeControl();
			Initialize();
		}

		void Initialize()
		{
		}

		/// <summary>
		/// Gets the first sub-view of this control or null if there is none
		/// </summary>
		public IFrameworkElement GetTemplateRoot()
		{
			return Subviews.FirstOrDefault() as IFrameworkElement;
		}

		partial void UnregisterSubView()
		{
			if (Subviews.Length > 0)
			{
				Subviews[0].RemoveFromSuperview();
			}
		}

		partial void RegisterSubView(UIView child)
		{
			if(Subviews.Length != 0)
			{
				throw new Exception("A Xaml control may not contain more than one child.");
			}

			if (FeatureConfiguration.UIElement.UseLegacyClipping)
			{
				// This is no longer needed when using normal clipping.
				// Assigning the frame overrides the standard Uno layouter, which 
				// prevents the clipping to be set to the proper size.

				child.Frame = Bounds;
				child.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			}

			AddSubview(child);
		}

		protected virtual bool RequestFocus(FocusState state)
		{
			FocusState = state;

			return true;
		}
	}
}

