using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        private Dialogue selectedDialogue = null;
        private GUIStyle nodeStyle;
        private DialogueNode draggingNode = null;

        private Vector2 draggingOffset;

        [MenuItem("Window/DialogueEditor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor", true);
        }
        
        //Open Editor from Asset
        [OnOpenAsset(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;

            if (dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = Texture2D.grayTexture;
        }

        private void OnSelectionChanged()
        {
            //check if selected object is of type dialogue
            Dialogue currentSelection = Selection.activeObject as Dialogue;
            
            //if current selection is of type Dialogue set selectedDialogue and Repaint the GUI
            if (currentSelection != null)
            {
                selectedDialogue = currentSelection;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No dialogue selected");
            }
            else
            {
                EditorGUILayout.LabelField(selectedDialogue.name);
                
                foreach (DialogueNode node in selectedDialogue.GetNodes())
                {
                    OnGUINode(node);
                }
                
            }

            MouseEvents();
        }

        private void MouseEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                // get node that mouse is currently clicking on
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                
                //offset for mouse position when dragging node
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Update node position");
                //Move selected node 
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            
        }
        
        private DialogueNode GetNodeAtPoint(Vector2 mousePoint)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetNodes())
            {
                if (node.rect.Contains(mousePoint))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }

        private void OnGUINode(DialogueNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();
                    
            EditorGUILayout.LabelField("Node:");
            string newText = EditorGUILayout.TextField(node.text);
            string newID = EditorGUILayout.TextField(node.id);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialogue text");
                
                node.text = newText;
                node.id = newID;
            }
            
            GUILayout.EndArea();
        }
    }
}