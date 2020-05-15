using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Shadow;
using Enemy;

namespace PillarSystem
{
    public class Pillar : MonoBehaviour
    {
        public bool isCharged;        
        bool myCharge;
        float pillarCooldown = 0.04f;
        bool canInteract;
        PlayerShadowMode playerShadowMode;
        public float timeBeforeChargeComeBack = 0.10f;
        public GameObject Fog;
        public GameObject Beam;
        [HideInInspector] public Vector2 beamDir;
        public LayerMask pillarLayer;
        public LayerMask enemyLayer;
        Collider2D shadowColliBox;
        BoxCollider2D colliBox;
        bool weGotShadow;
        public bool useFog;
        Vector2 pillarPos;
        Vector2 pillarMove;

        public float loadFogDistance;
        private float shadowPillarDistance;
        private GameObject shadow;

        // pour la sauvegarde
        
        public int isCharge;

        void Start()
        {
            playerShadowMode = PlayerManager.Instance.playerShadowMode;
            colliBox = GetComponent<BoxCollider2D>();

            //Pour la sauvegarde
            if (isCharge == 0)
            {
                isCharged = false;
            }
            else if (isCharge == 1)
            {
                isCharged = true;
            }

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

            //Ce vecteur est utiliser pour déplacer le fog si le pillar bouge
            pillarPos = new Vector2(transform.position.x, transform.position.y);


        }

        void Update()
        {
            //détecte le collider du Shadow lorsque celle ci est acivée
            if (playerShadowMode.isShadowActivated && !weGotShadow)
                GetShadow();
            if (weGotShadow && !playerShadowMode.isShadowActivated)
                weGotShadow = false;

            //Activation/désactivation du fog.
            if (playerShadowMode.isShadowActivated && !isCharged && useFog && !Fog.activeSelf)
            {
                //shadow = GameObject.Find("Shadow");
                shadow = PlayerManager.Instance.playerShadowMode.shadowObject;
                shadowPillarDistance = Vector2.Distance(shadow.transform.position, transform.position);

                if (shadowPillarDistance < loadFogDistance)
                {
                    Fog.SetActive(true);
                    //change la position du fog si la postion du pillar a changé
                    Fog.transform.position += new Vector3(pillarMove.x, pillarMove.y, 0f);
                    pillarMove = new Vector2(0f, 0f);
                }
 
            }
            else if (!playerShadowMode.isShadowActivated && useFog && Fog.activeSelf)
            {
                Fog.SetActive(false);
            }
            else if(playerShadowMode.isShadowActivated && isCharged && useFog && Fog.activeSelf)
            {
                Fog.SetActive(false);
            }
            
            //Stock le changement de position du pillier pour l'appliquer au fog
            if (useFog)
            {         
                if (pillarPos != new Vector2 (transform.position.x,transform.position.y) && !Fog.activeSelf)
                {
                    pillarMove.x += (transform.position.x - pillarPos.x);
                    pillarMove.y += (transform.position.y - pillarPos.y);

                    pillarPos = new Vector2(transform.position.x, transform.position.y);
                }
            }

            //Quand le player quitte le shadowMode, la charge revient.
            if (playerShadowMode.isCharged && myCharge && !playerShadowMode.isShadowActivated)
                Charge(true);

            //Le bool myCharge sert à lier une cgarge à un pillier lorsque celle ci n'est plus dessus
            if (!playerShadowMode.isCharged)
                myCharge = false;

            //Pour les sauvegarde
            if (isCharged && isCharge == 0)
            {
                isCharge = 1;
            }
            else if (!isCharged && isCharge == 1)
            {
                isCharge = 0;
            }

        }

        //Collisions avec le Shadow.
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col == shadowColliBox && isCharged && canInteract && !playerShadowMode.isCharged)
            {
                UnCharge(true);
            }
            if (col == shadowColliBox && !isCharged && canInteract && playerShadowMode.isCharged)
            {
                Charge(true);
            }
        }

        //détecte le collider du Shadow lorsque celle ci est acivée
        void GetShadow()
        {
            weGotShadow = true;
            shadowColliBox = PlayerManager.Instance.playerShadowMode.shadowObject.GetComponent<Collider2D>();
        }

        //fonction qui prend la charge du pillier, le paramètre signifie que l'action est le résultat de l'utilisation de la shadow ou pas
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

        //fonction qui donne la charge au pillier, le paramètre signifie que l'action est le résultat de l'utilisation de la shadow ou pas
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

        //délais avant que le joueur puisse inéragir avec le pillier
        IEnumerator ChargeCooldown()
        {
            yield return new WaitForSeconds(pillarCooldown);
            canInteract = true;
        }

        //si le joueur garde la charge trop longtemps, celle ci revient au pillier
        IEnumerator ChargeComeBack()
        {
            yield return new WaitForSeconds(timeBeforeChargeComeBack);

            if (playerShadowMode.isCharged)
                Charge(true);


        }

        //Cette fonction gère l'utilisation des pilliers sans la shadow
        public void CorruptionBeam(Vector2 direction)
        {
            //ces condition permettent de me faciliter la vie pour le raycast et l'instantiation du beam
            if (direction.x > 0)
                direction.x = 2;
            if (direction.x < 0)
                direction.x = -2;
            if (direction.y > 0)
                direction.y = 2;
            if (direction.y < 0)
                direction.y = -2;

            if (isCharged)
            {
                UnCharge(false);
                //la position d'origine du rayCast est un peu plus moin que le bord du collider du pillier
                RaycastHit2D hitPillar = Physics2D.Raycast(new Vector2(
                    (transform.position.x + colliBox.offset.x) + (colliBox.size.x * direction.x),
                    (transform.position.y + colliBox.offset.y) + (colliBox.size.y * direction.y)), direction, 3f, pillarLayer);

                beamDir = direction;
                //la position d'origine du beam est un peu plus moin que le bord du collider du pillier
                Instantiate(Beam, new Vector3(
                    (transform.position.x + colliBox.offset.x) + (colliBox.size.x * direction.x), 
                    (transform.position.y + colliBox.offset.y) + (colliBox.size.y * direction.y), 0f), Quaternion.identity, gameObject.transform);

                if (hitPillar.collider != null)
                {                
                    hitPillar.collider.GetComponent<Pillar>().Charge(false);
                }
                else if (hitPillar.collider == null)
                {
                    StartCoroutine(RaycastFalse());
                }

                //ce rayCast sert à stun les ennemis
                RaycastHit2D[] hitEnemis = Physics2D.RaycastAll(transform.position, direction, 3f, enemyLayer);

                foreach (RaycastHit2D hit in hitEnemis)
                {
                    hit.collider.GetComponent<EnemyBasicBehavior>().EnemyStun();
                }
            }

        }
        //retour de la charge si le raycast n'a rien touché
        IEnumerator RaycastFalse()
        {
            yield return new WaitForSeconds(3f);
            Charge(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, loadFogDistance);
        }
    }

}
