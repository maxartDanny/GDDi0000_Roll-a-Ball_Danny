using UnityEngine;

/// <summary>
/// Simply holds the most recent checkpoint
/// </summary>
public static class CheckpointKeeper {

    public static Transform LastCheckpoint { get; private set; }

    public static void SetCheckpoint(Transform newCheckpoint) {
        LastCheckpoint = newCheckpoint;
    }

}