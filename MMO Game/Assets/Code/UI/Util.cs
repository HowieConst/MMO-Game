using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

    public class Util : MonoBehaviour
    {
        public static GameObject CreateCanvas(string managername,string tag)
        {
            GameObject managergo = GameObject.FindGameObjectWithTag(tag);
            if (managergo == null)
            {
                GameObject go = new GameObject(managername);
                go.tag = tag;
                Canvas canvas = go.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 0;
                CanvasScaler canvasscaler = go.AddComponent<CanvasScaler>();
                canvasscaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasscaler.referenceResolution = new Vector2(1280, 720);
                canvasscaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                go.AddComponent<GraphicRaycaster>();
                return go;
            }
            else
            {
                return managergo;
            }
        }
        public static GameObject FindChildDeep(GameObject rootgo,string childname)
        {
            if (rootgo.name == childname) return rootgo;
            Queue<GameObject> queue = new Queue<GameObject>();
            queue.Enqueue(rootgo);
            while(queue.Count > 0)
            {
                GameObject current = queue.Dequeue();
                int childcout = current.transform.childCount;
                for (int i = 0; i < childcout; i++)
                {
                    GameObject go = current.transform.GetChild(i).gameObject;
                    string name = go.name;
                    if (name == childname)
                    {
                        return go;
                    }
                    else
                    {
                        queue.Enqueue(go);
                    }
                }
            }
            return null;
        }
        public static void AttachGoToTarget(GameObject parentgo ,GameObject childgo,bool isprefab,bool isvisiblefollowparent)
        {
            GameObject go = null;
            if (isprefab)
            {
                go = Instantiate(childgo) as GameObject;
            }
            else
            {
                go = childgo;
            }
            if (parentgo != null)
            {
                go.transform.parent = parentgo.transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localRotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;
                if (isvisiblefollowparent)
                {
                    bool active = parentgo.activeSelf;
                    go.SetActive(active);
                }
                else
                {
                    go.SetActive(false);
                }
            }
           
        }

    }
