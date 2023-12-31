﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDI.Crud.Domain.UserRepo.Models
{
    public class UserDTParamDomainModel
    {
        public int Skip { get; set; }

        public int PageSize { get; set; }

        public int Draw { get; set; }

        public string ColumnIndex { get; set; }

        public string SortDirection { get; set; }
    }
}
