using System;
using System.Collections.Generic;
using System.Linq;

namespace NineByteGames.FutureJourney.World
{
  /// <summary> Callback to be invoked when a grid item changes. </summary>
  public delegate void GridItemChangedCallback(
    Chunk chunk,
    GridCoordinate coordinate,
    GridItem oldValue,
    GridItem newValue
  );
}