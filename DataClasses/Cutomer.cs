using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace BeautyRegister.DataClasses
{
    [Table( Name = "Customer" )]
    public class Customer
    {
        [Column( Name = "ID" )]
        public long ID { get; set; }
        [Column( Name = "Name" )]
        public string Name { get; set; }
        [Column( Name = "Phone" )]
        public string Phone { get; set; }
        [Column( Name = "Image" )]
        public byte[] Image { get; set; }

        public string EMail { get; set; }

        public string Company { get; set; }

        public long? Birth { get; set; }  

        public long HairColorFK { get; set; }

        public string Note { get; set; }

        public string Hobby { get; set; }

        public long HairStyleFK { get; set; }
    }
}
