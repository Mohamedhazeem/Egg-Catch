# Egg-Catch

## Unity & Dependencies

- **Unity Version:** `2022.3.62f1`
- **Render Pipeline:** Universal Render Pipeline
- **Main Packages:**
  - [Lean Pool](https://assetstore.unity.com/packages/tools/utilities/lean-pool-103246)(2.0.2) – Efficient object pooling
  - [DOTween](http://dotween.demigiant.com/)(1.2.765) – Tweening animations
  - Addressables (v1.22.3) – Asset management
  - TextMeshPro – UI

---

## How to Play / Test

### In-Editor

1. Open the project in Unity.
2. Load the scene: `Assets/Scenes/Scene.unity`.
3. Press **Play**.
4. Use **A / S / D** keys to switch lanes for the player.
5. Catch falling eggs and avoid bombs. AI players also compete in other lanes.

### Multiplayer Ready (for later)

- No actual networking now.
- But all player logic, score updates, and input handling are modular and server-authority-ready.

## 💡 Notes

- Uses dynamic `PlayerId` assignment.
- AI is data-driven via `ScriptableObjects`.
- Sound/UI/Scoring logic respects player/AI distinction.
- Code designed to be scalable with minimal rewrite for networking.

---
