// Imports

using Espresso.EspMath;
using Espresso.EspScript;
using Espresso.EspStyling;

// Instance Namespace

namespace Espresso.EspInstance
{
    // Enums

    public enum EsLineType
    {
        None,
        Solid,
        Dotted,
        Dashed,
        DotDash,
        TwoDash,
        LongDash
    }
    
    // Interfaces

    public interface IEsInstance
    {
        // Properties
        
        public IEsInstance? Parent { get; set; }
        public string InstanceName { get; }
        public string Name { get; set; }
        public EsSignal<Action<IEsModifier>> OnModifierAdded { get; }
        public EsSignal<Action<IEsModifier>> OnModifierRemoved { get; }
        public EsSignal<Action<IEsInstance>> OnChildAdded { get; }
        public EsSignal<Action<IEsInstance>> OnChildRemoved { get; }
        
        // Methods

        public IEsInstance? Clone();
        public void Destroy();
        public List<IEsInstance> ChildrenSelector(string selector);
        public List<IEsInstance> DescendantsSelector(string selector);
        public List<IEsModifier> GetModifiers();
        public void AddModifier(IEsModifier modifier);
        public void RemoveModifier(IEsModifier modifier);
        public bool HasModifier(IEsModifier modifier);
        public List<IEsInstance> GetChildren();
        public void AddChild(IEsInstance child);
        public void RemoveChild(IEsInstance child);
        public bool HasChild(IEsInstance child);
        public List<IEsInstance> GetDescendants();
        public List<string> GetTags();
        public void AddTag(string tag);
        public void RemoveTag(string tag);
        public bool HasTag(string tag);
        public EsDrawInfo? Render();
    }

    public interface IEsModifier
    {
        // Properties
        
        public IEsInstance? Parent { get; set; }
        public string ModifierName { get; }
        public bool Active { get; set; }
    }
    
    // Records

    public record EsPointInfo
    {
        // Properties
        
        public required EsVector3<float> Position { get; init; }
        public float Radius { get; init; } = 0;
    }

    public record EsLineInfo
    {
        // Properties

        public required int Start { get; init; }
        public required int End { get; init; }
        public float Thickness { get; init; } = 5;
        public float Opacity { get; init; } = 1;
        public EsLineType Type { get; init; } = EsLineType.Solid;
        public IEsColor Fill { get; init; } = EsColor3.Black;
    }
    
    // Classes

    public class EsDrawInfo
    {
        // Properties and Fields

        private IEsColor _fill;
        private List<EsPointInfo> _points;
        private List<EsLineInfo> _lines;
        
        public IEsColor Fill { get => _fill; set => _fill = value; }
        
        public List<EsPointInfo> Points { get => _points; }
        
        public List<EsLineInfo> Lines { get => _lines; }
        
        // Constructors and Methods

        public EsDrawInfo(IEsColor? fill = null, List<EsPointInfo>? points = null, List<EsLineInfo>? lines = null)
        {
            _fill = fill ?? EsColor3.White;
            _points = points ?? new();
            _lines = lines ?? new();
        }
    }
}