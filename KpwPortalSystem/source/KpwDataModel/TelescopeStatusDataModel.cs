using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nairc.KpwFramework.DataModel
{
    /// <summary>
    /// 经度：114.07 纬度：22.62
    /// </summary>
    public class TelescopeStatusDataModel
    {
        public RaPosition RaPos { get; set; }
        public DecPosition DecPos { get; set; }

        public RaOverFlow RaOverflow { get; set; }
        public DecOverFlow DecOverflow { get; set; }
        public bool LevelOverflow { get; set; }
        public MirrorCoverStatus MirrorStatus { get; set; }
        public SearchStarStatus SearchStatus { get; set; }
        public DomeStatus DomeStatus { get; set; }

        public TelescopeStatusDataModel()
        {
            this.RaPos = new RaPosition(0);
            this.DecPos = new DecPosition(0);
        }
    }

    public class RaPosition
    {
        public string PositionString { get; set; }
        public long Position { get; set; }

        public RaPosition(string positionString)
        {
            this.PositionString = positionString;
            this.Position = long.Parse(positionString);
            this.Position = this.Position / 15;
        }

        public RaPosition(long position)
        {
            this.Position = position/15;
            this.PositionString = position.ToString();
        }

        public override string ToString()
        {
            if (this.Position == 0)
                return "0h0m0s";
            long hours = (Position/3600);
            long minutes = (Position - (Position / 3600)*3600) / 60;
            long seconds = Position%60;

            return string.Format("{0}h{1}m{2}s", hours, minutes, seconds);
        }
    }

    public class DecPosition
    {
        public string PositionString { get; set; }
        public long Position { get; set; }
        
        public DecPosition(string positionString)
        {
            this.PositionString = positionString;
            this.Position = long.Parse(positionString);
        }

        public DecPosition(long position)
        {
            this.Position = position;
            this.PositionString = position.ToString();
        }

        public override string ToString()
        {
            if (this.Position == 0)
                return "0°0′0″";
            long p = Math.Abs(this.Position);

            long hours = p / 3600;
            long minutes = (p - hours * 3600) / 60;
            long seconds = p % 60;

            return string.Format("{0}{1}°{2}′{3}″", this.Position > 0 ? "+":"-", hours, minutes, seconds);
        }
    }

    public enum MirrorCoverStatus
    {
        O = 1,
        C,
        R
    }

    public enum SearchStarStatus
    {
        P = 1,
        S,
        F
    }

    public enum DomeStatus
    {
        P = 1,
        S,
        F
    }


    public enum RaOverFlow
    {
        Positive = 1,
        Negative,
        None
    }

    public enum DecOverFlow
    {
        Positive = 1,
        Negative,
        None
    }
}
