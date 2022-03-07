using System.IO;

namespace NotaryOnline.Api.ServiceModel
{
	public class GetDocumentResponse
	{
		public byte[] Content { get; set; }

		public string DocumentName { get; set; }
	}
}
