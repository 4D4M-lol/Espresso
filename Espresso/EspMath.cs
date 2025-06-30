// Imports

using System;
using System.Numerics;

// Math Namespace

namespace Espresso.EspMath
{
    // Classes

    public class EsVector2<TNumber> where TNumber : INumber<TNumber>
    {
        // Statics
        
        public static EsVector2<TNumber> Zero { get; } = new(TNumber.Zero, TNumber.Zero);
        public static EsVector2<TNumber> Right { get; } = new(TNumber.One, TNumber.Zero);
        public static EsVector2<TNumber> Left { get; } = new(-TNumber.One, TNumber.Zero);
        public static EsVector2<TNumber> Up { get; } = new(TNumber.Zero, TNumber.One);
        public static EsVector2<TNumber> Down { get; } = new(TNumber.Zero, -TNumber.One);
        public static EsVector2<TNumber> One { get; } = new(TNumber.One, TNumber.One);
        
        // Properties and Fields

        private TNumber _x;
        private TNumber _y;

        public TNumber X { get => _x; set => _x = value; }
        
        public TNumber Y { get => _y; set => _y = value; }
        
        public double Magnitude
        {
            get
            {
                double dx = Convert.ToDouble(_x);
                double dy = Convert.ToDouble(_y);
                
                return Math.Sqrt(dx * dx + dy * dy);
            }
        }
        
        // Constructors, Methods, and Functions

        public EsVector2(TNumber? x = default, TNumber? y = default)
        {
            _x = x ?? TNumber.Zero;
            _y = y ?? TNumber.Zero;
        }

        public override string ToString()
        {
            return $"(EsVector2<{typeof(TNumber).Name}> X={_x} Y={_y})";
        }

        public static EsVector2<TNumber> operator +(EsVector2<TNumber> left, EsVector2<TNumber> right)
        {
            return new(left.X + right.X, left.Y + right.Y);
        }

        public static EsVector2<TNumber> operator +(EsVector2<TNumber> left, TNumber right)
        {
            return new(left.X + right, left.Y + right);
        }

        public static EsVector2<TNumber> operator -(EsVector2<TNumber> left, EsVector2<TNumber> right)
        {
            return new(left.X - right.X, left.Y - right.Y);
        }

        public static EsVector2<TNumber> operator -(EsVector2<TNumber> left, TNumber right)
        {
            return new(left.X - right, left.Y - right);
        }

        public static EsVector2<TNumber> operator *(EsVector2<TNumber> left, EsVector2<TNumber> right)
        {
            return new(left.X * right.X, left.Y * right.Y);
        }

        public static EsVector2<TNumber> operator *(EsVector2<TNumber> left, TNumber right)
        {
            return new(left.X * right, left.Y * right);
        }

        public static EsVector2<TNumber> operator /(EsVector2<TNumber> left, EsVector2<TNumber> right)
        {
            return new(left.X / right.X, left.Y / right.Y);
        }

        public static EsVector2<TNumber> operator /(EsVector2<TNumber> left, TNumber right)
        {
            return new(left.X / right, left.Y / right);
        }

        public static EsVector2<TNumber> operator %(EsVector2<TNumber> left, EsVector2<TNumber> right)
        {
            return new(left.X % right.X, left.Y % right.Y);
        }

        public static EsVector2<TNumber> operator %(EsVector2<TNumber> left, TNumber right)
        {
            return new(left.X % right, left.Y % right);
        }
    }

    public class EsVector3<TNumber> where TNumber : INumber<TNumber>
    {
        // Statics
        
        public static EsVector3<TNumber> Zero { get; } = new(TNumber.Zero, TNumber.Zero, TNumber.Zero);
        public static EsVector3<TNumber> Right { get; } = new(TNumber.One, TNumber.Zero, TNumber.Zero);
        public static EsVector3<TNumber> Left { get; } = new(-TNumber.One, TNumber.Zero, TNumber.Zero);
        public static EsVector3<TNumber> Up { get; } = new(TNumber.Zero, TNumber.One, TNumber.Zero);
        public static EsVector3<TNumber> Down { get; } = new(TNumber.Zero, -TNumber.One, TNumber.Zero);
        public static EsVector3<TNumber> Forward { get; } = new(TNumber.Zero, TNumber.Zero, TNumber.One);
        public static EsVector3<TNumber> Back { get; } = new(TNumber.Zero, TNumber.Zero, -TNumber.One);
        public static EsVector3<TNumber> One { get; } = new(TNumber.One, TNumber.One, TNumber.One);
        
        // Properties and Fields

        private TNumber _x;
        private TNumber _y;
        private TNumber _z;

        public TNumber X { get => _x; set => _x = value; }
        public TNumber Y { get => _y; set => _y = value; }
        public TNumber Z { get => _z; set => _z = value; }
        public double Magnitude
        {
            get
            {
                double dx = Convert.ToDouble(_x);
                double dy = Convert.ToDouble(_y);
                double dz = Convert.ToDouble(_z);
                
                return Math.Sqrt(dx * dx + dy * dy + dz * dz);
            }
        }
        
