// Imports

using Espresso.EspFile;
using Espresso.EspInstance;
using Espresso.EspInterface;
using Espresso.EspMath;
using Espresso.EspScript;
using Espresso.EspStyling;
using SDL3;
using System.Runtime.InteropServices;

// Main Namespace

namespace Espresso
{
    // Configs

    public static class EsConfigs
    {
        public static string Version { get; } = "1.0.0";
        public static bool Log { get; set; } = false;
    }
    
    // Enums

    public enum EsWindowBorder
    {
        None,
        Fixed,
        Resizable
    }

    public enum EsWindowState
    {
        Normal,
        Minimized,
        Maximized,
        Fullscreen
    }

    public enum EsCloseOperation
    {
        None,
        Close,
        Confirm
    }
    
    // Classes

    public class EsWindow : IEsInstance
    {
        // Properties and Fields
        
        private IntPtr _window;
        private IntPtr _renderer;
        private SDL.FRect _surface;
        private SDL.EventFilter _eventFilter;
        private List<IEsInstance> _children;
        private List<string> _tags;
        private EsCloseOperation _closeOperation;
        private EsWindowState _state;
        private EsImage _icon;
        private string _name;
        private EsVector2<int> _size;
        private EsVector2<int> _position;
        private EsWindowBorder _border;
        private IEsColor _fill;
        private float _opacity;
        private bool _running;
        private EsSignal<Action> _onWindowRestored;
        private EsSignal<Action> _onWindowMinimized;
        private EsSignal<Action> _onWindowMaximized;
        private EsSignal<Action> _onWindowFullscreen;
        private EsSignal<Action<EsVector2<int>>> _onResize;
        private EsSignal<Action<EsVector2<int>>> _onMove;
        private EsSignal<Action<IEsInstance>> _onChildAdded;
        private EsSignal<Action<IEsInstance>> _onChildRemoved;
        
        public IEsInstance? Parent
        {
            get => null;
            set => throw new NotImplementedException("Application parent is not settable.");
        }

        public EsCloseOperation CloseOperation { get => _closeOperation; set => _closeOperation = value; }

