using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NineByteGames.FutureJourney.Providers
{
  public interface IPerlinNoiseProvider
  {
    double GetNoise(double x, double y);
  }
}