﻿using System;
using System.Collections.Generic;

namespace service;

public partial class TblAssist
{
    public int TableCode { get; set; }

    public string? TableName { get; set; }

    public string? TableDescription { get; set; }

    public virtual ICollection<TblAssistValue> TblAssistValues { get; set; } = new List<TblAssistValue>();
}
