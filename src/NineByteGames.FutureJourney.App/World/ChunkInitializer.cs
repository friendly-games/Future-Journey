using System;
using System.Collections.Generic;
using System.Linq;
using NineByteGames.FutureJourney.Providers;

namespace NineByteGames.FutureJourney.World
{
  /// <summary>
  ///  Class that initializes each chunk with data-generated with perlin noise.  This class should
  ///  be replaced with something that loads it from a level data or save file or something.
  /// </summary>
  public static class ChunkInitializer
  {
    /// <summary> Initializes the given chunk. </summary>
    public static void InitializeChunk(Chunk chunk, IPerlinNoiseProvider noiseProvider)
    {
      var position = chunk.Position;

      for (int y = 0; y < Chunk.NumberOfGridItemsHigh; y++)
      {
        for (int x = 0; x < Chunk.NumberOfGridItemsWide; x++)
        {
          var gridPosition = new GridCoordinate(position, new InnerChunkGridCoordinate(x, y));
          var gridItem = GetGridItemAt(gridPosition, chunk, noiseProvider);

          chunk[gridPosition.InnerChunkGridCoordinate] = gridItem;
        }
      }
    }

    private static readonly Random random = new Random();

    private static GridItem GetGridItemAt(GridCoordinate gridPosition, Chunk chunk, IPerlinNoiseProvider noiseProvider)
    {
      // TODO UNITY
      // TODO load this from somewhere else
      var tileValue = noiseProvider.GetNoise(gridPosition.X / 10f, gridPosition.Y / 10f);

      // TODO UNITY
      // TODO remove random call
      byte variant = (byte)random.Next(0, 4);
      GridItem gridItem;

      if (tileValue > 0.7f)
      {
        gridItem = new GridItem(TileType.Water, variant);
      }
      else if (tileValue < 0.3f)
      {
        gridItem = new GridItem(TileType.Hill, variant);
      }
      else
      {
        gridItem = new GridItem(TileType.Path, variant);
      }

      var buildingValue = noiseProvider.GetNoise(-gridPosition.X / 8f, gridPosition.Y / 8f);
      if (gridPosition.X % 2 == 0
          && gridPosition.Y % 2 == 0
          && buildingValue > 0.85f
          && gridItem.Type != TileType.Water)
      {
        // TODO UNITY
        //gridItem.BuildingType = BuildingType.TreeStump;
        //gridItem.BuildingInstance = 1;
        //gridItem.Entity = chunk.Storage.CreateEntity(TreeTrunk.Instance);
      }

      return gridItem;
    }
  }
}