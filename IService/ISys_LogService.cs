﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClass.IService
{
    public partial interface ISys_LogService:IBaseService<Sys_Log>
    {
        bool AddLog(Sys_Log log);
    }
}
