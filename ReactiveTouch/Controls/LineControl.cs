using NControl.Abstractions;
using NGraphics;
using Xamarin.Forms;

namespace ReactiveTouch
{
	public class LineControl : NControlView
	{
		public NGraphics.Point Start { get; protected set;}

		public NGraphics.Point End { get; protected set; }

	    public LineControl(NGraphics.Point start, NGraphics.Point end)
		{
		    Start = start;
			End = end;

			var line = new NControlView
			{
			    DrawingFunction = (canvas, rect) =>
			    {
			        canvas.DrawLine(Start, End, new Pen(FillColor.ToNGraphicsColor(), Thickness));
			    }
			};

			Content = line;

		}

		public static BindableProperty FillColorProperty = BindableProperty.CreateAttached(
			propertyName: "FillColor",
			returnType: typeof(Xamarin.Forms.Color),
			declaringType: typeof(View),
			defaultValue: Xamarin.Forms.Color.Default,
			propertyChanged: (bindable, oldValue, newValue) =>
				{
					var ctrl = (LineControl)bindable;
					ctrl.FillColor = (Xamarin.Forms.Color)newValue;
				}
		);

		public Xamarin.Forms.Color FillColor
		{
			get { return (Xamarin.Forms.Color)GetValue(FillColorProperty); }
			set
			{
				SetValue(FillColorProperty, value);
				Invalidate();
			}
		}

		public static BindableProperty ThicknessProperty = BindableProperty.CreateAttached(
			propertyName: "Thickness",
			returnType: typeof(double),
			declaringType: typeof(View),
			defaultValue: 1.0,
			propertyChanged: (bindable, oldValue, newValue) =>
				{
					var ctrl = (LineControl)bindable;
					ctrl.Thickness = (double)newValue;
				}
		);

		public double Thickness
		{
			get { return (double)GetValue(ThicknessProperty); }
			set
			{
				SetValue(ThicknessProperty, value);
				Invalidate();
			}
		}
	}
}

