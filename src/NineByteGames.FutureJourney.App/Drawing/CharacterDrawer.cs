using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NineByteGames.FutureJourney.Livings;
using NineByteGames.FutureJourney.Resources;

namespace NineByteGames.FutureJourney.Drawing
{
  public class CharacterDrawer
  {
    private readonly Texture2D[] _textureLookup;
    private readonly TextureRenderer _renderer;

    public CharacterDrawer(GraphicsDevice device, ResourceLoader loader, Camera2D camera)
    {
      _textureLookup = loader.MapEnumToResources<CharacterTypes, Texture2D>("images/characters.");

      _renderer = new TextureRenderer(camera, new SpriteBatch(device))
                  {
                    Offset = new Vector2(0.5f, 0.5f) * Constants.PixelSize,
                  };
    }

    public void Draw(Character character, CharacterTypes type)
    {
      _renderer.Begin();

      var sprite = _textureLookup[(int)type];

      var position = character.Position;

      _renderer.Draw(sprite, new Vector2(position.X, position.Y));

      _renderer.End();
    }
  }

  public enum CharacterTypes
  {
    Default,
    Player,
  }
}