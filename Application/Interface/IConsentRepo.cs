using Application.Entity;

namespace Application.Interface
{
    public interface IConsentRepo
    {
        Task<string> Create(AcquiescenceEntity data);
        Task<AcquiescenceEntity> GetById(string id);
        Task<List<AcquiescenceEntity>> GetList();
    }
}
