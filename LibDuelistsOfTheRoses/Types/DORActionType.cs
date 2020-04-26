﻿// /**
// DORActionType
// Created 4/26/2020 12:52 PM
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
namespace LibDuelistsOfTheRoses.Types
{
    public enum DORActionType
    {
        Up,
        Down,
        Left,
        Right,
        Select,
        Summon,
        Information,
        Cancel,
        EndTurn,
        Flip,
        ChangePosition,
        ShowGraveyard,
        AIWait,
        AIFlagSummon
    }

    public enum DORMenuAction
    {
        Up,
        Down,
        Left,
        Right,
        Select,
        TBD1,
        Information,
        Cancel,
        TBD2,
        TBD3,
        TBD4,
        TBD5,
        TBD6,
        TBD7
    }
}