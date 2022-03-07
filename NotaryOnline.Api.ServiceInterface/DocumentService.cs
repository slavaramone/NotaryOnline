using AutoMapper;
using MongoDB.Bson;
using NotaryOnline.Api.ServiceModel;
using NotaryOnline.DataAccess.Interfaces;
using NotaryOnline.Entities;
using ServiceStack;
using SharedLib.Mongo;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NotaryOnline.Api.ServiceInterface
{
	/// <summary>
	/// API сервис работы с документами
	/// </summary>
	public class DocumentService : Service
	{
		private readonly IMapper _mapper;
		private readonly DocumentStorage _docStorage;
		private readonly IOrderDocumentRepository _orderDocumentRepo;

		public DocumentService(DocumentStorage docStorage, IOrderDocumentRepository orderDocumentRepo, IMapper mapper)
		{
			_mapper = mapper ?? throw new ArgumentNullException();
			_docStorage = docStorage ?? throw new ArgumentNullException();
			_orderDocumentRepo = orderDocumentRepo ?? throw new ArgumentNullException();
		}

		/// <summary>
		/// Загрузка документа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Post(UploadDocumentRequest request)
		{
			if (this.Request.Files.Length > 0)
			{
				var uploadedFile = base.Request.Files[0];
				var mongoObj = await _docStorage.UploadFileAsync(uploadedFile.Name, uploadedFile.InputStream);

				var orderDocument = _mapper.Map<OrderDocument>(request);
				orderDocument.DocumentId = mongoObj.ToString();

				await _orderDocumentRepo.Add(orderDocument);

				return new UploadDocumentResponse
				{
					Id = orderDocument.DocumentId
				};
			}
			throw new ArgumentException("Отсутствует содержимое файла");
		}

		/// <summary>
		/// Получение документа
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public async Task<object> Get(GetDocumentRequest request)
		{
			try
			{
				var ms = new MemoryStream();
				var doc = await _orderDocumentRepo.Get(request.DocumentId);
				await _docStorage.DownloadFileAsync(new ObjectId(request.DocumentId), ms);

				ms.Seek(0, SeekOrigin.Begin);

				return new GetDocumentResponse
				{
					Content = ms.ToArray(),
					DocumentName = doc.DocumentName
				};
			}
			catch
			{
				throw new ArgumentException("Файл не найден");
			}
		}
	}
}
