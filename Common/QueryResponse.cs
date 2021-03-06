﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class QueryResponse<T> : Response
    {
        public List<T> Data { get; set; }
    }

    public class SingleResponse<T>: Response
    {
        public T Data { get; set; }
    }
}
