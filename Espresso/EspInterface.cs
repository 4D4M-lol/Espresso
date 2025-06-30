// Imports

using Espresso.EspInstance;
using Espresso.EspMath;
using Espresso.EspScript;
using Espresso.EspStyling;
using System.Text.Json;

// Interface Namespace

namespace Espresso.EspInterface
{
    // Enums

    public enum EsAutomaticSize
    {
        None,
        Horizontal,
        Vertical,
        Both
    }

    public enum EsTriangleType
    {
        Acute,
        Equilateral,
        Isosceles,
        Obtuse,
        Right,
        Scalene
    }

    public enum EsArcType
    {
        Chord,
        Pie
    }
    
    // Interfaces

    public interface IEsInterface : IEsInstance
    {
        // Properties
        
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public EsAutomaticSize AutoSize { get; set; }
        public EsVector2<float> AbsoluteSize { get; }
        public EsVector2<float> AbsolutePosition { get; }
        public EsLayoutVector<float, int> Size { get; set; }
        public EsLayoutVector<float, int> Position { get; set; }
        public float Rotation { get; set; }
        public float Opacity { get; set; }
        public IEsColor BackgroundColor { get; set; }
        public bool Clip { get; set; }
    }

    public interface IEsDrawing
    {
        // Properties
        
        public EsVector2<float> Size { get; }
        public EsVector2<float> Position { get; }
        public float Rotation { get; }
        public IEsColor Fill { get; }
        
        // Methods

        public List<EsVector2<float>> GetPoints();
        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines();
    }

    public interface IEsModifier
    {
        
    }
    
    // Classes

    public class EsFrame : IEsInterface
    {
        // Properties and Fields

        private EsRectangle _rectangle;
        private IEsInstance? _parent;
        private List<IEsInstance> _children;
        private List<string> _tags;
        private string _name;
        private bool _active;
        private bool _visible;
        private EsAutomaticSize _autoSize;
        private EsVector2<float> _absoluteSize;
        private EsVector2<float> _absolutePosition;
        private EsLayoutVector<float, int> _size;
        private EsLayoutVector<float, int> _position;
        private float _rotation;
        private float _opacity;
        private IEsColor _backgroundColor;
        private bool _clip;
        private EsSignal<Action<IEsInstance>> _onChildAdded;
        private EsSignal<Action<IEsInstance>> _onChildRemoved;

        public IEsInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value) return;

                if (_parent != null) _parent.RemoveChild(this);

                _parent = value;

