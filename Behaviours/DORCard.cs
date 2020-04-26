using System;
using System.Collections.Generic;
using LibDuelistsOfTheRoses.Interfaces;

namespace LibDuelistsOfTheRoses.Behaviours
{
    public abstract class DORCard : IDORCard
    {
        public CardClass p_Class { get; set; }
        public string p_CardName { get; set; }
        public string p_CardDescription { get; set; }
        public CardType p_CardType { get; set; }
        public CardAttribute p_CardAttribute { get; set; }
        public ISUnityInterfaces.ISprite p_CardArt { get; set; }
        public int p_CardNumber { get; set; }
        public int p_DeckCost { get; set; }
    }

    public abstract class DORMonsterCard : DORCard, IDORMonsterCard
    {
        public int p_CardAttack { get; set; }
        public int p_CardDefense { get; set; }

        public bool p_AllowLabyrinthMovement { get; set; }
        public bool p_StrongInToonTerrain { get; set; }
    }

    public abstract class DOREffectMonsterCard : DORMonsterCard, IDOREffectCard
    {
        public IDORCardEffect[] p_CardEffects { get; set; }
    }

    public abstract class DOREffectCard : DORCard, IDOREffectCard
    {
        public IDORCardEffect[] p_CardEffects { get; set; }
    }
}
