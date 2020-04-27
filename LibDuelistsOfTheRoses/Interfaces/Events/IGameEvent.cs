using System;
using System.Collections.Generic;

namespace LibDuelistsOfTheRoses.Interfaces.Events
{
    public interface IGameEvent
    {
        List<IGameEventListener> p_GameEventListeners { get; set; }

        void Raise(object parameter = null);

        void RegisterListener(IGameEventListener gameEventListener);
        void UnregisterListener(IGameEventListener gameEventListener);
    }

    public interface IGameEvent<T> : IEquatable<IGameEvent>
    {
        T GameEventValue { get; set; }
    }

    public interface IGameEventListener
    {
        IGameEvent p_GameEvent { get; set; }
        Action p_Response { get; set; }

        void EnableListener();
        void DisableListener();

        void OnEventRaised(object parameter = null);
    }

    public interface IGameEventListener<T> : IEquatable<IGameEventListener>
    {
        T GameEventListenerValue { get; set; }
    }
}
