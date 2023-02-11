using System;

namespace ActiveTerrain;

[Flags]
public enum TempControlType : byte
{
    None,

    Heater,

    Cooler,

    Both
}