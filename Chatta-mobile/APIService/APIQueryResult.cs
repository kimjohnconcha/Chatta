using System;
using System.Collections.Generic;

namespace APIService
{
    public class APIQueryResult<TRecord> where TRecord : IAPIRecord
    {
        public int Total { get; set; }
        public List<TRecord> Data { get; set; }
    }
}
