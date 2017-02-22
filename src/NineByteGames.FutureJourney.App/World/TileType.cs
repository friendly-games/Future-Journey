using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.FutureJourney.World
{
  /// <summary> The type of tile that is present in the world. </summary>
  public enum TileType : byte
  {
    /// <summary> Special cased to indicate that no tile is present. </summary>
    Default,
    Water,
    Path,
    Hill,
  }
}