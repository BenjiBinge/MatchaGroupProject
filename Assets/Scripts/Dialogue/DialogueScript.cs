using UnityEngine;

[CreateAssetMenu]
public class DialogueScript: ScriptableObject
{
    [TextArea]
    public string[] lines;
}
