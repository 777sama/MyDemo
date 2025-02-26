using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zzz
{
    [CreateAssetMenu(fileName = "PackageTable", menuName = "Custom/Assets/PackageTable")]
    public class PackageTable : ScriptableObject
    {
        public List<PackageTableItem> DataList = new List<PackageTableItem>();
    }

    [Serializable]
    public class PackageTableItem
    {
        public string uid;
        public int id;
        public int type;
        public int star;
        public string name;
        public string description;
        public string skillDescription;
        public string imagePath;
        public int num;

    }
}
