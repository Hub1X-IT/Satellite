using UnityEngine;

[CreateAssetMenu(fileName = "SMSMessageSO", menuName = "SMS message")]
public class SMSMessageSO : ScriptableObject
{
    [SerializeField]
    private SMSMessage message;

    public SMSMessage GetMessage()
    {
        return message.Copy();
    }
}
