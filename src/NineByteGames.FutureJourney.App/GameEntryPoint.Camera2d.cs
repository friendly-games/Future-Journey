using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NineByteGames.FutureJourney
{
  public class Camera2D
  {
    private readonly GraphicsDevice _device;

    public Camera2D(GraphicsDevice device)
    {
      _device = device;
    }

    public Matrix TransformMatrix;

    public Vector2 CameraCenter { get; private set; }

    public void SetPosition(Vector2 cameraCenter)
    {
      CameraCenter = cameraCenter;

      Matrix playerOffset;
      Matrix.CreateTranslation(-cameraCenter.X * VisibleTileGridDrawer.PixelSize,
                               cameraCenter.Y * VisibleTileGridDrawer.PixelSize,
                               0,
                               out playerOffset);

      // Matrix scaleMatrix;
      // Matrix.CreateScale(1, 1, 1, out scaleMatrix);
      // Matrix.Multiply(ref playerOffset, ref scaleMatrix, out transformMatrix);

      Matrix viewPortOffset;
      Matrix.CreateTranslation(_device.Viewport.Width * 0.5f,
                               _device.Viewport.Height * 0.5f,
                               0,
                               out viewPortOffset);

      Matrix.Multiply(ref playerOffset, ref viewPortOffset, out TransformMatrix);
    }
  }
}