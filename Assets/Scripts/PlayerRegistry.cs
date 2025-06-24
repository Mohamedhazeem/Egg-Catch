using System.Collections.Generic;
using UnityEngine;

public static class PlayerRegistry
{
    private static readonly List<PlayerId> taken = new();

    public static PlayerId GetNextHumanId()
    {
        foreach (var id in new[] { PlayerId.Player1, PlayerId.Player2, PlayerId.Player3, PlayerId.Player4 })
        {
            if (!taken.Contains(id))
            {
                taken.Add(id);
                return id;
            }
        }

        Debug.LogWarning("Too many players! Falling back to AI");
        return GetNextAIId();
    }

    public static PlayerId GetNextAIId()
    {
        foreach (var id in new[] { PlayerId.AI1, PlayerId.AI2, PlayerId.AI3, PlayerId.AI4 })
        {
            if (!taken.Contains(id))
            {
                taken.Add(id);
                return id;
            }
        }

        Debug.LogWarning("Too many AIs! Reusing last AI id.");
        return PlayerId.AI4;
    }
}
