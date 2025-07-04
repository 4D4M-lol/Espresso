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

    public interface IEsInstance : IDisposable
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
        public List<IEsInstance> ChildrenSelector(string selector);
        public List<IEsInstance> DescendantsSelector(string selector);
        public List<IEsModifier> GetModifiers();
        public IEsModifier? GetModifier(string modifier);
        public void AddModifier(IEsModifier modifier);
        public void RemoveModifier(string modifier);
        public bool HasModifier(string modifier);
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
        public EsLineType Type { get; init; } = EsLineType.Solid;
        public IEsColor Fill { get; init; } = EsColor3.Black;
        public float Thickness { get; init; } = 5;
        public float Opacity { get; init; } = 1;
    }

    public record EsShapeInfo
    {
        // Properties
        
        public required List<EsPointInfo> Points { get; init; }
        public required List<EsLineInfo> Lines { get; init; }
        public IEsColor Fill { get; init; } = EsColor3.White;
    }
    
    // Classes

    public class EsDrawInfo
    {
        // Properties and Fields

        private List<EsShapeInfo> _shapes;
        
        public List<EsShapeInfo> Shapes { get => _shapes; }
        
        // Constructors and Methods

        public EsDrawInfo(List<EsShapeInfo>? shapes = null)
        {
            _shapes = shapes ?? new();
        }
    }
}