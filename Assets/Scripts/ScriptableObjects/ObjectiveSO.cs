using UnityEngine;

[CreateAssetMenu]
public class ObjectiveSO : ScriptableObject
{
    [SerializeField]
    private string objective;

    public string Objective => objective;
}
