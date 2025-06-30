// Imports

using Espresso.EspInterface;
using Espresso.EspScript;

// Instance Namespace

namespace Espresso.EspInstance
{
    // Interfaces

    public interface IEsInstance
    {
        // Properties
        
        public IEsInstance? Parent { get; set; }
        public string InstanceName { get; }
        public string Name { get; set; }
        public EsSignal<Action<IEsInstance>> OnChildAdded { get; }
        public EsSignal<Action<IEsInstance>> OnChildRemoved { get; }
        
        // Methods

        public IEsInstance? Clone();
        public void Destroy();
        public List<IEsInstance> ChildrenSelector(string selector);
        public List<IEsInstance> DescendantsSelector(string selector);
        public List<IEsInstance> GetChildren();
        public void AddChild(IEsInstance child);
        public void RemoveChild(IEsInstance child);
        public bool HasChild(IEsInstance child);
        public List<IEsInstance> GetDescendants();
        public List<string> GetTags();
        public void AddTag(string tag);
        public void RemoveTag(string tag);
        public bool HasTag(string tag);
        public IEsDrawing? Render();
    }
}