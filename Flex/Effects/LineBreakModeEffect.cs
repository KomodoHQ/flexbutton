using System;
using Xamarin.Forms;

namespace Flex.Effects
{
	public class LineBreakModeEffect : RoutingEffect
    {
		public LineBreakMode LineBreakMode { get; set; }

		public LineBreakModeEffect() : base($"Flex.Effects.{nameof(LineBreakModeEffect)}") { }
    }
}
