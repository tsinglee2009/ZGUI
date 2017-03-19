//---------------------------------------
// Author: Lee
// Date: 3/19/2017
// Desc: RoleModule
//---------------------------------------
using UnityEngine;
using System.Collections.Generic;
using System;

namespace ZGUI
{
    public class RoleModule : LogicModule
    {
        public ZUIRole m_ui;
        protected override ZUI UI
        {
            get
            {
                return m_ui;
            }
            set
            {
                m_ui = (ZUIRole)value;
            }
        }
    }
}
