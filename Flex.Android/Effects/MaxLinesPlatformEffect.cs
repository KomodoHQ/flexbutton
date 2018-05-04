using System;
using Xamarin.Forms.Platform.Android;
using System.Linq;
using Flex.Effects;
using Android.Widget;
using Android.Text;
using Xamarin.Forms;
using Flex.Android.Effects;

[assembly: ExportEffect(typeof(MaxLinesPlatformEffect), nameof(MaxLinesEffect))]
namespace Flex.Android.Effects
{
	public class MaxLinesPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var maxLinesEffect = Element.Effects.OfType<MaxLinesEffect>().First();
            if (maxLinesEffect == null)
            {
                return;
            }

            if (Control is TextView nativeTextView)
            {
                if (maxLinesEffect.MaxLines <= 0)
                {
                    nativeTextView.SetMaxLines(99999);
                }
                else if (maxLinesEffect.MaxLines == 1)
                {
                    nativeTextView.SetSingleLine(true);
                }
                else if (maxLinesEffect.MaxLines > 1)
                {
					nativeTextView.SetMaxLines(maxLinesEffect.MaxLines);
                }
            }
        }

        protected override void OnDetached() { }
    }
}
