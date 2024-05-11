using UnityEngine;

[CreateAssetMenu(fileName = "SaveValues", menuName = "tools/saveValues")]
public class SaveFile : ScriptableObject
{
    [SerializeField] public bool bootsEquiped = false;
    [SerializeField] public int curLevel = 0;
    [SerializeField] public float volumeMaster;
    [SerializeField] public float volumeMusic;
    [SerializeField] public float volumeSFX;
    [SerializeField] public float volumeUI;
    [SerializeField] public int tutorialPoint;
    [SerializeField] public bool usedToxics;
}
