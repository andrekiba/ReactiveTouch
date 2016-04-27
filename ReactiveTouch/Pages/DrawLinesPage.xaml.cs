using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using MR.Gestures;
using NControl.Abstractions;
using ReactiveTouch.Model;
using Xamarin.Forms;
using Button = Xamarin.Forms.Button;

namespace ReactiveTouch.Pages
{
	public partial class DrawLinesPage : Xamarin.Forms.ContentPage
	{
        private double width;
        private double height;

        private readonly MR.Gestures.RelativeLayout layer1;
	    private readonly MR.Gestures.RelativeLayout layer2;
	    //private readonly MR.Gestures.AbsoluteLayout layout;

	    public DrawLinesPage()
        {
            #region Setup

		    InitializeComponent();

            layer1 = new MR.Gestures.RelativeLayout();
            layer2 = new MR.Gestures.RelativeLayout();
            //layout = new MR.Gestures.AbsoluteLayout { BackgroundColor = Color.Gray };

            MainLayout.Children.Add(layer1);
            MainLayout.Children.Add(layer2);

            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer1, AbsoluteLayoutFlags.SizeProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer1, AbsoluteLayoutFlags.PositionProportional);
            //Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer1, AbsoluteLayoutFlags.All);
            Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(layer1, new Rectangle(0, 0, 1, 1));

            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer2, AbsoluteLayoutFlags.SizeProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer2, AbsoluteLayoutFlags.PositionProportional);
            //Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer2, AbsoluteLayoutFlags.All);
            Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(layer2, new Rectangle(0, 0, 1, 1));

            #region Clear

            var clearButton = new Button
            {
                Text = "Clear",
                TextColor = Color.White
            };
            clearButton.Clicked += (sender, args) =>
            {
                layer1.Children.Clear();
                layer2.Children.Clear();
            };

            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(clearButton, AbsoluteLayoutFlags.All);
            Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(clearButton, new Rectangle(0, 0, 1, 0.10));

            //MainLayout.Children.Add(clearButton);

            #endregion

            Content = MainLayout;

            #endregion

            #region RectiveX

            Func<EventPattern<DownUpEventArgs>, NGraphics.Point> getPosition = x =>
			{
				Debug.WriteLine("X " + x.EventArgs.Center.X + " Y " + x.EventArgs.Center.Y);
				return new NGraphics.Point(x.EventArgs.Center.X, x.EventArgs.Center.Y);
			};

			var points = Observable
				.FromEventPattern<DownUpEventArgs>(
					handler => MainLayout.Up += handler,
					handler => MainLayout.Up -= handler
					)
				.Select(getPosition);

			points
				.Subscribe(DrawCircle);

			points
				.Zip(points.Skip(1), Line.Create)
				.Subscribe(DrawLine);

            #endregion

            #region Orientation

		    width = Width;
		    height = Height;

		    SizeChanged += (sender, args) =>
		    {
		        if (Height > Width)
		        {
                    foreach (var line in layer1.Children)
                    {
                        ((NControlView)line).Invalidate();
                    }

                    foreach (var circle in layer2.Children)
                    {
                        ((NControlView)circle).Invalidate();
                    }
                }
		    };

		    #endregion

        }

        private void DrawCircle(NGraphics.Point p)
        {
            const double dim = 20;

            layer2.Children.Add(new CircleControl
                {
                    FillColor = Xamarin.Forms.Color.FromRgb(252, 37, 145),
                    HeightRequest = dim,
                    WidthRequest = dim
                },
                Constraint.RelativeToParent((parent) => p.X - dim * .5),
                Constraint.RelativeToParent((parent) => p.Y - dim * .5)
            );
        }

        private void DrawLine(Line line)
        {
            layer1.Children.Add(new LineControl(line.Start, line.End)
                {
                    Thickness = 4,
                    FillColor = Xamarin.Forms.Color.FromRgb(89, 89, 89),
                    HeightRequest = Height,
                    WidthRequest = Width
                },
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0)
            );
        }

        #region Test

        protected override void OnSizeAllocated(double w, double h)
        {
            base.OnSizeAllocated(w, h);

            if (width != w || height != h)
            {
                width = w;
                height = h;

                foreach (var line in layer1.Children)
                {
                    ((NControlView)line).Invalidate();
                }

                foreach (var circle in layer2.Children)
                {
                    ((NControlView)circle).Invalidate();
                }
            }
        }

        #endregion
	}
}
