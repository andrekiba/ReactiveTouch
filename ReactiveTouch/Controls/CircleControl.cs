using System;
using System.Windows.Input;
using NControl.Abstractions;
using NGraphics;
using Xamarin.Forms;

namespace ReactiveTouch
{
	public class CircleControl : NControlView
	{
	    public CircleControl()
		{
		    HeightRequest = 20;
			WidthRequest = 20;

			var circles = new NControlView
			{

			    DrawingFunction = (canvas, rect) =>
			    {
			        var fillColor = new NGraphics.Color(FillColor.R,
			            FillColor.G, FillColor.B, FillColor.A);
					
			        canvas.FillEllipse(rect, NGraphics.Color.FromRGB(89,52,133));
			        rect.Inflate(new NGraphics.Size(-2, -2));
			        //canvas.FillEllipse(rect, Colors.White);
			        //rect.Inflate(new NGraphics.Size(-4, -4));
			        canvas.FillEllipse(rect, fillColor);
			    }
			};

			Content = new Grid
			{
				Children = {circles}
			};
		}


		//public static BindableProperty CommandProperty = BindableProperty.CreateAttached(
		//	propertyName: "Command",
		//          returnType: typeof(ICommand),
		//          declaringType: typeof(View),
		//          defaultValue: null,
		//          defaultBindingMode: BindingMode.OneWay,
		//          validateValue: null,
		//          propertyChanged: (bindable, oldValue, newValue) =>
		//		{
		//			var ctrl = (CircleControl)bindable;
		//			ctrl.Command = (ICommand)newValue;
		//		}
		//);

		//public ICommand Command
		//{
		//	get { return (ICommand)GetValue(CommandProperty); }
		//	set
		//	{
		//		SetValue(CommandProperty, value);
		//	}
		//}

		//public static BindableProperty CommandParameterProperty = BindableProperty.CreateAttached(
		//	propertyName: "CommandParameter",
		//	returnType: typeof(object),
		//	declaringType: typeof(View),
		//	defaultValue: null,
		//	defaultBindingMode: BindingMode.OneWay,
		//	validateValue: null,
		//	propertyChanged: (bindable, oldValue, newValue) =>
		//		{
		//			var ctrl = (CircleControl)bindable;
		//			ctrl.CommandParameter = newValue;
		//		}
		//);

		//public object CommandParameter
		//{
		//	get { return GetValue(CommandParameterProperty); }
		//	set
		//	{
		//		SetValue(CommandParameterProperty, value);
		//	}
		//}

		public static BindableProperty FillColorProperty = BindableProperty.CreateAttached(
			propertyName: "FillColor",
			returnType: typeof(Xamarin.Forms.Color),
			declaringType: typeof(View),
			defaultValue: Xamarin.Forms.Color.Default,
			propertyChanged: (bindable, oldValue, newValue) =>
				{
					var ctrl = (CircleControl)bindable;
					ctrl.FillColor = (Xamarin.Forms.Color) newValue;
				}
		);

		public Xamarin.Forms.Color FillColor
		{
			get { return (Xamarin.Forms.Color) GetValue(FillColorProperty); }
			set
			{
				SetValue(FillColorProperty, value);
				Invalidate();
			}
		}

	}
}