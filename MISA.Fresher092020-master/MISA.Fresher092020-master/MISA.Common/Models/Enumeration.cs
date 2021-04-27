﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Common.Models
{
    public enum GenderEnum
    {
        Female = 0,
        Male = 1,
        Other = 2
    }

    /// <summary>
    /// Tình trạng công việc
    /// </summary>
    /// CreatedBy: NVMANH (13/10/2020)
    public enum WorkStatus
    {
        /// <summary>
        /// Đã nghỉ việc
        /// </summary>
        Stopped = 0,

        /// <summary>
        /// Đang làm việc
        /// </summary>
        Working = 1,

        /// <summary>
        /// Đang thử việc
        /// </summary>
        Waiting = 2
    }
}
