﻿using System.Collections.Generic;

using BetterCms.Module.Root.Mvc.Grids;
using BetterCms.Module.Root.Mvc.Grids.GridOptions;

using MvcContrib.Pagination;

namespace BetterCms.Module.Root.ViewModels.SiteSettings
{
    public class SearchableGridViewModel<TModel> where TModel : IEditableGridItem
    {
        /// <summary>
        /// Gets or sets the list of view models.
        /// </summary>
        /// <value>
        /// The list of view models.
        /// </value>
        public IPagination<TModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the grid options.
        /// </summary>
        /// <value>
        /// The grid sort options.
        /// </value>
        public SearchableGridOptions GridOptions { get; set; }

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        /// <value>
        /// The search query.
        /// </value>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableGridViewModel{TModel}" /> class.
        /// </summary>
        public SearchableGridViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableGridViewModel{TModel}" /> class.
        /// </summary>
        /// <param name="items">The models.</param>
        /// <param name="options">The options.</param>
        /// <param name="totalCount">The total count.</param>
        public SearchableGridViewModel(IEnumerable<TModel> items, SearchableGridOptions options, int totalCount)
        {
            Items = new CustomPagination<TModel>(items, options.PageNumber, options.PageSize, totalCount);
            SearchQuery = options.SearchQuery;
            GridOptions = options;
        }
    }
}