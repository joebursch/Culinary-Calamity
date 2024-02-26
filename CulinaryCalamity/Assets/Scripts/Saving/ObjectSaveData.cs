

namespace Saving
{
    public abstract class ObjectSaveData
    {
        public abstract string Serialize();
        public abstract void Deserialize(string serializedData);
        public abstract void ApplyToObject();
    }
}