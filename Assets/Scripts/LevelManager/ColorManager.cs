using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class ColorManager : Singleton<ColorManager>
{
  public List<Material> materials;
  public List<ColorSetup> colorSetups;
  
  public enum ArtType
  {
    
  }

  public void ChangeColorByType(ArtType artType)
  {
    var setup = colorSetups.Find(i => i.artType == artType);

    for (int i = 0; i < materials.Count; i++)
    {
      materials[i].SetColor("_Color", setup.colors[i]);
    }
  }
}

public class ColorSetup
{
  public List<Color> colors;
}
