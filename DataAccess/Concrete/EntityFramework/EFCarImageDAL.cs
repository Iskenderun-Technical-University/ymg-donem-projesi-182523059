﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCarImageDAL : EFEntityRepositoryBase<CarImage, ReCapDbContext>, ICarImageDAL
    {
    }
}
