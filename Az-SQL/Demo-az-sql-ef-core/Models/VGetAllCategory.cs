using System;
using System.Collections.Generic;

#nullable disable

namespace demo_1_az_sql_ef_core.Models
{
    public partial class VGetAllCategory
    {
        public string ParentProductCategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductCategoryId { get; set; }
    }
}
