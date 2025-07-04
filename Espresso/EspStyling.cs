// Imports

using Espresso.EspInstance;
using Espresso.EspMath;

// Styling Namespace

namespace Espresso.EspStyling
{
    // Enums

    public enum EsGrayscaleMethod
    {
        Average,
        Lighten,
        Luminance
    }

    public enum EsBlendMethod
    {
        ColorBurn,
        ColorDodge,
        Darken,
        Difference,
        Exclusion,
        HardLight,
        Lighten,
        Multiply,
        Normal,
        Overlay,
        Screen,
        SoftLight
    }
    
    public enum EsColors
    {
        // A
        
        AliceBlue = 0xF0F8FF,
        AntiqueWhite = 0xFAEBD7,
        Aqua = 0x00FFFF,
        Aquamarine = 0x7FFFD4,
        Azure = 0xF0FFFF,
        
        // B
        
        Beige = 0xF5F5DC,
        Bisque = 0xFFE4C4,
        Black = 0x000000,
        BlanchedAlmond = 0xFFEBCD,
        Blue = 0x0000FF,
        BlueViolet = 0x8A2BE2,
        Brown = 0xA52A2A,
        BurlyWood = 0xDEB887,
        
        // C
        
        CadetBlue = 0x5F9EA0,
        Chartreuse = 0x7FFF00,
        Chocolate = 0xD2691E,
        Coral = 0xFF7F50,
        CornflowerBlue = 0x6495ED,
        Cornsilk = 0xFFF8DC,
        Crimson = 0xDC143C,
        Cyan = 0x00FFFF,
        
        // D

        DarkBlue = 0x00008B,
        DarkCyan = 0x008B8B,
        DarkGoldenRod = 0xB8860B,
        DarkGray = 0xA9A9A9,
        DarkGreen = 0x006400,
        DarkKhaki = 0xBDB76B,
        DarkMagenta = 0x8B008B,
        DarkOliveGreen = 0x556B2F,
        DarkOrange = 0xFF8C00,
        DarkOrchid = 0x9932CC,
        DarkRed = 0x8B0000,
        DarkSalmon = 0xE9967A,
        DarkSeaGreen = 0x8FBC8F,
        DarkSlateBlue = 0x483D8B,
        DarkSlateGray = 0x2F4F4F,
        DarkTurquoise = 0x00CED1,
        DarkViolet = 0x9400D3,
        DeepPink = 0xFF1493,
        DeepSkyBlue = 0x00BFFF,
        DimGray = 0x696969,
        DodgerBlue = 0x1E90FF,
        
        // F

        FireBrick = 0xB22222,
        FloralWhite = 0xFFFAF0,
        ForestGreen = 0x228B22,
        Fuchsia = 0xFF00FF,
        
        // G

        Gainsboro = 0xDCDCDC,
        GhostWhite = 0xF8F8FF,
        Gold = 0xFFD700,
        GoldenRod = 0xDAA520,
        Gray = 0x808080,
        Green = 0x008000,
        GreenYellow = 0xADFF2F,
        
        // H

        HoneyDew = 0xF0FFF0,
        HotPink = 0xFF69B4,
        
        // I

        IndianRed = 0xCD5C5C,
        Indigo = 0x4B0082,
        Ivory = 0xFFFFF0,
        
        // K

        Khaki = 0xF0E68C,
        
        // L

        Lavender = 0xE6E6FA,
        LavenderBlush = 0xFFF0F5,
        LawnGreen = 0x7CFC00,
        LemonChiffon = 0xFFFACD,
        LightBlue = 0xADD8E6,
        LightCoral = 0xF08080,
        LightCyan = 0xE0FFFF,
        LightGoldenRodYellow = 0xFAFAD2,
        LightGray = 0xD3D3D3,
        LightGreen = 0x90EE90,
        LightPink = 0xFFB6C1,
        LightSalmon = 0xFFA07A,
        LightSeaGreen = 0x20B2AA,
        LightSkyBlue = 0x87CEFA,
        LightSlateGray = 0x778899,
        LightSteelBlue = 0xB0C4DE,
        LightYellow = 0xFFFFE0,
        Lime = 0x00FF00,
        LimeGreen = 0x32CD32,
        Linen = 0xFAF0E6,
        
        // M

