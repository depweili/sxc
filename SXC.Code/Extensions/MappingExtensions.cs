﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SXC.Code.Extensions
{
    public static class MappingExtensions
    {
        public static PrimitivePropertyConfiguration IsUnique(this PrimitivePropertyConfiguration configuration, string idxname="", int idxorder=0)
        {
            if (!string.IsNullOrEmpty(idxname) && idxorder > 0)
            {
                return configuration.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute(idxname, idxorder) { IsUnique = true }));
            }
            return configuration.HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
