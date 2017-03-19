//---------------------------------------
// Author: Lee
// Date: 3/19/2017 3:57:55 PM
// Desc: EditorZUIBuild
//---------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ZGUI
{
    public class EditorZUIBuildManager
    {
        [MenuItem("Assets/ZUIBuild &b")]
        public static void ZUIBuild()
        {
            Object obj = null;
            Object[] objs = Selection.objects;
            if (null != objs && objs.Length == 1)
            {
                obj = objs[0];
            }

            if (null == obj)
            {
                return;
            }
            string strTargetPath = AssetDatabase.GetAssetPath(obj.GetInstanceID());
            ZUIBuild(strTargetPath);
        }

        public static void ZUIBuild(string strObjUrl)
        {
            if (string.IsNullOrEmpty(strObjUrl))
            {
                return;
            }

            strObjUrl = strObjUrl.Replace("\\", "/");

            EditorZUIBuild builder = null;
            if (strObjUrl.Contains(EditorZUIConstans.ATLAS_FOLDER_NAME))
            {
                builder = GetOrCreateBuilder((int)E_BuilderType.Atlas);
            }
            else if (strObjUrl.Contains(EditorZUIConstans.FONTS_FOLDER_NAME))
            {
                builder = GetOrCreateBuilder((int)E_BuilderType.Fonts);
            }
            else if (strObjUrl.Contains(EditorZUIConstans.PANEL_FOLDER_NAME))
            {
                builder = GetOrCreateBuilder((int)E_BuilderType.Panel);
            }
            else
            {
                Debug.LogWarning(strObjUrl + " is not ZUI resource !");
            }

            if(null != builder)
            {
                builder.CreateBundle(strObjUrl);
            }
        }

        private static EditorZUIBuild GetOrCreateBuilder(int eBuilderType)
        {
            EditorZUIBuild builder;
            if (!m_zuiBuilderMap.TryGetValue(eBuilderType, out builder))
            {
                switch (eBuilderType)
                {
                    case (int)E_BuilderType.Atlas: builder = new EditorZUIBuildAtlas(); break;
                    case (int)E_BuilderType.Fonts: builder = new EditorZUIBuildFonts(); break;
                    case (int)E_BuilderType.Panel: builder = new EditorZUIBuildPanel(); break;
                    default: Debug.LogError("Unknow ZUI resource type " + eBuilderType); break;
                }
                if (null != builder)
                {
                    m_zuiBuilderMap.Add(eBuilderType, builder);
                }
            }
            return builder;
        }

        enum E_BuilderType
        {
            Atlas,
            Fonts,
            Panel,
        }
        static Dictionary<int, EditorZUIBuild> m_zuiBuilderMap = new Dictionary<int, EditorZUIBuild>();
    }

    public abstract class EditorZUIBuild
    {
        public void CreateBundle(string strObjUrl)
        {
            string strOutputPath;
            string strBundleName;
            if(!EditorZUIConstans.GetBuildOutputInfoByLocalFile(strObjUrl, out strOutputPath, out strBundleName))
            {
                return;
            }

            Object obj = AssetDatabase.LoadAssetAtPath(strObjUrl, typeof(Object));
            AssetImporter objImporter = AssetImporter.GetAtPath(strObjUrl);
            objImporter.assetBundleName = strBundleName;

            if(!System.IO.Directory.Exists(strOutputPath))
            {
                System.IO.Directory.CreateDirectory(strOutputPath);
            }

            DoCustomAction(obj);
            BuildPipeline.BuildAssetBundles(strOutputPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

            objImporter.assetBundleName = string.Empty;
            Debug.LogWarning(strOutputPath + strBundleName);
        }

        protected virtual void DoCustomAction(Object obj)
        {

        }

    }
}
