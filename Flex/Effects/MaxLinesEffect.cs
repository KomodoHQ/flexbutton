using System;
using Xamarin.Forms;

namespace Flex.Effects
{
	public class MaxLinesEffect : RoutingEffect
	{            
		public int MaxLines { get; set; }

		public MaxLinesEffect() : base($"Flex.Effects.{nameof(MaxLinesEffect)}") { }
	}
}
