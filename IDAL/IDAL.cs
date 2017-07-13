﻿ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace IDAL
{
   
	
	public partial interface ISys_LogDal :IBaseDal<Sys_Log>
    {
      
    }
	
	public partial interface ISys_UserDal :IBaseDal<Sys_User>
    {
      
    }
	
	public partial interface ISys_UserLogOnDal :IBaseDal<Sys_UserLogOn>
    {
      
    }
	
	public partial interface IZ_EquipmentDal :IBaseDal<Z_Equipment>
    {
      
    }
	
	public partial interface IZ_EquipmentLogDal :IBaseDal<Z_EquipmentLog>
    {
      
    }
	
	public partial interface IZ_RoomDal :IBaseDal<Z_Room>
    {
      
    }
	
}