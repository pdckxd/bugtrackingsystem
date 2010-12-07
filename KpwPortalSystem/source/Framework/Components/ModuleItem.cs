using System;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // ModuleItem Class
    //
    // This class encapsulates the basic attributes of a Module, and is used
    // by the administration pages when manipulating modules.  ModuleItem implements 
    // the IComparable interface so that an ArrayList of ModuleItems may be sorted
    // by ModuleOrder, using the ArrayList's Sort() method.
    //
    //*********************************************************************

    public class ModuleItem : IComparable
    {
        private int _moduleOrder;
        private String _title;
        private String _pane;
        private int _id;
        private int _defId;

        public int ModuleOrder
        {
            get { return _moduleOrder; }
            set { _moduleOrder = value; }
        }

        public String ModuleTitle
        {
            get { return _title; }
            set { _title = value; }
        }

        public String PaneName
        {
            get { return _pane; }
            set { _pane = value; }
        }

        public int ModuleId
        {
            get { return _id; }
            set { _id = value; }
        }

        public int ModuleDefId
        {
            get { return _defId; }
            set { _defId = value; }
        }

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((ModuleItem) value).ModuleOrder;

            if (ModuleOrder == compareOrder) return 0;
            if (ModuleOrder < compareOrder) return -1;
            if (ModuleOrder > compareOrder) return 1;
            return 0;
        }
    }
}