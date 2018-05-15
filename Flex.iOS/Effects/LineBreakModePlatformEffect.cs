using System;
using Xamarin.Forms.Platform.iOS;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Flex.iOS.Effects;
using Flex.Effects;

[assembly: ExportEffect(typeof(LineBreakModePlatformEffect), nameof(LineBreakModeEffect))]
namespace Flex.iOS.Effects
{
	public class LineBreakModePlatformEffect : PlatformEffect
    {
		protected override void OnAttached()
        {
            var effect = Element.Effects.OfType<LineBreakModeEffect>().First();
            if (effect == null)
            {
                return;
            }
            if (Control is UILabel nativeLabel)
            {
				switch (effect.LineBreakMode)
                {
                    case LineBreakMode.TailTruncation:
						nativeLabel.LineBreakMode = UILineBreakMode.TailTruncation;
                        break;
                    case LineBreakMode.HeadTruncation:
						nativeLabel.LineBreakMode = UILineBreakMode.HeadTruncation;
                        break;
                    case LineBreakMode.MiddleTruncation:
						nativeLabel.LineBreakMode = UILineBreakMode.MiddleTruncation;
                        break;
					case LineBreakMode.CharacterWrap:
                        nativeLabel.LineBreakMode = UILineBreakMode.CharacterWrap;
						break;
					case LineBreakMode.NoWrap:
                        nativeLabel.LineBreakMode = UILineBreakMode.Clip;
						break;
					case LineBreakMode.WordWrap:
                        nativeLabel.LineBreakMode = UILineBreakMode.WordWrap;
                        break;
					default:
						nativeLabel.LineBreakMode = UILineBreakMode.Clip    ;
                        break;
                }
            }
        }

        protected override void OnDetached() { }
    }
}
