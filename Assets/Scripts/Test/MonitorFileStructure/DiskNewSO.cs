using UnityEngine;

[CreateAssetMenu(fileName = "DiskNew", menuName = "Scriptable Objects/DiskNew")]
public class DiskNewSO : ScriptableObject
{
    [SerializeField]
    private string diskName;

    public DiskNew Disk { get; private set; }

    private void Awake()
    {
        Disk = new(null);
    }

    private void OnValidate()
    {
        Disk.SetName(diskName);
    }
}
