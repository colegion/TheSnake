using UnityEditor;
using UnityEngine;

namespace LevelDesign
{
    [CustomEditor(typeof(LevelEditor))]
    public class CustomInspector : Editor
    {
        private LevelEditor _levelEditor;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _levelEditor = (LevelEditor)target;
            
            ConfigureGridSizeFields();
            ConfigureCreateGridButton();
        }
        
        
        private void ConfigureGridSizeFields()
        {
            _levelEditor.width = EditorGUILayout.IntField("Grid Width", _levelEditor.width);
            _levelEditor.height = EditorGUILayout.IntField("Grid Height", _levelEditor.height);
        }

        private void ConfigureCreateGridButton()
        {
            if (GUILayout.Button("Create Grid"))
            {
                _levelEditor.CreateGrid();
            }
        }
    }
}
