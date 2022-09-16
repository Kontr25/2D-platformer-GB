using System.Collections.Generic;
using UnityEngine;



namespace _Scripts.Animations
{
    [CreateAssetMenu(fileName = "SpriteAnimationsConfig", menuName = "Configs/SpriteAnimationsConfig", order = 1)]
    public class SpriteAnimationsConfig: ScriptableObject
    {
        [SerializeField] 
        private List<SpritesSequence> _sequences;
        
        public List<SpritesSequence> Sequences => _sequences;
    }
}