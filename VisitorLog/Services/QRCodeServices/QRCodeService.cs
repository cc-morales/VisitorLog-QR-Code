using VisitorLog.Models;
using VisitorLog.Services.LocalStrorageService;

namespace VisitorLog.Services.QRCodeServices
{
    public class QRCodeService(ILocalStorageProvider localStorageProvider) : IQRCodeService
    {
        private ILocalStorageProvider _LocalStorageProvider => localStorageProvider;

        private const string ListKey = nameof(ListKey);

        private Dictionary<string, VisitorModel> ListOfVisitor = [];

        
        public Task AddVisitorAsync(VisitorModel visitor)
        {
            ListOfVisitor.Add(visitor.QRCode, visitor);

            return UpdateVisitorsAsync();
        }

        public Task DeleteVisitorAsync(VisitorModel visitor)
        {
            ListOfVisitor.Remove(visitor.QRCode);

            return UpdateVisitorsAsync();
        }

        public async Task<Dictionary<string, VisitorModel>> GetVisitorsAsync()
        {
            var results = await localStorageProvider.GetItemAsync<List<VisitorModel>>(ListKey);

            return ListOfVisitor = results.ToDictionary( c => c.QRCode, c => c);
        }

        public async Task UpdateVisitorAsync(VisitorModel visitor)
        {
            if (ListOfVisitor.TryGetValue(visitor.QRCode, out var current)) {

                current = visitor;

                await UpdateVisitorsAsync();

                await GetVisitorsAsync();
            }

        }

        public async Task UpdateVisitorsAsync()
        {
            await _LocalStorageProvider.SetItemAsync(ListKey, ListOfVisitor);
        }
    }
}
