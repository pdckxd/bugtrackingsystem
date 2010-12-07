using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.UI;

namespace Nairc.KPWPortal
{
    //*********************************************************************
    //
    // PortalModuleControl Class
    //
    // The PortalModuleControl class defines a custom base class inherited by all
    // desktop portal modules within the Portal.
    // 
    // The PortalModuleControl class defines portal specific properties
    // that are used by the portal framework to correctly display portal modules
    //
    //*********************************************************************

    public class PortalModuleControl : UserControl
    {
        // Private field variables

        private ModuleSettings _moduleConfiguration;
        private int _isEditable = 0;
        private int _portalId = 0;
        private Hashtable _settings;

        // Public property accessors

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ModuleId
        {
            get { return (int) _moduleConfiguration.ModuleId; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PortalId
        {
            get { return _portalId; }
            set { _portalId = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEditable
        {
            get
            {
                // Perform tri-state switch check to avoid having to perform a security
                // role lookup on every property access (instead caching the result)

                if (_isEditable == 0)
                {
                    // Obtain PortalSettings from Current Context

                    PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

                    if (portalSettings.AlwaysShowEditButton == true ||
                        PortalSecurity.IsInRoles(_moduleConfiguration.AuthorizedEditRoles))
                    {
                        _isEditable = 1;
                    }
                    else
                    {
                        _isEditable = 2;
                    }
                }

                return (_isEditable == 1);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ModuleSettings ModuleConfiguration
        {
            get { return _moduleConfiguration; }
            set { _moduleConfiguration = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Hashtable Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Configuration.GetModuleSettings(ModuleId);
                }

                return _settings;
            }
        }
    }
}