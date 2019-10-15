using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Updog.Domain.Paging;

namespace Updog.Api {
    public sealed class ContentRangeHeaderAttribute : Attribute, IResultFilter {
        public void OnResultExecuting(ResultExecutingContext context) {
            if (context.Result is ObjectResult objResult) {
                if (objResult.Value is IPagedResultSet resultSet) {
                    int pageStart = resultSet.Pagination.PageNumber * resultSet.Pagination.PageSize;
                    int pageEnd = pageStart + resultSet.Pagination.PageSize - 1;

                    context.HttpContext.Response.Headers.Add("Content-Range", $"{pageStart}-{pageEnd}/{resultSet.Pagination.TotalRecordCount}");

                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context) {
        }
    }
}