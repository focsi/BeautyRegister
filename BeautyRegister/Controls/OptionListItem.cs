using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyRegister.Controls
{
    public class OptionListItem
    {
        public String DisplayValue { get; set; }

        public Int64 Value { get; set; }


        public OptionListItem()
        {
            DisplayValue = "";
            Value = 0;
        }

        public OptionListItem( String DisplayValue, Int64 Value )
        {
            this.DisplayValue = DisplayValue;
            this.Value = Value;

        }

        public override string ToString()
        {
            return DisplayValue;
        }
    }
}
