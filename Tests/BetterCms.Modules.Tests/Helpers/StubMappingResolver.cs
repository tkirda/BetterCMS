﻿using BetterCms.Core.DataAccess.DataContext;
//using BetterCms.Module.Pages.Models.Maps;
using BetterCms.Module.MediaManager.Models.Maps;
using BetterCms.Module.Pages.Models.Maps;
using BetterCms.Module.Root.Models.Maps;

using FluentNHibernate.Cfg;

namespace BetterCms.Tests.Helpers
{
    public class StubMappingResolver : IMappingResolver
    {
        public void AddAvailableMappings(FluentConfiguration fluentConfiguration)
        {
            fluentConfiguration.Mappings(mc => mc.FluentMappings.AddFromAssemblyOf<AuthorMap>());
            fluentConfiguration.Mappings(mc => mc.FluentMappings.AddFromAssemblyOf<UserMap>());
            fluentConfiguration.Mappings(mc => mc.FluentMappings.AddFromAssemblyOf<MediaMap>());
        }
    }
}