        Magenta = 0xFF00FF,
        Maroon = 0x800000,
        MediumAquaMarine = 0x66CDAA,
        MediumBlue = 0x0000CD,
        MediumOrchid = 0xBA55D3,
        MediumPurple = 0x9370DB,
        MediumSeaGreen = 0x3CB371,
        MediumSlateBlue = 0x7B68EE,
        MediumSpringGreen = 0x00FA9A,
        MediumTurquoise = 0x48D1CC,
        MediumVioletRed = 0xC71585,
        MidnightBlue = 0x191970,
        MintCream = 0xF5FFFA,
        MistyRose = 0xFFE4E1,
        Moccasin = 0xFFE4B5,
        
        // N

        NavajoWhite = 0xFFDEAD,
        Navy = 0x000080,
        
        // O

        OldLace = 0xFDF5E6,
        Olive = 0x808000,
        OliveDrab = 0x6B8E23,
        Orange = 0xFFA500,
        OrangeRed = 0xFF4500,
        Orchid = 0xDA70D6,
        
        // P

        PaleGoldenRod = 0xEEE8AA,
        PaleGreen = 0x98FB98,
        PaleTurquoise = 0xAFEEEE,
        PaleVioletRed = 0xDB7093,
        PapayaWhip = 0xFFEFD5,
        PeachPuff = 0xFFDAB9,
        Peru = 0xCD853F,
        Pink = 0xFFC0CB,
        Plum = 0xDDA0DD,
        PowderBlue = 0xB0E0E6,
        Purple = 0x800080,
        
        // R

        RebeccaPurple = 0x663399,
        Red = 0xFF0000,
        RosyBrown = 0xBC8F8F,
        RoyalBlue = 0x4169E1,
        
        // S

        SaddleBrown = 0x8B4513,
        Salmon = 0xFA8072,
        SandyBrown = 0xF4A460,
        SeaGreen = 0x2E8B57,
        SeaShell = 0xFFF5EE,
        Sienna = 0xA0522D,
        Silver = 0xC0C0C0,
        SkyBlue = 0x87CEEB,
        SlateBlue = 0x6A5ACD,
        SlateGray = 0x708090,
        Snow = 0xFFFAFA,
        SpringGreen = 0x00FF7F,
        SteelBlue = 0x4682B4,
        
        // T

        Tan = 0xD2B48C,
        Teal = 0x008080,
        Thistle = 0xD8BFD8,
        Tomato = 0xFF6347,
        Turquoise = 0x40E0D0,
        
        // V

        Violet = 0xEE82EE,
        
        // W

        Wheat = 0xF5DEB3,
        White = 0xFFFFFF,
        WhiteSmoke = 0xF5F5F5,
        
        // Y

        Yellow = 0xFFFF00,
        YellowGreen = 0x9ACD32
    }

    public enum EsColorShades
    {
        // Red Shades

        Red100 = 0xFFEBEE,
        Red200 = 0xFFCDD2,
        Red300 = 0xEF9A9A,
        Red400 = 0xE57373,
        Red500 = 0xF44336,
        Red600 = 0xE53935,
        Red700 = 0xD32F2F,
        Red800 = 0xC62828,
        Red900 = 0xB71C1C,

        // Orange Shades

        Orange100 = 0xFFE0B2,
        Orange200 = 0xFFCC80,
        Orange300 = 0xFFB347,
        Orange400 = 0xFFA726,
        Orange500 = 0xFF9800,
        Orange600 = 0xFB8C00,
        Orange700 = 0xF57C00,
        Orange800 = 0xEF6C00,
        Orange900 = 0xE65100,

        // Yellow Shades

        Yellow100 = 0xFFF9C4,
        Yellow200 = 0xFFF59D,
        Yellow300 = 0xFFF176,
        Yellow400 = 0xFFEE58,
        Yellow500 = 0xFFEB3B,
        Yellow600 = 0xFDD835,
        Yellow700 = 0xFBC02D,
        Yellow800 = 0xF9A825,
        Yellow900 = 0xF57F17,

        // Green Shades

        Green100 = 0xE8F5E9,
        Green200 = 0xC8E6C9,
        Green300 = 0xA5D6A7,
        Green400 = 0x81C784,
        Green500 = 0x66BB6A,
        Green600 = 0x558B2F,
        Green700 = 0x388E3C,
        Green800 = 0x33691E,
        Green900 = 0x1B5E20,

