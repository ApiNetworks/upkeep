﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Shell.Infrastructure.Base
{
    public abstract class BaseModule : IModule
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unityContainer">The Unity container.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="regionViewRegistry">The region view registry.</param>
        public BaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            UnityContainer = unityContainer;
            RegionManager = regionManager;
        }

        #endregion Ctor

        #region Interface IModule

        /// <summary>
        /// Initialize module
        /// </summary>
        public virtual void Initialize()
        {

        }

        #endregion Interface IModule

        #region Properties

        /// <summary>
        /// The Unity container
        /// </summary>
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// The region manager
        /// </summary>
        public IRegionManager RegionManager { get; private set; }

        #endregion Properties
    }
}
