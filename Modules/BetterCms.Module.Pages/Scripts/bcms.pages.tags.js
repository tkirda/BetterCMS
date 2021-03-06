﻿/*jslint unparam: true, white: true, browser: true, devel: true */
/*global define, console */

define('bcms.pages.tags', ['jquery', 'bcms', 'bcms.dynamicContent', 'bcms.siteSettings', 'bcms.inlineEdit', 'bcms.grid'], function ($, bcms, dynamicContent, siteSettings, editor, grid) {
    'use strict';

    var tags = {},
        selectors = {
            deleteTagLink: 'a.bcms-icn-delete',
            addTagButton: '#bcms-site-settings-add-tag',
            tagName: 'a.bcms-tag-name',
            tagOldName: 'input.bcms-tag-old-name',
            tagNameEditor: 'input.bcms-tag-name',
            tagsListForm: '#bcms-tags-form',
            tagsSearchButton: '#bcms-tags-search-btn',
            
            deleteCategoryLink: 'a.bcms-icn-delete',
            addCategoryButton: '#bcms-site-settings-add-category',
            categoryName: 'a.bcms-category-name',
            categoryOldName: 'input.bcms-category-old-name',
            categoryNameEditor: 'input.bcms-category-name',
            categoriesListForm: '#bcms-categories-form',
            categoriesSearchButton: '#bcms-categories-search-btn'
        },
        links = {
            loadSiteSettingsCategoryListUrl: null,
            loadSiteSettingsTagListUrl: null,
            saveTagUrl: null,
            deleteTagUrl: null,
            saveCategoryUrl: null,
            deleteCategoryUrl: null
        },
        globalization = {
            confirmDeleteTagMessage: 'Delete tag?',
            confirmDeleteCategoryMessage: 'Delete category?'
        };

    /**
    * Assign objects to module.
    */
    tags.links = links;
    tags.globalization = globalization;

    /**
    * Retrieves tag field values from table row.
    */
    tags.getTagData = function(row) {
        var tagId = row.find(selectors.deleteTagLink).data('id'),
            tagVersion = row.find(selectors.deleteTagLink).data('version'),
            name = row.find(selectors.tagNameEditor).val();

        return {
            Id: tagId,
            Version: tagVersion,
            Name: name
        };
    };

    /**
    * Search site settings tags
    */
    tags.searchSiteSettingsTags = function (form) {
        grid.submitGridForm(form, function (data) {
            siteSettings.setContent(data);
            tags.initSiteSettingsTagsEvents(data);
        });
    };

    /**
    * Initializes site settings tags list and list items events
    */
    tags.initSiteSettingsTagsEvents = function () {
        var dialog = siteSettings.getModalDialog(),
            container = dialog.container;
        
        var form = dialog.container.find(selectors.tagsListForm);
        grid.bindGridForm(form, function (data) {
            siteSettings.setContent(data);
            tags.initSiteSettingsTagsEvents(data);
        });

        form.on('submit', function (event) {
            event.preventDefault();
            tags.searchSiteSettingsTags(form);
            return false;
        });

        container.find(selectors.tagsSearchButton).on('click', function () {
            tags.searchSiteSettingsTags(form);
        });

        container.find(selectors.addTagButton).on('click', function () {
            editor.addNewRow(container);
        });
        
        editor.initialize(container, {
            saveUrl: links.saveTagUrl,
            deleteUrl: links.deleteTagUrl,
            onSaveSuccess: tags.setTagFields,
            rowDataExtractor: tags.getTagData,
            deleteRowMessageExtractor: function (rowData) {
                return $.format(globalization.confirmDeleteTagMessage, rowData.Name);
            }
        });
    };

    /**
    * Set values, returned from server to row fields
    */
    tags.setTagFields = function (row, json) {
        if (json.Data) {
            row.find(selectors.tagName).html(json.Data.Name);
            row.find(selectors.tagNameEditor).val(json.Data.Name);
            row.find(selectors.tagOldName).val(json.Data.Name);
        }
    };

    /**
    * Retrieves category field values from table row.
    */
    tags.getCategoryData = function (row) {
        var categoryId = row.find(selectors.deleteCategoryLink).data('id'),
            categoryVersion = row.find(selectors.deleteCategoryLink).data('version'),
            name = row.find(selectors.categoryNameEditor).val();

        return {
            Id: categoryId,
            Version: categoryVersion,
            Name: name
        };
    };

    /**
    * Search site settings categories
    */
    tags.searchSiteSettingsCategories = function (form) {
        grid.submitGridForm(form, function (data) {
            siteSettings.setContent(data);
            tags.initSiteSettingsCategoriesEvents(data);
        });
    };

    /**
    * Initializes site settings categories list and list items events
    */
    tags.initSiteSettingsCategoriesEvents = function () {
        var dialog = siteSettings.getModalDialog(),
            container = dialog.container;

        var form = dialog.container.find(selectors.categoriesListForm);
        grid.bindGridForm(form, function (data) {
            siteSettings.setContent(data);
            tags.initSiteSettingsCategoriesEvents(data);
        });

        form.on('submit', function (event) {
            event.preventDefault();
            tags.searchSiteSettingsCategories(form);
            return false;
        });

        container.find(selectors.categoriesSearchButton).on('click', function () {
            tags.searchSiteSettingsCategories(form);
        });

        container.find(selectors.addCategoryButton).on('click', function () {
            editor.addNewRow(container);
        });

        editor.initialize(container, {
            saveUrl: links.saveCategoryUrl,
            deleteUrl: links.deleteCategoryUrl,
            onSaveSuccess: tags.setCategoryFields,
            rowDataExtractor: tags.getCategoryData,
            deleteRowMessageExtractor: function (rowData) {
                return $.format(globalization.confirmDeleteCategoryMessage, rowData.Name);
            }
        });
    };

    /**
    * Set values, returned from server to row fields
    */
    tags.setCategoryFields = function (row, json) {
        if (json.Data) {
            row.find(selectors.categoryName).html(json.Data.Name);
            row.find(selectors.categoryNameEditor).val(json.Data.Name);
            row.find(selectors.categoryOldName).val(json.Data.Name);
        }
    };
    
    /**
      * Loads site settings category list.
      */
    tags.loadSiteSettingsCategoryList = function () {
        dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsCategoryListUrl, {
            contentAvailable: function () {
                tags.initSiteSettingsCategoriesEvents();
            }
        });
    };

    /**
      * Loads site settings tag list.
      */
    tags.loadSiteSettingsTagList = function () {
        dynamicContent.bindSiteSettings(siteSettings, links.loadSiteSettingsTagListUrl, {
            contentAvailable: function () {
                tags.initSiteSettingsTagsEvents();
            }
        });
    };

    /**
    * Initializes tags module.
    */
    tags.init = function () {
        console.log('Initializing bcms.pages.tags module.');
    };
    
    /**
    * Register initialization
    */
    bcms.registerInit(tags.init);
    
    return tags;
});
