using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    private Vector2 _playerPosition;

    private VisibleTileGridDrawer _visibleTileGridDrawer;
    private InputManager _inputManager;
    private Camera2D _camera;

    public GameEntryPoint()
    {
      var graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      _world = new WorldGrid(new AspectStorageContainer());

      _playerPosition = new Vector2(50, 50);
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
      var resourceHelper = new ResourceHelper(Content);

      GraphicsDevice device = GraphicsDevice;
      _camera = new Camera2D(device);

      _inputManager = new InputManager(this);
      _visibleTileGridDrawer = new VisibleTileGridDrawer(_world, device, resourceHelper, _camera);
    }

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
      _inputManager.Update(ref _playerPosition);
      _camera.SetPosition(_playerPosition);

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

      _visibleTileGridDrawer.Update();
      _visibleTileGridDrawer.Draw();

      // TODO: Add your drawing code here

      base.Draw(gameTime);
    }
  }
}