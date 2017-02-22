using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.FutureJourney.Providers.Implementations;

namespace NineByteGames.FutureJourney.World
{
  public class Entity
  {
  }

  public interface IEntityLifeCycleHook
  {
  }

  public class AspectStorageContainer
  {
    public void AddHook(IEntityLifeCycleHook hook)
    {
    }
  }

  /// <summary>
  ///  Contains all of the chunks that make up the currently loaded world.
  /// </summary>
  public class WorldGrid : IEntityLifeCycleHook
  {
    // TODO make this bigger (bigger makes it slower to start)
    public const int NumberOfChunksWide = 4;
    public const int NumberOfChunksHigh = 4;

    private readonly Chunk[] _chunks;

    /// <summary> Constructor. </summary>
    /// <param name="engine"> The EntityComponentEngine for the current game. </param>
    public WorldGrid(AspectStorageContainer worldStorage)
    {
      WorldStorage = worldStorage;
      WorldStorage.AddHook(this);

      _chunks = new Chunk[NumberOfChunksHigh * NumberOfChunksWide];

      InitializeChunks(WorldStorage);
    }

    /// <summary> The storage for all entities contained that represent things on the grid. </summary>
    public AspectStorageContainer WorldStorage { get; }

    /// <summary> Creates and initializes each chunk with data. </summary>
    /// <param name="worldStorage"> The container that stores the data for each chunk. </param>
    private void InitializeChunks(AspectStorageContainer worldStorage)
    {
      for (int y = 0; y < NumberOfChunksHigh; y++)
      {
        for (int x = 0; x < NumberOfChunksWide; x++)
        {
          var chunk = new Chunk(new ChunkCoordinate(x, y), worldStorage);

          // TODO UNITY
          // load this data from elsewhere
          ChunkInitializer.InitializeChunk(chunk, new PerlinNoiseProvider());
          _chunks[CalculateIndex(x, y)] = chunk;
        }
      }
    }

    /// <summary>
    ///  Gets the chunk at the specified coordinate.
    /// </summary>
    public Chunk this[ChunkCoordinate coordinate]
    {
      get { return _chunks[CalculateIndex(coordinate.X, coordinate.Y)]; }
      set { _chunks[CalculateIndex(coordinate.X, coordinate.Y)] = value; }
    }

    private int CalculateIndex(int x, int y)
    {
      return x + y * NumberOfChunksWide;
    }

    /// <summary>
    ///  Returns true if the given coordinate is a valid coordinate that can be used in this grid.
    /// </summary>
    public bool IsValid(ChunkCoordinate chunkCoordinate)
    {
      return chunkCoordinate.X >= 0 && chunkCoordinate.X < NumberOfChunksWide
             && chunkCoordinate.Y >= 0 && chunkCoordinate.Y < NumberOfChunksWide;
    }

    /// <inheritdoc />
    public void HandleCreation(Entity entity)
    {
    }

    /// <inheritdoc />
    public void HandleDestruction(Entity entity)
    {
      // TODO: remove from grid etc. etc.
    }
  }
}