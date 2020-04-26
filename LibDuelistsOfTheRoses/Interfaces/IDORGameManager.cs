using System;
using System.Collections.Generic;
using ISUnityInterfaces;
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

        int[] p_Player1Hand { get; set; }
        int[] p_Player2Hand { get; set; }

        Queue<int> p_Player1Deck { get; set; }
        Queue<int> p_Player2Deck { get; set; }

        INetworkManager p_NetworkManagerInstance { get; set; }

        void CheckForActiveEffects(bool ending);
        void AddCardToGraveyard(IFieldCard card);
        void ApplyLifepoints(int amount, FieldCardOwnership player);

        void HandleMonsterOnField(IFieldCard card);
        void HandleMonsterFlip(IFieldCard card);
        void HandleMonsterPosition(IFieldCard card);
        void HandleMonsterMove(IFieldCard card);

        /// <summary>
        /// Summons the card server side
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <param name="gridPosition"></param>
        void SummonCard(int cardNumber, IVector gridPosition);

        void SetDeck(FieldCardOwnership player, int[] cards);
        void GetCardsFromDeck(uint playerID, FieldCardOwnership player, int neededCards);

        void ReceiveRequestedCards(int[] cards);

        void EndTurn();

        /// <summary>
        /// Verifies to make sure the cards that the player wants to play
        /// match up to the cards the server thinks the player has.
        /// </summary>
        /// <param name="summonQueue">List of cards that want to be summoned. The last one to be summoned exists at the end of the list.</param>
        /// <param name="player">Which player is playing the cards.</param>
        /// <returns></returns>
        bool PlayerCanSummonCards(int[] summonQueue, FieldCardOwnership player);

        int[] ValidateDeck(int[] cardsInput);


    }
}
