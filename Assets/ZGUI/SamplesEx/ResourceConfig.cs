//---------------------------------------
// Desc: Just A Download Sample Script
//---------------------------------------
class ResourceConfig
{
    public static uint GetResIdByName(string strResName)
    {
        switch(strResName)
        {
            case "panel_main": return 10;
            case "panel_role": return 11;
            case "panel_bag": return 12;
        }
        return 0;
    }

    public static string GetResPathByID(uint resId)
    {
        switch(resId)
        {
            case 10: return "http://192.168.1.103/panel_main.unity3d";
            case 11: return "http://192.168.1.103/panel_role.unity3d";
            case 12: return "http://192.168.1.103/panel_bag.unity3d";
        }
        return "";
    }
}
