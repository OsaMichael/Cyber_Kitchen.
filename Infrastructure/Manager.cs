using Cyber_Kitchen.Interface;
using Cyber_Kitchen.Manager;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cyber_Kitchen.Infrastructure
{
    public class Manager : NinjectModule
    {
        public override void Load()
        {
            Bind<IVoterManager>().To<VoterManager>();
            Bind<IRestaurantManager>().To<RestaurantManager>();
            Bind<IScoreManager>().To<ScoreManager>();
            Bind<IRatingManager>().To<RatingManager>();
            
        


        }
    }
}