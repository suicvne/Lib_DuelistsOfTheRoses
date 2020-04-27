using System;
namespace ISUnityInterfaces
{
    public interface IGameObject
    {
        ITransform transform { get; set; }
        string name { get; set; }

        T GetComponent<T>();
    }

    public interface IGameObject<T> : IEquatable<IGameObject>
    {
        T GetGameObjectValue { get; set; }
    }
}
