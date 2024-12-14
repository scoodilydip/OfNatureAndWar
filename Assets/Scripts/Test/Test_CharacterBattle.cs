using System;
using UnityEngine;

public class Test_CharacterBattle : MonoBehaviour {

    private Action slideComplete;

    [SerializeField] private Character_Base characterBase;
    [SerializeField] private Test_CharacterAnim anim;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private GameObject projectile;

    private bool isPlayerTeam;

    private Vector3 slideTarget;

    private State state;
    private enum State {
        Idle,
        Slide,
        Busy
    }

    private void Awake() {
        characterBase = GetComponent<Character_Base>();
        anim = GetComponent<Test_CharacterAnim>();
        state = State.Idle;
    }

    private void Update() {

        switch (state) {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Slide:
                float slideSpeed = 5f;
                float distance = .5f;
                transform.position += (slideTarget - GetPosition()) * slideSpeed * Time.deltaTime;

                if (Vector3.Distance(GetPosition(), slideTarget) < distance) {
                    transform.position = slideTarget;
                    slideComplete();
                }
                break;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetUp() {

        healthSystem = GetComponent<HealthSystem>();
        PlayIdleAnim();
    }

    private void PlayIdleAnim() {
        if (isPlayerTeam) {
            anim.PlayIdle(new Vector3(1, 0, 0));
        } else {
            anim.PlayIdle(new Vector3(-1, 0, 0));
        }
    }

    public void Damage(Test_CharacterBattle attacker, int dmg) {
        healthSystem.OnDamage(dmg);

        Vector3 attackPosition = (GetPosition() - attacker.GetPosition()).normalized;
        bool isCrit = UnityEngine.Random.Range(0, 100) < 30;
        //DamagePopUp.Create(GetPosition(), dmg, isCrit);
    }

    public bool IsDead() {
        return healthSystem.isDead();
    }

    public void ComboAttack(Action attack_One, Action attack_Two, Action attackComplete) {
        attack_One?.Invoke();
        attack_Two?.Invoke();
        attackComplete?.Invoke();
    }

    public void TestAttack(Test_CharacterBattle targetCharacter, Action attackComplete) {

        Vector3 attackMovement = targetCharacter.GetPosition() + (GetPosition() - targetCharacter.GetPosition()).normalized * 1.5f;
        Vector3 startingPos = GetPosition();

        //Slides to Target
        SlideToPosition(attackMovement, () => {
            //Arrives at Target
            state = State.Busy;
            Vector3 attackDir = targetCharacter.GetPosition() - GetPosition().normalized;
            anim.PlayAttackAnim(attackDir, () => {
                //Hits Target
                int damage = UnityEngine.Random.Range(20, 50);
                targetCharacter.Damage(this, damage);
            }, () => {
                SlideToPosition(startingPos, () => {
                    state = State.Idle;
                    anim.PlayIdle(attackDir);
                    attackComplete();
                });

            });
        });
    }

    public void AttackProjectile(Test_CharacterBattle targetCharacter, Action attackComplete) {
        Vector3 startingPos = GetPosition();
        Vector3 attackDir = targetCharacter.GetPosition() - GetPosition().normalized;

        anim.PlayIdle(startingPos);
        state = State.Busy;
        anim.PlayAttackAnim(attackDir, () => {
            //Test_Projectile.Create(GetPosition(), attackDir, 5f);
            GameObject moveProjectile = Instantiate(projectile, startingPos, transform.rotation);
            moveProjectile.GetComponent<ProjectileController>().MoveProjectile(attackDir);
            int dmg = UnityEngine.Random.Range(20, 50);
            targetCharacter.Damage(this, dmg);
            
        }, () => {
            attackComplete();
        });
    }

    private void SlideToPosition(Vector3 position, Action slideComplete) {
        slideTarget = position;
        this.slideComplete = slideComplete;
        state = State.Slide;

        /*if (slideTarget.x > 0) {
            characterBase.PlaySlideRightAnim();
        } else {
            characterBase.PlaySlideLeftAnim();
        }*/
    }
}