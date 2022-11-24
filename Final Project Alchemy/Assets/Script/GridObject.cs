using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    public class GridObject
    {
        public Vector2Int position;
        public GameObject gameObject;

        public GridObject(Vector2Int position, GameObject gameObject)
        {
            this.position = position;
            this.gameObject = gameObject;
        }
    }
}