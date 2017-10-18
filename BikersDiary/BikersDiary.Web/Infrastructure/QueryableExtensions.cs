using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BikersDiary.ForumSystem.Web.App_Start;

namespace BikersDiary.ForumSystem.Web.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<TDestination> MapTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
        {
            return source.ProjectTo(AutoMapperConfig.Configuration, membersToExpand);
        }
    }
}