        // Constructors, Methods, and Functions

        public EsVector3(TNumber? x = default, TNumber? y = default, TNumber? z = default)
        {
            _x = x ?? TNumber.Zero;
            _y = y ?? TNumber.Zero;
            _z = z ?? TNumber.Zero;
        }

        public override string ToString()
        {
            return $"(EsVector3<{typeof(TNumber).Name}> X={_x} Y={_y} Z={_z})";
        }

        public static EsVector3<TNumber> operator +(EsVector3<TNumber> left, EsVector3<TNumber> right)
        {
            return new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static EsVector3<TNumber> operator +(EsVector3<TNumber> left, TNumber right)
        {
            return new(left.X + right, left.Y + right, left.Z + right);
        }

        public static EsVector3<TNumber> operator -(EsVector3<TNumber> left, EsVector3<TNumber> right)
        {
            return new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static EsVector3<TNumber> operator -(EsVector3<TNumber> left, TNumber right)
        {
            return new(left.X - right, left.Y - right, left.Z - right);
        }

        public static EsVector3<TNumber> operator *(EsVector3<TNumber> left, EsVector3<TNumber> right)
        {
            return new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        public static EsVector3<TNumber> operator *(EsVector3<TNumber> left, TNumber right)
        {
            return new(left.X * right, left.Y * right, left.Z * right);
        }

        public static EsVector3<TNumber> operator /(EsVector3<TNumber> left, EsVector3<TNumber> right)
        {
            return new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        public static EsVector3<TNumber> operator /(EsVector3<TNumber> left, TNumber right)
        {
            return new(left.X / right, left.Y / right, left.Z / right);
        }

        public static EsVector3<TNumber> operator %(EsVector3<TNumber> left, EsVector3<TNumber> right)
        {
            return new(left.X % right.X, left.Y % right.Y, left.Z % right.Z);
        }

        public static EsVector3<TNumber> operator %(EsVector3<TNumber> left, TNumber right)
        {
            return new(left.X % right, left.Y % right, left.Z % right);
        }
    }

    public class EsLayoutVector<TScale, TOffset> where TScale : IFloatingPoint<TScale> where TOffset : IBinaryInteger<TOffset>
    {
        // Properties and Fields

        private EsVector2<TScale> _scale;
        private EsVector2<TOffset> _offset;
        
        public EsVector2<TScale> Scale { get => _scale; set => _scale = value; }
        public EsVector2<TOffset> Offset { get => _offset; set => _offset = value; }
        
        // Constructors, Methods, and Functions

        public EsLayoutVector(EsVector2<TScale>? scale = null, EsVector2<TOffset>? offset = null)
        {
            _scale = scale ?? new();
            _offset = offset ?? new();
        }

        public EsLayoutVector(TScale scaleX, TScale scaleY, TOffset offsetX, TOffset offsetY)
        {
            _scale = new(scaleX, scaleY);
            _offset = new(offsetX, offsetY);
        }

        public EsLayoutVector(TScale scaleX, TScale scaleY)
        {
            _scale = new(scaleX, scaleY);
            _offset = new();
        }

        public EsLayoutVector(TOffset offsetX, TOffset offsetY)
        {
            _scale = new();
            _offset = new(offsetX, offsetY);
        }

        public override string ToString()
        {
            return $"(EsLayoutVector<{typeof(TScale).Name}, {typeof(TOffset).Name}> Scale=({_scale.X}%, {_scale.Y}%) Offset=({_offset.X}, {_offset.Y});";
        }

        public static EsLayoutVector<TScale, TOffset> operator +(EsLayoutVector<TScale, TOffset> left, EsLayoutVector<TScale, TOffset> right)
        {
            return new(left.Scale + right.Scale, left.Offset + right.Offset);
        }

        public static EsLayoutVector<TScale, TOffset> operator -(EsLayoutVector<TScale, TOffset> left, EsLayoutVector<TScale, TOffset> right)
        {
            return new(left.Scale - right.Scale, left.Offset - right.Offset);
        }

        public static EsLayoutVector<TScale, TOffset> operator *(EsLayoutVector<TScale, TOffset> left, EsLayoutVector<TScale, TOffset> right)
        {
            return new(left.Scale * right.Scale, left.Offset * right.Offset);
        }

        public static EsLayoutVector<TScale, TOffset> operator /(EsLayoutVector<TScale, TOffset> left, EsLayoutVector<TScale, TOffset> right)
        {
            return new(left.Scale / right.Scale, left.Offset / right.Offset);
        }

        public static EsLayoutVector<TScale, TOffset> operator %(EsLayoutVector<TScale, TOffset> left, EsLayoutVector<TScale, TOffset> right)
        {
            return new(left.Scale % right.Scale, left.Offset % right.Offset);
        }
    }
}