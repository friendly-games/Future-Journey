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
    private readonly Camera2D _camera;
    private readonly Texture2D[] _textureLookup;
    private readonly SpriteBatch _spriteBatch;

    public CharacterDrawer(ResourceLoader loader, GraphicsDevice device, Camera2D camera)
    {
      _spriteBatch = new SpriteBatch(device);

      _camera = camera;
      _textureLookup = loader.MapEnumToResources<CharacterTypes, Texture2D>("images/characters.");
    }

    public void Draw(Character character, CharacterTypes type)
    {
      _spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);

      var sprite = _textureLookup[(int)type];

      var position = character.Position;

      _spriteBatch.Draw(sprite,
                        new Vector2(position.X * Constants.PixelSize, -position.Y * Constants.PixelSize),
                        origin: new Vector2(Constants.HalfPixelSize, Constants.HalfPixelSize));

      _spriteBatch.End();
    }
  }

  public enum CharacterTypes
  {
    Default,
    Player,
  }
}