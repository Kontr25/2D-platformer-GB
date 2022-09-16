using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Animations
{
    [Serializable]
    public class SpritesSequence
    {
        public Track Track;
        public List<Sprite> Sprites = new List<Sprite>();
    }
}