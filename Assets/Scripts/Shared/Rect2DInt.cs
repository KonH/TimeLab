using System;
using System.Globalization;
using UnityEngine;

namespace TimeLab.Shared {
	/// <summary>
	/// Rect2D analogue for integers
	/// </summary>
	public struct Rect2DInt : IEquatable<Rect2DInt>, IFormattable {
		int _xMin;
		int _yMin;
		int _width;
		int _height;

		public Rect2DInt(int x, int y, int width, int height) {
			_xMin   = x;
			_yMin   = y;
			_width  = width;
			_height = height;
		}

		public Rect2DInt(Vector2Int position, Vector2Int size) : this(position.x, position.y, size.x, size.y) {}

		public Rect2DInt(Rect2DInt source) : this(source._xMin, source._yMin, source._width, source._height) {}

		public static Rect2DInt Zero => new Rect2DInt(0, 0, 0, 0);

		public static Rect2DInt MinMaxRect(int xMin, int yMin, int xMax, int yMax) =>
			new Rect2DInt(xMin, yMin, xMax - xMin, yMax - yMin);

		public int X {
			get => _xMin;
			set => _xMin = value;
		}

		public int Y {
			get => _yMin;
			set => _yMin = value;
		}

		public Vector2Int Position {
			get => new Vector2Int(_xMin, _yMin);
			set {
				_xMin = value.x;
				_yMin = value.y;
			}
		}

		public Vector2 Center {
			get => new Vector2(X + _width / 2f, Y + _height / 2f);
			set {
				_xMin = (int) (value.x - _width / 2f);
				_yMin = (int) (value.y - _height / 2f);
			}
		}

		public Vector2Int Min {
			get => new Vector2Int(XMin, YMin);
			set {
				XMin = value.x;
				YMin = value.y;
			}
		}

		public Vector2Int Max {
			get => new Vector2Int(XMax, YMax);
			set {
				XMax = value.x;
				YMax = value.y;
			}
		}

		public int Width {
			get => _width;
			set => _width = value;
		}

		public int Height {
			get => _height;
			set => _height = value;
		}

		public Vector2Int Size {
			get => new Vector2Int(_width, _height);
			set {
				_width  = value.x;
				_height = value.y;
			}
		}

		public int XMin {
			get => _xMin;
			set {
				var xm = XMax;
				_xMin  = value;
				_width = xm - _xMin;
			}
		}

		public int YMin {
			get => _yMin;
			set {
				var ym = YMax;
				_yMin   = value;
				_height = ym - _yMin;
			}
		}

		public int XMax {
			get => _width + _xMin;
			set => _width = value - _xMin;
		}

		public int YMax {
			get => _height + _yMin;
			set => _height = value - _yMin;
		}

		public bool Contains(Vector2Int point) =>
			point.x >= XMin && point.x < XMax && point.y >= YMin && point.y < YMax;

		static Rect2DInt OrderMinMax(Rect2DInt rect) {
			if ( rect.XMin > rect.XMax ) {
				var xMin = rect.XMin;
				rect.XMin = rect.XMax;
				rect.XMax = xMin;
			}
			if ( rect.YMin > rect.YMax ) {
				var yMin = rect.YMin;
				rect.YMin = rect.YMax;
				rect.YMax = yMin;
			}
			return rect;
		}

		public bool Overlaps(Rect2DInt other) =>
			other.XMax > XMin && other.XMin < XMax && other.YMax > YMin && other.YMin < YMax;

		public bool Overlaps(Rect2DInt other, bool allowInverse) {
			var rect = this;
			if ( allowInverse ) {
				rect  = OrderMinMax(rect);
				other = OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		public static Vector2 NormalizedToPoint(Rect2DInt rect, Vector2 normalizedRect2DIntCoordinates) {
			return new Vector2(Mathf.Lerp(rect.X, rect.XMax, normalizedRect2DIntCoordinates.x), Mathf.Lerp(rect.Y, rect.YMax, normalizedRect2DIntCoordinates.y));
		}

		public static Vector2 PointToNormalized(Rect2DInt rect, Vector2 point) => new Vector2(Mathf.InverseLerp(rect.X, rect.XMax, point.x), Mathf.InverseLerp(rect.Y, rect.YMax, point.y));

		public static bool operator !=(Rect2DInt lhs, Rect2DInt rhs) => !(lhs == rhs);

		public static bool operator ==(Rect2DInt lhs, Rect2DInt rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Width == rhs.Width && lhs.Height == rhs.Height;

		public override int GetHashCode() {
			var num1     = X;
			var hashCode = num1.GetHashCode();
			num1 = Width;
			var num2 = num1.GetHashCode() << 2;
			var num3 = hashCode ^ num2;
			num1 = Y;
			var num4 = num1.GetHashCode() >> 2;
			var num5 = num3 ^ num4;
			num1 = Height;
			var num6 = num1.GetHashCode() >> 1;
			return num5 ^ num6;
		}

		public override bool Equals(object other) => other is Rect2DInt other1 && Equals(other1);

		public bool Equals(Rect2DInt other) {
			return
				(_xMin == other._xMin) &&
				(_yMin == other._yMin) &&
				(_width == other._width) &&
				(_height == other._height);
		}

		public override string ToString() => ToString(null, CultureInfo.InvariantCulture.NumberFormat);

		public string ToString(string format) => ToString(format, CultureInfo.InvariantCulture.NumberFormat);

		public string ToString(string format, IFormatProvider formatProvider) {
			if ( string.IsNullOrEmpty(format) )
				format = "F2";
			var objArray = new object[4];
			var num      = X;
			objArray[0] = num.ToString(format, formatProvider);
			num         = Y;
			objArray[1] = num.ToString(format, formatProvider);
			num         = Width;
			objArray[2] = num.ToString(format, formatProvider);
			num         = Height;
			objArray[3] = num.ToString(format, formatProvider);
			return string.Format("(x:{0}, y:{1}, width:{2}, height:{3})", objArray);
		}
	}
}