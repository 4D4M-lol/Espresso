// File Namespace

using Espresso.EspMath;
using SDL3;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Espresso.EspFile
{
    // Interfaces

    public interface IEsFile
    {
        // Statics
        
        public static abstract List<string> Extensions { get; }
        
        // Properties
        
        public EsPath Path { get; }
    }
    
    // Classes

    public class EsPath
    {
        // Properties and Fields
        
        private readonly string _path;
        private readonly string _absolutePath;
        private readonly string _relativePath;
        private readonly string _extension;
        private readonly bool _exists;
        private readonly bool _isDirectory;
        private readonly bool _isFile;
        
        public string Path { get => _path; }
        
        public string AbsolutePath { get => _absolutePath; }
        
        public string RelativePath { get => _relativePath; }
        
        public string Extension { get => _extension; }
        
        public bool Exists { get => _exists; }
        
        public bool IsDirectory { get => _isDirectory; }
        
        public bool IsFile { get => _isFile; }
        
        // Constructors and Methods

        public EsPath(string path, [CallerFilePath] string callerPath = "")
        {
            _path = path;
            _absolutePath = System.IO.Path.GetFullPath(path);
            _relativePath = System.IO.Path.GetRelativePath(callerPath, path);
            _extension = System.IO.Path.GetExtension(_absolutePath);
            _exists = File.Exists(_absolutePath) || Directory.Exists(_absolutePath);
            _isDirectory = Directory.Exists(_absolutePath);
            _isFile = File.Exists(_absolutePath);
        }
    }

    public class EsImage : IEsFile
    {
        // Statics

        public static List<string> Extensions { get; } = new()
        {
            ".avif", ".bmp", ".cur", ".gif", ".ico", ".jpeg", ".jxl", ".lbm", ".pcx", ".png", ".pnm", ".qoi", ".svg", ".tga", ".tiff", ".webp", ".xcf", ".xmp"
        };
        
        // Properties and Fields
        
        private EsPath _path;
        private EsVector2<int> _size;
        private uint[] _pixels;

        public EsPath Path { get => _path; }
        
        public EsVector2<int> Size { get => _size; }
        
        public uint[] Pixels { get => _pixels; }
        
        // Constructors, Functions, and Methods

        public EsImage(EsPath path)
        {
            (EsVector2<int> size, uint[] pixels) = Load(path);
            _path = path;
            _size = size;
            _pixels = pixels;
        }

        public static (EsVector2<int> Size, uint[] Pixels) Load(EsPath path)
        {
            Func<(EsVector2<int>, uint[])> load = () =>
            {
                (EsVector2<int> s, uint[]? p) result = (new EsVector2<int>(0, 0), null);

                if (!path.Exists)
                {
                    throw new FileNotFoundException($"Path not found: \"{path.Path}\"");
                }

                if (!Extensions.Contains(path.Extension))
                {
                    throw new FileLoadException($"Invalid extension: \"{path.Extension}\".");
                }

                if (SDL.IsMainThread())
                {
                    IntPtr surfacePtr = Image.Load(path.AbsolutePath);

                    if (surfacePtr == IntPtr.Zero)
                    {
                        throw new FileLoadException($"Unable to load image \"{path.AbsolutePath}\": {SDL.GetError()}.");
                    }

                    try
                    {
                        SDL.Surface surface = Marshal.PtrToStructure<SDL.Surface>(surfacePtr);

                        if (!SDL.LockSurface(surfacePtr))
                        {
                            throw new FileLoadException(
                                $"Unable to lock surface \"{path.AbsolutePath}\": {SDL.GetError()}");
                        }

                        uint[] pixels = new uint[surface.Width * surface.Height];

                        try
                        {
                            SDL.PixelFormat format = surface.Format;
                            IntPtr pixelsPtr = surface.Pixels;
                            SDL.PixelFormatDetails details = new();
                            details.Format = format;
                            bool is32BitDirectAccess = format == SDL.PixelFormat.ARGB8888 ||
                                                       format == SDL.PixelFormat.RGBA8888 ||
                                                       format == SDL.PixelFormat.ABGR8888 ||
                                                       format == SDL.PixelFormat.BGRA8888;

                            if (is32BitDirectAccess)
                            {
                                int bytesPerPixel = 4;
                                byte[] pixelBytes = new byte[surface.Pitch * surface.Height];
                                Marshal.Copy(pixelsPtr, pixelBytes, 0, pixelBytes.Length);

                                for (int y = 0; y < surface.Height; y++)
                                {
                                    for (int x = 0; x < surface.Width; x++)
                                    {
                                        int byteOffset = (y * surface.Pitch) + (x * bytesPerPixel);
                                        uint pixelValue = BitConverter.ToUInt32(pixelBytes, byteOffset);
                                        byte r, g, b, a;

                                        SDL.GetRGBA(pixelValue, details, new SDL.Palette(), out r, out g, out b, out a);

                                        pixels[y * surface.Width + x] = (uint)((a << 24) | (r << 16) | (g << 8) | b);
                                    }
                                }
                            }
                            else
                            {
                                details.Format = surface.Format;
                                int bytesPerPixel = details.BytesPerPixel;

                                for (int y = 0; y < surface.Height; y++)
                                {
                                    for (int x = 0; x < surface.Width; x++)
                                    {
                                        IntPtr currentPixelPtr = IntPtr.Add(pixelsPtr,
                                            (y * surface.Pitch) + (x * bytesPerPixel));
                                        uint pixelValue = 0;

                                        switch (bytesPerPixel)
                                        {
                                            case 1: pixelValue = Marshal.ReadByte(currentPixelPtr); break;
                                            case 2: pixelValue = (uint)Marshal.ReadInt16(currentPixelPtr); break;
                                            case 3:
                                                byte b0 = Marshal.ReadByte(currentPixelPtr);
                                                byte b1 = Marshal.ReadByte(IntPtr.Add(currentPixelPtr, 1));
                                                byte b2 = Marshal.ReadByte(IntPtr.Add(currentPixelPtr, 2));
                                                pixelValue = (uint)(b0 | (b1 << 8) | (b2 << 16));

                                                break;
                                            case 4:
                                                pixelValue = (uint)Marshal.ReadInt32(currentPixelPtr);

                                                break;
                                            default:
                                                throw new FileLoadException(
                                                    $"Unsupported BytesPerPixel: {bytesPerPixel}.");
                                        }

                                        byte r, g, b, a;

                                        SDL.GetRGBA(pixelValue, details, new SDL.Palette(), out r, out g, out b, out a);

                                        pixels[y * surface.Width + x] = (uint)((a << 24) | (r << 16) | (g << 8) | b);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            SDL.UnlockSurface(surfacePtr);

                            result = (new(surface.Width, surface.Height), pixels);
                        }
                    }
                    finally
                    {
                        SDL.DestroySurface(surfacePtr);
                    }
                }

                return result;
            };
            (EsVector2<int>?, uint[]?) loaded = (null, null);

            if (SDL.IsMainThread())
            {
                loaded = load();
            }
            else
            {
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    loaded = load();
                }, IntPtr.Zero, false);
            }
            
            return loaded;
        }

        public override string ToString()
        {
            return $"(EsImage Path=\"{_path.AbsolutePath}\" Size=({_size.X}, {_size.Y}))";
        }
    }
}