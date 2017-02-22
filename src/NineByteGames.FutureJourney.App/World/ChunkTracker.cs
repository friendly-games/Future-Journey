using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace NineByteGames.FutureJourney.World
{
  /// <summary>
  ///  Maintains a reference count of how many times a chunk has been marked as tracked (and
  ///  untracked), indicating when its added/removed whether or not it is for the first or last
  ///  time. This allows callers to know when to add or to remove events from chunks.
  /// </summary>
  internal sealed class ChunkTracker
  {
    private readonly Dictionary<ChunkCoordinate, int> _chunkCount
      = new Dictionary<ChunkCoordinate, int>();

    /// <summary>
    ///  Adds the chunk to the collection, returning true if the chunk was added for the first time.
    /// </summary>
    public bool StartTracking([CanBeNull] Chunk chunk)
    {
      if (chunk == null)
        return false;

      var position = chunk.Position;
      int count;
      if (_chunkCount.TryGetValue(position, out count))
      {
        _chunkCount[position] = count + 1;
        return false;
      }

      _chunkCount[position] = 1;
      return true;
    }

    /// <summary>
    ///  Removes a chunk from the collection, returning true if the chunk was actually removed.
    /// </summary>
    public bool StopTracking([CanBeNull] Chunk chunk)
    {
      if (chunk == null)
        return false;

      var position = chunk.Position;
      int count;
      if (!_chunkCount.TryGetValue(position, out count))
        return false;

      if (count != 1)
      {
        _chunkCount[position] = count - 1;
        return false;
      }

      _chunkCount.Remove(position);
      return true;
    }
  }
}