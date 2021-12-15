using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chimes/Chime Object List")]
public class ChimeObjectListSO : ScriptableObject
{
    public HashSet<ChimeObject> chimes = new HashSet<ChimeObject>();
}
