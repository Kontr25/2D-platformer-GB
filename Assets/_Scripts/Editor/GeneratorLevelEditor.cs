using _Scripts.Generator;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    [CustomEditor(typeof(GenerateLevelView))]
    public class GeneratorLevelEditor : UnityEditor.Editor
    {
        private GenerateLevelController _generateLevelController;
        public bool _groupEnabled;
        private GenerateLevelView generateLevelView;


        private void OnEnable()
        {
            generateLevelView = (GenerateLevelView) target;
            _generateLevelController = new GenerateLevelController(generateLevelView);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            serializedObject.Update();

            var generateButton = GUILayout.Button("Generate");
            var clearButton = GUILayout.Button("Clear");

            if (generateButton)
                _generateLevelController.Awake();

            if (clearButton)
                _generateLevelController.ClearTileMap();

            _generateLevelController.HeightMap = EditorGUILayout.IntSlider("Высота",_generateLevelController.HeightMap, 10, 50);
            _generateLevelController.WightMap = EditorGUILayout.IntSlider("Ширина",_generateLevelController.WightMap, 10, 1000);
            _generateLevelController.FactorSmooth = EditorGUILayout.IntField("Сглаживание: ", _generateLevelController.FactorSmooth);
            _generateLevelController.RandomFillPercent = EditorGUILayout.IntSlider("Процент заполнения: ", 
                _generateLevelController.RandomFillPercent, 1, 100);

            
            GUILayout.Space(40);
            serializedObject.ApplyModifiedProperties();
        }
    }
    
}