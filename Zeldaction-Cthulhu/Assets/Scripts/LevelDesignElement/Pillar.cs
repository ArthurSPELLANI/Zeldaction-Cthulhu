using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Shadow;
using Enemy;


public class Pillar : MonoBehaviour
{
    public bool isCharged;
    bool myCharge;
    float pillarCooldown = 0.04f;
    bool canInteract;
    PlayerShadowMode playerShadowMode;
    public float timeBeforeChargeComeBack = 0.10f;
    public GameObject Fog;
    public LayerMask pillarLayer;
    public LayerMask enemyLayer;
    BoxCollider2D colliBox;

    void Start()
    {

        playerShadowMode = GameObject.Find("ShadowMode").GetComponent<PlayerShadowMode>();
        colliBox = GetComponent<BoxCollider2D>();

        //Déclaration de si le pillar est chargé ou pas.

        if (isCharged == true)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        canInteract = true;
    }

    void Update()
    {
            
        //Activation/désactivation du fog.

        if(playerShadowMode.isShadowActivated && !isCharged)
        {
            Fog.SetActive(true);
        }
        else
        {
            Fog.SetActive(false);
        }

        //Quand le player quitte le shadowMode, la charge revient.

        if (playerShadowMode.isCharged && myCharge && !playerShadowMode.isShadowActivated)
            Charge(true);

        if (!playerShadowMode.isCharged)
            myCharge = false;

        Fog.transform.position = gameObject.transform.position;


    }
    //Collisions avec le Shadow.
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Shadow" && isCharged && canInteract && !playerShadowMode.isCharged)
        {
            UnCharge(true);
        }
        if (col.tag == "Shadow" && !isCharged && canInteract && playerShadowMode.isCharged)
        {
            Charge(true);
        }
    }

    void UnCharge(bool shadow)
    {
        isCharged = false;
        canInteract = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(ChargeCooldown());      
        myCharge = true;
        StartCoroutine(ChargeComeBack());

        if (shadow)
            playerShadowMode.isCharged = true;

    }

    void Charge(bool shadow)
    {
        isCharged = true;
        canInteract = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(ChargeCooldown());

        if (shadow)
            playerShadowMode.isCharged = false;
    }

    IEnumerator ChargeCooldown()
    {
            yield return new WaitForSeconds(pillarCooldown);
            canInteract = true;
    }
    IEnumerator ChargeComeBack()
    {
        yield return new WaitForSeconds(timeBeforeChargeComeBack);

        if(playerShadowMode.isCharged)
            Charge(true);

   
    }
    public void CorruptionBeam(Vector2 direction)
    {
        if (isCharged)
        {
            UnCharge(false);
            RaycastHit2D hitPillar = Physics2D.Raycast(new Vector2(transform.position.x + (colliBox.size.x * direction.x), transform.position.y + (colliBox.size.y * direction.y)), direction, 3f, pillarLayer);

            if (hitPillar.collider != null)
            {
                Debug.Log("yes");
                hitPillar.collider.GetComponent<Pillar>().Charge(false);
            }
            else if (hitPillar.collider == false)
            {
                StartCoroutine(RaycastFalse());
            }

            RaycastHit2D[] hitEnemis = Physics2D.RaycastAll(transform.position, direction, 3f, enemyLayer);

            foreach (RaycastHit2D hit in hitEnemis)
            {
                //hit.collider.GetComponent<EnemyBasicBehavior>().TaFonction();
            }
        }

    }
    IEnumerator RaycastFalse()
    {
        yield return new WaitForSeconds(3f);
        Charge(false);
    } 
}
