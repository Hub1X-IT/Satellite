using UnityEngine;

[CreateAssetMenu(fileName = "SerializableFileStructure", menuName = "Scriptable Objects/SerializableFileStructure")]
public class SerializableFileStructureSO : ScriptableObject
{
    public Disk[] disks;


    public void Initialize()
    {
        foreach (var disk in disks)
        {
            disk.Initialize();
        }
    }
}
