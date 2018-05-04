using System;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Flex.Effects;
using Android.Widget;
using Android.Text;
using Xamarin.Forms;
using Flex.Android.Effects;

[assembly: ExportEffect(typeof(LineBreakModePlatformEffect), nameof(LineBreakModeEffect))]
namespace Flex.Android.Effects
{
	public class LineBreakModePlatformEffect : PlatformEffect
    {
		protected override void OnAttached()
        {
			var maxLinesEffect = Element.Effects.OfType<LineBreakModeEffect>().First();
            if (maxLinesEffect == null)
            {
                return;
            }

            if (Control is TextView nativeTextView)
            {
				switch(maxLinesEffect.LineBreakMode)
				{
					case LineBreakMode.TailTruncation:
						nativeTextView.Ellipsize = TextUtils.TruncateAt.End;
						break;
					case LineBreakMode.HeadTruncation:
						nativeTextView.Ellipsize = TextUtils.TruncateAt.Start;
						break;
					case LineBreakMode.MiddleTruncation:
                        nativeTextView.Ellipsize = TextUtils.TruncateAt.Middle;
						break;
					default:
						nativeTextView.Ellipsize = TextUtils.TruncateAt.End;
                        break;
				}
            }
        }

        protected override void OnDetached() { }
    }
}
