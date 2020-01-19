using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BuilderGenerator.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var generator = new Core.BuilderGenerator();
            BuilderCode = generator.Generate(SourceCode);
        }

        [BindProperty]
        public string SourceCode { get; set; }

        public string BuilderCode { get; set; }
    }
}
