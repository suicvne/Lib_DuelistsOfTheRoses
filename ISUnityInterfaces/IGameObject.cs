using System;
namespace ISUnityInterfaces
{
    public interface IGameObject
    {
        ITransform transform { get; set; }
        string name { get; set; }

        T GetComponent<T>();
    }
}
