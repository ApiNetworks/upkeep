﻿using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Shell.Infrastructure.Base
{
    public abstract class BaseViewModel : BindableBase
    {
        public BaseViewModel()
        {
            this.Container = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IUnityContainer>();
            this.RegionManager = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IRegionManager>();
            this.EventAggregator = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<IEventAggregator>();
        }

        #region Properties

        private IUnityContainer unityContainer;

        /// <summary>
        /// The unity container
        /// </summary>
        public IUnityContainer Container
        {
            get { return unityContainer; }
            private set { this.SetProperty<IUnityContainer>(ref this.unityContainer, value); }
        }

        private IRegionManager regionManager;

        /// <summary>
        /// The region manager
        /// </summary>
        public IRegionManager RegionManager
        {
            get { return regionManager; }
            private set { this.SetProperty<IRegionManager>(ref this.regionManager, value); }
        }

        private IEventAggregator eventAggregator;

        /// <summary>
        /// The EventAggregator
        /// </summary>
        public IEventAggregator EventAggregator
        {
            get { return eventAggregator; }
            private set { this.SetProperty<IEventAggregator>(ref this.eventAggregator, value); }
        }

        #endregion Properties
    }
}
