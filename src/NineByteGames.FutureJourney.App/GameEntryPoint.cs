using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NineByteGames.FutureJourney.Drawing;
using NineByteGames.FutureJourney.Livings;
using NineByteGames.FutureJourney.Physics;
using NineByteGames.FutureJourney.Resources;
using NineByteGames.FutureJourney.World;

namespace NineByteGames.FutureJourney
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class GameEntryPoint : Game
  {
    private readonly WorldGrid _world;

    private VisibleTileGridDrawer _visibleTileGridDrawer;
    private InputManager _inputManager;
    private Camera2D _camera;
    private readonly Character _player;
    private CharacterDrawer _characterDrawer;

    private readonly DynamicBody _playerBody;
    private readonly GridBasedPhysicsEngine _physics;
    private DebugRenderer _debugDraw;

    public GameEntryPoint()
    {
      var graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      _world = new WorldGrid(new AspectStorageContainer());

      _player = new Character
                {
                  Position = new Vector2(10, 10)
                };

      _physics = new GridBasedPhysicsEngine(_world);
      _playerBody = new DynamicBody();
    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
      base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
      var resourceHelper = new ResourceLoader(Content);

      GraphicsDevice device = GraphicsDevice;
      _camera = new Camera2D(device);

      _inputManager = new InputManager(this);
      _visibleTileGridDrawer = new VisibleTileGridDrawer(device, resourceHelper, _world, _camera);
      _characterDrawer = new CharacterDrawer(GraphicsDevice, resourceHelper, _camera);
      _debugDraw = new DebugRenderer(device, _camera);

      spriteBatch = new SpriteBatch(GraphicsDevice);
      dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
      dummyTexture.SetData(Enumerable.Repeat(Color.White, 1 * 1).ToArray());
    }

    private SpriteBatch spriteBatch;
    private Texture2D dummyTexture;
    private readonly Rectangle dummyRectangle;
    private readonly Color Colori = Color.Red;

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// game-specific content.
    /// </summary>
    protected override void UnloadContent()
    {
    }

    /// <summary>
    /// Allows the game to run logic such as updating the world,
    /// checking for collisions, gathering input, and playing audio.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime)
    {
      _playerBody.Position = _player.Position;
      _inputManager.Update(ref _playerBody.Position, gameTime);

      if (!_physics.IsInvalid(_playerBody))
      {
        _player.Position = _playerBody.Position;
      }

      if (Keyboard.GetState().IsKeyDown(Keys.R))
      {
        _player.Position = new Vector2(10, 10);
      }

      if (Keyboard.GetState().IsKeyDown(Keys.Z))
      {
        _player.Position = new Vector2(0, 0);
      }

      _camera.SetPosition(_player.Position);

      _visibleTileGridDrawer.Update();

      base.Update(gameTime);
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Navy);

      _visibleTileGridDrawer.Draw();
      _characterDrawer.Draw(_player, CharacterTypes.Player);

      var box = _playerBody.GetGridBounds();

      _debugDraw.Begin();
      _debugDraw.Draw(box);
      _debugDraw.End();

      base.Draw(gameTime);
    }
  }
}