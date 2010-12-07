using System;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // TabItem Class
    //
    // This class encapsulates the basic attributes of a Tab, and is used
    // by the administration pages when manipulating tabs.  TabItem implements 
    // the IComparable interface so that an ArrayList of TabItems may be sorted
    // by TabOrder, using the ArrayList's Sort() method.
    //
    //*********************************************************************

    public class TabItem : IComparable
    {
        private int _tabOrder;
        private String _name;
        private int _id;

        public int TabOrder
        {
            get { return _tabOrder; }
            set { _tabOrder = value; }
        }

        public String TabName
        {
            get { return _name; }
            set { _name = value; }
        }

        public int TabId
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((TabItem) value).TabOrder;

            if (TabOrder == compareOrder) return 0;
            if (TabOrder < compareOrder) return -1;
            if (TabOrder > compareOrder) return 1;
            return 0;
        }
    }
}