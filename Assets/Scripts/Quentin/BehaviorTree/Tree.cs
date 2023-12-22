using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {

        protected abstract Node SetupTree();

        protected abstract void Init();
    }
}
