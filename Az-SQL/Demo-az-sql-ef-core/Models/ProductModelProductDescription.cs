using System;
using System.Collections.Generic;

#nullable disable

namespace demo_1_az_sql_ef_core.Models
{
    public partial class ProductModelProductDescription
    {
        public int ProductModelId { get; set; }
        public int ProductDescriptionId { get; set; }
        public string Culture { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}
