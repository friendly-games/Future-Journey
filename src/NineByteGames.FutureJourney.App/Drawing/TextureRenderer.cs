using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NineByteGames.FutureJourney.Drawing
{
  /// <summary> Helper class to aid in render textures. </summary>
  public class TextureRenderer
  {
    private readonly Camera2D _camera;
    private readonly SpriteBatch _spriteBatch;

    public TextureRenderer(Camera2D camera, SpriteBatch spriteBatch)
    {
      _camera = camera;
      _spriteBatch = spriteBatch;
    }

    /// <summary> The offset to the texture where the rotation is applied. </summary>
    public Vector2 Offset
    {
      get { return _offset; }
      set { _offset = value; }
    }

    private Vector2 _offset;

    /// <summary> The scale at which textures should be drawn. </summary>
    public Vector2 DefaultScale { get; set; }
      = new Vector2(1, 1);

    /// <summary> The color of sprites, if drawing a sprite with different colors. </summary>
    public Color SpriteColor { get; set; }
      = Color.White;

    /// <summary> The Rectangle to pass to XNA </summary>
    public Rectangle? SourceRectangle { get; set; }

    /// <summary> Begin the pass through the SpriteBatch. </summary>
    public void Begin()
      => _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);

    /// <summary> End the pass through the SpriteBatch </summary>
    public void End()
      => _spriteBatch.End();

    public void Draw(Texture2D sprite, Vector2 position)
      => Draw(sprite, position, 0, DefaultScale);

    public void Draw(Texture2D sprite, Vector2 position, float rotation)
      => Draw(sprite, position, rotation, DefaultScale);

    public void Draw(Texture2D sprite, Vector2 position, float rotation, Vector2 textureScale)
    {
      _spriteBatch.Draw(
        texture: sprite,
        position: new Vector2(position.X, -position.Y) * Constants.PixelSize,
        sourceRectangle: SourceRectangle,
        color: SpriteColor,
        rotation: rotation,
        origin: _offset,
        scale: textureScale,
        effects: SpriteEffects.None,
        layerDepth: 0f
      );
    }
  }
}