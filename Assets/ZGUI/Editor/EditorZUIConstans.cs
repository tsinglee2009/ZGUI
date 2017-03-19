//---------------------------------------
// Author: Lee
// Date: 3/19/2017 4:09:48 PM
// Desc: EditorZUIConstans
//---------------------------------------
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace ZGUI
{
    public class EditorZUIConstans
    {
        public const string ATLAS_FOLDER_NAME = "zui_atlas";
        public const string FONTS_FOLDER_NAME = "zui_fonts";
        public const string PANEL_FOLDER_NAME = "zui_panel";

        public const string ASSETS_FOLDER_NAME = "Assets";
        public const string BUNDLE_SUFFIX_NAME = ".unity3d";

        public static string GetBuildOutputRoot()
        {
            string strRoot = "Assets/game";
#if UNITY_ANDOIR || UNITY_IOS
            
#elif UNITY_STANDALONE

#endif
            return strRoot;
        }

        /// <summary>
        /// 获取打包后的输出文件夹目录和包名
        /// </summary>
        public static bool GetBuildOutputInfoByLocalFile(string strLocalFileUrl, out string strOutputFolder, out string strOutputName)
        {
            strOutputFolder = strOutputName = string.Empty;

            strLocalFileUrl = strLocalFileUrl.Replace("\\", "/");
            if(string.IsNullOrEmpty(strLocalFileUrl))
            {
                return false;
            }

            int index = strLocalFileUrl.IndexOf(ASSETS_FOLDER_NAME);
            if(index != -1)
            {
                strLocalFileUrl = strLocalFileUrl.Substring(index + ASSETS_FOLDER_NAME.Length);
            }
            else
            {
                //认为它是Assets下的目录
                if (!strLocalFileUrl.StartsWith("/"))
                {
                    strLocalFileUrl = "/" + strLocalFileUrl;
                }
            }

            index = strLocalFileUrl.LastIndexOf("/");
            strOutputFolder = strLocalFileUrl.Substring(0, index + 1);
            strOutputFolder = GetBuildOutputRoot() + strOutputFolder;

            strOutputName = strLocalFileUrl.Substring(index + 1);
            strOutputName = strOutputName.Substring(0, strOutputName.LastIndexOf(".")) + BUNDLE_SUFFIX_NAME;

            return true;
        }

    }
}
