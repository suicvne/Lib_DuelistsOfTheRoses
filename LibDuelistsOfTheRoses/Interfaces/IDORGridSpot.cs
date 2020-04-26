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
using LibDuelistsOfTheRoses.Types;

namespace LibDuelistsOfTheRoses.Interfaces
{
    public interface IDORGrid
    {
        IDORGridSpot GetGridSpot(IVector location);
    }

    public interface IDORGridSpot
    {
        IDORGrid p_Parent { get; set; }
        IFieldCard p_ContainedCard { get; set; }
        TerrainType p_TerrainType { get; set; }
    }
}