        // Cyan Shades

        Cyan100 = 0xE0F7FA,
        Cyan200 = 0xB2EBF2,
        Cyan300 = 0x80DEEA,
        Cyan400 = 0x4DD0E1,
        Cyan500 = 0x00BCD4,
        Cyan600 = 0x00ACC1,
        Cyan700 = 0x0097A7,
        Cyan800 = 0x00838F,
        Cyan900 = 0x006064,

        // Blue Shades

        Blue100 = 0xBBDEFB,
        Blue200 = 0x90CAF9,
        Blue300 = 0x64B5F6,
        Blue400 = 0x42A5F5,
        Blue500 = 0x2196F3,
        Blue600 = 0x1E88E5,
        Blue700 = 0x1976D2,
        Blue800 = 0x1565C0,
        Blue900 = 0x0D47A1,

        // Purple Shades

        Purple100 = 0xE1BEE7,
        Purple200 = 0xCE93D8,
        Purple300 = 0xBA68C8,
        Purple400 = 0xAB47BC,
        Purple500 = 0x9C27B0,
        Purple600 = 0x8E24AA,
        Purple700 = 0x7B1FA2,
        Purple800 = 0x6A1B9A,
        Purple900 = 0x4A148C,

        // Pink Shades

        Pink100 = 0xFCE4EC,
        Pink200 = 0xF8BBD0,
        Pink300 = 0xF48FB1,
        Pink400 = 0xF06292,
        Pink500 = 0xE91E63,
        Pink600 = 0xD81B60,
        Pink700 = 0xC2185B,
        Pink800 = 0xAD1457,

        // Gray Shades

        Gray100 = 0xF5F5F5,
        Gray200 = 0xEEEEEE,
        Gray300 = 0xE0E0E0,
        Gray400 = 0xBDBDBD,
        Gray500 = 0x9E9E9E,
        Gray600 = 0x757575,
        Gray700 = 0x616161,
        Gray800 = 0x424242,
        Gray900 = 0x212121,

        // White Shades

        White100 = 0xFFFFFF,
        White200 = 0xFAFAFA,
        White300 = 0xF5F5F5,

        // Black Shades

        Black100 = 0x1A1A1A,
        Black200 = 0x0D0D0D,
        Black300 = 0x000000
    }
    
    // Interfaces

    public interface IEsColor
    {
        // Properties
        
        public bool HasAlpha { get; }
        public uint Hex { get;  }
    }
    
    // Classes

    public class EsColor3 : IEsColor
    {
        // Statics
        
        public static EsColor3 Red { get; } = new(0xFF0000);
        public static EsColor3 Orange { get; } = new(0xFF7D00);
        public static EsColor3 Yellow { get; } = new(0xFFFF00);
        public static EsColor3 Green { get; } = new(0x00FF00);
        public static EsColor3 Cyan { get; } = new(0x00FFFF);
        public static EsColor3 Blue { get; } = new(0x0000FF);
        public static EsColor3 Purple { get; } = new(0x7D00FF);
        public static EsColor3 Pink { get; } = new(0xFF00FF);
        public static EsColor3 Gray { get; } = new(0x191919);
        public static EsColor3 White { get; } = new(0xFFFFFF);
        public static EsColor3 Black { get; } = new();
        
        // Properties and Fields
        
        private uint _hex;
        private (byte r, byte g, byte b) _rgb;
        private (ushort h, byte s, byte l) _hsl;

        public bool HasAlpha { get; } = false;

        public uint Hex { get => _hex; }

        public (byte Red, byte Green, byte Blue) Rgb { get => _rgb; }

        public (ushort Hue, byte Saturation, byte Lightness) Hsl { get => _hsl; }
        
        // Constructors and Methods

        public EsColor3()
        {
            _hex = 0;
            _rgb = (0, 0, 0);
            _hsl = (0, 0, 0);
        }

