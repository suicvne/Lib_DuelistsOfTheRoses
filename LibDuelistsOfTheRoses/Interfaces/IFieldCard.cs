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
    public interface IFieldCard
    {
        FieldCardOwnership p_CardOwnership { get; set; }

        FieldCardFace p_ThisCardFace { get; set; }
        FieldCardPosition p_ThisCardPosition { get; set; }

        int p_BonusAttack { get; set; }
        int p_BonusDefense { get; set; }

        int p_TerrainBonusAttack { get; set; }
        int p_TerrainBonusDefense { get; set; }

        bool p_MovedThisTurn { get; set; }

        IFieldCardRenderer p_ThisCardRenderer { get; set; }

        IVector p_GridPosition { get; set; }

        void ApplyBonusAttack(int amount, IFieldCard cardApplyingEffect);
        void RemoveBonusAttack(int amount, IFieldCard cardApplyingEffect);

        void FlipCard(bool applyEffects = false);
        void ChangePosition(bool applyEffects = false);

        void SpellbindCard(int turns);

        void CardBeginNewTurn(FieldCardOwnership nextPlayerTurn);

        bool IsAllowedSpace(IVector gridPosition, FieldCardOwnership playerIndex = 0, bool movingDeckLeader = false);
    }

    public interface IFieldCardEffectHandler
    {
        // IFieldCardEffectHandler

        bool p_EffectExecutedThisTurn { get; set; }
        int p_EffectTurnCounter { get; set; }
        int p_SpellboundTurns { get; set; }
        int p_EffectPreDeterminedBattleMode { get; set; }
        List<uint> p_FieldCardIDsAffectingThisCard { get; set; }

        void CheckCardEffects(EffectExecutionTime thisExecutionTime,
            IFieldCard optionalSecondCard = null,
            IDORGridSpot optionalBattlingTerrain = null,
            bool checkEndEffects = false
        );
    }

    public interface IFieldCardRenderer
    {
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
