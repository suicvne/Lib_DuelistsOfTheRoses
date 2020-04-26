using System;
using System.Collections.Generic;
using LibDuelistsOfTheRoses.Interfaces.Data;
using LibDuelistsOfTheRoses.Interfaces.Events;
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces
{
    [Serializable]
    public struct PlayerInformation
    {
        public FieldCardOwnership Player;

        public int LifePoints;

        public int SummoningLevel;

        public int MonstersOnField;

        public int SpellsTrapsOnField;

        public List<IDORCard> Graveyard;
    }

    /// <summary>
    /// Defines a Game Manager.
    ///
    /// On the server, this sends the deck to an equivalent object client side
    /// </summary>
    public interface IDORGameManager
    {
        ICardList p_MasterCardList { get; set; }

        /// <summary>
        /// A list of field cards that are providing persistent effects.
        /// These effects should be applied (if possible) to any card brought
        /// onto the field.
        /// </summary>
        List<IFieldCard> p_PersistentEffects { get; set; }

        int p_TurnsLeftInGame { get; set; }
        FieldCardOwnership p_CurrentPlayerTurn { get; set; }

        IDORGrid p_DuelingField { get; set; }

        IGameEvent p_NewTurnEvent { get; set; }

        PlayerInformation p_Player1GameInformation { get; set; }
        PlayerInformation p_Player2GameInformation { get; set; }

        Queue<int> p_Player1Deck { get; set; }
        Queue<int> p_Player2Deck { get; set; }

        INetworkManager p_NetworkManagerInstance { get; set; }
    }
}
