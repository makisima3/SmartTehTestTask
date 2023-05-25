using System;
using System.Collections.Generic;
using System.Linq;
using Code.Player.Shooting.Enums;
using UnityEngine;

namespace Code.Player.Configs
{
    [Serializable]
    public class TypeToColor
    {
        public BulletType Type;
        public Color Color;
    }

    [CreateAssetMenu(fileName = "BulletCollectablesConfig", menuName = "ScriptableObjects/Player/BulletCollectablesConfig", order = 0)]
    public class BulletCollectablesConfig : ScriptableObject
    {
        [SerializeField] private List<TypeToColor> TypeToColors;

        public Color GetColor(BulletType type)
        {
            return TypeToColors.First(ttc => ttc.Type == type).Color;
        }
    }
}