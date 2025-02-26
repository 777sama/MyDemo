using GGG.Tool.Singleton;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace zzz
{
    public class GameObjActiveManager : Singleton<GameObjActiveManager>
    {
        public HashSet<string> DestroyedObjs = new HashSet<string>();

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        
    }
}
