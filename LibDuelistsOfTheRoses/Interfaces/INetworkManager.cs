using System;
namespace LibDuelistsOfTheRoses.Interfaces
{
    /// <summary>
    /// This is really a Unity/Mirror type but whatever.
    /// </summary>
    public interface INetworkManager
    {
        void StartClient();
        void StartHost();
    }
}
