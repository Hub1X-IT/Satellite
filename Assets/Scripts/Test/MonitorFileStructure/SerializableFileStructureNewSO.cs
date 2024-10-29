using UnityEngine;

[CreateAssetMenu(fileName = "SerializableFileStructureNew", menuName = "Scriptable Objects/SerializableFileStructureNew")]
public class SerializableFileStructureNewSO : ScriptableObject
{
    public DiskNew[] disks;


    public void Initialize()
    {
        foreach (var disk in disks)
        {
            // disk.Initialize();
        }
    }
}
