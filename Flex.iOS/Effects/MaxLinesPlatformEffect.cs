using System;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Flex.iOS.Effects;

[assembly: ExportEffect(typeof(MaxLinesPlatformEffect), nameof(Flex.Effects.MaxLinesEffect))]
namespace Flex.iOS.Effects
{
    public class MaxLinesPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
			var effect = Element.Effects.OfType<Flex.Effects.MaxLinesEffect>().First();
            if (effect == null)
            {
                return;
            }
            if (Control is UILabel nativeLabel)
            {
				nativeLabel.Lines = effect.MaxLines;
            }
        }

        protected override void OnDetached() { }
    }
}
