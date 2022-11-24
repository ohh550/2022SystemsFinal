using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReganAlchemy
{
    [CreateAssetMenu(menuName = "ConstructionTemplate")]
    public class ConstructionTemplate : ScriptableObject
    {
        public string stringID;

        public GameObject gamePrefab;
        public GameObject previewPrefab;

        public static bool operator ==(ConstructionTemplate constructionTemplate1, ConstructionTemplate constructionTemplate2)
        {
            return (constructionTemplate1.stringID == constructionTemplate2.stringID);
        }

        public static bool operator !=(ConstructionTemplate constructionTemplate1, ConstructionTemplate constructionTemplate2)
        {
            return (constructionTemplate1.stringID != constructionTemplate2.stringID);
        }
    }
}