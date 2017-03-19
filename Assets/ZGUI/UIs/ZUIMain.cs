//---------------------------------------
// Author: Lee
// Date: 3/19/2017 6:02:14 PM
// Desc: ZUIMain
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;

namespace ZGUI
{
    public class ZUIMain : ZUI
    {
        public override uint LayoutStyle()
        {
            return (uint)E_ZUILayoutStyle.Main;
        }
    }
}
