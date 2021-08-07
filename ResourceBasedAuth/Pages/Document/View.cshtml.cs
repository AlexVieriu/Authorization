using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using ResourceBasedAuth.Models;
using ResourceBasedAuth.Data;
using ResourceBasedAuth.Services;

namespace ResourceBasedAuth.Pages
{
    public class ViewModel : PageModel
    {
        public Document Document { get; private set; }

        private readonly IAuthorizationService _authorizationService;
        private readonly IDocumentRepository _documentRepository;

        public ViewModel(IAuthorizationService authorizationService,
                         IDocumentRepository documentRepository)
        {
            _authorizationService = authorizationService;
            _documentRepository = documentRepository;
        }

        #region snippet_DocumentViewHandler
        public async Task<IActionResult> OnGetAsync(Guid documentId)
        {
            Document = _documentRepository.Find(documentId);

            if (Document == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, Document, Operations.Read);

            if (authorizationResult.Succeeded)
            {
                return Page();
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }
        #endregion
    }
}

