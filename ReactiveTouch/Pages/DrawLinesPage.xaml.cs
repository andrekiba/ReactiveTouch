using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using MR.Gestures;
using NControl.Abstractions;
using ReactiveTouch.Model;
using Xamarin.Forms;

namespace ReactiveTouch.Pages
{
	public partial class DrawLinesPage : Xamarin.Forms.ContentPage
	{
        private double width = 0;
        private double height = 0;

        private readonly MR.Gestures.RelativeLayout layer1;
	    private readonly MR.Gestures.RelativeLayout layer2;
	    private readonly MR.Gestures.AbsoluteLayout layout;

		public DrawLinesPage()
        {
            InitializeComponent();

			BackgroundColor = Xamarin.Forms.Color.Black;

			layer1 = new MR.Gestures.RelativeLayout();
			layer2 = new MR.Gestures.RelativeLayout();
			layout = new MR.Gestures.AbsoluteLayout { BackgroundColor = Xamarin.Forms.Color.Gray };

			layout.Children.Add(layer1);
			layout.Children.Add(layer2);

			Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer1, AbsoluteLayoutFlags.SizeProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer1, AbsoluteLayoutFlags.PositionProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(layer1, new Xamarin.Forms.Rectangle(0f, 0f, 1f, 1f));

            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer2, AbsoluteLayoutFlags.SizeProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutFlags(layer2, AbsoluteLayoutFlags.PositionProportional);
            Xamarin.Forms.AbsoluteLayout.SetLayoutBounds(layer2, new Xamarin.Forms.Rectangle(0f, 0f, 1f, 1f));

			Content = layout;

			#region RectiveX

			Func<EventPattern<DownUpEventArgs>, NGraphics.Point> GetPosition = x =>
			{
				Debug.WriteLine("X " + x.EventArgs.Center.X + " Y " + x.EventArgs.Center.Y);
				return new NGraphics.Point(x.EventArgs.Center.X, x.EventArgs.Center.Y);
			};

			var points = Observable
				.FromEventPattern<DownUpEventArgs>(
					handler => layout.Up += handler,
					handler => layout.Up -= handler
					)
				.Select(GetPosition);

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

        void DrawCircle(NGraphics.Point p)
		{
		    const double dim = 20;

		    layer2.Children.Add(new CircleControl { FillColor = Xamarin.Forms.Color.FromRgb(252, 37, 145),
													HeightRequest = dim,
													WidthRequest = dim },
			                 Constraint.RelativeToParent((parent) => p.X - dim * .5),
							 Constraint.RelativeToParent((parent) => p.Y - dim * .5)
							);
		}

	    void DrawLine(Line line)
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

    }
}
