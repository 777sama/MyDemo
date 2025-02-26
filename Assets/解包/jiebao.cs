using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class jiebao : EditorWindow
{
    [UnityEditor.MenuItem("Assets/导出FBX中的Animation")]
    private static void ExportAnimation()
    {
        // SelectionMode.DeepAssets: 如果选择包含文件夹，则还包括文件层级视图中该文件夹下的所有资源和子文件夹。
        var gameObjects = Selection.GetFiltered<UnityEngine.Object>(UnityEditor.SelectionMode.DeepAssets);
        string path = "Assets/ExportAnimation/{0}.anim";

        // 创建一个存放Animation的文件夹
        if (!AssetDatabase.IsValidFolder("Assets/ExportAnimation"))
            AssetDatabase.CreateFolder("Assets", "ExportAnimation");

        List<Object> animationClips = new List<Object>();
        for (int i = 0; i <= gameObjects.Length - 1; i++)
        {
            // AnimationUtility.GetAnimationClips()方法可以检索与游戏对象或组件关联的动画剪辑数组。但是这里不适用
            // 使用AssetDatabase.LoadAllAssetsAtPath函数提取fbx中的AnimationClip，该函数接收一个路径参数，即fbx文件所在路径，然后返回一个Object类型的数组，数组中存放的是fbx文件中的所有资源
            var objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(gameObjects[i]));
            // 取出其中的AnimationClip
            foreach (var obj in objs)
            {
                //UnityEngine.PreviewAnimationClip是在编辑器中查看动画的临时剪辑，比如在动画曲线编辑器中（名字格式如：__preview__Take 001），你可以看到一些动画的预览剪辑。
                //UnityEngine.AnimationClip是最终实际播放的动画剪辑，该剪辑可以保存在项目中，然后由Animator或Animation组件加载并播放。
                if (obj is AnimationClip && !obj.name.Contains("__preview__"))//脚本中没有UnityEngine.PreviewAnimationClip类型, 所以这里用string.Contains判断
                {
                    animationClips.Add(obj);
                }
            }
        }

        foreach (AnimationClip Clip in animationClips)
        {
            Object newClip = new AnimationClip();
            EditorUtility.CopySerialized(Clip, newClip);
            newClip.name = Clip.name;
            AssetDatabase.CreateAsset(newClip, string.Format(path, newClip.name));
        }
    }
}

