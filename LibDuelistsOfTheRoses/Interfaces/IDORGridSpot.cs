// /**
// IDORGridSpot
// Created 4/26/2020 1:39 PM
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
using ISUnityInterfaces;
using LibDuelistsOfTheRoses.Interfaces.Data;
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces
{
    /// <summary>
    /// Defines an interface that all Duelists of the Roses
    /// game boards inherit from.
    /// </summary>
    public interface IDORGrid
    {
        /// <summary>
        /// A single dimensional array of IDORGridSpots.
        /// </summary>
        IDORGridSpot[] p_GridSpots { get; set; }

        /// <summary>
        /// A reference to the IDORGameManager that
        /// manages this instance.
        /// </summary>
        IDORGameManager p_GameManagerRef { get; set; }

        /// <summary>
        /// Terrain model lookup table.
        /// Could easily be overriden to sprites or anything else.
        /// </summary>
        ILookupTable<TerrainType, IGameObject> p_TerrainToGameObjectLookup { get; set; }

        /// <summary>
        /// Helper method to find a grid spot based on its
        /// x, y location on the grid.
        /// </summary>
        /// <param name="location">The Vector location of the grid on the game board.</param>
        /// <returns>An IDORGridSpot matching the location parameter. Otherwise, null.</returns>
        IDORGridSpot GetGridSpot(IVector location);

        /// <summary>
        /// Clears the entire grid of any spawned grid spots.
        /// </summary>
        void ClearGrid();

        /// <summary>
        /// Inits the grid based on the DORConstants grid size.
        /// </summary>
        void InitGrid();

        /// <summary>
        /// Method called by the IDORGameManager when a new turn has started.
        ///
        /// This method's general responsibiility is to reset move state for cards,
        /// and find out if any cards have an OnNewTurn effect to execute.
        ///
        /// Also tells monsters that are spellbound to lower their turn count by one.
        /// </summary>
        void OnNewTurn();
    }

    /// <summary>
    /// Defines an interface for a GridSpot on the IDORGrid
    ///
    /// Each spot references its parent and can have an IFieldCard contained.
    /// Every spot has a TerrainType.
    /// </summary>
    public interface IDORGridSpot
    {
        /// <summary>
        /// The IDORGrid that manages this object.
        /// </summary>
        IDORGrid p_Parent { get; set; }

        /// <summary>
        /// The IFieldCard that is contained in this IDORGridSpot.
        ///
        /// If this is null, the spot is empty.
        /// </summary>
        IFieldCard p_ContainedCard { get; set; }

        /// <summary>
        /// The type of terrain this spot represents.
        /// </summary>
        TerrainType p_TerrainType { get; set; }
    }
}
