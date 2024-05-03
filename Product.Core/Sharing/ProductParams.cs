using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core.Sharing
{
    public class ProductParams
    {
        public int maxpagesize { get; set; } = 5;
        public int pagesize  = 3;
        public int Pagesize { get => pagesize; set => pagesize = value > maxpagesize ? maxpagesize : value; }

        public int PageNumber { get;set; } =1;

        public int? Categoryid { get; set; }
        public string Sorting { get; set; }

        private string _Search;
        public string Search
        {
            get => _Search;
            set => _Search = value.ToLower();
    }
    }

  
}
