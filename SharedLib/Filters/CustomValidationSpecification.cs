﻿using GreenPipes;
using System.Collections.Generic;
using System.Linq;

namespace SharedLib.Filters
{
	public class CustomValidationSpecification<T> : IPipeSpecification<T> where T : class, PipeContext
	{
		public IEnumerable<ValidationResult> Validate()
		{
			return Enumerable.Empty<ValidationResult>();
		}

		public void Apply(IPipeBuilder<T> builder)
		{
			builder.AddFilter(new CustomValidationFilter<T>());
		}
	}
}
