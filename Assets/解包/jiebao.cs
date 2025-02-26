using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class jiebao : EditorWindow
{
    [UnityEditor.MenuItem("Assets/����FBX�е�Animation")]
    private static void ExportAnimation()
    {
        // SelectionMode.DeepAssets: ���ѡ������ļ��У��򻹰����ļ��㼶��ͼ�и��ļ����µ�������Դ�����ļ��С�
        var gameObjects = Selection.GetFiltered<UnityEngine.Object>(UnityEditor.SelectionMode.DeepAssets);
        string path = "Assets/ExportAnimation/{0}.anim";

        // ����һ�����Animation���ļ���
        if (!AssetDatabase.IsValidFolder("Assets/ExportAnimation"))
            AssetDatabase.CreateFolder("Assets", "ExportAnimation");

        List<Object> animationClips = new List<Object>();
        for (int i = 0; i <= gameObjects.Length - 1; i++)
        {
            // AnimationUtility.GetAnimationClips()�������Լ�������Ϸ�������������Ķ����������顣�������ﲻ����
            // ʹ��AssetDatabase.LoadAllAssetsAtPath������ȡfbx�е�AnimationClip���ú�������һ��·����������fbx�ļ�����·����Ȼ�󷵻�һ��Object���͵����飬�����д�ŵ���fbx�ļ��е�������Դ
            var objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(gameObjects[i]));
            // ȡ�����е�AnimationClip
            foreach (var obj in objs)
            {
                //UnityEngine.PreviewAnimationClip���ڱ༭���в鿴��������ʱ�����������ڶ������߱༭���У����ָ�ʽ�磺__preview__Take 001��������Կ���һЩ������Ԥ��������
                //UnityEngine.AnimationClip������ʵ�ʲ��ŵĶ����������ü������Ա�������Ŀ�У�Ȼ����Animator��Animation������ز����š�
                if (obj is AnimationClip && !obj.name.Contains("__preview__"))//�ű���û��UnityEngine.PreviewAnimationClip����, ����������string.Contains�ж�
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

