using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NineByteGames.FutureJourney.Resources;
using NineByteGames.FutureJourney.World;

namespace NineByteGames.FutureJourney.Drawing
{
  /// <summary> Responsible for drawing the grid of tiles that are visible to the user. </summary>
  public class VisibleTileGridDrawer
  {
    private readonly Texture2D[] _tileTextures;
    private readonly Camera2D _camera;
    private readonly WorldGridSlice<int> _worldView;

    private readonly TextureRenderer _renderer;

    public VisibleTileGridDrawer(GraphicsDevice device,
                                 ResourceLoader resources,
                                 WorldGrid world,
                                 Camera2D camera2D)
    {
      _tileTextures = resources.MapEnumToResources<TileType, Texture2D>("images/tiles.");

      _worldView = new WorldGridSlice<int>(world,
                                           device.Viewport.Width / Constants.PixelSize + 2,
                                           device.Viewport.Height / Constants.PixelSize + 2);
      _worldView.Initialize(new GridCoordinate(0, 0));
      _camera = camera2D;

      _renderer = new TextureRenderer(_camera, new SpriteBatch(device))
                  {
                    Offset = new Vector2(0.5f, 0.5f) * Constants.PixelSize
                  };
    }

    /// <summary> Updates the center based on the position of the camera. </summary>
    public void Update()
    {
      _worldView.Recenter(new GridCoordinate(_camera.CameraCenter));
    }

    /// <summary> Draws all of the grids that are around the center of the camera. </summary>
    public void Draw()
    {
      _renderer.Begin();

      foreach (var item in _worldView.VisibleGridItems.Data)
      {
        if (item.GridItem.Type == TileType.Default)
          continue;

        var sprite = GetSprite(item.GridItem);

        var rotation = (float)Math.PI / 2.0f * item.GridItem.Variant;

        _renderer.Draw(sprite, new Vector2(item.Position.X + .5f, item.Position.Y + .5f), rotation);
      }

      _renderer.End();
    }

    /// <summary> Gets the sprite for the given GridItem. </summary>
    /// <param name="itemGridItem"> The item grid item. </param>
    /// <returns> The sprite. </returns>
    private Texture2D GetSprite(GridItem itemGridItem)
      => _tileTextures[(int)itemGridItem.Type];
  }
}