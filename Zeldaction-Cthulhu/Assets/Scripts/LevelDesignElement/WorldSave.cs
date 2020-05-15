using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PillarSystem;
using UnityEngine.SceneManagement;

public class WorldSave : MonoBehaviour
{
    //salut
    /*
     * Ce script sert à sauvegrder les pilliers et les shadowFragements
     * il faut renseigner chaque shadowFragements et Chaque pilliers dans le bon tableau dans l'inspector
     * Les fonctions de save sont lancés dans le script "SaveZone"
     * les variables sauvegardées le sont sous le nom de "numéro de la scene + numéro du pillier"
     * par exemple "scene1 pillar1
     */


    public GameObject[] Pillar;
    public GameObject[] Fragement;
    public GameObject[] Cinematic;
    Scene CurrentScene;

    int index;
    int index2;
    int index3;

    //La fonction Awake Parcours tous les tableaux assigne les valeurs sauvegardées aux éléments
    void Awake()
    {
        CurrentScene = SceneManager.GetActiveScene();

        //Assignation des charges aux pilliers sauvegardés
        foreach  (GameObject p in Pillar)
        {
            p.GetComponent<Pillar>().isCharge = PlayerPrefs.GetInt("scene" + CurrentScene.buildIndex.ToString() + " pillar" + index.ToString(), p.GetComponent<Pillar>().isCharge);
            index += 1;
        }
        index = 0;

        //Activation des fragements
        foreach (GameObject f in Fragement)
        {
            f.GetComponent<PickupFragment>().exist = PlayerPrefs.GetInt("scene" + CurrentScene.buildIndex.ToString() + " fragement " + index2.ToString(), f.GetComponent<PickupFragment>().exist);
            index2 += 1;
        }
        index2 = 0;

        //Désactivation des cinématiques déjà jouées
        foreach (GameObject c in Cinematic)
        {
            int i = PlayerPrefs.GetInt("scene" + CurrentScene.buildIndex.ToString() + " cinematic" + index3.ToString(), 1);

            if (i == 0)
            {
                c.SetActive(false);
            }
            index3 += 1;
        }
        index3 = 0;
    }

    //save des charges des pilliers
    public void SavePillar()
    {
        foreach (GameObject p in Pillar)
        { 
            PlayerPrefs.SetInt("scene" + CurrentScene.buildIndex.ToString() + " pillar" + index.ToString(), p.GetComponent<Pillar>().isCharge);
            index += 1;
        }
        index = 0;

        PlayerPrefs.Save();
    }
    //save de l'existeance des fragements
    public void SaveFragment()
    {
        foreach (GameObject f in Fragement)
        {
            PlayerPrefs.SetInt("scene" + CurrentScene.buildIndex.ToString() + " fragement " + index2.ToString(), f.GetComponent<PickupFragment>().exist);
            index2 += 1;
        }
        index2 = 0;

        PlayerPrefs.Save();
    }
    //Save de l'avtivation des cinématiques
    public void SaveGameObjectActivation()
    {
        foreach (GameObject c in Cinematic)
        {
            if (c.activeSelf)
            {
                PlayerPrefs.SetInt("scene" + CurrentScene.buildIndex.ToString() + " cinematic" + index3.ToString(), 1);
                index3 += 1;
            }
            else if (!c.activeSelf)
            {
                PlayerPrefs.SetInt("scene" + CurrentScene.buildIndex.ToString() + " cinematic" + index3.ToString(), 0);
                index3 += 1;
            }
            else
            {
                Debug.LogError("Le gameObject " + c.name + " n'est n' actif ni inactif (c'est trop bizarre");
            }
            index3 = 0;
        }

    }
}