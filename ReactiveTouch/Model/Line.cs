using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGraphics;

namespace ReactiveTouch.Model
{
    public class Line
    {
		public Point Start { get; protected set; }

		public Point End { get; protected set; }

		Line(Point start, Point end)
		{
			Start = start;
			End = end;
		}

		public static Line Create(Point start, Point end)
		{
			return new Line(start, end);
		}
    }
}