        public EsWindowState State
        {
            get => _state;
            set
            {
                _state = value;

                if (SDL.IsMainThread())
                {
                    switch (value)
                    {
                        case EsWindowState.Normal: SDL.SetWindowFullscreen(_window, false); SDL.RestoreWindow(_window); _onWindowRestored.Emit(); break;
                        case EsWindowState.Minimized: SDL.SetWindowFullscreen(_window, false); SDL.MinimizeWindow(_window); _onWindowMinimized.Emit(); break;
                        case EsWindowState.Maximized: SDL.SetWindowFullscreen(_window, false); SDL.MaximizeWindow(_window); _onWindowMaximized.Emit(); break;
                        case EsWindowState.Fullscreen: SDL.SetWindowFullscreen(_window, true); _onWindowFullscreen.Emit(); break;
                    }
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        switch (value)
                        {
                            case EsWindowState.Normal: SDL.SetWindowFullscreen(_window, false); SDL.RestoreWindow(_window); _onWindowRestored.Emit(); break;
                            case EsWindowState.Minimized: SDL.SetWindowFullscreen(_window, false); SDL.MinimizeWindow(_window); _onWindowMinimized.Emit(); break;
                            case EsWindowState.Maximized: SDL.SetWindowFullscreen(_window, false); SDL.MaximizeWindow(_window); _onWindowMaximized.Emit(); break;
                            case EsWindowState.Fullscreen: SDL.SetWindowFullscreen(_window, true); _onWindowFullscreen.Emit(); break;
                        }
                    }, IntPtr.Zero, false);
                }
            }
        }

        public EsImage Icon
        {
            get => _icon;
            set
            {
                // TODO: Add icon setter.
            }
        }

        public string InstanceName { get; } = "EsWindow";

        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                if (SDL.IsMainThread())
                {
                    SDL.SetWindowTitle(_window, value);
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        SDL.SetWindowTitle(_window, value);
                    }, IntPtr.Zero, false);
                }
            }
        }

        public EsVector2<int> Size
        {
            get => _size;
            set
            {
                _size = value;

                if (SDL.IsMainThread())
                {
                    SDL.SetWindowSize(_window, _size.X, _size.Y);
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        SDL.SetWindowSize(_window, _size.X, _size.Y);
                    }, IntPtr.Zero, false);
                }
            }
        }
        
        public EsVector2<int> Position
        {
            get => _position;
            set
            {
                _position = value;

                if (SDL.IsMainThread())
                {
                    SDL.SetWindowPosition(_window, _position.X, _position.Y);
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        SDL.SetWindowPosition(_window, _position.X, _position.Y);
                    }, IntPtr.Zero, false);
                }
            }
        }

        public EsWindowBorder Border
        {
            get => _border;
            set
            {
                _border = value;

                if (SDL.IsMainThread())
                {
                    switch (value)
                    {
                        case EsWindowBorder.None: SDL.SetWindowBordered(_window, false); break;
                        case EsWindowBorder.Fixed: SDL.SetWindowBordered(_window, true); SDL.SetWindowResizable(_window, false); break;
                        case EsWindowBorder.Resizable: SDL.SetWindowBordered(_window, true); SDL.SetWindowResizable(_window, true); break;
                    }
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        switch (value)
                        {
                            case EsWindowBorder.None: SDL.SetWindowBordered(_window, false); break;
                            case EsWindowBorder.Fixed: SDL.SetWindowBordered(_window, true); SDL.SetWindowResizable(_window, false); break;
                            case EsWindowBorder.Resizable: SDL.SetWindowBordered(_window, true); SDL.SetWindowResizable(_window, true); break;
                        }
                    }, IntPtr.Zero, false);
                }
            }
        }

        public IEsColor Fill { get => _fill; set => _fill = value; }

        public float Opacity
        {
            get => _opacity;
            set
            {
                _opacity = float.Clamp(value, 0, 1);

                if (SDL.IsMainThread())
                {
                    SDL.SetWindowOpacity(_window, _opacity);
                }
                else
                {
                    SDL.RunOnMainThread((IntPtr _) =>
                    {
                        SDL.SetWindowOpacity(_window, _opacity);
                    }, IntPtr.Zero, false);
                }
            }
        }

        public bool Running { get => _running; }
        
        public EsSignal<Action> OnWindowRestored { get => _onWindowRestored; }
        
        public EsSignal<Action> OnWindowMinimized { get => _onWindowMinimized; }
        
        public EsSignal<Action> OnWindowMaximized { get => _onWindowMaximized; }
        
        public EsSignal<Action> OnWindowFullscreen { get => _onWindowFullscreen; }
        
        public EsSignal<Action<EsVector2<int>>> OnResize { get => _onResize; }
        
        public EsSignal<Action<EsVector2<int>>> OnMove { get => _onMove; }

        public EsSignal<Action<IEsInstance>> OnChildAdded { get => _onChildAdded; }

        public EsSignal<Action<IEsInstance>> OnChildRemoved { get => _onChildRemoved; }

        public EsWindow(string? name = null, EsVector2<int>? size = null, EsVector2<int>? position = null)
        {
            _children = new();
            _tags = new();
            _closeOperation = EsCloseOperation.Close;
            _icon = new EsImage(new("E:/JetBrains Rider/Espresso/Espresso/assets/icons/EspressoIcon32x32.png"));
            _name = name ?? $"Espresso v{EsConfigs.Version}";
            _size = size ?? new EsVector2<int>(800, 600);
            _position = position ?? new EsVector2<int>();
            _fill = new EsColor3(EsColors.White);
            _opacity = 1;
            _onWindowRestored = new();
            _onWindowMinimized = new();
            _onWindowMaximized = new();
            _onWindowFullscreen = new();
            _onResize = new();
            _onMove = new();
            _onChildAdded = new();
            _onChildRemoved = new();
            Action<IntPtr> create = (IntPtr _) =>
            {
                if (!SDL.Init(SDL.InitFlags.Video))
                {
                    throw new ExternalException($"Failed to initialize SDL video: {SDL.GetError()}.");
                }

                if (!SDL.CreateWindowAndRenderer(_name, _size.X, _size.Y, SDL.WindowFlags.Resizable | SDL.WindowFlags.Transparent, out _window, out _renderer))
                {
                    throw new ExternalException($"Failed to create SDL window and/or renderer: {SDL.GetError()}.");
                }

                SDL.SetWindowIcon(_window, Image.Load(_icon.Path.AbsolutePath));

                if (position != null)
                {
                    SDL.SetWindowPosition(_window, position.X, position.Y);
                }
                else
                {
                    SDL.SetWindowPosition(_window, (int)SDL.WindowPosCentered(), (int)SDL.WindowPosCentered());
                    SDL.GetWindowPosition(_window, out int x, out int y);
                    
                    _position = new EsVector2<int>(x, y);
                }
                
                _eventFilter = (IntPtr _, ref SDL.Event ev) =>
                {
                    if (ev.Window.WindowID == SDL.GetWindowID(_window))
                    {
                        if (ev.Type == (uint)SDL.EventType.WindowCloseRequested || ev.Type == (uint)SDL.EventType.Quit)
                        {
                            if (_closeOperation == EsCloseOperation.Close)
                            {
                                _running = false;
                            }
                            else if (_closeOperation == EsCloseOperation.Confirm)
                            {
                                // TODO: Add confirmation dialogs.
                                
                                _running = false;
                            }
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowExposed || ev.Type == (uint)SDL.EventType.WindowResized || ev.Type == (uint)SDL.EventType.WindowPixelSizeChanged)
                        {
                            Render();
                            SDL.GetWindowSize(_window, out int w, out int h);
                            _onResize.Emit(new EsVector2<int>(w, h));
                                
                            _size = new EsVector2<int>(w, h);
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowMoved)
                        {
                            Render();
                            SDL.GetWindowPosition(_window, out int x, out int y);
                            _onMove.Emit(new EsVector2<int>(x, y));
                                
                            _position = new EsVector2<int>(x, y);
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowRestored)
                        {
                            _state = EsWindowState.Normal;
                            
                            _onWindowRestored.Emit();
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowMinimized)
                        {
                            _state = EsWindowState.Normal;
                            
                            _onWindowMinimized.Emit();
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowMaximized)
                        {
                            _state = EsWindowState.Normal;
                            
                            _onWindowMaximized.Emit();
                        }
                        else if (ev.Type == (uint)SDL.EventType.WindowEnterFullscreen)
                        {
                            _state = EsWindowState.Normal;
                            
                            _onWindowFullscreen.Emit();
                        }
                    }
                        
                    return false;
                };

                if (!SDL.AddEventWatch(_eventFilter, IntPtr.Zero))
                {
                    throw new ExternalException($"Failed to add SDL event watch: {SDL.GetError()}.");
                }

                _surface = new();
                _surface.W = _size.X;
                _surface.H = _size.Y;
            };
            
            if (SDL.IsMainThread()) { create(IntPtr.Zero); } else { SDL.RunOnMainThread((IntPtr _) => { create(IntPtr.Zero); }, IntPtr.Zero, false); }
        }

        public void Center()
        {
            if (SDL.IsMainThread())
            {
                SDL.SetWindowPosition(_window, (int)SDL.WindowPosCentered(), (int)SDL.WindowPosCentered());
                SDL.GetWindowPosition(_window, out int x, out int y);
                
                _position = new EsVector2<int>(x, y);
            }
            else
            {
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowPosition(_window, (int)SDL.WindowPosCentered(), (int)SDL.WindowPosCentered());
                    SDL.GetWindowPosition(_window, out int x, out int y);
                
                    _position = new EsVector2<int>(x, y);
                }, IntPtr.Zero, false);
            }
        }

        public void Run()
        {
            _running = true;

            SDL.RunOnMainThread((IntPtr _) =>
            {
                while (_running)
                {
                    Render();
                    SDL.WaitEventTimeout(out SDL.Event _, 16);
                }
            }, IntPtr.Zero, true);
        }

        public void Stop()
        {
            _running = false;
        }
        
        public IEsInstance? Clone()
        {
            throw new NotImplementedException("Application instance is not cloneable.");
        }

        public void Destroy()
        {
            _running = false;
        }

        public List<IEsInstance> ChildrenSelector(string selector)
        {
            if (
                !selector.StartsWith("#") &&
                !(selector.StartsWith('\'') && selector.EndsWith('\'')) &&
                !(selector.StartsWith('\"') && selector.EndsWith('\"')) &&
                !(selector.StartsWith('<') && selector.EndsWith('>'))
            )
            {
                throw new ArgumentException($"Invalid selector: \"{selector}\".");
            }

            if (selector.StartsWith('#'))
            {
                string tag = selector.Substring(1);
                
                return new(_children.Where(child => child.HasTag(tag)));
            }

            string name = selector[1..^1];

            if (selector.StartsWith('\'') || selector.StartsWith('\"'))
            {
                return new(_children.Where(child => child.Name == name));
            }
                
            return new(_children.Where(child => child.InstanceName == name));
        }

        public List<IEsInstance> DescendantsSelector(string selector)
        {
            if (
                !selector.StartsWith("#") &&
                !(selector.StartsWith('\'') && selector.EndsWith('\'')) &&
                !(selector.StartsWith('\"') && selector.EndsWith('\"')) &&
                !(selector.StartsWith('<') && selector.EndsWith('>'))
            )
            {
                throw new ArgumentException($"Invalid selector: \"{selector}\".");
            }
            
            List<IEsInstance> descendants = GetDescendants();

            if (selector.StartsWith('#'))
            {
                string tag = selector.Substring(1);
                
                return new(descendants.Where(descendant => descendant.HasTag(tag)));
            }

            string name = selector[1..^1];

            if (selector.StartsWith('\'') || selector.StartsWith('\"'))
            {
                return new(descendants.Where(descendant => descendant.Name == name));
            }
            
            return new(descendants.Where(descendant => descendant.InstanceName == name));
        }

        public List<IEsInstance> GetChildren()
        {
            return new(_children);
        }
        
        public void AddChild(IEsInstance child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (_children.Contains(child)) return;

            _children.Add(child);
            _onChildAdded.Emit(child);
        }

        public void RemoveChild(IEsInstance child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }
            
            if (!_children.Contains(child)) return;

            _children.Remove(child);
            _onChildRemoved.Emit(child);
        }

        public bool HasChild(IEsInstance child)
        {
            return _children.Contains(child);
        }

        public List<IEsInstance> GetDescendants()
        {
            List<IEsInstance> descendants = new(_children);

            foreach (IEsInstance child in _children)
            {
                foreach (IEsInstance descendant in child.GetDescendants())
                {
                    descendants.Add(descendant);
                }
            }
            
            return descendants;
        }

        public List<string> GetTags()
        {
            return new(_tags);
        }

        public void AddTag(string tag)
        {
            if (_tags.Contains(tag)) return;
            
            _tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
        }

        public bool HasTag(string tag)
        {
            return _tags.Contains(tag);
        }

        public IEsDrawing? Render()
        {
            _surface.W = _size.X;
            _surface.H = _size.Y;
            
            SDL.SetRenderDrawColor(_renderer, 0, 0, 0, 0);
            SDL.SetRenderDrawBlendMode(_renderer, SDL.BlendMode.None);
            SDL.RenderClear(_renderer);
            SDL.SetRenderDrawBlendMode(_renderer, SDL.BlendMode.Blend);

            uint hex = _fill.Hex;
            byte a = _fill.HasAlpha ? (byte)((hex >> 24) & 0xFF) : (byte)255;
            byte r = (byte)((hex >> 16) & 0xFF);
            byte g = (byte)((hex >> 8) & 0xFF);
            byte b = (byte)(hex & 0xFF);
            
            SDL.SetRenderDrawColor(_renderer, r, g, b, a);
            SDL.RenderFillRect(_renderer, _surface);

            foreach (IEsInstance child in _children)
            {
                IEsDrawing? drawing = child.Render();
                
                if (drawing == null) continue;
                
                hex = drawing.Fill.Hex;
                a = drawing.Fill.HasAlpha ? (byte)((hex >> 24) & 0xFF) : (byte)255;
                r = (byte)((hex >> 16) & 0xFF);
                g  = (byte)((hex >> 8) & 0xFF);
                b = (byte)(hex & 0xFF);
                List<EsVector2<float>> points = drawing.GetPoints();
                List<SDL.FPoint> sdlPoints = new();
                
                SDL.SetRenderDrawColor(_renderer, r, g, b, a);

                foreach (EsVector2<float> point in points)
                {
                    SDL.FPoint sdlPoint = new();
                    sdlPoint.X = point.X;
                    sdlPoint.Y = point.Y;
                    
                    sdlPoints.Add(sdlPoint);
                }
                
                float minYFloat = sdlPoints.Min(p => p.Y);
                float maxYFloat = sdlPoints.Max(p => p.Y);
                int minY = (int)Math.Floor(minYFloat);
                int maxY = (int)Math.Ceiling(maxYFloat);

                for (int y = minY; y <= maxY; y++)
                {
                    List<float> intersections = new List<float>();

                    for (int i = 0; i < points.Count; i++)
                    {
                        SDL.FPoint p1 = sdlPoints[i];
                        SDL.FPoint p2 = sdlPoints[(i + 1) % sdlPoints.Count];
                        
                        if ((p1.Y <= y && p2.Y > y) || (p2.Y <= y && p1.Y > y))
                        {
                            if (p1.Y != p2.Y)
                            {
                                float intersectX = p1.X + (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y);
                                
                                intersections.Add(intersectX);
                            }
                        }
                    }

                    intersections.Sort();

                    for (int i = 0; i + 1 < intersections.Count; i += 2)
                    {
                        float x1f = intersections[i];
                        float x2f = intersections[i + 1];

                        if (x1f > x2f) (x1f, x2f) = (x2f, x1f);

                        int x1 = (int)Math.Round(x1f);
                        int x2 = (int)Math.Round(x2f);

                        SDL.FRect fillRect = new()
                        {
                            X = x1,
                            Y = y,
                            W = x2 - x1 + 1, 
                            H = 1
                        };

                        SDL.RenderFillRect(_renderer, fillRect);
                    }
                }
            }
            
            SDL.RenderPresent(_renderer);

            return null;
        }
    }
}