using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NineByteGames.FutureJourney.World;

namespace NineByteGames.FutureJourney.Physics
{
  internal class DynamicBody
  {
    public Vector2 Position;

    public BoundingBox GetGridBounds()
    {
      return PositionToRectangle(Position, 1, 1);
    }

    private static BoundingBox PositionToRectangle(Vector2 position, int width, int height)
    {
      Debug.Assert(width < Chunk.NumberOfGridItemsWide);
      Debug.Assert(height < Chunk.NumberOfGridItemsHigh);

      double halfWidth = width / 2.0;
      double halfHeight = height / 2.0;

      int xMin = (int)Math.Floor(position.X - halfWidth);
      int xMax = (int)Math.Ceiling(position.X + halfWidth);

      int yMin = (int)Math.Floor(position.Y - halfHeight);
      int yMax = (int)Math.Ceiling(position.Y + halfHeight);

      var rect = new BoundingBox(xMin, yMin, xMax, yMax);

      Debug.WriteLine($"{position} => {rect}");

      return rect;
    }
  }

  public struct BoundingBox
  {
    public int XMin { get; }
    public int YMin { get; }
    public int XMax { get; }
    public int YMax { get; }

    public int Width
      => XMax - XMin;

    public int Height
      => YMax - YMin;

    public BoundingBox(int xMin, int yMin, int xMax, int yMax)
    {
      XMin = xMin;
      YMin = yMin;
      XMax = xMax;
      YMax = yMax;
    }

    public override string ToString()
    {
      return $"{nameof(XMin)}: {XMin}, {nameof(YMin)}: {YMin}, {nameof(XMax)}: {XMax}, {nameof(YMax)}: {YMax}";
    }

    public Rectangle ToRectangle()
    {
      return new Rectangle(XMin, YMin, XMax - XMin, YMax - YMin);
    }
  }

  internal class GridBasedPhysicsEngine
  {
    private readonly WorldGrid _world;

    public GridBasedPhysicsEngine(WorldGrid world)
    {
      _world = world;
    }

    public bool IsInvalid(DynamicBody body)
    {
      var bounds = body.GetGridBounds();

      // OPTIMIZE this could do chunk by chunk instead of looping through grid-item by grid-item. 
      Chunk currentChunk = null;
      ChunkCoordinate lastCoordinate = ChunkCoordinate.Invalid;

      for (int x = bounds.XMin; x < bounds.XMax; x++)
      {
        for (int y = bounds.YMin; y < bounds.YMax; y++)
        {
          var coordinate = new GridCoordinate(x, y);

          var chunkCoordinate = coordinate.ChunkCoordinate;

          if (!_world.IsValid(chunkCoordinate))
            return true;

          if (lastCoordinate != chunkCoordinate)
          {
            currentChunk = _world[chunkCoordinate];
          }

          var gridItem = currentChunk[coordinate.InnerChunkGridCoordinate];
          if (gridItem.IsFilled)
          {
            return true;
          }
        }
      }

      return false;
    }
  }
}