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
        
        public EsVector2<float> Size { get; set; }
        public EsVector2<float> Position { get; set; }
        public float Rotation { get; set; }
        public IEsColor Fill { get; set; }
        public EsSignal<Action<IEsModifier>> OnModifierAdded { get; }
        public EsSignal<Action<IEsModifier>> OnModifierRemoved { get; }
        
        // Methods

        public List<IEsModifier> GetModifiers();
        public void AddModifier(IEsModifier modifier);
        public void RemoveModifier(IEsModifier modifier);
        public bool HasModifier(IEsModifier modifier);
        public List<EsVector2<float>> GetPoints();
        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines();
    }
    
    // Classes
    
    public class EsInterfaceBorder<TOpacity, TRadius, TWidth> : IEsModifier where TOpacity : IEnumerable<float> where TRadius : IEnumerable<float> where TWidth : IEnumerable<float>
    {
        // Properties and Fields

        private IEsInstance? _parent;
        private bool _active;
        private (IEsColor b, IEsColor l, IEsColor r, IEsColor t) _color;
        private TOpacity _opacity;
        private TRadius _radius;
        private TWidth _width;

        public IEsInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value) return;

                if (_parent.HasModifier(this))
                {
                    if (EsConfigs.Log) Console.WriteLine($"Espresso: Couldn't add EsInterfaceBorder to {_parent}, {_parent} already has EsInterfaceBorder.");
                    
                    return;
                }

                if (_parent != null) _parent.RemoveModifier(this);

                _parent = value;

                if (_parent != null) _parent.AddModifier(this);
            }
        }
        
        public bool Active { get => _active; set => _active = value; }

        public string ModifierName { get => "EsInterfaceBorder"; }

        public (IEsColor Bottom, IEsColor Left, IEsColor Right, IEsColor Top) Color { get => _color; set => _color = value; }
        
        public TOpacity Opacity { get => _opacity; set => _opacity = value; }
        
        public TRadius Radius { get => _radius; set => _radius = value; }
        
        public TWidth Width { get => _width; set => _width = value; }
        
        // Constructors and Methods
    
        public EsInterfaceBorder(IEsInterface? parent = null)
        {
            _parent = parent;
            _active = true;
            _color = (EsColor3.Black, EsColor3.Black, EsColor3.Black, EsColor3.Black);
            _opacity = new TOpacity() { 1f, 1f, 1f, 1f};
            _radius = (0f, 0f, 0f, 0f);
            _width = (5f, 5f, 5f, 5f);

            if (parent != null)
            {
                if (!parent.HasModifier(this)) parent.AddModifier(this);
                else Console.WriteLine($"Espresso: Couldn't add EsInterfaceBorder to {parent}, {parent} already has EsInterfaceBorder.");
            }
        }
    
        public override string ToString()
        {
            return $"(EsInterfaceBorder)";
        }
    }

    public class EsInterfaceCorner : IEsModifier
    {
        // Properties and Fields

        private IEsInstance? _parent;
        private bool _active;
        private (float b, float l, float r, float t) _radius;

        public IEsInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value) return;

                if (_parent.HasModifier(this))
                {
                    if (EsConfigs.Log) Console.WriteLine($"Espresso: Couldn't add EsInterfaceCorner to {_parent}, {_parent} already has EsInterfaceCorner.");
                    
                    return;
                }

                if (_parent != null) _parent.RemoveModifier(this);

                _parent = value;

                if (_parent != null) _parent.AddModifier(this);
            }
        }
        
        public bool Active { get => _active; set => _active = value; }

        public string ModifierName { get => "EsInterfaceCorner"; }
        
        public (float Bottom, float Left, float Right, float Top) Radius { get => _radius; set => _radius = value; }
        
        // Constructors and Methods
    
        public EsInterfaceCorner(IEsInterface? parent = null)
        {
            _parent = parent;
            _active = true;
            _radius = (0f, 0f, 0f, 0f);

            if (parent != null)
            {
                if (!parent.HasModifier(this)) parent.AddModifier(this);
                else Console.WriteLine($"Espresso: Couldn't add EsInterfaceCorner to {parent}, {parent} already has EsInterfaceCorner.");
            }
        }
    
        public override string ToString()
        {
            return $"(EsInterfaceCorner)";
        }
    }

    public class EsFrame : IEsInterface
    {
        // Properties and Fields

        private EsRectangle _rectangle;
        private IEsInstance? _parent;
        private List<IEsModifier> _modifiers;
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
        private EsSignal<Action<IEsModifier>> _onModifierAdded;
        private EsSignal<Action<IEsModifier>> _onModifierRemoved;
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
        
        public EsSignal<Action<IEsModifier>> OnModifierAdded { get => _onModifierAdded; }
        
        public EsSignal<Action<IEsModifier>> OnModifierRemoved { get => _onModifierRemoved; }
        
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

            if (parent != null)
            {
                _parent.AddChild(this);
            }
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

        public List<IEsModifier> GetModifiers()
        {
            return new(_modifiers);
        }

        public void AddModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            if (_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            if (modifier.Parent != this)
            {
                Type childType = modifier.GetType();
                
                childType.GetField("_parent")?.SetValue(modifier, this);
            }

            _modifiers.Add(modifier);
            _rectangle.AddModifier(modifier);
            _onModifierAdded.Emit(modifier);
        }

        public void RemoveModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            
            if (!_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            if (modifier.Parent == this)
            {
                Type childType = modifier.GetType();
                
                childType.GetField("_parent")?.SetValue(modifier, null);
            }

            _modifiers.Remove(modifier);
            _rectangle.RemoveModifier(modifier);
            _onModifierRemoved.Emit(modifier);
        }

        public bool HasModifier(IEsModifier modifier)
        {
            return _modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName);
        }

        public List<IEsInstance> GetChildren()
        {
            return new(_children);
        }
        
        public void AddChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));

            if (_children.Contains(child)) return;

            if (child.Parent != this)
            {
                Type childType = child.GetType();
                
                childType.GetField("_parent")?.SetValue(child, this);
            }

            _children.Add(child);
            _onChildAdded.Emit(child);
        }

        public void RemoveChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            if (!_children.Contains(child)) return;

            if (child.Parent == this)
            {
                Type childType = child.GetType();
                
                childType.GetField("_parent")?.SetValue(child, null);
            }

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
            EsVector2<float> parentPosition;

            if (_parent.GetType() == typeof(EsWindow))
            {
                EsVector2<int> windowSize = parentType.GetProperty("Size")?.GetValue(_parent) as EsVector2<int> ?? new();
                EsVector2<int> windowPosition = parentType.GetProperty("Position")?.GetValue(_parent) as EsVector2<int> ?? new();
                parentSize = new(windowSize.X, windowSize.Y);
                parentPosition = new(windowPosition.X, windowPosition.Y);
            }
            else
            {
                parentSize = parentType.GetProperty("AbsoluteSize")?.GetValue(_parent) as EsVector2<float> ?? new();
                parentPosition = parentType.GetProperty("AbsolutePosition")?.GetValue(_parent) as EsVector2<float> ?? new();
            }

            EsVector2<float> size = new(
                (parentSize.X * _size.Scale.X) + _size.Offset.X,
                (parentSize.Y * _size.Scale.Y) + _size.Offset.Y
            );
            EsVector2<float> position = new(
                (parentSize.X * _position.Scale.X) + _position.Offset.X,
                (parentSize.Y * _position.Scale.Y) + _position.Offset.Y
            );
            _absoluteSize = size;
            _absolutePosition = parentPosition + position;

            return new EsRectangle(size, position, _rotation, _backgroundColor);
        }

        public override string ToString()
        {
            return $"<EsFrame Name=\"{_name}\">";
        }
    }

    public class EsTriangle : IEsDrawing
    {
        // Properties and Fields
        
        private List<IEsModifier> _modifiers;
        private EsTriangleType _type;
        private EsVector2<float> _size;
        private EsVector2<float> _position;
        private float _rotation;
        private IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(EsVector2<float> s, EsVector2<float> e)> _lines;
        private EsSignal<Action<IEsModifier>> _onModifierAdded;
        private EsSignal<Action<IEsModifier>> _onModifierRemoved;

        public EsTriangleType Type
        {
            get => _type;
            set
            {
                _type = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(
                    value, _size, _position, _rotation
                );
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }

        public EsVector2<float> Size { 
            get => _size; 
            set
            {
                _size = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(
                    _type, value, _position, _rotation
                );
                _points = calculated.points;
                _lines = calculated.lines;
            }
            
        }

        public EsVector2<float> Position
        {
            get => _position;
            set
            {
                _position = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(
                    _type, _size, value, _rotation
                );
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }

        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(
                    _type, _size, _position, value
                );
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }
        
        public IEsColor Fill { get => _fill; set => _fill = value; }

        public EsSignal<Action<IEsModifier>> OnModifierAdded { get => _onModifierAdded; }

        public EsSignal<Action<IEsModifier>> OnModifierRemoved { get => _onModifierRemoved; }

        // Constructors and Methods

        public EsTriangle(EsTriangleType type = EsTriangleType.Acute, EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _type = type;
            _size = size ?? new(100, 100);
            _position = position ?? new();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(
                type, _size, _position, rotation
            );
            _points = calculated.points;
            _lines = calculated.lines;
        }

        public List<IEsModifier> GetModifiers()
        {
            return new(_modifiers);
        }

        public void AddModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            if (_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            _modifiers.Add(modifier);
            _onModifierAdded.Emit(modifier);
        }

        public void RemoveModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            
            if (!_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            _modifiers.Remove(modifier);
            _onModifierRemoved.Emit(modifier);
        }

        public bool HasModifier(IEsModifier modifier)
        {
            return _modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName);
        }

        public List<EsVector2<float>> GetPoints()
        {
            
            return new(_points);
        }

        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines()
        {
            return new(_lines);
        }

        public static (List<EsVector2<float>> Points, List<(EsVector2<float> Start, EsVector2<float> End)> Lines) Calculate(
            EsTriangleType type, EsVector2<float> size, EsVector2<float> position, float rotation
        )
        {
            List<EsVector2<float>> points = new();
            List<(EsVector2<float> start, EsVector2<float> end)> lines = new();
            
            if (type == EsTriangleType.Acute)
            {
                EsVector2<float> p1 = new(0, size.Y);
                EsVector2<float> p2 = new(size.X, size.Y);
                EsVector2<float> p3 = new(size.X * 0.25f, 0);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else if (type == EsTriangleType.Equilateral)
            {
                float side = size.X;
                float height = side * (float)Math.Sqrt(3) / 2f;
                EsVector2<float> p1 = new(0, height);
                EsVector2<float> p2 = new(side, height);
                EsVector2<float> p3 = new(side / 2f, 0);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else if (type == EsTriangleType.Isosceles)
            {
                EsVector2<float> p1 = new(0, size.Y);
                EsVector2<float> p2 = new(size.X, size.Y);
                EsVector2<float> p3 = new(size.X / 2f, 0);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else if (type == EsTriangleType.Obtuse)
            {
                EsVector2<float> p1 = new(0, size.Y);
                EsVector2<float> p2 = new(size.X, size.Y);
                EsVector2<float> p3 = new(size.X * 0.2f, 0);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else if (type == EsTriangleType.Right)
            {
                EsVector2<float> p1 = new(0, 0);
                EsVector2<float> p2 = new(size.X, 0);
                EsVector2<float> p3 = new(0, size.Y);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            else if (type == EsTriangleType.Scalene)
            {
                EsVector2<float> p1 = new(0, size.Y);
                EsVector2<float> p2 = new(size.X, size.Y * 0.8f);
                EsVector2<float> p3 = new(size.X * 0.1f, 0);

                points.Add(p1);
                points.Add(p2);
                points.Add(p3);
            }
            
            if (rotation != 0)
            {
                float cos = (float)Math.Cos(rotation * Math.PI / 180.0f);
                float sin = (float)Math.Sin(rotation * Math.PI / 180.0f);

                for (int i = 0; i < points.Count; i++)
                {
                    float x = points[i].X;
                    float y = points[i].Y;
                    points[i] = new(
                        x * cos - y * sin,
                        x * sin + y * cos
                    );
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new(
                    points[i].X + position.X,
                    points[i].Y + position.Y
                );
            }
            
            lines.Clear();
            lines.Add((points[0], points[1]));
            lines.Add((points[1], points[2]));
            lines.Add((points[2], points[0]));
            
            return (points, lines);
        }

        public override string ToString()
        {
            return $"(EsTriangle Type={_type} Size=({_size.X}, {_size.Y}) Position=({_position.X}, {_position.Y}) Rotation={_rotation}º)";
        }
    }

    public class EsRectangle : IEsDrawing
    {
        // Properties and Fields
        
        private List<IEsModifier> _modifiers;
        private EsVector2<float> _size;
        private EsVector2<float> _position;
        private float _rotation;
        private IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(EsVector2<float> s, EsVector2<float> e)> _lines;
        private EsSignal<Action<IEsModifier>> _onModifierAdded;
        private EsSignal<Action<IEsModifier>> _onModifierRemoved;

        public EsVector2<float> Size
        {
            get => _size;
            set
            {
                _size = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(value, _position, _rotation);
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }

        public EsVector2<float> Position
        {
            get => _position;
            set
            {
                _position = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(_size, value, _rotation);
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }

        public float Rotation
        {
            get => _rotation;
            
            set
            {
                _rotation = value;
                (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(_size, _position, value);
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }
        
        public IEsColor Fill { get => _fill; set => _fill = value; }

        public EsSignal<Action<IEsModifier>> OnModifierAdded { get => _onModifierAdded; }

        public EsSignal<Action<IEsModifier>> OnModifierRemoved { get => _onModifierRemoved; }
        
        // Constructors and Methods

        public EsRectangle(EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _size = size ?? new(100, 100);
            _position = position ?? new();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            (List<EsVector2<float>> points, List<(EsVector2<float> start, EsVector2<float> end)> lines) calculated = Calculate(_size, _position, _rotation);
            _points = calculated.points;
            _lines = calculated.lines;
        }

        public List<IEsModifier> GetModifiers()
        {
            return new(_modifiers);
        }

        public void AddModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));

            if (_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            _modifiers.Add(modifier);
            _onModifierAdded.Emit(modifier);
        }

        public void RemoveModifier(IEsModifier modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            
            if (!_modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName)) return;

            _modifiers.Remove(modifier);
            _onModifierRemoved.Emit(modifier);
        }

        public bool HasModifier(IEsModifier modifier)
        {
            return _modifiers.Select(m => m.ModifierName).Contains(modifier.ModifierName);
        }

        public List<EsVector2<float>> GetPoints()
        {
            return new(_points);
        }

        public List<(EsVector2<float> Start, EsVector2<float> End)> GetLines()
        {
            return new(_lines);
        }

        public static (List<EsVector2<float>> Points, List<(EsVector2<float> Start, EsVector2<float> End)> Lines) Calculate(
            EsVector2<float> size, EsVector2<float> position, float rotation
        )
        {
            List<EsVector2<float>> points = new();
            List<(EsVector2<float> start, EsVector2<float> end)> lines = new();
            EsVector2<float> p1 = new();
            EsVector2<float> p2 = new(size.X, 0);
            EsVector2<float> p3 = new(size.X, size.Y);
            EsVector2<float> p4 = new(0, size.Y);

            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);

            if (rotation != 0)
            {
                float angleRad = rotation * (float)Math.PI / 180.0f;
                float cos = (float)Math.Cos(angleRad);
                float sin = (float)Math.Sin(angleRad);

                for (int i = 0; i < points.Count; i++)
                {
                    float x = points[i].X;
                    float y = points[i].Y;
                    points[i] = new(
                        x * cos - y * sin,
                        x * sin + y * cos
                    );
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new(
                    points[i].X + position.X,
                    points[i].Y + position.Y
                );
            }
            
            lines.Clear();
            lines.Add((points[0], points[1]));
            lines.Add((points[1], points[2]));
            lines.Add((points[2], points[3]));
            lines.Add((points[3], points[0]));
            
            return (points, lines);
        }

        public override string ToString()
        {
            return $"(EsRectangle Size=({_size.X}, {_size.Y}) Position=({_position.X}, {_position.Y}) Rotation={_rotation}º)";
        }
    }
}