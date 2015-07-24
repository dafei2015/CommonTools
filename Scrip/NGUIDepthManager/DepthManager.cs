using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Others
{
    /// <summary>
    /// NGUI的Depth管理
    /// </summary>
    public class DepthManager
    {
        private const string PREFAB_PATH = "Windows/";

        private const int INIT_DEPTH = -1; //初始的depth
        private static int depth = INIT_DEPTH; //当前的层数

        //存储已经打开过的窗体
        private static Dictionary<string, GameObject> openedWindows = new Dictionary<string, GameObject>();
        private static Dictionary<string, int> winNameToDepth = new Dictionary<string, int>();
        private static Dictionary<string, int> winNameToLastDepth = new Dictionary<string, int>();

        public static void ClearDepth()
        {
            depth = INIT_DEPTH;
        }

        /// <summary>
        /// 打开一个窗体，并返还对应的组件
        /// </summary>
        /// <typeparam name="T">想要返回的组件</typeparam>
        /// <param name="parent">窗体的父类</param>
        /// <param name="windowName">窗体的名称</param>
        /// <returns></returns>
        public static T OpenWindowAndGetComponent<T>(GameObject parent, string windowName) where T : Component
        {
            GameObject go = OpenWindow(parent, windowName);
            return go == null ? null : go.GetComponent<T>();
        }

        /// <summary>
        /// 打开一个窗体
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        private static GameObject OpenWindow(GameObject parent, string windowName)
        {
            GameObject go = null;
            if (openedWindows.ContainsKey(windowName))
            {
                go = openedWindows[windowName];
            }

            if (go == null)
            {
                GameObject windowPrefab = Resources.Load(PREFAB_PATH + windowName) as GameObject;
                if (windowPrefab == null)
                {
                    Debug.Log(string.Format("can not found such window {0} to open", windowName));
                    return null;
                }

                //将windowName放到parent下做为子物体
                go = NGUITools.AddChild(parent, windowPrefab);
                go.name = windowName;
                openedWindows.Add(windowName, go);
            }

            SetWindowDepth(go, windowName); // 设置GameObject的depth的值
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// ???????????????????????????????
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T OpenWindowAndGetComponent<T>(GameObject parent) where T : Component
        {
            return OpenWindowAndGetComponent<T>(parent, typeof (T).Name);
        }

        /// <summary>
        /// 创建一个新的空游戏体，并包含T 组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public static T CreateEmptyWindow<T>(GameObject parent, string windowName) where T : Component
        {
            GameObject go = null;
            if (openedWindows.ContainsKey(windowName))
            {
                go = openedWindows[windowName];
            }
            else
            {
                go = new GameObject();  //创建一个新的游戏体，并加入到已打开的字典中
                openedWindows.Add(windowName,go);
            }

            go.name = windowName;
            go.AddComponent<UIPanel>();
            go.transform.parent = parent.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = parent.transform.lossyScale;
            go.transform.localRotation = Quaternion.identity;

            SetWindowDepth(go,windowName);
            return go.AddComponent<T>();

        }

        public static void CloseWindow(string windowName)
        {
            if (openedWindows.ContainsKey(windowName))
            {
                GameObject.Destroy(openedWindows[windowName]);
                openedWindows.Remove(windowName);

                int winDepth = winNameToDepth[windowName];

                if (winDepth == depth)
                {
                    depth = winNameToLastDepth[windowName];
                    winNameToDepth.Remove(windowName);
                    winNameToLastDepth.Remove(windowName);
                }
                else
                {
                    int minDiff = -1;
                    string name = string.Empty;

                    //取得depth相差最小的物体
                    foreach (KeyValuePair<string,int> keyValuePair in winNameToDepth )
                    {
                        if (winDepth<keyValuePair.Value)
                        {
                            int diff = keyValuePair.Value - winDepth;
                            if (minDiff == -1 || minDiff > diff)
                            {
                                minDiff = diff;
                                name = keyValuePair.Key;
                            }
                        }
                    }

                    //找到最近的物体，然后将其设为要删除物体的深度
                    if (name != null)
                    {
                        winNameToLastDepth[name] = winNameToLastDepth[windowName];
                        winNameToLastDepth.Remove(windowName);
                        winNameToDepth.Remove(windowName);
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("Can not found such window {0} to close ", windowName));
            }
        }

        /// <summary>
        /// 设置depth的深度
        /// </summary>
        /// <param name="go"></param>
        /// <param name="windowName"></param>
        private static void SetWindowDepth(GameObject go, string windowName)
        {
            depth = CheckDepth(go);
            if (winNameToLastDepth.ContainsKey(windowName))
            {
                winNameToLastDepth[windowName] = depth;
            }
            else
            {
                winNameToLastDepth.Add(windowName,depth);
            }

            depth = OffsetDepth(go, depth);

            if (winNameToDepth.ContainsKey(windowName))
            {
                winNameToDepth[windowName] = depth;
            }
            else
            {
                winNameToDepth.Add(windowName, depth);
            }
        }

        /// <summary>
        /// 将go游戏体所有plane全部加1
        /// </summary>
        /// <param name="go"></param>
        /// <param name="i">当前的depth</param>
        /// <returns></returns>
        private static int OffsetDepth(GameObject go, int idepth)
        {
            UIPanel[] ps = go.GetComponentsInChildren<UIPanel>();
            int maxDepth = 0;
            foreach (UIPanel p in ps)
            {
                p.depth += depth + 1;
                if (p.depth>maxDepth)
                {
                    maxDepth = p.depth;
                }
            }
            return maxDepth;
        }

        /// <summary>
        /// 获得所有的Plane的depth
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        private static int CheckDepth(GameObject go)
        {
            if (depth == INIT_DEPTH)
            {
                GameObject root = NGUITools.GetRoot(go);
                UIPanel[] ps = root.GetComponentsInChildren<UIPanel>();
                int maxDepth = -1;
                foreach (UIPanel p in ps)
                {
                    if (p.depth > maxDepth)
                    {
                        maxDepth = p.depth;
                    }
                }
                depth = maxDepth;
            }

            return depth;
        }
    }
}