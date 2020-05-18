﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using Player;
using Game;
//et la c'est les usings (les bibliothèques pour trouver les machins en dessous)

public class TimelineManager : MonoBehaviour
{ //ici on déclare
    PlayableDirector Timeline;
    public GameObject[] Dialog;
    int Index; //on créer un Index
    int childIndex; // ici un 2nd Index
    public Sound[] Son;
    bool canSkip;
    bool desactivate;
    bool isplaying = true ;
    GameObject Movement;
    GameObject Attack;
    GameObject ShadowMode;

    GameObject Coeurs;
    GameObject Ammunitions;
    GameObject SanityGauge;
    GameObject Base;
    GameObject ActionPoints;


    void Start()
    {
        Timeline = GetComponent<PlayableDirector>();

        Movement = PlayerManager.Instance.playerMovement.gameObject;
        Attack = PlayerManager.Instance.playerAttack.gameObject;
        ShadowMode = PlayerManager.Instance.playerShadowMode.gameObject;

        Coeurs = UIManager.Instance.gameObject.transform.GetChild(0).gameObject;
        Ammunitions = UIManager.Instance.gameObject.transform.GetChild(1).gameObject;
        SanityGauge = UIManager.Instance.gameObject.transform.GetChild(2).gameObject;
        Base = UIManager.Instance.gameObject.transform.GetChild(3).gameObject;
        ActionPoints = UIManager.Instance.gameObject.transform.GetChild(4).gameObject;
    }
 
    void Update()
    {
        if (Input.GetButtonDown("Interract") && canSkip) // vérifie si le bool est true
            SkipDialog();

        if (Timeline.state == PlayState.Playing && !desactivate && isplaying ) // on regarde si la timeline est entrain d'etre joué et si les trucs en dessous sont désactivés.
        {
            //Debug.Log("Je suis la");
            desactivate = true; // desactivé est true
            Movement.SetActive(false);
            Attack.SetActive(false);
            ShadowMode.SetActive(false);

            Coeurs.SetActive(false);
            Ammunitions.SetActive(false);
            SanityGauge.SetActive(false);
            Base.SetActive(false);
            ActionPoints.SetActive(false);
        }

        else if (Timeline.state != PlayState.Playing && desactivate && !isplaying) // bah ici c'est l'inverse d'au dessus, si la timeline est pas joué et que les trucs sont pas ré activé
        {
            desactivate = false;
            Movement.SetActive(true);
            Attack.SetActive(true);
            ShadowMode.SetActive(true);

            Coeurs.SetActive(true);
            Ammunitions.SetActive(true);
            SanityGauge.SetActive(true);
            Base.SetActive(true);
            ActionPoints.SetActive(true);
        }

    }

    void SkipDialog() // c'est une fonction
    {
        if (childIndex == 0)
        {
            Dialog[Index].transform.GetChild(childIndex).gameObject.SetActive(true); //on appelle un enfant
            childIndex += 1; // on incrémente
        }

        else if (childIndex != 0)
        {
            if (childIndex == (Dialog[Index].transform.childCount)) // si on arrive au maximum du nbr d'enfant (childCount)
            {
                Dialog[Index].transform.GetChild(childIndex - 1).gameObject.SetActive(false);
                childIndex = 0; // On réinitialise le childIndex pour qu'il puisse recommencer de compter
                Index += 1; // On passe au parent suivant (le gros truc qui regroupe tout les enfants dialog Box)
                Debug.Log("Je suis la");
                Timeline.Resume(); //Resume est une fonction donc y'a des ()
                canSkip = false;
                isplaying = false;
            }

            else //else a pas besoin de parenthese
            {
                Dialog[Index].transform.GetChild(childIndex - 1).gameObject.SetActive(false); // on chercher l'enfant -1
                Dialog[Index].transform.GetChild(childIndex).gameObject.SetActive(true); // enfant actuel (pas besoin de mettre + 1)
                childIndex += 1; //on incrémente encore.
            }
        }
    }

    public void StartDialog ()
    {
        isplaying = true;
        Timeline.Pause();
        SkipDialog(); //on appelle la fonction 
        canSkip = true;
    }
}
