using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WayPointCreator : Editor
{
    [MenuItem(@"GameObject/路点/创建路点字符串 #%Q", priority = 0)]
    public static void GetTransforms()
    {
        Transform[] transforms = Selection.transforms;
        TextEditor textEd = new TextEditor();
        string str = "";

        Transform temp;
        for(int i = 0; i < transforms.Length; i++)
        {
            for(int j = 0; j < transforms.Length - i - 1; j++)
            {
                if (int.Parse(transforms[j].name.Replace("point_","")) > int.Parse(transforms[j+1].name.Replace("point_", "")))
                {
                    temp = transforms[j + 1];
                    transforms[j + 1] = transforms[j];
                    transforms[j] = temp;
                }
            }
        }

        for (int i = 0; i < transforms.Length; i++)
        {
            str += (transforms[i].position.x + "#" + transforms[i].position.y + "#" + transforms[i].position.z + "|");
        }
        str = str.Remove(str.Length-1, 1);
        textEd.text = str;
        textEd.OnFocus();
        textEd.Copy();
    
    }
    [MenuItem(@"GameObject/路点/创建视图", priority = 0)]
    public static void SetTransforms()
    {
        string str = GUIUtility.systemCopyBuffer;
        List<Vector3> wayPointList = new List<Vector3>();
        string[] posStr = str.Split('|');
        for (int i = 0; i < posStr.Length; i++)
        {
            string[] pos = posStr[i].Split('#');
            wayPointList.Add(new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2])));
        }
        for (int i = 0; i < wayPointList.Count; i++)
        {

            GameObject point = new GameObject("point_" + i);
            point.transform.position = wayPointList[i];
            point.AddComponent<WayPointView>();
        }
    }
}

public class WayPointView : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}