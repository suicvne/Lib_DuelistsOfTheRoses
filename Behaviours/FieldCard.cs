using System;
using System.Collections.Generic;
using ISUnityInterfaces;
using LibDuelistsOfTheRoses.Constants;
using LibDuelistsOfTheRoses.Interfaces;
using LibDuelistsOfTheRoses.Interfaces.Data;
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Behaviours
{
    public abstract class FieldCard : IFieldCard, IFieldCardEffectHandler
    {
        public FieldCardOwnership p_CardOwnership { get; set; }
        public FieldCardFace p_ThisCardFace { get; set; }
        public FieldCardPosition p_ThisCardPosition { get; set; }
        public int p_BonusAttack { get; set; }
        public int p_BonusDefense { get; set; }
        public int p_TerrainBonusAttack { get; set; }
        public int p_TerrainBonusDefense { get; set; }
        public bool p_MovedThisTurn { get; set; }
        public IFieldCardRenderer p_ThisCardRenderer { get; set; }
        public IVector p_GridPosition { get; set; }
        public IDORGridSpot p_ThisGridSpot { get; set; }
        public IGameObject p_GameObject { get; set; }
        public IFieldCardEffectHandler p_FieldCardEffectHandler { get; set; }

        // IFieldCardEffectHandler
        // In the current game state, these bits are bundled with IFieldCard
        public bool p_EffectExecutedThisTurn { get; set; }
        public int p_EffectTurnCounter { get; set; }
        public int p_SpellboundTurns { get; set; }
        public int p_EffectPreDeterminedBattleMode { get; set; }
        public List<uint> p_FieldCardIDsAffectingThisCard { get; set; }
        public uint p_UniqueID { get; set; }
        public ITerrainTypeToAdvantagesLookup p_TerrainAdvantageLookup { get; set; }

        public void CardBeginNewTurn(FieldCardOwnership nextPlayerTurn)
        {
            p_MovedThisTurn = false;
        }

        public void ChangePosition(bool applyEffects = false)
        {
            int _base = (int)(p_ThisCardPosition);
            _base++;
            if (_base > 1) _base = 0;

            //Debug.Log($"Changing position from {(int)p_ThisCardPosition} to {_base}");

            p_ThisCardPosition = (FieldCardPosition)_base;
            p_ThisCardRenderer.UpdateCardView(p_BonusAttack, p_BonusDefense, p_TerrainBonusAttack, p_TerrainBonusDefense);

            if (applyEffects)
            {
                CheckCardEffects(EffectExecutionTime.OnChangePosition);
            }
        }

        public void FlipCard(bool applyEffects = false)
        {
            // Card is already flipped
            if (p_ThisCardFace == FieldCardFace.FaceUp) return;

            int _base = (int)p_ThisCardFace;
            _base++;
            if (_base > 1) _base = 0;

            p_ThisCardFace = (FieldCardFace)_base;
            p_ThisCardRenderer.UpdateCardView(p_BonusAttack, p_BonusDefense, p_TerrainBonusAttack, p_TerrainBonusDefense);

            if(applyEffects)
            {
                CheckCardEffects(EffectExecutionTime.OnFlip);
            }
        }

        public bool IsAllowedSpace(IVector gridPosition, FieldCardOwnership playerIndex = FieldCardOwnership.Player1, bool movingDeckLeader = false)
        {
            if (p_ThisCardRenderer == null) throw new Exception("Must have a card renderer component attached.");

            IVector[] gridMovementOffsets =
                HasAdvantage() ? DORConstants.p_GridAdvantageMoveOffsets : DORConstants.p_GridMoveOffsets;

            // Check Advantage
            foreach (IVector allowedMove in gridMovementOffsets)
            {
                // Movement is valid
                if (allowedMove == gridPosition.Offset(allowedMove))
                {
                    IDORGridSpot leaderCheckGridSpot = p_ThisGridSpot.p_Parent.GetGridSpot(gridPosition);

                    if (leaderCheckGridSpot == null) throw new NullReferenceException("Got a null grid spot back");

                    IFieldDeckLeader deckLeaderComponent = leaderCheckGridSpot.p_ContainedCard.p_GameObject.GetComponent<IFieldDeckLeader>();

                    if (leaderCheckGridSpot.p_TerrainType == TerrainType.Labyrinth)
                    {
                        // Check if we're moving monster card
                        if (p_ThisCardRenderer.p_CardToRender is DORMonsterCard)
                        {
                            return ((DORMonsterCard)p_ThisCardRenderer.p_CardToRender).p_AllowLabyrinthMovement;
                        }

                        return false;
                    }

                }
            }

            return false;
        }

        public virtual bool HasAdvantage()
        {
            if (p_TerrainAdvantageLookup == null) throw new NullReferenceException($"No Terrain Advantages Lookup defined.");
            if (p_ThisCardRenderer == null || p_ThisCardRenderer.p_CardToRender == null || p_GameObject.GetComponent<IFieldDeckLeader>() != null) return false;
            if (p_ThisGridSpot == null) return false;


            return (p_TerrainAdvantageLookup.StrongOrWeak(p_ThisGridSpot.p_TerrainType, p_ThisCardRenderer.p_CardToRender.p_CardType) == 1);
        }

        public void ApplyBonusAttack(int amount, IFieldCard cardApplyingEffect)
        {
            if (!p_FieldCardIDsAffectingThisCard.Contains(cardApplyingEffect.p_UniqueID))
            {
                p_FieldCardIDsAffectingThisCard.Add(cardApplyingEffect.p_UniqueID);
                p_BonusAttack += amount;
            }
        }

        public void RemoveBonusAttack(int amount, IFieldCard cardApplyingEffect)
        {
            if (p_FieldCardIDsAffectingThisCard.Contains(cardApplyingEffect.p_UniqueID))
            {
                p_FieldCardIDsAffectingThisCard.Add(cardApplyingEffect.p_UniqueID);
                p_BonusAttack -= amount;
                p_FieldCardIDsAffectingThisCard.RemoveAll((x) => x == cardApplyingEffect.p_UniqueID);
            }
        }

        public void SpellbindCard(int turns)
        {
            if (p_FieldCardEffectHandler == null) throw new NullReferenceException($"No field card effect handler attached to this card renderer.");
            // Max
            if (turns > 99) turns = int.MaxValue;

            p_FieldCardEffectHandler.p_SpellboundTurns = turns;
            p_ThisCardRenderer.p_EnableNegativeFilter = true;

            // TODO: Trigger for negative filter
            //theCardRenderer.EnableNegative = true;

            p_ThisCardRenderer.UpdateCardView(p_BonusAttack, p_BonusDefense, p_TerrainBonusAttack, p_TerrainBonusDefense);
        }

        public void CheckCardEffects(EffectExecutionTime thisExecutionTime, IFieldCard optionalSecondCard = null, IDORGridSpot optionalBattlingTerrain = null, bool checkEndEffects = false)
        {
            if (p_ThisCardRenderer.p_CardToRender is DOREffectMonsterCard)
            {
                DOREffectMonsterCard asEffectMonster = (DOREffectMonsterCard)p_ThisCardRenderer.p_CardToRender;

                //Debug.Log($"Checking {asEffectMonster.Effects.Count} effects for effects that can be triggered during /{thisExecution}/");

                foreach (IDORCardEffect effect in asEffectMonster.p_CardEffects)
                {
                    if (effect == null)
                    {
                        //Debug.LogError("Effect was null in list?");
                        continue;
                    }
                    if (effect.p_When.HasFlag(thisExecutionTime))
                    {
                        bool canExecute = effect.CardEffect_CanPerformEffect(this);

                        if (checkEndEffects || canExecute == false)
                        {
                            effect.CardEffect_EndEffect(this, p_ThisGridSpot.p_Parent, optionalSecondCard, optionalBattlingTerrain);
                        }
                        else
                        {
                            // TODO: Notify for animating the effect
                            effect.CardEffect_Perform(this, p_ThisGridSpot.p_Parent, optionalSecondCard, optionalBattlingTerrain);
                        }
                    }
                }
            }
        }
    }
}
