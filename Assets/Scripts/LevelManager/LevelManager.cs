using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levelPrefabs;
// Make a list of levels as prefabs 
    [SerializeField] private int _index;
    private GameObject _currentLevel;
    void Awake()
    {
        SpawnNextLevel();
    }
    
    void SpawnNextLevel()
    {
        // Check if there is a level
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;
            if (_index >= levelPrefabs.Count)
            {
                // Reset level count when all levels are completed
                _index = 0;
            }
        }
        // Instantiate a level inside the Level Container everytime
        _currentLevel = Instantiate(levelPrefabs[_index], container.position, Quaternion.identity);
        _currentLevel.transform.localPosition = Vector3.zero;

        var artComponent = _currentLevel.GetComponent<ColorSetup>();
        if(artComponent != null)
        {
            ColorManager.Instance.ChangeColorByType(artComponent.artType);
        }
    }
}
[System.Serializable]
public class ColorSetup : MonoBehaviour
{
    public ColorManager.ArtType artType;
    public List<Color> colors;
}
