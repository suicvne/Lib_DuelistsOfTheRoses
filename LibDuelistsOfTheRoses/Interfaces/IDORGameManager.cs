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
        /// <summary>
        /// A static reference to this game's master card list.
        ///
        /// When IDORGameManager acts as a server, any cards sent to,
        /// summoned, or dealt to players are verified to be in this card list.
        ///
        /// The ICardList also defines a default card, should a case arise where
        /// an invalid/too new/hacked card that both clients share is non-existent.
        /// </summary>
        ICardList p_MasterCardList { get; set; }

        /// <summary>
        /// Similar to the card IDs affecting IFieldCards, any events that need
        /// to apply to any matching monster are managed here and executed
        /// when cards are summoned or a client sends events. 
        /// </summary>
        List<IFieldCard> p_PersistentEffects { get; set; }

        /// <summary>
        /// The amount of turns left in the game.
        ///
        /// Games generally start at 99. At turn 0, the player with the
        /// most lifepoints is declared the victor.
        /// </summary>
        int p_TurnsLeftInGame { get; set; }

        /// <summary>
        /// The current player that is controlling the IDORCursor.
        /// </summary>
        FieldCardOwnership p_CurrentPlayerTurn { get; set; }

        /// <summary>
        /// A reference to the Dueling Field that this IDORGameManager
        /// manages.
        /// </summary>
        IDORGrid p_DuelingField { get; set; }

        /// <summary>
        /// A reference to an IGameEvent. When the IDORGameManager begins a new turn,
        /// this event is raised and any IGameEventListeners listening to this event
        /// will be notified.
        /// </summary>
        IGameEvent p_NewTurnEvent { get; set; }

        /// <summary>
        /// A refernce to Player 1's current information
        ///
        /// Duelists of the Roses only has two players. This is the red rose
        /// player.
        /// </summary>
        PlayerInformation p_Player1GameInformation { get; set; }

        /// <summary>
        /// A refernce to Player 2's current information
        ///
        /// This is the white rose player.
        /// </summary>
        PlayerInformation p_Player2GameInformation { get; set; }

        /// <summary>
        /// Card IDs in player 1's hand.
        ///
        /// When the player asks the IDORGameManager for cards, the server
        /// manages that and sends it to the proper client.
        /// </summary>
        int[] p_Player1Hand { get; set; }

        /// <summary>
        /// Card IDs in player 2's hand.
        ///
        /// When the player asks the IDORGameManager for cards, the server
        /// manages that and sends it to the proper client.
        /// </summary>
        int[] p_Player2Hand { get; set; }

        /// <summary>
        /// When a player connects, the IDORGameManager resolves which
        /// player they're going to be. The player is responsible for sending an
        /// array of card DB IDs. This queue is populated with the verified, shuffled
        /// version of the deck that player sends.
        /// </summary>
        Queue<int> p_Player1Deck { get; set; }

        /// <summary>
        /// When a player connects, the IDORGameManager resolves which
        /// player they're going to be. The player is responsible for sending an
        /// array of card DB IDs. This queue is populated with the verified, shuffled
        /// version of the deck that player sends.
        /// </summary>
        Queue<int> p_Player2Deck { get; set; }

        /// <summary>
        /// A reference to the INetworkManager.
        /// </summary>
        INetworkManager p_NetworkManagerInstance { get; set; }

        void CheckForActiveEffects(bool ending);
        void AddCardToGraveyard(IFieldCard card);

        /// <summary>
        /// Applies damage or healing to the specified player given an amount.
        /// Negative values will inflict damage, positive values will heal.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="player"></param>
        void ApplyLifepoints(int amount, FieldCardOwnership player);

        #region Player sends commands to server to move card.

        void HandleMonsterOnField(IFieldCard card);
        void HandleMonsterFlip(IFieldCard card);
        void HandleMonsterPosition(IFieldCard card);
        void HandleMonsterMove(IFieldCard card);

        #endregion

        /// <summary>
        /// Asks the server to summon the card ID at IVector grid position.
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

        /// <summary>
        /// Validates the deck sent from the player against the Master Card List.
        /// </summary>
        /// <param name="cardsInput"></param>
        /// <returns>An array containing all cards on the master card list.</returns>
        int[] ValidateDeck(int[] cardsInput);
    }
}