                if (_parent != null) _parent.AddChild(this);
            }
        }
        
        public string InstanceName { get => "EsFrame"; }
        
        public string Name { get => _name; set => _name = value; }
        
        public bool Active { get => _active; set => _active = value; }
        
        public bool Visible { get => _visible; set => _visible = value; }
        
        public EsAutomaticSize AutoSize { get => _autoSize; set => _autoSize = value; }
        
        public EsVector2<float> AbsoluteSize { get => _absoluteSize; }
        
        public EsVector2<float> AbsolutePosition { get => _absolutePosition; }
        
        public EsLayoutVector<float, int> Size { get => _size; set => _size = value; }
        
        public EsLayoutVector<float, int> Position { get => _position; set => _position = value; }
        
        public float Rotation { get => _rotation; set => _rotation = float.Clamp(value, -360, 360); }
        
        public float Opacity { get => _opacity; set => _opacity = value; }
        
        public IEsColor BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }
        
        public bool Clip { get => _clip; set => _clip = value; }
        
        public EsSignal<Action<IEsInstance>> OnChildAdded { get => _onChildAdded; set => _onChildAdded = value; }
        
        public EsSignal<Action<IEsInstance>> OnChildRemoved { get => _onChildRemoved; set => _onChildRemoved = value; }
        
        // Constructors and Methods

        public EsFrame(IEsInstance? parent = null, string name = "EsFrame")
        {
            _rectangle = new EsRectangle(new(200, 200));
            _parent = parent;
            _children = new();
            _tags = new();
            _name = name;
            _active = true;
            _visible = true;
            _autoSize = EsAutomaticSize.None;
            _size = new(200, 200);
            _position = new();
            _rotation = 0;
            _opacity = 1;
            _backgroundColor = new EsColor3(255, 255, 255);
            _clip = false;
            _onChildAdded = new();
            _onChildRemoved = new();
        }

        public IEsInstance? Clone()
        {
            string serialized = JsonSerializer.Serialize(this);
            
            return JsonSerializer.Deserialize<EsFrame>(serialized);
        }

        public void Destroy()
        {
            // TODO: Add destroy method to EsFrame.
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
            if (_parent == null || (_parent.GetType() != typeof(EsWindow) && !typeof(IEsInterface).IsAssignableFrom(_parent.GetType()))) return null;

            Type parentType = _parent.GetType();
            EsVector2<float> parentSize;

            if (_parent.GetType() == typeof(EsWindow))
            {
                EsVector2<int> windowSize = parentType.GetProperty("Size")?.GetValue(_parent) as EsVector2<int> ?? new();
                parentSize = new(windowSize.X, windowSize.Y);
            }
            else
            {
                parentSize = parentType.GetProperty("AbsoluteSize")?.GetValue(_parent) as EsVector2<float> ?? new();
            }

            EsVector2<float> calculatedSize = new(
                (parentSize.X * _size.Scale.X) + _size.Offset.X,
                (parentSize.Y * _size.Scale.Y) + _size.Offset.Y
            );
            EsVector2<float> topLeftPosition = new(
                (parentSize.X * _position.Scale.X) + _position.Offset.X,
                (parentSize.Y * _position.Scale.Y) + _position.Offset.Y
            );
            EsVector2<float> centerPosition = new(
                topLeftPosition.X + (calculatedSize.X / 2f),
                topLeftPosition.Y + (calculatedSize.Y / 2f)
            );

            return new EsRectangle(calculatedSize, centerPosition, _rotation, _backgroundColor);
        }

        public override string ToString()
        {
            return $"<EsFrame Name=\"{_name}\">";
        }
    }

    public class EsTriangle : IEsDrawing
    {
        // Properties and Fields
        
        private readonly EsTriangleType _type;
        private readonly EsVector2<float> _size;
        private readonly EsVector2<float> _position;
        private readonly float _rotation;
        private readonly IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(EsVector2<float> s, EsVector2<float> e)> _lines;

        public EsTriangleType Type { get => _type; }

        public EsVector2<float> Size { get => _size; }

        public EsVector2<float> Position { get => _position; }

        public float Rotation { get => _rotation; }
        
        public IEsColor Fill { get => _fill; }
        
        // Constructors and Methods

        public EsTriangle(EsTriangleType type = EsTriangleType.Acute, EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _type = type;
            _size = size ?? new EsVector2<float>(100, 100);
            _position = position ?? new EsVector2<float>();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            _points = new();
            _lines = new();

            if (type == EsTriangleType.Acute)
            {
                float halfWidth = _size.X / 2f;
                float halfHeight = _size.Y / 2f;
                EsVector2<float> p1 = new EsVector2<float>(-halfWidth, -halfHeight);
                EsVector2<float> p2 = new EsVector2<float>(halfWidth, -halfHeight);
                EsVector2<float> p3 = new EsVector2<float>(halfWidth * 0.25f, halfHeight);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            else if (type == EsTriangleType.Equilateral)
            {
                float side = _size.X;
                float halfSide = side / 2f;
                float height = side * (float)Math.Sqrt(3) / 2f;
                float halfHeight = height / 2f;
                EsVector2<float> p1 = new EsVector2<float>(-halfSide, -halfHeight);
                EsVector2<float> p2 = new EsVector2<float>(halfSide, -halfHeight);
                EsVector2<float> p3 = new EsVector2<float>(0, halfHeight);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            else if (type == EsTriangleType.Isosceles)
            {
                float halfWidth = _size.X / 2f;
                float height = _size.Y;
                EsVector2<float> p1 = new EsVector2<float>(-halfWidth, -height / 2f);
                EsVector2<float> p2 = new EsVector2<float>(halfWidth, -height / 2f);
                EsVector2<float> p3 = new EsVector2<float>(0, height / 2f);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            else if (type == EsTriangleType.Obtuse)
            {
                float halfWidth = _size.X / 2f;
                float height = _size.Y;
                EsVector2<float> p1 = new EsVector2<float>(-halfWidth, -height / 2f);
                EsVector2<float> p2 = new EsVector2<float>(halfWidth, -height / 2f);
                EsVector2<float> p3 = new EsVector2<float>(-halfWidth * 0.8f, height / 2f);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            else if (type == EsTriangleType.Right)
            {
                float width = _size.X;
                float height = _size.Y;
                EsVector2<float> p1 = new EsVector2<float>(-width / 2f, -height / 2f);
                EsVector2<float> p2 = new EsVector2<float>(width / 2f, -height / 2f);
                EsVector2<float> p3 = new EsVector2<float>(-width / 2f, height / 2f);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            else
            {
                float halfWidth = _size.X / 2f;
                float halfHeight = _size.Y / 2f;
                EsVector2<float> p1 = new EsVector2<float>(-halfWidth, -halfHeight);
                EsVector2<float> p2 = new EsVector2<float>(halfWidth, -halfHeight * 0.8f);
                EsVector2<float> p3 = new EsVector2<float>(halfWidth * 0.1f, halfHeight);

                _points.Add(p1);
                _points.Add(p2);
                _points.Add(p3);
            }
            
            if (_rotation != 0)
            {
                float cos = (float)Math.Cos(_rotation * Math.PI / 180.0f);
                float sin = (float)Math.Sin(_rotation * Math.PI / 180.0f);

                for (int i = 0; i < _points.Count; i++)
                {
                    float x = _points[i].X;
                    float y = _points[i].Y;
                    _points[i] = new EsVector2<float>(
                        x * cos - y * sin,
                        x * sin + y * cos
                    );
                }
            }

            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new EsVector2<float>(
                    _points[i].X + _position.X,
                    _points[i].Y + _position.Y
                );
            }
            
            _lines.Clear();
            _lines.Add((_points[0], _points[1]));
            _lines.Add((_points[1], _points[2]));
            _lines.Add((_points[2], _points[0]));
        }

        public List<EsVector2<float>> GetPoints()
        {
            
            return new(_points);
        }

        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines()
        {
            return new(_lines);
        }

        public override string ToString()
        {
            return $"(EsTriangle Type={_type} Size=({_size.X}, {_size.Y}) Position=({_position.X}, {_position.Y}) Rotation={_rotation}º)";
        }
    }

    public class EsRectangle : IEsDrawing
    {
        // Properties and Fields
        
        private EsVector2<float> _size;
        private EsVector2<float> _position;
        private float _rotation;
        private IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(EsVector2<float> s, EsVector2<float> e)> _lines;

        public EsVector2<float> Size { get => _size; }

        public EsVector2<float> Position { get => _position; }

        public float Rotation { get => _rotation; }
        
        public IEsColor Fill { get => _fill; }
        
        // Constructors and Methods

        public EsRectangle(EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _size = size ?? new(100, 100);
            _position = position ?? new();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            _points = new();
            _lines = new();
            float halfWidth = _size.X / 2f;
            float halfHeight = _size.Y / 2f;
            EsVector2<float> p1 = new EsVector2<float>(-halfWidth, halfHeight);
            EsVector2<float> p2 = new EsVector2<float>(halfWidth, halfHeight);
            EsVector2<float> p3 = new EsVector2<float>(halfWidth, -halfHeight);
            EsVector2<float> p4 = new EsVector2<float>(-halfWidth, -halfHeight);

            _points.Add(p1);
            _points.Add(p2);
            _points.Add(p3);
            _points.Add(p4);

            if (_rotation != 0)
            {
                float angleRad = _rotation * (float)Math.PI / 180.0f;
                float cos = (float)Math.Cos(angleRad);
                float sin = (float)Math.Sin(angleRad);

                for (int i = 0; i < _points.Count; i++)
                {
                    float x = _points[i].X;
                    float y = _points[i].Y;
                    _points[i] = new EsVector2<float>(
                        x * cos - y * sin,
                        x * sin + y * cos
                    );
                }
            }

            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new EsVector2<float>(
                    _points[i].X + _position.X,
                    _points[i].Y + _position.Y
                );
            }
            
            _lines.Clear();
            _lines.Add((_points[0], _points[1]));
            _lines.Add((_points[1], _points[2]));
            _lines.Add((_points[2], _points[3]));
            _lines.Add((_points[3], _points[0]));
        }

        public List<EsVector2<float>> GetPoints()
        {
            return new(_points);
        }

        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines()
        {
            return new(_lines);
        }

        public override string ToString()
        {
            return $"(EsRectangle Size=({_size.X}, {_size.Y}) Position=({_position.X}, {_position.Y}) Rotation={_rotation}º)";
        }
    }

    public class EsBorderStyling : IEsModifier
    {
        // Properties and Fields

        private (IEsColor b, IEsColor l, IEsColor r, IEsColor t) _color;
        private (float b, float l, float r, float t) _opacity;
        private (float b, float l, float r, float t) _radius;
        private (float b, float l, float r, float t) _width;
        
        public (IEsColor Bottom, IEsColor Left, IEsColor Right, IEsColor Top) Color { get => _color; set => _color = value; }
        
        public (float Bottom, float Left, float Right, float Top) Opacity { get => _opacity; set => _opacity = value; }
        
        public (float Bottom, float Left, float Right, float Top) Radius { get => _radius; set => _radius = value; }
        
        public (float Bottom, float Left, float Right, float Top) Width { get => _width; set => _width = value; }
        
        // Constructors and Methods

        public EsBorderStyling()
        {
            _color = (EsColor3.Black, EsColor3.Black, EsColor3.Black, EsColor3.Black);
            _opacity = (1f, 1f, 1f, 1f);
            _radius = (0f, 0f, 0f, 0f);
            _width = (5f, 5f, 5f, 5f);
        }

        public override string ToString()
        {
            return $"(EsBorderStyling)";
        }
    }
}