        public EsColor3(EsColors color)
        {
            _hex = (uint)color;
            _rgb = ((byte)((_hex >> 16) & 0xFF), (byte)((_hex >> 8) & 0xFF), (byte)(_hex & 0xFF));
            double r = (double)_rgb.r / 255;
            double b = (double)_rgb.g / 255;
            double g = (double)_rgb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _hsl = (
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor3(EsColorShades shade)
        {
            _hex = (uint)shade;
            _rgb = ((byte)((_hex >> 16) & 0xFF), (byte)((_hex >> 8) & 0xFF), (byte)(_hex & 0xFF));
            double r = (double)_rgb.r / 255;
            double b = (double)_rgb.g / 255;
            double g = (double)_rgb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _hsl = (
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor3(uint hex)
        {
            _hex = uint.Clamp(hex, 0, 0xFFFFFF);
            _rgb = ((byte)((hex >> 16) & 0xFF), (byte)((hex >> 8) & 0xFF), (byte)(hex & 0xFF));
            double r = (double)_rgb.r / 255;
            double b = (double)_rgb.g / 255;
            double g = (double)_rgb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _hsl = (
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor3(byte red, byte green, byte blue)
        {
            _hex = (uint)((red << 16) | (green << 8) | blue);
            _rgb = (red, green, blue);
            double r = (double)red / 255;
            double b = (double)green / 255;
            double g = (double)blue / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _hsl = (
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor3(ushort hue, byte saturation, byte lightness)
        {
            hue = ushort.Clamp(hue, 0, 360);
            saturation = byte.Clamp(saturation, 0, 100);
            lightness = byte.Clamp(lightness, 0, 100);
            double s = saturation / 100.0;
            double l = lightness / 100.0;
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((hue / 60.0) % 2 - 1));
            double m = l - c / 2;
            double r, g, b;
    
            if (hue < 60) { r = c; g = x; b = 0; }
            else if (hue < 120) { r = x; g = c; b = 0; }
            else if (hue < 180) { r = 0; g = c; b = x; }
            else if (hue < 240) { r = 0; g = x; b = c; }
            else if (hue < 300) { r = x; g = 0; b = c; }
            else { r = c; g = 0; b = x; }
    
            (byte red, byte green, byte blue) = (
                (byte)Math.Round((r + m) * 255),
                (byte)Math.Round((g + m) * 255),
                (byte)Math.Round((b + m) * 255)
            );
            _hex = (uint)((red << 16) | (green << 8) | blue);
            _rgb = (red, green, blue);
            _hsl = (hue, saturation, lightness);
        }

        public void Grayscale(EsGrayscaleMethod method = EsGrayscaleMethod.Luminance)
        {
            (byte r, byte g, byte b) = _rgb;
            byte gray = 0;
            
            switch (method)
            {
                case EsGrayscaleMethod.Average: gray = (byte)((r + g + b) / 3.0); break;
                case EsGrayscaleMethod.Lighten: gray = (byte)((byte.Max(byte.Max(r, g), g) + byte.Min(byte.Min(r, g), g)) / 2); break;
                case EsGrayscaleMethod.Luminance: gray = (byte)(0.2126 * r + 0.7152 * g + 0.0722 * b); break;
            }
            
            (r, g, b) = (gray, gray, gray);
            _hex = (uint)((r << 16) | (g << 8) | b);
            _rgb = (r, g, b);
        }

        public void Blend(EsColor3 other, EsBlendMethod method = EsBlendMethod.Normal)
        {
            byte FloatToByte(float value) => (byte)(Clamp(value) * 255.0f);
            float Clamp(float value) => Math.Clamp(value, 0f, 1f);
            float BlendComponent(byte backdrop, byte source, EsBlendMethod blendMethod)
            {
                float b = backdrop / 255.0f;
                float s = source / 255.0f;
                float result = 0;

                switch (blendMethod)
                {
                    case EsBlendMethod.ColorBurn: result = s == 0 ? 0 : Math.Max(0, 1 - (1 - b) / s); break;
                    case EsBlendMethod.ColorDodge: result = s == 1 ? 1 : Math.Min(1, b / (1 - s)); break;
                    case EsBlendMethod.Darken: result = Math.Min(b, s);  break;
                    case EsBlendMethod.Difference:  result = Math.Abs(b - s); break;
                    case EsBlendMethod.Exclusion: result = b + s - 2 * b * s; break;
                    case EsBlendMethod.HardLight: result = s < 0.5f ? (2 * b * s) : (1 - 2 * (1 - b) * (1 - s)); break;
                    case EsBlendMethod.Lighten: result = Math.Max(b, s); break;
                    case EsBlendMethod.Multiply: result = b * s; break;
                    case EsBlendMethod.Normal: result = s; break;
                    case EsBlendMethod.Overlay: result = b < 0.5f ? (2 * b * s) : (1 - 2 * (1 - b) * (1 - s)); break;
                    case EsBlendMethod.Screen: result = 1 - (1 - b) * (1 - s); break;
                    case EsBlendMethod.SoftLight: result = s < 0.5f ? (b - (1 - 2 * s) * b * (1 - b)) : (b + (2 * s - 1) * (Clamp(b) - b)); break;
                }
                
                return Clamp(result);
            }
            
            byte r = FloatToByte(BlendComponent(_rgb.r, other.Rgb.Red, method));
            byte g = FloatToByte(BlendComponent(_rgb.g, other.Rgb.Green, method));
            byte b = FloatToByte(BlendComponent(_rgb.b, other.Rgb.Blue, method));

            _hex = (uint)((r << 16) | (g << 8) | b);
            _rgb = (r, g, b);
        }

        public override string ToString()
        {
            return $"(EsColor3 Hex=0x{_hex:x8} Rgb=({String.Join(", ", _rgb)})";
        }
    }

    public class EsColor4 : IEsColor
    {
        // Statics
        
        public static EsColor4 Red { get; } = new(0xFFFF0000);
        public static EsColor4 Orange { get; } = new(0xFFFF7D00);
        public static EsColor4 Yellow { get; } = new(0xFFFFFF00);
        public static EsColor4 Green { get; } = new(0xFF00FF00);
        public static EsColor4 Cyan { get; } = new(0xFF00FFFF);
        public static EsColor3 Blue { get; } = new(0xFF0000FF);
        public static EsColor4 Purple { get; } = new(0xFF7D00FF);
        public static EsColor4 Pink { get; } = new(0xFFFF00FF);
        public static EsColor4 Gray { get; } = new(0xFF191919);
        public static EsColor4 White { get; } = new(0xFFFFFFFF);
        public static EsColor4 Black { get; } = new(0xFF000000);
        public static EsColor4 Transparent { get; } = new();
        
        // Properties and Fields
        
        private uint _hex;
        private (byte a, byte r, byte g, byte b) _argb;
        private (byte a, ushort h, byte s, byte l) _ahsl;

        public bool HasAlpha { get; } = true;

        public uint Hex { get => _hex; }

        public (byte Alpha, byte Red, byte Green, byte Blue) Argb { get => _argb; }

        public (byte Alpha, ushort Hue, byte Saturation, byte Lightness) Ahsl { get => _ahsl; }
        
        // Constructors and Methods

        public EsColor4()
        {
            _hex = 0;
            _argb = (0, 0, 0, 0);
            _ahsl = (0, 0, 0, 0);
        }

        public EsColor4(EsColors color)
        {
            _hex = (uint)color;
            _argb = ((byte)((_hex >> 24) & 0xFF), (byte)((_hex >> 16) & 0xFF), (byte)((_hex >> 8) & 0xFF), (byte)(_hex & 0xFF));
            byte a = (byte)(((double)_argb.a / 255) * 100);
            double r = (double)_argb.r / 255;
            double b = (double)_argb.g / 255;
            double g = (double)_argb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _ahsl = (
                a,
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor4(EsColorShades shade)
        {
            _hex = (uint)shade;
            _argb = ((byte)((_hex >> 24) & 0xFF), (byte)((_hex >> 16) & 0xFF), (byte)((_hex >> 8) & 0xFF), (byte)(_hex & 0xFF));
            byte a = (byte)(((double)_argb.a / 255) * 100);
            double r = (double)_argb.r / 255;
            double b = (double)_argb.g / 255;
            double g = (double)_argb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _ahsl = (
                a,
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor4(uint hex)
        {
            _hex = uint.Clamp(hex, 0, 0xFFFFFF);
            _argb = ((byte)((hex >> 24) & 0xFF), (byte)((hex >> 16) & 0xFF), (byte)((hex >> 8) & 0xFF), (byte)(hex & 0xFF));
            byte a = (byte)(((double)_argb.a / 255) * 100);
            double r = (double)_argb.r / 255;
            double b = (double)_argb.g / 255;
            double g = (double)_argb.b / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _ahsl = (
                a,
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor4(byte alpha, byte red, byte green, byte blue)
        {
            _hex = (uint)((alpha << 24) | (red << 16) | (green << 8) | blue);
            _argb = (alpha, red, green, blue);
            byte a = (byte)(((double)alpha / 255) * 100);
            double r = (double)red / 255;
            double b = (double)green / 255;
            double g = (double)blue / 255;
            double min = Math.Min(r, Math.Min(b, g));
            double max = Math.Max(r, Math.Max(b, g));
            double delta = max - min;
            (double hue, double saturation, double lightness) = (0d, 0d, (max + min) / 2);
            
            if (delta > 0)
            {
                saturation = lightness < 0.5 ? delta / (max + min) : delta / (2.0 - max - min);

                if (Math.Abs(max - r) < double.Epsilon) hue = (g - b) / delta;
                else if (Math.Abs(max - g) < double.Epsilon)hue = 2.0 + (b - r) / delta;
                else hue = 4.0 + (r - g) / delta;

                hue *= 60;
                
                if (hue < 0) hue += 360;
            }
            
            _ahsl = (
                a,
                (ushort)Math.Round(360 - hue),
                (byte)Math.Round(saturation * 100),
                (byte)Math.Round(lightness * 100)
            );
        }

        public EsColor4(byte alpha, ushort hue, byte saturation, byte lightness)
        {
            alpha = byte.Clamp(alpha, 0, 100);
            hue = ushort.Clamp(hue, 0, 360);
            saturation = byte.Clamp(saturation, 0, 100);
            lightness = byte.Clamp(lightness, 0, 100);
            double s = saturation / 100.0;
            double l = lightness / 100.0;
            double c = (1 - Math.Abs(2 * l - 1)) * s;
            double x = c * (1 - Math.Abs((hue / 60.0) % 2 - 1));
            double m = l - c / 2;
            double r, g, b;
    
            if (hue < 60) { r = c; g = x; b = 0; }
            else if (hue < 120) { r = x; g = c; b = 0; }
            else if (hue < 180) { r = 0; g = c; b = x; }
            else if (hue < 240) { r = 0; g = x; b = c; }
            else if (hue < 300) { r = x; g = 0; b = c; }
            else { r = c; g = 0; b = x; }
    
            (byte alp, byte red, byte green, byte blue) = (
                (byte)Math.Round((alpha / 100d) * 255d),
                (byte)Math.Round((r + m) * 255),
                (byte)Math.Round((g + m) * 255),
                (byte)Math.Round((b + m) * 255)
            );
            _hex = (uint)((alp << 24) | (red << 16) | (green << 8) | blue);
            _argb = (alp, red, green, blue);
            _ahsl = (alpha, hue, saturation, lightness);
        }

        public void Grayscale(EsGrayscaleMethod method = EsGrayscaleMethod.Average)
        {
            (byte a, byte r, byte g, byte b) = _argb;
            byte gray = 0;
            
            switch (method)
            {
                case EsGrayscaleMethod.Average: gray = (byte)((r + g + b) / 3.0); break;
                case EsGrayscaleMethod.Lighten: gray = (byte)((byte.Max(byte.Max(r, g), g) + byte.Min(byte.Min(r, g), g)) / 2); break;
                case EsGrayscaleMethod.Luminance: gray = (byte)(0.2126 * r + 0.7152 * g + 0.0722 * b); break;
            }
            
            (r, g, b) = (gray, gray, gray);
            _hex = (uint)((a << 24) | (r << 16) | (g << 8) | b);
            _argb = (a, r, g, b);
        }

        public void Blend(EsColor4 other, EsBlendMethod method = EsBlendMethod.Normal)
        {
            float BlendComponent(float backdrop, float source, EsBlendMethod blendMethod)
            {
                float result = 0;

                switch (blendMethod)
                {
                    case EsBlendMethod.ColorBurn: result = source == 0 ? 0 : 1 - (1 - backdrop) / source; break;
                    case EsBlendMethod.ColorDodge: result = source == 1 ? 1 : backdrop / (1 - source); break;
                    case EsBlendMethod.Darken: result = Math.Min(backdrop, source); break;
                    case EsBlendMethod.Difference: result = Math.Abs(backdrop - source); break;
                    case EsBlendMethod.Exclusion: result = backdrop + source - 2 * backdrop * source; break;
                    case EsBlendMethod.HardLight: result = source < 0.5f ? (2 * backdrop * source) : (1 - 2 * (1 - backdrop) * (1 - source)); break;
                    case EsBlendMethod.Lighten: result = Math.Max(backdrop, source); break;
                    case EsBlendMethod.Multiply: result = backdrop * source; break;
                    case EsBlendMethod.Normal: result = source; break;
                    case EsBlendMethod.Overlay: result = backdrop < 0.5f ? (2 * backdrop * source) : (1 - 2 * (1 - backdrop) * (1 - source)); break;
                    case EsBlendMethod.Screen: result = 1 - (1 - backdrop) * (1 - source); break;
                    case EsBlendMethod.SoftLight: result = source < 0.5f ? (backdrop - (1 - 2 * source) * backdrop * (1 - backdrop)) : (backdrop + (2 * source - 1) * (4 * backdrop * (1 - backdrop) - backdrop)); break;
                }
                
                return result;
            }

            float ba = _argb.a / 255.0f;
            float br = _argb.r / 255.0f;
            float bg = _argb.g / 255.0f;
            float bb = _argb.b / 255.0f;
            float sa = other.Argb.Alpha / 255.0f;
            float sr = other.Argb.Red / 255.0f;
            float sg = other.Argb.Green / 255.0f;
            float sb = other.Argb.Blue / 255.0f;
            float blendedr = BlendComponent(br, sr, method);
            float blendedg = BlendComponent(bg, sg, method);
            float blendedb = BlendComponent(bb, sb, method);
            float newa = sa + ba * (1 - sa);
            
            if (newa < 1e-6) newa = 0;

            float finalr = (blendedr * sa + br * ba * (1 - sa)) / newa;
            float finalg = (blendedg * sa + bg * ba * (1 - sa)) / newa;
            float finalb = (blendedb * sa + bb * ba * (1 - sa)) / newa;
            byte a = (byte)(Math.Clamp(newa, 0f, 1f) * 255.0f);
            byte r = (byte)(Math.Clamp(finalr, 0f, 1f) * 255.0f);
            byte g = (byte)(Math.Clamp(finalg, 0f, 1f) * 255.0f);
            byte b = (byte)(Math.Clamp(finalb, 0f, 1f) * 255.0f);
            _hex = (uint)((a << 24) | (r << 16) | (g << 8) | b);
            _argb = (a, r, g, b);
        }

        public override string ToString()
        {
            return $"(EsColor3 Hex=0x{_hex:x8} Argb=({String.Join(", ", _argb)})";
        }
    }

    public class EsSizeConstraint : IEsModifier
    {
        private IEsInstance? _parent;
        private EsVector2<float> _minimumSize;
        private EsVector2<float> _maximumSize;
        private bool _active;

        public IEsInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value) return;
            
                if (_parent?.HasModifier("EsSizeConstraint") ?? false)
                {
                    if (EsConfigs.Log) Console.WriteLine($"Can not add EsSizeConstraint to {_parent}, {_parent} already has EsSizeConstraint."); return;
                }
            
                _parent?.RemoveModifier("EsSizeConstraint");
            
                _parent = value;
            
                if (_parent != null) _parent.AddModifier(this);
            }
        }

        public string ModifierName { get; } = "EsSizeConstraint";

        public EsVector2<float> MinimumSize { get => _minimumSize; set => _minimumSize = value; }

        public EsVector2<float> MaximumSize { get => _maximumSize; set => _maximumSize = value; }

        public bool Active { get => _active; set => _active = value; }
        
        // Constructors

        public EsSizeConstraint(IEsInstance? parent = null)
        {
            _parent = parent;
            _minimumSize = new EsVector2<float>(Single.NegativeInfinity, Single.NegativeInfinity);
            _maximumSize = new EsVector2<float>(Single.PositiveInfinity, Single.PositiveInfinity);
            _active = true;

            if (_parent != null)
            {
                if (!_parent.HasModifier("EsSizeConstraint")) _parent.AddModifier(this);
                else _parent = null;
            }
        }
    }
}