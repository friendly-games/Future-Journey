﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace NineByteGames.FutureJourney
{
  public class InputManager
  {
    private readonly Game _game;

    public InputManager(Game game)
    {
      _game = game;
    }

    public void Update(ref Vector2 playerPosition)
    {
      KeyboardState keyboardState = Keyboard.GetState();

      if (keyboardState.IsKeyDown(Keys.Escape))
      {
        _game.Exit();
      }

      var moveSpeed = 0.5f;

      if (keyboardState.IsKeyDown(Keys.A))
        playerPosition.X -= moveSpeed;

      if (keyboardState.IsKeyDown(Keys.D))
        playerPosition.X += moveSpeed;

      if (keyboardState.IsKeyDown(Keys.W))
        playerPosition.Y += moveSpeed;

      if (keyboardState.IsKeyDown(Keys.S))
        playerPosition.Y -= moveSpeed;
    }
  }
}