// /**
// IDORCardEffect
// Created 4/26/2020 12:37 PM
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
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces
{
    public interface IDORCardEffect
    {
        EffectExecutionTime p_When { get; set; }
        string p_EffectDescription { get; set; }
        bool p_PlayAnimation { get; set; }

        #region Formerly Unity "Build in Parameters". Not built into Unity types though lol
        
        EffectApplicationType p_EffectApplication { get; set; }

        void CardEffect_Perform(IFieldCard hostCard, IDORGrid monsterGrid, IFieldCard battleOpposingCard = null, IDORGridSpot battlingTerrain = null);
        void CardEffect_EndEffect(IFieldCard hostCard, IDORGrid monsterGrid, IFieldCard opposingCard = null, IDORGridSpot battlingTerrain = null);

        bool CardEffect_CanPerformEffect(IFieldCard hostCard);

        /// Instead, card effect gets to implement a lot of interfaces. Or better yet....just the ones they need. Oh yeah!

        #endregion
    }

    public interface IHostRequiresPosition
    {
        // IHostRequiresPosition
        bool p_HostRequiredSpecificPosition { get; set; }
        CardType p_TargetRequiredCardType { get; set; }
        //
    }

    public interface IHostRequiresCardFace
    {
        // IHostRequiresCardFace
        bool p_HostRequiresSpecificFace { get; set; }
        FieldCardFace p_HostRequiredCardFace { get; set; }
    }

    public interface ITargetRequiresCardFace
    {
        // ITargetRequiresCardFace
        bool p_TargetRequiresSpecificFace { get; set; }
        FieldCardFace p_TargetRequiredCardFace { get; set; }
    }

    public interface ITargetRequiresCardAttribute
    {
        // ITargetRequiresAttribute
        bool p_TargetRequiresSpecificAttribute { get; set; }
        CardAttribute p_TargetRequiredCardAttribute { get; set; }
    }

    public interface ITargetRequiresMonsterSpecs
    {
        // ITargetRequiresMonsterSpecs
        ComparisonMethod p_PointComparisonMethod { get; set; }
        bool p_TargetRequiresSpecificAttack { get; set; }
        ushort p_TargetRequiredAttack { get; set; }

        bool p_TargetRequiresSpecificDefense { get; set; }
        ushort p_TargetRequiredDefense { get; set; }
    }
}
