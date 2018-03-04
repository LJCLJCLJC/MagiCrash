using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

public class CreateMesh:Editor
{

    [MenuItem("Tools/create")]
    static void Start1()
    {
        GameObject root = GameObject.Find("Root");

        if (root)
        {
            MeshRenderer[] meshRenderers = root.GetComponentsInChildren<MeshRenderer>();
            int i = 0;
            GameObject[] gos = new GameObject[meshRenderers.Length];
            for (i = 0; i < meshRenderers.Length; i++)
            {
                gos[i] = meshRenderers[i].gameObject;
            }

            string scenePath = EditorSceneManager.GetSceneAt(0).path;
            string meshScenePath = scenePath.Replace(".unity", "_mesh");
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), meshScenePath);
            string fullPath2 = fullPath.Replace("\\", "/");
            if (Directory.Exists(fullPath))
                Directory.Delete(fullPath, true);
            Directory.CreateDirectory(fullPath);
            string assetPath = FileUtil.GetProjectRelativePath(fullPath2);

            MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                i++;
            }

            root.transform.GetComponent<MeshFilter>().mesh = new Mesh();
            root.transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
            string meshPath = AssetDatabase.GetAssetPath(root.transform.GetComponent<MeshFilter>().sharedMesh);

            string path = Path.Combine(assetPath, Random.Range(int.MinValue, int.MaxValue) + ".asset");
            AssetDatabase.CreateAsset(root.transform.GetComponent<MeshFilter>().sharedMesh, path);

            AssetDatabase.Refresh();
            EditorSceneManager.MarkAllScenesDirty();
        }
        }

}