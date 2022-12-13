using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ReganAlchemy
{
    public class MenuManagerAlchemy : MonoBehaviour
    {
        public void OnStartPressed()
        {
            SceneManager.LoadScene("Regan.Alchemy");
        }
    }
}

