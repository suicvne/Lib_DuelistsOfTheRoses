using System;
using System.Collections.Generic;
using ISUnityInterfaces;

namespace LibDuelistsOfTheRoses
{
    public interface IDORCard
    {
        CardClass p_Class {get;set;}
        string p_CardName {get;set;}
        string p_CardDescription {get;set;}
        CardType p_CardType {get;set;}
        CardAttribute p_CardAttribute {get;set;}
        ISprite p_CardArt {get;set;}
        int p_CardNumber {get;set;}
        int p_DeckCost {get;set;}
    }

    public class IDORCardNumericalCompare : IComparer<IDORCard>
    {
        public int Compare(IDORCard a, IDORCard b)
        {
            return a.p_CardNumber.CompareTo(b.p_CardNumber);
        }
    }

    public interface IDORMonsterCard
    {
        int p_CardAttack { get; set; }
        int p_CardDefense { get; set; }

        bool p_AllowLabyrinthMovement { get; set; }
        bool p_StrongInToonTerrain { get; set; }
    }
}
