using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    public class Phase1Pattern1 : MonoBehaviour
    {
        public Transform[] anchor;
        private int dashNbr;
        private bool patternCanStart = false;
        private Vector2 vecDir;
        Vector2 vecAnim;
        private Rigidbody2D bossPhase1Rb;
        private int currentAnchor;
        private bool isWaitingForNextAnchor;
        private bool pathIsChoosen = false;

        public List<int> anchorPath;

        private bool canMove = true;

        public GameObject bossBullet;

        public float moveSpeed;
        public float timebeforeFirstDash;
        public float timeBeforeNextDash;
        public float timeBeforeFiring;
        public float timeBeforeWeakStateBegin;
        public float timeBeforeWeakStateEnd;
        
        private double AnchorXMin;
        private double AnchorXMax;
        private double AnchorYMin;
        private double AnchorYMax;

        public Animator animator;


        void Awake()
        {
            
        }

        void Start()
        {
 
        }

        void OnEnable()
        {
            canMove = true;
            pathIsChoosen = false;
            patternCanStart = false;

            bossPhase1Rb = GetComponentInParent<Rigidbody2D>();
            anchorPath = new List<int>();

            dashNbr = Random.Range(2, 4);
      
        }

        void Update()
        {



            //le pattern commence par le boss qui se replace sur le point situé au milieu de l'arène.
            if (patternCanStart == false)
            {                
                vecDir = new Vector2(anchor[4].position.x - transform.position.x, anchor[4].position.y - transform.position.y).normalized;
                SetAnimDirection(vecDir);
                animator.SetFloat("Horizontal", vecAnim.x);
                animator.SetBool("isDrifting", true);

                //Soft spot around the Anchor position cause rigidbody can't reach a precise position while using velocity to move.
                AnchorXMin = anchor[4].position.x - 0.08;
                AnchorXMax = anchor[4].position.x + 0.08;
                AnchorYMin = anchor[4].position.y - 0.08;
                AnchorYMax = anchor[4].position.y + 0.08;

                //tant que le boss n'a pas atteint le millieu de l'arène, il s'y déplace.
                if (transform.position.x >= AnchorXMin && transform.position.x <= AnchorXMax && transform.position.y >= AnchorYMin && transform.position.y <= AnchorYMax)
                {
                    animator.SetBool("isDrifting", false);                    
                    patternCanStart = true;
                    currentAnchor = 4;
                    bossPhase1Rb.velocity = new Vector2(0, 0) * moveSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    animator.SetBool("isDrifting", true);
                    bossPhase1Rb.velocity = vecDir * moveSpeed * Time.fixedDeltaTime;
                    SetAnimDirection(vecDir);
                    animator.SetFloat("Horizontal", vecAnim.x);

                }
            }

            //une fois le milieu de l'arène atteint, le pattern commence avec la selection aléatoire du chemin (voir ChoosePath()). 
            //le Boss exécute ensuite ses dash en suivant le chemin puis envoie un projectile à la fin.
            else
            {
                if (pathIsChoosen == false)
                {
                    ChoosePath();
                    pathIsChoosen = true;
                }

                if(canMove == true)
                {
                    canMove = false;
                    StartCoroutine(DashToNextAnchor());
                }
                
            }
        }

        IEnumerator DashToNextAnchor()
        {
            yield return new WaitForSeconds(timebeforeFirstDash);

            for (int i = 0; i < dashNbr; i++)
            {
                vecDir = new Vector2(anchor[anchorPath[i]].position.x - transform.position.x, anchor[anchorPath[i]].position.y - transform.position.y).normalized;
                animator.SetBool("isDrifting", true);
                SetAnimDirection(vecDir);
                animator.SetFloat("Horizontal", vecAnim.x);


                yield return new WaitForSeconds(timeBeforeNextDash);

                //Soft spot around the Anchor position cause rigidbody can't reach a precise position while using velocity to move.
                AnchorXMin = anchor[anchorPath[i]].position.x - 0.04;
                AnchorXMax = anchor[anchorPath[i]].position.x + 0.04;
                AnchorYMin = anchor[anchorPath[i]].position.y - 0.04;
                AnchorYMax = anchor[anchorPath[i]].position.y + 0.04;

                //tant que le boss n'a pas atteint le prochain point d'ancrage situé dans la liste, il se déplace vers se dernier
                while (transform.position.x <= AnchorXMin || transform.position.x >= AnchorXMax || transform.position.y <= AnchorYMin || transform.position.y >= AnchorYMax)
                {
                    bossPhase1Rb.velocity = vecDir * moveSpeed * Time.fixedDeltaTime;
                    animator.SetBool("isDrifting", true);
                    SetAnimDirection(vecDir);
                    animator.SetFloat("Horizontal", vecAnim.x);

                    yield return null;
                }

                //une fois le point d'ancrage atteint, le boss s'arrête.
                bossPhase1Rb.velocity = new Vector2(0, 0) * moveSpeed * Time.fixedDeltaTime;
                animator.SetBool("isDrifting", false);
            }

            animator.SetBool("isShooting", true);
            yield return new WaitForSeconds(timeBeforeFiring);

            animator.SetBool("isShooting", false);
            Instantiate(bossBullet, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBeforeWeakStateBegin);

            animator.SetBool("isWeak", true);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = true;

            yield return new WaitForSeconds(timeBeforeWeakStateEnd);

            animator.SetBool("isWeak", false);
            transform.parent.GetComponentInParent<BossBaseBehavior>().isWeak = false;

            GetComponent<Phase1PatternManager>().NextPatternSelection();

        }

        /// <summary>
        /// Défini le chemin que va parcourir le boss avant de lancer son projectile. Chaque point possède des embranchement différents et le chemin est renvoyé dans la liste anchorPath.
        /// </summary>
        private void ChoosePath()
        {
            for (int i = 0; i < dashNbr; i++)
            {
                #region Beaucoup de lignes pour faire de l'aléatoire sur la selection du chemin.
                if (currentAnchor == 0)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {
                        anchorPath.Add(1);
                        currentAnchor = 1;
                    }
                    else if (random == 1)
                    {
                        anchorPath.Add(2);
                        currentAnchor = 2;
                    }
                    else if (random == 2)
                    {
                        anchorPath.Add(4);
                        currentAnchor = 4;
                    }
                }

                else if (currentAnchor == 1)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {
                        anchorPath.Add(0);
                        currentAnchor = 0;
                    }
                    else if (random == 1)
                    {
                        anchorPath.Add(3);
                        currentAnchor = 3;
                    }
                    else if (random == 2)
                    {
                        anchorPath.Add(4);
                        currentAnchor = 4;
                    }

                }

                else if (currentAnchor == 2)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {
                        anchorPath.Add(0);
                        currentAnchor = 0;
                    }
                    else if (random == 1)
                    {
                        anchorPath.Add(3);
                        currentAnchor = 3;
                    }
                    else if (random == 2)
                    {
                        anchorPath.Add(4);
                        currentAnchor = 4;
                    }
                }

                else if (currentAnchor == 3)
                {
                    int random = Random.Range(0, 3);

                    if (random == 0)
                    {
                        anchorPath.Add(1);
                        currentAnchor = 1;
                    }
                    else if (random == 1)
                    {
                        anchorPath.Add(2);
                        currentAnchor = 2;
                    }
                    else if (random == 2)
                    {
                        anchorPath.Add(4);
                        currentAnchor = 4;
                    }

                }

                else if (currentAnchor == 4)
                {
                    int random = Random.Range(0, 4);

                    if (random == 0)
                    {
                        anchorPath.Add(0);
                        currentAnchor = 0;
                    }
                    else if (random == 1)
                    {
                        anchorPath.Add(1);
                        currentAnchor = 1;
                    }
                    else if (random == 2)
                    {
                        anchorPath.Add(2);
                        currentAnchor = 2;
                    }
                    else if (random == 3)
                    {
                        anchorPath.Add(3);
                        currentAnchor = 3;
                    }

                }
                #endregion
            }
        }

        public void SetAnimDirection(Vector2 vecDir)
        {
            if (vecDir.x > 0)
            {
                vecAnim.x = 1;
            }
            else
            {
                vecAnim.x = -1;
            }
        }

    }
}

