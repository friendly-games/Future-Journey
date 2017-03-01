using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NineByteGames.FutureJourney.Drawing
{
  public class DebugRenderer
  {
    private readonly TextureRenderer _textureRenderer;
    private readonly Texture2D _dummyTexture;

    public DebugRenderer(GraphicsDevice graphics, Camera2D camera)
    {
      _textureRenderer = new TextureRenderer(camera, new SpriteBatch(graphics))
                         {
                           SpriteColor = new Color(128, 128, 128, 128),
                           Offset = new Vector2(0.5f, 0.5f),
                           SourceRectangle = new Rectangle(0, 0, 1, 1)
                         };

      _dummyTexture = new Texture2D(graphics, 1, 1);
      _dummyTexture.SetData(Enumerable.Repeat(Color.White, 1 * 1).ToArray());
    }

    public void Begin()
      => _textureRenderer.Begin();

    public void End()
      => _textureRenderer.End();

    public void Draw(Physics.BoundingBox box)
    {
      _textureRenderer.Draw(
        _dummyTexture,
        new Vector2(box.XMin + box.Width / 2.0f, box.YMin + box.Height / 2.0f),
        rotation: 0,
        textureScale: new Vector2(box.Width, box.Height) * Constants.PixelSize
      );
    }
  }
}