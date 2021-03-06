﻿using StarryEyes.Anomaly.TwitterApi.DataModels;

namespace StarryEyes.Models.Backstages.TwitterEvents
{
    public sealed class UnblockedEvent : TwitterEventBase
    {
        public UnblockedEvent(TwitterUser source, TwitterUser target)
            : base(source, target) { }

        public override string Title
        {
            get { return "UNBLOCKED"; }
        }

        public override string Detail
        {
            get { return Source.ScreenName + " -o-> " + TargetUser.ScreenName; }
        }
    }
}
