using System;
namespace ReactiveTouch
{
	public static class Exstensions
	{
		public static Xamarin.Forms.Color RandomColor()
		{
			var rand = new Random();
			return Xamarin.Forms.Color.FromRgb(rand.Next(255), rand.Next(255), rand.Next(255));
		}

		public static NGraphics.Color ToNGraphicsColor(this Xamarin.Forms.Color color)
		{
			return NGraphics.Color.FromRGB(color.R, color.G, color.B, color.A);
		}	
	}
}

