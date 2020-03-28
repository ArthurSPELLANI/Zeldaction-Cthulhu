using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Boss
{
    public class Phase1Pattern1 : MonoBehaviour
    {
        public Transform[] anchor;
        public float moveSpeed;
        public float timeBeforeNextDash;
        private int dashNbr;
        private bool patternCanStart = false;
        private Vector2 vecDir;
        private Rigidbody2D bossPhase1Rb;
        private int currentAnchor;
        private bool isWaitingForNextAnchor;
        private bool pathIsChoosen = false;
        public List<int> anchorPath;

        private double AnchorXMin;
        private double AnchorXMax;
        private double AnchorYMin;
        private double AnchorYMax;


        void Awake()
        {
            bossPhase1Rb = GetComponentInParent<Rigidbody2D>();
            anchorPath = new List<int>();
        }

        void Start()
        {
            dashNbr = Random.Range(2, 4);
        }

        void Update()
        {
            //le pattern commence par le boss qui se replace sur le point situé au milieu de l'arène.
            if (patternCanStart == false)
            {
                vecDir = new Vector2(anchor[4].position.x - transform.position.x, anchor[4].position.y - transform.position.y).normalized;

                //Soft spot around the Anchor position cause rigidbody can't reach a precise position while using velocity to move.
                AnchorXMin = anchor[4].position.x - 0.04;
                AnchorXMax = anchor[4].position.x + 0.04;
                AnchorYMin = anchor[4].position.y - 0.04;
                AnchorYMax = anchor[4].position.y + 0.04;

                //tant que le boss n'a pas atteint le millieu de l'arène, il s'y déplace.
                if (transform.position.x >= AnchorXMin && transform.position.x <= AnchorXMax && transform.position.y >= AnchorYMin && transform.position.y <= AnchorYMax)
                {
                    patternCanStart = true;
                    currentAnchor = 4;
                    bossPhase1Rb.velocity = new Vector2(0, 0) * moveSpeed * Time.fixedDeltaTime;
                }
                else
                {
                    bossPhase1Rb.velocity = vecDir * moveSpeed * Time.fixedDeltaTime;
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

                StartCoroutine(DashToNextAnchor());
            }
        }

        IEnumerator DashToNextAnchor()
        {
            yield return new WaitForSeconds(timeBeforeNextDash);

            for (int i = 0; i < dashNbr; i++)
            {
                vecDir = new Vector2(anchor[anchorPath[i]].position.x - transform.position.x, anchor[anchorPath[i]].position.y - transform.position.y).normalized;
                yield return new WaitForSeconds(timeBeforeNextDash);

                //Soft spot around the Anchor position cause rigidbody can't reach a precise position while using velocity to move.
                AnchorXMin = anchor[i].position.x - 0.04;
                AnchorXMax = anchor[i].position.x + 0.04;
                AnchorYMin = anchor[i].position.y - 0.04;
                AnchorYMax = anchor[i].position.y + 0.04;

                while (transform.position.x <= AnchorXMin && transform.position.x >= AnchorXMax && transform.position.y <= AnchorYMin && transform.position.y >= AnchorYMax)
                {
                    bossPhase1Rb.velocity = vecDir * moveSpeed * Time.fixedDeltaTime;
                }

                bossPhase1Rb.velocity = new Vector2(0, 0) * moveSpeed * Time.fixedDeltaTime;
            }
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
    }
}

