

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace Elixir.SecurityQuestions.Extensions
{
    public class ConfigMaxLength : MaxLengthAttribute
    {
		public ConfigMaxLength(string configKey, IConfigurationRoot config)
			: base(length: Convert.ToInt32(config[configKey]))
		{
		}
    }
}
