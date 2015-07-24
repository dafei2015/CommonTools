using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class BuildAnimation : Editor
{
    //生成出的Prefab的路径
    private static string prefabPath = "Assets/Resources/Prefabs";
    //生成出的AnimationController 的路径
    private static string animationControllerPath = "Assets/AnimationController";
    //生成出的Animation 的路径
    private static string animationPath = "Assets/Animation";
    //美术的原图路径
    private static string imagePath = Application.dataPath + "/Raw";
    //图片格式
    private static string imageFormat = "PNG";
    //动画速度
    private static float frameTime = 0.1f;
    //动画帧率，30比好合适
    private static float frameRate = 30.0f;

    [MenuItem("Tools/Build/Build Animation")]
    private static void BuildAnimations()
    {
        DirectoryInfo raw = new DirectoryInfo(imagePath);
        foreach (DirectoryInfo dictorys in raw.GetDirectories())
        {
            List<AnimationClip> clips = new List<AnimationClip>();
            foreach (DirectoryInfo  dictoryAnimations in dictorys.GetDirectories())
            {
                //每个文件夹就是一组动画，将每个文件夹下的所有图片生成一个动画文件
                clips.Add(BuildAnimationClip(dictoryAnimations));
            }
            //把所有动画文件生成在一个animationController里
            AnimatorController controller = BuildAnimationController(clips, dictorys.Name);
            //生成Prefab文件
            BuildPrefab(dictorys, controller);
        }
    }

    //将图集生成动画文件
    private static AnimationClip BuildAnimationClip(DirectoryInfo dictoryAnimations)
    {
        string animationName = dictoryAnimations.Name;
        FileInfo[] images = dictoryAnimations.GetFiles("*." + imageFormat);
        AnimationClip clip = new AnimationClip();
        AnimationUtility.SetAnimationType(clip, ModelImporterAnimationType.Generic); //设置动画类型
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof (SpriteRenderer);
        curveBinding.path = "";
        curveBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] keyFrames = new ObjectReferenceKeyframe[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            Sprite sprite = Resources.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images[i].FullName));
            keyFrames[i] = new ObjectReferenceKeyframe();
            keyFrames[i].time = frameTime*i;
            keyFrames[i].value = sprite;
        }
        clip.frameRate = frameRate;
        if (animationName.IndexOf("idle") >= 0)
        {
            //设置idle动画为循环动画
            SerializedObject serializedClip = new SerializedObject(clip);
            AnimationClipSettings clipSettings =
                new AnimationClipSettings(serializedClip.FindProperty("m_AnimationClipSettings"));
            clipSettings.loopTime = true;
            serializedClip.ApplyModifiedProperties();
        }

        string parentName = System.IO.Directory.GetParent((dictoryAnimations.FullName)).Name;
        System.IO.Directory.CreateDirectory(animationPath + "/" + parentName);
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);
        AssetDatabase.CreateAsset(clip, animationPath + "/" + parentName + "/" + animationName + ".anim");
        AssetDatabase.SaveAssets();
        return clip;
    }

    private static string DataPathToAssetPath(string path)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            return path.Substring(path.IndexOf("Assets\\"));
        else
            return path.Substring(path.IndexOf("Assets/"));
    }

    private static AnimatorController BuildAnimationController(List<AnimationClip> clips, string name)
    {
        AnimatorController animatorController =
            AnimatorController.CreateAnimatorControllerAtPath(animationControllerPath + "/" + name + ".controller");
        AnimatorControllerLayer layer = animatorController.GetLayer(0);
        UnityEditorInternal.StateMachine sm = layer.stateMachine;
        foreach (AnimationClip newClip in clips)
        {
            State state = sm.AddState(newClip.name);
            state.SetAnimationClip(newClip, layer);
            Transition trans = sm.AddAnyStateTransition(state);
            trans.RemoveCondition(0);
        }
        AssetDatabase.SaveAssets();
        return animatorController;
    }

    private static void BuildPrefab(DirectoryInfo dictorys, AnimatorController controller)
    {
        //生成Prefab 添加一张预览用的sprite
        FileInfo images = dictorys.GetDirectories()[0].GetFiles("*." + imageFormat)[0];
        GameObject go = new GameObject();
        go.name = dictorys.Name;
        SpriteRenderer spriteRender = go.AddComponent<SpriteRenderer>();
        spriteRender.sprite = Resources.LoadAssetAtPath<Sprite>(DataPathToAssetPath(images.FullName));
        Animator animator = go.AddComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        PrefabUtility.CreatePrefab(prefabPath + "/" + go.name + ".prefab", go);
        Destroy(go);
    }

    private class AnimationClipSettings
    {
        private SerializedProperty m_Property;

        private SerializedProperty Get(string property)
        {
            return m_Property.FindPropertyRelative(property);
        }

        public AnimationClipSettings(SerializedProperty prop)
        {
            m_Property = prop;
        }

        public float startTime
        {
            get { return Get("m_StartTime").floatValue; }
            set { Get("m_StartTime").floatValue = value; }
        }

        public float stopTime
        {
            get { return Get("m_StopTime").floatValue; }
            set { Get("m_StopTime").floatValue = value; }
        }

        public float orientationOffsetY
        {
            get { return Get("m_OrientationOffsetY").floatValue; }
            set { Get("m_OrientationOffsetY").floatValue = value; }
        }

        public float level
        {
            get { return Get("m_Level").floatValue; }
            set { Get("m_Level").floatValue = value; }
        }

        public float cycleOffset
        {
            get { return Get("m_CycleOffset").floatValue; }
            set { Get("m_CycleOffset").floatValue = value; }
        }

        public bool loopTime
        {
            get { return Get("m_LoopTime").boolValue; }
            set { Get("m_LoopTime").boolValue = value; }
        }

        public bool loopBlend
        {
            get { return Get("m_LoopBlend").boolValue; }
            set { Get("m_LoopBlend").boolValue = value; }
        }

        public bool loopBlendOrientation
        {
            get { return Get("m_LoopBlendOrientation").boolValue; }
            set { Get("m_LoopBlendOrientation").boolValue = value; }
        }

        public bool loopBlendPositionY
        {
            get { return Get("m_LoopBlendPositionY").boolValue; }
            set { Get("m_LoopBlendPositionY").boolValue = value; }
        }

        public bool loopBlendPositionXZ
        {
            get { return Get("m_LoopBlendPositionXZ").boolValue; }
            set { Get("m_LoopBlendPositionXZ").boolValue = value; }
        }

        public bool keepOriginalOrientation
        {
            get { return Get("m_KeepOriginalOrientation").boolValue; }
            set { Get("m_KeepOriginalOrientation").boolValue = value; }
        }

        public bool keepOriginalPositionY
        {
            get { return Get("m_KeepOriginalPositionY").boolValue; }
            set { Get("m_KeepOriginalPositionY").boolValue = value; }
        }

        public bool keepOriginalPositionXZ
        {
            get { return Get("m_KeepOriginalPositionXZ").boolValue; }
            set { Get("m_KeepOriginalPositionXZ").boolValue = value; }
        }

        public bool heightFromFeet
        {
            get { return Get("m_HeightFromFeet").boolValue; }
            set { Get("m_HeightFromFeet").boolValue = value; }
        }

        public bool mirror
        {
            get { return Get("m_Mirror").boolValue; }
            set { Get("m_Mirror").boolValue = value; }
        }
    }
}