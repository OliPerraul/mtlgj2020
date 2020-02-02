using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MTLGJ
{

    [System.Serializable]
    public class Group
    {
        // If difficuty non neg override wave
        [SerializeField]
        public float Difficulty = -1f;

        [SerializeField]
        public float Frequency = 1f;

        [SerializeField]
        public float Pause = 1f;

        // TODO random enemy
        [SerializeField]
        public List<Enemy> Enemies;
    }

    [System.Serializable]
    public class Wave
    {
        [SerializeField]
        public float Difficulty = 1f;

        [SerializeField]
        public List<Group> Groups;
    }

    [System.Serializable]
    public class SessionSettings
    {
        [SerializeField]
        public int MaxLives = 10;

        [SerializeField]
        public int MaxResourcesAmount = 9999;

        [SerializeField]
        public int InitialResourcesAmount = 100;

        [SerializeField]
        public List<Wave> Waves;
    };


    public class GameResources : Cirrus.Resources.BaseResourceManager<GameResources>
    {       
        [SerializeField]
        public SessionSettings SessionSettings;
    }
}