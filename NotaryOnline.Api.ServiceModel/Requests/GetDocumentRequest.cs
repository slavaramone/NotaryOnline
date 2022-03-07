using ServiceStack;
using System.IO;

namespace NotaryOnline.Api.ServiceModel
{
	/// <summary>
	/// Запрос документа
	/// </summary>
	[Route("/document", "GET")]
	public class GetDocumentRequest : IReturn<GetDocumentResponse>
	{
		/// <summary>
		/// Id документа
		/// </summary>
		public string DocumentId { get; set; }
	}
}
