using System;
namespace ISUnityInterfaces
{
    public interface ITransform
    {
        IGameObject gameObject { get; set; }
        IVector position { get; set; }
    }
}
