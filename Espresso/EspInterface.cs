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

    public enum EsAutomaticSizeRule
    {
        None,
        Horizontal,
        Vertical,
        Both
    }

    public enum EsScaleRule
    {
        None,
        Fit,
        Stretch
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
        public EsAutomaticSizeRule AutoSizeRule { get; set; }
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
        
        // Methods

        public List<EsVector2<float>> GetPoints();
        public List<(int Start, int End)> GetLines();
    }

    public class EsFrame : IEsInterface
    {
        // Properties and Fields

        private EsRectangle _rectangle;
        private IEsInstance? _parent;
        private List<string> _modifierNames;
        private List<IEsModifier> _modifiers;
        private List<IEsInstance> _children;
        private List<string> _tags;
        private string _name;
        private bool _active;
        private bool _visible;
        private EsAutomaticSizeRule _autoSizeRule;
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
        
                _parent?.RemoveChild(this);
        
                _parent = value;
        
                if (_parent != null) _parent.AddChild(this);
            }
        }
        
        public string InstanceName { get => "EsFrame"; }
        
        public string Name { get => _name; set => _name = value; }
        
        public bool Active { get => _active; set => _active = value; }
        
        public bool Visible { get => _visible; set => _visible = value; }
        
        public EsAutomaticSizeRule AutoSizeRule { get => _autoSizeRule; set => _autoSizeRule = value; }
        
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
            _modifierNames = new();
            _modifiers = new();
            _children = new();
            _tags = new();
            _name = name;
            _active = true;
            _visible = true;
            _autoSizeRule = EsAutomaticSizeRule.None;
            _size = new(200, 200);
            _position = new();
            _rotation = 0;
            _opacity = 1;
            _backgroundColor = new EsColor3(255, 255, 255);
            _clip = false;
            _onModifierAdded = new();
            _onModifierRemoved = new();
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

            if (_modifierNames.Contains(modifier.ModifierName))
            {
                if (EsConfigs.Log) Console.WriteLine($"Can not add {modifier.ModifierName} to {this}, {this} already has {modifier.ModifierName}.");
            }

            modifier.Parent = this;
            
            _modifierNames.Add(modifier.ModifierName);
            _modifiers.Add(modifier);
            _onModifierAdded.Emit(modifier);
        }

        public void RemoveModifier(string modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            
            if (!_modifierNames.Contains(modifier)) return;

            IEsModifier mod = _modifiers[_modifierNames.IndexOf(modifier)];

            if (mod.Parent == this) mod.Parent = null;
            
            _modifierNames.Remove(modifier);
            _modifiers.Remove(mod);
            _onModifierRemoved.Emit(mod);
        }

        public bool HasModifier(string modifier)
        {
            return _modifierNames.Contains(modifier);
        }

        public List<IEsInstance> GetChildren()
        {
            return new(_children);
        }
        
        public void AddChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            if (child == this) throw new ArgumentException("Cannot add self as child.");

            if (_children.Contains(child)) return;

            child.Parent = this;
            
            _children.Add(child);
            _onChildAdded.Emit(child);
        }

        public void RemoveChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            if (child == this) throw new ArgumentException("Cannot remove self as child.");
            
            if (!_children.Contains(child)) return;

            if (child.Parent == this) child.Parent = null;
            
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

        public EsDrawInfo? Render()
        {
            if (_parent == null || (_parent.GetType() != typeof(EsWindow) && !typeof(IEsInterface).IsAssignableFrom(_parent.GetType()))) return null;

            Type parentType = _parent.GetType();
            EsVector2<float> parentSize;
            EsVector2<float> parentPosition;
            bool parentIsWindow = false;

            if (_parent.GetType() == typeof(EsWindow))
            {
                EsVector2<int> windowSize = parentType.GetProperty("Size")?.GetValue(_parent) as EsVector2<int> ?? new();
                EsVector2<int> windowPosition = parentType.GetProperty("Position")?.GetValue(_parent) as EsVector2<int> ?? new();
                parentSize = new(windowSize.X, windowSize.Y);
                parentPosition = new(windowPosition.X, windowPosition.Y);
                parentIsWindow = true;
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

            if (_modifierNames.Contains("EsSizeConstraint"))
            {
                EsSizeConstraint? sizeConstraint = null;

                foreach (IEsModifier modifier in _modifiers)
                {
                    if (modifier.ModifierName == "EsSizeConstraint") sizeConstraint = (EsSizeConstraint)modifier; break;
                }
                
                size = new(
                    float.Clamp(size.X, sizeConstraint?.MinimumSize.X ?? Single.NegativeInfinity, sizeConstraint?.MaximumSize.X ?? Single.PositiveInfinity),
                    float.Clamp(size.Y, sizeConstraint?.MinimumSize.Y ?? Single.NegativeInfinity, sizeConstraint?.MaximumSize.Y ?? Single.PositiveInfinity)
                );
            }

            if (!parentIsWindow) position += parentPosition;
            
            _absoluteSize = size;
            _absolutePosition = position;
            (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = EsRectangle.Calculate(size, position, _rotation);
            EsDrawInfo drawInfo = new();
            EsShapeInfo shapeInfo = new() { Points = new(), Lines = new(), Fill = _backgroundColor };

            foreach (EsVector2<float> point in calculated.points) shapeInfo.Points.Add(new() { Position = new(point.X, point.Y) });

            foreach ((int start, int end) line in calculated.lines) shapeInfo.Lines.Add(new() { Start = line.start, End = line.end, Fill = _backgroundColor });

            drawInfo.Shapes.Add(shapeInfo);
            
            return drawInfo;
        }

        public override string ToString()
        {
            return $"<EsFrame Name=\"{_name}\">";
        }
    }

    public class EsCanvas : IEsInterface
    {
        // Properties and Fields

        private EsRectangle _rectangle;
        private IEsInstance? _parent;
        private List<string> _modifierNames;
        private List<IEsModifier> _modifiers;
        private List<IEsInstance> _children;
        private List<EsDrawInfo> _drawings;
        private List<string> _tags;
        private string _name;
        private bool _active;
        private bool _visible;
        private EsAutomaticSizeRule _autoSizeRule;
        private EsScaleRule _scaleRule;
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
        
                _parent?.RemoveChild(this);
        
                _parent = value;
        
                if (_parent != null) _parent.AddChild(this);
            }
        }
        
        public string InstanceName { get => "EsFrame"; }
        
        public string Name { get => _name; set => _name = value; }
        
        public bool Active { get => _active; set => _active = value; }
        
        public bool Visible { get => _visible; set => _visible = value; }
        
        public EsAutomaticSizeRule AutoSizeRule { get => _autoSizeRule; set => _autoSizeRule = value; }
        
        public EsScaleRule ScaleRule { get => _scaleRule; set => _scaleRule = value; }
        
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

        public EsCanvas(IEsInstance? parent = null, string name = "EsCanvas")
        {
            _rectangle = new EsRectangle(new(200, 200));
            _parent = parent;
            _modifierNames = new();
            _modifiers = new();
            _children = new();
            _drawings = new();
            _tags = new();
            _name = name;
            _active = true;
            _visible = true;
            _autoSizeRule = EsAutomaticSizeRule.None;
            _scaleRule = EsScaleRule.None;
            _size = new(200, 200);
            _position = new();
            _rotation = 0;
            _opacity = 1;
            _backgroundColor = new EsColor3(255, 255, 255);
            _clip = false;
            _onModifierAdded = new();
            _onModifierRemoved = new();
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
            // TODO: Add destroy method to EsCanvas.
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

            if (_modifierNames.Contains(modifier.ModifierName))
            {
                if (EsConfigs.Log) Console.WriteLine($"Can not add {modifier.ModifierName} to {this}, {this} already has {modifier.ModifierName}.");
            }

            modifier.Parent = this;
            
            _modifierNames.Add(modifier.ModifierName);
            _modifiers.Add(modifier);
            _onModifierAdded.Emit(modifier);
        }

        public void RemoveModifier(string modifier)
        {
            if (modifier == null) throw new ArgumentNullException(nameof(modifier));
            
            if (!_modifierNames.Contains(modifier)) return;

            IEsModifier mod = _modifiers[_modifierNames.IndexOf(modifier)];

            if (mod.Parent == this) mod.Parent = null;
            
            _modifierNames.Remove(modifier);
            _modifiers.Remove(mod);
            _onModifierRemoved.Emit(mod);
        }

        public bool HasModifier(string modifier)
        {
            return _modifierNames.Contains(modifier);
        }

        public List<IEsInstance> GetChildren()
        {
            return new(_children);
        }
        
        public void AddChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            if (child == this) throw new ArgumentException("Cannot add self as child.");

            if (_children.Contains(child)) return;

            child.Parent = this;
            
            _children.Add(child);
            _onChildAdded.Emit(child);
        }

        public void RemoveChild(IEsInstance child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            
            if (child == this) throw new ArgumentException("Cannot remove self as child.");
            
            if (!_children.Contains(child)) return;

            if (child.Parent == this) child.Parent = null;
            
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

        public List<EsDrawInfo> GetDrawings()
        {
            return new(_drawings);
        }

        public EsDrawInfo Draw(EsDrawInfo drawing)
        {
            _drawings.Add(drawing);
            
            return drawing;
        }

        public EsDrawInfo Draw(IEsDrawing drawing)
        {
            EsDrawInfo drawInfo = new();
            List<EsPointInfo> points = new();
            List<EsLineInfo> lines = new();

            foreach (EsVector2<float> point in drawing.GetPoints()) points.Add(new() { Position = new(point.X, point.Y, 0) });

            foreach ((int start, int end) line in drawing.GetLines()) lines.Add(new() { Start = line.start, End = line.end });
            
            drawInfo.Shapes.Add(new()
            {
                Points = points,
                Lines = lines,
                Fill = drawing.Fill,
            });
            
            _drawings.Add(drawInfo);
            
            return drawInfo;
        }

        public void Erase(EsDrawInfo drawing)
        {
            if (!_drawings.Contains(drawing)) return;
            
            _drawings.RemoveAll(draw => draw == drawing);
        }

        public void Clear()
        {
            _drawings.Clear();
        }

        public bool HasDrawings(EsDrawInfo drawing)
        {
            return _drawings.Contains(drawing);
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

        public EsDrawInfo? Render()
        {
            if (_parent == null || (_parent.GetType() != typeof(EsWindow) && !typeof(IEsInterface).IsAssignableFrom(_parent.GetType()))) return null;

            Type parentType = _parent.GetType();
            EsVector2<float> parentSize;
            EsVector2<float> parentPosition;
            bool parentIsWindow = false;

            if (_parent.GetType() == typeof(EsWindow))
            {
                EsVector2<int> windowSize = parentType.GetProperty("Size")?.GetValue(_parent) as EsVector2<int> ?? new();
                EsVector2<int> windowPosition = parentType.GetProperty("Position")?.GetValue(_parent) as EsVector2<int> ?? new();
                parentSize = new(windowSize.X, windowSize.Y);
                parentPosition = new(windowPosition.X, windowPosition.Y);
                parentIsWindow = true;
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

            if (_modifierNames.Contains("EsSizeConstraint"))
            {
                EsSizeConstraint? sizeConstraint = _modifiers[_modifierNames.IndexOf("EsSizeConstraint")] as EsSizeConstraint;
                
                size = new(
                    float.Clamp(size.X, sizeConstraint?.MinimumSize.X ?? float.NegativeInfinity, sizeConstraint?.MaximumSize.X ?? float.PositiveInfinity),
                    float.Clamp(size.Y, sizeConstraint?.MinimumSize.Y ?? float.NegativeInfinity, sizeConstraint?.MaximumSize.Y ?? float.PositiveInfinity)
                );
            }

            if (!parentIsWindow) position += parentPosition;

            _absoluteSize = size;
            _absolutePosition = position;

            (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = EsRectangle.Calculate(size, position, _rotation);
            EsDrawInfo drawInfo = new();
            EsShapeInfo shapeInfo = new() { Points = new(), Lines = new(), Fill = _backgroundColor };

            foreach (EsVector2<float> point in calculated.points) shapeInfo.Points.Add(new() { Position = new(point.X, point.Y) });

            foreach ((int start, int end) line in calculated.lines) shapeInfo.Lines.Add(new() { Start = line.start, End = line.end, Fill = _backgroundColor });

            drawInfo.Shapes.Add(shapeInfo);

            foreach (EsDrawInfo drawing in _drawings)
            {
                float minX = float.MaxValue, minY = float.MaxValue;
                float maxX = float.MinValue, maxY = float.MinValue;

                foreach (EsShapeInfo shape in drawing.Shapes)
                {
                    foreach (EsPointInfo point in shape.Points)
                    {
                        minX = Math.Min(minX, point.Position.X);
                        minY = Math.Min(minY, point.Position.Y);
                        maxX = Math.Max(maxX, point.Position.X);
                        maxY = Math.Max(maxY, point.Position.Y);
                    }
                }

                EsVector2<float> drawingSize = new(maxX - minX, maxY - minY);
                float drawingAspectRatio = drawingSize.X / drawingSize.Y;
                float canvasAspectRatio = size.X / size.Y;
                float scaleX = 1f, scaleY = 1f;

                switch (_scaleRule)
                {
                    case EsScaleRule.Fit:
                        float fitScale = Math.Min(size.X / drawingSize.X, size.Y / drawingSize.Y);
                        scaleX = fitScale;
                        scaleY = fitScale;

                        break;

                    case EsScaleRule.Stretch:
                        scaleX = size.X / drawingSize.X;
                        scaleY = size.Y / drawingSize.Y;

                        break;
                    default: break;
                }

                foreach (EsShapeInfo shape in drawing.Shapes)
                {
                    List<EsPointInfo> scaledPoints = new();
                    List<EsLineInfo> scaledLines = new();

                    foreach (EsPointInfo point in shape.Points)
                    {
                        scaledPoints.Add(new EsPointInfo()
                        {
                            Position = new(
                                (point.Position.X * scaleX) + position.X,
                                (point.Position.Y * scaleY) + position.Y,
                                point.Position.Z
                            )
                        });
                    }

                    foreach (EsLineInfo line in shape.Lines) scaledLines.Add(new() { Start = line.Start, End = line.End, Fill = shape.Fill });

                    drawInfo.Shapes.Add(new EsShapeInfo()
                    {
                        Points = scaledPoints,
                        Lines = scaledLines,
                        Fill = shape.Fill
                    });
                }
            }
            
            return drawInfo;
        }

        public override string ToString()
        {
            return $"<EsCanvas Name=\"{_name}\">";
        }
    }

    public class EsTriangle : IEsDrawing
    {
        // Properties and Fields
        
        private EsTriangleType _type;
        private EsVector2<float> _size;
        private EsVector2<float> _position;
        private float _rotation;
        private IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(int start, int end)> _lines;

        public EsTriangleType Type
        {
            get => _type;
            set
            {
                _type = value;
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(
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
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(
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
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(
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
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(
                    _type, _size, _position, value
                );
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }
        
        public IEsColor Fill { get => _fill; set => _fill = value; }

        // Constructors and Methods

        public EsTriangle(EsTriangleType type = EsTriangleType.Acute, EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _type = type;
            _size = size ?? new(100, 100);
            _position = position ?? new();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(
                type, _size, _position, rotation
            );
            _points = calculated.points;
            _lines = calculated.lines;
        }

        public List<EsVector2<float>> GetPoints()
        {
            
            return new(_points);
        }

        public List<(int Start, int End)> GetLines()
        {
            return new(_lines);
        }

        public static (List<EsVector2<float>> Points, List<(int Start, int End)> Lines) Calculate(
            EsTriangleType type, EsVector2<float> size, EsVector2<float> position, float rotation
        )
        {
            List<EsVector2<float>> points = new();
            List<(int start, int end)> lines = new() { (0, 1), (1, 2), (2, 0) };
            EsVector2<float> center = new(size.X / 2, size.Y / 2);
            
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
                    float x = points[i].X - center.X;
                    float y = points[i].Y - center.Y;
                    float rotatedX = x * cos - y * sin;
                    float rotatedY = x * sin + y * cos;
                    points[i] = new(
                        rotatedX + center.X,
                        rotatedY + center.Y
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
        
        private EsVector2<float> _size;
        private EsVector2<float> _position;
        private float _rotation;
        private IEsColor _fill;
        private List<EsVector2<float>> _points;
        private List<(int start, int end)> _lines;

        public EsVector2<float> Size
        {
            get => _size;
            set
            {
                _size = value;
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(value, _position, _rotation);
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
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(_size, value, _rotation);
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
                (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(_size, _position, value);
                _points = calculated.points;
                _lines = calculated.lines;
            }
        }
        
        public IEsColor Fill { get => _fill; set => _fill = value; }
        
        // Constructors and Methods

        public EsRectangle(EsVector2<float>? size = null, EsVector2<float>? position = null, float rotation = 0, IEsColor? fill = null)
        {
            _size = size ?? new(100, 100);
            _position = position ?? new();
            _rotation = float.Clamp(rotation, -360, 360);
            _fill = fill ?? new EsColor3(EsColors.White);
            (List<EsVector2<float>> points, List<(int start, int end)> lines) calculated = Calculate(_size, _position, _rotation);
            _points = calculated.points;
            _lines = calculated.lines;
        }

        public List<EsVector2<float>> GetPoints()
        {
            return new(_points);
        }

        public List<(int Start, int End)> GetLines()
        {
            return new(_lines);
        }

        public static (List<EsVector2<float>> Points, List<(int Start, int End)> Lines) Calculate(
            EsVector2<float> size, EsVector2<float> position, float rotation
        )
        {
            List<EsVector2<float>> points = new();
            List<(int start, int end)> lines = new() { (0, 1), (1, 2), (2, 3), (3, 0) };
            EsVector2<float> center = new(size.X / 2, size.Y / 2);
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
                    float x = points[i].X - center.X;
                    float y = points[i].Y - center.Y;
                    float rotatedX = x * cos - y * sin;
                    float rotatedY = x * sin + y * cos;
                    points[i] = new(
                        rotatedX + center.X,
                        rotatedY + center.Y
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
            
            return (points, lines);
        }

        public override string ToString()
        {
            return $"(EsRectangle Size=({_size.X}, {_size.Y}) Position=({_position.X}, {_position.Y}) Rotation={_rotation}º)";
        }
    }
}