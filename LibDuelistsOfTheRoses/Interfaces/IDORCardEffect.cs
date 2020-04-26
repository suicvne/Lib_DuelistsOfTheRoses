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

        void CardEffect_Perform(IFieldCard hostCard, IDORGrid monsterGrid, IFieldCard battleOpposingCard = null, IFieldCard battlingTerrain = null);
        void CardEffect_EndEffect();

        /// Instead, card effect gets to implement a lot of interfaces. Or better yet....just the ones they need. Oh yeah!
        
        #endregion
    }

    public interface IHostRequiresPosition
    {
        // IHostRequiresPosition
        public bool p_HostRequiredSpecificPosition { get; set; }
        public CardType p_TargetRequiredCardType { get; set; }
        //
    }

    public interface IHostRequiresCardFace
    {
        // IHostRequiresCardFace
        public bool p_HostRequiresSpecificFace { get; set; }
        public FieldCardFace p_HostRequiredCardFace { get; set; }
    }

    public interface ITargetRequiresCardFace
    {
        // ITargetRequiresCardFace
        public bool p_TargetRequiresSpecificFace { get; set; }
        public FieldCardFace p_TargetRequiredCardFace { get; set; }
    }

    public interface ITargetRequiresCardAttribute
    {
        // ITargetRequiresAttribute
        public bool p_TargetRequiresSpecificAttribute { get; set; }
        public CardAttribute p_TargetRequiredCardAttribute { get; set; }
    }

    public interface ITargetRequiresMonsterSpecs
    {
        // ITargetRequiresMonsterSpecs
        public ComparisonMethod p_PointComparisonMethod { get; set; }
        public bool p_TargetRequiresSpecificAttack { get; set; }
        public ushort p_TargetRequiredAttack { get; set; }

        public bool p_TargetRequiresSpecificDefense { get; set; }
        public ushort p_TargetRequiredDefense { get; set; }
    }
}
