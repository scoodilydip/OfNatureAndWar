using UnityEngine;

public class GameAssets : MonoBehaviour {

    private static GameAssets gameAssets;

    public static GameAssets Assets {
        get {
            if (gameAssets == null) gameAssets = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return gameAssets;
        }
    }

    public Transform p_DamagePopUp;
    public Transform p_Projectile;
}