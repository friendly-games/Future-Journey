using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace NineByteGames.FutureJourney.Resources
{
  /// <summary> Help for loading content from a <see cref="ContentManager"/>. </summary>
  public class ResourceHelper
  {
    private readonly ContentManager _content;

    public ResourceHelper(ContentManager content)
    {
      _content = content;
    }

    /// <summary>
    ///  Loads the resources named by the enum memories into an array whose indices are the values of
    ///  the enum.
    /// </summary>
    public TResourceType[] MapEnumToResources<TEnum, TResourceType>(string prefix)
    {
      var enumValues = (TEnum[])Enum.GetValues(typeof(TEnum));

      TResourceType[] elements = new TResourceType[enumValues.Length];

      foreach (var enumValue in enumValues)
      {
        int index = Convert.ToInt32(enumValue);
        var strName = enumValue.ToString();

        elements[index] = _content.Load<TResourceType>(prefix + strName);
      }

      return elements;
    }
  }
}