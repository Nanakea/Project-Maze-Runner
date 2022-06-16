using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu(menuName = "PortableWordList")]
    public class PortableWordList : ScriptableObject
    {
        public Word[] value;
    }
}