﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using AudioManaging;

namespace Menu
{
    public class SelectedButtonScript : MonoBehaviour
    {
        public GameObject myButton;





        public void ChangeSelectedButton()
        {
            EventSystem.current.SetSelectedGameObject(myButton);
        }

        
    }
}

