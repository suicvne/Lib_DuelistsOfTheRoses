﻿using System;
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

    public interface IGameEventListener
    {
        IGameEvent p_GameEvent { get; set; }
        Action p_Response { get; set; }

        void EnableListener();
        void DisableListener();

        void OnEventRaised(object parameter = null);
    }
}
