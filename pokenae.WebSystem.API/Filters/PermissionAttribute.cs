using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pokenae.WebSystem.Core.Entities;
using System;
using System.Security.Claims;

namespace pokenae.WebSystem.API.Filters
{
    /// <summary>
    /// アクションメソッドに対する権限を指定するアノテーション
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAttribute : Attribute, IFilterMetadata
    {
        public PermissionLevel RequiredPermission { get; }

        public PermissionAttribute(PermissionLevel requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}
