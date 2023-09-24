using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storm.Core.ViewComponents
{
	public  class NewsItemSummary : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(object model)
		{
			return await Task.FromResult(View(model));
		}
	}
}
