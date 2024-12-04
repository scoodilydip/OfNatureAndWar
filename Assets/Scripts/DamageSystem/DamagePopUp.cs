using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour {

    public static DamagePopUp Create(Vector3 position, int dmg, bool isCritHit) {
        Transform dmgPopup = Instantiate(GameAssets.Assets.p_DamagePopUp, position, Quaternion.identity);

        DamagePopUp dmgTextPopup = dmgPopup.GetComponent<DamagePopUp>();
        dmgTextPopup.Setup(dmg, isCritHit);

        return dmgTextPopup;
    }

    private TextMeshPro damageText;
    private static int sortOrder;
    private float fadeTimer;
    private const float MAX_Fade_TIMER = 1f;
    private Color textColor;

    private Vector3 moveVector;
    [SerializeField][Range(-1, 1)] private float moveX, moveY;

    private void Awake() {
        damageText = GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, bool isCrit) {
        damageText.text = damageAmount.ToString();
        if (!isCrit) {
            damageText.fontSize = 8;
            damageText.outlineColor = Color.black;
        } else {
            damageText.fontSize = damageText.fontSizeMax;
            damageText.outlineColor = Color.white;
        }
        textColor = damageText.color;
        fadeTimer = MAX_Fade_TIMER;

        sortOrder++;
        damageText.sortingOrder = sortOrder;

        moveVector = new Vector3(moveX, moveY) * 5f;
    }

    public void Update() {
        transform.position += moveVector * Time.deltaTime;


        if (fadeTimer > MAX_Fade_TIMER * .5f) {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        } else {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        fadeTimer -= Time.deltaTime;
        if (fadeTimer < 0) {
            float fadeSpeed = 3f;
            textColor.a -= fadeSpeed * Time.deltaTime;
            damageText.color = textColor;
            if (textColor.a <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
