using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Chimes/Note Set")]
public class NoteSet : ScriptableObject
{
    [SerializeField]
    int rootNote;
    [SerializeField]
    int numOctaveRepeats;
    [SerializeField]
    bool[] intervalsInScale = new bool[12];

    private List<int> notes;

    void OnValidate()
    {
        notes.Clear();
        notes.Add(rootNote);
        for (int octave = 0; octave < numOctaveRepeats; octave++)
        {
            for (int note = 0; note < 12; note++)
            {
                if (note == 0 && octave == 0) continue;
                if (!intervalsInScale[note]) continue;

                notes.Add(rootNote + octave * 12 + note);
            }
        }
    }

    public int GetRandomNote()
    {
        return notes[Random.Range(0, notes.Count - 1)];
    }
}
