using System;
using System.Collections.Generic;
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces.Data
{
    [System.Serializable]
    public struct AdvantageDefinition
    {
        public TerrainType For;

        public CardType[] StrongTypes;
        public CardType[] WeakTypes;
    }

    public interface ITerrainTypeToAdvantagesLookup
    {
        AdvantageDefinition[] p_AdvantageDefinitions { get; set; }

        AdvantageDefinition GetDefinitionByTerrainType(TerrainType type);
        int StrongOrWeak(TerrainType type, CardType card);
    }
}
