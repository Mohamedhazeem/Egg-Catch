using System.Collections.Generic;
using UnityEngine;

public static class PlayerRegistry
{
    private static readonly List<PlayerId> taken = new();

    public static PlayerId GetNextHumanId()
    {
        foreach (var id in new[] { PlayerId.Player_1, PlayerId.Player_2, PlayerId.Player_3, PlayerId.Player_4 })
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
        foreach (var id in new[] { PlayerId.AI_1, PlayerId.AI_2, PlayerId.AI_3, PlayerId.AI_4 })
        {
            if (!taken.Contains(id))
            {
                taken.Add(id);
                return id;
            }
        }

        Debug.LogWarning("Too many AIs! Reusing last AI id.");
        return PlayerId.AI_4;
    }
}
