// /**
// IFieldCard
// Created 4/26/2020 1:02 PM
//
// Copyright (C) 2019 Mike Santiago - All Rights Reserved
// axiom@ignoresolutions.xyz
//
// Permission to use, copy, modify, and/or distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
//
// */
using System;
using System.Collections.Generic;
using ISUnityInterfaces;
using LibDuelistsOfTheRoses.Interfaces.Data;
using LibDuelistsOfTheRoses.Interfaces.Renderers;
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces
{
    /// <summary>
    /// Defines an interface for an IFieldCard.
    ///
    /// An IFieldCard is a fairly complex object unfortunately. It is a physical
    /// representation of a card on the field. Thus, it requires a unique ID, an
    /// IGameObject in the scene, and also has state regarding Face, Position, and Ownership
    /// that are translated down to its IFieldCardRenderer.
    ///
    /// I've done the best that I could to document everything this object does. It seems hefty,
    /// but a Factory could easily take care of assigning any persistent/reference variables.
    /// </summary>
    public interface IFieldCard
    {
        /// <summary>
        /// An unsigned integer ID unique to this instance of the card.
        /// </summary>
        uint p_UniqueID { get; set; }

        /// <summary>
        /// The associated IGameObject.
        ///
        /// In the Unity representation, this is the GameObject
        /// that a MonoBehaviour inheriting from this interface is
        /// attached to.
        /// </summary>
        IGameObject p_GameObject { get; set; }

        /// <summary>
        /// Who owns this card on the field.
        /// </summary>
        FieldCardOwnership p_CardOwnership { get; set; }

        /// <summary>
        /// Is this card face up or face down?
        /// </summary>
        FieldCardFace p_ThisCardFace { get; set; }

        /// <summary>
        /// Is this card in attack or defense position?
        /// </summary>
        FieldCardPosition p_ThisCardPosition { get; set; }

        /// <summary>
        /// "Bonus" Points represent attack or defense points that are
        /// added or subtracted from the card's base attack or defense.
        ///
        /// Bonus points are traditionally from card effects or equip cards.
        /// </summary>
        int p_BonusAttack { get; set; }

        /// <summary>
        /// "Bonus" Points represent attack or defense points that are
        /// added or subtracted from the card's base attack or defense.
        ///
        /// Bonus points are traditionally from card effects or equip cards.
        /// </summary>
        int p_BonusDefense { get; set; }

        /// <summary>
        /// Terrain Bonus points represent attack or defense points that are
        /// added or subtracted from the card's base attack or defense based
        /// on their type matchup on this terrain.
        /// </summary>
        int p_TerrainBonusAttack { get; set; }

        /// <summary>
        /// Terrain Bonus points represent attack or defense points that are
        /// added or subtracted from the card's base attack or defense based
        /// on their type matchup on this terrain.
        /// </summary>
        int p_TerrainBonusDefense { get; set; }

        /// <summary>
        /// Has this card already moved this turn?
        /// </summary>
        bool p_MovedThisTurn { get; set; }

        /// <summary>
        /// The IFieldCardRenderer associated with this card.
        ///
        /// In short, it's the IFieldCardRenderer's responsibility for deciding
        /// how to layout and display the card in view.
        /// </summary>
        IFieldCardRenderer p_ThisCardRenderer { get; set; }

        /// <summary>
        /// The IFieldCardEffectHandler associated with this card.
        ///
        /// Despite the name, all IFieldCards should have an IFieldCardEffectHandler.
        ///
        /// In short, the IFieldCardHandler is responsible for managing effects and
        /// translating effects from other monsters onto this IFieldCard.
        /// </summary>
        IFieldCardEffectHandler p_FieldCardEffectHandler { get; set; }

        /// <summary>
        /// The current vector grid coordinate that this card is on.
        /// This should always match the Grid Coordinate property of
        /// p_ThisGridSpot.
        /// </summary>
        IVector p_GridPosition { get; set; }

        /// <summary>
        /// The IDORGridSpot reference that this card is contained in.
        /// </summary>
        IDORGridSpot p_ThisGridSpot { get; set; }

        
        /// <summary>
        /// "Static" reference table.
        /// </summary>
        ITerrainTypeToAdvantagesLookup p_TerrainAdvantageLookup { get; set; }

        /// <summary>
        /// Flip the card face up or face down.
        /// </summary>
        /// <param name="applyEffects"></param>
        void FlipCard(bool applyEffects = false);

        /// <summary>
        /// Invert the position of the card.
        /// </summary>
        /// <param name="applyEffects"></param>
        void ChangePosition(bool applyEffects = false);

        /// <summary>
        /// Begin the spellbind counter on the card.
        ///
        /// Translates the event to IFieldCardRenderer so it can
        /// enable the negative overlay.
        /// </summary>
        /// <param name="turns"></param>
        void SpellbindCard(int turns);

        /// <summary>
        /// Once the IDORGameManager has signaled a new turn has started,
        /// the IDORGrid will iterate over all grid spots and translate the
        /// event to the cards contained.
        ///
        /// Generally, this should do the following.
        /// 1. p_MovedThisTurn = false
        /// 2. p_FieldCardEffectHandler.p_SpellboundTurns -= 1;
        /// 3. Check for new turn effects.
        /// </summary>
        /// <param name="nextPlayerTurn">The new player's turn.</param>
        void CardBeginNewTurn(FieldCardOwnership nextPlayerTurn);

        /// <summary>
        /// Checks if the grid position is a valid move and the grid
        /// is not occupied. TODO: Maybe move?
        /// </summary>
        /// <param name="gridPosition"></param>
        /// <param name="playerIndex"></param>
        /// <param name="movingDeckLeader"></param>
        /// <returns></returns>
        bool IsAllowedSpace(IVector gridPosition, FieldCardOwnership playerIndex = 0, bool movingDeckLeader = false);
    }

    /// <summary>
    /// A seperate component meant to be tightly coupled with
    /// IFieldCard. This is responsible for checking and executing own effects
    /// and accepting effect events from other cards.
    /// </summary>
    public interface IFieldCardEffectHandler
    {
        /// <summary>
        /// Has this effect already been executed this turn?
        /// </summary>
        bool p_EffectExecutedThisTurn { get; set; }

        /// <summary>
        /// If the effect requires a turn counter, it will reference
        /// this variable.
        /// </summary>
        int p_EffectTurnCounter { get; set; }

        /// <summary>
        /// The amount of turns this card will be spellbound for.
        ///
        /// Each turn, this is subtracted by 1. Once 0, the card will be
        /// able to move again the negative filter will be lifted.
        /// </summary>
        int p_SpellboundTurns { get; set; }

        /// <summary>
        /// If the effect sets a pre-determined battle result, it will be set here.
        /// Otherwise it is 0.
        /// </summary>
        int p_EffectPreDeterminedBattleMode { get; set; }

        /// <summary>
        /// A list of unique card instance IDs that are currently affecting this card.
        ///
        /// This is used for persistent events to prevent unwanted compounding.
        ///
        /// For example, given the following effect:
        ///    "While this card is in face-up defense position, all WARRIOR monsters are reduced by 400 points"
        /// When an IFieldCard containing such an effect is triggered,
        /// its unique ID will be added to the list of any IFieldCardEffectHandler that is rendering a
        /// WARRIOR monster. 
        /// </summary>
        List<uint> p_FieldCardIDsAffectingThisCard { get; set; }

        /// <summary>
        /// Applies a bonus attack to an IFieldCard associated with this IFieldCardRenderer.
        ///
        /// </summary>
        /// <param name="amount">Positive or negative points applied to this card.</param>
        /// <param name="cardApplyingEffect">The unique ID of this card is added to the p_FieldCardIDsAffectingThisCard.</param>
        void ApplyBonusAttack(int amount, IFieldCard cardApplyingEffect);

        /// <summary>
        /// If the unique ID of the cardApplyingEffect is contained in this IFieldCardEffectHandler,
        /// the specified amount will be subtracted from this IFieldCard's bonus attack.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cardApplyingEffect"></param>
        void RemoveBonusAttack(int amount, IFieldCard cardApplyingEffect);

        /// <summary>
        /// Checks to see if this card has effects that match the EffectExecutionTime.
        /// </summary>
        /// <param name="thisExecutionTime">When this effect is happening.</param>
        /// <param name="optionalSecondCard">If a second card is associated with this event (IE, battle flip, battle, etc.) This value will not be null.</param>
        /// <param name="optionalBattlingTerrain">If this effect is taking place during battle, this will represent the IDORGridSpot</param>
        /// <param name="checkEndEffects">Should end effects be checked instead of performing effects.</param>
        void CheckCardEffects(EffectExecutionTime thisExecutionTime,
            IFieldCard optionalSecondCard = null,
            IDORGridSpot optionalBattlingTerrain = null,
            bool checkEndEffects = false
        );
    }

    public interface IFieldCardRenderer
    {
        IDORCard p_CardToRender { get; set; }
        IFieldCard p_AssociatedFieldCard { get; set; }

        bool p_EnableNegativeFilter { get; set; }
        bool p_EnableFireEffect { get; set; }

        #region Lookup Tables
        ILookupTable<CardAttribute, ISprite> p_AttributeToSpriteLookupTable { get; set; }
        ILookupTable<CardType, ISprite> p_CardTypeToSpriteLookupTable { get; set; }
        #endregion

        #region Renderer Components
        IUIText p_CardNameRenderer { get; set; }
        ISpriteRenderer p_CardAttributeRenderer { get; set; }
        ICardLevelRenderer p_LevelStarRenderer { get; set; }
        #endregion

        void UpdateCardView(int bonusAtk = 0, int bonusDef = 0, int terrainBonsuAtk = 0, int terrainBonusDef = 0, bool doNotApplyRotate = false);
    }
    
    public interface IMonsterCardRenderer
    {
        IUIText p_CardAttackRenderer { get; set; }
    }

    public interface IFieldDeckLeader
    {
        bool p_SummonedThisTurn { get; set; }

    }
}
