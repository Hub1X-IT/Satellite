using UnityEngine;

[CreateAssetMenu(fileName = "ContactSO", menuName = "Scriptable Objects/ContactSO")]
public class ContactSO : ScriptableObject
{
    [SerializeField]
    private string contactName;

    public string ContactName => contactName;
}
